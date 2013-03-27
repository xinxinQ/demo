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
    /// ILogFollow 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
     [System.Web.Script.Services.ScriptService]
    public class ILogFollow : System.Web.Services.WebService
    {
    
        /// <summary>
        /// 加载关注页面内菜单
        /// zhangl 20120705
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogFollowGetTopMenu(int MenuLive)
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



            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{FollowMenu:");

            try
            {
                sb.Append("[{UrlState:'1'},");

                if (MenuLive == 1)
                {
                    sb.Append("{MenuName:'全部',MenuID:'0',MenuLive:'1'},");
                }
                else
                {
                    sb.Append("{MenuName:'全部',MenuID:'0',MenuLive:'0'},");
                }

                if (MenuLive == 2)
                {

                    sb.Append("{MenuName:'互相关注',MenuID:'0',MenuLive:'2'},");
                }
                else
                {
                    sb.Append("{MenuName:'互相关注',MenuID:'0',MenuLive:'0'},");
                }



                if (MenuLive == 3)
                {

                    sb.Append("{MenuName:'未分组',MenuID:'0',MenuLive:'3'}");
                }
                else
                {
                    sb.Append("{MenuName:'未分组',MenuID:'0',MenuLive:'0'}");
                }


                if (MenuLive == 4)
                {
                    sb.Append(Ilog.BLL.ILogConcernGroup.GetJsonFollowContentMenuList(loUserId, MenuLive));
                }
                else
                {
                    sb.Append(Ilog.BLL.ILogConcernGroup.GetJsonFollowContentMenuList(loUserId, 0));
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
        /// 获取全部用户组
        /// zhangl 20120705
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogFollowGetGroupList(long groupID)
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



            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{GroupList:");

            try
            {

                sb.Append("[{UrlState:'1'},");

                //获取全部用户组
                sb.Append(Ilog.BLL.ILogConcernGroup.GetJsonFollowContentMenuList(loUserId, 0));


            }
            catch(Exception e)
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();
        }


        /// <summary>
        /// 获取全部用户名，我与他有关系用户作关联标记
        /// zhangl 20120705
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogFollowGetGroupListConcern(long ConcernUserID)
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



            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{GroupList:");

            try
            {

           

                //获取全部用户组
                sb.Append(Ilog.BLL.ILogConcernGroup.GetJsonFollowGroupConcernList(loUserId, ConcernUserID));


            }
            catch (Exception e)
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();
        }


        /// <summary>
        /// 添加分组
        /// zhangl 20120705
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogFollowInsertGroup(string groupName)
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



            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{GroupInsert:");

            try
            {
                if (Ilog.BLL.ILogConcernGroup.ConcernGroupCountUserID(loUserId) < 10)
                {
                    ILog.Model.ILogConcernGroup MGroup = new ILog.Model.ILogConcernGroup();

                    MGroup.icg_name = groupName;
                    MGroup.userid = loUserId;
                    if (!Ilog.BLL.ILogConcernGroup.ConcernGroupExistsUserID(MGroup))
                    {
                        sb.Append("[");
                        sb.Append("{UrlState:'1'},");
                        //不存在添加
                        sb.Append(Ilog.BLL.ILogConcernGroup.ConcernGroupAdd(MGroup));
                    }
                    else
                    {
                        //存在，不能重复添加
                        sb.Append("[{UrlState:'2'}");
                    }
                }
                else
                {
                    sb.Append("[{UrlState:'3'}");
                }

            }
            catch (Exception e)
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();
        }



        /// <summary>
        /// 加分组,并且将用户加到分组当中
        /// zhangl 20120705
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogFollowInsertGroup_ConcernUserID(string groupName,long concernUserID)
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



            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{GroupInsert:");

            try
            {
                if (Ilog.BLL.ILogConcernGroup.ConcernGroupCountUserID(loUserId) <= 10)
                {
                    ILog.Model.ILogConcernGroup MGroup = new ILog.Model.ILogConcernGroup();

                    MGroup.icg_name = groupName;
                    MGroup.userid = loUserId;
                    if (!Ilog.BLL.ILogConcernGroup.ConcernGroupExistsUserID(MGroup))
                    {
                        sb.Append("[");
                        sb.Append("{UrlState:'1'},");

                        //不存在添加
                        sb.Append(Ilog.BLL.ILogConcernGroup.ConcernGroupAdd(MGroup));

                        MGroup.icg_id = Ilog.BLL.ILogConcernGroup.GetGroupID_UserID_IcgName(loUserId, MGroup.icg_name);
                        if (MGroup.icg_id > 0)
                        {
                                                   //根据名称获取id
                            if(!Ilog.BLL.ILogConcernGroup.ConcernGropuExistsGroupID(loUserId,concernUserID,MGroup.icg_id))
                            {
                               //不存在，加联系组
                                if (Ilog.BLL.ILogConcernGroup.ConcernGroupContentAdd(loUserId, concernUserID, MGroup.icg_id) > 0)
                                {
                                    if (Ilog.BLL.ILogConcernGroup.ConcernGropuExistsUserid(loUserId, concernUserID))
                                    {
                                        Ilog.BLL.ILogUserConcern.Update_icgid(loUserId, concernUserID, 1);
                                    }
                                    else
                                    {
                                        Ilog.BLL.ILogUserConcern.Update_icgid(loUserId, concernUserID, 0);
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        //存在，不能重复添加
                        sb.Append("[{UrlState:'2'}");
                    }
                }
                else
                {
                    sb.Append("[{UrlState:'3'}");
                }

            }
            catch (Exception e)
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();
        }


        /// <summary>
        /// 编辑用户关注组
        /// zhangl 20120705
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogFollowEditGroupName(string groupName,long GroupID)
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



            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{GroupEdit:");

            try
            {

                    ILog.Model.ILogConcernGroup MGroup = new ILog.Model.ILogConcernGroup();

                    MGroup.icg_name = groupName;
                    MGroup.userid = loUserId;
                    MGroup.icg_id = GroupID;
                    //判断组是否存在
                    if (Ilog.BLL.ILogConcernGroup.isExists(GroupID))
                    {
                        sb.Append("[");
                        //存在更新
                        sb.Append(Ilog.BLL.ILogConcernGroup.Update_JsonGroupName(MGroup));
                    }
                    else
                    {
                        //已经不存在
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

        /// <summary>
        /// 删除用户关注组
        /// zhangl 20120705
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogFollowDeleteGoupID( long GroupID)
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



            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{GroupEdit:");

            try
            {


                //判断组是否存在
                if (Ilog.BLL.ILogConcernGroup.isExists(GroupID))
                {
                    sb.Append("[");
                    //删除
                    sb.Append(Ilog.BLL.ILogConcernGroup.Delte_JsonGroupID(GroupID,loUserId));
                }
                else
                {
                    //已经不存在
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


        #region 关注搜索列表
        /// <summary>
        /// 关注列表
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetList_ConcernSearch(string PageCurrent, string PageSize, string keyWord)
        {
            #region 统一校验

            //是否登陆
            ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            //strUserId = "2372800";
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

            if (!ILog.Common.Common.ISProcessSqlStr(loUserId.ToString()))
            {
                return Ilog.BLL.ILogSpread.GetNotJsonList();
            }

            #endregion

            long userid_ = Convert.ToInt64(loUserId);

            return Ilog.BLL.ILogUserConcern.GetSearchList_Search(PageCurrent_, PageSize_, userid_, keyWord);
        }

        /// <summary>
        /// 他人关注列表
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetList_ConcernOtherSearch(string PageCurrent, string PageSize, string keyWord,long otherUserID)
        {
            #region 统一校验

            //是否登陆
            ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            //strUserId = "2372800";
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

            if (!ILog.Common.Common.ISProcessSqlStr(loUserId.ToString()))
            {
                return Ilog.BLL.ILogSpread.GetNotJsonList();
            }

            #endregion

            long userid_ = Convert.ToInt64(loUserId);

            return Ilog.BLL.ILogUserConcern.GetSearchList_OtherSearch(PageCurrent_, PageSize_, otherUserID, keyWord, loUserId);
        }
        #endregion

         /// <summary>
        /// 获取关注用户昵称
        /// </summary>

        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetList_ConcernUserid(string NickName)
        {
            #region 统一校验

            //是否登陆
            ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            //strUserId = "2372800";
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


            

            long userid_ = Convert.ToInt64(loUserId);

            return Ilog.BLL.ILogUserConcern.GetJsonList_ConcernUserID(loUserId, NickName);
        }

        #region 关注列表
        /// <summary>
        /// 我的关注列表
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页数据条数</param>
       ///<param name="keyWord">关键字</param>
       ///<param name="PageSort">排序字段</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetList_Concern(string PageCurrent, string PageSize, string keyWord,string PageSort)
        {
            #region 统一校验

            //是否登陆
            //ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

           // strUserId = "2372800";
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

            if (!ILog.Common.Common.ISProcessSqlStr(loUserId.ToString()))
            {
                return Ilog.BLL.ILogSpread.GetNotJsonList();
            }

            #endregion

            long userid_ = Convert.ToInt64(loUserId);

            return Ilog.BLL.ILogUserConcern.GetSearchList(PageCurrent_, PageSize_, userid_,keyWord,PageSort);
        }
        #endregion


        /// <summary>
        /// 粉丝列表
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetList_Fan(string PageCurrent, string PageSize, string keyWord, string PageSort)
        {
            #region 统一校验

            //是否登陆
            ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            //strUserId = "2372800";
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

            if (!ILog.Common.Common.ISProcessSqlStr(loUserId.ToString()))
            {
                return Ilog.BLL.ILogSpread.GetNotJsonList();
            }

            #endregion

            long userid_ = Convert.ToInt64(loUserId);

            return Ilog.BLL.ILogUserConcern.GetSearchList_Fan(PageCurrent_, PageSize_, userid_, keyWord, PageSort);
        }

        /// <summary>
        /// 他/她的粉丝列表
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetList_OtherFan(string PageCurrent, string PageSize, string keyWord,long otherUserID)
        {
            #region 统一校验

            //是否登陆
            ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

           // strUserId = "2372800";
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

            if (!ILog.Common.Common.ISProcessSqlStr(loUserId.ToString()))
            {
                return Ilog.BLL.ILogSpread.GetNotJsonList();
            }

            #endregion

            long userid_ = Convert.ToInt64(loUserId);

            return Ilog.BLL.ILogUserConcern.GetSearchList_OtherFan(PageCurrent_, PageSize_, userid_, keyWord, "intime",otherUserID);
        }
        #endregion


        /// <summary>
        /// 互相关注
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetList_ConcernDouble(string PageCurrent, string PageSize, string keyWord)
        {
            #region 统一校验

            //是否登陆
            //ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            //strUserId = "2372800";
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

            if (!ILog.Common.Common.ISProcessSqlStr(loUserId.ToString()))
            {
                return Ilog.BLL.ILogSpread.GetNotJsonList();
            }

            #endregion

            long userid_ = Convert.ToInt64(loUserId);

            return Ilog.BLL.ILogUserConcern.GetSearchList_Double(PageCurrent_, PageSize_, userid_, keyWord);
        }



        /// <summary>
        /// 关注列表
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetList_ConcernGoupID(string PageCurrent, string PageSize, string keyWord, long GroupID)
        {
            #region 统一校验

            //是否登陆
           ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            //strUserId = "2372800";
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

            if (!ILog.Common.Common.ISProcessSqlStr(loUserId.ToString()))
            {
                return Ilog.BLL.ILogSpread.GetNotJsonList();
            }

            #endregion

            long userid_ = Convert.ToInt64(loUserId);

            return Ilog.BLL.ILogUserConcern.GetSearchList_GroupID(PageCurrent_, PageSize_, userid_, keyWord, GroupID);
        }



        /// <summary>
        ///取消关注
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogFollowConcernCancle(long iucID,long concernUserid)
        {


            #region 统一校验

            //是否登陆
           ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

           // strUserId = "2372800";
            //校验数据类型
            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }
            //非法用户

            long loUserId = Convert.ToInt64(strUserId);



            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{ConcernCancel:");

            try
            {
                sb.Append(Ilog.BLL.ILogUserConcern.Delete_JsonConcern(iucID, loUserId,concernUserid));



            }
            catch (Exception e)
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();
        }



        /// <summary>
        ///移除粉丝
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogFollowFanCancle(long iufID, long concernUserid)
        {


            #region 统一校验

            //是否登陆
            ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            // strUserId = "2372800";
            //校验数据类型
            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }
            //非法用户

            long loUserId = Convert.ToInt64(strUserId);



            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{FanCancel:");

            try
            {
                sb.Append(Ilog.BLL.ILogUserConcern.Delete_JsonFan(iufID,loUserId, concernUserid));



            }
            catch (Exception e)
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();
        }


        /// <summary>
        ///加关注
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogFollowAddFan(long iufID, long concernUserid)
        {


            #region 统一校验

            //是否登陆
            ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            // strUserId = "2372800";
            //校验数据类型
            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }
            //非法用户

            long loUserId = Convert.ToInt64(strUserId);



            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{FanCancel:");

            try
            {
                sb.Append(Ilog.BLL.ILogUserConcern.Follow_JsonAddFan(iufID,loUserId, concernUserid));
            }
            catch (Exception e)
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();
        }




        /// <summary>
        ///加关注组，取消关注组
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogFollowEditGroupConnect(long groupID, long concernUserid, int type)
        {


            #region 统一校验

            //是否登陆
             ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

           // strUserId = "2372800";
            //校验数据类型
            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }
            //非法用户

            long loUserId = Convert.ToInt64(strUserId);



            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{GroupEdit:");

            try
            {
                sb.Append(Ilog.BLL.ILogConcernGroup.CerateUserConcernConnect(loUserId, concernUserid, groupID, type));



            }
            catch (Exception e)
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();
        }



        /// <summary>
        ///加关注组，取消关注组搜索或其他用
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogFollowEditGroupConnect_s(string groupID, long concernUserid)
        {
            #region 统一校验

            //是否登陆
            ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            // strUserId = "2372800";
            //校验数据类型
            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }

            if (!ILog.Common.Common.ISProcessSqlStr(groupID))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }

            //非法用户

            long loUserId = Convert.ToInt64(strUserId);



            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{GroupEdit:");

            try
            {
               sb.Append(Ilog.BLL.ILogConcernGroup.CerateUserConcernConnect_s(loUserId, concernUserid, groupID));
            }
            catch(Exception ex)
            {
                sb.Append("[{UserState:'"+ex.Message.ToString()+"'}");
            }

            sb.Append("]}");

            return sb.ToString();
        }

        /// <summary>
        ///判断我与用户之间的状态
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogFollowConcernState(long concernUserid)
        {
            #region 统一校验

            ////是否登陆
            //ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            //strUserId = "2372800";

            //校验数据类型
            //if (!ILog.Common.Common.Int_IsType(strUserId))
            //{
            //    HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            //}


            //非法用户

            long loUserId = 0;

            if (strUserId != "")
            {
                loUserId = Convert.ToInt64(strUserId);
            }

            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{ConcernState:");

            try
            {
                //判断是否关注
                if (Ilog.BLL.ILogUserConcern.UserConcernOnly_State(loUserId, concernUserid))
                {
                    //是否互相关注
                    if (Ilog.BLL.ILogUserConcern.UserConcern_State(loUserId, concernUserid))
                    {
                        //互相关注
                        sb.Append("[{UrlState:'3'}");

                    }
                    else
                    {
                        //已关注
                        sb.Append("[{UrlState:'2'}");
                    }
                }   
                else
                {
                    //未关注
                    sb.Append("[{UrlState:'1'}");

                }
            }
            catch(Exception e)
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();
        }

        /// <summary>
        /// 功能描述：得到TA的粉丝
        /// 创建标识：ljd 20120626
        /// </summary>
        /// <param name="userid">TA的用户id</param>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetFansListWithTa(long userid)
        {

            string fansList = Ilog.BLL.ILogUserFan.GetFansListStrWithTa(userid);

            return fansList;

        }

        /// <summary>
        /// 功能描述：得到TA的关注
        /// 创建标识：ljd 20120626
        /// </summary>
        /// <param name="userid">TA的用户id</param>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetConcernListWithTa(long userid)
        {
            string concernList = Ilog.BLL.ILogUserConcern.GetUserConcernTopNineJsonListStr(userid);

            return concernList;

        }


        /// <summary>
        ///关闭用户提醒
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogPageOutMessageNumState()
        {
            #region 统一校验

            //是否登陆
            ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            //strUserId = "2372800";

            //校验数据类型
            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }



            //非法用户

            long loUserId = Convert.ToInt64(strUserId);



            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{MessageState:");

            try
            {
                //判断是否关注
                if (Ilog.BLL.VipILogCount.UpdateOutMessageNum(loUserId) > 0)
                {

                    //未关注
                    sb.Append("[{UrlState:'1'}");

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
