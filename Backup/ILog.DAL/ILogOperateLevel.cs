using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ILog.DAL
{
    public class ILogOperateLevel
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int OperatelevelAdd(ILog.Model.ILogOperateLevel model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@iol_number", SqlDbType.Int,4),
					new SqlParameter("@iol_name", SqlDbType.NVarChar,50),
					new SqlParameter("@iol_actionname", SqlDbType.VarChar,100),
					new SqlParameter("@iol_actionnumber", SqlDbType.Int,4),
					new SqlParameter("@intime", SqlDbType.DateTime)};
            parameters[0].Value = model.iol_number;
            parameters[1].Value = model.iol_name;
            parameters[2].Value = model.iol_actionname;
            parameters[3].Value = model.iol_actionnumber;
            parameters[4].Value = model.intime;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_operatelevel_OperatelevelAdd", parameters);
        }
    }
}
