<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FollowClass.aspx.cs" Inherits="ILog.Web.FollowClass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>�ҹ�ע���� iLog-������Ϣ��-��¼��ߵĵ��</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet"
        type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />

    <script src="js/jquery-1.3.2.js" type="text/javascript" language="javascript"></script>

    <script src="js/lhgdialog.min.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="js/jquery.cookie.js"></script>

    <script src="js/VipILogInfo.js" type="text/javascript"></script>

    <script src="js/Common.js" type="text/javascript" language="javascript"></script>

    <script src="js/FollowClass.js" type="text/javascript" language="javascript"></script>

    <script src="js/SendMail.js" type="text/javascript" language="javascript"></script>

    <script src="js/ilogcheck.js" type="text/javascript"></script>

</head>
<body class="body">
    <input type="hidden" name="followID" id="followID" value="<%=IcgID%>" />
    <input type="hidden" name="followName" id="followName" value="" />
    <!--�����ļ���ʼ-->
    <!--#include file="top.htm"-->
    <!--�����ļ�����-->
    <div class="TopSpan">
    </div>
    <div class="TopSpan">
    </div>
    <div class=" Area_B BrWh">
        <!--��߿�ʼ-->
        <div class="Llog L">
            <div class=" Llog_Nav L30">
                <div class="Hr_10">
                </div>
                <div class="L30 P10 F14 G4">
                    ��ע/��˿</div>
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
        <!--��߽���-->
        <!--�ұ߿�ʼ-->
        <!--#include file="CRight.htm"-->
        <!--�ұ߽���-->
        <!--�����ע��ʼ------------------------------>
        <div class="Center L">
            <div class="Hr_20">
            </div>
            <div class="Concern_Tit">
                <h1 class="F14 L30 G3">
                    <div class="MISearch R" style="position: relative">
                        <input class="input_s G9 L" value="�������ǳ�" onfocus="if(this.value=='�������ǳ�')this.value=''"
                            onblur="if(!this.value)this.value='�������ǳ�'" type="text" name="txtFollowtSearch"
                            id="txtFollowtSearch" maxlength="20" />
                        <input class="btn L" name="SearchFollow" id="SearchFollow" type="image" src="http://simg.instrument.com.cn/ilog/blue/images/btn_s.gif"
                            alt="������ť" />
                        <ul class="WindowMenu  Window450 Line_blue L30 More" id="GetFollow_Menu" style="position: absolute;
                            z-index: 3; top: 30px; left: 0px; width: 140px; display: none;">
                        </ul>
                    </div>
                    �ҹ�ע��<span id="spanConcern">0</span>��</h1>
            </div>
            <div class=" MiNav" style="position: relative">
                <div class=" R">
                    <div class="Hr_5">
                    </div>
                    <div class="WinBtn " style="margin: 0">
                        <span>
                            <input name="GroupAddP" type="button" id="GroupAddP" value="+ ��������" /></span></div>
                </div>
                <ul class="ul" style="width: 430px; z-index: 3;" id="FollowMenu">
                </ul>
            </div>
            <div class="WindowMenu2  L30" id="GroupMore_Menu" style="z-index: 3; display: none;
                position: relative">
                <ul id="MenuUL">
                </ul>
                <div class=" Line_dashed">
                </div>
                <div>
                    &nbsp;<img src="http://simg.instrument.com.cn/ilog/blue/images/ico-j1.gif" />
                    <a href="javascript:void(0);" class="Gray9" id="GroupAdd">��������</a></div>
            </div>
            <div class="L30 G4 BrGray Pl10" id="FollowClass_Group">
                <ul id="PageSort">
                    <li class="Pl10 L">
                        <img src="http://simg.instrument.com.cn/ilog/blue/images/ico_edie.png" class="L MiNav_img" /><a
                            href="javascript:">�༭����</a></li>
                    <li class="Pl10 L">
                        <img src="http://simg.instrument.com.cn/ilog/blue/images/ico_mistake.png" class="L MiNav_img" /><a
                            href="javascript:">ɾ������</a></li>
                </ul>
                <div class="Hr_1">
                </div>
            </div>
            <div class="Hr_20">
            </div>
            <div id="FollowClass_PageList">
            </div>
            <!--ҳ�뿪ʼ-->
            <div class="page" id="FollowClass_Page">
            </div>
            <!--ҳ�����-->
        </div>
        <!--�����ע����------------------------------>
        <div class="Hr_1">
        </div>
    </div>
    <!--�ײ���ʼ-->
    <!--#include file="bottom.htm"-->
    <!--�ײ�����-->
</body>
</html>
