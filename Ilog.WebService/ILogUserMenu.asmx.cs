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
using Com.ILog.Utils;


namespace Ilog.WebService
{
    /// <summary>
    /// ILogUserMenu 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
     [System.Web.Script.Services.ScriptService]
    public class ILogUserMenu : System.Web.Services.WebService
    {

        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetUserMenu(int MenuLive)
        {

            #region 统一校验

            //是否登陆
            //ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");


            long loUserId = 0;

            if (!string.IsNullOrEmpty(strUserId))
            {
                 loUserId = Convert.ToInt64(strUserId);
            }



            int state = 0;


            string username = Ilog.BLL.VipILog.GetNickNameByUserId(loUserId);


            #endregion
            StringBuilder sb = new StringBuilder();

            sb.Append("{UserMenu:");

            try
            {

                if (loUserId != 0)
                {
                    sb.Append("[{UrlState:'1'},");

                    sb.Append("{MenuName:'广场',MenuUrl:'/index.html',MenuLive:'0'},");

                    if (MenuLive == 1)
                    {

                        sb.Append("{MenuName:'我的首页',MenuUrl:'/H',MenuLive:'1'},");

                    }
                    else
                    {
                        sb.Append("{MenuName:'我的首页',MenuUrl:'/h',MenuLive:'0'},");
                    }
                    //截取username的长度
                    username = Utils.GetSubString(username, 14,"...");
                    sb.Append("{MenuName:'" + username + "',MenuUrl:'/u',MenuLive:'0'},");

                    sb.Append("{MenuName:'消息',MenuUrl:'javascript:void(0);',MenuLive:'0'},");

                    sb.Append("{MenuName:'帐号',MenuUrl:'javascript:void(0);',MenuLive:'0'}");
                }
                else
                {
                    sb.Append("[{UrlState:'2'},");

                    sb.Append("{MenuName:'广场',MenuUrl:'/index.html',MenuLive:'0'},");

                    sb.Append("{MenuName:'注册',MenuUrl:'http://www.instrument.com.cn/vip/Register01.asp?registerSource=16',MenuLive:'0'},");

                    sb.Append("{MenuName:'登录',MenuUrl:'javascript:',MenuLive:'0'}");
                }
            }
            catch
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetUserMenuUpList()
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




            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{UserMenuList:");

            try
            {
              sb.Append(Ilog.BLL.VipILogCount.GetModelTopMessageJson(loUserId));
            }
            catch(Exception e)
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();

        }



        /// <summary>
        /// 广场下拉列表
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetUserMenuGuangChangUpList()
        {

            #region 统一校验

            //是否登陆
            //ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");


            long loUserId = 0;

            if (!string.IsNullOrEmpty(strUserId))
            {
                loUserId = Convert.ToInt64(strUserId);
            }



            int state = 0;




            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{UserMenuList:");

            try
            {

                    sb.Append(Ilog.BLL.VipILogCount.GetModelTopGuangChnagJson());

            }
            catch (Exception e)
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();

        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetUserMenuOutList()
        {

            #region 统一校验

            //是否登陆
            //ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");


            long loUserId = 0;

            if (!string.IsNullOrEmpty(strUserId))
            {
                loUserId = Convert.ToInt64(strUserId);
            }





            string username = Ilog.BLL.VipILog.GetNickNameByUserId(loUserId);


            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{UserMenuList:");

            try
            {
                if (loUserId != 0)
                {
                    sb.Append(Ilog.BLL.VipILogCount.GetModelTopMessageJsonOut(loUserId));
                }
                else
                {
                    sb.Append("[{UrlState:'2'}");  
                }
            }
            catch (Exception e)
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();

        }



        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetUserSettingsUpList()
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




            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{UserMenuList:");

            try
            {
                sb.Append(Ilog.BLL.VipILogCount.GetModelTopUserSettingsJson(loUserId));
            }
            catch (Exception e)
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();

        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetUserOpenCardInfo()
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




            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{UserMenuList:");

            try
            {
                string month= DateTime.Now.Month.ToString();

                if (month.Length == 1) { month = "0" + month; }

                string day=DateTime.Now.Day.ToString();

                if (day.Length == 1) { day = "0" + day; }

                string[] weekdays = { "周日", "周一", "周二", "周三", "周四", "周五", "周六" };

                string Week = weekdays[Convert.ToInt32(DateTime.Now.DayOfWeek)];



                sb.Append("[{UrlState:'1'},");

                sb.Append("{Month:'" + month + "',Day:'" + day + "',Week:'" + Week + "'}");
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
