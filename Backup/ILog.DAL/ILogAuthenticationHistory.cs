using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Com.ILog.Data;
using Com.ILog.Utils;

namespace ILog.DAL
{
    public class ILogAuthenticationHistory
    {

        //public static List<Model.ILogAuthenticationHistory> GetAuthHistoryInfo(long userid,ref int urlState)
        //{
        //}


        #region 分页认证历史表数据


        /// <summary>
        /// 认证信息数据分页.by lx on 20120529
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
        public static List<Model.ILogAuthenticationHistory> GetAuthenticationHistoryPageList(string tableName, string fieldKey, int pageCurrent, int pageSize, string fieldShow, string fieldOrder, string where, ref int pageCount) 
        {


            //定义返回结果
            List<Model.ILogAuthenticationHistory> authenticationList = new List<ILog.Model.ILogAuthenticationHistory>();

            Model.ILogAuthenticationHistory authentication = null;

            try
            {

                DataTable dataTable = ILog.Common.Common.GetPageList(tableName, fieldKey, pageCurrent, pageSize, fieldShow, fieldOrder, where, ref pageCount);

                if (dataTable.Rows.Count>0)
                {

                    //迭代读取数据
                    foreach (DataRow dataRow in dataTable.Rows)
                    {

                        //实例化对象
                        authentication = new Model.ILogAuthenticationHistory();

                        authentication.ia_id = Convert.ToInt32(dataRow["ia_id"]);
                        authentication.userid = Convert.ToInt32(dataRow["Userid"]);
                        authentication.ia_IDNumber = Convert.ToString(dataRow["ia_IDNumber"]);
                        authentication.ia_Comment = Convert.ToString(dataRow["ia_Comment"]);
                        authentication.ia_Type = Convert.ToInt32(dataRow["ia_Type"]);
                        authentication.ia_State = Convert.ToInt32(dataRow["ia_State"]);
                        authentication.ia_adminname = Convert.ToString(dataRow["ia_adminname"]);
                        //authentication.ia_checktime = Convert.ToDateTime(dataRow["ia_checktime"]);
                        //authentication.ia_reason = Convert.ToString(dataRow["ia_reason"]);
                        //authentication.intime = Convert.ToDateTime(dataRow["intime"]);
                        authenticationList.Add(authentication);

                    }



                }

                return authenticationList;


            }
            catch (Exception ex)
            {

                return null ;
            }            

        }



        #endregion

        #region 根据编号获取认证单条记录


        /// <summary>
        /// 根据用户编号获取单条认证信息.by lx on 20120530
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public static ILog.Model.ILogAuthenticationHistory GetAuthenticationHistoryInfoByUserId(long userId, int type) 
        {

            //定义结果
            ILog.Model.ILogAuthenticationHistory authenticationInfo = null;

            try
            {

                SqlParameter[] parameters = 
                {
                    
					new SqlParameter("@userid", SqlDbType.BigInt),
                    new SqlParameter("@ia_Type", SqlDbType.Int)

				};

                parameters[0].Value = userId;
                parameters[1].Value = type;

                DataTable dataTable = Com.ILog.Data.DataAggregate.GetDateTabel("sp_ilog_AuthenticationHistory_GetAuthenticationInfoByUserid", parameters);


                if (dataTable.Rows.Count > 0)
                {

                    //实例化对象
                    authenticationInfo = new ILog.Model.ILogAuthenticationHistory();

                    if (dataTable.Rows[0]["ia_id"].ToString() != "")
                    {
                        authenticationInfo.ia_id = int.Parse(dataTable.Rows[0]["ia_id"].ToString());
                    }
                    if (dataTable.Rows[0]["userid"].ToString() != "")
                    {
                        authenticationInfo.userid = int.Parse(dataTable.Rows[0]["userid"].ToString());
                    }
                    authenticationInfo.ia_IDNumber = dataTable.Rows[0]["ia_IDNumber"].ToString();
                    authenticationInfo.ia_Comment = dataTable.Rows[0]["ia_Comment"].ToString();
                    if (dataTable.Rows[0]["ia_Type"].ToString() != "")
                    {
                        authenticationInfo.ia_Type = int.Parse(dataTable.Rows[0]["ia_Type"].ToString());
                    }
                    if (dataTable.Rows[0]["ia_State"].ToString() != "")
                    {
                        authenticationInfo.ia_State = int.Parse(dataTable.Rows[0]["ia_State"].ToString());
                    }
                    authenticationInfo.ia_adminname = dataTable.Rows[0]["ia_adminname"].ToString();
                    if (dataTable.Rows[0]["ia_checktime"].ToString() != "")
                    {
                        authenticationInfo.ia_checktime = DateTime.Parse(dataTable.Rows[0]["ia_checktime"].ToString());
                    }
                    authenticationInfo.ia_reason = dataTable.Rows[0]["ia_reason"].ToString();
                    if (dataTable.Rows[0]["intime"].ToString() != "")
                    {
                        authenticationInfo.intime = DateTime.Parse(dataTable.Rows[0]["intime"].ToString());
                    }

                    return authenticationInfo;

                }


            }
            catch (Exception ex)
            {

                return null;

            }

            return null;


        }

        #endregion

        #region 认证信息审核

        /// <summary>
        /// 审核认证信息.by lx on 20120530
        /// </summary>
        /// <param name="model">实体信息</param>
        /// <returns></returns>
        public static bool UpdateAuthenticationInfoByUserId(Model.ILogAuthenticationHistory model) 
        {
               
            try
            {

                SqlParameter[] parameters = 
                {
					
                    new SqlParameter("@aid", SqlDbType.Int),
					new SqlParameter("@userid", SqlDbType.BigInt,8),					
					new SqlParameter("@ia_State", SqlDbType.Int,4),
					new SqlParameter("@ia_adminname", SqlDbType.NVarChar,30),
					new SqlParameter("@ia_checktime", SqlDbType.DateTime),
					new SqlParameter("@ia_reason", SqlDbType.NVarChar,200)

				};
                parameters[0].Value = model.ia_id;
                parameters[1].Value = model.userid;
                parameters[2].Value = model.ia_State;
                parameters[3].Value = model.ia_adminname;
                parameters[4].Value = model.ia_checktime;
                parameters[5].Value = model.ia_reason;

                SqlConnection conn = Com.ILog.Data.DbHelperSQL.GetConnection();

                int result = Com.ILog.Data.DbHelperSQL.ExecuteNonQuery(conn, CommandType.StoredProcedure, "sp_ilog_AuthenticationHistory_UpdateAuthenticationInfoByUserId", parameters);
              
                if (result > 0)
                {

                    return true;

                }
                else
                {

                    return false;

                }
               

            }
            catch (Exception ex)
            {

                return false;
                
            }           


        }


        #endregion

        #region 增加一条认证申请数据
        /// <summary>
        /// 功能描述：增加一条认证申请数据
        /// 创建标识：ljd 20120604
        /// <param name="model">认证申请实体</param>
        /// </summary>
        public static int AuthenticationHistoryAdd(ILog.Model.ILogAuthenticationHistory model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt,8),
					new SqlParameter("@ia_IDNumber", SqlDbType.NVarChar,20),
					new SqlParameter("@ia_Comment", SqlDbType.NVarChar,500),
					new SqlParameter("@ia_Type", SqlDbType.Int),
					new SqlParameter("@ia_State", SqlDbType.Int),
					new SqlParameter("@intime", SqlDbType.DateTime)};
            parameters[0].Value = model.userid;
            parameters[1].Value = model.ia_IDNumber;
            parameters[2].Value = model.ia_Comment;
            parameters[3].Value = model.ia_Type;
            parameters[4].Value = model.ia_State;
            parameters[5].Value = model.intime;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("sp_ilog_AuthenticationHistory_Add", parameters);
        }
        #endregion

        #region 判断用户是否有审核中的认证申请
        /// <summary>
        /// 功能描述：判断用户是否有审核中的认证申请
        /// 创建标识：ljd 20120604
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="urlState">是否报错</param>
        /// <returns></returns>
        public static int GetAuthenticationingCountByUserID(long userid, ref int urlState)
        {
            //待认证的个数
            int count = 0;

            //存储过程
            string sql = "sp_ilog_AuthenticationHistory_GetCountByUserID";

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlParameter[] Parm = { new SqlParameter("@userid", SqlDbType.BigInt)};

            Parm[0].Value = userid;

            try
            {
                count = Convert.ToInt32(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, Parm), typeof(int)));
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

            return count;

        }
        #endregion

        #region 根据用户id和认证类别获取认证单条记录

        /// <summary>
        /// 功能描述：根据用户id和认证类别获取认证单条记录
        /// 创建标识：ljd 20120627
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="type">认证类别</param>
        /// <returns></returns>
        public static ILog.Model.ILogAuthenticationHistory GetInfoByUserId(long userId, int type)
        {
            SqlParameter[] Parm = new SqlParameter[2];
            Parm[0] = new SqlParameter("@userid", SqlDbType.BigInt);
            Parm[1] = new SqlParameter("@ia_Type", SqlDbType.Int);

            Parm[0].Value = userId;
            Parm[1].Value = type;

            SqlConnection con = Com.ILog.Data.DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "sp_ilog_AuthenticationHistory_GetAuthenticationInfoByUserid", Parm);

            Model.ILogAuthenticationHistory ooHistory = new ILog.Model.ILogAuthenticationHistory();

            try
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        ooHistory.ia_id = Convert.ToInt64(Utils.ChangeType(reader["ia_id"], typeof(long)));
                        ooHistory.userid = Convert.ToInt64(Utils.ChangeType(reader["userid"], typeof(long)));
                        ooHistory.ia_IDNumber = Utils.ChangeType(reader["ia_IDNumber"], typeof(string)).ToString();
                        ooHistory.ia_Comment = Utils.ChangeType(reader["ia_Comment"], typeof(string)).ToString();
                        ooHistory.ia_Type = Convert.ToInt32(Utils.ChangeType(reader["ia_Type"], typeof(int)));
                        ooHistory.ia_State = Convert.ToInt32(Utils.ChangeType(reader["ia_State"], typeof(int)));
                        ooHistory.ia_adminname = Utils.ChangeType(reader["ia_adminname"], typeof(string)).ToString();
                        ooHistory.ia_checktime = Convert.ToDateTime(Utils.ChangeType(reader["ia_checktime"], typeof(DateTime)));
                        ooHistory.ia_reason = Utils.ChangeType(reader["ia_reason"], typeof(string)).ToString();
                        ooHistory.intime = Convert.ToDateTime(Utils.ChangeType(reader["intime"], typeof(DateTime)));
                    }
                }
                else
                {
                    ooHistory = null;
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
            return ooHistory;

        }

        #endregion

    }

}
