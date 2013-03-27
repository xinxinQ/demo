using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILog.Model
{
    public class ILogVisithistory
    {
        private long _iv_id;
        private long _userid;
        private long _iv_userid;
        private DateTime _intime;


        /// <summary>
        /// 流水号
        /// </summary>
        public long iv_id
        {
            set { _iv_id = value; }
            get { return _iv_id; }
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
        /// 被访问人id
        /// </summary>
        public long iv_userid
        {
            set { _iv_userid = value; }
            get { return _iv_userid; }
        }
      
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime intime
        {
            set { _intime = value; }
            get { return _intime; }
        }


    }
}
