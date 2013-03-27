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
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

using Com.ILog.Utils;
using ILog.UI;


namespace ILog.Web
{
    public partial class profile : BaseWebPage
    {
        //昵称
        public string NickName { get;set; }
        //姓名
        public string Name { get; set; }
        //性别
        public string Sex { get; set; }
        //生日
        public string Birthday { get; set; }
        //单位类别
        public string CompanyType { get; set; }
        //行业类别
        public string Ctype { get; set; }
        //地区
        public string Area { get; set; }
        //单位名称
        public string Company { get; set; }
        //职位
        public string Job{get;set;}
        //手机号
        public string Mobile{get;set;}
       //联系邮箱
        public string Email{get;set;}
        //固定电话
        public string Tel { get;set; }
        //QQ
        public string QQ { get; set; }
        //msn
        public string Msn { get; set;}
        //地址
        public string Address { get; set; }

        public string School { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //用户id
            long userid = Ilog.BLL.VipILog.GetVIPUserID();

            int vipUrlstate = 0;

            List<ILog.Model.ILogSchool> schoollist = Ilog.BLL.ILogSchool.GetSchoolList(userid, ref vipUrlstate);
            Model.Vip ooVipR = Ilog.BLL.Vip.GetUserInfo(userid, ref vipUrlstate);
            if (ooVipR != null)
            {
                
                NickName = ooVipR.nickname;
                
                Name = ooVipR.name;
                
                Sex = ooVipR.sex;

                if (Sex == "female")
                {
                    Sex = "女";
                }
                else
                {
                    Sex="男";
                }
                
                
                Email = ooVipR.email;
                
              
                Mobile = ooVipR.mobile;

                Birthday = ooVipR.birthday;

                if (Com.ILog.Utils.Utils.IsDateString(Birthday))
                {
                    Birthday = Convert.ToDateTime(Birthday).Year + "年" + Convert.ToDateTime(Birthday).Month + "月" + Convert.ToDateTime(Birthday).Day + "日";
                }
                else
                {
                    Birthday = "";
                }

               

                string vccid = ooVipR.VCCID;

                string vcfid = ooVipR.VCFID;

                string vctid = ooVipR.VCTID;

                int cityid = ooVipR.CI_ID;

                Company = ooVipR.company;

                Address = ooVipR.address;
                
                QQ = ooVipR.QQ;
                
                Msn = ooVipR.MSN;
                
                Tel = ooVipR.tel;



                int vccnameUrlState = 0;

                //单位类型
               CompanyType = Ilog.BLL.Vip.GetVCCNameByVCCID(ooVipR.VCCID, ref vccnameUrlState);

                //行业类别
                Ctype = Ilog.BLL.Vip.GetVCFNameByVCFID(ooVipR.VCCID, ooVipR.VCFID, ref vccnameUrlState);


                Job = Ilog.BLL.Vip.GetVCTNameByVCTID(ooVipR.VCCID, ooVipR.VCTID, ref vccnameUrlState);

                Dictionary<string, string> DicRegion = Ilog.BLL.Vip.GetVIPRegion(cityid, ref vccnameUrlState);

                //地区
                Area = DicRegion["Province"] + " " + DicRegion["City"];

                List<ILog.Model.ILogSchool> SchoolList = Ilog.BLL.ILogSchool.GetSchoolList(userid, ref vipUrlstate);




                StringBuilder sb = new StringBuilder();

                if (SchoolList.Count > 0)
                {
                    foreach (ILog.Model.ILogSchool ooSchool in SchoolList)
                    {
                        sb.Append("<ul class=\"Iinfo L30 G4\">");
                        sb.AppendFormat(" <li>学校类型：{0}</li>  ", ooSchool.is_degreeName);
                        sb.AppendFormat(" <li>学校名称：{0}</li>", ooSchool.is_school);
                        sb.AppendFormat(" <li>入学年份：{0}</li>  ", ooSchool.is_entranceYear);
                        sb.Append("</ul><div class=\"Hr_10\"></div>");
                    }
                }

                School = sb.ToString();
            }
        }
    }
}
