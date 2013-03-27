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

    <!--用户基本信息.by lx on 20120626-->

    <script src="js/VipILogHome.js" type="text/javascript"></script>

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
            <div class="Hr_20">
            </div>
            <!--用户信息开始-->
            <div id="vipIlog">
            </div>
            <!--用户信息结束-->
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
        <!--左边结束-->
        <!--右边开始-->
        <!--#include file="right.htm"-->
        <!--右边结束-->
        <!--信息内容开始------------------------------>
        <div class="Center L">
            <div class="Hr_20">
            </div>
            <div class="Hr_20">
            </div>
            <div class="Concern_Tit">
                <!--搜索开始-->
                <div class="MISearch R" style="position: relative">
                    <input class="input_s G9 L" type="text" value="请输入昵称" onfocus="if(this.value=='请输入昵称')this.value=''"
                        onblur="if(!this.value)this.value='请输入昵称'" type="text" value="请输入昵称" type="text"
                        name="keyword_s" id="keyword_s" />
                    <input name="bnt_s" id="bnt_s" type="image" onclick="checkform();" class="btn L"
                        id="Image1" value="提交" src="http://simg.instrument.com.cn/ilog/blue/images/btn_s.gif"
                        alt="搜索" />
                    <input type="hidden" id="id_" name="id_" value="" />
                    <!--智能提示框容器-->
                    <ul class="WindowMenu WindowW Line_blue L30" id="GetSearchTowho_Menu" style="display: none;
                        top: 29px; left: 0px; z-index: 10;">
                    </ul>
                    <!--上下建索引-->
                    <input type="hidden" name="prevTrIndex" id="prevTrIndex" value="-1" />
                </div>
                <!--搜索结束-->
                <a href="javascript:void(0);" onclick="showdialog(2);">
                    <img src="http://simg.instrument.com.cn/ilog/blue/images/btn_fs.gif" alt="发站短" class="R messages_btn" /></a>
                <!--发件人关系开始-->
                <font class="L30 G3"><b class="F14">我和 <a id="tohow_a" href="#"></a>的对话 </b>
                    <!--收信人-->
                    <input type="hidden" id="tohow_h" name="tohow_h" />
                    <!--站短关系id-->
                    <input type="hidden" id="id_a" name="id_a" value="<%=id %>" />
                </font>
            </div>
            <!--发件人关系开始-->
            <div class="Hr_5">
            </div>
            <div class="Line">
            </div>
            <div class="messagesNav">
                <input type="image" name="button" onclick="GoMailDel()" src="http://simg.instrument.com.cn/ilog/blue/images/btn_del.gif"
                    alt="批量删除" class="R btn" id="button" value="提交" />
                <ul>
                    <li class="blueli"><a href="Messages.aspx" class="Blue">返回所有站短</a></li>
                    <li>
                        <img src="http://simg.instrument.com.cn/ilog/blue/images/messages_nav2.gif" /></li>
                    <li>
                        <!--站短条数开始-->
                        <span id="RowsCount"></span>
                        <!--站短条数结束-->
                    </li>
                    <li>
                        <img src="http://simg.instrument.com.cn/ilog/blue/images/messages_nav.gif" /></li>
                </ul>
            </div>
            <div class="Hr_20">
            </div>
            <div class="messages_top">
                <div class="info G4  L24 ">
                    <span id="prompt" class="R">还可以输入500字</span><img src="http://simg.instrument.com.cn/ilog/blue/images/ico-1.gif"
                        alt="信箱" class="L" />
                    发站短给：<span id="tohow_a2"></span></div>
                <!--回复文本域开始-->
                <div class="RIGHTBOX_top L">
                    <div class="Mound1">
                        <div class="Mound2">
                            <div class="Mound3" style="padding: 0">
                                <p class="F12 G6 L22 P10">
                                    <textarea class="messages_textarea" name="content_i" onkeyup="checkSendMail('content_i',2)"
                                        id="content_i" cols="45" rows="5"></textarea>
                                </p>
                                <div class="Sound_bottom">
                                    <!--提交按钮开始-->
                                    <div id="btnImg_div">
                                        <div class="WinBtn  R">
                                            <a id="btnImg" href="javascript:void(0);" onclick="SendMail(2);" class="White"><span>
                                                发送</span></a>
                                        </div>
                                    </div>
                                    <!--提交按钮结束-->
                                    <%--    <ul class="ICOlist">说
<li><span class="ico1"></span><a href="#" class="Blue">表情</a></li>
</ul>--%></div>
                            </div>
                        </div>
                        <span class="Mound_spanTop">
                            <img src="http://simg.instrument.com.cn/ilog/blue/images/san_5.gif" /></span>
                    </div>
                </div>
                <!--头像容器-->
                <div class="LEFTBOX R">
                    <a id="face_a" href="javascript:void(0);"></a>
                </div>
                <!--回复文本域结束-->
            </div>
            <div class="Hr_20">
            </div>
            <!--发件数据开始-->
            <div id="list_div">
            </div>
            <div class="Hr_10">
            </div>
            <!--信息内容结束-->
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
