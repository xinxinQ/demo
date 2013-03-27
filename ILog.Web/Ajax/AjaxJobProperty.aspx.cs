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
    public partial class AjaxJobProperty : Page//BaseWebPage
    {
        /// <summary>
        /// 单位性质id
        /// </summary>
        protected string vccid = "";

        /// <summary>
        /// 行业性质id
        /// </summary>
        protected string vcfid = "";

        /// <summary>
        /// 职位id
        /// </summary>
        protected string vctid = "";

        /// <summary>
        /// html
        /// </summary>
        protected StringBuilder strbHtml = new StringBuilder();

        /// <summary>
        /// 提示脚本
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
            isGet = IMRequest.IsGet();

            isPost = IMRequest.IsPost();

            if (isGet)
            {
                vccid = IMRequest.GetQueryString("vccid", true);
                vcfid = IMRequest.GetQueryString("vcfid", true);
                vctid = IMRequest.GetQueryString("vctid", true);


                strbHtml.Append("<div id=\"CategorySelector0\">");
                strbHtml.Append("<ul id=\"CategoryLevel0\" style=\"float: left; background-color: #FFFFFF\">");

                int urlstate = 0;

                Dictionary<string, string> VCCIDList = Ilog.BLL.Vip.GetCompanyList(ref urlstate);

                if (VCCIDList.Count > 0)
                {
                    int companycount = 0;

                    foreach (string key in VCCIDList.Keys)
                    {
                        companycount++;

                        string Selected = "";

                        if (key == vccid)
                        {
                            Selected = "class=\"Selected\"";
                            if (companycount>10)
                            {
                                infoScript = "<script type=\"text/javascript\">$(\"#CategoryLevel0\").scrollTop(200);</script>";
                            }
                        }
                        strbHtml.AppendFormat("<li key=\"{0}\" {2}>{1}</li>", key, VCCIDList[key], Selected);
                    }
                }

                strbHtml.Append("</ul>");

                Dictionary<string, string> VCFIDList = Ilog.BLL.Vip.GetFiledList(vccid, ref urlstate);

                if (VCFIDList.Count > 0)
                {
                    strbHtml.Append("<ul id=\"CategoryLevel1\" style=\"float: left; background-color: #FFFFFF;\">");

                    //行业性质个数
                    int fieldcount = 0;

                    foreach (string key in VCFIDList.Keys)
                    {
                        fieldcount++;

                        string Selected = "";

                        if (key == vcfid)
                        {
                            Selected = "class=\"Selected\"";
                            if (fieldcount >= 10 && fieldcount <= 20)
                            {
                                infoScript = "<script type=\"text/javascript\">$(\"#CategoryLevel1\").scrollTop(200);</script>";
                            }
                            else if (fieldcount>20)
                            {
                                infoScript = "<script type=\"text/javascript\">$(\"#CategoryLevel1\").scrollTop(500);</script>";
                            }
                        }
                        strbHtml.AppendFormat("<li key=\"{0}\" {2}>{1}</li>", key, VCFIDList[key], Selected);
                    }

                    strbHtml.Append("</ul>");
                }


                Dictionary<string, string> VCTIDList = Ilog.BLL.Vip.GetTitleList(vccid, ref urlstate);

                if (VCTIDList.Count > 0)
                {
                    strbHtml.Append("<ul id=\"CategoryLevel2\" style=\"float: left; background-color: #FFFFFF;\">");

                    foreach (string key in VCTIDList.Keys)
                    {
                        string Selected = "";

                        if (key == vctid)
                        {
                            Selected = "class=\"Selected\"";
                        }
                        strbHtml.AppendFormat("<li key=\"{0}\" {2}>{1}</li>", key, VCTIDList[key], Selected);
                    }

                    strbHtml.Append("</ul>");
                }

                strbHtml.AppendFormat("<input id=\"VCCID\" name=\"VCCID\" type=\"hidden\" value=\"{0}\" />", vccid);
                strbHtml.AppendFormat("<input id=\"VCFID\" name=\"VCFID\" type=\"hidden\" value=\"{0}\" />", vcfid);
                strbHtml.AppendFormat("<input id=\"VCTID\" name=\"VCTID\" type=\"hidden\" value=\"{0}\" />", vctid);

                strbHtml.Append("<input id=\"VCCName\" name=\"VCCName\" type=\"hidden\" />");
                strbHtml.Append("<input id=\"VCFName\" name=\"VCFName\" type=\"hidden\" />");
                strbHtml.Append("<input id=\"VCTName\" name=\"VCTName\" type=\"hidden\" />");

                strbHtml.Append("</div>");
                strbHtml.Append(" <div class=\"Hr_20\"></div>");
                strbHtml.Append("<div class=\"Tc\">");
                strbHtml.Append("<div class=\"WinBtn\"><span>");
                strbHtml.Append("<input style=\"cursor: pointer\" type=\"submit\" name=\"btnClassSubmit\" class=\"ui_close\" value=\"保存修改\" onclick=\"return submitcheck();\"/></span>");
                strbHtml.Append("</div>");
                strbHtml.Append("</div>");
            }

            else if (isPost)
            {
                long userid = Ilog.BLL.VipILog.GetVIPUserID();

                int urlState = 0;

                vccid = IMRequest.GetFormString("VCCID", true);
                vcfid = IMRequest.GetFormString("VCFID", true);
                vctid = IMRequest.GetFormString("VCTID", true);

                string vccname = IMRequest.GetFormString("VCCName", false);
                string vcfname = IMRequest.GetFormString("VCFName", false);
                string vctname = IMRequest.GetFormString("VCTName", false);

                Model.Vip ooVip = Ilog.BLL.Vip.GetUserInfo(userid, ref urlState);
                ooVip.VCCID = vccid;
                ooVip.VCFID = vcfid;
                ooVip.VCTID = vctid;

                int resultCount = Ilog.BLL.Vip.UpdateVipBaseInfo(ooVip, ref urlState);
                if (resultCount > 0)
                {
                    Response.Write("<script language='JavaScript' type='text/javascript'>window.parent.closeJobDialog(1,'"
                        + vccname + "','" + vcfname + "','" + vctname + "'," + vccid + "," + vcfid + "," + vctid + ");</script>");
                }
            }

        }
    }
}
