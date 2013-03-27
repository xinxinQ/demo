using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ILog.DAL
{
    public class ILogReportAbuse
    {
        #region 查看举报否存在（True：存在，False：不存在）
        /// <summary>
        /// 查看举报否存在（True：存在，False：不存在）
        /// </summary>
        public static bool ReportAbuseExists(int ir_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ir_id", SqlDbType.BigInt)};
            parameters[0].Value = ir_id;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("SP_ilog_reportabuse_ReportAbuseExists", "isEXISTS", parameters) == "1" ? true : false;
        }
        #endregion

        #region 添加举报信息
        /// <summary>
        /// 添加举报信息
        /// <param name="model">举报表实体</param>
        /// </summary>
        public static int ReportAbuseAdd(ILog.Model.ILogReportAbuse model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt,8),
					new SqlParameter("@ir_content", SqlDbType.NVarChar,500),
					new SqlParameter("@ip", SqlDbType.VarChar,20),
					new SqlParameter("@ir_desc", SqlDbType.NVarChar,400),
					new SqlParameter("@ir_time", SqlDbType.DateTime)};
            parameters[0].Value = model.userid;
            parameters[1].Value = model.ir_content;
            parameters[2].Value = model.ip;
            parameters[3].Value = model.ir_desc;
            parameters[4].Value = model.ir_time;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_reportabuse_ReportAbuseAdd", parameters);
        }
        #endregion

        #region 更新举报信息
        /// <summary>
        /// 更新举报信息
        /// <param name="model">举报表实体</param>
        /// </summary>
        public static int ReportAbuseUpdate(ILog.Model.ILogReportAbuse model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ir_id", SqlDbType.BigInt,8),
					new SqlParameter("@userid", SqlDbType.BigInt,8),
					new SqlParameter("@ir_content", SqlDbType.NVarChar,500),
					new SqlParameter("@ip", SqlDbType.VarChar,20),
					new SqlParameter("@ir_desc", SqlDbType.NVarChar,400)};
            parameters[0].Value = model.ir_id;
            parameters[1].Value = model.userid;
            parameters[2].Value = model.ir_content;
            parameters[3].Value = model.ip;
            parameters[4].Value = model.ir_desc;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_reportabuse_ReportAbuseUpdate", parameters);
        }
        #endregion

        #region 删除一举报信息条数据
        /// <summary>
        /// 删除一举报信息条数据
        /// <param name="ir_id">流水号</param>
        /// </summary>
        public static int ReportAbuseDel(int ir_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ir_id", SqlDbType.BigInt)};
            parameters[0].Value = ir_id;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_reportabuse_ReportAbuseDel", parameters);
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="ir_id">流水号</param>
        /// </summary>
        public static DataTable GetModel(int ir_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ir_id", SqlDbType.BigInt)};
            parameters[0].Value = ir_id;

            return Com.ILog.Data.DataAggregate.GetDateTabel("SP_ilog_reportabuse_GetReportAbuseInfo", parameters);
        }
        #endregion
    }
}
