<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="secondstep.aspx.cs" Inherits="ILog.Web.verify.secondstep" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>��д��֤��Ϣ</title>
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
    <!--�����ļ���ʼ-->
    <!--#include file="../top.htm"-->
    <!--�����ļ�����-->
    <div class="TopSpan">
    </div>
    <div class="TopSpan">
    </div>
    <div class=" Area_B BrWh">
        <!--��߿�ʼ-->
        <div class="Llog L">
            <div class="Hr_10">
            </div>
            <div class=" Llog_Nav L30">
                <div class="L30 P10 F14 G4">
                    �˻�����</div>
                <ul id="leftmenu">
                </ul>
            </div>
        </div>
        <!--��֤��������ʼ-->
        <div class="RightWhite R">
            <div class="Hr_20">
            </div>
            <div>
            </div>
            <div class="Hr_10">
            </div>
            <h1 class="F14  G3 L40 Fa">
                iLog��֤</h1>
            <div class="attestationNav">
                <img src="http://simg.instrument.com.cn/ilog/blue/images/nav2.gif" alt="��д��֤��Ϣ" /></div>
            <div class="Hr_20">
            </div>
            <div class="Hr_20">
            </div>
            <ul class="ListBD G4">
                <li><span class="Span L">��֤���ͣ�</span><div class="InpWidthW L">
                    <h1 class="L F14">
                        <span id="spanTypeName">
                            <%=authenticationName%></span></h1>
                    &nbsp;<span class="F12"> <a href="javascript:changeType();" class="Blue">[������֤����]</a></span><br />
                    <span class="F12 L18 G9">��һ���������ݺ�Ӱ�����ĸ���΢���˺ž���������֤
                        <div class="Hr_10">
                        </div>
                    </span>
                </div>
                </li>
                <li><span class="Span L">���֤���룺<span class="Red">*</span></span>
                    <input name="IDNumber" id="IDNumber" type="text" class="input Fa" size="35" maxlength="20" /></li>
                <div class="Hr_5">
                </div>
                <li style="line-height: 20px; margin: 0"><span class="Span L">��֤˵����<span class="Red">*</span></span><div
                    class="InpWidthW L">
                    <span class="F12 G9">��������֤˵�����ɹ���֤�󣬽�������������֤˵�������У�����ͼ��ʾ��</span></div>
                </li>
                <li><span class="Span L">&nbsp;</span> <span class="F12 G9">
                    <textarea class="Kuang L P5" name="Comment" cols="" rows="" id="Comment" style="overflow-y: hidden;
                        height: 50px;"></textarea></span><%=verifyImg %><div class="Hr_10">
                        </div>
                </li>
                <li><span class="Span L">���֤����<span class="Red">*</span></span>
                    <input name="fupCard" id="fupCard" type="file" class="input Fa F12" size="35" />
                </li>
                <li><span class="Span L">ְλ֤����<span class="Red">*</span></span>
                    <input name="fupPosition" id="fupPosition" type="file" class="input Fa F12" size="35" />
                </li>
                <li><span class="Span L">����֤����&nbsp;</span>
                    <input name="fupOther" id="fupOther" type="file" class="input Fa F12" size="35" />
                </li>
                <li><span class="Span L">&nbsp;</span> <span class="G9 F12">֧��jpg��png��gif��ʽ����С������2M/�š�</span></li>
                <li><span class="Span L">&nbsp;</span><div class="WinBtn L">
                    <span>
                        <input name="btnSubmit" type="submit" id="btnSubmit" value="�ύ��֤" /></span></div>
                    <a href="/verify/first_<%=authenticationType %>" class="Blue">������һ��</a>
                    <input id="type" name="type" type="hidden" value="<%=authenticationType %>" />
                </li>
            </ul>
        </div>
        <!--��֤�������Ͻ���-->
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
        <!--�ײ���ʼ-->
        <!--#include file="/bottom.htm"-->
    </form>
</body>
</html>
<%=infoScript %>