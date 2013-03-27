using System;
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
    public partial class Messages : ILog.UI.BaseWebPage
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        protected string strKeyword = "";

        protected void Page_Load(object sender, EventArgs e)
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
            if(Com.ILog.Utils.IMRequest.IsGet())
            {
                ILog.BLL.VipMail.UserMessagesCleared(userid);
            }
        }
    }
}
