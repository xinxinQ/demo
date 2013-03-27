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

using System.Collections.Generic;

using Com.ILog.Utils;
using System.Text.RegularExpressions;

namespace ILog.Web
{
    public partial class test2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Ilog.BLL.Vip.GetInfoPercent(2372800);

            string returnUrl = Utils.UrlDecode(IMRequest.GetFormString("strURL", false));

            if (returnUrl != "")
            {
                string username = IMRequest.GetFormString("username", true);

                string password = IMRequest.GetFormString("password", true);

                string code = IMRequest.GetFormString("acode", true);

                int urlstate = 0;
                long userid = Ilog.BLL.Vip.GetUserIDByUserName(username, ref urlstate);
                if (userid == 0)
                {
                    ErrorGuide.ErrDirect("用户名不存在或者您的用户名是2月份之后注册的！", returnUrl);
                    return;
                }
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                Response.Cookies.Clear();
                HttpCookie aCookie;
                string cookieName;
                int limit = Request.Cookies.Count;
                for (int i = 0; i < limit; i++)
                {
                    cookieName = Request.Cookies[i].Name;
                    aCookie = new HttpCookie(cookieName);
                    aCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(aCookie);
                }

                //错误信息
                string errorMsg="";

                //错误跳转地址
                string errorUrl="";

                int state= Ilog.BLL.Vip.CheckLogin(username, password,code, ref errorMsg, ref errorUrl);

                if (state==1)
                {
                    if (returnUrl == "http://ig.instrument.com.cn/index.html")
                    {
                        returnUrl = "http://ig.instrument.com.cn/h";
                    }
                    Response.Redirect(returnUrl);
                }
                else
                {
                    if (state==6||state==7||state==8)
                    {
                        ErrorGuide.ErrDirect(errorMsg, errorUrl);
                    }
                    else
                    {
                        ErrorGuide.ErrDirect(errorMsg, "http://ig.instrument.com.cn/index.html");
                    }
                }
                //Response.Write(userid);
                //Response.Write(CurrentCookie.GetCookieByKey("CheckValid"));
            }



            //Ilog.BLL.Vip.CheckLogin("tanghuizhi01", "520584", "", ref errorMsg1, ref errorUrl1);


            //string ss=Request.ServerVariables["HTTP_User_AGENT"];
   
            ////Dictionary<int, string> dicUser = new Dictionary<int, string>();
            ////Ilog.BLL.ILogOriginal.GetLoveItUserName("@胖署条/小窝 请与我联系243433927@。1w是.xx 手机：135@胖署条小窝@ak_倾城", dicUser);
            //string spreadContent = "http://simg.instrument.com.cn/bbs/images/brow/em09502.gif";

            //spreadContent = Ilog.BLL.ILogOriginal.GetIlogContentWithExpression(spreadContent) ;

            //Ilog.BLL.ILogUserFan.GetFansListStrWithTa(2383703);

            //Ilog.BLL.ILogat.GetATPageList(1, 45, "0", 1622715); 

            //Ilog.BLL.ILogSpread.GetAllList2(1, 45, 2383703,1); 

            //Ilog.BLL.ILogSpread.GetSearchAllList(1, 45, 2383703, "圣诞快乐");

            //Ilog.BLL.ILogUserConcern.GetSearchList(1, 45, 2383703, "", "intime");

            //Ilog.BLL.ILogUserConcern.GetSearchList_OtherSearch(1, 45, 1622715, "", 2383703);

            //Ilog.BLL.VipILog.GetFameList(2, 4, 2383703);

            //Ilog.BLL.ILogat.GetATPageList(1, 45, "0", 2383703);

            //Ilog.BLL.ILogUserConcern.GetSearchList_OtherSearch(1, 45, 2383703, "", 2383703);

            //Ilog.BLL.ILogOriginal.GetClearUUBAndChangeExpression("[url=http://4077.cn/ZFJfAba]http://4077.cn/ZFJfAba[/url][img]http://simg.instrument.com.cn/ilog/blue/images/video.jpg[/img] [img=24,24]http://simg.instrument.com.cn/ilog/head/dabing.gif[/img][img=24,24]http://simg.instrument.com.cn/ilog/head/qioudale.gif[/img]ds大水法说的说的的是", 20, "...");

            //Response.Redirect("ajax/AjaxShortToUrl.aspx?url=qAJZR3a");

            //Ilog.BLL.ILogComment.GetContentCommentPageList(1205, 0, 1, 10);

            //Ilog.BLL.ILogUserConcern.GetSearchList_OtherFan(1, 45, 2383703, "", "intime", 2372800);

            //Ilog.BLL.VipILog.GetVipIlogInfoById(2372800);

            //Ilog.BLL.ILogSpread.GetContentInfoById(546);

            //Ilog.BLL.ILogVisithistory.GetVisitedListStr(2372800);

            //Ilog.BLL.ILogComment.GetCommentPageList(1, 45, "1", 2383703);

            //Ilog.BLL.ILogat.GetAtUserListJsonStr("a", 0, 2383703);

            //Ilog.BLL.ILogComment.CommentIlog(938, 1, "回复@〓疯子哥〓:[发呆]三十大板", 229); 

            //Ilog.BLL.ILogOriginal.GetLastestOriginalInfo(2383703);

            //Ilog.BLL.ILogat.GetSearchList(1, 45, "三", "1", 2383703);

            //Ilog.BLL.ILogOriginal.GetHotCommentOriginalListJsonStr();

            //Ilog.BLL.ILogOriginal.GetHotSpreadOriginalListJsonStr();

            //Ilog.BLL.ILogSpread.ReplyIlog(1469, 1, 162715, 162715, "");

            ////string content = "00";
            //////int len = 0;
            //////Ilog.BLL.ILogOriginal.OperateLoveItUrl("00", ref content, ref len);
            //string content = "http://player.youku.com/player.php/sid/XMzY5NTAxMTQ0/v.swf";
            ////string content =Utils.ClearUBB("纯属测试@智慧的弟弟 @胖署条小窝 @ak倾城 @gui_qi 23423@qq.com fdsfsd@fsdf发到.com@发生的发大水> [url=http://www.baidu.com]http://www.instrument.com.cn/qAJZR3 [/url] [url=http://163.comhttp://fdsf.com]http://www.instrument.com.cn/Eb226j [/url]测试 www.baidu.com [url=https://fsdf.com]http://www.instrument.com.cn/YJVFbe [/url] [url=ftp://fdsf.com]http://www.instrument.com.cn/VNVf6f [/url] [url=ftp://fsdfds]http://www.instrument.com.cn/jQzmuy [/url]发到@gui_qi 地");
            //////////int removedLen = 0;
            //Ilog.BLL.ILogOriginal.GetFinalIlogContentShow("[url=http://4077.cn/2mUZ3aa]http://4077.cn/2mUZ3aa[/url][img]http://simg.instrument.com.cn/ilog/blue/images/video.jpg[/img] @ak倾城 分享图片[img=24,24]http://simg.instrument.com.cn/ilog/head/fadai.gif[/img] [img=24,24]http://simg.instrument.com.cn/ilog/head/se.gif[/img] [img=24,24]http://simg.instrument.com.cn/ilog/head/weixiao.gif[/img] [img=24,24]http://simg.instrument.com.cn/ilog/head/pizui.gif[/img] [img=24,24]http://simg.instrument.com.cn/ilog/head/shuijiao.gif[/img] [img=24,24]http://simg.instrument.com.cn/ilog/head/haixiu.gif[/img] xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", 1);

            //////////Ilog.BLL.VipILog.GetIlogContentShow(content, ref content, ref removedLen);
            ////////////处理后的博文内容
            ////////string operatedContent = Com.ILog.Utils.UBBToHtml.UBBToHTML(content);

            //ILog.Model.ILogOriginal ooOriginal = new ILog.Model.ILogOriginal();
            //ooOriginal.cw_type = 0;
            //ooOriginal.intime = DateTime.Now;
            //ooOriginal.io_content = content;
            //ooOriginal.io_haspic = false;
            //ooOriginal.io_ip = Utils.GetRealIP();
            //ooOriginal.userid = 2383703;
            //Ilog.BLL.ILogOriginal.SendOriginal(ooOriginal, "");

            //Ilog.BLL.ILogOriginal.GetOriginalInfoJsonStr(20);

            //Ilog.BLL.ILogSpread.ReplyIlog(488, 0, 2372800, 2471867, "");

            //Ilog.BLL.ILogComment.GetBlogCommentPageList(3, 1, 1, 100);

            //Ilog.BLL.ILogSpread.GetSpreadWindowContent(122);

            // Ilog.BLL.VipILog.GetMemberLevelAndVerifyCommentJsonStr(2372800);

            //UBBToHtml.UBBToHTML("纯属测试@智慧的弟弟 @胖署条小窝 @JW倾城 23423@qq.com fdsfsd@fsdf发到.com@发生的发大水> [url=http://www.baidu.com]http://www.instrument.com.cn/qAJZR3 [/url] [url=http://163.comhttp://fdsf.com]http://www.instrument.com.cn/Eb226j [/url]测试 www.baidu.com [url=https://fsdf.com]http://www.instrument.com.cn/YJVFbe [/url] [url=ftp://fdsf.com]http://www.instrument.com.cn/VNVf6f [/url] [url=ftp://fsdfds]http://www.instrument.com.cn/jQzmuy [/url]发到@gui_qi 地方[img]http://simg.instrument.com.cn/bbs/images/brow/em09512.gif[/img][img]http://simg.instrument.com.cn/bbs/images/brow/em09511.gif[/img][卡卡卡][我擦[我");

            //int removeLen = 0;
            //int usercount = 0;
            //Ilog.BLL.VipILog.GetIlogContentShow(content, ref content, ref removeLen, 598, ref usercount);

            // Response.Write(Server.UrlEncode("转发博文 @胖署条小窝 @ak倾城"));

            //Ilog.BLL.ILogVisithistory.GetVisitListStr(2383703);


        }
    }
}
