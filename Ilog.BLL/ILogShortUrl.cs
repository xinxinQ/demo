using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ilog.BLL
{
    public class ILogShortUrl
    {
        #region 查看某个短地址是否存在（True：存在，False：不存在）
        /// <summary>
        /// 查看某个短地址是否存在（True：存在，False：不存在）
        /// <param name="ir_id">流水号</param>
        /// </summary>
        public static string ShortUrlExists(int isu_id)
        {
            StringBuilder strShortUrlExists = new StringBuilder();

            strShortUrlExists.Append("var strShortUrlExistsJsonObject = ");
            strShortUrlExists.Append("({");
            strShortUrlExists.Append("\"Exists\": \"" + ILog.DAL.ILogShortUrl.ShortUrlExists(isu_id).ToString() + "\"");
            strShortUrlExists.Append("})");

            return strShortUrlExists.ToString();
        }
        #endregion

        #region  添加短地址
        /// <summary>
        /// 添加短地址
        /// <param name="model">短地址实体</param>
        /// </summary>
        public static string ShortUrlAdd(ILog.Model.ILogShortUrl model)
        {
            StringBuilder strShortUrlAdd = new StringBuilder();

            strShortUrlAdd.Append("var strShortUrlAddJsonObject = ");
            strShortUrlAdd.Append("({");
            strShortUrlAdd.Append("\"state\": \"" + ILog.DAL.ILogShortUrl.ShortUrlAdd(model).ToString() + "\"");
            strShortUrlAdd.Append("})");

            return strShortUrlAdd.ToString();
        }
        #endregion

        #region 更新短地址
        /// <summary>
        /// 更新短地址
        /// <param name="model">短地址实体</param>
        /// </summary>
        public static string ShortUrlUpdate(ILog.Model.ILogShortUrl model)
        {
            StringBuilder strShortUrlUpdate = new StringBuilder();

            strShortUrlUpdate.Append("var strShortUrlUpdateJsonObject = ");
            strShortUrlUpdate.Append("({");
            strShortUrlUpdate.Append("\"state\": \"" + ILog.DAL.ILogShortUrl.ShortUrlUpdate(model).ToString() + "\"");
            strShortUrlUpdate.Append("})");

            return strShortUrlUpdate.ToString();
        }
        #endregion

        #region 删除短地址
        /// <summary>
        /// 删除短地址
        /// <param name="isu_id">流水号</param>
        /// </summary>
        public static string ShortUrlDel(int isu_id)
        {
            StringBuilder strShortUrlDel = new StringBuilder();

            strShortUrlDel.Append("var strShortUrlDelJsonObject = ");
            strShortUrlDel.Append("({");
            strShortUrlDel.Append("\"state\": \"" + ILog.DAL.ILogShortUrl.ShortUrlDel(isu_id).ToString() + "\"");
            strShortUrlDel.Append("})");

            return strShortUrlDel.ToString();
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="isu_id">流水号</param>
        /// </summary>
        public static string GetModel(int isu_id)
        {
            DataTable dblILogShortUrlModelList = ILog.DAL.ILogShortUrl.GetModel(isu_id);

            //构建josn字符串 
            string strdblILogShortUrlModelListJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblILogShortUrlModelList).ToString();

            return strdblILogShortUrlModelListJosn;
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
        public static string GetILogShortUrlPageList(string tbname, string FieldKey, int PageCurrent, int PageSize, string FieldShow, string FieldOrder, string Where, ref int RecordCount)
        {
            string strTableName = " ilog_shorturl ";
            string strFieldKey = "isu_id";
            string strFieldShow = " isu_id,isu_url,isu_shorturl,isu_num,intime ";
            string strFieldOrder = " isu_id desc ";
            string strWhere = " ";

            DataTable dblILogShortUrlPageList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            //构建josn字符串 
            string strLogShortUrlPageJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblILogShortUrlPageList).ToString();

            return strLogShortUrlPageJosn;
        }
        #endregion


        #region 创建不重复的短地址
        /// <summary>
        /// 功能描述：创建不重复的短地址
        /// 创建标识：ljd 20120628
        /// </summary>
        /// <param name="url">长地址</param>
        /// <returns>短地址</returns>
        public static string GetShortUrl(string url)
        {
            string shorturl = ILog.Common.Common.ShortUrl(url);
            //判断短地址是否存在
            ILog.Model.ILogShortUrl ooShortUrl = Ilog.BLL.ILogShortUrl.GetShortUrlInfoByShortUrl(shorturl);
            if (ooShortUrl != null)
            {
                shorturl = GetShortUrl(url);
            }
            return shorturl;

        }
        #endregion


        #region 根据长地址得到短地址实体对象

        /// <summary>
        /// 功能描述：根据长地址得到短地址实体对象
        /// 创建标识：ljd 20120618
        /// </summary>
        /// <param name="url">长地址</param>
        /// <returns>短地址实体对象</returns>
        public static ILog.Model.ILogShortUrl GetShorUrlInfoByUrl(string url)
        {
            ILog.Model.ILogShortUrl ooShortUrl = ILog.DAL.ILogShortUrl.GetShorUrlInfoByUrl(url);
            return ooShortUrl;

        }
        #endregion

        #region 根据短地址得到短地址信息

        /// <summary>
        /// 功能描述：根据短地址得到短地址信息
        /// 创建标识：ljd 20120618
        /// </summary>
        /// <param name="shorturl">短地址</param>
        /// <returns>长地址</returns>
        public static ILog.Model.ILogShortUrl GetShortUrlInfoByShortUrl(string shorturl)
        {
            ILog.Model.ILogShortUrl ooShortUrl = ILog.DAL.ILogShortUrl.GetShortUrlInfoByShortUrl(shorturl);
            return ooShortUrl;

        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 功能描述：得到一个对象实体
        /// 创建标识：ljd 20120618
        /// </summary>
        public static ILog.Model.ILogShortUrl GetShortUrlInfo(long isu_id)
        {
            ILog.Model.ILogShortUrl ooShortUrl = ILog.DAL.ILogShortUrl.GetShortUrlInfo(isu_id);
            return ooShortUrl;

        }
        #endregion

        #region  添加短地址并返回id
        /// <summary>
        /// 功能描述：添加短地址并返回id
        /// 创建标识：ljd 20120628
        /// <param name="model">短地址实体</param>
        /// </summary>
        public static long ShortUrlAddAndGetID(ILog.Model.ILogShortUrl model)
        {
            long isu_id = ILog.DAL.ILogShortUrl.ShortUrlAdd(model);

            return isu_id;

        }
        #endregion

        #region 得到唯一的长短地址均不重复的短地址实体对象
       
        /// <summary>
        /// 功能描述：得到唯一的长短地址均不重复的短地址实体对象
        /// 创建标识：ljd 20120629
        /// </summary>
        /// <param name="url">输入的url</param>
        /// <param name="type">url类型 0普通url 1视频url</param>
        /// <returns></returns>
        public static ILog.Model.ILogShortUrl GetTheOneShortInfo(string url,int type)
        {
            //长地址变为短地址
            /*
             * 判断长地址是否存在，如果存在，直接读取之前的短地址
             * 判断链接地址是否与短地址相同，如果相同，读取对应的长地址
             * 不存在长地址与短地址，直接生成新的短地址
             */

            //短地址
            string shortUrl = "";
            //根据长地址获得短地址实体对象
            ILog.Model.ILogShortUrl ooShortUrl_Short = Ilog.BLL.ILogShortUrl.GetShorUrlInfoByUrl(url);
            //根据短地址得到短地址实体对象
            ILog.Model.ILogShortUrl ooShortUrl_Long = Ilog.BLL.ILogShortUrl.GetShortUrlInfoByShortUrl(url);

            //短地址id
            long suid = 0;
            //短地址类型
            int urlType = 0;

            if (ooShortUrl_Short == null && ooShortUrl_Long == null)//既不存在短地址也不存在长地址
            {
                shortUrl = Ilog.BLL.ILogShortUrl.GetShortUrl(url);
                //插入短地址表
                ILog.Model.ILogShortUrl ooUrl = new ILog.Model.ILogShortUrl();
                ooUrl.isu_num = 0;
                ooUrl.isu_shorturl = shortUrl;
                ooUrl.isu_url = url;
                ooUrl.isu_type = type;
                suid = Ilog.BLL.ILogShortUrl.ShortUrlAddAndGetID(ooUrl);
            }

            else if (ooShortUrl_Long != null)//该地址即为短地址
            {
                shortUrl = url;
                url = ooShortUrl_Long.isu_url;
                suid = ooShortUrl_Long.isu_id;
                urlType = ooShortUrl_Long.isu_type;
            }
            else if (ooShortUrl_Short != null)
            {
                shortUrl = ooShortUrl_Short.isu_shorturl;
                suid = ooShortUrl_Short.isu_id;
                urlType = ooShortUrl_Short.isu_type;
            }

            if (type == 1 && urlType==0)
            {
                ILog.DAL.ILogShortUrl.ShortUrlUpdateType(suid, type);
                urlType = 1;
            }

            ILog.Model.ILogShortUrl ooShortUrl = new ILog.Model.ILogShortUrl();
            ooShortUrl.isu_id = suid;
            ooShortUrl.isu_shorturl = shortUrl;
            ooShortUrl.isu_url = url;
            ooShortUrl.isu_type = urlType;

            return ooShortUrl;

        }

        #endregion

    }
}
