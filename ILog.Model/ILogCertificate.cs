using System;
namespace ILog.Model
{
	/// <summary>
    /// ʵ���ࣺ��֤֤����
	/// </summary>
	public class ILogCertificate
	{

		#region Model
        private long _ic_id;
        private long _userid;
		private int _ic_type;
		private string _ic_name;
		private string _ic_pic;
		private DateTime _intime;
		/// <summary>
        /// ������ʶ 1
		/// </summary>
        public long ic_id
		{
			set{ _ic_id=value;}
			get{return _ic_id;}
		}
		/// <summary>
        /// Vip���еĵ�userid�ֶ�
		/// </summary>
		public long userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
        /// ֤������
		/// </summary>
		public int ic_type
		{
			set{ _ic_type=value;}
			get{return _ic_type;}
		}
		/// <summary>
        /// ֤��������
		/// </summary>
		public string ic_name
		{
			set{ _ic_name=value;}
			get{return _ic_name;}
		}
		/// <summary>
        /// ֤������ͼ����
		/// </summary>
		public string ic_pic
		{
			set{ _ic_pic=value;}
			get{return _ic_pic;}
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

