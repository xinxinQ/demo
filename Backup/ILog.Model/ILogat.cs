using System;
namespace ILog.Model
{
	/// <summary>
    ///  ʵ���ࣺ@��Ϣ��
	/// </summary>
	public class ILogat
	{

		#region Model
		private long _ia_id;
		private long _userid;
		private long _ia_atuserid;
		private string _ia_content;
		private DateTime _intime;
		private int _ia_type;
        private int _is_id;
        private long _iso_id;
        private long _ia_logid;


		/// <summary>
        /// ������ʶ 1
		/// </summary>
        public long ia_id
		{
			set{ _ia_id=value;}
			get{return _ia_id;}
		}
		/// <summary>
		/// Vip���е�userid@�û�
		/// </summary>
        public long userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
        /// ����@�û�
		/// </summary>
        public long ia_atuserid
		{
			set{ _ia_atuserid=value;}
			get{return _ia_atuserid;}
		}
		/// <summary>
        /// ����
		/// </summary>
		public string ia_content
		{
			set{ _ia_content=value;}
			get{return _ia_content;}
		}
		/// <summary>
        ///д��ʱ��  Ĭ����getdate()
		/// </summary>
		public DateTime intime
		{
			set{ _intime=value;}
			get{return _intime;}
		}
		/// <summary>
        /// 0΢����1���ۣ�2ת��
		/// </summary>
		public int ia_type
		{
			set{ _ia_type=value;}
			get{return _ia_type;}
		}
		/// <summary>
        /// ��Դid
		/// </summary>
		public int is_id
		{
			set{ _is_id=value;}
			get{return _is_id;}
		}
        /// <summary>
        /// ԭ��id
        /// </summary>
        public long iso_id
        {
            set { _iso_id = value; }
            get { return _iso_id; }
        }

        /// <summary>
        /// ����id
        /// </summary>
        public long ia_logid
        {
            set { _ia_logid = value; }
            get { return _ia_logid; }
        }
		#endregion Model

	}
}

