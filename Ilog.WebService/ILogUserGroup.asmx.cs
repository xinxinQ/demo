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
    /// ILogUserGroup 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
     [System.Web.Script.Services.ScriptService]
    public class ILogUserGroup : System.Web.Services.WebService
    {

        /// <summary>
        /// 加载关注项部菜单
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetGroupName(long groupID)
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

            var GetJson="";

            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{GroupList:");

            try
            {



                GetJson = Ilog.BLL.ILogConcernGroup.GetJsonGroupName(groupID);
                //获取全部用户组
                if(!string.IsNullOrEmpty(GetJson))
                {
                    sb.Append("[{UrlState:'1'},");
                    
                    sb.Append(GetJson);
                }
                else
                {
                   sb.Append("[{UrlState:'0'}");
                }


            }
            catch (Exception e)
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();
        }
    }
}
