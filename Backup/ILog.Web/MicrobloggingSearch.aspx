<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MicrobloggingSearch.aspx.cs" Inherits="ILog.Web.MicrobloggingSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
 
<title></title>

<link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet" type="text/css" />
<link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet" type="text/css" />
<!--ͼƬ����-->
<link href="css/rotate.css" rel="stylesheet" type="text/css" />

<script language="javascript" type="text/javascript" src="js/jquery-1.3.2.js"></script>
<script language="javascript" type="text/javascript" src="js/common.js"></script>
<script language="javascript" type="text/javascript" src="js/MicrobloggingSearch.js"></script>
<script language="javascript" type="text/javascript" src="js/jquery.cookie.js"></script>
<script src="js/lhgdialog.min.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript" src="js/VipILogInfo.js"></script>

<!--ͼƬ����-->
<script src="js/rotate.js" type="text/javascript" charset="gb2312"></script>


 <script language="javascript" type="text/javascript" src="js/ilogcheck.js"></script>
 
</head>
 
<body class="body">
    <!--�����ļ���ʼ-->
    <!--#include file="top.htm"-->
<div class="TopSpan"></div>
<div class="TopSpan"></div>
    <!--�����ļ�����-->
<!--�����ļ�����-->
<div class=" Area BrWh" >
<div class="SearchTop"><img src="http://simg.instrument.com.cn/ilog/blue/images/search1.gif" class="L" />
<div class=" SeaBox L">


<!--������ʼ-->
<div class="SeaBoxBj" style="position:relative">
  <input class="SeaInp L" type="text" value="�����������ؼ���" onclick="return LoginDiv(16);" onfocus="if(this.value=='�����������ؼ���')this.value=''" onblur="if(!this.value)this.value='�����������ؼ���'"  type="text" value="<%=string.IsNullOrEmpty(strKeyword) ? "�����������ؼ���" : strKeyword%>" type="text" name="keyword_s" id="keyword_s" maxlength="20"  />
  <input name="button" type="image" onclick="checkform(0);" class="L" id="bnt_ss" value="�ύ" src="http://simg.instrument.com.cn/ilog/blue/images/btn_search.gif" alt="����" />
  <div class=" Cl"></div>
  
      <!--������ʾ������-->
      <ul class="WindowMenu WindowW Line_blue" id="GetSearchTowho_Menu" style="display:none; top:33px; left:4px; z-index:10; width:425px;">
      

      </ul>
  
</div>
<!--��������-->  


    <div class="Hr_1"></div>
    
    
<!--��������������ʼ-->
<div id="RowsCount" class="Tr G9"></div>
<!--����������������-->


</div>
<%--<div class="L L35 Pl10">+<a href="#">���Ĺؼ���</a></div>--%>
 
</div>
<div class="Cl"></div>


<!--��߿�ʼ-->
<div class="Llog L">
<div class="Hr_20"></div>
<div class="SearchLeft">
<div class=" Llog_Nav L30">

<!--�����л���ʼ-->
<ul>
<li ><a href="javascript:void(0);" class="Blue F14" onclick="LeftMenu(0)" ><span class="ico14"></span>����</a></li>
<li class="liv"><a class="F14 White" href="javascript:void(0);" onclick="LeftMenu(1)" ><span class="ico13"></span>����</a></li>

<!--����������߲˵�ʹ��-->
<input id="ation_s" name="ation_s" type="hidden" value="<%=strKeyword %>" />

<!--�����ؼ���-->
<input id="keyword_h" name="keyword_h" type="hidden" value="<%=strKeyword %>" />

</ul>
<!--�����л�����-->

<%--<div class="Hr_10"></div>
<div class=" Line_ilog"></div>
<ul>
<li><a href="#">������ʷ </a><img src="http://simg.instrument.com.cn/ilog/blue/images/SanJ.gif" width="9" height="6" /></li>
<li>���������ʷ</li>

</ul>--%>
<div class="Hr_10"></div>
</div></div>
</div><!--��߽���-->
<!--�ұ߿�ʼ-->
<div class="R SearchRig">
<div class="Hr_10"></div>
 <!--#include file="/everyright.htm"-->
</div>

<!--�ұ߽���-->
 
 
<!--�����¿�ʼ------------------------------>
<div class="L SearchCen">
<DIV class="Hr_20"></DIV>

<!--ƥ���û���ʼ-->
<div id="Personal_s_div">

</div>
<!--ƥ���û�����-->

 <!--��������ʱ�õ�.by lx on 20120716--> 
 <input id="sendCommentHiddent" type="hidden" value="0"/>

<!--�����б�ʼ-->
<div id="list_div">


</div>
<!--�����б����-->
 
<!--������ʼ-->
<div class="SeaBoxBj" style="position:relative">
  <input class="SeaInp L" type="text" value="�����������ؼ���" onclick="return LoginDiv(16);" onfocus="if(this.value=='�����������ؼ���')this.value=''" onblur="if(!this.value)this.value='�����������ؼ���'" type="text" value="<%=string.IsNullOrEmpty(strKeyword) ? "�����������ؼ���" : strKeyword%>" type="text" name="keyword_s2" id="keyword_s2" maxlength="20"  />
  <input name="button" type="image" onclick="checkform(1);" class="L" id="bnt_s2" value="�ύ" src="http://simg.instrument.com.cn/ilog/blue/images/btn_search.gif" alt="����" />
  <div class=" Cl"></div>
  
  
    <!--������ʾ������-->
    <ul class="WindowMenu WindowW Line_blue"  id="GetSearchTowho_Menu2" style="display:none; top:33px; left:4px; z-index:10;width:425px; ">


    </ul>
  
  
</div>
<!--��������-->


<div class="Hr_20"></div>
 
<!--��Ϣ�б�ʼ----------------->
<!--��Ϣ�б����----------------->
 
 
<!--ҳ�뿪ʼ-->
<div id="sowhpage_div" class="page">


</div>
<!--ҳ�����-->


</div>
<!--�յ������۽���------------------------------>
<div class="Hr_1"></div>
</div>
<!--�ײ���ʼ-->
<div class="bottom Tc">
<div class="Hr_10">
</div>
<a href="#" class="White" >��������</a> - 
<a href="#"class="White">��Ŀ����</a> - 
<a href="#" class="White">��ƸӢ��</a> - 
<a href="#"class="White">VIP��Ա����</a> - 
<a href="#"class="White" >�ͻ�Ͷ��</a> - 
<a href="#" class="White">��������</a> - 
<a href="#" class="White">��������</a> - 
<a href="#" class="White">��ϵ����</a> - 
<a href="#"class="White">������̬</a>
<br />
Instrument.com.cn Copyright<span class="Fa">&copy; </span>
1999-2012,All Rights Reserved
��Ȩ����</div>
<!--�ص�����JS-->
<script type="text/javascript"><!--
var w = 90;
var h = 100;
var str = "";
var obj = document.getElementById("divStayTopLeft");
if (obj)str = obj.innerHTML;
if( typeof document.compatMode != 'undefined' && document.compatMode != 'BackCompat'){
document.writeln('<DIV  style="z-index:9;right:0;bottom:0;height:'+h+'px;width:'+w+'px;overflow:hidden;POSITION:fixed;_position:absolute; _margin-top:expression(document.documentElement.clientHeight-this.style.pixelHeight+document.documentElement.scrollTop);">');
}
else {
document.writeln('<DIV  style="z-index:9;right:0;bottom:0;height:'+h+'px;width:'+w+'px;overflow:hidden;POSITION:fixed;*position:absolute; *top:expression(eval(document.body.scrollTop)+eval(document.body.clientHeight)-this.style.pixelHeight);">');
}
document.writeln('<div style="clear:both;height:60px;"><a href="javascript:scroll(0,0)" hidefocus="true"><img src="http://simg.instrument.com.cn/ilog/blue/images/top_img.png" alt="�ص�����" style="border: 0px;" /></a></div> ');
document.write('<div style="clear:both;margin:auto;overflow:hidden;text-align:left;">'+str+'</div>');
document.writeln('</DIV>');
-->
</script>
<script src="js/MSIE.PNG.js"></script>
 
<!--�ײ�����-->
</body>
</html>