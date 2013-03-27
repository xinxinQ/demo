using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ILog.DAL
{
    public class ILogConcernGroup
    {
        #region 查看某组是否存在（True：存在，False：不存在）
        /// <summary>
        /// 查看某组是否存在（True：存在，False：不存在）
        /// <param name="icg_id">流水号</param>
        /// </summary>
        public static bool ConcernGroupExists(long icg_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@icg_id", SqlDbType.BigInt)};
            parameters[0].Value = icg_id;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("SP_ilog_concerngroup_ConcerngroupExists", "isEXISTS", parameters) == "1" ? true : false;
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
            SqlParameter[] parameters = {
                    new SqlParameter("@userid",SqlDbType.BigInt),
                    new SqlParameter("@concernUserID",SqlDbType.BigInt),
					new SqlParameter("@icg_id", SqlDbType.BigInt)};
            parameters[0].Value = userID;
            parameters[1].Value = ConcernUserID;
            parameters[2].Value = GroupID;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("sp_ilogConcernUserConnect_Exists", "isEXISTS", parameters) == "1" ? true : false;

        }

        /// <summary>
        /// 判断用户组是否存在
        /// </summary>
        /// <param name="userID">用户id</param>
        /// <param name="ConcernUserID">关注用户</param>
        /// <returns>返回一个bool值</returns>
        public static bool ConcernGropuExistsUserid(long userID, long ConcernUserID)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@userid",SqlDbType.BigInt),
                    new SqlParameter("@concernUserID",SqlDbType.BigInt)};
            parameters[0].Value = userID;
            parameters[1].Value = ConcernUserID;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("sp_ilogConcernUserConnect_ExistsUserid", "isEXISTS", parameters) == "1" ? true : false;

        }
       
        /// <summary>
        /// 查看某组是否存在（True：存在，False：不存在）
        /// <param name="userid">用户名</param>
        /// <param name="mode">实体</param>
        /// </summary>
        public static bool ConcernGroupExistsUserID(ILog.Model.ILogConcernGroup mode)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
                    new SqlParameter("@icg_name",SqlDbType.VarChar)
                    };
            parameters[0].Value = mode.userid;
            parameters[1].Value = mode.icg_name;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("SP_ilog_concerngroup_ConcerngroupExistsUserid", "isEXISTS", parameters) == "1" ? true : false;
        }


        /// <summary>
        /// 获取组id
        /// <param name="userid">用户名</param>
        /// <param name="mode">实体</param>
        /// </summary>
        public static int GetGroupID_UserID_IcgName(long userid,string icg_Name)
        {



            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
                    new SqlParameter("@icg_name",SqlDbType.VarChar)
                    };
            parameters[0].Value = userid;
            parameters[1].Value = icg_Name;


            return Com.ILog.Utils.Utils.StrToInt(Com.ILog.Data.DataAggregate.GetColumnInfo("sp_ilog_Concerngroup_icg_name", "icg_id", parameters),0); 
        }


        /// <summary>
        /// 统计用户，组的数量
        /// <param name="userid">用户名</param>
        /// </summary>
        public static string ConcernGroupCountUserID(long UserID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
                    };
            parameters[0].Value = UserID;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("SP_ilog_concerngroup_ConcerngroupUserIDCount", "UserCount", parameters); 
        }

        #endregion

        #region
        /// <summary>
        /// 增加一条数据
        /// <param name="model">用户组实体</param>
        /// </summary>
        public static string ConcernGroupAdd(ILog.Model.ILogConcernGroup model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@icg_name", SqlDbType.NVarChar,20),
					new SqlParameter("@userid", SqlDbType.BigInt)};
            parameters[0].Value = model.icg_name;
            parameters[1].Value = model.userid;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("SP_ilog_concerngroup_ConcernGroupAdd", "SIDENTITY", parameters);
        }

        /// <summary>
        /// 增加一条数据
        
        /// </summary>
        public static int ConcernGroupContentAdd(long userID, long ConcernUserID, long GroupID)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@userid",SqlDbType.BigInt),
                    new SqlParameter("@concernUserID",SqlDbType.BigInt),
					new SqlParameter("@icg_id", SqlDbType.BigInt)};

            parameters[0].Value = userID;
            parameters[1].Value = ConcernUserID;
            parameters[2].Value = GroupID;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("sp_ilogConcernUserConnect_Insert", parameters);
        }
        #endregion

        #region 更新用户组数据
        /// <summary>
        /// 更新用户组数据
        ///<param name="model">用户组实体</param>
        /// </summary>
        public static int ConcernGroupUpdate(ILog.Model.ILogConcernGroup model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@icg_id", SqlDbType.BigInt,8),
					new SqlParameter("@icg_name", SqlDbType.NVarChar,20),
					new SqlParameter("@userid", SqlDbType.BigInt,8)};
            parameters[0].Value = model.icg_id;
            parameters[1].Value = model.icg_name;
            parameters[2].Value = model.userid;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_concerngroup_ConcernGroupUpdate", parameters);
        }
        #endregion

        #region
        /// <summary>
        /// 删除用户组
        /// <param name="icg_id">流水号</param>
        /// </summary>
        public static int ConcernGroupDel(long icg_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@icg_id", SqlDbType.BigInt)};
            parameters[0].Value = icg_id;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_concerngroup_ConcernGroupDel", parameters);
        }


        /// <summary>
        /// 删除用户组关系
        /// <param name="icg_id">流水号</param>
        /// </summary>
        public static int ConcernGroupDel_GroupID(long userID, long ConcernUserID, long GroupID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
                    new SqlParameter("@concernUserID",SqlDbType.BigInt),
                    new SqlParameter("@icg_id",SqlDbType.BigInt)
                                        
                                        };
            parameters[0].Value = userID;
            parameters[1].Value = ConcernUserID;
            parameters[2].Value = GroupID;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("sp_ilogConcernUserConnect_delete", parameters);
        }

        /// <summary>
        /// 取消关注，删除用户关系分组
        /// <param name="icg_id">流水号</param>
        /// </summary>
        public static int ConcernGroupDel_ConnectUserid(long userID, long ConcernUserID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
                    new SqlParameter("@concernuserid",SqlDbType.BigInt)
                                        
                                        };
            parameters[0].Value = userID;
            parameters[1].Value = ConcernUserID;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("sp_ilog_ConcernUserConnect_deleteConnectUser", parameters);
        }


        public static DataTable GetList_ConnectUserid_IcgID(long userID, long GroupID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
                    new SqlParameter("@icg_id",SqlDbType.BigInt)
                                        };

            parameters[0].Value = userID;

            parameters[1].Value = GroupID;

            return Com.ILog.Data.DataAggregate.GetDateTabel("sp_ilog_ConcernUserConnect_icg_id", parameters);
        }
        #endregion

        #region 获取一个用户组
        /// <summary>
        /// 获取一个用户组
        /// </summary>
        public static DataTable GetModel(long icg_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@icg_id", SqlDbType.BigInt)};
            parameters[0].Value = icg_id;

            return Com.ILog.Data.DataAggregate.GetDateTabel("SP_ilog_concerngroup_GetConcernGroupInfo", parameters);
        }
        #endregion

        /// <summary>
        /// 获取某用户全部用户组
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns>返一个DataTable类型的结果集</returns>
        public static DataTable GetListByUserid(long userid)
        {
            SqlParameter[] parameters ={
                                        new SqlParameter("@userid",SqlDbType.BigInt)
                                      };
            parameters[0].Value = userid;

            return Com.ILog.Data.DataAggregate.GetDateTabel("sp_ilogConcerngroup_list", parameters);
        }

        /// <summary>
        /// 获取某用户关注人所在用户组
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>返一个DataTable类型的结果集</returns>
        public static DataTable GetListByConcernGoup(long userid,long concernuserid)
        {
            SqlParameter[] parameters ={
                                        new SqlParameter("@userid",SqlDbType.BigInt),
                                        new SqlParameter("@concernuserid",SqlDbType.BigInt)
                                      };
            parameters[0].Value = userid;
            parameters[1].Value = concernuserid;

            return Com.ILog.Data.DataAggregate.GetDateTabel("sp_ilogConcernUserConnect_GetList", parameters);
        }
    }
}
