using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using Com.ILog.Data;
using Com.ILog.Utils;

namespace ILog.DAL
{
    public class VipILog
    {

        #region 查看某条记录是否存在（true：存在，false：不存在）
        /// <summary>
        /// 是否存在该记录
        /// <param name="nickname">用户昵称</param>
        /// </summary>
        public static bool VipILogExists(string nickname)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@nickname", SqlDbType.NVarChar,20)};
            parameters[0].Value = nickname;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("SP_vip_ilog_VipILogExists", "isEXISTS", parameters) == "1" ? true : false;
        }
        #endregion

        #region 增加一条用户数据
        /// <summary>
        /// 增加一条用户数据
        /// </summary>
        public static int VipILogAdd(ILog.Model.VipILog model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt,8),
					new SqlParameter("@username", SqlDbType.NVarChar,20),
					new SqlParameter("@nickname", SqlDbType.NVarChar,20),
					new SqlParameter("@face", SqlDbType.NVarChar,255),
					new SqlParameter("@vi_memberlevel", SqlDbType.Int,4),
					new SqlParameter("@vi_state", SqlDbType.Int,4)};
            parameters[0].Value = model.userid;
            parameters[1].Value = model.username;
            parameters[2].Value = model.nickname;
            parameters[3].Value = model.face;
            parameters[4].Value = model.vi_memberlevel;
            parameters[5].Value = model.vi_state;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_vip_ilog_VipILogAdd", parameters);
        }
        #endregion

        #region 更新一条用户数据
        /// <summary>
        /// 更新一条用户数据
        /// <param name="model">用户信息表实体</param>
        /// </summary>
        public static int VipILogUpdate(ILog.Model.VipILog model)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@vi_id", SqlDbType.BigInt,8),
					new SqlParameter("@userid", SqlDbType.BigInt,8),
					new SqlParameter("@username", SqlDbType.NVarChar,20),
					new SqlParameter("@nickname", SqlDbType.NVarChar,20),
					new SqlParameter("@face", SqlDbType.NVarChar,255),
					new SqlParameter("@vi_memberlevel", SqlDbType.Int,4),
					new SqlParameter("@vi_state", SqlDbType.Int,4)};
            parameters[0].Value = model.vi_id;
            parameters[1].Value = model.userid;
            parameters[2].Value = model.username;
            parameters[3].Value = model.nickname;
            parameters[4].Value = model.face;
            parameters[5].Value = model.vi_memberlevel;
            parameters[6].Value = model.vi_state;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_vip_ilog_VipILogUpdate", parameters);
        }
        #endregion

        #region 删除一条数据
        /// <summary>
        /// 删除一条数据
        /// <param name="userid">用户id</param>
        /// <param name="vi_id">流水号</param>
        /// </summary>
        public static int VipILogDel(int userid, int vi_id)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
					new SqlParameter("@vi_id", SqlDbType.BigInt)};
            parameters[0].Value = userid;
            parameters[1].Value = vi_id;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_vip_ilog_VipILogDel", parameters);
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static DataTable GetModel(long userid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt)};
            parameters[0].Value = userid;

            return Com.ILog.Data.DataAggregate.GetDateTabel("SP_vip_ilog_GetVipILogInfo", parameters);
        }

        #endregion

        #region  根据用户编号获取用户信息

        /// <summary>
        /// 根据用户编号获取用户信息.by lx on 20120522 
        /// </summary>
        /// <param name="userId">用户表编号</param>
        /// <returns></returns>
        public static ILog.Model.VipILog GetVipIlogInfoById(long userId)
        {

            ILog.Model.VipILog userInfo = null;

            try
            {

                SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt)
					};
                parameters[0].Value = userId;


                DataTable dataTable = Com.ILog.Data.DataAggregate.GetDateTabel("SP_vip_ilog_GetVipIlogInfoById", parameters);


                if (dataTable.Rows.Count > 0)
                {

                    //实例化对象
                    userInfo = new ILog.Model.VipILog();

                    if (dataTable.Rows[0]["vi_id"].ToString() != "")
                    {

                        userInfo.vi_id = int.Parse(dataTable.Rows[0]["vi_id"].ToString());

                    }
                    if (dataTable.Rows[0]["userid"].ToString() != "")
                    {

                        userInfo.userid = int.Parse(dataTable.Rows[0]["userid"].ToString());

                    }

                    userInfo.username = dataTable.Rows[0]["username"].ToString();

                    userInfo.vi_memberlevel = Convert.ToInt32(dataTable.Rows[0]["vi_memberlevel"]);

                    userInfo.nickname = dataTable.Rows[0]["nickname"].ToString();

                    userInfo.face = dataTable.Rows[0]["face"].ToString();

                    //开通状态
                    userInfo.vi_state = string.IsNullOrEmpty(dataTable.Rows[0]["vi_state"].ToString()) == true ? 0 : Convert.ToInt32(dataTable.Rows[0]["vi_state"]);


                    return userInfo;

                }


            }
            catch (Exception ex)
            {

                return null;

            }

            return null;

        }

        #endregion

        #region  根据用户编号获取用户昵称、头像

        /// <summary>
        /// 根据用户编号获取用户昵称、头像.by lx on 20120522 
        /// </summary>
        /// <param name="userId">用户表编号</param>
        /// <returns></returns>
        public static ILog.Model.VipILog GetVipUserInfoByUserId(long userId)
        {

            ILog.Model.VipILog userInfo = null;

            try
            {

                SqlParameter[] parameters = 
                {

					new SqlParameter("@userid", SqlDbType.BigInt)
					
                };
                parameters[0].Value = userId;               

                //执行读取数据
                DataTable dataTable = Com.ILog.Data.DataAggregate.GetDateTabel("SP_vip_ilog_GetVipIlogInfoById", parameters);
                if (dataTable.Rows.Count > 0)
                {

                    //实例化对象
                    userInfo = new ILog.Model.VipILog();

                    if (dataTable.Rows[0]["vi_id"].ToString() != "")
                    {

                        userInfo.vi_id = int.Parse(dataTable.Rows[0]["vi_id"].ToString());

                    }
                    if (dataTable.Rows[0]["userid"].ToString() != "")
                    {

                        userInfo.userid = int.Parse(dataTable.Rows[0]["userid"].ToString());

                    }

                    userInfo.username = dataTable.Rows[0]["username"].ToString();

                    userInfo.nickname = dataTable.Rows[0]["nickname"].ToString();

                    userInfo.face = dataTable.Rows[0]["face"].ToString();

                    if (dataTable.Rows[0]["vi_memberlevel"].ToString() != "")
                    {

                        userInfo.vi_memberlevel = int.Parse(dataTable.Rows[0]["vi_memberlevel"].ToString());

                    }
                    if (dataTable.Rows[0]["intime"].ToString() != "")
                    {

                        userInfo.intime = DateTime.Parse(dataTable.Rows[0]["intime"].ToString());

                    }
                    if (dataTable.Rows[0]["vi_state"].ToString() != "")
                    {

                        userInfo.vi_state = int.Parse(dataTable.Rows[0]["vi_state"].ToString());

                    }



                    return userInfo;

                }


            }
            catch (Exception ex)
            {

                return null;

            }

            return null;


        }

        #endregion

        #region 根据用户名获取用户勋章

        /// <summary>
        /// 根据用户名获取用户勋章.by lx on 20120523 
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static string GetVipUserInsigniaByUserName(string userName)
        {

            //存储过程
            string sql = "VIP_UserInsignia";

            StringBuilder userInsignia = new StringBuilder();

            SqlConnection conn = Com.ILog.Data.DbHelperSQL.GetIMConnection();

            try
            {

                SqlParameter[] parameters = 
                {

					    new SqlParameter("@username", SqlDbType.VarChar,30)

			    };

                parameters[0].Value = userName;

                SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, conn, sql, parameters);

                while (reader.Read())
                {

                    userInsignia.Append(reader["Insignia_image"]);

                }

                reader.Close();

            }
            catch (Exception ex)
            {

                userInsignia.Append("暂无");

            }
            finally
            {

                conn.Close();
                conn.Dispose();

            }

            return userInsignia.ToString();

        }

        #endregion

        #region 根据用户编号获取前10位粉丝的信息（@时用到）

        /// <summary>
        /// 根据用户编号获取前10位粉丝的信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public static List<ILog.Model.VipILog> GetfanInfoByUserId(long userId, string nickname)
        {

            List<ILog.Model.VipILog> fanList = new List<ILog.Model.VipILog>();

            ILog.Model.VipILog fan = null;

            try
            {

                SqlParameter[] parameters = 
                {

                    new SqlParameter("@userid", SqlDbType.BigInt),
                    new SqlParameter("@nickname", SqlDbType.NVarChar,20)
                   
                };

                parameters[0].Value = userId;
                parameters[1].Value = nickname;


                //执行读取数据
                DataTable dataTable = Com.ILog.Data.DataAggregate.GetDateTabel("SP_ilog_userfan_GetfanInfoByUserId", parameters);
                if (dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {

                        //实例化对象
                        fan = new ILog.Model.VipILog();

                        if (dataRow["userid"].ToString() != "")
                        {

                            fan.userid = int.Parse(dataRow["userid"].ToString());

                        }

                        fan.nickname = dataRow["nickname"].ToString();

                        fanList.Add(fan);

                    }

                }

                return fanList;


            }
            catch (Exception ex)
            {

                return null;

            }


        }


        #endregion

        #region 判断用户是否开通认证ilog

        /// <summary>
        /// 功能描述：判断用户是否开通认证ilog
        /// 创建标识：ljd 20120522
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>-1 未开通ilog 0 未手机认证 1已认证</returns>
        public static int CheckIlogOpen(long userid)
        {
            string sql = "SP_vip_ilog_CheckOpen";

            SqlParameter[] Parm = new SqlParameter[1];
            Parm[0] = new SqlParameter("@userid", SqlDbType.BigInt);

            Parm[0].Value = userid;

            SqlConnection con = DbHelperSQL.GetConnection();

            //ilog开通认证状态
            int state = -1;

            try
            {
                state = Convert.ToInt32(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, Parm), typeof(int)));
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

            return state;

        }

        #endregion

        #region 判断用户是否认证手机

        /// <summary>
        /// 功能描述：判断用户是否认证手机
        /// 创建标识：ljd 20120522
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>0 未手机认证 1已认证 2已发送验证码待验证</returns>
        public static int CheckMobilePass(long userid)
        {
            string sql = "VIP_GetMobilePassState";

            SqlParameter[] Parm = new SqlParameter[1];
            Parm[0] = new SqlParameter("@userid", SqlDbType.BigInt);

            Parm[0].Value = userid;

            SqlConnection con = DbHelperSQL.GetIMConnection();

            //手机认证状态
            int state = 0;

            try
            {
                state = Convert.ToInt32(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, Parm), typeof(int)));
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

            return state;

        }

        #endregion

        #region 更新用户头像
        /// <summary>
        /// 更新用户头像
        /// <param name="userid">用户id</param>
        /// <param name="face">用户头像</param>
        /// </summary>
        public static int VipILogUpdateFace(long userid, string face)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt,8),
					new SqlParameter("@face", SqlDbType.NVarChar,255)};

            parameters[0].Value = userid;
            parameters[1].Value = face;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_vip_ilog_UpdateFace", parameters);

        }
        #endregion

        #region 更新用户昵称和省份城市
        /// <summary>
        /// 功能描述：更新用户昵称和省份城市
        /// 创建标识：ljd 20120605
        /// <param name="userid">用户id</param>
        /// <param name="nickname">昵称</param>
        /// <param name="prid">省份id</param>
        /// <param name="cityid">城市id</param>
        /// </summary>
        public static int UpdateNickName(long userid, string nickname,int prid,int cityid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
					new SqlParameter("@nickname", SqlDbType.NVarChar,20),
                    new SqlParameter("@pr_id", SqlDbType.Int),
                    new SqlParameter("@ci_id", SqlDbType.Int)};

            parameters[0].Value = userid;
            parameters[1].Value = nickname;
            parameters[2].Value = prid;
            parameters[3].Value = cityid;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_vip_ilog_UpdateNickName", parameters);

        }
        #endregion

        #region 手机认证通过后更新vip
        /// <summary>
        /// 功能描述：手机认证通过后更新vip
        /// 创建标识：ljd 20120605
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="vistate">手机认证状态 0未通过 1通过 2禁止</param>
        /// <param name="urlstate">json是否成功</param>
        /// <returns></returns>
        public static int UpdateVipIlogMobileState(long userid, int vistate, ref int urlstate)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
					new SqlParameter("@vistate", SqlDbType.Int)};

            parameters[0].Value = userid;
            parameters[1].Value = vistate;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("SP_vip_ilog_UpdateViState", parameters);

        }
        #endregion

        #region 根据userid获取username
        /// <summary>
        /// 功能描述：根据userid获取username
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static string GetUserNameByUserId(long userid)
        {
            string sql = "SP_Vip_ILog_GetUserNameByUserId";

            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt)};
            parameters[0].Value = userid;

            SqlConnection con = DbHelperSQL.GetConnection();

            string username = "";

            try
            {
                username = Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, parameters), typeof(string)).ToString();
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
            return username;

        }
        #endregion

        #region 根据username获取userid
        /// <summary>
        /// 功能描述：根据username获取userid
        /// 创建标识：ljd 20120609
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public static long GetUserIDByUserName(string username)
        {
            string sql = "SP_Vip_ILog_GetUserIDByUserName";

            SqlParameter[] parameters = {
					new SqlParameter("@username", SqlDbType.NVarChar,30)};
            parameters[0].Value = username;

            SqlConnection con = DbHelperSQL.GetConnection();

            long userid = 0;

            try
            {
                userid = Convert.ToInt64(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, parameters), typeof(long)));
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
            return userid;

        }
        #endregion

        #region 根据用户ID得到ilog实体
        /// <summary>
        /// 功能描述：根据用户ID得到ilog实体
        /// 创建标识：ljd 20120611
        /// </summary>
        public static Model.VipILog GetModelByUserID(long userid)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt)};

            parameters[0].Value = userid;

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_vip_ilog_GetVipILogInfoByUserid", parameters);

            Model.VipILog ooILog = new Model.VipILog();

            try
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        ooILog.ci_id = Convert.ToInt32(Utils.ChangeType(reader["ci_id"], typeof(int)));
                        ooILog.pr_id = Convert.ToInt32(Utils.ChangeType(reader["pr_id"], typeof(int)));
                        ooILog.face = Utils.ChangeType(reader["face"], typeof(string)).ToString();
                        ooILog.userid = Convert.ToInt64(Utils.ChangeType(reader["userid"], typeof(long)));
                        ooILog.username = Utils.ChangeType(reader["username"], typeof(string)).ToString();
                        ooILog.nickname = Utils.ChangeType(reader["nickname"], typeof(string)).ToString();
                        ooILog.vi_memberlevel = Convert.ToInt32(Utils.ChangeType(reader["vi_memberlevel"], typeof(int)));
                        ooILog.intime = Convert.ToDateTime(Utils.ChangeType(reader["intime"], typeof(DateTime)));
                        ooILog.vi_state = Convert.ToInt32(Utils.ChangeType(reader["vi_state"], typeof(int)));
                    }
                }
                else
                {
                    ooILog = null;
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
            return ooILog;

        }

        #endregion

        #region 根据userid获取用户昵称
        /// <summary>
        /// 功能描述：根据userid获取用户昵称
        /// 创建标识：ljd 20120612
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static string GetNickNameByUserId(long userid)
        {
            //昵称
            string nickname = "";

            string sql = "SP_Vip_Ilog_GetNickNameByUserID";

            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt)};

            parameters[0].Value = userid;

            SqlConnection con = DbHelperSQL.GetConnection();

            try
            {
                nickname = Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, parameters), typeof(string)).ToString();
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
            return nickname;

        }
        #endregion

        #region 智能搜索得到前10个该用户的粉丝和关注的昵称
        /// <summary>
        /// 功能描述：智能搜索得到前10个该用户的粉丝和关注的昵称
        /// 创建标识：ljd 20120617
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="nickname">昵称</param>
        /// <returns></returns>
        public static List<Model.VipILog> GetAtUserList(long userid,string nickname)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
                    new SqlParameter("@nickname", SqlDbType.NVarChar,20)};

            parameters[0].Value = userid;
            parameters[1].Value = nickname;

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_vip_ilog_GetAtUserList", parameters);

            List<Model.VipILog> ilogList = new List<ILog.Model.VipILog>();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model.VipILog ooILog = new Model.VipILog();

                        ooILog.nickname = Utils.ChangeType(reader["nickname"], typeof(string)).ToString();

                        ilogList.Add(ooILog);
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
            return ilogList;

        }

        #endregion

        #region 根据用户昵称获取userid
        /// <summary>
        /// 功能描述：根据用户昵称获取userid
        /// 创建标识：ljd 20120618
        /// </summary>
        /// <param name="nickname">用户昵称</param>
        /// <returns></returns>
        public static long GetUserIDByNickName(string nickname)
        {
            string sql = "SP_Vip_Ilog_GetUserIDByNickName";

            SqlParameter[] parameters = {
					new SqlParameter("@nickname", SqlDbType.NVarChar,20)};
            parameters[0].Value = nickname;

            SqlConnection con = DbHelperSQL.GetConnection();

            long userid = 0;

            try
            {
                userid = Convert.ToInt64(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, parameters), typeof(long)));
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
            return userid;

        }
        #endregion

        #region 查看用户是否在线（true：在线，false不在线）
        /// <summary>
        /// 查看用户是否在线（true：在线，false不在线）
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public static bool GetUserIsOnline(string username) 
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters = {
					new SqlParameter("@username", SqlDbType.NVarChar,20)};
            parameters[0].Value = username;

            return Com.ILog.Data.DataAggregate.GetColumnInfo("SP_VipOnline_GetUserIsOnline", "isEXISTS", parameters) == "1" ? true : false;
        }
        #endregion

        #region 找人智能提示（页面搜索）
        /// <summary>
        /// 找人智能提示（页面搜索）
        /// </summary>
        /// <param name="nickname">用户昵称</param>
        /// <returns></returns>
        public static List<ILog.Model.VipILog> GetvipilogByNickName(string nickname)
        {
            List<ILog.Model.VipILog> GetNickNameList = new List<ILog.Model.VipILog>();

            ILog.Model.VipILog VipILogList;

            SqlParameter[] parameters = {
					new SqlParameter("@nickname", SqlDbType.NVarChar,20)};
            parameters[0].Value = nickname;

            SqlConnection con = Com.ILog.Data.DbHelperSQL.GetConnection();
            SqlDataReader reader = Com.ILog.Data.DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_vip_ilog_GetvipilogByNickName", parameters);

            try
            {
                while (reader.Read())
                {
                    VipILogList = new ILog.Model.VipILog();

                    VipILogList.nickname = reader["nickname"].ToString();

                    GetNickNameList.Add(VipILogList);
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

            return GetNickNameList;
        }
        #endregion

        #region 得到名人微博列表（昨日发微博最多的经过名人认证的前10名用户）
        /// <summary>
        /// 功能描述：得到名人微博列表（昨日发微博最多的经过名人认证的前10名用户）
        /// 创建标识：ljd 20120705
        /// </summary>
        /// <returns></returns>
        public static List<Model.VipILog> GetFamousUserList()
        {
            List<Model.VipILog> famousList = new List<ILog.Model.VipILog>();

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_vip_ilog_GetMostilogFamousList");

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model.VipILog ooILog = new Model.VipILog();
                        ooILog.face = Utils.ChangeType(reader["face"], typeof(string)).ToString();
                        ooILog.userid = Convert.ToInt64(Utils.ChangeType(reader["userid"], typeof(long)));
                        ooILog.nickname = Utils.ChangeType(reader["nickname"], typeof(string)).ToString();

                        famousList.Add(ooILog);
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
            return famousList;

        }
        #endregion

        #region 得到草根微博列表（昨日发微博最多的非名人认证的前10名用户）
        /// <summary>
        /// 功能描述：得到草根微博列表（昨日发微博最多的非名人认证的前10名用户）
        /// 创建标识：ljd 20120705
        /// </summary>
        /// <returns></returns>
        public static List<Model.VipILog> GetCommonUserList()
        {
            List<Model.VipILog> commonList = new List<ILog.Model.VipILog>();

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_vip_ilog_GetMostilogCommonList");

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model.VipILog ooILog = new Model.VipILog();
                        ooILog.face = Utils.ChangeType(reader["face"], typeof(string)).ToString();
                        ooILog.userid = Convert.ToInt64(Utils.ChangeType(reader["userid"], typeof(long)));
                        ooILog.nickname = Utils.ChangeType(reader["nickname"], typeof(string)).ToString();

                        commonList.Add(ooILog);
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
            return commonList;

        }
        #endregion

        #region 得到最新开通微博的用户列表
        /// <summary>
        /// 功能描述：得到最新开通微博的用户列表
        /// 创建标识：ljd 20120705
        /// </summary>
        /// <returns></returns>
        public static List<Model.VipILog> GetNewUserList()
        {
            List<Model.VipILog> userlist = new List<ILog.Model.VipILog>();

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_vip_ilog_GetNewUserList");

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model.VipILog ooILog = new Model.VipILog();
                        ooILog.face = Utils.ChangeType(reader["face"], typeof(string)).ToString();
                        ooILog.userid = Convert.ToInt64(Utils.ChangeType(reader["userid"], typeof(long)));
                        ooILog.nickname = Utils.ChangeType(reader["nickname"], typeof(string)).ToString();

                        userlist.Add(ooILog);
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
            return userlist;

        }
        #endregion

        #region 得到最新经过名人认证的用户列表
        /// <summary>
        /// 功能描述：得到最新经过名人认证的用户列表
        /// 创建标识：ljd 20120705
        /// </summary>
        /// <returns></returns>
        public static List<Model.VipILog> GetNewFamousUserList()
        {
            List<Model.VipILog> userlist = new List<ILog.Model.VipILog>();

            SqlConnection con = DbHelperSQL.GetConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "SP_vip_ilog_GetNewFamousUserList");

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model.VipILog ooILog = new Model.VipILog();
                        ooILog.face = Utils.ChangeType(reader["face"], typeof(string)).ToString();
                        ooILog.userid = Convert.ToInt64(Utils.ChangeType(reader["userid"], typeof(long)));
                        ooILog.nickname = Utils.ChangeType(reader["nickname"], typeof(string)).ToString();

                        userlist.Add(ooILog);
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
            return userlist;

        }
        #endregion

   
    }
}
