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
    public partial class HePersonal : System.Web.UI.Page
    {
        public int userid { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            userid = Com.ILog.Utils.IMRequest.GetQueryInt("userid", 0);
             if (userid == 0)
             {
                 Response.Redirect("Personal.aspx");
             }

        }

    }
}
