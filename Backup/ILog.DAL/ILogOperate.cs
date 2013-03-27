using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ILog.DAL
{
    public class ILogOperate
    {
        #region 写入日志
        /// <summary>
        ///  写入日志
        /// </summary>
        /// <param name="log_operate">日志实体</param>
        public static int Operate_ADD(ILog.Model.ILogOperate log_operate)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@Admin_name", SqlDbType.VarChar,25),
					new SqlParameter("@oc_ActionNumber", SqlDbType.Int,4),
					new SqlParameter("@ip", SqlDbType.VarChar,15),
					new SqlParameter("@ID", SqlDbType.BigInt,8),
					new SqlParameter("@Title", SqlDbType.VarChar,200),
					new SqlParameter("@op_TableName", SqlDbType.VarChar,30)};
            parameters[0].Value = log_operate.admin_name;
            parameters[1].Value = log_operate.io_actionnumber;
            parameters[2].Value = log_operate.io_ip;
            parameters[3].Value = log_operate.io_id;
            parameters[4].Value = log_operate.io_title;
            parameters[5].Value = log_operate.io_tablename;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("log.dbo.Operate_ADD", parameters);
        }

        #endregion
    }
}
