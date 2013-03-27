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
    /// Vip 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class Vip : System.Web.Services.WebService
    {

        #region 根据用户id获得用户信息

        /// <summary>
        /// 功能描述：根据用户id获得用户信息
        /// 创建标识：ljd 20120525
        /// </summary>
        /// <param name="userid">用户id</param>      
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetVipInfoByUserID(long userid)
        {
            return Ilog.BLL.Vip.GetModel(userid);

        }

        #endregion

        #region 根据年月日得到天数下拉列表

        /// <summary>
        /// 功能描述：根据年月日得到天数下拉列表
        /// 创建标识：ljd 20120531
        /// </summary>
        /// <param name="year">年</param>  
        /// <param name="month">月</param>  
        /// <param name="day">天</param>  
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetDaysListStr(int year, int month, int day)
        {
            return Ilog.BLL.Vip.GetJsonDaysList(year, month, day);

        }

        #endregion

        #region 根据单位性质id得到行业列表

        /// <summary>
        /// 功能描述：根据单位性质id得到行业列表
        /// 创建标识：ljd 20120531
        /// </summary>
        /// <param name="vccid">单位性质id</param>  
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetFieldListStr(string vccid)
        {
            return Ilog.BLL.Vip.GetFiledListJsonStr(vccid);

        }

        #endregion

        #region 根据单位性质id得到职位列表

        /// <summary>
        /// 功能描述：根据单位性质id得到职位列表
        /// 创建标识：ljd 20120531
        /// </summary>
        /// <param name="vccid">单位性质id</param>  
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetTitleListStr(string vccid)
        {
            return Ilog.BLL.Vip.GetTitleListJsonStr(vccid);

        }

        #endregion

        #region 根据国家id得到省份列表

        /// <summary>
        /// 功能描述：根据国家id得到省份列表
        /// 创建标识：ljd 20120601
        /// </summary>
        /// <param name="countryID">国家id</param>  
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetProvinceListStr(int countryID)
        {
            return Ilog.BLL.Vip.GetPorvinceListJsonStr(countryID);

        }

        #endregion

        #region 根据省份id得到城市列表

        /// <summary>
        /// 功能描述：根据省份id得到城市列表
        /// 创建标识：ljd 20120601
        /// </summary>
        /// <param name="provinceID">省份id</param>  
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetCityListStr(int provinceID)
        {
            return Ilog.BLL.Vip.GetCityListJsonStr(provinceID);

        }

        #endregion

        #region 判断用户的登录状态与手机认证状态

        /// <summary>
        /// 功能描述：判断用户的登录状态与手机认证状态
        /// 创建标识：ljd 20120604
        /// </summary>
        /// <returns>1 登录并手机认证 2未登录 3未认证手机</returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string CheckUserLoginorIlogState()
        {
            return Ilog.BLL.Vip.CheckUserLoginorIlogStateJsonStr();

        }

        #endregion

        #region 得到签到状态

        /// <summary>
        /// 功能描述：得到签到状态
        /// 创建标识：ljd 20120813
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetSignInStatus()
        {
            long userid = Ilog.BLL.VipILog.GetVIPUserID();

            string username = Ilog.BLL.VipILog.GetUserNameByUserId(userid);

            return Ilog.BLL.Vip.GetSignInStatus(username);

        }

        #endregion

        #region 得到该用户可用的邀请码列表

        /// <summary>
        /// 功能描述：得到该用户可用的邀请码列表
        /// 创建标识：ljd 20120813
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetInviteList()
        {
            long userid = Ilog.BLL.VipILog.GetVIPUserID();

            string strInvite = Ilog.BLL.ILogInvitecode.GetInviteListJsonStr(userid);

            return strInvite;

        }

        #endregion


    }
}
