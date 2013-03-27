using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILog.Model
{
    public class ILogSchool
    {
        private int _is_id;
        private long _userid;
        private string _is_school;
        private int _is_degree;
        private string _is_degreeName;
        private int _is_entranceyear;
        private int _is_schoolid;
        private DateTime _intime;
        /// <summary>
        /// 
        /// </summary>
        public int is_id
        {
            set { _is_id = value; }
            get { return _is_id; }
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
        /// 学校名称
        /// </summary>
        public string is_school
        {
            set { _is_school = value; }
            get { return _is_school; }
        }
        /// <summary>
        /// 学历 1：大学；2：高中；3：中专技校；4：初中；5：小学
        /// </summary>
        public int is_degree
        {
            set { _is_degree = value; }
            get { return _is_degree; }
        }

        /// <summary>
        /// 学历名称
        /// </summary>
        public string is_degreeName
        {
            set { _is_degreeName = value; }
            get { return _is_degreeName; }
        }

        /// <summary>
        /// 入学年份
        /// </summary>
        public int is_entranceYear
        {
            set { _is_entranceyear = value; }
            get { return _is_entranceyear; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime intime
        {
            set { _intime = value; }
            get { return _intime; }
        }

        /// <summary>
        /// 学校id
        /// </summary>
        public int is_schoolid
        {
            set { _is_schoolid = value; }
            get { return _is_schoolid; }
        }

    }
}
