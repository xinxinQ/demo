<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nowilog.aspx.cs" Inherits="ILog.Web.nowilog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>��㿴��-���ڷ���</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet"
        type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />
    <link href="css/rotate.css" rel="stylesheet" type="text/css" />

    <script src="js/jquery-1.3.2.js" type="text/javascript" language="javascript"></script>

    <script language="javascript" type="text/javascript" src="js/jquery.cookie.js"></script>

    <script src="js/lhgdialog.min.js" type="text/javascript"></script>

    <script src="js/VipILogInfo.js" type="text/javascript"></script>

    <script src="js/Common.js" type="text/javascript" language="javascript"></script>

    <script src="js/nowilog.js" type="text/javascript" language="javascript"></script>

    <script src="js/rotate.js" type="text/javascript" charset="gb2312"></script>

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
    <div class=" Area BrWh">
        <div class="HofNav">
            <div class="HofNavB F14">
                <a href="/index.html" class="White B">��ҳ</a> | <a href="/fame.html" class="White  B">
                    ������</a> | <a href="/transmit.html" class="White  B">��㿴��</a></div>
        </div>
        <div class="P10 M10 ">
            <div class="CententNav">
                <ul>
                    <li><a href="/transmit.html" class="F14">����ת��</a></li>
                    <li><a href="/hotcomment.html" class="F14">��������</a></li>
                    <li>
                        <div class="top">
                        </div>
                        <div class="center">
                            <a href="/nowilog.html" class="Blue F14"><strong>���ڷ���</strong></a></div>
                    </li>
                </ul>
            </div>
            <div class="Line Look">
                <div class="CententBox L30">
                </div>
                <div class="LookL  L">
                    <div id="divContent">
                    </div>
                    <!--ҳ�뿪ʼ-->
                    <div id="sowhpage_div" class="page">
                    </div>
                    <!--ҳ�����-->
                </div>
                <input id="deleteIdHidden" type="hidden" value="" />
                <!--#include file="/everyright.htm"-->
                <div class="Hr_10">
                </div>
            </div>
        </div>
        <div class="Hr_1">
        </div>
    </div>
    <!--�ײ���ʼ-->
    <!--#include file="/bottom.htm"-->
    <!--�ײ�����-->
</body>
</html>
