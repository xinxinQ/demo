<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessagesContent.aspx.cs"
    Inherits="ILog.Web.MessagesContent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet"
        type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />

    <script src="js/jquery-1.3.2.js" type="text/javascript" language="javascript"></script>

    <script language="javascript" type="text/javascript" src="js/common.js"></script>

    <script language="javascript" type="text/javascript" src="js/MessagesContent.js"></script>

    <script language="javascript" type="text/javascript" src="js/ReplyMail.js"></script>

    <script language="javascript" type="text/javascript" src="js/jquery.cookie.js"></script>

    <script src="js/lhgdialog.min.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="js/SendMail.js"></script>

    <script language="javascript" type="text/javascript" src="js/VipILogInfo.js"></script>

    <!--�û�������Ϣ.by lx on 20120626-->

    <script src="js/VipILogHome.js" type="text/javascript"></script>

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
    <div class=" Area_B BrWh">
        <!--��߿�ʼ-->
        <div class="Llog L">
            <div class="Hr_20">
            </div>
            <!--�û���Ϣ��ʼ-->
            <div id="vipIlog">
            </div>
            <!--�û���Ϣ����-->
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
        </div>
        <!--��߽���-->
        <!--�ұ߿�ʼ-->
        <!--#include file="right.htm"-->
        <!--�ұ߽���-->
        <!--��Ϣ���ݿ�ʼ------------------------------>
        <div class="Center L">
            <div class="Hr_20">
            </div>
            <div class="Hr_20">
            </div>
            <div class="Concern_Tit">
                <!--������ʼ-->
                <div class="MISearch R" style="position: relative">
                    <input class="input_s G9 L" type="text" value="�������ǳ�" onfocus="if(this.value=='�������ǳ�')this.value=''"
                        onblur="if(!this.value)this.value='�������ǳ�'" type="text" value="�������ǳ�" type="text"
                        name="keyword_s" id="keyword_s" />
                    <input name="bnt_s" id="bnt_s" type="image" onclick="checkform();" class="btn L"
                        id="Image1" value="�ύ" src="http://simg.instrument.com.cn/ilog/blue/images/btn_s.gif"
                        alt="����" />
                    <input type="hidden" id="id_" name="id_" value="" />
                    <!--������ʾ������-->
                    <ul class="WindowMenu WindowW Line_blue L30" id="GetSearchTowho_Menu" style="display: none;
                        top: 29px; left: 0px; z-index: 10;">
                    </ul>
                    <!--���½�����-->
                    <input type="hidden" name="prevTrIndex" id="prevTrIndex" value="-1" />
                </div>
                <!--��������-->
                <a href="javascript:void(0);" onclick="showdialog(2);">
                    <img src="http://simg.instrument.com.cn/ilog/blue/images/btn_fs.gif" alt="��վ��" class="R messages_btn" /></a>
                <!--�����˹�ϵ��ʼ-->
                <font class="L30 G3"><b class="F14">�Һ� <a id="tohow_a" href="#"></a>�ĶԻ� </b>
                    <!--������-->
                    <input type="hidden" id="tohow_h" name="tohow_h" />
                    <!--վ�̹�ϵid-->
                    <input type="hidden" id="id_a" name="id_a" value="<%=id %>" />
                </font>
            </div>
            <!--�����˹�ϵ��ʼ-->
            <div class="Hr_5">
            </div>
            <div class="Line">
            </div>
            <div class="messagesNav">
                <input type="image" name="button" onclick="GoMailDel()" src="http://simg.instrument.com.cn/ilog/blue/images/btn_del.gif"
                    alt="����ɾ��" class="R btn" id="button" value="�ύ" />
                <ul>
                    <li class="blueli"><a href="Messages.aspx" class="Blue">��������վ��</a></li>
                    <li>
                        <img src="http://simg.instrument.com.cn/ilog/blue/images/messages_nav2.gif" /></li>
                    <li>
                        <!--վ��������ʼ-->
                        <span id="RowsCount"></span>
                        <!--վ����������-->
                    </li>
                    <li>
                        <img src="http://simg.instrument.com.cn/ilog/blue/images/messages_nav.gif" /></li>
                </ul>
            </div>
            <div class="Hr_20">
            </div>
            <div class="messages_top">
                <div class="info G4  L24 ">
                    <span id="prompt" class="R">����������500��</span><img src="http://simg.instrument.com.cn/ilog/blue/images/ico-1.gif"
                        alt="����" class="L" />
                    ��վ�̸���<span id="tohow_a2"></span></div>
                <!--�ظ��ı���ʼ-->
                <div class="RIGHTBOX_top L">
                    <div class="Mound1">
                        <div class="Mound2">
                            <div class="Mound3" style="padding: 0">
                                <p class="F12 G6 L22 P10">
                                    <textarea class="messages_textarea" name="content_i" onkeyup="checkSendMail('content_i',2)"
                                        id="content_i" cols="45" rows="5"></textarea>
                                </p>
                                <div class="Sound_bottom">
                                    <!--�ύ��ť��ʼ-->
                                    <div id="btnImg_div">
                                        <div class="WinBtn  R">
                                            <a id="btnImg" href="javascript:void(0);" onclick="SendMail(2);" class="White"><span>
                                                ����</span></a>
                                        </div>
                                    </div>
                                    <!--�ύ��ť����-->
                                    <%--    <ul class="ICOlist">˵
<li><span class="ico1"></span><a href="#" class="Blue">����</a></li>
</ul>--%></div>
                            </div>
                        </div>
                        <span class="Mound_spanTop">
                            <img src="http://simg.instrument.com.cn/ilog/blue/images/san_5.gif" /></span>
                    </div>
                </div>
                <!--ͷ������-->
                <div class="LEFTBOX R">
                    <a id="face_a" href="javascript:void(0);"></a>
                </div>
                <!--�ظ��ı������-->
            </div>
            <div class="Hr_20">
            </div>
            <!--�������ݿ�ʼ-->
            <div id="list_div">
            </div>
            <div class="Hr_10">
            </div>
            <!--��Ϣ���ݽ���-->
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
