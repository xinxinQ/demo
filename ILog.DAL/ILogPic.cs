using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using Com.ILog.Data;
using Com.ILog.Utils;

namespace ILog.DAL
{
    public class ILogPic
    {
        #region 查看某张缩略图是否存在（True：存在，False：不存在）
        /// <summary>
        /// 查看某张缩略图是否存在（True：存在，False：不存在）
        /// </summary>
        public static bool PicExists(int ip_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ip_id", SqlDbType.BigInt)};
            parameters[0].Value = ip_id;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("SP_ilog_pic_PicExists", "isEXISTS", parameters) == "1" ? true : false;
        }
        #endregion

        #region 增加一条缩略图数据
        /// <summary>
        /// 增加一条缩略图数据
        /// <param name="model">缩略图实体</param>
        /// </summary>
        public static int PicAdd(ILog.Model.ILogPic model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@io_id", SqlDbType.BigInt,8),
					new SqlParameter("@ip_name", SqlDbType.VarChar,255),
					new SqlParameter("@ip_type", SqlDbType.Int,4),
                    new SqlParameter("@ip_mark", SqlDbType.VarChar,50)};
            parameters[0].Value = model.io_id;
            parameters[1].Value = model.ip_name;
            parameters[2].Value = model.ip_type;
            parameters[3].Value = model.ip_mark;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_pic_PicAdd", parameters);

        }
        #endregion

        #region 更新缩略图数据
        /// <summary>
        /// 更新缩略图数据
        /// <param name="model">缩略图实体</param>
        /// </summary>
        public static int PicUpdate(ILog.Model.ILogPic model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ip_id", SqlDbType.BigInt,8),
					new SqlParameter("@io_id", SqlDbType.BigInt,8),
					new SqlParameter("@ip_name", SqlDbType.VarChar,255),
					new SqlParameter("@ip_type", SqlDbType.Int,4)};
            parameters[0].Value = model.ip_id;
            parameters[1].Value = model.io_id;
            parameters[2].Value = model.ip_name;
            parameters[3].Value = model.ip_type;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_pic_PicUpdate", parameters);
        }
        #endregion

        #region 删除缩略图数据
        /// <summary>
        /// 删除缩略图数据
        /// <param name="ip_id">流水号</param>
        /// </summary>
        public static int PicDel(int ip_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ip_id", SqlDbType.BigInt)};
            parameters[0].Value = ip_id;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_ilog_pic_PicDel", parameters);
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="ip_id">流水号</param>
        /// </summary>
        public static DataTable GetModel(int ip_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ip_id", SqlDbType.BigInt)};
            parameters[0].Value = ip_id;

            return Com.ILog.Data.DataAggregate.GetDateTabel("SP_ilog_pic_GetPicInfo", parameters);
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
            SqlParameter[] parameters = {
					new SqlParameter("@ip_mark", SqlDbType.VarChar,50),
					new SqlParameter("@originalID", SqlDbType.BigInt)};
            parameters[0].Value = mark;
            parameters[1].Value = originalID;

            SqlConnection con = DbHelperSQL.GetConnection();

            //执行行数
            int resultCount = 0;

            try
            {
                resultCount = DbHelperSQL.ExecuteNonQuery(con, CommandType.StoredProcedure, "SP_ilog_pic_UpdateMark", parameters);
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
            }
            return resultCount;

        }
        #endregion

        #region 读取原创图片
        /// <summary>
        /// 读取原创图片
        /// </summary>
        /// <param name="Io_Id">博文id</param>
        /// <returns></returns>
        public static DataTable GetPicByIoId(long Io_Id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@IoId", SqlDbType.BigInt)};
            parameters[0].Value = Io_Id;

            return Com.ILog.Data.DataAggregate.GetDateTabel("SP_ilog_pic_GetPicByIoId", parameters);
        }
        #endregion

        #region 根据原创id得到图片列表
        /// <summary>
        /// 功能描述：根据原创id得到图片列表
        /// 创建标识：ljd 20120625
        /// </summary>
        /// <param name="io_id">原创id</param>
        /// <returns></returns>
        public static List<Model.ILogPic> GetPicList(long io_id)
        {
            SqlParameter[] Parm = new SqlParameter[1];
            Parm[0] = new SqlParameter("@io_id", SqlDbType.BigInt);

            Parm[0].Value = io_id;

            SqlConnection con = Com.ILog.Data.DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_ilog_pic_GetPicList", Parm);

            List<Model.ILogPic> picList = new List<ILog.Model.ILogPic>();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model.ILogPic ooPic = new ILog.Model.ILogPic();

                        ooPic.intime = Convert.ToDateTime(Utils.ChangeType(reader["intime"], typeof(DateTime)));
                        ooPic.io_id = Convert.ToInt64(Utils.ChangeType(reader["io_id"], typeof(long)));
                        ooPic.ip_id = Convert.ToInt64(Utils.ChangeType(reader["ip_id"], typeof(long)));
                        ooPic.ip_name = Utils.ChangeType(reader["ip_name"], typeof(string)).ToString();
                        ooPic.ip_type = Convert.ToInt32(Utils.ChangeType(reader["ip_type"], typeof(int)));

                        picList.Add(ooPic);
                    }
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
            return picList;

        }
        #endregion
    }
}
