using System;
namespace ILog.Model
{
	/// <summary>
    /// ʵ���ࣺ��Դ��
	/// </summary>
	public class ILogSource
	{
		#region Model
        private long _is_id;
		private string _is_name;
		private string _is_url;
		private DateTime _intime;
		/// <summary>
        /// ������ʶ 1
		/// </summary>
		public long is_id
		{
			set{ _is_id=value;}
			get{return _is_id;}
		}
		/// <summary>
        /// ��Դ����
		/// </summary>
		public string is_name
		{
			set{ _is_name=value;}
			get{return _is_name;}
		}
		/// <summary>
        /// ����
		/// </summary>
		public string is_url
		{
			set{ _is_url=value;}
			get{return _is_url;}
		}
		/// <summary>
        /// д��ʱ�䣺Ĭ����getdate()
		/// </summary>
		public DateTime intime
		{
			set{ _intime=value;}
			get{return _intime;}
		}
		#endregion Model

	}
}

