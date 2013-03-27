using System;
namespace ILog.Model
{
	/// <summary>
	///  µÃÂ¿‡£∫
	/// </summary>
	public class ILogOperateLevel
	{
		#region Model
		private int _iol_id;
		private int _iol_number;
		private string _iol_name;
		private string _iol_actionname;
		private int _iol_actionnumber;
		private DateTime _intime;
		/// <summary>
		/// 
		/// </summary>
		public int iol_id
		{
			set{ _iol_id=value;}
			get{return _iol_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int iol_number
		{
			set{ _iol_number=value;}
			get{return _iol_number;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string iol_name
		{
			set{ _iol_name=value;}
			get{return _iol_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string iol_actionname
		{
			set{ _iol_actionname=value;}
			get{return _iol_actionname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int iol_actionnumber
		{
			set{ _iol_actionnumber=value;}
			get{return _iol_actionnumber;}
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

