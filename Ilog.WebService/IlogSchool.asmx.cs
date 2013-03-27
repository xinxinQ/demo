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
    /// IlogSchool 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class IlogSchool : System.Web.Services.WebService
    {

        /// <summary>
        /// 功能描述：根据用户id获得用户信息
        /// 创建标识：ljd 20120525
        /// </summary>
        /// <param name="id">学校id</param>      
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogSchoolDelete(long id)
        {
            return Ilog.BLL.ILogSchool.DeleteSchoolByWebservice(id);

        }

        /// <summary>
        /// 功能描述：根据用户id获得学校列表
        /// 创建标识：ljd 20120525
        /// </summary>   
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogSchoolGetList()
        {
            long userid = Ilog.BLL.VipILog.GetVIPUserID();

            return Ilog.BLL.ILogSchool.GetSchoolListJsonStr(userid);

        }

        /// <summary>
        /// 功能描述：根据省份id获得学校列表
        /// 创建标识：ljd 20120525
        /// </summary>
        /// <param name="provid">省份id</param>      
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogSchoolGetListByProvID(int provid)
        {
            return Ilog.BLL.ILogSchool.GetSchoolListJsonStrByProvID(provid);

        }

    }
}
