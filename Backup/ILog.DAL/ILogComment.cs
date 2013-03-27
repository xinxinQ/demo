using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using Com.ILog.Data;
using Com.ILog.Utils;

namespace ILog.DAL
{
    public class ILogComment
    {
        #region 查看某条评论信息是否存在（true：存在，false：不存在）
        /// <summary>
        /// 查看某条评论信息是否存在
        /// </summary>
        public static bool ILogCommentExists(int ic_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ic_id", SqlDbType.BigInt)};
            parameters[0].Value = ic_id;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("SP_ilog_comment_commentEXISTS", "isEXISTS", parameters) == "1" ? true : false;
        }
        #endregion

        #region
        /// <summary>
        /// 增加一条数据
        /// <param name="model">评论表实体</param>
        /// </summary>
        public static long CommentAdd(ILog.Model.ILogComment model)
        {
            //评论id
            long commentid = 0;

            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt,8),
					new SqlParameter("@ic_content", SqlDbType.NVarChar,3000),
					new SqlParameter("@intime", SqlDbType.DateTime),
					new SqlParameter("@ic_currentid", SqlDbType.BigInt,8),
					new SqlParameter("@ic_currentuserid", SqlDbType.BigInt,8),
					new SqlParameter("@is_id", SqlDbType.BigInt,8),
                    new SqlParameter("@ic_type", SqlDbType.Int),
                    new SqlParameter("@ic_state", SqlDbType.Int),
                    new SqlParameter("@ic_commentid", SqlDbType.BigInt,8),
					new SqlParameter("@ic_commentcontent", SqlDbType.NVarChar,3000)
           };
            parameters[0].Value = model.userid;
            parameters[1].Value = model.ic_content;
            parameters[2].Value = model.intime;
            parameters[3].Value = model.ic_currentid;
            parameters[4].Value = model.ic_currentuserid;
            parameters[5].Value = model.is_id;
            parameters[6].Value = model.ic_type;
            parameters[7].Value = model.ic_state;
            parameters[8].Value = model.ic_commentid;
            parameters[9].Value = model.ic_commentcontent;

            SqlConnection con = DbHelperSQL.GetConnection();

            try
            {
                commentid = Convert.ToInt64(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, "SP_ilog_comment_commentAdd", parameters), typeof(long)));
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
            return commentid;

        }
        #endregion


        #region 删除一条评论数据
        /// <summary>
        /// 删除一条评论数据
        /// <param name="ic_id">流水号</param>
        /// <param name="userid">用户id</param>
        /// </summary>
        public static int CommentDel(long ic_id,long userid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ic_id", SqlDbType.BigInt),
                    new SqlParameter("@userid", SqlDbType.BigInt)};

            parameters[0].Value = ic_id;
            parameters[1].Value = userid;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_comment_commentDel", parameters);
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="ic_id">流水号</param>
        /// </summary>
        public static DataTable GetModel(int ic_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ic_id", SqlDbType.BigInt)};
            parameters[0].Value = ic_id;

            return Com.ILog.Data.DataAggregate.GetDateTabel("SP_ilog_comment_GetCommentInfo", parameters);
        }
        #endregion

        #region 评论搜索智能提示
        /// <summary>
        /// 评论搜索智能提示
        /// </summary>
        /// <param name="commentinfo">搜索关键字</param>
        /// <param name="ation">操作类型查看类型（1：收到的评论，0：发出的评论 ）</param>
        /// <param name="userid">当前用户id</param>
        /// <returns></returns>
        public static List<ILog.Model.ILogComment> GetSearchCommentInfo(string commentinfo, int ation, long userid)
        {
            List<ILog.Model.ILogComment> GetILogCommentList = new List<ILog.Model.ILogComment>();

            ILog.Model.ILogComment ILogCommentList;

            SqlParameter[] parameters = {
					new SqlParameter("@commentinfo", SqlDbType.VarChar,40),
                    new SqlParameter("@ation",SqlDbType.Int),
                    new SqlParameter("@userid",SqlDbType.BigInt)};
            parameters[0].Value = commentinfo;
            parameters[1].Value = ation;
            parameters[2].Value = userid;

            SqlConnection con = Com.ILog.Data.DbHelperSQL.GetConnection();
            SqlDataReader reader = Com.ILog.Data.DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_comment_GetSearchCommentInfo", parameters);

            try
            {
                while (reader.Read())
                {
                    ILogCommentList = new ILog.Model.ILogComment();

                    ILogCommentList.nickname = reader["nickname"].ToString();

                    GetILogCommentList.Add(ILogCommentList);
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

            return GetILogCommentList;
        }
        #endregion

        //#region 删除@到我的回复数据
        ///// <summary>
        ///// 删除一条评论数据
        ///// <param name="ic_id">流水号</param>
        ///// <param name="userid">用户id</param>
        ///// </summary>
        //public static int AtReplyDel(long ic_id, long userid)
        //{
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ic_id", SqlDbType.BigInt),
        //            new SqlParameter("@userid", SqlDbType.BigInt)};

        //    parameters[0].Value = ic_id;
        //    parameters[1].Value = userid;

        //    return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_comment_AtReplyDel", parameters);
        //}

        //#endregion

        #region 得到评论实体对象
        /// <summary>
        /// 功能描述：得到评论实体对象
        /// 创建标识：ljd 20120716
        /// <param name="ic_id">流水号</param>
        /// </summary>
        public static Model.ILogComment GetCommentInfoById(long ic_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ic_id", SqlDbType.BigInt)};
            parameters[0].Value = ic_id;

            SqlConnection con = Com.ILog.Data.DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_comment_GetCommentInfo", parameters);

            Model.ILogComment ooComment = new ILog.Model.ILogComment();

            try
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        ooComment.ic_id = Convert.ToInt64(Utils.ChangeType(reader["ic_id"], typeof(long)));
                        ooComment.userid = Convert.ToInt64(Utils.ChangeType(reader["userid"], typeof(long)));
                        ooComment.ic_content = Utils.ChangeType(reader["ic_content"], typeof(string)).ToString();
                        ooComment.intime = Convert.ToDateTime(Utils.ChangeType(reader["intime"], typeof(DateTime)));
                        ooComment.ic_currentid = Convert.ToInt64(Utils.ChangeType(reader["ic_currentid"], typeof(long)));
                        ooComment.ic_currentuserid = Convert.ToInt64(Utils.ChangeType(reader["ic_currentuserid"], typeof(long)));
                        ooComment.is_id = Convert.ToInt64(Utils.ChangeType(reader["is_id"], typeof(long)));
                        ooComment.ic_type = Convert.ToInt32(Utils.ChangeType(reader["ic_type"], typeof(int)));
                        ooComment.ic_commentid = Convert.ToInt64(Utils.ChangeType(reader["ic_commentid"], typeof(long)));
                        ooComment.ic_commentcontent = Utils.ChangeType(reader["ic_commentcontent"], typeof(string)).ToString();
                    }
                }
                else
                {
                    ooComment = null;
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
            return ooComment;

        }
        #endregion

    }
}
