﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

using System.Web.Script.Services;

namespace Ilog.WebService
{
    /// <summary>
    /// ILogAuthenticationHistory 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class ILogAuthenticationHistory : System.Web.Services.WebService
    {

        /// <summary>
        /// 功能描述：申请认证验证
        /// 创建标识：ljd 20120604
        /// </summary>
        /// <param name="userid">用户id</param>      
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string CheckAuthentication(int type)
        {
            //用户id
            long userid = Ilog.BLL.VipILog.GetVIPUserID();

            return Ilog.BLL.ILogAuthenticationHistory.GetCheckAuthenticationJsonStr(userid,type);

        }

    }
}
