using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.Web;

using Com.ILog.Utils;

namespace Ilog.BLL
{

    /// <summary>
    /// 认证历史表
    /// </summary>
    public class ILogAuthenticationHistory
    {

        #region 分页认证信息

        /// <summary>
        /// 认证信息数据分页
        /// </summary>
        /// <param name="pageCurrent">页码</param>
        /// <param name="type">认证类型（1 个人认证 加i；2 名人认证 加v）</param>
        /// <param name="state">认证状态（0 待审核；1 审核通过；2 审核不通过）</param>
        /// <param name="pageHtml">页码计算</param>
        /// <returns></returns>
        public static List<ILog.Model.ILogAuthenticationHistory> GetAuthenticationHistoryPageList(int pageCurrent, int type, int state, ref string pageHtml)
        {

            StringBuilder strWhere = new StringBuilder();

            string strTableName = "ilog_AuthenticationHistory";
            string strFieldKey = "ia_id";
            string strFieldShow = "ia_id,Userid,ia_IDNumber,ia_Comment,ia_Type,ia_State,ia_adminname,ia_checktime,ia_reason,intime";
            string strFieldOrder = "ia_id desc";
            int pageSize = 20;

            strWhere.AppendFormat("ia_Type={0} and ia_State={1}", type, state);


            int pageCount = 0;

            string strLink = string.Format("certificateHome.aspx?type={0}&state={1}&page=", type, state);

            List<ILog.Model.ILogAuthenticationHistory> result = ILog.DAL.ILogAuthenticationHistory.GetAuthenticationHistoryPageList(strTableName, strFieldKey, pageCurrent, pageSize, strFieldShow, strFieldOrder, strWhere.ToString(), ref pageCount);

            pageHtml = PageNumber.GetShowPageStr(pageCount, pageSize, pageCurrent, strLink);

            return result;

        }




        #endregion

        #region 根据用户编号获取单条认证信息

        /// <summary>
        /// 根据用户编号获取单条认证信息.by lx on 20120530
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public static ILog.Model.ILogAuthenticationHistory GetAuthenticationHistoryInfoByUserId(long userId, int type)
        {

            return ILog.DAL.ILogAuthenticationHistory.GetAuthenticationHistoryInfoByUserId(userId, type);

        }


        #endregion

        #region 认证审核


        /// <summary>
        /// 审核认证信息.by lx on 20120530
        /// </summary>
        /// <param name="model">实体信息</param>
        /// <returns></returns>
        /// </summary>
        public static bool UpdateAuthenticationInfoByUserId(ILog.Model.ILogAuthenticationHistory model, ref string message)
        {


            bool result = ILog.DAL.ILogAuthenticationHistory.UpdateAuthenticationInfoByUserId(model);

            StringBuilder mess = new StringBuilder();

            if (result)
            {

                mess.AppendFormat("{0}", model.ia_State == 1 ? "审核成功" : "拒绝成功");

                int errorState = 0;

                ILog.Model.VipILog vipIlog = ILog.DAL.VipILog.GetVipIlogInfoById(model.userid);

                ILog.Model.Vip vip = Ilog.BLL.Vip.GetUserInfo(model.userid, ref errorState);

                if (vipIlog != null && vip != null)
                {

                    StringBuilder content = new StringBuilder();

                    //审核通过
                    if (model.ia_State == 1)
                    {

                        content.AppendFormat("亲爱的{0},", vipIlog.nickname);
                        content.AppendFormat("您申请的{0},", model.ia_Type == 1 ? "个人认证" : "名人认证");
                        content.Append("已审核通过，感谢您对iLog的支持！");

                    }
                    else
                    {

                        //不通过
                        content.AppendFormat("亲爱的{0},", vipIlog.nickname);
                        content.AppendFormat("您申请的{0},", model.ia_Type == 1 ? "个人认证" : "名人认证");
                        content.Append("因以下原因审核未通过：");
                        content.AppendFormat("1.{0}.", model.ia_reason);
                        content.Append("请完善相关信息和材料后再次申请，感谢您对iLog的支持.");

                        int mobileResult = ILog.DAL.IlogTool.SendMobile(vip.mobile, content.ToString(), ref errorState);

                        mess.AppendFormat(",{0}", mobileResult == 1 ? "手机短信发送成功" : "手机短信发送失败");

                    }

                    int vipEmail = ILog.DAL.Vip.InsertVipEmail("admin", vipIlog.nickname, "ilog认证信息", content.ToString(), "127.0.0.1");

                    mess.AppendFormat(",{0}", vipEmail > 0 ? "站内短信发送成功" : "站内短信发送失败");

                }

            }
            else
            {

                mess.AppendFormat("{0}", model.ia_State == 1 ? "审核失败！" : "拒绝失败！");

            }

            message = mess.ToString();

            return result;

        }

        #endregion

        #region 增加一条认证申请数据
        /// <summary>
        /// 功能描述：增加一条认证申请数据
        /// 创建标识：ljd 20120604
        /// <param name="model">认证申请实体</param>
        /// <returns></returns>
        /// </summary>
        public static int AuthenticationHistoryAdd(ILog.Model.ILogAuthenticationHistory model)
        {
            int resultCount = ILog.DAL.ILogAuthenticationHistory.AuthenticationHistoryAdd(model);
            return resultCount;

        }
        #endregion

        #region 申请认证验证
        /// <summary>
        /// 功能描述：申请认证验证
        /// 创建标识：ljd 20120604
        /// <param name="userid">用户id</param>
        /// <param name="type">认证类型</param>
        /// <param name="urlState">是否报错</param>
        /// <returns>1可以申请 2有待审核的认证 3已经认证过该类型认证 4已经认证过名人 5未达到认证标准 </returns>
        /// </summary>
        public static int CheckAuthentication(long userid, int type)
        {
            int urlState = 0;

            //认证状态
            int state = 1;

            /*
             * 是否上传过头像，粉丝数与关注数是否达到50
             * 是否有待申请的认证
             * 是否已经认证过名人认证，已经认证过名人的不可以认证个人
             * 是否已经认证过了该类型的认证
            */
            ILog.Model.VipILog ooIlog = Ilog.BLL.VipILog.GetModelByUserID(userid);
            if (ooIlog != null)
            {
                //未上传头像
                string face = ooIlog.face;
                string file = HttpContext.Current.Server.MapPath("/images/face/small/") + face;
                if (face == "default.png" || face=="")//默认头像或者头像不存在
                {
                    state = 5;
                    return state;
                }
                //获得用户的粉丝数和关注数
                ILog.Model.VipILogCount ooIlogCount = ILog.DAL.VipILogCount.GetModelByUserID(userid);
                if (ooIlogCount != null)
                {
                    if (ooIlogCount.vic_concernnum < 50 || ooIlogCount.vic_fannum < 50 || ooIlogCount.vic_ilognum < 50)
                    {
                        state = 5;
                        return state;
                    }
                }
                else
                {
                    state = 5;
                    return state;
                }
            }
            else
            {
                state = 5;
                return state;
            }

            //待认证的个数
            int autnenticationingCount = ILog.DAL.ILogAuthenticationHistory.GetAuthenticationingCountByUserID(userid, ref urlState);

            if (autnenticationingCount == 1)
            {
                state = 2;//有待认证的个数
                return state;
            }

            //是否认证过该类型认证
            ILog.Model.ILogAuthenticationHistory ooHistory = ILog.DAL.ILogAuthenticationHistory.GetAuthenticationHistoryInfoByUserId(userid, type);
            if (ooHistory != null)
            {
                if (ooHistory.ia_State == 1)
                {
                    state = 3;//已经认证过该类型
                    return state;
                }
            }

            if (ooIlog != null)
            {
                if (ooIlog.vi_memberlevel == 2)
                {
                    state = 4;//已通过名人认证
                    return state;
                }
            }

            return state;

        }
        #endregion

        #region 申请认证验证(json格式)
        /// <summary>
        /// 功能描述：申请认证验证(json格式)
        /// 创建标识：ljd 20120604
        /// <param name="userid">用户id</param>
        /// <param name="type">认证类型</param>
        /// <returns>0可以申请 1有待审核的认证 2已经认证过名人 3已经认证过该类型认证</returns>
        /// </summary>
        public static string GetCheckAuthenticationJsonStr(long userid, int type)
        {
            int state = 0;

            try
            {
                state = CheckAuthentication(userid, type);
            }
            catch (Exception)
            {
                state = -1;
            }

            StringBuilder strbAuthentication = new StringBuilder();

            strbAuthentication.Append("{CheckState:[");

            if (state == -1)
            {
                strbAuthentication.Append("{State:'-1'}]}");
                return strbAuthentication.ToString();
            }
            else if (state > 0)
            {
                strbAuthentication.Append("{State:'" + state + "'}]}");
            }
            else
            {
                strbAuthentication.Append("{State:'0'}]}");
            }
            return strbAuthentication.ToString();

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
            ILog.Model.ILogAuthenticationHistory ooHistory = ILog.DAL.ILogAuthenticationHistory.GetAuthenticationHistoryInfoByUserId(userId, type);
            return ooHistory;

        }
        #endregion

    }

}
