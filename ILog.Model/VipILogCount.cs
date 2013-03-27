using System;
namespace ILog.Model
{
	/// <summary>
	/// ʵ���ࣺ
	/// </summary>
	public class VipILogCount
	{

		#region Model
        private long _vic_id;
		private long _userid;
		private int _vic_concernnum;
		private int _vic_onewayconcernnum;
		private int _vic_doubleconcernnum;
		private int _vic_fannum;
		private int _vic_ilognum;
        private int _vic_commentcountnum;
		private int _vic_fanoutnum;
		private int _vic_messageoutnum;
		private int _vic_messagenum;
		private int _vic_atnum;
		private int _vic_commentnum;
		private DateTime _intime;
		/// <summary>
        /// vic_id����ˮ��
		/// </summary>
        public long vic_id
		{
			set{ _vic_id=value;}
			get{return _vic_id;}
		}
		/// <summary>
		/// �û�id
		/// </summary>
        public long userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
        /// ��ע����
		/// </summary>
		public int vic_concernnum
		{
			set{ _vic_concernnum=value;}
			get{return _vic_concernnum;}
		}
		/// <summary>
		/// �����ע����
		/// </summary>
		public int vic_onewayconcernnum
		{
			set{ _vic_onewayconcernnum=value;}
			get{return _vic_onewayconcernnum;}
		}
		/// <summary>
		/// �����ע
		/// </summary>
		public int vic_doubleconcernnum
		{
			set{ _vic_doubleconcernnum=value;}
			get{return _vic_doubleconcernnum;}
		}
		/// <summary>
		/// ��˿����
		/// </summary>
		public int vic_fannum
		{
			set{ _vic_fannum=value;}
			get{return _vic_fannum;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public int vic_ilognum
		{
			set{ _vic_ilognum=value;}
			get{return _vic_ilognum;}
		}
		/// <summary>
		/// ������������
		/// </summary>
        public int vic_commentcountnum
		{
			set{ _vic_commentcountnum=value;}
            get { return _vic_commentcountnum; }
		}
		/// <summary>
		/// ��˿��������
		/// </summary>
		public int vic_fanoutnum
		{
			set{ _vic_fanoutnum=value;}
			get{return _vic_fanoutnum;}
		}
		/// <summary>
        /// ��Ϣ��������
		/// </summary>
		public int vic_messageoutnum
		{
			set{ _vic_messageoutnum=value;}
			get{return _vic_messageoutnum;}
		}
		/// <summary>
        /// ��Ϣ����
		/// </summary>
		public int vic_messagenum
		{
			set{ _vic_messagenum=value;}
			get{return _vic_messagenum;}
		}
		/// <summary>
        /// it����
		/// </summary>
		public int vic_atnum
		{
			set{ _vic_atnum=value;}
			get{return _vic_atnum;}
		}
		/// <summary>
        /// ��������
		/// </summary>
		public int vic_commentnum
		{
			set{ _vic_commentnum=value;}
			get{return _vic_commentnum;}
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

