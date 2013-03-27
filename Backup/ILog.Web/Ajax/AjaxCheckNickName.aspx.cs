using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Com.ILog.Utils;

namespace ILog.Web.Ajax
{
    public partial class AjaxCheckNickName : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //昵称
            string nickname = IMRequest.GetQueryString("nickname", false);

            nickname = Utils.UrlDecode(nickname);

            long userid = Ilog.BLL.VipILog.GetVIPUserID();

            int urlState = 0;

            //昵称重复个数
            int count = 0;

            count = Ilog.BLL.Vip.CheckNickNameExists(userid, nickname, ref urlState);

            if (count == 0)
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
