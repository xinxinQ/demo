<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Friends.aspx.cs" Inherits="ILog.Web.Friends" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>我关注的人 iLog-仪器信息网-记录身边的点滴</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet"
        type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />

    <script src="js/jquery-1.3.2.js" type="text/javascript" language="javascript"></script>

    <script src="js/lhgdialog.min.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="js/jquery.cookie.js"></script>

    <script src="js/Common.js" type="text/javascript" language="javascript"></script>

    <script src="js/VipILogInfo.js" type="text/javascript"></script>

    <script src="js/Friends.js" type="text/javascript" language="javascript"></script>

    <script src="js/SendMail.js" type="text/javascript" language="javascript"></script>

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
        <!--互相关注开始------------------------------>
        <div class="Center L">
            <div class="Hr_20">
            </div>
            <div class="Concern_Tit">
                <h1 class="F14 L30 G3">
                    <div class="MISearch R" style="position: relative;">
                        <input class="input_s G9 L" value="请输入昵称" onfocus="if(this.value=='请输入昵称')this.value=''"
                            onblur="if(!this.value)this.value='请输入昵称'" type="text" name="txtFollowtSearch"
                            id="txtFollowtSearch" maxlength="20" />
                        <input class="btn L" name="SearchFollow" id="SearchFollow" type="image" src="http://simg.instrument.com.cn/ilog/blue/images/btn_s.gif"
                            alt="搜索按钮" />
                        <ul class="WindowMenu  Window450 Line_blue L30 More" id="GetFollow_Menu" style="position: absolute;
                            top: 30px; z-index: 3; left: 0px; width: 140px; display: none;">
                        </ul>
                    </div>
                    我关注了<span id="spanConcern">0</span>人</h1>
            </div>
            <div class=" MiNav">
                <div class=" R">
                    <div class="Hr_5">
                    </div>
                    <div class="WinBtn " style="margin: 0">
                        <span>
                            <input name="GroupAddP" type="button" id="GroupAddP" value="+ 创建分组" /></span></div>
                </div>
                <ul class="ul" style="width: 420px; z-index: 3;" id="FollowMenu">
                </ul>
            </div>
            <div class="WindowMenu2  L30" id="GroupMore_Menu" style="display: none; z-index: 3;">
                <ul id="MenuUL">
                </ul>
                <div class=" Line_dashed">
                </div>
                <div>
                    &nbsp;<img src="http://simg.instrument.com.cn/ilog/blue/images/ico-j1.gif" />
                    <a href="javascript:void(0);" class="Gray9" id="GroupAdd">创建分组</a></div>
            </div>
            <div class="L30 G4 BrGray Pl10">
                <ul>
                    <li class="Pl10 L">
                        <img src="http://simg.instrument.com.cn/ilog/blue/images/men.gif" class="L MiNav_img" /><span
                            id="Friends_Count"></span></li>
                </ul>
                <div class="Hr_1">
                </div>
            </div>
            <div class="Hr_20">
            </div>
            <!--内容开始-->
            <div id="Friends_PageList">
            </div>
            <!--内容结束-->
            <!--页码开始-->
            <div class="page" id="Friends_Page">
                </ul>
            </div>
            <!--页码结束-->
        </div>
        <div class="WindowMenu2  L30" id="GroupMoreList_Menu" style="display: none; z-index: 3;">
            <ul id="MenuListUL">
            </ul>
            <div class=" Line_dashed">
            </div>
            <div>
                &nbsp;<img src="http://simg.instrument.com.cn/ilog/blue/images/ico-j1.gif" />
                <a href="javascript:void(0);" class="Gray9" id="GroupAddList">创建分组</a></div>
        </div>
        <!--互相关注结束------------------------------>
        <div class="Hr_1">
        </div>
    </div>
    <!--顶部文件开始-->
    <!--#include file="bottom.htm"-->
    <!--顶部文件结束-->
</body>
</html>
