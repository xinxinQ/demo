using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Com.ILog.Utils;

namespace Ilog.BLL
{
    public class VipILogCount
    {
        #region 用户统计是否存在（True：存在，False：不存在）
        /// <summary>
        /// 用户统计是否存在（True：存在，False：不存在）
        /// <param name="vic_id">流水号</param>
        /// <param name="userid">用户id</param>
        /// </summary>
        public static string CountExists(int userid, int vic_id)
        {
            StringBuilder strCountExists = new StringBuilder();

            strCountExists.Append("var strCountExistsJsonObject = ");
            strCountExists.Append("({");
            strCountExists.Append("\"Exists\": \"" + ILog.DAL.VipILogCount.CountExists(userid, vic_id).ToString() + "\"");
            strCountExists.Append("})");

            return strCountExists.ToString();
        }
        #endregion

        #region  增加一条用户统计
        /// <summary>
        /// 增加一条用户统计
        /// <param name="model">统计表实体</param>
        /// </summary>
        public static string CountAdd(ILog.Model.VipILogCount model)
        {
            StringBuilder strCountAdd = new StringBuilder();

            strCountAdd.Append("var strCountAddJsonObject = ");
            strCountAdd.Append("({");
            strCountAdd.Append("\"state\": \"" + ILog.DAL.VipILogCount.CountAdd(model).ToString() + "\"");
            strCountAdd.Append("})");

            return strCountAdd.ToString();
        }
        #endregion

        #region 更新用户统计
        /// <summary>
        /// 更新用户统计
        /// <param name="model">统计表实体</param>
        /// </summary>
        public static string CountUpdate(ILog.Model.VipILogCount model)
        {
            StringBuilder strCountUpdate = new StringBuilder();

            strCountUpdate.Append("var strUserFanUpdateJsonObject = ");
            strCountUpdate.Append("({");
            strCountUpdate.Append("\"state\": \"" + ILog.DAL.VipILogCount.CountUpdate(model).ToString() + "\"");
            strCountUpdate.Append("})");

            return strCountUpdate.ToString();
        }
        /// <summary>
        /// 更新提醒
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int UpdateOutMessageNum(long userid)
        {
            return ILog.DAL.VipILogCount.UpdateOutMessageNum(userid);
        }

        /// <summary>
        /// 取消粉丝提醒
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static int UpdateOutFanNumConcel(long userid)
        {
            return ILog.DAL.VipILogCount.UpdateOutFanNumConcel(userid);
        }


        /// <summary>
        /// 更新我的关注
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="concernNum">关注数</param>
        /// <returns></returns>
        public static int UpdateConcernNum(long userid, int concernNum)
        {
            return ILog.DAL.VipILogCount.UpdateConcernNum(userid, concernNum);
        }

        /// <summary>
        /// 更新互相关注
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="concernuNum"></param>
        /// <returns></returns>
        public static int UpdateDoubleConcernNum(long userid, int concernuNum)
        {
            return ILog.DAL.VipILogCount.UpdateDoubleConcernNum(userid, concernuNum);
        }

        /// <summary>
        /// 更新我的粉丝带提醒
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="concernNum">关注数</param>
        /// <returns></returns>
        public static int UpdateFanOutNum(long userid, int fanNum)
        {
            return ILog.DAL.VipILogCount.UpdateFanOutNum(userid, fanNum);
        }


        /// <summary>
        /// 更新我的粉丝
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="concernNum">关注数</param>
        /// <returns></returns>
        public static int UpdateFanNum(long userid, int fanNum)
        {
            return ILog.DAL.VipILogCount.UpdateFanNum(userid, fanNum);
        }
        #endregion

        #region 删除一条用户统计
        /// <summary>
        /// 删除一条用户统计
        /// <param name="vic_id">流水号</param>
        /// <param name="userid">用户id</param> 
        /// </summary>
        public static string CountDel(int userid, int vic_id)
        {
            StringBuilder strCountDel = new StringBuilder();

            strCountDel.Append("var strCountDelJsonObject = ");
            strCountDel.Append("({");
            strCountDel.Append("\"state\": \"" + ILog.DAL.VipILogCount.CountDel(userid, vic_id).ToString() + "\"");
            strCountDel.Append("})");

            return strCountDel.ToString();
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="iuc_id">流水号</param>
        /// </summary>
        public static string GetModel(long userId)
        {
            DataTable dblCountModelList = ILog.DAL.VipILogCount.GetModel(userId);

            //构建josn字符串 
            string strCountJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblCountModelList).ToString();

            return strCountJosn;
        }



        public static string GetModelJsonCount(long userId)
        {
            DataTable dt = ILog.DAL.VipILogCount.GetModel(userId);

            StringBuilder strJson = new StringBuilder();

            strJson.Append("{UrlState:'1'},");
            //关注数
            strJson.Append("{Concern:'" + Utils.ChangeType(dt.Rows[0]["vic_concernnum"], typeof(int)) + "',");

            strJson.Append("DoubleConcern:'" + Utils.ChangeType(dt.Rows[0]["vic_doubleconcernnum"], typeof(int)) + "',");

            //新增加的粉丝数
            strJson.Append("Fan:'" + Utils.ChangeType(dt.Rows[0]["vic_fannum"], typeof(int)) + "',");

            //新增加的粉丝数
            strJson.Append("FanTotal:'" + Utils.ChangeType(dt.Rows[0]["vic_fanoutnum"], typeof(int)) + "',");

            //总的博文数
            strJson.Append("ILog:'" + Utils.ChangeType(dt.Rows[0]["vic_ilognum"], typeof(int)) + "'}");

            return strJson.ToString();

        }

        public static string GetFanOutZeroJson(long userId)
        {

            StringBuilder strJson = new StringBuilder();

            if (UpdateOutFanNumConcel(userId) > 0)
            {
                strJson.Append("{UrlState:'1'}");
            }
            else
            {
                strJson.Append("{UrlState:'2'}");
            }


            return strJson.ToString();

        }


        public static string GetModelTopMessageJson(long userId)
        {
            DataTable dt = ILog.DAL.VipILogCount.GetModel(userId);

            StringBuilder strJson = new StringBuilder();

            strJson.Append("[{UrlState:'1'},");
            //@
            strJson.Append("{MessageName:'查看@我',MessageUrl:'../It'},");

            //评论
            strJson.Append("{MessageName:'查看评论',MessageUrl:'../comment'},");

            //粉丝数
            strJson.Append("{MessageName:'查看粉丝',MessageUrl:'../fans'},");

            //总的博文数
            strJson.Append("{MessageName:'查看站短',MessageUrl:'../Msg'}");

            return strJson.ToString();

        }

        public static string GetModelTopGuangChnagJson()
        {


            StringBuilder strJson = new StringBuilder();

            strJson.Append("[{UrlState:'1'},");
            //@
            strJson.Append("{MessageName:'名人堂',MessageUrl:'../Fame.html',ImgUrl:'http://simg.instrument.com.cn/ilog/blue/images/ico_7.png'},");

            //@
            strJson.Append("{MessageName:'随便看看',MessageUrl:'../Transmit.html',ImgUrl:'http://simg.instrument.com.cn/ilog/blue/images/ico_8.png'}");


            return strJson.ToString();

        }

        /// <summary>
        /// 获取一个属性
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static DataTable GetModel_UserID(long userID)
        {
            return ILog.DAL.VipILogCount.GetModel(userID);
        }


        public static string GetModelTopMessageJsonOut(long userId)
        {

            DataTable dt = ILog.DAL.VipILogCount.GetModel(userId);

            StringBuilder strJson = new StringBuilder();

            strJson.Append("[{UrlState:'1'},");
            //@
            strJson.Append("{MessageName:'查看@我',MessageNum:'" + dt.Rows[0]["vic_atnum"].ToString() + "',MessageUrl:'../It'},");

            //评论
            strJson.Append("{MessageName:'查看评论',MessageNum:' " + dt.Rows[0]["vic_commentnum"].ToString() + "',MessageUrl:'../comment'},");

            //粉丝数
            strJson.Append("{MessageName:'查看粉丝',MessageNum:' " + dt.Rows[0]["vic_fanoutnum"].ToString() + "',MessageUrl:'../fans'},");

            //总的博文数
            strJson.Append("{MessageName:'查看站短',MessageNum:' " + dt.Rows[0]["vic_messageoutnum"].ToString() + "',MessageUrl:'../Msg'}");

            return strJson.ToString();

        }

        public static string GetModelTopUserSettingsJson(long userId)
        {
            DataTable dt = ILog.DAL.VipILogCount.GetModel(userId);

            StringBuilder strJson = new StringBuilder();

            ILog.Model.VipILog userInfo = ILog.DAL.VipILog.GetModelByUserID(userId);


            strJson.Append("[{UrlState:'1'},");

            if (userInfo == null)
            {
                //设置
                strJson.Append("{MessageName:'设  置',MessageUrl:'/settings',Face:''},");

                //退出
                strJson.Append("{MessageName:'退  出',MessageUrl:'/settings/GetOut',Face:''}");
            }
            else
            {
                //用户名
                strJson.Append("{MessageName:'" + userInfo.nickname + "',MessageUrl:'/u',Face:'" + userInfo.face + "'},");

                //设置
                strJson.Append("{MessageName:'设  置',MessageUrl:'/settings',Face:''},");

                //退出
                strJson.Append("{MessageName:'退  出',MessageUrl:'/settings/GetOut',Face:''}");
            }

            return strJson.ToString();

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
        public static string GetCountPageList(string tbname, string FieldKey, int PageCurrent, int PageSize, string FieldShow, string FieldOrder, string Where, ref int RecordCount)
        {
            string strTableName = " vip_ilog_count ";
            string strFieldKey = "vic_id";
            string strFieldShow = " vic_id,userid,vic_concernnum,vic_onewayconcernnum,vic_doubleconcernnum,vic_fannum,vic_ilognum,vic_commentoutnum,vic_fanoutnum,vic_messageoutnum,vic_messagenum,vic_atnum,vic_commentnum,intime ";
            string strFieldOrder = " vic_id desc ";
            string strWhere = " ";

            DataTable dblCountPageList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            //构建josn字符串 
            string strCountPageJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblCountPageList).ToString();

            return strCountPageJosn;
        }
        #endregion

        #region 用户@数加1
        /// <summary>
        /// 功能描述：用户@数加1
        /// 创建标识：ljd 20120526
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="urlstate">json是否成功</param>
        /// <returns></returns>
        public static int UpdateAtNum(long userid, ref int urlstate)
        {
            int resultCount = ILog.DAL.VipILogCount.UpdateAtNum(userid, ref urlstate);
            return resultCount;

        }
        #endregion

        #region 用户@数与评论数加1
        /// <summary>
        /// 功能描述：用户@数与评论数加1
        /// 创建标识：ljd 20120612
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="urlstate">json是否成功</param>
        /// <returns></returns>
        public static int UpdateAtNumAndCommentNum(long userid, ref int urlstate)
        {
            int resultCount = ILog.DAL.VipILogCount.UpdateAtNumAndCommentNum(userid, ref urlstate);
            return resultCount;

        }
        #endregion

        #region 用户评论数加1
        /// <summary>
        /// 功能描述：用户评论数加1
        /// 创建标识：ljd 20120612
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="urlstate">json是否成功</param>
        /// <returns></returns>
        public static int UpdateCommentNum(long userid, ref int urlstate)
        {
            int resultCount = ILog.DAL.VipILogCount.UpdateCommentNum(userid, ref urlstate);
            return resultCount;

        }
        #endregion

        #region 清除@提醒记录
        /// <summary>
        /// 功能描述：清除@提醒记录
        /// 创建标识：ljd 20120711
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static string ClearAtNum(long userid)
        {
            int resultCount = 0;
            try
            {
                ILog.DAL.VipILogCount.ClearAtCount(userid);
            }
            catch (Exception)
            {
                resultCount = -1;
            }

            StringBuilder strbResult = new StringBuilder();

            strbResult.Append("{State:'" + resultCount + "'}");

            return strbResult.ToString();

        }
        #endregion

        #region 根据userid得到数量实体
        /// <summary>
        /// 功能描述：根据userid得到数量实体
        /// 创建标识：ljd 20120621
        /// </summary>
        /// <returns></returns>
        public static ILog.Model.VipILogCount GetModelByUserID(long userid)
        {
            ILog.Model.VipILogCount ooIlogCount = ILog.DAL.VipILogCount.GetModelByUserID(userid);
            return ooIlogCount;

        }
        #endregion

    }
}
