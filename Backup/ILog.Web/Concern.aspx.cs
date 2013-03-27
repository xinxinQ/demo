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

using ILog.UI;
using Com.ILog.Utils;

namespace ILog.Web
{
    public partial class Concern : BaseWebPage
    {
        /// <summary>
        /// 关键字
        /// </summary>
        protected string keyword = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            keyword = IMRequest.GetQueryString("Search", false);
            keyword = Common.Common.GetJScriptGlobalObjectUnEscape(keyword);

        }
    }
}
