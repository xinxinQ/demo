using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Web.Script.Serialization;
using System.Collections.Generic;

using Com.ILog.Utils;
using ILog.UI;
using System.Text;

namespace ILog.Web.verify
{
    public partial class firststep : BaseWebPage
    {
        /// <summary>
        /// 认证类型
        /// </summary>
        protected string strVerityType = "";

        /// <summary>
        /// 认证类型
        /// </summary>
        protected int intVerityType = 1;

        /// <summary>
        /// 年份下拉列表字符串
        /// </summary>
        protected string strYearList = "";

        /// <summary>
        /// 月份下拉列表字符串
        /// </summary>
        protected string strMonthList = "";

        /// <summary>
        /// 天数下拉列表
        /// </summary>
        protected string strDayList = "";

        /// <summary>
        /// 学校列表字符串
        /// </summary>
        protected string strSchoolList = "";

        /// <summary>
        /// 出生年份
        /// </summary>
        protected int year = 0;

        /// <summary>
        /// 出生月份
        /// </summary>
        protected int month = 0;

        /// <summary>
        /// 出生月份
        /// </summary>
        protected int day = 0;

        /// <summary>
        /// 昵称
        /// </summary>
        protected string nickname = "";

        /// <summary>
        /// 姓名
        /// </summary>
        protected string realname = "";

        /// <summary>
        /// 性别
        /// </summary>
        protected string sex = "";

        /// <summary>
        /// 生日
        /// </summary>
        protected DateTime birthday = DateTime.Now;

        /// <summary>
        /// 单位类别ID
        /// </summary>
        protected string vccid = "";

        /// <summary>
        /// 单位类别名称
        /// </summary>
        protected string vccname = "";

        /// <summary>
        /// 行业类别id
        /// </summary>
        protected string vcfid = "";

        /// <summary>
        /// 行业类别名称
        /// </summary>
        protected string vcfname = "";

        /// <summary>
        /// 职位类别id
        /// </summary>
        protected string vctid = "";

        /// <summary>
        /// 职位名称
        /// </summary>
        protected string vctname = "";

        /// <summary>
        /// 城市id
        /// </summary>
        protected int cityid = 0;

        /// <summary>
        /// 国家id
        /// </summary>
        protected int countryid = 0;

        /// <summary>
        /// 省份id
        /// </summary>
        protected int provinceid = 0;

        /// <summary>
        /// 城市名称
        /// </summary>
        protected string cityName = "";

        /// <summary>
        /// 单位名称
        /// </summary>
        protected string company = "";

        /// <summary>
        /// 通讯地址
        /// </summary>
        protected string address = "";

        /// <summary>
        /// QQ
        /// </summary>
        protected string qq = "";

        /// <summary>
        /// MSN
        /// </summary>
        protected string msn = "";

        /// <summary>
        /// 固定电话
        /// </summary>
        protected string tel = "";

        /// <summary>
        /// 性别男单选按钮字符串
        /// </summary>
        protected string radMale = "";

        /// <summary>
        /// 性别女单选按钮字符串
        /// </summary>
        protected string radFemale = "";

        /// <summary>
        /// 提示信息
        /// </summary>
        protected string infoScript = "";

        /// <summary>
        /// email地址
        /// </summary>
        protected string email = "";

        /// <summary>
        /// 手机号
        /// </summary>
        protected string mobile = "";

        /// <summary>
        /// 是否填写完善的基本资料
        /// </summary>
        protected int percent = 0;

        /// <summary>
        /// Get访问方式
        /// </summary>
        protected bool isGet = false;

        /// <summary>
        /// post访问方式
        /// </summary>
        protected bool isPost = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            isGet = Com.ILog.Utils.IMRequest.IsGet();

            isPost = Com.ILog.Utils.IMRequest.IsPost();

            //用户id
            long userid = Ilog.BLL.VipILog.GetVIPUserID();

            intVerityType = IMRequest.GetQueryInt("type", 0);

            if (intVerityType == 0)
            {
                intVerityType = IMRequest.GetFormInt("verityType", 1);
            }

            int vipUrlstate = 0;

            //验证认证申请
            int checkState = Ilog.BLL.ILogAuthenticationHistory.CheckAuthentication(userid, intVerityType);

            if (checkState == 5)
            {
                infoScript = "<script>showTipe('您未满足认证的基本条件！',0);</script>";
                return;
            }
            else if (checkState == 2)
            {
                infoScript = "<script>showTipe('您已经申请过该认证，请等待审核！',0);</script>";
                return;
            }
            else if (checkState == 3)
            {
                infoScript = "<script>showTipe('您已经通过该认证，不可以重复申请！',0);</script>";
                return;
            }
            else if (checkState == 4)
            {
                infoScript = "<script>showTipe('您已经通过名人认证，不可以再申请个人认证！',0);</script>";
                return;
            }

            if (isGet)
            {
                if (userid == 0)
                {
                    Response.End();
                }

                if (intVerityType == 1)
                {
                    strVerityType = "个人认证";
                }
                else
                {
                    strVerityType = "名人认证";
                }

                Model.Vip ooVip = Ilog.BLL.Vip.GetUserInfo(userid, ref vipUrlstate);
                if (ooVip != null)
                {
                    nickname = ooVip.nickname;
                    realname = ooVip.name;
                    company = ooVip.company;
                    email = ooVip.email;
                    mobile = ooVip.mobile;

                    int vccnameUrlState = 0;

                    vctname = Ilog.BLL.Vip.GetVCTNameByVCTID(ooVip.VCCID, ooVip.VCTID, ref vccnameUrlState);

                    StringBuilder strbSchoolList = new StringBuilder();

                    List<Model.ILogSchool> schoolList = Ilog.BLL.ILogSchool.GetSchoolList(userid, ref vccnameUrlState);

                    if (schoolList.Count > 0)
                    {
                        foreach (Model.ILogSchool ooSchool in schoolList)
                        {
                            strbSchoolList.AppendFormat("<li class=\"F14\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/san_4.gif\"/> {0}&nbsp;&nbsp;&nbsp;&nbsp;{1}&nbsp;&nbsp;&nbsp;&nbsp;{2}年</li>",
                                ooSchool.is_degreeName, ooSchool.is_school, ooSchool.is_entranceYear);
                        }
                    }

                    strSchoolList = strbSchoolList.ToString();

                    if (nickname == "" || realname == "" || company == "" || email == "" || mobile == "" || schoolList.Count == 0)
                    {
                        percent = 0;
                    }
                    else
                    {
                        percent = 1;
                    }
                }

            }
            else if (isPost)
            {
                Response.Redirect("/verify/second_" + intVerityType);
            }

        }



    }
}
