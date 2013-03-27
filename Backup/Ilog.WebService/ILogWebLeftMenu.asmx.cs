using System;
using System.Text;
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
    /// ILogWebLeftMenu 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class ILogWebLeftMenu : System.Web.Services.WebService
    {

        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetHomeLeftMneu(int MenuLive)
        {
            StringBuilder sb = new StringBuilder();
            #region 统一校验

            long loUserId = Ilog.BLL.VipILog.GetVIPUserID();

            #endregion

            sb.Append("{leftMenu:");

            try
            {
                sb.Append("[{UrlState:'1'},");

                if (MenuLive == 1)
                {
                    sb.Append("{MenuName:'我的首页',MenuUrl:'/h',MenuIco:'ico1',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'},");
                }
                else
                {
                    sb.Append("{MenuName:'我的首页',MenuUrl:'/h',MenuIco:'ico1',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'},");
                }


                if (MenuLive == 2)
                {
                    sb.Append("{MenuName:'提到我的',MenuUrl:'/It',MenuIco:'ico4',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'},");
                }
                else
                {
                    sb.Append("{MenuName:'提到我的',MenuUrl:'/It',MenuIco:'ico4',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'},");
                }

                if (MenuLive == 3)
                {
                    sb.Append("{MenuName:'我的评论',MenuUrl:'/Comment',MenuIco:'ico3',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'},");
                }
                else
                {
                    sb.Append("{MenuName:'我的评论',MenuUrl:'/Comment',MenuIco:'ico3',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'},");
                }

                if (MenuLive == 4)
                {
                    sb.Append("{MenuName:'我的站短',MenuUrl:'/msg',MenuIco:'ico5',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'}");
                }
                else
                {
                    sb.Append("{MenuName:'我的站短',MenuUrl:'/msg',MenuIco:'ico5',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'}");
                }

            }
            catch
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();

        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetFollowLeftMneu(int MenuLive)
        {
            StringBuilder sb = new StringBuilder();

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




            #endregion

            DataTable dt = Ilog.BLL.VipILogCount.GetModel_UserID(loUserId);

            sb.Append("{leftMenu:");

            try
            {
                sb.Append("[{UrlState:'1'},");


                sb.Append("{MenuName:'关注',MenuUrl:'/Follow.aspx',MenuIco:'ico7',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'},");

                if (MenuLive == 1)
                {
                    sb.Append("{MenuName:'全部关注(" + dt.Rows[0]["vic_concernnum"].ToString() + ")',MenuUrl:'/Follow',MenuIco:'',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'},");
                }
                else
                {
                    sb.Append("{MenuName:'全部关注(" + dt.Rows[0]["vic_concernnum"].ToString() + ")',MenuUrl:'/Follow',MenuIco:'',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'},");
                }

                if (MenuLive == 2)
                {
                    sb.Append("{MenuName:'互相关注(" + dt.Rows[0]["vic_doubleconcernnum"].ToString() + ")',MenuUrl:'/Friends',MenuIco:'',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'},");
                }
                else
                {
                    sb.Append("{MenuName:'互相关注(" + dt.Rows[0]["vic_doubleconcernnum"].ToString() + ")',MenuUrl:'/Friends',MenuIco:'',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'},");
                }

                if (MenuLive == 3)
                {
                    sb.Append("{MenuName:'粉丝(" + dt.Rows[0]["vic_fannum"].ToString() + ")',MenuUrl:'/fans',MenuIco:'ico8',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'},");
                }
                else
                {
                    sb.Append("{MenuName:'粉丝(" + dt.Rows[0]["vic_fannum"].ToString() + ")',MenuUrl:'/fans',MenuIco:'ico8',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'},");
                }

                if (MenuLive == 4)
                {
                    sb.Append("{MenuName:'邀请好友',MenuUrl:'/invite',MenuIco:'ico10',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'}");
                }
                else
                {
                    sb.Append("{MenuName:'邀请好友',MenuUrl:'/invite',MenuIco:'ico10',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'}");
                }

            }
            catch
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();

        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetSettingsLeftMneu(int MenuLive)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("{leftMenu:");

            try
            {
                sb.Append("[{UrlState:'1'},");


                sb.Append("{MenuName:'资料完整度',MenuUrl:'/settings/',MenuIco:'ico6',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'},");

                if (MenuLive == 1)
                {
                    sb.Append("{MenuName:'个人资料',MenuUrl:'/settings/',MenuIco:'',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'},");
                }
                else
                {
                    sb.Append("{MenuName:'个人资料',MenuUrl:'/settings/',MenuIco:'',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'},");
                }

                if (MenuLive == 2)
                {
                    sb.Append("{MenuName:'修改头像',MenuUrl:'/settings/Face',MenuIco:'',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'},");
                }
                else
                {
                    sb.Append("{MenuName:'修改头像',MenuUrl:'/settings/Face',MenuIco:'',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'},");
                }

                if (MenuLive == 3)
                {
                    sb.Append("{MenuName:'修改邮箱',MenuUrl:'/settings/Email',MenuIco:'',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'},");
                }
                else
                {
                    sb.Append("{MenuName:'修改邮箱',MenuUrl:'/settings/Email',MenuIco:'',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'},");
                }

                if (MenuLive == 4)
                {
                    sb.Append("{MenuName:'修改手机',MenuUrl:'/settings/Mobile',MenuIco:'',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'},");
                }
                else
                {
                    sb.Append("{MenuName:'修改手机',MenuUrl:'/settings/Mobile',MenuIco:'',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'},");
                }

                if (MenuLive == 5)
                {
                    sb.Append("{MenuName:'ILOG认证',MenuUrl:'/verify/',MenuIco:'ico12',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'}");
                }
                else
                {
                    sb.Append("{MenuName:'ILOG认证',MenuUrl:'/verify/',MenuIco:'ico12',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'}");
                }


            }
            catch
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();

        }




        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetPersonalLeftMneu(int MenuLive)
        {
            StringBuilder sb = new StringBuilder();


            #region 统一校验

            long loUserId = Ilog.BLL.VipILog.GetVIPUserID();

            #endregion

            DataTable dt = Ilog.BLL.VipILogCount.GetModel_UserID(loUserId);

            sb.Append("{leftMenu:");

            try
            {
                sb.Append("[{UrlState:'1'},");

                if (MenuLive == 1)
                {
                    sb.Append("{MenuName:'我的微博',MenuUrl:'/u',MenuIco:'ico1',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'},");
                }
                else
                {
                    sb.Append("{MenuName:'我的微博',MenuUrl:'/u',MenuIco:'ico1',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'},");
                }


                if (MenuLive == 2)
                {
                    sb.Append("{MenuName:'个人资料',MenuUrl:'/profile',MenuIco:'ico6',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'},");
                }
                else
                {
                    sb.Append("{MenuName:'个人资料',MenuUrl:'/profile',MenuIco:'ico6',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'},");
                }

                if (MenuLive == 3)
                {
                    sb.Append("{MenuName:'我的关注(" + dt.Rows[0]["vic_concernnum"].ToString() + ")',MenuUrl:'/follow',MenuIco:'ico7',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'},");
                }
                else
                {
                    sb.Append("{MenuName:'我的关注(" + dt.Rows[0]["vic_concernnum"].ToString() + ")',MenuUrl:'/follow',MenuIco:'ico7',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'},");
                }

                if (MenuLive == 4)
                {
                    sb.Append("{MenuName:'我的粉丝(" + dt.Rows[0]["vic_fannum"].ToString() + ")',MenuUrl:'/fans',MenuIco:'ico8',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'}");
                }
                else
                {
                    sb.Append("{MenuName:'我的粉丝(" + dt.Rows[0]["vic_fannum"].ToString() + ")',MenuUrl:'/fans',MenuIco:'ico8',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'}");
                }

            }
            catch
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();

        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetHPersonalLeftMneu(int MenuLive, long hUserID)
        {
            #region 统一校验

            ////是否登陆
            //ILog.Common.Common.IsLogin();

            //string strUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            ////校验数据类型
            //if (!ILog.Common.Common.Int_IsType(strUserId))
            //{
            //    HttpContext.Current.Response.Redirect("http://c.instrument.com.cn/art/ilog/404.asp");
            //}
            ////非法用户

            //long loUserId = Convert.ToInt64(strUserId);

            int urlstate = 0;

            #endregion

            DataTable dt = Ilog.BLL.VipILogCount.GetModel_UserID(hUserID);

            ILog.Model.Vip vipModel = Ilog.BLL.Vip.GetUserInfo(hUserID, ref urlstate);

            StringBuilder sb = new StringBuilder();

            sb.Append("{leftMenu:");

            try
            {
                sb.Append("[{UrlState:'1'},");

                if (string.IsNullOrEmpty(vipModel.sex) || vipModel.sex == "male")
                {
                    if (MenuLive == 1)
                    {
                        sb.Append("{MenuName:'他的微博',MenuUrl:'/u_" + hUserID + "',MenuIco:'ico1',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'},");
                    }
                    else
                    {
                        sb.Append("{MenuName:'他的微博',MenuUrl:'/u_" + hUserID + "',MenuIco:'ico1',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'},");
                    }


                    if (MenuLive == 2)
                    {
                        sb.Append("{MenuName:'个人资料',MenuUrl:'/profile_" + hUserID + "',MenuIco:'ico6',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'},");
                    }
                    else
                    {
                        sb.Append("{MenuName:'个人资料',MenuUrl:'/profile_" + hUserID + "',MenuIco:'ico6',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'},");
                    }

                    if (MenuLive == 3)
                    {
                        sb.Append("{MenuName:'他的关注(" + dt.Rows[0]["vic_concernnum"].ToString() + ")',MenuUrl:'/follow_" + hUserID + "',MenuIco:'ico7',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'},");
                    }
                    else
                    {
                        sb.Append("{MenuName:'他的关注(" + dt.Rows[0]["vic_concernnum"].ToString() + ")',MenuUrl:'/follow_" + hUserID + "',MenuIco:'ico7',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'},");
                    }

                    if (MenuLive == 4)
                    {
                        sb.Append("{MenuName:'他的粉丝(" + dt.Rows[0]["vic_fannum"].ToString() + ")',MenuUrl:'/fans_" + hUserID + "',MenuIco:'ico8',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'}");
                    }
                    else
                    {
                        sb.Append("{MenuName:'他的粉丝(" + dt.Rows[0]["vic_fannum"].ToString() + ")',MenuUrl:'/fans_" + hUserID + "',MenuIco:'ico8',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'}");
                    }
                }
                else
                {

                    if (MenuLive == 1)
                    {
                        sb.Append("{MenuName:'她的微博',MenuUrl:'/u_" + hUserID + "',MenuIco:'ico1',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'},");
                    }
                    else
                    {
                        sb.Append("{MenuName:'她的微博',MenuUrl:'/u_" + hUserID + "',MenuIco:'ico1',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'},");
                    }


                    if (MenuLive == 2)
                    {
                        sb.Append("{MenuName:'个人资料',MenuUrl:'/profile_" + hUserID + "',MenuIco:'ico6',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'},");
                    }
                    else
                    {
                        sb.Append("{MenuName:'个人资料',MenuUrl:'/profile_" + hUserID + "',MenuIco:'ico6',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'},");
                    }

                    if (MenuLive == 3)
                    {
                        sb.Append("{MenuName:'她的关注(" + dt.Rows[0]["vic_concernnum"].ToString() + ")',MenuUrl:'/follow_" + hUserID + "',MenuIco:'ico7',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'},");
                    }
                    else
                    {
                        sb.Append("{MenuName:'她的关注(" + dt.Rows[0]["vic_concernnum"].ToString() + ")',MenuUrl:'/follow_" + hUserID + "',MenuIco:'ico7',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'},");
                    }

                    if (MenuLive == 4)
                    {
                        sb.Append("{MenuName:'她的粉丝(" + dt.Rows[0]["vic_fannum"].ToString() + ")',MenuUrl:'/fans_" + hUserID + "',MenuIco:'ico8',MenuLive:'Class=\"liv\"',MenuHrefLive:'Class=\"White F14\"'}");
                    }
                    else
                    {
                        sb.Append("{MenuName:'她的粉丝(" + dt.Rows[0]["vic_fannum"].ToString() + ")',MenuUrl:'/fans_" + hUserID + "',MenuIco:'ico8',MenuLive:'',MenuHrefLive:'Class=\"Blue F14\"'}");
                    }
                }

            }
            catch
            {
                sb.Append("[{UrlState:'0'}");
            }

            sb.Append("]}");

            return sb.ToString();

        }
    }
}
