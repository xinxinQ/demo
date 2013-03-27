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

using ILog.UI;
using Com.ILog.Utils;

namespace ILog.Web.settings
{
    public partial class Email : BaseWebPage
    {
        /// <summary>
        /// email
        /// </summary>
        protected string email = "";

        /// <summary>
        /// 信息提示脚本
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

            if (isPost)
            {
                email = IMRequest.GetFormString("email", false);

                //验证输入
                if (Utils.StrIsNullOrEmpty(email))
                {
                    infoScript = "<script type=\"text/javascript\">showTipe('请输入Email！',0);</script>";
                    return;
                }

                //用户id
                long userid = Ilog.BLL.VipILog.GetVIPUserID();

                int urlstate = 0;

                Model.Vip ooVip = Ilog.BLL.Vip.GetUserInfo(userid, ref urlstate);

                if (ooVip != null)
                {

                    StringBuilder strbContent = new StringBuilder();
                    strbContent.Append("验证方法一：<br/><br/>");
                    strbContent.Append("请点击下面的链接地址验证您的VIP账号：<br/>");
                    strbContent.AppendFormat("http://www.instrument.com.cn/vip/confirm_fromemail.asp?code={0}<br/><br/><br/>", ooVip.MD5Code);
                    strbContent.Append("验证方法二：<br/><br/>");
                    strbContent.Append("请打开下面的网址：<br/>");
                    strbContent.Append("http://www.instrument.com.cn/vip/confirm_index.asp<br/>");
                    strbContent.AppendFormat("在验证码输入框中输入系统给您发送的验证码：{0}", ooVip.MD5Code);

                    Com.ILog.SendMail.Emails.JYSmtpMailToUser(ooVip.email, "仪器信息网VIP用户帐号验证邮件", strbContent.ToString());

                    //结果行数
                    int resultCount = Ilog.BLL.Vip.UpdateVipEmail(userid, email, ref urlstate);

                    if (resultCount == 0)
                    {
                        infoScript = "<script type=\"text/javascript\">showTipe('修改失败！',0);</script>";
                    }
                    else
                    {
                        infoScript = "<script type=\"text/javascript\">showTipeWithWidth('您的Email已经成功修改！您的账号将暂时变成未激活状态。<br/>请根据系统发送的激活Email内的提示激活您的账号',1,450);</script>";
                    }

                }
                else
                {
                    infoScript = "<script type=\"text/javascript\">showTipe('注册资料已经不存在！',0);</script>";
                }

            }

        }
    }
}
