using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using Com.ILog.Data;
using Com.ILog.Utils;

namespace ILog.DAL
{
	/// <summary>
	/// 数据访问类ilog_at。
	/// </summary>
    public class ILogat
    {
        #region 是否存在该记录
        /// <summary>
		/// 是否存在该记录
        /// <param name="ia_id">流水号</param>
		/// </summary>
        public static bool AtInfoExists(int ia_id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ia_id", SqlDbType.BigInt)};
			parameters[0].Value = ia_id;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("SP_ilog_at_atInfoExists","isEXISTS" ,parameters) == "1" ? true : false;
		}
        #endregion

        #region 增加一条数据
        /// <summary>
		/// 增加一条数据
        /// <param name="model">at表实体</param>
		/// </summary>
		public static int AtInfoAdd(ILog.Model.ILogat model)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt,8),
					new SqlParameter("@ia_atuserid", SqlDbType.BigInt,8),
					new SqlParameter("@ia_content", SqlDbType.NVarChar,3000),
					new SqlParameter("@intime", SqlDbType.DateTime),
					new SqlParameter("@ia_type", SqlDbType.Int,4),
					new SqlParameter("@is_id", SqlDbType.BigInt,8),
                    new SqlParameter("@iso_id", SqlDbType.BigInt,8),
                    new SqlParameter("@ia_logid", SqlDbType.BigInt,8)};
			parameters[0].Value = model.userid;
			parameters[1].Value = model.ia_atuserid;
			parameters[2].Value = model.ia_content;
			parameters[3].Value = model.intime;
			parameters[4].Value = model.ia_type;
			parameters[5].Value = model.is_id;
            parameters[6].Value = model.iso_id;
            parameters[7].Value = model.ia_logid;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_at_atInfoAdd", parameters);
		}
        #endregion

        #region 更新一条数据
		/// <summary>
		/// 更新一条数据
        /// <param name="model">at表实体</param>
		/// </summary>
        public static int AtInfoUpdate(ILog.Model.ILogat model)
		{
			SqlParameter[] parameters = {

					new SqlParameter("@ia_id", SqlDbType.BigInt,8),
					new SqlParameter("@userid", SqlDbType.BigInt,8),
					new SqlParameter("@ia_atuserid", SqlDbType.BigInt,8),
					new SqlParameter("@ia_content", SqlDbType.NVarChar,300),
					new SqlParameter("@ia_type", SqlDbType.Int,4),
					new SqlParameter("@is_id", SqlDbType.BigInt,8)};
			parameters[0].Value = model.ia_id;
			parameters[1].Value = model.userid;
			parameters[2].Value = model.ia_atuserid;
			parameters[3].Value = model.ia_content;
			parameters[4].Value = model.ia_type;
			parameters[5].Value = model.is_id;

			return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_at_atInfoUPDATE", parameters);
		}
        #endregion

        #region 删除一条数据
		/// <summary>
		/// 删除一条数据
        /// <param name="ia_id">流水号</param>
		/// </summary>
		public static int AtInfoDel(int ia_id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ia_id", SqlDbType.BigInt)};
			parameters[0].Value = ia_id;

			return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_at_atInfoDel", parameters);
		}
        #endregion

        #region  得到一个对象实体
		/// <summary>
		/// 得到一个对象实体
        /// <param name="ia_id">流水号</param>
		/// </summary>
		public static DataTable GetModel(int ia_id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@ia_id", SqlDbType.BigInt)};
			parameters[0].Value = ia_id;

            return Com.ILog.Data.DataAggregate.GetDateTabel("SP_ilog_at_GetatInfoInfo",parameters);
		}
        #endregion

        #region 根据搜索关键字查询用户列表
        /// <summary>
        /// 功能描述：根据搜索关键字查询用户列表
        /// 创建标识：ljd 20120718
        /// </summary>
        /// <param name="keyword">搜索关键字</param>
        /// <param name="userid">当前用户id</param>
        /// <param name="ation">0 博文 1评论</param>
        /// <returns></returns>
        public static List<Model.VipILog> GetAtUserList(string keyword, long userid, int ation)
        {

            SqlParameter[] Parm = new SqlParameter[3];
            Parm[0] = new SqlParameter("@keyword", SqlDbType.NVarChar,50);
            Parm[1] = new SqlParameter("@userid", SqlDbType.BigInt);
            Parm[2] = new SqlParameter("@ation", SqlDbType.Int);

            Parm[0].Value = keyword;
            Parm[1].Value = userid;
            Parm[2].Value = ation;

            SqlConnection con = Com.ILog.Data.DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_vip_ilog_GetListForAt", Parm);

            List<Model.VipILog> vipList = new List<ILog.Model.VipILog>();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model.VipILog ooVip = new ILog.Model.VipILog();

                        ooVip.userid = Convert.ToInt64(Utils.ChangeType(reader["userid"], typeof(long)));
                        ooVip.nickname = Utils.ChangeType(reader["nickname"], typeof(string)).ToString();

                        vipList.Add(ooVip);
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
            return vipList;

        }
        #endregion

	}
}

