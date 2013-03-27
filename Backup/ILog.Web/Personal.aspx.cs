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
    public partial class Personal : BaseWebPage
    {
        
        #region 变量. by lx on 20120709

        /// <summary>
        /// 生成唯一标示(上传图片、短地址用到).by lx on 20120611
        /// </summary>
        public string guid = Guid.NewGuid().ToString();

        /// <summary>
        /// 生成ip.by lx on 20120627
        /// </summary>
        public string ip = Com.ILog.Utils.Utils.GetRealIP();

        
        #endregion

    }
}
