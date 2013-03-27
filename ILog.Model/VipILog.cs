using System;
namespace ILog.Model
{
	/// <summary>
	/// ʵ���ࣺ
	/// </summary>
	public class VipILog
	{

		#region Model
		private long _vi_id;
        private long _userid;
		private string _username;
		private string _nickname;
		private string _face;
		private int _vi_memberlevel;
		private DateTime _intime;
		private int _vi_state;
		/// <summary>
        /// ������ʶ 1
		/// </summary>
        public long vi_id
		{
			set{ _vi_id=value;}
			get{return _vi_id;}
		}
		/// <summary>
        /// ͬvip���е�
		/// </summary>
        public long userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
        /// �û���
		/// </summary>
		public string username
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
        /// �ǳƣ�Ĭ�ϵ��ǳƾ�Ϊ�û���
		/// </summary>
		public string nickname
		{
			set{ _nickname=value;}
			get{return _nickname;}
		}
		/// <summary>
        /// �û�ͷ
		/// </summary>
		public string face
		{
			set{ _face=value;}
			get{return _face;}
		}
		/// <summary>
        /// �û��ȼ�0��Ĭ�ϵȼ�,1��i�ȼ���2��v�ȼ� Ĭ����0
		/// </summary>
		public int vi_memberlevel
		{
			set{ _vi_memberlevel=value;}
			get{return _vi_memberlevel;}
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
        /// ��ͨ״̬
		/// </summary>
		public int vi_state
		{
			set{ _vi_state=value;}
			get{return _vi_state;}
		}

        /// <summary>
        /// ��˿��ע״̬
        /// </summary>
        public int iuc_state { get; set; }

        /// <summary>
        /// ����id
        /// </summary>
        public int ci_id { get; set; }

        /// <summary>
        /// ʡid
        /// </summary>
        public int pr_id { get; set; }

        /// <summary>
        /// ��˿��
        /// </summary>
        public int vic_fannum { get; set; }

		#endregion Model

	}
}

