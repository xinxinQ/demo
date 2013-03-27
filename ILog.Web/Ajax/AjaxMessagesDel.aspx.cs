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
    public partial class AjaxMessagesDel : ILog.UI.BaseWebPage
    {
        /// <summary>
        /// 站短流水
        /// </summary>
        protected int id;
        /// <summary>
        /// 发信人
        /// </summary>
        protected string fromwhoid;
        /// <summary>
        /// 收信人
        /// </summary>
        protected string towhoid;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Com.ILog.Utils.IMRequest.IsGet())
            {
                id = Com.ILog.Utils.IMRequest.GetQueryInt("id",0);
                fromwhoid = Com.ILog.Utils.IMRequest.GetQueryString("fromwhoid");
                towhoid = Com.ILog.Utils.IMRequest.GetQueryString("towhoid");
            }
        }
    }
}
