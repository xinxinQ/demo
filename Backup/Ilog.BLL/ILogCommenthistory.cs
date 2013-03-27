using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace Ilog.BLL
{
    public class ILogCommenthistory
    {

        #region 得到昨日评论最多的原创博文（热门评论博文列表）
        /// <summary>
        /// 功能描述：得到昨日评论最多的原创博文（热门评论博文列表）
        /// 创建标识：ljd 20120705
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="type">0 每日 1每周</param>
        /// <returns></returns>
        public static DataTable GetYesterdayCommentList(int PageCurrent, int PageSize,ref  int RecordCount,int type)
        {
            DataTable dblSearchList = ILog.DAL.ILogCommenthistory.GetYesterdayCommentList(PageCurrent, PageSize, ref RecordCount, type);
            return dblSearchList;

        }
        #endregion


        #region 得到热门评论的博文列表的json字符串
        /// <summary>
        /// 功能描述：得到热门评论的博文列表的json字符串
        /// 创建标识：ljd 20120705
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="type">0 每日 1每周</param>
        /// <returns></returns> 
        public static string GetYesterdayCommentJsonList(int PageCurrent, int PageSize, int type)
        {
            int RecordCount = 0;

            DataTable List = GetYesterdayCommentList(PageCurrent, PageSize, ref RecordCount, type);

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
                        ILog.Model.ILogOriginal ooOriginal = Ilog.BLL.ILogOriginal.GetOriginalInfo(Convert.ToInt64(List.Rows[i]["io_id"]));

                        if (ooOriginal != null)
                        {
                            strPageList.Append("{io_id:\"" + ooOriginal.io_id + "\",userid:\"" + List.Rows[i]["userid"].ToString() + "\",");
                            strPageList.Append("io_spreadnum:\"" + ooOriginal.io_spreadnum + "\",io_commentnum:\"" + ooOriginal.io_commentnum + "\",");
                            strPageList.Append("io_haspic:\"" + ooOriginal.io_haspic+ "\",ih_commentnum:\"" + List.Rows[i]["ih_commentnum"].ToString() + "\",");

                            ILog.Model.VipILog ooVip = Ilog.BLL.VipILog.GetModelByUserID(ooOriginal.userid);

                            strPageList.Append("nickname:\"" + ooVip.nickname + "\",is_url:\"" + List.Rows[i]["is_url"].ToString() + "\",");
                            strPageList.Append("intime:\"" + ILog.Common.Common.GetIlogTime(ooOriginal.intime) + "\",level:\"" + ooVip.vi_memberlevel + "\",");

                            strPageList.Append("face:\"" + ooVip.face + "\",io_content:\""
                                + ILog.Common.Common.GetJScriptGlobalObjectEscape(Ilog.BLL.ILogOriginal.GetFinalIlogContentShow(ooOriginal.io_content,ooOriginal.io_id)) + "\",");

                            strPageList.Append("is_name:\"" + List.Rows[i]["is_name"].ToString() + "\",iso_id:\"" + List.Rows[i]["iso_id"].ToString() + "\"}");

                            //最后一个元素不需要加逗号
                            if ((count - 1) > i)
                            {
                                strPageList.Append(",");
                            }
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
