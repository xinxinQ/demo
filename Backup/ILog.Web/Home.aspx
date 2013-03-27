<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ILog.Web.Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>个人中心</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet" type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"  type="text/css" />
    <link href="/css/rotate.css" rel="stylesheet" type="text/css" />
    
    
    <script src="js/jquery-1.3.2.js" type="text/javascript" language="javascript"></script>        
    <script src="js/Common.js" type="text/javascript" language="javascript"></script>
        
    <!--用户基本信息.by lx on 20120626-->
    <script src="js/VipILogHome.js" type="text/javascript"></script>
    
    <!--悬浮用户信息.by lx on 20120626-->

    <script src="js/VipILogInfo.js" type="text/javascript"></script>
    
    <script src="js/home.js" type="text/javascript" language="javascript"></script>
    <script language="javascript" type="text/javascript" src="js/jquery.cookie.js"></script> 
   
       
    <!--表情插入-->
    <script src="js/jquery.insertContent.js" type="text/javascript"></script>
    
    <!--图片上传-->
    <script src="js/ajaxfileupload.js" type="text/javascript"></script>
    
    <!--提示-->
    <script src="js/lhgdialog.min.js" type="text/javascript"></script>
    
    <script src="/js/jquery.curpos.js" type="text/javascript"></script>

    <script src="/js/jquery.cursorposition.js" type="text/javascript"></script>

    <script src="/js/rotate.js" type="text/javascript" charset="gb2312"></script>
   
    <script src="/js/jquery.query.js" type="text/javascript"></script>


    <script src="js/SendMail.js" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript" src="js/ilogcheck.js"></script>
    
    
    <style type="text/css">
    /*回到顶部*/
    #scrolltop {
	DISPLAY: block; BACKGROUND: url(http://simg.instrument.com.cn/bbs/20110720/images/gotop.gif) no-repeat 50% 0px;WIDTH: 26px; CURSOR: pointer; BOTTOM: 100px; LINE-HEIGHT: 999px; POSITION: fixed; HEIGHT: 50px
    }
    .ie6 #scrolltop {
        BOTTOM: auto; POSITION: absolute
    }
    
    /*顶部定位*/
    .dw{width:126px; height:190px;background-color: #b1d927;}

    </style>
</head>
<body class="body">


    <!--顶部文件开始-->
    <!--#include file="top.htm"-->
    <div class="TopSpan"></div>    
    <div class="TopSpan"></div>
    <!--顶部文件结束-->
    
    
    <div class=" Area_B BrWh" id="ShowAD">
        <!--左边开始-->
        <div class="Llog L" id="divleft">
            <div  class="Hr_20">
            </div>
            
            <!--用户信息显示-->
           <div id="vipIlog"></div>

           
            <div class=" Llog_Nav L30">
                <ul id="leftmenu">

                </ul>
            </div>
            <div class="Hr_10">
            </div>
             <div class="Llog_line" style="height:370px">
            </div>
            <div class="Hr_10">
            </div>
        </div>
        <!--左边结束-->
        <!--右边开始-->
        <!--#include file="right.htm"-->
        <!--右边结束-->
        <div class="Center L">
            <div class="Hr_20">
            </div>
            <br />
            <div class="publish">
                <div class="G3">
                    <img class="L" src="http://simg.instrument.com.cn/ilog/blue/images/publish.gif" /><span id="prompt" class="R">你还可以输入<font class="publish_num">140</font>字</span></div>
                <div class="Hr_5">
                </div>
     
                <textarea class="Input F14 textarea Fa ENG"  name="textarea" id="textarea" cols="45" rows="5" onfocus="hideDiv();" onchange="checkSendBlog('textarea');"></textarea>                
             
                
                <ul id="flttishi" class="WindowMenu  WindowW Line_blue L30" style="position:absolute; display:none;Z-index:3;">
               
                </ul><input type="hidden" name="prevTrIndex" id="prevTrIndex" value="-1" />
                
             <!--上传图片变量-->
             <input id="guidHidden" type="hidden" value="<%=guid %>"/> 
             <input id="ipHidden" type="hidden" value="<%=ip %>"/>
             
             <!--判断是否有图片-->
             <input id="hasPicHidden" type="hidden" value="0"/> 
             
              <!--删除评论-->
             <input id="deleteIdHidden" type="hidden" value=""/> 
             
             <!--发送评论时用到.by lx on 20120716--> 
             <input id="sendCommentHiddent" type="hidden" value="0"/>
                
                             
                <div class="Hr_4">
                </div>
                <div>
                    <div class="L list" style="position:relative;"  >
                        <ul id="showee">
                            <li><span class="ico1" id="pngShow"></span><a href="javascript:void(0);" class="Blue" id="faceId" onclick="changeExpressio(this,'textarea');">表情</a></li>
                            <li><span class="ico2"></span><a href="javascript:void(0);" class="Blue" id="pictureInfoId" onclick="changePicture();">图片</a>                            
                            
                            </li>
                            <li><span class="ico3"></span><a href="javascript:void(0);" class="Blue" id="screenInfoId" onclick="changeScreen();">视频</a>
                            
                            </li>
                        </ul>                
                        
                    </div>
                    
                    
                    
                    
                    <input type="image" id="btnImg" class="R" src="http://simg.instrument.com.cn/ilog/blue/images/btn_fb.gif"
                        alt="发布" onclick="return sendBlog();" /></div>
            </div>
            
            
            
            <div class="Banner">

                <script src="js/LunBo.js" type="text/javascript"></script>

                <div id="flashFCI">
                    <a href="http://www.lanrentuku.com" arget="_blank">
                        <img alt="http://simg.instrument.com.cn/ilog/blue/images/pic2.jpg" border="0"></a></div>


                <script type="text/javascript">
   var s1 = new SWFObject("http://simg.instrument.com.cn/ilog/blue/images/LunBo.swf", "mymovie1", "540", "80", "8", "#ffffff");
   s1.addParam("wmode", "transparent");
   s1.addParam("AllowscriptAccess", "sameDomain");
   s1.addVariable("bigSrc", "http://www.instrument.com.cn/lib/editor/UploadFile/20128/201286135136595.jpg|http://bimg.instrument.com.cn/g/SH100000/wyc/wyc_550_90.gif|http://simg.instrument.com.cn/ilog/images/aoyunyiqi.jpg|http://www.instrument.com.cn/lib/editor/UploadFile/20128/201286135136595.jpg|http://simg.instrument.com.cn/ilog/images/aoyunyiqi.jpg");
   s1.addVariable("smallSrc", "");
   s1.addVariable("href", "http://www.instrument.com.cn/job/special/?sid=1|http://www.woyaoce.cn|http://bbs.instrument.com.cn/shtml/20120727/4165502/|http://www.instrument.com.cn/job/special/?sid=1|http://bbs.instrument.com.cn/shtml/20120727/4165502/");
   s1.addVariable("txt", "图1|图2|图3|图4|图5");
   s1.addVariable("width", "540");
   s1.addVariable("height", "80");
   s1.write("flashFCI");
                </script>

            </div>
            <br />
            
            
            <!--切换标签开始-->
            <div class="CententNav">
                <ul>
                    <li id="all_il">
                        <div class="top"></div>
                        <div class="center">
                        <a href="javascript:void(0);" onclick="ListTyle(0);" class="Blue"><strong>全部</strong></a></div>
                    </li>
                    <li id="Original_li">
                        <a  href="javascript:void(0);" onclick="ListTyle(1);" >博文</a>
                    
                    
                    
                    
                    
                    </li>
                </ul>
            </div>
            <div class="Hr_20">
            </div>
            <!--切换标签结束-->
            <!--数据列表开始-->
            <div id="list_div">
            </div>
            <!--数据列表结束-->
            <!--页码开始-->
            <div id="sowhpage_div" class="page">
            </div>
            <!--页码结束-->
        </div>
        <div class="Hr_1">
        </div>
    </div>
    <!--底部开始-->
    <!--顶部文件开始-->
    <!--#include file="bottom.htm"-->
    <!--顶部文件结束-->
    <!--底部结束-->
</body>
</html>
