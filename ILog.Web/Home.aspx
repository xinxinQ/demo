<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ILog.Web.Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>��������</title>
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet" type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet"  type="text/css" />
    <link href="/css/rotate.css" rel="stylesheet" type="text/css" />
    
    
    <script src="js/jquery-1.3.2.js" type="text/javascript" language="javascript"></script>        
    <script src="js/Common.js" type="text/javascript" language="javascript"></script>
        
    <!--�û�������Ϣ.by lx on 20120626-->
    <script src="js/VipILogHome.js" type="text/javascript"></script>
    
    <!--�����û���Ϣ.by lx on 20120626-->

    <script src="js/VipILogInfo.js" type="text/javascript"></script>
    
    <script src="js/home.js" type="text/javascript" language="javascript"></script>
    <script language="javascript" type="text/javascript" src="js/jquery.cookie.js"></script> 
   
       
    <!--�������-->
    <script src="js/jquery.insertContent.js" type="text/javascript"></script>
    
    <!--ͼƬ�ϴ�-->
    <script src="js/ajaxfileupload.js" type="text/javascript"></script>
    
    <!--��ʾ-->
    <script src="js/lhgdialog.min.js" type="text/javascript"></script>
    
    <script src="/js/jquery.curpos.js" type="text/javascript"></script>

    <script src="/js/jquery.cursorposition.js" type="text/javascript"></script>

    <script src="/js/rotate.js" type="text/javascript" charset="gb2312"></script>
   
    <script src="/js/jquery.query.js" type="text/javascript"></script>


    <script src="js/SendMail.js" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript" src="js/ilogcheck.js"></script>
    
    
    <style type="text/css">
    /*�ص�����*/
    #scrolltop {
	DISPLAY: block; BACKGROUND: url(http://simg.instrument.com.cn/bbs/20110720/images/gotop.gif) no-repeat 50% 0px;WIDTH: 26px; CURSOR: pointer; BOTTOM: 100px; LINE-HEIGHT: 999px; POSITION: fixed; HEIGHT: 50px
    }
    .ie6 #scrolltop {
        BOTTOM: auto; POSITION: absolute
    }
    
    /*������λ*/
    .dw{width:126px; height:190px;background-color: #b1d927;}

    </style>
</head>
<body class="body">


    <!--�����ļ���ʼ-->
    <!--#include file="top.htm"-->
    <div class="TopSpan"></div>    
    <div class="TopSpan"></div>
    <!--�����ļ�����-->
    
    
    <div class=" Area_B BrWh" id="ShowAD">
        <!--��߿�ʼ-->
        <div class="Llog L" id="divleft">
            <div  class="Hr_20">
            </div>
            
            <!--�û���Ϣ��ʾ-->
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
        <!--��߽���-->
        <!--�ұ߿�ʼ-->
        <!--#include file="right.htm"-->
        <!--�ұ߽���-->
        <div class="Center L">
            <div class="Hr_20">
            </div>
            <br />
            <div class="publish">
                <div class="G3">
                    <img class="L" src="http://simg.instrument.com.cn/ilog/blue/images/publish.gif" /><span id="prompt" class="R">�㻹��������<font class="publish_num">140</font>��</span></div>
                <div class="Hr_5">
                </div>
     
                <textarea class="Input F14 textarea Fa ENG"  name="textarea" id="textarea" cols="45" rows="5" onfocus="hideDiv();" onchange="checkSendBlog('textarea');"></textarea>                
             
                
                <ul id="flttishi" class="WindowMenu  WindowW Line_blue L30" style="position:absolute; display:none;Z-index:3;">
               
                </ul><input type="hidden" name="prevTrIndex" id="prevTrIndex" value="-1" />
                
             <!--�ϴ�ͼƬ����-->
             <input id="guidHidden" type="hidden" value="<%=guid %>"/> 
             <input id="ipHidden" type="hidden" value="<%=ip %>"/>
             
             <!--�ж��Ƿ���ͼƬ-->
             <input id="hasPicHidden" type="hidden" value="0"/> 
             
              <!--ɾ������-->
             <input id="deleteIdHidden" type="hidden" value=""/> 
             
             <!--��������ʱ�õ�.by lx on 20120716--> 
             <input id="sendCommentHiddent" type="hidden" value="0"/>
                
                             
                <div class="Hr_4">
                </div>
                <div>
                    <div class="L list" style="position:relative;"  >
                        <ul id="showee">
                            <li><span class="ico1" id="pngShow"></span><a href="javascript:void(0);" class="Blue" id="faceId" onclick="changeExpressio(this,'textarea');">����</a></li>
                            <li><span class="ico2"></span><a href="javascript:void(0);" class="Blue" id="pictureInfoId" onclick="changePicture();">ͼƬ</a>                            
                            
                            </li>
                            <li><span class="ico3"></span><a href="javascript:void(0);" class="Blue" id="screenInfoId" onclick="changeScreen();">��Ƶ</a>
                            
                            </li>
                        </ul>                
                        
                    </div>
                    
                    
                    
                    
                    <input type="image" id="btnImg" class="R" src="http://simg.instrument.com.cn/ilog/blue/images/btn_fb.gif"
                        alt="����" onclick="return sendBlog();" /></div>
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
   s1.addVariable("txt", "ͼ1|ͼ2|ͼ3|ͼ4|ͼ5");
   s1.addVariable("width", "540");
   s1.addVariable("height", "80");
   s1.write("flashFCI");
                </script>

            </div>
            <br />
            
            
            <!--�л���ǩ��ʼ-->
            <div class="CententNav">
                <ul>
                    <li id="all_il">
                        <div class="top"></div>
                        <div class="center">
                        <a href="javascript:void(0);" onclick="ListTyle(0);" class="Blue"><strong>ȫ��</strong></a></div>
                    </li>
                    <li id="Original_li">
                        <a  href="javascript:void(0);" onclick="ListTyle(1);" >����</a>
                    
                    
                    
                    
                    
                    </li>
                </ul>
            </div>
            <div class="Hr_20">
            </div>
            <!--�л���ǩ����-->
            <!--�����б�ʼ-->
            <div id="list_div">
            </div>
            <!--�����б����-->
            <!--ҳ�뿪ʼ-->
            <div id="sowhpage_div" class="page">
            </div>
            <!--ҳ�����-->
        </div>
        <div class="Hr_1">
        </div>
    </div>
    <!--�ײ���ʼ-->
    <!--�����ļ���ʼ-->
    <!--#include file="bottom.htm"-->
    <!--�����ļ�����-->
    <!--�ײ�����-->
</body>
</html>
