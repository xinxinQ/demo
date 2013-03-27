using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;

namespace Ilog.WebService
{
    /// <summary>
    /// ILog_Spread 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class ILog_Spread : System.Web.Services.WebService
    {

        #region 获取所有博文
        /// <summary>
        /// 获取所有博文
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetAllList(string PageCurrent, string PageSize, int type)
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

            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                return Ilog.BLL.ILogSpread.GetNotJsonList();
            }
            else
            {
                loUserId = Convert.ToInt64(strUserId);
            }

            #endregion

            return Ilog.BLL.ILogSpread.GetAllList(PageCurrent_, PageSize_, loUserId, type);

            //return Ilog.BLL.ILogSpread.GetAllList(PageCurrent_, PageSize_, 1001858);
        }
        #endregion

        #region 获取所有博文
        /// <summary>
        /// 获取所有博文
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetSerchSpreadAllList(string PageCurrent, string PageSize, string keyword)
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

            if (!ILog.Common.Common.ISProcessSqlStr(keyword))
            {
                return Ilog.BLL.ILogSpread.GetNotJsonList();
            }

            #endregion

            return Ilog.BLL.ILogSpread.GetSerchSpreadAllList(PageCurrent_, PageSize_, keyword);

            //return Ilog.BLL.ILogSpread.GetAllList(PageCurrent_, PageSize_, 1001858);
        }
        #endregion

        #region 根据搜索关键字随机显示用户
        /// <summary>
        /// 根据搜索关键字随机显示用户
        /// </summary>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetSearchPersonalInfo(string keyword)
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


            if (!ILog.Common.Common.ISProcessSqlStr(keyword))
            {
                return Ilog.BLL.ILogSpread.GetNotJsonList();
            }

            #endregion

            return Ilog.BLL.ILogSpread.GetSearchPersonalInfo(keyword, loUserId);

            //return Ilog.BLL.ILogSpread.GetAllList(PageCurrent_, PageSize_, 1001858);
        }
        #endregion


        #region 获取所有博文（他人主页）
        /// <summary>
        /// 获取所有博文
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetAllList_He(string PageCurrent, string PageSize, string userid)
        {
            #region 统一校验

            //是否登陆
            //ILog.Common.Common.IsLogin();

            //string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            ////校验数据类型
            //if (!ILog.Common.Common.Int_IsType(strUserId))
            //{
            //    HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            //}
            ////非法用户

            //long loUserId = Convert.ToInt64(strUserId);

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

            if (!ILog.Common.Common.Int_IsType(userid))
            {
                return Ilog.BLL.ILogSpread.GetNotJsonList();
            }


            if (!ILog.Common.Common.ISProcessSqlStr(userid))
            {
                return Ilog.BLL.ILogSpread.GetNotJsonList();
            }

            #endregion

            long userid_ = Convert.ToInt64(userid);

            return Ilog.BLL.ILogSpread.GetAllList2(PageCurrent_, PageSize_, userid_, 1);
        }
        #endregion


        #region 获取所有博文
        /// <summary>
        /// 获取所有博文
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetSerchaAllList(string PageCurrent, string PageSize, string keyword)
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
                return Ilog.BLL.ILogSpread.GetNotJsonList();
            }

            #endregion

            return Ilog.BLL.ILogSpread.GetSearchAllList(PageCurrent_, PageSize_, loUserId, keyword);
        }
        #endregion


        #region 获取所有博文（他人主页专用）
        /// <summary>
        /// 获取所有博文
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetSerchaAllList_He(string PageCurrent, string PageSize, string keyword, string userid)
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
                return Ilog.BLL.ILogSpread.GetNotJsonList();
            }

            if (!ILog.Common.Common.ISProcessSqlStr(userid))
            {
                return Ilog.BLL.ILogSpread.GetNotJsonList();
            }

            #endregion

            long userid_ = Convert.ToInt64(userid);

            return Ilog.BLL.ILogSpread.GetSearchAllList(PageCurrent_, PageSize_, userid_, keyword);
        }
        #endregion


        #region 博文内页转发列表. by lx on 20120627

        /// <summary>
        /// 获取转发列表
        /// </summary>
        /// <param name="ioId">博文Id</param>
        /// <param name="type">类型</param>
        /// <param name="PageCurrent">当前页</param>
        /// <param name="PageSize">页码</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetForwordList(int isoId, int type, int PageCurrent, int PageSize)
        {

            return Ilog.BLL.ILogSpread.GetForwordList(isoId, type, PageCurrent, PageSize);

        }


        /// <summary>
        /// 获取博文内页转发列表. by lx on 20120719
        /// </summary>
        /// <param name="ioId">博文Id</param>
        /// <param name="type">类型</param>
        /// <param name="PageCurrent">当前页</param>
        /// <param name="PageSize">页码</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetContentForwordPageList(int isoId, int type, int pageCurrent, int pageSize)
        {

            return Ilog.BLL.ILogSpread.GetContentForwordPageList(isoId, type, pageCurrent, pageSize);

        }


        #endregion


        #region 根据博文ID删除博文

        /// <summary>
        /// 删除一条博文.by lx on 20120704
        /// <param name="isid">编号</param>
        /// <param name="userid">用户id</param>
        /// </summary>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogSpreadDeleteById(long isid)
        {

            return Ilog.BLL.ILogSpread.SpreadDel(isid);

        }


        #endregion

        #region 得到大家正在说的内容列表
        /// <summary>
        /// 功能描述：得到最新的博文列表（大家正在说）（json格式）
        /// 创建标识：ljd 20120705
        /// </summary>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetNewSpreadListJsonStr()
        {
            string ilogContent = Ilog.BLL.ILogSpread.GetNewSpreadListJsonStr();
            return ilogContent;

        }
        #endregion

        #region 得到热门转发的博文列表的json字符串
        /// <summary>
        /// 功能描述：得到热门转发的博文列表的json字符串
        /// 创建标识：ljd 20120705
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="type">0 每日 1每周</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetHotSpreadListJsonStr(int PageCurrent, int PageSize, int type)
        {
            string ilogList = Ilog.BLL.ILogSpreadhistory.GetYesterdaySpreadJsonList(PageCurrent, PageSize, type);
            return ilogList;

        }
        #endregion

        #region 得到最新原创
        /// <summary>
        /// 功能描述：得到正在发生的博文列表的json字符串
        /// 创建标识：zhangl 20120712
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="type">0 每日 1每周</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetTodaySpreadListJsonStr(int PageCurrent, int PageSize)
        {
            string ilogList = Ilog.BLL.ILogOriginal.GetTodaySpreadJsonList(PageCurrent, PageSize);
            return ilogList;

        }
        #endregion

        #region person页获取所有博文
        /// <summary>
        /// 功能描述：person页获取所有博文
        /// 创建标识：ljd 20120809
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetAllList2(string PageCurrent, string PageSize, int type)
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

            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                return Ilog.BLL.ILogSpread.GetNotJsonList();
            }
            else
            {
                loUserId = Convert.ToInt64(strUserId);
            }

            #endregion

            return Ilog.BLL.ILogSpread.GetAllList2(PageCurrent_, PageSize_, loUserId, type);

            //return Ilog.BLL.ILogSpread.GetAllList(PageCurrent_, PageSize_, 1001858);
        }
        #endregion


    }
}
