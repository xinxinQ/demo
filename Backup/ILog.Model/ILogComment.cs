using System;
namespace ILog.Model
{
	/// <summary>
	/// 实体类：
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
        /// 自增标识 1
		/// </summary>
        public long ic_id
		{
			set{ _ic_id=value;}
			get{return _ic_id;}
		}
		/// <summary>
		/// Vip表中的userid评论人
		/// </summary>
        public long userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
        /// 发表内容
		/// </summary>
		public string ic_content
		{
			set{ _ic_content=value;}
			get{return _ic_content;}
		}
		/// <summary>
        /// 写入时间 默认是getdate()
		/// </summary>
		public DateTime intime
		{
			set{ _intime=value;}
			get{return _intime;}
		}
		/// <summary>
        /// 当前文章id
		/// </summary>
        public long ic_currentid
		{
			set{ _ic_currentid=value;}
			get{return _ic_currentid;}
		}
		/// <summary>
        /// 当前文章所属用户
		/// </summary>
        public long ic_currentuserid
		{
			set{ _ic_currentuserid=value;}
			get{return _ic_currentuserid;}
		}
		/// <summary>
        /// 来源id
		/// </summary>
		public long is_id
		{
			set{ _is_id=value;}
			get{return _is_id;}
		}

        /// <summary>
        /// 评论类别 1原创 2转发 3评论
        /// </summary>
        public int ic_type
        {
            set { _ic_type = value; }
            get { return _ic_type; }
        }

        /// <summary>
        /// 评论状态 0未删除 1评论人删除 2被评论人删除
        /// </summary>
        public int ic_state
        {
            set { _ic_state = value; }
            get { return _ic_state; }
        }

        /// <summary>
        /// 被回复的评论id
        /// </summary>
        public long ic_commentid
        {
            set { _ic_commentid = value; }
            get { return _ic_commentid; }
        }
        /// <summary>
        /// 被回复的评论内容
        /// </summary>
        public string ic_commentcontent
        {
            set { _ic_commentcontent = value; }
            get { return _ic_commentcontent; }
        }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string nickname { get; set; }


		#endregion Model

	}
}

