<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fame.aspx.cs" Inherits="ILog.Web.Fame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>������</title>
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
    <!--�����ļ���ʼ-->
    <!--�����ļ���ʼ-->
    <!--#include file="top.htm"-->
    <!--�����ļ�����-->
    <div class="TopSpan">
    </div>
    <!--�����ļ�����-->
    <!--������ͷ����ʼ-->
    <div class="Area BrWh">
        <div class="HofTop ">
            <a href="/verify/">
                <img src="http://simg.instrument.com.cn/ilog/blue/images/hof_logo.gif" alt="��Ҫ��֤"
                    class="L" /></a>
            <div class="SeaBoxBj L Sea">
                <input class="SeaInp L" type="text" name="txtKeyword" id="txtKeyword" />
                <input name="btnSearch" type="image" class="L" id="btnSearch" onclick="GetFameList(1,20);return false;" value="�ύ" src="http://simg.instrument.com.cn/ilog/blue/images/btn_search.gif"
                    alt="����" />
            </div>
            <div class="Hr_10">
            </div>
            <div class="HofNav">
                <div class="HofNavB F14">
                    <a href="index.html" class="White B">��ҳ</a> | <a href="fame.html" class="White  B">������</a>
                    | <a href="Transmit.html" class="White  B">��㿴��</a></div>
            </div>
        </div>
        <div class="Line P10 M10">
            <div class="Hr_10">
            </div>
            <div id="divContent">
            </div>
            <!--ҳ�뿪ʼ-->
            <div class="page">
                <span class="L Pl10">
                    <input name="btnConcern" type="image" src="http://simg.instrument.com.cn/ilog/blue/images/btn_Gz.gif" onclick="ConcernThem();" /></span>
                <span class="L Pl10 G9"><a href="javascript:CheckAllFame();" class="Blue">ȫѡ</a> | <a href="javascript:RevAllFame();" class="Blue">��ѡ</a></span>
                <span id="sowhpage_div"></span>
                <div class="Hr_10">
                </div>
            </div>
            <!--ҳ�����-->
            <div class="Hr_10">
            </div>
        </div>
    </div>
    <div>
        <div class="Hr_10">
        </div>
    </div>
    <!--������ͷ������-->
    <!--�ײ���ʼ-->

    <script src="js/copyright.js" type="text/javascript"></script>

    <!--�ײ�����-->
</body>
</html>
