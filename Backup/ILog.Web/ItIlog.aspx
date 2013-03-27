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

    <!--用户基本信息.by lx on 20120626-->

    <script src="js/VipILogHome.js" type="text/javascript"></script>

    <!--悬浮用户信息.by lx on 20120626-->

    <script src="js/VipILogInfo.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="js/ItIlog.js"></script>

    <script language="javascript" type="text/javascript" src="js/jquery.cookie.js"></script>

    <!--表情插入-->

    <script src="js/jquery.insertContent.js" type="text/javascript"></script>

    <!--提示-->

    <script src="js/lhgdialog.min.js" type="text/javascript"></script>

    <script src="/js/rotate.js" type="text/javascript" charset="gb2312"></script>

    <script src="/js/jquery.query.js" type="text/javascript"></script>

    <script src="js/SendMail.js" type="text/javascript"></script>

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
            <div class="Llog_line" style="height:370px">
            </div>
              <div class="Hr_10">
            </div>
        </div>
        <!--左边结束-->
        <!--右边开始-->
        <!--#include file="right.htm"-->
        <!--右边结束-->
        <!--收到的评论开始------------------------------>
        <!--删除评论-->
        <input id="deleteIdHidden" type="hidden" value="" />
        <input id="sendCommentHiddent" type="hidden" value="0" />
        <input id="hidPageIndex" type="hidden" value="1" />
        <div class="Center L">
            <div class="Hr_20">
            </div>
            <div class="Hr_20">
            </div>
            <!--消息标题切换开始-->
            <div class=" MiNav">
                <ul class="L ul">
                    <li>
                        <div id="top_comment_my" class="top">
                        </div>
                        <div class="center F14" id="mycomment">
                            <a href="javascript:void(0);" onclick="commentTyle(1);" class="Blue"><strong><span
                                class="Fa">@</span>我的博文</strong> </a>
                        </div>
                    </li>
                    <li>
                        <div id="top_comment_post">
                        </div>
                        <div id="postcomment">
                            <a href="javascript:void(0);" onclick="commentTyle(0);" class="F14"><span class="Fa">
                                @</span>我的评论</a>
                        </div>
                    </li>
                </ul>
                <!--消息标题切换结束-->
                <div class="MISearch R" style="position: relative">
                    <input class="input_s G9 L" value="请输入昵称" onfocus="if(this.value=='请输入昵称')this.value=''"
                        onblur="if(!this.value)this.value='请输入昵称'" type="text" value="<%=string.IsNullOrEmpty(strKeyword) ? "请输入昵称" : strKeyword%>"
                        type="text" name="keyword_s" id="keyword_s" />
                    <input class="btn L" onclick="checkform();" type="image" src="http://simg.instrument.com.cn/ilog/blue/images/btn_s.gif"
                        alt="搜索按钮" />
                    <input type="hidden" id="ation_s" name="ation_s" value="1" />
                    <!--智能提示框容器-->
                    <ul class="WindowMenu WindowW Line_blue L30" id="GetSearchTowho_Menu" style="display: none;
                        top: 28px; left: 0px; width: 160px; z-index: 10;">
                    </ul>
                </div>
            </div>
            <!--数据条数开始-->
            <div id="RowsCount" class="G9 L25">
            </div>
            <!--数据条数结束-->
            <div class="Hr_20">
            </div>
            <!--@我的博文开始----------------->
            <!--列表容器-->
            <div id="list_div">
            </div>
            <!--@我的博文结束----------------->
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
