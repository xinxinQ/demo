using System;
namespace ILog.Model
{
	/// <summary>
	///  µÃÂ¿‡£∫
	/// </summary>
	public class VipILogLimits
	{
		#region Model
		private int _vil_id;
		private int _userid;
		private int _vil_systemconcernnum;
		private int _vil_systemfannum;
		private DateTime _intime;
		/// <summary>
		/// 
		/// </summary>
		public int vil_id
		{
			set{ _vil_id=value;}
			get{return _vil_id;}
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
		public int vil_systemconcernnum
		{
			set{ _vil_systemconcernnum=value;}
			get{return _vil_systemconcernnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int vil_systemfannum
		{
			set{ _vil_systemfannum=value;}
			get{return _vil_systemfannum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime intime
		{
			set{ _intime=value;}
			get{return _intime;}
		}
		#endregion Model

	}
}

