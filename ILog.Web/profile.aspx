<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="ILog.Web.profile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>��������</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet"
        type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />

    <script src="js/jquery-1.3.2.js" type="text/javascript" language="javascript"></script>

    <script language="javascript" type="text/javascript" src="js/jquery.cookie.js"></script>

    <script src="/js/jquery.query.js" type="text/javascript"></script>

    <script src="js/Common.js" type="text/javascript" language="javascript"></script>

    <script src="js/profile.js" type="text/javascript" language="javascript"></script>

    <script src="js/SendMail.js" type="text/javascript"></script>

    <script src="js/VipILogInfo.js" type="text/javascript"></script>

    <script src="js/ilogcheck.js" type="text/javascript"></script>

</head>
<body class="body">
    <!--�����ļ���ʼ-->
    <!--#include file="top.htm"-->
    <div class="TopSpan">
    </div>
    <div class="TopSpan">
    </div>
    <!--�����ļ�����-->
    <!--�����ļ�����-->
    <div class=" Area_B BrWh">
        <!--��߿�ʼ-->
        <div class="Llog L">
            <div class="Hr_20">
            </div>
            <div class=" Llog_Head">
                <!--�û���Ϣ-->
                <div id="headInfo">
                </div>
                <div class="Hr_6">
                </div>
            </div>
            <div class=" Llog_Nav L30">
                <div class="Hr_20">
                </div>
                <ul id="leftmenu">
                </ul>
            </div>
            <div class="Hr_10">
            </div>
            <div class="Llog_line" style="height: 370px">
            </div>
            <div class="Hr_10">
            </div>
        </div>
        <!--��߽���-->
        <!--�ұ߿�ʼ-->
        <!--�ұ߿�ʼ-->
        <!--#include file="iright.htm"-->
        <!--�ұ߽���-->
        <div class="Center L">
            <div class="Hr_20">
            </div>
            <div class="Line_blue BrBlue P10 G3" id="personalInfo">
            </div>
            <div class="Hr_20">
            </div>
            <div class="IinfoNav L40">
                <h1 class="Pl10 F14 G4 L">
                    ������Ϣ
                </h1>
                <span class="Pl10"><a href="/settings/index.aspx" class="Blue">�޸�</a></span></div>
            <div class="Hr_10">
            </div>
            <div class="P10">
                <ul class="Iinfo L30 G4">
                    <li>�ǳƣ�<%=NickName%>
                    </li>
                    <li>������<%=Name%></li>
                    <li>�Ա�<%=Sex %>
                    </li>
                    <li>���գ�<%=Birthday%></li>
                    <li>��λ���<%=CompanyType%>
                    </li>
                    <li>��ҵ���<%=Ctype%></li>
                    <li>������<%=Area%>
                    </li>
                    <li>��λ���ƣ�<%=Company%></li>
                    <li>ְλ��<%=Job%></li>
                    <li>�ֻ����룺<%=Mobile%>
                    </li>
                    <li>��ϵ���䣺<%=Email%></li>
                    <li>�̶��绰��<%=Tel%></li>
                    <li>QQ��<%=QQ%>
                    </li>
                    <li>MSN��<%=Msn %>
                    </li>
                    <li style="width: 100%">ͨѶ��ַ��<%=Address%></li>
                </ul>
                <div class="Hr_1">
                </div>
            </div>
            <div class="Hr_20">
            </div>
            <div class="IinfoNav L40">
                <h1 class="Pl10 F14 G4 L">
                    ��������
                </h1>
                <span class="Pl10"><a href="/settings/index.aspx" class="Blue">�޸�</a></span></div>
            <div class="Hr_10">
            </div>
            <div class="P10">
                <%=School%>
                <div class="Hr_1">
                </div>
            </div>
        </div>
        <div class="Hr_1">
        </div>
    </div>
    <!--�ײ���ʼ-->
    <!--#include file="bottom.htm"-->
    <!--�ײ�����-->
</body>
</html>
