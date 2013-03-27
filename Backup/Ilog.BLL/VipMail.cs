using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ILog.BLL
{
    public class VipMail
    {
        #region  增加一条站短（返回受影响行数）
        /// <summary>
        /// 增加一条站短（返回受影响行数）
        /// <param name="model">站短实体</param>
        /// </summary>
        public static string VipMailAdd(ILog.Model.VipMail model)
        {
            StringBuilder strVipMailAdd = new StringBuilder ();

            strVipMailAdd.Append("var VipMailAddJsonObject = ");
            strVipMailAdd.Append("({");
            strVipMailAdd.Append("\"state\": \"" + ILog.DAL.VipMail.VipMailAdd(model).ToString() + "\"");
            strVipMailAdd.Append("})");

            return strVipMailAdd.ToString();
        }
        #endregion

        #region 更新一条站短（返回受影响行数）
        /// <summary>
        /// 更新一条站短（返回受影响行数）
        /// <param name="model">站短实体</param>
        /// </summary>
        public static string VipMailUpdate(ILog.Model.VipMail model)
        {
            StringBuilder strVippMailUpdate = new StringBuilder();

            strVippMailUpdate.Append("var VipMailUpdateJsonObject = ");
            strVippMailUpdate.Append("({");
            strVippMailUpdate.Append("\"state\": \"" + ILog.DAL.VipMail.VipMailUpdate(model).ToString() + "\"");
            strVippMailUpdate.Append("})");

            return strVippMailUpdate.ToString();
        }
        #endregion

        #region 删除一条站短（返回受影响行数）
        /// <summary>
        /// 删除一条站短（返回受影响行数）
        /// <param name="id">流水号</param>
        /// </summary>
        public static string VipMailDel(int id)
        {
            StringBuilder strVipMailDel = new StringBuilder();

            strVipMailDel.Append("var strVipMailDeJsonObject = ");
            strVipMailDel.Append("({");
            strVipMailDel.Append("\"state\": \"" + ILog.DAL.VipMail.VipMailDel(id).ToString() + "\"");
            strVipMailDel.Append("})");

            return strVipMailDel.ToString();
        }
        #endregion

        #region 得到一个对象实体
        /// <summary>
        /// 得到一个对象实体
        /// <param name="id">流水号</param>
        /// </summary>
        public static string GetModel(int id)
        {
            DataTable dblModelList = ILog.DAL.VipMail.GetModel(id);

            //构建josn字符串 
            string VipMailModelJosn = Com.ILog.Utils.Utils.DataTableToJSON(dblModelList).ToString();

            return VipMailModelJosn;
        }
        #endregion

        #region 读取列表
        /// <summary>
        /// 读取列表
        /// </summary>
        ///<param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="keyword">搜索关键字</param>
        ///<param name="userid">当前用户</param>
        ///<param name="ation">操作类型0：读取类表，1搜索列表</param>
        /// <returns></returns>
        public static string GetList(int PageCurrent, int PageSize, string keyword, long userid,int ation)
        {
            string strTableName = " vipmaillist ";
            string strFieldKey = "id";
            string strFieldShow = " id,towho as userid ,lastcontent,intime,m_number,(select face from vip_ilog where userid = vipmaillist.towho) as face, ";
            strFieldShow += "(select nickname from vip_ilog where userid = vipmaillist.fromwho) as fromwho,(select nickname from vip_ilog where userid = vipmaillist.towho) as towho, ";
            strFieldShow += " (SELECT count(0) AS fromwhocount FROM vipmaillist WHERE fromwho = " + userid.ToString() + ") as fromwhocount,fromwho as fromwho_ ,issend ";
            string strFieldOrder = " intime desc ";
            string strWhere = " ";

            //读取站短
            if (ation == 0)
            {
                strWhere = " fromwho = " + userid.ToString();
            }
            else if(ation == 1) //搜索站短
            {
                strWhere = " (select nickname from vip_ilog where userid = vipmaillist.towho)  like'%" + keyword + "%' and fromwho = " + userid.ToString();
            }
            else
            {
                strWhere = " fromwho = " + userid.ToString();
            }

            int RecordCount = 0;

            DataTable dblList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            return GetJsonList(dblList, RecordCount);
        }
        #endregion

        #region 收缩列表
        /// <summary>
        /// 读取列表
        /// </summary>
        ///<param name="PageCurrent">当前页码</param>
        /// <param name="PageSize">每页条数</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="keyword">搜索关键字</param>
        ///<param name="userid">当前用户</param>
        ///<param name="ation">操作类型0：读取类表，1搜索列表</param>
        ///<param name="id">收件人id</param>
        /// <returns></returns>
        public static string GetAllMailList(int PageCurrent, int PageSize, string keyword, long userid, int ation,long id)
        {
            string strUserName = Ilog.BLL.VipILog.GetUserNameByUserId(userid);
            string[] arrToHow = ILog.DAL.VipMail.GetTowhoById(id, userid);

            string strTableName = " vipmail ";
            string strFieldKey = "id";
            string strFieldShow = " id,mailid,fromwho as fromwho_,towho as userid,tid,subject,content,intime,(select face from vip_ilog where userid = vipmail.fromwho) as face,";
            strFieldShow += "(select nickname from vip_ilog where userid = vipmail.fromwho) as fromwho,(select nickname from vip_ilog where userid = vipmail.towho) as towho ";
            string strFieldOrder = " id desc ";
            string strWhere = " ";


            //读取站短（先取发出的私信）
            if (ation == 0)
            {
                strWhere = " (fromwho = '" + userid.ToString() + "' and towho = '" + arrToHow[3] + "' and fromwhoisdel = 0 ) or (towho = '" + userid.ToString() + "' and fromwho = '" + arrToHow[3] + "' and towhoisdel = 0 ) ";

                //strWhere = " fromwho = '" + userid.ToString() + "' and towho = '" + arrToHow[3] + "' and fromwhoisdel = 0 ";
            }
            else if (ation == 1) //搜索站短
            {
                strWhere = " fromwho = '" + userid.ToString() + "' and towho = '" + arrToHow[3] + "' and fromwhoisdel = 0 and content  like'%" + keyword + "%' ";
            }
            else
            {
                strWhere = " fromwho = '" + userid.ToString() + "' and towho = '" + arrToHow[3] + "' and fromwhoisdel = 0 ";
            }

            int RecordCount = 0;

            DataTable dblList = ILog.Common.Common.GetPageList(strTableName, strFieldKey, PageCurrent, PageSize, strFieldShow, strFieldOrder, strWhere, ref RecordCount);

            return GetAllMailJsonList(dblList, RecordCount, arrToHow[1]);
        }
        #endregion


        #region 构建json
        /// <summary>
        /// 构建json
        /// </summary>
        /// <param name="List">数据</param>
        /// <param name="RecordCount">总页数</param>
        /// <returns></returns>
        private static string GetJsonList(DataTable List, int RecordCount)
        {
            int count = List.Rows.Count;

            //预留统计联系人

            int RowsCount = 0;

            if (RecordCount > 1)
            {
                RowsCount = 45 * RecordCount;
            }
            else                   
            {
                RowsCount = count;  //如果是一页就返回实际的查询条数
            }        
                 
            StringBuilder strPageList = new StringBuilder();

            strPageList.Append("{List:");
            
            try
            {
                if (count > 0)
                {
                    strPageList.Append("[{State:'1'}");

                    strPageList.Append("," + (RecordCount > 1 ? "{RecordCount:'" + RecordCount + "'}," : "{RowsCount:'" + (RowsCount > 450 ? 450 : RowsCount) + "'},"));

                    //获取加i信息
                    ILog.Model.VipILog userInfo;

                    for (int i = 0; i < count; i++)
                    {
                        strPageList.Append("{id:\"" + List.Rows[i]["id"].ToString() + "\",fromwho:\"" + List.Rows[i]["fromwho"].ToString() + "\",");
                        strPageList.Append("fromwho_:\"" + List.Rows[i]["fromwho_"].ToString() + "\",");

                        //查看发@的时间是不是当天
                        strPageList.Append("intime:\"" + ILog.Common.Common.GetIlogTime(Convert.ToDateTime(List.Rows[i]["intime"])) + "\",");

                        //加i信息
                        userInfo = ILog.DAL.VipILog.GetVipIlogInfoById(Convert.ToInt64(List.Rows[i]["userid"]));
                        //加i信息
                        strPageList.Append("vi_memberlevel:\"" + userInfo.vi_memberlevel + "\",");

                        //站短中实际发信人与收信人
                        strPageList.Append("issend:\"" + List.Rows[i]["issend"].ToString() + "\",");

                        strPageList.Append("userid:\"" + List.Rows[i]["userid"].ToString() + "\",fromwhocount:\"" + List.Rows[i]["fromwhocount"].ToString() + "\",");
                        strPageList.Append("towho:\"" + List.Rows[i]["towho"].ToString() + "\",");
                        strPageList.Append("lastcontent:\"" + ILog.Common.Common.GetJScriptGlobalObjectEscape(List.Rows[i]["lastcontent"].ToString()) + "\",m_number:\""+List.Rows[i]["m_number"].ToString()+"\",");
                        strPageList.Append("towho:\"" + List.Rows[i]["towho"].ToString() + "\",face:\"" + List.Rows[i]["face"].ToString() + "\"}");

                        //最后一个元素不需要加逗号
                        if ((count - 1) > i)
                        {
                            strPageList.Append(",");
                        }
                    }
                }
                else
                {
                    strPageList.Append("[{State:'2'},{RowsCount:'0'}");  //无数据不
                }
            }
            catch
            {
                strPageList.Append("[{State:'0'}");
            }

            strPageList.Append("]}");

            return strPageList.ToString();
        }
        #endregion

        #region 获取所有私信
        /// <summary>
        /// 获取所有私信
        /// </summary>
        /// <param name="List">数据集</param>
        /// <param name="RecordCount">总页数</param>
        /// <param name="m_number">私信数</param>
        /// <returns></returns>
        public static string GetAllMailJsonList(DataTable List, int RecordCount,string m_number)
        {
            int count = List.Rows.Count;

            //预留统计联系人

            int RowsCount = 0;

            if (RecordCount > 1)
            {
                RowsCount = 45 * RecordCount;
            }
            else
            {
                RowsCount = count;  //如果是一页就返回实际的查询条数
            }

            StringBuilder strPageList = new StringBuilder();

            strPageList.Append("{List:");

            try
            {
                if (count > 0)
                {
                    strPageList.Append("[{State:'1'}");

                    strPageList.Append(",{RecordCount:'" + RecordCount + "'},{RowsCount:'" + (RowsCount > 450 ? 450 : RowsCount) + "'},{m_number:'" + m_number + "'},");

                    for (int i = 0; i < count; i++)
                    {                                                    
                        strPageList.Append("{id:\"" + List.Rows[i]["id"].ToString() + "\",fromwho:\"" + List.Rows[i]["fromwho"].ToString() + "\",");

                        //查看发@的时间是不是当天
                        strPageList.Append("intime:\"" + ILog.Common.Common.GetIlogTime(Convert.ToDateTime(List.Rows[i]["intime"])) + "\",");

                        strPageList.Append("userid:\"" + List.Rows[i]["userid"].ToString() + "\",fromwho_:\"" + List.Rows[i]["fromwho_"].ToString() + "\",");
                        strPageList.Append("towho:\"" + List.Rows[i]["towho"].ToString() + "\",tid:\"" + List.Rows[i]["tid"].ToString() + "\",");
                        strPageList.Append("subject:\"" + List.Rows[i]["subject"].ToString() + "\",content:\"" + ILog.Common.Common.GetJScriptGlobalObjectEscape(List.Rows[i]["content"].ToString()) + "\",");
                        strPageList.Append("face:\"" + List.Rows[i]["face"].ToString() + "\",mailid:\"" + List.Rows[i]["mailid"].ToString() + "\"}");

                        //最后一个元素不需要加逗号
                        if ((count - 1) > i)
                        {
                            strPageList.Append(",");
                        }
                    }
                }
                else
                {
                    strPageList.Append("[{State:'2'},{RowsCount:'0'}");  //无数据不
                }
            }
            catch
            {
                strPageList.Append("[{State:'0'}");
            }

            strPageList.Append("]}");

            return strPageList.ToString();
        }

        #endregion

        #region 获取去回复私信
        /// <summary>
        /// 获取去回复私信
        /// </summary>
        /// <param name="id">发件id</param>
        /// <returns></returns>
        public static string GetReMailJsonList(long id) 
        {
            //获取收件列表
            List<ILog.Model.VipMail> VipMailList = ILog.DAL.VipMail.GerReplyByMailid(id);

            StringBuilder strPageList = new StringBuilder();

            strPageList.Append("{List:");

            int cont_l = VipMailList.Count;
            int j = 0;

            try
            {
                if (cont_l > 0)
                {
                    strPageList.Append("[{State:'1'},");

                    foreach (ILog.Model.VipMail VipMailInfo in VipMailList)
                    {
                        strPageList.Append("{id_:\"" + VipMailInfo.id.ToString() + "\",fromwho_:\"" + VipMailInfo.fromwho + "\",");

                        //查看发@的时间是不是当天
                        strPageList.Append("intime:\"" + ILog.Common.Common.GetIlogTime(VipMailInfo.intime) + "\",");

                        strPageList.Append("towho_:\"" + VipMailInfo.towho.ToString() + "\",tid_:\"" + VipMailInfo.tid.ToString() + "\",");
                        strPageList.Append("subject_:\"" + VipMailInfo.subject.ToString() + "\",content_:\"" + VipMailInfo.content.ToString() + "\",");
                        strPageList.Append("face_:\"" + VipMailInfo.face.ToString() + "\"}");

                        //最后一个元素不需要加逗号
                        if ((cont_l - 1) > j)
                        {
                            strPageList.Append(",");
                        }

                        j++;    //索引加1
                    }
                }
                else
                {
                    strPageList.Append("[{State:'2'}");  //无数据不
                }
            }
            catch
            {
                strPageList.Append("[{State:'0'}");
            }

            strPageList.Append("]}");

            return strPageList.ToString();
        }

        #endregion

        #region 构建json（如果有注入就返回空数据集）
        /// <summary>
        /// 构建json（如果有注入就返回空数据集）
        /// </summary>
        /// <param name="List">数据</param>
        /// <param name="RecordCount">总页数</param>
        /// <returns></returns>
        public static string GetNotJsonList()
        {
            StringBuilder strCommentPageList = new StringBuilder();

            strCommentPageList.Append("{List:");

            strCommentPageList.Append("[{State:'2'}");  //无数据

            strCommentPageList.Append("]}");

            return strCommentPageList.ToString();
        }
        #endregion      

        #region 根据id回去收件人昵称
        /// <summary>
        /// 根据id回去收件人昵称
        /// </summary>
        /// <param name="id">流水号</param>
        /// <param name="userid">当前用户id</param>
        /// <returns></returns>
        public static string GetTowhoById(long id,long userid)
        {
            StringBuilder strInfo = new StringBuilder();

            string[] arrTowho = ILog.DAL.VipMail.GetTowhoById(id, userid);

            strInfo.Append("{List:");

            try
            {
                if (!string.IsNullOrEmpty(arrTowho[0]) || !string.IsNullOrEmpty(arrTowho[1]))
                {
                    strInfo.Append("[{State:'1'}");

                    strInfo.Append(",{Towho:'" + arrTowho[0] + "',m_number:'" + arrTowho[1] + "',face:'" + arrTowho[2] + "',towhoid:'" + arrTowho[3] + "'}");
                }
                else
                {
                    strInfo.Append("[{State:'2'},{RowsCount:'0'}");  //无数据不
                }
            }
            catch
            {
                strInfo.Append("[{State:'0'}");
            }

            strInfo.Append("]}");

            return strInfo.ToString();
        }
        #endregion

        #region 根据id回去收件人昵称
        /// <summary>
        /// 根据id回去收件人昵称
        /// </summary>
        /// <param name="id">流水号</param>
        /// <param name="userid">当前用户id</param>
        /// <returns></returns>
        public static string[] GetTowhoById_arr(long id,long userid)
        {
            return ILog.DAL.VipMail.GetTowhoById(id, userid);
        }
        #endregion

        #region 删除私信
        /// <summary>
        /// 删除私信
        /// </summary>
        /// <param name="id">私信id</param>
        /// <param name="username">当前用户名</param>
        /// <param name="fromwhoid">发件人id</param>
        /// <param name="towhoid">收件人id</param>
        /// <returns></returns>
        public static string VipMailDel(string id, string usernameid, string fromwhoid, string towhoid) 
        {
            string[] arrId = id.Split(',');

            int count = arrId.Length;

            StringBuilder strPageList = new StringBuilder();

            strPageList.Append("{List:");

            int count_s = 0;

            int index_d = 0;    //记录删除数量

            try
            {
                for (int i = 0; i < count; i++)
                {
                    if (!string.IsNullOrEmpty(arrId[i]))
                    {
                        count_s += ILog.DAL.VipMail.VipMailDel(Convert.ToInt64(arrId[i]), usernameid);

                        index_d++;  //记录删除数量
                    }
                }

                //更新发件人与收件人关系中的站短数量，如果站短全部被删除就解除当前用户与收件人的关系
                ILog.DAL.VipMail.UpdateVipMailCount(fromwhoid, towhoid, index_d, usernameid);

                string strM_number = ILog.DAL.VipMail.Getm_number(fromwhoid, towhoid);

                if (count_s > 0)  //执行成功
                {
                    strPageList.Append("[{State:'1'},");

                    strPageList.Append("{exec:\"" + count_s + "\",count_del:\"" + index_d + "\",m_number:\"" + strM_number + "\"}");
                }
                else
                {
                    strPageList.Append("[{State:'2'}");  //无数据不
                }

            }
            catch
            {
                strPageList.Append("[{State:'0'}");
            }

            strPageList.Append("]}");

            return strPageList.ToString();
        }
        #endregion

        #region 发送私信
        /// <summary>
        /// 实体
        /// </summary>
        /// <returns></returns>
        public static string SendMail(ILog.Model.VipMail vipmailmodel) 
        {
            StringBuilder strPageList = new StringBuilder();

            strPageList.Append("{List:");

            try
            {

                //合法收件人
                if (ILog.DAL.VipILog.VipILogExists(vipmailmodel.towho))
                {
                    //查看是否是重复的站短
                    string strInTime = ILog.DAL.VipMail.GetMailContentTime(vipmailmodel.towho, vipmailmodel.content);

                    if (strInTime == "0") //不是重复的信息可以发送
                    {
                        //发送站短
                        int count = ILog.DAL.VipMail.SendMail(vipmailmodel);

                        //更新用户的相关提示信息
                        Ilog.BLL.ILogUserConcern.Update_ConnectTime(vipmailmodel.userid, vipmailmodel.towhoid);

                        if (count > 0)  //执行成功
                        {
                            strPageList.Append("[{State:'1'},");

                            strPageList.Append("{exec:\"" + count + "\"}");
                        }
                        else
                        {
                            strPageList.Append("[{State:'2'}");  //无数据不
                        }
                    }
                    else 
                    {
                        DateTime dtSentTime = Convert.ToDateTime(strInTime);

                        TimeSpan tsSendedTime = DateTime.Now - dtSentTime;

                        //如果信息重复了，两次相等信息要大于10分钟才能发布
                        if (tsSendedTime.TotalMinutes > 10)
                        {
                            int count = ILog.DAL.VipMail.SendMail(vipmailmodel);

                            //更新用户的相关提示信息
                            Ilog.BLL.ILogUserConcern.Update_ConnectTime(vipmailmodel.userid, vipmailmodel.towhoid);

                            if (count > 0)  //执行成功
                            {
                                strPageList.Append("[{State:'1'},");

                                strPageList.Append("{exec:\"" + count + "\"}");
                            }
                            else
                            {
                                strPageList.Append("[{State:'2'}");  //无数据不
                            }
                        }
                        else //两次一样的信息10分钟以内不能发布
                        {
                            strPageList.Append("[{State:'3'}");  //重复内容
                        }
                    }
                }
                else  //非法收件人
                {
                    strPageList.Append("[{State:'2'}");  //无数据不
                }
            }
            catch
            {
                strPageList.Append("[{State:'0'}");
            }

    
            strPageList.Append("]}");

            return strPageList.ToString();
        }
        #endregion

        #region 发送私信
        /// <summary>
        /// 实体
        /// </summary>
        /// <returns></returns>
        public static string ReplyMail(ILog.Model.VipMail vipmailmodel)
        {
            StringBuilder strPageList = new StringBuilder();

            strPageList.Append("{List:");

            //合法收件人
            if (ILog.DAL.VipILog.VipILogExists(vipmailmodel.towho))
            {
                int count = ILog.DAL.VipMail.ReplyMail(vipmailmodel);

                //更新用户的相关提示信息
                Ilog.BLL.ILogUserConcern.Update_ConnectTime(vipmailmodel.userid, vipmailmodel.towhoid);

                try
                {
                    if (count > 0)  //执行成功
                    {
                        strPageList.Append("[{State:'1'},");

                        strPageList.Append("{exec:\"" + count + "\"}");
                    }
                    else
                    {
                        strPageList.Append("[{State:'2'}");  //无数据不
                    }
                }
                catch
                {
                    strPageList.Append("[{State:'0'}");
                }
            }
            else
            {
                strPageList.Append("[{State:'2'}");  //非法收件人
            }

            strPageList.Append("]}");

            return strPageList.ToString();
        }
        #endregion

        #region 获取收件人id
        /// <summary>
        /// 获取收件人id
        /// </summary>
        /// <param name="towho">收信人</param>
        /// <returns></returns>
        public static long GetTowhoIbByTowho(string towho)
        {
            return ILog.DAL.VipMail.GetTowhoIbByTowho(towho);
        }
        #endregion


        #region 发送站短智能提示收信人
        /// <summary>
        /// 发送站短智能提示收信人
        /// </summary>
        /// <returns></returns>
        public static string GetNickName_SendMail(string nickname)
        {
            StringBuilder strPageList = new StringBuilder();

            strPageList.Append("{List:");

            try
            {
                List<ILog.Model.VipMail> GetNickNameList = ILog.DAL.VipMail.GetNickName_SendMail(nickname);

                int count = GetNickNameList.Count;

                int j = 0;

                if (count > 0)  //找到用户
                {
                    strPageList.Append("[{State:'1'},");

                    foreach (ILog.Model.VipMail GetNickNameListInfo in GetNickNameList)
                    {
                        strPageList.Append("{nickname:\"" + GetNickNameListInfo.towho + "\"}");

                        //最后一个元素不需要加逗号
                        if ((count - 1) > j)
                        {
                            strPageList.Append(",");
                        }

                        j++;    //索引加1
                    }
                }
                else
                {
                    strPageList.Append("[{State:'2'}");  //无数据不
                }
            }
            catch
            {
                strPageList.Append("[{State:'0'}");
            }

            strPageList.Append("]}");

            return strPageList.ToString();
        }
        #endregion


        #region 发送站短智能提示收信人
        /// <summary>
        /// 发送站短智能提示收信人
        /// </summary>
        /// <returns></returns>
        public static string GetNickNameByUserID_MailList(string nickname)
        {
            StringBuilder strPageList = new StringBuilder();

            strPageList.Append("{List:");

            try
            {
                List<ILog.Model.VipMail> GetNickNameList = ILog.DAL.VipMail.GetNickNameByUserID_MailList(nickname);

                int count = GetNickNameList.Count;

                int j = 0;

                if (count > 0)  //找到用户
                {
                    strPageList.Append("[{State:'1'},");

                    foreach (ILog.Model.VipMail GetNickNameListInfo in GetNickNameList)
                    {
                        strPageList.Append("{nickname:\"" + GetNickNameListInfo.towho + "\"}");

                        //最后一个元素不需要加逗号
                        if ((count - 1) > j)
                        {
                            strPageList.Append(",");
                        }

                        j++;    //索引加1
                    }
                }
                else
                {
                    strPageList.Append("[{State:'2'}");  //无数据不
                }
            }
            catch
            {
                strPageList.Append("[{State:'0'}");
            }

            strPageList.Append("]}");

            return strPageList.ToString();
        }
        #endregion

        #region 提醒清零
        /// <summary>
        /// 提醒清零
        /// </summary>
        /// <param name="userid">当前用户id</param>
        /// <returns></returns>
        public static void UserMessagesCleared(long userid)
        {
            ILog.DAL.VipMail.UserMessagesCleared(userid);
        }
        #endregion

        #region 评论提醒清零
        /// <summary>
        /// 评论提醒清零
        /// </summary>
        /// <param name="userid">当前用户id</param>
        /// <returns></returns>
        public static void UserCommentCleared(long userid)
        {
            ILog.DAL.VipMail.UserCommentCleared(userid);
        }
        #endregion
    }
}
