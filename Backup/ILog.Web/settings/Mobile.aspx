<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mobile.aspx.cs" Inherits="ILog.Web.settings.Mobile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改手机</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet" type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />

    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../js/jquery.cookie.js"></script>

    <script src="../js/jquery.validate.min.js" type="text/javascript"></script>

    <script src="../js/lhgdialog.min.js" type="text/javascript"></script>

    <script src="../js/Common.js" type="text/javascript"></script>

    <script src="../js/Mobile.js" type="text/javascript"></script>

</head>
<body class="body">
    <!--顶部文件开始-->
    <!--#include file="../top.htm"-->
    <!--顶部文件结束-->
    <div class="TopSpan">
    </div>
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
        <!--左边结束-->
        <form id="form_input" action="Mobile.aspx" method="post">
        <!--修改手机开始-->
        <div class="RightWhite R">
            <div class="Hr_20">
            </div>
            <div class="G3 L35">
                <h1 class="F14 L">
                    修改手机</h1>
                &nbsp;</div>
            <div class="Line_solid">
            </div>
            <div class="Hr_20">
            </div>
            <div class="Hr_20">
            </div>
            <ul class="ListBD G4">
                <li><span class="Span L">原有手机号：</span><input type="text" class="input L" size="25"
                    name="oldmobile" id="oldmobile" /></li>
                <li><span class="Span L">手机号码：</span>
                    <input name="mobile" id="mobile" type="text" class="input L" size="25" maxlength="20" />
                </li>
                <li>
                    <span class="Span L">&nbsp;</span><div class="InpWidthW L"><span class="L18 G9 F12 Fa">如：13439255411</span><div class="Hr_5">
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
            <div class="Line_ilog">
            </div>
            <br />
            <div class="Tc">
                <div class="WinBtn"  style="margin-right:200px">
                    <span>
                        <input name="btnSubmit" type="submit" id="btnSubmit" value="保存修改" /></span></div>
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
        </div>
        <!--修改手机结束-->
        </form>
        <div class="Hr_1">
        </div>
    </div>
    <!--底部开始-->
    <!--#include file="/bottom.htm"-->
</body>
</html>
<%=infoScript%>
