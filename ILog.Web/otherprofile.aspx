<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="otherprofile.aspx.cs" Inherits="ILog.Web.otherprofile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>个人资料</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet"
        type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />
    <link href="css/rotate.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="js/jquery-1.4.2.min.js"></script>

    <script src="js/Common.js" type="text/javascript" language="javascript"></script>

    <script language="javascript" type="text/javascript" src="js/jquery.cookie.js"></script>

    <script src="js/jquery.query.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="js/otherprofile.js"></script>

    <script src="js/rotate.js" type="text/javascript" charset="gb2312"></script>

    <script src="js/lhgdialog.min.js" type="text/javascript"></script>

    <script src="js/SendMail.js" type="text/javascript" language="javascript"></script>

    <script src="js/VipILogInfo.js" type="text/javascript"></script>

    <script src="js/ilogcheck.js" type="text/javascript"></script>

</head>
<body class="body">
    <!--顶部文件开始-->
    <!--#include file="top.htm"-->
    <div class="TopSpan">
    </div>
    <div class="TopSpan">
    </div>
    <!--顶部文件结束-->
    <!--顶部文件结束-->
    <div class=" Area_B BrWh">
        <!--左边开始-->
        <div class="Llog L">
            <div class="Hr_20">
            </div>
            <div class=" Llog_Head">
                <div id="headInfo">
                </div>
                <div class="Hr_6">
                </div>
            </div>
            <input id="userid" type="hidden" value="<%=userid %>" />
            <input id="nikename" type="hidden" name="nikename" value="" />
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
        <!--左边结束-->
        <!--右边开始-->
        <!--#include file="taright.htm"-->
        <!--右边结束-->
        <div class="Center L">
            <div class="Hr_20">
            </div>
            <div class="Line_blue BrBlue P10 G3" id="personalInfo">
            </div>
            <br />
            <div id="HpersionConcern">
            </div>
            <div class="Hr_20">
            </div>
            <div class="IinfoNav L40">
                <h1 class="Pl10 F14 G4 L">
                    基本信息
                </h1>
                <span class="Pl10"></span>
            </div>
            <div class="Hr_10">
            </div>
            <div class="P10">
                <ul class="Iinfo L30 G4">
                    <li>昵称：<%=NickName%>
                    </li>
                    <li>性别：<%=Sex %>
                    </li>
                    <li>单位类别：<%=CompanyType%>
                    </li>
                    <li>行业类别：<%=Ctype%></li>
                    <li>地区：<%=Area%>
                    </li>
                    <li>职位：<%=Job%></li>
                </ul>
                <div class="Hr_1">
                </div>
            </div>
            <div class="Hr_20">
            </div>
            <div class="IinfoNav L40">
                <h1 class="Pl10 F14 G4 L">
                    教育背景
                </h1>
                <span class="Pl10"></span>
            </div>
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
    <!--底部开始-->
    <!--#include file="bottom.htm"-->
    <!--底部结束-->
</body>
</html>
