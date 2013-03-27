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

using Com.ILog.Utils;
using ILog.UI;

namespace ILog.Web.verify
{
    public partial class secondstep : BaseWebPage
    {
        /// <summary>
        /// 用户id
        /// </summary>
        protected long userid = 0;

        /// <summary>
        /// 身份证号
        /// </summary>
        protected string id_number = "";

        /// <summary>
        /// 认证说明
        /// </summary>
        protected string comment = "";

        /// <summary>
        /// 认证类型
        /// </summary>
        protected int authenticationType = 0;

        /// <summary>
        /// 认证类型名称
        /// </summary>
        protected string authenticationName = "";

        /// <summary>
        /// 认证图片
        /// </summary>
        protected string verifyImg = "";

        /// <summary>
        /// 提示信息
        /// </summary>
        protected string infoScript = "";

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
            isGet = Com.ILog.Utils.IMRequest.IsGet();

            isPost = Com.ILog.Utils.IMRequest.IsPost();

            //用户id
            userid = Ilog.BLL.VipILog.GetVIPUserID();

            int urlState = 0;

            authenticationType = IMRequest.GetFormInt("type", 0);

            if (authenticationType == 0)
            {
                authenticationType = IMRequest.GetQueryInt("type", 1);
            }

            //验证认证申请
            int checkState = Ilog.BLL.ILogAuthenticationHistory.CheckAuthentication(userid, authenticationType);

            if (checkState == 2)
            {
                infoScript = "<script>showTipe('您已经申请过该认证，请等待审核！',0);</script>";
                return;
            }
            else if (checkState == 3)
            {
                infoScript = "<script>showTipe('您已经通过该认证，不可以重复申请！',0);</script>";
                return;
            }
            else if (checkState == 4)
            {
                infoScript = "<script>showTipe('您已经通过名人认证，不可以再申请个人认证！',0);</script>";
                return;
            }
            else if (checkState == 5)
            {
                infoScript = "<script>showTipe('您未满足认证的基本条件！',0);</script>";
                return;
            }

            Model.ILogAuthenticationHistory ooAuthenticationR = Ilog.BLL.ILogAuthenticationHistory.GetAuthenticationHistoryInfoByUserId(userid, 1);
            if (ooAuthenticationR != null)
            {
                id_number = ooAuthenticationR.ia_IDNumber;
                comment = ooAuthenticationR.ia_Comment;
            }

            if (authenticationType == 1)
            {
                verifyImg = "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-7.gif\" alt=\"个人认证\" class=\"ICO L\"/>";
                authenticationName = "个人认证";
            }
            else
            {
                verifyImg = "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-5.gif\" alt=\"名人认证\" class=\"ICO L\"/>";
                authenticationName = "名人认证";
            }


            if (isPost)
            {
                id_number = IMRequest.GetFormString("IDNumber", false);
                comment = IMRequest.GetFormString("comment", false);

                if (id_number == "")
                {
                    infoScript = "<script>showTipe('请输入身份证号码！',0);</script>";
                    return;
                }

                if (comment == "")
                {
                    infoScript = "<script>showTipe('请输入认证说明！',0);</script>";
                    return;
                }

                ///从Request对象中获取上载文件的列表
                HttpFileCollection files = HttpContext.Current.Request.Files;

                ///处理文件列表中的每一个文件
                for (int i = 0; i < files.Count; i++)
                {   ///获取当前上载的文件
                    HttpPostedFile postedFile = files[i];

                    //证件类型
                    int type = 0;

                    //新文件名称
                    string fileName = "";

                    //时间戳
                    string fileTime = DateTime.Now.ToString("yyyyMMddHHmmss");

                    if (i == 0)
                    {
                        //身份证明
                        type = 1;
                        fileName = "card_" + fileTime;
                    }
                    else if (i == 1)
                    {
                        //职位证明
                        type = 2;
                        fileName = "position_" + fileTime;
                    }
                    else
                    {
                        //其他证明
                        type = 3;
                        fileName = "other_" + fileTime;
                    }

                    if (postedFile != null && postedFile.ContentLength != 0)
                    {

                        ///获取文件的类型和大小
                        string filetype = postedFile.ContentType;

                        if (filetype.ToLower() != "image/jpeg" && filetype.ToLower() != "image/pjpeg" && filetype.ToLower() != "image/gif" && filetype.ToLower() != "image/x-png" && filetype.ToLower() != "image/png")
                        {
                            infoScript = "<script>showTipe('只支持jpg、png、gif格式！',0);</script>";
                            return;
                        }

                        int size = postedFile.ContentLength;

                        if (size > 2 * 1024 * 1024)
                        {
                            infoScript = "<script>showTipe('请上传小于2M的文件！',0);</script>";
                            return;
                        }

                        string extension = Path.GetExtension(postedFile.FileName);

                        ///创建保存文件的虚拟路径
                        string url = "../css/" + fileName + extension;
                        ///获取全路径
                        string fullPath = Server.MapPath(url);
                        ///上载文件
                        postedFile.SaveAs(fullPath);

                        long fileid = Ilog.BLL.ILogCertificate.GetIDByUserIDAndType(userid, type, ref urlState);

                        Model.ILogCertificate ooIlogCertificate = new ILog.Model.ILogCertificate();

                        ooIlogCertificate.ic_pic = fileName + extension;

                        if (fileid == 0)
                        {
                            ooIlogCertificate.userid = userid;
                            ooIlogCertificate.ic_type = type;
                            ooIlogCertificate.intime = DateTime.Now;
                            //新增
                            Ilog.BLL.ILogCertificate.AddCertificateInfo(ooIlogCertificate);
                        }
                        else
                        {
                            ooIlogCertificate.ic_id = fileid;
                            Ilog.BLL.ILogCertificate.UpdateCertificate(ooIlogCertificate);
                        }
                    }
                }

                Model.ILogAuthenticationHistory ooHistory = new ILog.Model.ILogAuthenticationHistory();
                ooHistory.userid = userid;
                ooHistory.ia_IDNumber = id_number;
                ooHistory.ia_Comment = comment;
                ooHistory.ia_Type = authenticationType;
                ooHistory.ia_State = 0;
                ooHistory.intime = DateTime.Now;

                Ilog.BLL.ILogAuthenticationHistory.AuthenticationHistoryAdd(ooHistory);

                infoScript = "<script>showTipeWithSeconds('申请认证成功，请等待审核！',1,6);window.setTimeout(function(){gotohome()}, 5000)</script>";

            }

        }
    }
}
