using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILog.Model
{
    public class VIPLoginErr
    {
        private long id;
        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private string ip;
        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        private DateTime intime;
        public DateTime Intime
        {
            get { return intime; }
            set { intime = value; }
        }

        private int erint;
        public int Erint
        {
            get { return erint; }
            set { erint = value; }
        }

        private string cookieID;
        public string CookieID
        {
            get { return cookieID; }
            set { cookieID = value; }
        }

        private string agent;
        public string Agent
        {
            get { return agent; }
            set { agent = value; }
        }


    }
}
