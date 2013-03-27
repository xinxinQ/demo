using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Text;

using Com.ILog.Utils;


namespace ILog.Web.Ajax
{
    public partial class AjaxSendMobile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //手机号
            string mobile = Com.ILog.Utils.IMRequest.GetQueryString("mobile", true);
            long userid = Ilog.BLL.VipILog.GetVIPUserID();

            int mobileUrlState = 0;

            //判断手机号是否已经认证
            int mobileState = Ilog.BLL.Vip.IsMobileExists(userid, mobile, ref mobileUrlState);

            if (mobileState == 1)
            {
                Response.Write("此手机号已被其它用户占用，请重新输入一个新的手机号！");
                return;
            }

            int intimeUrlState = 0;
            //判断短信是否在1分钟内重复发送
            ILog.Model.ILogMobileCheck ooMobileCheckEntity = Ilog.BLL.ILogMobileCheck.GetLastestMobileSendTime(userid, mobile, ref intimeUrlState);
            if (ooMobileCheckEntity != null)
            {
                DateTime dtSendTime = ooMobileCheckEntity.intime;
                TimeSpan tsTime = DateTime.Now - dtSendTime;
                if (tsTime.TotalSeconds < 60)
                {
                    Response.Write("您的短信发送过于频繁，请稍后再试！");
                    return;
                }
            }

            //手机验证码
            long mobileCode = 0;
            Random radomMobile = new Random();
            mobileCode = radomMobile.Next(100000, 999999);

            //短信内容
            string mobileContent = string.Format("{1}年{2}月{3}日，您申请仪器信息网ILOG手机认证，验证码是：{0}，请尽快激活。",
                mobileCode, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            int usernameUrlState = 0;

            //用户名
            string username = Ilog.BLL.Vip.GetUserNameByUserID(userid, ref usernameUrlState);

            //向ilog库中的ilog_MobileCheck中添加一条记录
            ILog.Model.ILogMobileCheck ooMobileCheck = new ILog.Model.ILogMobileCheck();
            ooMobileCheck.im_checkcode = mobileCode.ToString();
            ooMobileCheck.im_mobilenumber = mobile;
            ooMobileCheck.intime = DateTime.Now;
            ooMobileCheck.userid = userid;
            Ilog.BLL.ILogMobileCheck.MobileCheckAdd(ooMobileCheck);

            //发送短信
            int sendUrlState = 0;
            Ilog.BLL.IlogTool.SendMobile(mobile, mobileContent, ref sendUrlState);
            //添加申请日志
            Ilog.BLL.Vip.VIPActiveSleepingAdd(username, 11, ref sendUrlState);

        }
    }
}
