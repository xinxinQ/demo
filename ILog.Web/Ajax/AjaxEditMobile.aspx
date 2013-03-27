<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxEditMobile.aspx.cs"
    Inherits="ILog.Web.Ajax.AjaxEditMobile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>修改手机</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet" type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />

    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../js/jquery.validate.min.js" type="text/javascript"></script>

    <script src="../js/jquery.cookie.js" type="text/javascript"></script>

    <script src="../js/Common.js" type="text/javascript"></script>

    <script src="../js/Mobile.js" type="text/javascript"></script>

</head>
<body>
    <div style="width: 100%; height: 100%">
        <h1 class="WindowTil G4 F14">
            <a href="javascript:window.parent.closeMobileDialog('','');">
                <img class=" R Img" src="http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif"
                    alt="关闭" /></a>修改手机</h1>
        <!--修改手机开始-->
        <div class="Hr_20">
        </div>
        <form id="form_input" action="AjaxEditMobile.aspx" method="post">
         <ul class="ListBD G4">
            <li><span class="Span L">原有手机号：</span><input type="text" class="input L" size="25"
                name="oldmobile" id="oldmobile" /></li>
            <li><span class="Span L">手机号码：</span>
                <input name="mobile" id="mobile" type="text" class="input L" size="25" maxlength="20" />
            </li>
            <li><span class="Span L">&nbsp;</span><div class=" L">
                <span class="L18 G9 F12 Fa">如：13439255411</span><div class="Hr_5">
                </div>
                <p class="WinBtn_H">
                    <span id="spanGetNumber" onclick="return GetCheckNumber();">获取验证码</span></p>
            </div>
            </li>
            <li><span class="Span L">&nbsp;</span><span class="yanz L F12" id="spanSendMsg">请输入您手机收到的6位短信验证码</span></li>
            <li><span class="Span L">手机验证码：</span><input name="CheckNumber" id="CheckNumber"
                type="text" class="input L" size="15" maxlength="6" /></li>
        </ul>
        <div class="Hr_1">
        </div>
        <br />
        <div class="Tc">
            <div class="WinBtn">
                <span>
                    <input name="btnSubmit" type="submit" id="btnSubmit" value="保存修改" /></span></div>
        </div>
        
        <!--修改手机结束-->
        </form>
    </div>
</body>
</html>
<%=infoScript%>
