var lang = new Array();
lang['defaultitle']= "��Ϣ��ʾ";

lang['evaluationtitle0']="��д�˻ش������";
lang['evaluationtitle3']="��д�ܾ��������";



function ShowLoading(){
	return "<div id=loading>Loading...</div>";
}

///�����Ի��������
	
function floatDiv(floattitle,wh,hg){   
	var pmwidth = wh;
	var clientHeight = document.documentElement.clientHeight ? document.documentElement.clientHeight : document.body.clientHeight;
	
	var windowstitle;
	
	if (!hg){
		var pmheight = clientHeight * 0.9;
	}else{
		var pmheight = hg;
	}
	
	//pmwidthiframe=pmheight -35
	
	if (!floattitle){		
		windowstitle="��Ϣ��ʾ";
	}else{
		windowstitle=floattitle;
	}
	
	var html;
	html = ' <div id="messagebox_win" style="position:absolute;z-index:99999;border:#767676 solid 5px; background-color:#FFFFFF;width:'+ pmwidth +'px;height:' + pmheight + 'px;"><div id="headDiv" style="padding-top:10px;height:25px; cursor:move;text-align:left; font-size:14px; font-weight:bold; color:#1388DC; padding-left:15px;border-bottom:#CCCCCC solid 1px;">'+ windowstitle +'<span onclick="hideDiv(\'messagebox_win\')" style="position: absolute; font-size:12px; right: 15px; top: 10px;cursor:pointer">�ر�</span></div><div id="messagebox_body"></div></div>';	
	
	//var enabled = '<div id="mask" style="background-color: lightgrey;width:100%;height:100%;position:absolute;z-index:99998;"></div>';
	
	//$(document.body).append(enabled);
	$(document.body).append(html);
	 SetEnabledStyle();
    floatDivsetMenuPosition('messagebox_win',clientHeight,pmheight);
	
	window.onscroll = function (){
		 floatDivsetMenuPosition('messagebox_win',clientHeight,pmheight);
	};
	DivMovePosition('headDiv','messagebox_win');
}


//�Ի���Ķ�λ
function floatDivsetMenuPosition(floatdivID,cheight,pheight){
	var div_obj = $('#'+floatdivID);   
    var windowWidth = document.documentElement.clientWidth; 
	var scrollTop = document.body.scrollTop ? document.body.scrollTop : document.documentElement.scrollTop;      
   // var windowHeight = document.documentElement.clientHeight;       
  	var popupHeight = div_obj.height();       
    var popupWidth = div_obj.width();    
                             						    
    div_obj.css({"position": "absolute",
	'left': windowWidth/2-popupWidth/2,
	'top': (cheight - pheight) / 2 + scrollTop
	}).show();           
}
 
 //�رնԻ���
function hideDiv(div_id) {   
    //$('#mask').remove();   
    $("#" + div_id).remove();  
	//showAllSelect();
}


//������ʾʱ������ʽ
function SetEnabledStyle()
{
  var css;
  css ={width:$('body').width()+"px",height:$('body').height()+"px",left: '0px',top: '0px'}
  GetOpacity(css);
  $("#mask").css(css);
 }
    
//����͸��ʽ��
function GetOpacity(css)
{
	 if(window.navigator.userAgent.indexOf('MSIE')>=1)
	 {
	   css.filter= 'progid:DXImageTransform.Microsoft.Alpha(opacity=30)';
	 }else
	 {
		css.opacity= '0.3';
	 }   
}

//�ƶ�div
function DivMovePosition(headDivID,moveid){
$("#" + headDivID).mousedown(
     function(event){
      var offset=$("#" + moveid).offset();
      x1=event.clientX-offset.left;
      y1=event.clientY-offset.top;
      var witchButton=false;
      if(document.all&&event.button==1){witchButton=true;}
      else{if(event.button==0)witchButton=true;}
      if(witchButton)//�����,FF��0��IE��1
      {
       $(document).mousemove(function(event){
        $("#" + moveid).css("left",(event.clientX-x1)+"px"); 
        $("#" + moveid).css("top",(event.clientY-y1)+"px"); 
       })
      }
     })
     $("#" + headDivID).mouseup(
     function(event){
      $(document).unbind("mousemove");
     })
}



//ajax function
// ajurl ������Ӧ��·�� showID��ʾ���ҳ�����Ĳ�����waitID �ȴ����ص���ʾID,postquerystr  ��ѯ���ַ���

function getajax(ajurl,showID,waitID){

	if (!ajurl) $('#'+ showID).html('����ʧ��... ��ˢ��ҳ������');
	if (!showID){ alert("error");return false;}
	if (!waitID) waitID=showID;

	$.ajax({
		url: ajurl,
		cache: false,
		beforeSend: function(XMLHttpRequest){
			Loadingstr=ShowLoading();
			$('#'+ waitID).html(Loadingstr);
		},
		success: function(html){
			$('#'+ showID).html(html);
		},
		complete: function(){
			$('#loading').remove();
		},
		error: function(){
			$('#'+ waitID).html('����ʧ��...');
		}
	}); 
}

function hideAllSelect() 
{ 
	//����select
	var selects = document.getElementsByTagName( "Select"); 
	for(var i = 0 ; i <selects.length;i++) 
		{ 
			selects[i].style.display= "none"; 
		} 
	
	//����falsh
	var objs = document.getElementsByTagName("OBJECT");
	for(i = 0;i < objs.length; i ++) {
		if(objs[i].style.visibility != 'hidden') {
			objs[i].setAttribute("oldvisibility", objs[i].style.visibility);
			objs[i].style.visibility = 'hidden';
		}
	}
	
	//����falsh
	var objs = document.getElementsByTagName("embed");
	for(i = 0;i < objs.length; i ++) {
		if(objs[i].style.visibility != 'hidden') {
			objs[i].setAttribute("oldvisibility", objs[i].style.visibility);
			objs[i].style.visibility = 'hidden';
		}
	}
} 

function showAllSelect() 
{ 
	//��ʾselect
	var selects = document.getElementsByTagName( "Select"); 
	for(var i = 0 ; i <selects.length;i++) 
		{ 
			selects[i].style.display= "block"; 
		} 
		
	//��ʾfalsh	
	var objs = document.getElementsByTagName("OBJECT");
	for(i = 0;i < objs.length; i ++) {
		if(objs[i].attributes['oldvisibility']) {
			objs[i].style.visibility = objs[i].attributes['oldvisibility'].nodeValue;
			objs[i].removeAttribute('oldvisibility');
		}
	}
	
	var objs = document.getElementsByTagName("embed");
	for(i = 0;i < objs.length; i ++) {
		if(objs[i].attributes['oldvisibility']) {
			objs[i].style.visibility = objs[i].attributes['oldvisibility'].nodeValue;
			objs[i].removeAttribute('oldvisibility');
		}
	}
} 

function opendialog(otherurl,othretitle,dw,wh){
	floatDiv(othretitle,dw,wh);
	getajax(otherurl,'messagebox_body');
	//hideAllSelect();
}

function ajaxpost_bbs(theform){
	var ajaxframeid = 'ajaxframe';
	var ajaxifrm = document.getElementById(ajaxframeid); 

	if(ajaxifrm == null) {
		html='<iframe name="'+ajaxframeid+'" id="'+ajaxframeid+'" style="display: none" src="javascript:void(0)"></iframe>';
		$(document.body).append(html);
	}
	theform.target = ajaxframeid;
	return false;
}


//ajax function
// RqUrl ������Ӧ��·�� Ajax_Div��ʾ���ҳ�����Ĳ��� RqPar ��ѯ���ַ���

function Ajax_Get(Ajax_Div,RqUrl,RqPar){

	if (!Ajax_Div){ alert("error");return false;}
	if (!RqPar){ alert("error");return false;}
	if (!RqUrl) $('#'+ Ajax_Div +'_menu').html('����ʧ��... ��ˢ��ҳ������');
	
	$.ajax({
		url: RqUrl,
		cache: false,
		data: RqPar,
		beforeSend: function(XMLHttpRequest){
			Loadingstr=ShowLoading();
			$('#'+ Ajax_Div +'_menu').html(Loadingstr);
		},
		success: function(html){
			$('#'+ Ajax_Div +'_menu').html(html);
		},
		complete: function(){
			$('#loading').remove();
		},
		error: function(){
			$('#'+ Ajax_Div +'_menu').html('����ʧ��...');
		}
	}); 
}

