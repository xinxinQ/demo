﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ILog.Web
{
    public partial class Comment : ILog.UI.BaseWebPage
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        protected string strKeyword = "请输入昵称";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Com.ILog.Utils.IMRequest.IsGet())
            {
                strKeyword = Com.ILog.Utils.IMRequest.GetQueryString("keyword_s");

                long userid = 0;

                try
                {
                    userid = Convert.ToInt64(CurrentUserId);
                }
                catch
                {
                    userid = 0;
                }

                //清除用户提醒
                ILog.BLL.VipMail.UserCommentCleared(userid);
            }
        }
    }
}