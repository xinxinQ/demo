<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="firststep.aspx.cs" Inherits="ILog.Web.verify.firststep" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>完善基本信息</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet" type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />

    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript" src="../js/jquery.cookie.js"></script>

    <script src="../js/Common.js" type="text/javascript"></script>

    <script src="../js/firststep.js" type="text/javascript"></script>

    <script src="../js/jquery.validate.min.js" type="text/javascript"></script>

    <script src="../js/lhgdialog.min.js" type="text/javascript"></script>

</head>
<body class="body">
    <!--顶部文件开始-->
    <!--#include file="../top.htm"-->
    <!--顶部文件结束-->
    <div class="TopSpan">
    </div>
    <div class=" Area_B BrWh">
        <!--左边开始-->
        <div class="Llog L">

            <div class=" Llog_Nav L30">
                <div class="L30 P10 F14 G4">
                    账户设置</div>
                <ul id="leftmenu">
                </ul>
            </div>
        </div>
        <!--认证第二步开始-->
        <form id="form_input" method="post" action="firststep.aspx">
        <div class="RightWhite R">
            <div class="Hr_20">
            </div>
            <div>
            </div>
            <div class="Hr_10">
            </div>
            <h1 class="F14  G3 L40 Fa">
                iLog认证</h1>
            <div class="attestationNav">
                <img src="http://simg.instrument.com.cn/ilog/blue/images/nav1.gif" alt="完善基本信息" /></div>
            <div class="P10" style="margin-left:20px">
                <p class="G9">
                    你正在申请的是<%=strVerityType %>，请先完善并确认你的基本信息，以便认证成功后能被更多人关注。<br />
                    以后如需修改，请至“<a href="/settings/index.aspx" class="Blue">个人资料</a>”中进行修改。</p>
                <div class="Hr_20">
                </div>
                <div class="Hr_20">
                </div>
                <ul class="ListBD G4">
                    <li><span class="L">昵&nbsp;&nbsp;&nbsp;&nbsp;称：</span><div class="L InpWidth">
                        <%=nickname %></div>
                    </li>
                    <li><span class="L">姓&nbsp;&nbsp;&nbsp;&nbsp;名：</span><div class="L InpWidth">
                        <%=realname %></div>
                    </li>
                    <li><span class="L">联系邮箱：</span><div class="L InpWidth Fa">
                        <%=email %></div>
                    </li>
                    <li><span class="L">联系手机：</span><div class="L InpWidth  Fa">
                        <%=mobile %></div>
                    </li>
                </ul>
                <div class="Hr_20">
                </div>
                <p class="L35 blue F14 P5">
                    教育信息</p>
                <div class=" Line_dashed">
                </div>
                <div class="Hr_10">
                </div>
                <ul class="G4 L25 attestationList">
                    <%=strSchoolList %>
                </ul>
                <div class="Hr_20">
                </div>
                <p class="L35 blue F14 P5">
                    职业信息</p>
                <div class=" Line_dashed">
                </div>
                <div class="Hr_10">
                </div>
                <ul class="G4 L30 attestationList">
                    <li class="F14">
                        <img src="http://simg.instrument.com.cn/ilog/blue/images/san_4.gif" />
                        <%=company %>&nbsp;&nbsp;&nbsp;&nbsp;<%=vctname %></li>
                </ul>
                <div class="Hr_20">
                </div>
                <div>
                    <p class="WinBtn L">
                        <span class="White">
                            <input name="btnSubmit" type="submit" id="btnSubmit" value="下一步"  onclick="return CheckBaseInfo();"/></span>
                    </p>
                    <a href="index.aspx" class="Blue">返回上一步</a></div>
            </div>
        </div>
        <!--认证第二步料结束-->
        <input id="verityType" name="verityType" type="hidden" value="<%=intVerityType %>" />
        </form>
        <div class="Hr_1">
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
    <input id="hidPercent" type="hidden"  value="<%=percent %>"/>
    <!--底部开始-->
     <!--#include file="/bottom.htm"-->


</body>
</html>
<%=infoScript %>