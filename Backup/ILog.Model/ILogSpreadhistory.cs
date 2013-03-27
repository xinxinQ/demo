using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILog.Model
{
    /// <summary>
    /// 转发历史表
    /// </summary>
    public class ILogSpreadhistory
    {
        private long _ih_id;
        private long _io_id;
        private DateTime _intime;


        /// <summary>
        /// 流水号
        /// </summary>
        public long ih_id
        {
            set { _ih_id = value; }
            get { return _ih_id; }
        }


        /// <summary>
        /// 原创id
        /// </summary>
        public long io_id
        {
            set { _io_id = value; }
            get { return _io_id; }
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
