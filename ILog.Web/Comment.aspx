<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Comment.aspx.cs" Inherits="ILog.Web.Comment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet"
        type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />

    <script language="javascript" type="text/javascript" src="js/jquery-1.3.2.js"></script>

    <script language="javascript" type="text/javascript" src="js/common.js"></script>

    <script src="js/VipILogHome.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="js/comment.js"></script>

    <script language="javascript" type="text/javascript" src="js/jquery.cookie.js"></script>

    <script language="javascript" src="js/lhgdialog.min.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="js/VipILogInfo.js"></script>

    <script src="js/SendMail.js" type="text/javascript"></script>

    <script src="js/ilogcheck.js" type="text/javascript"></script>

</head>
<body class="body">
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
        <!--#include file="cright.htm"-->
        <!--�ұ߽���-->
        <!--�յ������ۿ�ʼ------------------------------>
        <div class="Center L">
            <div class="Hr_20">
            </div>
            <div class="Hr_20">
            </div>
            <div class=" MiNav">
                <ul class="L ul">
                    <!--��Ϣ�����л���ʼ-->
                    <li>
                        <div id="top_comment_my" class="top">
                        </div>
                        <div class="center F14" id="mycomment">
                            <a href="javascript:void(0);" onclick="commentTyle(1);" class="Blue"><strong>�յ�������</strong>
                            </a>
                        </div>
                    </li>
                    <li>
                        <div id="top_comment_post">
                        </div>
                        <div id="postcomment">
                            <a href="javascript:void(0);" onclick="commentTyle(0);" class="F14">����������</a>
                        </div>
                    </li>
                    <!--��Ϣ�����л�����-->
                </ul>
                <!--С������ʼ-->
                <div class="MISearch R" style="position: relative">
                    <input class="input_s G9 L" value="�������ǳ�" onfocus="if(this.value=='�������ǳ�')this.value=''"
                        onblur="if(!this.value)this.value='�������ǳ�'" type="text" value="<%=string.IsNullOrEmpty(strKeyword) ? "�������ǳ�" : strKeyword%>"
                        type="text" name="keyword_s" id="keyword_s" maxlength="15" />
                    <input class="btn L" onclick="checkform();" type="image" id="bnt_s" src="http://simg.instrument.com.cn/ilog/blue/images/btn_s.gif"
                        alt="������ť" />
                    <input type="hidden" id="ation_s" name="ation_s" value="1" />
                    <!--������ʾ������-->
                    <ul class="WindowMenu WindowW Line_blue L30" id="GetSearchTowho_Menu" style="display: none;
                        top: 28px; left: 0px; width: 160px; z-index: 10;">
                    </ul>
                    <!--���½�����-->
                    <input type="hidden" name="prevTrIndex" id="prevTrIndex" value="-1" />
                </div>
                <!--С��������-->
            </div>
            <!--��������������ʼ-->
            <div class="G9 L25" id="RowsCount">
            </div>
            <!--����������������-->
            <!--Ĭ�ϵ��б�����ǩ-->
            <div class="Hr_20" id="hr_d">
            </div>
            <!--���������б�-->
            <span style="float: left;" id="div_all"></span>
            <div class="Hr_20" id="Hr_20_1" style="display: none">
            </div>
            <div class="Hr_20" id="Hr_20_2" style="display: none">
            </div>
            <div class="Cl">
            </div>
            <!--�յ����ۿ�ʼ-------------->
            <!--����������ʼ-->
            <div id="commentlist_div">
            </div>
            <!--������������-->
            <!--ҳ�뿪ʼ-->
            <div id="sowhpage_div" class="page">
            </div>
            <!--ҳ�����-->
        </div>
        <!--�յ������۽���------------------------------>
        <!--��������ʱ�õ�.by lx on 20120716-->
        <input id="sendCommentHiddent" type="hidden" value="0" />
        <!--��ǰҳ��-->
        <input id="currentIndex" type="hidden" value="1" />
        <div class="Hr_1">
        </div>
    </div>
    <!--�ײ���ʼ-->
    <!--#include file="bottom.htm"-->
    <!--�ײ�����-->
</body>
</html>
