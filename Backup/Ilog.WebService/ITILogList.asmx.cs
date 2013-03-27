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
    /// ITILogList 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class ITILogList : System.Web.Services.WebService
    {
        /// <summary>
        /// 获取当前用户的it博文
        /// </summary>
        /// <param name="RecordCount">当前页码</param>
        /// <param name="PageSize">每页数据</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="ation">查看类型（0：博文，1：评论 ）</param>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetATPageList(string PageCurrent, string PageSize, string ation)
        {
            #region 统一校验

            long loUserId = BLL.VipILog.GetVIPUserID();

            int PageCurrent_;
            int PageSize_;
            string ation_;

            if (!ILog.Common.Common.Int_IsType(PageCurrent))
            {
                PageCurrent_ = 1;
            }
            else
            {
                PageCurrent_ = Convert.ToInt32(PageCurrent);
            }
            if (!ILog.Common.Common.Int_IsType(PageSize))
            {
                PageSize_ = 45;
            }
            else
            {
                PageSize_ = Convert.ToInt32(PageSize);
            }
            if (!ILog.Common.Common.Int_IsType(ation))
            {
                ation_ = "0";
            }
            else
            {
                ation_ = ation;
            }

            #endregion

            return Ilog.BLL.ILogat.GetATPageList(PageCurrent_, PageSize_, ation_, loUserId);
        }

        /// <summary>
        /// 搜索列表
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">一页多少条数据</param>
        /// <param name="keyword">搜索关键字</param>
        /// <param name="ation">查看类型（0：博文，1：评论 ）</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetIlSearchList(string PageCurrent, string PageSize, string keyword, string ation) 
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


            //if (Ilog.BLL.Vip.IsExistsVIP(loUserId, ref state) == 0)
            //{
            //    HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            //}


            int PageCurrent_;
            int PageSize_;
            string ation_;

            if (!ILog.Common.Common.Int_IsType(PageCurrent))
            {
                PageCurrent_ = 1;
            }
            else
            {
                PageCurrent_ = Convert.ToInt32(PageCurrent);
            }
            if (!ILog.Common.Common.Int_IsType(PageSize))
            {
                PageSize_ = 45;
            }
            else
            {
                PageSize_ = Convert.ToInt32(PageSize);
            }
            if (!ILog.Common.Common.Int_IsType(ation))
            {
                ation_ = "0";
            }
            else
            {
                ation_ = ation;
            }
            if (!ILog.Common.Common.ISProcessSqlStr(keyword))
            {
                return Ilog.BLL.ILogat.GetNotJsonList();
            }

            #endregion

            return Ilog.BLL.ILogat.GetSearchList(PageCurrent_, PageSize_, keyword, ation_, loUserId);
        }

        /// <summary>
        /// 功能描述："提到我的"页面的智能搜索用户
        /// 创建标识：ljd 20120718
        /// </summary>
        /// <param name="keyword">搜索关键字</param>
        /// <param name="ation">查看类型（0：博文，1：评论 ）</param>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetATUserList(string keyword,int ation)
        {
            BLL.VipILog.IsLoginVIP();

            long userid = BLL.VipILog.GetVIPUserID();

            return Ilog.BLL.ILogat.GetAtUserListJsonStr(keyword,ation,userid);

        }


    }
}
