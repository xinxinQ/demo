<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ILog.Web.verify.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>申请认证</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet" type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"
        type="text/css" />

    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../js/jquery.cookie.js"></script>
    <script src="../js/lhgdialog.min.js" type="text/javascript"></script>
    
    
    <script src="../js/Common.js" type="text/javascript"></script>

    <script src="../js/verify.js" type="text/javascript"></script>

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
        <!--认证第一步开始-->
        <div class="RightWhite R">
            <div class="Hr_20">
            </div>
            <div>
                <img src="http://simg.instrument.com.cn/ilog/blue/images/i1.gif" alt="ILOG 名人认证"
                    width="730" height="220" />
               </div>
            <div class="Hr_10">
            </div>
            <p class="L20 G6">
                <span class="Orange">iLog个人/名人认证是完全免费的，任何收费的认证行为都是虚假欺骗的。</span></p>
            <div class="Hr_20">
            </div>
            <h1 class="F16  G3 L40">
                如何申请认证？</h1>
            <div class="Hr_10">
            </div>
            <div class="attestation L">
                <img class="img" src="http://simg.instrument.com.cn/ilog/blue/images/pic1.gif" width="120"
                    height="120" />
                <div class="L widthbox">
                    <div>
                        <h1 class="F14">
                            <a href="#">
                                <img class="L" src="http://simg.instrument.com.cn/ilog/blue/images/i.gif" /></a>个人认证</h1>
                        <div class="Hr_5">
                        </div>
                        <p class="L18 ">
                            认证范围：仪器仪表、分析检测及相关行业、职位的人士认证申请。
                            <div class="Hr_4">
                            </div>
                            <span class=" G6">基本条件：有头像、粉丝不低于50个、关注不低于50、微博不低于50篇。</span></p>
                        <div class="Hr_5">
                        </div>
                        <p class="WinBtn L">
                            <a href="javascript:CheckAuthentication(1);" class="White"><span>申请认证</span></a></p>
                    </div>
                </div>
            </div>
            <div class="Hr_10">
            </div>
            <div class="attestation L">
                <img class="img" src="http://simg.instrument.com.cn/ilog/blue/images/pic2.gif" width="120"
                    height="120" />
                <div class="L widthbox">
                    <div>
                        <h1 class="F14">
                            <a href="#">
                                <img class="L" src="http://simg.instrument.com.cn/ilog/blue/images/i.gif" /></a>名人认证</h1>
                        <div class="Hr_5">
                        </div>
                        <p class="L20">
                            认证范围：仪器仪表、分析检测及相关行业名人、专家，企（事）业高管、领导，博士、高级工程师、院士等人士的认证申请。
                            <div class="Hr_4">
                            </div>
                            <span class=" G6">基本条件：有头像、粉丝不低于50个、关注不低于50、微博不低于50篇</span>
                        </p>
                        <div class="Hr_5">
                        </div>
                        <p class="WinBtn L">
                            <a href="javascript:CheckAuthentication(2);" class="White"><span>申请认证</span></a></p>
                    </div>
                </div>
            </div>
            <div class="Hr_20">
            </div>
            <h1 class="F16  G3 L35">
                帮助与建议</h1>
            <p class="F14 G4">
                各类认证相关问题与建议请到<a href="#" class="Blue">用户反馈</a>页进行反馈，iLog将充分考虑用户的建议和意见，为用户提供更满意的服务。</p>
            <div class="Hr_20">
            </div>
            <h1 class="F16  G3 L35">
                &nbsp;</h1>
            <br />
            <br />
            <br />
        </div>
        <!--认证第一步结束-->
        <div class="Hr_1">
        </div>
    </div>
    <!--底部开始-->
        <!--#include file="/bottom.htm"-->

</body>
</html>
