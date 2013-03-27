using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

using System.Web.Script.Services;

using Com.ILog.Utils;

namespace Ilog.WebService
{
    /// <summary>
    /// IlogVisitHistory 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class IlogVisitHistory : System.Web.Services.WebService
    {
        /// <summary>
        /// 功能描述：添加访问历史记录
        /// 创建标识：ljd 20120626
        /// </summary>
        /// <param name="visiteduserid">被访问用户id</param>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public void AddVisitHistory(string visiteduserid)
        {
            //被访问的用户id
            long iv_userid = 0;

            try
            {
                iv_userid = Convert.ToInt64(visiteduserid);

                long userid = Ilog.BLL.VipILog.GetVIPUserID();

                if (userid==0)
                {
                    return;
                }

                ILog.Model.ILogVisithistory ooHistory = new ILog.Model.ILogVisithistory();
                ooHistory.intime = DateTime.Now;
                ooHistory.userid = userid;
                ooHistory.iv_userid = iv_userid;
                if (ooHistory.userid ==ooHistory.iv_userid)
                {
                    return;
                }
                Ilog.BLL.ILogVisithistory.AddVisitHistory(ooHistory);
            }
            catch (Exception)
            {
                iv_userid = 0;
            }

        }

        /// <summary>
        /// 功能描述：得到我看过谁列表
        /// 创建标识：ljd 20120626
        /// </summary>
        /// <param name="visiteduserid">当前用户id</param>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetVisitList()
        {
            //当前用户id
            long userid = Ilog.BLL.VipILog.GetVIPUserID();

            string visitList = Ilog.BLL.ILogVisithistory.GetVisitListStr(userid);

            return visitList;

        }

        /// <summary>
        /// 功能描述：得到谁看过我列表
        /// 创建标识：ljd 20120626
        /// </summary>
        /// <param name="visiteduserid">当前用户id</param>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetVisitedList()
        {
            //当前用户id
            long userid = Ilog.BLL.VipILog.GetVIPUserID();

            string visitList = Ilog.BLL.ILogVisithistory.GetVisitedListStr(userid);

            return visitList;

        }

    }
}
