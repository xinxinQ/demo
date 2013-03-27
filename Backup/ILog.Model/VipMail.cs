using System;
namespace ILog.Model
{
	/// <summary>
    /// 实体类：站内信息表
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
        /// 自增标识 1
		/// </summary>
		public long id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
        /// 发送人
		/// </summary>
		public string fromwho
		{
			set{ _fromwho=value;}
			get{return _fromwho;}
		}
		/// <summary>
        /// 接收人
		/// </summary>
		public string towho
		{
			set{ _towho=value;}
			get{return _towho;}
		}
		/// <summary>
        /// 帖子id
		/// </summary>
		public long tid
		{
			set{ _tid=value;}
			get{return _tid;}
		}
		/// <summary>
        /// 信息标题
		/// </summary>
		public string subject
		{
			set{ _subject=value;}
			get{return _subject;}
		}
		/// <summary>
        /// 内容
		/// </summary>
		public string content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
        /// 发送ip
		/// </summary>
		public string ip
		{
			set{ _ip=value;}
			get{return _ip;}
		}
		/// <summary>
        /// 是否阅读
		/// </summary>
		public bool hasread
		{
			set{ _hasread=value;}
			get{return _hasread;}
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
        /// 头像
        /// </summary>
        public string face { get; set; }

        /// <summary>
        /// 回信表示列1是回信0发信
        /// </summary>
        public long mailid { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public long userid { get; set; }

        /// <summary>
        /// 收信人id
        /// </summary>
        public long towhoid { get; set; }

		#endregion Model

	}
}

