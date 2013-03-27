using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ilog.BLL
{
    public class VipILogLimits
    {
        #region 查看范围是否存在（True：存在，False：不存在）
        /// <summary>
        /// 查看范围是否存在（True：存在，False：不存在）
        /// <param name="vil_id">流水号</param>
        /// <param name="userid">用户id</param>
        /// </summary>
        public static string LimitsExists(int userid, int vil_id)
        {
            StringBuilder strLimitsExists = new StringBuilder();

            strLimitsExists.Append("var strLimitsExistsJsonObject = ");
            strLimitsExists.Append("({");
            strLimitsExists.Append("\"Exists\": \"" + ILog.DAL.VipILogLimits.LimitsExists(userid, vil_id).ToString() + "\"");
            strLimitsExists.Append("})");

            return strLimitsExists.ToString();
        }
        #endregion

        #region  增加一条范围数据
        /// <summary>
        /// 增加一条范围数据
        /// <param name="model">范围实体</param>
        /// </summary>
        public static string LimitsAdd(ILog.Model.VipILogLimits model)
        {
            StringBuilder strLimitsAdd = new StringBuilder();

            strLimitsAdd.Append("var strLimitsAddJsonObject = ");
            strLimitsAdd.Append("({");
            strLimitsAdd.Append("\"state\": \"" + ILog.DAL.VipILogLimits.LimitsAdd(model).ToString() + "\"");
            strLimitsAdd.Append("})");

            return strLimitsAdd.ToString();
        }
        #endregion

        #region 更新一条数据
        /// <summary>
        /// 更新一条数据
        /// <param name="model">范围实体</param>
        /// </summary>
        public static string LimitsUpdate(ILog.Model.VipILogLimits model)
        {
            StringBuilder strLimitsUpdate = new StringBuilder();

            strLimitsUpdate.Append("var strLimitsUpdateJsonObject = ");
            strLimitsUpdate.Append("({");
            strLimitsUpdate.Append("\"state\": \"" + ILog.DAL.VipILogLimits.LimitsUpdate(model).ToString() + "\"");
            strLimitsUpdate.Append("})");

            return strLimitsUpdate.ToString();
        }
        #endregion

        #region 删除一条数据
        /// <summary>
        /// 删除一条数据
        /// <param name="vic_id">流水号</param>
        /// <param name="userid">用户id</param> 
        /// </summary>
        public static string LimitsDel(int userid, int vil_id)
        {
            StringBuilder strLimitsDel = new StringBuilder();

            strLimitsDel.Append("var strLimitsDelJsonObject = ");
            strLimitsDel.Append("({");
            strLimitsDel.Append("\"state\": \"" + ILog.DAL.VipILogLimits.LimitsDel(userid, vil_id).ToString() + "\"");
            strLimitsDel.Append("})");

            return strLimitsDel.ToString();
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="userid">用户id</param>
        /// <param name="vil_id">流水号</param>
        /// </summary>
        public static string GetModel(int userid, int vil_id)
        {
            DataTable dblLimitsModelList = ILog.DAL.VipILogLimits.GetModel(userid, vil_id);

            //构建josn字符串 
            string strLimitsJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblLimitsModelList).ToString();

            return strLimitsJosn;
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
        public static string GetLimitsPageList(string tbname, string FieldKey, int PageCurrent, int PageSize, string FieldShow, string FieldOrder, string Where, ref int RecordCount)
        {
            string strTableName = " vip_ilog_limits ";
            string strFieldKey = "vil_id";
            string strFieldShow = " vil_id,userid,vil_systemconcernnum,vil_systemfannum,intime ";
            string strFieldOrder = " vil_id desc ";
            string strWhere = " ";

            DataTable dblLimitsPageList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            //构建josn字符串 
            string strLimitsPageJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblLimitsPageList).ToString();

            return strLimitsPageJosn;
        }
        #endregion
    }
}
