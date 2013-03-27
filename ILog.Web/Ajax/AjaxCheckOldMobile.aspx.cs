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

using Com.ILog.Utils;

namespace ILog.Web.Ajax
{
    public partial class AjaxCheckOldMobile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int type = IMRequest.GetQueryInt("type", 0);

            long userid = Ilog.BLL.VipILog.GetVIPUserID();

            if (type == 0)//校验输入的原手机号是否正确
            {
                string oldmobile = IMRequest.GetQueryString("oldmobile", true);

                string mobile = Ilog.BLL.Vip.GetCheckedMobile(userid);

                string isRight = "false";

                if (mobile == oldmobile)
                {
                    isRight = "true";
                }
                Response.Write(isRight);
            }
            else if (type == 1)//校验输入的手机验证码是否正确
            {
                string checkNum = IMRequest.GetQueryString("CheckNumber", true);

                string mobile = IMRequest.GetQueryString("mobile", true);

                int intimeUrlState = 0;

                //获得验证码
                ILog.Model.ILogMobileCheck ooMobileCheckEntity = Ilog.BLL.ILogMobileCheck.GetLastestMobileSendTime(userid, mobile, ref intimeUrlState);

                string isRight = "false";

                if (ooMobileCheckEntity.im_checkcode == checkNum)
                {
                    isRight = "true";
                }
                Response.Write(isRight);
            }
            else if (type == 2)//校验输入的手机号码是否被占用
            {
                string mobile = IMRequest.GetQueryString("mobile", true);

                int mobileUrlState = 0;
                //判断手机号是否已经认证

                int mobileState = Ilog.BLL.Vip.IsMobileExists(userid, mobile, ref mobileUrlState);
                string isRight = "false";

                if (mobileState == 0)
                {
                    isRight = "true";
                }
                Response.Write(isRight);
            }


        }


    }
}
