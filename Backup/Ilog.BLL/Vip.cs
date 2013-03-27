using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;
using System.Web;

using System.Data;
using Com.ILog.Utils;
using ILog.Common;


namespace Ilog.BLL
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
            int existState = ILog.DAL.Vip.IsExistsVIP(userid, ref urlState);
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
        public static ILog.Model.Vip GetUserFace(long userid, ref int urlState)
        {
            ILog.Model.Vip ooVip = ILog.DAL.Vip.GetUserFace(userid, ref urlState);
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
        /// <returns>用户实体</returns>
        public static ILog.Model.Vip GetUserInfo(long userid, ref int urlstate)
        {
            ILog.Model.Vip ooVip = ILog.DAL.Vip.GetUserInfo(userid, ref urlstate);
            return ooVip;

        }

        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="id">流水号</param>
        /// </summary>
        public static string GetModel(long id)
        {
            DataTable dblModelList = ILog.DAL.Vip.GetModel(id);

            //构建josn字符串 
            string VipModelJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblModelList).ToString();

            return VipModelJosn;

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
            int resultCount = ILog.DAL.Vip.UpdateVipEmail(userid, email, ref urlstate);
            return resultCount;

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
            string email = ILog.DAL.Vip.GetUserEmail(userid, ref urlState);
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
            //vip,vip_sleeping,vip_blacklist是否存在email
            bool isExistVip = ILog.DAL.Vip.VipAllExistsEmail(email, ref urlState);

            return isExistVip;

        }
        #endregion

        #region 更新认证的邮箱
        /// <summary>
        /// 功能描述：更新认证的邮箱
        /// 创建标识：ljd 20120526
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="email">email</param>
        /// <param name="urlState">url状态</param>
        /// <returns>-1:不存在vip 1:邮箱不存在 2:邮箱已存在</returns>
        public static int CheckExistsEmail(long userid, string email, ref int urlState)
        {
            //状态
            int state = 0;

            //是否存在vip
            int existsVip = BLL.Vip.IsExistsVIP(userid, ref urlState);

            if (existsVip == 0)
            {
                //不存在vip
                state = -1;
                return state;
            }

            //之前填写的邮箱
            string oldEMail = BLL.Vip.GetUserEmail(userid, ref urlState);
            if (oldEMail != email)
            {
                //是否存在该邮箱
                bool isExistEmail = BLL.Vip.VipAllExistsEmail(email, ref urlState);

                if (isExistEmail)//存在该邮箱
                {
                    state = 2;//邮箱已存在
                    return state;
                }
            }

            //更新vip邮箱 0更新失败 1更新成功
            state = BLL.Vip.UpdateVipEmail(userid, email, ref urlState);
            return state;

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
            string username = ILog.DAL.Vip.GetUserNameByUserID(userid, ref urlState);
            return username;

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
            int state = ILog.DAL.Vip.IsMobileExists(userid, mobile, ref urlState);
            return state;

        }

        #endregion

        #region 得到出生年份下拉列表

        /// <summary>
        /// 功能描述：得到出生年份下拉列表
        /// 创建标识：ljd 20120529
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static string GetSelYearListStr(int year)
        {
            StringBuilder strbYearList = new StringBuilder();

            for (int i = DateTime.Now.Year; i >= 1900; i--)
            {
                if (year != i)
                {
                    strbYearList.AppendFormat("<option value=\"{0}\">{0}</option>", i);
                }
                else
                {
                    strbYearList.AppendFormat("<option value=\"{0}\" selected=\"selected\">{0}</option>", i);
                }
            }
            return strbYearList.ToString();

        }

        #endregion

        #region 得到出生月份下拉列表

        /// <summary>
        /// 功能描述：得到出生月份下拉列表
        /// 创建标识：ljd 20120529
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static string GetSelMonthListStr(int month)
        {
            StringBuilder strbMonthList = new StringBuilder();

            strbMonthList.AppendFormat("<option value=\"\">&nbsp;</option>");
            for (int i = 1; i <= 12; i++)
            {
                if (month != i)
                {
                    strbMonthList.AppendFormat("<option value=\"{0}\">{1}</option>", i, i.ToString("00"));
                }
                else
                {
                    strbMonthList.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}</option>", i, i.ToString("00"));
                }
            }
            return strbMonthList.ToString();

        }

        #endregion

        #region 得到出生天数下拉列表

        /// <summary>
        /// 功能描述：得到出生天数下拉列表
        /// 创建标识：ljd 20120529
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static string GetSelDayListStr(int year, int month, int day)
        {
            StringBuilder strbDayList = new StringBuilder();

            strbDayList.AppendFormat("<option value=\"\">&nbsp;</option>");

            if (year != 0 && month != 0)
            {
                //一个月的天数
                int days = DateTime.DaysInMonth(year, month);

                for (int i = 1; i <= days; i++)
                {
                    if (day != i)
                    {
                        strbDayList.AppendFormat("<option value=\"{0}\">{1}</option>", i, i.ToString("00"));
                    }
                    else
                    {
                        strbDayList.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}</option>", i, i.ToString("00"));
                    }
                }
            }

            return strbDayList.ToString();

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
            string fieldName = ILog.DAL.Vip.GetVCFNameByVCFID(vccid, vcfid, ref urlState);
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
            string vccname = ILog.DAL.Vip.GetVCCNameByVCCID(vccid, ref urlState);
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
            string vctname = ILog.DAL.Vip.GetVCTNameByVCTID(vccid, vctid, ref urlState);
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
            Dictionary<string, string> DicRegion = ILog.DAL.Vip.GetVIPRegion(cityid, ref urlState);
            return DicRegion;

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
            int resultCount = ILog.DAL.Vip.InsertVipEmail(fromwho, towho, subject, content, ip);
            return resultCount;

        }
        #endregion

        #region 构建天数json
        /// <summary>
        /// 功能描述：构建天数json
        /// 创建标识：ljd 20120531
        /// </summary>
        /// <param name="year">数据</param>
        /// <param name="month">数据</param>
        /// <param name="day">数据</param>
        /// <returns></returns>
        public static string GetJsonDaysList(int year, int month, int day)
        {
            StringBuilder strbDayList = new StringBuilder();

            strbDayList.Append("{DayList:[");


            if (year != 0 && month != 0)
            {
                strbDayList.Append("{State:'1'},");
                //一个月的天数
                int days = DateTime.DaysInMonth(year, month);

                for (int i = 1; i <= days; i++)
                {
                    strbDayList.Append("{key:'" + i.ToString("00") + "',value:'" + i + "'}");
                    if (i == days)
                    {
                        strbDayList.Append("]}");
                    }
                    else
                    {
                        strbDayList.Append(",");
                    }
                }
            }
            else
            {
                strbDayList.Append("{State:'0'}]}");
            }
            return strbDayList.ToString();

        }
        #endregion

        #region 得到单位性质列表

        /// <summary>
        /// 功能描述：得到单位性质列表
        /// 创建标识：ljd 20120529
        /// </summary>
        /// <param name="urlState">是否报错</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetCompanyList(ref int urlState)
        {
            Dictionary<string, string> dicCompany = ILog.DAL.Vip.GetCompanyList(ref urlState);
            return dicCompany;

        }
        #endregion

        #region 根据单位性质得到行业性质列表
        /// <summary>
        /// 功能描述：根据单位性质得到行业性质列表
        /// 创建标识：ljd 20120529
        /// </summary>
        /// <param name="VCCID">单位性质id</param>
        /// <param name="urlState">是否报错</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetFiledList(string VCCID, ref int urlState)
        {
            Dictionary<string, string> dicField = new Dictionary<string, string>();
            switch (VCCID)
            {
                case "00":
                    dicField.Add("00", "仪器仪表制造");
                    urlState = 1;
                    break;
                case "01":
                    dicField.Add("01", "仪器仪表销售");
                    urlState = 1;
                    break;
                case "17":
                    dicField.Add("26", "卫生/医疗/临床");
                    urlState = 1;
                    break;
                case "19":
                    dicField.Add("25", "环保/水工业");
                    urlState = 1;
                    break;
                default:
                    dicField = ILog.DAL.Vip.GetFiledList(VCCID, ref urlState);
                    break;
            }
            return dicField;

        }

        #endregion

        #region 根据单位性质得到职位列表

        /// <summary>
        /// 功能描述：根据单位性质得到职位列表
        /// 创建标识：ljd 20120531
        /// </summary>
        /// <param name="VCCID">单位性质id</param>
        /// <param name="urlState">是否报错</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetTitleList(string VCCID, ref int urlState)
        {
            Dictionary<string, string> dicTitle = new Dictionary<string, string>();
            switch (VCCID)
            {
                case "00":
                case "01":
                    dicTitle.Add("0", "董事长/总经理/厂长/副总");
                    dicTitle.Add("1", "总工");
                    dicTitle.Add("2", "市场部");
                    dicTitle.Add("3", "销售部");
                    dicTitle.Add("4", "技术支持");
                    dicTitle.Add("5", "售后维修");
                    dicTitle.Add("6", "生产");
                    dicTitle.Add("7", "研发");
                    dicTitle.Add("8", "人力资源");
                    dicTitle.Add("9", "其他");
                    break;
                case "10":
                case "11":
                case "12":
                    dicTitle.Add("0", "校长/所长/院长/系主任");
                    dicTitle.Add("1", "教师/科研");
                    dicTitle.Add("2", "分析技术人员");
                    dicTitle.Add("3", "仪器采购人员");
                    dicTitle.Add("4", "学生");
                    dicTitle.Add("9", "其他");
                    break;
                case "13":
                case "14":
                case "17":
                case "19":
                    dicTitle.Add("0", "主任/局长/所长/院长/科长");
                    dicTitle.Add("1", "分析技术人员");
                    dicTitle.Add("2", "仪器采购人员");
                    dicTitle.Add("9", "其他");
                    break;
                case "15":
                    dicTitle.Add("0", "总经理/厂长/总工程师");
                    dicTitle.Add("1", "研发");
                    dicTitle.Add("2", "分析技术人员");
                    dicTitle.Add("3", "仪器采购人员");
                    dicTitle.Add("9", "其他");
                    break;
                case "16":
                case "18":
                case "99":
                    dicTitle.Add("0", "负责人");
                    dicTitle.Add("1", "职员");
                    dicTitle.Add("9", "其他");
                    break;
                default:
                    break;
            }
            urlState = 1;
            return dicTitle;

        }

        #endregion

        #region 根据单位性质得到行业性质列表（json格式）
        /// <summary>
        /// 功能描述：根据单位性质得到行业性质列表（json格式）
        /// 创建标识：ljd 20120531
        /// </summary>
        /// <param name="VCCID">单位性质id</param>
        /// <param name="urlState">是否报错</param>
        /// <returns></returns>
        public static string GetFiledListJsonStr(string VCCID)
        {
            StringBuilder strbFieldList = new StringBuilder();

            strbFieldList.Append("{FieldList:[");

            int urlState = 0;

            Dictionary<string, string> dicField = GetFiledList(VCCID, ref urlState);

            if (urlState == 0)
            {
                strbFieldList.Append("{State:'-1'}]}");
                return strbFieldList.ToString();
            }

            if (dicField.Count == 0)
            {
                strbFieldList.Append("{State:'0'}]}");
                return strbFieldList.ToString();
            }
            strbFieldList.Append("{State:'1'},");

            foreach (string key in dicField.Keys)
            {
                strbFieldList.Append("{key:'" + key + "',value:'" + dicField[key] + "'},");
            }
            strbFieldList.Remove(strbFieldList.Length - 1, 1);
            strbFieldList.Append("]}");

            return strbFieldList.ToString();

        }

        #endregion

        #region 根据单位性质得到职位列表（json格式）
        /// <summary>
        /// 功能描述：根据单位性质得到职位列表（json格式）
        /// 创建标识：ljd 20120531
        /// </summary>
        /// <param name="VCCID">单位性质id</param>
        /// <param name="urlState">是否报错</param>
        /// <returns></returns>
        public static string GetTitleListJsonStr(string VCCID)
        {
            StringBuilder strbTitleList = new StringBuilder();

            strbTitleList.Append("{TitleList:[");

            int urlState = 0;

            Dictionary<string, string> dicTitle = GetTitleList(VCCID, ref urlState);

            if (urlState == 0)
            {
                strbTitleList.Append("{State:'-1'}]}");
                return strbTitleList.ToString();
            }

            if (dicTitle.Count == 0)
            {
                strbTitleList.Append("{State:'0'}]}");
                return strbTitleList.ToString();
            }
            strbTitleList.Append("{State:'1'},");

            foreach (string key in dicTitle.Keys)
            {
                strbTitleList.Append("{key:'" + key + "',value:'" + dicTitle[key] + "'},");
            }
            strbTitleList.Remove(strbTitleList.Length - 1, 1);
            strbTitleList.Append("]}");

            return strbTitleList.ToString();

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
            Dictionary<short, string> dicCountry = ILog.DAL.Vip.GetCountryList(ref urlState);
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
            Dictionary<int, string> dicProvince = ILog.DAL.Vip.GetPorvinceList(countryID, ref urlState);
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
            Dictionary<int, string> dicCity = ILog.DAL.Vip.GetCityList(provinceID, ref urlState);

            return dicCity;

        }

        #endregion

        #region 得到省份列表(json格式)

        /// <summary>
        /// 功能描述：得到省份列表(json格式)
        /// 创建标识：ljd 20120601
        /// </summary>
        /// <param name="countryID">国家id</param>
        /// <param name="urlState">是否报错</param>
        /// <returns></returns>
        public static string GetPorvinceListJsonStr(int countryID)
        {
            StringBuilder strbProvinceList = new StringBuilder();

            strbProvinceList.Append("{ProvinceList:[");

            int urlState = 0;

            Dictionary<int, string> dicProvince = GetPorvinceList(countryID, ref urlState);

            if (urlState == 0)
            {
                strbProvinceList.Append("{State:'-1'}]}");
                return strbProvinceList.ToString();
            }

            if (dicProvince.Count == 0)
            {
                strbProvinceList.Append("{State:'0'}]}");
                return strbProvinceList.ToString();
            }
            strbProvinceList.Append("{State:'1'},");

            foreach (int key in dicProvince.Keys)
            {
                strbProvinceList.Append("{key:'" + key + "',value:'" + dicProvince[key] + "'},");
            }
            strbProvinceList.Remove(strbProvinceList.Length - 1, 1);
            strbProvinceList.Append("]}");

            return strbProvinceList.ToString();

        }

        #endregion

        #region 得到城市列表(json格式)

        /// <summary>
        /// 功能描述：得到城市列表(json格式)
        /// 创建标识：ljd 20120601
        /// </summary>
        /// <param name="provinceID">省份id</param>
        /// <param name="urlState">是否报错</param>
        /// <returns></returns>
        public static string GetCityListJsonStr(int provinceID)
        {
            StringBuilder strbCityList = new StringBuilder();

            strbCityList.Append("{CityList:[");

            int urlState = 0;

            Dictionary<int, string> dicCity = GetCityList(provinceID, ref urlState);

            if (urlState == 0)
            {
                strbCityList.Append("{State:'-1'}]}");
                return strbCityList.ToString();
            }

            if (dicCity.Count == 0)
            {
                strbCityList.Append("{State:'0'}]}");
                return strbCityList.ToString();
            }
            strbCityList.Append("{State:'1'},");

            foreach (int key in dicCity.Keys)
            {
                strbCityList.Append("{key:'" + key + "',value:'" + dicCity[key] + "'},");
            }
            strbCityList.Remove(strbCityList.Length - 1, 1);
            strbCityList.Append("]}");

            return strbCityList.ToString();

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
        public static int UpdateVipBaseInfo(ILog.Model.Vip ooVip, ref int urlstate)
        {
            int resultCount = ILog.DAL.Vip.UpdateVipBaseInfo(ooVip, ref urlstate);
            return resultCount;

        }

        #endregion

        #region 得到资料完整度百分比
        /// <summary>
        /// 功能描述：得到资料完整度百分比
        /// 创建标识：ljd 20120603
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static int GetInfoPercent(long userid)
        {
            int completePercent = 0;
            int urlState = 0;
            ILog.Model.Vip ooVip = GetUserInfo(userid, ref urlState);
            if (ooVip != null)
            {
                if (ooVip.name != "")
                {
                    completePercent = 15;
                }
                if (ooVip.mobile != "" && ooVip.mobile_pass == 1)
                {
                    completePercent += 20;
                }
                try
                {
                    Convert.ToDateTime(ooVip.birthday);
                    completePercent += 5;
                }
                catch
                {
                }
                if (ooVip.sex != "")
                {
                    completePercent += 5;
                }
                if (ooVip.company != "")
                {
                    completePercent += 15;
                }
                if (ooVip.address != "")
                {
                    completePercent += 15;
                }
                if (ooVip.postcode != "")
                {
                    completePercent += 5;
                }
                List<ILog.Model.ILogSchool> schoolList = Ilog.BLL.ILogSchool.GetSchoolList(userid, ref urlState);
                if (schoolList.Count > 0)
                {
                    completePercent += 20;
                }

            }
            return completePercent;

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
            int count = ILog.DAL.Vip.CheckNickNameExists(userid, nickname, ref urlState);
            return count;

        }
        #endregion

        #region 判断用户的登录状态与手机认证状态
        /// <summary>
        /// 功能描述：判断用户的登录状态与手机认证状态
        /// 创建标识：ljd 20120604
        /// </summary>
        /// <returns>1 登录并手机认证 2未登录 3未认证手机</returns>
        public static int CheckUserLoginorIlogState()
        {

            int ilogstate = 1;

            #region 正式测试登录
            ////判断用户是否登录
            bool isloginVIP = BLL.VipILog.IsLoginVIP();
            if (!isloginVIP)
            {
                ilogstate = 2;//未登录VIP
                return ilogstate;
            }
            //用户id
            long userid = Ilog.BLL.VipILog.GetVIPUserID();
            #endregion

            //用户id
            //string useridcookie = Com.ILog.Utils.CurrentCookie.GetCookieByKey("useid");
            //long userid = 0;

            //if (!string.IsNullOrEmpty(useridcookie))
            //{
            //    userid = Convert.ToInt64(useridcookie);// Ilog.BLL.VipILog.GetVIPUserID();
            //}

            //if (userid == 0)
            //{
            //    ilogstate = 2;//未登录VIP
            //    return ilogstate;
            //}

            //判断是否认证过手机
            int mobileState = VipILog.CheckMobilePass(userid);

            int urlState = 0;

            if (mobileState != 1)
            {
                #region 20120807 临时测试新增
                string username = Ilog.BLL.VipILog.GetUserNameByUserId(userid);
                if (username == "")
                {
                    ILog.Model.Vip ooVip = Ilog.BLL.Vip.GetUserInfo(userid, ref urlState);
                    Ilog.BLL.VipILog.CreateVipIlogAndInit(ooVip, ref urlState);
                }
                #endregion

                //ilogstate = 3;//未认证手机
                //return ilogstate;
            }
            if (ilogstate == 1)//如果认证了手机，判断是否初始化了vipilog用户，没有则初始化
            {
                string username = Ilog.BLL.VipILog.GetUserNameByUserId(userid);
                if (username == "")
                {
                    ILog.Model.Vip ooVip = Ilog.BLL.Vip.GetUserInfo(userid, ref urlState);
                    Ilog.BLL.VipILog.CreateVipIlogAndInit(ooVip, ref urlState);
                }
            }
            return ilogstate;

        }
        #endregion

        #region 判断用户的登录状态与手机认证状态(json格式)
        /// <summary>
        /// 功能描述：判断用户的登录状态与手机认证状态(json格式)
        /// 创建标识：ljd 20120604
        /// </summary>
        /// <returns>1 登录并手机认证 2未登录 3未认证手机</returns>
        public static string CheckUserLoginorIlogStateJsonStr()
        {
            int checkState = Vip.CheckUserLoginorIlogState();

            StringBuilder strbCheckLogin = new StringBuilder();

            strbCheckLogin.Append("{CheckLogin:[");
            strbCheckLogin.Append("{State:'" + checkState + "'}]}");
            return strbCheckLogin.ToString();

        }
        #endregion

        #region 申请手机认证后添加申请记录
        /// <summary>
        /// 功能描述：申请手机认证后添加申请记录
        /// 创建标识：ljd 20120605
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="urlstate">json是否成功</param>
        /// <returns></returns>
        public static int VIPActiveSleepingAdd(string username, int activeType, ref int urlstate)
        {
            int resultCount = ILog.DAL.Vip.VIPActiveSleepingAdd(username, activeType, ref urlstate);
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
            int resultCount = ILog.DAL.Vip.UpdateVipMobile(userid, mobile, ref urlstate);
            return resultCount;

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
            long userid = ILog.DAL.Vip.GetUserIDByUserName(username, ref urlState);
            return userid;

        }
        #endregion

        #region 得到模拟vip用户列表字符串
        /// <summary>
        /// 功能描述：得到模拟vip用户列表字符串
        /// 创建标识：ljd 20120618
        /// </summary>
        /// <returns></returns>
        public static string GetDemoUserListStr()
        {
            StringBuilder strbHtml = new StringBuilder();

            List<ILog.Model.Vip> vipList = ILog.DAL.Vip.GetDemoUserList();
            foreach (ILog.Model.Vip ooVip in vipList)
            {
                string ilogstate = "";
                string option = "";
                if (ooVip.RID == -1)
                {
                    ilogstate = "未开通";
                    option = "<a href=\"UserTest.aspx?userid=" + ooVip.UserID + "&state=-1\">开通</a>";
                }
                else if (ooVip.RID == 0)
                {
                    ilogstate = "未手机认证";
                    option = "<a href=\"UserTest.aspx?userid=" + ooVip.UserID + "&state=0\">认证</a>";
                }
                else if (ooVip.RID == 1)
                {
                    ilogstate = "已手机认证";
                    option = "<a href=\"/u_" + ooVip.UserID + "\">去TA的主页看看</a>";
                }
                else
                {
                    ilogstate = "禁言";
                    option = "<a href=\"UserTest.aspx?userid=" + ooVip.UserID + "&state=2\">认证</a>";
                }
                strbHtml.AppendFormat("<tr><td><a href=\"UserTest.aspx?userid={3}&to=1\">{0}</td><td>{1}</td><td>{2}</td></tr>", ooVip.username, ilogstate, option, ooVip.UserID);
            }

            return strbHtml.ToString();

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
            int resultCount = ILog.DAL.Vip.ApproveMobile(userid, ip, username);
            return resultCount;

        }
        #endregion

        #region 得到中国大陆省份名称字典
        /// <summary>
        /// 功能描述：得到中国大陆省份名称字典
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetProvinceDic()
        {
            Dictionary<int, string> dicProvince = new Dictionary<int, string>();
            dicProvince.Add(1, "北京");
            dicProvince.Add(2, "上海");
            dicProvince.Add(3, "天津");
            dicProvince.Add(4, "重庆");
            dicProvince.Add(5, "河北");
            dicProvince.Add(6, "山西");
            dicProvince.Add(7, "辽宁");
            dicProvince.Add(8, "吉林");
            dicProvince.Add(9, "黑龙江");
            dicProvince.Add(10, "江苏");
            dicProvince.Add(11, "浙江");
            dicProvince.Add(12, "安徽");
            dicProvince.Add(13, "福建");
            dicProvince.Add(14, "江西");
            dicProvince.Add(15, "山东");
            dicProvince.Add(16, "河南");
            dicProvince.Add(17, "湖北");
            dicProvince.Add(18, "湖南");
            dicProvince.Add(19, "广东");
            dicProvince.Add(20, "海南");
            dicProvince.Add(21, "四川");
            dicProvince.Add(22, "贵州");
            dicProvince.Add(23, "云南");
            dicProvince.Add(24, "陕西");
            dicProvince.Add(25, "甘肃");
            dicProvince.Add(26, "青海");
            dicProvince.Add(27, "内蒙古");
            dicProvince.Add(28, "广西");
            dicProvince.Add(29, "西藏");
            dicProvince.Add(30, "宁夏");
            dicProvince.Add(31, "新疆");
            return dicProvince;

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
            string mobile = ILog.DAL.Vip.GetCheckedMobile(userid);
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
            int hitcount = ILog.DAL.Vip.GetLastWeekAllHitCount(username);
            return hitcount;

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
            int totalonline = ILog.DAL.Vip.GetYesterdayTotalOnLine(username);
            return totalonline;

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
            int acceptReward = ILog.DAL.Vip.GetLastDayReward(username);
            return acceptReward;

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
            int acceptReward = ILog.DAL.Vip.GetLastWeekReward(username);
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
            int todayHit = ILog.DAL.Vip.GetTodayHit(username);
            return todayHit;

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
            int resultCount = ILog.DAL.Vip.InsertDaka(username, score);
            return resultCount;

        }
        #endregion

        //#region 根据用户的打卡情况返回用户的签到状态
        ///// <summary>
        ///// 功能描述：根据用户的打卡情况返回用户的签到状态
        ///// 创建标识：ljd 20120813
        ///// </summary>
        ///// <param name="username">用户名</param>
        ///// <returns>得到签到状态</returns>
        //public static string GetSignInStatus(string username)
        //{
        //    StringBuilder strbHtml = new StringBuilder();

        //    string weekstr = "";

        //    //判断前一周是否全打卡
        //    int weekcount = GetLastWeekAllHitCount(username);

        //    if (weekcount >= 7)
        //    {
        //        //判断是否已经领取奖励
        //        int lingqcount = GetLastWeekReward(username);
        //        if (lingqcount == 0)
        //        {
        //            weekstr = "<div class=\"daka\" id=\"qq\">领取全勤奖<br>上周</div>";
        //        }
        //    }

        //    //读取用户的前一天的在线时长
        //    int TotalOnline = GetYesterdayTotalOnLine(username);

        //    string Rewardstr = "";

        //    if (TotalOnline >= 30)
        //    {
        //        //统计当天是否领取上一天的时长奖励
        //        int RewardNum = GetLastDayReward(username);

        //        if (RewardNum == 0)
        //        {
        //            Rewardstr = "<div class=\"daka\" id=\"lq\">领取时长奖<br>昨日：" + TotalOnline + "min</div>";
        //        }
        //        else
        //        {
        //            Rewardstr = "<div class=\"daka1\" >昨日时长<br>" + TotalOnline + "min</div>";
        //        }
        //    }
        //    string Dakastr = "";
        //    //判断当天是否已经打卡
        //    int todaySignIn = GetTodayHit(username);

        //    if (todaySignIn == 0)
        //    {
        //        Dakastr = "<div class=\"daka1\">已打卡<br>" + Utils.GetDate() + "</div>  ";
        //    }
        //    else
        //    {
        //        Dakastr = "<div class=\"daka\" id=\"dk\">打卡签到<br>" + Utils.GetDate() + "</div>  ";
        //    }

        //    strbHtml.Append("{daka:\"" + Common.GetJScriptGlobalObjectEscape(Dakastr + Rewardstr + weekstr) + "\"}");

        //    return strbHtml.ToString();

        //}
        //#endregion

        #region 根据用户的打卡情况返回用户的签到状态
        /// <summary>
        /// 功能描述：根据用户的打卡情况返回用户的签到状态
        /// 创建标识：ljd 20120813
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>得到签到状态 0未签到 1已签到</returns>
        public static string GetSignInStatus(string username)
        {
            StringBuilder strbHtml = new StringBuilder();

            //判断当天是否已经打卡
            int todaySignIn = GetTodayHit(username);

            strbHtml.Append("{daka:" + todaySignIn + "}");

            return strbHtml.ToString();

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
        public static int InsertAddScoreBatch_U(ILog.Model.VipScore score)
        {
            int resultCount = ILog.DAL.Vip.InsertAddScoreBatch_U(score);
            return resultCount;

        }
        #endregion

        #region 根据用户id和类型得到打卡数量

        /// <summary>
        /// 功能描述：根据用户id和类型得到打卡数量
        /// 创建标识：ljd 20120913
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="type">打卡类别</param>
        /// <returns>打卡状态</returns>
        public static string VipSignIn(long userid, int type)
        {
            string signinState = "";

            string username = VipILog.GetUserNameByUserId(userid);

            //type 1 打开签到  2 领取周全勤  3 领取时长

            if (type <= 0 || type > 3)
            {
                signinState = "打卡机故障";
                return signinState;
            }

            if (userid == 0)
            {
                signinState = "打卡机故障";
                return signinState;
            }

            int urlstate = 0;
            ILog.Model.Vip ooVip = Ilog.BLL.Vip.GetUserInfo(userid, ref urlstate);
            if (ooVip == null)
            {
                signinState = "打卡机故障";
                return signinState;
            }

            if (type == 1)//打开签到
            {
                //判断当天是否已经打卡
                int todayhit = GetTodayHit(username);

                if (todayhit == 1)
                {
                    signinState = "已打卡<br>" + Utils.GetDateTime();
                    return signinState;
                }

                //判断前一天是否已经打卡
                int lastdayHit = 0;

                int score = 0;

                TimeSpan regSpan = DateTime.Now - ooVip.regDate;


                if (ooVip.knowledge < 50 && regSpan.TotalDays < 50)
                {
                    if (ooVip.MemberLevel == "C")
                    {
                        score = 2;
                    }
                    else
                    {
                        score = 1;
                    }
                }
                else
                {
                    score = 1;
                }

                if (lastdayHit > 0)
                {
                    score = score * 2;
                }

                //写入打开记录
                InsertDaka(username, score);

                //获取IP地址
                string IP = Utils.GetRealIP();

                ILog.Model.VipScore ooScore = new ILog.Model.VipScore();
                //把积分写进日志库

                ooScore.username = username;
                ooScore.score = score;
                ooScore.knowledge = 0;
                ooScore.fame = 0;
                ooScore.OnlineTime = 0;
                ooScore.ActionType = 38;
                ooScore.ActionTime = DateTime.Now;
                ooScore.id = 0;
                ooScore.ip = IP;
                ooScore.referer = "打卡签到奖励";
                ooScore.gold = 0;

                Ilog.BLL.Vip.InsertAddScoreBatch_U(ooScore);
                signinState = "已打卡<br>" + Utils.GetDateTime();
                return signinState;
            }

            else if (type == 2)//领取周全勤奖
            {


                //判断前一周是否全打卡
                int weekcount = GetLastWeekAllHitCount(username);

                if (weekcount >= 7)
                {
                    //判断是否已经领取全勤奖励
                    int lingqcount = GetLastWeekReward(username);

                    if (lingqcount > 0)
                    {
                        signinState = "已领全勤奖<br>上周";
                        return signinState;
                    }
                    else
                    {
                        //领取周全勤奖
                        int weekRewardCount = ILog.DAL.Vip.VIPAcceptWeekReward(username);

                        //写入周全勤奖的勋章
                        int Insigniacount = ILog.DAL.Vip.VIPExistWeekRewardMedal(username);

                        if (Insigniacount > 0)
                        {
                            ILog.DAL.Vip.UpdateInsigniaTime(username, 64);
                        }
                        else
                        {
                            ILog.DAL.Vip.AddInsignia(username, 64);
                        }

                        //获取IP地址
                        string IP = Utils.GetRealIP();

                        //把积分写进日志库  10积分、7声望
                        ILog.Model.VipScore ooScore = new ILog.Model.VipScore();

                        ooScore.username = username;
                        ooScore.score = 10;
                        ooScore.knowledge = 0;
                        ooScore.fame = 7;
                        ooScore.OnlineTime = 0;
                        ooScore.ActionType = 37;
                        ooScore.ActionTime = DateTime.Now;
                        ooScore.id = 0;
                        ooScore.ip = IP;
                        ooScore.referer = "领取上周全勤奖励";
                        ooScore.gold = 0;

                        BLL.Vip.InsertAddScoreBatch_U(ooScore);

                        signinState = "已领全勤奖<br>上周";
                        return signinState;
                    }
                }

                else
                {
                    signinState = "未全勤<br>上周";
                    return signinState;
                }

            }
            else if (type == 3)
            {
                /*
                 *30min<N<60min   1积分 1经验
                 *60min≤N<120min  2积分 2经验
                 *120min≤N  4积分 4经验 1声望
                */
                //统计当天是否领取上一天的时长奖励
                int RewardNum = GetLastDayReward(username);

                //读取用户的前一天的在线时长

                int TotalOnline = GetYesterdayTotalOnLine(username);

                if (RewardNum == 1)
                {
                    signinState = "已领时长奖<br>昨日" + TotalOnline + "min";
                    return signinState;
                }
                else
                {
                    int LOG_Score = 0;
                    int LOG_Knowledge = 0;
                    int LOG_Fame = 0;

                    if (TotalOnline < 30)
                    {
                        signinState = "时长不够<br>30min";
                        return signinState;
                    }

                    else if (TotalOnline >= 30 && TotalOnline < 60)
                    {
                        LOG_Score = 1;
                        LOG_Knowledge = 1;
                        LOG_Fame = 0;
                    }
                    else if (TotalOnline >= 60 && TotalOnline < 120)
                    {
                        LOG_Score = 2;
                        LOG_Knowledge = 2;
                        LOG_Fame = 0;
                    }
                    else if (TotalOnline >= 120)
                    {
                        LOG_Score = 4;
                        LOG_Knowledge = 4;
                        LOG_Fame = 1;
                    }
                    else
                    {
                        signinState = "机器故障";
                        return signinState;
                    }


                    //获取IP地址
                    string IP = Utils.GetRealIP();


                    //把积分写进日志库  10积分、7声望
                    ILog.Model.VipScore ooScore = new ILog.Model.VipScore();

                    ooScore.username = username;
                    ooScore.score = LOG_Score;
                    ooScore.knowledge = LOG_Knowledge;
                    ooScore.fame = LOG_Fame;
                    ooScore.OnlineTime = TotalOnline;
                    ooScore.ActionType = 36;
                    ooScore.ActionTime = DateTime.Now;
                    ooScore.id = 0;
                    ooScore.ip = IP;
                    ooScore.referer = "领取上一天的在线时长奖励";
                    ooScore.gold = 0;
                    //把积分写进日志库
                    BLL.Vip.InsertAddScoreBatch_U(ooScore);

                    signinState = "已领时长奖<br>昨日" + TotalOnline + "min";
                    return signinState;
                }
            }

            else
            {
                signinState = "机器故障";
                return signinState;
            }

        }

        #endregion

        #region 验证用户登录
        /// <summary>
        /// 功能描述：验证用户登录
        /// 创建标识：ljd 20120912
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="code">激活码</param>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="returnUrl">跳转地址</param>
        /// <returns>0 登录失败 1登录成功 2黑名单 3用户不存在 4被封全站 5密码错误 6未激活账号 7未更新资料 8未完善行业信息 9未激活</returns>
        public static int CheckLogin(string username, string password, string code, ref string errorMsg, ref string returnUrl)
        {
            //验证状态
            int state = 0;

            //md5密码
            string md5password = Utils.MD5(Utils.MD5(password + "@#hqewiukew$^gsy39843jdf") + "sakjfl@qircxwe4opgsy3");

            //登录类型
            string logintype = "";

            Regex regexMobile = new Regex(@"^1[358]\d{9}$");

            Regex regexEmail = new Regex(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$");

            if (regexMobile.IsMatch(username))
            {
                logintype = "mobile";
            }
            else if (regexEmail.IsMatch(username))
            {
                logintype = "email";
            }
            else
            {
                logintype = "username";
            }

            long sleepingUserid = ILog.DAL.Vip.GetSleepingVipUserID(username, logintype);

            if (sleepingUserid > 0)
            {
                //将沉默用户拉入vip
                int isFaile = ILog.DAL.Vip.ActiveVip(username, logintype);
            }

            long blackUserid = ILog.DAL.Vip.GetBlackListUserID(username, logintype);

            //ip地址
            string ip = Utils.GetRealIP();

            //i3的cookie值
            string cookieid = "0";

            //浏览器类型
            string agent = HttpContext.Current.Request.ServerVariables["HTTP_User_AGENT"];

            ILog.Model.VIPLoginErr ooError = new ILog.Model.VIPLoginErr();

            ooError.Agent = agent;
            ooError.Ip = ip;
            ooError.Username = username;

            if (HttpContext.Current.Request.Cookies["3i"] == null)
            {
                Random random = new Random(1);
                int randnum = random.Next(100);

                string cookie3i = DateTime.Now.ToString("yyyyMMddHHmmss") + randnum.ToString("##");

                HttpContext.Current.Response.Cookies["3i"].Value = cookie3i;
                HttpContext.Current.Response.Cookies["3i"].Expires = DateTime.Now.AddYears(1);
            }
            else
            {
                cookieid = HttpContext.Current.Request.Cookies["3i"].Value;
            }

            ooError.CookieID = cookieid;

            //是否进入黑名单
            if (blackUserid > 0)
            {
                errorMsg = "您的账号因非法操作被列入黑名单，请联系管理咨询解禁方法！电话010-51299927-112";
                ooError.Erint = 1;
                ILog.DAL.Vip.VIPLoginErrAdd(ooError);
                state = 2;
                return state;
            }

            //读取用户信息
            ILog.Model.Vip ooVip = ILog.DAL.Vip.GetUserInfoByUserNameAndType(username, logintype);

            if (ooVip != null && ooVip.username != "")
            {
                ILog.Model.UBBForbidPost ooForbid = ILog.DAL.Vip.GetForbidUser(username);
                if (ooForbid != null)//被封全站
                {
                    errorMsg = string.Format("您于 {0} 被 {1} 封掉了在 全站 的发帖权限，系统将于 {2} 自动给您解封，您也可以到论坛的投诉建议版对 {1} 进行投诉！",
                        ooForbid.InTime, ooForbid.Admin, ooForbid.OutDate);
                    ooError.Erint = 3;
                    ILog.DAL.Vip.VIPLoginErrAdd(ooError);
                    state = 4;
                    return state;
                }

                if (ooVip.MD5Password != md5password)
                {
                    errorMsg = "密码错误！";
                    ooError.Erint = 4;
                    ILog.DAL.Vip.VIPLoginErrAdd(ooError);
                    state = 5;
                    return state;
                }

                if (!ooVip.isPassed)//未激活账号
                {
                    errorMsg = "仪器信息网为防止大量注册临时帐号和清理无效帐号，现推出帐号激活机制，并且定期删除长时间未激活的VIP帐号！您的VIP帐号还没有激活，请点击确认后根据提示激活您的帐号！";
                    returnUrl = "http://www.instrument.com.cn/vip/confirm_index.asp";
                    ooError.Erint = 5;
                    ILog.DAL.Vip.VIPLoginErrAdd(ooError);
                    state = 6;
                    return state;
                }

                if (ooVip.MemberLevel == "U")//未更新资料
                {
                    errorMsg = "您的资料尚未更新，请更新个人信息！";
                    returnUrl = string.Format("http://www.instrument.com.cn/vip/vipupdate_form.asp?UserName={0}&UID={1}", Utils.UrlEncode(username), ooVip.MD5Code);
                    ooError.Erint = 6;
                    ILog.DAL.Vip.VIPLoginErrAdd(ooError);
                    state = 7;
                    return state;
                }

                if (!ooVip.hasFillInField)//未完善行业信息
                {
                    errorMsg = "您的资料尚未更新，请更新个人信息！";
                    returnUrl = string.Format("http://www.instrument.com.cn/vip/vipupdate_form.asp?UserName={0}&UID={1}", Utils.UrlEncode(username), ooVip.MD5Code);
                    ooError.Erint = 7;
                    ILog.DAL.Vip.VIPLoginErrAdd(ooError);
                    state = 8;
                    return state;
                }

                //判断是否是专家、版主

                //专家id
                int expertid = ILog.DAL.Vip.GetExpertID(username);

                //版主id
                int masterid = ILog.DAL.Vip.GetMasterID(username);

                if (expertid == 0 && masterid == 0)//既不是版主也不是专家
                {
                    //判断是否是激活用户
                    long activeid = ILog.DAL.ILogInvitecode.GetActiveID(ooVip.UserID);

                    if (activeid == 0)
                    {
                        if (string.IsNullOrEmpty(code))
                        {
                            errorMsg = "ilog现在正在内测中，您还没有内测权限！";
                            returnUrl = "http://ig.instrument.com.cn/";
                            state = 9;
                            return state;
                        }
                        ILog.Model.ILogInvitecode ooCode = ILog.DAL.ILogInvitecode.GetInviteEntity(code);
                        if (ooCode == null)
                        {
                            errorMsg = "ilog现在正在内测中，您还没有内测权限！";
                            returnUrl = "http://ig.instrument.com.cn/";
                            state = 9;
                            return state;
                        }
                        if (ooCode.Userid == 0)//未激活
                        {
                            ooCode.Userid = ooVip.UserID;
                            ILog.DAL.ILogInvitecode.InviteCodeUpdate(ooCode);
                        }
                    }

                }

                //给该用户增加5个邀请码名额
                List<ILog.Model.ILogInvitecode> codeList = ILog.DAL.ILogInvitecode.GetInviteListByUserID(ooVip.UserID);
                if (codeList.Count == 0)
                {
                    ILogInvitecode.InviteCodeAdd(ooVip.UserID);
                }

                CurrentCookie.SetCookie("username", ooVip.username);
                CurrentCookie.SetCookie("useid", ooVip.UserID.ToString());
                CurrentCookie.SetCookie("CheckValid", Utils.MD5(ooVip.username.ToLower() + "-instrument_4077_20091124"));

                state = 1;
            }
            else//此账号不存在
            {
                errorMsg = "此账号不存在！";
                ooError.Erint = 2;
                ILog.DAL.Vip.VIPLoginErrAdd(ooError);
                state = 3;
                return state;
            }

            return state;

        }
        #endregion
    }


}
