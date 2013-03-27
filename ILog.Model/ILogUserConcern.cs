using System;
namespace ILog.Model
{
	/// <summary>
	/// ʵ���ࣺ��ע��
	/// </summary>
	public class ILogUserConcern
	{
		#region Model
		private long _iuc_id;
        private long _userid;
        private long _concernuserid;
        private long _icg_id;
		private DateTime _intime;
		private bool _iuc_state;
        private DateTime _connectime;

		/// <summary>
        /// ������ʶ 1
		/// </summary>
        public long iuc_id
		{
			set{ _iuc_id=value;}
			get{return _iuc_id;}
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
        /// Ilog_usergroup���е�ug_id����������id
		/// </summary>
        public long icg_id
		{
			set{ _icg_id=value;}
			get{return _icg_id;}
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
        /// �Ƿ����ע,Ĭ���Ƿ�
		/// </summary>
		public bool iuc_state
		{
			set{ _iuc_state=value;}
			get{return _iuc_state;}
		}

        /// <summary>
        /// �����ϵʱ��
        /// </summary>
        public DateTime connectime
        {
            set { _connectime = value; }
            get { return _connectime; }
        }
		#endregion Model

	}
}

