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
    public class Vip
    {
        #region 根据userid判断vip是否存在

        /// <summary>
        /// 功能描述：根据userid判断vip是否存在
        /// 创建标识：ljd 20120525
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="urlState">成功状态</param>
        /// <returns>0 不存在 1存在</returns>
        public static int IsExistsVIP(long userid, ref int urlState)
        {
            int existState = 0;

            if (userid != 0)
            {
                //存储过程
                string sql = "VIP_IfExistByUserID";

                SqlConnection con = Com.ILog.Data.DbHelperSQL.GetIMConnection();

                SqlParameter[] Parm = { new SqlParameter("@userid", SqlDbType.BigInt) };
                Parm[0].Value = userid;

                try
                {
                    existState = Convert.ToInt32(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, Parm), typeof(int)));
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

            }

            return existState;

        }

        #endregion

        #region 根据userid得到用户头像和用户名

        /// <summary>
        /// 功能描述：根据userid得到用户头像和用户名
        /// 创建标识：ljd 20120525
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="urlState">成功状态</param>
        /// <returns>用户实体对象(用户头像和用户名)</returns>
        public static Model.Vip GetUserFace(long userid, ref int urlState)
        {
            Model.Vip ooVip = new ILog.Model.Vip();

            if (userid != 0)
            {
                //存储过程
                string sql = "VIP_UserFaceByUserID";

                SqlConnection con = DbHelperSQL.GetIMConnection();

                SqlParameter[] Parm = { new SqlParameter("@userid", SqlDbType.BigInt) };
                Parm[0].Value = userid;

                SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, sql, Parm);

                try
                {
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            ooVip.face = Utils.ChangeType(reader["face"], typeof(string)).ToString();
                            ooVip.username = Utils.ChangeType(reader["username"], typeof(string)).ToString();
                        }
                    }
                    else
                    {
                        ooVip = null;
                    }
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

            }

            return ooVip;

        }

        #endregion

        #region 根据用户ID读取用户信息
        /// <summary>
        /// 功能描述：根据用户ID读取用户信息
        /// 创建标识：ljd 20120525
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="urlstate">是否报错</param>
        /// <returns></returns>
        public static Model.Vip GetUserInfo(long userid, ref int urlstate)
        {

            SqlParameter[] Parm = new SqlParameter[1];
            Parm[0] = new SqlParameter("@UserID", SqlDbType.BigInt);

            Parm[0].Value = userid;

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "VIP_InfoBasicByUserID", Parm);

            Model.Vip vip = new Model.Vip();

            try
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        vip.username = Utils.ChangeType(reader["username"], typeof(string)).ToString();
                        vip.UserType = Convert.ToInt32(Utils.ChangeType(reader["usertype"], typeof(int)));
                        vip.name = Utils.ChangeType(reader["name"], typeof(string)).ToString();
                        vip.company = Utils.ChangeType(reader["company"], typeof(string)).ToString();
                        vip.address = Utils.ChangeType(reader["address"], typeof(string)).ToString();
                        vip.postcode = Utils.ChangeType(reader["postcode"], typeof(string)).ToString();
                        vip.tel = Utils.ChangeType(reader["tel"], typeof(string)).ToString();
                        vip.fax = Utils.ChangeType(reader["fax"], typeof(string)).ToString();
                        vip.email = Utils.ChangeType(reader["email"], typeof(string)).ToString();
                        vip.mobile = Utils.ChangeType(reader["mobile"], typeof(string)).ToString();
                        vip.MSN = Utils.ChangeType(reader["MSN"], typeof(string)).ToString();
                        vip.sex = Utils.ChangeType(reader["sex"], typeof(string)).ToString();
                        vip.QQ = Utils.ChangeType(reader["QQ"], typeof(string)).ToString();
                        vip.VCCID = Utils.ChangeType(reader["VCCID"], typeof(string)).ToString();
                        vip.VCFID = Utils.ChangeType(reader["VCFID"], typeof(string)).ToString();
                        vip.VCTID = Utils.ChangeType(reader["VCTID"], typeof(string)).ToString();
                        vip.CI_ID = Convert.ToInt32(Utils.ChangeType(reader["CI_ID"], typeof(int)));
                        vip.experience = Convert.ToDecimal(Utils.ChangeType(reader["experience"], typeof(decimal)));
                        vip.fame = Convert.ToInt32(Utils.ChangeType(reader["fame"], typeof(int)));
                        vip.knowledge = Convert.ToInt32(Utils.ChangeType(reader["knowledge"], typeof(int)));
                        vip.regDate = Convert.ToDateTime(Utils.ChangeType(reader["RegDate"], typeof(DateTime)));
                        vip.MemberLevel = Utils.ChangeType(reader["MemberLevel"], typeof(string)).ToString();
                        vip.password = Utils.ChangeType(reader["password"], typeof(string)).ToString();
                        vip.RID = Convert.ToInt32(Utils.ChangeType(reader["RID"], typeof(int)));
                        vip.Attach_UploadForbidden = Convert.ToBoolean(Utils.ChangeType(reader["Attach_UploadForbidden"], typeof(bool)));
                        vip.NewTopicNum = Convert.ToInt32(Utils.ChangeType(reader["NewTopicNum"], typeof(int)));
                        vip.isBBSNewbie = Convert.ToBoolean(Utils.ChangeType(reader["isBBSNewbie"], typeof(bool)));
                        vip.Buyer_Group_Level = Utils.ChangeType(reader["Buyer_Group_Level"], typeof(string)).ToString();
                        vip.ForumID = Utils.ChangeType(reader["ForumID"], typeof(string)).ToString();
                        vip.TopicCount = Convert.ToInt32(Utils.ChangeType(reader["TopicCount"], typeof(int)));
                        vip.PaperCount = Convert.ToInt32(Utils.ChangeType(reader["PaperCount"], typeof(int)));
                        vip.nickname = Utils.ChangeType(reader["nickname"], typeof(string)).ToString();
                        vip.LevelName = Convert.ToInt32(Utils.ChangeType(reader["LevelName"], typeof(int)));
                        vip.Factor_Exp = Convert.ToInt32(Utils.ChangeType(reader["Factor_Exp"], typeof(int)));
                        vip.OnlineTime = Convert.ToInt32(Utils.ChangeType(reader["onlinetime"], typeof(int)));
                        vip.mobile_pass = Convert.ToInt32(Utils.ChangeType(reader["mobile_pass"], typeof(int)));
                        vip.etc_pass = Convert.ToInt32(Utils.ChangeType(reader["etc_pass"], typeof(int)));
                        vip.school_pass = Convert.ToInt32(Utils.ChangeType(reader["school_pass"], typeof(int)));
                        vip.UserID = Convert.ToInt64(Utils.ChangeType(reader["UserID"], typeof(long)));
                        vip.MD5Code = Utils.ChangeType(reader["MD5Code"], typeof(string)).ToString();
                        vip.birthday = Utils.ChangeType(reader["birthday"], typeof(string)).ToString();
                        vip.mobile_time = Convert.ToDateTime(Utils.ChangeType(reader["mobile_time"], typeof(DateTime)));
                    }
                }
                else
                {
                    vip = null;
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
            return vip;

        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="id">流水号</param>
        /// </summary>
        public static DataTable GetModel(long id)
        {
            SqlParameter[] Param = new SqlParameter[1];

            Param[0] = new SqlParameter("@userid", SqlDbType.BigInt);
            Param[0].Value = id;

            return Com.ILog.Data.DataAggregate.GetDateTabel("VIP_GetEntityByUserID", Param);
        }
        #endregion

        #region 根据userid得到vip表的用户邮箱

        /// <summary>
        /// 功能描述：根据userid得到vip表的用户邮箱
        /// 创建标识：ljd 20120526
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="urlState">成功状态</param>
        /// <returns>用户邮箱</returns>
        public static string GetUserEmail(long userid, ref int urlState)
        {
            //用户邮箱
            string email = "";

            //存储过程
            string sql = "VIP_GetEmailByUserID";

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = { new SqlParameter("@userid", SqlDbType.BigInt) };
            Parm[0].Value = userid;

            try
            {
                email = Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, Parm), typeof(string)).ToString();
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

            return email;

        }

        #endregion

 

        #region 判断vip,vip_sleeping,vip_blacklist中是否存在该邮箱

        /// <summary>
        /// 功能描述：判断vip,vip_sleeping,vip_blacklist中是否存在该邮箱
        /// 创建标识：ljd 20120526
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="urlState">成功状态</param>
        /// <returns>是否存在</returns>
        public static bool VipAllExistsEmail(string email, ref int urlState)
        {
            //存储过程
            string sql = "VIP_IfExist_Email";

            //是否存在email
            bool isExist = false;

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = { new SqlParameter("@Email", SqlDbType.VarChar, 200) };
            Parm[0].Value = email;

            try
            {
                isExist = Convert.ToBoolean(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, Parm), typeof(bool)));
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

            return isExist;

        }

        #endregion

        #region 重新认证邮箱时更新vip信息
        /// <summary>
        /// 功能描述：重新认证邮箱时更新vip信息
        /// 创建标识：ljd 20120526
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="email">新的邮箱</param>
        /// <param name="urlstate">json是否成功</param>
        /// <returns></returns>
        public static int UpdateVipEmail(long userid, string email, ref int urlstate)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
					new SqlParameter("@email", SqlDbType.VarChar,200)};
            parameters[0].Value = userid;
            parameters[1].Value = email;

            SqlConnection con = DbHelperSQL.GetIMConnection();

            //执行行数
            int resultCount = 0;

            try
            {
                resultCount = DbHelperSQL.ExecuteNonQuery(con, CommandType.StoredProcedure, "VIP_UpdateEmail", parameters);
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

        #region 根据用户id得到用户名
        /// <summary>
        /// 功能描述：根据用户id得到用户名
        /// 创建标识：ljd 20120527
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="urlsate"></param>
        /// <returns></returns>
        public static string GetUserNameByUserID(long userid, ref int urlState)
        {
            //用户名
            string username = "";

            //存储过程
            string sql = "vip_GetUserNameByUserID";

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = { new SqlParameter("@userid", SqlDbType.BigInt) };
            Parm[0].Value = userid;

            try
            {
                username = Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, Parm), typeof(string)).ToString();
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

            return username;

        }
        #endregion

        #region 根据用户名得到用户id
        /// <summary>
        /// 功能描述：根据用户名得到用户id
        /// 创建标识：ljd 20120611
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="urlsate"></param>
        /// <returns></returns>
        public static long GetUserIDByUserName(string username, ref int urlState)
        {
            //用户id
            long userid = 0;

            //存储过程
            string sql = "vip_GetUserIDByUserName";

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = { new SqlParameter("@username", SqlDbType.NVarChar, 30) };
            Parm[0].Value = username;

            try
            {
                userid = Convert.ToInt64(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, Parm), typeof(long)));
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

            return userid;

        }
        #endregion

        #region 判断VIP用户手机号是否被其他人认证过

        /// <summary>
        /// 功能描述：判断VIP用户手机号是否被其他人认证过
        /// 创建标识：ljd 20120527
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="mobile">手机号</param>
        /// <param name="urlState">是否执行成功</param>
        /// <returns>0 未占用 1已占用</returns>
        public static int IsMobileExists(long userid, string mobile, ref int urlState)
        {
            string sql = "Vip_getExistmobileByUserID";

            SqlParameter[] Parm = new SqlParameter[2];
            Parm[0] = new SqlParameter("@userid", SqlDbType.BigInt);
            Parm[1] = new SqlParameter("@mobile", SqlDbType.VarChar, 11);

            Parm[0].Value = userid;
            Parm[1].Value = mobile;

            SqlConnection con = DbHelperSQL.GetIMConnection();

            //是否存在手机
            int state = 0;

            try
            {
                state = Convert.ToInt32(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, Parm), typeof(int)));
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

            return state;

        }

        #endregion

        #region 得到单位性质列表

        /// <summary>
        /// 功能描述：得到单位性质列表
        /// 创建标识：ljd 20120529
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetCompanyList(ref int urlState)
        {

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "VIPCode_CompanyGetList");

            Dictionary<string, string> dicCompany = new Dictionary<string, string>();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string VCCID = Utils.ChangeType(reader["VCCID"], typeof(string)).ToString();
                        string VCCName = Utils.ChangeType(reader["VCCName"], typeof(string)).ToString();
                        dicCompany.Add(VCCID, VCCName);
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
            return dicCompany;

        }

        #endregion

        #region 根据单位性质得到行业性质列表
        /// <summary>
        /// 功能描述：根据单位性质得到行业性质列表
        /// 创建标识：ljd 20120529
        /// </summary>
        /// <param name="VCCID"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetFiledList(string VCCID, ref int urlState)
        {
            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = { new SqlParameter("@vccid", SqlDbType.Char, 2) };
            Parm[0].Value = VCCID;

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "VIPCode_FieldListByVCCID", Parm);

            Dictionary<string, string> dicField = new Dictionary<string, string>();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string VCFID = Utils.ChangeType(reader["VCFID"], typeof(string)).ToString();
                        string VCFName = Utils.ChangeType(reader["VCFName"], typeof(string)).ToString();
                        dicField.Add(VCFID, VCFName);
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
            return dicField;

        }
        #endregion

        #region 根据行业性质id得到行业性质名称
        /// <summary>
        /// 功能描述：根据行业性质id得到行业性质名称
        /// 创建标识：ljd 20120530
        /// </summary>
        /// <param name="vccid">单位性质id</param>
        /// <param name="vcfid">行业性质id</param>
        /// <param name="urlState">是否报错</param>
        /// <returns></returns>
        public static string GetVCFNameByVCFID(string vccid, string vcfid, ref int urlState)
        {
            //行业名称
            string fieldName = "";

            //存储过程
            string sql = "vip_GetUserVCFNAME";

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = { new SqlParameter("@vccid", SqlDbType.Char, 2), new SqlParameter("@vcfid", SqlDbType.Char, 2) };
            Parm[0].Value = vccid;
            Parm[1].Value = vcfid;

            try
            {
                fieldName = Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, Parm), typeof(string)).ToString();
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

            return fieldName;

        }
        #endregion

        #region 根据单位性质id得到单位性质名称
        /// <summary>
        /// 功能描述：根据单位性质id得到单位性质名称
        /// 创建标识：ljd 20120530
        /// </summary>
        /// <param name="vccid">单位性质id</param>
        /// <param name="urlState">是否报错</param>
        /// <returns></returns>
        public static string GetVCCNameByVCCID(string vccid, ref int urlState)
        {
            //单位性质名称
            string vccname = "";

            //存储过程
            string sql = "vip_GetUserVCCNAME";

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = { new SqlParameter("@vccid", SqlDbType.Char, 2) };
            Parm[0].Value = vccid;

            try
            {
                vccname = Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, Parm), typeof(string)).ToString();
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

            return vccname;

        }
        #endregion

        #region 根据职位id得到职位名称
        /// <summary>
        /// 功能描述：根据职位id得到职位名称
        /// 创建标识：ljd 20120530
        /// </summary>
        /// <param name="vccid">单位性质id</param>
        /// <param name="vctid">职位性质id</param>
        /// <param name="urlState">是否报错</param>
        /// <returns></returns>
        public static string GetVCTNameByVCTID(string vccid, string vctid, ref int urlState)
        {
            //职位名称
            string vctname = "";

            //存储过程
            string sql = "vip_GetUserVCTNAME";

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = { new SqlParameter("@vccid", SqlDbType.Char, 2), new SqlParameter("@vctid", SqlDbType.Char, 2) };

            Parm[0].Value = vccid;
            Parm[1].Value = vctid;

            try
            {
                vctname = Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, Parm), typeof(string)).ToString();
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

            return vctname;

        }
        #endregion

        #region 得到vip地区
        /// <summary>
        /// 功能描述：得到vip地区
        /// 创建标识：ljd 20120530
        /// </summary>
        /// <param name="cityid">城市id</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetVIPRegion(int cityid, ref int urlState)
        {
            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = { new SqlParameter("@CI_ID", SqlDbType.BigInt) };
            Parm[0].Value = cityid;

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "vip_GetUserRegionIDAndName", Parm);

            Dictionary<string, string> dicRegion = new Dictionary<string, string>();

            try
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        dicRegion.Add("Country", Utils.ChangeType(reader["Country"], typeof(string)).ToString());
                        dicRegion.Add("Province", Utils.ChangeType(reader["Province"], typeof(string)).ToString());
                        dicRegion.Add("City", Utils.ChangeType(reader["City"], typeof(string)).ToString());
                        dicRegion.Add("CountryID", Convert.ToInt32(Utils.ChangeType(reader["CO_ID"], typeof(int))).ToString());
                        dicRegion.Add("ProvinceID", Convert.ToInt32(Utils.ChangeType(reader["PR_ID"], typeof(int))).ToString());
                        dicRegion.Add("CityID", Convert.ToInt32(Utils.ChangeType(reader["CI_ID"], typeof(int))).ToString());
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
            return dicRegion;

        }
        #endregion

        #region 更新vip的基本信息
        /// <summary>
        /// 功能描述：重新认证邮箱时更新vip信息
        /// 创建标识：ljd 20120526
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="email">新的邮箱</param>
        /// <param name="urlstate">json是否成功</param>
        /// <returns></returns>
        public static int UpdateVipBaseInfo(Model.Vip ooVip, ref int urlstate)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@userid", SqlDbType.BigInt),
                    new SqlParameter("@name", SqlDbType.VarChar,100),
                    new SqlParameter("@nickname", SqlDbType.VarChar,30),
                    new SqlParameter("@sex", SqlDbType.VarChar,7),
                    new SqlParameter("@birthday", SqlDbType.VarChar,10),
                    new SqlParameter("@vccid", SqlDbType.Char,2),
                    new SqlParameter("@vcfid", SqlDbType.Char,2),
                    new SqlParameter("@vctid", SqlDbType.Char,1),
                    new SqlParameter("@ci_id", SqlDbType.Int),
                    new SqlParameter("@company", SqlDbType.VarChar,120),
                    new SqlParameter("@address", SqlDbType.VarChar,150),
                    new SqlParameter("@qq", SqlDbType.VarChar,25),
                    new SqlParameter("@msn", SqlDbType.VarChar,70),
                    new SqlParameter("@tel", SqlDbType.VarChar,200)};

            parameters[0].Value = ooVip.UserID;
            parameters[1].Value = ooVip.name;
            parameters[2].Value = ooVip.nickname;
            parameters[3].Value = ooVip.sex;
            parameters[4].Value = ooVip.birthday;
            parameters[5].Value = ooVip.VCCID;
            parameters[6].Value = ooVip.VCFID;
            parameters[7].Value = ooVip.VCTID;
            parameters[8].Value = ooVip.CI_ID;
            parameters[9].Value = ooVip.company;
            parameters[10].Value = ooVip.address;
            parameters[11].Value = ooVip.QQ;
            parameters[12].Value = ooVip.MSN;
            parameters[13].Value = ooVip.tel;

            SqlConnection con = DbHelperSQL.GetIMConnection();

            //执行行数
            int resultCount = 0;

            try
            {
                resultCount = DbHelperSQL.ExecuteNonQuery(con, CommandType.StoredProcedure, "VIP_UpdateBaseInfo", parameters);
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

        #region 发送站短

        /// <summary>
        /// 功能描述：发送站短
        /// 创建标识：ljd 20120203
        /// </summary>
        /// <param name="fromwho">发件人</param>
        /// <param name="towho">收件人</param>
        /// <param name="subject">标题</param>
        /// <param name="content">内容</param>
        /// <param name="ip">ip</param>
        /// <returns></returns>
        public static int InsertVipEmail(string fromwho, string towho, string subject, string content, string ip)
        {
            int resultCount = 0;

            SqlParameter[] Parm = new SqlParameter[5];

            Parm[0] = new SqlParameter("@fromwho", SqlDbType.VarChar, 50);
            Parm[1] = new SqlParameter("@towho", SqlDbType.VarChar, 50);
            Parm[2] = new SqlParameter("@subject", SqlDbType.VarChar, 150);
            Parm[3] = new SqlParameter("@content", SqlDbType.VarChar, 2000);
            Parm[4] = new SqlParameter("@ip", SqlDbType.VarChar, 20);

            Parm[0].Value = fromwho;
            Parm[1].Value = towho;
            Parm[2].Value = subject;
            Parm[3].Value = content;
            Parm[4].Value = ip;

            SqlConnection con = DbHelperSQL.GetIMConnection();

            try
            {
                resultCount = Convert.ToInt32(Utils.ChangeType(DbHelperSQL.ExecuteNonQuery(con, CommandType.StoredProcedure, "vip_insert_vipEmail", Parm),
                    typeof(int)));
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

        #region 得到国家列表

        /// <summary>
        /// 功能描述：得到国家列表
        /// 创建标识：ljd 20120601
        /// </summary>
        /// <param name="fromwho">是否报错</param>
        /// <returns></returns>
        public static Dictionary<short, string> GetCountryList(ref int urlState)
        {

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "vipzone_countryGetList");

            Dictionary<short, string> dicCountry = new Dictionary<short, string>();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        short CO_ID = Convert.ToInt16(Utils.ChangeType(reader["CO_ID"], typeof(short)));
                        string CO_Name = Utils.ChangeType(reader["CO_Name"], typeof(string)).ToString();
                        dicCountry.Add(CO_ID, CO_Name);
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
            return dicCountry;

        }

        #endregion

        #region 得到省份列表

        /// <summary>
        /// 功能描述：得到省份列表
        /// 创建标识：ljd 20120601
        /// </summary>
        /// <param name="countryID">国家id</param>
        /// <param name="urlState">是否报错</param>
        /// <returns></returns>
        public static Dictionary<int, string> GetPorvinceList(int countryID, ref int urlState)
        {

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = new SqlParameter[1];

            Parm[0] = new SqlParameter("@nationalityid", SqlDbType.Int);

            Parm[0].Value = countryID;

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "vip_GetProvince", Parm);

            Dictionary<int, string> dicProvince = new Dictionary<int, string>();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int PR_ID = Convert.ToInt32(Utils.ChangeType(reader["PR_ID"], typeof(int)));
                        string PR_Name = Utils.ChangeType(reader["PR_Name"], typeof(string)).ToString();
                        dicProvince.Add(PR_ID, PR_Name);
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
            return dicProvince;

        }

        #endregion

        #region 得到城市列表

        /// <summary>
        /// 功能描述：得到城市列表
        /// 创建标识：ljd 20120601
        /// </summary>
        /// <param name="provinceID">省份id</param>
        /// <param name="urlState">是否报错</param>
        /// <returns></returns>
        public static Dictionary<int, string> GetCityList(int provinceID, ref int urlState)
        {

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = new SqlParameter[1];

            Parm[0] = new SqlParameter("@provinceid", SqlDbType.Int);

            Parm[0].Value = provinceID;

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, "vip_GetCity", Parm);

            Dictionary<int, string> dicCity = new Dictionary<int, string>();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int CI_ID = Convert.ToInt32(Utils.ChangeType(reader["CI_ID"], typeof(int)));
                        string CI_Name = Utils.ChangeType(reader["CI_Name"], typeof(string)).ToString();
                        dicCity.Add(CI_ID, CI_Name);
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
            return dicCity;

        }

        #endregion


        #region 判断用户昵称是否存在

        /// <summary>
        /// 功能描述：判断用户昵称是否存在
        /// 创建标识：ljd 20120603
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="nickname">昵称</param>
        /// <param name="urlState">是否报错</param>
        /// <returns>0 不重名 1重名</returns>
        public static int CheckNickNameExists(long userid, string nickname, ref int urlState)
        {
            string sql = "VIP_CheckNickName";

            SqlParameter[] Parm = new SqlParameter[2];
            Parm[0] = new SqlParameter("@userid", SqlDbType.BigInt);
            Parm[1] = new SqlParameter("@nickname", SqlDbType.NVarChar, 20);

            Parm[0].Value = userid;
            Parm[1].Value = nickname;

            SqlConnection con = DbHelperSQL.GetIMConnection();

            //数据库中已存在个数
            int count = 0;

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

        #region 添加申请记录
        /// <summary>
        /// 功能描述：添加申请记录
        /// 创建标识：ljd 20120605
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="urlstate">json是否成功</param>
        /// <returns></returns>
        public static int VIPActiveSleepingAdd(string username, int activeType, ref int urlstate)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@username", SqlDbType.NVarChar,30),
                    new SqlParameter("@activeType", SqlDbType.Int)};
            parameters[0].Value = username;
            parameters[1].Value = activeType;

            SqlConnection con = DbHelperSQL.GetIMConnection();

            //执行行数
            int resultCount = 0;

            try
            {
                resultCount = DbHelperSQL.ExecuteNonQuery(con, CommandType.StoredProcedure, "VIPActiveSleepingAdd", parameters);
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

        #region 手机认证通过后更新vip
        /// <summary>
        /// 功能描述：手机认证通过后更新vip
        /// 创建标识：ljd 20120605
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="mobile">手机号</param>
        /// <param name="urlstate">json是否成功</param>
        /// <returns></returns>
        public static int UpdateVipMobile(long userid, string mobile, ref int urlstate)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
                    new SqlParameter("@mobile", SqlDbType.NVarChar,20)};

            parameters[0].Value = userid;
            parameters[1].Value = mobile;

            SqlConnection con = DbHelperSQL.GetIMConnection();

            //执行行数
            int resultCount = 0;

            try
            {
                resultCount = DbHelperSQL.ExecuteNonQuery(con, CommandType.StoredProcedure, "VIP_UpdateMobile", parameters);
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

        #region 得到模拟vip用户列表
        /// <summary>
        /// 功能描述：得到模拟vip用户列表
        /// 创建标识：ljd 20120618
        /// </summary>
        /// <returns></returns>
        public static List<Model.Vip> GetDemoUserList()
        {
            string sql = "SELECT UserName,UserID,ISNULL(ilogstate,-1) AS IlogState from ( SELECT UserName,UserID,(SELECT ISNULL(vi_State,-1) FROM ilog.dbo.VIP_ILog ilog WHERE ilog.userid=vip.userid) AS IlogState  FROM VIP where username like 'ak_ljd%' or username in('portia','weichengzhe','gui_qi','_4077','huaweimiann','wedochem03','fjcfmx-6511','sinengwangfang','beijingyuhua','chenjunzhang','yejinceshi','maruping','miracle7','madprodigy','lhx106','tiger333','ysuyf','dongxin1976','kedangliang','kedapig','kedian17','shirley1987','shirley263','shirley65530','shirley7','wl5678','wl86919707','hui539')) tmp ";

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.Text, con, sql);

            List<Model.Vip> vipList = new List<ILog.Model.Vip>();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Model.Vip ooVip = new ILog.Model.Vip();
                        ooVip.UserID = Convert.ToInt64(Utils.ChangeType(reader["UserID"], typeof(long)));
                        ooVip.username = Utils.ChangeType(reader["username"], typeof(string)).ToString();
                        ooVip.RID = Convert.ToInt32(Utils.ChangeType(reader["IlogState"], typeof(int)));

                        vipList.Add(ooVip);
                    }
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
            return vipList;

        }
        #endregion


        #region 手机第一次通过认证后加积分
        /// <summary>
        /// 功能描述：手机第一次通过认证后加积分
        /// 创建标识：ljd 20120620
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="ip">ip地址</param>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public static int ApproveMobile(long userid, string ip, string username)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@userid", SqlDbType.BigInt),
					new SqlParameter("@ip", SqlDbType.VarChar,50),
                    new SqlParameter("@username", SqlDbType.NVarChar,30)};

            parameters[0].Value = userid;
            parameters[1].Value = ip;
            parameters[2].Value = username;

            SqlConnection con = DbHelperSQL.GetIMConnection();

            //执行行数
            int resultCount = 0;

            try
            {
                resultCount = DbHelperSQL.ExecuteNonQuery(con, CommandType.StoredProcedure, "VIP_ApproveMobile", parameters);
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

        #region 得到vip用户认证过的手机号
        /// <summary>
        /// 功能描述：得到vip用户认证过的手机号
        /// 创建标识：ljd 20120701
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static string GetCheckedMobile(long userid)
        {
            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = { new SqlParameter("@userid", SqlDbType.BigInt) };
            Parm[0].Value = userid;

            string mobile = "";

            try
            {
                mobile = Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, "Vip_GetCheckedMobile", Parm), typeof(string)).ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return mobile;

        }
        #endregion

        #region 根据username判断前一周是否全打卡

        /// <summary>
        /// 功能描述：根据username判断前一周是否全打卡
        /// 创建标识：ljd 20120812
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>0 没有 1全部</returns>
        public static int GetLastWeekAllHitCount(string username)
        {
            //前一周是否全部打卡个数
            int hitCount = 0;

            //存储过程
            string sql = "ss_VIP_Daka_LastWeekAllHit";

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = { new SqlParameter("@username", SqlDbType.VarChar, 30) };
            Parm[0].Value = username;

            try
            {
                hitCount = Convert.ToInt32(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, Parm), typeof(int)));
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

            return hitCount;

        }

        #endregion

        #region 读取用户的前一天的在线时长

        /// <summary>
        /// 功能描述：读取用户的前一天的在线时长
        /// 创建标识：ljd 20120812
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>前一天的在线时长</returns>
        public static int GetYesterdayTotalOnLine(string username)
        {
            //前一天的在线时长
            int totalline = 0;

            //存储过程
            string sql = "VIP_BBSOnlinetime_LastDayCount";

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = { new SqlParameter("@username", SqlDbType.VarChar, 30) };
            Parm[0].Value = username;

            try
            {
                totalline = Convert.ToInt32(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, Parm), typeof(int)));
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

            return totalline;

        }

        #endregion

        #region 统计当天是否领取上一天的时长奖励

        /// <summary>
        /// 功能描述：统计当天是否领取上一天的时长奖励
        /// 创建标识：ljd 20120812
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>是否领取奖励 0未领取 1领取</returns>
        public static int GetLastDayReward(string username)
        {
            //是否领取奖励
            int acceptReward = 0;

            //存储过程
            string sql = "VIP_BBSOnlinetime_LastDayReward";

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = { new SqlParameter("@username", SqlDbType.VarChar, 30) };
            Parm[0].Value = username;

            try
            {
                acceptReward = Convert.ToInt32(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, Parm), typeof(int)));
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

            return acceptReward;

        }

        #endregion

        #region 判断当天是否已经打卡

        /// <summary>
        /// 功能描述：判断当天是否已经打卡
        /// 创建标识：ljd 20120812
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>是否打卡 0未打卡 1已打卡</returns>
        public static int GetTodayHit(string username)
        {
            //是否领取奖励
            int hitCount = 0;

            //存储过程
            string sql = "ss_VIP_Daka_TodayHit";

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = { new SqlParameter("@username", SqlDbType.VarChar, 30) };
            Parm[0].Value = username;

            try
            {
                hitCount = Convert.ToInt32(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, Parm), typeof(int)));
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

            return hitCount;

        }

        #endregion

        #region 判断是否领取上一周的全勤奖励

        /// <summary>
        /// 功能描述：判断是否领取上一周的全勤奖励
        /// 创建标识：ljd 20120813
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>是否领取奖励 0未领取 1领取</returns>
        public static int GetLastWeekReward(string username)
        {
            //是否领取奖励
            int acceptReward = 0;

            //存储过程
            string sql = "VIP_LastWeekReward";

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = { new SqlParameter("@username", SqlDbType.VarChar, 30) };
            Parm[0].Value = username;

            try
            {
                acceptReward = Convert.ToInt32(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, Parm), typeof(int)));
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

            return acceptReward;

        }

        #endregion

        #region 添加打卡记录
        /// <summary>
        /// 功能描述：添加打卡记录
        /// 创建标识：ljd 20120813
        /// <param name="userid">用户id</param>
        /// <param name="face">用户头像</param>
        /// </summary>
        public static int InsertDaka(string username, int score)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@username", SqlDbType.VarChar,30),
					new SqlParameter("@score", SqlDbType.Int)};

            parameters[0].Value = username;
            parameters[1].Value = score;

            return Com.ILog.Data.DataAggregate.EXECprocedureCount("ss_VIP_Daka_Insert", parameters);

        }
        #endregion

        #region 添加会员积分增减记录

        /// <summary>
        /// 功能描述：添加会员积分、经验、声望增减记录
        /// 创建标识：ljd 20120203
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="fame">声望</param>
        /// <param name="experience">积分</param>
        /// <param name="ip">ip</param>
        /// <param name="referer">原因</param>
        /// <param name="atype">加分类型</param>
        public static int InsertAddScoreBatch_U(Model.VipScore score)
        {
            int resultCount = 0;

            SqlParameter[] Parm = new SqlParameter[11];

            Parm[0] = new SqlParameter("@username", SqlDbType.VarChar, 30);
            Parm[1] = new SqlParameter("@score", SqlDbType.Int);
            Parm[2] = new SqlParameter("@knowledge", SqlDbType.Int);
            Parm[3] = new SqlParameter("@fame", SqlDbType.Int);
            Parm[4] = new SqlParameter("@OnlineTime", SqlDbType.Int);
            Parm[5] = new SqlParameter("@ActionType", SqlDbType.Int);
            Parm[6] = new SqlParameter("@ActionTime", SqlDbType.DateTime);
            Parm[7] = new SqlParameter("@id", SqlDbType.BigInt);
            Parm[8] = new SqlParameter("@ip", SqlDbType.VarChar, 20);
            Parm[9] = new SqlParameter("@referer", SqlDbType.VarChar, 500);
            Parm[10] = new SqlParameter("@Gold", SqlDbType.Int);

            Parm[0].Value = score.username;
            Parm[1].Value = score.score;
            Parm[2].Value = score.knowledge;
            Parm[3].Value = score.fame;
            Parm[4].Value = score.OnlineTime;
            Parm[5].Value = score.ActionType;
            Parm[6].Value = score.ActionTime;
            Parm[7].Value = score.id;
            Parm[8].Value = score.ip;
            Parm[9].Value = score.referer;
            Parm[10].Value = score.gold;

            SqlConnection con = DbHelperSQL.GetIMConnection();

            try
            {
                resultCount = Convert.ToInt32(Utils.ChangeType(DbHelperSQL.ExecuteNonQuery(con, CommandType.StoredProcedure, "VIP_InsertLog", Parm),
                    typeof(int)));
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

        #region 领取打卡周全勤奖励
        /// <summary>
        /// 功能描述：领取打卡周全勤奖励
        /// 创建标识：ljd 20120820
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public static int VIPAcceptWeekReward(string username)
        {
            int resultCount = 0;

            SqlParameter[] Parm = new SqlParameter[1];

            Parm[0] = new SqlParameter("@username", SqlDbType.VarChar, 30);

            Parm[0].Value = username;

            SqlConnection con = DbHelperSQL.GetIMConnection();

            try
            {
                resultCount = Convert.ToInt32(Utils.ChangeType(DbHelperSQL.ExecuteNonQuery(con, CommandType.StoredProcedure, "ss_VIP_Daka_AcceptWeekReward", Parm),
                    typeof(int)));
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

        #region 查看是否领取全勤奖勋章
        /// <summary>
        /// 功能描述：查看是否领取全勤奖勋章
        /// 创建标识：ljd 20120820
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public static int VIPExistWeekRewardMedal(string username)
        {
            int resultCount = 0;

            SqlParameter[] Parm = new SqlParameter[1];

            Parm[0] = new SqlParameter("@username", SqlDbType.VarChar, 30);

            Parm[0].Value = username;

            SqlConnection con = DbHelperSQL.GetIMConnection();

            try
            {
                resultCount = Convert.ToInt32(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, "SS_Insignia_User_ExistModel", Parm),
                    typeof(int)));
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

        #region 更新勋章的获得时间
        /// <summary>
        /// 功能描述：更新勋章的获得时间
        /// 创建标识：ljd 20120820
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="ssid">勋章id</param>
        /// <returns></returns>
        public static int UpdateInsigniaTime(string username, int ssid)
        {
            int resultCount = 0;

            SqlParameter[] Parm = new SqlParameter[2];

            Parm[0] = new SqlParameter("@username", SqlDbType.VarChar, 30);
            Parm[1] = new SqlParameter("@ssid", SqlDbType.Int);

            Parm[0].Value = username;
            Parm[1].Value = ssid;

            SqlConnection con = DbHelperSQL.GetIMConnection();

            try
            {
                resultCount = Convert.ToInt32(Utils.ChangeType(DbHelperSQL.ExecuteNonQuery(con, CommandType.StoredProcedure, "SS_Insignia_User_UpdateTime", Parm),
                    typeof(int)));
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

        #region 添加勋章
        /// <summary>
        /// 功能描述：添加勋章
        /// 创建标识：ljd 20120820
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="ssid">勋章id</param>
        /// <returns></returns>
        public static int AddInsignia(string username, int ssid)
        {
            int resultCount = 0;

            SqlParameter[] Parm = new SqlParameter[2];

            Parm[0] = new SqlParameter("@username", SqlDbType.VarChar, 30);
            Parm[1] = new SqlParameter("@insigniaID", SqlDbType.Int);

            Parm[0].Value = username;
            Parm[1].Value = ssid;

            SqlConnection con = DbHelperSQL.GetIMConnection();

            try
            {
                resultCount = Convert.ToInt32(Utils.ChangeType(DbHelperSQL.ExecuteNonQuery(con, CommandType.StoredProcedure, "SS_Insignia_UserInsert", Parm),
                    typeof(int)));
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

        #region 根据用户名，用户名类别得到用户信息

        /// <summary>
        /// 功能描述：根据用户名，用户名类别得到用户信息
        /// 创建标识：ljd 20120912
        /// </summary>
        /// <param name="username"></param>
        /// <param name="logintype"></param>
        /// <returns></returns>
        public static Model.Vip GetUserInfoByUserNameAndType(string username, string logintype)
        {
            string sql = "VIP_Login_info";

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = new SqlParameter[2];

            Parm[0] = new SqlParameter("@FileKey", SqlDbType.VarChar, 10);
            Parm[1] = new SqlParameter("@username", SqlDbType.VarChar, 30);

            Parm[0].Value = logintype;
            Parm[1].Value = username;

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.StoredProcedure, con, sql, Parm);

            Model.Vip ooVip = new ILog.Model.Vip();

            try
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        ooVip.UserID = Convert.ToInt64(Utils.ChangeType(reader["UserID"], typeof(long)));
                        ooVip.username = Utils.ChangeType(reader["username"], typeof(string)).ToString();
                        ooVip.UserType = Convert.ToInt32(Utils.ChangeType(reader["UserType"], typeof(int)));
                        ooVip.isPassed = Convert.ToBoolean(Utils.ChangeType(reader["isPassed"], typeof(bool)));
                        ooVip.MD5Code = Utils.ChangeType(reader["MD5Code"], typeof(string)).ToString();
                        ooVip.MemberLevel = Utils.ChangeType(reader["MemberLevel"], typeof(string)).ToString();
                        ooVip.hasFillInField = Convert.ToBoolean(Utils.ChangeType(reader["hasFillInField"], typeof(bool)));
                        ooVip.hasFillInField = Convert.ToBoolean(Utils.ChangeType(reader["hasFillInField"], typeof(bool)));
                        ooVip.MD5Password = Utils.ChangeType(reader["MD5Password"], typeof(string)).ToString();
                    }
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
            return ooVip;

        }
        #endregion

        #region 将沉默用户拉入vip

        /// <summary>
        /// 功能描述：将沉默用户拉入vip
        /// 创建标识：ljd 20120912
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="logintype">用户名类别</param>
        /// <returns>-1 导入失败 0导入成功</returns>
        public static int ActiveVip(string username, string logintype)
        {
            //是否登录成功
            int isfaile = -1;

            //存储过程
            string sql = "VIP_CheckVipLogin";

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = { new SqlParameter("@UserName", SqlDbType.VarChar, 30), new SqlParameter("@FileKey", SqlDbType.VarChar, 10) };

            Parm[0].Value = username;
            Parm[1].Value = logintype;

            try
            {
                isfaile = Convert.ToInt32(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.StoredProcedure, sql, Parm), typeof(int)));
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

            return isfaile;

        }

        #endregion

        #region 判断用户是否在vip表中存在

        /// <summary>
        /// 功能描述：判断用户是否在vip表中存在
        /// 创建标识：ljd 20120912
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="logintype">用户名类别</param>
        /// <returns>0不存在</returns>
        public static long GetSleepingVipUserID(string username, string logintype)
        {
            //沉默表用户id
            long userid = 0;

            //存储过程
            string sql = "select userid from vip_sleeping where " + logintype + " = @username";

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = { new SqlParameter("@username", SqlDbType.VarChar, 30) };

            Parm[0].Value = username;

            try
            {
                userid = Convert.ToInt64(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.Text, sql, Parm), typeof(long)));
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

        #region 判断用户是否在黑名单表中

        /// <summary>
        /// 功能描述：判断用户是否在黑名单表中
        /// 创建标识：ljd 20120912
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="logintype">用户名类别</param>
        /// <returns>0不存在</returns>
        public static long GetBlackListUserID(string username, string logintype)
        {
            //沉默表用户id
            long userid = 0;

            //存储过程
            string sql = "select userid from vip_blacklist where " + logintype + " = @username";

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = { new SqlParameter("@username", SqlDbType.VarChar, 30) };

            Parm[0].Value = username;

            try
            {
                userid = Convert.ToInt64(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.Text, sql, Parm), typeof(long)));
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

        #region 添加登录错误日志

        /// <summary>
        /// 功能描述：添加登录错误日志
        /// 创建标识：ljd 20120912
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="ip">ip地址</param>
        /// <returns>0不存在</returns>
        public static int VIPLoginErrAdd(Model.VIPLoginErr ooErr)
        {
            //影响行数
            int resultCount = 0;

            //存储过程
            string sql = "VIP_LoginErrAdd";

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = { new SqlParameter("@username", SqlDbType.VarChar, 30),new SqlParameter("@ip", SqlDbType.VarChar, 20) ,new SqlParameter("@cookieid", SqlDbType.VarChar, 50) 
                                  ,new SqlParameter("@agent", SqlDbType.VarChar, 600) ,new SqlParameter("@erint", SqlDbType.Int) };

            Parm[0].Value = ooErr.Username;
            Parm[1].Value = ooErr.Ip;
            Parm[2].Value = ooErr.CookieID;
            Parm[3].Value = ooErr.Agent;
            Parm[4].Value = ooErr.Erint;

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

        #region 判断用户是否被封全站

        /// <summary>
        /// 功能描述：判断用户是否被封全站
        /// 创建标识：ljd 20120912
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="logintype">用户名类别</param>
        /// <returns>0不存在</returns>
        public static Model.UBBForbidPost GetForbidUser(string username)
        {
            Model.UBBForbidPost ooForbidPost = new ILog.Model.UBBForbidPost();

            //存储过程
            string sql = "SELECT top 1 OutDate,Reason,Intime,Admin FROM UBB_ForbidPost where ForumID=0 and username=@username order by ForumID";

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = { new SqlParameter("@username", SqlDbType.VarChar, 30) };

            Parm[0].Value = username;

            SqlDataReader reader = DbHelperSQL.ExecuteReader(CommandType.Text, con, sql, Parm);

            try
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        ooForbidPost.OutDate = Convert.ToDateTime(Utils.ChangeType(reader["OutDate"], typeof(DateTime)));
                        ooForbidPost.Reason = Utils.ChangeType(reader["Reason"], typeof(string)).ToString();
                        ooForbidPost.InTime = Convert.ToDateTime(Utils.ChangeType(reader["InTime"], typeof(DateTime)));
                        ooForbidPost.Admin = Utils.ChangeType(reader["Admin"], typeof(string)).ToString();
                    }
                }
                else
                {
                    ooForbidPost = null;
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

            return ooForbidPost;

        }

        #endregion

        #region 判断用户是否是专家

        /// <summary>
        /// 功能描述：判断用户是否是专家
        /// 创建标识：ljd 20120913
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>0不是</returns>
        public static int GetExpertID(string username)
        {
            //专家id
            int expertid = 0;

            //存储过程
            string sql = "select id from ss_expert where username = @username AND IS_Expert=1";

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = { new SqlParameter("@username", SqlDbType.VarChar, 30) };

            Parm[0].Value = username;

            try
            {
                expertid = Convert.ToInt32(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.Text, sql, Parm), typeof(int)));
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

            return expertid;

        }

        #endregion

        #region 判断用户是否是版主

        /// <summary>
        /// 功能描述：判断用户是否是版主
        /// 创建标识：ljd 20120913
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>0不是</returns>
        public static int GetMasterID(string username)
        {
            //版主id
            int masterid = 0;

            //存储过程
            string sql = "select id from SS_Board_Master where BoardMaster = @username AND IS_State=2";

            SqlConnection con = DbHelperSQL.GetIMConnection();

            SqlParameter[] Parm = { new SqlParameter("@username", SqlDbType.VarChar, 30) };

            Parm[0].Value = username;

            try
            {
                masterid = Convert.ToInt32(Utils.ChangeType(DbHelperSQL.ExecuteScalar(con, CommandType.Text, sql, Parm), typeof(int)));
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

            return masterid;

        }

        #endregion

    }
}
