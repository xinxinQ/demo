using System;
namespace ILog.Model
{
	/// <summary>
    /// 实体类：短地址表
	/// </summary>
	public class ILogShortUrl
	{
		#region Model
        private long _isu_id;
		private string _isu_url;
		private string _isu_shorturl;
        private long _isu_num;
		private DateTime _intime;
        private int _isu_type;

		/// <summary>
        /// 自增标识 1
		/// </summary>
		public long isu_id
		{
			set{ _isu_id=value;}
			get{return _isu_id;}
		}
		/// <summary>
        /// 源文地址，是唯一的，
		/// </summary>
		public string isu_url
		{
			set{ _isu_url=value;}
			get{return _isu_url;}
		}
		/// <summary>
        /// 短地址，对应源文件的短地址也是唯一的
		/// </summary>
		public string isu_shorturl
		{
			set{ _isu_shorturl=value;}
			get{return _isu_shorturl;}
		}
		/// <summary>
        /// 访问次数：默认0
		/// </summary>
		public long isu_num
		{
			set{ _isu_num=value;}
			get{return _isu_num;}
		}
		/// <summary>
        /// 默认是getdate()
		/// </summary>
		public DateTime intime
		{
			set{ _intime=value;}
			get{return _intime;}
		}

        /// <summary>
        /// 视频类别 0普通地址 1 视频地址
        /// </summary>
        public int isu_type
        {
            set { _isu_type = value; }
            get { return _isu_type; }
        }
		#endregion Model

	}
}

