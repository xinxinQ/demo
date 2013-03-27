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
    public partial class MessagesDel : ILog.UI.BaseWebPage
    {
        /// <summary>
        /// 接收参数
        /// </summary>
        protected int id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Com.ILog.Utils.IMRequest.IsGet())
            {
                id = Com.ILog.Utils.IMRequest.GetQueryInt("id", 0);

                //无效id
                if (id <= 0)
                {
                    Response.Redirect("Msg");
                }
            }
        }
    }
}
