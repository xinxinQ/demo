using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ILog.DAL
{
	/// <summary>
	/// ���ݷ�����vipmail��
	/// </summary>
    public class VipMail
    {
        #region  ����һ��վ�̣�������Ӱ��������
        /// <summary>
		/// ����һ��վ�̣�������Ӱ��������
        /// <param name="model">վ��ʵ��</param>
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

        #region ����һ��վ�̣�������Ӱ��������
        /// <summary>
        /// ����һ��վ�̣�������Ӱ��������
        /// <param name="model">վ��ʵ��</param>
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

        #region ɾ��һ��վ�̣�������Ӱ��������
        /// <summary>
        /// ɾ��һ��վ�̣�������Ӱ��������
        /// <param name="id">��ˮ��</param>
		/// </summary>
        public static int VipMailDel(int id)
		{
            SqlParameter[] parameters = {
            new SqlParameter("@id", SqlDbType.BigInt)};

            parameters[0].Value = id;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_vipmail_MailDel", parameters);
		}
        #endregion

        #region �õ�һ������ʵ��
        /// <summary>
		/// �õ�һ������ʵ��
        /// <param name="id">��ˮ��</param>
		/// </summary>
		public static DataTable GetModel(int id)
		{
            SqlParameter[] Param = new SqlParameter[1];

            Param[0] = new SqlParameter("@id", SqlDbType.BigInt);
            Param[0].Value = id;

            return Com.ILog.Data.DataAggregate.GetDateTabel("SP_vipmail_GetMailInfo", Param);
		}
        #endregion

        #region ���ݷ���id��ȡ�ظ���˽��
        /// <summary>
        /// ���ݷ���id��ȡ�ظ���˽��
        /// </summary>
        /// <param name="id">����id</param>
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

        #region ����id��ȥ�ռ��ˡ�0�����ơ�1��˽����
        /// <summary>
        /// ����id��ȥ�ռ����ǳ�
        /// </summary>
        /// <param name="id">��ˮ��</param>
        /// <param name="userid">��ǰ�û�id</param>
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

        #region ɾ��˽��
        /// <summary>
        /// ɾ��˽��
        /// </summary>
        /// <param name="id">˽��id</param>
        /// <param name="username">��ǰ�û���</param>
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

        #region ����˽��
        /// <summary>
        /// ����˽��
        /// </summary>
        /// <param name="fromwho">������</param>
        /// <param name="towho">������</param>
        /// <param name="content">���һ������</param>
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

        #region �ظ�˽��
        /// <summary>
        /// �ظ�˽��
        /// </summary>
        /// <param name="fromwho">������</param>
        /// <param name="towho">������</param>
        /// <param name="content">���һ������</param>
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

        #region ��ȡ�ռ���id
        /// <summary>
        /// ��ȡ�ռ���id
        /// </summary>
        /// <param name="towho">������</param>
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

        #region ���·��������ռ���֮���վ�����������վ��ȫ����ɾ���ͽ����ǰ�û����ռ��˵Ĺ�ϵ
        /// <summary>
        /// ���·��������ռ���֮���վ�����������վ��ȫ����ɾ���ͽ����ǰ�û����ռ��˵Ĺ�ϵ
        /// </summary>
        /// <param name="delcount">ɾ��վ����</param>
        /// <param name="fromwhoid">������id</param>
        /// <param name="towhoid">������id</param>
        /// <param name="userid">��ǰ�û�</param>
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

        #region ȡ��վ����
        /// <summary>
        /// ȡ��վ����
        /// </summary>
        /// <param name="delcount">ɾ��վ����</param>
        /// <param name="fromwhoid">������id</param>
        /// <param name="towhoid">������id</param>
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

        #region ����վ��������ʾ������
        /// <summary>
        /// ����վ��������ʾ������
        /// </summary>
        /// <param name="nickname">�û��ǳ�</param>
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


        #region ����վ��������ʾ�����ˣ�ҳ��������
        /// <summary>
        /// ����վ��������ʾ�����ˣ�ҳ��������
        /// </summary>
        /// <param name="nickname">�û��ǳ�</param>
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

        


        #region ��ȡվ������ʱ��
        /// <summary>
        /// ��ȡվ������ʱ��
        /// </summary>
        /// <param name="towho">������id</param>
        /// <param name="content">վ��</param>
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


        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="userid">��ǰ�û�id</param>
        /// <returns></returns>
        public static void UserMessagesCleared(long userid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt)};
            parameters[0].Value = userid;

            Com.ILog.Data.DataAggregate.EXECprocedure("SP_vip_ilog_count_UserMessagesCleared", parameters);
        }
        #endregion

        #region ������������
        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="userid">��ǰ�û�id</param>
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

