using System;
namespace ILog.Model
{
	/// <summary>
	/// 实体类：关注表
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
        /// 自增标识 1
		/// </summary>
        public long iuc_id
		{
			set{ _iuc_id=value;}
			get{return _iuc_id;}
		}
		/// <summary>
        /// Vip表中的的userid字段
		/// </summary>
        public long userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
        /// Vip表中的userid,我关注的对象
		/// </summary>
        public long concernuserid
		{
			set{ _concernuserid=value;}
			get{return _concernuserid;}
		}
		/// <summary>
        /// Ilog_usergroup表中的ug_id，用所在组id
		/// </summary>
        public long icg_id
		{
			set{ _icg_id=value;}
			get{return _icg_id;}
		}
		/// <summary>
        /// 写入时间
		/// </summary>
		public DateTime intime
		{
			set{ _intime=value;}
			get{return _intime;}
		}
		/// <summary>
        /// 是否互相关注,默认是否
		/// </summary>
		public bool iuc_state
		{
			set{ _iuc_state=value;}
			get{return _iuc_state;}
		}

        /// <summary>
        /// 最近联系时间
        /// </summary>
        public DateTime connectime
        {
            set { _connectime = value; }
            get { return _connectime; }
        }
		#endregion Model

	}
}

