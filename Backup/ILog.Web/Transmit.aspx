<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Transmit.aspx.cs" Inherits="ILog.Web.Transmit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>随便看看-热门转发</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet"
        type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />
    <link href="css/rotate.css" rel="stylesheet" type="text/css" />

    <script src="js/jquery-1.4.2.min.js" type="text/javascript" language="javascript"></script>

    <script language="javascript" type="text/javascript" src="js/jquery.cookie.js"></script>

    <script src="js/common.js" type="text/javascript" language="javascript"></script>

    <script src="js/lhgdialog.min.js" type="text/javascript"></script>

    <script src="js/VipILogInfo.js" type="text/javascript"></script>

    <script src="js/transmit.js" type="text/javascript" language="javascript"></script>

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
    <div class=" Area BrWh">
        <div class="HofNav">
            <div class="HofNavB F14">
                <a href="index.html" class="White B">首页</a> | <a href="fame.html" class="White  B">名人堂</a>
                | <a href="transmit.html" class="White  B">随便看看</a></div>
        </div>
        <div class="P10 M10 ">
            <div class="CententNav">
                <ul>
                    <li>
                        <div class="top">
                        </div>
                        <div class="center">
                            <a href="#" class="Blue F14"><strong>热门转发</strong></a></div>
                    </li>
                    <li><a href="hotComment.html" class="F14">热门评论</a> </li>
                    <li><a href="nowilog.html" class="F14">正在发生</a></li>
                </ul>
            </div>
            <div class="Line Look">
                <div class="CententBox L30">
                    <span class="R Pl10">&nbsp;</span><a href="javascript:changeType(0);" id="hrefDay"><strong>今日热门转发</strong></a>
                    <span class="G9">|</span> <a href="javascript:changeType(1);" class="Blue" id="hrefWeek">
                        一周热门转发</a></div>
                <div class="LookL  L">
                    <div id="divContent">
                    </div>
                    <!--页码开始-->
                    <div id="sowhpage_div" class="page">
                    </div>
                    <!--页码结束-->
                </div>
                <!--删除评论-->
                <input id="deleteIdHidden" type="hidden" value="" />
                
                  <!--#include file="/everyright.htm"-->
                <div class="Hr_10">
                </div>
            </div>
        </div>
        <div class="Hr_1">
        </div>
    </div>

       <!--底部开始-->
    <!--#include file="bottom.htm"-->
    <!--底部文件结束-->
</body>
</html>
