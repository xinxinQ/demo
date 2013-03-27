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

using System.Text;
using System.Collections.Generic;

using Com.ILog.Utils;
using ILog.UI;


namespace ILog.Web.Ajax
{
    public partial class AjaxSchool : BaseWebPage
    {
        /// <summary>
        /// 学历id
        /// </summary>
        protected int is_Degree = 1;

        /// <summary>
        /// 学校名称
        /// </summary>
        protected string is_School = "";

        /// <summary>
        /// 入学年份
        /// </summary>
        protected int inYear = 0;

        /// <summary>
        /// 学校认证id
        /// </summary>
        protected long is_id = 0;

        /// <summary>
        /// 用户id
        /// </summary>
        protected long userid = 0;

        /// <summary>
        /// 学校id
        /// </summary>
        protected int schoolid = 0;

        /// <summary>
        /// 提示信息
        /// </summary>
        protected string infoScript = "";

        /// <summary>
        /// html内容
        /// </summary>
        protected StringBuilder strbHtml = new StringBuilder();

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
            isGet = IMRequest.IsGet();

            isPost = IMRequest.IsPost();

            userid = Ilog.BLL.VipILog.GetVIPUserID();

            if (isGet)
            {
                int urlState = 0;

                is_id = IMRequest.GetQueryInt("id", 0);

                List<Model.ILogSchool> schoolList = Ilog.BLL.ILogSchool.GetSchoolList(userid, ref urlState);

                if (schoolList.Count == 5 && is_id == 0)
                {
                    infoScript = "<script language='JavaScript' type='text/javascript'>window.parent.closeSchoolDialog(2);</script>";
                    return;
                }

                if (is_id != 0)
                {
                    Model.ILogSchool ooSchool = Ilog.BLL.ILogSchool.GetSchoolInfo(is_id, ref urlState);
                    if (ooSchool != null)
                    {
                        is_Degree = ooSchool.is_degree;
                        inYear = ooSchool.is_entranceYear;
                        is_School = ooSchool.is_school;
                        schoolid = ooSchool.is_schoolid;
                    }
                }

                strbHtml.Append("<ul class=\"ListBD G4\">");
                strbHtml.Append(" <li><span class=\"Span L\">学校类型：<span class=\"Red\">*</span></span>");
                strbHtml.Append("<select name=\"selDegree\" id=\"selDegree\" style=\"width: 100px\">");
                strbHtml.Append(Ilog.BLL.ILogSchool.GetDegreeListStr(is_Degree));
                strbHtml.Append("</select></li>");
                strbHtml.Append("<li><span class=\"Span L\">学校名称：<span class=\"Red\">*</span></span>");
                strbHtml.AppendFormat("<input name=\"SchoolName\" type=\"text\" class=\"input\" size=\"35\"  id=\"SchoolName\" value=\"{0}\" maxlength=\"50\"/></li>", is_School);
                strbHtml.Append("<li><span class=\"Span L\">入学年份：<span class=\"Red\">*</span></span>");

                strbHtml.Append("<select name=\"selYear\" id=\"selYear\" style=\"width: 70px\">");
                strbHtml.Append("<option value=\"0\">--请选择--</option>");
                strbHtml.Append(Ilog.BLL.Vip.GetSelYearListStr(inYear));
                strbHtml.Append("</select>");

                strbHtml.Append("</li></ul>");

                strbHtml.Append("<div class=\"Tc\">");
                strbHtml.AppendFormat("<input id=\"hidID\" name=\"hidID\" type=\"hidden\"  value=\"{0}\"/>", is_id);
                strbHtml.Append(" <div class=\"Hr_20\"></div>");
                strbHtml.Append("<div class=\"Tc\">");
                strbHtml.Append("<div class=\"WinBtn\"><span>");
                strbHtml.Append("<input style=\"cursor: pointer\" type=\"submit\" name=\"btnClassSubmit\" class=\"ui_close\" value=\"保存修改\" onclick=\"return submitcheck();\"/></span>");
                strbHtml.Append("</div>");
                strbHtml.Append("</div>");
            }

            else if (isPost)
            {
                is_id = IMRequest.GetFormInt("hidID", 0);
                is_Degree = IMRequest.GetFormInt("selDegree", 0);
                inYear = IMRequest.GetFormInt("selYear", 0);
                is_School = IMRequest.GetFormString("SchoolName", false);
                schoolid = IMRequest.GetFormInt("schoolid", 0);

                if (is_Degree != 1)
                {
                    schoolid = 0;
                }

                int urlState = 0;

                Model.ILogSchool ooSchool = Ilog.BLL.ILogSchool.GetSchoolInfo(is_id, ref urlState);
                if (is_id == 0 || ooSchool == null)
                {
                    ooSchool = new ILog.Model.ILogSchool();
                    ooSchool.userid = userid;
                    ooSchool.intime = DateTime.Now;
                }
                ooSchool.is_degree = is_Degree;
                ooSchool.is_entranceYear = inYear;
                ooSchool.is_school = is_School;
                ooSchool.is_schoolid = schoolid;

                int resultCount = 0;

                if (ooSchool.is_id == 0)//新增
                {
                    resultCount = Ilog.BLL.ILogSchool.AddSchool(ooSchool, ref urlState);
                }
                else
                {
                    resultCount = Ilog.BLL.ILogSchool.UpdateSchool(ooSchool, ref urlState);
                }

                if (resultCount > 0)
                {
                    infoScript = "<script language='JavaScript' type='text/javascript'>window.parent.closeSchoolDialog(1);</script>";
                }

            }

        }
    }
}
