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

namespace ILog.Web.Ajax
{
    public partial class AjaxSendMail : ILog.UI.BaseWebPage
    {
        /// <summary>
        /// ation：1站短第一个页面，2站短第一个页面，3站短第一个页面
        /// </summary>
        protected string strAtion = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //strAtion = Com.ILog.Utils.IMRequest.GetQueryString("ation");
        }
    }
}
