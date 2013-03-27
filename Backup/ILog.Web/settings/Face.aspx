<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Face.aspx.cs" Inherits="ILog.Web.settings.Face" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title></title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet" type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"  type="text/css" />
    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript" src="../js/jquery.cookie.js"></script>
    <script src="../js/Common.js" type="text/javascript"></script>
    <script src="../js/face.js" type="text/javascript"></script>
    <script src="../js/swfobject.js" type="text/javascript"></script>

</head>
<body class="body">
    <!--顶部文件开始-->
    <!--#include file="../top.htm"-->
    <div class="TopSpan">
    </div>
    <div class="TopSpan">
    </div>
    <!--顶部文件结束-->
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
        <form id="form_input" method="post" action="Face.aspx">
        <!--->
        <div class="RightWhite R">
            <div class="Hr_20">
            </div>
            <div class="Hr_20">
            </div>
            <div class="Hr_20">
            </div>
            <div class="Hr_20">
            </div>
                       <div class="Hr_20">
            </div>
            <div class="G3 L35">
                <h1 class="F14 L">
                </h1>
            </div>
            <div class="Line_solid">
                <div style="margin-left: 30px">

                    <script type="text/javascript">
            // For version detection, set to min. required Flash Player version, or 0 (or 0.0.0), for no version detection. 
            var swfVersionStr = "10.2.0";
            // To use express install, set to playerProductInstall.swf, otherwise the empty string. 
            var xiSwfUrlStr = "/swf/playerProductInstall.swf";
            var flashvars = {};
            flashvars.avatar_url="<%=faceurl%>";// 上传头像的具体地址
            flashvars.policy_file_url="/crossdomain.xml";
           flashvars.pSize="200|200|200|200|200|200|200|200";
           
            var params = {};
            params.quality = "high";
            params.bgcolor = "#ffffff";
            params.allowscriptaccess = "sameDomain";
            params.allowfullscreen = "true";
          
            
            var attributes = {};
            attributes.id = "mayland_avatar_miniblog";
            attributes.name = "mayland_avatar_miniblog";
            attributes.align = "middle";
      
            swfobject.embedSWF(
                "/swf/mayland_avatar_miniblog.swf", "flashContent", 
                "700", "450", 
                swfVersionStr, xiSwfUrlStr, 
                flashvars, params, attributes);
            // JavaScript enabled so display the flashContent div in case it is not replaced with a swf object.
            swfobject.createCSS("#flashContent", "display:block;text-align:left;");
			<!--			
			function reload() {
				window.location.reload();
			}
			
			function getUploadUrl() {
				return "upload_avatar.aspx";
			}
					
			function uploadSuccessHandler() {
				//把下面的xxx.asp换成跳转到的页面的名称
				alert("上传成功！");
			    window.location.href="/home.aspx";
			}
			// -->
                    </script>

                    <div id="flashContent">
                        <p>
                            To view this page ensure that Adobe Flash Player version 10.2.0 or greater is installed.
                        </p>

                        <script type="text/javascript">
                            var pageHost = ((document.location.protocol == "https:") ? "https://" : "http://");
                            document.write("<a href='http://www.adobe.com/go/getflashplayer'><img src='"
                                + pageHost + "www.adobe.com/images/shared/download_buttons/get_flash_player.gif' alt='Get Adobe Flash player' /></a>"); 
                        </script>

                    </div>
                    <noscript>
                        <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="665" height="430"
                            id="mayland_avatar_miniblog">
                            <param name="movie" value="/swf/mayland_avatar_miniblog.swf" />
                            <param name="wmode" value="transparent">
                            <param name="bgcolor" value="#ffffff" />
                            <param name="allowScriptAccess" value="sameDomain" />
                            <param name="allowFullScreen" value="true" />
                            <!--[if !IE]>-->
                            <object type="application/x-shockwave-flash" data="/swf/mayland_avatar_miniblog.swf"
                                width="665" height="430">
                                <param name="wmode" value="transparent">
                                <param name="bgcolor" value="#ffffff" />
                                <param name="allowScriptAccess" value="sameDomain" />
                                <param name="allowFullScreen" value="true" />
                            </object>
                            <!--<![endif]-->
                            <!--[if gte IE 6]>-->
                            <p>
                                Either scripts and active content are not permitted to run or Adobe Flash Player
                                version 10.2.0 or greater is not installed.
                            </p>
                            <!--<![endif]-->
                            <a href="http://www.adobe.com/go/getflashplayer">
                                <img src="http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif"
                                    alt="Get Adobe Flash Player" />
                            </a>
                            <!--[if !IE]>-->
                            <!--<![endif]-->
                        </object>
                    </noscript>
                    <div style="height: 40px;">
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
        <div class="Tc">
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
        </div>
        <!---->
        </form>
        <div class="Hr_1">
        </div>
    </div>
    <!--底部开始-->
    <!--#include file="/bottom.htm"-->
</body>
</html>
