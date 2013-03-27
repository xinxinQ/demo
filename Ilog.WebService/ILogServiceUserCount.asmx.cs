using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Services;
using System.Text;

namespace Ilog.WebService
{
    /// <summary>
    /// ILogServiceUserCount 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class ILogServiceUserCount : System.Web.Services.WebService
    {

        

        /// <summary>
        /// 获取用户关注，粉丝，微博数量
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogServiceUserNumCount()
        {
            #region 统一校验

            //是否登陆
            ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            //校验数据类型
            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }
            //非法用户

            long loUserId = Convert.ToInt64(strUserId);

            int state = 0;

            //if (Ilog.BLL.Vip.IsExistsVIP(loUserId, ref state) == 0)
            //{
            //    HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            //}


           
            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{IlogCount:");

            try
            {

                sb.Append("[");
               sb.Append(Ilog.BLL.VipILogCount.GetModelJsonCount(loUserId));




            }
            catch
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();
        }

        /// <summary>
        ///将粉丝关注清零
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogServiceUserOutFanZero()
        {
            #region 统一校验

            //是否登陆
            ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            //校验数据类型
            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }
            //非法用户

            long loUserId = Convert.ToInt64(strUserId);

            int state = 0;

            //if (Ilog.BLL.Vip.IsExistsVIP(loUserId, ref state) == 0)
            //{
            //    HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            //}



            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{IlogCount:");

            try
            {

                sb.Append("[");
                sb.Append(Ilog.BLL.VipILogCount.GetFanOutZeroJson(loUserId));




            }
            catch
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();
        }
    }
}
