<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="firststep.aspx.cs" Inherits="ILog.Web.verify.firststep" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>���ƻ�����Ϣ</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet" type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />

    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript" src="../js/jquery.cookie.js"></script>

    <script src="../js/Common.js" type="text/javascript"></script>

    <script src="../js/firststep.js" type="text/javascript"></script>

    <script src="../js/jquery.validate.min.js" type="text/javascript"></script>

    <script src="../js/lhgdialog.min.js" type="text/javascript"></script>

</head>
<body class="body">
    <!--�����ļ���ʼ-->
    <!--#include file="../top.htm"-->
    <!--�����ļ�����-->
    <div class="TopSpan">
    </div>
    <div class=" Area_B BrWh">
        <!--��߿�ʼ-->
        <div class="Llog L">

            <div class=" Llog_Nav L30">
                <div class="L30 P10 F14 G4">
                    �˻�����</div>
                <ul id="leftmenu">
                </ul>
            </div>
        </div>
        <!--��֤�ڶ�����ʼ-->
        <form id="form_input" method="post" action="firststep.aspx">
        <div class="RightWhite R">
            <div class="Hr_20">
            </div>
            <div>
            </div>
            <div class="Hr_10">
            </div>
            <h1 class="F14  G3 L40 Fa">
                iLog��֤</h1>
            <div class="attestationNav">
                <img src="http://simg.instrument.com.cn/ilog/blue/images/nav1.gif" alt="���ƻ�����Ϣ" /></div>
            <div class="P10" style="margin-left:20px">
                <p class="G9">
                    �������������<%=strVerityType %>���������Ʋ�ȷ����Ļ�����Ϣ���Ա���֤�ɹ����ܱ������˹�ע��<br />
                    �Ժ������޸ģ�������<a href="/settings/index.aspx" class="Blue">��������</a>���н����޸ġ�</p>
                <div class="Hr_20">
                </div>
                <div class="Hr_20">
                </div>
                <ul class="ListBD G4">
                    <li><span class="L">��&nbsp;&nbsp;&nbsp;&nbsp;�ƣ�</span><div class="L InpWidth">
                        <%=nickname %></div>
                    </li>
                    <li><span class="L">��&nbsp;&nbsp;&nbsp;&nbsp;����</span><div class="L InpWidth">
                        <%=realname %></div>
                    </li>
                    <li><span class="L">��ϵ���䣺</span><div class="L InpWidth Fa">
                        <%=email %></div>
                    </li>
                    <li><span class="L">��ϵ�ֻ���</span><div class="L InpWidth  Fa">
                        <%=mobile %></div>
                    </li>
                </ul>
                <div class="Hr_20">
                </div>
                <p class="L35 blue F14 P5">
                    ������Ϣ</p>
                <div class=" Line_dashed">
                </div>
                <div class="Hr_10">
                </div>
                <ul class="G4 L25 attestationList">
                    <%=strSchoolList %>
                </ul>
                <div class="Hr_20">
                </div>
                <p class="L35 blue F14 P5">
                    ְҵ��Ϣ</p>
                <div class=" Line_dashed">
                </div>
                <div class="Hr_10">
                </div>
                <ul class="G4 L30 attestationList">
                    <li class="F14">
                        <img src="http://simg.instrument.com.cn/ilog/blue/images/san_4.gif" />
                        <%=company %>&nbsp;&nbsp;&nbsp;&nbsp;<%=vctname %></li>
                </ul>
                <div class="Hr_20">
                </div>
                <div>
                    <p class="WinBtn L">
                        <span class="White">
                            <input name="btnSubmit" type="submit" id="btnSubmit" value="��һ��"  onclick="return CheckBaseInfo();"/></span>
                    </p>
                    <a href="index.aspx" class="Blue">������һ��</a></div>
            </div>
        </div>
        <!--��֤�ڶ����Ͻ���-->
        <input id="verityType" name="verityType" type="hidden" value="<%=intVerityType %>" />
        </form>
        <div class="Hr_1">
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
    <input id="hidPercent" type="hidden"  value="<%=percent %>"/>
    <!--�ײ���ʼ-->
     <!--#include file="/bottom.htm"-->


</body>
</html>
<%=infoScript %>