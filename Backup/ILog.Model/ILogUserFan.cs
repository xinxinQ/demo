using System;
namespace ILog.Model
{
	/// <summary>
    /// 实体类：粉丝表
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
        /// 自增标识 1
		/// </summary>
        public long iuf_id
		{
			set{ _iuf_id=value;}
			get{return _iuf_id;}
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
		/// 写入时间
		/// </summary>
		public DateTime intime
		{
			set{ _intime=value;}
			get{return _intime;}
		}

        /// <summary>
        /// 最后一次联系时间
        /// </summary>
        public DateTime connecttime
        {
            set { _connecttime = value; }
            get { return _connecttime; }
        }
		#endregion Model

	}
}

