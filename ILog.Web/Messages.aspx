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

    <!--�û�������Ϣ.by lx on 20120626-->

    <script src="js/VipILogHome.js" type="text/javascript"></script>

    <script src="js/ilogcheck.js" type="text/javascript"></script>

</head>
<body class="body">
    <!--�����ļ���ʼ-->
    <!--#include file="top.htm"-->
    <!--�����ļ�����-->
    <!--ͷ�����-->
    <div class="TopSpan">
    </div>
    <div class="TopSpan">
    </div>
    <!--ͷ�����-->
    <div class=" Area_B BrWh">
        <!--��߿�ʼ-->
        <div class="Llog L">
            <div class="Hr_20">
            </div>
            <!--�û���Ϣ��ʼ-->
            <div id="vipIlog">
            </div>
            <!--�û���Ϣ����-->
            <!--��߲˵���ʼ-->
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
            <!--��߲˵�����-->
        </div>
        <!--��߽���-->
        <!--�ұ߿�ʼ-->
        <!--#include file="right.htm"-->
        <!--�ұ߽���-->
        <!--�յ������ۿ�ʼ------------------------------>
        <div class="Center L">
            <div class="Hr_20">
            </div>
            <div class="Hr_20">
            </div>
            <div class="Concern_Tit">
                <!--������ť��ʼ-->
                <div class="MISearch R" style="position: relative">
                    <input class="input_s G9 L" type="text" value="�������ǳ�" onfocus="if(this.value=='�������ǳ�')this.value=''"
                        onblur="if(!this.value)this.value='�������ǳ�'" type="text" value="�������ǳ�" type="text"
                        name="keyword_s" id="keyword_s" />
                    <input name="bnt_s" type="image" onclick="checkform();" class="btn L" id="bnt_s"
                        value="�ύ" src="http://simg.instrument.com.cn/ilog/blue/images/btn_s.gif" alt="����" />
                    <input type="hidden" id="keyword_a" name="keyword_a" value="<%=strKeyword %>" />
                    <!--������ʾ������-->
                    <ul class="WindowMenu WindowW Line_blue L30" id="GetSearchTowho_Menu" style="display: none;
                        top: 29px; left: 0px; z-index: 20;">
                    </ul>
                    <!--���½�����-->
                    <input type="hidden" name="prevTrIndex" id="prevTrIndex" value="-1" />
                </div>
                <!--������ť����-->
                <a href="javascript:void(0);" onclick="showdialog(1);" id="sendmail_a">
                    <img src="http://simg.instrument.com.cn/ilog/blue/images/btn_fs.gif" alt="��վ��" class="R messages_btn" /></a>
                <font class="L30 G3"><b class="F14"><span id="myamil_a">�ҵ�վ��</span></b>
                    <!--��ϵ������ʼ-->
                    <span id="RowsCount"></span>&nbsp;&nbsp;&nbsp;
                    <!--��ϵ��������-->
                </font>
            </div>
            <div class="Hr_5">
            </div>
            <div class="Line">
            </div>
            <div class="Hr_20">
            </div>
            <!--���������б�-->
            <span style="float: left;" id="div_all"></span>
            <div class="Hr_20" id="Hr_20_1" style="display: none">
            </div>
            <div class="Hr_20" id="Hr_20_2" style="display: none">
            </div>
            <div class="Cl">
            </div>
            <!--�������-->
            <!--��Ϣ�б�ʼ----------------->
            <div id="list_div">
            </div>
            <!--��Ϣ�б����----------------->
            <!--ҳ�뿪ʼ-->
            <div id="sowhpage_div" class="page">
            </div>
            <!--ҳ�����-->
        </div>
        <!--�յ������۽���------------------------------>
        <div class="Hr_1">
        </div>
    </div>
    <!--�ײ���ʼ-->
    <!--#include file="bottom.htm"-->
    <!--�ײ�����-->
</body>
</html>
