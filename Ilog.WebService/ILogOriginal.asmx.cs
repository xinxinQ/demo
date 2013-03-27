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
    /// ILogOriginal 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class ILogOriginal : System.Web.Services.WebService
    {
        #region 搜索博文
        /// <summary>
        /// 搜索（找人）列表
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetSrechList(string PageCurrent, string PageSize, string keyword)
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

            if (string.IsNullOrEmpty(keyword))
            {
                return Ilog.BLL.ILogOriginal.GetNotJsonList();
            }

            if (!ILog.Common.Common.ISProcessSqlStr(keyword))
            {
                return Ilog.BLL.ILogOriginal.GetNotJsonList();
            }

            #endregion

            return Ilog.BLL.ILogOriginal.GetSearchList(PageCurrent_, PageSize_, keyword);
        }
        #endregion

        #region 找文章（搜搜页面）
        /// <summary>
        /// 找文章（搜搜页面）
        /// </summary>
        ///<param name="Originaltitle">内容信息</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetSearchOriginalInfo(string Originaltitle)
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

            if (!ILog.Common.Common.ISProcessSqlStr(Originaltitle))
            {
                return Ilog.BLL.ILogOriginal.GetNotJsonList();
            }

            return Ilog.BLL.ILogOriginal.GetSearchOriginalInfo(Originaltitle);

            #endregion
        }
        #endregion


        #region 首页博文
        /// <summary>
        /// 首页博文
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetList(string PageCurrent, string PageSize)
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


            int PageCurrent_;
            int PageSize_;

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

            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                return Ilog.BLL.ILogOriginal.GetNotJsonList();
            }
            else
            {
                loUserId = Convert.ToInt64(strUserId);
            }

            #endregion

            return Ilog.BLL.ILogOriginal.GetList(PageCurrent_, PageSize_, loUserId);

            //return Ilog.BLL.ILogOriginal.GetList(PageCurrent_,PageSize_, 2469409);
        }
        #endregion


        #region 首页博文（他人主页专用）
        /// <summary>
        /// 首页博文
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetList_He(string PageCurrent, string PageSize,string userid)
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


            int PageCurrent_;
            int PageSize_;

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

            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                return Ilog.BLL.ILogOriginal.GetNotJsonList();
            }
            else
            {
                loUserId = Convert.ToInt64(strUserId);
            }

            if (!ILog.Common.Common.ISProcessSqlStr(userid))
            {
                return Ilog.BLL.ILogSpread.GetNotJsonList();
            }

            #endregion

            long userid_ = Convert.ToInt64(userid);

            return Ilog.BLL.ILogOriginal.GetList(PageCurrent_, PageSize_, userid_);
        }
        #endregion


        #region 搜索自己的博文（个人主页专用）
        /// <summary>
        /// 首页博文
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetSearchList2(string PageCurrent, string PageSize, string keyword)
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


            int PageCurrent_;
            int PageSize_;

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

            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                return Ilog.BLL.ILogOriginal.GetNotJsonList();
            }
            else
            {
                loUserId = Convert.ToInt64(strUserId);
            }

            if (!ILog.Common.Common.ISProcessSqlStr(keyword))
            {
                return Ilog.BLL.ILogComment.GetNotJsonList();
            }

            #endregion

            return Ilog.BLL.ILogOriginal.GetSearchList(PageCurrent_, PageSize_, loUserId, keyword);

            //return Ilog.BLL.ILogOriginal.GetSearchList(PageCurrent_, PageSize_, 2469409, keyword);
        }
        #endregion

        #region 搜索自己的博文（他人主页专用）
        /// <summary>
        /// 首页博文
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="keyword">搜索关键字</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetSearchList2_He(string PageCurrent, string PageSize, string keyword, string userid)
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


            int PageCurrent_;
            int PageSize_;

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

            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                return Ilog.BLL.ILogOriginal.GetNotJsonList();
            }
            else
            {
                loUserId = Convert.ToInt64(strUserId);
            }

            if (!ILog.Common.Common.ISProcessSqlStr(keyword))
            {
                return Ilog.BLL.ILogComment.GetNotJsonList();
            }

            if (!ILog.Common.Common.ISProcessSqlStr(userid))
            {
                return Ilog.BLL.ILogSpread.GetNotJsonList();
            }

            #endregion

            long userid_ = Convert.ToInt64(userid);

            return Ilog.BLL.ILogOriginal.GetSearchList(PageCurrent_, PageSize_, userid_, keyword);
        }
        #endregion


        #region 根据博文ID获取原创信息

        /// <summary>
        /// 根据编号获取用户信息(home)
        /// </summary>       
        /// <param name="io_id">流水号</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetOriginalInfoById(int ioId)
        {

            return Ilog.BLL.ILogOriginal.GetOriginalInfoById(ioId);

        }



        #endregion 


        #region 得到热门评论博文
        /// <summary>
        /// 功能描述：得到热门评论博文
        /// 创建标识：ljd 20120626
        /// </summary>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetHotCommentList()
        {
            string hotCommentList = Ilog.BLL.ILogOriginal.GetHotCommentOriginalListJsonStr();

            return hotCommentList;

        }

        #endregion


        #region 得到最热的前两条热转博文

        /// <summary>
        /// 功能描述：得到最热的前两条热转博文
        /// 创建标识：ljd 20120716
        /// </summary>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetHotTransmitTopTwoList()
        {
            string hotSpreadList = Ilog.BLL.ILogOriginal.GetHotSpreadOriginalListJsonStr();

            return hotSpreadList;

        }

        #endregion

        #region 根据原创id得到原创的详细信息和图片信息

        /// <summary>
        /// 功能描述：根据原创id得到原创的详细信息和图片信息
        /// 创建标识：ljd 20120628
        /// </summary>
        /// <param name="id">原创id</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetOriginalInfoByID(long id)
        {
            string originalInfo = Ilog.BLL.ILogOriginal.GetOriginalInfoJsonStr(id);
            return originalInfo;

        }

        #endregion


    }
}
