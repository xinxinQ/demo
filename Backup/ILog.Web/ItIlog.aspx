<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItIlog.aspx.cs" Inherits="ILog.Web.ItBlog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet"
        type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />
    <link href="css/rotate.css" rel="stylesheet" type="text/css" />

    <script src="js/jquery-1.3.2.js" type="text/javascript" language="javascript"></script>

    <script src="/js/jquery.curpos.js" type="text/javascript"></script>

    <script src="/js/jquery.cursorposition.js" type="text/javascript"></script>

    <script src="js/Common.js" type="text/javascript" language="javascript"></script>

    <!--�û�������Ϣ.by lx on 20120626-->

    <script src="js/VipILogHome.js" type="text/javascript"></script>

    <!--�����û���Ϣ.by lx on 20120626-->

    <script src="js/VipILogInfo.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="js/ItIlog.js"></script>

    <script language="javascript" type="text/javascript" src="js/jquery.cookie.js"></script>

    <!--�������-->

    <script src="js/jquery.insertContent.js" type="text/javascript"></script>

    <!--��ʾ-->

    <script src="js/lhgdialog.min.js" type="text/javascript"></script>

    <script src="/js/rotate.js" type="text/javascript" charset="gb2312"></script>

    <script src="/js/jquery.query.js" type="text/javascript"></script>

    <script src="js/SendMail.js" type="text/javascript"></script>

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
            <div class="Llog_line" style="height:370px">
            </div>
              <div class="Hr_10">
            </div>
        </div>
        <!--��߽���-->
        <!--�ұ߿�ʼ-->
        <!--#include file="right.htm"-->
        <!--�ұ߽���-->
        <!--�յ������ۿ�ʼ------------------------------>
        <!--ɾ������-->
        <input id="deleteIdHidden" type="hidden" value="" />
        <input id="sendCommentHiddent" type="hidden" value="0" />
        <input id="hidPageIndex" type="hidden" value="1" />
        <div class="Center L">
            <div class="Hr_20">
            </div>
            <div class="Hr_20">
            </div>
            <!--��Ϣ�����л���ʼ-->
            <div class=" MiNav">
                <ul class="L ul">
                    <li>
                        <div id="top_comment_my" class="top">
                        </div>
                        <div class="center F14" id="mycomment">
                            <a href="javascript:void(0);" onclick="commentTyle(1);" class="Blue"><strong><span
                                class="Fa">@</span>�ҵĲ���</strong> </a>
                        </div>
                    </li>
                    <li>
                        <div id="top_comment_post">
                        </div>
                        <div id="postcomment">
                            <a href="javascript:void(0);" onclick="commentTyle(0);" class="F14"><span class="Fa">
                                @</span>�ҵ�����</a>
                        </div>
                    </li>
                </ul>
                <!--��Ϣ�����л�����-->
                <div class="MISearch R" style="position: relative">
                    <input class="input_s G9 L" value="�������ǳ�" onfocus="if(this.value=='�������ǳ�')this.value=''"
                        onblur="if(!this.value)this.value='�������ǳ�'" type="text" value="<%=string.IsNullOrEmpty(strKeyword) ? "�������ǳ�" : strKeyword%>"
                        type="text" name="keyword_s" id="keyword_s" />
                    <input class="btn L" onclick="checkform();" type="image" src="http://simg.instrument.com.cn/ilog/blue/images/btn_s.gif"
                        alt="������ť" />
                    <input type="hidden" id="ation_s" name="ation_s" value="1" />
                    <!--������ʾ������-->
                    <ul class="WindowMenu WindowW Line_blue L30" id="GetSearchTowho_Menu" style="display: none;
                        top: 28px; left: 0px; width: 160px; z-index: 10;">
                    </ul>
                </div>
            </div>
            <!--����������ʼ-->
            <div id="RowsCount" class="G9 L25">
            </div>
            <!--������������-->
            <div class="Hr_20">
            </div>
            <!--@�ҵĲ��Ŀ�ʼ----------------->
            <!--�б�����-->
            <div id="list_div">
            </div>
            <!--@�ҵĲ��Ľ���----------------->
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
