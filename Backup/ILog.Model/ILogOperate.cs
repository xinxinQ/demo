using System;
namespace ILog.Model
{
	/// <summary>
	/// ʵ���ࣺ
	/// </summary>
	public class ILogOperate
                 
	{
		#region Model
		private int _io_id;
		private string _admin_name;
		private int _io_actionnumber;
		private string _io_ip;
		private int _io_oid;
		private string _io_title;
		private string _io_tablename;
		private DateTime _intime;
		/// <summary>
        /// ������ID
		/// </summary>
		public int io_id
		{
			set{ _io_id=value;}
			get{return _io_id;}
		}
		/// <summary>
        /// �����û�
		/// </summary>
		public string admin_name
		{
			set{ _admin_name=value;}
			get{return _admin_name;}
		}
		/// <summary>
        /// log_Operate_Class���е�
		/// </summary>
		public int io_actionnumber
		{
			set{ _io_actionnumber=value;}
			get{return _io_actionnumber;}
		}
		/// <summary>
        /// ��ȡ�����û���ip��ַ
		/// </summary>
		public string io_ip
		{
			set{ _io_ip=value;}
			get{return _io_ip;}
		}
		/// <summary>
        /// ִ�м�¼ID
		/// </summary>
		public int io_oid
		{
			set{ _io_oid=value;}
			get{return _io_oid;}
		}
		/// <summary>
        /// ִ�м�¼����
		/// </summary>
		public string io_title
		{
			set{ _io_title=value;}
			get{return _io_title;}
		}
		/// <summary>
        /// ִ�б�����
		/// </summary>
		public string io_tablename
		{
			set{ _io_tablename=value;}
			get{return _io_tablename;}
		}
		/// <summary>
        /// д��ʱ��
		/// </summary>
		public DateTime intime
		{
			set{ _intime=value;}
			get{return _intime;}
		}
		#endregion Model

	}
}

