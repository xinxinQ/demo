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
        <!--#include file="cright.htm"-->
        <!--右边结束-->
        <!--收到的评论开始------------------------------>
        <div class="Center L">
            <div class="Hr_20">
            </div>
            <div class="Hr_20">
            </div>
            <div class=" MiNav">
                <ul class="L ul">
                    <!--消息标题切换开始-->
                    <li>
                        <div id="top_comment_my" class="top">
                        </div>
                        <div class="center F14" id="mycomment">
                            <a href="javascript:void(0);" onclick="commentTyle(1);" class="Blue"><strong>收到的评论</strong>
                            </a>
                        </div>
                    </li>
                    <li>
                        <div id="top_comment_post">
                        </div>
                        <div id="postcomment">
                            <a href="javascript:void(0);" onclick="commentTyle(0);" class="F14">发出的评论</a>
                        </div>
                    </li>
                    <!--消息标题切换结束-->
                </ul>
                <!--小搜索开始-->
                <div class="MISearch R" style="position: relative">
                    <input class="input_s G9 L" value="请输入昵称" onfocus="if(this.value=='请输入昵称')this.value=''"
                        onblur="if(!this.value)this.value='请输入昵称'" type="text" value="<%=string.IsNullOrEmpty(strKeyword) ? "请输入昵称" : strKeyword%>"
                        type="text" name="keyword_s" id="keyword_s" maxlength="15" />
                    <input class="btn L" onclick="checkform();" type="image" id="bnt_s" src="http://simg.instrument.com.cn/ilog/blue/images/btn_s.gif"
                        alt="搜索按钮" />
                    <input type="hidden" id="ation_s" name="ation_s" value="1" />
                    <!--智能提示框容器-->
                    <ul class="WindowMenu WindowW Line_blue L30" id="GetSearchTowho_Menu" style="display: none;
                        top: 28px; left: 0px; width: 160px; z-index: 10;">
                    </ul>
                    <!--上下建索引-->
                    <input type="hidden" name="prevTrIndex" id="prevTrIndex" value="-1" />
                </div>
                <!--小搜索结束-->
            </div>
            <!--搜索数据条数开始-->
            <div class="G9 L25" id="RowsCount">
            </div>
            <!--搜索数据条数结束-->
            <!--默认的列表间隔标签-->
            <div class="Hr_20" id="hr_d">
            </div>
            <!--返回所有列表-->
            <span style="float: left;" id="div_all"></span>
            <div class="Hr_20" id="Hr_20_1" style="display: none">
            </div>
            <div class="Hr_20" id="Hr_20_2" style="display: none">
            </div>
            <div class="Cl">
            </div>
            <!--收到评论开始-------------->
            <!--评论容器开始-->
            <div id="commentlist_div">
            </div>
            <!--评论容器结束-->
            <!--页码开始-->
            <div id="sowhpage_div" class="page">
            </div>
            <!--页码结束-->
        </div>
        <!--收到的评论结束------------------------------>
        <!--发送评论时用到.by lx on 20120716-->
        <input id="sendCommentHiddent" type="hidden" value="0" />
        <!--当前页码-->
        <input id="currentIndex" type="hidden" value="1" />
        <div class="Hr_1">
        </div>
    </div>
    <!--底部开始-->
    <!--#include file="bottom.htm"-->
    <!--底部结束-->
</body>
</html>
