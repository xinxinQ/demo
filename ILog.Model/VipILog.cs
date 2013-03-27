using System;
namespace ILog.Model
{
	/// <summary>
	/// 实体类：
	/// </summary>
	public class VipILog
	{

		#region Model
		private long _vi_id;
        private long _userid;
		private string _username;
		private string _nickname;
		private string _face;
		private int _vi_memberlevel;
		private DateTime _intime;
		private int _vi_state;
		/// <summary>
        /// 自增标识 1
		/// </summary>
        public long vi_id
		{
			set{ _vi_id=value;}
			get{return _vi_id;}
		}
		/// <summary>
        /// 同vip表中的
		/// </summary>
        public long userid
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
        /// 用户名
		/// </summary>
		public string username
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
        /// 昵称，默认的昵称就为用户名
		/// </summary>
		public string nickname
		{
			set{ _nickname=value;}
			get{return _nickname;}
		}
		/// <summary>
        /// 用户头
		/// </summary>
		public string face
		{
			set{ _face=value;}
			get{return _face;}
		}
		/// <summary>
        /// 用户等级0，默认等级,1加i等级，2加v等级 默认是0
		/// </summary>
		public int vi_memberlevel
		{
			set{ _vi_memberlevel=value;}
			get{return _vi_memberlevel;}
		}
		/// <summary>
		/// 写入时间
		/// </summary>
		public DateTime intime
		{
			set{ _intime=value;}
			get{return _intime;}
		}
		/// <summary>
        /// 开通状态
		/// </summary>
		public int vi_state
		{
			set{ _vi_state=value;}
			get{return _vi_state;}
		}

        /// <summary>
        /// 粉丝关注状态
        /// </summary>
        public int iuc_state { get; set; }

        /// <summary>
        /// 城市id
        /// </summary>
        public int ci_id { get; set; }

        /// <summary>
        /// 省id
        /// </summary>
        public int pr_id { get; set; }

        /// <summary>
        /// 粉丝数
        /// </summary>
        public int vic_fannum { get; set; }

		#endregion Model

	}
}

