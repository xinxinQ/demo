using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Data;

using Com.ILog.Data;
using Com.ILog.Utils;


namespace ILog.DAL
{
    public class ILogSchool
    {

        #region 根据用户id得到教育信息列表
        /// <summary>
        /// 功能描述：根据用户id得到教育信息列表
        /// 创建标识：ljd 20120529
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="urlState"></param>
        /// <returns></returns>
        public static List<Model.ILogSchool> GetSchoolList(long userid, ref int urlState)
        {
            SqlParameter[] Parm = new SqlParameter[1];
            Parm[0] = new SqlParameter("@userid", SqlDbType.BigInt);

            Parm[0].Value = userid;

            SqlConnection con = Com.ILog.Data.DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "sp_ilog_schoolGetListByUserID", Parm);

            List<Model.ILogSchool> schoolList = new List<ILog.Model.ILogSchool>();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model.ILogSchool ooSchool = new ILog.Model.ILogSchool();

                        ooSchool.is_id = Convert.ToInt32(Utils.ChangeType(reader["is_id"], typeof(int)));
                        ooSchool.is_school = Utils.ChangeType(reader["is_school"], typeof(string)).ToString();
                        ooSchool.is_entranceYear = Convert.ToInt32(Utils.ChangeType(reader["is_entranceYear"], typeof(int)));
                        ooSchool.is_degree = Convert.ToInt32(Utils.ChangeType(reader["is_degree"], typeof(int)));
                        ooSchool.is_degreeName = Utils.ChangeType(reader["is_degreeName"], typeof(string)).ToString();
                        ooSchool.is_schoolid = Convert.ToInt32(Utils.ChangeType(reader["is_schoolid"], typeof(int)));

                        schoolList.Add(ooSchool);
                    }
                }
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
                reader.Close();
                reader.Dispose();
            }
            return schoolList;

        }
        #endregion

        #region 根据id得到教育详细信息
        /// <summary>
        /// 功能描述：根据id得到教育详细信息
        /// 创建标识：ljd 20120602
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="urlState"></param>
        /// <returns></returns>
        public static Model.ILogSchool GetSchoolInfo(long is_id, ref int urlState)
        {
            SqlParameter[] Parm = new SqlParameter[1];
            Parm[0] = new SqlParameter("@id", SqlDbType.BigInt);

            Parm[0].Value = is_id;

            SqlConnection con = Com.ILog.Data.DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "sp_ilog_schoolGetInfo", Parm);

            Model.ILogSchool ooSchool = new ILog.Model.ILogSchool();

            try
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        ooSchool.is_id = Convert.ToInt32(Utils.ChangeType(reader["is_id"], typeof(int)));
                        ooSchool.is_school = Utils.ChangeType(reader["is_school"], typeof(string)).ToString();
                        ooSchool.is_entranceYear = Convert.ToInt32(Utils.ChangeType(reader["is_entranceYear"], typeof(int)));
                        ooSchool.is_degree = Convert.ToInt32(Utils.ChangeType(reader["is_degree"], typeof(int)));
                        ooSchool.is_degreeName = Utils.ChangeType(reader["is_degreeName"], typeof(string)).ToString();
                        ooSchool.is_schoolid = Convert.ToInt32(Utils.ChangeType(reader["is_schoolid"], typeof(int)));
                    }
                }
                else
                {
                    ooSchool = null;
                }
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
                reader.Close();
                reader.Dispose();
            }
            return ooSchool;

        }
        #endregion

        #region 更新学校信息
        /// <summary>
        /// 功能描述：更新学校信息
        /// 创建标识：ljd 20120602
        /// </summary>
        /// <param name="ooSchool">学校实体对象</param>
        /// <param name="urlstate">是否报错</param>
        /// <returns></returns>
        public static int UpdateSchool(Model.ILogSchool ooSchool, ref int urlstate)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@is_id", SqlDbType.BigInt),
					new SqlParameter("@is_school", SqlDbType.NVarChar,100),
                    new SqlParameter("@is_degree", SqlDbType.Int),   
                    new SqlParameter("@is_entranceYear", SqlDbType.Int),
                    new SqlParameter("@is_schoolid", SqlDbType.Int)};

            parameters[0].Value = ooSchool.is_id;
            parameters[1].Value = ooSchool.is_school;
            parameters[2].Value = ooSchool.is_degree;
            parameters[3].Value = ooSchool.is_entranceYear;
            parameters[4].Value = ooSchool.is_schoolid;

            SqlConnection con = DbHelperSQL.GetConnection();

            //执行行数
            int resultCount = 0;

            try
            {
                resultCount = DbHelperSQL.ExecuteNonQuery(con, CommandType.StoredProcedure, "sp_ilog_schoolUpdate", parameters);
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

        #region 添加学校信息
        /// <summary>
        /// 功能描述：添加学校信息
        /// 创建标识：ljd 20120602
        /// </summary>
        /// <param name="ooSchool">学校实体对象</param>
        /// <param name="urlstate">是否报错</param>
        /// <returns></returns>
        public static int AddSchool(Model.ILogSchool ooSchool, ref int urlstate)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
					new SqlParameter("@is_school", SqlDbType.NVarChar,100),
                    new SqlParameter("@is_degree", SqlDbType.Int),   
                    new SqlParameter("@is_entranceYear", SqlDbType.Int),
                    new SqlParameter("@intime",SqlDbType.DateTime),
                    new SqlParameter("@is_schoolid", SqlDbType.Int)};


            parameters[0].Value = ooSchool.userid;
            parameters[1].Value = ooSchool.is_school;
            parameters[2].Value = ooSchool.is_degree;
            parameters[3].Value = ooSchool.is_entranceYear;
            parameters[4].Value = ooSchool.intime;
            parameters[5].Value = ooSchool.is_schoolid;

            SqlConnection con = DbHelperSQL.GetConnection();

            //执行行数
            int resultCount = 0;

            try
            {
                resultCount = DbHelperSQL.ExecuteNonQuery(con, CommandType.StoredProcedure, "sp_ilog_schoolAdd", parameters);
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

        #region 删除学校信息
        /// <summary>
        /// 功能描述：删除学校信息
        /// 创建标识：ljd 20120602
        /// </summary>
        /// <param name="is_id">学校id</param>
        /// <param name="urlstate">是否报错</param>
        /// <returns></returns>
        public static int DeleteSchool(long is_id, ref int urlstate)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@is_id", SqlDbType.BigInt)};

            parameters[0].Value = is_id;


            SqlConnection con = DbHelperSQL.GetConnection();

            //执行行数
            int resultCount = 0;

            try
            {
                resultCount = DbHelperSQL.ExecuteNonQuery(con, CommandType.StoredProcedure, "sp_ilog_schoolDelete", parameters);
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

        #region 根据省份id得到学校列表
        /// <summary>
        /// 功能描述：根据省份id得到学校列表
        /// 创建标识：ljd 20120625
        /// </summary>
        /// <param name="provid"></param>
        /// <returns></returns>
        public static Dictionary<int, string> GetSchoolListByProvID(int provid)
        {
            SqlParameter[] Parm = new SqlParameter[1];
            Parm[0] = new SqlParameter("@pi_id", SqlDbType.BigInt);

            Parm[0].Value = provid;

            SqlConnection con = Com.ILog.Data.DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "sp_ilog_colledgeGetListByProvID", Parm);

            Dictionary<int, string> colledgeList = new Dictionary<int, string>();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        colledgeList.Add(Convert.ToInt32(Utils.ChangeType(reader["ic_id"], typeof(int))), Utils.ChangeType(reader["ic_schoolname"], typeof(string)).ToString());
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
            return colledgeList;


        }
        #endregion

    }

}
