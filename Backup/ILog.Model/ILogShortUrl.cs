using System;
namespace ILog.Model
{
	/// <summary>
    /// ʵ���ࣺ�̵�ַ��
	/// </summary>
	public class ILogShortUrl
	{
		#region Model
        private long _isu_id;
		private string _isu_url;
		private string _isu_shorturl;
        private long _isu_num;
		private DateTime _intime;
        private int _isu_type;

		/// <summary>
        /// ������ʶ 1
		/// </summary>
		public long isu_id
		{
			set{ _isu_id=value;}
			get{return _isu_id;}
		}
		/// <summary>
        /// Դ�ĵ�ַ����Ψһ�ģ�
		/// </summary>
		public string isu_url
		{
			set{ _isu_url=value;}
			get{return _isu_url;}
		}
		/// <summary>
        /// �̵�ַ����ӦԴ�ļ��Ķ̵�ַҲ��Ψһ��
		/// </summary>
		public string isu_shorturl
		{
			set{ _isu_shorturl=value;}
			get{return _isu_shorturl;}
		}
		/// <summary>
        /// ���ʴ�����Ĭ��0
		/// </summary>
		public long isu_num
		{
			set{ _isu_num=value;}
			get{return _isu_num;}
		}
		/// <summary>
        /// Ĭ����getdate()
		/// </summary>
		public DateTime intime
		{
			set{ _intime=value;}
			get{return _intime;}
		}

        /// <summary>
        /// ��Ƶ��� 0��ͨ��ַ 1 ��Ƶ��ַ
        /// </summary>
        public int isu_type
        {
            set { _isu_type = value; }
            get { return _isu_type; }
        }
		#endregion Model

	}
}

