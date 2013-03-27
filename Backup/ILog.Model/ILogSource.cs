using System;
namespace ILog.Model
{
	/// <summary>
    /// 实体类：来源表
	/// </summary>
	public class ILogSource
	{
		#region Model
        private long _is_id;
		private string _is_name;
		private string _is_url;
		private DateTime _intime;
		/// <summary>
        /// 自增标识 1
		/// </summary>
		public long is_id
		{
			set{ _is_id=value;}
			get{return _is_id;}
		}
		/// <summary>
        /// 来源名称
		/// </summary>
		public string is_name
		{
			set{ _is_name=value;}
			get{return _is_name;}
		}
		/// <summary>
        /// 链接
		/// </summary>
		public string is_url
		{
			set{ _is_url=value;}
			get{return _is_url;}
		}
		/// <summary>
        /// 写入时间：默认是getdate()
		/// </summary>
		public DateTime intime
		{
			set{ _intime=value;}
			get{return _intime;}
		}
		#endregion Model

	}
}

