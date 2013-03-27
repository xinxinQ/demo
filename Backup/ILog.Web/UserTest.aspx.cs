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

namespace ILog.Web
{
    public partial class UserTest : System.Web.UI.Page
    {
        /// <summary>
        /// 用户列表字符串
        /// </summary>
        protected string strUserList = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            string userid = IMRequest.GetQueryString("userid", false);

            int state = IMRequest.GetQueryInt("state", -2);

            int to = IMRequest.GetQueryInt("to", 0);

            int urlState = 0;

            if (to == 1)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                Response.Cookies.Clear();
                HttpCookie aCookie;
                string cookieName;
                int limit = Request.Cookies.Count;
                //for (int i = 0; i < limit; i++)
                //{
                //    cookieName = Request.Cookies[i].Name;
                //    aCookie = new HttpCookie(cookieName);
                //    aCookie.Expires = DateTime.Now.AddDays(-1);
                //    Response.Cookies.Add(aCookie);
                //}

              //  CurrentCookie.ClearCookieByKey("useid","","");

                CurrentCookie.SetCookie("useid", userid.ToString());

                string validateCode = Utils.MD5(userid + "-instrument_4077_20091124").ToLower();
                CurrentCookie.SetCookie("CheckValid", validateCode);

                Response.Redirect("/home.aspx");
            }

            if (state == -1)
            {
                ILog.Model.Vip ooVip = Ilog.BLL.Vip.GetUserInfo(Convert.ToInt64(userid), ref urlState);
                Ilog.BLL.VipILog.CreateVipIlogAndInit(ooVip, ref urlState);
            }

            if (state == 0 || state == 2)
            {
                Ilog.BLL.VipILog.UpdateVipIlogMobileState(Convert.ToInt64(userid), 1, ref urlState);
            }

            strUserList = Ilog.BLL.Vip.GetDemoUserListStr();

        }
    }
}
