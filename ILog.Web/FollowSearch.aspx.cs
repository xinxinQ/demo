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
    public partial class FollowSearch : System.Web.UI.Page
    {
        public string SearchContent { get; set; } //组ID
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Com.ILog.Utils.IMRequest.IsGet())
            {
                SearchContent = Com.ILog.Utils.IMRequest.GetQueryString("Search");
            }
        }
    }
}
