<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxMessagesDel.aspx.cs" Inherits="ILog.Web.Ajax.AjaxMessagesDel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet" type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet" type="text/css" />
    

    
</head>
<body>
<div style="width:100%;height:100%">
 <%-- <h1 class="WindowTil G4 F14"><a href="#"><img class=" R Img" src="http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif" alt="关闭"  /></a>删除</h1>--%>
  <div class="WindowBox Tc">
    <div class=" G4 Tc F14 L30 WindowSak"><span class="L">确定要删除该站短&nbsp;</span> <img src="http://simg.instrument.com.cn/ilog/blue/images/ask.gif" class="L" /></div>
    <div class="Hr_10"></div>
    
<div class="WinBtn_H R"><a href="#" ><span><input name="取消"  onclick="window.parent.closeDivlog();" type="button" id="取消" value="取消" /></span></a></div>
<div class="WinBtn  R"><a href="#"  class="White"><span><input name="确定" onclick="window.parent.VipMailDel_a(<%=id %>,<%=fromwhoid %>,<%=towhoid %>)" type="button" id="确定" value="确定" /></span></a></div>
    <div class="Hr_10"></div>
  </div>
</div>
</body>
</html>
