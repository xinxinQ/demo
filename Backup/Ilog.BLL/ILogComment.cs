using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;



namespace Ilog.BLL
{
    public class ILogComment
    {
        #region 查看某条评论信息是否存在（True：存在，False：不存在）
        /// <summary>
        /// 查看某条评论信息是否存在（True：存在，False：不存在）
        /// <param name="ic_id">流水号</param>
        /// </summary>
        public static string ILogCommentExists(int ic_id)
        {
            StringBuilder strILogCommentExists = new StringBuilder();

            strILogCommentExists.Append("var ILogCommentExistsJsonObject = ");
            strILogCommentExists.Append("({");
            strILogCommentExists.Append("\"Exists\": \"" + ILog.DAL.ILogComment.ILogCommentExists(ic_id).ToString() + "\"");
            strILogCommentExists.Append("})");

            return strILogCommentExists.ToString();
        }
        #endregion

        #region  增加一条数据
        /// <summary>
        /// 增加一条数据
        /// <param name="model">评论表实体</param>
        /// </summary>
        public static string CommentAdd(ILog.Model.ILogComment model)
        {
            StringBuilder strCommentAdd = new StringBuilder();

            strCommentAdd.Append("var CommentAddJsonObject = ");
            strCommentAdd.Append("({");
            strCommentAdd.Append("\"state\": \"" + ILog.DAL.ILogComment.CommentAdd(model).ToString() + "\"");
            strCommentAdd.Append("})");

            return strCommentAdd.ToString();
        }
        #endregion


        #region 删除一条评论数据
        /// <summary>
        /// 删除一条评论数据
        /// <param name="ic_id">流水号</param>
        /// <param name="userid">用户id</param>
        /// </summary>
        public static string CommentDel(long ic_id, long userid)
        {
            int resultCount = ILog.DAL.ILogComment.CommentDel(ic_id, userid);

            if (resultCount > 0)
            {
                resultCount = 1;
            }
            else
            {
                resultCount = 0;
            }

            StringBuilder strCommentDel = new StringBuilder();

            //strCommentDel.Append("var CommentDelJsonObject = ");
            strCommentDel.Append("{");
            strCommentDel.Append("\"state\": \"" + resultCount.ToString() + "\"");
            strCommentDel.Append("}");

            return strCommentDel.ToString();
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="ic_id">流水号</param>
        /// </summary>
        public static string GetModel(int ic_id)
        {
            DataTable dblModelList = ILog.DAL.ILogComment.GetModel(ic_id);

            //构建josn字符串 
            string ILogAtModelJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblModelList).ToString();

            return ILogAtModelJosn;
        }
        #endregion

        #region 分页（json节点见表字段）
        /// <summary>
        /// 数据分页
        /// </summary>
        /// <param name="RecordCount">当前页码</param>
        /// <param name="PageSize">每页数据</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="ation">查看类型（1：收到的评论，0：发出的评论 ）</param>
        /// <param name="ix_id">原创id，转发id</param>
        /// <returns></returns>
        //public static string GetCommentList(int PageCurrent, int PageSize, string ation, long ix_id)
        //{
        //    string strTableName = " ilog_comment ";
        //    string strFieldKey = "ic_id";
        //    string strFieldShow = "";

        //    if (ation == "0")   //发出的评论
        //    {
        //        strFieldShow = " ic_id,userid,ic_content,intime,ic_currentid,ic_currentuserid,(select nickname from vip_ilog where vip_ilog.userid = ilog_comment.userid) as nickname ";
        //    }
        //    else               //收到的评论
        //    {
        //        strFieldShow = " ic_id,userid,ic_content,intime,ic_currentid,ic_currentuserid,(select nickname from vip_ilog where vip_ilog.userid = ilog_comment.ic_currentuserid) as nickname ";
        //    }

        //    strFieldShow += " ,(select face from vip_ilog where vip_ilog.userid = ilog_comment.userid) as face,(SELECT is_name FROM ilog_source WHERE ilog_source.is_id = ilog_comment.is_id) as is_name,is_id  ";
        //    string strFieldOrder = " ic_id desc ";
        //    string strWhere = " ic_currentuserid = " + userid;

        //    //收到评论
        //    if (ation == "1")
        //    {
        //        strWhere = " ic_currentuserid = " + userid;
        //    }
        //    else //发出评论
        //    {
        //        strWhere = " userid = " + userid;
        //    }

        //    int RecordCount = 0;

        //    DataTable dblCommentPageList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

        //    return GetJsonList(dblCommentPageList, RecordCount);
        //}
        #endregion

        #region 分页（json节点见表字段）
        /// <summary>
        /// 数据分页
        /// </summary>
        /// <param name="RecordCount">当前页码</param>
        /// <param name="PageSize">每页数据</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="ation">查看类型（1：收到的评论，0：发出的评论 ）</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static string GetCommentPageList(int PageCurrent, int PageSize, string ation, long userid)
        {
            string strTableName = " ilog_comment ";
            string strFieldKey = "ic_id";
            string strFieldShow = "";

            if (ation == "0")   //发出的评论
            {
                strFieldShow = " ic_id,userid,ic_content,intime,ic_currentid,ic_currentuserid,(select nickname from vip_ilog where vip_ilog.userid = ilog_comment.ic_currentuserid) as nickname ";
                strFieldShow += " ,(select face from vip_ilog where vip_ilog.userid = ilog_comment.ic_currentuserid) as face,(SELECT is_name FROM ilog_source WHERE ilog_source.is_id = ilog_comment.is_id) as is_name,is_id,ic_type,  ";
                strFieldShow += " (select is_commentnum from ilog_spread where is_id = dbo.fn_GetSpreadidForCommont(ic_type,ic_currentid) ) as vic_commentnum,dbo.fn_GetSpreadContentForComment(ic_type,ic_currentid,ic_commentid,ic_commentcontent) as is_content,dbo.fn_GetSpreadContentForCommentNickname(ic_type,ic_currentid,ic_currentuserid,ic_commentid) as is_nickname, ";
                strFieldShow += " ic_commentid,ic_commentcontent,dbo.fn_GetSpreadidForCommont(ic_type,ic_currentid) as is_id_,dbo.fn_GetSpreaduseridForCommont(ic_type,ic_currentid) as userid_ ";
            }
            else                //收到的评论
            {
                strFieldShow = " ic_id,userid,ic_content,intime,ic_currentid,ic_currentuserid,(select nickname from vip_ilog where vip_ilog.userid = ilog_comment.userid) as nickname ";
                strFieldShow += " ,(select face from vip_ilog where vip_ilog.userid = ilog_comment.userid) as face,(SELECT is_name FROM ilog_source WHERE ilog_source.is_id = ilog_comment.is_id) as is_name,is_id,ic_type, ";
                strFieldShow += " (select is_commentnum from ilog_spread where is_id = dbo.fn_GetSpreadidForCommont(ic_type,ic_currentid)) as vic_commentnum,dbo.fn_GetSpreadContentForComment(ic_type,ic_currentid,ic_commentid,ic_commentcontent) as is_content,dbo.fn_GetSpreadContentForCommentNickname(ic_type,ic_currentid,ic_currentuserid,ic_commentid) as is_nickname, ";
                strFieldShow += " ic_commentid,ic_commentcontent,dbo.fn_GetSpreadidForCommont(ic_type,ic_currentid) as is_id_,dbo.fn_GetSpreaduseridForCommont(ic_type,ic_currentid) as userid_ ";
            }

            string strFieldOrder = " ic_id desc ";
            string strWhere = "";

            //收到评论
            if (ation == "1")
            {
                strWhere = " ic_state <> 2 and ic_currentuserid = " + userid;
            }
            else //发出评论
            {
                strWhere = " ic_state <> 1 and  userid = " + userid;
            }

            int RecordCount = 0;

            DataTable dblCommentPageList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            return GetJsonList(dblCommentPageList, RecordCount, ation);
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
        /// <param name="ation">1：收到的评论，0：发出的评论</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static string GetSearchList(int PageCurrent, int PageSize, string keyword, string ation, long userid)
        {
            string strTableName = " ilog_comment ";
            string strFieldKey = "ic_id";
            string strFieldShow = "";

            if (ation == "0")   //发出的评论
            {
                strFieldShow = " ic_id,userid,ic_content,intime,ic_currentid,ic_currentuserid,(select nickname from vip_ilog where vip_ilog.userid = ilog_comment.ic_currentuserid) as nickname ";
                strFieldShow += " ,(select face from vip_ilog where vip_ilog.userid = ilog_comment.ic_currentuserid) as face,(SELECT is_name FROM ilog_source WHERE ilog_source.is_id = ilog_comment.is_id) as is_name,is_id,ic_type,  ";
                strFieldShow += " (select is_commentnum from ilog_spread where is_id = dbo.fn_GetSpreadidForCommont(ic_type,ic_currentid) ) as vic_commentnum,dbo.fn_GetSpreadContentForComment(ic_type,ic_currentid,ic_commentid,ic_commentcontent) as is_content,dbo.fn_GetSpreadContentForCommentNickname(ic_type,ic_currentid,ic_currentuserid,ic_commentid) as is_nickname, ";
                strFieldShow += " ic_commentid,ic_commentcontent,dbo.fn_GetSpreadidForCommont(ic_type,ic_currentid) as is_id_,dbo.fn_GetSpreaduseridForCommont(ic_type,ic_currentid) as userid_ ";
            }
            else                //收到的评论
            {
                strFieldShow = " ic_id,userid,ic_content,intime,ic_currentid,ic_currentuserid,(select nickname from vip_ilog where vip_ilog.userid = ilog_comment.userid) as nickname ";
                strFieldShow += " ,(select face from vip_ilog where vip_ilog.userid = ilog_comment.userid) as face,(SELECT is_name FROM ilog_source WHERE ilog_source.is_id = ilog_comment.is_id) as is_name,is_id,ic_type, ";
                strFieldShow += " (select is_commentnum from ilog_spread where is_id = dbo.fn_GetSpreadidForCommont(ic_type,ic_currentid)) as vic_commentnum,dbo.fn_GetSpreadContentForComment(ic_type,ic_currentid,ic_commentid,ic_commentcontent) as is_content,dbo.fn_GetSpreadContentForCommentNickname(ic_type,ic_currentid,ic_currentuserid,ic_commentid) as is_nickname, ";
                strFieldShow += " ic_commentid,ic_commentcontent,dbo.fn_GetSpreadidForCommont(ic_type,ic_currentid) as is_id_,dbo.fn_GetSpreaduseridForCommont(ic_type,ic_currentid) as userid_ ";
            }

            string strFieldOrder = " ic_id desc ";
            string strWhere = " ";

            if (ation == "0")     //发出的评论
            {
                strWhere = " ic_state <> 1 and  ic_currentuserid in (SELECT dbo.vip_ilog.userid FROM vip_ilog WHERE vip_ilog.nickname like '%" + keyword + "%') and userid = " + userid;
            }
            else               //收到的评论
            {
                strWhere = " ic_state <> 2 and  userid in (SELECT dbo.vip_ilog.userid FROM vip_ilog WHERE vip_ilog.nickname like '%" + keyword + "%') and ic_currentuserid = " + userid;
            }

            int RecordCount = 0;

            DataTable dblSearchList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            return GetJsonList(dblSearchList, RecordCount, ation);
        }
        #endregion

        #region 构建json
        /// <summary>
        /// 构建json
        /// </summary>
        /// <param name="List">数据</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="ation">操作类型1：收到的评论，0：发出的评论</param>
        /// <returns></returns>
        private static string GetJsonList(DataTable dblCommentPageList, int RecordCount, string ation)
        {
            int count = dblCommentPageList.Rows.Count;

            //预留统计联系人

            int RowsCount = 0;

            if (RecordCount > 1)
            {
                RowsCount = 45 * RecordCount;
            }
            else
            {
                RowsCount = count;  //如果是一页就返回实际的查询条数
            }

            StringBuilder strCommentPageList = new StringBuilder();

            strCommentPageList.Append("{CommentList:");

            try
            {
                if (count > 0)
                {
                    strCommentPageList.Append("[{State:'1'}");

                    strCommentPageList.Append("," + (RecordCount > 1 ? "{RecordCount:'" + RecordCount + "'}," : "{RowsCount:'" + (RowsCount > 450 ? 450 : RowsCount) + "'},"));



                    for (int i = 0; i < count; i++)
                    {

                        //获取加i信息
                        ILog.Model.VipILog userInfo = new ILog.Model.VipILog();

                        if (ation == "0")   //发出的评论
                        {
                            //加i信息
                            userInfo = ILog.DAL.VipILog.GetVipIlogInfoById(Convert.ToInt64(dblCommentPageList.Rows[i]["ic_currentuserid"]));
                        }
                        else                //收到的评论
                        {
                            //加i信息
                            userInfo = ILog.DAL.VipILog.GetVipIlogInfoById(Convert.ToInt64(dblCommentPageList.Rows[i]["userid"]));
                        }

                        if (userInfo == null)
                        {
                            continue;
                        }

                        DateTime date = Convert.ToDateTime(dblCommentPageList.Rows[i]["intime"]);

                        strCommentPageList.Append("{ic_id:\"" + dblCommentPageList.Rows[i]["ic_id"].ToString() + "\",userid:\"" + dblCommentPageList.Rows[i]["userid"].ToString() + "\",");
                        strCommentPageList.Append("ic_content:\""
                                                    + ILog.Common.Common.GetJScriptGlobalObjectEscape(Ilog.BLL.ILogOriginal.GetFinalIlogContentShow(dblCommentPageList.Rows[i]["ic_content"].ToString(),
                                                    Convert.ToInt64(dblCommentPageList.Rows[i]["ic_id"]))) + "\",");

                        strCommentPageList.Append("vi_memberlevel:\"" + userInfo.vi_memberlevel + "\",");
                        strCommentPageList.Append("intime:\"" + ILog.Common.Common.GetIlogTime(Convert.ToDateTime(dblCommentPageList.Rows[i]["intime"])) + "\",");
                        strCommentPageList.Append("ic_commentid:\"" + dblCommentPageList.Rows[i]["ic_commentid"] + "\",ic_commentcontent:\"" + dblCommentPageList.Rows[i]["ic_commentcontent"].ToString() + "\",");
                        strCommentPageList.Append("is_content:\"" + ILog.Common.Common.GetJScriptGlobalObjectEscape(Ilog.BLL.ILogOriginal.GetClearUUBAndChangeExpression(dblCommentPageList.Rows[i]["is_content"].ToString(), 50, "...")) + "\",is_nickname:\"" + dblCommentPageList.Rows[i]["is_nickname"].ToString() + "\",");
                        strCommentPageList.Append("ic_currentid:\"" + dblCommentPageList.Rows[i]["ic_currentid"].ToString() + "\",ic_currentuserid:\"" + dblCommentPageList.Rows[i]["ic_currentuserid"].ToString() + "\",");
                        strCommentPageList.Append("is_id_:\"" + dblCommentPageList.Rows[i]["is_id_"].ToString() + "\",userid_:\"" + dblCommentPageList.Rows[i]["userid_"].ToString() + "\",");
                        strCommentPageList.Append("nickname:\"" + dblCommentPageList.Rows[i]["nickname"].ToString() + "\",face:\"" + dblCommentPageList.Rows[i]["face"].ToString() + "\",");
                        strCommentPageList.Append("ic_type:\"" + dblCommentPageList.Rows[i]["ic_type"].ToString() + "\",vic_commentnum:\"" + dblCommentPageList.Rows[i]["vic_commentnum"].ToString() + "\",");
                        strCommentPageList.Append("is_name:\"" + dblCommentPageList.Rows[i]["is_name"].ToString() + "\",is_id:\"" + dblCommentPageList.Rows[i]["is_id"].ToString() + "\"}");

                        //最后一个元素不需要加逗号
                        if ((count - 1) > i)
                        {
                            strCommentPageList.Append(",");
                        }
                    }
                }
                else
                {
                    strCommentPageList.Append("[{State:'2'}");  //无数据
                }
            }
            catch
            {
                strCommentPageList.Append("[{State:'0'}");
            }

            strCommentPageList.Append("]}");

            return strCommentPageList.ToString();
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

            strCommentPageList.Append("{CommentList:");

            strCommentPageList.Append("[{State:'2'}");  //无数据

            strCommentPageList.Append("]}");

            return strCommentPageList.ToString();
        }
        #endregion

        //#region  增加一条数据
        ///// <summary>
        ///// 功能描述：增加一条数据
        ///// 创建标识：ljd 20120607
        ///// <param name="model">评论表实体</param>
        ///// </summary>
        //public static string CommentAdd(ILog.Model.ILogComment model)
        //{
        //    StringBuilder strCommentAdd = new StringBuilder();

        //    strCommentAdd.Append("var CommentAddJsonObject = ");
        //    strCommentAdd.Append("({");
        //    strCommentAdd.Append("\"state\": \"" + ILog.DAL.ILogComment.CommentAdd(model).ToString() + "\"");
        //    strCommentAdd.Append("})");

        //    return strCommentAdd.ToString();
        //}
        //#endregion

        #region  增加一条评论并返回评论id
        /// <summary>
        /// 功能描述：增加一条评论并返回评论id
        /// 创建标识：ljd 20120621
        /// <param name="model">评论表实体</param>
        /// <returns>新增加的评论id</returns>
        /// </summary>
        public static long CommentAddAndGetID(ILog.Model.ILogComment model)
        {
            long commentid = ILog.DAL.ILogComment.CommentAdd(model);

            return commentid;

        }
        #endregion

        #region 得到评论实体对象
        /// <summary>
        /// 功能描述：得到评论实体对象
        /// 创建标识：ljd 20120716
        /// <param name="ic_id">流水号</param>
        /// </summary>
        public static ILog.Model.ILogComment GetCommentInfoById(long ic_id)
        {
            ILog.Model.ILogComment ooComment = ILog.DAL.ILogComment.GetCommentInfoById(ic_id);
            return ooComment;

        }
        #endregion

        #region 评论文章
        /// <summary>
        /// 功能描述：评论文章
        /// 创建标识：ljd 20120610
        /// </summary>
        /// <param name="spreadid">转发id</param>
        /// <param name="isoriginal">是否原创</param>
        /// <param name="content">评价内容</param>
        /// <param name="currentCommentid">被回复的评价id</param>
        /// <returns></returns>
        public static string CommentIlog(long spreadid, int isoriginal, string content, long currentCommentid)
        {
            long userid = Ilog.BLL.VipILog.GetVIPUserID();
            /*
             * 添加评论
             * 给原创与当前用户增加评论数，如果评论中有@，给所有@用户增加@评论数，增加@用户的@评论记录
             * 同一用户只增加一条评论数
             */
            //当前文章id

            int urlstate = 0;

            try
            {
                //被回复的用户id
                long currentCommentUserid = 0;

                //被回复的评论内容
                string currentCommentContent = "";

                if (currentCommentid != 0)
                {
                    //取出被评论的评论内容
                    ILog.Model.ILogComment ooCurrentComment = GetCommentInfoById(currentCommentid);

                    if (ooCurrentComment != null)
                    {
                        currentCommentUserid = ooCurrentComment.userid;
                        currentCommentContent = ooCurrentComment.ic_content;
                    }
                    else
                    {
                        currentCommentid = 0;
                    }
                }

                //过滤回复@xxx:后的内容
                string atcontent = content;
                //回复用户的id
                long replyUserID = 0;

                if (atcontent.IndexOf("回复@") == 0)//是回复
                {
                    //第一个冒号的位置
                    int colonIndex = atcontent.IndexOf(":");
                    //下一个@的位置
                    int secondAtIndex = atcontent.Substring(3, atcontent.Length - 3).IndexOf("@");

                    if (colonIndex > secondAtIndex)//如果:的位置小于下一个@的位置
                    {
                        //回复的用户昵称
                        string replynickname = atcontent.Substring(3, colonIndex - 3);
                        //被回复人的用户id
                        replyUserID = Ilog.BLL.VipILog.GetUserIDByNickName(replynickname.Trim());
                        /*
                         * 判断用户是否修改过回复后的用户昵称，如果修改了，虽然有回复二次，依然算作评论。
                         */
                        if (replyUserID == currentCommentUserid)
                        {
                            atcontent = atcontent.Substring(colonIndex + 1, atcontent.Length - colonIndex - 1);
                        }
                        else//假回复（昵称对应的用户id与被回复评论的评论人id不是同一个）
                        {
                            currentCommentUserid = 0;
                        }
                    }
                }

                //获取@用户id
                Dictionary<int, long> dicUserID = ILogOriginal.GetLoveItUserID(atcontent);

                //处理博文时记录的移除字符串的长度
                int removeLen = 0;

                //处理博文内容，将长地址转为短地址
                ILogOriginal.OperateLoveItUrl(content, ref content, ref removeLen);

                //将表情转为ubb代码
                content = Ilog.BLL.ILogOriginal.GetIlogContentByExpression(content);

                ILog.Model.ILogSpread ooSpread = ILog.DAL.ILogSpread.GetSpreadInfo(spreadid, ref urlstate);
                //评论用户id
                long spreadUserid = ooSpread.userid;
                //当前博文id
                long finalspreadid = ooSpread.is_id;

                if (ooSpread == null)
                {
                    return "{state:0}";
                }

                if (ooSpread.is_spreadtype == 0 && ooSpread.is_type == 0)//原创
                {
                    isoriginal = 1;
                    ILog.Model.ILogSpread ooSpreadTa = ILog.DAL.ILogSpread.GetSpreadOriginalInfo(ooSpread.io_id);
                    spreadUserid = ooSpreadTa.userid;
                    finalspreadid = ooSpread.io_id;
                }
                else
                {
                    isoriginal = 0;
                }

                //评论类别 1原创 2转发
                int commentType = isoriginal == 1 ? 1 : 2;

                //判断当前博文是否是别人的传播
                //如果是别人的传播（别人的原创或转发），需取出传播表中原博文id
                if (ooSpread.is_spreadtype == 0 && ooSpread.is_isoriginal == 0 && ooSpread.is_type == 1)//别人的转发
                {
                    ILog.Model.ILogSpread ooSpreadTa = ILog.DAL.ILogSpread.GetSpreadInfo(ooSpread.iso_id, ref urlstate);
                    spreadUserid = ooSpreadTa.userid;
                    finalspreadid = ooSpreadTa.is_id;
                }

                ILog.Model.ILogComment ooComment = new ILog.Model.ILogComment();
                ooComment.ic_content = content;
                ooComment.ic_currentid = finalspreadid;
                ooComment.ic_currentuserid = spreadUserid;
                ooComment.ic_state = 0;
                ooComment.ic_type = commentType;
                ooComment.intime = DateTime.Now;
                ooComment.is_id = 1;
                ooComment.userid = userid;
                ooComment.ic_commentcontent = "";
                ooComment.ic_commentid = 0;
                //新增用户评论
                long commentid = Ilog.BLL.ILogComment.CommentAddAndGetID(ooComment);

                long originalUserID = 0;

                ILog.Model.ILogOriginal ooOriginal = ILog.DAL.ILogOriginal.GetOriginalInfo(ooSpread.io_id);
                if (ooOriginal != null)
                {
                    originalUserID = ooOriginal.userid;

                    if (isoriginal != 1)
                    {
                        //给原创作者加用户评论
                        ILog.Model.ILogComment ooCommentSelf = new ILog.Model.ILogComment();
                        ooCommentSelf.ic_content = content;
                        ooCommentSelf.ic_currentid = ooOriginal.io_id;
                        ooCommentSelf.ic_currentuserid = originalUserID;
                        ooCommentSelf.ic_state = 0;
                        ooCommentSelf.ic_type = 1;
                        ooCommentSelf.intime = DateTime.Now;
                        ooCommentSelf.is_id = 1;
                        ooCommentSelf.userid = userid;
                        ooCommentSelf.ic_commentcontent = "";
                        ooCommentSelf.ic_commentid = 0;

                        Ilog.BLL.ILogComment.CommentAddAndGetID(ooCommentSelf);
                    }
                }

                //如果存在@用户，加入@信息表
                if (dicUserID.Count > 0)
                {
                    foreach (int key in dicUserID.Keys)
                    {
                        //添加@
                        ILog.Model.ILogat ooAt = new ILog.Model.ILogat();
                        ooAt.ia_atuserid = userid;
                        ooAt.ia_content = content;//评论内容
                        ooAt.ia_type = 1;//评论
                        ooAt.intime = DateTime.Now;
                        ooAt.iso_id = ooSpread.io_id;
                        ooAt.userid = dicUserID[key];
                        ooAt.ia_logid = commentid;
                        ooAt.is_id = 1;//ilog

                        Ilog.BLL.ILogat.AtInfoAdd(ooAt);

                        //如果@的信息中有原创作者，不再进行@提醒，但是有@记录
                        if (dicUserID[key] == spreadUserid)
                        {
                            continue;
                        }

                        //更新@数
                        Ilog.BLL.VipILogCount.UpdateAtNum(dicUserID[key], ref urlstate);
                    }
                }
                if (ooOriginal != null)
                {
                    //给原创文章加评论数
                    Ilog.BLL.ILogOriginal.UpdateCommentNum(ooSpread.io_id);
                    //给原创的转发增加评论数
                    Ilog.BLL.ILogSpread.UpdateOriginalCommentNum(ooSpread.io_id);

                    //给原创用户加评论总数
                    Ilog.BLL.VipILogCount.UpdateCommentNum(originalUserID, ref urlstate);
                }
                if (isoriginal != 1)//给转发用户加评论数
                {
                    //给转发文章评论次数加1
                    Ilog.BLL.ILogSpread.UpdateCommentNum(finalspreadid);

                    Ilog.BLL.VipILogCount.UpdateCommentNum(spreadUserid, ref urlstate);
                }

                //如果是回复某人
                if (currentCommentid != 0 && ooOriginal != null && currentCommentUserid != ooOriginal.userid && currentCommentUserid != spreadUserid)
                {
                    ILog.Model.ILogComment ooCommentReply = new ILog.Model.ILogComment();

                    ooCommentReply.ic_content = content;
                    ooCommentReply.ic_currentid = finalspreadid;
                    ooCommentReply.ic_currentuserid = replyUserID;
                    ooCommentReply.ic_state = 0;
                    ooCommentReply.ic_type = commentType;
                    ooCommentReply.intime = DateTime.Now;
                    ooCommentReply.is_id = 1;
                    ooCommentReply.userid = userid;
                    ooCommentReply.ic_commentcontent = currentCommentContent;
                    ooCommentReply.ic_commentid = currentCommentid;
                    //新增用户评论
                    Ilog.BLL.ILogComment.CommentAddAndGetID(ooCommentReply);
                    //添加用户评论提醒数
                    Ilog.BLL.VipILogCount.UpdateCommentNum(replyUserID, ref urlstate);
                }

            }
            catch
            {
                urlstate = 0;
            }

            //成功标记
            StringBuilder strbResult = new StringBuilder();

            strbResult.Append("{");
            strbResult.Append("state:" + urlstate);
            strbResult.Append("}");

            return strbResult.ToString();

        }
        #endregion


        #region 博文内页分页. by lx on 20120628

        /// <summary>
        /// 博文内页分页数据. by lx on 20120628
        /// </summary>        
        /// <param name="RecordCount">总页数</param>
        /// <param name="currentid">文章ID</param>
        /// <param name="type">评论类别 1原创 2转发</param>
        /// <param name="RecordCount">当前页码</param>
        /// <param name="PageSize">每页数据</param>
        /// <returns></returns>
        public static string GetBlogCommentPageList(long currentid, int type, int PageCurrent, int PageSize)
        {
            int urlstate = 0;

            ILog.Model.ILogSpread ooSpread = ILog.DAL.ILogSpread.GetSpreadInfo(currentid, ref urlstate);

            //如果是别人的传播（别人的原创或转发），需取出传播表中原博文id
            if (ooSpread.is_spreadtype == 0 && ooSpread.is_type == 0)//别人的原创或自己的原创
            {
                currentid = ooSpread.io_id;
                type = 1;
            }
            //如果是别人的传播（别人的原创或转发），需取出传播表中原博文id
            else if (ooSpread.is_spreadtype == 0 && ooSpread.is_isoriginal == 0 && ooSpread.is_type == 1)//别人的转发
            {
                currentid = ooSpread.iso_id;
                type = 2;
            }
            else if (ooSpread.is_spreadtype == 1 && ooSpread.is_isoriginal == 0)//自己的转发
            {
                type = 2;
            }

            StringBuilder result = new StringBuilder();

            string strTableName = " ilog_comment ";
            string strFieldKey = "ic_id";
            string strFieldShow = "ic_id,userid,ic_content,intime,ic_currentid,ic_type,ic_currentuserid,(select nickname from vip_ilog where vip_ilog.userid = ilog_comment.userid) as nickname,(select face from vip_ilog where vip_ilog.userid = ilog_comment.userid) as face";
            string strFieldOrder = " ic_id desc ";
            string strWhere = string.Format("ic_currentid={0} AND ic_type={1} and ic_state=0 and ic_commentid=0 ", currentid, type);

            int RecordCount = 0;

            DataTable dblCommentPageList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            result.Append("{ List:");

            if (dblCommentPageList.Rows.Count > 0)
            {

                #region 添加列时间、内容

                dblCommentPageList.Columns.Add("timeStr", Type.GetType("System.String"));//时间计算
                dblCommentPageList.Columns.Add("content", Type.GetType("System.String"));//

                for (int i = 0; i < dblCommentPageList.Rows.Count; i++)
                {
                    //修改时间
                    DateTime time = string.IsNullOrEmpty(dblCommentPageList.Rows[i]["intime"].ToString()) == true ? DateTime.Now : Convert.ToDateTime(dblCommentPageList.Rows[i]["intime"]);

                    string content = ILogOriginal.GetFinalIlogContentShow(dblCommentPageList.Rows[i]["ic_content"].ToString(), currentid);

                    dblCommentPageList.Rows[i]["timeStr"] = ILog.Common.Common.GetIlogTime(time);
                    dblCommentPageList.Rows[i]["content"] = content;

                }

                #endregion

                string listStr = Com.ILog.Utils.Utils.DataTableToJSON(dblCommentPageList).ToString();

                listStr.Substring(0, listStr.Length - 1);

                result.Append(listStr.Substring(0, listStr.Length - 1));

            }
            else
            {

                result.Append("[]");

            }

            result.Append("}");

            return result.ToString();

        }


        /// <summary>
        /// 博文内页分页数据. by lx on 20120628
        /// </summary>        
        /// <param name="RecordCount">总页数</param>
        /// <param name="currentid">文章ID</param>
        /// <param name="type">评论类别 1原创 2转发</param>
        /// <param name="RecordCount">当前页码</param>
        /// <param name="PageSize">每页数据</param>
        /// <returns></returns>
        public static string GetContentCommentPageList(long currentid, int type, int PageCurrent, int PageSize)
        {

            StringBuilder result = new StringBuilder();

            try
            {

                int urlstate = 0;

                ILog.Model.ILogSpread ooSpread = ILog.DAL.ILogSpread.GetSpreadInfo(currentid, ref urlstate);

                //如果是别人的传播（别人的原创或转发），需取出传播表中原博文id
                if (ooSpread.is_spreadtype == 0 && ooSpread.is_type == 0)//别人的原创或自己的原创
                {
                    currentid = ooSpread.io_id;
                    type = 1;
                }
                //如果是别人的传播（别人的原创或转发），需取出传播表中原博文id
                else if (ooSpread.is_spreadtype == 0 && ooSpread.is_isoriginal == 0 && ooSpread.is_type == 1)//别人的转发
                {
                    currentid = ooSpread.iso_id;
                    type = 2;
                }
                else if (ooSpread.is_spreadtype == 1 && ooSpread.is_isoriginal == 0)//自己的转发
                {
                    type = 2;
                }

                string strTableName = " ilog_comment ";
                string strFieldKey = "ic_id";
                string strFieldShow = "ic_id,userid,ic_content,intime,ic_currentid,ic_type,ic_currentuserid,(select nickname from vip_ilog where vip_ilog.userid = ilog_comment.userid) as nickname,(select face from vip_ilog where vip_ilog.userid = ilog_comment.userid) as face";
                string strFieldOrder = " ic_id desc ";
                string strWhere = string.Format("ic_currentid={0} AND ic_type={1} and ic_state=0 and ic_commentid=0 ", currentid, type);

                int RecordCount = 0;

                DataTable dblCommentPageList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

                result.Append(GetCommentJsonString(dblCommentPageList, RecordCount));

            }
            catch (Exception ex)
            {

                result.Length = 0;
                result.Append("{List:[{State:'0'}]}");

            }

            return result.ToString();

        }


        /// <summary>
        /// 构建json格式数据.by lx on 20120719
        /// </summary>
        /// <param name="List">数据</param>
        /// <param name="RecordCount">总页数</param>
        /// <returns></returns>
        public static string GetCommentJsonString(DataTable List, int RecordCount)
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
                        strPageList.Append(",{RowsCount:'" + count + "'},");
                    }
                    else
                    {
                        strPageList.Append(",{RecordCount:'" + (RecordCount > 10 ? 10 : RecordCount) + "'},");  //所有列表页最多显示10页
                    }

                    for (int i = 0; i < count; i++)
                    {
                        strPageList.Append("{ic_id:\"" + List.Rows[i]["ic_id"].ToString() + "\",userid:\"" + List.Rows[i]["userid"].ToString() + "\",");
                        strPageList.Append("ic_currentid:\"" + List.Rows[i]["ic_currentid"].ToString() + "\",ic_type:\"" + List.Rows[i]["ic_type"].ToString() + "\",");
                        strPageList.Append("ic_currentuserid:\"" + List.Rows[i]["ic_currentuserid"].ToString() + "\",nickname:\"" + List.Rows[i]["nickname"].ToString() + "\",");
                        strPageList.Append("face:\"" + List.Rows[i]["face"].ToString() + "\",");

                        //查看发@的时间是不是当天
                        strPageList.Append("timeStr:\"" + ILog.Common.Common.GetIlogTime(Convert.ToDateTime(List.Rows[i]["intime"])) + "\",");



                        //内容转码
                        strPageList.Append("content:\"" + ILog.Common.Common.GetJScriptGlobalObjectEscape(ILogOriginal.GetFinalIlogContentShow(List.Rows[i]["ic_content"].ToString(), Convert.ToInt32(List.Rows[i]["ic_currentid"].ToString()))) + "\"}");

                        //strPageList.Append("content:\"" + Microsoft.JScript.GlobalObject.escape(Ilog.BLL.ILogOriginal.GetIlogContentWithExpression(List.Rows[i]["ic_content"].ToString())) + "\"}");



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

                strPageList.Length = 0;
                strPageList.Append("{List:[{State:'0'}");

            }

            strPageList.Append("]}");

            return strPageList.ToString();
        }



        #endregion


        //#region 删除一条回复数据
        ///// <summary>
        ///// 功能描述：删除一条回复数据
        ///// 创建标识：ljd 20120709
        ///// <param name="ic_id">流水号</param>
        ///// </summary>
        //public static string ReplyDel(long ic_id)
        //{
        //    long userid = Ilog.BLL.VipILog.GetVIPUserID();

        //    int resultCount = ILog.DAL.ILogComment.CommentDel(ic_id, userid);

        //    if (resultCount > 0)
        //    {
        //        resultCount = 1;
        //    }
        //    else
        //    {
        //        resultCount = 0;
        //    }

        //    StringBuilder strCommentDel = new StringBuilder();

        //    //strCommentDel.Append("var CommentDelJsonObject = ");
        //    strCommentDel.Append("{");
        //    strCommentDel.Append("\"state\": \"" + resultCount.ToString() + "\"");
        //    strCommentDel.Append("}");

        //    return strCommentDel.ToString();

        //}
        //#endregion

        #region 发送站短智能提示收信人
        /// <summary>
        /// 发送站短智能提示收信人
        /// </summary>
        /// <param name="commentinfo">关键字</param>
        /// <param name="ation">操作类型查看类型（1：收到的评论，0：发出的评论 ）</param>
        /// <param name="userid">当前用户id</param>
        /// <returns></returns>
        public static string GetSearchCommentInfo(string commentinfo, int ation, long userid)
        {
            StringBuilder strPageList = new StringBuilder();

            strPageList.Append("{List:");

            try
            {
                List<ILog.Model.ILogComment> GetIILogCommentList = ILog.DAL.ILogComment.GetSearchCommentInfo(commentinfo, ation, userid);

                int count = GetIILogCommentList.Count;

                int j = 0;

                //搜索结果
                string strio_content;

                //内容
                string ilogContent;

                if (count > 0)  //找到用户
                {
                    strPageList.Append("[{State:'1'},");

                    foreach (ILog.Model.ILogComment GetIILogCommentListInfo in GetIILogCommentList)
                    {
                        ////把表情替换成空
                        //ilogContent = Regex.Replace(GetIILogCommentListInfo.ic_content, @"\[img=(\d*|),(\d*|)\](.[^\]]*)(\[\/img\])", "");

                        ////去掉\n
                        //ilogContent = ilogContent.Replace("\n", "");

                        ////长度截取
                        //strio_content = Com.ILog.Utils.Utils.GetUnicodeSubString(ilogContent, 60, "");

                        ////判断是否在30个字内匹配了到内容，如果匹配到了内容就显示在搜索联想中
                        //if (strio_content.Contains(commentinfo))
                        //{
                        strPageList.Append("{nickname:\"" + GetIILogCommentListInfo.nickname + "\"}");
                        //}
                        //else
                        //{
                        //    strPageList.Append("{nickname:\"\"}");
                        //}

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


    }
}
