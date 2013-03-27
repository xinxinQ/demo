using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ilog.BLL
{
    public class ILogUserFan
    {
        #region 查看粉丝是否存在（True：存在，False：不存在）

        /// <summary>
        /// 查看粉丝是否存在
        /// </summary>
        public static bool isExists(long userid, long concernuserid)
        {
            return ILog.DAL.ILogUserFan.isExists(userid, concernuserid);
        }
        /// <summary>
        /// 查看粉丝是否存在（True：存在，False：不存在）
        /// <param name="iuc_id">流水号</param>
        /// </summary>
        public static string UserFanExists(int iuc_id)
        {
            StringBuilder strUserFanExists = new StringBuilder();

            strUserFanExists.Append("var strUserFanExistsJsonObject = ");
            strUserFanExists.Append("({");
            strUserFanExists.Append("\"Exists\": \"" + ILog.DAL.ILogUserFan.UserFanExists(iuc_id).ToString() + "\"");
            strUserFanExists.Append("})");

            return strUserFanExists.ToString();
        }

        /// <summary>
        /// 判断用户是否是我的粉丝
        /// </summary>
        /// <param name="userID">我</param>
        /// <param name="ConcernUserid">用户</param>
        /// <returns></returns>
        public static bool IsExists_UserID(long userID, long ConcernUserid)
        {
            return ILog.DAL.ILogUserFan.IsExists_UserID(userID, ConcernUserid);
        }

        #endregion

        #region  添加粉丝
        /// <summary>
        /// 添加粉丝
        /// <param name="model">粉丝实体</param>
        /// </summary>
        public static string UserFanAdd(ILog.Model.ILogUserFan model)
        {
            StringBuilder strUserFanAdd = new StringBuilder();

            strUserFanAdd.Append("var strUserFanAddJsonObject = ");
            strUserFanAdd.Append("({");
            strUserFanAdd.Append("\"state\": \"" + ILog.DAL.ILogUserFan.UserFanAdd(model).ToString() + "\"");
            strUserFanAdd.Append("})");

            return strUserFanAdd.ToString();
        }

        // <summary>
        /// 添加粉丝
        /// <param name="model">粉丝实体</param>
        /// </summary>
        public static int Add(ILog.Model.ILogUserFan model)
        {
            return ILog.DAL.ILogUserFan.UserFanAdd(model);
        }
        #endregion

        #region 更新粉丝
        /// <summary>
        /// 更新粉丝
        /// <param name="model">粉丝实体</param>
        /// </summary>
        public static string UserFanUpdate(ILog.Model.ILogUserFan model)
        {
            StringBuilder strUserFanUpdate = new StringBuilder();

            strUserFanUpdate.Append("var strUserFanUpdateJsonObject = ");
            strUserFanUpdate.Append("({");
            strUserFanUpdate.Append("\"state\": \"" + ILog.DAL.ILogUserFan.UserFanUpdate(model).ToString() + "\"");
            strUserFanUpdate.Append("})");

            return strUserFanUpdate.ToString();
        }

        /// <summary>
        /// 更新粉丝表中关注我的用户粉丝数
        /// </summary>
        /// <param name="concernuserid">用户ID</param>
        /// <returns>返回一个值</returns>
        public static int Update_FanNum(long concernuserid)
        {
            return ILog.DAL.ILogUserFan.Update_FanNum(concernuserid);
        }
        #endregion

        #region 删除一条粉丝
        /// <summary>
        /// 删除一条粉丝
        /// <param name="iuf_id">流水号</param>
        /// </summary>
        public static string UserFanDel(int iuf_id)
        {
            StringBuilder strUserFanDel = new StringBuilder();

            strUserFanDel.Append("var strUserConcernDelJsonObject = ");
            strUserFanDel.Append("({");
            strUserFanDel.Append("\"state\": \"" + ILog.DAL.ILogUserFan.UserFanDel(iuf_id).ToString() + "\"");
            strUserFanDel.Append("})");

            return strUserFanDel.ToString();
        }

        /// <summary>
        /// 删除一条粉丝
        /// <param name="model">粉丝实体</param>
        /// </summary>
        public static int Delete_iufID(long userid, long concernUserID)
        {
            return ILog.DAL.ILogUserFan.Delete_iufID(userid, concernUserID);
        }

        /// <summary>
        /// 删除一条粉丝
        /// <param name="model"></param>
        /// </summary>
        public static int Delete_Userid(long userid, long concernuserid)
        {
            return ILog.DAL.ILogUserFan.Delete_Userid(userid, concernuserid);
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="iuc_id">流水号</param>
        /// </summary>
        public static string GetModel(int iuc_id)
        {
            DataTable dblUserFanModelList = ILog.DAL.ILogUserConcern.GetModel(iuc_id);

            //构建josn字符串 
            string strUserFanJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblUserFanModelList).ToString();

            return strUserFanJosn;
        }
        #endregion

        #region 分页（json节点见表字段）
        /// <summary>
        /// 数据分页
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
        public static string GetUserFanPageList(string tbname, string FieldKey, int PageCurrent, int PageSize, string FieldShow, string FieldOrder, string Where, ref int RecordCount)
        {
            string strTableName = " ilog_userfan ";
            string strFieldKey = "iuf_id";
            string strFieldShow = " iuf_id,userid,concernuserid,intime ";
            string strFieldOrder = " iuf_id desc ";
            string strWhere = " ";

            DataTable dblUserFanPageList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            //构建josn字符串 
            string strUserFanPageJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblUserFanPageList).ToString();

            return strUserFanPageJosn;
        }
        #endregion

        #region 根据用户id得到用户所有的粉丝的userid
        /// <summary>
        /// 功能描述：根据用户id得到用户所有的粉丝的userid
        /// 创建标识：ljd 20120610
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="urlState"></param>
        /// <returns></returns>
        public static List<long> GetUserFanListByUserid(long userid, ref int urlState)
        {
            List<long> fansList = ILog.DAL.ILogUserFan.GetUserFanListByUserid(userid, ref urlState);
            return fansList;

        }
        #endregion

        #region 根据用户id得到用户的前9个粉丝
        /// <summary>
        /// 功能描述：根据用户id得到用户的前9个粉丝
        /// 创建标识：ljd 20120626
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public static List<ILog.Model.ILogUserFan> GetUserFanTopNineListByUserid(long userid)
        {
            List<ILog.Model.ILogUserFan> fansList = ILog.DAL.ILogUserFan.GetUserFanTopNineListByUserid(userid);
            return fansList;

        }
        #endregion

        #region 得到他人主页用户的前九个粉丝（json格式）
        /// <summary>
        /// 功能描述：得到他人主页用户的前九个粉丝（json格式）
        /// 创建标识：ljd 20120626
        /// </summary>
        /// <param name="userid">TA的用户id</param>
        /// <returns></returns>
        public static string GetFansListStrWithTa(long userid)
        {
            List<ILog.Model.ILogUserFan> fansList = new List<ILog.Model.ILogUserFan>();

            StringBuilder strbFansList = new StringBuilder();

            strbFansList.Append("{FansList:[");

            try
            {
                fansList = GetUserFanTopNineListByUserid(userid);

                if (fansList.Count == 0)
                {
                    strbFansList.Append("{State:'0'}]}");//无数据
                }
                else
                {
                    strbFansList.Append("{State:'1'},");

                    foreach (ILog.Model.ILogUserFan ooFan in fansList)
                    {
                        ILog.Model.VipILog ooIlog = BLL.VipILog.GetModelByUserID(ooFan.concernuserid);
                        if (ooIlog != null)
                        {
                            strbFansList.Append("{nickname:'" + ooIlog.nickname + "',userid:'" + ooIlog.userid + "',date:'" + BLL.ILogVisithistory.GetVisitTimeShow(ooFan.intime)
                          + "',face:'/images/face/small/" + ooIlog.face + "'},");
                        }
                    }
                    strbFansList.Remove(strbFansList.Length - 1, 1);
                    strbFansList.Append("]}");
                }

            }
            catch
            {
                strbFansList.Append("{State:'-1'}]}");//报错
            }

            return strbFansList.ToString();

        }
        #endregion




    }
}
