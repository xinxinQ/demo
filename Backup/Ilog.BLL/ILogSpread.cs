using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;

using System.Text.RegularExpressions;

using Com.ILog.Utils;


namespace Ilog.BLL
{
    public class ILogSpread
    {
        #region 查看某个博文是否存在（True：存在，False：不存在）
        /// <summary>
        /// 查看某个博文是否存在（True：存在，False：不存在）
        /// <param name="is_id">流水号</param>
        /// </summary>
        public static string SpreadExists(int is_id)
        {
            StringBuilder strSpreadExists = new StringBuilder();

            strSpreadExists.Append("var strSpreadExistsJsonObject = ");
            strSpreadExists.Append("({");
            strSpreadExists.Append("\"Exists\": \"" + ILog.DAL.ILogSpread.SpreadExists(is_id).ToString() + "\"");
            strSpreadExists.Append("})");

            return strSpreadExists.ToString();
        }
        #endregion

        #region  增加一条数据博文
        /// <summary>
        /// 增加一条数据博文
        /// <param name="model">博文表实体</param>
        /// </summary>
        public static string SpreadAdd(ILog.Model.ILogSpread model)
        {
            StringBuilder strSpreadAdd = new StringBuilder();

            strSpreadAdd.Append("var strSpreadAddJsonObject = ");
            strSpreadAdd.Append("({");
            strSpreadAdd.Append("\"state\": \"" + ILog.DAL.ILogSpread.SpreadAdd(model).ToString() + "\"");
            strSpreadAdd.Append("})");

            return strSpreadAdd.ToString();
        }
        #endregion

        #region  新增转发并获取id
        /// <summary>
        /// 功能描述：新增转发并获取id
        /// 创建标识：ljd 20120701
        /// <param name="model">博文表实体</param>
        /// </summary>
        public static long SpreadAddAndGetID(ILog.Model.ILogSpread model)
        {
            long spreadID = ILog.DAL.ILogSpread.SpreadAddAndGetID(model);
            return spreadID;

        }
        #endregion

        #region 更新一条来源
        /// <summary>
        /// 更新一条来源
        /// <param name="model">来源表实体</param>
        /// </summary>
        //public static string SpreadUpdate(ILog.Model.ILogSpread model)
        //{
        //    StringBuilder strSpreadUpdate = new StringBuilder();

        //    strSpreadUpdate.Append("var strSpreadUpdateJsonObject = ");
        //    strSpreadUpdate.Append("({");
        //    strSpreadUpdate.Append("\"state\": \"" + ILog.DAL.ILogSpread.SpreadUpdate(model).ToString() + "\"");
        //    strSpreadUpdate.Append("})");

        //    return strSpreadUpdate.ToString();
        //}
        #endregion

        #region 删除一条转发
        /// <summary>
        /// 删除一条转发
        /// <param name="is_id">流水号</param>
        /// </summary>
        public static string SpreadDel(long is_id)
        {

            int resultCount = ILog.DAL.ILogSpread.SpreadDel(is_id);

            if (resultCount > 0)
            {
                resultCount = 1;
            }
            else
            {
                resultCount = 0;
            }

            StringBuilder strSpreadDel = new StringBuilder();

            //strSpreadDel.Append("var strSpreadDelJsonObject = ");
            strSpreadDel.Append("{");
            strSpreadDel.Append("\"state\": \"" + resultCount + "\"");
            strSpreadDel.Append("}");

            return strSpreadDel.ToString();
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="isu_id">流水号</param>
        /// </summary>
        public static string GetModel(int isu_id)
        {
            DataTable dblILogSpreadModelList = ILog.DAL.ILogSpread.GetModel(isu_id);

            //构建josn字符串 
            string strILogSpreadJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblILogSpreadModelList).ToString();

            return strILogSpreadJosn;
        }
        #endregion

        #region 分页（json节点见表字段）
        /// <summary>
        /// 数据分页
        /// </summary>
        /// <param name="tbname">要分页显示的表名</param>
        /// <param name="FieldKey">用于定位记录的主键(惟一键)字段,只能是单个字段</param>
        /// <param name="PageCurrent">要显示的页码</param>
        /// <param name="PageSize">每页的大小(记录数)</param>
        /// <param name="FieldShow">以逗号分隔的要显示的字段列表,如果不指定,则显示所有字段</param>
        /// <param name="FieldOrder">以逗号分隔的排序字段列表,可以指定在字段后面指定DESC/ASC 用于指定排序顺序</param>
        /// <param name="Where">查询条件</param>
        /// <param name="RecordCount">总页数</param>
        /// <returns></returns>
        public static string GetILogSpreadPageList(string tbname, string FieldKey, int PageCurrent, int PageSize, string FieldShow, string FieldOrder, string Where, ref int RecordCount)
        {
            string strTableName = " ilog_spread ";
            string strFieldKey = "is_id";
            string strFieldShow = " is_id,iso_id,is_type,is_content,is_fanuserid,userid,intime,iss_id ";
            string strFieldOrder = " is_id desc ";
            string strWhere = " ";

            DataTable dblILogSpreadPageList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            //构建josn字符串 
            string strILogSpreadPageJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblILogSpreadPageList).ToString();

            return strILogSpreadPageJosn;
        }
        #endregion


        #region 全部（个人主页）
        /// <summary>
        /// 首页博文
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="userid">当前用户id</param>
        /// <returns></returns>
        public static string GetAllList(int PageCurrent, int PageSize, long userid, int type)
        {
            string strTableName = " ilog_spread ";
            string strFieldKey = "is_id";
            string strFieldShow = " is_id,userid,io_id,is_isoriginal,is_type,intime,dbo.fn_GetSpreadCommentNum(iso_id,io_id,is_spreadtype,is_isoriginal,is_type,is_commentnum) as vic_commentnum, ";
            strFieldShow += "dbo.fn_GetSpreadSpreadNum(iso_id,io_id,is_spreadtype,is_isoriginal,is_type,is_spreadnum) is_spreadnum,";
            strFieldShow += "(select is_name from ilog_source where is_id = ilog_spread.iss_id ) as is_name,is_spreadtype,";
            strFieldShow += "dbo.fn_GetSpreadContent(iso_id,io_id,is_spreadtype,is_isoriginal,is_type,is_content) as io_content,";
            strFieldShow += "(select is_url from ilog_source where is_id = ilog_spread.iss_id ) as is_url,";
            strFieldShow += "(CASE WHEN  is_spreadtype=0 and is_type=0 THEN (select io_haspic from ilog_original ori where ori.io_id = ilog_spread.io_id) ELSE 0 END) as is_haspic";
            string strFieldOrder = " is_id desc ";
            string strWhere = " intime >= convert(varchar(10),dateadd(day,-7,getdate()),120) and is_fanuserid = " + userid;

            //个人/他人首页全部列表
            if (type == 1)
            {

                strWhere += " and dbo.fn_GetSpreadType(is_spreadtype,is_type,is_isoriginal) IN (1,3)";

            }


            int RecordCount = 0;

            DataTable dblSearchList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            return GetJsonList(dblSearchList, RecordCount);
        }
        #endregion

        #region 全部（个人主页）
        /// <summary>
        /// 首页博文
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="userid">当前用户id</param>
        /// <returns></returns>
        public static string GetAllList2(int PageCurrent, int PageSize, long userid, int type)
        {
            string strTableName = " ilog_spread ";
            string strFieldKey = "is_id";
            string strFieldShow = " is_id,userid,io_id,is_isoriginal,is_type,(select nickname from vip_ilog where userid = ilog_spread.userid) as nickname,intime,dbo.fn_GetSpreadCommentNum(iso_id,io_id,is_spreadtype,is_isoriginal,is_type,is_commentnum) as vic_commentnum, ";
            strFieldShow += "dbo.fn_GetSpreadSpreadNum(iso_id,io_id,is_spreadtype,is_isoriginal,is_type,is_spreadnum) is_spreadnum,(select is_name from ilog_source where is_id = ilog_spread.iss_id ) as is_name,is_spreadtype";
            strFieldShow += ",dbo.fn_GetSpreadContent(iso_id,io_id,is_spreadtype,is_isoriginal,is_type,is_content) as io_content, ";
            strFieldShow += " (select is_url from ilog_source where is_id = ilog_spread.iss_id ) as is_url,(select face from vip_ilog where userid = ilog_spread.userid ) as face ";
            strFieldShow += " ,(select vi_memberlevel from vip_ilog where userid = ilog_spread.userid ) as memberLevel ";
            strFieldShow += ",(CASE WHEN  is_spreadtype=0 and is_type=0 THEN (select io_haspic from ilog_original ori where ori.io_id = ilog_spread.io_id) ELSE 0 END) as is_haspic";
            string strFieldOrder = " is_id desc ";
            string strWhere = " is_fanuserid = " + userid;

            //个人/他人首页全部列表
            if (type == 1)
            {

                strWhere += " and dbo.fn_GetSpreadType(is_spreadtype,is_type,is_isoriginal) IN (1,3)";

            }
            else //子原创
            {
                strWhere += " and dbo.fn_GetSpreadType(is_spreadtype,is_type,is_isoriginal) =1";
            }

            int RecordCount = 0;

            DataTable dblSearchList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            return GetJsonList(dblSearchList, RecordCount);
        }
        #endregion


        #region 搜文章
        /// <summary>
        /// 搜文章
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        public static string GetSerchSpreadAllList(int PageCurrent, int PageSize, string keyword)
        {
            string strTableName = " ilog_spread ";
            string strFieldKey = "is_id";
            string strFieldShow = " is_id,userid,io_id,is_isoriginal,is_type,(select nickname from vip_ilog where userid = ilog_spread.userid) as nickname,intime,dbo.fn_GetSpreadCommentNum(iso_id,io_id,is_spreadtype,is_isoriginal,is_type,is_commentnum) as vic_commentnum, ";
            strFieldShow += "dbo.fn_GetSpreadSpreadNum(iso_id,io_id,is_spreadtype,is_isoriginal,is_type,is_spreadnum) is_spreadnum,(select is_name from ilog_source where is_id = ilog_spread.iss_id ) as is_name,is_spreadtype";
            strFieldShow += ",dbo.fn_GetSpreadContent(iso_id,io_id,is_spreadtype,is_isoriginal,is_type,is_content) as io_content, ";
            strFieldShow += " (select is_url from ilog_source where is_id = ilog_spread.iss_id ) as is_url,(select face from vip_ilog where userid = ilog_spread.userid ) as face ";
            strFieldShow += ",(CASE WHEN  is_spreadtype=0 and is_type=0 THEN (select io_haspic from ilog_original ori where ori.io_id = ilog_spread.io_id) ELSE 0 END) as is_haspic";
            string strFieldOrder = " is_id desc ";
            string strWhere = " dbo.fn_GetSpreadType(is_spreadtype,is_type,is_isoriginal) IN (1,3) and dbo.fn_GetSpreadContent(iso_id,io_id,is_spreadtype,is_isoriginal,is_type,is_content) like'%" + keyword + "%'";

            int RecordCount = 0;

            DataTable dblSearchList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            return GetJsonList(dblSearchList, RecordCount);
        }
        #endregion


        #region 搜索全部（个人主页）
        /// <summary>
        /// 首页博文
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="userid">当前用户id</param>
        /// <param name="keyword">搜索关键字</param>
        /// <returns></returns>
        public static string GetSearchAllList(int PageCurrent, int PageSize, long userid, string keyword)
        {
            string strTableName = "ilog_spread ";
            string strFieldKey = "is_id";
            string strFieldShow = " is_id,userid,is_type,(select nickname from vip_ilog where userid = ilog_spread.userid) as nickname,intime,is_commentnum as vic_commentnum,is_spreadtype is_spreadnum,(select is_name from ilog_source where is_id = ilog_spread.is_id ) as is_name,(select is_url from ilog_source where is_id = ilog_spread.is_id ) as is_url ,";
            strFieldShow += "dbo.fn_GetSpreadContent(iso_id,io_id,is_spreadtype,is_isoriginal,is_type,is_content) AS io_content,io_id,is_isoriginal,(select face from vip_ilog where userid = ilog_spread.userid ) as face,(CASE WHEN  is_spreadtype=0 and is_type=0 THEN (select io_haspic from ilog_original ori where ori.io_id = ilog_spread.io_id) ELSE 0 END) as is_haspic,is_spreadtype ";

            string strFieldOrder = " is_id desc ";
            string strWhere = " userid = " + userid 
                + " and (dbo.fn_GetSpreadContent(iso_id,io_id,is_spreadtype,is_isoriginal,is_type,is_content) like'%" + keyword + "%' OR "+
                " dbo.fn_GetOriginalContent(io_id) like'%" + keyword + "%' )" + "  AND dbo.fn_GetSpreadType(is_spreadtype,is_type,is_isoriginal) IN (1,3)";

            int RecordCount = 0;

            DataTable dblSearchList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            return GetJsonList_s(dblSearchList, RecordCount);
        }
        #endregion

        #region 构建json
        /// <summary>
        /// 构建json
        /// </summary>
        /// <param name="List">数据</param>
        /// <param name="RecordCount">总页数</param>
        /// <returns></returns>
        public static string GetJsonList(DataTable List, int RecordCount)
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
                        strPageList.Append("{is_id:\"" + List.Rows[i]["is_id"].ToString() + "\",userid:\"" + List.Rows[i]["userid"].ToString() + "\",");
                        strPageList.Append("io_id:\"" + List.Rows[i]["io_id"].ToString() + "\",is_isoriginal:\"" + List.Rows[i]["is_isoriginal"].ToString() + "\",");
                        strPageList.Append("is_type:\"" + List.Rows[i]["is_type"].ToString() + "\",");
                        strPageList.Append("is_haspic:\"" + List.Rows[i]["is_haspic"].ToString() + "\",is_spreadtype:\"" + List.Rows[i]["is_spreadtype"].ToString() + "\",");

                        //查看发@的时间是不是当天
                        strPageList.Append("intime:\"" + ILog.Common.Common.GetIlogTime(Convert.ToDateTime(List.Rows[i]["intime"])) + "\",");

                        //加i信息
                        userInfo = ILog.DAL.VipILog.GetModelByUserID(Convert.ToInt64(List.Rows[i]["userid"]));

                        strPageList.Append("memberlevel:\"" + userInfo.vi_memberlevel + "\",face:\"" + userInfo.face + "\",");

                        strPageList.Append("nickname:\"" + userInfo.nickname + "\",io_content:\""
                            + ILog.Common.Common.GetJScriptGlobalObjectEscape(Ilog.BLL.ILogOriginal.GetFinalIlogContentShow(List.Rows[i]["io_content"].ToString(), Convert.ToInt64(List.Rows[i]["is_id"]))) + "\",");

                        strPageList.Append("is_spreadnum:\"" + List.Rows[i]["is_spreadnum"].ToString() + "\",vic_commentnum:\"" + List.Rows[i]["vic_commentnum"].ToString() + "\",");
                        strPageList.Append("is_name:\"" + List.Rows[i]["is_name"].ToString() + "\",is_url:\"" + List.Rows[i]["is_url"].ToString() + "\"}");

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
                strPageList.Append("[{State:'0'}");
            }

            strPageList.Append("]}");

            return strPageList.ToString();
        }
        #endregion

        #region 构建json
        /// <summary>
        /// 构建json
        /// </summary>
        /// <param name="List">数据</param>
        /// <param name="RecordCount">总页数</param>
        /// <returns></returns>
        public static string GetJsonList_s(DataTable List, int RecordCount)
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
                        strPageList.Append("{is_id:\"" + List.Rows[i]["is_id"].ToString() + "\",userid:\"" + List.Rows[i]["userid"].ToString() + "\",");
                        strPageList.Append("io_id:\"" + List.Rows[i]["io_id"].ToString() + "\",is_isoriginal:\"" + List.Rows[i]["is_isoriginal"].ToString() + "\",");
                        strPageList.Append("is_type:\"" + List.Rows[i]["is_type"].ToString() + "\",face:\"" + List.Rows[i]["face"].ToString() + "\",");
                        strPageList.Append("is_haspic:\"" + List.Rows[i]["is_haspic"].ToString() + "\",is_spreadtype:\"" + List.Rows[i]["is_spreadtype"].ToString() + "\",");

                        //查看发@的时间是不是当天
                        strPageList.Append("intime:\"" + ILog.Common.Common.GetIlogTime(Convert.ToDateTime(List.Rows[i]["intime"])) + "\",");

                        //加i信息
                        userInfo = ILog.DAL.VipILog.GetVipIlogInfoById(Convert.ToInt64(List.Rows[i]["userid"]));

                        strPageList.Append("vi_memberlevel:\"" + userInfo.vi_memberlevel + "\",");

                        strPageList.Append("nickname:\"" + List.Rows[i]["nickname"].ToString() + "\",io_content:\""
                            + ILog.Common.Common.GetJScriptGlobalObjectEscape(Ilog.BLL.ILogOriginal.GetFinalIlogContentShow(List.Rows[i]["io_content"].ToString(), Convert.ToInt64(List.Rows[i]["is_id"]))) + "\",");

                        strPageList.Append("is_spreadnum:\"" + List.Rows[i]["is_spreadnum"].ToString() + "\",vic_commentnum:\"" + List.Rows[i]["vic_commentnum"].ToString() + "\",");
                        strPageList.Append("is_name:\"" + List.Rows[i]["is_name"].ToString() + "\",is_url:\"" + List.Rows[i]["is_url"].ToString() + "\"}");

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


        #region 转发文章
        /// <summary>
        /// 功能描述：转发文章
        /// 创建标识：ljd 20120610
        /// </summary>
        /// <param name="ilogID">博文id</param>
        /// <param name="type">转发类别 0原创 1转发</param>
        /// <param name="ilogUserID">当前博文userid</param>
        /// <param name="userid">当前转发人userid</param>
        /// <param name="content">评论内容</param>
        /// <returns></returns>
        public static string ReplyIlog(long ilogID, int type, long ilogUserID, long userid, string content)
        {
            userid = Ilog.BLL.VipILog.GetVIPUserID();

            if (content.Trim() == "")
            {
                content = "转发微博";
            }
            int urlstate = 0;

            int blogtype = 0;//原创

            //原创id
            long originalID = 0;

            ILog.Model.ILogSpread ooSpreadInfo = ILog.DAL.ILogSpread.GetSpreadInfo(ilogID, ref urlstate);
            originalID = ooSpreadInfo.io_id;

            //获取@用户id
            Dictionary<int, long> dicUserID = ILogOriginal.GetLoveItUserID(content);

            int maxuserkey = 1000;

            //判断当前博文是否是别人的传播
            //如果是别人的传播（别人的原创或转发），需取出传播表中原博文id
            if (ooSpreadInfo.is_spreadtype == 0 && ooSpreadInfo.is_isoriginal == 0 && ooSpreadInfo.is_type == 0)//别人的原创
            {
                ILog.Model.ILogSpread ooSpreadTa = ILog.DAL.ILogSpread.GetSpreadOriginalInfo(ooSpreadInfo.io_id);
                if (ooSpreadTa != null)
                {
                    ilogID = ooSpreadTa.is_id;
                    ilogUserID = ooSpreadTa.userid;
                    //转发别人的原创，给原作者发@
                    if (!dicUserID.ContainsValue(ilogUserID))
                    {
                        dicUserID.Add(maxuserkey, ilogUserID);
                        maxuserkey++;
                    }
                }
            }
            else if ((ooSpreadInfo.is_spreadtype == 0 && ooSpreadInfo.is_isoriginal == 0 && ooSpreadInfo.is_type == 1) || (ooSpreadInfo.is_spreadtype == 1 && ooSpreadInfo.is_isoriginal == 0))//别人的转发或自己的转发
            {
                ILog.Model.ILogSpread ooSpreadTa = ILog.DAL.ILogSpread.GetSpreadInfo(ooSpreadInfo.iso_id, ref urlstate);
                if (ooSpreadTa != null)
                {
                    ilogID = ooSpreadTa.is_id;
                    ilogUserID = ooSpreadTa.userid;
                    //给上篇文章和原创作者发@
                    if (!dicUserID.ContainsValue(ilogUserID))
                    {
                        dicUserID.Add(maxuserkey, ilogUserID);
                        maxuserkey++;
                    }
                }
                ILog.Model.ILogSpread ooSpreadOriginal = ILog.DAL.ILogSpread.GetSpreadOriginalInfo(ooSpreadInfo.io_id);
                if (ooSpreadOriginal != null)
                {
                    if (ooSpreadOriginal.userid != ilogUserID)
                    {
                        dicUserID.Add(maxuserkey, ooSpreadOriginal.userid);
                        maxuserkey++;
                    }
                }
                blogtype = 1;
            }
            else
            {
                //给上篇文章和原创作者发@
                if (!dicUserID.ContainsValue(ilogUserID))
                {
                    dicUserID.Add(maxuserkey, ilogUserID);
                    maxuserkey++;
                }
            }

            ILog.Model.ILogOriginal ooOriginal = BLL.ILogOriginal.GetOriginalInfo(originalID);
            if (ooOriginal != null)
            {
                string originalContent = ooOriginal.io_content;
                Dictionary<int, long> dicOriUserID = ILogOriginal.GetLoveItUserID(originalContent);
                foreach (int oriKey in dicOriUserID.Keys)
                {
                    if (!dicUserID.ContainsValue(oriKey))
                    {
                        dicUserID.Add(maxuserkey, dicOriUserID[oriKey]);
                        maxuserkey++;
                    }
                }
            }

            //处理博文时记录的移除字符串的长度
            int removeLen = 0;
            //处理博文内容，将长地址转为短地址
            ILogOriginal.OperateLoveItUrl(content, ref content, ref removeLen);
            //将表情转为ubb代码
            content = Ilog.BLL.ILogOriginal.GetIlogContentByExpression(content);

            //给自己发一条信息传播(转发)
            ILog.Model.ILogSpread ooSpreadSelf = new ILog.Model.ILogSpread();
            ooSpreadSelf.intime = DateTime.Now;
            ooSpreadSelf.is_fanuserid = userid;
            ooSpreadSelf.is_type = blogtype;
            ooSpreadSelf.iso_id = ilogID;
            ooSpreadSelf.userid = userid;
            ooSpreadSelf.io_id = originalID;
            ooSpreadSelf.is_content = content;
            ooSpreadSelf.is_spreadtype = 1;//转发
            ooSpreadSelf.iss_id = 1;//来自ilog
            ooSpreadSelf.is_isoriginal = 0;//非原创

            long newspreadid = Ilog.BLL.ILogSpread.SpreadAddAndGetID(ooSpreadSelf);

            //如果存在@用户，加入@信息表
            if (dicUserID.Count > 0)
            {
                foreach (int key in dicUserID.Keys)
                {
                    ILog.Model.ILogat ooAt = new ILog.Model.ILogat();
                    ooAt.ia_atuserid = userid;
                    ooAt.ia_content = content;//评论内容
                    ooAt.ia_type = 2;//0原创 2转发
                    ooAt.intime = DateTime.Now;
                    ooAt.is_id = 1;//ilog
                    ooAt.userid = dicUserID[key];
                    ooAt.iso_id = originalID;
                    ooAt.ia_logid = newspreadid;

                    Ilog.BLL.ILogat.AtInfoAdd(ooAt);

                    if (dicUserID[key] != userid)//如果作者是自己，只增加@记录，不增加@提醒数
                    {
                        //给每个@到的用户的@数加1
                        Ilog.BLL.VipILogCount.UpdateAtNum(ooAt.userid, ref urlstate);
                    }
                }
            }

            //给用户所有的粉丝加信息传播记录
            //获取用户所有粉丝列表
            List<long> fansList = Ilog.BLL.ILogUserFan.GetUserFanListByUserid(userid, ref urlstate);
            if (fansList.Count > 0)
            {
                foreach (long fansuserid in fansList)
                {
                    //新增信息传播表记录
                    ILog.Model.ILogSpread ooSpread = new ILog.Model.ILogSpread();
                    ooSpread.intime = DateTime.Now;
                    ooSpread.is_fanuserid = fansuserid;
                    ooSpread.is_type = 1;//转发
                    ooSpread.iso_id = newspreadid;
                    ooSpread.userid = userid;
                    ooSpread.is_spreadtype = 0;//传播
                    ooSpread.io_id = originalID;
                    ooSpread.is_content = "";
                    ooSpread.iss_id = 1;//来自ilog
                    ooSpread.is_isoriginal = 0;//非原创

                    Ilog.BLL.ILogSpread.SpreadAdd(ooSpread);
                }
            }

            //原创 当前文章转发次数加1
            if (blogtype == 0)
            {
                ILog.DAL.ILogOriginal.UpdateSpreadNum(originalID);
                ILog.DAL.ILogSpread.UpdateOriginalSpreadNum(originalID);
            }
            else//信息传播表转发文章转发次数加1
            {
                ILog.DAL.ILogOriginal.UpdateSpreadNum(originalID);
                ILog.DAL.ILogSpread.UpdateOriginalSpreadNum(originalID);
                ILog.DAL.ILogSpread.UpdateSpreadNum(ilogID);
            }

            //更新自己的博文数量加1
            ILog.DAL.VipILogCount.UpdateLogCount(userid);

            //成功标记
            StringBuilder strbResult = new StringBuilder();

            strbResult.Append("{");
            strbResult.Append("state:" + urlstate);
            strbResult.Append("}");

            return strbResult.ToString();

        }
        #endregion

        #region 更新转发的博文的转发次数
        /// <summary>
        /// 功能描述：更新转发的博文的转发次数
        /// 创建标识：ljd 20120614
        /// <param name="is_id">转发id</param>
        /// </summary>
        public static int UpdateSpreadNum(long is_id)
        {
            int resultcount = ILog.DAL.ILogSpread.UpdateSpreadNum(is_id);
            return resultcount;

        }
        #endregion


        #region 更新转发的博文的评论次数
        /// <summary>
        /// 功能描述：更新转发的博文的评论次数
        /// 创建标识：ljd 20120628
        /// <param name="is_id">转发id</param>
        /// </summary>
        public static int UpdateCommentNum(long is_id)
        {
            int resultcount = ILog.DAL.ILogSpread.UpdateCommentNum(is_id);
            return resultcount;

        }
        #endregion

        #region 更新原创向传播表中冗余的评论数
        /// <summary>
        /// 功能描述：更新原创向传播表中冗余的评论数
        /// 创建标识：ljd 20120628
        /// <param name="io_id">原创id</param>
        /// </summary>
        public static int UpdateOriginalCommentNum(long io_id)
        {
            int resultcount = ILog.DAL.ILogSpread.UpdateOriginalCommentNum(io_id);
            return resultcount;

        }
        #endregion

        #region 根据博文id与博文类型得到博文内容
        /// <summary>
        /// 功能描述：根据博文id与博文类型得到博文内容
        /// 创建标识：ljd 20120612
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static string GetContent(long is_id, int is_type)
        {
            //博文内容
            string content = ILog.DAL.ILogSpread.GetContent(is_id, is_type);
            return content;

        }
        #endregion


        #region 得到转发弹出框中的内容
        /// <summary>
        /// 功能描述：得到转发弹出框中的内容
        /// 创建标识：ljd 20120613
        /// </summary>
        /// <param name="spreadID">转发id</param>
        /// <returns></returns>
        public static string GetSpreadWindowContent(long spreadID)
        {

            int urlState = 0;

            StringBuilder strbSpreadWindow = new StringBuilder();

            try
            {
                ILog.Model.ILogSpread ooSpread = ILog.DAL.ILogSpread.GetSpreadInfo(spreadID, ref urlState);

                ILog.Model.ILogOriginal ooOriginal = ILog.DAL.ILogOriginal.GetOriginalInfo(ooSpread.io_id);


                if (ooSpread == null || ooOriginal == null)
                {
                    strbSpreadWindow.Append("{State:'0'}");  //无数据
                    return strbSpreadWindow.ToString();
                }


                //转发人userid
                long spreadUserID = ooSpread.userid;

                //转发内容
                string spreadContent = ooSpread.is_content;

                if (ooSpread.is_spreadtype == 0 && ooSpread.is_isoriginal == 0 && ooSpread.is_type == 1)//别人的转发
                {
                    ILog.Model.ILogSpread ooSpreadTa = ILog.DAL.ILogSpread.GetSpreadInfo(ooSpread.iso_id, ref urlState);
                    if (ooSpreadTa != null)
                    {
                        spreadContent = ooSpreadTa.is_content;
                        spreadUserID = ooSpreadTa.userid;
                    }
                }

                //原创
                int isoriginal = 0;

                if (ooSpread.is_spreadtype == 0 && ooSpread.is_type == 0)
                {
                    isoriginal = 1;
                }

                //原创userid
                long originalUserID = ooOriginal.userid;

                //原创昵称
                string originalNickName = Ilog.BLL.VipILog.GetNickNameByUserId(originalUserID);
                //转发username
                string spreadNickName = Ilog.BLL.VipILog.GetNickNameByUserId(spreadUserID);

                if (urlState == 0)
                {
                    strbSpreadWindow.Append("{State:'2'}");  //报错
                    return strbSpreadWindow.ToString();
                }

                //转发内容
                spreadContent = Utils.ClearUBB(spreadContent);

                spreadContent = Ilog.BLL.ILogOriginal.GetIlogContentWithExpression(spreadContent);

                strbSpreadWindow.Append("{State:'1',ogusername:'" + originalNickName + "',oguserid:'" + originalUserID
                    + "',ogcontent:'" + Microsoft.JScript.GlobalObject.escape(UBBToHtml.UBBToHTML(ooOriginal.io_content)) + "',spusername:'" + spreadNickName + "',spuserid:'" + spreadUserID + "',spcontent:'"
                    + Microsoft.JScript.GlobalObject.escape(spreadContent) + "',isoriginal:'" + isoriginal + "'}");  //有数据  
            }
            catch (Exception)
            {
                strbSpreadWindow.Append("{State:'2'}");  //报错
            }


            return strbSpreadWindow.ToString();

        }

        #endregion

        #region 分页（json节点见表字段）
        /// <summary>
        /// 功能描述：转发数据分页
        /// 创建标识：ljd 20120613
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页数据</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="ilogid">博文id</param>
        /// <param name="ilogtype">博文类别 0原创 1转发</param>
        /// <returns></returns>
        public static string GetSpreadPageList(int PageCurrent, int PageSize, long ilogid, int ilogtype)
        {
            string strTableName = "ilog_spread";
            string strFieldKey = "is_id";
            string strFieldShow = "is_id,userid,is_content,intime";
            string strFieldOrder = "is_id desc ";
            string strWhere = string.Format(" iso_id = {0} and is_type={1} ", ilogid, ilogtype);

            int RecordCount = 0;

            DataTable dblReplyPageList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            return GetReplyJsonList(dblReplyPageList, RecordCount);
        }
        #endregion

        #region 构建转发分页json
        /// <summary>
        /// 功能描述：构建转发分页json
        /// 创建标识：ljd 20120613
        /// </summary>
        /// <param name="List">数据</param>
        /// <param name="RecordCount">总页数</param>
        /// <returns></returns>
        private static string GetReplyJsonList(DataTable dblReplyPageList, int RecordCount)
        {
            int count = dblReplyPageList.Rows.Count;

            StringBuilder strbReplyPageList = new StringBuilder();

            strbReplyPageList.Append("{ReplyList:");

            try
            {
                if (count > 0)
                {
                    strbReplyPageList.Append("[{State:'1'}");

                    strbReplyPageList.Append(",{RecordCount:'" + RecordCount + "'},");

                    for (int i = 0; i < count; i++)
                    {

                        DateTime date = Convert.ToDateTime(dblReplyPageList.Rows[i]["intime"]);

                        string content = dblReplyPageList.Rows[i]["is_content"].ToString();

                        strbReplyPageList.Append("{is_id:\"" + dblReplyPageList.Rows[i]["is_id"].ToString() + "\",userid:\"" + dblReplyPageList.Rows[i]["userid"].ToString() + "\",");
                        strbReplyPageList.Append("is_content:\"" + Ilog.BLL.ILogOriginal.GetFinalIlogContentShow(dblReplyPageList.Rows[i]["is_content"].ToString(),
                            Convert.ToInt64(dblReplyPageList.Rows[i]["is_id"])) + "\",intime:\"" + ILog.Common.Common.GetIlogTime(date) + "\"}");

                        //最后一个元素不需要加逗号
                        if ((count - 1) > i)
                        {
                            strbReplyPageList.Append(",");
                        }
                    }
                }
                else
                {
                    strbReplyPageList.Append("[{State:'2'}");  //无数据
                }
            }
            catch
            {
                strbReplyPageList.Append("[{State:'0'}");
            }

            strbReplyPageList.Append("]}");

            return strbReplyPageList.ToString();
        }
        #endregion


        #region 博文内页转发分页. by lx on 20120628

        /// <summary>
        /// 博文内页分页数据. by lx on 20120628
        /// </summary>        
        /// <param name="RecordCount">总页数</param>
        /// <param name="currentid">文章ID</param>
        /// <param name="type">是否原创 0 否 1是</param>
        /// <param name="RecordCount">当前页码</param>
        /// <param name="PageSize">每页数据</param>
        /// <returns></returns>
        public static string GetForwordList(long ioId, int type, int PageCurrent, int PageSize)
        {

            StringBuilder result = new StringBuilder();

            string strTableName = " ilog_spread ";
            string strFieldKey = "is_id";
            string strFieldShow = "[is_id],[iso_id],[is_type],[is_content],[is_fanuserid],[userid],[intime],[iss_id],[is_spreadnum],[is_commentnum],[is_spreadtype],[io_id],[is_isoriginal]";
            string strFieldOrder = " is_id desc ";

            int urlstate = 0;
            ILog.Model.ILogSpread ooSpread = ILog.DAL.ILogSpread.GetSpreadInfo(ioId, ref urlstate);
            //如果是别人的传播（别人的原创或转发），需取出传播表中原博文id
            if (ooSpread.is_spreadtype == 0 && ooSpread.is_isoriginal == 0 && ooSpread.is_type == 0)//别人的原创
            {
                ILog.Model.ILogSpread ooSpreadTa = ILog.DAL.ILogSpread.GetSpreadOriginalInfo(ooSpread.io_id);
                ioId = ooSpreadTa.is_id;
            }
            //如果是别人的传播（别人的原创或转发），需取出传播表中原博文id
            else if (ooSpread.is_spreadtype == 0 && ooSpread.is_isoriginal == 0 && ooSpread.is_type == 1)//别人的转发
            {
                ioId = ooSpread.iso_id;
            }

            string strWhere = "";

            if (ooSpread.is_spreadtype == 0 && ooSpread.is_type == 0)//原创（自己或他人的）
            {
                strWhere = string.Format("io_id={0} AND dbo.fn_GetSpreadType(is_spreadtype,is_type,is_isoriginal)=3", ooSpread.io_id);
            }

            else
            {
                strWhere = string.Format("iso_id={0} AND dbo.fn_GetSpreadType(is_spreadtype,is_type,is_isoriginal)=3", ioId);
            }

            int RecordCount = 0;

            DataTable dblCommentPageList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            result.Append("{ List:");

            if (dblCommentPageList.Rows.Count > 0)
            {

                #region 添加一列时间



                //时间
                dblCommentPageList.Columns.Add("timeStr", Type.GetType("System.String"));
                //博文内容
                dblCommentPageList.Columns.Add("content", Type.GetType("System.String"));
                //昵称
                dblCommentPageList.Columns.Add("nickname", Type.GetType("System.String"));
                //头像
                dblCommentPageList.Columns.Add("face", Type.GetType("System.String"));
                //会员等级
                dblCommentPageList.Columns.Add("memberlevel", Type.GetType("System.String"));

                for (int i = 0; i < dblCommentPageList.Rows.Count; i++)
                {
                    //修改时间
                    DateTime time = string.IsNullOrEmpty(dblCommentPageList.Rows[i]["intime"].ToString()) == true ? DateTime.Now : Convert.ToDateTime(dblCommentPageList.Rows[i]["intime"]);

                    ILog.Model.VipILog ooVipIlog = BLL.VipILog.GetModelByUserID(Convert.ToInt64(dblCommentPageList.Rows[i]["userid"]));

                    dblCommentPageList.Rows[i]["content"] = Ilog.BLL.ILogOriginal.GetFinalIlogContentShow(dblCommentPageList.Rows[i]["is_content"].ToString(), Convert.ToInt64(dblCommentPageList.Rows[i]["is_id"]));
                    dblCommentPageList.Rows[i]["timeStr"] = ILog.Common.Common.GetIlogTime(time);
                    dblCommentPageList.Rows[i]["nickname"] = ooVipIlog.nickname;
                    dblCommentPageList.Rows[i]["face"] = ooVipIlog.face;
                    dblCommentPageList.Rows[i]["memberlevel"] = ooVipIlog.vi_memberlevel;

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
        /// 博文内页分页数据. by lx on 20120719
        /// </summary>
        /// <param name="ioId">博文编号</param>
        /// <param name="type">是否原创 0 否 1是</param>
        /// <param name="PageCurrent">当前页</param>
        /// <param name="PageSize">每页数据</param>
        /// <returns>字符串</returns>
        public static string GetContentForwordPageList(long ioId, int type, int PageCurrent, int PageSize)
        {

            StringBuilder result = new StringBuilder();

            try
            {

                string strTableName = " ilog_spread ";
                string strFieldKey = "is_id";
                string strFieldShow = "[is_id],[iso_id],[is_type],[is_content],[is_fanuserid],[userid],[intime],[iss_id],[is_spreadnum],[is_commentnum],[is_spreadtype],[io_id],[is_isoriginal],(select nickname from vip_ilog where vip_ilog.userid = ilog_spread.is_fanuserid) as nickname,(select face from vip_ilog where vip_ilog.userid = ilog_spread.userid) as face";
                string strFieldOrder = " is_id desc ";

                int urlstate = 0;
                ILog.Model.ILogSpread ooSpread = ILog.DAL.ILogSpread.GetSpreadInfo(ioId, ref urlstate);
                //如果是别人的传播（别人的原创或转发），需取出传播表中原博文id
                if (ooSpread.is_spreadtype == 0 && ooSpread.is_isoriginal == 0 && ooSpread.is_type == 0)//别人的原创
                {
                    ILog.Model.ILogSpread ooSpreadTa = ILog.DAL.ILogSpread.GetSpreadOriginalInfo(ooSpread.io_id);
                    ioId = ooSpreadTa.is_id;
                }
                //如果是别人的传播（别人的原创或转发），需取出传播表中原博文id
                else if (ooSpread.is_spreadtype == 0 && ooSpread.is_isoriginal == 0 && ooSpread.is_type == 1)//别人的转发
                {
                    ioId = ooSpread.iso_id;
                }

                string strWhere = "";

                if (ooSpread.is_spreadtype == 0 && ooSpread.is_type == 0)//原创（自己或他人的）
                {
                    strWhere = string.Format("io_id={0} AND dbo.fn_GetSpreadType(is_spreadtype,is_type,is_isoriginal)=3", ooSpread.io_id);
                }

                else
                {
                    strWhere = string.Format("iso_id={0} AND dbo.fn_GetSpreadType(is_spreadtype,is_type,is_isoriginal)=3", ioId);
                }

                int RecordCount = 0;

                DataTable dblCommentPageList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

                result.Append(GetContentJsonString(dblCommentPageList, RecordCount));

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
        public static string GetContentJsonString(DataTable List, int RecordCount)
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
                        strPageList.Append("{is_id:\"" + List.Rows[i]["is_id"].ToString() + "\",iso_id:\"" + List.Rows[i]["iso_id"].ToString() + "\",");
                        strPageList.Append("is_type:\"" + List.Rows[i]["is_type"].ToString() + "\",io_id:\"" + List.Rows[i]["io_id"].ToString() + "\",");
                        strPageList.Append("nickname:\"" + List.Rows[i]["nickname"].ToString() + "\",is_fanuserid:\"" + List.Rows[i]["is_fanuserid"].ToString() + "\",");
                        strPageList.Append("userid:\"" + List.Rows[i]["userid"].ToString() + "\",iss_id:\"" + List.Rows[i]["iss_id"].ToString() + "\",");

                        //查看发@的时间是不是当天
                        strPageList.Append("timeStr:\"" + ILog.Common.Common.GetIlogTime(Convert.ToDateTime(List.Rows[i]["intime"])) + "\",");

                        //ILog.Common.Common.GetJScriptGlobalObjectEscape();转码

                        //内容转码
                        strPageList.Append("content:\"" + ILog.Common.Common.GetJScriptGlobalObjectEscape(Ilog.BLL.ILogOriginal.GetFinalIlogContentShow(List.Rows[i]["is_content"].ToString(), Convert.ToInt64(List.Rows[i]["is_id"]))) + "\",");


                        strPageList.Append("is_spreadnum:\"" + List.Rows[i]["is_spreadnum"].ToString() + "\",is_commentnum:\"" + List.Rows[i]["is_commentnum"].ToString() + "\",");
                        strPageList.Append("is_isoriginal:\"" + List.Rows[i]["is_isoriginal"].ToString() + "\",face:\"" + List.Rows[i]["face"].ToString() + "\"}");

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

        #region 根据原创id和是否原创得到传播表信息
        /// <summary>
        /// 功能描述：根据原创id和是否原创得到传播表信息
        /// 创建标识：ljd 20120701
        /// <param name="is_id">流水号</param>
        /// <param name="urlstate">是否报错</param>
        /// </summary>
        public static ILog.Model.ILogSpread GetSpreadOriginalInfo(long io_id)
        {
            ILog.Model.ILogSpread ooSpread = ILog.DAL.ILogSpread.GetSpreadOriginalInfo(io_id);
            return ooSpread;

        }
        #endregion

        #region 得到最新的博文列表
        /// <summary>
        /// 功能描述：得到最新的博文列表
        /// 创建标识：ljd 20120705
        /// </summary>
        public static List<ILog.Model.ILogSpread> GetNewSpreadList()
        {
            List<ILog.Model.ILogSpread> spreadList = ILog.DAL.ILogSpread.GetNewSpreadList();
            return spreadList;

        }
        #endregion

        #region 得到最新的博文列表（大家正在说）（json格式）
        /// <summary>
        /// 功能描述：得到最新的博文列表（大家正在说）（json格式）
        /// 创建标识：ljd 20120705
        /// </summary>
        public static string GetNewSpreadListJsonStr()
        {
            StringBuilder strbList = new StringBuilder();

            try
            {
                List<ILog.Model.ILogSpread> spreadList = ILog.DAL.ILogSpread.GetNewSpreadList();

                strbList.Append("{ilogList:[");

                if (spreadList.Count > 0)
                {
                    strbList.Append("{State:'1'},");

                    foreach (ILog.Model.ILogSpread ooSpread in spreadList)
                    {
                        ILog.Model.VipILog ooIlog = Ilog.BLL.VipILog.GetModelByUserID(ooSpread.userid);

                        //替换图片为“”
                        string ilogContent = Regex.Replace(ooSpread.is_content, @"\[img=(\d*|),(\d*|)\](.[^\]]*)(\[\/img\])", "");

                        //清除ubb代码
                        ilogContent = Utils.ClearUBB(ilogContent);

                        //清除视频图标
                        ilogContent = ilogContent.Replace("http://simg.instrument.com.cn/ilog/blue/images/video.jpg", "");

                        strbList.Append("{userid:'" + ooSpread.userid + "',is_content:'" + ilogContent + "',intime:'" + ILog.Common.Common.GetIlogTime(ooSpread.intime) + "',nickname:'"
                             + ooIlog.nickname + "',face:'" + ooIlog.face + "'},");
                    }
                    strbList.Remove(strbList.Length - 1, 1);
                    strbList.Append("]}");
                }
                else
                {
                    strbList.Append("{State:'0'}]}");
                }

            }
            catch (Exception)
            {

                strbList.Append("{State:'-1'}]}");
            }
            return strbList.ToString();

        }
        #endregion


        #region 根据搜索关键字随机显示用户
        /// <summary>
        /// 根据搜索关键字随机显示用户
        /// </summary>
        /// <param name="Originaltitle">关键字</param>
        /// <param name="userid">当前用户id</param>
        /// <returns></returns>
        public static string GetSearchPersonalInfo(string Originaltitle, long userid)
        {
            StringBuilder strPageList = new StringBuilder();

            strPageList.Append("{List:");

            try
            {
                List<ILog.Model.VipILog> GetILogPersonalList = ILog.DAL.ILogSpread.GetSearchPersonalInfo(Originaltitle);

                int count = GetILogPersonalList.Count;

                int j = 0;

                //获取加i信息
                ILog.Model.VipILog userInfo;

                if (count > 0)  //找到用户
                {
                    strPageList.Append("[{State:'1'},");

                    foreach (ILog.Model.VipILog GetILogPersonalListInfo in GetILogPersonalList)
                    {

                        //加i信息
                        userInfo = ILog.DAL.VipILog.GetVipIlogInfoById(GetILogPersonalListInfo.userid);

                        strPageList.Append("{userid:\"" + GetILogPersonalListInfo.userid + "\",");

                        strPageList.Append("isfollow:\"" + Ilog.BLL.ILogUserConcern.UserConcernOnly_State(userid, GetILogPersonalListInfo.userid) + "\",");

                        //是否双项关注：true关注
                        strPageList.Append("isuserconcernstate:\"" + ILog.DAL.ILogUserConcern.UserConcern_State(userid, GetILogPersonalListInfo.userid) + "\",");

                        strPageList.Append("vi_memberlevel:\"" + userInfo.vi_memberlevel + "\",");
                        strPageList.Append("vi_id:\"" + GetILogPersonalListInfo.vi_id + "\",");
                        strPageList.Append("nickname:\"" + GetILogPersonalListInfo.nickname + "\",");
                        strPageList.Append("vic_fannum:\"" + GetILogPersonalListInfo.vic_fannum + "\",");
                        strPageList.Append("face:\"" + GetILogPersonalListInfo.face + "\",}");

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

        #region 得到用户最新的博文信息
        /// <summary>
        /// 功能描述：得到用户最新的博文信息
        /// 创建标识：ljd 20120627
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static ILog.Model.ILogSpread GetLastestILogInfo(long userid)
        {
            ILog.Model.ILogSpread ooSpread = ILog.DAL.ILogSpread.GetLastestILogInfo(userid);
            return ooSpread;

        }
        #endregion

        #region 获取博文内页单条原创/转发信息.by lx on 20120716


        /// <summary>
        /// 获取博文内页单条原创/转发信息.by lx on 20120716
        /// </summary>
        /// <param name="id">博文编号</param>
        /// <returns></returns>
        public static string GetContentInfoById(long id)
        {

            //定义结果
            StringBuilder result = new StringBuilder();

            result.Append("{list:");

            try
            {

                //获取数据
                DataTable dTable = ILog.DAL.ILogSpread.GetContentInfoById(id);



                if (dTable.Rows.Count > 0)
                {

                    string spreadContent = string.Empty;

                    #region 添加一列时间

                    dTable.Columns.Add("timeStr", Type.GetType("System.String"));
                    dTable.Columns.Add("content", Type.GetType("System.String"));
                    dTable.Columns.Add("faceStr", Type.GetType("System.String"));

                    for (int i = 0; i < dTable.Rows.Count; i++)
                    {
                        //修改时间
                        DateTime time = string.IsNullOrEmpty(dTable.Rows[i]["intime"].ToString()) == true ? DateTime.Now : Convert.ToDateTime(dTable.Rows[i]["intime"]);

                        dTable.Rows[i]["content"] = Microsoft.JScript.GlobalObject.escape(Ilog.BLL.ILogOriginal.GetFinalIlogContentShow(dTable.Rows[i]["io_content"].ToString(), Convert.ToInt64(dTable.Rows[i]["is_id"])));
                        dTable.Rows[i]["timeStr"] = ILog.Common.Common.GetIlogTime(time);

                        spreadContent = Utils.ClearUBB(dTable.Rows[i]["io_content"].ToString());


                        dTable.Rows[i]["io_content"] = Microsoft.JScript.GlobalObject.escape(dTable.Rows[i]["io_content"].ToString());


                        spreadContent = Ilog.BLL.ILogOriginal.GetIlogContentWithExpression(spreadContent);
                        dTable.Rows[i]["faceStr"] = Microsoft.JScript.GlobalObject.escape(spreadContent);


                    }

                    #endregion

                    string listStr = Com.ILog.Utils.Utils.DataTableToJSON(dTable).ToString();

                    listStr.Substring(0, listStr.Length - 1);

                    result.Append(listStr.Substring(0, listStr.Length - 1));

                }
                else
                {

                    result.Append("[]");

                }

            }
            catch (Exception ex)
            {

                result.Append("[]");

            }

            result.Append("}");

            return result.ToString();

        }


        #endregion



    }

}
