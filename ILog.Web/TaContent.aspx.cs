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

namespace ILog.Web
{
    public partial class TaContent : System.Web.UI.Page
    {

        #region 变量

        /// <summary>
        /// 原创/转发ID.by lx on 20120628
        /// </summary>
        protected int ioId = 0;

        /// <summary>
        /// 获取提交类型默认评论(转发-0 ,评论-1). by lx on 20120714
        /// </summary>
        protected int actionType = 1;

        /// <summary>
        /// 用户编号.by lx on 20120727
        /// </summary>
        protected int userId = 0;

        /// <summary>
        /// 用户昵称
        /// </summary>
        protected string nickname = "";

        #endregion

        #region 接收参数by lx on 2012120727       

        /// <summary>
        /// 接收传递参数.by lx on 2012120727
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            ioId = Com.ILog.Utils.IMRequest.GetQueryInt("ioid", 0);

            actionType = Com.ILog.Utils.IMRequest.GetQueryInt("action", 1);

            userId = Com.ILog.Utils.IMRequest.GetQueryInt("userid");

            nickname = Ilog.BLL.VipILog.GetNickNameByUserId(userId);

        }

        #endregion

    }
}
