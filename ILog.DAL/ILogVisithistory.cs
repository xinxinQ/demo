using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;

using Com.ILog.Data;
using Com.ILog.Utils;

namespace ILog.DAL
{
    public class ILogVisithistory
    {
        #region 添加访问记录
        /// <summary>
        /// 功能描述：添加访问记录
        /// 创建标识：ljd 20120625
        /// </summary>
        /// <param name="ooSchool">访问记录实体对象</param>
        /// <returns></returns>
        public static int AddVisitHistory(Model.ILogVisithistory ooVisitHistory)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
					new SqlParameter("@iv_userid", SqlDbType.BigInt),
                    new SqlParameter("@intime",SqlDbType.DateTime)};

            parameters[0].Value = ooVisitHistory.userid;
            parameters[1].Value = ooVisitHistory.iv_userid;
            parameters[2].Value = ooVisitHistory.intime;

            SqlConnection con = DbHelperSQL.GetConnection();

            //执行行数
            int resultCount = 0;

            try
            {
                resultCount = DbHelperSQL.ExecuteNonQuery(con, CommandType.StoredProcedure, "SP_ilog_visithistory_Add", parameters);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return resultCount;

        }
        #endregion

        #region 得到当前用户访问过的前15个用户（我看过谁）
        /// <summary>
        /// 功能描述：得到当前用户访问过的前15个用户（我看过谁）
        /// 创建标识：ljd 20120626
        /// </summary>
        /// <param name="userid">当前用户id</param>
        /// <returns></returns>
        public static List<Model.ILogVisithistory> GetVisitList(long userid)
        {
            SqlParameter[] Parm = new SqlParameter[1];
            Parm[0] = new SqlParameter("@userid", SqlDbType.BigInt);

            Parm[0].Value = userid;

            SqlConnection con = Com.ILog.Data.DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_visithistory_GetVisitList", Parm);

            List<Model.ILogVisithistory> historyList = new List<ILog.Model.ILogVisithistory>();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model.ILogVisithistory ooHistory = new ILog.Model.ILogVisithistory();

                        ooHistory.iv_userid = Convert.ToInt64(Utils.ChangeType(reader["iv_userid"], typeof(long)));
                        ooHistory.intime = Convert.ToDateTime(Utils.ChangeType(reader["intime"], typeof(DateTime)));

                        historyList.Add(ooHistory);
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
            return historyList;

        }
        #endregion

        #region 得到访问过当前用户的前15个用户（谁看过我）
        /// <summary>
        /// 功能描述：得到访问过当前用户的前15个用户（谁看过我）
        /// 创建标识：ljd 20120626
        /// </summary>
        /// <param name="userid">当前用户id</param>
        /// <returns></returns>
        public static List<Model.ILogVisithistory> GetVisitedList(long userid)
        {
            SqlParameter[] Parm = new SqlParameter[1];
            Parm[0] = new SqlParameter("@iv_userid", SqlDbType.BigInt);

            Parm[0].Value = userid;

            SqlConnection con = Com.ILog.Data.DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_visithistory_GetVisitedList", Parm);

            List<Model.ILogVisithistory> historyList = new List<ILog.Model.ILogVisithistory>();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model.ILogVisithistory ooHistory = new ILog.Model.ILogVisithistory();

                        ooHistory.userid = Convert.ToInt64(Utils.ChangeType(reader["userid"], typeof(long)));
                        ooHistory.intime = Convert.ToDateTime(Utils.ChangeType(reader["intime"], typeof(DateTime)));

                        historyList.Add(ooHistory);
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
            return historyList;

        }
        #endregion

    }

}
