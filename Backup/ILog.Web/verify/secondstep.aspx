<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="secondstep.aspx.cs" Inherits="ILog.Web.verify.secondstep" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>填写认证信息</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet" type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />

    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="../js/jquery.cookie.js"></script>

    <script src="../js/Common.js" type="text/javascript"></script>

    <script src="../js/secondstep.js" type="text/javascript"></script>

    <script src="../js/jquery.validate.min.js" type="text/javascript"></script>

    <script src="../js/lhgdialog.min.js" type="text/javascript"></script>

</head>
<body class="body">
    <form id="form_input" method="post" action="secondstep.aspx" enctype="multipart/form-data">
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
            <div class="Hr_10">
            </div>
            <div class=" Llog_Nav L30">
                <div class="L30 P10 F14 G4">
                    账户设置</div>
                <ul id="leftmenu">
                </ul>
            </div>
        </div>
        <!--认证第三步开始-->
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
                <img src="http://simg.instrument.com.cn/ilog/blue/images/nav2.gif" alt="填写认证信息" /></div>
            <div class="Hr_20">
            </div>
            <div class="Hr_20">
            </div>
            <ul class="ListBD G4">
                <li><span class="Span L">认证类型：</span><div class="InpWidthW L">
                    <h1 class="L F14">
                        <span id="spanTypeName">
                            <%=authenticationName%></span></h1>
                    &nbsp;<span class="F12"> <a href="javascript:changeType();" class="Blue">[更换认证类型]</a></span><br />
                    <span class="F12 L18 G9">有一定的社会身份和影响力的个人微博账号均可申请认证
                        <div class="Hr_10">
                        </div>
                    </span>
                </div>
                </li>
                <li><span class="Span L">身份证号码：<span class="Red">*</span></span>
                    <input name="IDNumber" id="IDNumber" type="text" class="input Fa" size="35" maxlength="20" /></li>
                <div class="Hr_5">
                </div>
                <li style="line-height: 20px; margin: 0"><span class="Span L">认证说明：<span class="Red">*</span></span><div
                    class="InpWidthW L">
                    <span class="F12 G9">请完善认证说明，成功认证后，将出现在您的认证说明介绍中，如右图所示。</span></div>
                </li>
                <li><span class="Span L">&nbsp;</span> <span class="F12 G9">
                    <textarea class="Kuang L P5" name="Comment" cols="" rows="" id="Comment" style="overflow-y: hidden;
                        height: 50px;"></textarea></span><%=verifyImg %><div class="Hr_10">
                        </div>
                </li>
                <li><span class="Span L">身份证明：<span class="Red">*</span></span>
                    <input name="fupCard" id="fupCard" type="file" class="input Fa F12" size="35" />
                </li>
                <li><span class="Span L">职位证明：<span class="Red">*</span></span>
                    <input name="fupPosition" id="fupPosition" type="file" class="input Fa F12" size="35" />
                </li>
                <li><span class="Span L">其他证明：&nbsp;</span>
                    <input name="fupOther" id="fupOther" type="file" class="input Fa F12" size="35" />
                </li>
                <li><span class="Span L">&nbsp;</span> <span class="G9 F12">支持jpg、png、gif格式，大小不超过2M/张。</span></li>
                <li><span class="Span L">&nbsp;</span><div class="WinBtn L">
                    <span>
                        <input name="btnSubmit" type="submit" id="btnSubmit" value="提交认证" /></span></div>
                    <a href="/verify/first_<%=authenticationType %>" class="Blue">返回上一步</a>
                    <input id="type" name="type" type="hidden" value="<%=authenticationType %>" />
                </li>
            </ul>
        </div>
        <!--认证第三步料结束-->
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
        <br />
        <br />
        <br />
        <br />
        <!--底部开始-->
        <!--#include file="/bottom.htm"-->
    </form>
</body>
</html>
<%=infoScript %>