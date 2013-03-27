using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace ILog.DAL
{
    public class ILogSpreadhistory
    {
        #region 得到昨日转发最多的原创博文（热门转发博文列表）
        /// <summary>
        /// 功能描述：得到昨日转发最多的原创博文（热门转发博文列表）
        /// 创建标识：ljd 20120705
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="type">0 每日 1每周</param>
        /// <returns></returns>
        public static DataTable GetYesterdaySpreadList(int PageCurrent, int PageSize, ref int RecordCount,int type)
        {
            string strTableName = "ilog_spreadhistory";
            string strFieldKey = "ih_id";
            string strFieldShow = " io_id,userid,(select is_url from ilog_source where is_id = ilog_spreadhistory.is_id ) as is_url";
            strFieldShow += ",ih_spreadNum,(select is_name from ilog_source where is_id = ilog_spreadhistory.is_id ) as is_name";
            strFieldShow += ",(select is_id from ilog_spread where is_spreadtype=0 AND is_type=0 AND is_isoriginal=1 AND io_id=ilog_spreadhistory.io_id) AS iso_id";

            string strFieldOrder = " ih_spreadNum desc ";

            string strWhere = " ih_spreadNum>0 AND ih_type=" + type ;

            DataTable dblSearchList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            return dblSearchList;

        }
        #endregion

    }
}
