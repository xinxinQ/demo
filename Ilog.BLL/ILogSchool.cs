using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ilog.BLL
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
        public static List<ILog.Model.ILogSchool> GetSchoolList(long userid, ref int urlState)
        {
            List<ILog.Model.ILogSchool> schoolList = ILog.DAL.ILogSchool.GetSchoolList(userid, ref urlState);
            return schoolList;

        }
        #endregion

        #region 得到学校列表字符串
        /// <summary>
        /// 功能描述：得到学校列表字符串
        /// 创建标识：ljd 20120530
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="urlState"></param>
        /// <returns></returns>
        public static string GetSchoolListStr(long userid, ref int urlState)
        {
            List<ILog.Model.ILogSchool> SchoolList = GetSchoolList(userid, ref urlState);

            StringBuilder strbSchoolList = new StringBuilder();

            if (SchoolList.Count > 0)
            {
                foreach (ILog.Model.ILogSchool ooSchool in SchoolList)
                {
                    strbSchoolList.Append("<tr>");
                    strbSchoolList.AppendFormat("<td height=\"37\" class=\"F14\">{0}</td>", ooSchool.is_degreeName);
                    strbSchoolList.AppendFormat("<td  class=\"F14\">{0}</td>", ooSchool.is_school);
                    strbSchoolList.AppendFormat("<td  class=\"F14 Fa\">{0}年</td>", ooSchool.is_entranceYear);
                    strbSchoolList.AppendFormat("<td  class=\"F14\"><a href=\"javascript:ChangeSchool({0})\">[修改]</a> <a href=\"javascript:delSchool({0});\">[删除]</a></td>", ooSchool.is_id);
                    strbSchoolList.Append("</tr>");
                }
            }

            return strbSchoolList.ToString();

        }
        #endregion

        #region 得到学校列表字符串(json格式)
        /// <summary>
        /// 功能描述：得到学校列表字符串(json格式)
        /// 创建标识：ljd 20120603
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="urlState"></param>
        /// <returns></returns>
        public static string GetSchoolListJsonStr(long userid)
        {
            int urlState = 0;

            List<ILog.Model.ILogSchool> SchoolList = GetSchoolList(userid, ref urlState);

            StringBuilder strbSchoolList = new StringBuilder();

            strbSchoolList.Append("{SchoolList:[");

            if (urlState == 0)
            {
                strbSchoolList.Append("{State:'-1'}]}");
            }
            else
            {
                if (SchoolList.Count > 0)
                {
                    strbSchoolList.Append("{State:'1'},");

                    foreach (ILog.Model.ILogSchool ooSchool in SchoolList)
                    {
                        strbSchoolList.Append("{id:'" + ooSchool.is_id + "',schoolname:'" + ooSchool.is_school + "',degreename:'" + ooSchool.is_degreeName + "',inyear:'" + ooSchool.is_entranceYear + "'},");
                    }
                    strbSchoolList.Remove(strbSchoolList.Length - 1, 1);
                    strbSchoolList.Append("]}");
                }
                else
                {
                    strbSchoolList.Append("{State:'0'}]}");
                }
            }
            return strbSchoolList.ToString();

        }
        #endregion


        #region 得到学历下拉列表字符串
        /// <summary>
        /// 功能描述：得到学历下拉列表字符串
        /// 创建标识：ljd 20120601
        /// </summary>
        /// <param name="is_degree">学历id</param>
        /// <returns></returns>
        public static string GetDegreeListStr(int is_degree)
        {
            Dictionary<int, string> dicDegree = GetDegreeList();

            StringBuilder strbDegree = new StringBuilder();

            foreach (int key in dicDegree.Keys)
            {
                string selected = "";

                if (is_degree == key)
                {
                    selected = "selected=\"selected\"";
                }
                strbDegree.AppendFormat("<option value=\"{0}\" {2}>{1}</option>", key, dicDegree[key], selected);
            }

            return strbDegree.ToString();

        }
        #endregion

        #region 得到学历列表
        /// <summary>
        /// 功能描述：得到学历列表
        /// 创建标识；ljd 20120601
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetDegreeList()
        {
            Dictionary<int, string> dicDegree = new Dictionary<int, string>();
            dicDegree.Add(1, "大学");
            dicDegree.Add(2, "高中");
            dicDegree.Add(3, "中专技校");
            dicDegree.Add(4, "初中");
            dicDegree.Add(5, "小学");
            return dicDegree;

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
        public static ILog.Model.ILogSchool GetSchoolInfo(long is_id, ref int urlState)
        {
            ILog.Model.ILogSchool ooSchool = ILog.DAL.ILogSchool.GetSchoolInfo(is_id, ref urlState);
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
        public static int UpdateSchool(ILog.Model.ILogSchool ooSchool, ref int urlstate)
        {
            int resultCount = ILog.DAL.ILogSchool.UpdateSchool(ooSchool, ref urlstate);
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
        public static int AddSchool(ILog.Model.ILogSchool ooSchool, ref int urlstate)
        {
            int resultCount = ILog.DAL.ILogSchool.AddSchool(ooSchool, ref urlstate);
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
            int resultCount = ILog.DAL.ILogSchool.DeleteSchool(is_id, ref urlstate);
            return resultCount;

        }
        #endregion

        #region webservice方式删除学校信息
        /// <summary>
        /// 功能描述：webservice方式删除学校信息
        /// 创建标识：ljd 20120602
        /// </summary>
        /// <param name="is_id">学校id</param>
        /// <param name="urlstate">是否报错</param>
        /// <returns></returns>
        public static string DeleteSchoolByWebservice(long is_id)
        {
            int urlState = 0;
            int resultCount = ILog.DAL.ILogSchool.DeleteSchool(is_id, ref urlState);

            StringBuilder strbSchool = new StringBuilder();

            strbSchool.Append("{DeleteSchool:[");

            if (urlState == 0)
            {
                strbSchool.Append("{State:'-1'}]}");
                return strbSchool.ToString();
            }
            if (resultCount == 1)
            {
                strbSchool.Append("{State:'1'}]}");
            }
            else
            {
                strbSchool.Append("{State:'0'}]}");
            }
            return strbSchool.ToString();

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
            Dictionary<int, string> colledgeList = ILog.DAL.ILogSchool.GetSchoolListByProvID(provid);
            return colledgeList;

        }
        #endregion


        #region 根据省份id得到学校列表字符串(json格式)
        /// <summary>
        /// 功能描述：根据省份id得到学校列表字符串(json格式)
        /// 创建标识：ljd 20120625
        /// </summary>
        /// <param name="provid">省份id</param>
        /// <returns></returns>
        public static string GetSchoolListJsonStrByProvID(int provid)
        {
            int state = 0;

            Dictionary<int, string> colledgeList = new Dictionary<int, string>();

            try
            {
                colledgeList = GetSchoolListByProvID(provid);
                state = 1;
            }
            catch
            {
                state = -1;
            }

            StringBuilder strbSchoolList = new StringBuilder();

            strbSchoolList.Append("{SchoolList:[");

            if (state == -1)
            {
                strbSchoolList.Append("{State:'-1'}]}");
            }
            else
            {
                if (colledgeList.Count > 0)
                {
                    strbSchoolList.Append("{State:'1'},");

                    foreach (int key in colledgeList.Keys)
                    {
                        strbSchoolList.Append("{id:'" + key + "',schoolname:'" + colledgeList[key] + "'},");
                    }
                    strbSchoolList.Remove(strbSchoolList.Length - 1, 1);
                    strbSchoolList.Append("]}");
                }
                else
                {
                    strbSchoolList.Append("{State:'0'}]}");
                }
            }
            return strbSchoolList.ToString();

        }
        #endregion

    }

}
