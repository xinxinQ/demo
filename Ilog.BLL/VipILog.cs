using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using System.Web;
using System.Text.RegularExpressions;
using System.Threading;

using Com.ILog.Utils;
using Com.IM.Utils;

namespace Ilog.BLL
{
    public class VipILog
    {
        #region  增加一条用户数据
        /// <summary>
        /// 增加一条用户数据
        /// <param name="model">用户信息表实体</param>
        /// </summary>
        public static string VipILogAdd(ILog.Model.VipILog model)
        {
            StringBuilder strVipILogAdd = new StringBuilder();

            strVipILogAdd.Append("var CertificateInfoAddJsonObject = ");
            strVipILogAdd.Append("({");
            strVipILogAdd.Append("\"state\": \"" + ILog.DAL.VipILog.VipILogAdd(model).ToString() + "\"");
            strVipILogAdd.Append("})");

            return strVipILogAdd.ToString();
        }
        #endregion

        #region 更新一条用户数据
        /// <summary>
        /// 更新一条用户数据
        /// <param name="model">用户信息表实体</param>
        /// </summary>
        public static string VipILogUpdate(ILog.Model.VipILog model)
        {
            StringBuilder strCertificateUpdate = new StringBuilder();

            strCertificateUpdate.Append("var CertificateUpdateJsonObject = ");
            strCertificateUpdate.Append("({");
            strCertificateUpdate.Append("\"state\": \"" + ILog.DAL.VipILog.VipILogUpdate(model).ToString() + "\"");
            strCertificateUpdate.Append("})");

            return strCertificateUpdate.ToString();
        }
        #endregion

        #region 删除一条认证数据
        /// <summary>
        /// 删除一条认证数据
        /// <param name="vi_id">流水号</param>
        /// <param name="userid">用户id</param>
        /// </summary>
        public static string VipILogDel(int userid, int vi_id)
        {
            StringBuilder strVipILogDel = new StringBuilder();

            strVipILogDel.Append("var CertificateDelJsonObject = ");
            strVipILogDel.Append("({");
            strVipILogDel.Append("\"state\": \"" + ILog.DAL.VipILog.VipILogDel(userid, vi_id).ToString() + "\"");
            strVipILogDel.Append("})");

            return strVipILogDel.ToString();
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="vi_id">流水号</param>
        /// <param name="userid">用户id</param>
        /// </summary>
        public static string GetModel(long userid)
        {
            DataTable dblModelList = ILog.DAL.VipILog.GetModel(userid);

            //构建josn字符串 
            string ILogAtModelJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblModelList).ToString();

            return ILogAtModelJosn;
        }
        #endregion

        #region  根据用户编号获取用户信息昵称、头像


        /// <summary>
        /// 根据用户编号获取用户昵称、头像.by lx on 20120522
        /// </summary>
        /// <param name="userId">用户表编号</param>
        /// <param name="tranId">流水号</param>
        /// <returns></returns>
        public static string GetVipUserInfoByUserId(long userId)
        {

            StringBuilder result = new StringBuilder();

            result.Append("{");

            try
            {
                int urlstate = 0;

                //用户基本信息
                ILog.Model.VipILog vipIlog = Ilog.BLL.VipILog.GetModelByUserID(userId);

                //职业信息
                ILog.Model.Vip ooVip = Ilog.BLL.Vip.GetUserInfo(userId, ref urlstate);

                if (ooVip != null)
                {

                    //勋章信息
                    string userInsignia = ILog.DAL.VipILog.GetVipUserInsigniaByUserName(ooVip.username);


                    DataTable dt = ILog.DAL.VipILogCount.GetModel(userId);


                    //粉丝
                    int fan = 0;
                    //关注
                    int userconcern = 0;

                    //微博
                    int weibo = 0;

                    if (dt.Rows.Count != 0)
                    {

                        fan = Convert.ToInt32(dt.Rows[0]["vic_fannum"]);

                        userconcern = Convert.ToInt32(dt.Rows[0]["vic_concernnum"]);

                        weibo = Convert.ToInt32(dt.Rows[0]["vic_ilognum"]);

                    }

                    //资料完整度
                    int integrity = Vip.GetInfoPercent(userId);

                    result.Append("UrlState:'1',");

                    result.Append("nickname:'");
                    result.AppendFormat("{0}", vipIlog.nickname);
                    result.Append("'");
                    result.Append(",");

                    result.Append("sex:'");
                    result.AppendFormat("{0}", ooVip.sex);
                    result.Append("'");
                    result.Append(",");

                    result.Append("face:'");
                    result.AppendFormat("{0}", vipIlog.face);
                    result.Append("'");
                    result.Append(",");

                    result.Append("integrity:'");
                    result.AppendFormat("{0}", integrity);
                    result.Append("'");
                    result.Append(",");

                    result.Append("fan:'");
                    result.AppendFormat("{0}", fan);
                    result.Append("'");
                    result.Append(",");

                    result.Append("concern:'");
                    result.AppendFormat("{0}", userconcern);
                    result.Append("'");
                    result.Append(",");

                    result.Append("blog:'");
                    result.AppendFormat("{0}", weibo);
                    result.Append("'");
                    result.Append(",");

                    result.Append("Insignia:'");
                    result.AppendFormat("{0}", userInsignia);
                    result.Append("'");




                }
                else
                {

                    result.Append("UrlState:'0'");

                }


            }
            catch
            {

                result.Length = 0;
                result.Append("{");
                result.Append("UrlState:'0'");

            }

            result.Append("}");

            return result.ToString();

        }


        /// <summary>
        /// 根据用户编号他人/自己个人主页信息.by lx on 201206111
        /// </summary>
        /// <param name="userId">用户表编号</param>
        /// <param name="tranId">流水号</param>
        /// <returns></returns>
        public static string GetPersonalUserInfoById(long userId)
        {

            StringBuilder result = new StringBuilder();

            result.Append("{");

            try
            {
                int errorState = 0;
                //用户基本信息
                ILog.Model.VipILog vipIlog = Ilog.BLL.VipILog.GetModelByUserID(userId);

                //职业信息
                ILog.Model.Vip vip = Ilog.BLL.Vip.GetUserInfo(userId, ref errorState);

                //教育信息 
                List<ILog.Model.ILogSchool> schoolList = Ilog.BLL.ILogSchool.GetSchoolList(userId, ref errorState);

                Dictionary<string, string> DicRegion = Ilog.BLL.Vip.GetVIPRegion(vip.CI_ID, ref errorState);

                if (vipIlog != null)
                {
                    result.Append("UrlState:'1',");

                    result.Append("nickname:'");
                    result.AppendFormat("{0}", vipIlog.nickname);
                    result.Append("'");
                    result.Append(",");

                    //性别.by lx on 20120730
                    result.Append("sex:'");
                    result.AppendFormat("{0}", vip.sex);
                    result.Append("'");
                    result.Append(",");

                    result.Append("face:'");
                    result.AppendFormat("{0}", vipIlog.face);
                    result.Append("'");
                    result.Append(",");
                    result.Append("memberlevel:'");
                    result.AppendFormat("{0}", vipIlog.vi_memberlevel);
                    result.Append("'");
                    result.Append(",");
                    result.Append("company:'");
                    result.AppendFormat("{0}", vip == null ? "未填写" : vip.company);
                    result.Append("'");
                    result.Append(",");
                    result.Append("address:'");

                    result.AppendFormat("{0}", DicRegion["Province"] == "" ? "未填写" : DicRegion["Province"] + "  " + DicRegion["City"]);

                    result.Append("'");
                    result.Append(",");
                    result.Append("school:'");
                    result.AppendFormat("{0}", schoolList.Count == 0 ? "未填写" : schoolList[0].is_school);
                    result.Append("'");


                }


            }
            catch (Exception ex)
            {

                result.Length = 0;
                result.Append("{");
                result.Append("UrlState:'0'");

            }

            result.Append("}");


            return result.ToString();


        }

        #endregion

        #region 根据用户名获取用户勋章

        /// <summary>
        /// 根据用户名获取用户勋章.by lx on 20120523 
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static string GetVipUserInsigniaByUserName(string userName)
        {

            StringBuilder result = new StringBuilder();

            //勋章信息
            string userInsignia = ILog.DAL.VipILog.GetVipUserInsigniaByUserName(userName);


            result.Append("{userInfo:");

            try
            {
                result.Append("[{UrlState:'1'},");
                result.Append("{");
                result.Append("Insignia:'");
                result.AppendFormat("{0}", userInsignia);
                result.Append("'");
                result.Append("}");

            }
            catch
            {
                result.Length = 0;
                result.Append("[{UrlState:'0'}");
            }

            result.Append("]}");

            return result.ToString();

        }

        #endregion

        #region 根据用户编号获取用户信息

        /// <summary>
        /// 根据用户编号获取用户信息.by lx on 20120523 
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static string GetVipIlogInfoById(long userId)
        {

            StringBuilder result = new StringBuilder();
            int jsonNum = 0;

            //判断是否存在
            int vipState = ILog.DAL.Vip.IsExistsVIP(userId, ref jsonNum);

            result.Append("{");

            try
            {

                //获取性别.by lx on 20120703
                ILog.Model.Vip vipSex = ILog.DAL.Vip.GetUserInfo(userId, ref jsonNum);

                string sex = vipSex == null ? "male" : vipSex.sex;

                //存在
                if (vipState == 1)
                {

                    //判断是否开通Ilog
                    int IsOpen = ILog.DAL.VipILog.CheckIlogOpen(userId);

                    //获取用户信息
                    ILog.Model.VipILog userInfo = ILog.DAL.VipILog.GetVipIlogInfoById(userId);


                    //开通
                    if (IsOpen == 1 || IsOpen == 0)
                    {

                        #region 开通

                        long currentuserid = Ilog.BLL.VipILog.GetVIPUserID();

                        ILog.Model.ILogUserConcern userConcern = ILog.DAL.ILogUserConcern.GetIlogUserconcernInfoByUserId(userInfo.userid, currentuserid);

                        ILog.Model.ILogOriginal originalInfo = Ilog.BLL.ILogOriginal.GetLastestOriginalInfo(userId);

                        string comment = originalInfo == null ? "暂无信息......" : originalInfo.io_content == null ? "暂无信息......" : Com.ILog.Utils.Utils.GetSubString(originalInfo.io_content, 70);


                        DataTable dt = ILog.DAL.VipILogCount.GetModel(userId);

                        //粉丝
                        int fan = 0;
                        //关注
                        int userconcern = 0;

                        //微博
                        int weibo = 0;

                        if (dt.Rows.Count != 0)
                        {

                            fan = Convert.ToInt32(dt.Rows[0]["vic_fannum"]);

                            userconcern = Convert.ToInt32(dt.Rows[0]["vic_concernnum"]);

                            weibo = Convert.ToInt32(dt.Rows[0]["vic_ilognum"]);

                        }

                        //用户信息
                        if (userInfo != null)
                        {

                            result.Append("UrlState:'1',");

                            result.Append("id:'");
                            result.AppendFormat("{0}", userInfo.vi_id);
                            result.Append("'");
                            result.Append(",");

                            result.Append("nickname:'");
                            result.AppendFormat("{0}", userInfo.nickname);
                            result.Append("'");
                            result.Append(",");

                            result.Append("sex:'");
                            result.AppendFormat("{0}", sex);
                            result.Append("'");
                            result.Append(",");

                            result.Append("memberlevel:'");
                            result.AppendFormat("{0}", userInfo.vi_memberlevel);
                            result.Append("'");
                            result.Append(",");

                            result.Append("face:'");
                            result.AppendFormat("{0}", userInfo.face);
                            result.Append("'");
                            result.Append(",");

                            result.Append("fan:'");
                            result.AppendFormat("{0}", fan);
                            result.Append("'");
                            result.Append(",");

                            result.Append("concern:'");
                            result.AppendFormat("{0}", userconcern);
                            result.Append("'");
                            result.Append(",");
                            result.Append("concernState:'");
                            result.AppendFormat("{0}", userConcern == null ? 2 : Convert.ToInt32(userConcern.iuc_state));//0:已关注 1：互相关注 2：未关注
                            result.Append("'");
                            result.Append(",");

                            result.Append("blog:'");
                            result.AppendFormat("{0}", weibo);
                            result.Append("'");
                            result.Append(",");

                            result.Append("remark:'");
                            result.AppendFormat("{0}", comment);
                            result.Append("'");
                            result.Append(",");

                            result.Append("state:'");
                            result.AppendFormat("{0}", IsOpen);
                            result.Append("'");


                        }
                        else
                        {
                            result.Append("UrlState:'0'");
                        }

                        #endregion

                    }
                    else
                    {


                        #region 未开通


                        //未开通(-1,2)把用户名和头像取出来
                        ILog.Model.Vip vipInfo = ILog.DAL.Vip.GetUserFace(userId, ref jsonNum);

                        if (vipInfo != null)
                        {

                            result.Append("UrlState:'1',");
                            result.Append("nickname:'");
                            result.AppendFormat("{0}", vipInfo.nickname);
                            result.Append("'");
                            result.Append(",");

                            result.Append("sex:'");
                            result.AppendFormat("{0}", sex);
                            result.Append("'");
                            result.Append(",");

                            result.Append("memberlevel:'");
                            result.AppendFormat("{0}", userInfo.vi_memberlevel);
                            result.Append("'");
                            result.Append(",");

                            result.Append("face:'");
                            result.AppendFormat("{0}", vipInfo.face);
                            result.Append("'");
                            result.Append(",");
                            result.Append("state:'");
                            result.AppendFormat("{0}", IsOpen);
                            result.Append("'");

                        }

                        #endregion

                    }

                }
                else
                {

                    //不存在
                    result.Append("UrlState:'0'");


                }

            }
            catch
            {

                result.Length = 0;
                result.Append("{UrlState:'0'");

            }

            result.Append("}");

            return result.ToString();

        }

        /// <summary>
        /// 根据用户编号获取用户信息实体
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public static ILog.Model.VipILog GetVipIlogEntityById(long userId)
        {

            return ILog.DAL.VipILog.GetVipIlogInfoById(userId);

        }


        #endregion

        #region 根据用户编号获取前10位粉丝的信息（@时用到）

        /// <summary>
        /// 根据用户编号获取前10位粉丝的信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public static string GetfanInfoByUserId(long userId, string nickname)
        {

            StringBuilder result = new StringBuilder();

            List<ILog.Model.VipILog> fanList = ILog.DAL.VipILog.GetfanInfoByUserId(userId, nickname);

            result.Append("{fans:");

            if (fanList != null && fanList.Count > 0)
            {

                result.Append("[{UrlState:'1'}");

                foreach (ILog.Model.VipILog vipInfo in fanList)
                {

                    result.Append(",");
                    result.Append("{nickname:'");
                    result.AppendFormat("{0}", vipInfo.nickname);
                    result.Append("'}");

                }

            }
            else
            {

                result.Append("[{UrlState:'0'}");

            }

            result.Append("]}");

            return result.ToString();


        }


        #endregion

        #region 搜索列表（已开通的）
        /// <summary>
        /// 搜索列表（已开通的）
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>和
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="keyword">搜索关键字</param>
        /// <param name="userid">当前用户id</param>
        /// <returns></returns>
        public static string GetSearchList(int PageCurrent, int PageSize, string keyword, long userid)
        {
            string strTableName = " vip_ilog ";
            string strFieldKey = "vi_id";
            string strFieldShow = " vi_id,userid,nickname,username,face,ci_id,vi_memberlevel";
            string strFieldOrder = " vi_id desc ";
            string strWhere = " vi_state = 1 and nickname  like'%" + keyword + "%'";

            int RecordCount = 0;

            DataTable dblSearchList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            return GetJsonList(dblSearchList, RecordCount, userid);
        }
        #endregion

        #region 构建json
        /// <summary>
        /// 构建json
        /// </summary>
        /// <param name="List">数据</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="userid">当前用户id</param>
        /// <returns></returns>
        private static string GetJsonList(DataTable List, int RecordCount, long userid)
        {
            int count = List.Rows.Count;

            int RowsCount = 45 * RecordCount;

            StringBuilder strPageList = new StringBuilder();

            strPageList.Append("{List:");

            try
            {
                if (count > 0)
                {
                    strPageList.Append("[{State:'1'}");


                    strPageList.Append(",{RecordCount:'" + (RecordCount > 10 ? 10 : RecordCount) + "'},");  //所有列表页最多显示10页


                    int jsonNum = 0;

                    //用户性别
                    ILog.Model.Vip vipInfo;

                    for (int i = 0; i < count; i++)
                    {
                        strPageList.Append("{isfollow:\"" + Ilog.BLL.ILogUserConcern.UserConcernOnly_State(userid, Convert.ToInt64(List.Rows[i]["userid"])).ToString() + "\",");

                        //是否双项关注：true关注
                        strPageList.Append("isuserconcernstate:\"" + ILog.DAL.ILogUserConcern.UserConcern_State(userid, Convert.ToInt64(List.Rows[i]["userid"])) + "\",");

                        //获取用户基本信息
                        vipInfo = ILog.DAL.Vip.GetUserInfo(Convert.ToInt64(List.Rows[i]["userid"]), ref jsonNum);
                        //获得用户最新博文信息
                        ILog.Model.ILogOriginal ooOriginal = Ilog.BLL.ILogOriginal.GetLastestOriginalInfo(Convert.ToInt64(List.Rows[i]["userid"]));

                        if (ooOriginal != null)
                        {
                            strPageList.Append("is_id:\"" + ooOriginal.io_id + "\",intime:\"" + ILog.Common.Common.GetIlogTime(ooOriginal.intime) + "\",");
                            strPageList.Append("is_content:\"" + ILog.Common.Common.GetJScriptGlobalObjectEscape(ooOriginal.io_content) + "\",");
                        }
                        else
                        {
                            strPageList.Append("is_id:0,");
                        }

                        Dictionary<string, string> dicRegion = Ilog.BLL.Vip.GetVIPRegion(Convert.ToInt32(List.Rows[i]["ci_id"]), ref jsonNum);

                        ILog.Model.VipILogCount ooCount = Ilog.BLL.VipILogCount.GetModelByUserID(Convert.ToInt64(List.Rows[i]["userid"]));

                        strPageList.Append("sex:\"" + vipInfo.sex + "\",vi_memberlevel:\"" + List.Rows[i]["vi_memberlevel"].ToString() + "\",");
                        strPageList.Append("vi_id:\"" + List.Rows[i]["vi_id"].ToString() + "\",nickname:\"" + List.Rows[i]["nickname"].ToString() + "\",");
                        strPageList.Append("userid:\"" + List.Rows[i]["userid"].ToString() + "\",IsOnline:\"" + ILog.DAL.VipILog.GetUserIsOnline(List.Rows[i]["username"].ToString()) + "\",");
                        strPageList.Append("PR_Name:\"" + dicRegion["Province"] + "\",CI_Name:\"" + dicRegion["City"] + "\",");
                        strPageList.Append("vic_concernnum:\"" + ooCount.vic_concernnum + "\",vic_fannum:\"" + ooCount.vic_fannum + "\",");
                        strPageList.Append("vic_ilognum:\"" + ooCount.vic_ilognum + "\",face:\"" + List.Rows[i]["face"].ToString() + "\"}");

                        //最后一个元素不需要加逗号
                        if ((count - 1) > i)
                        {
                            strPageList.Append(",");
                        }
                    }
                }
                else
                {
                    strPageList.Append("[{State:'2'}");  //无数据不
                }
            }
            catch
            {
                strPageList.Append("[{State:'0'}");
            }

            strPageList.Append("]}");

            return strPageList.ToString();

        }
        #endregion

        #region 构建json（如果有注入就返回空数据集）
        /// <summary>
        /// 构建json（如果有注入就返回空数据集）
        /// </summary>
        /// <param name="List">数据</param>
        /// <param name="RecordCount">总页数</param>
        /// <returns></returns>
        public static string GetNotJsonList()
        {
            StringBuilder strCommentPageList = new StringBuilder();

            strCommentPageList.Append("{List:");

            strCommentPageList.Append("[{State:'2'}");  //无数据

            strCommentPageList.Append("]}");

            return strCommentPageList.ToString();
        }
        #endregion

        #region 发送站短智能提示收信人
        /// <summary>
        /// 发送站短智能提示收信人
        /// </summary>
        /// <returns></returns>
        public static string GetvipilogByNickName(string nickname)
        {
            StringBuilder strPageList = new StringBuilder();

            strPageList.Append("{List:");

            try
            {
                List<ILog.Model.VipILog> GetNickNameList = ILog.DAL.VipILog.GetvipilogByNickName(nickname);

                int count = GetNickNameList.Count;

                int j = 0;

                if (count > 0)  //找到用户
                {
                    strPageList.Append("[{State:'1'},");

                    foreach (ILog.Model.VipILog GetNickNameListInfo in GetNickNameList)
                    {
                        strPageList.Append("{nickname:\"" + GetNickNameListInfo.nickname + "\"}");

                        //最后一个元素不需要加逗号
                        if ((count - 1) > j)
                        {
                            strPageList.Append(",");
                        }

                        j++;    //索引加1
                    }
                }
                else
                {
                    strPageList.Append("[{State:'2'}");  //无数据不
                }
            }
            catch
            {
                strPageList.Append("[{State:'0'}");
            }

            strPageList.Append("]}");

            return strPageList.ToString();
        }
        #endregion

        #region 判断用户是否登录vip
        /// <summary>
        /// 功能描述：判断用户是否登录vip
        /// 一、是否存在useid的cookie
        /// 二、是否通过校验cookie
        /// 创建标识：ljd 20120522
        /// </summary>
        /// <returns></returns>
        public static bool IsLoginVIP()
        {
            string userid = CurrentCookie.GetCookieByKey("UseID");
            if (Utils.StrIsNullOrEmpty(userid))
            {
                return false;
            }
            string VaileResult = CurrentCookie.GetCookieByKey("CheckValid");
            if (string.IsNullOrEmpty(VaileResult))
            {
                return false;
            }
            else
            {
                //string VailCookie = Utils.MD5(userid + "-instrument_4077_20091124").ToLower();
                //if (VaileResult != VailCookie)
                //{
                //    return false;
                //}
            }
            return true;

        }
        #endregion

        #region 获取vip的userid
        /// <summary>
        /// 功能描述：获取vip的userid
        /// 一、是否存在useid的cookie
        /// 二、是否通过校验cookie
        /// 创建标识：ljd 20120523
        /// </summary>
        /// <returns></returns>
        public static long GetVIPUserID()
        {
            long longUserID = 0;

            string userid = CurrentCookie.GetCookieByKey("UseID");
            if (Utils.StrIsNullOrEmpty(userid))
            {
                return 0;
            }
            //string VaileResult = CurrentCookie.GetCookieByKey("CheckValid");
            //if (string.IsNullOrEmpty(VaileResult))
            //{
            //    return 0;
            //}
            //else
            //{
            //string VailCookie = Utils.MD5(userid + "-instrument_4077_20091124").ToLower();
            //if (VaileResult != VailCookie)
            //{
            //    return 0;
            //}
            //}
            longUserID = Convert.ToInt64(userid);
            return longUserID;

        }
        #endregion

        #region 判断用户是否开通认证ilog

        /// <summary>
        /// 功能描述：判断用户是否开通认证ilog
        /// 创建标识：ljd 20120522
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>-1 未开通ilog 0 未手机认证 1已认证</returns>
        public static int CheckIlogOpen(long userid)
        {
            int state = ILog.DAL.VipILog.CheckIlogOpen(userid);
            return state;

        }

        #endregion

        #region 判断用户是否认证手机

        /// <summary>
        /// 功能描述：判断用户是否认证手机
        /// 创建标识：ljd 20120522
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>0 未手机认证 1已认证 2已发送验证码待验证</returns>
        public static int CheckMobilePass(long userid)
        {
            int mobilePass = ILog.DAL.VipILog.CheckMobilePass(userid);
            return mobilePass;

        }

        #endregion

        #region 更新用户头像
        /// <summary>
        /// 更新用户头像
        /// 创建标识：ljd 20120522
        /// <param name="userid">用户id</param>
        /// <param name="face">用户头像</param>
        /// </summary>
        public static int VipILogUpdateFace(long userid, string face)
        {
            int resultCount = ILog.DAL.VipILog.VipILogUpdateFace(userid, face);


            return resultCount;

        }
        #endregion

        #region 手机认证通过后更新vip
        /// <summary>
        /// 功能描述：手机认证通过后更新vip
        /// 创建标识：ljd 20120605
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="vistate">手机认证状态 0未通过 1通过 2禁止</param>
        /// <param name="urlstate">json是否成功</param>
        /// <returns></returns>
        public static int UpdateVipIlogMobileState(long userid, int vistate, ref int urlstate)
        {
            int resultCount = ILog.DAL.VipILog.UpdateVipIlogMobileState(userid, vistate, ref urlstate);

            return resultCount;

        }
        #endregion

        #region 根据userid获取username
        /// <summary>
        /// 根据userid获取username
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static string GetUserNameByUserId(long userid)
        {
            return ILog.DAL.VipILog.GetUserNameByUserId(userid);
        }
        #endregion

        #region 根据username获取userid
        /// <summary>
        /// 功能描述：根据username获取userid
        /// 创建标识：ljd 20120609
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public static long GetUserIDByUserName(string username)
        {
            long userid = ILog.DAL.VipILog.GetUserIDByUserName(username);
            return userid;

        }
        #endregion

        #region 根据用户ID得到ilog实体
        /// <summary>
        /// 功能描述：根据用户ID得到ilog实体
        /// 创建标识：ljd 20120611
        /// </summary>
        public static ILog.Model.VipILog GetModelByUserID(long userid)
        {
            ILog.Model.VipILog ooIlog = ILog.DAL.VipILog.GetModelByUserID(userid);
            return ooIlog;

        }
        #endregion

        #region 创建vipilog用户并初始化count

        /// <summary>
        /// 功能描述：创建vipilog用户并初始化count
        /// 创建标识：ljd 20120611
        /// </summary>
        /// <param name="ooVip">用户实体对象</param>
        /// <param name="urlstate">是否报错</param>
        /// <returns></returns>
        public static int CreateVipIlogAndInit(ILog.Model.Vip ooVip, ref int urlstate)
        {
            int resultCount = 1;

            //写入ilog用户
            ILog.Model.VipILog ooIlog = ILog.DAL.VipILog.GetModelByUserID(ooVip.UserID);
            if (ooIlog == null)
            {
                Dictionary<string, string> dicLog = Ilog.BLL.Vip.GetVIPRegion(ooVip.CI_ID, ref urlstate);

                ooIlog = new ILog.Model.VipILog();
                ooIlog.ci_id = ooVip.CI_ID;
                ooIlog.face = "default.png";
                ooIlog.intime = DateTime.Now;
                ooIlog.iuc_state = 0;
                ooIlog.nickname = ooVip.nickname;
                ooIlog.pr_id = Convert.ToInt32(dicLog["ProvinceID"]);
                ooIlog.userid = ooVip.UserID;
                ooIlog.username = ooVip.username;
                ooIlog.vi_memberlevel = 0;
                ooIlog.vi_state = ooVip.mobile_pass == 1 ? 1 : 0;

                int ilogCount = ILog.DAL.VipILog.VipILogAdd(ooIlog);

                //初始化count
                ILog.Model.VipILogCount ooIlogCount = new ILog.Model.VipILogCount();
                ooIlogCount.intime = DateTime.Now;
                ooIlogCount.userid = ooVip.UserID;
                ooIlogCount.vic_atnum = 0;
                ooIlogCount.vic_commentnum = 0;
                ooIlogCount.vic_commentcountnum = 0;
                ooIlogCount.vic_concernnum = 0;
                ooIlogCount.vic_doubleconcernnum = 0;
                ooIlogCount.vic_fannum = 0;
                ooIlogCount.vic_fanoutnum = 0;
                ooIlogCount.vic_ilognum = 0;
                ooIlogCount.vic_messagenum = 0;
                ooIlogCount.vic_messageoutnum = 0;
                ooIlogCount.vic_onewayconcernnum = 0;

                int count = ILog.DAL.VipILogCount.CountAdd(ooIlogCount);

                if (ilogCount > 0 && count > 0)
                {
                    resultCount = 1;
                }
                else
                {
                    resultCount = 0;
                }
            }

            return resultCount;

        }
        #endregion

        #region 更新用户昵称
        /// <summary>
        /// 功能描述：更新用户昵称
        /// 创建标识：ljd 20120605
        /// <param name="userid">用户id</param>
        /// <param name="nickname">昵称</param>
        /// <param name="prid">省份id</param>
        /// <param name="cityid">城市id</param>
        /// </summary>
        public static int UpdateNickName(long userid, string nickname, int prid, int cityid)
        {
            int resultCount = ILog.DAL.VipILog.UpdateNickName(userid, nickname, prid, cityid);
            return resultCount;

        }
        #endregion

        #region 根据userid获取用户昵称
        /// <summary>
        /// 功能描述：根据userid获取用户昵称
        /// 创建标识：ljd 20120612
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static string GetNickNameByUserId(long userid)
        {
            //昵称
            string nickname = ILog.DAL.VipILog.GetNickNameByUserId(userid);
            return nickname;

        }
        #endregion

        #region 博文中显示内容
        /// <summary>
        /// 功能描述：博文中显示内容
        /// 创建标识：ljd 20120613
        /// </summary>
        /// <param name="ilogContent">博文内容</param>
        /// <param name="ilogOriginalContent">原始的博文内容</param>
        /// <param name="removedlen">被移除的长度</param>
        /// <param name="ilogid">博文id</param>
        /// <param name="usercount">用户个数</param>
        /// <returns></returns>
        public static void GetIlogContentShow(string ilogContent, ref string ilogOriginalContent, ref int removedlen, long ilogid, ref int usercount)
        {
            string username = "";

            //第一个@的位置
            int atIndex = ilogContent.IndexOf("@");

            //第一个@后的用户名
            string firstIlogContent = "";

            //第一个@后的文字内容
            string secondIlogContent = "";

            /*
             * 如果存在@，则截取第一个@之后的内容
             * 判断取出来的内容空格与第二个@的位置哪个靠前
             * 判断靠前的第二个标志位是否与第一个@位置相邻
             * 判断第一个@的内容是否是数字+英文+半角字符的形式
             * 判断筛选出的用户名是否在数据库中存在，存在的话取出用户id
             * 进行下个递归
             */
            if (atIndex >= 0)
            {
                secondIlogContent = ilogContent.Substring(atIndex + 1, ilogContent.Length - atIndex - 1);
            }

            //第二个@的位置
            int secondAtIndex = secondIlogContent.IndexOf("@");
            //第二个空格的位置
            int secondspaceIndex = secondIlogContent.IndexOf(" ");

            //第二个标识位的位置
            int secondIndex = -1;

            if (secondspaceIndex >= 0 && secondAtIndex < 0)
            {
                secondIndex = secondspaceIndex;
            }
            else if (secondspaceIndex < 0 && secondAtIndex >= 0)
            {
                secondIndex = secondAtIndex;
            }
            else
            {
                secondIndex = secondspaceIndex < secondAtIndex ? secondspaceIndex : secondAtIndex;
            }

            if (secondIndex > 0)//存在其他@或空格标识，并且不与上一个@紧邻
            {
                //截取第一个@到第二个标识位之间的内容
                firstIlogContent = secondIlogContent.Substring(0, secondIndex);
            }
            else if (secondAtIndex < 0)
            {
                //截取到第一个@到最后
                firstIlogContent = secondIlogContent;
            }

            //用于替换@样式的截取ilog原内容的开始索引
            int secondSubIndex = secondIndex;

            //获得第一个@内容中半角字符（除_）的位置
            int pointIndex = -1;

            for (int i = 0; i < firstIlogContent.Length; i++)
            {
                Regex regexHalf = new Regex("^[\x00-\xff]*$");
                Regex regexEnglish = new Regex("^[0-9a-zA-Z_]{1,}$");
                if (regexHalf.IsMatch(firstIlogContent[i].ToString()) && !regexEnglish.IsMatch(firstIlogContent[i].ToString()))
                {
                    pointIndex = i;
                    break;
                }
            }

            if (pointIndex > 0)//存在.
            {
                //@到半角字符之间的内容
                string pointContent = firstIlogContent.Substring(0, pointIndex);
                //Regex regex = new Regex("[\u4E00-\u9FA5\uf900-\ufa2d]");
                ////是否包含中文
                //bool isSuccess = regex.IsMatch(pointContent);
                //if (isSuccess)//包含中文，截取@到全角之间的内容
                //{
                username = pointContent;
                //}
                secondSubIndex = pointIndex;
            }
            else if (pointIndex < 0)//不包含.
            {
                username = firstIlogContent;
            }

            if (username != "")
            {
                //前一段ilog原文
                string prevOriginalILog = ilogOriginalContent.Substring(0, atIndex + removedlen);
                //后一段ilog原文
                string nextOriginalILog = "";
                if (secondSubIndex >= 0)
                {
                    nextOriginalILog = secondIlogContent.Substring(secondSubIndex, ilogContent.Length - atIndex - secondSubIndex - 1);
                }
                else
                {
                    nextOriginalILog = "";
                }
                //用户id
                long userid = Ilog.BLL.VipILog.GetUserIDByNickName(username);

                string strUrl = "";

                if (userid == BLL.VipILog.GetVIPUserID())
                {
                    //伪静态地址
                    strUrl = "u";
                }
                else
                {
                    //伪静态地址
                    strUrl = "u_" + userid;
                }

                //替换之后的新的用户名
                string newusername = "<a href=\"" + strUrl + "\" id=\"uu_" + ilogid + "_" + userid + "_" + usercount
                    + "_" + Guid.NewGuid() + "\" onMouseOver=\"UserInfoShowOver(this," + userid + ","
                    + ilogid + ");\" class=\"Blue Fa\">@" + username + "</a>";

                ilogOriginalContent = string.Format("{0}{2}{1}", prevOriginalILog, nextOriginalILog, newusername);
                removedlen = prevOriginalILog.Length + newusername.Length - username.Length;
                usercount++;
            }
            if (secondIndex > 0)
            {
                GetIlogContentShow(secondIlogContent, ref ilogOriginalContent, ref removedlen, ilogid, ref usercount);
            }
            return;

        }
        #endregion

        #region 根据用户昵称获取userid
        /// <summary>
        /// 功能描述：根据用户昵称获取userid
        /// 创建标识：ljd 20120618
        /// </summary>
        /// <param name="nickname">用户昵称</param>
        /// <returns></returns>
        public static long GetUserIDByNickName(string nickname)
        {
            long userid = ILog.DAL.VipILog.GetUserIDByNickName(nickname);
            return userid;

        }
        #endregion

        #region 智能搜索得到前10个该用户的粉丝和关注的昵称
        /// <summary>
        /// 功能描述：智能搜索得到前10个该用户的粉丝和关注的昵称
        /// 创建标识：ljd 20120617
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="nickname">昵称</param>
        /// <returns></returns>
        public static List<ILog.Model.VipILog> GetAtUserList(long userid, string nickname)
        {
            List<ILog.Model.VipILog> ilogList = ILog.DAL.VipILog.GetAtUserList(userid, "%" + nickname + "%");
            return ilogList;

        }
        #endregion

        #region 智能搜索得到前10个该用户的粉丝和关注的昵称(json格式)
        /// <summary>
        /// 功能描述：智能搜索得到前10个该用户的粉丝和关注的昵称(json格式)
        /// 创建标识：ljd 20120617
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="nickname">昵称</param>
        /// <returns></returns>
        public static string GetAtUserListStr(long userid, string nickname)
        {
            StringBuilder strbAtUserlList = new StringBuilder();

            strbAtUserlList.Append("{AtUserList:[");

            List<ILog.Model.VipILog> ilogList = new List<ILog.Model.VipILog>();
            try
            {
                ilogList = GetAtUserList(userid, nickname);
                if (ilogList.Count == 0)
                {
                    strbAtUserlList.Append("{State:'0'}]}");//无数据
                }
                else
                {
                    strbAtUserlList.Append("{State:'1'},");

                    foreach (ILog.Model.VipILog ooIlog in ilogList)
                    {
                        strbAtUserlList.Append("{nickname:'" + ooIlog.nickname + "'},");
                    }
                    strbAtUserlList.Remove(strbAtUserlList.Length - 1, 1);
                    strbAtUserlList.Append("]}");
                }

            }
            catch
            {
                strbAtUserlList.Append("{State:'-1'}]}");//报错
            }

            return strbAtUserlList.ToString();

        }
        #endregion

        #region 得到他人主页与个人主页右侧认证等级与认证说明（json格式）

        /// <summary>
        /// 功能描述：得到他人主页与个人主页右侧认证等级与认证说明（json格式）
        /// 创建标识：ljd 20120627
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static string GetMemberLevelAndVerifyCommentJsonStr(long userid)
        {
            int urlstate = 0;

            ILog.Model.VipILog ooIlog = ILog.DAL.VipILog.GetModelByUserID(userid);

            ILog.Model.Vip ooVip = Ilog.BLL.Vip.GetUserInfo(userid, ref urlstate);

            ILog.Model.ILogAuthenticationHistory ooHistory = ILog.DAL.ILogAuthenticationHistory.GetAuthenticationHistoryInfoByUserId(userid, ooIlog.vi_memberlevel);

            StringBuilder strbIlog = new StringBuilder();

            if (ooIlog == null)
            {
                strbIlog.Append("{memberlevel:'0',comment:''}");
            }
            else
            {
                string comment = "";

                if (ooHistory != null && ooHistory.ia_State == 1)
                {
                    comment = ooHistory.ia_Comment;
                }
                strbIlog.Append("{memberlevel:'" + ooIlog.vi_memberlevel + "',comment:'" + comment + "',sex:'" + ooVip.sex + "'}");
            }

            return strbIlog.ToString();

        }

        /// <summary>
        /// 功能描述：得到举报用户当中的昵称，认证类型，头像，认证信息（json格式）
        /// 创建标识：zhangl 2012075
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static string GetUserReportJsonStr(long userid, long loUserID)
        {
            StringBuilder strbIlog = new StringBuilder();

            if (userid != loUserID)
            {
                ILog.Model.VipILog ooIlog = ILog.DAL.VipILog.GetModelByUserID(userid);

                ILog.Model.ILogAuthenticationHistory ooHistory = ILog.DAL.ILogAuthenticationHistory.GetAuthenticationHistoryInfoByUserId(userid, ooIlog.vi_memberlevel);




                if (ooIlog == null)
                {
                    strbIlog.Append("{UrlState:'0'}");
                }
                else
                {
                    string comment = "";

                    if (ooHistory != null && ooHistory.ia_State == 1)
                    {
                        comment = ooHistory.ia_Comment;
                    }
                    strbIlog.Append("{UrlState:'1',memberlevel:'" + ooIlog.vi_memberlevel + "',comment:'" + comment + "',NickName:'" + ooIlog.nickname + "',Face:'" + ooIlog.face + "'}");
                }
            }
            else
            {
                //自己不能举报自己
                strbIlog.Append("{UrlState:'2'}");
            }

            return strbIlog.ToString();

        }

        #endregion

        #region 查看某条记录是否存在（true：1，false：0）前台用户
        /// <summary>
        /// 查看某条记录是否存在（true：1，false：0）前台用户
        /// <param name="nickname">用户昵称</param>
        /// </summary>
        public static string VipILogExists(string nickname)
        {
            StringBuilder strInfo = new StringBuilder();

            bool isExists = ILog.DAL.VipILog.VipILogExists(nickname);

            strInfo.Append("{List:");

            try
            {
                if (isExists)
                {
                    strInfo.Append("[{State:'1'}");

                    strInfo.Append(",{exists:'" + 1 + "'}");
                }
                else
                {
                    strInfo.Append("[{State:'2'}");  //无数据不
                }
            }
            catch
            {
                strInfo.Append("[{State:'0'}");
            }

            strInfo.Append("]}");

            return strInfo.ToString();
        }
        #endregion


        #region 查看某条记录是否存在（true：1，false：0）后台用户
        /// <summary>
        /// 查看某条记录是否存在（true：1，false：0）后台用户
        /// <param name="nickname">用户昵称</param>
        /// </summary>
        public static bool VipILogExists_a(string nickname)
        {
            return ILog.DAL.VipILog.VipILogExists(nickname);
        }
        #endregion

        #region 得到名人微博列表（昨日发微博最多的经过名人认证的前10名用户）
        /// <summary>
        /// 功能描述：得到名人微博列表（昨日发微博最多的经过名人认证的前10名用户）
        /// 创建标识：ljd 20120705
        /// </summary>
        /// <returns></returns>
        public static List<ILog.Model.VipILog> GetFamousUserList()
        {
            List<ILog.Model.VipILog> famousList = ILog.DAL.VipILog.GetFamousUserList();
            return famousList;

        }
        #endregion

        #region 得到草根微博列表（昨日发微博最多的非名人认证的前10名用户）
        /// <summary>
        /// 功能描述：得到草根微博列表（昨日发微博最多的非名人认证的前10名用户）
        /// 创建标识：ljd 20120705
        /// </summary>
        /// <returns></returns>
        public static List<ILog.Model.VipILog> GetCommonUserList()
        {
            List<ILog.Model.VipILog> commonList = ILog.DAL.VipILog.GetCommonUserList();
            return commonList;

        }
        #endregion



        #region 得到名人微博列表字符串（json格式）（昨日发微博最多的经过名人认证的前10名用户）
        /// <summary>
        /// 功能描述：得到名人微博列表字符串（json格式）（昨日发微博最多的经过名人认证的前10名用户）
        /// 创建标识：ljd 20120705
        /// </summary>
        /// <returns></returns>
        public static string GetFamousUserListJsonStr()
        {
            StringBuilder strbUserList = new StringBuilder();

            strbUserList.Append("{UserList:[");

            try
            {
                List<ILog.Model.VipILog> famousList = GetFamousUserList();

                if (famousList.Count > 0)
                {
                    strbUserList.Append("{State:'1'},");

                    foreach (ILog.Model.VipILog ooVipilog in famousList)
                    {
                        ILog.Model.VipILogCount ooCount = ILog.DAL.VipILogCount.GetModelByUserID(ooVipilog.userid);

                        strbUserList.Append("{userid:'" + ooVipilog.userid + "',nickname:'" + ooVipilog.nickname + "',face:'" + ooVipilog.face + "',fansnum:'" + ooCount.vic_fannum + "'},");
                    }
                    strbUserList.Remove(strbUserList.Length - 1, 1);
                    strbUserList.Append("]}");
                }
                else
                {
                    strbUserList.Append("{State:'0'}]}");
                }
            }
            catch
            {
                strbUserList.Append("{State:'-1'}]}");
            }
            return strbUserList.ToString();

        }
        #endregion

        #region 得到草根微博列表字符串（json格式）（昨日发微博最多的草根(非名人认证)的前10名用户）
        /// <summary>
        /// 功能描述：得到草根微博列表字符串（json格式）（昨日发微博最多的草根(非名人认证)的前10名用户）
        /// 创建标识：ljd 20120705
        /// </summary>
        /// <returns></returns>
        public static string GetCommonUserListJsonStr()
        {
            StringBuilder strbUserList = new StringBuilder();

            strbUserList.Append("{UserList:[");

            try
            {
                List<ILog.Model.VipILog> commonList = GetCommonUserList();

                if (commonList.Count > 0)
                {
                    strbUserList.Append("{State:'1'},");

                    foreach (ILog.Model.VipILog ooVipilog in commonList)
                    {
                        ILog.Model.VipILogCount ooCount = ILog.DAL.VipILogCount.GetModelByUserID(ooVipilog.userid);

                        strbUserList.Append("{userid:'" + ooVipilog.userid + "',nickname:'" + ooVipilog.nickname + "',face:'" + ooVipilog.face + "',fansnum:'" + ooCount.vic_fannum + "'},");
                    }
                    strbUserList.Remove(strbUserList.Length - 1, 1);
                    strbUserList.Append("]}");
                }
                else
                {
                    strbUserList.Append("{State:'0'}]}");
                }
            }
            catch
            {
                strbUserList.Append("{State:'-1'}]}");
            }
            return strbUserList.ToString();

        }
        #endregion


        #region 得到最新开通微博的用户列表
        /// <summary>
        /// 功能描述：得到最新开通微博的用户列表
        /// 创建标识：ljd 20120705
        /// </summary>
        /// <returns></returns>
        public static List<ILog.Model.VipILog> GetNewUserList()
        {
            List<ILog.Model.VipILog> userlist = ILog.DAL.VipILog.GetNewUserList();

            return userlist;

        }
        #endregion

        #region 得到最新经过名人认证的用户列表
        /// <summary>
        /// 功能描述：得到最新经过名人认证的用户列表
        /// 创建标识：ljd 20120705
        /// </summary>
        /// <returns></returns>
        public static List<ILog.Model.VipILog> GetNewFamousUserList()
        {
            List<ILog.Model.VipILog> userlist = ILog.DAL.VipILog.GetNewFamousUserList();

            return userlist;

        }
        #endregion

        #region 得到最新开通微博的前9个用户列表（json格式）
        /// <summary>
        /// 功能描述：得到最新开通微博的前9个用户列表（json格式）
        /// 创建标识：ljd 20120705
        /// </summary>
        /// <returns></returns>
        public static string GetNewUserListJsonStr()
        {
            StringBuilder strbUserList = new StringBuilder();

            strbUserList.Append("{UserList:[");

            try
            {
                List<ILog.Model.VipILog> userList = GetNewUserList();

                if (userList.Count > 0)
                {
                    strbUserList.Append("{State:'1'},");

                    foreach (ILog.Model.VipILog ooVipilog in userList)
                    {
                        ILog.Model.VipILogCount ooCount = ILog.DAL.VipILogCount.GetModelByUserID(ooVipilog.userid);

                        strbUserList.Append("{userid:'" + ooVipilog.userid + "',nickname:'" + ooVipilog.nickname + "',face:'" + ooVipilog.face + "',fansnum:'" + ooCount.vic_fanoutnum + "'},");
                    }
                    strbUserList.Remove(strbUserList.Length - 1, 1);
                    strbUserList.Append("]}");
                }
                else
                {
                    strbUserList.Append("{State:'0'}]}");
                }
            }
            catch
            {
                strbUserList.Append("{State:'-1'}]}");
            }
            return strbUserList.ToString();

        }
        #endregion

        #region 得到最新经过名人认证的前9个用户列表（json格式）
        /// <summary>
        /// 功能描述：得到最新经过名人认证的前9个用户列表（json格式）
        /// 创建标识：ljd 20120705
        /// </summary>
        /// <returns></returns>
        public static string GetNewFamousUserListJsonStr()
        {
            StringBuilder strbUserList = new StringBuilder();

            strbUserList.Append("{UserList:[");

            try
            {
                List<ILog.Model.VipILog> userList = GetNewFamousUserList();

                if (userList.Count > 0)
                {
                    strbUserList.Append("{State:'1'},");

                    foreach (ILog.Model.VipILog ooVipilog in userList)
                    {
                        ILog.Model.VipILogCount ooCount = ILog.DAL.VipILogCount.GetModelByUserID(ooVipilog.userid);

                        strbUserList.Append("{userid:'" + ooVipilog.userid + "',nickname:'" + ooVipilog.nickname + "',face:'" + ooVipilog.face + "',fansnum:'" + ooCount.vic_fannum + "'},");
                    }
                    strbUserList.Remove(strbUserList.Length - 1, 1);
                    strbUserList.Append("]}");
                }
                else
                {
                    strbUserList.Append("{State:'0'}]}");
                }
            }
            catch
            {
                strbUserList.Append("{State:'-1'}]}");
            }
            return strbUserList.ToString();

        }
        #endregion


        #region 得到名人堂列表分页数据
        /// <summary>
        /// 功能描述：得到名人堂列表分页数据
        /// 创建标识：ljd 20120821
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="userid">当前用户id</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        public static string GetFameList(int PageCurrent, int PageSize, long userid, string keyword)
        {
            string strTableName = " vip_ilog ";
            string strFieldKey = "vi_id";
            string strFieldShow = "vi_id,userid ";
            string strFieldOrder = " dbo.fn_GetFansNumAndilogNum(userid) desc ";
            string strWhere = " vi_memberlevel=2 ";
            if (!string.IsNullOrEmpty(keyword))
            {
                strWhere += " AND nickname LIKE '%" + keyword + "%'";
            }
            int RecordCount = 0;

            DataSet dsSearchList = ILog.Common.Common.DataSelect_New(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            DataTable dblSearchList = new DataTable();

            if (dsSearchList != null && dsSearchList.Tables.Count > 0)
            {
                dblSearchList = dsSearchList.Tables[0];
            }

            return GetFameJsonList(dblSearchList, RecordCount, userid, PageSize);
        }
        #endregion


        #region 构建json
        /// <summary>
        /// 构建json
        /// </summary>
        /// <param name="List">数据</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="userid">当前用户id</param>
        /// <returns></returns>
        private static string GetFameJsonList(DataTable List, int RecordCount, long userid, int pagesize)
        {
            int count = List.Rows.Count;

            int RowsCount = pagesize * RecordCount;

            StringBuilder strPageList = new StringBuilder();

            strPageList.Append("{List:");

            try
            {
                if (count > 0)
                {
                    strPageList.Append("[{State:'1'}");

                    strPageList.Append(",{RecordCount:'" + (RecordCount > 10 ? 10 : RecordCount) + "'},");  //所有列表页最多显示10页

                    int jsonNum = 0;

                    //获取加i信息
                    ILog.Model.VipILog userInfo;

                    //用户性别
                    ILog.Model.Vip vipInfo;

                    for (int i = 0; i < count; i++)
                    {
                        long iloguserid = Convert.ToInt64(List.Rows[i]["userid"]);


                        strPageList.Append("{isfollow:\"" + Ilog.BLL.ILogUserConcern.UserConcernOnly_State(userid, iloguserid).ToString() + "\",");

                        //是否双项关注：true关注
                        strPageList.Append("isuserconcernstate:\"" + ILog.DAL.ILogUserConcern.UserConcern_State(userid, iloguserid) + "\",");

                        //加i信息
                        userInfo = ILog.DAL.VipILog.GetModelByUserID(iloguserid);

                        //获取用户性别
                        vipInfo = ILog.DAL.Vip.GetUserInfo(iloguserid, ref jsonNum);

                        //获取用户的最新博文
                        ILog.Model.ILogOriginal ooOriginal = BLL.ILogOriginal.GetLastestOriginalInfo(iloguserid);

                        //获取用户数量属性
                        ILog.Model.VipILogCount ooIlogCount = BLL.VipILogCount.GetModelByUserID(iloguserid);

                        //获取用户的地区
                        Dictionary<string, string> dicPosition = BLL.Vip.GetVIPRegion(userInfo.ci_id, ref jsonNum);

                        strPageList.Append("sex:\"" + vipInfo.sex + "\",memberlevel:\"" + userInfo.vi_memberlevel + "\",");// + "\",is_id:\"" + ooOriginal.is_id  
                        strPageList.Append("vi_id:\"" + List.Rows[i]["vi_id"].ToString() + "\",nickname:\"" + userInfo.nickname + "\",");
                        strPageList.Append("userid:\"" + iloguserid + "\",IsOnline:\"" + ILog.DAL.VipILog.GetUserIsOnline(userInfo.username) + "\",");
                        if (ooOriginal != null)
                        {
                            strPageList.Append("intime:\"" + ILog.Common.Common.GetIlogTime(ooOriginal.intime) + "\",is_content:\""
                                + ILog.Common.Common.GetJScriptGlobalObjectEscape(ooOriginal.io_content) + "\",");//+ "\",is_url:\"" + ooSource.is_url+ "\","

                            strPageList.Append("iso_id:\"" + ooOriginal.io_id + "\",");
                        }
                        else
                        {
                            strPageList.Append("iso_id:0,");
                        }
                        strPageList.Append("address:\"" + dicPosition["Province"] + " " + dicPosition["City"] + "\",");
                        strPageList.Append("vic_concernnum:\"" + ooIlogCount.vic_concernnum + "\",vic_fannum:\"" + ooIlogCount.vic_fannum + "\",");
                        strPageList.Append("vic_ilognum:\"" + ooIlogCount.vic_ilognum + "\",");
                        strPageList.Append("face:\"" + userInfo.face + "\"}");

                        //最后一个元素不需要加逗号
                        if ((count - 1) > i)
                        {
                            strPageList.Append(",");
                        }
                    }
                }
                else
                {
                    strPageList.Append("[{State:'2'}");  //无数据不
                }
            }
            catch
            {
                strPageList.Append("[{State:'0'}");
            }

            strPageList.Append("]}");

            return strPageList.ToString();
        }
        #endregion

        //#region 根据关注人得到关注状态
        ///// <summary>
        ///// 功能描述：根据关注人得到关注状态
        ///// 创建标识：ljd 20120829
        ///// </summary>
        ///// <param name="concernuserid"></param>
        ///// <returns></returns>
        //public static string GetConcernStateStr(long concernuserid)
        //{
        //    long userid = BLL.VipILog.GetVIPUserID();

        //    StringBuilder strbHtml = new StringBuilder();

        //    ILog.Model.ILogUserConcern userConcern = ILog.DAL.ILogUserConcern.GetIlogUserconcernInfoByUserId(concernuserid, userid);

        //    int concernstate = 2;

        //    if (userConcern != null)
        //    {
        //        concernstate = Convert.ToInt32(userConcern.iuc_state);
        //    }

        //    strbHtml.Append("{concernstate:\"" + concernstate + "\"}");

        //    return strbHtml.ToString();

        //}
        //#endregion

    }

}
