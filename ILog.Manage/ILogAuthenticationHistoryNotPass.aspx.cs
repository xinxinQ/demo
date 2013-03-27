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

namespace ILog.Manage
{

    /// <summary>
    /// 审核页面.by lx on 20120531
    /// </summary>
    public partial class ILogAuthenticationHistoryNotPass : System.Web.UI.Page
    {

        #region 变量        

        /// <summary>
        /// 变量
        /// </summary>
        public int userId = 0;

        /// <summary>
        /// 流水号
        /// </summary>
        public int aid = 0;

        /// <summary>
        /// 认证类型
        /// </summary>
        public int type = 1;


        #endregion

        #region 审核
        
        /// <summary>
        /// 默认执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            bool isPost = Com.ILog.Utils.IMRequest.IsPost();

            //post提交
            if (isPost)
            {


                userId = Com.ILog.Utils.IMRequest.GetFormInt("number", 0);//用户编号
                aid = Com.ILog.Utils.IMRequest.GetFormInt("aid", 0);//流水编号
                type = Com.ILog.Utils.IMRequest.GetFormInt("type", 1);//用户编号
                string admin = Com.ILog.Utils.CurrentCookie.GetCookieByKey("admin");//审核人   
                int radioCheck = Com.ILog.Utils.IMRequest.GetFormInt("reason", 0);
                

                ILog.Model.ILogAuthenticationHistory authentication = new ILog.Model.ILogAuthenticationHistory();
                bool result = false;
                string message = string.Empty;
                string exitReason = string.Empty;

                if (radioCheck == 5)
                {

                    exitReason = Com.ILog.Utils.IMRequest.GetFormString("exitReason", false);//理由

                }
                else 
                {

                    exitReason = GetRidioHtml(radioCheck);

                }


                //通过
                authentication.userid = Convert.ToInt32(userId);
                authentication.ia_id = aid;
                authentication.ia_State = 2;
                authentication.ia_adminname = admin;
                authentication.ia_checktime = DateTime.Now;
                authentication.ia_reason = exitReason;

                result = Ilog.BLL.ILogAuthenticationHistory.UpdateAuthenticationInfoByUserId(authentication, ref message);


                Response.Write(Com.ILog.Utils.ErrorGuide.ResultspostedRedirect("waitID", message, "certificateHome.aspx?type="+type));
                Response.End();

               // Com.ILog.Utils.ErrorGuide.ResultspostedRedirect("waitID", message, "certificateHome.aspx");
                
            }
            else 
            {

                userId = Com.ILog.Utils.IMRequest.GetQueryInt("userid", 0);//用户编号
                aid = Com.ILog.Utils.IMRequest.GetQueryInt("aid", 0);//流水号
                type = Com.ILog.Utils.IMRequest.GetQueryInt("type", 0);//认证类型


            }

        }


        public static string GetRidioHtml(int id)
        {

            StringBuilder radioHtml = new StringBuilder();

            radioHtml.AppendFormat("{0}", id == 0 ? "基本信息不完整" : id == 1 ? "证件扫描不清晰" : id == 2 ? "真实姓名与身份证不符" : id == 3 ? "职业证明无效" : id == 4 ? "认证说明填写不清晰" : "其它");

            return radioHtml.ToString();
            
        }
        
        #endregion

    }

}
