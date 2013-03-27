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
using ILog.Model;
using System.Collections.Generic;
using System.Text;

namespace ILog.Manage
{

    /// <summary>
    /// 认证详情.by lx on 20122529
    /// </summary>
    public partial class certificateDetial : System.Web.UI.Page
    {

        #region 变量

        /// <summary>
        /// 当前位置
        /// </summary>
        public string currentLocationHtml = null;

        /// <summary>
        /// 认证详情
        /// </summary>
        public string certificateDetialHtml = null;
     
        

        #endregion

        #region 默认执行

        /// <summary>
        /// 默认执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            bool isPost = Com.ILog.Utils.IMRequest.IsPost();
            bool isGet = Com.ILog.Utils.IMRequest.IsGet();

            #region get操作

            
            if (isGet)
            {


                int type = Com.ILog.Utils.IMRequest.GetQueryInt("type", 0);

                int userId = Com.ILog.Utils.IMRequest.GetQueryInt("id", 0);

                int aId = Com.ILog.Utils.IMRequest.GetQueryInt("aid", 0);


                //当前位置
                currentLocationHtml = string.Format("<a href=\"certificateHome.aspx?type={0}\">{1}认证</a> &gt;&nbsp;{1}认证详情", type, type == 1 ? "个人" : "名人"); 

                if (userId != 0 && aId != 0)
                {

                    int errorState = 0;

                    //用户基本信息
                    ILog.Model.VipILog vipIlog = Ilog.BLL.VipILog.GetVipIlogEntityById(userId);               

                    //职业信息
                    ILog.Model.Vip vip= Ilog.BLL.Vip.GetUserInfo(userId, ref errorState);

                    //认证历史信息

                    ILog.Model.ILogAuthenticationHistory authentication = Ilog.BLL.ILogAuthenticationHistory.GetAuthenticationHistoryInfoByUserId(userId, type);

                    //查询多条认证记录

                    List<ILogCertificate> certificateList = Ilog.BLL.ILogCertificate.GetCertificateInfoByUserId(userId);

                    //教育信息 
                    List<ILog.Model.ILogSchool> schoolList = Ilog.BLL.ILogSchool.GetSchoolList(userId, ref errorState);

                    certificateDetialHtml = GetcertificateDetialHtml(vipIlog, vip, authentication, certificateList, schoolList);

                }
                else
                {

                    Response.Write("请选择管理的栏目");
                    Response.End();

                }

            }

            #endregion

            #region post操作

            if (isPost)
            {

                ILog.Model.ILogAuthenticationHistory authentication = new ILog.Model.ILogAuthenticationHistory();
                string message = string.Empty;

                int userId = Com.ILog.Utils.IMRequest.GetFormInt("number", 0);//用户编号

                int aId = Com.ILog.Utils.IMRequest.GetFormInt("aid", 0);//自增编号

                int type = Com.ILog.Utils.IMRequest.GetFormInt("type", 1);//自增编号


                string admin = Com.ILog.Utils.CurrentCookie.GetCookieByKey("admin");//审核人 

                //通过
                authentication.userid = Convert.ToInt32(userId);
                authentication.ia_id = aId;
                authentication.ia_State = 1;
                authentication.ia_adminname = admin;
                authentication.ia_checktime = DateTime.Now;
                authentication.ia_reason = string.Empty;

                bool result = Ilog.BLL.ILogAuthenticationHistory.UpdateAuthenticationInfoByUserId(authentication, ref message);

                Com.ILog.Utils.ErrorGuide.ErrDirect(message, "certificateHome.aspx?type="+type); 

                //Response.Write(Com.ILog.Utils.ErrorGuide.ShowRedirect("waitID", message));
                //Response.End();

            }

           


            #endregion


        }

        #endregion

        #region 教育信息

        /// <summary>
        /// 教育信息拼接
        /// </summary>
        /// <param name="schoolList"></param>
        /// <returns></returns>
        public static string GetSchoolHtml(List<ILogSchool> schoolList)
        {

            StringBuilder schoolHtml = new StringBuilder();

            schoolHtml.Append("<tr align=\"left\" class=\"xingmu\">");
            schoolHtml.Append("<td colspan=\"2\" class=\"style1\">");
            schoolHtml.Append("<strong> 教育信息</strong>");
            schoolHtml.Append("</td>");
            schoolHtml.Append("</tr>");
            

            if (schoolList.Count > 0)
            {

                foreach (ILogSchool school in schoolList)
                {

                    schoolHtml.Append("<tr>");
                    schoolHtml.Append("<td width=\"120\" height=\"25\" align=\"right\" class=\"hback\">");
                    schoolHtml.Append(school.is_degreeName);
                    schoolHtml.Append(" </td>");
                    schoolHtml.Append("<td height=\"25\" class=\"hback\">");
                    schoolHtml.Append(school.is_school);
                    schoolHtml.Append("&nbsp;&nbsp;&nbsp;");
                    schoolHtml.Append(school.is_entranceYear);
                    schoolHtml.Append("</td>");
                    schoolHtml.Append(" </tr>");

                }

            }
            else
            {

                schoolHtml.Append("<tr align=\"center\">");
                schoolHtml.Append("<td width=\"120\" height=\"25\" align=\"left\" class=\"hback\" colspan=\"2\">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;暂无</td>");
                schoolHtml.Append("</tr>");

            }

            return schoolHtml.ToString();

        }

        #endregion

        #region 认证信息

        /// <summary>
        ///拼接认证信息
        /// </summary>
        /// <param name="certificateList">集合</param>
        /// <returns></returns>
        public static string GetcertificateHtml(List<ILogCertificate> certificateList)
        {

            StringBuilder certificateHtml = new StringBuilder();

            if (certificateList.Count > 0)
            {

                foreach (ILogCertificate certificate in certificateList)
                {

                    certificateHtml.Append("<tr>");
                    certificateHtml.Append("<td width=\"120\" height=\"25\" align=\"right\" class=\"hback\">");
                    certificateHtml.AppendFormat("{0}证明:", certificate.ic_type == 1 ? "身份" : certificate.ic_type == 2 ? "职位" : "其他");
                    certificateHtml.Append("</td>");
                    certificateHtml.Append("<td height=\"25\" class=\"hback\">");
                    certificateHtml.Append(certificate.ic_pic);
                    certificateHtml.Append("</td>");
                    certificateHtml.Append("</tr>");

                }

            }

            return certificateHtml.ToString();

        }


        #endregion

        #region 详情列表

        /// <summary>
        /// 认证详情
        /// </summary>
        /// <param name="vipIlog">用户信息</param>
        /// <param name="vip">职位信息</param>
        /// <param name="certificateList">证件信息</param>
        /// <param name="schoolList">教育信息</param>
        /// <returns></returns>
        public static string GetcertificateDetialHtml(ILog.Model.VipILog vipIlog, ILog.Model.Vip vip, ILog.Model.ILogAuthenticationHistory authentication, List<ILogCertificate> certificateList, List<ILog.Model.ILogSchool> schoolList)
        {

            StringBuilder result = new StringBuilder();

            result.Append("<table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"1\">");

            result.Append("<tr align=\"left\" class=\"xingmu\">");
            result.Append("<td colspan=\"2\">");
            result.Append("<strong>基本信息</strong>");
            result.Append("</td>");
            result.Append("</tr>");
            result.Append("<tr>");
            result.Append("<td width=\"120\" height=\"25\" align=\"right\" class=\"hback\">");
            result.Append("昵称：");
            result.Append("</td>");
            result.Append("<td height=\"25\" class=\"hback\">");
            result.AppendFormat("{0}", vipIlog == null ? "未知" : vipIlog.nickname);
            result.Append("</td>");
            result.Append("</tr>");
            result.Append("<tr>");
            result.Append("<td width=\"120\" height=\"25\" align=\"right\" class=\"hback\">");
            result.Append("真实姓名：");
            result.Append("</td>");
            result.Append("<td height=\"25\" class=\"hback\">");
            result.Append(vip == null ? "未知" : vip.name);
            result.Append("</td>");
            result.Append("</tr>");
            result.Append("<tr>");
            result.Append("<td width=\"120\" height=\"25\" align=\"right\" class=\"hback\">");
            result.Append("身份证号码：");
            result.Append("</td>");
            result.Append("<td height=\"25\" class=\"hback\">");
            result.Append(authentication == null ? "未知" : authentication.ia_IDNumber);
            result.Append("</td>");
            result.Append("</tr>");
            result.Append("<tr>");
            result.Append("<td width=\"120\" height=\"25\" align=\"right\" class=\"hback\">");
            result.Append("认证说明：");
            result.Append("</td>");
            result.Append("<td height=\"25\" class=\"hback\">");
            result.AppendFormat("<input id=\"number\" name=\"number\" type=\"hidden\"  value=\"{0}\" />", vipIlog.userid);
            result.AppendFormat("<input id=\"aid\" name=\"aid\" type=\"hidden\"  value=\"{0}\" />", authentication == null ? 0 : authentication.ia_id);
            result.AppendFormat("<input id=\"type\" name=\"type\" type=\"hidden\"  value=\"{0}\" />", authentication == null ? 0 : authentication.ia_Type);
            result.AppendFormat("<textarea id=\"content\" readonly=\"readonly\" name=\"content\" rows=\"10\" cols=\"20\" style=\"width: 99%\">{0}</textarea>", authentication == null ? "未知" : authentication.ia_Comment);
            result.Append("</td>");
            result.Append("</tr>");
            result.Append("<tr>");
            result.Append("<td width=\"120\" height=\"25\" align=\"right\" class=\"hback\">");
            result.Append(" 联系手机：");
            result.Append("</td>");
            result.Append("<td height=\"25\" class=\"hback\">");
            result.Append(vip == null ? "未知" : vip.mobile);
            result.Append("</td>");
            result.Append("</tr>");
            result.Append("<tr>");
            result.Append("<td width=\"120\" height=\"25\" align=\"right\" class=\"hback\">");
            result.Append("联系邮箱：");
            result.Append("</td>");
            result.Append("<td height=\"25\" class=\"hback\">");
            result.Append(vip == null ? "未知" : vip.email);
            result.Append("</td>");
            result.Append("</tr>");
            result.Append(GetcertificateHtml(certificateList));
            //result.Append(GetSchoolHtml(schoolList));
            result.Append("<tr align=\"left\" class=\"xingmu\">");
            result.Append("<td colspan=\"2\" class=\"style1\">");
            result.Append("<strong>职业信息</strong>");
            result.Append("</td>");
            result.Append("</tr>");
            result.Append("<tr>");
            result.Append("<td width=\"120\" height=\"25\" align=\"right\" class=\"hback\">");
            result.Append("公司名称：");
            result.Append("</td>");
            result.Append("<td height=\"25\" class=\"hback\">");
            result.Append(vip == null ? "未知" : vip.company);
            result.Append("</td>");
            result.Append("</tr>");
            result.Append("<tr>");
            result.Append("<td width=\"120\" height=\"25\" align=\"right\" class=\"hback\">");
            result.Append("地址：");
            result.Append("</td>");
            result.Append("<td height=\"25\" class=\"hback\">");
            result.Append(vip == null ? "未知" : vip.address);
            result.Append("</td>");
            result.Append("</tr>");

            if (authentication.ia_State==0)
            {            

                result.Append("<tr>");
                result.Append("<td width=\"120\" height=\"25\" align=\"right\" class=\"hback\">");              
                result.Append("<input name=\"Submit\" type=\"submit\" id=\"Submit\" value=\"确认通过\" style=\"cursor: pointer;\" />");
                result.Append("</td>");
                result.Append("<td height=\"25\" class=\"hback\"> &nbsp;&nbsp;&nbsp;");
                result.Append("<input name=\"Submit\" type=\"button\" id=\"btnRefuse\" value=\" 拒绝 \" onclick=\"Returned();\"  style=\"cursor: pointer;\" />");
                //result.Append("<span style=\"padding-left: 12px;color: Red;\" id=\"waitID\"></span>");
                result.Append("</td>");
                result.Append("</tr>");

            }

            result.Append("</table>");

            return result.ToString();

        }


        #endregion

    }
}
