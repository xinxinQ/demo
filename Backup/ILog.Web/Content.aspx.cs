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

using ILog.UI;

namespace ILog.Web
{
    public partial class Content : BaseWebPage
    {

        #region 变量

        /// <summary>
        /// 原创/转发ID.by lx on 20120628
        /// </summary>
        public int ioId = 0; 

        /// <summary>
        /// 获取提交类型默认评论(转发-0 ,评论-1). by lx on 20120714
        /// </summary>
        public int actionType = 1;


        #endregion

        #region 接收参数方法
        
        /// <summary>
        /// 接收参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {


            ioId = Com.ILog.Utils.IMRequest.GetQueryInt("ioid",0);

            actionType = Com.ILog.Utils.IMRequest.GetQueryInt("action",1);

            

        }

        #endregion

    }
}
