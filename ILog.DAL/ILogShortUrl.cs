using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using Com.ILog.Utils;
using Com.ILog.Data;

namespace ILog.DAL
{
    public class ILogShortUrl
    {
        #region 查看某个短地址是否存在（True：存在，False：不存在）
        /// <summary>
        /// 查看某个短地址是否存在（True：存在，False：不存在）
        /// </summary>
        public static bool ShortUrlExists(int isu_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@isu_id", SqlDbType.BigInt)};
            parameters[0].Value = isu_id;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("SP_ilog_shorturl_ShortUrlExists", "isEXISTS", parameters) == "1" ? true : false;
        }
        #endregion

        #region 添加短地址
        /// <summary>
        /// 添加短地址
        /// <param name="model">短地址实体</param>
        /// </summary>
        public static long ShortUrlAdd(ILog.Model.ILogShortUrl model)
        {
            SqlConnection con = DbHelperSQL.GetConnection();

            SqlParameter[] parameters = {
					new SqlParameter("@isu_url", SqlDbType.VarChar,300),
					new SqlParameter("@isu_shorturl", SqlDbType.VarChar,50),
					new SqlParameter("@isu_num", SqlDbType.BigInt,8),
					new SqlParameter("@isu_type", SqlDbType.Int)};
            parameters[0].Value = model.isu_url;
            parameters[1].Value = model.isu_shorturl;
            parameters[2].Value = model.isu_num;
            parameters[3].Value = model.isu_type;

            long isu_id = Convert.ToInt64(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, "SP_ilog_shorturl_ShortUrlAdd", parameters), typeof(long)));

            return isu_id;

        }
        #endregion

        #region 更新短地址访问次数
        /// <summary>
        /// 更新短地址
        /// <param name="model">短地址实体</param>
        /// </summary>
        public static int ShortUrlUpdate(ILog.Model.ILogShortUrl model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@isu_id", SqlDbType.BigInt,8),
					new SqlParameter("@isu_num", SqlDbType.BigInt,8)};
            parameters[0].Value = model.isu_id;
            parameters[1].Value = model.isu_num;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_shorturl_ShortUrlUpdate", parameters);
        }
        #endregion


        #region 更新短地址类型
        /// <summary>
        /// 功能描述：更新短地址类型
        /// 创建标识：ljd 20120701
        /// <param name="isu_id">流水号</param>
        /// <param name="isu_type">短地址类型 1视频 0普通地址</param>
        /// </summary>
        public static int ShortUrlUpdateType(long isu_id,int isu_type)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@isu_id", SqlDbType.BigInt,8),
					new SqlParameter("@isu_type", SqlDbType.Int)};

            parameters[0].Value = isu_id;
            parameters[1].Value = isu_type;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_shorturl_UpdateType", parameters);

        }
        #endregion

        #region 删除短地址
        /// <summary>
        /// 删除短地址
        /// </summary>
        public static int ShortUrlDel(int isu_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@isu_id", SqlDbType.BigInt)};
            parameters[0].Value = isu_id;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_shorturl_ShortUrlDel", parameters);
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static DataTable GetModel(int isu_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@isu_id", SqlDbType.BigInt)};
            parameters[0].Value = isu_id;

            return Com.ILog.Data.DataAggregate.GetDateTabel("SP_ilog_shorturl_GetShortUrlInfo", parameters);
        }
        #endregion

        #region 根据长地址得到短地址实体对象

        /// <summary>
        /// 功能描述：根据长地址得到短地址实体对象
        /// 创建标识：ljd 20120618
        /// </summary>
        /// <param name="url">长地址</param>
        /// <returns>短地址实体对象</returns>
        public static Model.ILogShortUrl GetShorUrlInfoByUrl(string url)
        {
            SqlConnection con = DbHelperSQL.GetConnection();

            SqlParameter[] Parm = { new SqlParameter("@url", SqlDbType.VarChar, 300) };
            Parm[0].Value = url;

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_shorturl_GetEntityByUrl", Parm);

            Model.ILogShortUrl ooShortUrl = new ILog.Model.ILogShortUrl();

            try
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        ooShortUrl.isu_shorturl = Utils.ChangeType(reader["isu_shorturl"], typeof(string)).ToString();
                        ooShortUrl.isu_url = Utils.ChangeType(reader["isu_url"], typeof(string)).ToString();
                        ooShortUrl.isu_id = Convert.ToInt64(Utils.ChangeType(reader["isu_id"], typeof(long)));
                        ooShortUrl.isu_type = Convert.ToInt32(Utils.ChangeType(reader["isu_type"], typeof(int)));
                        ooShortUrl.isu_num = Convert.ToInt32(Utils.ChangeType(reader["isu_num"], typeof(int)));
                    }
                }
                else
                {
                    ooShortUrl = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
                reader.Close();
                reader.Dispose();
            }

            return ooShortUrl;

        }

        #endregion

        #region 根据短地址得到长地址

        /// <summary>
        /// 功能描述：根据短地址得到长地址
        /// 创建标识：ljd 20120614
        /// </summary>
        /// <param name="shorturl">短地址</param>
        /// <returns>短地址实体对象</returns>
        public static Model.ILogShortUrl GetShortUrlInfoByShortUrl(string shorturl)
        {

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlParameter[] parameters = {
					new SqlParameter("@shorturl", SqlDbType.VarChar,50)};
            parameters[0].Value = shorturl;

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_shorturl_GetEntityByShortUrl", parameters);

            Model.ILogShortUrl ooShortUrl = new Model.ILogShortUrl();

            try
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        ooShortUrl.isu_shorturl = Utils.ChangeType(reader["isu_shorturl"], typeof(string)).ToString();
                        ooShortUrl.isu_url = Utils.ChangeType(reader["isu_url"], typeof(string)).ToString();
                        ooShortUrl.isu_id = Convert.ToInt64(Utils.ChangeType(reader["isu_id"], typeof(long)));
                        ooShortUrl.isu_type = Convert.ToInt32(Utils.ChangeType(reader["isu_type"], typeof(int)));
                        ooShortUrl.isu_num = Convert.ToInt32(Utils.ChangeType(reader["isu_num"], typeof(int)));
                    }
                }
                else
                {
                    ooShortUrl = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
                reader.Close();
                reader.Dispose();
            }
            return ooShortUrl;

        }

        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 功能描述：得到一个对象实体
        /// 创建标识：ljd 20120618
        /// </summary>
        public static Model.ILogShortUrl GetShortUrlInfo(long isu_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@isu_id", SqlDbType.BigInt)};
            parameters[0].Value = isu_id;

            SqlConnection con = Com.ILog.Data.DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_shorturl_GetShortUrlInfo", parameters);

            Model.ILogShortUrl ooShortUrl = new Model.ILogShortUrl();

            try
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        ooShortUrl.isu_num = Convert.ToInt32(Utils.ChangeType(reader["isu_num"], typeof(int)));
                        ooShortUrl.isu_shorturl = Utils.ChangeType(reader["isu_shorturl"], typeof(string)).ToString();
                        ooShortUrl.isu_url = Utils.ChangeType(reader["isu_url"], typeof(string)).ToString();
                        ooShortUrl.isu_id = Convert.ToInt64(Utils.ChangeType(reader["isu_id"], typeof(long)));
                        ooShortUrl.intime = Convert.ToDateTime(Utils.ChangeType(reader["intime"], typeof(DateTime)));
                        ooShortUrl.isu_type = Convert.ToInt32(Utils.ChangeType(reader["isu_type"], typeof(int)));
                    }
                }
                else
                {
                    ooShortUrl = null;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
                reader.Close();
                reader.Dispose();
            }
            return ooShortUrl;

        }
        #endregion

    }
}
