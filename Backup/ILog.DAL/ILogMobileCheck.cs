using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using Com.ILog.Data;
using Com.ILog.Utils;

namespace ILog.DAL
{
    public class ILogMobileCheck
    {
        #region 查看发送短信是否存在（True：存在，False：不存在）
        /// <summary>
        /// 查看发送短信是否存在（True：存在，False：不存在）
        /// <param name="im_id">流水号</param>
        /// </summary>
        public static bool MobileCheckExists(int im_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@im_id", SqlDbType.Int,4)};
            parameters[0].Value = im_id;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("", "isEXISTS", parameters) == "1" ? true : false;
        }
        #endregion

        #region 增加一条数据
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static int MobileCheckAdd(ILog.Model.ILogMobileCheck model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@im_mobilenumber", SqlDbType.NVarChar,20),
					new SqlParameter("@im_checkcode", SqlDbType.NVarChar,10),
					new SqlParameter("@userid", SqlDbType.BigInt),
					new SqlParameter("@intime", SqlDbType.DateTime)};
            parameters[0].Value = model.im_mobilenumber;
            parameters[1].Value = model.im_checkcode;
            parameters[2].Value = model.userid;
            parameters[3].Value = model.intime;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_MobileCheck_MobileCheckAdd", parameters);
        }
        #endregion

        #region 更新一条数据
        /// <summary> 
        /// 更新一条数据
        /// </summary>
        public static int MobileCheckUpdate(ILog.Model.ILogMobileCheck model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@im_id", SqlDbType.Int,4),
					new SqlParameter("@im_mobilenumber", SqlDbType.NVarChar,20),
					new SqlParameter("@im_checkcode", SqlDbType.NVarChar,10)};
            parameters[0].Value = model.im_id;
            parameters[1].Value = model.im_mobilenumber;
            parameters[2].Value = model.im_checkcode;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_MobileCheck_MobileCheckUpdate", parameters);
        }
        #endregion

        #region 删除一条数据
        /// <summary>
        /// 删除一条数据
        /// <param name="im_id">流水号</param>
        /// </summary>
        public static int MobileCheckDel(int im_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@im_id", SqlDbType.Int,4)};
            parameters[0].Value = im_id;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_MobileCheck_MobileCheckDel", parameters);
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static DataTable GetModel(int im_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@im_id", SqlDbType.Int,4)};
            parameters[0].Value = im_id;

            return Com.ILog.Data.DataAggregate.GetDateTabel("SP_ilog_MobileCheck_GetMobileCheckInfo", parameters);
        }
        #endregion

        #region 根据用户手机号得到最近一次发送手机短信的时间

        /// <summary>
        /// 功能描述：根据用户手机号得到最近一次发送手机短信的时间
        /// 创建标识：ljd 20120527
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="mobile">发送手机号</param>
        /// <param name="urlstate">是否报错</param>
        /// <returns></returns>
        public static ILog.Model.ILogMobileCheck GetLastestMobileSendTime(long userid, string mobile, ref int urlstate)
        {
            ILog.Model.ILogMobileCheck ooMobileCheck = new ILog.Model.ILogMobileCheck();

            SqlParameter[] Parm = new SqlParameter[2];
            Parm[0] = new SqlParameter("@userid", SqlDbType.BigInt);
            Parm[1] = new SqlParameter("@mobile", SqlDbType.NVarChar, 20);

            Parm[0].Value = userid;
            Parm[1].Value = mobile;

            SqlConnection con = Com.ILog.Data.DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "sp_Ilog_MobileCheckGetLastestEntity", Parm);

            try
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        ooMobileCheck.intime = Convert.ToDateTime(Utils.ChangeType(reader["intime"], typeof(DateTime)));
                        ooMobileCheck.im_checkcode = Utils.ChangeType(reader["im_checkcode"], typeof(string)).ToString();
                    }
                }
                else
                {
                    ooMobileCheck = null;
                }
                urlstate = 1;
            }
            catch
            {
                urlstate = 0;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return ooMobileCheck;

        }

        #endregion

    }
}
