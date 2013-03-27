using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Com.ILog.Utils;
using System.Web;
namespace Ilog.BLL
{
    public class ILogUserConcern
    {


        #region 查看用户关系是否存在（True：存在，False：不存在）


        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="iuc_id">关注id</param>
        /// <returns>返回一个int值</returns>
        public static bool Exists(long userid, long concernuserid)
        {
            return ILog.DAL.ILogUserConcern.UserConcernExists(userid, concernuserid);
        }
        #endregion

        #region  增加一条用户关系
        /// <summary>
        /// 增加一条用户关系
        /// <param name="model">用户关系实体</param>
        /// </summary>
        public static string UserConcernAdd(ILog.Model.ILogUserConcern model)
        {
            StringBuilder strUserConcernAdd = new StringBuilder();

            strUserConcernAdd.Append("var strUserConcernAddJsonObject = ");
            strUserConcernAdd.Append("({");
            strUserConcernAdd.Append("\"state\": \"" + ILog.DAL.ILogUserConcern.UserConcernAdd(model).ToString() + "\"");
            strUserConcernAdd.Append("})");

            return strUserConcernAdd.ToString();
        }

        /// <summary>
        /// 增加一条用户关系
        /// <param name="model">用户关系实体</param>
        /// </summary>
        public static int Add(ILog.Model.ILogUserConcern model)
        {
            return ILog.DAL.ILogUserConcern.UserConcernAdd(model);
        }
        #endregion

        #region 更新一条用户关系
        /// <summary>
        /// 更新一条用户关系
        /// <param name="model">用户关系实体</param>
        /// </summary>
        public static string UserConcernUpdate(ILog.Model.ILogUserConcern model)
        {
            StringBuilder strUserConcernUpdate = new StringBuilder();

            strUserConcernUpdate.Append("var strUserConcernUpdateJsonObject = ");
            strUserConcernUpdate.Append("({");
            strUserConcernUpdate.Append("\"state\": \"" + ILog.DAL.ILogUserConcern.UserConcernUpdate(model).ToString() + "\"");
            strUserConcernUpdate.Append("})");

            return strUserConcernUpdate.ToString();
        }


        /// <summary>
        /// 更新粉丝数关注表中的粉丝数
        /// </summary>
        /// <param name="concernuserid">用户ID</param>
        /// <returns>返回一个值</returns>
        public static int Update_ConnectVICFanNum(long concernuserid)
        {
            return ILog.DAL.ILogUserConcern.Update_ConnecrnFanNum(concernuserid);
        }
        #endregion

        #region 删除一条用户关系

        /// <summary>
        /// 删除一条用户关系
        /// </summary>
        /// <param name="iuc_id"></param>
        /// <returns></returns>
        public static int UserConcernDelete(long userid, long concernUserID)
        {
            return ILog.DAL.ILogUserConcern.UserConcernDel(userid, concernUserID);
        }

        /// <summary>
        /// 删除我的关注
        /// <param name="iuc_id">流水号</param>
        /// </summary>
        public static int Delete_ConcernUserID(long userid, long concernuserid)
        {
            return ILog.DAL.ILogUserConcern.Delete_ConcernUserID(userid, concernuserid);
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="iuc_id">流水号</param>
        /// </summary>
        public static string GetModel(int iuc_id)
        {
            DataTable dblIILogUserConcernModelList = ILog.DAL.ILogUserConcern.GetModel(iuc_id);

            //构建josn字符串 
            string strILogUserConcernJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblIILogUserConcernModelList).ToString();

            return strILogUserConcernJosn;
        }
        #endregion

        #region 分页（json节点见表字段）
        /// <summary>
        /// 数据分页
        /// </summary>
        /// <param name="tbname">要分页显示的表名</param>
        /// <param name="FieldKey">用于定位记录的主键(惟一键)字段,只能是单个字段</param>
        /// <param name="PageCurrent">要显示的页码</param>
        /// <param name="PageSize">每页的大小(记录数)</param>
        /// <param name="FieldShow">以逗号分隔的要显示的字段列表,如果不指定,则显示所有字段</param>
        /// <param name="FieldOrder">以逗号分隔的排序字段列表,可以指定在字段后面指定DESC/ASC 用于指定排序顺序</param>
        /// <param name="Where">查询条件</param>
        /// <param name="RecordCount">总页数</param>
        /// <returns></returns>
        public static string GetILogUserConcernPageList(string tbname, string FieldKey, int PageCurrent, int PageSize, string FieldShow, string FieldOrder, string Where, ref int RecordCount)
        {
            string strTableName = " ilog_userconcern ";
            string strFieldKey = "iuc_id";
            string strFieldShow = " iuc_id,userid,concernuserid,icg_id,intime,iuc_state ";
            string strFieldOrder = " iuc_id desc ";
            string strWhere = " ";

            DataTable dblILogUserConcernPageList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            //构建josn字符串 
            string strILogUserConcernPageJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblILogUserConcernPageList).ToString();

            return strILogUserConcernPageJosn;
        }
        #endregion

        #region 关注搜索列表
        /// <summary>
        /// 关注列表
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        public static string GetSearchList_Search(int PageCurrent, int PageSize, long UserID, string keyword)
        {
            string strTableName = " ilog_userconcern ";
            string strFieldKey = "iuc_id";
            string strFieldShow = " iuc_id,userid,concernuserid,intime,icg_id,iuc_state";
            string strFieldOrder = " intime desc ";
            string strWhere = "";
            int RecordCount = 0;
            if (!string.IsNullOrEmpty(keyword))
            {
                strWhere = " userid=" + UserID + " and concernuserid in (select userid from vip_ilog where nickname like '%" + keyword + "%')";


            }
            else
            {
                strWhere = " userid=" + UserID + "";
            }

            DataTable dblSearchList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);
            return GetJsonList(dblSearchList, RecordCount, UserID);
        }


        /// <summary>
        /// 他人主页搜索列表
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        public static string GetSearchList_OtherSearch(int PageCurrent, int PageSize, long otherUserID, string keyword, long UserID)
        {
            string strTableName = " ilog_userconcern ";
            string strFieldKey = "iuc_id";
            string strFieldShow = " iuc_id,userid,concernuserid,intime,icg_id,iuc_state";
            string strFieldOrder = " intime desc ";
            string strWhere = "";
            int RecordCount = 0;
            if (!string.IsNullOrEmpty(keyword))
            {
                strWhere = " userid=" + otherUserID + " and concernuserid in (select userid from vip_ilog where nickname like '%" + keyword + "%')";
            }
            else
            {
                strWhere = " userid=" + otherUserID + "";
            }

            DataTable dblSearchList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            return GetJsonOtherList(dblSearchList, RecordCount, otherUserID, UserID);
        }
        #endregion

        #region 关注列表
        /// <summary>
        /// 关注列表
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        public static string GetSearchList(int PageCurrent, int PageSize, long UserID, string keyword, string PageSort)
        {
            string strTableName = " ilog_userconcern ";
            string strFieldKey = "iuc_id";
            string strFieldShow = " iuc_id,userid,concernuserid,intime,icg_id,iuc_state";
            string strFieldOrder = " " + PageSort + " desc ";
            string strWhere = "";
            int RecordCount = 0;
            if (!string.IsNullOrEmpty(keyword))
            {
                strWhere = " userid=" + UserID + " and  nickname  like'%" + keyword + "%'";
            }
            else
            {
                strWhere = " userid=" + UserID + "";
            }

            DataTable dblSearchList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);
            return GetJsonList(dblSearchList, RecordCount, UserID);
        }
        #endregion


        #region 粉丝列表
        /// <summary>
        /// 粉丝列表
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        public static string GetSearchList_Fan(int PageCurrent, int PageSize, long UserID, string keyword, string PageSort)
        {
            string strTableName = " ilog_userfan ";
            string strFieldKey = "iuf_id";
            string strFieldShow = " iuf_id,userid,concernuserid,intime,connecttime,vic_fannum";
            string strFieldOrder = " " + PageSort + " desc ";
            string strWhere = "";
            int RecordCount = 0;
            if (!string.IsNullOrEmpty(keyword))
            {
                strWhere = " userid=" + UserID + " and concernuserid in (select userid from vip_ilog where nickname like '%" + keyword + "%')";


            }
            else
            {
                strWhere = " userid=" + UserID + "";
            }

            DataTable dblSearchList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            return GetJsonFanList(dblSearchList, RecordCount, UserID);
        }


        /// <summary>
        /// 他/她的粉丝列表
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        public static string GetSearchList_OtherFan(int PageCurrent, int PageSize, long UserID, string keyword, string PageSort, long otherUserID)
        {
            string strTableName = " ilog_userfan ";
            string strFieldKey = "iuf_id";
            string strFieldShow = " iuf_id,userid,concernuserid,intime,connecttime,vic_fannum";
            string strFieldOrder = " " + PageSort + " desc ";
            string strWhere = "";
            int RecordCount = 0;
            if (!string.IsNullOrEmpty(keyword))
            {
                strWhere = " userid=" + otherUserID + " and concernuserid in (select userid from vip_ilog where nickname like '%" + keyword + "%')";


            }
            else
            {
                strWhere = " userid=" + otherUserID + "";
            }

            DataTable dblSearchList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            return GetJsonFanList_Other(dblSearchList, RecordCount, UserID, otherUserID);
        }
        #endregion



        #region 搜索列表
        /// <summary>
        /// 互相关注列表
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        public static string GetSearchList_Double(int PageCurrent, int PageSize, long UserID, string keyword)
        {
            string strTableName = " ilog_userconcern ";
            string strFieldKey = "iuc_id";
            string strFieldShow = " iuc_id,userid,concernuserid,intime,icg_id,iuc_state";
            string strFieldOrder = " intime desc ";
            string strWhere = "";
            int RecordCount = 0;
            if (!string.IsNullOrEmpty(keyword))
            {
                strWhere = " userid=" + UserID + " and  nickname  like'%" + keyword + "%' and iuc_state=1";


            }
            else
            {
                strWhere = " userid=" + UserID + " and iuc_state=1";
            }

            DataTable dblSearchList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);
            return GetJsonList(dblSearchList, RecordCount, UserID);
        }
        #endregion


        /// <summary>
        /// 关注列表
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        public static string GetSearchList_GroupID(int PageCurrent, int PageSize, long UserID, string keyword, long GroupID)
        {
            string strTableName = " ilog_userconcern ";
            string strFieldKey = "iuc_id";
            string strFieldShow = " iuc_id,userid,concernuserid,intime,icg_id,iuc_state";
            string strFieldOrder = " intime desc ";
            string strWhere = "";
            int RecordCount = 0;
            if (GroupID == 0)
            {
                strWhere = " userid=" + UserID + " and icg_id=0";


            }
            else
            {
                strWhere = " userid=" + UserID + " and concernuserid in (select concernuserid from ilog_ConcernUserConnect where userid=" + UserID + " and icg_id=" + GroupID + " )";
            }

            DataTable dblSearchList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);
            return GetJsonList(dblSearchList, RecordCount, UserID);
        }


        /// <summary>
        /// 获取Json值
        /// </summary>
        /// <param name="dt">列表</param>
        /// <param name="RecordCount">记录条数</param>
        /// <returns>返回一组Json</returns>
        public static string GetJsonList(DataTable dt, int RecordCount, long UserID)
        {
            DataTable dtlist = dt;

            StringBuilder sbRe = new StringBuilder();
            sbRe.Append("{ConcernList:");
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {

                    sbRe.Append("[{UrlState:1},");

                    if (RecordCount == 1)     //如果有搜索结果只有一页那么就显示页数
                    {
                        sbRe.Append("{RowsCount:'" + RecordCount + "'},");
                    }
                    else
                    {
                        sbRe.Append("{RecordCount:'" + RecordCount + "'},");
                    }

                    //循环处理内容
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {

                        //关注用户id
                        long loUserID = Convert.ToInt64(dt.Rows[i]["concernuserid"].ToString());

                        //ilog实体
                        ILog.Model.VipILog userInfo = ILog.DAL.VipILog.GetModelByUserID(loUserID);
                        int urlstate = 0;
                        ILog.Model.Vip vipInfo = ILog.DAL.Vip.GetUserInfo(loUserID, ref urlstate);

                        //组id
                        long loGroupID = Convert.ToInt64(dt.Rows[i]["icg_id"].ToString());

                        int cityid = userInfo.ci_id;

                        int vipUrlstate = 0;

                        //得到省市信息
                        Dictionary<string, string> dicLog = Ilog.BLL.Vip.GetVIPRegion(cityid, ref vipUrlstate);

                        //得到用户组实体
                        DataTable dtGroup = ILog.DAL.ILogConcernGroup.GetListByConcernGoup(UserID, loUserID);

                        //用户统计实体
                        DataTable dtCount = ILog.DAL.VipILogCount.GetModel(loUserID);

                        //得到用户最新发送博文
                        ILog.Model.ILogOriginal original = new ILog.Model.ILogOriginal();

                        original = Ilog.BLL.ILogOriginal.GetLastestOriginalInfo(loUserID);

                        //关注ID
                        sbRe.Append("{RecordID:'" + dt.Rows[i]["iuc_id"].ToString() + "',");

                        //关注人
                        sbRe.Append("ConcernUserID:'" + dt.Rows[i]["concernuserid"].ToString() + "',");

                        //昵称
                        sbRe.Append("NickName:'" + userInfo.nickname.ToString() + "',");

                        //头像
                        sbRe.Append("Face:'" + userInfo.face + "',");

                        //关注数
                        sbRe.Append("Concernn:'" + dtCount.Rows[0]["vic_concernnum"].ToString() + "',");

                        //粉丝数
                        sbRe.Append("Fan:'" + dtCount.Rows[0]["vic_fannum"].ToString() + "',");

                        //总的博文数
                        sbRe.Append("ILog:'" + dtCount.Rows[0]["vic_ilognum"].ToString() + "',");

                        //等级
                        sbRe.Append("ILogClass:'" + userInfo.vi_memberlevel.ToString() + "',");

                        //用户组
                        sbRe.Append("GroupID:'" + loGroupID.ToString() + "',");

                        if (dtGroup != null && dtGroup.Rows.Count > 0)
                        {
                            sbRe.Append("GroupName:'");
                            for (int n = 0; n <= dtGroup.Rows.Count - 1; n++)
                            {
                                long loglistGoupid = Convert.ToInt64(dtGroup.Rows[n]["icg_id"].ToString());
                                //用户组名称
                                DataTable dtGroupName = ILog.DAL.ILogConcernGroup.GetModel(loglistGoupid);
                                if (dtGroupName != null && dtGroupName.Rows.Count > 0)
                                {
                                    if (n == dtGroup.Rows.Count - 1)
                                    {
                                        sbRe.Append(dtGroupName.Rows[0]["icg_name"].ToString() + "");
                                    }
                                    else
                                    {
                                        sbRe.Append(dtGroupName.Rows[0]["icg_name"].ToString() + ",");
                                    }
                                }
                            }
                            sbRe.Append("',");
                        }
                        else
                        {
                            //用户组名称
                            sbRe.Append("GroupName:'',");
                        }

                        if (loUserID != UserID)
                        {
                            if (UserConcern_State(UserID, loUserID))
                            {
                                //互相关注
                                sbRe.Append("ConcernState:'1',");
                            }
                            else
                            {
                                if (UserConcernOnly_State(UserID, loUserID))
                                {
                                    sbRe.Append("ConcernState:'0',");
                                }
                                else
                                {
                                    sbRe.Append("ConcernState:'2',");
                                }
                            }
                        }
                        else
                        {
                            sbRe.Append("ConcernState:'3',");
                        }

                        //城市
                        sbRe.Append("City:'" + dicLog["Province"].ToString() + "&nbsp;" + dicLog["City"].ToString() + "',");

                        if (string.IsNullOrEmpty(vipInfo.sex))
                        {
                            sbRe.Append("Sex:'male',");
                        }
                        else
                        {
                            sbRe.Append("Sex:'" + vipInfo.sex + "',");
                        }



                        //最新微博
                        if (original != null)
                        {
                            sbRe.Append("IlogContent:'" + ILog.Common.Common.GetJScriptGlobalObjectEscape(original.io_content) + "',");

                            //博文ID
                            sbRe.Append("IlogID:'" + original.io_id.ToString() + "',");

                            if (i == dt.Rows.Count - 1)
                            {
                                sbRe.Append("intime:'" + ILog.Common.Common.GetIlogTime(original.intime) + "'}");
                            }
                            else
                            {
                                sbRe.Append("intime:'" + ILog.Common.Common.GetIlogTime(original.intime) + "'},");
                            }
                        }
                        else
                        {
                            sbRe.Append("IlogContent:'',");

                            sbRe.Append("IlogID:'0',");

                            if (i == dt.Rows.Count - 1)
                            {
                                sbRe.Append("intime:''}");
                            }
                            else
                            {
                                sbRe.Append("intime:''},");
                            }
                        }


                    }
                }
                else
                {
                    sbRe.Append("[{UrlState:2}");
                }
            }
            catch
            {
                sbRe.Append("[{UrlState:0}");
            }

            sbRe.Append("]}");
            return sbRe.ToString();
        }


        /// <summary>
        /// 获取他的关注Json值
        /// </summary>
        /// <param name="dt">列表</param>
        /// <param name="RecordCount">记录条数</param>
        /// <returns>返回一组Json</returns>
        public static string GetJsonOtherList(DataTable dt, int RecordCount, long otherUserID, long UserID)
        {
            DataTable dtlist = dt;

            StringBuilder sbRe = new StringBuilder();
            sbRe.Append("{ConcernList:");
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {

                    sbRe.Append("[{UrlState:1},");

                    if (RecordCount == 1)     //如果有搜索结果只有一页那么就显示页数
                    {
                        sbRe.Append("{RowsCount:'" + RecordCount + "'},");
                    }
                    else
                    {
                        sbRe.Append("{RecordCount:'" + RecordCount + "'},");
                    }

                    //循环处理内容
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {

                        //关注用户id
                        long loUserID = Convert.ToInt64(dt.Rows[i]["concernuserid"].ToString());

                        //ilog实体
                        ILog.Model.VipILog userInfo = ILog.DAL.VipILog.GetModelByUserID(loUserID);
                        int urlstate = 0;
                        ILog.Model.Vip vipInfo = ILog.DAL.Vip.GetUserInfo(loUserID, ref urlstate);
                        //组id
                        long loGroupID = Convert.ToInt64(dt.Rows[i]["icg_id"].ToString());

                        int cityid = userInfo.ci_id;

                        int vipUrlstate = 0;

                        //得到省市信息
                        Dictionary<string, string> dicLog = Ilog.BLL.Vip.GetVIPRegion(cityid, ref vipUrlstate);

                        //得到用户组实体
                        DataTable dtGroup = ILog.DAL.ILogConcernGroup.GetListByConcernGoup(otherUserID, loUserID);

                        //用户统计实体
                        DataTable dtCount = ILog.DAL.VipILogCount.GetModel(loUserID);

                        //得到用户最新发送博文
                        ILog.Model.ILogOriginal original = new ILog.Model.ILogOriginal();

                        original = Ilog.BLL.ILogOriginal.GetLastestOriginalInfo(loUserID);


                        //关注ID
                        sbRe.Append("{RecordID:'" + dt.Rows[i]["iuc_id"].ToString() + "',");

                        //关注人
                        sbRe.Append("ConcernUserID:'" + dt.Rows[i]["concernuserid"].ToString() + "',");

                        //昵称
                        sbRe.Append("NickName:'" + userInfo.nickname.ToString() + "',");

                        //头像
                        sbRe.Append("Face:'" + userInfo.face + "',");

                        //关注数
                        sbRe.Append("Concernn:'" + dtCount.Rows[0]["vic_concernnum"].ToString() + "',");

                        //粉丝数
                        sbRe.Append("Fan:'" + dtCount.Rows[0]["vic_fannum"].ToString() + "',");

                        //总的博文数
                        sbRe.Append("ILog:'" + dtCount.Rows[0]["vic_ilognum"].ToString() + "',");

                        //等级
                        sbRe.Append("ILogClass:'" + userInfo.vi_memberlevel.ToString() + "',");

                        //用户组
                        sbRe.Append("GroupID:'" + loGroupID.ToString() + "',");

                        if (dtGroup != null && dtGroup.Rows.Count > 0)
                        {
                            sbRe.Append("GroupName:'");
                            for (int n = 0; n <= dtGroup.Rows.Count - 1; n++)
                            {
                                long loglistGoupid = Convert.ToInt64(dtGroup.Rows[n]["icg_id"].ToString());
                                //用户组名称
                                DataTable dtGroupName = ILog.DAL.ILogConcernGroup.GetModel(loglistGoupid);
                                if (dtGroupName != null && dtGroupName.Rows.Count > 0)
                                {
                                    if (n == dtGroup.Rows.Count - 1)
                                    {
                                        sbRe.Append(dtGroupName.Rows[0]["icg_name"].ToString() + "");
                                    }
                                    else
                                    {
                                        sbRe.Append(dtGroupName.Rows[0]["icg_name"].ToString() + ",");
                                    }
                                }
                            }
                            sbRe.Append("',");
                        }
                        else
                        {
                            //用户组名称
                            sbRe.Append("GroupName:'',");
                        }

                        if (loUserID != UserID)
                        {
                            if (UserConcern_State(UserID, loUserID))
                            {
                                //互相关注
                                sbRe.Append("ConcernState:'1',");
                            }
                            else
                            {
                                if (UserConcernOnly_State(UserID, loUserID))
                                {
                                    sbRe.Append("ConcernState:'0',");
                                }
                                else
                                {
                                    sbRe.Append("ConcernState:'2',");
                                }
                            }
                        }
                        else
                        {
                            sbRe.Append("ConcernState:'3',");
                        }

                        //城市
                        sbRe.Append("City:'" + dicLog["Province"].ToString() + "&nbsp;" + dicLog["City"].ToString() + "',");

                        if (string.IsNullOrEmpty(vipInfo.sex))
                        {
                            sbRe.Append("Sex:'male',");
                        }
                        else
                        {
                            sbRe.Append("Sex:'" + vipInfo.sex + "',");
                        }

                        //最新微博
                        if (original != null)
                        {
                            sbRe.Append("IlogContent:'" +ILog.Common.Common.GetJScriptGlobalObjectEscape(original.io_content) + "',");

                            sbRe.Append("IlogID:'" + original.io_id.ToString() + "',");

                            if (i == dt.Rows.Count - 1)
                            {
                                sbRe.Append("intime:'(" + ILog.Common.Common.GetIlogTime(original.intime) + ")'}");
                            }
                            else
                            {
                                sbRe.Append("intime:'(" + ILog.Common.Common.GetIlogTime(original.intime) + ")'},");
                            }
                        }
                        else
                        {
                            sbRe.Append("IlogContent:'',");
                            sbRe.Append("IlogID:'0',");

                            if (i == dt.Rows.Count - 1)
                            {
                                sbRe.Append("intime:''}");
                            }
                            else
                            {
                                sbRe.Append("intime:''},");
                            }
                        }
                    }
                }
                else
                {
                    sbRe.Append("[{UrlState:2}");
                }
            }
            catch
            {
                sbRe.Append("[{UrlState:0}");
            }

            sbRe.Append("]}");
            return sbRe.ToString();
        }

        /// <summary>
        /// 获取关注用户昵称Json值
        /// </summary>
        /// <param name="dt">列表</param>
        /// <param name="RecordCount">记录条数</param>
        /// <returns>返回一组Json</returns>
        public static string GetJsonList_ConcernUserID(long UserID, string NickName)
        {
            DataTable dtlist = GetListConcernUserName_UserID(UserID, NickName);



            StringBuilder sbRe = new StringBuilder();

            sbRe.Append("{ConcernList:");
            try
            {
                if (dtlist != null && dtlist.Rows.Count > 0)
                {


                    sbRe.Append("[{UrlState:1},");

                    for (int i = 0; i <= dtlist.Rows.Count - 1; i++)
                    {
                        if (i <= 9)
                        {
                            if (i == 9 || i == dtlist.Rows.Count - 1)
                            {
                                sbRe.Append("{NickName:'" + dtlist.Rows[i]["nickname"].ToString() + "'}");
                            }
                            else
                            {
                                sbRe.Append("{NickName:'" + dtlist.Rows[i]["nickname"].ToString() + "'},");
                            }
                        }
                    }

                }
                else
                {
                    sbRe.Append("[{UrlState:2}");
                }
            }
            catch
            {
                sbRe.Append("[{UrlState:0}");
            }

            sbRe.Append("]}");
            return sbRe.ToString();
        }

        /// <summary>
        /// 获取Json值
        /// </summary>
        /// <param name="dt">列表</param>
        /// <param name="RecordCount">记录条数</param>
        /// <returns>返回一组Json</returns>
        public static string GetJsonFanList(DataTable dt, int RecordCount, long UserID)
        {
            DataTable dtlist = dt;

            StringBuilder sbRe = new StringBuilder();
            sbRe.Append("{ConcernList:");
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {

                    sbRe.Append("[{UrlState:1},");

                    if (RecordCount == 1)     //如果有搜索结果只有一页那么就显示页数
                    {
                        sbRe.Append("{RowsCount:'" + RecordCount + "'},");
                    }
                    else
                    {
                        sbRe.Append("{RecordCount:'" + RecordCount + "'},");
                    }

                    //循环处理内容
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {

                        //关注用户id
                        long loUserID = Convert.ToInt64(dt.Rows[i]["concernuserid"].ToString());

                        //ilog实体
                        ILog.Model.VipILog userInfo = ILog.DAL.VipILog.GetModelByUserID(loUserID);
                        int urlstate = 0;
                        ILog.Model.Vip vipInfo = ILog.DAL.Vip.GetUserInfo(loUserID, ref urlstate);

                        if (vipInfo == null || userInfo == null)
                        {
                            continue;
                        }

                        int cityid = userInfo.ci_id;

                        int vipUrlstate = 0;

                        //得到省市信息
                        Dictionary<string, string> dicLog = Ilog.BLL.Vip.GetVIPRegion(cityid, ref vipUrlstate);


                        //用户统计实体
                        DataTable dtCount = ILog.DAL.VipILogCount.GetModel(loUserID);

                        //得到用户最新发送博文
                        ILog.Model.ILogOriginal original = new ILog.Model.ILogOriginal();

                        original = BLL.ILogOriginal.GetLastestOriginalInfo(loUserID);


                        //关注ID
                        sbRe.Append("{RecordID:'" + dt.Rows[i]["iuf_id"].ToString() + "',");

                        //关注人
                        sbRe.Append("ConcernUserID:'" + dt.Rows[i]["concernuserid"].ToString() + "',");

                        //昵称
                        sbRe.Append("NickName:'" + userInfo.nickname.ToString() + "',");

                        //头像
                        sbRe.Append("Face:'" + userInfo.face + "',");

                        //关注数
                        sbRe.Append("Concernn:'" + dtCount.Rows[0]["vic_concernnum"].ToString() + "',");

                        //粉丝数
                        sbRe.Append("Fan:'" + dtCount.Rows[0]["vic_fannum"].ToString() + "',");

                        //总的博文数
                        sbRe.Append("ILog:'" + dtCount.Rows[0]["vic_ilognum"].ToString() + "',");

                        //等级
                        sbRe.Append("ILogClass:'" + userInfo.vi_state.ToString() + "',");


                        if (loUserID != UserID)
                        {
                            if (UserConcern_State(UserID, loUserID))
                            {
                                //互相关注
                                sbRe.Append("ConcernState:'1',");
                            }
                            else
                            {
                                if (UserConcernOnly_State(UserID, loUserID))
                                {
                                    sbRe.Append("ConcernState:'0',");
                                }
                                else
                                {
                                    sbRe.Append("ConcernState:'2',");
                                }
                            }
                        }
                        else
                        {
                            sbRe.Append("ConcernState:'3',");
                        }

                        //城市
                        sbRe.Append("City:'" + dicLog["Province"].ToString() + "&nbsp;" + dicLog["City"].ToString() + "',");

                        if (string.IsNullOrEmpty(vipInfo.sex))
                        {
                            sbRe.Append("Sex:'male',");
                        }
                        else
                        {
                            sbRe.Append("Sex:'" + vipInfo.sex + "',");
                        }

                        //最新微博
                        if (original != null)
                        {
                            sbRe.Append("IlogContent:'" + ILog.Common.Common.GetJScriptGlobalObjectEscape(original.io_content) + "',");

                            //博文ID
                            sbRe.Append("IlogID:'" + original.io_id.ToString() + "',");


                            if (i == dt.Rows.Count - 1)
                            {
                                sbRe.Append("intime:'(" + ILog.Common.Common.GetIlogTime(original.intime) + ")'}");
                            }
                            else
                            {
                                sbRe.Append("intime:'(" + ILog.Common.Common.GetIlogTime(original.intime) + ")'},");
                            }
                        }
                        else
                        {
                            sbRe.Append("IlogContent:'',");

                            //博文ID
                            sbRe.Append("IlogID:'0',");

                            if (i == dt.Rows.Count - 1)
                            {
                                sbRe.Append("intime:''}");
                            }
                            else
                            {
                                sbRe.Append("intime:''},");
                            }
                        }


                    }
                }
                else
                {
                    sbRe.Append("[{UrlState:2}");
                }
            }
            catch
            {
                sbRe.Append("[{UrlState:0}");
            }
            sbRe = new StringBuilder(sbRe.ToString().TrimEnd(','));
            sbRe.Append("]}");
            return sbRe.ToString();
        }


        /// <summary>
        /// 获取Json值
        /// </summary>
        /// <param name="dt">列表</param>
        /// <param name="RecordCount">记录条数</param>
        /// <returns>返回一组Json</returns>
        public static string GetJsonFanList_Other(DataTable dt, int RecordCount, long UserID, long otherUserid)
        {
            // DataTable dtlist = dt;

            StringBuilder sbRe = new StringBuilder();
            sbRe.Append("{ConcernList:");
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {

                    sbRe.Append("[{UrlState:1},");

                    if (RecordCount == 1)     //如果有搜索结果只有一页那么就显示页数
                    {
                        sbRe.Append("{RowsCount:'" + RecordCount + "'},");
                    }
                    else
                    {
                        sbRe.Append("{RecordCount:'" + RecordCount + "'},");
                    }

                    //循环处理内容
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {

                        //关注用户id
                        long loUserID = Convert.ToInt64(dt.Rows[i]["concernuserid"].ToString());

                        //ilog实体
                        ILog.Model.VipILog userInfo = ILog.DAL.VipILog.GetModelByUserID(loUserID);

                        int urlstate = 0;

                        ILog.Model.Vip vipInfo = ILog.DAL.Vip.GetUserInfo(loUserID, ref urlstate);
                        if (vipInfo == null || userInfo == null)
                        {
                            continue;
                        }

                        int cityid = userInfo.ci_id;

                        int vipUrlstate = 0;

                        //得到省市信息
                        Dictionary<string, string> dicLog = Ilog.BLL.Vip.GetVIPRegion(cityid, ref vipUrlstate);



                        //用户统计实体
                        DataTable dtCount = ILog.DAL.VipILogCount.GetModel(loUserID);

                        //得到用户最新发送博文
                        ILog.Model.ILogOriginal original = new ILog.Model.ILogOriginal();

                        original = Ilog.BLL.ILogOriginal.GetLastestOriginalInfo(loUserID);


                        //粉丝ID
                        sbRe.Append("{RecordID:'" + dt.Rows[i]["iuf_id"].ToString() + "',");

                        //他的粉丝id
                        sbRe.Append("ConcernUserID:'" + dt.Rows[i]["concernuserid"].ToString() + "',");

                        //昵称
                        sbRe.Append("NickName:'" + userInfo.nickname.ToString() + "',");

                        //头像
                        sbRe.Append("Face:'" + userInfo.face + "',");

                        //关注数
                        sbRe.Append("Concernn:'" + dtCount.Rows[0]["vic_concernnum"].ToString() + "',");

                        //粉丝数
                        sbRe.Append("Fan:'" + dtCount.Rows[0]["vic_fannum"].ToString() + "',");

                        //总的博文数
                        sbRe.Append("ILog:'" + dtCount.Rows[0]["vic_ilognum"].ToString() + "',");

                        //等级
                        sbRe.Append("ILogClass:'" + userInfo.vi_memberlevel + "',");


                        if (loUserID != UserID)
                        {
                            if (UserConcern_State(UserID, loUserID))
                            {
                                //互相关注
                                sbRe.Append("ConcernState:'1',");
                            }
                            else
                            {
                                if (UserConcernOnly_State(UserID, loUserID))
                                {
                                    sbRe.Append("ConcernState:'0',");
                                }
                                else
                                {
                                    sbRe.Append("ConcernState:'2',");
                                }
                            }
                        }
                        else
                        {
                            sbRe.Append("ConcernState:'3',");
                        }

                        //城市
                        sbRe.Append("City:'" + dicLog["Province"].ToString() + "&nbsp;" + dicLog["City"].ToString() + "',");

                        if (string.IsNullOrEmpty(vipInfo.sex))
                        {
                            sbRe.Append("Sex:'male',");
                        }
                        else
                        {
                            sbRe.Append("Sex:'" + vipInfo.sex + "',");
                        }

                        //最新微博
                        if (original != null)
                        {
                            sbRe.Append("IlogContent:'" + ILog.Common.Common.GetJScriptGlobalObjectEscape(original.io_content.ToString()) + "',");

                            sbRe.Append("IlogID:" + original.io_id + ",");

                            if (i == dt.Rows.Count - 1)
                            {
                                sbRe.Append("intime:'(" + ILog.Common.Common.GetIlogTime(original.intime) + ")'}");
                            }
                            else
                            {
                                sbRe.Append("intime:'(" + ILog.Common.Common.GetIlogTime(original.intime) + ")'},");
                            }
                        }
                        else
                        {
                            sbRe.Append("IlogContent:'',");

                            if (i == dt.Rows.Count - 1)
                            {
                                sbRe.Append("intime:''}");
                            }
                            else
                            {
                                sbRe.Append("intime:''},");
                            }
                        }


                    }
                }
                else
                {
                    sbRe.Append("[{UrlState:2}");
                }
            }
            catch
            {
                sbRe.Append("[{UrlState:0}");
            }
            sbRe = new StringBuilder(sbRe.ToString().TrimEnd(','));
            sbRe.Append("]}");
            return sbRe.ToString();

        }

        /// <summary>
        /// 更新最后联系时间
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="concernuserid">最后发送时间</param>
        /// <returns></returns>
        public static int Update_ConnectTime(long userid, long concernuserid)
        {
            return ILog.DAL.ILogUserConcern.Update_ConnectTime(userid, concernuserid);
        }

        /// <summary>
        /// 更新是否有组关联
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="concernuserid">最后发送时间</param>
        /// <returns></returns>
        public static int Update_icgid(long userid, long concernuserid, long icg_id)
        {
            return ILog.DAL.ILogUserConcern.Update_icgid(userid, concernuserid, icg_id);

        }

        /// <summary>
        /// 判断是否已经互相关注
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="concernuserid">关注用户</param>
        /// <returns></returns>
        public static bool UserConcern_State(long userid, long concernuserid)
        {
            return ILog.DAL.ILogUserConcern.UserConcern_State(userid, concernuserid);
        }

        /// <summary>
        /// 判断是否已经单向关注
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="concernuserid">关注用户</param>
        /// <returns></returns>
        public static bool UserConcernOnly_State(long userid, long concernuserid)
        {
            return ILog.DAL.ILogUserConcern.UserConcernOnly_State(userid, concernuserid);
        }



        /// <summary>
        /// 获取某用户全部关注的用户名
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataTable GetListConcernUserName_UserID(long userID, string NickName)
        {
            return ILog.DAL.ILogUserConcern.GetListConcernUserName_UserID(userID, NickName);
        }
        /// <summary>
        /// 更新互相关注状态
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="concernuserid">关注用户</param>
        /// <returns></returns>
        public static int UserConcernDouble_State(long userid, long concernuserid, int isState)
        {
            return ILog.DAL.ILogUserConcern.UserConcernDouble_State(userid, concernuserid, isState);
        }




        /// <summary>
        /// 更新关注表粉丝数
        /// </summary>
        /// <param name="concernuserid">用户ID</param>
        /// <returns>返回一个值</returns>
        public static int Update_ConnecrnFanNum(long concernuserid)
        {
            return ILog.DAL.ILogUserConcern.Update_ConnecrnFanNum(concernuserid);
        }


        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="iucid">关注id</param>
        /// <param name="concernUserID">关注人</param>
        /// <returns>返回一个用户json</returns>
        public static string Delete_JsonConcern(long iucid, long userid, long concernUserID)
        {
            StringBuilder sbRe = new StringBuilder();

            //判断关注是否存在
            if (Exists(userid, concernUserID))
            {
                //删除关注信息表
                if (UserConcernDelete(userid, concernUserID) > 0)
                {
                    //删除全部用户分组
                    Ilog.BLL.ILogConcernGroup.ConcernGroupDel_ConnectUserid(userid, concernUserID);


                    //更新我的关注数量
                    Ilog.BLL.VipILogCount.UpdateConcernNum(userid, -1);


                    //清除我在关注人的粉丝中位置
                    Ilog.BLL.ILogUserFan.Delete_Userid(concernUserID, userid);


                    //更新的我的粉丝数量；
                    Ilog.BLL.VipILogCount.UpdateFanNum(concernUserID, -1);


                    //关注粉丝统计更新
                    Ilog.BLL.ILogUserConcern.Update_ConnecrnFanNum(concernUserID);


                    //是否是互相关注
                    if (UserConcern_State(concernUserID, userid))
                    {
                        //将互相关注更新为单向关注
                        UserConcernDouble_State(concernUserID, userid, 0);
                        //更新互相关注数量

                        Ilog.BLL.VipILogCount.UpdateDoubleConcernNum(userid, -1);

                        //更新对方的互相关注数量
                        Ilog.BLL.VipILogCount.UpdateDoubleConcernNum(concernUserID, -1);

                    }

                    sbRe.Append("[{UrlState:1}");
                }
                else
                {
                    sbRe.Append("[{UrlState:0}");
                }
            }
            else
            {
                sbRe.Append("[{UrlState:2}");
            }

            return sbRe.ToString();
        }



        /// <summary>
        /// 移除粉丝
        /// </summary>
        /// <param name="iucid">关注id</param>
        /// <param name="concernUserID">关注人</param>
        /// <returns>返回一个用户json</returns>
        public static string Delete_JsonFan(long iufid, long userid, long concernUserID)
        {
            StringBuilder sbRe = new StringBuilder();

            //判断粉丝关系是否存在
            if (Ilog.BLL.ILogUserFan.isExists(userid, concernUserID))
            {

                if (Ilog.BLL.ILogUserFan.Delete_iufID(userid, concernUserID) > 0)
                {
                    //粉丝移除
                    //我的粉丝数减1移除
                    Ilog.BLL.VipILogCount.UpdateFanNum(userid, -1);

                    //所有关注了我的人粉丝数更新
                    Ilog.BLL.ILogUserConcern.Update_ConnecrnFanNum(userid);



                    //删除粉丝，把对方的关注移除
                    Delete_ConcernUserID(concernUserID, userid);

                    //删除他全部用户分组关系
                    Ilog.BLL.ILogConcernGroup.ConcernGroupDel_ConnectUserid(concernUserID, userid);

                    //更新他的关注数量
                    Ilog.BLL.VipILogCount.UpdateConcernNum(concernUserID, -1);

                    //是否是互相关注
                    if (UserConcern_State(userid, concernUserID))
                    {
                        //将互相关注更新为单向关注
                        UserConcernDouble_State(userid, concernUserID, 0);

                        //更新互相关注数量

                        Ilog.BLL.VipILogCount.UpdateDoubleConcernNum(userid, -1);

                        //更新对方的互相关注数量
                        Ilog.BLL.VipILogCount.UpdateDoubleConcernNum(concernUserID, -1);

                    }


                    sbRe.Append("[{UrlState:1}");
                }
                else
                {
                    sbRe.Append("[{UrlState:0}");
                }
            }
            else
            {
                sbRe.Append("[{UrlState:2}");
            }

            return sbRe.ToString();
        }





        /// <summary>
        /// 加关注
        /// </summary>
        /// <param name="iucid">关注id</param>
        /// <param name="concernUserID">关注人</param>
        /// <returns>返回一个用户json</returns>
        public static string Follow_JsonAddFan(long iufid, long Userid, long concernUserID)
        {
            StringBuilder sbRe = new StringBuilder();

            bool bl = true;

            bool AtoB = true;

            if (Userid != concernUserID)
            {
                //我关注用户,我是用户的粉丝,判断我是否是用我是否是用户的粉丝
                if (!Ilog.BLL.ILogUserFan.IsExists_UserID(concernUserID, Userid))
                {

                    //单向关注关系生成，我关注用户
                    //我要关注用户，我是用户粉丝
                    ILog.Model.ILogUserFan userFan = new ILog.Model.ILogUserFan();

                    userFan.concernuserid = Userid;


                    userFan.userid = concernUserID;

                    ILog.Model.ILogUserConcern userConcern = new ILog.Model.ILogUserConcern();



                    userConcern.concernuserid = concernUserID;

                    userConcern.userid = Userid;

                    userConcern.icg_id = 0;

                    userConcern.iuc_state = false;


                    //我成为用户粉丝
                    if (!(Ilog.BLL.ILogUserFan.Add(userFan) > 0))
                    {
                        bl = false;
                    }
                    else
                    {
                        //用户的粉丝加
                        Ilog.BLL.VipILogCount.UpdateFanOutNum(concernUserID, 1);


                    }

                    if (bl) //我成为用户粉丝是否成功
                    {
                        //我的关注增加
                        if (!(Add(userConcern) > 0))
                        {
                            bl = false;
                        }
                        else
                        {
                            //更新关注用户的粉丝数在粉丝表中
                            Ilog.BLL.ILogUserFan.Update_FanNum(Userid);

                            ////更新关注统计
                            Ilog.BLL.VipILogCount.UpdateConcernNum(Userid, 1);

                            ////更新关注粉丝统计
                            Ilog.BLL.ILogUserConcern.Update_ConnecrnFanNum(concernUserID);

                            ////判断他是否已经关注我
                            if (Ilog.BLL.ILogUserFan.IsExists_UserID(Userid, concernUserID))
                            {
                                //是否是互相关注,将单向关注更新为互相关注
                                UserConcernDouble_State(Userid, concernUserID, 1);

                                //是否是互相关注,将单向关注更新为互相关注
                                UserConcernDouble_State(concernUserID, Userid, 1);

                                //更新互相关注数量
                                Ilog.BLL.VipILogCount.UpdateDoubleConcernNum(Userid, 1);

                                //更新互相关注数量
                                Ilog.BLL.VipILogCount.UpdateDoubleConcernNum(concernUserID, 1);

                            }
                        }
                    }

                    if (bl)
                    {
                        if (UserConcern_State(Userid, concernUserID))
                        {
                            //互相关注
                            sbRe.Append("[{UrlState:'1'}");
                        }
                        else
                        {
                            //单向关注
                            sbRe.Append("[{UrlState:'2'}");
                        }
                    }



                }
                else
                {
                    if (UserConcern_State(Userid, concernUserID))
                    {
                        //互相关注
                        sbRe.Append("[{UrlState:'1'}");
                    }
                    else
                    {
                        //单向关注
                        sbRe.Append("[{UrlState:'2'}");
                    }
                    //我已经关注了用户
                    AtoB = true;
                }
            }
            else
            {
                sbRe.Append("[{UrlState:'3'}");
                //自己不能关注自己
                AtoB = true;
            }



            return sbRe.ToString();
        }




        #region 根据用户id得到用户的前9个关注用户id
        /// <summary>
        /// 功能描述：根据用户id得到用户的前9个关注用户id
        /// 创建标识：ljd 20120627
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static List<ILog.Model.ILogUserConcern> GetUserConcernTopNineListByUserid(long userid)
        {
            List<ILog.Model.ILogUserConcern> userlist = ILog.DAL.ILogUserConcern.GetUserConcernTopNineListByUserid(userid);
            return userlist;

        }
        #endregion


        #region 得到他人主页用户的前九个关注（json格式）
        /// <summary>
        /// 功能描述：得到他人主页用户的前九个粉丝（json格式）
        /// 创建标识：ljd 20120626
        /// </summary>
        /// <param name="userid">TA的用户id</param>
        /// <returns></returns>
        public static string GetUserConcernTopNineJsonListStr(long userid)
        {
            List<ILog.Model.ILogUserConcern> concernList = new List<ILog.Model.ILogUserConcern>();

            StringBuilder strbConcernList = new StringBuilder();

            strbConcernList.Append("{ConcernList:[");

            try
            {
                concernList = GetUserConcernTopNineListByUserid(userid);

                if (concernList.Count == 0)
                {
                    strbConcernList.Append("{State:'0'}]}");//无数据
                }
                else
                {
                    strbConcernList.Append("{State:'1'},");

                    foreach (ILog.Model.ILogUserConcern ooConcern in concernList)
                    {
                        ILog.Model.VipILog ooIlog = BLL.VipILog.GetModelByUserID(ooConcern.concernuserid);

                        strbConcernList.Append("{nickname:'" + ooIlog.nickname + "',userid:'" + ooIlog.userid + "',date:'" + BLL.ILogVisithistory.GetVisitTimeShow(ooConcern.intime)
                            + "',face:'/images/face/small/" + ooIlog.face + "'},");
                    }
                    strbConcernList.Remove(strbConcernList.Length - 1, 1);
                    strbConcernList.Append("]}");
                }

            }
            catch
            {
                strbConcernList.Append("{State:'-1'}]}");//报错
            }

            return strbConcernList.ToString();

        }
        #endregion



        /// <summary>
        /// 得到我与用户的关系
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="concernUserID"></param>
        /// <returns></returns>
        public static int Follow_UserToConcern_State(long userID, long concernUserID)
        {


            if (UserConcern_State(userID, concernUserID))
            {
                //互相关注
                return 0;
            }
            else
            {
                //判断是否是单向关注
                if (UserConcernOnly_State(userID, concernUserID))
                {
                    //单向关注 
                    return 1;
                }
                else
                {
                    //我的粉丝
                    if (UserConcernOnly_State(concernUserID, userID))
                    {
                        //是我的粉丝
                        return 2;
                    }
                    else
                    {
                        //没有关系
                        return 3;
                    }
                }
            }
        }

    }






}
