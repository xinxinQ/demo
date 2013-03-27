using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using Com.ILog.Data;
using Com.ILog.Utils;

namespace ILog.DAL
{
    public class ILogUserConcern
    {
        #region 查看用户关系是否存在（True：存在，False：不存在）
        /// <summary>
        /// 查看用户关系是否存在（True：存在，False：不存在）
        /// </summary>
        public static bool UserConcernExists(long userid,long concernuserid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
                    new SqlParameter("@concernuserid",SqlDbType.BigInt)
                                        };
            parameters[0].Value = userid;
            parameters[1].Value = concernuserid;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("SP_ilog_userconcern_UserConcernExists", "isEXISTS", parameters) == "1" ? true : false;
        }


        #endregion

        #region 增加一条用户关系
        /// <summary>
        /// 增加一条用户关系
        /// <param name="model">用户关系实体</param>
        /// </summary>
        public static int UserConcernAdd(ILog.Model.ILogUserConcern model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt,8),
					new SqlParameter("@concernuserid", SqlDbType.BigInt,8),
					new SqlParameter("@icg_id", SqlDbType.BigInt,8),
					new SqlParameter("@iuc_state", SqlDbType.Bit,1)};
            parameters[0].Value = model.userid;
            parameters[1].Value = model.concernuserid;
            parameters[2].Value = model.icg_id;
            parameters[3].Value = model.iuc_state;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_userconcern_UserConcernAdd", parameters);
        }
        #endregion

        #region 更新一条用户关系
        /// <summary>
        /// 更新一条用户关系
        /// <param name="model">更新一条用户关系</param>
        /// </summary>
        public static int UserConcernUpdate(ILog.Model.ILogUserConcern model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@iuc_id", SqlDbType.BigInt,8),
					new SqlParameter("@userid", SqlDbType.BigInt,8),
					new SqlParameter("@concernuserid", SqlDbType.BigInt,8),
					new SqlParameter("@icg_id", SqlDbType.BigInt,8),
					new SqlParameter("@iuc_state", SqlDbType.Bit,1)};
            parameters[0].Value = model.iuc_id;
            parameters[1].Value = model.userid;
            parameters[2].Value = model.concernuserid;
            parameters[3].Value = model.icg_id;
            parameters[4].Value = model.iuc_state;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_userconcern_UserConcernUpdate", parameters);
        }
        #endregion

        #region 删除一条用户关系
        /// <summary>
        /// 删除一条用户关系
        /// <param name="iuc_id">流水号</param>
        /// </summary>
        public static int UserConcernDel(long userid, long concernuserid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
                    new SqlParameter("@concernuserid",SqlDbType.BigInt)};
            parameters[0].Value = userid;
            parameters[1].Value = concernuserid;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_userconcern_UserConcernDel", parameters);
        }


        /// <summary>
        /// 删除我的关注
        /// <param name="iuc_id">流水号</param>
        /// </summary>
        public static int Delete_ConcernUserID(long userid, long concernuserid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
                    new SqlParameter("@concernuserid",SqlDbType.BigInt)
                                        };

            parameters[0].Value = userid;
            parameters[1].Value = concernuserid;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("sp_ilog_concern_delete_Concernuserid", parameters);
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static DataTable GetModel(int iuc_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@iuc_id", SqlDbType.BigInt)};
            parameters[0].Value = iuc_id;

            return Com.ILog.Data.DataAggregate.GetDateTabel("SP_ilog_userconcern_GetUserConcernInfo", parameters);
        }
        #endregion

        #region 根据用户编号和当前用户获取用户关注信息

        /// <summary>
        /// 功能描述：根据用户编号和当前用户获取用户关注信息
        /// </summary>
        /// <param name="userId">悬浮用户id</param>
        /// <param name="currentuserid">当前用户id</param>
        /// <returns></returns>
        public static ILog.Model.ILogUserConcern GetIlogUserconcernInfoByUserId(long userId,long currentuserid)
        {

            ILog.Model.ILogUserConcern model = null;

            try
            {

                  SqlParameter[] parameters =
                    {	
    				
                    new SqlParameter("userid", SqlDbType.BigInt),
                    new SqlParameter("currentuserid", SqlDbType.BigInt)

                    };

                parameters[0].Value = userId;
                parameters[1].Value = currentuserid;

                model = new ILog.Model.ILogUserConcern();

                DataTable dataTable = Com.ILog.Data.DataAggregate.GetDateTabel("SP_vip_ilog_GetIlogUserconcernInfoByUserId", parameters);

                if (dataTable.Rows.Count > 0)
                {
                    if (dataTable.Rows[0]["iuc_id"].ToString() != "")
                    {
                        model.iuc_id = int.Parse(dataTable.Rows[0]["iuc_id"].ToString());
                    }
                    if (dataTable.Rows[0]["userid"].ToString() != "")
                    {
                        model.userid = int.Parse(dataTable.Rows[0]["userid"].ToString());
                    }
                    if (dataTable.Rows[0]["concernuserid"].ToString() != "")
                    {
                        model.concernuserid = int.Parse(dataTable.Rows[0]["concernuserid"].ToString());
                    }
                    if (dataTable.Rows[0]["icg_id"].ToString() != "")
                    {
                        model.icg_id = int.Parse(dataTable.Rows[0]["icg_id"].ToString());
                    }

                    if (dataTable.Rows[0]["iuc_state"].ToString() != "")
                    {
                        if ((dataTable.Rows[0]["iuc_state"].ToString() == "1") || (dataTable.Rows[0]["iuc_state"].ToString().ToLower() == "true"))
                        {
                            model.iuc_state = true;
                        }
                        else
                        {
                            model.iuc_state = false;
                        }
                    }

                    return model;

                }


            }
            catch (Exception ex)
            {

                return null;

            }

            return null;

        }


        #endregion



        /// <summary>
        /// 更新最后联系时间
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="concernuserid">最后发送时间</param>
        /// <returns></returns>
        public static int Update_ConnectTime(long userid,long concernuserid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
					new SqlParameter("@concernuserid", SqlDbType.BigInt)
                                        };
            parameters[0].Value = userid;
            parameters[1].Value = concernuserid;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("sp_ilog_userconcern_connecttimeUpdate", parameters);
        }

        /// <summary>
        /// 更新粉丝数关注表中的粉丝数
        /// </summary>
        /// <param name="concernuserid">用户ID</param>
        /// <returns>返回一个值</returns>
        public static int Update_ConnecrnFanNum(long concernuserid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@concernuserid", SqlDbType.BigInt)
                                        };
            parameters[0].Value = concernuserid;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("sp_ilog_userconcern_UpdateFanumtnum", parameters);
        }


        /// <summary>
        /// 更新是否有组关联
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="concernuserid">最后发送时间</param>
        /// <returns></returns>
        public static int Update_icgid(long userid, long concernuserid,long icg_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
					new SqlParameter("@concernuserid", SqlDbType.BigInt),
                    new SqlParameter("@icg_id",SqlDbType.BigInt)
                                        };
            parameters[0].Value = userid;
            parameters[1].Value = concernuserid;
            parameters[2].Value = icg_id;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("sp_ilog_userconcern_update_icg_id", parameters);
        }

        /// <summary>
        /// 判断是否已经互相关注
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="concernuserid">关注用户</param>
        /// <returns></returns>
        public static bool UserConcern_State(long userid, long concernuserid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
					new SqlParameter("@concernUserID", SqlDbType.BigInt)
                                        };
            parameters[0].Value = userid;
            parameters[1].Value = concernuserid;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("sp_ilog_userconcern_Exists_IucState", "isEXISTS", parameters) == "1" ? true : false;

        }

        /// <summary>
        /// 判断是否已经单向关注
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="concernuserid">关注用户</param>
        /// <returns></returns>
        public static bool UserConcernOnly_State(long userid, long concernuserid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
					new SqlParameter("@concernUserID", SqlDbType.BigInt)
                                        };
            parameters[0].Value = userid;
            parameters[1].Value = concernuserid;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("sp_ilog_userconcern_Exists", "isEXISTS", parameters) == "1" ? true : false;

        }

        /// <summary>
        /// 更新互相关注状态
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="concernuserid">关注用户</param>
        /// <returns></returns>
        public static int UserConcernDouble_State(long userid, long concernuserid,int isState)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
					new SqlParameter("@concernuserid", SqlDbType.BigInt),
                    new SqlParameter("@iuc_state",SqlDbType.Int)
                                        };
            parameters[0].Value = userid;

            parameters[1].Value = concernuserid;

            parameters[2].Value = isState;


            return Com.ILog.Data.DataAggregate.EXECprocedureCount("sp_ilog_concern_update_iucstate", parameters);

        }

        #region 根据用户id得到用户的前9个关注用户id
        /// <summary>
        /// 功能描述：根据用户id得到用户的前9个关注用户id
        /// 创建标识：ljd 20120627
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static List<Model.ILogUserConcern> GetUserConcernTopNineListByUserid(long userid)
        {
            SqlParameter[] Parm = new SqlParameter[1];
            Parm[0] = new SqlParameter("@userid", SqlDbType.BigInt);

            Parm[0].Value = userid;

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_userconcern_GetUserConcernTopList", Parm);

            List<Model.ILogUserConcern> concernList = new List<Model.ILogUserConcern>();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model.ILogUserConcern ooUserConcern = new ILog.Model.ILogUserConcern();

                        ooUserConcern.concernuserid = Convert.ToInt64(Utils.ChangeType(reader["concernuserid"], typeof(long)));
                        ooUserConcern.intime = Convert.ToDateTime(Utils.ChangeType(reader["intime"], typeof(DateTime)));

                        concernList.Add(ooUserConcern);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
                reader.Close();
                reader.Dispose();
            }
            return concernList;

        }
        #endregion


        public static DataTable GetListConcernUserName_UserID(long userID, string NickName)
        { 
                   

                  SqlParameter[] parameters =
                    {	
    				
                       new SqlParameter("@userid", SqlDbType.BigInt),
                       new SqlParameter("@nickname",SqlDbType.VarChar)

                    };

                  parameters[0].Value = userID;

                  parameters[1].Value = NickName;


                DataTable dataTable = Com.ILog.Data.DataAggregate.GetDateTabel("sp_ilg_userconcern_GetList_Userid", parameters);

                return dataTable;
        }
    }
}
