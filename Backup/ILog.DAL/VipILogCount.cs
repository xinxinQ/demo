using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using Com.ILog.Data;
using Com.ILog.Utils;

namespace ILog.DAL
{
    public class VipILogCount
    {
        #region 用户统计是否存在（True：存在，False：不存在）
        /// <summary>
        /// 用户统计是否存在（True：存在，False：不存在）
        /// <param name="vic_id">流水号</param>
        /// <param name="userid">用户id</param> 
        /// </summary>
        public static bool CountExists(int userid, int vic_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
					new SqlParameter("@vic_id", SqlDbType.BigInt)};
            parameters[0].Value = userid;
            parameters[1].Value = vic_id;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("SP_vip_ilog_count_CountExists", "isEXISTS", parameters) == "1" ? true : false;
        }
        #endregion

        #region 增加一条用户统计
        /// <summary>
        /// 增加一条用户统计
        /// <param name="model">统计表实体</param>
        /// </summary>
        public static int CountAdd(ILog.Model.VipILogCount model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt,8),
					new SqlParameter("@vic_concernnum", SqlDbType.BigInt,8),
					new SqlParameter("@vic_onewayconcernnum", SqlDbType.BigInt,8),
					new SqlParameter("@vic_doubleconcernnum", SqlDbType.BigInt,8),
					new SqlParameter("@vic_fannum", SqlDbType.BigInt,8),
					new SqlParameter("@vic_ilognum", SqlDbType.BigInt,8),
					new SqlParameter("@vic_commentcountnum", SqlDbType.BigInt,8),
					new SqlParameter("@vic_fanoutnum", SqlDbType.BigInt,8),
					new SqlParameter("@vic_messageoutnum", SqlDbType.BigInt,8),
					new SqlParameter("@vic_messagenum", SqlDbType.BigInt,8),
					new SqlParameter("@vic_atnum", SqlDbType.BigInt,8),
					new SqlParameter("@vic_commentnum", SqlDbType.BigInt,8)};
            parameters[0].Value = model.userid;
            parameters[1].Value = model.vic_concernnum;
            parameters[2].Value = model.vic_onewayconcernnum;
            parameters[3].Value = model.vic_doubleconcernnum;
            parameters[4].Value = model.vic_fannum;
            parameters[5].Value = model.vic_ilognum;
            parameters[6].Value = model.vic_commentcountnum;
            parameters[7].Value = model.vic_fanoutnum;
            parameters[8].Value = model.vic_messageoutnum;
            parameters[9].Value = model.vic_messagenum;
            parameters[10].Value = model.vic_atnum;
            parameters[11].Value = model.vic_commentnum;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_vip_ilog_count_CountAdd", parameters);
        }
        #endregion

        #region 更新用户统计
        /// <summary>
        /// 更新用户统计
        /// <param name="model">统计表实体</param>
        /// </summary>
        public static int CountUpdate(ILog.Model.VipILogCount model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@vic_id", SqlDbType.BigInt,8),
					new SqlParameter("@userid", SqlDbType.BigInt,8),
					new SqlParameter("@vic_concernnum", SqlDbType.BigInt,8),
					new SqlParameter("@vic_onewayconcernnum", SqlDbType.BigInt,8),
					new SqlParameter("@vic_doubleconcernnum", SqlDbType.BigInt,8),
					new SqlParameter("@vic_fannum", SqlDbType.BigInt,8),
					new SqlParameter("@vic_ilognum", SqlDbType.BigInt,8),
					new SqlParameter("@vic_commentoutnum", SqlDbType.BigInt,8),
					new SqlParameter("@vic_fanoutnum", SqlDbType.BigInt,8),
					new SqlParameter("@vic_messageoutnum", SqlDbType.BigInt,8),
					new SqlParameter("@vic_messagenum", SqlDbType.BigInt,8),
					new SqlParameter("@vic_atnum", SqlDbType.BigInt,8),
					new SqlParameter("@vic_commentnum", SqlDbType.BigInt,8)};
            parameters[0].Value = model.vic_id;
            parameters[1].Value = model.userid;
            parameters[2].Value = model.vic_concernnum;
            parameters[3].Value = model.vic_onewayconcernnum;
            parameters[4].Value = model.vic_doubleconcernnum;
            parameters[5].Value = model.vic_fannum;
            parameters[6].Value = model.vic_ilognum;
            parameters[7].Value = model.vic_commentcountnum;
            parameters[8].Value = model.vic_fanoutnum;
            parameters[9].Value = model.vic_messageoutnum;
            parameters[10].Value = model.vic_messagenum;
            parameters[11].Value = model.vic_atnum;
            parameters[12].Value = model.vic_commentnum;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_vip_ilog_count_CountUpdate", parameters);
        }

        /// <summary>
        /// 更新提醒
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int UpdateOutMessageNum(long userid)
        {
            SqlParameter[] parameters = {

					new SqlParameter("@userid", SqlDbType.BigInt)
                                        };

            parameters[0].Value = userid;


            return Com.ILog.Data.DataAggregate.EXECprocedureCount("sp_vip_ilog_count_updateNum", parameters);
        }


        /// <summary>
        /// 取消粉丝提醒
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int UpdateOutFanNumConcel(long userid)
        {
            SqlParameter[] parameters = {

					new SqlParameter("@userid", SqlDbType.BigInt)
                                        };

            parameters[0].Value = userid;


            return Com.ILog.Data.DataAggregate.EXECprocedureCount("sp_ilog_count_fanoutnum", parameters);
        }

        /// <summary>
        /// 更新我的关注
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="concernNum">关注数</param>
        /// <returns></returns>
        public static int UpdateConcernNum(long userid,int concernNum)
        {
            SqlParameter[] parameters = {

					new SqlParameter("@userid", SqlDbType.BigInt),
                    new SqlParameter("@concernnum",SqlDbType.Int)
                   
                                        };

            parameters[0].Value = userid;
            parameters[1].Value = concernNum;


            return Com.ILog.Data.DataAggregate.EXECprocedureCount("sp_vip_ilog_count_Updateconcernnum", parameters);
        }


        /// <summary>
        /// 更新互相关注
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="concernuNum"></param>
        /// <returns></returns>
        public static int UpdateDoubleConcernNum(long userid, int concernuNum)
        {
            SqlParameter[] parameters = {

					new SqlParameter("@userid", SqlDbType.BigInt),
                    new SqlParameter("@doubleconcernnum",SqlDbType.Int)
                   
                                        };

            parameters[0].Value = userid;
            parameters[1].Value = concernuNum;


            return Com.ILog.Data.DataAggregate.EXECprocedureCount("sp_ilog_count_doubleconcernnum", parameters);
        }


        /// <summary>
        /// 更新我的粉丝带提醒
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="concernNum">关注数</param>
        /// <returns></returns>
        public static int UpdateFanOutNum(long userid, int fanNum)
        {
            SqlParameter[] parameters = {

					new SqlParameter("@userid", SqlDbType.BigInt),
                    new SqlParameter("@fannum",SqlDbType.Int)
                   
                                        };

            parameters[0].Value = userid;
            parameters[1].Value = fanNum;


            return Com.ILog.Data.DataAggregate.EXECprocedureCount("sp_vip_ilog_count_UpdateFanumoutnum", parameters);
        }

        /// <summary>
        /// 更新我的粉丝
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="concernNum">关注数</param>
        /// <returns></returns>
        public static int UpdateFanNum(long userid, int fanNum)
        {
            SqlParameter[] parameters = {

					new SqlParameter("@userid", SqlDbType.BigInt),
                    new SqlParameter("@fannum",SqlDbType.Int)
                   
                                        };

            parameters[0].Value = userid;
            parameters[1].Value = fanNum;


            return Com.ILog.Data.DataAggregate.EXECprocedureCount("sp_vip_ilog_count_UpdateFanum", parameters);
        } 
        #endregion

        #region 删除一条用户统计
        /// <summary>
        /// 删除一条用户统计
        /// <param name="vic_id">流水号</param>
        /// <param name="userid">用户id</param> 
        /// </summary>
        public static int CountDel(int userid, int vic_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
					new SqlParameter("@vic_id", SqlDbType.BigInt)};
            parameters[0].Value = userid;
            parameters[1].Value = vic_id;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_vip_ilog_count_CountDel", parameters);
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static DataTable GetModel(long userid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt)
                                        };
            parameters[0].Value = userid;

            return Com.ILog.Data.DataAggregate.GetDateTabel("SP_vip_ilog_count_GetCountInfo", parameters);
        }
        #endregion

        #region 用户@数加1
        /// <summary>
        /// 功能描述：用户@数加1
        /// 创建标识：ljd 20120526
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="urlstate">json是否成功</param>
        /// <returns></returns>
        public static int UpdateAtNum(long userid, ref int urlstate)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt)};

            parameters[0].Value = userid;

            SqlConnection con = DbHelperSQL.GetConnection();

            //执行行数
            int resultCount = 0;

            try
            {
                resultCount = DbHelperSQL.ExecuteNonQuery(con, CommandType.StoredProcedure, "SP_vip_ilog_count_UpdateAtCount", parameters);
                urlstate = 1;
            }
            catch (Exception e)
            {
                urlstate = 0;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return resultCount;

        }
        #endregion

        #region 用户@数与评论数加1
        /// <summary>
        /// 功能描述：用户@数与评论数加1
        /// 创建标识：ljd 20120612
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="urlstate">json是否成功</param>
        /// <returns></returns>
        public static int UpdateAtNumAndCommentNum(long userid, ref int urlstate)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt)};

            parameters[0].Value = userid;

            SqlConnection con = DbHelperSQL.GetConnection();

            //执行行数
            int resultCount = 0;

            try
            {
                resultCount = DbHelperSQL.ExecuteNonQuery(con, CommandType.StoredProcedure, "SP_vip_ilog_count_UpdateAtCountAndCommentCount", parameters);
                urlstate = 1;
            }
            catch (Exception e)
            {
                urlstate = 0;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return resultCount;

        }
        #endregion

        #region 用户评论数加1
        /// <summary>
        /// 功能描述：用户评论数加1
        /// 创建标识：ljd 20120612
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="urlstate">json是否成功</param>
        /// <returns></returns>
        public static int UpdateCommentNum(long userid, ref int urlstate)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt)};

            parameters[0].Value = userid;

            SqlConnection con = DbHelperSQL.GetConnection();

            //执行行数
            int resultCount = 0;

            try
            {
                resultCount = DbHelperSQL.ExecuteNonQuery(con, CommandType.StoredProcedure, "SP_vip_ilog_count_UpdateCommentCount", parameters);
                urlstate = 1;
            }
            catch (Exception e)
            {
                urlstate = 0;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return resultCount;

        }
        #endregion

        #region 用户博文数加1
        /// <summary>
        /// 功能描述：用户博文数加1
        /// 创建标识：ljd 20120621
        /// <param name="model">统计表实体</param>
        /// </summary>
        /// <returns></returns>
        public static int UpdateLogCount(long userid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt,8)};

            parameters[0].Value = userid;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_vip_ilog_count_UpdatelogCount", parameters);
        }
        #endregion

        #region 根据userid得到数量实体
        /// <summary>
        /// 功能描述：根据userid得到数量实体
        /// 创建标识：ljd 20120621
        /// </summary>
        /// <returns></returns>
        public static Model.VipILogCount GetModelByUserID(long userid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt)
                                        };
            parameters[0].Value = userid;

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_vip_ilog_count_GetCountInfo", parameters);

            Model.VipILogCount ooIlogCount = new ILog.Model.VipILogCount();

            try
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        ooIlogCount.vic_id = Convert.ToInt64(Utils.ChangeType(reader["vic_id"], typeof(long)));
                        ooIlogCount.userid = Convert.ToInt64(Utils.ChangeType(reader["userid"], typeof(long)));
                        ooIlogCount.vic_concernnum = Convert.ToInt32(Utils.ChangeType(reader["vic_concernnum"], typeof(int)));
                        ooIlogCount.vic_onewayconcernnum = Convert.ToInt32(Utils.ChangeType(reader["vic_onewayconcernnum"], typeof(int)));
                        ooIlogCount.vic_doubleconcernnum = Convert.ToInt32(Utils.ChangeType(reader["vic_onewayconcernnum"], typeof(int)));
                        ooIlogCount.vic_fannum = Convert.ToInt32(Utils.ChangeType(reader["vic_fannum"], typeof(int)));
                        ooIlogCount.vic_ilognum = Convert.ToInt32(Utils.ChangeType(reader["vic_ilognum"], typeof(int)));
                        ooIlogCount.vic_commentcountnum = Convert.ToInt32(Utils.ChangeType(reader["vic_commentcountnum"], typeof(int)));
                        ooIlogCount.vic_fanoutnum = Convert.ToInt32(Utils.ChangeType(reader["vic_fanoutnum"], typeof(int)));
                        ooIlogCount.vic_messageoutnum = Convert.ToInt32(Utils.ChangeType(reader["vic_messageoutnum"], typeof(int)));
                        ooIlogCount.vic_messagenum = Convert.ToInt32(Utils.ChangeType(reader["vic_messagenum"], typeof(int)));
                        ooIlogCount.vic_atnum = Convert.ToInt32(Utils.ChangeType(reader["vic_atnum"], typeof(int)));
                        ooIlogCount.vic_commentnum = Convert.ToInt32(Utils.ChangeType(reader["vic_commentnum"], typeof(int)));
                        ooIlogCount.intime = Convert.ToDateTime(Utils.ChangeType(reader["intime"], typeof(DateTime)));
                    }
                }
            }
            catch(Exception ex)
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
            return ooIlogCount;

        }
        #endregion

        #region 清除用户@数
        /// <summary>
        /// 功能描述：清除用户@数
        /// 创建标识：ljd 20120711
        /// <param name="userid">用户id</param>
        /// </summary>
        /// <returns></returns>
        public static int ClearAtCount(long userid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt,8)};

            parameters[0].Value = userid;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_vip_ilog_count_ClearAtCount", parameters);

        }
        #endregion

    }
}
