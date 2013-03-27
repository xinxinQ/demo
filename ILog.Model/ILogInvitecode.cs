using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILog.Model
{
    public class ILogInvitecode
    {
        private long vi_id;

        /// <summary>
        /// 流水号
        /// </summary>
        public long Vi_id
        {
            get { return vi_id; }
            set { vi_id = value; }
        }

        private long userid;

        /// <summary>
        /// 用户id
        /// </summary>
        public long Userid
        {
            get { return userid; }
            set { userid = value; }
        }

        private string vi_code;

        /// <summary>
        /// 邀请码
        /// </summary>
        public string Vi_code
        {
            get { return vi_code; }
            set { vi_code = value; }
        }

        private long vi_senduserid;

        /// <summary>
        /// 发送人id
        /// </summary>
        public long Vi_senduserid
        {
            get { return vi_senduserid; }
            set { vi_senduserid = value; }
        }

        private DateTime intime;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Intime
        {
            get { return intime; }
            set { intime = value; }
        }

    }

}
