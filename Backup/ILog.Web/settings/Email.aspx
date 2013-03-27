<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Email.aspx.cs" Inherits="ILog.Web.settings.Email" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改邮箱</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet" type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../js/jquery.cookie.js"></script>
    <script src="../js/jquery.validate.min.js" type="text/javascript"></script>
    <script src="../js/lhgdialog.min.js" type="text/javascript"></script>
    <script src="../js/Common.js" type="text/javascript"></script>
    <script src="../js/email.js" type="text/javascript"></script>

</head>
<body class="body">
    <!--顶部文件开始-->
    <!--#include file="../top.htm"-->
       <div class="TopSpan"></div>    
    <div class="TopSpan"></div>
    <!--顶部文件结束-->
    <div class=" Area_B BrWh">
        <!--左边开始-->
        <div class="Llog L">
            <div class="Hr_20">
            </div>
            <div id="vipIlog">
            </div>

            <div class=" Llog_Nav L30">
                <div class="L30 P10 F14 G4">
                    账户设置</div>
                <ul id="leftmenu">
                </ul>
            </div>
        </div>
        <!--左边结束-->
        <!--填写邮箱开始-->
        <div class="RightWhite R">
            <form id="form_input" method="post" action="Email.aspx">
            <div class="Hr_20">
            </div>
            <div class="G3 L35">
                <h1 class="F14 L">
                    修改邮箱</h1>
                &nbsp;</div>
            <div class="Line_solid">
            </div>
            <div class="Hr_10">
            </div>
            <p class="L18 G6">
                注意：如果您修改了您的Email地址，在重新激活前，您的账号暂时将无法登录。系统会给您的新Email发送激活邮件，您需要根据激活邮件的提示重新对账号进行激活！</p>
            <div class="Hr_20">
            </div>
            <div class="Hr_20">
            </div>
            <ul class="ListBD G4">
                <li><span class="Span L Fa">确认Email:</span><input type="text" class="input L" size="35"
                    maxlength="100" id="email" name="email" /><div class="WinBtn L">
                        <span>
                            <input name="btnSubmit" type="submit" id="btnSubmit" value="确定" /></span></div>
                </li>
            </ul>
            <div class="Tc">
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
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
            </form>
        </div>
        <!--填写邮箱结束-->
        <div class="Hr_1">
        </div>
        <!--底部开始-->
       <!--#include file="/Bottom.htm"-->


        <!--底部结束-->
</body>
</html>


<%=infoScript %>
