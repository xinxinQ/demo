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

namespace ILog.Web.Ajax
{
    public partial class AjaxShortToUrl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 根据短地址表中的id得到短地址的详细信息

            ////短地址id
            //string strUid = IMRequest.GetQueryString("suid", true);

            //long shortUrlID = 0;
            //try
            //{
            //    shortUrlID = Convert.ToInt64(strUid);
            //}
            //catch
            //{
            //    shortUrlID = 0;
            //}
            //if (shortUrlID>0)
            //{
            //    ILog.Model.ILogShortUrl ooShortUrl = Ilog.BLL.ILogShortUrl.GetShortUrlInfo(shortUrlID);
            //    if (ooShortUrl!=null)
            //    {
            //        //点击次数加1
            //        ooShortUrl.isu_num = ooShortUrl.isu_num + 1;
            //        //更新点击次数
            //        Ilog.BLL.ILogShortUrl.ShortUrlUpdate(ooShortUrl);
            //        //跳转到对应的长连接地址
            //        Response.Redirect(ooShortUrl.isu_url);
            //    }
            //    else
            //    {
            //        Response.Redirect("/Home.aspx");
            //    }
            //}
            //else
            //{
            //    Response.Redirect("/Home.aspx");
            //}

            #endregion

            #region 根据短地址表中的url得到短地址的详细信息

            //短地址链接
            string url = IMRequest.GetQueryString("surl", false);

            url = url.Replace("Ajax/AjaxShortToUrl.aspx,", "");

            ILog.Model.ILogShortUrl ooShortUrl = Ilog.BLL.ILogShortUrl.GetShortUrlInfoByShortUrl("http://4077.cn/" + url);
            if (ooShortUrl != null)
            {
                //点击次数加1
                ooShortUrl.isu_num = ooShortUrl.isu_num + 1;
                //更新点击次数
                Ilog.BLL.ILogShortUrl.ShortUrlUpdate(ooShortUrl);
                //跳转到对应的长连接地址
                Response.Redirect(ooShortUrl.isu_url);
            }
            else
            {
                Response.Write("没有找到");
                //   Response.Redirect("/Home.aspx");
            }

            #endregion

        }
    }
}
