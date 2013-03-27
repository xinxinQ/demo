using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILog.Model
{
    public class ILogAuthenticationHistory
    {
        private long _ia_id;
        private long _userid;
        private string _ia_idnumber;
        private string _ia_comment;
        private int _ia_type;
        private int _ia_state;
        private string _ia_adminname;
        private DateTime? _ia_checktime;
        private string _ia_reason;
        private DateTime _intime;
        /// <summary>
        /// 
        /// </summary>
        public long ia_id
        {
            set { _ia_id = value; }
            get { return _ia_id; }
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
        /// 身份证号
        /// </summary>
        public string ia_IDNumber
        {
            set { _ia_idnumber = value; }
            get { return _ia_idnumber; }
        }
        /// <summary>
        /// 认证说明
        /// </summary>
        public string ia_Comment
        {
            set { _ia_comment = value; }
            get { return _ia_comment; }
        }
        /// <summary>
        /// 认证类型：1 个人认证 加i；2 名人认证 加v
        /// </summary>
        public int ia_Type
        {
            set { _ia_type = value; }
            get { return _ia_type; }
        }
        /// <summary>
        /// 认证状态：0 待审核；1 审核通过；2 审核不通过
        /// </summary>
        public int ia_State
        {
            set { _ia_state = value; }
            get { return _ia_state; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ia_adminname
        {
            set { _ia_adminname = value; }
            get { return _ia_adminname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ia_checktime
        {
            set { _ia_checktime = value; }
            get { return _ia_checktime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ia_reason
        {
            set { _ia_reason = value; }
            get { return _ia_reason; }
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
