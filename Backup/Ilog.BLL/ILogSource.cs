using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ilog.BLL
{
    public class ILogSource
    {
        #region 查看来源信息是否存在（True：存在，False：不存在）
        /// <summary>
        /// 查看来源信息是否存在（True：存在，False：不存在）
        /// <param name="is_id">流水号</param>
        /// </summary>
        public static string SourceExists(int is_id)
        {
            StringBuilder strSourceExists = new StringBuilder();

            strSourceExists.Append("var strSourceExistsJsonObject = ");
            strSourceExists.Append("({");
            strSourceExists.Append("\"Exists\": \"" + ILog.DAL.ILogSource.SourceExists(is_id).ToString() + "\"");
            strSourceExists.Append("})");

            return strSourceExists.ToString();
        }
        #endregion

        #region  增加一条来源
        /// <summary>
        /// 增加一条来源
        /// <param name="model">原来表实体</param>
        /// </summary>
        public static string SourceAdd(ILog.Model.ILogSource model)
        {
            StringBuilder strSourceAdd = new StringBuilder();

            strSourceAdd.Append("var strSourceAddJsonObject = ");
            strSourceAdd.Append("({");
            strSourceAdd.Append("\"state\": \"" + ILog.DAL.ILogSource.SourceAdd(model).ToString() + "\"");
            strSourceAdd.Append("})");

            return strSourceAdd.ToString();
        }
        #endregion

        #region 更新一条来源
        /// <summary>
        /// 更新一条来源
        /// <param name="model">来源表实体</param>
        /// </summary>
        public static string SourceUpdate(ILog.Model.ILogSource model)
        {
            StringBuilder strSourceUpdate = new StringBuilder();

            strSourceUpdate.Append("var strSourceUpdateJsonObject = ");
            strSourceUpdate.Append("({");
            strSourceUpdate.Append("\"state\": \"" + ILog.DAL.ILogSource.SourceUpdate(model).ToString() + "\"");
            strSourceUpdate.Append("})");

            return strSourceUpdate.ToString();
        }
        #endregion

        #region 删除一条来源数据
        /// <summary>
        /// 删除一条来源数据
        /// <param name="is_id">流水号</param>
        /// </summary>
        public static string SourceDel(int is_id)
        {
            StringBuilder strSourceDel = new StringBuilder();

            strSourceDel.Append("var strSourceDelJsonObject = ");
            strSourceDel.Append("({");
            strSourceDel.Append("\"state\": \"" + ILog.DAL.ILogSource.SourceDel(is_id).ToString() + "\"");
            strSourceDel.Append("})");

            return strSourceDel.ToString();
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="isu_id">流水号</param>
        /// </summary>
        public static string GetModel(int isu_id)
        {
            DataTable dblIILogSourceModelList = ILog.DAL.ILogSource.GetModel(isu_id);

            //构建josn字符串 
            string strLogSourceJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblIILogSourceModelList).ToString();

            return strLogSourceJosn;
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
        public static string GetILogSourcePageList(string tbname, string FieldKey, int PageCurrent, int PageSize, string FieldShow, string FieldOrder, string Where, ref int RecordCount)
        {
            string strTableName = " ilog_source ";
            string strFieldKey = "is_id";
            string strFieldShow = " is_id,is_name,is_url,intime ";
            string strFieldOrder = " is_id desc ";
            string strWhere = " ";

            DataTable dblILogSourcePageList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            //构建josn字符串 
            string strLogSourcePageJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblILogSourcePageList).ToString();

            return strLogSourcePageJosn;
        }
        #endregion

        #region 得到链接来源的详细信息
        /// <summary>
        /// 功能描述：得到链接来源的详细信息
        /// 创建标识：ljd 20120628
        /// <param name="isu_id">流水号</param>
        /// </summary>
        public static ILog.Model.ILogSource GetModelById(long isu_id)
        {
            ILog.Model.ILogSource ooSource = ILog.DAL.ILogSource.GetModelById(isu_id);
            return ooSource;

        }
        #endregion

    }
}
