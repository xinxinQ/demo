using System;
namespace ILog.Model
{
	/// <summary>
    /// ʵ���ࣺվ����Ϣ��
	/// </summary>
	public class VipMail
	{
		#region Model
        private long _id;
		private string _fromwho;
		private string _towho;
		private long _tid;
		private string _subject;
		private string _content;
		private string _ip;
		private bool _hasread;
		private DateTime _intime;
		/// <summary>
        /// ������ʶ 1
		/// </summary>
		public long id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
        /// ������
		/// </summary>
		public string fromwho
		{
			set{ _fromwho=value;}
			get{return _fromwho;}
		}
		/// <summary>
        /// ������
		/// </summary>
		public string towho
		{
			set{ _towho=value;}
			get{return _towho;}
		}
		/// <summary>
        /// ����id
		/// </summary>
		public long tid
		{
			set{ _tid=value;}
			get{return _tid;}
		}
		/// <summary>
        /// ��Ϣ����
		/// </summary>
		public string subject
		{
			set{ _subject=value;}
			get{return _subject;}
		}
		/// <summary>
        /// ����
		/// </summary>
		public string content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
        /// ����ip
		/// </summary>
		public string ip
		{
			set{ _ip=value;}
			get{return _ip;}
		}
		/// <summary>
        /// �Ƿ��Ķ�
		/// </summary>
		public bool hasread
		{
			set{ _hasread=value;}
			get{return _hasread;}
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
        /// ͷ��
        /// </summary>
        public string face { get; set; }

        /// <summary>
        /// ���ű�ʾ��1�ǻ���0����
        /// </summary>
        public long mailid { get; set; }

        /// <summary>
        /// �û�id
        /// </summary>
        public long userid { get; set; }

        /// <summary>
        /// ������id
        /// </summary>
        public long towhoid { get; set; }

		#endregion Model

	}
}

