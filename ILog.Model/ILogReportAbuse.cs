using System;
namespace ILog.Model
{
	/// <summary>
	/// ÊµÌåÀà£º
	/// </summary>
	public class ILogReportAbuse
	{
		#region Model
		private int _ir_id;
		private int _userid;
		private string _ir_content;
		private string _ip;
		private DateTime _intime;
		private string _ir_desc;
		private DateTime _ir_time;
		/// <summary>
		/// 
		/// </summary>
		public int ir_id
		{
			set{ _ir_id=value;}
			get{return _ir_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ir_content
		{
			set{ _ir_content=value;}
			get{return _ir_content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ip
		{
			set{ _ip=value;}
			get{return _ip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime intime
		{
			set{ _intime=value;}
			get{return _intime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ir_desc
		{
			set{ _ir_desc=value;}
			get{return _ir_desc;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime ir_time
		{
			set{ _ir_time=value;}
			get{return _ir_time;}
		}
		#endregion Model

	}
}

