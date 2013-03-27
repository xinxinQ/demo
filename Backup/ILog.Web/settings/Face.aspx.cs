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

namespace ILog.Web.settings
{
    public partial class Face : BaseWebPage
    {
        /// <summary>
        /// 用户头像位置
        /// </summary>
        protected string faceurl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //用户id
            long userid = Ilog.BLL.VipILog.GetVIPUserID();

            ILog.Model.VipILog ooIlog = Ilog.BLL.VipILog.GetModelByUserID(userid);
            if (ooIlog != null)
            {
                faceurl = "/images/face/big/" + ooIlog.face;
            }

        }

    }
}
