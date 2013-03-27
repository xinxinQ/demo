<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxChooseSchool.aspx.cs"
    Inherits="ILog.Web.Ajax.AjaxChooseSchool" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>学校名称搜索</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet" type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />

    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../js/Common.js" type="text/javascript"></script>

    <script src="../js/chooseschool.js" type="text/javascript"></script>

    <style type="text/css">
        #CategorySelector0
        {
            margin-left: 0px;
        }
        #CategorySelector0 ul
        {
            border: 1px solid #CCC;
            width: 700px;
            height: 277px;
            overflow-x: hidden;
            overflow-y: auto;
            list-style: none;
            padding: 0px 0px 0px 0px;
        }
        #CategorySelector0 li
        {
            cursor: pointer;
            width: 220px;
            line-height: 40px;
            display: inline;
            float: left;
        }
        .Selected
        {
            background-color: #CAFFC0;
            float: left;
            color: #006623;
        }
    </style>
</head>
<body>
    <div style="width: 100%; height: 100%">
        <h1 class="WindowTil G4 F14">
            <a href="javascript:window.parent.closeColledgeDialog();">
                <img class=" R Img" src="http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif"
                    alt="关闭" /></a>搜索学校</h1>
        <div class="Hr_20">
        </div>
        <div class="P10" style="width: 670px; height: 400px;">
            <ul class="ListBD G4">
                <li><span class="L">学校所在地：</span><div class="L InpWidth">
                    <select id="selProvince">
                        <%=selProvince%>
                    </select>
                </div>
                </li>
            </ul>
            <div class="Hr_1">
            </div>
            <ul id="CategorySelector0">
            </ul>
            <div class="Hr_20">
            </div>
            <div class="Tc">
                <div class="WinBtn">
                    <span>
                        <input style="cursor: pointer" type="submit" name="btnClose" class="ui_close" value="关 闭"
                            onclick="window.parent.closeColledgeDialog();" /></span>
                </div>
            </div>
            <div class="Hr_20">
            </div>
        </div>
    </div>
</body>
</html>
