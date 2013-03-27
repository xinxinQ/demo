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
    public partial class AjaxReplyMail : ILog.UI.BaseWebPage
    {
        /// <summary>
        /// 收信人
        /// </summary>
        protected string strTow;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Com.ILog.Utils.IMRequest.IsGet())
            {
                int id = Com.ILog.Utils.IMRequest.GetQueryInt("id", 0);

                long id_ = Convert.ToInt64(id);

                string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

                //校验数据类型
                if (!ILog.Common.Common.Int_IsType(strUserId))
                {
                    HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
                }

                long loUserId = Convert.ToInt64(strUserId);

                string[] arrTow = ILog.BLL.VipMail.GetTowhoById_arr(id_, loUserId);

                strTow = arrTow[0];
            }
        }
    }
}
