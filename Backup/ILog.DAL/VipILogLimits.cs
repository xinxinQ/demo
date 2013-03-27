using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ILog.DAL
{
    public class VipILogLimits
    {
        #region 查看范围是否存在
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool LimitsExists(int userid, int vil_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
					new SqlParameter("@vil_id", SqlDbType.BigInt)};
            parameters[0].Value = userid;
            parameters[1].Value = vil_id;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("SP_vip_ilog_limits_LimitsExists", "isEXISTS", parameters) == "1" ? true : false;
        }
        #endregion

        #region 增加一条范围数据
        /// <summary>
        /// 增加一条范围数据
        /// <param name="model">范围实体</param>
        /// </summary>
        public static int LimitsAdd(ILog.Model.VipILogLimits model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt,8),
					new SqlParameter("@vil_systemconcernnum", SqlDbType.Int,4),
					new SqlParameter("@vil_systemfannum", SqlDbType.Int,4),
					new SqlParameter("@intime", SqlDbType.DateTime)};
            parameters[0].Value = model.userid;
            parameters[1].Value = model.vil_systemconcernnum;
            parameters[2].Value = model.vil_systemfannum;
            parameters[3].Value = model.intime;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_vip_ilog_limits_LimitsAdd", parameters);
        }
        #endregion

        #region 更新一条数据
        /// <summary>
        /// 更新一条数据
        /// <param name="model">范围实体</param>
        /// </summary>
        public static int LimitsUpdate(ILog.Model.VipILogLimits model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@vil_id", SqlDbType.BigInt,8),
					new SqlParameter("@userid", SqlDbType.BigInt,8),
					new SqlParameter("@vil_systemconcernnum", SqlDbType.Int,4),
					new SqlParameter("@vil_systemfannum", SqlDbType.Int,4),
					new SqlParameter("@intime", SqlDbType.DateTime)};
            parameters[0].Value = model.vil_id;
            parameters[1].Value = model.userid;
            parameters[2].Value = model.vil_systemconcernnum;
            parameters[3].Value = model.vil_systemfannum;
            parameters[4].Value = model.intime;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_vip_ilog_limits_LimitsUpdate", parameters);
        }
        #endregion

        #region 删除一条数据
        /// <summary>
        /// 删除一条数据
        /// <param name="userid">用户id</param>
        /// <param name="vil_id">流水号</param>
        /// </summary>
        public static int LimitsDel(int userid, int vil_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
					new SqlParameter("@vil_id", SqlDbType.BigInt)};
            parameters[0].Value = userid;
            parameters[1].Value = vil_id;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_vip_ilog_limits_LimitsDel", parameters);
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="userid">用户id</param>
        /// <param name="vil_id">流水号</param>
        /// </summary>
        public static DataTable GetModel(int userid, int vil_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
					new SqlParameter("@vil_id", SqlDbType.BigInt)};
            parameters[0].Value = userid;
            parameters[1].Value = vil_id;

            return Com.ILog.Data.DataAggregate.GetDateTabel("SP_vip_ilog_limits_GetLimitsInfo", parameters);
        }
        #endregion
    }
}
