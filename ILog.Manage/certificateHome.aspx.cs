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
using System.Web.UI.MobileControls;
using System.Collections.Generic;
using ILog.Model;
using ILog.BLL;

namespace ILog.Manage
{

    /// <summary>
    /// ilog认证管理.by lx on 20120529
    /// </summary>
    public partial class _certificateHome : System.Web.UI.Page
    {

        #region 变量        


        /// <summary>
        /// 二级导航
        /// </summary>
        public string navigationHtml = null;

        /// <summary>
        /// 当前位置
        /// </summary>
        public string currentLocationHtml = null;

        /// <summary>
        /// 认证列表
        /// </summary>
        public string certificateHtml = null;

        /// <summary>
        /// 页码计算
        /// </summary>
        public string pageHtml = null;

        /// <summary>
        /// 认证类型
        /// </summary>
        public int type=1;

       
        #endregion

        #region 默认执行

        /// <summary>
        /// 默认执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {                        
           
            //认证类型
            type = Com.ILog.Utils.IMRequest.GetQueryInt("type",1);

            //受理状态
            int state =Com.ILog.Utils.IMRequest.GetQueryInt("state",0);
           
            //页码
           int page =Com.ILog.Utils.IMRequest.GetQueryInt("page",1);

            //当前位置
            currentLocationHtml = type == 1 ? "个人认证" : "名人认证";

           
            
            //获取认证信息
            List<ILog.Model.ILogAuthenticationHistory> certificateList = Ilog.BLL.ILogAuthenticationHistory.GetAuthenticationHistoryPageList(page, type, state,ref pageHtml);

            certificateHtml = GetCertificateHtml(type,certificateList);


        }

        #endregion

        #region 页面拼接

        /// <summary>
        /// 认证页面信息拼接
        /// </summary>
        /// <returns></returns>
        public static string GetCertificateHtml(int type, List<ILogAuthenticationHistory> authenticationList) 
        {          

            StringBuilder resultHtml = new StringBuilder();

            ILog.Model.VipILog vipIlog = null;
            ILog.Model.Vip vip = null;

            resultHtml.Append("<tr class=\"xingmu\">");
            resultHtml.Append("<td class=\"xingmu\">昵称</td>");
            resultHtml.Append("<td class=\"xingmu\">真实姓名</td>");
            resultHtml.Append("<td class=\"xingmu\">身份证号码</td>");
            resultHtml.Append("<td class=\"xingmu\">邮箱</td>");
            resultHtml.Append("<td class=\"xingmu\">手机</td>");
            resultHtml.Append("</tr>");

            if (authenticationList.Count > 0)
            {

                foreach (ILogAuthenticationHistory authentication in authenticationList)
                {

                    int errorState = 0;

                    vipIlog=Ilog.BLL.VipILog.GetVipIlogEntityById(authentication.userid);
                    vip = Ilog.BLL.Vip.GetUserInfo(authentication.userid,ref errorState);


                    resultHtml.Append("<tr onmouseover=\"overColor(this)\" onmouseout=\"outColor(this)\">");
                    resultHtml.Append("<td class=\"hback\">");
                    resultHtml.Append(vipIlog==null ? "无" : vipIlog.nickname);
                    resultHtml.Append("</td>");
                    resultHtml.Append("<td class=\"hback\">");
                    resultHtml.Append(vip==null ? "无" : vip.name);
                    resultHtml.Append("</td>");
                    resultHtml.Append("<td class=\"hback\">");
                    resultHtml.Append(authentication.ia_IDNumber);
                    resultHtml.Append("</td>");
                    resultHtml.Append("<td class=\"hback\">");
                    resultHtml.Append(vip == null ? "无" : vip.email);
                    resultHtml.Append("</td>");
                    resultHtml.Append("<td class=\"hback\">");
                    resultHtml.AppendFormat("<a href=\"certificateDetial.aspx?type={0}&id={1}&aid={2}\" title=\"查看\" class=\"menu\"><u>查看</u></a>", type, authentication.userid, authentication.ia_id);
                    resultHtml.Append("</td>");
                    resultHtml.Append("</tr>");

                }

            }
            else
            {

                resultHtml.Append("<tr align=\"center\"><td class=\"hback\" colspan=\"5\">暂无信息！</td></tr>");

            }

            return resultHtml.ToString();

        }


        #endregion

    }
}
