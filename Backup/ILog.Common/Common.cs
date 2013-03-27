using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

using System.Data.SqlClient;

using Com.ILog.Data;

namespace ILog.Common
{
    public class Common
    {
        #region 数据分页
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
        public static DataTable GetPageList(string tbname, string FieldKey, int PageCurrent, int PageSize, string FieldShow, string FieldOrder, string Where, ref int RecordCount)
        {
            DataTable dblVipMailList = Com.ILog.Data.DataAggregate.DataSelect(tbname, FieldKey, PageCurrent, PageSize, FieldShow, FieldOrder, Where, ref RecordCount).Tables[0];

            return dblVipMailList;
        }
        #endregion

        #region 获取分页数据
        /// <summary>
        /// 获取分页数据
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
        public static DataSet DataSelect_New(string tbname, string FieldKey, int PageCurrent, int PageSize, string FieldShow, string FieldOrder, string Where, ref int RecordCount)
        {
            SqlConnection con = DbHelperSQL.GetConnection();
            SqlDataAdapter dad = new SqlDataAdapter();
            dad.SelectCommand = new SqlCommand();
            dad.SelectCommand.Connection = con;
            dad.SelectCommand.CommandText = "sp_PageView2005";
            dad.SelectCommand.CommandType = CommandType.StoredProcedure;

            dad.SelectCommand.Parameters.Add("@tbname", SqlDbType.NVarChar, 128).Value = tbname;
            dad.SelectCommand.Parameters.Add("@FieldKey", SqlDbType.NVarChar, 128).Value = FieldKey;
            dad.SelectCommand.Parameters.Add("@PageCurrent", SqlDbType.Int).Value = PageCurrent;
            dad.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = PageSize;
            dad.SelectCommand.Parameters.Add("@FieldShow", SqlDbType.NVarChar, 1000).Value = FieldShow;
            dad.SelectCommand.Parameters.Add("@FieldOrder", SqlDbType.NVarChar, 1000).Value = FieldOrder;
            dad.SelectCommand.Parameters.Add("@Where", SqlDbType.NVarChar, 1000).Value = Where;
            dad.SelectCommand.Parameters.Add("@PageCount", SqlDbType.Int).Direction = ParameterDirection.Output;

            DataSet dst = new DataSet();

            try
            {
                dad.Fill(dst);
                RecordCount = (Int32)dad.SelectCommand.Parameters["@PageCount"].Value; //求出总记录数，该值是output出来的值 
                return dst;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
                dad.Dispose();
            }

        }
        #endregion

        #region 判断是否是数字类型
        /// <summary>
        /// 判断是否是数字类型
        /// </summary>
        /// <param name="value">要要交验的字符</param>
        /// <returns></returns>
        public static bool Int_IsType(string value)
        {
            Regex regexInt = new Regex(@"^\d*$");

            if (regexInt.IsMatch(value))
            {
                return true;          //匹配到了是数字
            }
            return false;
        }
        #endregion

        #region 判断是否是有sql注入关键字
        /// <summary>
        /// 判断是否是有sql注入关键字
        /// </summary>
        /// <param name="value">字符内容</param>
        /// <returns></returns>
        public static bool ISProcessSqlStr(string value)
        {
            bool ReturnValue = true;

            try
            {
                if (!string.IsNullOrEmpty(value.Trim()))
                {
                    string StrSql = "exec|insert+|select+|delete|update|count|chr|mid|master+|truncate|char|declare|drop+|drop+table|creat+|create|*|iframe|script|";
                    StrSql += "exec+|insert|delete+|update+|count(|count+|chr+|+mid(|+mid+|+master+|truncate+|char+|+char(|declare+|drop+table|creat+table|'";
                    string[] arrSqlStr = StrSql.Split('|');

                    foreach (string strs in arrSqlStr)
                    {
                        if (value.ToLower().IndexOf(strs) >= 0)
                        {
                            ReturnValue = false;
                            break;
                        }
                    }
                }
            }
            catch
            {
                ReturnValue = false;
            }
            return ReturnValue;
        }
        #endregion

        #region 是否登陆
        /// <summary>
        /// 是否登陆
        /// </summary>
        public static void IsLogin()
        {
            string strCurrentUserId = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");

            if (string.IsNullOrEmpty(strCurrentUserId))
            {
                HttpContext.Current.Response.Write("<script language=\"JavaScript\" type=\"text/javascript\">");
                //HttpContext.Current.Response.Write("window.parent.location.href='http://www.instrument.com.cn/vip/login.asp?strURL=" + Com.ILog.Utils.Utils.UrlDecode("http://www.instrument.com.cn/ilog/") + "';");
                HttpContext.Current.Response.Write("window.parent.location.href='/usertest.aspx'");
                HttpContext.Current.Response.Write("</script>");
                HttpContext.Current.Response.End();
            }
        }
        #endregion

        #region 站短生成规则

        /// <summary>
        /// 站短生成.by lx on 20120601
        /// </summary>
        /// <param name="url">分享的原始地址</param>
        /// <returns> http://www.instrument.com.cn/MD5加密串</returns>
        public static string ShortUrl(string url)
        {

            string result = string.Empty;

            try
            {

                //可以自定义生成MD5加密字符传前的混合KEY   
                string key = "instrumentcomcnUrl";
                //要使用生成URL的字符   
                string[] chars = new string[]
            {   

                "a","b","c","d","e","f","g","h",   
                "i","j","k","l","m","n","o","p",   
                "q","r","s","t","u","v","w","x",   
                "y","z","0","1","2","3","4","5",   
                "6","7","8","9","A","B","C","D",   
                "E","F","G","H","I","J","K","L",   
                "M","N","O","P","Q","R","S","T",   
                "U","V","W","X","Y","Z","o","k" 
 
            };

                //对传入网址进行MD5加密   
                string hex = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key + url, "md5");

                string[] resUrl = new string[4];

                for (int i = 0; i < 4; i++)
                {
                    //把加密字符按照8位一组16进制与0x3FFFFFFF进行位与运算   
                    int hexint = 0x3FFFFFFF & Convert.ToInt32("0x" + hex.Substring(i * 8, 8), 16);
                    string outChars = string.Empty;
                    for (int j = 0; j < 7; j++)
                    {
                        //把得到的值与0x0000003D进行位与运算，取得字符数组chars索引   
                        int index = 0x0000003D & hexint;
                        //把取得的字符相加   
                        outChars += chars[index];
                        //每次循环按位右移5位   
                        hexint = hexint >> 5;
                    }
                    //把字符串存入对应索引的输出数组   
                    resUrl[i] = outChars;
                }

                ////生成随机数
                //Random r = new Random();
                //int count = r.Next(3);

                result = string.Format("http://4077.cn/{0}", resUrl[2]);


            }
            catch (Exception ex)
            {

                result = "http://4077.cn/error";

            }
            return result;

        }

        #endregion


        #region 获取用户ip

        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.UserHostAddress;

            if (string.IsNullOrEmpty(result) || !IsIP(result))
                return "127.0.0.1";

            return result;
        }
        #endregion

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        #region 得到ilog的显示时间

        /// <summary>
        /// 功能描述：得到ilog的显示时间
        /// 创建标识：ljd 20120613
        /// </summary>
        /// <param name="dtTime"></param>
        /// <returns></returns>
        public static string GetIlogTime(DateTime dtTime)
        {
            //ilog的显示时间
            string strLogTime = "";

            DateTime dtNow = DateTime.Now;

            TimeSpan tsTime = dtNow - dtTime;

            if (tsTime.TotalSeconds < 60)
            {
                strLogTime = string.Format("{0}秒前", (int)Math.Floor(tsTime.TotalSeconds));
            }
            else if (tsTime.TotalMinutes < 60)
            {
                strLogTime = string.Format("{0}分钟前", (int)Math.Floor(tsTime.TotalMinutes));
            }
            else if (dtTime.ToString("yyyyMMdd") == dtNow.ToString("yyyyMMdd"))
            {
                strLogTime = string.Format("今天 {0}", dtTime.ToString("HH:mm"));
            }
            else if (dtTime.Year == dtNow.Year)
            {
                strLogTime = dtTime.ToString("MM月dd日 HH:mm");
            }
            else
            {
                strLogTime = dtTime.ToString("yyyy-MM-dd HH:mm");
            }

            return strLogTime;

        }

        #endregion

        #region 配合json编码
        /// <summary>
        /// 配合json编码
        /// </summary>
        /// <param name="value">处理内容</param>
        /// <returns></returns>
        public static string GetJScriptGlobalObjectEscape(string value)
        {
            return Microsoft.JScript.GlobalObject.escape(value);
        }
        #endregion

        #region 配合json解码
        /// <summary>
        /// 配合json解码
        /// </summary>
        /// <param name="value">处理内容</param>
        /// <returns></returns>
        public static string GetJScriptGlobalObjectUnEscape(string value)
        {
            return Microsoft.JScript.GlobalObject.unescape(value);
        }
        #endregion

        #region 返回字符串真实长度, 1个汉字长度为2
        /// <summary>
        /// 返回字符串真实长度, 1个汉字长度为2
        /// </summary>
        /// <returns>字符长度</returns>
        public static int GetStringLength(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }
        #endregion


        #region 得到随机函数所需的种子
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int GetRandomSeed()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        #endregion

    }
}
