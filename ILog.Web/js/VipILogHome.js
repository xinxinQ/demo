
//生成随机数
var rand=Math.random(Math.random()*10000)

//用户左侧信息组装.by lx on 20120626
function VipILogHome(servicesUrl, userid)
 {

    if($.trim(userid)=="")
    {
        
        window.location='../UserTest.aspx';
        
    }

    $.ajax({
    //请求WebService Url         
    url:""+servicesUrl+"",
    //请求类型,请据WebService接受类型定制          
    type: "POST",  
    //预期指定服务器返回类型
    dataType: "json",
    //内容返回类型            
    contentType: "application/json;",
    //缓存
    cache: false,
    //请求参数              
     data: "{userid:'" + userid + "',i:'" + rand + "'}",  
    //成功           
    success: function(json) 
    {            
            
         //获取服务器的值        
         var dataObj = eval("(" + json.d + ")"); //转换为json对象
                
          if(dataObj.UrlState==1)
          {
          
            var face=$.trim(dataObj.face)=="" ? "default.png" : dataObj.face;
            
            var fontSize="";
            
            //字体判断
            if(dataObj.concern <= 9999 || dataObj.fan <= 9999 || dataObj.blog<=9999)
            {
            
                fontSize ="F12";
                
            }else
            {
            
                fontSize ="F14";
            
            }
           
          
            var result="<div class=\" Llog_Head\">";
            result += "<a href=\"/u\">";
            result += "<img src=\"../images/face/big/"+face+"\" alt=\""+dataObj.nickname+"\" width=\"140\" height=\"140\" class=\"img\" id=\"face\" />";
            result += "</a><h1 class=\"F14\">";
            result += "<a href=\"/u\" id=\"nickname\">"+dataObj.nickname+"</a>";
            result += "</h1><div class=\"Hr_6\"></div></div>";
            result += "<div class=\"Llog_btn\">";
            result += "<a href=\"../settings/\">";
            result += "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_ws.gif\" class=\"L\" alt=\"完善资料\" />";            
            result += "</a>";
            result += "<div class=\"Jd R\">";
            result += "<div class=\"Jd_lv\" style=\"width: "+dataObj.integrity+"%\" id=\"\"></div>";
            result += "<div class=\"Jd_font Fw\" id=\"percentage\">"+dataObj.integrity+"%</div>";
            result += "</div></div>";
            result += "<div class=\"Hr_10\"></div>";
            result += "<div class=\"Llog_info\">";            
            result += "<div class=\"box\">";
            result += "<strong class=\""+fontSize+"\">";
            result += "<a href=\"../Follow\" id=\"userConcern\">"+dataObj.concern+"</a>";
            result += "</strong><br>";
            result += "<a href=\"../Follow\">关注</a>";
            result += "</div>";
            result += "<div class=\"box\">";
            result += "<strong class=\" "+fontSize+"\">";
            result += "<a href=\"../fans\" id=\"userFan\">"+dataObj.fan+"</a>";
            result += "</strong><br> ";
            result += "<a href=\"../fans\">粉丝</a>";
            result += "</div>";
            result += "<div class=\"box  box_no\">";
            result += "<strong class=\""+fontSize+"\">";
            result += "<a href=\"../u\" id=\"blog\">"+dataObj.blog+"</a>";
            result += "</strong><br>";
            result += "<a href=\"../u\">微博</a>";
            result += "</div></div>";
            result += "<div class=\"Hr_10\"></div>";
            result += "<div class=\" Llog_line\"></div>";
            result += "<div class=\"Hr_10\"></div>";
            
            if($.trim(dataObj.Insignia) != "")
            {
                
                result +="<div class=\"Llog_Jz\">"+dataObj.Insignia+"</div>";
                result += "<div class=\"Hr_20\"></div><div class=\"Hr_10\"></div><div class=\" Llog_line\"></div><div class=\"Hr_10\"></div>";              
            
            }           
          
          $("#vipIlog").html(result);
          
          }
         
        
　　     
　　 }, 
　 //出错调试         
   error: function(x, e) {  
   
     
         //alert(x.responseText); 
     }, 
    //执行成功后自动执行           
    complete: function(x) {      
                          
      }          
  });
 
 } 




























