<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxCheckMobile.aspx.cs"
    Inherits="ILog.Web.Ajax.AjaxCheckMobile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>认证手机</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet" type="text/css" />

    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../js/jquery.validate.min.js" type="text/javascript"></script>


    <script src="../js/checkmobile.js" type="text/javascript"></script>

</head>
<body>
    <div style="width: 100%; height: 100%">
        <h1 class="WindowTil G4 F14">
            <a href="javascript:window.parent.CloseMobileDialog('');">
                <img class=" R Img" src="http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif"
                    alt="关闭" /></a>认证手机</h1>
        <div class="Hr_20">
        </div>
        <form id="form_mobile" method="post" action="AjaxCheckMobile.aspx">
        <div class="WindowBox" style="width: 400px; z-index: 9998;">
            <p class="G4 L25">
                为了保护您的账户安全，请先进行手机验证。</p>
            <div class="BrBlue P10">
                <ul class="WindowMobile G4">
                    <li><span class="span L Tr">请输入手机号：</span><input class="input" name="mobile" id="mobile"
                        type="text" /></li>
                    <li><span class="span L">&nbsp;</span><p class="WinBtn_H L">
                        <span id="spanGetNumber">获取验证码</span></p>
                    </li>
                    <li id="liMsg"><span class="span L">&nbsp;</span><span class="yanz L" id="spanSendMsg">请输入您手机收到的6位短信验证码</span></li>
                    <li><span class="span L Tr">验证码：</span><input class="input" name="CheckNumber" id="CheckNumber"
                        type="text" /></li>
                </ul>
                <div class="Hr_10">
                </div>
                <div class="WindowMobile_Txt">
                    <img src="http://simg.instrument.com.cn/ilog/blue/images/Win_Deng.gif" class="L" />
                    无法通过验证？请联系客服： <span class=" blue">010－51654077－8127</span></div>
            </div>
            <div class="Hr_10">
            </div>
            <div class="WinBtn_H R">
                <span>
                    <input name="取消" type="button" id="btnCancle" value="取消" onclick="window.parent.CloseMobileDialog('');" /></span></div>
            <div class="WinBtn  R">
                <span>
                    <input name="确定" type="submit" id="btnSubmitMobile" value="确定" /></span></div>
            <div class="Hr_10">
            </div>
        </div>
        </form>
    </div>
</body>
</html>
