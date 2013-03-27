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
    public partial class AjaxCity : BaseWebPage
    {

        /// <summary>
        /// 国家id
        /// </summary>
        protected short countryID = 0;

        /// <summary>
        /// 省份id
        /// </summary>
        protected int provinceID = 0;

        /// <summary>
        /// 城市id
        /// </summary>
        protected int cityid = 0;

        /// <summary>
        /// 页面内容
        /// </summary>
        protected StringBuilder strbHtml = new StringBuilder();

        /// <summary>
        /// 信息提示
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
                countryID = Convert.ToInt16(IMRequest.GetQueryInt("countryid", 0));
                provinceID = IMRequest.GetQueryInt("provinceid", 0);
                cityid = IMRequest.GetQueryInt("cityid", 0);

                strbHtml.Append("<div id=\"CategorySelector0\">");
                strbHtml.Append("<ul id=\"CategoryLevel0\" style=\"float: left; background-color: #FFFFFF\">");

                int urlstate = 0;
                Dictionary<short, string> CountryList = Ilog.BLL.Vip.GetCountryList(ref urlstate);


                if (CountryList.Count > 0)
                {
                    foreach (short key in CountryList.Keys)
                    {
                        string selected = "";

                        if (key == countryID)
                        {
                            selected = "class=\"Selected\"";
                        }
                        strbHtml.AppendFormat("<li key=\"{0}\" {2}>{1}</li>", key, CountryList[key], selected);
                    }
                }

                strbHtml.Append("</ul>");


                strbHtml.Append("<ul id=\"CategoryLevel1\" style=\"float: left; background-color: #FFFFFF; \">");

                Dictionary<int, string> ProvinceList = Ilog.BLL.Vip.GetPorvinceList(countryID, ref urlstate);

                if (ProvinceList.Count > 0)
                {
                    int provcount = 0;

                    foreach (int key in ProvinceList.Keys)
                    {
                        provcount++;

                        string selected = "";

                        if (key == provinceID)
                        {
                            selected = "class=\"Selected\"";

                            if (provcount >= 10 && provcount <= 20)
                            {
                                infoScript = "<script type=\"text/javascript\">$(\"#CategoryLevel1\").scrollTop(200);</script>";
                            }
                            else if (provcount > 20)
                            {
                                infoScript = "<script type=\"text/javascript\">$(\"#CategoryLevel1\").scrollTop(500);</script>";
                            }
                        }
                        strbHtml.AppendFormat("<li key=\"{0}\" {2}>{1}</li>", key, ProvinceList[key], selected);
                    }
                }

                strbHtml.Append("</ul>");


                strbHtml.Append("<ul id=\"CategoryLevel2\" style=\"float: left; background-color: #FFFFFF;\">");

                Dictionary<int, string> CityList = Ilog.BLL.Vip.GetCityList(provinceID, ref urlstate);

                if (CityList.Count > 0)
                {
                    int citycount = 0;

                    foreach (int key in CityList.Keys)
                    {
                        citycount++;

                        string selected = "";

                        if (key == cityid)
                        {
                            selected = "class=\"Selected\"";

                            if (citycount >= 10 && citycount <= 20)
                            {
                                infoScript = "<script type=\"text/javascript\">$(\"#CategoryLevel2\").scrollTop(200);</script>";
                            }
                            else if (citycount > 20)
                            {
                                infoScript = "<script type=\"text/javascript\">$(\"#CategoryLevel2\").scrollTop(500);</script>";
                            }
                        }
                        strbHtml.AppendFormat("<li key=\"{0}\" {2}>{1}</li>", key, CityList[key], selected);
                    }
                }

                strbHtml.Append("</ul>");

                strbHtml.Append("<input id=\"CountryID\" name=\"CountryID\" type=\"hidden\" value=\"\" />");
                strbHtml.Append("<input id=\"ProvinceID\" name=\"ProvinceID\" type=\"hidden\" value=\"\" />");
                strbHtml.Append("<input id=\"CityID\" name=\"CityID\" type=\"hidden\" value=\"\" />");

                strbHtml.Append("<input id=\"CountryName\" name=\"CountryName\" type=\"hidden\" />");
                strbHtml.Append("<input id=\"ProvinceName\" name=\"ProvinceName\" type=\"hidden\"  />");
                strbHtml.Append("<input id=\"CityName\" name=\"CityName\" type=\"hidden\" />");

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

                cityid = IMRequest.GetFormInt("CityID", 0);
                provinceID = IMRequest.GetFormInt("ProvinceID", 0);
                countryID = Convert.ToInt16(IMRequest.GetFormInt("CountryID", 0));

                string CountryName = IMRequest.GetFormString("CountryName", false);
                string ProvinceName = IMRequest.GetFormString("ProvinceName", false);
                string CityName = IMRequest.GetFormString("CityName", false);

                Model.Vip ooVip = Ilog.BLL.Vip.GetUserInfo(userid, ref urlState);
                ooVip.CI_ID = cityid;
                Ilog.BLL.Vip.UpdateVipBaseInfo(ooVip, ref urlState);

                int resultCount = Ilog.BLL.Vip.UpdateVipBaseInfo(ooVip, ref urlState);
                if (resultCount > 0)
                {
                    infoScript = "<script language='JavaScript' type='text/javascript'>window.parent.closeCityDialog(1,'"
                         + CountryName + "','" + ProvinceName + "','" + CityName + "'," + countryID + "," + provinceID + "," + cityid + ");</script>";
                }

            }


        }
    }
}