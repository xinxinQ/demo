using System;
namespace ILog.Model
{
	/// <summary>
    ///  实体类：@信息表
	/// </summary>
	public class ILogat
	{

		#region Model
		private long _ia_id;
		private long _userid;
		private long _ia_atuserid;
		private string _ia_content;
		private DateTime _intime;
		private int _ia_type;
        private int _is_id;
        private long _iso_id;
        private long _ia_logid;


		/// <summary>
        /// 自增标识 1
		/// </summary>
        public long ia_id
		{
			set{ _ia_id=value;}
			get{return _ia_id;}
		}
		/// <summary>
		/// Vip表中的userid@用户
		/// </summary>
        public long userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
        /// 发出@用户
		/// </summary>
        public long ia_atuserid
		{
			set{ _ia_atuserid=value;}
			get{return _ia_atuserid;}
		}
		/// <summary>
        /// 内容
		/// </summary>
		public string ia_content
		{
			set{ _ia_content=value;}
			get{return _ia_content;}
		}
		/// <summary>
        ///写入时间  默认是getdate()
		/// </summary>
		public DateTime intime
		{
			set{ _intime=value;}
			get{return _intime;}
		}
		/// <summary>
        /// 0微博，1评论，2转发
		/// </summary>
		public int ia_type
		{
			set{ _ia_type=value;}
			get{return _ia_type;}
		}
		/// <summary>
        /// 来源id
		/// </summary>
		public int is_id
		{
			set{ _is_id=value;}
			get{return _is_id;}
		}
        /// <summary>
        /// 原创id
        /// </summary>
        public long iso_id
        {
            set { _iso_id = value; }
            get { return _iso_id; }
        }

        /// <summary>
        /// 博文id
        /// </summary>
        public long ia_logid
        {
            set { _ia_logid = value; }
            get { return _ia_logid; }
        }
		#endregion Model

	}
}

