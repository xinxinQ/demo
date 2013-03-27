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
using System.Text.RegularExpressions;
using System.Text;

using Com.ILog.Utils;
using ILog.UI;



namespace ILog.Web.settings
{
    public partial class index : BaseWebPage
    {
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
        /// 邮箱
        /// </summary>
        protected string email = "";

        /// <summary>
        /// 手机号
        /// </summary>
        protected string mobile = "";

        /// <summary>
        /// mobile的html字符串
        /// </summary>
        protected string mobileHtml = "";

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

            int vipUrlstate = 0;

            if (isPost)
            {
                nickname = IMRequest.GetFormString("vip_nickname", false);
                realname = IMRequest.GetFormString("name", false);
                sex = IMRequest.GetFormString("sex", false);
                year = IMRequest.GetFormInt("selYear", 0);
                month = IMRequest.GetFormInt("selMonth", 0);
                day = IMRequest.GetFormInt("selDay", 0);
                address = IMRequest.GetFormString("address", false);
                qq = IMRequest.GetFormString("qq", false);
                msn = IMRequest.GetFormString("msn", false);
                tel = IMRequest.GetFormString("tel", false);
                company = IMRequest.GetFormString("company", false);
                mobile = IMRequest.GetFormString("mobile", true);

                Model.Vip ooVip = Ilog.BLL.Vip.GetUserInfo(userid, ref vipUrlstate);

                int mobilepass = ooVip.mobile_pass;

                if (mobilepass != 1)//之前手机验证不通过
                {
                    //更新vip表中手机状态
                    Ilog.BLL.Vip.UpdateVipMobile(userid, mobile, ref vipUrlstate);

                    //用户名
                    string username = Ilog.BLL.Vip.GetUserNameByUserID(userid, ref vipUrlstate);

                    //添加申请记录
                    Ilog.BLL.Vip.VIPActiveSleepingAdd(username, 12, ref vipUrlstate);

                    //更新vipilog中的认证状态
                    Ilog.BLL.VipILog.UpdateVipIlogMobileState(userid, 1, ref vipUrlstate);

                    if (ooVip.mobile_time.ToShortDateString() == "0001-1-1")
                    {
                        string ip = Utils.GetRealIP();
                        //加积分
                        Ilog.BLL.Vip.ApproveMobile(userid, ip, username);
                        //站短内容
                        StringBuilder strbContent = new StringBuilder();
                        strbContent.AppendFormat("亲爱的{0}：<br/>您好！", username);
                        strbContent.Append("&nbsp;&nbsp;&nbsp;&nbsp;很高兴的通知您，您的手机认证已通过，您的认证积分奖励已发，请查看积分增减历史：<br/>      &nbsp;&nbsp;");
                        strbContent.Append("[url=http://www.instrument.com.cn/vip/Scorelist.asp]积分增减历史[/url]");

                        //发送vip站短
                        Com.ILog.SendMail.Emails.JYSmtpMailToUser(username, "您的手机认证通过!", strbContent.ToString());
                    }

                }

                ooVip.nickname = nickname;
                ooVip.name = realname;
                ooVip.sex = sex;
                ooVip.birthday = year + "-" + month + "-" + day;
                ooVip.address = address;
                ooVip.QQ = qq;
                ooVip.MSN = msn;
                ooVip.tel = tel;
                ooVip.company = company;


                //更新vip表中信息
                Ilog.BLL.Vip.UpdateVipBaseInfo(ooVip, ref vipUrlstate);

                Dictionary<string, string> dicLog = Ilog.BLL.Vip.GetVIPRegion(ooVip.CI_ID, ref vipUrlstate);

                //更新vip_ilog中的昵称，省份、城市id
                Ilog.BLL.VipILog.UpdateNickName(userid, nickname, Convert.ToInt32(dicLog["ProvinceID"]), ooVip.CI_ID);

                infoScript = "<script>showTipe('保存成功！',1);</script>";

            }


            if (userid == 0)
            {
                return;
            }

            List<ILog.Model.ILogSchool> schoollist = Ilog.BLL.ILogSchool.GetSchoolList(userid, ref vipUrlstate);
            Model.Vip ooVipR = Ilog.BLL.Vip.GetUserInfo(userid, ref vipUrlstate);
            if (ooVipR != null)
            {
                nickname = ooVipR.nickname;
                realname = ooVipR.name;
                sex = ooVipR.sex;
                email = ooVipR.email;
                //email中@的索引
                int atIndex = email.IndexOf("@");
                if (atIndex > 3)
                {
                    //截取前3个内容
                    string beforeAt = email.Substring(0, 3);
                    //截取后3个到@之间的内容
                    string leftbefore = email.Substring(3, atIndex - 3);
                    //@之后的内容
                    string afterAt = email.Substring(atIndex, email.Length - atIndex);
                    leftbefore = Regex.Replace(leftbefore, @"\S", "*");
                    email = beforeAt + leftbefore + afterAt;
                }
                mobile = ooVipR.mobile;
                StringBuilder strbMobile = new StringBuilder();
                if (mobile.Length == 11 && ooVipR.mobile_pass == 1)
                {
                    mobile = mobile.Substring(0, 3) + "****" + mobile.Substring(7, 4);
                    strbMobile.Append("<li><span class=\"Span L\"><font class=\"Red F12\">* </font>手机号码：</span><div class=\"L InpWidth Fa\">");
                    strbMobile.AppendFormat("<a class=\"Blue R\" href=\"javascript:editMobile();\">修改</a><span id=\"spanMobile\">{0}</span></div>", mobile);
                    strbMobile.Append("</li> ");
                }
                else
                {
                    strbMobile.Append("<li><span class=\"Span L\"><font class=\"Red F12\">* </font>手机号码：</span><input name=\"mobile\" type=\"text\" class=\"input L\" size=\"35\" id=\"mobile\">");
                    strbMobile.Append("<p class=\"WinBtn_H L\">");
                    strbMobile.Append("<span id=\"spanGetNumber\" onclick=\"return GetCheckNumber();\" style=\"cursor: pointer\">获取验证码</span></p>");
                    strbMobile.Append("</li>");
                    strbMobile.Append("<li><span class=\"Span L\">&nbsp;</span><span class=\"yanz L F12\" id=\"spanSendMsg\">请输入您手机收到的6位短信验证码</span></li>");
                    strbMobile.Append("<li><span class=\"Span L\">手机验证码：</span>");
                    strbMobile.Append("<input name=\"CheckNumber\" id=\"CheckNumber\" type=\"text\" class=\"input L\" size=\"15\" maxlength=\"6\" /></li>");
                }
                mobileHtml = strbMobile.ToString();

                try
                {
                    if (!string.IsNullOrEmpty(ooVipR.birthday))
                    {
                        birthday = Convert.ToDateTime(ooVipR.birthday);
                        year = birthday.Year;
                        month = birthday.Month;
                        day = birthday.Day;
                    }
                    else
                    {
                        year = 0;
                        month = 0;
                        day = 0;
                    }
                }
                catch
                {
                    birthday = DateTime.Now;
                    year = 0;
                    month = 0;
                    day = 0;
                }

                vccid = ooVipR.VCCID;
                vcfid = ooVipR.VCFID;
                vctid = ooVipR.VCTID;
                cityid = ooVipR.CI_ID;
                company = ooVipR.company;
                address = ooVipR.address;
                qq = ooVipR.QQ;
                msn = ooVipR.MSN;
                tel = ooVipR.tel;

                if (ooVipR.sex == "female")
                {
                    radMale = "<input id=\"radMale\" name=\"sex\" value=\"male\" type=\"radio\" />";
                    radFemale = "<input id=\"radFemale\" name=\"sex\" value=\"female\" checked=\"checked\" type=\"radio\" />";
                }
                else
                {
                    radMale = "<input id=\"radMale\" name=\"sex\" value=\"male\" checked=\"checked\" type=\"radio\" />";
                    radFemale = "<input id=\"radFemale\" name=\"sex\" value=\"female\" type=\"radio\" />";
                }

                int vccnameUrlState = 0;

                vccname = Ilog.BLL.Vip.GetVCCNameByVCCID(ooVipR.VCCID, ref vccnameUrlState);

                vcfname = Ilog.BLL.Vip.GetVCFNameByVCFID(ooVipR.VCCID, ooVipR.VCFID, ref vccnameUrlState);

                vctname = Ilog.BLL.Vip.GetVCTNameByVCTID(ooVipR.VCCID, ooVipR.VCTID, ref vccnameUrlState);

                Dictionary<string, string> DicRegion = Ilog.BLL.Vip.GetVIPRegion(cityid, ref vccnameUrlState);

                cityName = DicRegion["Province"] + " " + DicRegion["City"];

                provinceid = Convert.ToInt32(DicRegion["ProvinceID"]);

                countryid = Convert.ToInt32(DicRegion["CountryID"]);

                strYearList = Ilog.BLL.Vip.GetSelYearListStr(year);

                strMonthList = Ilog.BLL.Vip.GetSelMonthListStr(month);

                strDayList = Ilog.BLL.Vip.GetSelDayListStr(year, month, day);

                strSchoolList = Ilog.BLL.ILogSchool.GetSchoolListStr(userid, ref vccnameUrlState);

            }


        }



    }
}
