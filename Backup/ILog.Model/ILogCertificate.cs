using System;
namespace ILog.Model
{
	/// <summary>
    /// 实体类：认证证件表
	/// </summary>
	public class ILogCertificate
	{

		#region Model
        private long _ic_id;
        private long _userid;
		private int _ic_type;
		private string _ic_name;
		private string _ic_pic;
		private DateTime _intime;
		/// <summary>
        /// 自增标识 1
		/// </summary>
        public long ic_id
		{
			set{ _ic_id=value;}
			get{return _ic_id;}
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
        /// 证件类型
		/// </summary>
		public int ic_type
		{
			set{ _ic_type=value;}
			get{return _ic_type;}
		}
		/// <summary>
        /// 证件的名称
		/// </summary>
		public string ic_name
		{
			set{ _ic_name=value;}
			get{return _ic_name;}
		}
		/// <summary>
        /// 证件描述图命名
		/// </summary>
		public string ic_pic
		{
			set{ _ic_pic=value;}
			get{return _ic_pic;}
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

