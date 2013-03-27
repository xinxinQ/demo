using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ilog.BLL
{
    public class ILogConcernGroup
    {
        #region 查看某组是否存在（True：存在，False：不存在）
        /// <summary>
        /// 查看某组是否存在（True：存在，False：不存在）
        /// <param name="ic_id">流水号</param>
        /// </summary>
        public static string ConcernGroupExists(int icg_id)
        {
            StringBuilder strConcernGroupExists = new StringBuilder();

            strConcernGroupExists.Append("var strConcernGroupExistsJsonObject = ");
            strConcernGroupExists.Append("({");
            strConcernGroupExists.Append("\"Exists\": \"" + ILog.DAL.ILogConcernGroup.ConcernGroupExists(icg_id).ToString() + "\"");
            strConcernGroupExists.Append("})");

            return strConcernGroupExists.ToString();
        }


        /// <summary>
        /// 判断组是否还存在
        /// </summary>
        /// <param name="icg_id"></param>
        /// <returns></returns>
        public static bool isExists(long icg_id)
        {
            return ILog.DAL.ILogConcernGroup.ConcernGroupExists(icg_id);
        }
      /// <summary>
        /// 判断用户组是否已经关注
        /// </summary>
        /// <param name="userID">用户id</param>
        /// <param name="ConcernUserID">关注用户</param>
        /// <param name="GroupID">用户组id</param>
        /// <returns>返回一个bool值</returns>
        public static bool ConcernGropuExistsGroupID(long userID, long ConcernUserID, long GroupID)
        {
            return ILog.DAL.ILogConcernGroup.ConcernGropuExistsGroupID(userID, ConcernUserID, GroupID);
        }


        /// <summary>
        /// 判断用户组是否存在
        /// </summary>
        /// <param name="userID">用户id</param>
        /// <param name="ConcernUserID">关注用户</param>
        /// <returns>返回一个bool值</returns>
        public static bool ConcernGropuExistsUserid(long userID, long ConcernUserID)
        {
            return ILog.DAL.ILogConcernGroup.ConcernGropuExistsUserid(userID, ConcernUserID);
        }
                /// <summary>
        /// 查看某组是否存在（True：存在，False：不存在）
        /// <param name="userid">用户名</param>
        /// <param name="mode">实体</param>
        /// </summary>
        public static bool ConcernGroupExistsUserID(ILog.Model.ILogConcernGroup model)
        {


            return ILog.DAL.ILogConcernGroup.ConcernGroupExistsUserID(model);

        }

                /// <summary>
        /// 获取组id
        /// <param name="userid">用户名</param>
        /// <param name="mode">实体</param>
        /// </summary>
        public static int GetGroupID_UserID_IcgName(long userid, string icg_Name)
        {
            return ILog.DAL.ILogConcernGroup.GetGroupID_UserID_IcgName(userid, icg_Name);
        }

         /// <summary>
        /// 统计用户，组的数量
        /// <param name="userid">用户名</param>
        /// </summary>
        public static int ConcernGroupCountUserID(long UserID)
        {
            return Com.ILog.Utils.Utils.StrToInt(ILog.DAL.ILogConcernGroup.ConcernGroupCountUserID(UserID),0);
        }
        #endregion

        #region  增加一条数据
        /// <summary>
        /// 增加一条数据
        /// <param name="model">评论表实体</param>
        /// </summary>
        public static string ConcernGroupAdd(ILog.Model.ILogConcernGroup model)
        {
            StringBuilder strConcernGroupAdd = new StringBuilder();


            strConcernGroupAdd.Append("{ListID: '" + ILog.DAL.ILogConcernGroup.ConcernGroupAdd(model).ToString() + "'}");


            return strConcernGroupAdd.ToString();
        }

        /// <summary>
        /// 增加一条关系
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="ConcernUserID"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public static int ConcernGroupContentAdd(long userID, long ConcernUserID, long GroupID)
        {
            return ILog.DAL.ILogConcernGroup.ConcernGroupContentAdd(userID, ConcernUserID, GroupID);
        }
        #endregion

        #region 更新一条用户组数据
        /// <summary>
        /// 更新一条用户组数据
        /// <param name="model">用户组息表实体</param>
        /// </summary>
        public static string ConcernGroupUpdate(ILog.Model.ILogConcernGroup model)
        {
            StringBuilder strConcernGroupUpdate = new StringBuilder();

            strConcernGroupUpdate.Append("var ConcernGroupUpdateJsonObject = ");
            strConcernGroupUpdate.Append("({");
            strConcernGroupUpdate.Append("\"state\": \"" + ILog.DAL.ILogConcernGroup.ConcernGroupUpdate(model).ToString() + "\"");
            strConcernGroupUpdate.Append("})");

            return strConcernGroupUpdate.ToString();
        }

        /// <summary>
        /// 更新用户组
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string  Update_JsonGroupName(ILog.Model.ILogConcernGroup model)
        {
            if (ILog.DAL.ILogConcernGroup.ConcernGroupUpdate(model) > 0)
            {
                return "{UrlState:'1'}";
            }
            else
            {

                return "{UrlState:'0'}";
            }
        }
        #endregion

        #region 删除一条评论数据


        public static string Delte_JsonGroupID(long GroupID,long userID)
        {
            if (ILog.DAL.ILogConcernGroup.ConcernGroupDel(GroupID)>=1)
            {
                //清理关系
                //我，用户组与他人全部关系
                DataTable dt = new DataTable();

                dt = GetList_ConnectUserid_IcgID(userID, GroupID);

                long ConcernUserID=0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        ConcernUserID = Convert.ToInt64(dt.Rows[i]["concernuserid"].ToString());

                        if (ConcernGroupDel_GroupID(userID, ConcernUserID,GroupID) > 0)
                        {

                            if (ConcernGropuExistsUserid(userID, ConcernUserID))
                            {
                                Ilog.BLL.ILogUserConcern.Update_icgid(userID, ConcernUserID, 1);
                            }
                            else
                            {
                                Ilog.BLL.ILogUserConcern.Update_icgid(userID, ConcernUserID, 0);
                            }
                        }
                    }
                }


                return "{UrlState:'1'}";
                //删除成功
            }
            else
            {
                return "{UrlState:'0'}";
            }
        }

        //获取用户组所有关系
        public static DataTable GetList_ConnectUserid_IcgID(long userID, long GroupID)
        {
           return ILog.DAL.ILogConcernGroup.GetList_ConnectUserid_IcgID(userID, GroupID);
        }


        /// <summary>
        /// 删除一条评论数据
        /// <param name="vi_id">流水号</param>
        /// <param name="userid">用户id</param>
        /// </summary>
        public static string ConcernGroupDel(int icg_id)
        {
            StringBuilder strConcernGroupDel = new StringBuilder();

            strConcernGroupDel.Append("var ConcernGroupDelJsonObject = ");
            strConcernGroupDel.Append("({");
            strConcernGroupDel.Append("\"state\": \"" + ILog.DAL.ILogConcernGroup.ConcernGroupDel(icg_id).ToString() + "\"");
            strConcernGroupDel.Append("})");

            return strConcernGroupDel.ToString();
        }

          /// <summary>
        /// 删除用户组
        /// <param name="icg_id">流水号</param>
        /// </summary>
        public static int ConcernGroupDel_GroupID(long userID, long ConcernUserID, long GroupID)
        {
            return ILog.DAL.ILogConcernGroup.ConcernGroupDel_GroupID(userID, ConcernUserID, GroupID);
        }
                /// <summary>
        /// 取消关注，删除用户关系分组
        /// <param name="icg_id">流水号</param>
        /// </summary>
        public static int ConcernGroupDel_ConnectUserid(long userID, long ConcernUserID)
        {
            return ILog.DAL.ILogConcernGroup.ConcernGroupDel_ConnectUserid(userID, ConcernUserID);
        }

        
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="icg_id">流水号</param>
        /// </summary>
        public static string GetModel(int icg_id)
        {
            DataTable dblModelList = ILog.DAL.ILogConcernGroup.GetModel(icg_id);

            //构建josn字符串 
            string ILogAtModelJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblModelList).ToString();

            return ILogAtModelJosn;
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
        public static string GetConcernGroupPageList(string tbname, string FieldKey, int PageCurrent, int PageSize, string FieldShow, string FieldOrder, string Where, ref int RecordCount)
        {
            string strTableName = " ilog_concerngroup ";
            string strFieldKey = "icg_id";
            string strFieldShow = " icg_id,icg_name,userid,intime ";
            string strFieldOrder = " icg_id desc ";
            string strWhere = " ";

            DataTable dblConcernGroupPageList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            //构建josn字符串 
            string strConcernGroupPageListPageJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblConcernGroupPageList).ToString();

            return strConcernGroupPageListPageJosn;
        }
        #endregion

                /// <summary>
        /// 获取某用户全部用户组
        /// </summary>
        /// <param name="userid">用户名</param>
        /// <returns>返回一个结果</returns>
        public static DataTable GetListByUserid(long userid)
        {
     

            DataTable dt = ILog.DAL.ILogConcernGroup.GetListByUserid(userid);

            return dt;
        }


        /// <summary>
        /// 返回一个
        /// </summary>
        /// <param name="userid">用户名</param>
        /// <returns>返回一个结果</returns>
        public static string GetJsonFollowContentMenuList(long userid,int menuLive)
        {
            StringBuilder sbjson = new StringBuilder();
            DataTable dt = GetListByUserid(userid);
            if (dt != null && dt.Rows.Count > 0)
            {
                sbjson.Append(",");
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if (i <= dt.Rows.Count - 2)
                    {
                        sbjson.Append("{MenuName:'" + dt.Rows[i]["icg_name"].ToString() + "',MenuID:'" + dt.Rows[i]["icg_id"].ToString() + "',MenuLive:'" + menuLive + "'},");
                    }
                    else
                    {
                        sbjson.Append("{MenuName:'" + dt.Rows[i]["icg_name"].ToString() + "',MenuID:'" + dt.Rows[i]["icg_id"].ToString() + "',MenuLive:'" + menuLive + "'}");
                    }
                }
            }
            else
            {
                sbjson.Append("");
            }

            return sbjson.ToString();
        }

        /// <summary>
        /// 根据关注用户名
        /// </summary>
        /// <param name="userid">用户名</param>
        /// <param name="ConcernUserID">被关注用户</param>
        /// <returns>返回一个结果</returns>
        public static string GetJsonFollowGroupConcernList(long userid, long ConcernUserID)
        {
            StringBuilder sbjson = new StringBuilder();
            DataTable dt = GetListByUserid(userid);
            if (dt != null && dt.Rows.Count > 0)
            {

                string ConcernState = "0";
                long GroupID = 0;

                sbjson.Append("[{UrlState:'1'},");

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    GroupID = Convert.ToInt64(dt.Rows[i]["icg_id"].ToString());
                    if (ConcernGropuExistsGroupID(userid, ConcernUserID, GroupID))
                    {
                        ConcernState = "1";
                    }
                    else
                    {
                        ConcernState = "0";
                    }

                    if (i != dt.Rows.Count - 1)
                    {

                        sbjson.Append("{GroupName:'" + dt.Rows[i]["icg_name"].ToString() + "',GroupID:'" + GroupID.ToString() + "',MenuLive:'" + ConcernState.ToString() + "'},");
                    }
                    else
                    {
                        sbjson.Append("{GroupName:'" + dt.Rows[i]["icg_name"].ToString() + "',GroupID:'" + GroupID.ToString() + "',MenuLive:'" + ConcernState.ToString() + "'}");
                    }
                }
            }
            else
            {
                sbjson.Append("[{UrlState:'2'}");

            }

            return sbjson.ToString();
        }

        /// <summary>
        /// 根据关注用户名
        /// </summary>
        /// <param name="userid">用户名</param>
        /// <param name="ConcernUserID">被关注用户</param>
        /// <returns>返回一个结果</returns>
        public static string GetJsonFollowGroupConcernGroup(long userid, long ConcernUserID)
        {
            StringBuilder sbjson = new StringBuilder();
            DataTable dt = GetListByUserid(userid);
            if (dt != null && dt.Rows.Count > 0)
            {

                string ConcernState = "0";
                long GroupID = 0;


                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    GroupID = Convert.ToInt64(dt.Rows[i]["icg_id"].ToString());
                    if (ConcernGropuExistsGroupID(userid, ConcernUserID, GroupID))
                    {
                        ConcernState = "1";
                    }
                    else
                    {
                        ConcernState = "0";
                    }

                    if (i != dt.Rows.Count - 1)
                    {

                        sbjson.Append("{GroupName:'" + dt.Rows[i]["icg_name"].ToString() + "',GroupID:'" + GroupID.ToString() + "',MenuLive:'" + ConcernState.ToString() + "'},");
                    }
                    else
                    {
                        sbjson.Append("{GroupName:'" + dt.Rows[i]["icg_name"].ToString() + "',GroupID:'" + GroupID.ToString() + "',MenuLive:'" + ConcernState.ToString() + "'}");
                    }
                }
            }
            else
            {
                sbjson.Append("");

            }

            return sbjson.ToString();
        }


        /// <summary>
        /// 返回一个
        /// </summary>
        /// <param name="userid">用户名</param>
        /// <returns>返回一个结果</returns>
        public static string GetJsonGroupName(long groupid)
        {
            StringBuilder sbjson = new StringBuilder();

            DataTable dt = ILog.DAL.ILogConcernGroup.GetModel(groupid);
            if (dt != null && dt.Rows.Count > 0)
            {
                sbjson.Append("{GroupName:'" + dt.Rows[0]["icg_name"].ToString() + "'}");
            }
            else
            {
                sbjson.Append("");
            }

            return sbjson.ToString();
        }



        
        /// <summary>
        /// 获取某用户关注人所在用户组
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>返一个DataTable类型的结果集</returns>
        public static DataTable GetListByConcernGoup(long userid, long concernuserid)
        {
            DataTable dt = ILog.DAL.ILogConcernGroup.GetListByConcernGoup(userid, concernuserid);

            return dt;
        }

        #region 加关注
        /// <summary>
        /// 加关注
        /// </summary>
        /// <param name="UserID">当前用户id</param>
        /// <param name="concernuserid">被关注者id</param>
        /// <param name="GroupID">组id</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string CerateUserConcernConnect(long UserID, long concernuserid, long GroupID,int type)
        {
            StringBuilder sbRe = new StringBuilder();

            //判断用户组关系是否已经建立
            if (ConcernGropuExistsGroupID(UserID, concernuserid, GroupID))
            {
                if (type == 0)
                {
                    //存在取消
                    if (ConcernGroupDel_GroupID(UserID, concernuserid, GroupID)>0)
                    {
                        sbRe.Append("[{UserState:1},");

                        if (ConcernGropuExistsUserid(UserID,concernuserid))
                        {
                            Ilog.BLL.ILogUserConcern.Update_icgid(UserID, concernuserid, 1);
                        }
                        else
                        {
                            Ilog.BLL.ILogUserConcern.Update_icgid(UserID, concernuserid, 0);
                        }

                        sbRe.Append(Ilog.BLL.ILogConcernGroup.GetJsonFollowGroupConcernGroup(UserID, concernuserid));
                    }
                    else
                    {
                        sbRe.Append("[{UserState:0}]");
                    }
                }
                else
                {
                    //已经存在
                    sbRe.Append("[{UserState:2}");

                }
            }
            else
            {
                if (type == 0)
                {
                   //不存在，且没有绑定
                   sbRe.Append("[{UserState:2}");
                }
                else
                {
                   //不存在，加联系组
                    if (ConcernGroupContentAdd(UserID, concernuserid, GroupID) > 0)
                    {
                        if (ConcernGropuExistsUserid(UserID,concernuserid))
                        {
                            Ilog.BLL.ILogUserConcern.Update_icgid(UserID, concernuserid, 1);
                        }
                        else
                        {
                            Ilog.BLL.ILogUserConcern.Update_icgid(UserID, concernuserid, 0);
                        }
                        sbRe.Append("[{UserState:1},");
                        sbRe.Append(Ilog.BLL.ILogConcernGroup.GetJsonFollowGroupConcernGroup(UserID, concernuserid));

                    }
                    else
                    {
                        sbRe.Append("[{UserState:0}]");
                    }
                }
            }

            return sbRe.ToString();
        }

        #endregion


        #region 加关注搜索用或其他用
        /// <summary>
        /// 加关注搜索用或其他用
        /// </summary>
        /// <param name="UserID">当前用户id</param>
        /// <param name="concernuserid">被关注者id</param>
        /// <param name="GroupID">组id</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string CerateUserConcernConnect_s(long UserID, long concernuserid, string GroupID)
        {
            StringBuilder sbRe = new StringBuilder();

            string[] arrGroupID = GroupID.Split(',');

            int count = arrGroupID.Length;

            //开始绑定组
	        
            for (int i = 0; i < count-1; i++)
            {
                //判断用户组关系是否已经建立
                if (!ConcernGropuExistsGroupID(UserID, concernuserid, Convert.ToInt64(arrGroupID[i])))
                {
                    //不存在，加联系组
                    ConcernGroupContentAdd(UserID, concernuserid, Convert.ToInt64(arrGroupID[i]));
                }
            }


            Ilog.BLL.ILogUserConcern.Update_icgid(UserID, concernuserid,1);

            sbRe.Append("[{UserState:1}");

            return sbRe.ToString();
        }

        #endregion




    }
}
