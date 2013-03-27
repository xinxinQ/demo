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

using System.Collections.Generic;
using System.Text;

namespace ILog.Web.Ajax
{
    public partial class AjaxChooseSchool : System.Web.UI.Page
    {
        /// <summary>
        /// 省份下拉列表
        /// </summary>
        protected string selProvince = "";

        protected string strSchoolList = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Dictionary<int, string> dicProvince = Ilog.BLL.Vip.GetProvinceDic();
            StringBuilder strbProvince = new StringBuilder();
            if (dicProvince.Count > 0)
            {
                foreach (int key in dicProvince.Keys)
                {
                    strbProvince.AppendFormat("<option value=\"{0}\">{1}</option>", key, dicProvince[key]);
                }
                selProvince = strbProvince.ToString();
            }

        }
    }
}
