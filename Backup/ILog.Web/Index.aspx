<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ILog.Web.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>�㳡</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet"
        type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />
    <link rel="stylesheet" type="text/css" href="http://simg.instrument.com.cn/menu/101110/topmenu.css" />

    <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script src="js/lhgdialog.min.js" type="text/javascript"></script>

    <script src="js/jquery.cookie.js" type="text/javascript"></script>

    <script src="js/Common.js" type="text/javascript" language="javascript"></script>

    <script src="js/index.js" type="text/javascript" language="javascript"></script>

    <script type="text/javascript" src="js/jquery-easing-1.3.pack.js"></script>

    <script type="text/javascript" src="js/jquery-easing-compatibility.1.2.pack.js"></script>

    <script type="text/javascript" src="js/coda-slider.1.1.1.pack.js"></script>

    <script type="text/javascript" src="js/ilogcheck.js"></script>

    <style type="text/css">
        /*
	UTILITY STYLES
*/.clear
        {
            clear: both;
        }
        a
        {
            outline: none;
        }
        /*
	PAGE STRUCTURE
*/#page-wrap
        {
            width: 300px;
            margin: 25px auto;
            position: relative;
            min-height: 285px;
            background: url(images/bg.png) top center;
        }
        /*
	TYPOGRAPHY
*/ul
        {
            list-style: square inside;
        }
        a, a:visited
        {
            color: #729dff;
            text-decoration: none;
        }
        a:hover, a:active
        {
        }
        blockquote
        {
            padding: 0 20px;
            margin-left: 20px;
            border-left: 20px solid #ccc;
            font-size: 14px;
            font-family: Georgia, serif;
            font-style: italic;
            margin-top: 10px;
        }
        /*
	SLIDER
*/.slider-wrap
        {
            width: 300px;
            height: 285px;
        }
        .stripViewer .panelContainer .panel ul
        {
            text-align: left;
            margin: 0 15px 0 30px;
        }
        .stripViewer
        {
            position: relative;
            overflow: hidden;
            width: 300px;
        }
        .stripViewer .panelContainer
        {
            position: relative;
            left: 0;
            top: 0;
        }
        .stripViewer .panelContainer .panel
        {
            float: left;
            height: 100%;
            position: relative;
            width: 350px;
        }
        .stripNavL, .stripNavR, .stripNav
        {
            display: none;
        }
        .nav-thumb
        {
            border: 1px solid black;
            margin-right: 5px;
            width: 53px;
        }
        #movers-row
        {
            margin: 3px 0 0 0px;
            width: 300px;
        }
        #movers-row div
        {
            width: 20%;
            float: left;
        }
        #movers-row div a.cross-link
        {
            float: right;
        }
        .photo-meta-data
        {
            background: url(images/transpBlack.png);
            padding: 10px;
            height: 30px;
            margin-top: -50px;
            position: relative;
            z-index: 9999;
            color: white;
        }
        .photo-meta-data span
        {
            font-size: 13px;
        }
        .cross-link
        {
            display: block;
            margin-top: -14px;
            position: relative;
            padding-top: 20px;
            z-index: 10;
        }
        .active-thumb
        {
            background: transparent url(images/icon-uparrowsmallwhite.png) top center no-repeat;
        }
        .indexrow
        {
            margin: 0 auto;
            height: auto;
            z-index: 100;
            overflow: hidden;
        }
    </style>
</head>
<body>
    <!--�����ļ���ʼ-->
    <!--#include file="top.htm"-->
    <!--�����ļ�����-->
    <div class="TopSpan">
    </div>
    <div class="TopSpan">
    </div>
    <!--�㳡��ʼ-->
    <div class="Area">
        <div class="SquareNav  Fw ">
            <img src="http://simg.instrument.com.cn/ilog/blue/images/Nav01.gif" class="L" /><a
                href="Index.html" class="Blue">�� ҳ </a>|<a href="Fame.html" class="Blue" target="_blank">
                    ������</a> | <a href="Transmit.html" class="Blue" target="_blank">��㿴��</a><img src="http://simg.instrument.com.cn/ilog/blue/images/Nav03.gif"
                        class="R" /></div>
        <div class="Hr_10">
        </div>
        <div class="SquareL L">
            <div class="Info1">
                <div class="leftbox L">
                    <div class="slider-wrap">
                        <div id="main-photo-slider" class="csw">
                            <div class="panelContainer">
                                <div class="panel" title="Panel 1">
                                    <div class="wrapper">
                                        <a href="http://www.instrument.com.cn/eZine/webinar/4/" target="_blank">
                                            <img src="http://bimg.instrument.com.cn/g/sh100000/webinar/310_260.jpg" alt="temp"
                                                width="300" height="240" /></a>
                                    </div>
                                </div>
                                <div class="panel" title="Panel 2">
                                    <div class="wrapper">
                                        <a href="http://www.instrument.com.cn/application/" target="_blank">
                                            <img src="http://bimg.instrument.com.cn/g/sh100000/application/20120802_300_240.jpg"
                                                alt="temp" width="300" height="240" /></a>
                                    </div>
                                </div>
                                <div class="panel" title="Panel 3">
                                    <div class="wrapper">
                                        <a href="http://bbs.instrument.com.cn/shtml/20120726/4163058/" target="_blank">
                                            <img src="http://simg.instrument.com.cn/ilog/images/zhijianshangdeyiqi.jpg" alt="scotch egg"
                                                width="300" height="240" /></a>
                                    </div>
                                </div>
                                <div class="panel" title="Panel 4">
                                    <div class="wrapper">
                                        <a href="http://www.instrument.com.cn/eZine/webinar/4/" target="_blank">
                                            <img src="http://bimg.instrument.com.cn/g/sh100000/webinar/310_260.jpg" alt="temp"
                                                width="300" height="240" /></a>
                                    </div>
                                </div>
                                <div class="panel" title="Panel 5">
                                    <div class="wrapper">
                                        <a href="http://www.instrument.com.cn/application/" target="_blank">
                                            <img src="http://bimg.instrument.com.cn/g/sh100000/application/20120802_300_240.jpg"
                                                alt="temp" width="300" height="240" /></a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="movers-row">
                            <div>
                                <a href="#1" class="cross-link active-thumb">
                                    <img src="http://bimg.instrument.com.cn/g/sh100000/webinar/310_260.jpg" class="nav-thumb"
                                        alt="temp-thumb" /></a></div>
                            <div>
                                <a href="#2" class="cross-link">
                                    <img src="http://bimg.instrument.com.cn/g/sh100000/application/20120802_300_240.jpg"
                                        class="nav-thumb" alt="temp-thumb" /></a></div>
                            <div>
                                <a href="#3" class="cross-link">
                                    <img src="http://simg.instrument.com.cn/ilog/images/zhijianshangdeyiqi.jpg" class="nav-thumb"
                                        alt="temp-thumb" /></a></div>
                            <div>
                                <a href="#4" class="cross-link">
                                    <img src="http://bimg.instrument.com.cn/g/sh100000/webinar/310_260.jpg" class="nav-thumb"
                                        alt="temp-thumb" /></a></div>
                            <div>
                                <a href="#5" class="cross-link">
                                    <img src="http://bimg.instrument.com.cn/g/sh100000/application/20120802_300_240.jpg"
                                        class="nav-thumb" alt="temp-thumb" /></a></div>
                        </div>
                    </div>
                </div>
                <div class="rightbox R">
                    <h1 class="Fw F20 L40">
                        <a href="http://ig.instrument.com.cn/" target="_blank" class="Blue">
                            <center>
                                �°�iLog�ڲ������</center>
                        </a>
                    </h1>
                    <p class="Txt">
                        ������һ����з����°�iLog�����������Ѽ����ˣ��°�iLog�����΢����SNS�Ĺ��ܣ������ѡ�ת�������ۡ�վ�̡�ͬ��������ȹ��ܼ���һ����������������Ϣ�����罻����Ϣ����ʹ��ݵĹ��ܡ��ǿ�ѧ�����ͷ���������ҵ��ͬ�������Ͻ����ͷ�������Ӫ����һ�Ѽ�ʵ���С�</p>
                    <div class="Hr_10">
                    </div>
                    <img src="http://simg.instrument.com.cn/ilog/blue/images/square_line.gif" />
                    <h1 class="Fw F20 L40 blue">
                        <a href="http://bbs.instrument.com.cn/shtml/20120731/4171836/" class="Blue" target="_blank">
                            <center>
                                ԭ������8�¿����������</center>
                        </a>
                    </h1>
                    <p class="Txt">
                        ������ѧ��������ԭ����Ʒ��������ʱ�䣺8��1�ա�11��30�գ�������Ϊ12�������������ͺ�����ҵ��������������������Ӧ�á��¼�����չ������ά��ά�ޡ���������ʹ�þ���</p>
                </div>
                <div class="Hr_1">
                </div>
            </div>
            <div class="Hr_10">
            </div>
            <div class="Info2 L35 Pl10  Wh " style="display: none">
                <img src="http://simg.instrument.com.cn/ilog/blue/images/ico_la.png" class="L img" />
                <span class="span">���Ż��⣨�ڶ��ڣ�</span>�߿�������ߺ����� ��iPhone��ͬѧ���� ��ѧ��ռ������ȺŹ ������ָ����Э��ԱŹ��Ů����</div>
            <div class="Hr_20">
            </div>
            <div class="Info3 ">
                <div class="L Info3L ">
                    <h1 class="TIT B L30">
                        <img src="http://simg.instrument.com.cn/ilog/blue/images/ico_hg.gif" class="L img" /><a
                            href="/Fame.html">��ҵ����</a></h1>
                    <div class="Line_solid">
                    </div>
                    <div class="Hr_10">
                    </div>
                    <div class="SquareImg G9">
                        <ul id="ulNewFamousList">
                        </ul>
                    </div>
                    <div class="Hr_10">
                    </div>
                    <h1 class="TIT B L30">
                        <img src="http://simg.instrument.com.cn/ilog/blue/images/ico_hg.gif" class="L img" />ilog����</h1>
                    <div class="Line_solid">
                    </div>
                    <div class="Hr_10">
                    </div>
                    <div class="SquareImg G9">
                        <ul id="ulUserList">
                        </ul>
                    </div>
                </div>
                <div class="R Info3R  ">
                    <h1 class="TIT L30 ">
                        <span class="R"><a href="nowilog.html" class="Blue">�Ͻ���ɳ����</a>~</span><img src="http://simg.instrument.com.cn/ilog/blue/images/ico-xx.gif"
                            class="L" /><a href="nowilog.html" class=" B">���ڷ���</a>
                        <div class="Line_solid">
                        </div>
                    </h1>
                    <div class="Hr_10">
                    </div>
                    <div id="divContent" style="margin: 10px 0; overflow: hidden; height: 520px;">
                    </div>
                </div>
                <div class="Hr_10">
                </div>
            </div>
        </div>
        <div class="SquareR R Line_blue BrBlue P10">
            <h1 class="F14 L35 blue">
                <strong>ÿ�����˰�</strong></h1>
            <div class="Line_dashed">
            </div>
            <div class="Hr_10">
            </div>
            <ul class="Search_list" id="ulFamous">
            </ul>
            <div class="Hr_10">
            </div>
            <div class="Line_dashed">
            </div>
            <div class="Hr_20">
            </div>
            <h1 class="F14 L35 blue">
                <strong>ÿ�ղݸ���</strong></h1>
            <div class="Line_dashed">
            </div>
            <div class="Hr_10">
            </div>
            <ul class="Search_list" id="ulCommon">
            </ul>
        </div>
    </div>
    <input id="hidIlogCount" type="hidden" />
    <input id="code" name="code" type="hidden" value="<%=code %>" />
    <div class="Hr_10">
    </div>
    <div class="Area">
        <div class="SquareLink Line ">
            <div class="Tit F14">
                <span class="R  More F12"><a href="http://www.instrument.com.cn/service/link.htm"
                    target="_blank" class="Blue">����>></a></span><span class="span L"><a href="http://www.instrument.com.cn/service/link.htm"
                        target="_blank" class=" White"><strong>��������</strong></a></span>
            </div>
            <div class="Hr_1">
            </div>
            <div class="P10">
                <ul class="L20 ">
                    <li class="li"><a href="http://www.caia.org.cn/" target="_blank">�й���������Э��</a></li>
                    <li class="li"><a href="http://www.woyaoce.cn/" target="_blank">��Ҫ��</a></li>
                    <li><a href="http://www.bioon.com/" target="_blank">�����</a></li>
                    <li><a href="http://www.rmhot.com/" target="_blank">���ұ�׼������</a></li>
                    <li><a href="http://www.frponline.com.cn/" target="_blank">���ϲ�����</a> </li>
                    <li><a href="http://www.foodmate.net/" target="_blank">ʳƷ�����</a></li>
                    <li class="li"><a href="http://www.fxxh.org.cn/" target="_blank">�й������Ǳ�ѧ����������ֻ�</a></li>
                    <li class="li"><a href="http://www.dxyq.zj.cn/" target="_blank">�㽭��������Э����</a></li>
                    <li><a href="http://www.54pc.com/" target="_blank">�й�����������</a></li>
                    <li><a href="http://www.caigou.com.cn" target="_blank">�й�����װ���ɹ���</a></li>
                    <li><a href="http://www.bmlink.com" target="_blank">�й�������</a></li>
                    <li><a href="http://www.21food.cn/" target="_blank">ʳƷ������</a></li>
                    <li class="li"><a href="http://www.csp.org.cn/" target="_blank">�й�����ѧ��</a></li>
                    <li class="li"><a href="http://www.sndy.org" target="_blank">������������Э����</a></li>
                    <li><a href="http://www.sepu.net/" target="_blank">�й�ɫ����</a></li>
                    <li><a href="http://www.testmart.cn/" target="_blank">�����Ǳ�����</a></li>
                    <li><a href="http://www.elecfans.com/" target="_blank">���ӷ�����</a></li>
                    <li><a href="http://ch.gongchang.com/" target="_blank">���繤����</a></li>
                    <li class="li"><a href="http://www.cmss.org.cn/" target="_blank">�й�����ѧ��</a></li>
                    <li class="li"><a href="http://www.testbj.com/" target="_blank">�������Ϸ������Է�������</a></li>
                    <li><a href="http://www.bio-equip.com/" target="_blank">����������</a></li>
                    <li><a href="http://www.chemdrug.com/ " target="_blank">ҩƷ��Ѷ��</a></li>
                    <li><a href="http://www.autocontrol.com.cn/" target="_blank">�й��Կ���</a></li>
                    <li><a href="http://www.shuigongye.com/" target="_blank">�й�ˮ��ҵ��</a></li>
                    <li class="li"><a href="http://www.bjx.com.cn/" target="_blank">�����ǵ�����</a></li>
                    <li class="li"><a href="http://bbs.instrument.com.cn/" target="_blank">������̳</a></li>
                    <li class="li"><a href="http://www.ccvic.com" target="_blank">��ý��</a></li>
                </ul>
                <div>
                    <div class="Hr_1">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <!--�ײ���ʼ-->
    <div class=" Tc G4">
        <a href="http://www.instrument.com.cn/service/about.htm" class="G4" target="_blank">
            ��������</a> - <a href="http://www.instrument.com.cn/service/navigate.htm" class="G4"
                target="_blank">��Ŀ����</a> - <a href="http://www.instrument.com.cn/Resume/" class="G4"
                    target="_blank">��ƸӢ��</a> - <a href="http://www.instrument.com.cn/service/vip_service.htm"
                        class="G4" target="_blank">VIP��Ա����</a> - <a href="http://bbs.instrument.com.cn/forum_464.htm"
                            class="G4" target="_blank">�ͻ�Ͷ��</a> - <a href="http://www.instrument.com.cn/service/link.htm"
                                class="G4" target="_blank">��������</a> - <a href="http://www.instrument.com.cn/service/juristic.htm"
                                    class="G4" target="_blank">��������</a> - <a href="http://www.instrument.com.cn/service/contacts.htm"
                                        class="G4" target="_blank">��ϵ����</a> - <a href="http://www.instrument.com.cn/news/NewsList.asp?SortID=7"
                                            class="G4" target="_blank">������̬</a>
        <br />
        Instrument.com.cn Copyright<span class="Fa">&copy; </span>1999-2012,All Rights Reserved
        ��Ȩ����</div>
    <!--�ײ�����-->
</body>
</html>
