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
    public class ILogInvitecode
    {
        #region 添加邀请码
        /// <summary>
        /// 功能描述：添加邀请码
        /// 创建标识：ljd 20120911
        /// </summary>
        /// <param name="ooInviteCode"></param>
        /// <returns></returns>
        public static int InviteCodeAdd(Model.ILogInvitecode ooInviteCode)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@vi_code", SqlDbType.VarChar,40),
					new SqlParameter("@vi_senduserid", SqlDbType.BigInt),
					new SqlParameter("@intime", SqlDbType.DateTime)};
            parameters[0].Value = ooInviteCode.Vi_code;
            parameters[1].Value = ooInviteCode.Vi_senduserid;
            parameters[2].Value = ooInviteCode.Intime;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_invitecodeAdd", parameters);

        }
        #endregion

        #region 根据验证码更新被邀请的用户id
        /// <summary>
        /// 功能描述：根据验证码更新被邀请的用户id
        /// 创建标识：ljd 20120911
        /// </summary>
        /// <param name="ooInviteCode"></param>
        /// <returns></returns>
        public static int InviteCodeUpdate(Model.ILogInvitecode ooInviteCode)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@vi_id", SqlDbType.BigInt),
					new SqlParameter("@userid", SqlDbType.BigInt)};
            parameters[0].Value = ooInviteCode.Vi_id;
            parameters[1].Value = ooInviteCode.Userid;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_invitecodeUpdate", parameters);

        }
        #endregion

        #region 得到邀请内容
        /// <summary>
        /// 功能描述：得到邀请内容
        /// 创建标识：ljd 20120912
        /// </summary>
        /// <param name="code">邀请码</param>
        /// <returns></returns>
        public static Model.ILogInvitecode GetInviteEntity(string code)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@vi_code", SqlDbType.VarChar,40)};

            parameters[0].Value = code;

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_invitecodeGetEntityByCode",parameters);

            Model.ILogInvitecode ooCode = new ILog.Model.ILogInvitecode();

            try
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        ooCode.Vi_id = Convert.ToInt64(Utils.ChangeType(reader["vi_id"], typeof(long)));
                        ooCode.Userid = Convert.ToInt64(Utils.ChangeType(reader["userid"], typeof(long)));
                    }
                }
                else
                {
                    ooCode = null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
                reader.Close();
                reader.Dispose();
            }
            return ooCode;

        }
        #endregion

        #region 得到用户未使用过的邀请码列表
        /// <summary>
        /// 功能描述：得到用户未使用过的邀请码列表
        /// 创建标识：ljd 20120912
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static List<Model.ILogInvitecode> GetInviteListByUserID(long userid)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@userid", SqlDbType.BigInt)};

            parameters[0].Value = userid;

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_invitecodeGetListByUserid",parameters);

            List<Model.ILogInvitecode> inviteList = new List<ILog.Model.ILogInvitecode>();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model.ILogInvitecode ooInvite = new ILog.Model.ILogInvitecode();

                        ooInvite.Vi_id = Convert.ToInt64(Utils.ChangeType(reader["vi_id"], typeof(long)));
                        ooInvite.Vi_code = Utils.ChangeType(reader["vi_code"], typeof(string)).ToString();

                        inviteList.Add(ooInvite);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
                reader.Close();
                reader.Dispose();
            }
            return inviteList;

        }
        #endregion

        #region 判断用户id得到是否已被激活

        /// <summary>
        /// 功能描述：判断用户id得到是否已被激活
        /// 创建标识：ljd 20120913
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns>0不是</returns>
        public static long GetActiveID(long userid)
        {
            //激活码id
            long vi_id = 0;

            //存储过程
            string sql = "select vi_id from ilog_invitecode where userid = @userid";

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlParameter[] Parm = { new SqlParameter("@userid", SqlDbType.BigInt) };

            Parm[0].Value = userid;

            try
            {
                vi_id = Convert.ToInt32(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.Text, sql, Parm), typeof(int)));
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

            return vi_id;

        }

        #endregion

    }
}
