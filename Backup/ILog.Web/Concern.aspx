<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Concern.aspx.cs" Inherits="ILog.Web.Concern" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>关注我的人 iLog-仪器信息网-记录身边的点滴</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet"
        type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />

    <script src="js/jquery-1.3.2.js" type="text/javascript" language="javascript"></script>

    <script language="javascript" type="text/javascript" src="js/jquery.cookie.js"></script>

    <script src="js/lhgdialog.min.js" type="text/javascript"></script>

    <script src="js/Common.js" type="text/javascript" language="javascript"></script>

    <script src="js/VipILogInfo.js" type="text/javascript"></script>

    <script src="js/SendMail.js" type="text/javascript" language="javascript"></script>

    <script src="js/Concern.js" type="text/javascript" language="javascript"></script>

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
    <div class=" Area_B BrWh">
        <!--左边开始-->
        <div class="Llog L">
            <div class=" Llog_Nav L30">
                <div class="Hr_10">
                </div>
                <div class="L30 P10 F14 G4">
                    关注/粉丝</div>
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
        </div>
        <!--左边结束-->
        <!--右边开始-->
        <!--#include file="cright.htm"-->
        <!--右边结束-->
        <!--我的粉丝开始------------------------------>
        <div class="Center L">
            <div class="Hr_20">
            </div>
            <div class="Hr_20">
            </div>
            <div class="Concern_Tit">
                <div class="MISearch R" style="position: relative;">
                    <input class="input_s G9 L" value="请输入昵称" onfocus="if(this.value=='请输入昵称')this.value=''"
                        onblur="if(!this.value)this.value='请输入昵称'" type="text" name="txtFollowtSearch"
                        id="txtFollowtSearch" maxlength="20" />
                    <input class="btn L" name="SearchFollow" id="SearchFollow" type="image" src="http://simg.instrument.com.cn/ilog/blue/images/btn_s.gif"
                        alt="搜索按钮" />
                    <ul class="WindowMenu Line_blue L30  G6" id="GetFollow_Menu" style="position: absolute;
                        top: 30px; left: 0px; width: 140px; display: none;">
                    </ul>
                </div>
                <h1 class="F14 L30 G3">
                    已有<span id="spanfan">0</span>人关注你</h1>
            </div>
            <div class="Hr_5">
            </div>
            <div class="Line">
            </div>
            <div class="L30 G4" id="PageSort">
            </div>
            <div class="Hr_20">
            </div>
            <input id="keyword" name="keyword" type="hidden" value="<%=keyword %>" />
            <div id="Follow_PageList">
            </div>
            <!--页码开始-->
            <div class="page" id="Follow_Page">
            </div>
            <!--页码结束-->
        </div>
        <!--我的粉丝结束------------------------------>
        <div class="Hr_1">
        </div>
    </div>
    <!--底部开始-->
    <!--#include file="bottom.htm"-->
    <!--底部结束-->
</body>
</html>
