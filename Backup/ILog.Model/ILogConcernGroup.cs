using System;
namespace ILog.Model
{
	/// <summary>
	/// ʵ���ࣺ
	/// </summary>
	public class ILogConcernGroup
	{            

		#region Model
		private long _icg_id;
		private string _icg_name;
		private long _userid;
		private DateTime _intime;
		/// <summary>
        /// ������ʶ 1
		/// </summary>
		public long icg_id
		{
			set{ _icg_id=value;}
			get{return _icg_id;}
		}
		/// <summary>
        /// Ĭ����ͬ��,ͬѧ,����
		/// </summary>
		public string icg_name
		{
			set{ _icg_name=value;}
			get{return _icg_name;}
		}
		/// <summary>
        /// ������ID=0ʱ����Ϊϵͳ��
		/// </summary>
		public long userid
		{
			set{ _userid=value;}
			get{return _userid;}
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

