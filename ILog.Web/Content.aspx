<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Content.aspx.cs" Inherits="ILog.Web.Content" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>博文内页</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet"
        type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />
    <link href="css/rotate.css" rel="stylesheet" type="text/css" />

    <script src="js/jquery-1.3.2.js" type="text/javascript" language="javascript"></script>

    <script language="javascript" type="text/javascript" src="js/jquery.cookie.js"></script>

    <script src="/js/jquery.query.js" type="text/javascript"></script>

    <script src="js/lhgdialog.min.js" type="text/javascript"></script>

    <script src="js/Common.js" type="text/javascript" language="javascript"></script>

    <script src="js/VipILogInfo.js" type="text/javascript"></script>

    <script src="js/content.js" type="text/javascript"></script>

    <script src="js/rotate.js" type="text/javascript" charset="gb2312"></script>

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
        <!--左边开始-->
        <div class="Llog L">
            <div class="Hr_20">
            </div>
            <div class=" Llog_Head">
                <!--用户信息-->
                <div id="headInfo">
                </div>
                <div class="Hr_6">
                </div>
            </div>
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
        <!--左边结束-->
        <!--右边开始-->
        <!--右边开始-->
        <!--#include file="iright.htm"-->
        <!--右边结束-->
        <input id="iloguserid" type="hidden" />
        <div class="Center L">
            <div class="Hr_20">
            </div>
            <div class="Hr_20">
            </div>
            <div class="Centent">
                <div class="Hr_1">
                </div>
            </div>
            <div class="Centent">
                <div>
                    <p class="F14 G6 L26">
                        <span class="L" id="conNickName"></span><span class="L Pl10" id="conCertification">
                        </span>
                        <div class="L Pl10">
                    </p>
                </div>
            </div>
            <div id="contentOriginal">
            </div>
            <div class="Line_ilog">
            </div>
            <!--接收参数-->
            <input id="ioidHidden" type="hidden" value="<%=ioId %>" />
            <input id="actionHidden" type="hidden" value="<%=actionType %>" />
            <!--原创还是转发类型.by lx on 20120719-->
            <input id="contentTypeHidden" type="hidden" value="0" />
            <!--发送评论时用到(评论Id).by lx on 20120719-->
            <input id="sendCommentHiddent" type="hidden" value="0" />
            <!--评论开始--------------------------------->
            <div class="CententNav">
                <ul>
                    <li id="commentTagId"><a href="javascript:void(0);" class="Blue" id="conCommentId"
                        onclick="checkTag(0)"></a> </li>
                    <li id="forwardTagId"></li>
                </ul>
            </div>
            <textarea class="CenCom_text" id="conTextarea" name="conTextarea" cols="" rows=""
                onpropertychange="if(value.length>140) value=value.substr(0,140)">
            </textarea>
            <div>
                <div class="Hr_10">
                </div>
                <div class="WinBtn  R">
                    <span id="commSubmit"></span>
                </div>
                <div class="ICOlist L" style="position: relative;">
                    <ul>
                        <li onmouseover="this.style.cursor='pointer'" onclick="document.location='#'"><span
                            class="ico1"></span><a href="javascript:void(0);" class="Blue" id="faceId" onclick="changeExpressio(this,'conTextarea');">
                                表情</a></li>
                    </ul>
                </div>
            </div>
            <div class="Hr_20">
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
            <!--评论结束--------------------------------->
            <br />
            <br />
            <br />
            <br />
        </div>
    </div>
    <div class="Hr_10">
    </div>
    </div>
    <!--底部开始-->
    <!--#include file="bottom.htm"-->
    <!--底部结束-->
</body>
</html>
