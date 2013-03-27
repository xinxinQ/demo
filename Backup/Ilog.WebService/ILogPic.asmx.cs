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

namespace Ilog.WebService
{
    /// <summary>
    /// ILogPic 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class ILogPic : System.Web.Services.WebService
    {

        #region 获取博文中的图片
        /// <summary>
        /// 获取博文中的图片
        /// </summary>
        /// <param name="IoId">博文id</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetPic(string IoId)
        {
            #region 统一校验

            //if (Ilog.BLL.Vip.IsExistsVIP(loUserId, ref state) == 0)
            //{
            //    HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            //}

            long IoId_ = 0;

            if (!ILog.Common.Common.Int_IsType(IoId))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }
            else
            {
                IoId_ = Convert.ToInt64(IoId);
            }

            return Ilog.BLL.ILogOriginal.GetJsonList_Pic(IoId_);

            #endregion

        }
        #endregion
    }
}
