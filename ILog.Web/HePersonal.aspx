<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HePersonal.aspx.cs" Inherits="ILog.Web.HePersonal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>他人主页</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet"
        type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />
    <link href="/css/rotate.css" rel="stylesheet" type="text/css" />

    <script src="js/jquery-1.3.2.js" type="text/javascript" language="javascript"></script>

    <script src="js/rotate.js" type="text/javascript" charset="gb2312"></script>

    <script language="javascript" type="text/javascript" src="js/jquery.cookie.js"></script>

    <script src="js/Common.js" type="text/javascript" language="javascript"></script>

    <script src="js/SendMail.js" type="text/javascript"></script>

    <script src="js/VipILogInfo.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript" src="js/HePersonal.js"></script>

    <script src="js/lhgdialog.min.js" type="text/javascript"></script>

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
            <div class=" Llog_Head">
                <div id="headInfo">
                </div>
                <div class="Hr_6">
                </div>
            </div>
            <input id="userid" type="hidden" value="<%=userid %>" />
            <input id="nikename" type="hidden" value="" />
            <!--发送评论时用到.by lx on 20120716-->
            <input id="sendCommentHiddent" type="hidden" value="0" />
            <div class=" Llog_Nav L30">
                <div class="Hr_20">
                </div>
                <ul id="leftmenu">
                </ul>
            </div>
            <div class="Hr_10">
            </div>
            <div class="Llog_line" style="height:370px">
            </div>
              <div class="Hr_10">
            </div>
        </div>
        <!--删除评论-->
        <input id="deleteIdHidden" type="hidden" value="" />
        <!--左边结束-->
        <!--右边开始-->
        <!--#include file="taright.htm"-->
        <!--右边结束-->
        <div class="Center L">
            <div class="Hr_20">
            </div>
            <div class="Line_blue BrBlue P10 G3" id="personalInfo">
            </div>
            <br />
            <div id="HpersionConcern">
            </div>
            <div class="Hr_20">
            </div>
            <!--切换标签开始-->
            <div class="CententNav">
                <ul>
                    <li id="all_il">
                        <div class="top">
                        </div>
                        <div class="center">
                            <a href="javascript:void(0);" onclick="ListTyle(0);" class="Blue"><strong>全部</strong></a></div>
                    </li>
                    <li id="Original_li"><a href="javascript:void(0);" onclick="ListTyle(1);">博文</a>
                    </li>
                </ul>
            </div>
            <!--切换标签结束-->
            <div class="CententBox L30">
                <span class="R Pl10"></span>
                <div class="MISearch R">
                    <input class="input_s G9 L" style="margin-top: 2px" value="请输入内容" onfocus="if(this.value=='请输入内容')this.value=''"
                        onblur="if(!this.value)this.value='请输入内容'" type="text" name="keyword_s" id="keyword_s" />
                    <input class="btn L" name="" style="margin-top: 2px" type="image" src="http://simg.instrument.com.cn/ilog/blue/images/btn_s.gif"
                        onclick="checkform();" name="keyword_s" id="keyword_s" alt="搜索按钮" />
                    <input type="hidden" id="ation_s" name="ation_s" value="" />
                </div>
                <a href="#" class="Blue">&nbsp;</a>
            </div>
            <div class="Hr_20">
            </div>
            <!--数据列表开始-->
            <div id="list_div">
            </div>
            <!--数据列表结束-->
            <!--页码开始-->
            <div id="sowhpage_div" class="page">
            </div>
            <!--页码结束-->
        </div>
        <div class="Hr_1">
        </div>
    </div>
    <!--底部开始-->
    <!--#include file="bottom.htm"-->
    <!--底部结束-->
</body>
</html>
