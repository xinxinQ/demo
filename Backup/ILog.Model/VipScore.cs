using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILog.Model
{
    public class VipScore
    {
        private string _username;
        private decimal _score;
        private int _knowledge;
        private int _fame;
        private int _onlinetime;
        private int _actiontype;
        private DateTime _actiontime;
        private int _id;
        private string _ip;
        private string _referer;
        private int _gold;

        /// <summary>
        ///  用户名
        /// </summary>
        public string username
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 积分
        /// </summary>
        public decimal score
        {
            set { _score = value; }
            get { return _score; }
        }
        /// <summary>
        /// 经验
        /// </summary>
        public int knowledge
        {
            set { _knowledge = value; }
            get { return _knowledge; }
        }
        /// <summary>
        /// 声望
        /// </summary>
        public int fame
        {
            set { _fame = value; }
            get { return _fame; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int OnlineTime
        {
            set { _onlinetime = value; }
            get { return _onlinetime; }
        }
        /// <summary>
        /// 插入积分记录=16
        /// </summary>
        public int ActionType
        {
            set { _actiontype = value; }
            get { return _actiontype; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime ActionTime
        {
            set { _actiontime = value; }
            get { return _actiontime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }

        /// <summary>
        /// IP地址
        /// </summary>
        public string ip
        {
            set { _ip = value; }
            get { return _ip; }
        }
        /// <summary>
        /// 加分原因
        /// </summary>
        public string referer
        {
            set { _referer = value; }
            get { return _referer; }
        }

        /// <summary>
        /// 金币
        /// </summary>
        public int gold
        {
            set { _gold = value; }
            get { return _gold; }
        }
    }
}
