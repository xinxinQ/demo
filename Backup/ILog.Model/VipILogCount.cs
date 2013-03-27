using System;
namespace ILog.Model
{
	/// <summary>
	/// 实体类：
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
        /// vic_id：流水号
		/// </summary>
        public long vic_id
		{
			set{ _vic_id=value;}
			get{return _vic_id;}
		}
		/// <summary>
		/// 用户id
		/// </summary>
        public long userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
        /// 关注数量
		/// </summary>
		public int vic_concernnum
		{
			set{ _vic_concernnum=value;}
			get{return _vic_concernnum;}
		}
		/// <summary>
		/// 单项关注数量
		/// </summary>
		public int vic_onewayconcernnum
		{
			set{ _vic_onewayconcernnum=value;}
			get{return _vic_onewayconcernnum;}
		}
		/// <summary>
		/// 互相关注
		/// </summary>
		public int vic_doubleconcernnum
		{
			set{ _vic_doubleconcernnum=value;}
			get{return _vic_doubleconcernnum;}
		}
		/// <summary>
		/// 粉丝数量
		/// </summary>
		public int vic_fannum
		{
			set{ _vic_fannum=value;}
			get{return _vic_fannum;}
		}
		/// <summary>
		/// 博文数量
		/// </summary>
		public int vic_ilognum
		{
			set{ _vic_ilognum=value;}
			get{return _vic_ilognum;}
		}
		/// <summary>
		/// 评论提醒数量
		/// </summary>
        public int vic_commentcountnum
		{
			set{ _vic_commentcountnum=value;}
            get { return _vic_commentcountnum; }
		}
		/// <summary>
		/// 粉丝提醒数量
		/// </summary>
		public int vic_fanoutnum
		{
			set{ _vic_fanoutnum=value;}
			get{return _vic_fanoutnum;}
		}
		/// <summary>
        /// 消息提醒数量
		/// </summary>
		public int vic_messageoutnum
		{
			set{ _vic_messageoutnum=value;}
			get{return _vic_messageoutnum;}
		}
		/// <summary>
        /// 消息数量
		/// </summary>
		public int vic_messagenum
		{
			set{ _vic_messagenum=value;}
			get{return _vic_messagenum;}
		}
		/// <summary>
        /// it数量
		/// </summary>
		public int vic_atnum
		{
			set{ _vic_atnum=value;}
			get{return _vic_atnum;}
		}
		/// <summary>
        /// 评论数量
		/// </summary>
		public int vic_commentnum
		{
			set{ _vic_commentnum=value;}
			get{return _vic_commentnum;}
		}
		/// <summary>
		/// 写入时间
		/// </summary>
		public DateTime intime
		{
			set{ _intime=value;}
			get{return _intime;}
		}
		#endregion Model

	}
}

