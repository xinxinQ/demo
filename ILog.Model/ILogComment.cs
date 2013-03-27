using System;
namespace ILog.Model
{
	/// <summary>
	/// ʵ���ࣺ
	/// </summary>
	public class ILogComment
	{

		#region Model
		private long _ic_id;
        private long _userid;
		private string _ic_content;
		private DateTime _intime;
        private long _ic_currentid;
        private long _ic_currentuserid;
        private long _is_id;
        private int _ic_type;
        private int _ic_state;
        private long _ic_commentid;
        private string _ic_commentcontent;


		/// <summary>
        /// ������ʶ 1
		/// </summary>
        public long ic_id
		{
			set{ _ic_id=value;}
			get{return _ic_id;}
		}
		/// <summary>
		/// Vip���е�userid������
		/// </summary>
        public long userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
        /// ��������
		/// </summary>
		public string ic_content
		{
			set{ _ic_content=value;}
			get{return _ic_content;}
		}
		/// <summary>
        /// д��ʱ�� Ĭ����getdate()
		/// </summary>
		public DateTime intime
		{
			set{ _intime=value;}
			get{return _intime;}
		}
		/// <summary>
        /// ��ǰ����id
		/// </summary>
        public long ic_currentid
		{
			set{ _ic_currentid=value;}
			get{return _ic_currentid;}
		}
		/// <summary>
        /// ��ǰ���������û�
		/// </summary>
        public long ic_currentuserid
		{
			set{ _ic_currentuserid=value;}
			get{return _ic_currentuserid;}
		}
		/// <summary>
        /// ��Դid
		/// </summary>
		public long is_id
		{
			set{ _is_id=value;}
			get{return _is_id;}
		}

        /// <summary>
        /// ������� 1ԭ�� 2ת�� 3����
        /// </summary>
        public int ic_type
        {
            set { _ic_type = value; }
            get { return _ic_type; }
        }

        /// <summary>
        /// ����״̬ 0δɾ�� 1������ɾ�� 2��������ɾ��
        /// </summary>
        public int ic_state
        {
            set { _ic_state = value; }
            get { return _ic_state; }
        }

        /// <summary>
        /// ���ظ�������id
        /// </summary>
        public long ic_commentid
        {
            set { _ic_commentid = value; }
            get { return _ic_commentid; }
        }
        /// <summary>
        /// ���ظ�����������
        /// </summary>
        public string ic_commentcontent
        {
            set { _ic_commentcontent = value; }
            get { return _ic_commentcontent; }
        }

        /// <summary>
        /// �û��ǳ�
        /// </summary>
        public string nickname { get; set; }


		#endregion Model

	}
}

