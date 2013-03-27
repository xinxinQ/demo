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
	/// ���ݷ�����ilog_certificate��
	/// </summary>
    public class ILogCertificate
	{
		#region  �Ƿ���ڸ�ĳ����֤��¼��¼
		/// <summary>
		/// �Ƿ���ڸ�ĳ����֤��¼��¼
		/// </summary>
		public static bool CertificateInfoEXISTS(int userid,int ic_id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
					new SqlParameter("@ic_id", SqlDbType.BigInt)};
			parameters[0].Value = userid;
			parameters[1].Value = ic_id;

			return Com.ILog.Data.DataAggregate.GetColumnInfo("SP_ilog_certificate_certificateInfoEXISTS","isEXISTS" ,parameters) == "1" ? true : false;
        }
        #endregion

        #region ����һ����֤����
        /// <summary>
		/// ����һ����֤����
        /// <param name="model">֤�˱�ʵ��</param>
		/// </summary>
        public static int CertificateInfoAdd(ILog.Model.ILogCertificate model)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt,8),
					new SqlParameter("@ic_type", SqlDbType.Int,4),
					new SqlParameter("@ic_pic", SqlDbType.VarChar,100),
                    new SqlParameter("@intime", SqlDbType.DateTime)};
			parameters[0].Value = model.userid;
			parameters[1].Value = model.ic_type;
			parameters[2].Value = model.ic_pic;
            parameters[3].Value = model.intime;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_certificate_certificateAdd", parameters);
		}
        #endregion

        #region ����һ����֤����
		/// <summary>
		/// ����һ����֤����
        /// <param name="model">��֤��ʵ��</param>
		/// </summary>
		public static int CertificateUpdate(ILog.Model.ILogCertificate model)
		{
            SqlParameter[] parameters = {
					new SqlParameter("@ic_id", SqlDbType.BigInt,8),
					new SqlParameter("@ic_pic", SqlDbType.VarChar,100)};
			parameters[0].Value = model.ic_id;
			parameters[1].Value = model.ic_pic;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_certificate_certificateUpdate", parameters);
		}
        #endregion

        #region ɾ��һ����֤����
		/// <summary>
		/// ɾ��һ����֤����
        /// <param name="ic_id">��ˮ��</param>
        /// <param name="userid">�û�id</param>
		/// </summary>
		public static int CertificateDel(int userid,int ic_id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
					new SqlParameter("@ic_id", SqlDbType.BigInt)};
			parameters[0].Value = userid;
			parameters[1].Value = ic_id;

			return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_certificate_certificateDel", parameters);
		}
        #endregion

        #region �õ�һ��������֤ʵ��
		/// <summary>
		/// �õ�һ��������֤ʵ��
		/// </summary>
		public static DataTable GetModel(int userid,int ic_id)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
					new SqlParameter("@ic_id", SqlDbType.BigInt)};
			parameters[0].Value = userid;
			parameters[1].Value = ic_id;

            return Com.ILog.Data.DataAggregate.GetDateTabel("SP_ilog_certificate_GetcertificateInfo", parameters);
		}
        #endregion

        #region �����û���Ż�ȡ��֤��Ϣ

        /// <summary>
        /// �����û���Ż�ȡ��֤��Ϣ
        /// </summary>
        public static List<ILog.Model.ILogCertificate> GetCertificateInfoByUserId(long userid)
        {

            List<ILog.Model.ILogCertificate> certificateList = new List<ILog.Model.ILogCertificate>();
            ILog.Model.ILogCertificate certificate = null;

            try
            {

                SqlParameter[] parameters =
                {

			        new SqlParameter("@userid", SqlDbType.BigInt)					

                };

                parameters[0].Value = userid;

                DataTable dataTable = Com.ILog.Data.DataAggregate.GetDateTabel("SP_ilog_certificate_GetcertificateInfoByUserId", parameters);

                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {


                        //ʵ��������
                        certificate = new ILog.Model.ILogCertificate();

                        if (dataRow["ic_id"].ToString() != "")
                        {

                            certificate.ic_id = int.Parse(dataRow["ic_id"].ToString());

                        }

                        if (dataRow["userid"].ToString() != "")
                        {

                            certificate.userid = int.Parse(dataRow["userid"].ToString());

                        }

                        if (dataRow["ic_type"].ToString() != "")
                        {

                            certificate.ic_type = int.Parse(dataRow["ic_type"].ToString());

                        }

                        certificate.ic_name = dataRow["ic_name"].ToString();

                        certificate.ic_pic = dataRow["ic_pic"].ToString();

                        if (dataRow["intime"].ToString() != "")
                        {

                            certificate.intime = DateTime.Parse(dataRow["intime"].ToString());

                        }

                        certificateList.Add(certificate);

                    }

                }

                return certificateList;


            }
            catch (Exception ex)
            {

                return null;

            }

        }

        #endregion

        #region �����û�id��֤�����͵õ�֤����id
        /// <summary>
        /// ���������������û�id��֤�����͵õ�֤����id
        /// ������ʶ��ljd 20120604
        /// </summary>
        /// <param name="userid">�û�id</param>
        /// <param name="type">֤������</param>
        /// <param name="urlState">�Ƿ񱨴�</param>
        /// <returns></returns>
        public static long GetIDByUserIDAndType(long userid, int type, ref int urlState)
        {
            //֤��id
            long ia_id = 0;

            //�洢����
            string sql = "SP_ilog_certificate_GetID";

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlParameter[] Parm = { new SqlParameter("@userid", SqlDbType.BigInt), new SqlParameter("@ic_type", SqlDbType.Int) };

            Parm[0].Value = userid;
            Parm[1].Value = type;

            try
            {
                ia_id = Convert.ToInt64(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, Parm), typeof(long)));
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

            return ia_id;

        }
        #endregion



	}
}

