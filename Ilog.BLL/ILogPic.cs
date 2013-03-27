using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ilog.BLL
{
    public class ILogPic
    {
        #region 查看某张缩略图是否存在（True：存在，False：不存在）
        /// <summary>
        /// 查看某张缩略图是否存在（True：存在，False：不存在）
        /// <param name="ip_id">流水号</param>
        /// </summary>
        public static string PicExists(int ip_id)
        {
            StringBuilder strPicExists = new StringBuilder();

            strPicExists.Append("var strPicExistsJsonObject = ");
            strPicExists.Append("({");
            strPicExists.Append("\"Exists\": \"" + ILog.DAL.ILogPic.PicExists(ip_id).ToString() + "\"");
            strPicExists.Append("})");

            return strPicExists.ToString();
        }
        #endregion

        #region  增加一条缩略图数据
        /// <summary>
        /// 增加一条缩略图数据
        /// <param name="model">缩略图实体</param>
        /// </summary>
        public static string PicAdd(ILog.Model.ILogPic model)
        {
            StringBuilder strPicAdd = new StringBuilder();

            strPicAdd.Append("{");
            strPicAdd.Append("state: \"" + ILog.DAL.ILogPic.PicAdd(model).ToString() + "\"");
            strPicAdd.Append("}");

            return strPicAdd.ToString();
        }
        #endregion

        #region 更新缩略图数据
        /// <summary>
        /// 更新缩略图数据
        /// <param name="model">缩略图实体</param>
        /// </summary>
        public static string PicUpdate(ILog.Model.ILogPic model)
        {
            StringBuilder strPicUpdate = new StringBuilder();

            strPicUpdate.Append("var strPicUpdateJsonObject = ");
            strPicUpdate.Append("({");
            strPicUpdate.Append("\"state\": \"" + ILog.DAL.ILogPic.PicUpdate(model).ToString() + "\"");
            strPicUpdate.Append("})");

            return strPicUpdate.ToString();
        }
        #endregion

        #region 删除缩略图数据
        /// <summary>
        /// 删除缩略图数据
        /// <param name="io_id">流水号</param>
        /// </summary>
        public static string PicDel(int ip_id)
        {
            StringBuilder strPicDel = new StringBuilder();

            strPicDel.Append("var strPicDelJsonObject = ");
            strPicDel.Append("({");
            strPicDel.Append("\"state\": \"" + ILog.DAL.ILogPic.PicDel(ip_id).ToString() + "\"");
            strPicDel.Append("})");

            return strPicDel.ToString();
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="ip_id">流水号</param>
        /// </summary>
        public static string GetModel(int ip_id)
        {
            DataTable dblOriginalModelList = ILog.DAL.ILogPic.GetModel(ip_id);

            //构建josn字符串 
            string strOriginalModelListJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblOriginalModelList).ToString();

            return strOriginalModelListJosn;
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
        public static string GetPicPageList(string tbname, string FieldKey, int PageCurrent, int PageSize, string FieldShow, string FieldOrder, string Where, ref int RecordCount)
        {
            string strTableName = " ilog_pic ";
            string strFieldKey = "ip_id";
            string strFieldShow = " ip_id,io_id,ip_name,intime,ip_type ";
            string strFieldOrder = " ip_id desc ";
            string strWhere = " ";

            DataTable dblPicPageList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            //构建josn字符串 
            string strPicPageJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblPicPageList).ToString();

            return strPicPageJosn;
        }
        #endregion

        #region 更新图片表中带有原创标记的原创id
        /// <summary>
        /// 功能描述：更新图片表中带有原创标记的原创id
        /// 创建标识：ljd 20120610
        /// </summary>
        /// <param name="mark">原创标记</param>
        /// <param name="originalID">原创id</param>
        /// <param name="urlstate">json是否成功</param>
        /// <returns></returns>
        public static int UpdatePicOriginalIDByMark(string mark, long originalID, ref int urlstate)
        {
            int resultCount = ILog.DAL.ILogPic.UpdatePicOriginalIDByMark(mark, originalID, ref urlstate);
            return resultCount;

        }
        #endregion

        #region 根据原创id得到图片列表
        /// <summary>
        /// 功能描述：根据原创id得到图片列表
        /// 创建标识：ljd 20120625
        /// </summary>
        /// <param name="io_id">原创id</param>
        /// <returns></returns>
        public static List<ILog.Model.ILogPic> GetPicList(long io_id)
        {
            List<ILog.Model.ILogPic> picList = ILog.DAL.ILogPic.GetPicList(io_id);
            return picList;

        }
        #endregion
    }
}
