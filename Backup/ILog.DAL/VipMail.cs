using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ILog.DAL
{
	/// <summary>
	/// 数据访问类vipmail。
	/// </summary>
    public class VipMail
    {
        #region  增加一条站短（返回受影响行数）
        /// <summary>
		/// 增加一条站短（返回受影响行数）
        /// <param name="model">站短实体</param>
		/// </summary>
        public static int VipMailAdd(ILog.Model.VipMail model)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@fromwho", SqlDbType.VarChar,20),
					new SqlParameter("@towho", SqlDbType.VarChar,20),
					new SqlParameter("@tid", SqlDbType.BigInt,8),
					new SqlParameter("@subject", SqlDbType.NVarChar,50),
					new SqlParameter("@content", SqlDbType.NVarChar,200),
					new SqlParameter("@ip", SqlDbType.VarChar,20),
					new SqlParameter("@hasread", SqlDbType.Bit,1)};
			parameters[0].Value = model.fromwho;
			parameters[1].Value = model.towho;
			parameters[2].Value = model.tid;
			parameters[3].Value = model.subject;
			parameters[4].Value = model.content;
			parameters[5].Value = model.ip;
			parameters[6].Value = model.hasread;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_vipmail_mailAdd", parameters);
		}
        #endregion

        #region 更新一条站短（返回受影响行数）
        /// <summary>
        /// 更新一条站短（返回受影响行数）
        /// <param name="model">站短实体</param>
		/// </summary>
        public static int VipMailUpdate(ILog.Model.VipMail model)
		{
            SqlParameter[] parameters = {
					new SqlParameter("@fromwho", SqlDbType.VarChar,20),
					new SqlParameter("@towho", SqlDbType.VarChar,20),
					new SqlParameter("@tid", SqlDbType.BigInt,8),
					new SqlParameter("@subject", SqlDbType.NVarChar,50),
					new SqlParameter("@content", SqlDbType.NVarChar,200),
					new SqlParameter("@ip", SqlDbType.VarChar,20),
					new SqlParameter("@hasread", SqlDbType.Bit,1),
                    new SqlParameter("@id", SqlDbType.BigInt)};
            parameters[0].Value = model.fromwho;
            parameters[1].Value = model.towho;
            parameters[2].Value = model.tid;
            parameters[3].Value = model.subject;
            parameters[4].Value = model.content;
            parameters[5].Value = model.ip;
            parameters[6].Value = model.hasread;
            parameters[7].Value = model.id;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_vipmail_MailUpdate", parameters);
		}
        #endregion

        #region 删除一条站短（返回受影响行数）
        /// <summary>
        /// 删除一条站短（返回受影响行数）
        /// <param name="id">流水号</param>
		/// </summary>
        public static int VipMailDel(int id)
		{
            SqlParameter[] parameters = {
            new SqlParameter("@id", SqlDbType.BigInt)};

            parameters[0].Value = id;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_vipmail_MailDel", parameters);
		}
        #endregion

        #region 得到一个对象实体
        /// <summary>
		/// 得到一个对象实体
        /// <param name="id">流水号</param>
		/// </summary>
		public static DataTable GetModel(int id)
		{
            SqlParameter[] Param = new SqlParameter[1];

            Param[0] = new SqlParameter("@id", SqlDbType.BigInt);
            Param[0].Value = id;

            return Com.ILog.Data.DataAggregate.GetDateTabel("SP_vipmail_GetMailInfo", Param);
		}
        #endregion

        #region 根据发信id读取回复的私信
        /// <summary>
        /// 根据发信id读取回复的私信
        /// </summary>
        /// <param name="id">发件id</param>
        /// <returns></returns>
        public static List<ILog.Model.VipMail> GerReplyByMailid(long id) 
        {
            List<ILog.Model.VipMail> VipMailList = new List<ILog.Model.VipMail>();

            ILog.Model.VipMail MailList_Model;

            SqlParameter[] Param = new SqlParameter[1];

            Param[0] = new SqlParameter("@id", SqlDbType.BigInt);
            Param[0].Value = id;

            SqlConnection con = Com.ILog.Data.DbHelperSQL.GetConnection();

            SqlDataReader reader = Com.ILog.Data.DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_vipmail_GerReplyByMailid",Param);

            try
            {
                while (reader.Read())
                {
                    MailList_Model = new ILog.Model.VipMail();

                    MailList_Model.id = Convert.ToInt64(reader["id"]);
                    MailList_Model.fromwho = reader["fromwho"].ToString();
                    MailList_Model.towho = reader["towho"].ToString();
                    MailList_Model.tid = Convert.ToInt64(reader["tid"]);
                    MailList_Model.subject = reader["subject"].ToString();
                    MailList_Model.content = reader["content"].ToString();
                    MailList_Model.intime = Convert.ToDateTime(reader["intime"]);
                    MailList_Model.face = reader["face"].ToString();

                    VipMailList.Add(MailList_Model);
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

            return VipMailList;
        }

        #endregion

        #region 根据id回去收件人【0】名称【1】私信数
        /// <summary>
        /// 根据id回去收件人昵称
        /// </summary>
        /// <param name="id">流水号</param>
        /// <param name="userid">当前用户id</param>
        /// <returns></returns>
        public static string[] GetTowhoById(long id,long userid)
        {
            string[] arrTowho = new string[4];

            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.BigInt),
                    new SqlParameter("@userid",SqlDbType.BigInt) };
            parameters[0].Value = id;
            parameters[1].Value = userid;

            SqlConnection con = Com.ILog.Data.DbHelperSQL.GetConnection();
            SqlDataReader reader = Com.ILog.Data.DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_vipmaillist_GetTowhoById", parameters);

            try
            {
                while (reader.Read())
                {
                    arrTowho[0] = reader["Towho"].ToString();
                    arrTowho[1] = reader["m_number"].ToString();
                    arrTowho[2] = reader["face"].ToString();
                    arrTowho[3] = reader["towho_"].ToString();
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

            return arrTowho;
        }
        #endregion

        #region 删除私信
        /// <summary>
        /// 删除私信
        /// </summary>
        /// <param name="id">私信id</param>
        /// <param name="username">当前用户名</param>
        /// <returns></returns>
        public static int VipMailDel(long id, string usernameid) 
        {
            SqlParameter[] parameters = {
            new SqlParameter("@id", SqlDbType.BigInt),
            new SqlParameter("@usernameid", SqlDbType.VarChar,50)};

            parameters[0].Value = id;
            parameters[1].Value = usernameid;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_vipmaildel", parameters);
        }
        #endregion

        #region 发送私信
        /// <summary>
        /// 发送私信
        /// </summary>
        /// <param name="fromwho">发信人</param>
        /// <param name="towho">收信人</param>
        /// <param name="content">最后一条内容</param>
        /// <returns></returns>
        public static int SendMail(ILog.Model.VipMail vipmailmodel) 
        {
            SqlParameter[] parameters = {
            new SqlParameter("@fromwho", SqlDbType.VarChar,20),
            new SqlParameter("@towho", SqlDbType.VarChar,20),
            new SqlParameter("@content", SqlDbType.VarChar,1000),
            new SqlParameter("@subject", SqlDbType.VarChar,50),
            new SqlParameter("@ip", SqlDbType.VarChar,20)};

            parameters[0].Value = vipmailmodel.fromwho;
            parameters[1].Value = vipmailmodel.towho;
            parameters[2].Value = vipmailmodel.content;
            parameters[3].Value = vipmailmodel.subject;
            parameters[4].Value = vipmailmodel.ip;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_SendMail", parameters);
        }
        #endregion

        #region 回复私信
        /// <summary>
        /// 回复私信
        /// </summary>
        /// <param name="fromwho">发信人</param>
        /// <param name="towho">收信人</param>
        /// <param name="content">最后一条内容</param>
        /// <returns></returns>
        public static int ReplyMail(ILog.Model.VipMail vipmailmodel)
        {
            SqlParameter[] parameters = {
            new SqlParameter("@fromwho", SqlDbType.VarChar,20),
            new SqlParameter("@towho", SqlDbType.VarChar,20),
            new SqlParameter("@content", SqlDbType.VarChar,200),
            new SqlParameter("@subject", SqlDbType.VarChar,50),
            new SqlParameter("@ip", SqlDbType.VarChar,20)};

            parameters[0].Value = vipmailmodel.fromwho;
            parameters[1].Value = vipmailmodel.towho;
            parameters[2].Value = vipmailmodel.content;
            parameters[3].Value = vipmailmodel.subject;
            parameters[4].Value = vipmailmodel.ip;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ReplyMail", parameters);
        }
        #endregion

        #region 获取收件人id
        /// <summary>
        /// 获取收件人id
        /// </summary>
        /// <param name="towho">收信人</param>
        /// <returns></returns>
        public static long GetTowhoIbByTowho(string towho)
        {
            long TowhoId = 0;

            SqlParameter[] parameters = {
					new SqlParameter("@towho", SqlDbType.VarChar,20)};
            parameters[0].Value = towho;

            SqlConnection con = Com.ILog.Data.DbHelperSQL.GetConnection();
            SqlDataReader reader = Com.ILog.Data.DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_GetTowhoIbByTowho", parameters);

            try
            {
                while (reader.Read())
                {
                    TowhoId = Convert.ToInt64(reader["userid"]);
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

            return TowhoId;
        }
        #endregion

        #region 更新发件人于收件人之间的站短数量，如果站短全部被删除就解除当前用户与收件人的关系
        /// <summary>
        /// 更新发件人于收件人之间的站短数量，如果站短全部被删除就解除当前用户与收件人的关系
        /// </summary>
        /// <param name="delcount">删除站短数</param>
        /// <param name="fromwhoid">发信人id</param>
        /// <param name="towhoid">收信人id</param>
        /// <param name="userid">当前用户</param>
        /// <returns></returns>
        public static void UpdateVipMailCount(string fromwhoid, string towhoid, int delcount, string userid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@fromwho", SqlDbType.VarChar,20),
                    new SqlParameter("@towho", SqlDbType.VarChar,20),
                    new SqlParameter("@delCount", SqlDbType.Int),
                    new SqlParameter("@userid",SqlDbType.VarChar,20)};
            parameters[0].Value = fromwhoid;
            parameters[1].Value = towhoid;
            parameters[2].Value = delcount;
            parameters[3].Value = userid;

            Com.ILog.Data.DataAggregate.EXECprocedure("SP_Vip_ILogUpdateVipMailCount", parameters);
        }
        #endregion

        #region 取出站短数
        /// <summary>
        /// 取出站短数
        /// </summary>
        /// <param name="delcount">删除站短数</param>
        /// <param name="fromwhoid">发信人id</param>
        /// <param name="towhoid">收信人id</param>
        /// <returns></returns>
        public static string Getm_number(string fromwhoid, string towhoid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@fromwho", SqlDbType.VarChar,20),
                    new SqlParameter("@towho", SqlDbType.VarChar,20)};
            parameters[0].Value = fromwhoid;
            parameters[1].Value = towhoid;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("SP_ilog_Getm_number", "m_number", parameters);
        }
        #endregion

        #region 发送站短智能提示收信人
        /// <summary>
        /// 发送站短智能提示收信人
        /// </summary>
        /// <param name="nickname">用户昵称</param>
        /// <returns></returns>
        public static List<ILog.Model.VipMail> GetNickName_SendMail(string nickname)
        {
            List<ILog.Model.VipMail> GetNickNameList = new List<ILog.Model.VipMail>();

            ILog.Model.VipMail VipMailList;

            SqlParameter[] parameters = {
					new SqlParameter("@nickname", SqlDbType.NVarChar)};
            parameters[0].Value = nickname;

            SqlConnection con = Com.ILog.Data.DbHelperSQL.GetConnection();
            SqlDataReader reader = Com.ILog.Data.DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_vip_ilog_GetNickName", parameters);

            try
            {
                while (reader.Read())
                {
                    VipMailList = new ILog.Model.VipMail();

                    VipMailList.towho = reader["nickname"].ToString();

                    GetNickNameList.Add(VipMailList);
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

            return GetNickNameList;
        }
        #endregion


        #region 发送站短智能提示收信人（页面搜索）
        /// <summary>
        /// 发送站短智能提示收信人（页面搜索）
        /// </summary>
        /// <param name="nickname">用户昵称</param>
        /// <returns></returns>
        public static List<ILog.Model.VipMail> GetNickNameByUserID_MailList(string nickname)
        {
            List<ILog.Model.VipMail> GetNickNameList = new List<ILog.Model.VipMail>();

            ILog.Model.VipMail VipMailList;

            SqlParameter[] parameters = {
					new SqlParameter("@nickname", SqlDbType.NVarChar)};
            parameters[0].Value = nickname;

            SqlConnection con = Com.ILog.Data.DbHelperSQL.GetConnection();
            SqlDataReader reader = Com.ILog.Data.DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_vipmaillist_GetNickNameByUserID", parameters);

            try
            {
                while (reader.Read())
                {
                    VipMailList = new ILog.Model.VipMail();

                    VipMailList.towho = reader["nickname"].ToString();

                    GetNickNameList.Add(VipMailList);
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

            return GetNickNameList;
        }
        #endregion

        


        #region 获取站短内容时间
        /// <summary>
        /// 获取站短内容时间
        /// </summary>
        /// <param name="towho">发信人id</param>
        /// <param name="content">站短</param>
        /// <returns></returns>
        public static string GetMailContentTime(string towhoid, string content)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@towho", SqlDbType.VarChar,20),
                    new SqlParameter("@content", SqlDbType.VarChar,1000)};
            parameters[0].Value = towhoid;
            parameters[1].Value = content;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("SP_vipmail_GetMailContentTime", "intime", parameters);
        }
        #endregion


        #region 提醒清零
        /// <summary>
        /// 提醒清零
        /// </summary>
        /// <param name="userid">当前用户id</param>
        /// <returns></returns>
        public static void UserMessagesCleared(long userid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt)};
            parameters[0].Value = userid;

            Com.ILog.Data.DataAggregate.EXECprocedure("SP_vip_ilog_count_UserMessagesCleared", parameters);
        }
        #endregion

        #region 评论提醒清零
        /// <summary>
        /// 评论提醒清零
        /// </summary>
        /// <param name="userid">当前用户id</param>
        /// <returns></returns>
        public static void UserCommentCleared(long userid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt)};
            parameters[0].Value = userid;

            Com.ILog.Data.DataAggregate.EXECprocedure("SP_vip_ilog_count_UserCommentCleared", parameters);
        }
        #endregion
    }
}

