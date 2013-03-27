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
using Com.ILog.Utils;
namespace ILog.Web
{
    public partial class Index : System.Web.UI.Page
    {
        /// <summary>
        /// 激活码
        /// </summary>
        protected string code = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            code = IMRequest.GetQueryString("code", true);


        }
    }
}
