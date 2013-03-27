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
    /// ILogComment 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class ILogComment : System.Web.Services.WebService
    {
        #region 获取评论列表
        /// <summary>
        /// 获取评论列表
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">页面数据条数</param>
        /// <param name="ation">查看类型（1：收到的评论，0：发出的评论 ）</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetUserCommentList(string PageCurrent, string PageSize, string ation)
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

            if (Ilog.BLL.Vip.IsExistsVIP(loUserId, ref state) == 0)
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }


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

            return Ilog.BLL.ILogComment.GetCommentPageList(PageCurrent_, PageSize_, ation_, loUserId);
        }
        #endregion

        #region
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        //搜索关键字
        public string GetSearchCommentList(string PageCurrent, string PageSize, string keyword, string ation)
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

            if (Ilog.BLL.Vip.IsExistsVIP(loUserId, ref state) == 0)
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }

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
                ation_ = "1";
            }
            else
            {
                ation_ = ation;
            }
            if (!ILog.Common.Common.ISProcessSqlStr(keyword))
            {
                return Ilog.BLL.ILogComment.GetNotJsonList();
            }

            #endregion

            return Ilog.BLL.ILogComment.GetSearchList(PageCurrent_, PageSize_, keyword, ation_, loUserId);
        }
        #endregion


        #region 博文内页评论列表. by lx on 20120627

        /// <summary>
        /// 博文下拉数据. by lx on 20120628
        /// </summary>        
        /// <param name="RecordCount">总页数</param>
        /// <param name="currentid">文章ID</param>
        /// <param name="type">评论类别 1原创 2转发</param>
        /// <param name="RecordCount">当前页码</param>
        /// <param name="PageSize">每页数据</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetCommentPageList(int currentid, int type, int PageCurrent, int PageSize)
        {

            return Ilog.BLL.ILogComment.GetBlogCommentPageList(currentid, type, PageCurrent, PageSize);

        }


        /// <summary>
        /// 博文下拉数据. by lx on 20120628
        /// </summary>        
        /// <param name="RecordCount">总页数</param>
        /// <param name="currentid">文章ID</param>
        /// <param name="type">评论类别 1原创 2转发</param>
        /// <param name="RecordCount">当前页码</param>
        /// <param name="PageSize">每页数据</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetContentCommentPageList(int currentid, int type, int PageCurrent, int PageSize)
        {

            return Ilog.BLL.ILogComment.GetContentCommentPageList(currentid, type, PageCurrent, PageSize);

        }


        #endregion

        #region 得到热门评论的博文列表的json字符串
        /// <summary>
        /// 功能描述：得到热门评论的博文列表的json字符串
        /// 创建标识：ljd 20120705
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="type">0 每日 1每周</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetHotCommentListJsonStr(int PageCurrent, int PageSize, int type)
        {
            string ilogList = Ilog.BLL.ILogCommenthistory.GetYesterdayCommentJsonList(PageCurrent, PageSize, type);
            return ilogList;

        }
        #endregion

        #region 评论页面搜索只能提示
        /// <summary>
        ///评论页面搜索只能提示
        /// </summary>
        ///<param name="commentinfo">关键字</param>
        ///<param name="ation">操作类型查看类型（1：收到的评论，0：发出的评论 ）</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetSearchCommentInfo(string commentinfo,int ation)
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

            if (Ilog.BLL.Vip.IsExistsVIP(loUserId, ref state) == 0)
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }

            if (!ILog.Common.Common.ISProcessSqlStr(commentinfo))
            {
                return Ilog.BLL.ILogOriginal.GetNotJsonList();
            }

            return Ilog.BLL.ILogComment.GetSearchCommentInfo(commentinfo, ation, loUserId);

            #endregion
        }
        #endregion

        //#region 删除一条回复
        ///// <summary>
        ///// 功能描述：删除一条回复
        ///// 创建标识：ljd 20120708
        ///// </summary>
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        //[WebMethod]
        //public string ILogReplyDeleteById(long icid)
        //{

        //    return Ilog.BLL.ILogComment.CommentDel(icid, userid);

        //}

        //#endregion


    }
}
