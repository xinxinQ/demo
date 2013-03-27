using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ilog.BLL
{
    public class ILogInvitecode
    {

        #region 添加邀请码（每个开通ilog的用户增加5个）
        /// <summary>
        /// 功能描述：添加邀请码（每个开通ilog的用户增加5个）
        /// 创建标识：ljd 20120911
        /// </summary>
        /// <param name="senduserid">发送邀请码的用户id</param>
        /// <returns></returns>
        public static int InviteCodeAdd(long senduserid)
        {
            int resultcount = 0;
            for (int i = 0; i < 5; i++)
            {
                ILog.Model.ILogInvitecode ooInviteCode = new ILog.Model.ILogInvitecode();
                ooInviteCode.Intime = DateTime.Now;
                ooInviteCode.Vi_code = Guid.NewGuid().ToString().Replace("-", "");
                ooInviteCode.Vi_senduserid = senduserid;
                resultcount = ILog.DAL.ILogInvitecode.InviteCodeAdd(ooInviteCode);
            }
            return resultcount;

        }
        #endregion

        #region 根据邀请码更新用户id
        /// <summary>
        /// 功能描述：根据邀请码更新用户id
        /// 创建标识：ljd 20120911
        /// </summary>
        /// <param name="userid">被邀请的用户id</param>
        /// <param name="code">验证码id</param>
        /// <returns>-1 邀请码不存在 0更新失败 1更新成功 2已被激活过</returns>
        public static int InviteCodeUpdate(long userid, string code)
        {
            int resultcount = 0;

            ILog.Model.ILogInvitecode ooInviteCode = ILog.DAL.ILogInvitecode.GetInviteEntity(code);
            if (ooInviteCode != null)
            {
                if (ooInviteCode.Userid == 0)
                {
                    ooInviteCode.Userid = userid;
                    ooInviteCode.Vi_code = code;
                    resultcount = ILog.DAL.ILogInvitecode.InviteCodeUpdate(ooInviteCode);
                }
                else
                {
                    resultcount = 2;
                }
            }
            else
            {
                resultcount = -1;
            }

            return resultcount;

        }
        #endregion

        #region 得到邀请列表
        /// <summary>
        /// 功能描述：得到邀请列表
        /// 创建标识：ljd 20120912
        /// </summary>
        /// <returns></returns>
        public static string GetInviteListJsonStr(long userid)
        {

            List<ILog.Model.ILogInvitecode> codeList = ILog.DAL.ILogInvitecode.GetInviteListByUserID(userid);

            StringBuilder strbList = new StringBuilder();

            strbList.Append("{InviteList:[");

            if (codeList.Count > 0)
            {
                strbList.Append("{State:'1'},");

                foreach (ILog.Model.ILogInvitecode ooCode in codeList)
                {
                    strbList.Append("{id:'" + ooCode.Vi_id + "',code:'" + ooCode.Vi_code + "'},");
                }
                strbList.Remove(strbList.Length - 1, 1);
                strbList.Append("]}");
            }
            else
            {
                strbList.Append("{State:'0'}]}");
            }

            return strbList.ToString();

        }
        #endregion


    }
}
