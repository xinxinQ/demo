using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ILog.UI
{
    public class BaseWebPage : System.Web.UI.Page
    {
        /// <summary>
        /// 当前管理员
        /// </summary>
        protected string CurrentUserId;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            CurrentUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            if (string.IsNullOrEmpty(CurrentUserId))
            {
                HttpContext.Current.Response.Write("<script language=\"JavaScript\" type=\"text/javascript\">");
                //HttpContext.Current.Response.Write("window.parent.location.href='http://www.instrument.com.cn/vip/login.asp?strURL=" + Com.ILog.Utils.Utils.UrlDecode("http://www.instrument.com.cn/ilog/") + "';");
                HttpContext.Current.Response.Write("window.parent.location.href='/index.html';");
                HttpContext.Current.Response.Write("</script>");
                HttpContext.Current.Response.End();
            }
        }
    }
}
