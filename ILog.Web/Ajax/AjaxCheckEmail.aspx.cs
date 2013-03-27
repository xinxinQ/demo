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

namespace ILog.Web.Ajax
{
    public partial class AjaxCheckEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string email = IMRequest.GetQueryString("email",false);

            long userid = Ilog.BLL.VipILog.GetVIPUserID();

            int urlState=0;

            //是否存在email
            int existEmail = 0;

              //之前填写的邮箱
            string oldEMail = Ilog.BLL.Vip.GetUserEmail(userid, ref urlState);
            if (oldEMail != email)
            {
                bool isExist=Ilog.BLL.Vip.VipAllExistsEmail(email, ref urlState);
                existEmail = isExist ? 1 : 0;
            }

            if (existEmail==0)
            {
                Response.Write("true");
            }
            else
            {
                Response.Write("false");
            }

        }
    }
}
