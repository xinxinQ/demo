using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Ilog.BLL
{
    public class ILogCertificate
    {
        #region 是否存在该某条认证记录记录（True：存在，False：不存在）
        /// <summary>
        /// 是否存在该某条认证记录记录（True：存在，False：不存在）
        /// <param name="ia_id">流水号</param>
        /// <param name="userid">用户id</param>
        /// </summary>
        public static string CertificateInfoEXISTS(int userid, int ic_id)
        {
            StringBuilder strCertificateInfoEXISTS = new StringBuilder();

            strCertificateInfoEXISTS.Append("var CertificateInfoEXISTSJsonObject = ");
            strCertificateInfoEXISTS.Append("({");
            strCertificateInfoEXISTS.Append("\"Exists\": \"" + ILog.DAL.ILogCertificate.CertificateInfoEXISTS(userid, ic_id).ToString() + "\"");
            strCertificateInfoEXISTS.Append("})");

            return strCertificateInfoEXISTS.ToString();
        }
        #endregion

        #region  增加一条认证数据
        /// <summary>
        /// 增加一条认证数据
        /// <param name="model">认证表实体</param>
        /// </summary>
        public static string CertificateInfoAdd(ILog.Model.ILogCertificate model)
        {
            StringBuilder strCertificateInfoAdd = new StringBuilder();

            strCertificateInfoAdd.Append("var CertificateInfoAddJsonObject = ");
            strCertificateInfoAdd.Append("({");
            strCertificateInfoAdd.Append("\"state\": \"" + ILog.DAL.ILogCertificate.CertificateInfoAdd(model).ToString() + "\"");
            strCertificateInfoAdd.Append("})");

            return strCertificateInfoAdd.ToString();
        }
        #endregion

        #region 更新一条认证数据
        /// <summary>
        /// 更新一条认证数据
        /// <param name="model">认证表实体</param>
        /// </summary>
        public static string CertificateUpdate(ILog.Model.ILogCertificate model)
        {
            StringBuilder strCertificateUpdate = new StringBuilder();

            strCertificateUpdate.Append("var CertificateUpdateJsonObject = ");
            strCertificateUpdate.Append("({");
            strCertificateUpdate.Append("\"state\": \"" + ILog.DAL.ILogCertificate.CertificateUpdate(model).ToString() + "\"");
            strCertificateUpdate.Append("})");

            return strCertificateUpdate.ToString();
        }
        #endregion

        #region 删除一条认证数据
        /// <summary>
        /// 删除一条认证数据
        /// <param name="ic_id">流水号</param>
        /// <param name="userid">用户id</param>
        /// </summary>
        public static string CertificateDel(int userid, int ic_id)
        {
            StringBuilder strCertificateDel = new StringBuilder();

            strCertificateDel.Append("var CertificateDelJsonObject = ");
            strCertificateDel.Append("({");
            strCertificateDel.Append("\"state\": \"" + ILog.DAL.ILogCertificate.CertificateDel(userid, ic_id).ToString() + "\"");
            strCertificateDel.Append("})");

            return strCertificateDel.ToString();
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="ic_id">流水号</param>
        /// <param name="userid">用户id</param>
        /// </summary>
        public static string GetModel(int userid, int ic_id)
        {
            DataTable dblModelList = ILog.DAL.ILogCertificate.GetModel(userid, ic_id);

            //构建josn字符串 
            string ILogAtModelJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblModelList).ToString();

            return ILogAtModelJosn;
        }
        #endregion

        #region 根据用户编号获取认证信息

        /// <summary>
        /// 根据用户编号获取认证信息
        /// </summary>
        public static List<ILog.Model.ILogCertificate> GetCertificateInfoByUserId(long userid)
        {

            return ILog.DAL.ILogCertificate.GetCertificateInfoByUserId(userid);

        }

        #endregion

        #region 认证信息分页（json节点见表字段）
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
        public static string GetCertificatePageList(string tbname, string FieldKey, int PageCurrent, int PageSize, string FieldShow, string FieldOrder, string Where, ref int RecordCount)
        {
            string strTableName = " ilog_certificate ";
            string strFieldKey = "ic_id";
            string strFieldShow = "ic_id,userid,ic_type,ic_name,ic_pic,intime ";
            string strFieldOrder = " ic_id desc ";
            string strWhere = " ";

            DataTable dblCertificatePageList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            //构建josn字符串 
            string strdblCertificatePageJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblCertificatePageList).ToString();

            return strdblCertificatePageJosn;
        }
        #endregion

        #region 根据用户id与证件类型得到证件的id
        /// <summary>
        /// 功能描述：根据用户id与证件类型得到证件的id
        /// 创建标识：ljd 20120604
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="type">证件类型</param>
        /// <param name="urlState">是否报错</param>
        /// <returns></returns>
        public static long GetIDByUserIDAndType(long userid, int type, ref int urlState)
        {
            long ia_id = ILog.DAL.ILogCertificate.GetIDByUserIDAndType(userid, type, ref urlState);
            return ia_id;

        }
        #endregion

        #region  增加一条认证数据
        /// <summary>
        /// 功能描述：增加一条认证数据
        /// 创建标识：ljd 20120604
        /// <param name="model">认证表实体</param>
        /// </summary>
        public static int AddCertificateInfo(ILog.Model.ILogCertificate model)
        {
            int resultCount = ILog.DAL.ILogCertificate.CertificateInfoAdd(model);

            return resultCount;

        }
        #endregion

        #region 更新一条认证数据
        /// <summary>
        /// 功能描述：更新一条认证数据
        /// 创建标识：ljd 20120604
        /// <param name="model">认证表实体</param>
        /// </summary>
        public static int UpdateCertificate(ILog.Model.ILogCertificate model)
        {
            int resultCount = ILog.DAL.ILogCertificate.CertificateUpdate(model);

            return resultCount;

        }
        #endregion

    }
}
