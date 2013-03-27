using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;

using Com.ILog.Data;

namespace ILog.DAL
{
    public class IlogTool
    {
        #region  发送手机短信

        /// <summary>
        /// 功能描述：发送手机短信
        /// 创建标识：ljd 20120522
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static int SendMobile(string mobileNumber,string content,ref int urlState)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@mobleNumber", SqlDbType.NVarChar,20),
					new SqlParameter("@content", SqlDbType.NVarChar,200)};

            parameters[0].Value = mobileNumber;
            parameters[1].Value = content;

            string sql = "vip_outbox_SendMobile";

            SqlConnection con = DbHelperSQL.GetIMConnection();

            //发送成功影响行数
            int resultcount = 0;

            try
            {
                resultcount = DbHelperSQL.ExecuteNonQuery(con, CommandType.StoredProcedure, sql, parameters);
                urlState = 1;
            }
            catch (Exception e)
            {
                urlState = 0;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return resultcount;

        }
        #endregion

    }
}
