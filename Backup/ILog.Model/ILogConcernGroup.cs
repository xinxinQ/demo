using System;
namespace ILog.Model
{
	/// <summary>
	/// 实体类：
	/// </summary>
	public class ILogConcernGroup
	{            

		#region Model
		private long _icg_id;
		private string _icg_name;
		private long _userid;
		private DateTime _intime;
		/// <summary>
        /// 自增标识 1
		/// </summary>
		public long icg_id
		{
			set{ _icg_id=value;}
			get{return _icg_id;}
		}
		/// <summary>
        /// 默认是同事,同学,其他
		/// </summary>
		public string icg_name
		{
			set{ _icg_name=value;}
			get{return _icg_name;}
		}
		/// <summary>
        /// 当用名ID=0时，组为系统组
		/// </summary>
		public long userid
		{
			set{ _userid=value;}
			get{return _userid;}
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

