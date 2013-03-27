using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ilog.BLL
{
    public class ILogMobileCheck
    {
        #region 查看发送短信是否存在（True：存在，False：不存在）
        /// <summary>
        /// 查看发送短信是否存在（True：存在，False：不存在）
        /// <param name="im_id">流水号</param>
        /// </summary>
        public static string MobileCheckExists(int im_id)
        {
            StringBuilder strMobileCheckExists = new StringBuilder();

            strMobileCheckExists.Append("var strMobileCheckExistsJsonObject = ");
            strMobileCheckExists.Append("({");
            strMobileCheckExists.Append("\"Exists\": \"" + ILog.DAL.ILogMobileCheck.MobileCheckExists(im_id).ToString() + "\"");
            strMobileCheckExists.Append("})");

            return strMobileCheckExists.ToString();
        }
        #endregion

        #region  增加一条数据
        /// <summary>
        /// 增加一条数据
        /// <param name="model">短信实体</param>
        /// </summary>
        public static string MobileCheckAdd(ILog.Model.ILogMobileCheck model)
        {
            StringBuilder strMobileCheckAdd = new StringBuilder();

            strMobileCheckAdd.Append("var strLimitsAddJsonObject = ");
            strMobileCheckAdd.Append("({");
            strMobileCheckAdd.Append("\"state\": \"" + ILog.DAL.ILogMobileCheck.MobileCheckAdd(model).ToString() + "\"");
            strMobileCheckAdd.Append("})");

            return strMobileCheckAdd.ToString();
        }
        #endregion

        #region 更新一条数据
        /// <summary>
        /// 更新一条数据
        /// <param name="model">范围实体</param>
        /// </summary>
        public static string MobileCheckUpdate(ILog.Model.ILogMobileCheck model)
        {
            StringBuilder strMobileCheckUpdate = new StringBuilder();

            strMobileCheckUpdate.Append("var strMobileCheckUpdateJsonObject = ");
            strMobileCheckUpdate.Append("({");
            strMobileCheckUpdate.Append("\"state\": \"" + ILog.DAL.ILogMobileCheck.MobileCheckUpdate(model).ToString() + "\"");
            strMobileCheckUpdate.Append("})");

            return strMobileCheckUpdate.ToString();
        }
        #endregion

        #region 删除一条数据
        /// <summary>
        /// 删除一条数据
        /// <param name="im_id">流水号</param>
        /// </summary>
        public static string MobileCheckDel(int im_id)
        {
            StringBuilder strMobileCheckDel = new StringBuilder();

            strMobileCheckDel.Append("var strMobileCheckDelJsonObject = ");
            strMobileCheckDel.Append("({");
            strMobileCheckDel.Append("\"state\": \"" + ILog.DAL.ILogMobileCheck.MobileCheckDel(im_id).ToString() + "\"");
            strMobileCheckDel.Append("})");

            return strMobileCheckDel.ToString();
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="im_id">流水号</param>
        /// </summary>
        public static string GetModel(int im_id)
        {
            DataTable dblMobileCheckList = ILog.DAL.ILogMobileCheck.GetModel(im_id);

            //构建josn字符串 
            string strMobileCheckList = Com.ILog.Utils.Utils.DataTableToJSON(dblMobileCheckList).ToString();

            return strMobileCheckList;
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
        public static string GetMobileCheckPageList(string tbname, string FieldKey, int PageCurrent, int PageSize, string FieldShow, string FieldOrder, string Where, ref int RecordCount)
        {
            string strTableName = " ilog_MobileCheck ";
            string strFieldKey = "im_id";
            string strFieldShow = " im_id,im_mobilenumber,im_checkcode,intime ";
            string strFieldOrder = " im_id desc ";
            string strWhere = " ";

            DataTable dblMobileCheckPageList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            //构建josn字符串 
            string strdblMobileCheckPageListJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblMobileCheckPageList).ToString();

            return strdblMobileCheckPageListJosn;
        }
        #endregion

        #region 根据用户手机号得到最近一次发送手机短信的时间

        /// <summary>
        /// 功能描述：根据用户手机号得到最近一次发送手机短信的时间
        /// 创建标识：ljd 20120527
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="mobile">发送手机号</param>
        /// <param name="urlstate">是否报错</param>
        /// <returns></returns>
        public static ILog.Model.ILogMobileCheck GetLastestMobileSendTime(long userid, string mobile, ref int urlstate)
        {
            ILog.Model.ILogMobileCheck  ooMobileCheck= ILog.DAL.ILogMobileCheck.GetLastestMobileSendTime(userid, mobile, ref urlstate);
            return ooMobileCheck;

        }

        #endregion

    }
}
