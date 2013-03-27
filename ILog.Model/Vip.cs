using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ILog.Model
{
    public class Vip
    {
        #region VIP实体类
        private string _username;
        private string _password="";
        private string _nickname="";
        private string _name="";
        private string _company="";
        private string _address="";
        private string _postcode="";
        private string _tel="";
        private string _fax="";
        private string _email="";
        private DateTime _date = DateTime.Now;
        private DateTime _regdate = DateTime.Now;
        private string _degree="";
        private DateTime _lastvisit = DateTime.Now;
        private long _visitcount = 0;
        private decimal _experience = 0;
        private long _knowledge = 0;
        private decimal _experience_increased = 0;
        private decimal _experience_decreased = 0;
        private decimal _experience_buyer = 0;
        private long _fame = 10;
        private long _fame_increased = 0;
        private long _fame_decreased = 0;
        private string _cardid;
        private string _qq = "";
        private string _msn = "";
        private string _face = "2016.gif";
        private string _signature = "";
        private string _yessend = "";
        private string _sex = "";
        private string _birthday = "";
        private string _mobile = "";
        private bool _issub = false;
        private bool _iscis = false;
        private bool _isdownload = false;
        private int _scorecount = 0;
        private int _feecount = 0;
        private int _topiccount = 0;
        private int _papercount = 0;
        private string _forumid = "0";
        private int _newtopicnum = 0;
        private int _loyalty = 0;
        private decimal _diligence = 0;
        private bool _attach_allowshow = false;
        private int _attach_c_d_num = 0;
        private int _attach_valid_num = 0;
        private int _attach_del_num = 0;
        private bool _attach_uploadforbidden = false;
        private int _rid = 10;
        private long _onlinetime = 0;
        private bool _ispassed = false;
        private DateTime _datepassed;
        private bool _isauthentication = false;
        private DateTime _dateauthentication;
        private string _md5code;
        private int _mobilecode = 0;
        private bool _isolduser = false;
        private int _ci_id = 0;
        private string _memberlevel = "N";
        private string _username_recommend;
        private string _vccid = "99";
        private string _vcfid = "99";
        private string _vctid = "9";
        private string _familiarins1 = "0";
        private string _familiarins2 = "0";
        private string _familiarins3 = "0";
        private long _click = 0;
        private int _authenticatednum = 0;
        private int _userlevel = 1;
        private bool _hasfillinfield = true;
        private bool _isbbsnewbie = false;
        private DateTime _bbsnewbiedate = DateTime.Now;
        private long _bmscore = 0;
        private bool _willbuyer = false;
        private bool _isbuyer = false;
        private DateTime _buyerdate_apply;
        private DateTime _buyerdate_authorized;
        private int _buyergiveupnum = 0;
        private DateTime _authentication_datetime;
        private int _buyer_groupid = 0;
        private string _buyer_group_level;
        private string _cardtype;
        private bool _isbuyeradd = false;
        private bool _ismod_nickname = false;
        private string _school1 = "";
        private string _degree1 = "";
        private string _school2 = "";
        private string _degree2 = "";
        private string _useins1 = "";
        private string _useins2 = "";
        private string _useins3 = "";
        private int _factor_exp = 1;
        private string _reason = "";
        private int _etc_class = 0;
        private int _etc_com = 0;
        private int _etc_name = 0;
        private int _etc_class2;
        private int _etc_com2 = 0;
        private int _etc_name2 = 0;
        private int _mobile_pass = 0;
        private int _school_pass = 0;
        private int _etc_pass = 0;
        private DateTime _mobile_time;
        private DateTime _school_time;
        private DateTime _etc_time;
        private DateTime _mobile_intime;
        private DateTime _school_intime;
        private DateTime _etc_intime;
        private int _usertype = 0;
        private long _userid;
        private long _shopid = 0;
        private int _continuouslogindays = 0;
        private int _gold = 0;
        private int _contribution = 0;
        private string _ip = "127.0.0.1";
        private int _registersourceid = 0;
        private bool _ispassedmobile = false;
        private bool _ispassedemail = false;
        private bool _ispasshand = false;
        private string _md5password = "";
        /// <summary>
        /// 用户名
        /// </summary>
        public string username
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 昵称
        /// </summary>
        public string nickname
        {
            set { _nickname = value; }
            get { return _nickname; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 单位
        /// </summary>
        public string company
        {
            set { _company = value; }
            get { return _company; }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string postcode
        {
            set { _postcode = value; }
            get { return _postcode; }
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 传真
        /// </summary>
        public string fax
        {
            set { _fax = value; }
            get { return _fax; }
        }
        /// <summary>
        /// email
        /// </summary>
        public string email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime date
        {
            set { _date = value; }
            get { return _date; }
        }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime regDate
        {
            set { _regdate = value; }
            get { return _regdate; }
        }
        /// <summary>
        /// 学历
        /// </summary>
        public string degree
        {
            set { _degree = value; }
            get { return _degree; }
        }
        /// <summary>
        /// 最后来访时间
        /// </summary>
        public DateTime lastvisit
        {
            set { _lastvisit = value; }
            get { return _lastvisit; }
        }
        /// <summary>
        /// 访问次数
        /// </summary>
        public long visitcount
        {
            set { _visitcount = value; }
            get { return _visitcount; }
        }
        /// <summary>
        /// 最终积分
        /// </summary>
        public decimal experience
        {
            set { _experience = value; }
            get { return _experience; }
        }
        /// <summary>
        /// 知识（经验）
        /// </summary>
        public long knowledge
        {
            set { _knowledge = value; }
            get { return _knowledge; }
        }
        /// <summary>
        /// 增长的积分
        /// </summary>
        public decimal experience_increased
        {
            set { _experience_increased = value; }
            get { return _experience_increased; }
        }
        /// <summary>
        /// 减少的积分
        /// </summary>
        public decimal experience_decreased
        {
            set { _experience_decreased = value; }
            get { return _experience_decreased; }
        }
        /// <summary>
        /// 在买家俱乐部里的积分
        /// </summary>
        public decimal experience_buyer
        {
            set { _experience_buyer = value; }
            get { return _experience_buyer; }
        }
        /// <summary>
        /// 声望，默认是10
        /// </summary>
        public long fame
        {
            set { _fame = value; }
            get { return _fame; }
        }
        /// <summary>
        /// 增长的声望
        /// </summary>
        public long fame_increased
        {
            set { _fame_increased = value; }
            get { return _fame_increased; }
        }
        /// <summary>
        /// 减少的声望
        /// </summary>
        public long fame_decreased
        {
            set { _fame_decreased = value; }
            get { return _fame_decreased; }
        }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string CardID
        {
            set { _cardid = value; }
            get { return _cardid; }
        }
        /// <summary>
        /// QQ号码
        /// </summary>
        public string QQ
        {
            set { _qq = value; }
            get { return _qq; }
        }
        /// <summary>
        /// MSN帐号
        /// </summary>
        public string MSN
        {
            set { _msn = value; }
            get { return _msn; }
        }
        /// <summary>
        /// 用户的头像
        /// </summary>
        public string face
        {
            set { _face = value; }
            get { return _face; }
        }
        /// <summary>
        /// 签名
        /// </summary>
        public string signature
        {
            set { _signature = value; }
            get { return _signature; }
        }
        /// <summary>
        /// 未知
        /// </summary>
        public string yessend
        {
            set { _yessend = value; }
            get { return _yessend; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 生日
        /// </summary>
        public string birthday
        {
            set { _birthday = value; }
            get { return _birthday; }
        }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// 是否订阅用户
        /// </summary>
        public bool isSub
        {
            set { _issub = value; }
            get { return _issub; }
        }
        /// <summary>
        /// 是否CIS会员
        /// </summary>
        public bool isCIS
        {
            set { _iscis = value; }
            get { return _iscis; }
        }
        /// <summary>
        /// 是否具有上传资料的权限
        /// </summary>
        public bool isDownLoad
        {
            set { _isdownload = value; }
            get { return _isdownload; }
        }
        /// <summary>
        /// 加减分记录条数
        /// </summary>
        public int ScoreCount
        {
            set { _scorecount = value; }
            get { return _scorecount; }
        }
        /// <summary>
        /// 缴费记录条数
        /// </summary>
        public int FeeCount
        {
            set { _feecount = value; }
            get { return _feecount; }
        }
        /// <summary>
        /// 帖子数
        /// </summary>
        public int TopicCount
        {
            set { _topiccount = value; }
            get { return _topiccount; }
        }
        /// <summary>
        /// 上传资料数
        /// </summary>
        public int PaperCount
        {
            set { _papercount = value; }
            get { return _papercount; }
        }
        /// <summary>
        /// 定制论坛小类
        /// </summary>
        public string ForumID
        {
            set { _forumid = value; }
            get { return _forumid; }
        }
        /// <summary>
        /// 发布新手求助帖的数量,发布新手求助帖的数量
        /// </summary>
        public int NewTopicNum
        {
            set { _newtopicnum = value; }
            get { return _newtopicnum; }
        }
        /// <summary>
        /// 忠实度
        /// </summary>
        public int loyalty
        {
            set { _loyalty = value; }
            get { return _loyalty; }
        }
        /// <summary>
        /// 勤奋度
        /// </summary>
        public decimal diligence
        {
            set { _diligence = value; }
            get { return _diligence; }
        }
        /// <summary>
        /// 上传的附件是否能直接显示
        /// </summary>
        public bool Attach_AllowShow
        {
            set { _attach_allowshow = value; }
            get { return _attach_allowshow; }
        }
        /// <summary>
        /// 上传的附件被确认或被删除的数量,>0则表示连续被确认的数量，<0则表示连续被删除的数量
        /// </summary>
        public int Attach_C_D_Num
        {
            set { _attach_c_d_num = value; }
            get { return _attach_c_d_num; }
        }
        /// <summary>
        /// 论坛附件被验证的次数
        /// </summary>
        public int Attach_Valid_Num
        {
            set { _attach_valid_num = value; }
            get { return _attach_valid_num; }
        }
        /// <summary>
        /// 论坛附件被删除的次数
        /// </summary>
        public int Attach_Del_Num
        {
            set { _attach_del_num = value; }
            get { return _attach_del_num; }
        }
        /// <summary>
        /// 附件上传的权限被禁止
        /// </summary>
        public bool Attach_UploadForbidden
        {
            set { _attach_uploadforbidden = value; }
            get { return _attach_uploadforbidden; }
        }
        /// <summary>
        /// 论坛中的等级
        /// </summary>
        public int RID
        {
            set { _rid = value; }
            get { return _rid; }
        }
        /// <summary>
        /// 在线时长,从2005年9月13日开始计算
        /// </summary>
        public long OnlineTime
        {
            set { _onlinetime = value; }
            get { return _onlinetime; }
        }
        /// <summary>
        /// 是否已经通过验证
        /// </summary>
        public bool isPassed
        {
            set { _ispassed = value; }
            get { return _ispassed; }
        }
        /// <summary>
        /// 通过验证的日期
        /// </summary>
        public DateTime datePassed
        {
            set { _datepassed = value; }
            get { return _datepassed; }
        }
        /// <summary>
        /// 是否提交了认证申请
        /// </summary>
        public bool isAuthentication
        {
            set { _isauthentication = value; }
            get { return _isauthentication; }
        }
        /// <summary>
        /// 通过认证的日期
        /// </summary>
        public DateTime dateAuthentication
        {
            set { _dateauthentication = value; }
            get { return _dateauthentication; }
        }
        /// <summary>
        /// vip用户的验证码
        /// </summary>
        public string MD5Code
        {
            set { _md5code = value; }
            get { return _md5code; }
        }
        /// <summary>
        /// 手机验证码
        /// </summary>
        public int MobileCode
        {
            set { _mobilecode = value; }
            get { return _mobilecode; }
        }
        /// <summary>
        /// 是否老帐户，2005年9月28号以前的是老帐户
        /// </summary>
        public bool isOldUser
        {
            set { _isolduser = value; }
            get { return _isolduser; }
        }
        /// <summary>
        /// VIP所在的城市的编号
        /// </summary>
        public int CI_ID
        {
            set { _ci_id = value; }
            get { return _ci_id; }
        }
        /// <summary>
        /// 会员级别：待定（U）：之前所有老会员都赋予此级别。普通（N）：认证（C）：填写了详细个人资料的金牌（G）：收费个人会员
        /// </summary>
        public string MemberLevel
        {
            set { _memberlevel = value; }
            get { return _memberlevel; }
        }
        /// <summary>
        /// 推荐人Vip用户名
        /// </summary>
        public string Username_Recommend
        {
            set { _username_recommend = value; }
            get { return _username_recommend; }
        }
        /// <summary>
        /// 单位类别的编号
        /// </summary>
        public string VCCID
        {
            set { _vccid = value; }
            get { return _vccid; }
        }
        /// <summary>
        /// 所属行业的编号
        /// </summary>
        public string VCFID
        {
            set { _vcfid = value; }
            get { return _vcfid; }
        }
        /// <summary>
        /// 个人职位的编号
        /// </summary>
        public string VCTID
        {
            set { _vctid = value; }
            get { return _vctid; }
        }
        /// <summary>
        /// 最熟悉的仪器1
        /// </summary>
        public string FamiliarINS1
        {
            set { _familiarins1 = value; }
            get { return _familiarins1; }
        }
        /// <summary>
        /// 最熟悉的仪器2
        /// </summary>
        public string FamiliarINS2
        {
            set { _familiarins2 = value; }
            get { return _familiarins2; }
        }
        /// <summary>
        /// 最熟悉的仪器3
        /// </summary>
        public string FamiliarINS3
        {
            set { _familiarins3 = value; }
            get { return _familiarins3; }
        }
        /// <summary>
        /// BBSLog的访问次数
        /// </summary>
        public long Click
        {
            set { _click = value; }
            get { return _click; }
        }
        /// <summary>
        /// 通过认证的次数
        /// </summary>
        public int AuthenticatedNum
        {
            set { _authenticatednum = value; }
            get { return _authenticatednum; }
        }
        /// <summary>
        /// 用户军衔的等级
        /// </summary>
        public int UserLevel
        {
            set { _userlevel = value; }
            get { return _userlevel; }
        }
        /// <summary>
        /// 是否补充了行业数据
        /// </summary>
        public bool hasFillInField
        {
            set { _hasfillinfield = value; }
            get { return _hasfillinfield; }
        }
        /// <summary>
        /// 是否需要学习版规,以前默认为1，2008年8月14日改为0
        /// </summary>
        public bool isBBSNewbie
        {
            set { _isbbsnewbie = value; }
            get { return _isbbsnewbie; }
        }
        /// <summary>
        /// 开始学习版规的时间
        /// </summary>
        public DateTime BBSNewbieDate
        {
            set { _bbsnewbiedate = value; }
            get { return _bbsnewbiedate; }
        }
        /// <summary>
        /// 版务积分
        /// </summary>
        public long BMScore
        {
            set { _bmscore = value; }
            get { return _bmscore; }
        }
        /// <summary>
        /// 是否愿意加入买家俱乐部
        /// </summary>
        public bool WillBuyer
        {
            set { _willbuyer = value; }
            get { return _willbuyer; }
        }
        /// <summary>
        /// 是否买家
        /// </summary>
        public bool IsBuyer
        {
            set { _isbuyer = value; }
            get { return _isbuyer; }
        }
        /// <summary>
        /// 申请加入时间
        /// </summary>
        public DateTime BuyerDate_Apply
        {
            set { _buyerdate_apply = value; }
            get { return _buyerdate_apply; }
        }
        /// <summary>
        /// 批准时间
        /// </summary>
        public DateTime BuyerDate_Authorized
        {
            set { _buyerdate_authorized = value; }
            get { return _buyerdate_authorized; }
        }
        /// <summary>
        /// 主动放弃申请买家俱乐部的次数
        /// </summary>
        public int BuyerGiveupNum
        {
            set { _buyergiveupnum = value; }
            get { return _buyergiveupnum; }
        }
        /// <summary>
        /// 提交申请 VIP 认证的时间
        /// </summary>
        public DateTime Authentication_datetime
        {
            set { _authentication_datetime = value; }
            get { return _authentication_datetime; }
        }
        /// <summary>
        /// 群的 ID
        /// </summary>
        public int Buyer_GroupID
        {
            set { _buyer_groupid = value; }
            get { return _buyer_groupid; }
        }
        /// <summary>
        /// 群的会员级别，C为普通用户，O为群主
        /// </summary>
        public string Buyer_Group_Level
        {
            set { _buyer_group_level = value; }
            get { return _buyer_group_level; }
        }
        /// <summary>
        /// 证件类型，CA是身份证，CB是军官证，CC是其他
        /// </summary>
        public string CardType
        {
            set { _cardtype = value; }
            get { return _cardtype; }
        }
        /// <summary>
        /// 是否从买家俱乐部管理后台添加的，1是，0否
        /// </summary>
        public bool ISBuyerAdd
        {
            set { _isbuyeradd = value; }
            get { return _isbuyeradd; }
        }
        /// <summary>
        /// 是否修改过昵称
        /// </summary>
        public bool ISMod_Nickname
        {
            set { _ismod_nickname = value; }
            get { return _ismod_nickname; }
        }
        /// <summary>
        /// 毕业院校1
        /// </summary>
        public string School1
        {
            set { _school1 = value; }
            get { return _school1; }
        }
        /// <summary>
        /// 认证毕业院校学历1
        /// </summary>
        public string degree1
        {
            set { _degree1 = value; }
            get { return _degree1; }
        }
        /// <summary>
        /// 毕业院校2
        /// </summary>
        public string School2
        {
            set { _school2 = value; }
            get { return _school2; }
        }
        /// <summary>
        /// 认证毕业院校学历2
        /// </summary>
        public string degree2
        {
            set { _degree2 = value; }
            get { return _degree2; }
        }
        /// <summary>
        /// 正在使用的仪器1
        /// </summary>
        public string UseINS1
        {
            set { _useins1 = value; }
            get { return _useins1; }
        }
        /// <summary>
        /// 正在使用的仪器2
        /// </summary>
        public string UseINS2
        {
            set { _useins2 = value; }
            get { return _useins2; }
        }
        /// <summary>
        /// 正在使用的仪器3
        /// </summary>
        public string UseINS3
        {
            set { _useins3 = value; }
            get { return _useins3; }
        }
        /// <summary>
        /// 增加experience的系数
        /// </summary>
        public int Factor_Exp
        {
            set { _factor_exp = value; }
            get { return _factor_exp; }
        }
        /// <summary>
        /// 原因
        /// </summary>
        public string Reason
        {
            set { _reason = value; }
            get { return _reason; }
        }
        /// <summary>
        /// 使用仪器类别
        /// </summary>
        public int etc_class
        {
            set { _etc_class = value; }
            get { return _etc_class; }
        }
        /// <summary>
        /// 使用的仪器厂商
        /// </summary>
        public int etc_com
        {
            set { _etc_com = value; }
            get { return _etc_com; }
        }
        /// <summary>
        /// 使用的仪器名称
        /// </summary>
        public int etc_Name
        {
            set { _etc_name = value; }
            get { return _etc_name; }
        }
        /// <summary>
        /// 使用的仪器类别2
        /// </summary>
        public int etc_class2
        {
            set { _etc_class2 = value; }
            get { return _etc_class2; }
        }
        /// <summary>
        /// 使用的仪器厂商2
        /// </summary>
        public int etc_com2
        {
            set { _etc_com2 = value; }
            get { return _etc_com2; }
        }
        /// <summary>
        /// 使用的仪器名称2
        /// </summary>
        public int etc_Name2
        {
            set { _etc_name2 = value; }
            get { return _etc_name2; }
        }
        /// <summary>
        /// 是否手机认证用户
        /// </summary>
        public int mobile_pass
        {
            set { _mobile_pass = value; }
            get { return _mobile_pass; }
        }
        /// <summary>
        /// 毕业院校认证用户，默认0，1通过认证，3取消认证，2 拒绝认证
        /// </summary>
        public int school_pass
        {
            set { _school_pass = value; }
            get { return _school_pass; }
        }
        /// <summary>
        /// 使用仪器认证用户,默认是0，1通过认证 3 取消认证 2 拒绝认证
        /// </summary>
        public int etc_pass
        {
            set { _etc_pass = value; }
            get { return _etc_pass; }
        }
        /// <summary>
        /// 手机认证通过时间
        /// </summary>
        public DateTime mobile_time
        {
            set { _mobile_time = value; }
            get { return _mobile_time; }
        }
        /// <summary>
        /// 毕业院校认证通过时间
        /// </summary>
        public DateTime school_time
        {
            set { _school_time = value; }
            get { return _school_time; }
        }
        /// <summary>
        /// 使用仪器认证通过时间
        /// </summary>
        public DateTime etc_time
        {
            set { _etc_time = value; }
            get { return _etc_time; }
        }
        /// <summary>
        /// 手机号输入时间
        /// </summary>
        public DateTime mobile_inTime
        {
            set { _mobile_intime = value; }
            get { return _mobile_intime; }
        }
        /// <summary>
        /// 录入毕业院校时间
        /// </summary>
        public DateTime school_inTime
        {
            set { _school_intime = value; }
            get { return _school_intime; }
        }
        /// <summary>
        /// 使用仪器认证信息录入时间
        /// </summary>
        public DateTime etc_inTime
        {
            set { _etc_intime = value; }
            get { return _etc_intime; }
        }
        /// <summary>
        /// 标识厂商还是用户
        /// </summary>
        public int UserType
        {
            set { _usertype = value; }
            get { return _usertype; }
        }
        /// <summary>
        /// 用户ID号
        /// </summary>
        public long UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long ShopID
        {
            set { _shopid = value; }
            get { return _shopid; }
        }
        /// <summary>
        /// 连续登录时间
        /// </summary>
        public int ContinuousLoginDays
        {
            set { _continuouslogindays = value; }
            get { return _continuouslogindays; }
        }
        /// <summary>
        /// 金币
        /// </summary>
        public int Gold
        {
            set { _gold = value; }
            get { return _gold; }
        }
        /// <summary>
        /// 贡献
        /// </summary>
        public int Contribution
        {
            set { _contribution = value; }
            get { return _contribution; }
        }
        /// <summary>
        /// 注册IP
        /// </summary>
        public string IP
        {
            set { _ip = value; }
            get { return _ip; }
        }
        /// <summary>
        /// 注册来源编号
        /// </summary>
        public int RegisterSourceID
        {
            set { _registersourceid = value; }
            get { return _registersourceid; }
        }
        /// <summary>
        /// 是否手机激活通过
        /// </summary>
        public bool ISPassedMobile
        {
            set { _ispassedmobile = value; }
            get { return _ispassedmobile; }
        }
        /// <summary>
        /// 是否邮件激活通过
        /// </summary>
        public bool ISPassedEmail
        {
            set { _ispassedemail = value; }
            get { return _ispassedemail; }
        }
        /// <summary>
        /// 是否VIP管理后台进行手工激活
        /// </summary>
        public bool ISPassHand
        {
            set { _ispasshand = value; }
            get { return _ispasshand; }
        }

        /// <summary>
        /// 级别名称
        /// </summary>
        public int LevelName { get; set; }

        /// <summary>
        /// MD5密码
        /// </summary>
        public string MD5Password
        {
            set { _md5password = value; }
            get { return _md5password; }
        }

        #endregion VIP实体类结束

    }
}
