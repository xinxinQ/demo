<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxSchool.aspx.cs" Inherits="ILog.Web.Ajax.AjaxSchool" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择学校</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet" type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />

    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../js/common.js" type="text/javascript"></script>

    <script src="../js/school.js" type="text/javascript"></script>

</head>
<body>
    <div style="width: 100%; height: 100%">
        <h1 class="WindowTil G4 F14">
            <a href="javascript:window.parent.closeSchoolDialog(0);">
                <img class=" R Img" src="http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif"
                    alt="关闭" /></a>教育信息</h1>
        <div class="Hr_20">
        </div>
        <form id="form_SchoolP" name="form_SchoolP" method="post" action="AjaxSchool.aspx">
        <%=strbHtml.ToString() %>
        <input id="schoolid" name="schoolid" type="hidden" value="<%=schoolid %>" />
        </form>
    </div>
    <%=infoScript%>
</body>
</html>
