using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILog.Model
{
    public class UBBForbidPost
    {

        private int id;
        public int Id
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

        private int forumID;
        public int ForumID
        {
            get { return forumID; }
            set { forumID = value; }
        }

        private DateTime outDate;
        public DateTime OutDate
        {
            get { return outDate; }
            set { outDate = value; }
        }

        private string reason;
        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }

        private DateTime inTime;
        public DateTime InTime
        {
            get { return inTime; }
            set { inTime = value; }
        }

        private string admin;
        public string Admin
        {
            get { return admin; }
            set { admin = value; }
        }


    }

}
