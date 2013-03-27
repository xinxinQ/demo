using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Ilog.BLL
{
    public class ILogat
    {
        #region 是否存在该记录（True：存在，False：不存在）
        /// <summary>
        /// 是否存在该记录（True：存在，False：不存在）
        /// <param name="ia_id">流水号</param>
        /// </summary>
        public static string AtInfoExists(int ia_id)
        {
            StringBuilder stratInfoExists = new StringBuilder();

            stratInfoExists.Append("var atInfoExistsJsonObject = ");
            stratInfoExists.Append("({");
            stratInfoExists.Append("\"Exists\": \"" + ILog.DAL.ILogat.AtInfoExists(ia_id).ToString() + "\"");
            stratInfoExists.Append("})");

            return stratInfoExists.ToString();
        }
        #endregion

        #region  增加一条at（返回受影响行数）
        /// <summary>
        /// 增加一条at（返回受影响行数）
        /// <param name="model">站短实体</param>
        /// </summary>
        public static string AtInfoAdd(ILog.Model.ILogat model)
        {
            StringBuilder stratInfoAdd = new StringBuilder();

            stratInfoAdd.Append("var ILogAtInfoAddJsonObject = ");
            stratInfoAdd.Append("({");
            stratInfoAdd.Append("\"state\": \"" + ILog.DAL.ILogat.AtInfoAdd(model).ToString() + "\"");
            stratInfoAdd.Append("})");

            return stratInfoAdd.ToString();
        }
        #endregion

        #region 更新一条at信息（返回受影响行数）
        /// <summary>
        /// 更新一条at信息（返回受影响行数）
        /// <param name="model">站短实体</param>
        /// </summary>
        public static string AtInfoUpdate(ILog.Model.ILogat model)
        {
            StringBuilder stratInfoUpdate = new StringBuilder();

            stratInfoUpdate.Append("var atInfoUpdateJsonObject = ");
            stratInfoUpdate.Append("({");
            stratInfoUpdate.Append("\"state\": \"" + ILog.DAL.ILogat.AtInfoUpdate(model).ToString() + "\"");
            stratInfoUpdate.Append("})");

            return stratInfoUpdate.ToString();
        }
        #endregion

        #region 删除一条站短（返回受影响行数）
        /// <summary>
        /// 删除一条站短（返回受影响行数）
        /// <param name="id">流水号</param>
        /// </summary>
        public static string AtInfoDel(int ia_id)
        {
            StringBuilder stratInfoDel = new StringBuilder();

            stratInfoDel.Append("var atInfoDelJsonObject = ");
            stratInfoDel.Append("({");
            stratInfoDel.Append("\"state\": \"" + ILog.DAL.ILogat.AtInfoDel(ia_id).ToString() + "\"");
            stratInfoDel.Append("})");

            return stratInfoDel.ToString();
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="id">流水号</param>
        /// </summary>
        public static string GetModel(int ia_id)
        {
            DataTable dblModelList = ILog.DAL.ILogat.GetModel(ia_id);

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
        /// <param name="ation">查看类型（0：博文，1：评论 ）</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static string GetATPageList(int PageCurrent, int PageSize, string ation, long userid)
        {
            string strTableName = " ilog_at ";
            string strFieldKey = "ia_id";
            string strFieldShow = " ia_id,ia_atuserid,ia_content,intime,ia_type,iso_id,ia_logid,is_id,userid";
            strFieldShow += ",dbo.fn_GetAtSpreadNum(ia_logid,ia_type) as is_spreadnum,dbo.fn_GetAtCommentNum(ia_logid,ia_type) as is_commentnum,dbo.fn_GetAtSpreadID(ia_logid,ia_type) AS spreadid";
            strFieldShow += ",(CASE WHEN  ia_type=0 THEN (select io_haspic from ilog_original ori where ori.io_id = ilog_at.iso_id) ELSE 0 END) as is_haspic,dbo.fn_GetAtCommentedUserID(ia_logid) AS commenteduserid";
            strFieldShow += " , dbo.fn_GetAtCommentedUserNickName(ia_logid) AS commentednickname,dbo.fn_GetAtContent(ia_logid,ia_type) AS atContent";
            strFieldShow += ",(select ic_commentid from ilog_comment where ic_id=ia_logid) ic_commentid,(select is_url from ilog_source where is_id = ilog_at.is_id ) as is_url";
            string strFieldOrder = " ia_id desc ";

            string strWhere = " userid = " + userid;

            if (ation == "0")
            {
                strWhere += " and (ia_type = 0 or ia_type=2)";
            }
            else//@我的评论
            {
                strWhere += " AND ia_logid NOT IN (SELECT ic_id FROM ilog_comment WHERE ic_state=2) and ia_type=1";
            }


            int RecordCount = 0;

            DataTable dblATPageList = new DataTable();

            DataSet dsATPageList = ILog.Common.Common.DataSelect_New(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            if (dsATPageList != null && dsATPageList.Tables.Count > 0)
            {
                dblATPageList = dsATPageList.Tables[0];
            }

            return GetJsonList(dblATPageList, RecordCount);
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
        /// <param name="ation">查看类型（0：博文，1：评论 ）</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static string GetSearchList(int PageCurrent, int PageSize, string keyword, string ation, long userid)
        {
            string strTableName = " ilog_at ";
            string strFieldKey = "ia_id";
            string strFieldShow = " ia_id,ia_atuserid,ia_content,intime,ia_type,iso_id,ia_logid,is_id,userid,(select nickname from vip_ilog where vip_ilog.userid = ilog_at.ia_atuserid) as nickname ";
            strFieldShow += " ,(select face from vip_ilog where vip_ilog.userid = ilog_at.ia_atuserid) as face";
            strFieldShow += ",dbo.fn_GetAtSpreadNum(ia_logid,ia_type) as is_spreadnum,dbo.fn_GetAtCommentNum(ia_logid,ia_type) as is_commentnum,dbo.fn_GetAtSpreadID(ia_logid,ia_type) AS spreadid";
            strFieldShow += ",(CASE WHEN  ia_type=0 THEN (select io_haspic from ilog_original ori where ori.io_id = ilog_at.iso_id) ELSE 0 END) as is_haspic,dbo.fn_GetAtCommentedUserID(ia_logid) AS commenteduserid";
            strFieldShow += " ,dbo.fn_GetAtCommentedUserNickName(ia_logid) AS commentednickname,dbo.fn_GetAtContent(ia_logid,ia_type) AS atContent";
            strFieldShow += ",(select ic_commentid from ilog_comment where ic_id=ia_logid) ic_commentid";
            string strFieldOrder = " ia_id desc ";

            string strWhere = " userid = " + userid;

            strWhere += " AND ((dbo.fn_GetAtCommentedUserNickName(ia_logid) LIKE '%" + keyword + "%') OR ((select nickname from vip_ilog where vip_ilog.userid = ilog_at.ia_atuserid) LIKE '%" + keyword + "%'))";
            //" (dbo.fn_GetAtContent(ia_logid,ia_type)  LIKE '%" + keyword + "%' OR ";

            if (ation == "0")
            {
                strWhere += " and (ia_type = 0 or ia_type=2)";
            }
            else//@我的评论
            {
                strWhere += " AND ia_logid NOT IN (SELECT ic_id FROM ilog_comment WHERE ic_state=2) and ia_type=1";
            }


            int RecordCount = 0;

            DataTable dblATPageList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            return GetJsonList(dblATPageList, RecordCount);

        }
        #endregion

        #region 构建json
        /// <summary>
        /// 构建json
        /// </summary>
        /// <param name="List">数据</param>
        /// <param name="RecordCount">总页数</param>
        /// <returns></returns>
        private static string GetJsonList(DataTable dblATPageList, int RecordCount)
        {
            int count = dblATPageList.Rows.Count;

            int RowsCount = 45 * RecordCount;

            StringBuilder strATPageList = new StringBuilder();

            strATPageList.Append("{ILList:");

            try
            {
                if (count > 0)
                {
                    strATPageList.Append("[{State:'1'}");

                    strATPageList.Append(",{RecordCount:'" + (RecordCount > 10 ? 10 : RecordCount) + "'},");  //所有列表页最多显示10页

                    for (int i = 0; i < count; i++)
                    {

                        DateTime date = Convert.ToDateTime(dblATPageList.Rows[i]["intime"]);

                        ILog.Model.ILogSource ooSource = Ilog.BLL.ILogSource.GetModelById(Convert.ToInt64(dblATPageList.Rows[i]["is_id"]));

                        strATPageList.Append("{ia_id:\"" + dblATPageList.Rows[i]["ia_id"].ToString() + "\",userid:\"" + dblATPageList.Rows[i]["userid"].ToString() + "\",ia_logid:\""
                            + dblATPageList.Rows[i]["ia_logid"].ToString() + "\",");
                        strATPageList.Append("ia_content:\""
                            + ILog.Common.Common.GetJScriptGlobalObjectEscape(Ilog.BLL.ILogOriginal.GetFinalIlogContentShow(dblATPageList.Rows[i]["ia_content"].ToString(),
                            Convert.ToInt64(dblATPageList.Rows[i]["ia_id"]))) + "\",");

                        //查看发@的时间是不是当天
                        strATPageList.Append("intime:\"" + ILog.Common.Common.GetIlogTime(Convert.ToDateTime(dblATPageList.Rows[i]["intime"])) + "\",");

                        strATPageList.Append("ia_atuserid:\"" + dblATPageList.Rows[i]["ia_atuserid"].ToString() + "\",ic_commentid:\"" + dblATPageList.Rows[i]["ic_commentid"].ToString() + "\",");

                        strATPageList.Append("iso_id:\"" + dblATPageList.Rows[i]["iso_id"].ToString() + "\",spreadid:\"" + dblATPageList.Rows[i]["spreadid"].ToString() + "\",");

                        strATPageList.Append("commenteduserid:\"" + dblATPageList.Rows[i]["commenteduserid"].ToString() + "\",commentednickname:\"" + dblATPageList.Rows[i]["commentednickname"].ToString() + "\",");

                        strATPageList.Append("ia_type:\"" + dblATPageList.Rows[i]["ia_type"].ToString() + "\",is_name:\"" + ooSource.is_name + "\",");
                        strATPageList.Append("is_url:\"" + ooSource.is_url + "\",");

                        ILog.Model.VipILog ooVip = Ilog.BLL.VipILog.GetModelByUserID(Convert.ToInt64(dblATPageList.Rows[i]["ia_atuserid"]));
                        strATPageList.Append("is_id:\"" + dblATPageList.Rows[i]["is_id"].ToString() + "\",nickname:\"" + ooVip.nickname + "\",");
                        strATPageList.Append("face:\"" + ooVip.face + "\",is_spreadnum:\"" + dblATPageList.Rows[i]["is_spreadnum"].ToString() + "\",memberlevel:\"" + ooVip.vi_memberlevel + "\",");
                        strATPageList.Append("atContent:\""
                            + ILog.Common.Common.GetJScriptGlobalObjectEscape(Ilog.BLL.ILogOriginal.GetClearUUBAndChangeExpression(dblATPageList.Rows[i]["atContent"].ToString(), 0, "")) + "\",");
                        strATPageList.Append("is_commentnum:\"" + dblATPageList.Rows[i]["is_commentnum"].ToString() + "\",is_haspic:\"" + dblATPageList.Rows[i]["is_haspic"].ToString() + "\"}");

                        //最后一个元素不需要加逗号
                        if ((count - 1) > i)
                        {
                            strATPageList.Append(",");
                        }
                    }
                }
                else
                {
                    strATPageList.Append("[{State:'2'}");  //无数据
                }
            }
            catch
            {
                strATPageList.Append("[{State:'0'}");
            }

            strATPageList.Append("]}");

            return strATPageList.ToString();
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

            strCommentPageList.Append("{ILList:");

            strCommentPageList.Append("[{State:'2'}");  //无数据

            strCommentPageList.Append("]}");

            return strCommentPageList.ToString();
        }
        #endregion

        #region 根据搜索关键字查询用户列表
        /// <summary>
        /// 功能描述：根据搜索关键字查询用户列表
        /// 创建标识：ljd 20120718
        /// </summary>
        /// <param name="keyword">搜索关键字</param>
        /// <param name="userid">当前用户id</param>
        /// <param name="ation">0 博文 1评论</param>
        /// <returns></returns>
        public static List<ILog.Model.VipILog> GetAtUserList(string keyword, long userid, int ation)
        {
            List<ILog.Model.VipILog> vipList = ILog.DAL.ILogat.GetAtUserList("%" + keyword + "%", userid, ation);
            return vipList;

        }
        #endregion


        #region "提到我的"页面的智能搜索用户
        /// <summary>
        /// 功能描述："提到我的"页面的智能搜索用户
        /// 创建标识：ljd 20120718
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="userid">当前用户id</param>
        /// <param name="ation">操作类型查看类型（0：博文，1：评论 ）</param>
        /// <returns></returns>
        public static string GetAtUserListJsonStr(string keyword, int ation, long userid)
        {
            StringBuilder strPageList = new StringBuilder();

            strPageList.Append("{List:");

            try
            {
                List<ILog.Model.VipILog> vipList = GetAtUserList(keyword, userid, ation);

                int count = vipList.Count;

                int j = 0;

                if (count > 0)  //找到用户
                {
                    strPageList.Append("[{State:'1'},");

                    foreach (ILog.Model.VipILog ooILog in vipList)
                    {
                        strPageList.Append("{nickname:\"" + ooILog.nickname + "\"}");
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
