<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fame.aspx.cs" Inherits="ILog.Web.Fame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>名人堂</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet"
        type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />

    <script src="js/jquery-1.3.2.js" type="text/javascript" language="javascript"></script>

    <script src="js/jquery.cookie.js" type="text/javascript"></script>

    <script src="js/Common.js" type="text/javascript" language="javascript"></script>

    <script src="js/Fame.js" type="text/javascript" language="javascript"></script>

    <script src="js/VipILogInfo.js" type="text/javascript"></script>

    <script src="js/SendMail.js" type="text/javascript"></script>

    <script src="js/lhgdialog.min.js" type="text/javascript"></script>
    
    <script src="js/ilogcheck.js" type="text/javascript"></script>

    <script src="js/pager.js" type="text/javascript"></script>

</head>
<body class="bodyHof">
    <!--顶部文件开始-->
    <!--顶部文件开始-->
    <!--#include file="top.htm"-->
    <!--顶部文件结束-->
    <div class="TopSpan">
    </div>
    <!--顶部文件结束-->
    <!--名人堂头部开始-->
    <div class="Area BrWh">
        <div class="HofTop ">
            <a href="/verify/">
                <img src="http://simg.instrument.com.cn/ilog/blue/images/hof_logo.gif" alt="我要认证"
                    class="L" /></a>
            <div class="SeaBoxBj L Sea">
                <input class="SeaInp L" type="text" name="txtKeyword" id="txtKeyword" />
                <input name="btnSearch" type="image" class="L" id="btnSearch" onclick="GetFameList(1,20);return false;" value="提交" src="http://simg.instrument.com.cn/ilog/blue/images/btn_search.gif"
                    alt="搜索" />
            </div>
            <div class="Hr_10">
            </div>
            <div class="HofNav">
                <div class="HofNavB F14">
                    <a href="index.html" class="White B">首页</a> | <a href="fame.html" class="White  B">名人堂</a>
                    | <a href="Transmit.html" class="White  B">随便看看</a></div>
            </div>
        </div>
        <div class="Line P10 M10">
            <div class="Hr_10">
            </div>
            <div id="divContent">
            </div>
            <!--页码开始-->
            <div class="page">
                <span class="L Pl10">
                    <input name="btnConcern" type="image" src="http://simg.instrument.com.cn/ilog/blue/images/btn_Gz.gif" onclick="ConcernThem();" /></span>
                <span class="L Pl10 G9"><a href="javascript:CheckAllFame();" class="Blue">全选</a> | <a href="javascript:RevAllFame();" class="Blue">反选</a></span>
                <span id="sowhpage_div"></span>
                <div class="Hr_10">
                </div>
            </div>
            <!--页码结束-->
            <div class="Hr_10">
            </div>
        </div>
    </div>
    <div>
        <div class="Hr_10">
        </div>
    </div>
    <!--名人堂头部结束-->
    <!--底部开始-->

    <script src="js/copyright.js" type="text/javascript"></script>

    <!--底部结束-->
</body>
</html>
