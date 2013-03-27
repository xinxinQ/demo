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

namespace Ilog.WebService
{
    /// <summary>
    /// VipMail 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class VipMail : System.Web.Services.WebService
    {
        #region 搜索列表
        /// <summary>
        /// 搜索列表
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页多收条数据</param>
        /// <param name="keyword">搜索搜索关键字</param>
        /// <param name="id">收件人id</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetSearchMailList(string PageCurrent, string PageSize, string keyword,string id)
        {
            #region 统一校验

            //是否登陆
            ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            //校验数据类型
            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }
            //非法用户

            long loUserId = Convert.ToInt64(strUserId);

            int state = 0;

            if (Ilog.BLL.Vip.IsExistsVIP(loUserId, ref state) == 0)
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }


            int PageCurrent_;
            int PageSize_;
            long id_;

            if (!ILog.Common.Common.Int_IsType(PageCurrent))
            {
                PageCurrent_ = 1;
            }
            else
            {
                PageCurrent_ = Convert.ToInt32(PageCurrent);
            }
            if (!ILog.Common.Common.Int_IsType(PageSize))
            {
                PageSize_ = 45;
            }
            else
            {
                PageSize_ = Convert.ToInt32(PageSize);
            }
            if (!ILog.Common.Common.Int_IsType(id))
            {
                id_ = 1;
            }
            else
            {
                id_ = Convert.ToInt64(id);
            }
            if (!ILog.Common.Common.ISProcessSqlStr(keyword))
            {
                return ILog.BLL.VipMail.GetNotJsonList();
            }

            #endregion

            return ILog.BLL.VipMail.GetAllMailList(PageCurrent_, PageSize_, keyword, loUserId, 1, id_);
        }
        #endregion

        #region 读取列表
        /// <summary>
        /// 读取列表
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页多收条数据</param>
        /// <param name="keyword">搜索搜索关键字</param>
        /// <param name="ation">操作类型0：读取数据，1,：搜索数据</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetMailList(string PageCurrent, string PageSize, string keyword, string ation)
        {
            #region 统一校验

            //是否登陆
            ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            //校验数据类型
            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }

            if (!ILog.Common.Common.ISProcessSqlStr(keyword))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }

            //非法用户

            long loUserId = Convert.ToInt64(strUserId);

            int state = 0;

            if (Ilog.BLL.Vip.IsExistsVIP(loUserId, ref state) == 0)
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }


            int PageCurrent_;
            int PageSize_;
            int ation_;

            if (!ILog.Common.Common.Int_IsType(PageCurrent))
            {
                PageCurrent_ = 1;
            }
            else
            {
                PageCurrent_ = Convert.ToInt32(PageCurrent);
            }
            if (!ILog.Common.Common.Int_IsType(PageSize))
            {
                PageSize_ = 45;
            }
            else
            {
                PageSize_ = Convert.ToInt32(PageSize);
            }
            if (!ILog.Common.Common.Int_IsType(ation))
            {
                ation_ = 0;
            }
            else
            {
                ation_ = Convert.ToInt32(ation);
            }


            #endregion

            return ILog.BLL.VipMail.GetList(PageCurrent_, PageSize_, keyword, loUserId, ation_);
        }
        #endregion

        #region 读取所有私信列表
        /// <summary>
        /// 读取所有私信列表
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页多收条数据</param>
        /// <param name="keyword">搜索搜索关键字</param>
        /// <param name="id">收件人id</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetAllMailList(string PageCurrent, string PageSize,string id)
        {
            #region 统一校验

            //是否登陆
            ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            //校验数据类型
            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }
            //非法用户

            long loUserId = Convert.ToInt64(strUserId);

            int state = 0;

            if (Ilog.BLL.Vip.IsExistsVIP(loUserId, ref state) == 0)
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }


            int PageCurrent_;
            int PageSize_;
            long id_;

            if (!ILog.Common.Common.Int_IsType(PageCurrent))
            {
                PageCurrent_ = 1;
            }
            else
            {
                PageCurrent_ = Convert.ToInt32(PageCurrent);
            }
            if (!ILog.Common.Common.Int_IsType(PageSize))
            {
                PageSize_ = 45;
            }
            else
            {
                PageSize_ = Convert.ToInt32(PageSize);
            }
            if (!ILog.Common.Common.Int_IsType(id))
            {
                id_ = 1;
            }
            else
            {
                id_ = Convert.ToInt64(id);
            }

            #endregion

            return ILog.BLL.VipMail.GetAllMailList(PageCurrent_, PageSize_, "", loUserId, 0, id_);
        }
        #endregion

        #region 获取回复私信
        /// <summary>
        /// 获取回复私信
        /// </summary>
        /// <param name="mailid">发信id</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetReMailList(string id)
        {
            #region 统一校验

            //是否登陆
            //ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            //校验数据类型
            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }
            //非法用户

            long loUserId = Convert.ToInt64(strUserId);

            int state = 0;

            if (Ilog.BLL.Vip.IsExistsVIP(loUserId, ref state) == 0)
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }

            long id_ = 0;

            if (!ILog.Common.Common.Int_IsType(id))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }
            else
            {
                id_ = Convert.ToInt64(id);
            }

            #endregion

            return ILog.BLL.VipMail.GetReMailJsonList(id_);
        }
        #endregion

        #region 根据id回去收件人昵称
        /// <summary>
        /// 根据id回去收件人昵称
        /// </summary>
        /// <param name="id">流水号</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetTowhoById(string id)
        {
            #region 统一校验

            //是否登陆
            ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            //校验数据类型
            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }
            //非法用户

            long loUserId = Convert.ToInt64(strUserId);

            int state = 0;

            if (Ilog.BLL.Vip.IsExistsVIP(loUserId, ref state) == 0)
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }


            long id_ = 0;

            if (!ILog.Common.Common.Int_IsType(id))
            {
                id_ = 1;
            }
            else
            {
                id_ = Convert.ToInt64(id);
            }

            #endregion

            return ILog.BLL.VipMail.GetTowhoById(id_, loUserId);
        }
        #endregion

        #region 删除私信
        /// <summary>
        /// 删除私信
        /// </summary>
        /// <param name="id">私信流水号</param>
        /// <param name="fromwhoid">发件人id</param>
        /// <param name="towhoid">收件人id</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string VipMailDel(string id, string fromwhoid, string towhoid) 
        {
            #region 统一校验

            //是否登陆
            ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            //校验数据类型
            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }

            if (!ILog.Common.Common.Int_IsType(fromwhoid))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }

            if (!ILog.Common.Common.Int_IsType(towhoid))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }

            //非法用户

            long loUserId = Convert.ToInt64(strUserId);

            int state = 0;

            if (Ilog.BLL.Vip.IsExistsVIP(loUserId, ref state) == 0)
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }


            if (!ILog.Common.Common.ISProcessSqlStr(id))
            {
                return ILog.BLL.VipMail.GetNotJsonList();
            }

            string strUserName = Ilog.BLL.VipILog.GetUserNameByUserId(loUserId);

            return ILog.BLL.VipMail.VipMailDel(id, strUserId, fromwhoid, towhoid);

            #endregion
        }
        #endregion

        #region 发送私信
        /// <summary>
        /// 发送私信
        /// </summary>
        /// <param name="fromwho">私信id</param>
        /// <param name="towho">当前用户名</param>
        /// <param name="content">最后一条信息</param>
        /// <param name="subject">标题</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string SendMail(string towho, string content, string subject)
        {
            #region 统一校验

            //是否登陆
            ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");


            //校验数据类型
            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }
            //非法用户

            long loUserId = Convert.ToInt64(strUserId);

            int state = 0;

            //合法用户（当前）

            if (Ilog.BLL.Vip.IsExistsVIP(loUserId, ref state) == 0)
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }

            //收件人是否合法


            string strUserName = Ilog.BLL.VipILog.GetUserNameByUserId(loUserId);

            //注入校验

            if (!ILog.Common.Common.ISProcessSqlStr(strUserName))
            {
                return ILog.BLL.VipMail.GetNotJsonList();
            }

            if (!ILog.Common.Common.ISProcessSqlStr(towho))
            {
                return ILog.BLL.VipMail.GetNotJsonList();
            }

            if (!ILog.Common.Common.ISProcessSqlStr(content))
            {
                return ILog.BLL.VipMail.GetNotJsonList();
            }

            //数据长度校验
            if (ILog.Common.Common.GetStringLength(towho) > 20)
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }

            if (ILog.Common.Common.GetStringLength(content) > 1000)
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }

            if (ILog.Common.Common.GetStringLength(subject) > 50)
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }

            ILog.Model.VipMail vipmailmodel = new ILog.Model.VipMail();

            vipmailmodel.fromwho = strUserName;
            vipmailmodel.towho = towho;
            vipmailmodel.content = content;
            vipmailmodel.subject = subject;
            vipmailmodel.ip = ILog.Common.Common.GetIP();
            vipmailmodel.userid = loUserId;
            vipmailmodel.towhoid = ILog.BLL.VipMail.GetTowhoIbByTowho(towho);

            return ILog.BLL.VipMail.SendMail(vipmailmodel);

            #endregion
        }
        #endregion

        #region 回复私信
        /// <summary>
        /// 回复私信
        /// </summary>
        /// <param name="fromwho">私信id</param>
        /// <param name="towho">当前用户名</param>
        /// <param name="content">最后一条信息</param>
        /// <param name="subject">标题</param>
        /// <param name="mailid">发信id</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ReplyMail(string towho, string content, string subject)
        {
            #region 统一校验

            //是否登陆
            ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            //校验数据类型
            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }
            //非法用户

            long loUserId = Convert.ToInt64(strUserId);

            int state = 0;

            if (Ilog.BLL.Vip.IsExistsVIP(loUserId, ref state) == 0)
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }

            string strUserName = Ilog.BLL.VipILog.GetUserNameByUserId(loUserId);

            if (!ILog.Common.Common.ISProcessSqlStr(strUserName))
            {
                return ILog.BLL.VipMail.GetNotJsonList();
            }

            if (!ILog.Common.Common.ISProcessSqlStr(towho))
            {
                return ILog.BLL.VipMail.GetNotJsonList();
            }

            if (!ILog.Common.Common.ISProcessSqlStr(content))
            {
                return ILog.BLL.VipMail.GetNotJsonList();
            }

            ILog.Model.VipMail vipmailmodel = new ILog.Model.VipMail();

            vipmailmodel.fromwho = strUserId;
            vipmailmodel.towho = towho;
            vipmailmodel.content = content;
            vipmailmodel.subject = subject;
            vipmailmodel.ip = ILog.Common.Common.GetIP();
            vipmailmodel.userid = loUserId;
            vipmailmodel.towhoid = ILog.BLL.VipMail.GetTowhoIbByTowho(towho);

            return ILog.BLL.VipMail.ReplyMail(vipmailmodel);

            #endregion
        }
        #endregion


        #region 回复私信
        /// <summary>
        /// 回复私信
        /// </summary>
        ///<param name="nickname">昵称</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetNickName_SendMail(string nickname)
        {
            #region 统一校验

            //是否登陆
            ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            //校验数据类型
            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }
            //非法用户

            long loUserId = Convert.ToInt64(strUserId);

            int state = 0;

            if (Ilog.BLL.Vip.IsExistsVIP(loUserId, ref state) == 0)
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }

            if (!ILog.Common.Common.ISProcessSqlStr(nickname))
            {
                return ILog.BLL.VipMail.GetNotJsonList();
            }

            return ILog.BLL.VipMail.GetNickName_SendMail(nickname);

            #endregion
        }
        #endregion


        #region 按昵称搜索站短
        /// <summary>
        /// 按昵称搜索站短
        /// </summary>
        ///<param name="nickname">昵称</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetNickNameByUserID_MailList(string nickname)
        {
            #region 统一校验

            //是否登陆
            ILog.Common.Common.IsLogin();

            string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            //校验数据类型
            if (!ILog.Common.Common.Int_IsType(strUserId))
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }
            //非法用户

            long loUserId = Convert.ToInt64(strUserId);

            int state = 0;

            if (Ilog.BLL.Vip.IsExistsVIP(loUserId, ref state) == 0)
            {
                HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            }

            if (!ILog.Common.Common.ISProcessSqlStr(nickname))
            {
                return ILog.BLL.VipMail.GetNotJsonList();
            }

            return ILog.BLL.VipMail.GetNickNameByUserID_MailList(nickname);

            #endregion
        }
        #endregion
    }
}
