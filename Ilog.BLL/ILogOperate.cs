using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Ilog.BLL
{
    public class ILogOperate
    {
        #region  写入日志
        /// <summary>
        /// 写入日志
        /// <param name="model">日志实体</param>
        /// </summary>
        public static string Operate_ADD(ILog.Model.ILogOperate log_operate)
        {
            StringBuilder strOperate_ADD = new StringBuilder();

            strOperate_ADD.Append("var strOperate_ADDJsonObject = ");
            strOperate_ADD.Append("({");
            strOperate_ADD.Append("\"state\": \"" + ILog.DAL.ILogOperate.Operate_ADD(log_operate).ToString() + "\"");
            strOperate_ADD.Append("})");

            return strOperate_ADD.ToString();
        }
        #endregion
    }
}
