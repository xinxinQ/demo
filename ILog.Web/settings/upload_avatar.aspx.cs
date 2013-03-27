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

using System.IO;
using System.Drawing;

using Com.ILog.Utils;
using ILog.UI;

namespace ILog.Web.settings
{
    public partial class upload_avatar : BaseWebPage
    {
        /// <summary>
        /// 文件大小
        /// </summary>
        protected int FormSize = 0;

        /// <summary>
        /// 文件二进制流
        /// </summary>
        protected byte[] FormData;

        /// <summary>
        /// 中等图片名称
        /// </summary>
        protected string strSaveFileName_Normal = "";

        /// <summary>
        /// 大图名称
        /// </summary>
        protected string strSaveFileName_Big = "";

        /// <summary>
        /// 小图名称
        /// </summary>
        protected string strSaveFileName_Small = "";

        /// <summary>
        /// Get访问方式
        /// </summary>
        protected bool isGet = false;

        /// <summary>
        /// post访问方式
        /// </summary>
        protected bool isPost = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            //用户id
            long userid = Ilog.BLL.VipILog.GetVIPUserID();

            //头像名称
            string faceName = DateTime.Now.ToString("yyyyMMddHHmm") + userid + ".png";
            strSaveFileName_Normal = "normal/" + faceName;
            strSaveFileName_Big = "big/" + faceName;
            strSaveFileName_Small = "small/" + faceName;

            int filesize = Request.TotalBytes;
            byte[] b_File = Request.BinaryRead(filesize);

            System.IO.MemoryStream ms = new System.IO.MemoryStream(b_File);

            using (Stream localFile = new FileStream(Server.MapPath("/images/face/" + strSaveFileName_Big), FileMode.OpenOrCreate))
            {
                //ms.ToArray()转换为字节数组就是想要的图片源字节
                localFile.Write(ms.ToArray(), 0, (int)ms.Length);
            }

            try
            {
                Com.ILog.Utils.Thumbnail.MakeThumbnailImage(Server.MapPath("/images/face/" + strSaveFileName_Big), Server.MapPath("/images/face/" + strSaveFileName_Normal), 100, 100);
            }
            catch (Exception)
            {
                
                throw;
            }


            Com.ILog.Utils.Thumbnail.MakeThumbnailImage(Server.MapPath("/images/face/" + strSaveFileName_Big), Server.MapPath("/images/face/" + strSaveFileName_Small), 50, 50);

            Ilog.BLL.VipILog.VipILogUpdateFace(userid, faceName);

            Response.Write("200");

        }

    }
}
