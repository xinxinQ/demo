using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILog.Model
{
    public class ILogMobileCheck
    {
        #region Model
        private long _im_id;
        private long _userid;
        private string _im_mobilenumber;
        private string _im_checkcode;
        private DateTime _intime;
        /// <summary>
        /// 流水号
        /// </summary>
        public long im_id
        {
            set { _im_id = value; }
            get { return _im_id; }
        }

        /// <summary>
        /// 用户id
        /// </summary>
        public long userid
        {
            set { _userid = value; }
            get { return _userid; }
        }

        /// <summary>
        /// 手机号
        /// </summary>
        public string im_mobilenumber
        {
            set { _im_mobilenumber = value; }
            get { return _im_mobilenumber; }
        }
        /// <summary>
        /// 验证码
        /// </summary>
        public string im_checkcode
        {
            set { _im_checkcode = value; }
            get { return _im_checkcode; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime intime
        {
            set { _intime = value; }
            get { return _intime; }
        }
        #endregion Model
    }
}
