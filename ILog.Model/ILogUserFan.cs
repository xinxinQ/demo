using System;
namespace ILog.Model
{
	/// <summary>
    /// ʵ���ࣺ��˿��
	/// </summary>
	public class ILogUserFan
	{
		#region Model
		private long _iuf_id;
        private long _userid;
        private long _concernuserid;
		private DateTime _intime;
        private DateTime _connecttime;


		/// <summary>
        /// ������ʶ 1
		/// </summary>
        public long iuf_id
		{
			set{ _iuf_id=value;}
			get{return _iuf_id;}
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
        /// Vip���е�userid,�ҹ�ע�Ķ���
		/// </summary>
        public long concernuserid
		{
			set{ _concernuserid=value;}
			get{return _concernuserid;}
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
        /// ���һ����ϵʱ��
        /// </summary>
        public DateTime connecttime
        {
            set { _connecttime = value; }
            get { return _connecttime; }
        }
		#endregion Model

	}
}

