using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Services;
using System.Text;
namespace Ilog.WebService
{
    /// <summary>
    /// ILogAdvertisement 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
     [System.Web.Script.Services.ScriptService]
    public class ILogAdvertisement : System.Web.Services.WebService
    {

        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetTopMenu()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("{ILogAd:");

            try
            {
                sb.Append("[{UrlState:'1'},");

                sb.Append("{AdName:'归真堂',AdUrl:'http://www.instrument.com.cn/show/',adImgUrl:'http://simg.instrument.com.cn/ilog/blue/images/pic1.jpg'},");

                sb.Append("{AdName:'归真堂',AdUrl:'http://www.instrument.com.cn/show/',adImgUrl:'http://simg.instrument.com.cn/ilog/blue/images/pic1.jpg'},");

                sb.Append("{AdName:'归真堂',AdUrl:'http://www.instrument.com.cn/show/',adImgUrl:'http://simg.instrument.com.cn/ilog/blue/images/pic1.jpg'},");

                sb.Append("{AdName:'归真堂',AdUrl:'http://www.instrument.com.cn/show/',adImgUrl:'http://simg.instrument.com.cn/ilog/blue/images/pic1.jpg'},");

                sb.Append("{AdName:'归真堂',AdUrl:'http://www.instrument.com.cn/show/',adImgUrl:'http://simg.instrument.com.cn/ilog/blue/images/pic1.jpg'}");
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
