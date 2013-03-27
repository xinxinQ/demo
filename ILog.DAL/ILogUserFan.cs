using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using Com.ILog.Data;
using Com.ILog.Utils;

namespace ILog.DAL
{
    public class ILogUserFan
    {
        #region 查看粉丝是否存在
        /// <summary>
        /// 查看粉丝是否存在
        /// </summary>
        public static bool UserFanExists(int iuf_id)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters = {
					new SqlParameter("@iuf_id", SqlDbType.BigInt)};
            parameters[0].Value = iuf_id;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("SP_ilog_userfan_UserFanExists", "isEXISTS", parameters) == "1" ? true : false;
        }


        /// <summary>
        /// 查看粉丝是否存在
        /// </summary>
        public static bool isExists(long userid, long concernuserid)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
                    new SqlParameter("@concernuserid",SqlDbType.BigInt)
                                        };
            parameters[0].Value = userid;
            parameters[1].Value = concernuserid;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("SP_ilog_userfan_UserFanExists", "isEXISTS", parameters) == "1" ? true : false;
        }

        /// <summary>
        /// 判断用户是否是我的粉丝
        /// </summary>
        /// <param name="userID">我</param>
        /// <param name="ConcernUserid">用户</param>
        /// <returns></returns>
        public static bool IsExists_UserID(long userID, long ConcernUserid)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
                    new SqlParameter("@concernUserID",SqlDbType.BigInt)
                                        };
            parameters[0].Value = userID;
            parameters[1].Value = ConcernUserid;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("sp_ilogUserFan_ExistsUserid", "isEXISTS", parameters) == "1" ? true : false;

        }
        #endregion

        #region 添加粉丝
        /// <summary>
        /// 添加粉丝
        /// <param name="model">粉丝实体</param>
        /// </summary>
        public static int UserFanAdd(ILog.Model.ILogUserFan model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
					new SqlParameter("@concernuserid", SqlDbType.BigInt)};
            parameters[0].Value = model.userid;
            parameters[1].Value = model.concernuserid;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_userfan_UserFanAdd", parameters);
        }
        #endregion

        #region 更新粉丝
        /// <summary>
        /// 更新粉丝
        /// <param name="model">粉丝实体</param>
        /// </summary>
        public static int UserFanUpdate(ILog.Model.ILogUserFan model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@iuf_id", SqlDbType.BigInt,8),
					new SqlParameter("@userid", SqlDbType.BigInt,8),
					new SqlParameter("@concernuserid", SqlDbType.BigInt,8)};
            parameters[0].Value = model.iuf_id;
            parameters[1].Value = model.userid;
            parameters[2].Value = model.concernuserid;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_userfan_UserFanUpdate", parameters);
        }

        /// <summary>
        /// 更新粉丝表中关注我的用户粉丝数
        /// </summary>
        /// <param name="concernuserid">用户ID</param>
        /// <returns>返回一个值</returns>
        public static int Update_FanNum(long concernuserid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@concernuserid", SqlDbType.BigInt)
                                        };
            parameters[0].Value = concernuserid;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("sp_ilog_userfan_UpdateFanumtnum", parameters);
        }

        #endregion

        #region 删除一条粉丝
        /// <summary>
        /// 删除一条粉丝
        /// <param name="model">粉丝实体</param>
        /// </summary>
        public static int UserFanDel(int iuf_id)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@iuf_id", SqlDbType.BigInt)};
            parameters[0].Value = iuf_id;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_userfan_UserFanDel", parameters);
        }

        /// <summary>
        /// 删除一条粉丝
        /// <param name="model">粉丝实体</param>
        /// </summary>
        public static int Delete_iufID(long userid, long concernUserID)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@userid", SqlDbType.BigInt),
                    new SqlParameter("@concernUserID",SqlDbType.BigInt)
                                        };
            parameters[0].Value = userid;
            parameters[1].Value = concernUserID;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_userfan_UserFanDel", parameters);
        }

        /// <summary>
        /// 删除一条粉丝
        /// <param name="model"></param>
        /// </summary>
        public static int Delete_Userid(long userid,long concernuserid)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@userid", SqlDbType.BigInt),
                    new SqlParameter("@concernuserid",SqlDbType.BigInt)
                                        };
            parameters[0].Value = userid;
            parameters[1].Value = concernuserid;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("sp_ilog_userfan_delete_userid", parameters);
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static DataTable GetModel(int iuf_id)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@iuf_id", SqlDbType.BigInt)};
            parameters[0].Value = iuf_id;

            return Com.ILog.Data.DataAggregate.GetDateTabel("SP_ilog_userfan_GetUserFanInfo", parameters);
        }
        #endregion

        #region 根据用户id得到用户所有的粉丝的userid
        /// <summary>
        /// 功能描述：根据用户id得到用户所有的粉丝的userid
        /// 创建标识：ljd 20120610
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="urlState"></param>
        /// <returns></returns>
        public static List<long> GetUserFanListByUserid(long userid, ref int urlState)
        {
            SqlParameter[] Parm = new SqlParameter[1];
            Parm[0] = new SqlParameter("@userid", SqlDbType.BigInt);

            Parm[0].Value = userid;

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_userfan_GetListByUserid", Parm);

            List<long> fansList = new List<long>();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        long concernuserid = Convert.ToInt64(Utils.ChangeType(reader["concernuserid"], typeof(long)));

                        fansList.Add(concernuserid);
                    }
                }
                urlState = 1;
            }
            catch
            {
                urlState = 0;
            }
            finally
            {
                con.Close();
                con.Dispose();
                reader.Close();
                reader.Dispose();
            }
            return fansList;

        }
        #endregion


        #region 根据用户id得到用户的前9个粉丝
        /// <summary>
        /// 功能描述：根据用户id得到用户的前9个粉丝
        /// 创建标识：ljd 20120626
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static List<Model.ILogUserFan> GetUserFanTopNineListByUserid(long userid)
        {
            SqlParameter[] Parm = new SqlParameter[1];
            Parm[0] = new SqlParameter("@userid", SqlDbType.BigInt);

            Parm[0].Value = userid;

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_userfan_GetTopListByUserid", Parm);

            List<Model.ILogUserFan> fansList = new List<Model.ILogUserFan>();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model.ILogUserFan ooFan = new ILog.Model.ILogUserFan();

                        ooFan.concernuserid = Convert.ToInt64(Utils.ChangeType(reader["concernuserid"], typeof(long)));
                        ooFan.intime = Convert.ToDateTime(Utils.ChangeType(reader["intime"], typeof(DateTime)));

                        fansList.Add(ooFan);
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
            return fansList;

        }
        #endregion

    }
}
