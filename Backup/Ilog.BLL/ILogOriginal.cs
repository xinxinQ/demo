using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using System.Text.RegularExpressions;

using Com.ILog.Utils;
using ILog.Common;


namespace Ilog.BLL
{
    public class ILogOriginal
    {
        #region 查看某个原创是否存在（True：存在，False：不存在）
        /// <summary>
        /// 查看某个原创是否存在（True：存在，False：不存在）
        /// <param name="io_id">流水号</param>
        /// </summary>
        public static string OriginalExists(int io_id)
        {
            StringBuilder strOriginalExists = new StringBuilder();

            strOriginalExists.Append("var strstrOriginalExistsJsonObject = ");
            strOriginalExists.Append("({");
            strOriginalExists.Append("\"Exists\": \"" + ILog.DAL.ILogOriginal.OriginalExists(io_id).ToString() + "\"");
            strOriginalExists.Append("})");

            return strOriginalExists.ToString();
        }
        #endregion

        #region  增加一条原创数据
        /// <summary>
        /// 增加一条原创数据
        /// <param name="model">原创表实体</param>
        /// </summary>
        public static string OriginalAdd(ILog.Model.ILogOriginal model)
        {
            int urlState = 0;
            OriginalAddAndGetID(model, ref urlState);
            StringBuilder strOriginalAdd = new StringBuilder();

            strOriginalAdd.Append("{");
            strOriginalAdd.Append("state:" + urlState);
            strOriginalAdd.Append("}");

            return strOriginalAdd.ToString();
        }
        #endregion


        #region  增加一条原创数据，并返回id
        /// <summary>
        /// 功能描述：增加一条原创数据，并返回id
        /// 创建标识：ljd
        /// <param name="model">原创表实体</param>
        /// <returns>原创id</returns>
        /// </summary>
        public static long OriginalAddAndGetID(ILog.Model.ILogOriginal model, ref int urlState)
        {
            long originalID = ILog.DAL.ILogOriginal.OriginalAdd(model, ref urlState);
            return originalID;

        }
        #endregion

        #region 更新原创
        /// <summary>
        /// 更新原创
        /// <param name="model">原创表实体</param>
        /// </summary>
        public static string OriginalUpdate(ILog.Model.ILogOriginal model)
        {
            StringBuilder strOriginalUpdate = new StringBuilder();

            strOriginalUpdate.Append("var strOriginalUpdateJsonObject = ");
            strOriginalUpdate.Append("({");
            strOriginalUpdate.Append("\"state\": \"" + ILog.DAL.ILogOriginal.OriginalUpdate(model).ToString() + "\"");
            strOriginalUpdate.Append("})");

            return strOriginalUpdate.ToString();
        }
        #endregion

        #region 删除一条原创
        /// <summary>
        /// 删除一条原创
        /// <param name="io_id">流水号</param>
        /// </summary>
        public static string OriginalDel(int io_id)
        {
            StringBuilder strOriginalDel = new StringBuilder();

            //strOriginalDel.Append("var strOriginalDelJsonObject = ");
            strOriginalDel.Append("{");
            strOriginalDel.Append("\"state\": \"" + ILog.DAL.ILogOriginal.OriginalDel(io_id).ToString() + "\"");
            strOriginalDel.Append("}");

            return strOriginalDel.ToString();
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="icg_id">流水号</param>
        /// </summary>
        public static string GetModel(int io_id)
        {
            DataTable dblOriginalModelList = ILog.DAL.ILogOriginal.GetModel(io_id);

            //构建josn字符串 
            string strOriginalModelListJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblOriginalModelList).ToString();

            return strOriginalModelListJosn;
        }
        #endregion

        #region 得到原创详细信息
        /// <summary>
        /// 功能描述：得到原创详细信息
        /// 创建标识：ljd 20120610
        /// </summary>
        /// <param name="io_id">流水号</param>
        /// <param name="urlState">是否报错</param>
        /// <returns></returns>
        public static ILog.Model.ILogOriginal GetOriginalInfo(long io_id)
        {
            ILog.Model.ILogOriginal ooOriginal = ILog.DAL.ILogOriginal.GetOriginalInfo(io_id);
            return ooOriginal;

        }

        #endregion


        #region 博文内页获取原创及用户信息


        /// <summary>
        /// 根据原创编号获取原创及用户信息
        /// </summary>
        /// <param name="ioId">原创编号</param>
        /// <returns></returns>
        public static string GetOriginalInfoById(long ioId)
        {

            StringBuilder result = new StringBuilder();


            try
            {



                ILog.Model.ILogOriginal ooOriginal = ILog.DAL.ILogOriginal.GetOriginalInfo(ioId);

                result.Append("{Original:[");



                    //获取用户关注状态

                    long currentuserid = Ilog.BLL.VipILog.GetVIPUserID();

                    ILog.Model.ILogUserConcern userConcern = ILog.DAL.ILogUserConcern.GetIlogUserconcernInfoByUserId(ooOriginal.userid, currentuserid);

                    ILog.Model.VipILog userInfo = ILog.DAL.VipILog.GetVipIlogInfoById(ooOriginal.userid);

                    result.Append("{State:'1',");
                    result.Append("nickname:'");
                    result.Append(userInfo == null ? "未知" : userInfo.nickname);//用户昵称
                    result.Append("',");
                    result.Append("userid:'");
                    result.Append(ooOriginal.userid);//用户编号
                    result.Append("',");
                    result.Append("concertState:'");
                    result.Append(userConcern == null ? 2 : Convert.ToInt32(userConcern.iuc_state));
                    result.Append("'}");

                    //有图片
                    if (ooOriginal.io_haspic == true)
                    {

                        List<ILog.Model.ILogPic> picList = ILog.DAL.ILogPic.GetPicList(ooOriginal.io_id);


                        //判断是否存在图片
                        if (picList.Count > 0)
                        {

                            for (int i = 0; i < picList.Count; i++)
                            {

                                result.Append(",{");
                                result.AppendFormat("pic{0}:'{0}'", i, picList[i].ip_name);
                                result.Append("}");

                            }

                            result.Append("]}");

                        }


                    }

                    result.Append("]}");


            }
            catch (Exception ex)
            {

                result.Length = 0;
                result.Append("{Original:[{State:'0'}]}");

            }

            return result.ToString();

        }




        #endregion


        #region 搜索列表
        /// <summary>
        /// 搜索列表
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        public static string GetSearchList(int PageCurrent, int PageSize, string keyword)
        {
            string strTableName = " ilog_original ";
            string strFieldKey = "io_id";
            string strFieldShow = " io_id,userid,cw_type,(select nickname from vip_ilog where userid = ilog_original.userid) as nickname,intime,io_content,(select vic_commentnum from vip_ilog_count where userid = ilog_original.userid) as vic_commentnum, ";
            strFieldShow += " io_spreadnum,(select is_name from ilog_source where is_id = ilog_original.is_id ) as is_name,(select face from vip_ilog where vip_ilog.userid = ilog_original.userid) as face,(select username from vip_ilog where vip_ilog.userid = ilog_original.userid) as username, ";
            strFieldShow += " (select is_url from ilog_source where is_id = ilog_original.is_id ) as is_url,io_haspic ";

            string strFieldOrder = " io_id desc ";
            string strWhere = " io_content  like'%" + keyword + "%'";

            int RecordCount = 0;

            DataTable dblSearchList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            return GetJsonList(dblSearchList, RecordCount);
        }
        #endregion

        #region 构建json
        /// <summary>
        /// 构建json
        /// </summary>
        /// <param name="List">数据</param>
        /// <param name="RecordCount">总页数</param>
        /// <returns></returns>
        private static string GetJsonList(DataTable List, int RecordCount)
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

                    if (RecordCount == 1)     //如果有搜索结果只有一页那么就显示页数
                    {
                        strPageList.Append(",{RowsCount:'" + count + "',RecordCount:'1'},");
                    }
                    else
                    {
                        strPageList.Append(",{RecordCount:'" + (RecordCount > 10 ? 10 : RecordCount) + "'},");  //所有列表页最多显示10页
                    }

                    //获取加i信息
                    ILog.Model.VipILog userInfo;

                    for (int i = 0; i < count; i++)
                    {
                        strPageList.Append("{io_id:\"" + List.Rows[i]["io_id"].ToString() + "\",userid:\"" + List.Rows[i]["userid"].ToString() + "\",");

                        //加i信息
                        userInfo = ILog.DAL.VipILog.GetVipIlogInfoById(Convert.ToInt64(List.Rows[i]["userid"]));

                        //加i信息
                        strPageList.Append("vi_memberlevel:\"" + userInfo.vi_memberlevel + "\",");

                        //是否在线
                        strPageList.Append("IsOnline:\"" + ILog.DAL.VipILog.GetUserIsOnline(List.Rows[i]["username"].ToString()) + "\",");

                        //查看发@的时间是不是当天
                        strPageList.Append("intime:\"" + ILog.Common.Common.GetIlogTime(Convert.ToDateTime(List.Rows[i]["intime"])) + "\",face:\"" + List.Rows[i]["face"].ToString() + "\",");
                        strPageList.Append("cw_type:\"" + List.Rows[i]["cw_type"].ToString() + "\",");
                        strPageList.Append("nickname:\"" + List.Rows[i]["nickname"].ToString() + "\",io_content:\"" +
                            //ILog.Common.Common.GetJScriptGlobalObjectEscape(GetFinalIlogContentShow(List.Rows[i]["io_content"].ToString(), Convert.ToInt64(List.Rows[i]["io_id"]))) + "\",");
                            ILog.Common.Common.GetJScriptGlobalObjectEscape(GetClearUUBAndChangeExpression(List.Rows[i]["io_content"].ToString(), 0, "")) + "\",");


                        //可传给举报中方法中的内容，表情要替换成文字
                        strPageList.Append("io_content_:\"" + GetIlogContentWithExpression(List.Rows[i]["io_content"].ToString()) + "\",");

                        strPageList.Append("io_spreadnum:\"" + List.Rows[i]["io_spreadnum"].ToString() + "\",vic_commentnum:\"" + List.Rows[i]["vic_commentnum"].ToString() + "\",");
                        strPageList.Append("is_name:\"" + List.Rows[i]["is_name"].ToString() + "\",is_url:\"" + List.Rows[i]["is_url"].ToString() + "\",");
                        strPageList.Append("io_haspic:\"" + List.Rows[i]["io_haspic"].ToString() + "\"}");

                        //最后一个元素不需要加逗号
                        if ((count - 1) > i)
                        {
                            strPageList.Append(",");
                        }
                    }
                }
                else
                {
                    strPageList.Append("[{State:'2'},{RowsCount:'0'}");  //无数据不
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                strPageList.Append("[{State:'0'}");
            }

            strPageList.Append("]}");

            return strPageList.ToString();
        }
        #endregion


        #region 构建json（原厂中的图片）
        /// <summary>
        /// 构建json（原厂中的图片）
        /// </summary>
        /// <param name="List">数据</param>
        /// <returns></returns>
        public static string GetJsonList_Pic(long IoId)
        {
            DataTable List = ILog.DAL.ILogPic.GetPicByIoId(IoId);   //获取原创中的图片

            int count = List.Rows.Count;

            StringBuilder strPageList = new StringBuilder();

            strPageList.Append("{List:");

            try
            {
                if (count > 0)
                {
                    strPageList.Append("[{State:'1'},");

                    for (int i = 0; i < count; i++)
                    {
                        strPageList.Append("{ip_id:\"" + List.Rows[i]["ip_id"].ToString() + "\",ip_name:\"" + List.Rows[i]["ip_name"].ToString() + "\"}");

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

        #region 发送站短智能提示收信人
        /// <summary>
        /// 发送站短智能提示收信人
        /// </summary>
        /// <param name="Originaltitle">文章内容</param>
        /// <returns></returns>
        public static string GetSearchOriginalInfo(string Originaltitle)
        {
            StringBuilder strPageList = new StringBuilder();

            strPageList.Append("{List:");

            try
            {
                List<ILog.Model.ILogOriginal> GetILogOriginalList = ILog.DAL.ILogOriginal.GetSearchOriginalInfo(Originaltitle);

                int count = GetILogOriginalList.Count;

                int j = 0;

                //搜索结果
                string strio_content;

                //内容
                string ilogContent;

                if (count > 0)  //找到用户
                {
                    strPageList.Append("[{State:'1'},");

                    foreach (ILog.Model.ILogOriginal GetILogOriginalListInfo in GetILogOriginalList)
                    {
                        //把表情替换成空
                        ilogContent = Regex.Replace(GetILogOriginalListInfo.io_content, @"\[img=(\d*|),(\d*|)\](.[^\]]*)(\[\/img\])", "");

                        //去掉\n
                        ilogContent = ilogContent.Replace("\n", "");

                        //长度截取
                        strio_content = Com.ILog.Utils.Utils.GetUnicodeSubString(ilogContent, 60, "");

                        //判断是否在30个字内匹配了到内容，如果匹配到了内容就显示在搜索联想中
                        if (strio_content.Contains(Originaltitle))
                        {
                            strPageList.Append("{originalinfo:\"" + strio_content + "\"}");
                        }
                        else
                        {
                            strPageList.Append("{originalinfo:\"\"}");
                        }

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


        #region 首页博文
        /// <summary>
        /// 首页博文
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="userid">当前用户id</param>
        /// <returns></returns>
        public static string GetList(int PageCurrent, int PageSize, long userid)
        {
            string strTableName = " ilog_spread ";
            string strFieldKey = "is_id";
            string strFieldShow = " is_id,userid,io_id,is_isoriginal,is_type,(select nickname from vip_ilog where userid = ilog_spread.userid) as nickname,intime,dbo.fn_GetSpreadCommentNum(iso_id,io_id,is_spreadtype,is_isoriginal,is_type,is_commentnum) as vic_commentnum, ";
            strFieldShow += "dbo.fn_GetSpreadSpreadNum(iso_id,io_id,is_spreadtype,is_isoriginal,is_type,is_spreadnum) is_spreadnum,(select is_name from ilog_source where is_id = ilog_spread.iss_id ) as is_name,is_spreadtype";
            strFieldShow += ",dbo.fn_GetSpreadContent(iso_id,io_id,is_spreadtype,is_isoriginal,is_type,is_content) as io_content, ";
            strFieldShow += " (select is_url from ilog_source where is_id = ilog_spread.iss_id ) as is_url,(select face from vip_ilog where userid = ilog_spread.userid ) as face ";
            strFieldShow += ",(CASE WHEN  is_spreadtype=0 and is_type=0 THEN (select io_haspic from ilog_original ori where ori.io_id = ilog_spread.io_id) ELSE 0 END) as is_haspic";
            string strFieldOrder = " is_id desc ";
            string strWhere = " ((is_spreadtype=0 AND is_type=0 AND is_isoriginal=1) OR (is_spreadtype=1 AND is_isoriginal=0))";
            strWhere += " AND intime >= convert(varchar(10),dateadd(day,-7,getdate()),120) and is_fanuserid = " + userid;

            //查询博文(fn_GetSpreadType:全部—1,3 博文—1 )
            strWhere += " and dbo.fn_GetSpreadType(is_spreadtype,is_type,is_isoriginal)=1";

            int RecordCount = 0;

            DataTable dblSearchList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            return Ilog.BLL.ILogSpread.GetJsonList(dblSearchList, RecordCount);
        }
        #endregion

        #region 首页博文
        /// <summary>
        /// 首页博文
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="userid">当前用户id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public static string GetSearchList(int PageCurrent, int PageSize, long userid, string keyword)
        {
            string strTableName = " ilog_original ";
            string strFieldKey = "io_id";
            string strFieldShow = " io_id,userid,(select nickname from vip_ilog where userid = ilog_original.userid) as nickname,intime,io_content,io_commentnum as vic_commentnum, ";
            strFieldShow += " io_spreadnum,(select is_name from ilog_source where is_id = ilog_original.is_id ) as is_name, (select username from vip_ilog where vip_ilog.userid = ilog_original.userid ) as username, ";
            strFieldShow += " (select is_url from ilog_source where is_id = ilog_original.is_id ) as is_url,(select face from vip_ilog where userid = ilog_original.userid ) as face,cw_type,io_haspic,io_spreadnum ";

            string strFieldOrder = " io_id desc ";
            string strWhere = " intime >= convert(varchar(10),dateadd(day,-7,getdate()),120) and userid = " + userid + " and io_content like'%" + keyword + "%' ";

            int RecordCount = 0;

            DataTable dblSearchList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            return GetJsonList(dblSearchList, RecordCount);
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


        #region 发表博文(分享)

        //public static string SendOriginalInfo(ILog.Model.ILogOriginal ooOriginal)
        //{

        //    //#region 废弃



        //    //StringBuilder result = new StringBuilder();


        //    //        //入参
        //    //        ILog.Model.ILogOriginal originalInfo = new ILog.Model.ILogOriginal();

        //    //        string userId = userJson["Userid"].ToString();
        //    //        string content = userJson["io_content"].ToString();

        //    //        //判断用户编号和内容非空
        //    //        if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(content))
        //    //        {

        //    //            result.Append("{state:0}");

        //    //        }
        //    //        else
        //    //        {


        //    //            if (content.Substring(content.IndexOf("@")) != "@" && content.ToLower().Contains("http://"))
        //    //            {

        //    //                #region 短地址，@粉丝都存在

        //    //                #region @粉丝



        //    //                #endregion

        //    //                #region 短地址操作

        //    //                string[] url = content.Split(' ');

        //    //                for (int i = 0; i < url.Length; i++)
        //    //                {

        //    //                    if (url[i].ToLower().Contains("http://"))
        //    //                    {

        //    //                        //按规则生成短地址
        //    //                        string encryUrl = url[i];

        //    //                        //判定短地址表是否存在数据

        //    //                        bool ret = false;

        //    //                        //不存在即插入新的短地址
        //    //                        if (!ret)
        //    //                        {



        //    //                        }

        //    //                    }

        //    //                }

        //    //                #endregion

        //    //                #endregion

        //    //            }
        //    //            else if (content.Substring(content.IndexOf("@")) != "@")
        //    //            {

        //    //                #region @粉丝信息


        //    //                #endregion

        //    //            }
        //    //            else if (content.Substring(content.ToLower().IndexOf("http://")) != "http://")
        //    //            {

        //    //                #region 短地址信息(分享多个地址插入多条记录)

        //    //                #endregion

        //    //            }
        //    //            else
        //    //            {

        //    //                #region 只发送内容

        //    //                originalInfo.userid = Convert.ToInt32(userJson["Userid"]);
        //    //                originalInfo.io_content = Convert.ToString(userJson["io_content"]);
        //    //                originalInfo.io_ip = HttpContext.Current.Request.UserHostAddress;
        //    //                originalInfo.intime = DateTime.Now;
        //    //                originalInfo.io_haspic = userJson["Io_ispic"].ToString() == "" ? false : Convert.ToBoolean(userJson["Io_ispic"]);
        //    //                originalInfo.is_id = userJson["Is_id"].ToString() == "" ? 0 : Convert.ToInt32(userJson["Is_id"]);
        //    //                originalInfo.cw_type = userJson["cw_type"].ToString() == "" ? 0 : Convert.ToInt32(userJson["cw_type"]);

        //    //                result.Append(Ilog.BLL.ILogOriginal.OriginalAdd(originalInfo));

        //    //                #endregion

        //    //            }

        //    //    return result.ToString();

        //    //}
        //    //catch (Exception ex)
        //    //{

        //    //    result.Length = 0;
        //    //        return result.Append("{state:0}").ToString();


        //    //#endregion

        //    return string.Empty;


        //}

        #endregion

        #region 处理微博内容，截取@，得到@用户username数组
        /// <summary>
        /// 功能描述：处理微博内容，截取@，得到@用户username数组
        /// 创建标识：ljd 20120608
        /// </summary>
        /// <param name="ilogContent"></param>
        /// <param name="dicUser"></param>
        /// <returns></returns>
        public static Dictionary<int, string> GetLoveItUserName(string ilogContent, Dictionary<int, string> dicUser)
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
             * 判断第一个@的内容是否是数字+英文+.的形式
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
                Regex regex = new Regex("[\u4E00-\u9FA5\uf900-\ufa2d]");
                //是否包含中文
                bool isSuccess = regex.IsMatch(pointContent);
                if (isSuccess)//包含中文，截取@到全角之间的内容
                {
                    username = pointContent;
                }
            }
            else if (pointIndex < 0)//不包含.
            {
                username = firstIlogContent;
            }
            if (username != "")
            {
                int key = dicUser.Count + 1;
                dicUser.Add(key, username);
            }
            if (secondIndex > 0)
            {
                GetLoveItUserName(secondIlogContent, dicUser);
            }
            return dicUser;

        }
        #endregion

        #region 处理微博内容，截取@，得到@用户唯一的username数组
        /// <summary>
        /// 功能描述：处理微博内容，截取@，得到@用户唯一的username数组
        /// 创建标识：ljd 20120608
        /// </summary>
        /// <param name="dicUserName">用户名数组</param>
        /// <returns></returns>
        public static Dictionary<int, string> GetLoveItDistinctUserName(Dictionary<int, string> dicUserName)
        {
            //不重复的username
            Dictionary<int, string> dicDistinctUsername = new Dictionary<int, string>();

            foreach (int key in dicUserName.Keys)
            {
                bool IsExists = false;
                foreach (int distinctKey in dicDistinctUsername.Keys)
                {
                    if (dicUserName[key] == dicDistinctUsername[distinctKey])
                    {
                        IsExists = true;
                    }
                }
                if (!IsExists)
                {
                    dicDistinctUsername.Add(key, dicUserName[key]);
                }
            }
            return dicDistinctUsername;

        }
        #endregion

        #region 处理微博内容，截取@，得到@用户userid数组
        /// <summary>
        /// 功能描述：处理微博内容，截取@，得到@用户userid数组
        /// 创建标识：ljd 20120609
        /// </summary>
        /// <param name="ilogContent">博文内容</param>
        /// <returns></returns>
        public static Dictionary<int, long> GetLoveItUserID(string ilogContent)
        {
            //@用户
            Dictionary<int, string> dicUserName = new Dictionary<int, string>();
            dicUserName = GetLoveItUserName(ilogContent, dicUserName);
            //唯一的@用户
            Dictionary<int, string> dicDistinctUserName = new Dictionary<int, string>();
            dicDistinctUserName = GetLoveItDistinctUserName(dicUserName);

            //不重复的username
            Dictionary<int, long> dicUserID = new Dictionary<int, long>();

            foreach (int key in dicDistinctUserName.Keys)
            {
                long userid = Ilog.BLL.VipILog.GetUserIDByNickName(dicDistinctUserName[key]);
                if (userid != 0)
                {
                    dicUserID.Add(key, userid);
                }
            }
            return dicUserID;

        }
        #endregion

        #region 处理微博内容，截取@，得到@用户userid数组
        /// <summary>
        /// 功能描述：处理微博内容，截取@，得到@用户userid数组
        /// 创建标识：ljd 20120609
        /// </summary>
        /// <param name="dicUserName">用户名数组</param>
        /// <returns></returns>
        public static Dictionary<int, long> GetLoveItUserID(Dictionary<int, string> dicUserName)
        {
            //userid
            Dictionary<int, long> dicUserID = new Dictionary<int, long>();

            foreach (int key in dicUserName.Keys)
            {
                long userid = Ilog.BLL.VipILog.GetUserIDByUserName(dicUserName[key]);
                if (userid != 0)
                {
                    dicUserID.Add(key, userid);
                }
            }
            return dicUserID;

        }
        #endregion

        #region 处理微博内容，截取url地址，并将其处理为短地址
        /// <summary>
        /// 功能描述：处理微博内容，截取url地址，并将其处理为短地址
        /// 创建标识：ljd 20120609
        /// </summary>
        /// <param name="ilogContent"></param>
        /// <param name="dicUrl"></param>
        /// <returns></returns>
        public static void OperateLoveItUrl(string ilogContent, ref string originalILog, ref int removedLen)
        {
            string url = "";

            //url开头标识
            string urlMark = "http://";

            //第一个http的位置
            int httpIndex = ilogContent.IndexOf("http://");

            //第一个https的位置
            int httpsIndex = ilogContent.IndexOf("https://");

            //第一个ftp的位置
            int ftpIndex = ilogContent.IndexOf("ftp://");

            //第一个http://https://ftp://后的内容
            string firstIlogContent = "";

            //url开头地址
            int startIndex = httpIndex;

            if (httpIndex >= 0 && httpsIndex < 0)//只有http://
            {
                startIndex = httpIndex;
                urlMark = "http://";
            }
            else if (httpIndex < 0 && httpsIndex >= 0)//只有https://
            {
                startIndex = httpsIndex;
                urlMark = "https://";
            }
            else
            {
                startIndex = httpIndex < httpsIndex ? httpIndex : httpsIndex;
                urlMark = httpIndex < httpsIndex ? "http://" : "https://";
            }

            if (startIndex < 0 && ftpIndex >= 0)//只有fpt://
            {
                startIndex = ftpIndex;
                urlMark = "ftp://";
            }
            else if (startIndex >= 0 && ftpIndex >= 0)
            {
                startIndex = startIndex < ftpIndex ? startIndex : ftpIndex;
                urlMark = startIndex < ftpIndex ? urlMark : "ftp://";
            }

            if (startIndex < 0)//不在存在任何url标识位
            {
                return;
            }
            //截取url标识之后的文本内容
            firstIlogContent = ilogContent.Substring(startIndex + urlMark.Length, ilogContent.Length - startIndex - urlMark.Length);
            //获取第一个空格的位置
            int firstSpaceIndex = firstIlogContent.IndexOf(" ");
            //全角字符索引位置
            int firstChineseIndex = -1;
            //判断剩余字符串中是否包含中文
            Regex regex = new Regex("[^\x00-\xff]");
            //是否包含中文
            bool isSuccess = regex.IsMatch(firstIlogContent);
            if (isSuccess)//包含中文
            {
                //遍历字符串，查找第一个全角字符位置
                for (int i = 0; i < firstIlogContent.Length; i++)
                {
                    if (regex.IsMatch(firstIlogContent[i].ToString()))
                    {
                        firstChineseIndex = i;
                        break;
                    }
                }
            }
            //第二个标志位
            int secondIndex = 0;
            if (firstSpaceIndex >= 0 && firstChineseIndex < 0)//存在空格
            {
                secondIndex = firstSpaceIndex;
            }
            else if (firstSpaceIndex < 0 && firstChineseIndex >= 0)//存在全角
            {
                secondIndex = firstChineseIndex;
            }
            else
            {
                secondIndex = firstSpaceIndex < firstChineseIndex ? firstSpaceIndex : firstChineseIndex;
            }
            if (secondIndex > 0)//如果存在第二个标志位
            {
                url = urlMark + firstIlogContent.Substring(0, secondIndex);
                //前一段ilog原文
                string prevOriginalILog = originalILog.Substring(0, startIndex + removedLen);
                //后一段ilog原文
                string nextOriginalILog = firstIlogContent.Substring(secondIndex, firstIlogContent.Length - secondIndex);

                ILog.Model.ILogShortUrl ooShortUrl = Ilog.BLL.ILogShortUrl.GetTheOneShortInfo(url, 0);

                //视频图标
                string videoImg = "";
                if (ooShortUrl.isu_type == 1)
                {
                    videoImg = "[img]http://simg.instrument.com.cn/ilog/blue/images/video.jpg[/img]";
                }
                //uub的url代码
                string uubUrl = string.Format("[url={0}]{1}[/url]{2}", ooShortUrl.isu_shorturl, ooShortUrl.isu_shorturl, videoImg);
                originalILog = string.Format("{0}{2}{1}", prevOriginalILog, nextOriginalILog, uubUrl);
                removedLen = prevOriginalILog.Length + uubUrl.Length;
                //截取url之后剩下的部分
                string secondIogContent = firstIlogContent.Substring(secondIndex, firstIlogContent.Length - secondIndex);
                OperateLoveItUrl(secondIogContent, ref originalILog, ref removedLen);
            }
            else
            {
                if (startIndex >= 0)//只有第一个标识
                {
                    ILog.Model.ILogShortUrl ooShortUrl = Ilog.BLL.ILogShortUrl.GetTheOneShortInfo(urlMark + firstIlogContent, 0);

                    //视频图标
                    string videoImg = "";
                    if (ooShortUrl.isu_type == 1)
                    {
                        videoImg = "[img]http://simg.instrument.com.cn/ilog/blue/images/video.jpg[/img]";
                    }

                    //前一段ilog原文
                    string prevOriginalILog = originalILog.Substring(0, startIndex + removedLen);

                    //uub的url代码
                    string uubUrl = string.Format("[url={0}]{1}[/url]{2}", ooShortUrl.isu_shorturl, ooShortUrl.isu_shorturl, videoImg);
                    originalILog = string.Format("{0}{1}", prevOriginalILog, uubUrl);
                }
            }

            return;

        }
        #endregion

        #region 发表原创
        /// <summary>
        /// 功能描述：发表原创
        /// 创建标识：ljd 20120610
        /// </summary>
        /// <param name="ooOriginal">原创实体对象</param>
        /// <param name="mark">原创标记</param>
        /// <returns></returns>
        public static string SendOriginal(ILog.Model.ILogOriginal ooOriginal, string mark)
        {
            int urlstate = 0;
            //博文内容
            string content = Microsoft.JScript.GlobalObject.unescape(ooOriginal.io_content);
            //获取@用户id
            Dictionary<int, long> dicUserID = GetLoveItUserID(content);
            //处理博文时记录的移除字符串的长度
            int removeLen = 0;
            //处理博文内容，将长地址转为短地址
            OperateLoveItUrl(content, ref content, ref removeLen);
            //将表情转为ubb代码
            content = GetIlogContentByExpression(content);
            //生成一条原创记录
            ooOriginal.io_content = content;
            ooOriginal.intime = DateTime.Now;
            ooOriginal.is_id = 1;//来自ilog

            ooOriginal.io_id = Ilog.BLL.ILogOriginal.OriginalAddAndGetID(ooOriginal, ref urlstate);
            //如果有图片，更新图片表标识
            if (ooOriginal.io_haspic && mark != "")
            {
                Ilog.BLL.ILogPic.UpdatePicOriginalIDByMark(mark, ooOriginal.io_id, ref urlstate);
            }
            //如果存在@用户，加入@信息传播表
            if (dicUserID.Count > 0)
            {
                foreach (int key in dicUserID.Keys)
                {
                    ILog.Model.ILogat ooAt = new ILog.Model.ILogat();
                    ooAt.ia_atuserid = ooOriginal.userid;
                    ooAt.ia_content = ooOriginal.io_content;
                    ooAt.ia_type = 0;//微博
                    ooAt.intime = DateTime.Now;
                    ooAt.iso_id = ooOriginal.io_id;
                    ooAt.userid = dicUserID[key];
                    ooAt.ia_logid = ooOriginal.io_id;
                    ooAt.is_id = 1;//ilog

                    Ilog.BLL.ILogat.AtInfoAdd(ooAt);
                    //更新@用户@数
                    Ilog.BLL.VipILogCount.UpdateAtNum(dicUserID[key], ref urlstate);
                }
            }

            //给用户所有的粉丝加信息传播记录
            //获取用户所有粉丝列表
            List<long> fansList = Ilog.BLL.ILogUserFan.GetUserFanListByUserid(ooOriginal.userid, ref urlstate);
            if (fansList.Count > 0)
            {
                foreach (long fansuserid in fansList)
                {
                    //新增信息传播表记录
                    ILog.Model.ILogSpread ooSpread = new ILog.Model.ILogSpread();
                    ooSpread.intime = DateTime.Now;
                    ooSpread.is_fanuserid = fansuserid;
                    ooSpread.is_type = 0;
                    ooSpread.iso_id = ooOriginal.io_id;
                    ooSpread.userid = ooOriginal.userid;
                    ooSpread.io_id = ooOriginal.io_id;
                    ooSpread.is_spreadtype = 0;
                    ooSpread.is_content = "";
                    ooSpread.is_isoriginal = 0;//非原创
                    ooSpread.iss_id = 1;//来自ilog

                    Ilog.BLL.ILogSpread.SpreadAdd(ooSpread);
                }
            }

            //给自己加一条传播记录
            ILog.Model.ILogSpread ooSelfSpread = new ILog.Model.ILogSpread();
            ooSelfSpread.intime = DateTime.Now;
            ooSelfSpread.is_fanuserid = ooOriginal.userid;
            ooSelfSpread.is_type = 0;
            ooSelfSpread.iso_id = ooOriginal.io_id;
            ooSelfSpread.userid = ooOriginal.userid;
            ooSelfSpread.io_id = ooOriginal.io_id;
            ooSelfSpread.is_spreadtype = 0;
            ooSelfSpread.is_content = "";
            ooSelfSpread.is_isoriginal = 1;//原创
            ooSelfSpread.iss_id = 1;//来自ilog

            Ilog.BLL.ILogSpread.SpreadAdd(ooSelfSpread);

            //更新自己的博文数量加1
            ILog.DAL.VipILogCount.UpdateLogCount(ooOriginal.userid);

            //成功标记
            StringBuilder strbResult = new StringBuilder();

            strbResult.Append("{");
            strbResult.Append("state:" + urlstate);
            strbResult.Append("}");

            return strbResult.ToString();

        }
        #endregion


        #region 判断用户输入的内容是否已存在,返回发表时间
        /// <summary>
        /// 功能描述：判断用户输入的内容是否已存在,返回发表时间
        /// 创建标识：ljd 20120612
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="content">博文内容</param>
        /// <param name="urlState">是否报错</param>
        /// <returns></returns>
        public static DateTime GetSendTime(long userid, string content, ref int urlState)
        {
            DateTime dtSendTime = ILog.DAL.ILogOriginal.GetSendTime(userid, content, ref urlState);
            return dtSendTime;

        }

        #endregion

        #region 判断用户输入的内容是否已存在,并且发送时间在十分钟之外
        /// <summary>
        /// 功能描述：判断用户输入的内容是否已存在,并且发送时间在十分钟之外
        /// 创建标识：ljd 20120612
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="content">博文内容</param>
        /// <param name="urlState">是否报错</param>
        /// <returns>0 10分钟内重复发表 1可以发表</returns>
        public static int IsPermitSendOriginal(long userid, string content)
        {
            int urlstate = 0;

            DateTime dtSentTime = GetSendTime(userid, content, ref urlstate);

            TimeSpan tsSendedTime = DateTime.Now - dtSentTime;

            if (tsSendedTime.TotalMinutes > 10)
            {
                urlstate = 1;
            }
            else
            {
                urlstate = 0;
            }

            return urlstate;

        }

        #endregion

        #region 更新原创的转发次数
        /// <summary>
        /// 功能描述：更新原创的转发次数
        /// 创建标识：ljd 20120613
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="urlsate"></param>
        /// <returns></returns>
        public static int UpdateSpreadNum(long id)
        {
            int resultCount = ILog.DAL.ILogOriginal.UpdateSpreadNum(id);
            return resultCount;

        }
        #endregion

        #region 更新原创的评论次数
        /// <summary>
        /// 功能描述：更新原创的评论次数
        /// 创建标识：ljd 20120628
        /// </summary>
        /// <param name="id">博文id</param>
        /// <returns></returns>
        public static int UpdateCommentNum(long id)
        {
            int resultCount = ILog.DAL.ILogOriginal.UpdateCommentNum(id);
            return resultCount;

        }
        #endregion

        #region 将用户表情封装到字典里
        /// <summary>
        /// 功能描述：将用户表情封装到字典里
        /// 创建标识：ljd 20120618
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetDicExpression()
        {
            Dictionary<string, string> dicExpression = new Dictionary<string, string>();
            dicExpression.Add("微笑", "weixiao.gif");
            dicExpression.Add("撇嘴", "pizui.gif");
            dicExpression.Add("色", "se.gif");
            dicExpression.Add("发呆", "fadai.gif");
            dicExpression.Add("得意", "deyi.gif");
            dicExpression.Add("流泪", "liulei.gif");
            dicExpression.Add("害羞", "haixiu.gif");
            dicExpression.Add("闭嘴", "bizui.gif");
            dicExpression.Add("睡", "shuijiao.gif");
            dicExpression.Add("大哭", "daku.gif");
            dicExpression.Add("尴尬", "gangga.gif");
            dicExpression.Add("发怒", "fanu.gif");
            dicExpression.Add("调皮", "tiaopi.gif");
            dicExpression.Add("呲牙", "ciya.gif");
            dicExpression.Add("惊讶", "jingya.gif");
            dicExpression.Add("难过", "nanguo.gif");
            dicExpression.Add("酷", "ku.gif");
            dicExpression.Add("冷汗", "lenghan.gif");
            dicExpression.Add("抓狂", "zhuakuang.gif");
            dicExpression.Add("吐", "tu.gif");
            dicExpression.Add("偷笑", "touxiao.gif");
            dicExpression.Add("可爱", "keai.gif");
            dicExpression.Add("白眼", "baiyan.gif");
            dicExpression.Add("傲慢", "aoman.gif");
            dicExpression.Add("饥饿", "er.gif");
            dicExpression.Add("困", "kun.gif");
            dicExpression.Add("惊恐", "jingkong.gif");
            dicExpression.Add("流汗", "liuhan.gif");
            dicExpression.Add("憨笑", "haha.gif");
            dicExpression.Add("大兵", "dabing.gif");
            dicExpression.Add("奋斗", "fendou.gif");
            dicExpression.Add("咒骂", "ma.gif");
            dicExpression.Add("疑问", "wen.gif");
            dicExpression.Add("嘘", "xu.gif");
            dicExpression.Add("晕", "yun.gif");
            dicExpression.Add("折磨", "zhemo.gif");
            dicExpression.Add("衰", "shuai.gif");
            dicExpression.Add("骷髅", "kulou.gif");
            dicExpression.Add("敲打", "da.gif");
            dicExpression.Add("再见", "zaijian.gif");
            dicExpression.Add("擦汗", "cahan.gif");
            dicExpression.Add("抠鼻", "wabi.gif");
            dicExpression.Add("鼓掌", "guzhang.gif");
            dicExpression.Add("糗大了", "qioudale.gif");
            dicExpression.Add("坏笑", "huaixiao.gif");
            dicExpression.Add("左哼哼", "zuohengheng.gif");
            dicExpression.Add("右哼哼", "youhengheng.gif");
            dicExpression.Add("哈欠", "haqian.gif");
            dicExpression.Add("鄙视", "bishi.gif");
            dicExpression.Add("委屈", "weiqu.gif");
            dicExpression.Add("快哭了", "kuaikule.gif");
            dicExpression.Add("阴险", "yinxian.gif");
            dicExpression.Add("亲亲", "qinqin.gif");
            dicExpression.Add("吓", "xia.gif");
            dicExpression.Add("可怜", "kelian.gif");
            dicExpression.Add("菜刀", "caidao.gif");
            dicExpression.Add("西瓜", "xigua.gif");
            dicExpression.Add("啤酒", "pijiu.gif");
            dicExpression.Add("篮球", "lanqiu.gif");
            dicExpression.Add("乒乓", "pingpang.gif");
            dicExpression.Add("咖啡", "kafei.gif");
            dicExpression.Add("饭", "fan.gif");
            dicExpression.Add("猪头", "zhutou.gif");
            dicExpression.Add("玫瑰", "hua.gif");
            dicExpression.Add("凋谢", "diaoxie.gif");
            dicExpression.Add("示爱", "kiss.gif");
            dicExpression.Add("爱心", "love.gif");
            dicExpression.Add("心碎", "xinsui.gif");
            dicExpression.Add("蛋糕", "dangao.gif");
            dicExpression.Add("闪电", "shandian.gif");
            dicExpression.Add("炸弹", "zhadan.gif");
            dicExpression.Add("刀", "dao.gif");
            dicExpression.Add("足球", "zuqiu.gif");
            dicExpression.Add("瓢虫", "chong.gif");
            dicExpression.Add("便便", "dabian.gif");
            dicExpression.Add("月亮", "yueliang.gif");
            dicExpression.Add("太阳", "taiyang.gif");
            dicExpression.Add("礼物", "liwu.gif");
            dicExpression.Add("拥抱", "yongbao.gif");
            dicExpression.Add("强", "qiang.gif");
            dicExpression.Add("弱", "ruo.gif");
            dicExpression.Add("握手", "woshou.gif");
            dicExpression.Add("胜利", "shengli.gif");
            dicExpression.Add("抱拳", "peifu.gif");
            dicExpression.Add("勾引", "gouyin.gif");
            dicExpression.Add("拳头", "quantou.gif");
            dicExpression.Add("差劲", "chajin.gif");
            dicExpression.Add("给力", "geili.gif");
            dicExpression.Add("NO", "no.gif");
            dicExpression.Add("OK", "ok.gif");
            dicExpression.Add("干杯", "cheer.gif");
            dicExpression.Add("飞吻", "feiwen.gif");
            dicExpression.Add("跳跳", "tiao.gif");
            dicExpression.Add("发抖", "fadou.gif");
            dicExpression.Add("怄火", "dajiao.gif");
            dicExpression.Add("转圈", "zhuanquan.gif");
            dicExpression.Add("磕头", "ketou.gif");
            dicExpression.Add("回头", "huitou.gif");
            dicExpression.Add("跳绳", "tiaosheng.gif");
            dicExpression.Add("挥手", "huishou.gif");
            dicExpression.Add("激动", "jidong.gif");
            dicExpression.Add("街舞", "tiaowu.gif");
            dicExpression.Add("献吻", "xianwen.gif");
            dicExpression.Add("左太极", "zuotaiji.gif");
            dicExpression.Add("右太极", "youtaiji.gif");


            return dicExpression;

        }
        #endregion

        #region 处理博文中的表情
        /// <summary>
        /// 功能描述：处理博文中的表情
        /// 创建标识：ljd 20120618
        /// </summary>
        /// <param name="ilogcontent">博文内容</param>
        /// <returns>处理用户表情显示后得到的博文内容</returns>
        public static string GetIlogContentByExpression(string ilogcontent)
        {
            Dictionary<string, string> dicExpression = GetDicExpression();

            string[] ilogArray = ilogcontent.Split(new char[] { '[' });
            foreach (string strilog in ilogArray)
            {
                int lastIndex = strilog.IndexOf(']');
                if (lastIndex >= 0)
                {
                    string strExpression = strilog.Substring(0, lastIndex);
                    if (dicExpression.ContainsKey(strExpression))
                    {
                        ilogcontent = ilogcontent.Replace(string.Format("[{0}]", strExpression),
                             string.Format("[img=24,24]http://simg.instrument.com.cn/ilog/head/{0}[/img]", dicExpression[strExpression]));
                    }
                }
            }

            return ilogcontent;

        }
        #endregion

        #region 将博文中的表情href转为文字
        /// <summary>
        /// 功能描述：将博文中的表情href转为文字
        /// 创建标识：ljd 20120701
        /// </summary>
        /// <param name="ilogcontent">博文内容</param>
        /// <returns>处理用户表情链接后显示后得到的博文内容</returns>
        public static string GetIlogContentWithExpression(string ilogcontent)
        {
            Dictionary<string, string> dicExpression = GetDicExpression();

            //截取url
            int index = ilogcontent.IndexOf("http://simg.instrument.com.cn/ilog/head/");
            if (index < 0)
            {
                return ilogcontent;
            }
            //截取这之前的内容
            string prevContent = ilogcontent.Substring(0, index);
            //截取这之后的内容
            string nextContent = ilogcontent.Substring(index, ilogcontent.Length - index);
            //获取.gif的位置
            int gifindex = nextContent.IndexOf(".gif");
            //截取图片的全称
            string imgFullName = nextContent.Substring(0, gifindex) + ".gif";
            //截取图片的名称
            string imgName = imgFullName.Replace("http://simg.instrument.com.cn/ilog/head/", "");

            if (dicExpression.ContainsValue(imgName))
            {
                foreach (string key in dicExpression.Keys)
                {
                    if (dicExpression[key] == imgName)
                    {
                        ilogcontent = ilogcontent.Replace(imgFullName, "[" + key + "]").Replace("[/img]", "").Replace("[img=24,24]", "");
                    }
                }
            }

            return GetIlogContentWithExpression(ilogcontent);

        }
        #endregion

        #region 清除ubb中的url并保留短地址文字
        /// <summary>
        /// 功能描述：清除ubb中的url并保留短地址文字
        /// 创建标识：ljd 20120717
        /// </summary>
        /// <param name="sDetail"></param>
        /// <returns></returns>
        public static string ClearUbbUrl(string sDetail)
        {
            sDetail = Regex.Replace(sDetail, @"\[url\]\s*(((?!"")[\s\S])+?)(?:""[\s\S]*?)?\s*\[\/url\]", "$1");
            sDetail = Regex.Replace(sDetail, @"\[url\s*=\s*([^\]""]+?)(?:""[^\]]*?)?\s*\]\s*([\s\S]+?)\s*\[\/url\]", "$2");

            if (sDetail.Contains("www.") || sDetail.Contains("bbs."))
            {
                sDetail = Regex.Replace(sDetail, @"(^|[^\/\\\w\=])((www|bbs)\.(\w)+\.([\w\/\\\.\=\?\+\-~`@\'!%#]|(&amp;))+)", "$1$2");
            }

            return sDetail;

        }
        #endregion


        #region 得到清除uub代码并且将图片转为表情图片的博文内容
        /// <summary>
        /// 功能描述：得到清除uub代码并且将图片转为表情图片的博文内容
        /// 创建标识：ljd 20120717
        /// </summary>
        /// <param name="ilogcontent">博文内容</param>
        /// <param name="len">截取字符串长度</param>
        /// <param name="replaceString">替换字符串</param>
        /// <returns>处理后的博文</returns>
        public static string GetClearUUBAndChangeExpression(string ilogcontent, int len, string replaceString)
        {
            //清除视频标记
            ilogcontent = ilogcontent.Replace("[img]http://simg.instrument.com.cn/ilog/blue/images/video.jpg[/img]", "");
            //表情转为汉字
            ilogcontent = GetIlogContentWithExpression(ilogcontent);
            //清除ubb代码
            ilogcontent = ClearUbbUrl(ilogcontent);
            if (len > 0)//截取长度
            {
                ilogcontent = Utils.GetUnicodeSubString(ilogcontent, len, replaceString);
            }
            //将表情文字转为uub链接
            ilogcontent = GetIlogContentByExpression(ilogcontent);
            //UBB转为html
            ilogcontent = UBBToHtml.UBBToHTML(GetIlogContentByExpression(ilogcontent));
            return ilogcontent;

        }
        #endregion

        #region 得到最终显示的博文内容
        /// <summary>
        /// 功能描述：得到最终显示的博文内容
        /// 创建标识：ljd 20120618
        /// </summary>
        /// <param name="ilogContent">博文内容</param>
        /// <param name="ilogid">博文id</param>
        /// <returns></returns>
        public static string GetFinalIlogContentShow(string ilogContent, long ilogid)
        {
            //移除的长度
            int removelength = 0;

            //@用户个数
            int usercount = 0;

            Ilog.BLL.VipILog.GetIlogContentShow(ilogContent, ref ilogContent, ref removelength, ilogid, ref usercount);

            string ilogFinalContent = UBBToHtml.UBBToHTML(ilogContent);

            return ilogFinalContent;

        }

        #endregion

        #region 得到热门评论的原创列表
        /// <summary>
        /// 功能描述：得到热门评论的原创列表
        /// 创建标识：ljd 20120626
        /// </summary>
        /// <returns></returns>
        public static List<ILog.Model.ILogOriginal> GetHotCommentOriginalList()
        {
            List<ILog.Model.ILogOriginal> originalList = ILog.DAL.ILogOriginal.GetHotCommentOriginalList();
            return originalList;

        }
        #endregion

        #region 得到热门转发的原创列表
        /// <summary>
        /// 功能描述：得到热门转发的原创列表
        /// 创建标识：ljd 20120716
        /// </summary>
        /// <returns></returns>
        public static List<ILog.Model.ILogOriginal> GetHotSpreadOriginalList()
        {
            List<ILog.Model.ILogOriginal> originalList = ILog.DAL.ILogOriginal.GetHotSpreadOriginalList();
            return originalList;

        }
        #endregion

        #region 得到热门评论的原创列表（json格式）
        /// <summary>
        /// 功能描述：得到热门评论的原创列表（json格式）
        /// 创建标识：ljd 20120626
        /// </summary>
        /// <returns></returns>
        public static string GetHotCommentOriginalListJsonStr()
        {
            List<ILog.Model.ILogOriginal> originalList = new List<ILog.Model.ILogOriginal>();


            StringBuilder strbCommentList = new StringBuilder();

            strbCommentList.Append("{HotCommentList:[");

            try
            {
                originalList = GetHotCommentOriginalList();

                if (originalList.Count == 0)
                {
                    strbCommentList.Append("{State:'0'}]}");//无数据
                }
                else
                {
                    strbCommentList.Append("{State:'1'},");

                    foreach (ILog.Model.ILogOriginal ooOriginal in originalList)
                    {
                        ILog.Model.VipILog ooIlog = BLL.VipILog.GetModelByUserID(ooOriginal.userid);

                        ILog.Model.ILogSpread ooSpread = Ilog.BLL.ILogSpread.GetSpreadOriginalInfo(ooOriginal.io_id);

                        if (ooIlog == null || ooSpread == null)
                        {
                            continue;
                        }

                        strbCommentList.Append("{id:'" + ooOriginal.io_id + "',content:'" + Common.GetJScriptGlobalObjectEscape(GetClearUUBAndChangeExpression(ooOriginal.io_content, 0, "")) + "',commentnum:'" + ooOriginal.io_commentnum
                            + "',level:'" + ooIlog.vi_memberlevel + "',face:'images/face/small/" + ooIlog.face + "',nickname:'" + ooIlog.nickname + "',userid:'" + ooIlog.userid
                           + "',iso_id:" + ooSpread.is_id + "},");
                    }
                    strbCommentList.Remove(strbCommentList.Length - 1, 1);
                    strbCommentList.Append("]}");
                }
            }
            catch
            {
                strbCommentList.Append("{State:'-1'}]}");//报错
            }
            return strbCommentList.ToString();

        }
        #endregion

        #region 得到热门转发的原创列表（json格式）
        /// <summary>
        /// 功能描述：得到热门转发的原创列表（json格式）
        /// 创建标识：ljd 20120716
        /// </summary>
        /// <returns></returns>
        public static string GetHotSpreadOriginalListJsonStr()
        {
            List<ILog.Model.ILogOriginal> originalList = new List<ILog.Model.ILogOriginal>();


            StringBuilder strbSpreadList = new StringBuilder();

            strbSpreadList.Append("{HotSpreadList:[");

            try
            {
                originalList = GetHotSpreadOriginalList();

                if (originalList.Count == 0)
                {
                    strbSpreadList.Append("{State:'0'}]}");//无数据
                }
                else
                {
                    strbSpreadList.Append("{State:'1'},");

                    foreach (ILog.Model.ILogOriginal ooOriginal in originalList)
                    {
                        ILog.Model.VipILog ooIlog = BLL.VipILog.GetModelByUserID(ooOriginal.userid);

                        ILog.Model.ILogSpread ooSpread = Ilog.BLL.ILogSpread.GetSpreadOriginalInfo(ooOriginal.io_id);

                        strbSpreadList.Append("{id:'" + ooOriginal.io_id + "',content:'"
                            + Common.GetJScriptGlobalObjectEscape(GetClearUUBAndChangeExpression(ooOriginal.io_content, 0, "")) + "',spreadnum:'" + ooOriginal.io_spreadnum
                            + "',level:'" + ooIlog.vi_memberlevel + "',face:'images/face/small/" + ooIlog.face + "',nickname:'" + ooIlog.nickname + "',userid:'"
                            + ooIlog.userid + "',iso_id:" + ooSpread.is_id + "},");
                    }
                    strbSpreadList.Remove(strbSpreadList.Length - 1, 1);
                    strbSpreadList.Append("]}");
                }
            }
            catch
            {
                strbSpreadList.Append("{State:'-1'}]}");//报错
            }
            return strbSpreadList.ToString();

        }
        #endregion

        #region 得到用户最新的原创信息
        /// <summary>
        /// 功能描述：得到用户最新的原创信息
        /// 创建标识：ljd 20120627
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static ILog.Model.ILogOriginal GetLastestOriginalInfo(long userid)
        {
            ILog.Model.ILogSpread ooSpread = Ilog.BLL.ILogSpread.GetLastestILogInfo(userid);

            ILog.Model.ILogOriginal ooOriginal = new ILog.Model.ILogOriginal();
            if (ooSpread != null)
            {
                //清除表情与视频的内容
                ooOriginal.io_content = Regex.Replace(ooSpread.is_content, @"\[img=(\d*|),(\d*|)\](.[^\]]*)(\[\/img\])", "");
                //清除uub代码
                ooOriginal.io_content = Utils.ClearUBB(ooOriginal.io_content);
                ooOriginal.io_content = ooOriginal.io_content.Replace("http://simg.instrument.com.cn/ilog/blue/images/video.jpg", "");

                ooOriginal.intime = ooSpread.intime;
                ooOriginal.io_id = ooSpread.is_id;
            }
            else
            {
                ooOriginal = null;
            }
            return ooOriginal;

        }
        #endregion

        #region 得到原创信息的json字符串
        /// <summary>
        /// 功能描述：得到原创信息的json字符串
        /// 创建标识：ljd 20120628
        /// </summary>
        /// <param name="io_id"></param>
        /// <returns></returns>
        public static string GetOriginalInfoJsonStr(long io_id)
        {
            StringBuilder strbOriginal = new StringBuilder();

            try
            {
                ILog.Model.ILogOriginal ooOriginal = Ilog.BLL.ILogOriginal.GetOriginalInfo(io_id);

                strbOriginal.Append("{Original:[");
                if (ooOriginal == null)
                {
                    strbOriginal.Append("{State:'0'}]}");
                    return strbOriginal.ToString();
                }

                else
                {
                    //点击来源
                    string source = "";
                    //来源url
                    string sourceUrl = "";

                    ILog.Model.ILogSource ooSource = Ilog.BLL.ILogSource.GetModelById(ooOriginal.is_id);
                    if (ooSource != null)
                    {
                        source = ooSource.is_name;
                        sourceUrl = ooSource.is_url;
                    }

                    ILog.Model.VipILog ooIlog = Ilog.BLL.VipILog.GetModelByUserID(ooOriginal.userid);

                    string content = ooOriginal.io_content;

                    content = GetFinalIlogContentShow(content, ooOriginal.io_id);

                    strbOriginal.Append("{State:'1',content:'" + Common.GetJScriptGlobalObjectEscape(content) + "',intime:'" + ILog.Common.Common.GetIlogTime(ooOriginal.intime) + "',source:'"
                        + source + "',sourceurl:'" + sourceUrl + "',nickname:'" + ooIlog.nickname + "',face:'" + ooIlog.face + "',memberlevel:'" + ooIlog.vi_memberlevel
                        + "',spreadnum:'" + ooOriginal.io_spreadnum + "',commentnum:'" + ooOriginal.io_commentnum + "',haspic:'" + ooOriginal.io_haspic + "',");

                    ILog.Model.ILogSpread ooSpread = Ilog.BLL.ILogSpread.GetSpreadOriginalInfo(ooOriginal.io_id);

                    strbOriginal.Append("spreadid:" + ooSpread.is_id + ",");

                    strbOriginal.Append(" userid:'" + ooOriginal.userid + "'}");

                    if (ooOriginal.io_haspic)
                    {
                        List<ILog.Model.ILogPic> picList = Ilog.BLL.ILogPic.GetPicList(ooOriginal.io_id);
                        if (picList.Count > 0)
                        {
                            strbOriginal.Append(",");
                            foreach (ILog.Model.ILogPic ooPic in picList)
                            {
                                strbOriginal.Append("{picid:'" + ooPic.ip_id + "',picname:'" + ooPic.ip_name + "'},");
                            }
                            strbOriginal.Remove(strbOriginal.Length - 1, 1);
                        }
                    }
                }
                strbOriginal.Append("]}");
            }
            catch
            {
                strbOriginal.Append("{State:'0'}");
            }

            return strbOriginal.ToString();

        }
        #endregion

        /// <summary>
        /// 功能描述： 正在发生的事
        /// /// 创建标识：zhangl  20120712
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="type">0 每日 1每周</param>
        /// <returns></returns>
        public static DataTable GetTopdaySpreadList(int PageCurrent, int PageSize, ref int RecordCount)
        {
            return ILog.DAL.ILogOriginal.GetTopdaySpreadList(PageCurrent, PageSize, ref RecordCount);
        }


        #region 得到正在发生的事（最新原创博文列表）
        /// <summary>
        /// 功能描述：得到热门评论的博文列表的json字符串
        /// 创建标识：zhangl 20120712
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="type">0 每日 1每周</param>
        /// <returns></returns> 
        public static string GetTodaySpreadJsonList(int PageCurrent, int PageSize)
        {
            int RecordCount = 0;

            DataTable List = GetTopdaySpreadList(PageCurrent, PageSize, ref RecordCount);

            int count = List.Rows.Count;

            int RowsCount = 45 * RecordCount;

            StringBuilder strPageList = new StringBuilder();

            strPageList.Append("{List:");

            try
            {
                if (count > 0)
                {
                    strPageList.Append("[{State:'1'}");

                    if (RecordCount == 1)     //如果有搜索结果只有一页那么就显示页数
                    {
                        strPageList.Append(",{RowsCount:'" + count + "'},");
                    }
                    else
                    {
                        strPageList.Append(",{RecordCount:'" + (RecordCount > 10 ? 10 : RecordCount) + "'},");  //所有列表页最多显示10页
                    }

                    for (int i = 0; i < count; i++)
                    {
                        ILog.Model.ILogSpread ooSpread = Ilog.BLL.ILogSpread.GetSpreadOriginalInfo(Convert.ToInt64(List.Rows[i]["io_id"]));

                        long userid = Convert.ToInt64(List.Rows[i]["userid"]);

                        strPageList.Append("{io_id:\"" + List.Rows[i]["io_id"].ToString() + "\",userid:\"" + List.Rows[i]["userid"].ToString() + "\",");
                        strPageList.Append("is_url:\"" + List.Rows[i]["is_url"].ToString() + "\",");
                        strPageList.Append("io_spreadnum:\"" + List.Rows[i]["io_spreadnum"].ToString() + "\",io_commentnum:\"" + List.Rows[i]["io_commentnum"].ToString() + "\",");
                        strPageList.Append("io_haspic:\"" + List.Rows[i]["io_haspic"].ToString() + "\",iso_id:\"" + ooSpread.is_id + "\",");

                        ILog.Model.VipILog ooVip = Ilog.BLL.VipILog.GetModelByUserID(userid);

                        strPageList.Append("nickname:\"" + ooVip.nickname + "\",face:\"" + ooVip.face + "\",memberlevel:\"" + ooVip.vi_memberlevel + "\",");

                        //查看发@的时间是不是当天
                        strPageList.Append("intime:\"" + ILog.Common.Common.GetIlogTime(Convert.ToDateTime(List.Rows[i]["intime"])) + "\",");

                        strPageList.Append("io_content:\""
                            + ILog.Common.Common.GetJScriptGlobalObjectEscape(Ilog.BLL.ILogOriginal.GetFinalIlogContentShow(List.Rows[i]["io_content"].ToString(), Convert.ToInt64(List.Rows[i]["io_id"]))) + "\",");

                        strPageList.Append("is_name:\"" + List.Rows[i]["is_name"].ToString() + "\"}");

                        //最后一个元素不需要加逗号
                        if ((count - 1) > i)
                        {
                            strPageList.Append(",");
                        }
                    }
                }
                else
                {
                    strPageList.Append("[{State:'2'},{RowsCount:'0'}");  //无数据不
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
    }
}
