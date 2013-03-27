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
using System.Web.Script.Serialization;
using System.Collections.Generic;


namespace Ilog.WebService
{
    /// <summary>
    /// VipIlog用户服务.by lx on 20120523
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class VipIlogUser : System.Web.Services.WebService
    {

        #region 根据编号获取用户信息


        /// <summary>
        /// 根据编号获取用户信息(home)
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="tranId">流水号</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetUserInfoById(long userid)
        {

            return Ilog.BLL.VipILog.GetVipUserInfoByUserId(userid);
                       

        }

        /// <summary>
        /// 根据编号获取用户信息(home)
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <param name="tranId">流水号</param>
        /// <returns>json字符串</returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetPersonalUserInfoById(long userid)
        {

            return Ilog.BLL.VipILog.GetPersonalUserInfoById(userid);

        }

        #endregion

        #region 根据用户名获取勋章信息

        /// <summary>
        /// 根据用户名获取勋章信息、关注、粉丝、微博数
        /// </summary>
        /// <param name="userName">用户名</param>      
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetVipUserInsigniaByUserName(string userName)
        {

            return Ilog.BLL.VipILog.GetVipUserInsigniaByUserName(userName);
          
        }



        #endregion

        #region 用户信息悬停

        /// <summary>
        /// 根据用户编号查询用户信息
        /// </summary>
        /// <param name="userName">用户名</param>      
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetVipIlogInfoById(long userId)
        {

            return Ilog.BLL.VipILog.GetVipIlogInfoById(userId);

        }



        #endregion

        #region 分享图片

        /// <summary>
        /// 分享图片.by lx on 20120627
        /// </summary>
        /// <param name="guid">唯一</param>
        /// <param name="picName">图片名称</param>
        /// <param name="picType">0:jpg 1:gif</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogSharePicture(string guid, string picName, int picType)
        {

            StringBuilder result = new StringBuilder();

            try
            {

                if (!string.IsNullOrEmpty(guid) && !string.IsNullOrEmpty(picName))
                {

                    ILog.Model.ILogPic model = new ILog.Model.ILogPic();                    
                    model.ip_mark = guid;
                    model.ip_name = picName;
                    model.ip_type = picType;
                    model.intime = DateTime.Now;
                    result.Append(Ilog.BLL.ILogPic.PicAdd(model)); 

                }
                else 
                {

                    result.Append("{");
                    result.Append("state: \"0\"");
                    result.Append("}");

                }        


            }
            catch (Exception)
            {
               
                result.Append("{");
                result.Append("state: \"0\"");
                result.Append("}");

            }

            return result.ToString();

        }

        #endregion        

        #region 短地址生成

        /// <summary>
        /// 短地址生成.by lx on 20120607
        /// </summary>
        /// <param name="screenUrl">原地址</param>
        /// <returns>短地址</returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetShortUrl(string url, int type) 
        {

            string shortUrl = Ilog.BLL.ILogShortUrl.GetTheOneShortInfo(url, type).isu_shorturl;

            StringBuilder result = new StringBuilder();
            result.Append("{");            
            result.AppendFormat("shortUrl:'{0}'", shortUrl);
            result.Append("}");

            return result.ToString();

        }
               

        #endregion

        #region 发表内容

        /// <summary>
        /// 根据用户编号查询用户信息
        /// </summary>
        /// <param name="original">发布内容的json串</param>
        /// <returns></returns>      
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogAddOriginalInfo(long userId, string mark, int isid, string ip, string content, int type)
        {

            StringBuilder result = new StringBuilder();

            try
            {               
               
                int repeat = Ilog.BLL.ILogOriginal.IsPermitSendOriginal(userId, content); 

                //表示内容重复
                if (repeat==0)
                {

                    result.Append("{");
                    result.Append("state:2");
                    result.Append("}");

                }
                else 
                {                 

                    ILog.Model.ILogOriginal originalinfo = new ILog.Model.ILogOriginal();
                    originalinfo.userid = userId;
                    originalinfo.io_content = content;
                    originalinfo.io_haspic = mark == "" ? false : true;
                    originalinfo.io_ip =ip;
                    originalinfo.is_id = Convert.ToInt32(isid);
                    originalinfo.intime =DateTime.Now;
                    originalinfo.cw_type = Convert.ToInt32(type);

                    string sendResult = Ilog.BLL.ILogOriginal.SendOriginal(originalinfo, mark);

                    result.Append(sendResult);


                 }


                
            }
            catch (Exception ex)
            {

                result.Append("{");
                result.AppendFormat("state:0,mess:'{0}'", ex.StackTrace);
                result.Append("}");

            }

            return result.ToString();            
            
        }

        #endregion

        #region 获取@时粉丝列表

        /// <summary>
        /// 获取@时粉丝的列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="nickname">昵称</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetfanInfoByUserId(long userId, string nickname)
        {

            return Ilog.BLL.VipILog.GetfanInfoByUserId(userId, nickname);

        }


        #endregion        

        #region 评论相关

        /// <summary>
        /// 评论文章       
        /// </summary>
        /// <param name="originalID">原创id</param>
        /// <param name="originalUserID">原创用户id</param>
        /// <param name="spreadid">转发id</param>
        /// <param name="spreadUserID">转发用户id</param>
        /// <param name="content">评价内容</param>
        /// <param name="userid">评价人用户id</param>
        /// <returns></returns>     
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogAddCommentInfo(long spreadid, int isoriginal, string content,long commontId) 
        {

            //定义返回结果
            StringBuilder result = new StringBuilder();

            try
            {

                string ret = Ilog.BLL.ILogComment.CommentIlog(spreadid, isoriginal, content, commontId);

                result.Append(ret);
        		
	        }
	        catch (Exception ex)
	        {

                result.Length = 0;
                result.Append("{");
                result.AppendFormat("state:0,mess:'{0}'", ex.StackTrace);
                result.Append("}");
		        
	        }

            return result.ToString();

        }


        /// <summary>
        /// 删除一条评论.by lx on 20120620
        /// <param name="ic_id">流水号</param>
        /// <param name="userid">用户id</param>
        /// </summary>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogCommentDeleteById(long icid, long userid)
        {

            return Ilog.BLL.ILogComment.CommentDel(icid, userid);

        }

        #endregion

        #region 转发相关方法


        /// <summary>
        /// 功能描述：转发文章
        /// 创建标识：ljd 20120610
        /// </summary>
        /// <param name="ilogID">博文id</param>
        /// <param name="type">转发类别 0原创 1转发</param>
        /// <param name="originalID">当前博文userid</param>
        /// <param name="userid">当前转发人userid</param>
        /// <param name="comment">评论内容</param>
        /// <returns></returns>   
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogForwardingCommentInfo(long ilogID, int type, long originalID, long userid, string comment)
        {

            StringBuilder result = new StringBuilder();

            try
            {

                if (ilogID!=0 && originalID != 0 && userid != 0 && !string.IsNullOrEmpty(comment))
                {

                    string ret = Ilog.BLL.ILogSpread.ReplyIlog(ilogID, type, originalID, userid, comment);
                    result.Append(ret);

                }
                else 
                {

                    result.Append("{");
                    result.Append("state:0,mess:'入参有为空的数据。'");
                    result.Append("}");

                }                


            }
            catch (Exception ex)
            {

                result.Length = 0;
                result.Append("{");
                result.AppendFormat("state:0,mess:'{0}'", ex.StackTrace);
                result.Append("}");

            }

            return result.ToString();           

        }
      
         /// <summary>
        /// 用户转发信息
        /// </summary>
        /// <param name="spreadID">转发ID</param>
        /// <returns>结果格式({State:'0/1'})</returns>      
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetSpreadWindowContent(long spreadId) 
        {

            return Ilog.BLL.ILogSpread.GetSpreadWindowContent(spreadId);           

        }

        /// <summary>
        /// 转发数据分页       
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页数据</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="ilogid">博文id</param>
        /// <param name="ilogtype">博文类别 0原创 1转发</param>
        /// <returns></returns>    
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogGetOriginalInfoByUserId(int PageCurrent, int PageSize, long ilogid, int ilogtype)
        {

            return Ilog.BLL.ILogSpread.GetSpreadPageList(PageCurrent, PageSize, ilogid, ilogtype); 

        }


        /// <summary>
        /// 删除一条转发.by lx on 20120620
        /// <param name="is_id">流水号</param>
        /// </summary>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogSpreadDel(long is_id)
        {

            return Ilog.BLL.ILogSpread.SpreadDel(is_id);

        }


        #endregion

        #region 删除原创

        /// <summary>
        /// 删除一条原创.by lx on 20120620
        /// </summary>
        /// <param name="is_id"></param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string ILogOriginalDel(int is_id)
        {

            return Ilog.BLL.ILogOriginal.OriginalDel(is_id);

        }




        #endregion


        #region 获取博文内页单条原创/转发信息

        /// <summary>
        /// 获取博文内页单条原创/转发信息.by lx on 20120716
        /// </summary>
        /// <param name="id">博文编号</param>
        /// <returns></returns>
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        [WebMethod]
        public string IlogGetContentInfoById(long id) 
        {


            return Ilog.BLL.ILogSpread.GetContentInfoById(id);

        }


        #endregion

    }

}
