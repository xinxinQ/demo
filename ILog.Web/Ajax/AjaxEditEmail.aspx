<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxEditEmail.aspx.cs"
    Inherits="ILog.Web.Ajax.AjaxEditEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>修改邮箱</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet" type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />

    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../js/jquery.validate.min.js" type="text/javascript"></script>

    <script src="../js/jquery.cookie.js" type="text/javascript"></script>

    <script src="../js/Common.js" type="text/javascript"></script>

    <script src="../js/email.js" type="text/javascript"></script>

</head>
<body>
    <div style="width: 100%; height: 100%">
        <form id="form_input" method="post" action="AjaxEditEmail.aspx">
        <h1 class="WindowTil G4 F14">
            <a href="javascript:window.parent.closeEmailDialog('');">
                <img class=" R Img" src="http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif"
                    alt="关闭" /></a>修改Email</h1>
        <div class="Hr_20">
        </div>
        <p class="L18 G6" style="margin-left: 10px">
            注意：如果您修改了您的Email地址，在重新激活前，您的账号暂时将无法登录。</p>
        <p class="L18 G6" style="margin-left: 10px">
            系统会给您的新Email发送激活邮件，您需要根据激活邮件的提示重新对账号进行激活！</p>
        <div class="Hr_20">
        </div>
        <ul class="ListBD G4">
            <li><span class="Span L Fa">确认Email:</span><input type="text" class="input L" size="35"
                maxlength="100" id="email" name="email" /><div class="WinBtn L">
                    <span>
                        <input name="btnSubmit" type="submit" id="btnSubmit" value="确定" /></span></div>
            </li>
        </ul>
        </form>
    </div>
    <%=infoScript%>
</body>
</html>
