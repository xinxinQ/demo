using System;
namespace ILog.Model
{
	/// <summary>
	/// ʵ���ࣺ���ͱ�
	/// </summary>
	public class ILogSpread
	{
		#region Model
        private long _is_id;
        private long _iso_id;
		private int _is_type;
		private string _is_content;
        private long _is_fanuserid;
        private long _userid;
		private DateTime _intime;
		private int _iss_id;
        private int _is_spreadnum;
        private int _is_commentnum;
        private long _io_id;
        private int _is_spreadtype;
        private int _is_isoriginal;

		/// <summary>
        /// ������ʶ 1
		/// </summary>
        public long is_id
		{
			set{ _is_id=value;}
			get{return _is_id;}
		}
		/// <summary>
        /// ����id
		/// </summary>
		public long iso_id
		{
			set{ _iso_id=value;}
			get{return _iso_id;}
		}

        /// <summary>
        /// ԭ��id
        /// </summary>
        public long io_id
        {
            set { _io_id = value; }
            get { return _io_id; }
        }

		/// <summary>
        /// �������ͣ�0��ilogԭ����1��ת��
		/// </summary>
		public int is_type
		{
			set{ _is_type=value;}
			get{return _is_type;}
		}
		/// <summary>
        /// ��������
		/// </summary>
		public string is_content
		{
			set{ _is_content=value;}
			get{return _is_content;}
		}
		/// <summary>
        /// ��˿id
		/// </summary>
        public long is_fanuserid
		{
			set{ _is_fanuserid=value;}
			get{return _is_fanuserid;}
		}
		/// <summary>
        /// Vip���е�userid�ֶ�
		/// </summary>
        public long userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// д��ʱ��
		/// </summary>
		public DateTime intime
		{
			set{ _intime=value;}
			get{return _intime;}
		}
		/// <summary>
        /// ��Դid
		/// </summary>
		public int iss_id
		{
			set{ _iss_id=value;}
			get{return _iss_id;}
		}
        /// <summary>
        /// ����ת������
        /// </summary>
        public int is_spreadnum
        {
            set { _is_spreadnum = value; }
            get { return _is_spreadnum; }
        }

        /// <summary>
        /// ���۴��� Ĭ����0
        /// </summary>
        public int is_commentnum
        {
            set { _is_commentnum = value; }
            get { return _is_commentnum; }
        }


        /// <summary>
        /// ������� 0���� 1ת��
        /// </summary>
        public int is_spreadtype
        {
            set { _is_spreadtype = value; }
            get { return _is_spreadtype; }
        }

        /// <summary>
        /// �Ƿ�ԭ�� 0 �� 1��
        /// </summary>
        public int is_isoriginal
        {
            set { _is_isoriginal = value; }
            get { return _is_isoriginal; }
        }

		#endregion Model

	}
}

