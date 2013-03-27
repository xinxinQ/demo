<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hconcern.aspx.cs" Inherits="ILog.Web.hconcern" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet"
        type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />

    <script src="js/jquery-1.3.2.js" type="text/javascript" language="javascript"></script>

    <script language="javascript" type="text/javascript" src="js/jquery.cookie.js"></script>

    <script src="js/lhgdialog.min.js" type="text/javascript"></script>

    <script src="js/Common.js" type="text/javascript" language="javascript"></script>

    <script src="js/VipILogInfo.js" type="text/javascript"></script>

    <script src="js/hconcern.js" type="text/javascript" language="javascript"></script>

    <script src="js/SendMail.js" type="text/javascript" language="javascript"></script>

    <script src="js/rotate.js" type="text/javascript" charset="gb2312"></script>

    <script src="js/ilogcheck.js" type="text/javascript"></script>

</head>
<body class="body">
    <!--顶部文件开始-->
    <!--#include file="top.htm"-->
    <!--顶部文件结束-->
    <div class="TopSpan">
    </div>
    <div class="TopSpan">
    </div>
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
            <div class=" Llog_Nav L30">
                <div class="Hr_20">
                </div>
                <ul id="leftmenu">
                </ul>
            </div>
            
                                 <div class="Hr_10">
            </div>
            <div class="Llog_line" style="height:370px">
            </div>
              <div class="Hr_10">
            </div>
        </div>
        <!--左边结束-->
        <!--右边开始-->
        <!--#include file="CRight.htm"-->
        <!--右边结束-->
        <!--全部关注开始------------------------------>
        <div class="Center L">
            <div class="Hr_20">
            </div>
            <div class="Concern_Tit">
                <span id="hfollow_title"></span>
                <h1 class="F14 L30 G3" id="FollowNikeName">
                </h1>
            </div>
            <div class="Line">
            </div>
            <div class="Hr_20">
            </div>
            <div id="Follow_PageList">
            </div>
            <!--页码开始-->
            <div class="page" id="Follow_Page">
            </div>
            <!--页码结束-->
        </div>
        <!--全部关注结束------------------------------>
        <div class="Hr_1">
        </div>
    </div>
    <!--底部开始-->
    <!--#include file="bottom.htm"-->
    <!--顶部文件结束-->
</body>
</html>
