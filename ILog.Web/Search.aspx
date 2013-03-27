<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="ILog.Web.Search" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet"
        type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />

    <script language="javascript" type="text/javascript" src="js/jquery-1.3.2.js"></script>

    <script language="javascript" type="text/javascript" src="js/common.js"></script>

    <script language="javascript" type="text/javascript" src="js/Search.js"></script>

    <script src="js/lhgdialog.min.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="js/SendMail.js"></script>

    <script language="javascript" type="text/javascript" src="js/jquery.cookie.js"></script>

    <script language="javascript" type="text/javascript" src="js/VipILogInfo.js"></script>

    <script language="javascript" type="text/javascript" src="js/ilogcheck.js"></script>

</head>
<body class="body">
    <!--顶部文件开始-->
    <!--#include file="top.htm"-->
    <div class="TopSpan">
    </div>
    <div class="TopSpan">
    </div>
    <!--顶部文件结束-->
    <div class=" Area BrWh">
        <div class="SearchTop">
            <img src="http://simg.instrument.com.cn/ilog/blue/images/search1.gif" class="L" />
            <div class=" SeaBox L">
                <!--搜索开始-->
                <div class="SeaBoxBj" style="position: relative">
                    <input class="SeaInp L" type="text" value="请输入昵称" onclick="return LoginDiv(16);"
                        onfocus="if(this.value=='请输入昵称')this.value=''" onblur="if(!this.value)this.value='请输入昵称'"
                        type="text" value="<%=string.IsNullOrEmpty(strKeyword) ? "请输入昵称" : strKeyword%>"
                        type="text" name="keyword_s" id="keyword_s" maxlength="20" />
                    <input name="button" type="image" onclick="return checkform(0);" class="L" id="bnt_ss"
                        value="提交" src="http://simg.instrument.com.cn/ilog/blue/images/btn_search.gif"
                        alt="搜索" />
                    <div class=" Cl">
                    </div>
                    <!--智能提示框容器-->
                    <ul class="WindowMenu WindowW Line_blue L30" id="GetSearchTowho_Menu" style="display: none;
                        top: 33px; left: 4px; z-index: 10;">
                    </ul>
                    <!--上下建索引-->
                    <input type="hidden" name="prevTrIndex" id="prevTrIndex" value="-1" />
                </div>
                <!--搜索结束-->
                <!--记录当前用户操作的是是上文本框还是下文本框：0是上搜索框，1是是下搜索框-->
                <input type="hidden" name="as" id="as" value="0" />
                <div class="Hr_1">
                </div>
                <!--搜索数据条数开始-->
                <div id="RowsCount" class="Tr G9">
                </div>
                <!--搜索数据条数结束-->
            </div>
            <%--<div class="L L35 Pl10">+<a href="#">订阅关键字</a></div>--%>
        </div>
        <div class="Cl">
        </div>
        <!--左边开始-->
        <div class="Llog L">
            <div class="Hr_20">
            </div>
            <div class="SearchLeft">
                <div class=" Llog_Nav L30">
                    <ul>
                        <li class="liv"><a class="F14 White" href="javascript:void(0);" onclick="LeftMenu(0)">
                            <span class="ico13"></span>找人</a></li>
                        <li><a href="javascript:void(0);" class="Blue F14" onclick="LeftMenu(1)"><span class="ico14">
                        </span>文章</a></li>
                        <input id="ation_s" name="ation_s" type="hidden" value="<%=strKeyword %>" />
                        <!--搜索关键字-->
                        <input id="keyword_h" name="keyword_h" type="hidden" value="<%=strKeyword %>" />
                    </ul>
                    <%--<div class="Hr_10"></div>
<div class=" Line_ilog"></div>
<ul>
<li><a href="#">搜索历史 </a><img src="http://simg.instrument.com.cn/ilog/blue/images/SanJ.gif" width="9" height="6" /></li>
<li><a href="#" class="Blue">暂时没有找到相关搜索</a></li>
</ul>
<div class="Hr_10"></div>--%>
                </div>
            </div>
                                            <div class="Hr_10">
            </div>
            <div class="Llog_line" style="height:370px">
            </div>
              <div class="Hr_10">
            </div>
        </div>
        <!--左边结束-->
        <!--右边开始-->
        <div class="R SearchRig">
            <div class="Hr_10">
            </div>
            <!--#include file="/everyright.htm"-->
        </div>
        <!--右边结束-->
        <!--搜人开始------------------------------>
        <div class="L SearchCen">
            <div class="Hr_20">
            </div>
            <!--数据列表开始-->
            <div id="list_div">
            </div>
            <!--数据列表结束-->
            <!--搜索开始-->
            <div class="SeaBoxBj" style="position: relative">
                <input class="SeaInp L" type="text" value="请输入昵称" onclick="return LoginDiv(16);"
                    onfocus="if(this.value=='请输入昵称')this.value=''" onblur="if(!this.value)this.value='请输入昵称'"
                    type="text" value="<%=string.IsNullOrEmpty(strKeyword) ? "请输入昵称" : strKeyword%>"
                    type="text" name="keyword_s2" id="keyword_s2" maxlength="20" />
                <input name="button" type="image" onclick="checkform(1);" class="L" id="bnt_s2" value="提交"
                    src="http://simg.instrument.com.cn/ilog/blue/images/btn_search.gif" alt="搜索" />
                <div class=" Cl">
                </div>
                <!--智能提示框容器-->
                <ul class="WindowMenu WindowW Line_blue L30" id="GetSearchTowho_Menu2" style="display: none;
                    top: 33px; left: 4px; z-index: 10;">
                </ul>
            </div>
            <!--搜索结束-->
            <div class="Hr_20">
            </div>
            <!--页码开始-->
            <div id="sowhpage_div" class="page">
            </div>
            <!--页码结束-->
        </div>
        <!--收到的评论结束------------------------------>
        <div class="Hr_1">
        </div>
    </div>
    <!--底部开始-->
    <div class="bottom Tc">
        <div class="Hr_10">
        </div>
        <a href="#" class="White">关于我们</a> - <a href="#" class="White">栏目导航</a> - <a href="#"
            class="White">诚聘英才</a> - <a href="#" class="White">VIP会员中心</a> - <a href="#" class="White">
                客户投诉</a> - <a href="#" class="White">友情链接</a> - <a href="#" class="White">法律声明</a>
        - <a href="#" class="White">联系我们</a> - <a href="#" class="White">本网动态</a>
        <br />
        Instrument.com.cn Copyright<span class="Fa">&copy; </span>1999-2012,All Rights Reserved
        版权所有</div>
    <!--回到顶部JS-->

    <script type="text/javascript"><!--
var w = 90;
var h = 100;
var str = "";
var obj = document.getElementById("divStayTopLeft");
if (obj)str = obj.innerHTML;
if( typeof document.compatMode != 'undefined' && document.compatMode != 'BackCompat'){
document.writeln('<DIV  style="z-index:9;right:0;bottom:0;height:'+h+'px;width:'+w+'px;overflow:hidden;POSITION:fixed;_position:absolute; _margin-top:expression(document.documentElement.clientHeight-this.style.pixelHeight+document.documentElement.scrollTop);">');
}
else {
document.writeln('<DIV  style="z-index:9;right:0;bottom:0;height:'+h+'px;width:'+w+'px;overflow:hidden;POSITION:fixed;*position:absolute; *top:expression(eval(document.body.scrollTop)+eval(document.body.clientHeight)-this.style.pixelHeight);">');
}
document.writeln('<div style="clear:both;height:60px;"><a href="javascript:scroll(0,0)" hidefocus="true"><img src="http://simg.instrument.com.cn/ilog/blue/images/top_img.png" alt="回到顶部" style="border: 0px;" /></a></div> ');
document.write('<div style="clear:both;margin:auto;overflow:hidden;text-align:left;">'+str+'</div>');
document.writeln('</DIV>');
-->
    </script>

    <script src="js/MSIE.PNG.js"></script>

    <!--底部结束-->
</body>
</html>