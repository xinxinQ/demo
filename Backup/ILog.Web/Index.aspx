<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ILog.Web.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>广场</title>
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
    <!--顶部文件开始-->
    <!--#include file="top.htm"-->
    <!--顶部文件结束-->
    <div class="TopSpan">
    </div>
    <div class="TopSpan">
    </div>
    <!--广场开始-->
    <div class="Area">
        <div class="SquareNav  Fw ">
            <img src="http://simg.instrument.com.cn/ilog/blue/images/Nav01.gif" class="L" /><a
                href="Index.html" class="Blue">首 页 </a>|<a href="Fame.html" class="Blue" target="_blank">
                    名人堂</a> | <a href="Transmit.html" class="Blue" target="_blank">随便看看</a><img src="http://simg.instrument.com.cn/ilog/blue/images/Nav03.gif"
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
                                新版iLog内测进行中</center>
                        </a>
                    </h1>
                    <p class="Txt">
                        经过近一年的研发，新版iLog终于与广大网友见面了！新版iLog结合了微博和SNS的功能，将交友、转发、评论、站短、同步、分享等功能集于一身，大大提高了仪器信息网的社交、信息分享和传递的功能。是科学仪器和分析测试行业的同行们网上交流和分享、网络营销的一把坚实利刃。</p>
                    <div class="Hr_10">
                    </div>
                    <img src="http://simg.instrument.com.cn/ilog/blue/images/square_line.gif" />
                    <h1 class="Fw F20 L40 blue">
                        <a href="http://bbs.instrument.com.cn/shtml/20120731/4171836/" class="Blue" target="_blank">
                            <center>
                                原创大奖赛8月开赛亮点汇总</center>
                        </a>
                    </h1>
                    <p class="Txt">
                        第五届科学仪器网络原创作品大赛征文时间：8月1日—11月30日，大赛分为12个赛区征文类型涵盖行业综述、分析方法开发与应用、新技术发展、仪器维护维修、仪器操作使用经验</p>
                </div>
                <div class="Hr_1">
                </div>
            </div>
            <div class="Hr_10">
            </div>
            <div class="Info2 L35 Pl10  Wh " style="display: none">
                <img src="http://simg.instrument.com.cn/ilog/blue/images/ico_la.png" class="L img" />
                <span class="span">热门话题（第二期）</span>倪匡惆怅体走红网络 用iPhone遭同学抢劫 大学生占座引发群殴 民工捧断指离足协官员殴打女记者</div>
            <div class="Hr_20">
            </div>
            <div class="Info3 ">
                <div class="L Info3L ">
                    <h1 class="TIT B L30">
                        <img src="http://simg.instrument.com.cn/ilog/blue/images/ico_hg.gif" class="L img" /><a
                            href="/Fame.html">行业名人</a></h1>
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
                        <img src="http://simg.instrument.com.cn/ilog/blue/images/ico_hg.gif" class="L img" />ilog新人</h1>
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
                        <span class="R"><a href="nowilog.html" class="Blue">赶紧抢沙发吧</a>~</span><img src="http://simg.instrument.com.cn/ilog/blue/images/ico-xx.gif"
                            class="L" /><a href="nowilog.html" class=" B">正在发生</a>
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
                <strong>每日名人榜</strong></h1>
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
                <strong>每日草根榜</strong></h1>
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
                    target="_blank" class="Blue">更多>></a></span><span class="span L"><a href="http://www.instrument.com.cn/service/link.htm"
                        target="_blank" class=" White"><strong>友情链接</strong></a></span>
            </div>
            <div class="Hr_1">
            </div>
            <div class="P10">
                <ul class="L20 ">
                    <li class="li"><a href="http://www.caia.org.cn/" target="_blank">中国分析测试协会</a></li>
                    <li class="li"><a href="http://www.woyaoce.cn/" target="_blank">我要测</a></li>
                    <li><a href="http://www.bioon.com/" target="_blank">生物谷</a></li>
                    <li><a href="http://www.rmhot.com/" target="_blank">国家标准物质网</a></li>
                    <li><a href="http://www.frponline.com.cn/" target="_blank">复合材料网</a> </li>
                    <li><a href="http://www.foodmate.net/" target="_blank">食品伙伴网</a></li>
                    <li class="li"><a href="http://www.fxxh.org.cn/" target="_blank">中国仪器仪表学会分析仪器分会</a></li>
                    <li class="li"><a href="http://www.dxyq.zj.cn/" target="_blank">浙江大型仪器协作网</a></li>
                    <li><a href="http://www.54pc.com/" target="_blank">中国分析仪器网</a></li>
                    <li><a href="http://www.caigou.com.cn" target="_blank">中国教育装备采购网</a></li>
                    <li><a href="http://www.bmlink.com" target="_blank">中国建材网</a></li>
                    <li><a href="http://www.21food.cn/" target="_blank">食品商务网</a></li>
                    <li class="li"><a href="http://www.csp.org.cn/" target="_blank">中国颗粒学会</a></li>
                    <li class="li"><a href="http://www.sndy.org" target="_blank">陕西大型仪器协作网</a></li>
                    <li><a href="http://www.sepu.net/" target="_blank">中国色谱网</a></li>
                    <li><a href="http://www.testmart.cn/" target="_blank">仪器仪表交易网</a></li>
                    <li><a href="http://www.elecfans.com/" target="_blank">电子发烧友</a></li>
                    <li><a href="http://ch.gongchang.com/" target="_blank">世界工厂网</a></li>
                    <li class="li"><a href="http://www.cmss.org.cn/" target="_blank">中国质谱学会</a></li>
                    <li class="li"><a href="http://www.testbj.com/" target="_blank">北京材料分析测试服务联盟</a></li>
                    <li><a href="http://www.bio-equip.com/" target="_blank">生物器材网</a></li>
                    <li><a href="http://www.chemdrug.com/ " target="_blank">药品资讯网</a></li>
                    <li><a href="http://www.autocontrol.com.cn/" target="_blank">中国自控网</a></li>
                    <li><a href="http://www.shuigongye.com/" target="_blank">中国水工业网</a></li>
                    <li class="li"><a href="http://www.bjx.com.cn/" target="_blank">北极星电力网</a></li>
                    <li class="li"><a href="http://bbs.instrument.com.cn/" target="_blank">仪器论坛</a></li>
                    <li class="li"><a href="http://www.ccvic.com" target="_blank">华媒网</a></li>
                </ul>
                <div>
                    <div class="Hr_1">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <!--底部开始-->
    <div class=" Tc G4">
        <a href="http://www.instrument.com.cn/service/about.htm" class="G4" target="_blank">
            关于我们</a> - <a href="http://www.instrument.com.cn/service/navigate.htm" class="G4"
                target="_blank">栏目导航</a> - <a href="http://www.instrument.com.cn/Resume/" class="G4"
                    target="_blank">诚聘英才</a> - <a href="http://www.instrument.com.cn/service/vip_service.htm"
                        class="G4" target="_blank">VIP会员中心</a> - <a href="http://bbs.instrument.com.cn/forum_464.htm"
                            class="G4" target="_blank">客户投诉</a> - <a href="http://www.instrument.com.cn/service/link.htm"
                                class="G4" target="_blank">友情链接</a> - <a href="http://www.instrument.com.cn/service/juristic.htm"
                                    class="G4" target="_blank">法律声明</a> - <a href="http://www.instrument.com.cn/service/contacts.htm"
                                        class="G4" target="_blank">联系我们</a> - <a href="http://www.instrument.com.cn/news/NewsList.asp?SortID=7"
                                            class="G4" target="_blank">本网动态</a>
        <br />
        Instrument.com.cn Copyright<span class="Fa">&copy; </span>1999-2012,All Rights Reserved
        版权所有</div>
    <!--底部结束-->
</body>
</html>
