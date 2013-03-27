using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using Com.ILog.Data;
using Com.ILog.Utils;

namespace ILog.DAL
{
    public class ILogSource
    {
        #region 查看来源信息是否存在（True：存在，False：不存在）
        /// <summary>
        /// 查看来源信息是否存在（True：存在，False：不存在）
        /// <param name="is_id">流水号</param>
        /// </summary>
        public static bool SourceExists(int is_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@is_id", SqlDbType.BigInt)};
            parameters[0].Value = is_id;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("SP_ilog_source_SourceExists", "isEXISTS", parameters) == "1" ? true : false;
        }
        #endregion

        #region 增加一条来源
        /// <summary>
        /// 增加一条来源
        /// <param name="model">原来表实体</param>
        /// </summary>
        public static int SourceAdd(ILog.Model.ILogSource model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@is_name", SqlDbType.NVarChar,50),
					new SqlParameter("@is_url", SqlDbType.VarChar,500)};
            parameters[0].Value = model.is_name;
            parameters[1].Value = model.is_url;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_source_SourceAdd", parameters);
        }
        #endregion

        #region 更新一条来源
        /// <summary>
        /// 更新一条来源
        /// <param name="model">来源表实体</param>
        /// </summary>
        public static int SourceUpdate(ILog.Model.ILogSource model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@is_id", SqlDbType.BigInt,8),
					new SqlParameter("@is_name", SqlDbType.NVarChar,50),
					new SqlParameter("@is_url", SqlDbType.VarChar,500)};
            parameters[0].Value = model.is_id;
            parameters[1].Value = model.is_name;
            parameters[2].Value = model.is_url;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_source_SourceUpdate", parameters);
        }
        #endregion

        #region 删除一条来源数据
        /// <summary>
        /// 删除一条来源数据
        /// <param name="is_id">流水号</param>
        /// </summary>
        public static int SourceDel(int is_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@is_id", SqlDbType.BigInt)};
            parameters[0].Value = is_id;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_source_SourceDel", parameters);
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="is_id">流水号</param>
        /// </summary>
        public static DataTable GetModel(int is_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@is_id", SqlDbType.BigInt)};
            parameters[0].Value = is_id;

            return Com.ILog.Data.DataAggregate.GetDateTabel("SP_ilog_source_GetSourceInfo", parameters);
        }
        #endregion

        #region 得到链接来源的详细信息
        /// <summary>
        /// 功能描述：得到链接来源的详细信息
        /// 创建标识：ljd 20120628
        /// <param name="isu_id">流水号</param>
        /// </summary>
        public static Model.ILogSource GetModelById(long is_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@is_id", SqlDbType.BigInt)};
            parameters[0].Value = is_id;

            SqlConnection con = Com.ILog.Data.DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_source_GetSourceInfo", parameters);

            Model.ILogSource ooSource = new ILog.Model.ILogSource();

            try
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        ooSource.is_id = Convert.ToInt64(Utils.ChangeType(reader["is_id"], typeof(long)));
                        ooSource.is_name = Utils.ChangeType(reader["is_name"], typeof(string)).ToString();
                        ooSource.is_url = Utils.ChangeType(reader["is_url"], typeof(string)).ToString();
                        ooSource.intime = Convert.ToDateTime(Utils.ChangeType(reader["intime"], typeof(DateTime)));
                    }
                }
                else
                {
                    ooSource = null;
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
            return ooSource;

        }
        #endregion
    }
}
