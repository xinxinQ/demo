<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Messages.aspx.cs" Inherits="ILog.Web.Messages" %>

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

    <script language="javascript" type="text/javascript" src="js/common.js"></script>

    <script language="javascript" type="text/javascript" src="js/Messages.js"></script>

    <script language="javascript" type="text/javascript" src="js/SendMail.js"></script>

    <script language="javascript" type="text/javascript" src="js/ReplyMail.js"></script>

    <script language="javascript" type="text/javascript" src="js/jquery.cookie.js"></script>

    <script src="js/lhgdialog.min.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="js/VipILogInfo.js"></script>

    <!--用户基本信息.by lx on 20120626-->

    <script src="js/VipILogHome.js" type="text/javascript"></script>

    <script src="js/ilogcheck.js" type="text/javascript"></script>

</head>
<body class="body">
    <!--顶部文件开始-->
    <!--#include file="top.htm"-->
    <!--顶部文件结束-->
    <!--头部间距-->
    <div class="TopSpan">
    </div>
    <div class="TopSpan">
    </div>
    <!--头部间距-->
    <div class=" Area_B BrWh">
        <!--左边开始-->
        <div class="Llog L">
            <div class="Hr_20">
            </div>
            <!--用户信息开始-->
            <div id="vipIlog">
            </div>
            <!--用户信息结束-->
            <!--左边菜单开始-->
            <div class=" Llog_Nav L30">
                <div class="Hr_10">
                </div>
                <ul id="leftmenu">
                </ul>
                <div class="Hr_10">
                </div>
            </div>
            <div class="Hr_10">
            </div>
            <div class="Llog_line" style="height: 370px">
            </div>
            <div class="Hr_10">
            </div>
            <!--左边菜单结束-->
        </div>
        <!--左边结束-->
        <!--右边开始-->
        <!--#include file="right.htm"-->
        <!--右边结束-->
        <!--收到的评论开始------------------------------>
        <div class="Center L">
            <div class="Hr_20">
            </div>
            <div class="Hr_20">
            </div>
            <div class="Concern_Tit">
                <!--搜索按钮开始-->
                <div class="MISearch R" style="position: relative">
                    <input class="input_s G9 L" type="text" value="请输入昵称" onfocus="if(this.value=='请输入昵称')this.value=''"
                        onblur="if(!this.value)this.value='请输入昵称'" type="text" value="请输入昵称" type="text"
                        name="keyword_s" id="keyword_s" />
                    <input name="bnt_s" type="image" onclick="checkform();" class="btn L" id="bnt_s"
                        value="提交" src="http://simg.instrument.com.cn/ilog/blue/images/btn_s.gif" alt="搜索" />
                    <input type="hidden" id="keyword_a" name="keyword_a" value="<%=strKeyword %>" />
                    <!--智能提示框容器-->
                    <ul class="WindowMenu WindowW Line_blue L30" id="GetSearchTowho_Menu" style="display: none;
                        top: 29px; left: 0px; z-index: 20;">
                    </ul>
                    <!--上下建索引-->
                    <input type="hidden" name="prevTrIndex" id="prevTrIndex" value="-1" />
                </div>
                <!--搜索按钮结束-->
                <a href="javascript:void(0);" onclick="showdialog(1);" id="sendmail_a">
                    <img src="http://simg.instrument.com.cn/ilog/blue/images/btn_fs.gif" alt="发站短" class="R messages_btn" /></a>
                <font class="L30 G3"><b class="F14"><span id="myamil_a">我的站短</span></b>
                    <!--联系人数开始-->
                    <span id="RowsCount"></span>&nbsp;&nbsp;&nbsp;
                    <!--联系人数结束-->
                </font>
            </div>
            <div class="Hr_5">
            </div>
            <div class="Line">
            </div>
            <div class="Hr_20">
            </div>
            <!--返回所有列表-->
            <span style="float: left;" id="div_all"></span>
            <div class="Hr_20" id="Hr_20_1" style="display: none">
            </div>
            <div class="Hr_20" id="Hr_20_2" style="display: none">
            </div>
            <div class="Cl">
            </div>
            <!--清楚浮动-->
            <!--信息列表开始----------------->
            <div id="list_div">
            </div>
            <!--信息列表结束----------------->
            <!--页码开始-->
            <div id="sowhpage_div" class="page">
            </div>
            <!--页码结束-->
        </div>
        <!--收到的评论结束------------------------------>
        <div class="Hr_1">
        </div>
    </div>
    <!--底部开始-->
    <!--#include file="bottom.htm"-->
    <!--底部结束-->
</body>
</html>
