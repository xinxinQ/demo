using System;
namespace ILog.Model
{
	/// <summary>
	/// 实体类：推送表
	/// </summary>
	public class ILogSpread
	{
		#region Model
        private long _is_id;
        private long _iso_id;
		private int _is_type;
		private string _is_content;
        private long _is_fanuserid;
        private long _userid;
		private DateTime _intime;
		private int _iss_id;
        private int _is_spreadnum;
        private int _is_commentnum;
        private long _io_id;
        private int _is_spreadtype;
        private int _is_isoriginal;

		/// <summary>
        /// 自增标识 1
		/// </summary>
        public long is_id
		{
			set{ _is_id=value;}
			get{return _is_id;}
		}
		/// <summary>
        /// 博文id
		/// </summary>
		public long iso_id
		{
			set{ _iso_id=value;}
			get{return _iso_id;}
		}

        /// <summary>
        /// 原创id
        /// </summary>
        public long io_id
        {
            set { _io_id = value; }
            get { return _io_id; }
        }

		/// <summary>
        /// 博文类型：0：ilog原创，1：转发
		/// </summary>
		public int is_type
		{
			set{ _is_type=value;}
			get{return _is_type;}
		}
		/// <summary>
        /// 博文内容
		/// </summary>
		public string is_content
		{
			set{ _is_content=value;}
			get{return _is_content;}
		}
		/// <summary>
        /// 粉丝id
		/// </summary>
        public long is_fanuserid
		{
			set{ _is_fanuserid=value;}
			get{return _is_fanuserid;}
		}
		/// <summary>
        /// Vip表中的userid字段
		/// </summary>
        public long userid
		{
			set{ _userid=value;}
			get{return _userid;}
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
        /// 来源id
		/// </summary>
		public int iss_id
		{
			set{ _iss_id=value;}
			get{return _iss_id;}
		}
        /// <summary>
        /// 博文转发数量
        /// </summary>
        public int is_spreadnum
        {
            set { _is_spreadnum = value; }
            get { return _is_spreadnum; }
        }

        /// <summary>
        /// 评论次数 默认是0
        /// </summary>
        public int is_commentnum
        {
            set { _is_commentnum = value; }
            get { return _is_commentnum; }
        }


        /// <summary>
        /// 传播类别 0传播 1转发
        /// </summary>
        public int is_spreadtype
        {
            set { _is_spreadtype = value; }
            get { return _is_spreadtype; }
        }

        /// <summary>
        /// 是否原创 0 否 1是
        /// </summary>
        public int is_isoriginal
        {
            set { _is_isoriginal = value; }
            get { return _is_isoriginal; }
        }

		#endregion Model

	}
}

