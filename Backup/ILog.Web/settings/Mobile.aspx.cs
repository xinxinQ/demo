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
using ILog.UI;

namespace ILog.Web.settings
{
    public partial class Mobile : BaseWebPage
    {
        /// <summary>
        /// 手机验证码
        /// </summary>
        protected string checkCode = "";

        /// <summary>
        /// 手机号
        /// </summary>
        protected string mobile = "";

        /// <summary>
        /// 原手机号
        /// </summary>
        protected string oldmobile = "";

        //信息提示
        protected string infoScript = "";

        /// <summary>
        /// Get方式
        /// </summary>
        protected bool IsGet = true;

        /// <summary>
        /// Post方式
        /// </summary>
        protected bool IsPost = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            IsGet = IMRequest.IsGet();

            IsPost = IMRequest.IsPost();

            if (IsPost)
            {
                checkCode = IMRequest.GetFormString("CheckNumber", true);

                mobile = IMRequest.GetFormString("mobile", true);

                oldmobile = IMRequest.GetFormString("oldmobile", true);

                //得到用户认证的手机号
               

                //用户id
                long userid = Ilog.BLL.VipILog.GetVIPUserID();

                int intimeUrlState = 0;

                //获得验证码
                ILog.Model.ILogMobileCheck ooMobileCheckEntity = Ilog.BLL.ILogMobileCheck.GetLastestMobileSendTime(userid, mobile, ref intimeUrlState);

                if (ooMobileCheckEntity.im_checkcode != checkCode)
                {
                    infoScript = "<script type=\"text/javascript\">showTipe('手机验证码输入错误！',0);</script>";
                    return;
                }

                ILog.Model.Vip ooVip = Ilog.BLL.Vip.GetUserInfo(userid, ref intimeUrlState);

                //更新vip表中手机状态
                Ilog.BLL.Vip.UpdateVipMobile(userid, mobile, ref intimeUrlState);

                //用户名
                string username = Ilog.BLL.Vip.GetUserNameByUserID(userid, ref intimeUrlState);

                //添加申请记录
                Ilog.BLL.Vip.VIPActiveSleepingAdd(username, 12, ref intimeUrlState);

                //更新vipilog中的认证状态
                Ilog.BLL.VipILog.UpdateVipIlogMobileState(userid, 1, ref intimeUrlState);

                if (ooVip.mobile_time.ToShortDateString() == "0001-1-1")
                {
                    string ip = Utils.GetRealIP();
                    //加积分
                    Ilog.BLL.Vip.ApproveMobile(userid, ip, username);
                    //站短内容
                    StringBuilder strbContent = new StringBuilder();
                    strbContent.AppendFormat("亲爱的{0}：<br/>您好！", username);
                    strbContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;很高兴的通知您，您的手机认证已通过，您的认证积分奖励已发，请查看积分增减历史：<br/>      &nbsp;&nbsp;");
                    strbContent.Append("[url=http://www.instrument.com.cn/vip/Scorelist.asp]积分增减历史[/url]");

                    //发送vip站短
                    Com.ILog.SendMail.Emails.JYSmtpMailToUser(username, "您的手机认证通过!", strbContent.ToString());
                }

                infoScript = "<script type=\"text/javascript\">window.parent.showTipe('保存成功！',1);</script>";

            }


        }
    }
}
