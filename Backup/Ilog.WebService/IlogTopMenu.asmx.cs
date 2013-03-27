using System;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Services;


namespace Ilog.WebService
{
    /// <summary>
    /// IlogTopMenu 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
     [System.Web.Script.Services.ScriptService]
    public class IlogTopMenu : System.Web.Services.WebService
    {


        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)] 
        [WebMethod]
        public string ILogGetTopMenu()
        {

            #region 统一校验

            //是否登陆
            //ILog.Common.Common.IsLogin();


            long loUserId = BLL.VipILog.GetVIPUserID();

            string username = Ilog.BLL.VipILog.GetNickNameByUserId(loUserId);


            #endregion

            StringBuilder sb = new StringBuilder();

            sb.Append("{Menu:"); 

            try
            {

                if (loUserId == 0)
                {
                    sb.Append("[{UrlState:'2'},");
                }
                else
                {
                    sb.Append("[{UrlState:'1'},");
                }

                sb.Append("{MenuName:'选仪器',MenuUrl:'http://www.instrument.com.cn/show/'},");

                sb.Append("{MenuName:'学仪器',MenuUrl:'http://www.instrument.com.cn/webinar/'},");

                sb.Append("{MenuName:'看资讯',MenuUrl:'http://www.instrument.com.cn/news/'},");

                sb.Append("{MenuName:'找同行',MenuUrl:'http://bbs.instrument.com.cn/'},");

                sb.Append("{MenuName:'找工作',MenuUrl:'http://job.instrument.com.cn/'}");
            }
            catch
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();
        
        }

        
        


    }
}
