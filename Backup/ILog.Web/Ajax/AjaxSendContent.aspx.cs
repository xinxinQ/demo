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
using System.Text;

namespace ILog.Web.Ajax
{
    public partial class AjaxSendContent : System.Web.UI.Page
    {

        /// <summary>
        /// 发送博文.by lx on 20120612
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            //定义返回结果
            StringBuilder resultInfo = new StringBuilder();
            StringBuilder result = new StringBuilder();

            bool isPost = IMRequest.IsPost();

            if (isPost)
            {

                int userId = string.IsNullOrEmpty(CurrentCookie.GetCookieByKey("useid")) == true
                    ? 0 : Convert.ToInt32(CurrentCookie.GetCookieByKey("useid"));

                //userId = 1001858;

                string mark = IMRequest.GetFormString("guid", false);

                string content =IMRequest.GetFormString("content", false);

                string ip = Request.UserHostAddress;


                //VipIlogUserInfo.VipIlogUserSoapClient sendServices = new ILog.Web.VipIlogUserInfo.VipIlogUserSoapClient();

                

               
                result.Append("{");

                result.AppendFormat("userid:'{0}',", userId);
                result.AppendFormat("mark:'{0}',", mark);
                result.AppendFormat("isid:'{0}',", 0);
                result.AppendFormat("ip:'{0}',", ip);
                result.AppendFormat("content:'{0}',", content);
                result.AppendFormat("type:'{0}',", 0);//0代表原创
                result.AppendFormat("time:'{0}'", DateTime.Now);               

                result.Append("}");

                //string sendResult = sendServices.ILogAddOriginalInfo(result.ToString());

                //resultInfo.Append(sendResult);            

            }
            else 
            {
                result.Length = 0;
                result.Append("{");
                result.Append("state:0");
                result.Append("}");
                resultInfo.Append(result);

            }

            Response.Write(resultInfo);



        }

    }
}
