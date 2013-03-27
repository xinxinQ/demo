using System;
namespace ILog.Model
{
	/// <summary>
	/// ʵ���ࣺͼƬ��
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
        /// ������ʶ 1
		/// </summary>
        public long ip_id
		{
			set{ _ip_id=value;}
			get{return _ip_id;}
		}
		/// <summary>
        /// ilog_createword���е�cw_id
		/// </summary>
		public long io_id
		{
			set{ _io_id=value;}
			get{return _io_id;}
		}
		/// <summary>
        /// �ļ�����
		/// </summary>
		public string ip_name
		{
			set{ _ip_name=value;}
			get{return _ip_name;}
		}
		/// <summary>
        /// д��ʱ�䣺Ĭ����getdate()
		/// </summary>
		public DateTime intime
		{
			set{ _intime=value;}
			get{return _intime;}
		}
		/// <summary>
        /// ͼƬ��ʽ��Jpg,gif(0:jpg 1:gif)
		/// </summary>
		public int ip_type
		{
			set{ _ip_type=value;}
			get{return _ip_type;}
		}
        /// <summary>
        /// ͼƬ��ʶ
        /// </summary>
        public string ip_mark
        {
            set { _ip_mark = value; }
            get { return _ip_mark; }
        }
		#endregion Model

	}
}

