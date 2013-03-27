using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ilog.BLL
{
    public class ILogVisithistory
    {
        #region 添加访问记录
        /// <summary>
        /// 功能描述：添加访问记录
        /// 创建标识：ljd 20120625
        /// </summary>
        /// <param name="ooSchool">访问记录实体对象</param>
        /// <returns></returns>
        public static int AddVisitHistory(ILog.Model.ILogVisithistory ooVisitHistory)
        {
            int resultCount = ILog.DAL.ILogVisithistory.AddVisitHistory(ooVisitHistory);
            return resultCount;

        }
        #endregion


        #region 得到当前用户访问过的前9个用户（我看过谁）
        /// <summary>
        /// 功能描述：得到当前用户访问过的前9个用户（我看过谁）
        /// 创建标识：ljd 20120626
        /// </summary>
        /// <param name="userid">当前用户id</param>
        /// <returns></returns>
        public static List<ILog.Model.ILogVisithistory> GetVisitList(long userid)
        {
            List<ILog.Model.ILogVisithistory> historyList = ILog.DAL.ILogVisithistory.GetVisitList(userid);
            return historyList;

        }
        #endregion


        #region 得到访问过当前用户的前9个用户（谁看过我）
        /// <summary>
        /// 功能描述：得到访问过当前用户的前9个用户（谁看过我）
        /// 创建标识：ljd 20120626
        /// </summary>
        /// <param name="userid">当前用户id</param>
        /// <returns></returns>
        public static List<ILog.Model.ILogVisithistory> GetVisitedList(long userid)
        {
            List<ILog.Model.ILogVisithistory> historyList = ILog.DAL.ILogVisithistory.GetVisitedList(userid);
            return historyList;

        }
        #endregion

        #region 得到当前用户访问过的前9个用户（我看过谁）（json格式）
        /// <summary>
        /// 功能描述：得到当前用户访问过的前9个用户（我看过谁）（json格式）
        /// 创建标识：ljd 20120625
        /// </summary>
        /// <param name="userid">当前用户id</param>
        /// <returns></returns>
        public static string GetVisitListStr(long userid)
        {
            List<ILog.Model.ILogVisithistory> historyList = new List<ILog.Model.ILogVisithistory>(); 

            StringBuilder strbHistoryList = new StringBuilder();

            strbHistoryList.Append("{HistoryList:[");

            List<ILog.Model.ILogVisithistory> ilogList = new List<ILog.Model.ILogVisithistory>();
            try
            {
                historyList = GetVisitList(userid);

                if (historyList.Count == 0)
                {
                    strbHistoryList.Append("{State:'0'}]}");//无数据
                }
                else
                {
                    strbHistoryList.Append("{State:'1'},");

                    foreach (ILog.Model.ILogVisithistory ooHistory in historyList)
                    {
                        ILog.Model.VipILog ooIlog = BLL.VipILog.GetModelByUserID(ooHistory.iv_userid);

                        strbHistoryList.Append("{nickname:'" + ooIlog.nickname + "',visiteduserid:'" + ooHistory.iv_userid + "',id:'" + ooHistory.iv_id
                            + "',date:'" + GetVisitTimeShow(ooHistory.intime) + "',face:'/images/face/small/" + ooIlog.face + "'},");
                    }
                    strbHistoryList.Remove(strbHistoryList.Length - 1, 1);
                    strbHistoryList.Append("]}");
                }

            }
            catch
            {
                strbHistoryList.Append("{State:'-1'}]}");//报错
            }

            return strbHistoryList.ToString();

        }
        #endregion

        #region 得到当前用户访问过的前9个用户（我看过谁）（json格式）
        /// <summary>
        /// 功能描述：得到当前用户访问过的前9个用户（我看过谁）（json格式）
        /// 创建标识：ljd 20120625
        /// </summary>
        /// <param name="userid">当前用户id</param>
        /// <returns></returns>
        public static string GetVisitedListStr(long userid)
        {
            List<ILog.Model.ILogVisithistory> historyList = new List<ILog.Model.ILogVisithistory>();

            StringBuilder strbHistoryList = new StringBuilder();

            strbHistoryList.Append("{HistoryList:[");

            List<ILog.Model.ILogVisithistory> ilogList = new List<ILog.Model.ILogVisithistory>();
            try
            {
                historyList = GetVisitedList(userid);

                if (historyList.Count == 0)
                {
                    strbHistoryList.Append("{State:'0'}]}");//无数据
                }
                else
                {
                    strbHistoryList.Append("{State:'1'},");

                    foreach (ILog.Model.ILogVisithistory ooHistory in historyList)
                    {
                        ILog.Model.VipILog ooIlog = BLL.VipILog.GetModelByUserID(ooHistory.userid);

                        if (ooIlog==null)
                        {
                            continue;
                        }
                        strbHistoryList.Append("{nickname:'" + ooIlog.nickname + "',userid:'" + ooHistory.userid + "',id:'" + ooHistory.iv_id
                            + "',date:'" + GetVisitTimeShow(ooHistory.intime) + "',face:'/images/face/small/" + ooIlog.face + "'},");
                    }
                    strbHistoryList.Remove(strbHistoryList.Length - 1, 1);
                    strbHistoryList.Append("]}");
                }

            }
            catch
            {
                strbHistoryList.Append("{State:'-1'}]}");//报错
            }

            return strbHistoryList.ToString();

        }
        #endregion

        #region 得到访问时间的显示
        /// <summary>
        /// 功能描述：得到访问时间的显示
        /// 创建标识：ljd 20120626
        /// </summary>
        /// <param name="dtTime">需要处理的时间</param>
        /// <returns>当天显示时分，昨天，前天，xx月xx日</returns>
        public static string GetVisitTimeShow(DateTime dtTime)
        {
            //访问时间
            string strTime = "";

            DateTime dtNow = DateTime.Now;

            //时间间隔
            TimeSpan tsTime = dtNow - dtTime;

            if (Convert.ToDateTime(dtTime.ToShortDateString()) < Convert.ToDateTime(dtNow.AddDays(-2).ToShortDateString()))
            {
                strTime = dtTime.ToString("MM月dd日");   
            }
            else if (Convert.ToDateTime(dtTime.ToShortDateString()) == Convert.ToDateTime(dtNow.AddDays(-2).ToShortDateString()))
            {
                strTime = "前天";
            }
            else if (Convert.ToDateTime(dtTime.ToShortDateString()) == Convert.ToDateTime(dtNow.AddDays(-1).ToShortDateString()))
            {
                strTime = "昨天";
            }
            else
            {
                strTime = dtTime.ToString("HH:mm");
            }
            return strTime;

        }
        #endregion



    }

}
