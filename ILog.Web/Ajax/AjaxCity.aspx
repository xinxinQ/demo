<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxCity.aspx.cs" Inherits="ILog.Web.Ajax.AjaxCity" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择城市</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet" type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />

    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../js/common.js" type="text/javascript"></script>

    <script src="../js/city.js" type="text/javascript"></script>

    <style type="text/css">
        body
        {
            font-size: 12px;
            background: #F6F9FF;
        }
        #CategorySelector0
        {
            margin-left: 30px;
        }
        #CategorySelector0 ul
        {
            margin: 0 2px 0 0;
            padding: 0;
            border: 1px solid #CCC;
            float: left;
            width: 240px;
            height: 277px;
            overflow-x: hidden;
            overflow-y: auto;
        }
        #CategorySelector0 li
        {
            cursor: pointer;
            list-style-type: none;
            width: 220px;
            margin-right: 2px;
            padding-left: 5px;
            padding-top: 2px;
            padding-bottom: 0px;
            cursor: hand;
            text-align: left;
        }
        .Selected
        {
            background-color: #CAFFC0;
            border: 1px solid #0A9800;
            color: #006623;
            background-repeat: no-repeat;
            background-image: url(http://www.instrument.com.cn/show/manager/images/publishitem_subcate_arrow.gif);
            background-position: 98% 50%;
            margin-right: 2px;
            padding-left: 5px;
            padding-top: 2px;
            padding-bottom: 0px;
        }

    </style>
</head>
<body>
    <div style="width: 100%; height: 100%">
        <h1 class="WindowTil G4 F14">
            <a href="javascript:window.parent.closeCityDialog(0,'','','',0,0,0);">
                <img class=" R Img" src="http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif"
                    alt="关闭" /></a>修改地区</h1>
        <div class="Hr_20">
        </div>
        <form id="form_CItyP" name="form_CItyP" method="post" action="AjaxCity.aspx">
        <div class="Hr_20">
        </div>
        <%=strbHtml.ToString() %>
        </form>
    </div>
    <%=infoScript%>
</body>
</html>
