using System;
namespace ILog.Model
{
	/// <summary>
	/// 实体类：
	/// </summary>
	public class ILogOperate
                 
	{
		#region Model
		private int _io_id;
		private string _admin_name;
		private int _io_actionnumber;
		private string _io_ip;
		private int _io_oid;
		private string _io_title;
		private string _io_tablename;
		private DateTime _intime;
		/// <summary>
        /// 自增长ID
		/// </summary>
		public int io_id
		{
			set{ _io_id=value;}
			get{return _io_id;}
		}
		/// <summary>
        /// 操作用户
		/// </summary>
		public string admin_name
		{
			set{ _admin_name=value;}
			get{return _admin_name;}
		}
		/// <summary>
        /// log_Operate_Class表中的
		/// </summary>
		public int io_actionnumber
		{
			set{ _io_actionnumber=value;}
			get{return _io_actionnumber;}
		}
		/// <summary>
        /// 获取操作用户的ip地址
		/// </summary>
		public string io_ip
		{
			set{ _io_ip=value;}
			get{return _io_ip;}
		}
		/// <summary>
        /// 执行记录ID
		/// </summary>
		public int io_oid
		{
			set{ _io_oid=value;}
			get{return _io_oid;}
		}
		/// <summary>
        /// 执行记录标题
		/// </summary>
		public string io_title
		{
			set{ _io_title=value;}
			get{return _io_title;}
		}
		/// <summary>
        /// 执行表名称
		/// </summary>
		public string io_tablename
		{
			set{ _io_tablename=value;}
			get{return _io_tablename;}
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

