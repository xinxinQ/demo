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

using System.Text;

namespace Ilog.WebService
{
    /// <summary>
    /// VipILog 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class VipILog : System.Web.Services.WebService
    {

        #region 搜索（找人）列表
        /// <summary>
        /// 搜索（找人）列表
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页数据条数</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetSrechList(string PageCurrent, string PageSize, string keyword)
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

            if (string.IsNullOrEmpty(keyword))
            {
                return Ilog.BLL.VipILog.GetNotJsonList();
            }

            if (!ILog.Common.Common.ISProcessSqlStr(keyword))
            {
                return Ilog.BLL.VipILog.GetNotJsonList();
            }

            #endregion

            return Ilog.BLL.VipILog.GetSearchList(PageCurrent_, PageSize_, keyword, loUserId);
        }
        #endregion

        #region 智能搜索显示用户下拉列表
        /// <summary>
        /// 功能描述：搜索（找人）列表
        /// 创建标识：ljd 20120617
        /// </summary>
        /// <param name="nickname">用户昵称匹配字</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetAtUserList(string nickname)
        {
            //当前用户id
            long userid = Ilog.BLL.VipILog.GetVIPUserID();
            //@用户列表
            string strUserList = Ilog.BLL.VipILog.GetAtUserListStr(userid, nickname);

            return strUserList;

        }
        #endregion

        #region 得到用户数量属性
        /// <summary>
        /// 功能描述：得到用户数量属性
        /// 创建标识：ljd 20120627
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetIlogCountInfo(long userid)
        {
            string strLogCountInfo = Ilog.BLL.VipILogCount.GetModelJsonCount(userid);

            return strLogCountInfo;

        }
        #endregion

        #region 得到用户的基本信息
        /// <summary>
        /// 功能描述：得到用户的基本信息
        /// 创建标识：ljd 20120627
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetVIPIlogInfo(long userid)
        {
            string strVipInfo = Ilog.BLL.VipILog.GetMemberLevelAndVerifyCommentJsonStr(userid);

            return strVipInfo;

        }
        #endregion

        #region 得到举报用户的基本信息
        /// <summary>
        /// 功能描述：得到用户的基本信息
        /// 创建标识：ljd 20120627
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetVIPIlogReportInfo(long userid)
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




            #endregion
            string strVipInfo = Ilog.BLL.VipILog.GetUserReportJsonStr(userid, loUserId);

            return strVipInfo;

        }
        #endregion

        #region 查看某条记录是否存在（true：1，false：0）前台用户
        /// <summary>
        /// 查看某条记录是否存在（true：1，false：0）前台用户
        /// </summary>
        /// <param name="nickname">用户昵称</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string VipILogExists(string nickname)
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

            #endregion

            return Ilog.BLL.VipILog.VipILogExists(nickname);
        }
        #endregion

        #region 招人（搜搜页面）
        /// <summary>
        /// 招人（搜搜页面）
        /// </summary>
        ///<param name="nickname">昵称</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string GetvipilogByNickName(string nickname)
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
                return Ilog.BLL.VipILog.GetNotJsonList();
            }

            return Ilog.BLL.VipILog.GetvipilogByNickName(nickname);

            #endregion
        }
        #endregion

        #region 得到每日名人榜数据
        /// <summary>
        /// 功能描述：得到每日名人榜数据
        /// 创建标识：ljd 20120705
        /// </summary>   
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string VipIlogGetFamousList()
        {
            string famouslist = Ilog.BLL.VipILog.GetFamousUserListJsonStr();

            return famouslist;

        }
        #endregion

        #region 得到每日草根榜数据
        /// <summary>
        /// 功能描述：得到每日草根榜数据
        /// 创建标识：ljd 20120705
        /// </summary>   
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string VipIlogGetCommonList()
        {
            string commonlist = Ilog.BLL.VipILog.GetCommonUserListJsonStr();

            return commonlist;

        }
        #endregion

        #region 得到最新开通ilog的前9个用户
        /// <summary>
        /// 功能描述：得到最新开通ilog的前9个用户
        /// 创建标识：ljd 20120705
        /// </summary>   
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string VipIlogGetNewList()
        {
            string userlist = Ilog.BLL.VipILog.GetNewUserListJsonStr();

            return userlist;

        }
        #endregion

        #region 得到最新名人认证的前9个用户
        /// <summary>
        /// 功能描述：得到最新名人认证的前9个用户
        /// 创建标识：ljd 20120705
        /// </summary>   
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string VipIlogGetNewFamousList()
        {
            string userlist = Ilog.BLL.VipILog.GetNewFamousUserListJsonStr();

            return userlist;

        }
        #endregion

        #region 清除所有@提醒
        /// <summary>
        /// 功能描述：清除所有@提醒
        /// 创建标识：ljd 20120711
        /// </summary>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ClearAtNum()
        {
            long userid = Ilog.BLL.VipILog.GetVIPUserID();

            string strAtNum = Ilog.BLL.VipILogCount.ClearAtNum(userid);

            return strAtNum;

        }
        #endregion

        #region 验证手机，开通ilog
        /// <summary>
        /// 功能描述：验证手机，开通ilog
        /// 创建标识：ljd 20120806
        /// </summary>
        /// <param name="CheckNumber">验证码</param>
        /// <param name="Mobile">手机号</param>
        /// <returns>提示信息</returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ConfirmOpenIlog(string CheckNumber, string Mobile)
        {
            string infoMsg = "";

            //用户id
            long userid = Ilog.BLL.VipILog.GetVIPUserID();

            int urlstate = 0;

            ILog.Model.Vip ooVip = Ilog.BLL.Vip.GetUserInfo(userid, ref urlstate);

            int intimeUrlState = 0;

            //获得验证码
            ILog.Model.ILogMobileCheck ooMobileCheckEntity = Ilog.BLL.ILogMobileCheck.GetLastestMobileSendTime(userid, Mobile, ref intimeUrlState);

            if (ooMobileCheckEntity.im_checkcode != CheckNumber)
            {
                infoMsg = "{state:0,msg:'手机验证码输入错误！'}";
                return infoMsg;
            }

            //更新vip表中手机状态
            Ilog.BLL.Vip.UpdateVipMobile(userid, Mobile, ref intimeUrlState);

            //用户名
            string username = Ilog.BLL.Vip.GetUserNameByUserID(userid, ref intimeUrlState);

            //添加申请记录
            Ilog.BLL.Vip.VIPActiveSleepingAdd(username, 12, ref intimeUrlState);

            //判断vipilog中是否有数据
            ILog.Model.VipILog ooIlog = Ilog.BLL.VipILog.GetModelByUserID(userid);

            if (ooIlog != null)
            {
                //更新vipilog中的认证状态
                Ilog.BLL.VipILog.UpdateVipIlogMobileState(userid, 1, ref intimeUrlState);
            }
            else
            {
                //站短内容
                StringBuilder strbContent = new StringBuilder();
                strbContent.AppendFormat("亲爱的{0}：<br/>您好！", username);
                strbContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;很高兴的通知您，您的手机认证已通过，您的认证积分奖励已发，请查看积分增减历史：<br/>      &nbsp;&nbsp;");
                strbContent.Append("[url=http://www.instrument.com.cn/vip/Scorelist.asp]积分增减历史[/url]");

                //发送vip站短
                Com.ILog.SendMail.Emails.JYSmtpMailToUser(username, "您的手机认证通过!", strbContent.ToString());

                Ilog.BLL.VipILog.CreateVipIlogAndInit(ooVip, ref urlstate);

            }
            infoMsg = "{state:1,msg:'开通成功！'}";
            return infoMsg;

        }
        #endregion

        #region 得到名人堂用户列表字符串

        /// <summary>
        /// 功能描述：得到名人堂用户列表字符串
        /// 创建标识：ljd 20120821
        /// </summary>   
        /// <param name="PageCurrent"></param>
        /// <param name="PageSize"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string VipIlogGetFameList(string PageCurrent, string PageSize, string keyword)
        {
            int PageCurrent_;
            int PageSize_;

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
                PageSize_ = 20;
            }
            else
            {
                PageSize_ = Convert.ToInt32(PageSize);
            }

            long userid = Ilog.BLL.VipILog.GetVIPUserID();

            string userlist = Ilog.BLL.VipILog.GetFameList(PageCurrent_, PageSize_, userid, keyword);

            return userlist;

        }
        #endregion



    }
}
