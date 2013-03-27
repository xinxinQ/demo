<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ILog.Web.settings.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>个人资料</title>
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
    <!--顶部文件开始-->
    <!--#include file="../top.htm"-->
    <!--顶部文件结束-->
    <div class="TopSpan">
    </div>
    <div class="TopSpan">
    </div>
    <div class=" Area_B BrWh">
        <!--左边开始-->
        <div class="Llog L">
            <div class=" Llog_Nav L30">
                <div class="L30 P10 F14 G4">
                    账户设置</div>
                <ul id="leftmenu">
                </ul>
            </div>
        </div>
        <!--左边结束-->
        <!--个人资料开始-->
        <div class="RightWhite R">
            <form id="form_input" method="post" action="index.aspx">
            <div class="Hr_20">
            </div>
            <div class="G3 L35">
                <h1 class="F14 L">
                    个人资料</h1>
                &nbsp; (<span class="Red">*</span> 必须填写项)</div>
            <div class="Line_solid">
            </div>
            <p class="L35 blue F14 P5">
                基本信息</p>
            <ul class="ListBD G4">
                <li><span class="Span L"><font class="Red F12">*</font> 昵称：</span><input type="text"
                    class="input" size="35" name="vip_nickname" id="vip_nickname" value="<%=nickname %>"
                    maxlength="10" /></li>
                <li><span class="Span L">姓名：</span><input type="text" class="input" size="35" name="name"
                    id="name" value="<%=realname %>" /></li>
                <li><span class="Span L"><font class="Red F12">*</font> 性别：</span><%=radMale %>男&nbsp;&nbsp;
                    <%=radFemale %>女 </li>
                <li><span class="Span L">生日：</span><select name="selYear" id="selYear" style="width: 70px"><option
                    value="0">&nbsp;</option>
                    <%=strYearList %></select>
                    年
                    <select name="selMonth" id="selMonth" style="width: 50px">
                        <%=strMonthList %>
                    </select>
                    月
                    <select name="selDay" id="selDay" style="width: 50px">
                        <%=strDayList%>
                    </select>
                    日</li>
                <li style="display: none"><span class="Span L"><font class="Red F12">*</font> 单位类别：</span><div
                    class="L InpWidth">
                    <a class="Blue R" href="javascript:ChangeJob();">修改</a><input id="vccid" name="vccid"
                        type="hidden" value="<%=vccid %>" /></div>
                </li>
                <li style="display: none"><span class="Span L"><font class="Red F12">*</font> 行业类别：</span><div
                    class="L InpWidth">
                    <input id="vcfid" name="vcfid" type="hidden" value="<%=vcfid %>" /></div>
                </li>
                <li><span class="Span L"><font class="Red F12">*</font> 地区：</span><div class="L InpWidth">
                    <a class="Blue R" href="javascript:ChangeRegion();">修改</a><span id="region"><%=cityName%></span><input
                        id="cityid" name="cityid" type="hidden" value="<%=cityid %>" /><input id="provinceid"
                            name="provinceid" type="hidden" value="<%=provinceid %>" /><input id="countryid"
                                name="countryid" type="hidden" value="<%=countryid %>" /></div>
                </li>
                <li><span class="Span L">单位名称：</span><input type="text" class="input" size="35" name="company"
                    id="company" value="<%=company %>" />
                </li>
                <li><span class="Span L"><font class="Red F12">*</font> 职位：</span><div class="L InpWidth">
                    <a class="Blue R" href="javascript:ChangeJob();">修改</a><span id="vccname"><%=vccname %></span>
                    > <span id="vcfname">
                        <%=vcfname%></span> > <span id="vctname">
                            <%=vctname%></span><input id="vctid" name="vctid" type="hidden" value="<%=vctid %>" /></div>
                </li>
                <li><span class="Span L">通讯地址：</span><input name="address" id="address" type="text"
                    class="input" size="35" value="<%=address %>" /></li>
                <li><span class="Span L"><font class="Red F12">* </font>联系邮箱：</span><div class="L InpWidth Fa">
                    <a class="Blue R" href="javascript:editemail();">修改</a><span id="email"><%=email %></span></div>
                </li>
                <li><span class="Span L  Fa">QQ：&nbsp;</span><input name="qq" id="qq" type="text"
                    class="input" size="35" value="<%=qq %>" /></li>
                <li><span class="Span L  Fa">MSN：</span><input name="msn" id="msn" type="text" class="input"
                    size="35" value="<%=msn %>" /></li>
                <li><span class="Span L Fa">固定电话：</span><input name="tel" id="tel" type="text" class="input"
                    size="35" value="<%=tel %>" /></li>
                <%=mobileHtml %>
            </ul>
            <div class="Line_ilog">
            </div>
            <p class="L35 blue F14 P5">
                <span class="R">
                    <input name="btnAddSchool" type="image" id="btnAddSchool" value="提交" src="http://simg.instrument.com.cn/ilog/blue/images/btn_add.gif"
                        alt="添加" onclick="ChangeSchool(0);return false;" /></span> 教育背景</p>
            <table width="95%" border="0" align="center" cellpadding="0" cellspacing="0" class=" ListBD Table"
                id="tblSchool">
                <tr>
                    <td height="36" class="nav G6">
                        <strong>学校类型 </strong>
                    </td>
                    <td class="nav  G6">
                        <strong>学校名称 </strong>
                    </td>
                    <td class="nav  G6">
                        <strong>入学年份</strong>
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
                        <input name="btnSubmit" type="submit" id="btnSubmit" value="保存修改" /></span></div>
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
        <!--个人资料结束-->
        <div class="Hr_1">
        </div>
    </div>
    <!--底部开始-->
    <!--#include file="/bottom.htm"-->
    <!--底部结束-->
</body>
</html>
<%=infoScript %>