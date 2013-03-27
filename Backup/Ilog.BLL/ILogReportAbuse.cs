using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ilog.BLL
{
    public class ILogReportAbuse
    {
        #region 查看举报否存在（True：存在，False：不存在）
        /// <summary>
        /// 查看举报否存在（True：存在，False：不存在）
        /// <param name="ir_id">流水号</param>
        /// </summary>
        public static string ReportAbuseExists(int ir_id)
        {
            StringBuilder strReportAbuseExists = new StringBuilder();

            strReportAbuseExists.Append("var strReportAbuseExistsJsonObject = ");
            strReportAbuseExists.Append("({");
            strReportAbuseExists.Append("\"Exists\": \"" + ILog.DAL.ILogReportAbuse.ReportAbuseExists(ir_id).ToString() + "\"");
            strReportAbuseExists.Append("})");

            return strReportAbuseExists.ToString();
        }
        #endregion

        #region  添加举报信息
        /// <summary>
        /// 添加举报信息
        /// <param name="model">举报表实体</param>
        /// </summary>
        public static string ReportAbuseAdd(ILog.Model.ILogReportAbuse model)
        {
            StringBuilder strReportAbuseAdd = new StringBuilder();

            strReportAbuseAdd.Append("var strReportAbuseAddJsonObject = ");
            strReportAbuseAdd.Append("({");
            strReportAbuseAdd.Append("\"state\": \"" + ILog.DAL.ILogReportAbuse.ReportAbuseAdd(model).ToString() + "\"");
            strReportAbuseAdd.Append("})");

            return strReportAbuseAdd.ToString();
        }
        #endregion

        #region 更新举报信息
        /// <summary>
        /// 更新举报信息
        /// <param name="model">举报表实体</param>
        /// </summary>
        public static string ReportAbuseUpdate(ILog.Model.ILogReportAbuse model)
        {
            StringBuilder strReportAbuseUpdate = new StringBuilder();

            strReportAbuseUpdate.Append("var strReportAbuseUpdateJsonObject = ");
            strReportAbuseUpdate.Append("({");
            strReportAbuseUpdate.Append("\"state\": \"" + ILog.DAL.ILogReportAbuse.ReportAbuseUpdate(model).ToString() + "\"");
            strReportAbuseUpdate.Append("})");

            return strReportAbuseUpdate.ToString();
        }
        #endregion

        #region 删除一举报信息条数据
        /// <summary>
        /// 删除一举报信息条数据
        /// <param name="ir_id">流水号</param>
        /// </summary>
        public static string ReportAbuseDel(int ir_id)
        {
            StringBuilder strReportAbuseDel = new StringBuilder();

            strReportAbuseDel.Append("var strReportAbuseDelJsonObject = ");
            strReportAbuseDel.Append("({");
            strReportAbuseDel.Append("\"state\": \"" + ILog.DAL.ILogReportAbuse.ReportAbuseDel(ir_id).ToString() + "\"");
            strReportAbuseDel.Append("})");

            return strReportAbuseDel.ToString();
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="ip_id">流水号</param>
        /// </summary>
        public static string GetModel(int ip_id)
        {
            DataTable dblReportAbuseModelList = ILog.DAL.ILogReportAbuse.GetModel(ip_id);

            //构建josn字符串 
            string strReportAbuseListJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblReportAbuseModelList).ToString();

            return strReportAbuseListJosn;
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
        public static string GetReportAbusePageList(string tbname, string FieldKey, int PageCurrent, int PageSize, string FieldShow, string FieldOrder, string Where, ref int RecordCount)
        {
            string strTableName = " ilog_reportabuse ";
            string strFieldKey = "ir_id";
            string strFieldShow = " ir_id,userid,ir_content,ip,intime,ir_desc,ir_time ";
            string strFieldOrder = " ir_id desc ";
            string strWhere = " ";

            DataTable dblReportAbusePageList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            //构建josn字符串 
            string strReportAbusePageJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblReportAbusePageList).ToString();

            return strReportAbusePageJosn;
        }
        #endregion
    }
}
