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
using System.Drawing;
using System.Drawing.Imaging;
using Com.ILog.Utils;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Text;

namespace ILog.Web.Ajax
{
    public partial class AjaxUploadPicture : System.Web.UI.Page
    {


        #region 变量

        /// <summary>
        /// 生成缩略图使用
        /// </summary>
        public System.Drawing.Image ResourceImage;

        #endregion

        #region 处理图片资源

        /// <summary>
        /// 图片分享.by lx on 20120607
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            StringBuilder picInfo = new StringBuilder();

            string guid = IMRequest.GetFormString("guid");


            //获取上传图片资源集合
            HttpPostedFile files = HttpContext.Current.Request.Files["uploadpic"];

            long maxSize = 4194304;//4MB

            if (files != null)
            {

                //获取文件扩展名
                string extension = System.IO.Path.GetExtension(files.FileName);
                //文件大小
                long filesSize = files.ContentLength;


                if ((extension.ToLower() == ".jpg" || extension.ToLower() == ".png" || extension.ToLower() == ".gif" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".bmp") && filesSize <= maxSize)
                {

                    Random rand = new Random();

                    int s = rand.Next(999);

                    //生成随即文件名
                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + rand.Next(999) + extension;

                    #region 图片地址


                    ////获取上传图片源目录
                    //string fileDirectory= System.Configuration.ConfigurationManager.AppSettings["UploadPicture"].ToString();

                    ////缩略图中图片源目录
                    //string fileMiddle = System.Configuration.ConfigurationManager.AppSettings["UploadMiddlePicture"].ToString();

                    ////缩略图小图片源目录
                    //string fileLittle = System.Configuration.ConfigurationManager.AppSettings["UploadLittlePicture"].ToString();

                    #endregion

                    string fileDirectory = System.IO.Path.Combine("../images/sourse/", DateTime.Now.ToString("yyyyMMdd"));

                    string fileMiddle = System.IO.Path.Combine("../images/Middle/", DateTime.Now.ToString("yyyyMMdd"));
                    ;

                    string fileLittle = System.IO.Path.Combine("../images/Little/", DateTime.Now.ToString("yyyyMMdd"));


                    fileDirectory = Page.MapPath(fileDirectory);


                    string savePath = System.IO.Path.Combine(fileDirectory, fileName);

                    if (!System.IO.Directory.Exists(fileDirectory))
                    {

                        System.IO.Directory.CreateDirectory(fileDirectory);

                    }

                    //保存原图片路径
                    files.SaveAs(savePath);

                    System.Text.StringBuilder picJson = new System.Text.StringBuilder();


                    // picInfo.Append(userServices.ILogSharePicture(picJson.ToString()));

                    fileMiddle = Page.MapPath(fileMiddle);
                    if (!System.IO.Directory.Exists(fileMiddle))
                    {
                        System.IO.Directory.CreateDirectory(fileMiddle);
                    }

                    //fileMiddle = Page.MapPath(fileMiddle);
                    fileMiddle += System.IO.Path.Combine("\\", fileName);

                    //中图
                    bool result = ReducedImage(1, savePath, fileMiddle, extension);

                    fileLittle = Page.MapPath(fileLittle);
                    if (!System.IO.Directory.Exists(fileLittle))
                    {
                        System.IO.Directory.CreateDirectory(fileLittle);
                    }

                    //fileLittle = Page.MapPath(fileLittle);
                    fileLittle += System.IO.Path.Combine("\\", fileName);

                    //小图
                    result = ReducedImage(0, savePath, fileLittle, extension);

                    picJson.Append("{");
                    picJson.Append("state:'1'");
                    picJson.Append(",");
                    picJson.Append("guid:'");
                    picJson.AppendFormat("{0}", guid);
                    picJson.Append("'");
                    picJson.Append(",");
                    picJson.Append("picName:'");
                    picJson.AppendFormat("{0}", fileName);
                    picJson.Append("'");
                    picJson.Append(",");
                    picJson.Append("picType:'");
                    picJson.AppendFormat("{0}", result == false ? 0 : 1);
                    picJson.Append("'");
                    picJson.Append("}");

                    //返回图片数据结果
                    picInfo.Append(picJson);


                }
                else
                {

                    picInfo.Append("{");
                    picInfo.Append("\"state\": \"0\"");
                    picInfo.Append("}");

                }

                Response.Write(picInfo);


            }

        }

        #endregion

        #region 图片比例生成


        /// <summary>
        /// 回调函数.by lx on 20120608
        /// </summary>
        /// <returns>true/false</returns>
        public bool ThumbnailCallback()
        {
            return false;
        }


        /// <summary>
        /// 按大小生成缩略图
        /// </summary>
        /// <param name="Width">宽</param>
        /// <param name="Height">高</param>
        /// <param name="targetFilePath">原图片路径</param>
        /// <param name="outPath">缩略图路径</param>
        /// <returns></returns>
        public bool ReducedImage(int type, string targetFilePath, string outPath, string extension)
        {

            int width = 0;
            int height = 0;

            try
            {

                ImageFormat imgFormat = ImageFormat.Jpeg;

                if (extension.ToLower() == ".gif")
                {

                    imgFormat = ImageFormat.Gif;

                }
                else if (extension.ToLower() == ".bmp")
                {

                    imgFormat = ImageFormat.Bmp;

                }
                else if (extension.ToLower() == ".png")
                {

                    imgFormat = ImageFormat.Png;

                }

                //加载图片
                System.Drawing.Image reducedImage = System.Drawing.Image.FromFile(targetFilePath);

                decimal ret = 1;//比例 

                //中图
                if (type == 1)
                {
                    /*
                     * 宽度小于440的，保持原始比例
                     * 宽高相同并且宽度大于400的，宽高都压缩为400
                     * 宽度大于440的，按照宽度比例压缩
                     */
                    if (reducedImage.Width <= 440)//宽度小于440的，保持原始比例
                    {
                        width = reducedImage.Width;
                        height = reducedImage.Height;
                    }
                    else if (reducedImage.Height == reducedImage.Width && reducedImage.Width > 440)//宽高相同并且宽度大于400的，宽高都压缩为400
                    {

                        width = height = 440;
                    }
                    else if (reducedImage.Width > 440)//宽度大于440的
                    {
                        width = 440;

                        ret = Convert.ToDecimal(440) / Convert.ToDecimal(reducedImage.Width);
                        height = Convert.ToInt32(reducedImage.Height * ret);
                    }
                }
                else
                {
                    //小图
                    if (reducedImage.Height < 120 && reducedImage.Width <= 120)
                    {

                        width = reducedImage.Width;
                        height = reducedImage.Height;

                    }
                    else if (reducedImage.Height == reducedImage.Width && reducedImage.Width > 120)
                    {

                        width = height = 120;

                    }
                    else if (reducedImage.Height > 120 || reducedImage.Width > 120)
                    {

                        if (reducedImage.Height > reducedImage.Width)
                        {
                            height = 120;

                            ret = Convert.ToDecimal(120) / Convert.ToDecimal(reducedImage.Height);
                            width = Convert.ToInt32(reducedImage.Width * ret);
                        }
                        else
                        {
                            width = 120;

                            ret = Convert.ToDecimal(120) / Convert.ToDecimal(reducedImage.Width);
                            height = Convert.ToInt32(reducedImage.Height * ret);
                        }

                    }

                }

                System.Drawing.Image.GetThumbnailImageAbort callb = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                reducedImage = reducedImage.GetThumbnailImage(width, height, callb, IntPtr.Zero);
                reducedImage.Save(outPath, imgFormat);
                reducedImage.Dispose();
                return true;

            }
            catch (Exception ex)
            {

                return false;

            }

        }


        /// <summary>
        /// 按百分比  缩小60% Percent为0.6 targetFilePath为目标路径
        /// </summary>
        /// <param name="Percent">百分比</param>
        /// <param name="targetFilePath">原图片地址</param>
        /// <param name="outPath">缩略图保存路径</param>
        /// <returns></returns>
        public bool ReducedImage(double Percent, string targetFilePath, string outPath)
        {

            try
            {

                //加载图片
                System.Drawing.Image imgReducedImage = System.Drawing.Image.FromFile(targetFilePath);
                System.Drawing.Image.GetThumbnailImageAbort callb = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                int ImageWidth = Convert.ToInt32(ResourceImage.Width * Percent);
                int ImageHeight = (ResourceImage.Height) * ImageWidth / ResourceImage.Width;//等比例缩放
                imgReducedImage = ResourceImage.GetThumbnailImage(ImageWidth, ImageHeight, callb, IntPtr.Zero);
                imgReducedImage.Save(outPath, ImageFormat.Jpeg);
                imgReducedImage.Dispose();
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }



        #endregion

    }
}
