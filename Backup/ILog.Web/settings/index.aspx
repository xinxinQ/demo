<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ILog.Web.settings.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>��������</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet" type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>

    <script language="javascript" type="text/javascript" src="../js/jquery.cookie.js"></script>

    <script src="../js/jquery.validate.min.js" type="text/javascript"></script>

    <script src="../js/lhgdialog.min.js" type="text/javascript"></script>

    <script src="../js/Common.js" type="text/javascript"></script>

    <script src="../js/settindex.js" type="text/javascript"></script>

    <script src="../js/ilogcheck.js" type="text/javascript"></script>

</head>
<body class="body">
    <!--�����ļ���ʼ-->
    <!--#include file="../top.htm"-->
    <!--�����ļ�����-->
    <div class="TopSpan">
    </div>
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
        <!--��߽���-->
        <!--�������Ͽ�ʼ-->
        <div class="RightWhite R">
            <form id="form_input" method="post" action="index.aspx">
            <div class="Hr_20">
            </div>
            <div class="G3 L35">
                <h1 class="F14 L">
                    ��������</h1>
                &nbsp; (<span class="Red">*</span> ������д��)</div>
            <div class="Line_solid">
            </div>
            <p class="L35 blue F14 P5">
                ������Ϣ</p>
            <ul class="ListBD G4">
                <li><span class="Span L"><font class="Red F12">*</font> �ǳƣ�</span><input type="text"
                    class="input" size="35" name="vip_nickname" id="vip_nickname" value="<%=nickname %>"
                    maxlength="10" /></li>
                <li><span class="Span L">������</span><input type="text" class="input" size="35" name="name"
                    id="name" value="<%=realname %>" /></li>
                <li><span class="Span L"><font class="Red F12">*</font> �Ա�</span><%=radMale %>��&nbsp;&nbsp;
                    <%=radFemale %>Ů </li>
                <li><span class="Span L">���գ�</span><select name="selYear" id="selYear" style="width: 70px"><option
                    value="0">&nbsp;</option>
                    <%=strYearList %></select>
                    ��
                    <select name="selMonth" id="selMonth" style="width: 50px">
                        <%=strMonthList %>
                    </select>
                    ��
                    <select name="selDay" id="selDay" style="width: 50px">
                        <%=strDayList%>
                    </select>
                    ��</li>
                <li style="display: none"><span class="Span L"><font class="Red F12">*</font> ��λ���</span><div
                    class="L InpWidth">
                    <a class="Blue R" href="javascript:ChangeJob();">�޸�</a><input id="vccid" name="vccid"
                        type="hidden" value="<%=vccid %>" /></div>
                </li>
                <li style="display: none"><span class="Span L"><font class="Red F12">*</font> ��ҵ���</span><div
                    class="L InpWidth">
                    <input id="vcfid" name="vcfid" type="hidden" value="<%=vcfid %>" /></div>
                </li>
                <li><span class="Span L"><font class="Red F12">*</font> ������</span><div class="L InpWidth">
                    <a class="Blue R" href="javascript:ChangeRegion();">�޸�</a><span id="region"><%=cityName%></span><input
                        id="cityid" name="cityid" type="hidden" value="<%=cityid %>" /><input id="provinceid"
                            name="provinceid" type="hidden" value="<%=provinceid %>" /><input id="countryid"
                                name="countryid" type="hidden" value="<%=countryid %>" /></div>
                </li>
                <li><span class="Span L">��λ���ƣ�</span><input type="text" class="input" size="35" name="company"
                    id="company" value="<%=company %>" />
                </li>
                <li><span class="Span L"><font class="Red F12">*</font> ְλ��</span><div class="L InpWidth">
                    <a class="Blue R" href="javascript:ChangeJob();">�޸�</a><span id="vccname"><%=vccname %></span>
                    > <span id="vcfname">
                        <%=vcfname%></span> > <span id="vctname">
                            <%=vctname%></span><input id="vctid" name="vctid" type="hidden" value="<%=vctid %>" /></div>
                </li>
                <li><span class="Span L">ͨѶ��ַ��</span><input name="address" id="address" type="text"
                    class="input" size="35" value="<%=address %>" /></li>
                <li><span class="Span L"><font class="Red F12">* </font>��ϵ���䣺</span><div class="L InpWidth Fa">
                    <a class="Blue R" href="javascript:editemail();">�޸�</a><span id="email"><%=email %></span></div>
                </li>
                <li><span class="Span L  Fa">QQ��&nbsp;</span><input name="qq" id="qq" type="text"
                    class="input" size="35" value="<%=qq %>" /></li>
                <li><span class="Span L  Fa">MSN��</span><input name="msn" id="msn" type="text" class="input"
                    size="35" value="<%=msn %>" /></li>
                <li><span class="Span L Fa">�̶��绰��</span><input name="tel" id="tel" type="text" class="input"
                    size="35" value="<%=tel %>" /></li>
                <%=mobileHtml %>
            </ul>
            <div class="Line_ilog">
            </div>
            <p class="L35 blue F14 P5">
                <span class="R">
                    <input name="btnAddSchool" type="image" id="btnAddSchool" value="�ύ" src="http://simg.instrument.com.cn/ilog/blue/images/btn_add.gif"
                        alt="���" onclick="ChangeSchool(0);return false;" /></span> ��������</p>
            <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0" class=" ListBD Table"
                id="tblSchool">
                <tr>
                    <td height="36" class="nav G6">
                        <strong>ѧУ���� </strong>
                    </td>
                    <td class="nav  G6">
                        <strong>ѧУ���� </strong>
                    </td>
                    <td class="nav  G6">
                        <strong>��ѧ���</strong>
                    </td>
                    <td class="nav  G6">
                        &nbsp;
                    </td>
                </tr>
                <%=strSchoolList%>
            </table>
            <br />
            <br />
            <br />
            <div class="Tc">
                <div class="WinBtn">
                    <span>
                        <input name="btnSubmit" type="submit" id="btnSubmit" value="�����޸�" /></span></div>
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
            <br />
            <br />
            <br />
            <br />
            <br />
            </form>
        </div>
        <!--�������Ͻ���-->
        <div class="Hr_1">
        </div>
    </div>
    <!--�ײ���ʼ-->
    <!--#include file="/bottom.htm"-->
    <!--�ײ�����-->
</body>
</html>
<%=infoScript %>