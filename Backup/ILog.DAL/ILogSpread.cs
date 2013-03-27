using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using Com.ILog.Data;
using Com.ILog.Utils;

namespace ILog.DAL
{
    public class ILogSpread
    {
        #region 查看某个博文是否存在（True：存在，False：不存在）
        /// <summary>
        /// 查看某个博文是否存在（True：存在，False：不存在）
        /// </summary>
        public static bool SpreadExists(int is_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@is_id", SqlDbType.BigInt)};
            parameters[0].Value = is_id;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("SP_ilog_spread_SpreadExists", "isEXISTS", parameters) == "1" ? true : false;
        }
        #endregion

        #region  增加一条数据博文
        /// <summary>
        /// 增加一条数据博文
        /// <param name="model">博文表实体</param>
        /// </summary>
        public static int SpreadAdd(ILog.Model.ILogSpread model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@iso_id", SqlDbType.BigInt,8),
					new SqlParameter("@is_type", SqlDbType.Int,4),
					new SqlParameter("@is_content", SqlDbType.NVarChar,3000),
					new SqlParameter("@is_fanuserid", SqlDbType.BigInt,8),
					new SqlParameter("@userid", SqlDbType.BigInt,8),
					new SqlParameter("@iss_id", SqlDbType.Int,4),
                    new SqlParameter("@io_id", SqlDbType.BigInt,8),
                    new SqlParameter("@is_spreadtype", SqlDbType.Int,4),
                    new SqlParameter("@is_isoriginal", SqlDbType.Int,4),
                    new SqlParameter("@intime", SqlDbType.DateTime)};

            parameters[0].Value = model.iso_id;
            parameters[1].Value = model.is_type;
            parameters[2].Value = model.is_content;
            parameters[3].Value = model.is_fanuserid;
            parameters[4].Value = model.userid;
            parameters[5].Value = model.iss_id;
            parameters[6].Value = model.io_id;
            parameters[7].Value = model.is_spreadtype;
            parameters[8].Value = model.is_isoriginal;
            parameters[9].Value = model.intime;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_spread_SpreadAdd", parameters);

        }
        #endregion

        #region 删除一条博文
        /// <summary>
        /// 删除一条博文
        /// </summary>
        public static int SpreadDel(long is_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@is_id", SqlDbType.BigInt)};
            parameters[0].Value = is_id;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_spread_SpreadDel", parameters);
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="is_id">流水号</param>
        /// </summary>
        public static DataTable GetModel(int is_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@is_id", SqlDbType.BigInt)};
            parameters[0].Value = is_id;

            return Com.ILog.Data.DataAggregate.GetDateTabel("SP_ilog_spread_GetSpreadInfo", parameters);
        }
        #endregion

        #region 根据博文id与博文类型得到博文内容
        /// <summary>
        /// 功能描述：根据博文id与博文类型得到博文内容
        /// 创建标识：ljd 20120612
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static string GetContent(long is_id,int is_type)
        {
            //博文内容
            string content = "";

            string sql = "SP_ilog_spread_GetContent";

            SqlParameter[] parameters = {
					new SqlParameter("@is_id", SqlDbType.BigInt),
                    new SqlParameter("@is_type", SqlDbType.Int)};

            parameters[0].Value = is_id;
            parameters[1].Value = is_type;

            SqlConnection con = DbHelperSQL.GetConnection();

            try
            {
                content = Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, parameters), typeof(string)).ToString();
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
            return content;

        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 功能描述：得到一个对象实体
        /// 创建标识：ljd 20120613
        /// <param name="is_id">流水号</param>
        /// <param name="urlstate">是否报错</param>
        /// </summary>
        public static ILog.Model.ILogSpread GetSpreadInfo(long is_id,ref int urlstate)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@is_id", SqlDbType.BigInt)};
            parameters[0].Value = is_id;

            ILog.Model.ILogSpread ooSpread = new ILog.Model.ILogSpread();

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_spread_GetSpreadInfo", parameters);

            try
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        ooSpread.is_id = Convert.ToInt64(Utils.ChangeType(reader["is_id"], typeof(long)));
                        ooSpread.iso_id = Convert.ToInt64(Utils.ChangeType(reader["iso_id"], typeof(long)));
                        ooSpread.is_type = Convert.ToInt32(Utils.ChangeType(reader["is_type"], typeof(int)));
                        ooSpread.is_content = Utils.ChangeType(reader["is_content"], typeof(string)).ToString();
                        ooSpread.is_fanuserid = Convert.ToInt64(Utils.ChangeType(reader["is_fanuserid"], typeof(bool)));
                        ooSpread.userid = Convert.ToInt64(Utils.ChangeType(reader["userid"], typeof(long)));
                        ooSpread.intime = Convert.ToDateTime(Utils.ChangeType(reader["intime"], typeof(DateTime)));
                        ooSpread.iss_id = Convert.ToInt32(Utils.ChangeType(reader["iss_id"], typeof(int)));
                        ooSpread.io_id = Convert.ToInt64(Utils.ChangeType(reader["io_id"], typeof(long)));
                        ooSpread.is_spreadnum = Convert.ToInt32(Utils.ChangeType(reader["is_spreadnum"], typeof(int)));
                        ooSpread.is_commentnum = Convert.ToInt32(Utils.ChangeType(reader["is_commentnum"], typeof(int)));
                        ooSpread.is_spreadtype = Convert.ToInt32(Utils.ChangeType(reader["is_spreadtype"], typeof(int)));
                        ooSpread.is_isoriginal = Convert.ToInt32(Utils.ChangeType(reader["is_isoriginal"], typeof(int)));
                    }
                }
                else
                {
                    ooSpread = null;
                }
                urlstate = 1;
            }
            catch (Exception e)
            {
                urlstate = 0;
            }
            finally
            {
                con.Close();
                con.Dispose();
                reader.Close();
                reader.Dispose();
            }

            return ooSpread;

        }
        #endregion

        #region 根据原创id和是否原创得到传播表信息
        /// <summary>
        /// 功能描述：根据原创id和是否原创得到传播表信息
        /// 创建标识：ljd 20120701
        /// <param name="io_id">原创id</param>
        /// </summary>
        public static ILog.Model.ILogSpread GetSpreadOriginalInfo(long io_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@io_id", SqlDbType.BigInt)};
            parameters[0].Value = io_id;

            ILog.Model.ILogSpread ooSpread = new ILog.Model.ILogSpread();

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_spread_GetOriginalInfo", parameters);

            try
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        ooSpread.is_id = Convert.ToInt64(Utils.ChangeType(reader["is_id"], typeof(long)));
                        ooSpread.userid = Convert.ToInt64(Utils.ChangeType(reader["userid"], typeof(long)));
                    }
                }
                else
                {
                    ooSpread = null;
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

            return ooSpread;

        }
        #endregion


        #region 更新转发的博文的转发次数
        /// <summary>
        /// 功能描述：更新转发的博文的转发次数
        /// 创建标识：ljd 20120614
        /// <param name="is_id">流水号</param>
        /// </summary>
        public static int UpdateSpreadNum(long is_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@is_id", SqlDbType.BigInt)};
            parameters[0].Value = is_id;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_spread_UpdateSpreadNum", parameters);

        }
        #endregion

        #region 更新转发的博文的评论次数
        /// <summary>
        /// 功能描述：更新转发的博文的评论次数
        /// 创建标识：ljd 20120628
        /// <param name="is_id">流水号</param>
        /// </summary>
        public static int UpdateCommentNum(long is_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@is_id", SqlDbType.BigInt)};
            parameters[0].Value = is_id;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_spread_UpdateCommentNum", parameters);

        }
        #endregion

        #region 更新原创向传播表中冗余的评论数
        /// <summary>
        /// 功能描述：更新原创向传播表中冗余的评论数
        /// 创建标识：ljd 20120628
        /// <param name="io_id">原创id</param>
        /// </summary>
        public static int UpdateOriginalCommentNum(long io_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@io_id", SqlDbType.BigInt)};
            parameters[0].Value = io_id;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_spread_UpdateOriginalCommentNum", parameters);

        }
        #endregion

        #region 更新原创向传播表中冗余的转发数
        /// <summary>
        /// 功能描述：更新原创向传播表中冗余的转发数
        /// 创建标识：ljd 20120628
        /// <param name="io_id">原创id</param>
        /// </summary>
        public static int UpdateOriginalSpreadNum(long io_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@io_id", SqlDbType.BigInt)};
            parameters[0].Value = io_id;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_spread_UpdateOriginalSpreadNum", parameters);

        }
        #endregion

        #region  新增转发并获取id
        /// <summary>
        /// 功能描述：新增转发并获取id
        /// 创建标识：ljd 20120701
        /// <param name="model">博文表实体</param>
        /// </summary>
        public static long SpreadAddAndGetID(ILog.Model.ILogSpread model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@iso_id", SqlDbType.BigInt,8),
					new SqlParameter("@is_type", SqlDbType.Int,4),
					new SqlParameter("@is_content", SqlDbType.NVarChar,3000),
					new SqlParameter("@is_fanuserid", SqlDbType.BigInt,8),
					new SqlParameter("@userid", SqlDbType.BigInt,8),
					new SqlParameter("@iss_id", SqlDbType.Int,4),
                    new SqlParameter("@io_id", SqlDbType.BigInt,8),
                    new SqlParameter("@is_spreadtype", SqlDbType.Int,4),
                    new SqlParameter("@is_isoriginal", SqlDbType.Int,4),
                    new SqlParameter("@intime", SqlDbType.DateTime)};

            parameters[0].Value = model.iso_id;
            parameters[1].Value = model.is_type;
            parameters[2].Value = model.is_content;
            parameters[3].Value = model.is_fanuserid;
            parameters[4].Value = model.userid;
            parameters[5].Value = model.iss_id;
            parameters[6].Value = model.io_id;
            parameters[7].Value = model.is_spreadtype;
            parameters[8].Value = model.is_isoriginal;
            parameters[9].Value = model.intime;

              SqlConnection con = DbHelperSQL.GetConnection();

              long spreadID = 0;

            try
            {
                spreadID = Convert.ToInt64(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con,CommandType.StoredProcedure,"SP_ilog_spread_SpreadAdd",parameters), typeof(long)));
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
            return spreadID;          

        }
        #endregion

        #region 得到最新的博文列表
        /// <summary>
        /// 功能描述：得到最新的博文列表
        /// 创建标识：ljd 20120705
        /// </summary>
        public static List<ILog.Model.ILogSpread> GetNewSpreadList()
        {

            List<ILog.Model.ILogSpread> spreadList = new List<ILog.Model.ILogSpread>();

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_spread_GetNewList");

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ILog.Model.ILogSpread ooSpread = new ILog.Model.ILogSpread();

                        ooSpread.is_content = Utils.ChangeType(reader["is_content"], typeof(string)).ToString();
                        ooSpread.userid = Convert.ToInt64(Utils.ChangeType(reader["userid"], typeof(long)));
                        ooSpread.intime = Convert.ToDateTime(Utils.ChangeType(reader["intime"], typeof(DateTime)));

                        spreadList.Add(ooSpread);
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
                reader.Close();
                reader.Dispose();
            }

            return spreadList;

        }
        #endregion

        #region 找文章智能提示（上面的用户）
        /// <summary>
        /// 找文章智能提示（页面搜索）
        /// </summary>
        /// <param name="Originaltitle">文章</param>
        /// <returns></returns>
        public static List<ILog.Model.VipILog> GetSearchPersonalInfo(string Originaltitle)
        {
            List<ILog.Model.VipILog> GetVipILogList = new List<ILog.Model.VipILog>();

            ILog.Model.VipILog VipILogList;

            SqlParameter[] parameters = {
					new SqlParameter("@Originaltitle", SqlDbType.NVarChar,20)};
            parameters[0].Value = Originaltitle;

            SqlConnection con = Com.ILog.Data.DbHelperSQL.GetConnection();
            SqlDataReader reader = Com.ILog.Data.DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_vip_ilog_GetSearchPersonalInfo", parameters);

            try
            {
                while (reader.Read())
                {
                    VipILogList = new ILog.Model.VipILog();

                    VipILogList.vi_id = Convert.ToInt64(reader["vi_id"]);
                    VipILogList.userid = Convert.ToInt64(reader["userid"]);
                    VipILogList.nickname = reader["nickname"].ToString();
                    VipILogList.vic_fannum = Convert.ToInt32(reader["vic_fannum"]);
                    VipILogList.face = reader["face"].ToString();

                    GetVipILogList.Add(VipILogList);
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

            return GetVipILogList;
        }
        #endregion

        #region 得到用户最新的博文信息
        /// <summary>
        /// 功能描述：得到用户最新的博文信息
        /// 创建标识：ljd 20120627
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static Model.ILogSpread GetLastestILogInfo(long userid)
        {
            Model.ILogSpread ooSpread = new ILog.Model.ILogSpread();

            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt)};
            parameters[0].Value = userid;


            SqlConnection con = DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_spread_GetLastestSpreadInfo", parameters);

            try
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        ooSpread.is_id = Convert.ToInt64(Utils.ChangeType(reader["is_id"], typeof(long)));
                        ooSpread.is_content = Utils.ChangeType(reader["is_content"], typeof(string)).ToString();
                        ooSpread.intime = Convert.ToDateTime(Utils.ChangeType(reader["intime"], typeof(DateTime)));
                    }
                }
                else
                {
                    ooSpread = null;
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

            return ooSpread;

        }
        #endregion

        #region 获取博文内页单条原创/转发信息.by lx on 20120716

        /// <summary>
        /// 获取博文内页单条原创/转发信息.by lx on 20120716
        /// </summary>
        /// <param name="id">博文编号</param>
        /// <returns></returns>
        public static DataTable GetContentInfoById(long id) 
        {


            SqlParameter[] parameters = 
            {
					
                new SqlParameter("@id", SqlDbType.BigInt)

            };
            parameters[0].Value = id;

           // SqlConnection con = DbHelperSQL.GetConnection();

            return Com.ILog.Data.DataAggregate.GetDateTabel("SP_ilog_spread_GetContentInfoById", parameters);
        

        }



        #endregion


    }
}
