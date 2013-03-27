using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using Com.ILog.Utils;
using Com.ILog.Data;

namespace ILog.DAL
{
    public class ILogOriginal
    {
        #region 查看某个原创是否存在（True：存在，False：不存在）
        /// <summary>
        /// 查看某个原创是否存在（True：存在，False：不存在）
        /// <param name="io_id">流水号</param>
        /// </summary>
        public static bool OriginalExists(int io_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@io_id", SqlDbType.BigInt)};
            parameters[0].Value = io_id;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("SP_ilog_original_OriginalExists", "isEXISTS", parameters) == "1" ? true : false;
        }
        #endregion

        #region 增加一条原创数据
        /// <summary>
        /// 增加一条原创数据
        /// <param name="model">原创表实体</param>
        /// </summary>
        public static long OriginalAdd(ILog.Model.ILogOriginal model, ref int urlState)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt,8),
					new SqlParameter("@io_content", SqlDbType.NVarChar,3000),
					new SqlParameter("@io_ip", SqlDbType.VarChar,50),
					new SqlParameter("@io_haspic", SqlDbType.Bit,1),
					new SqlParameter("@is_id", SqlDbType.BigInt,8),
					new SqlParameter("@cw_type", SqlDbType.Int,4),
                    new SqlParameter("@intime", SqlDbType.DateTime)};
            parameters[0].Value = model.userid;
            parameters[1].Value = model.io_content;
            parameters[2].Value = model.io_ip;
            parameters[3].Value = model.io_haspic;
            parameters[4].Value = model.is_id;
            parameters[5].Value = model.cw_type;
            parameters[6].Value = model.intime;

            long originalID = 0;

            //存储过程
            string sql = "SP_ilog_original_OriginalAdd";

            SqlConnection con = Com.ILog.Data.DbHelperSQL.GetConnection();

            try
            {
                originalID = Convert.ToInt32(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, parameters), typeof(int)));
                urlState = 1;
            }
            catch
            {
                urlState = 0;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return originalID;

        }
        #endregion

        #region 更新原创
        /// <summary>
        /// 更新原创
        /// <param name="model">原创表实体</param>
        /// </summary>
        public static int OriginalUpdate(ILog.Model.ILogOriginal model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@io_id", SqlDbType.BigInt,8),
					new SqlParameter("@userid", SqlDbType.BigInt,8),
					new SqlParameter("@io_content", SqlDbType.NVarChar,300),
					new SqlParameter("@io_ip", SqlDbType.VarChar,50),
					new SqlParameter("@io_haspic", SqlDbType.Bit,1),
					new SqlParameter("@is_id", SqlDbType.BigInt,8),
					new SqlParameter("@cw_type", SqlDbType.Int,4)};
            parameters[0].Value = model.io_id;
            parameters[1].Value = model.userid;
            parameters[2].Value = model.io_content;
            parameters[3].Value = model.io_ip;
            parameters[4].Value = model.io_haspic;
            parameters[5].Value = model.is_id;
            parameters[6].Value = model.cw_type;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_original_OriginalUpdate", parameters);
        }
        #endregion

        #region 删除一条原创
        /// <summary>
        /// 删除一条原创
        /// <param name="io_id">流水号</param>
        /// </summary>
        public static int OriginalDel(int io_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@io_id", SqlDbType.BigInt)};
            parameters[0].Value = io_id;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_original_OriginalDel", parameters);
        }
        #endregion

        #region 获取用户最新发送博文
        /// <summary>
        /// 获取一条最大的信息
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns>返回一条新的消息</returns>
        public static DataTable GetMaxInfo_Userid(long userid)
        {
            SqlParameter[] parameters ={
                                        new SqlParameter("@userid",SqlDbType.BigInt)
                                      };

            parameters[0].Value = userid;

            return Com.ILog.Data.DataAggregate.GetDateTabel("sp_GetMaxOriginal_UserID", parameters);
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="io_id">流水号</param>
        /// </summary>
        public static DataTable GetModel(int io_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@io_id", SqlDbType.BigInt)};
            parameters[0].Value = io_id;

            return Com.ILog.Data.DataAggregate.GetDateTabel("SP_ilog_original_GetOriginalInfo", parameters);
        }
        #endregion

        #region 得到原创详细信息
        /// <summary>
        /// 功能描述：得到原创详细信息
        /// 创建标识：ljd 20120610
        /// </summary>
        /// <param name="io_id">流水号</param>
        /// <param name="urlState">是否报错</param>
        /// <returns></returns>
        public static Model.ILogOriginal GetOriginalInfo(long io_id)
        {
            Model.ILogOriginal ooOriginal = new ILog.Model.ILogOriginal();

            SqlParameter[] parameters = {
					new SqlParameter("@io_id", SqlDbType.BigInt)};
            parameters[0].Value = io_id;


            SqlConnection con = DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_original_GetOriginalInfo", parameters);

            try
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        ooOriginal.io_id = Convert.ToInt64(Utils.ChangeType(reader["io_id"], typeof(long)));
                        ooOriginal.userid = Convert.ToInt64(Utils.ChangeType(reader["userid"], typeof(long)));
                        ooOriginal.io_content = Utils.ChangeType(reader["io_content"], typeof(string)).ToString();
                        ooOriginal.io_ip = Utils.ChangeType(reader["io_ip"], typeof(string)).ToString();
                        ooOriginal.io_haspic = Convert.ToBoolean(Utils.ChangeType(reader["io_haspic"], typeof(bool)));
                        ooOriginal.is_id = Convert.ToInt32(Utils.ChangeType(reader["is_id"], typeof(int)));
                        ooOriginal.cw_type = Convert.ToInt32(Utils.ChangeType(reader["cw_type"], typeof(int)));
                        ooOriginal.intime = Convert.ToDateTime(Utils.ChangeType(reader["intime"], typeof(DateTime)));
                        ooOriginal.io_spreadnum = Convert.ToInt32(Utils.ChangeType(reader["io_spreadnum"], typeof(int)));
                        ooOriginal.io_commentnum = Convert.ToInt32(Utils.ChangeType(reader["io_commentnum"], typeof(int)));
                    }
                }
                else
                {
                    ooOriginal = null;
                }
            }
            catch (Exception e)
            {
                ooOriginal = null;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return ooOriginal;

        }
        #endregion

        #region 判断用户输入的内容是否已存在,返回发表时间
        /// <summary>
        /// 功能描述：判断用户输入的内容是否已存在,返回发表时间
        /// 创建标识：ljd 20120612
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="content">博文内容</param>
        /// <param name="urlState">是否报错</param>
        /// <returns></returns>
        public static DateTime GetSendTime(long userid, string content, ref int urlState)
        {
            DateTime dtSendTime = DateTime.Now.AddDays(-1);

            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
                    new SqlParameter("@content", SqlDbType.NVarChar,300)};

            parameters[0].Value = userid;
            parameters[1].Value = content;

            SqlConnection con = DbHelperSQL.GetConnection();

            try
            {
                dtSendTime = Convert.ToDateTime(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, "SP_ilog_original_GetSendTime", parameters), typeof(DateTime)));
                urlState = 1;
            }
            catch (Exception e)
            {
                urlState = 0;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return dtSendTime;

        }
        #endregion

        #region 找文章智能提示（页面搜索）
        /// <summary>
        /// 找文章智能提示（页面搜索）
        /// </summary>
        /// <param name="Originaltitle">文章</param>
        /// <returns></returns>
        public static List<ILog.Model.ILogOriginal> GetSearchOriginalInfo(string Originaltitle)
        {
            List<ILog.Model.ILogOriginal> GetOriginalList = new List<ILog.Model.ILogOriginal>();

            ILog.Model.ILogOriginal OriginalList;

            SqlParameter[] parameters = {
					new SqlParameter("@Originaltitle", SqlDbType.NVarChar,20)};
            parameters[0].Value = Originaltitle;

            SqlConnection con = Com.ILog.Data.DbHelperSQL.GetConnection();
            SqlDataReader reader = Com.ILog.Data.DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_vip_ilog_GetSearchOriginalInfo", parameters);

            try
            {
                while (reader.Read())
                {
                    OriginalList = new ILog.Model.ILogOriginal();

                    OriginalList.io_content = reader["is_content"].ToString();

                    GetOriginalList.Add(OriginalList);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
                reader.Close();
                reader.Dispose();
            }

            return GetOriginalList;
        }
        #endregion


        #region 更新原创的转发次数
        /// <summary>
        /// 功能描述：更新原创的转发次数
        /// 创建标识：ljd 20120613
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="urlsate"></param>
        /// <returns></returns>
        public static int UpdateSpreadNum(long id)
        {
            //影响行数
            int resultCount = 0;

            //存储过程
            string sql = "SP_ilog_original_UpdateSpreadNum";

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlParameter[] Parm = { new SqlParameter("@io_id", SqlDbType.BigInt) };
            Parm[0].Value = id;

            try
            {
                resultCount = DbHelperSQL.ExecuteNonQuery(con, CommandType.StoredProcedure, sql, Parm);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return resultCount;

        }
        #endregion

        #region 得到热门评论的原创列表
        /// <summary>
        /// 功能描述：得到热门评论的原创列表
        /// 创建标识：ljd 20120626
        /// </summary>
        /// <returns></returns>
        public static List<Model.ILogOriginal> GetHotCommentOriginalList()
        {
            List<Model.ILogOriginal> originalList = new  List<ILog.Model.ILogOriginal>();

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_original_GetHotCommentList");

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model.ILogOriginal ooOriginal = new ILog.Model.ILogOriginal();

                        ooOriginal.io_id = Convert.ToInt64(Utils.ChangeType(reader["io_id"], typeof(long)));
                        ooOriginal.userid = Convert.ToInt64(Utils.ChangeType(reader["userid"], typeof(long)));
                        ooOriginal.io_content = Utils.ChangeType(reader["io_content"], typeof(string)).ToString();
                        ooOriginal.io_commentnum = Convert.ToInt32(Utils.ChangeType(reader["io_commentnum"], typeof(int)));

                        originalList.Add(ooOriginal);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return originalList;

        }
        #endregion

        #region 得到热门转发的原创列表
        /// <summary>
        /// 功能描述：得到热门转发的原创列表
        /// 创建标识：ljd 20120716
        /// </summary>
        /// <returns></returns>
        public static List<Model.ILogOriginal> GetHotSpreadOriginalList()
        {
            List<Model.ILogOriginal> originalList = new List<ILog.Model.ILogOriginal>();

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_original_GetHotSpreadList");

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model.ILogOriginal ooOriginal = new ILog.Model.ILogOriginal();

                        ooOriginal.io_id = Convert.ToInt64(Utils.ChangeType(reader["io_id"], typeof(long)));
                        ooOriginal.userid = Convert.ToInt64(Utils.ChangeType(reader["userid"], typeof(long)));
                        ooOriginal.io_content = Utils.ChangeType(reader["io_content"], typeof(string)).ToString();
                        ooOriginal.io_spreadnum = Convert.ToInt32(Utils.ChangeType(reader["io_spreadnum"], typeof(int)));

                        originalList.Add(ooOriginal);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return originalList;

        }
        #endregion

        #region 得到用户最新的原创信息
        /// <summary>
        /// 功能描述：得到用户最新的原创信息
        /// 创建标识：ljd 20120627
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static Model.ILogOriginal GetLastestOriginalInfo(long userid)
        {
            Model.ILogOriginal ooOriginal = new ILog.Model.ILogOriginal();

            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt)};
            parameters[0].Value = userid;


            SqlConnection con = DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_original_GetLastestOriginalInfo", parameters);

            try
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        ooOriginal.io_id = Convert.ToInt64(Utils.ChangeType(reader["io_id"], typeof(long)));
                        ooOriginal.io_content = Utils.ChangeType(reader["io_content"], typeof(string)).ToString();
                        ooOriginal.intime = Convert.ToDateTime(Utils.ChangeType(reader["intime"], typeof(DateTime)));
                    }
                }
                else
                {
                    ooOriginal = null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return ooOriginal;

        }
        #endregion

        #region 更新原创的评论次数
        /// <summary>
        /// 功能描述：更新原创的评论次数
        /// 创建标识：ljd 20120628
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateCommentNum(long id)
        {
            //影响行数
            int resultCount = 0;

            //存储过程
            string sql = "SP_ilog_original_UpdateCommentNum";

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlParameter[] Parm = { new SqlParameter("@io_id", SqlDbType.BigInt) };
            Parm[0].Value = id;

            try
            {
                resultCount = DbHelperSQL.ExecuteNonQuery(con, CommandType.StoredProcedure, sql, Parm);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return resultCount;

        }
        #endregion

        #region 得到正在发生的事（最新原创博文列表）
        /// <summary>
        /// 功能描述： 正在发生的事
        /// /// 创建标识：zhangl  20120712
        /// </summary>
        /// <param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="type">0 每日 1每周</param>
        /// <returns></returns>
        public static DataTable GetTopdaySpreadList(int PageCurrent, int PageSize, ref int RecordCount)
        {
            string strTableName = "ilog_original";
            string strFieldKey = "io_id";
            string strFieldShow = " io_id,userid,intime";
            strFieldShow += ", io_spreadnum";
            strFieldShow += ", io_commentnum";
            strFieldShow += " ,io_haspic";
            strFieldShow += ", io_content";
            strFieldShow += ",(select is_url from ilog_source where is_id = ilog_original.is_id ) as is_url";
            strFieldShow += ",(select is_name from ilog_source where is_id = ilog_original.is_id ) as is_name";
            strFieldShow += ",(select is_id from ilog_spread where is_spreadtype=0 AND is_type=0 AND is_isoriginal=1 AND io_id=ilog_original.io_id) AS iso_id";

            string strFieldOrder = " io_id desc ";

            string strWhere = "1=1";

            DataTable dblSearchList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            return dblSearchList;

        }
        #endregion


       
    }
}
