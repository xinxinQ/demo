using System;
namespace ILog.Model
{
	/// <summary>
	/// 实体类：图片表
	/// </summary>
	public class ILogPic
	{
		#region Model
        private long _ip_id;
        private long _io_id;
		private string _ip_name;
		private DateTime _intime;
		private int _ip_type;
        private string _ip_mark;

		/// <summary>
        /// 自增标识 1
		/// </summary>
        public long ip_id
		{
			set{ _ip_id=value;}
			get{return _ip_id;}
		}
		/// <summary>
        /// ilog_createword表中的cw_id
		/// </summary>
		public long io_id
		{
			set{ _io_id=value;}
			get{return _io_id;}
		}
		/// <summary>
        /// 文件名称
		/// </summary>
		public string ip_name
		{
			set{ _ip_name=value;}
			get{return _ip_name;}
		}
		/// <summary>
        /// 写入时间：默认是getdate()
		/// </summary>
		public DateTime intime
		{
			set{ _intime=value;}
			get{return _intime;}
		}
		/// <summary>
        /// 图片格式：Jpg,gif(0:jpg 1:gif)
		/// </summary>
		public int ip_type
		{
			set{ _ip_type=value;}
			get{return _ip_type;}
		}
        /// <summary>
        /// 图片标识
        /// </summary>
        public string ip_mark
        {
            set { _ip_mark = value; }
            get { return _ip_mark; }
        }
		#endregion Model

	}
}

