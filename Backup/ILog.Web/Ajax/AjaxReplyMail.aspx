﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxReplyMail.aspx.cs" Inherits="ILog.Web.Ajax.AjaxReplyMail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet" type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet" type="text/css" />
    
    <script src="../js/jquery-1.3.2.js" type="text/javascript" language="javascript"></script>
    <script language="javascript" type="text/javascript" src="../js/common.js"></script>

    
    <script language="javascript" type="text/javascript">


 $(document).ready(function() 
{
    //接收操作类型
    var ation = getParameter("ation");
    
    $("#ation_i").val(ation);
    
//    alert(ation);

         //收件人下拉框
     $("#towho").keyup(function()
     {
            searchtowho();
     }); 

    
});


        //发送站短
//大于0是回复的
function ReplyMail()
{
    //收件人
    var towho = $("#towho");
    
    //站短内容
    var content = $("#content_r");
    
    var strTowhoValue = towho.val();   
    var strContentValu = content.val();

   
    //校验收信人
    if (strTowhoValue == "" || strTowhoValue == null) 
    {
        alert("请输入收信人！");
        towho.focus();
        return false;
    }
    if (ignoreSpaces(strTowhoValue) == "") 
    {
        alert("请输入收信人！");
        towho.focus();
        return false;
    }
    if (HTMLEncode(strTowhoValue) == "") 
    {
        alert("请输入收信人！");
        towho.focus();
        return false;
    }
    if (removeHTMLTag(strTowhoValue) == "") 
    {
        alert("请输入收信人！");
        towho.focus();
        return false;
    }
   
    //校验站短内容
    if (strContentValu == "" || strContentValu == null) 
    {
        alert("请输入内容！");
        content.focus();
        return false;
    }
    if (ignoreSpaces(strContentValu) == "") 
    {
        alert("请输入内容！");
        content.focus();
        return false;
    }
    if (HTMLEncode(strContentValu) == "") 
    {
        alert("请输入内容！");
        content.focus();
        return false;
    }
    if (removeHTMLTag(strContentValu) == "") 
    {
        alert("请输入内容！");
        content.focus();
        return false;
    }
    
    if(Getlength(strContentValu) > 1000)
    {
        alert("站短内容过长！");
        content.focus();
        return false;
    }
    
    //开始发送
    $.ajax({
        //请求WebService Url         
        url: "" + vServiceUrl + "VipMail.asmx/SendMail",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        cache: false,
        //请求参数              
        data: "{towho:'" + strTowhoValue + "',content:'" + strContentValu + "',subject:'回复标题'}",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var strList = "";

            //获取当然用户id
            //var userid = $.cookie('useid'); 

            //循环获取值
            $.each(dataObj.List, function(idx, item) 
            {

                if (idx == 0) 
                {
                    if (item.State == "0") 
                    {
                        alert("加载错误！");
                    }
                    else if (item.State == "2")  //无数据不显示分页控件
                    {
                        alert("未找到收件人！");

                        return false;   //无数据不再往下执行
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if(item.exec != undefined)
                    {
                       //显示提示
                        window.parent.showsuccess_r($("#ation_i").val());

                        return true;
                    }
                }
            });
            
        },
        //出错调试         
        error: function(x, e) {

            //alert("加载异常");

            window.location.href = "http://c.instrument.com.cn/art/ilog/404.asp";
        },
        //执行成功后自动执行           
        complete: function(x) {

        }
    });
}


   //只能提示收件人
function searchtowho()
{
     //收件人
    var towho = $("#towho");
    
    //站短内容
    var content = $("#content_r");
    
    var strTowhoValue = towho.val();   
   
    //校验收信人
    if (strTowhoValue != "") 
    {
        if (ignoreSpaces(strTowhoValue) == "") 
        {
            alert("请输入收信人！");
            towho.focus();
            return false;
        }
        if (HTMLEncode(strTowhoValue) == "") 
        {
            alert("请输入收信人！");
            towho.focus();
            return false;
        }
        if (removeHTMLTag(strTowhoValue) == "") 
        {
            alert("请输入收信人！");
            towho.focus();
            return false;
        }
    }


    //开始发送
    $.ajax({
        //请求WebService Url         
        url: "" + vServiceUrl + "VipMail.asmx/GetNickName_SendMail",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        cache: false,
        //请求参数              
        data: "{nickname:'" + strTowhoValue + "'}",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var strList = "";

            //获取当然用户id
            //var userid = $.cookie('useid'); 

            //循环获取值
            $.each(dataObj.List, function(idx, item) 
            {

                if (idx == 0) 
                {
                    if (item.State == "0") 
                    {
                        alert("加载错误！");
                    }
                    else if (item.State == "2")  //无数据不显示分页控件
                    {
//                        alert("未找到收件人！");
                    
                        //没有搜到就隐藏提示框 
                        
                        return false;   //无数据不再往下执行
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if(item.nickname != undefined)
                    {
                        strList += "<li><a href=\"javascript:void(0)\" id=\"hrSearchTowhPeple" + idx + "\" onclick=\"Getnickname_Box('" + item.nickname + "')\">" + item.nickname + "</a></li>";  

                        return true;
                    }
                }
            });

            //加载下拉菜单
            $("#GetSearchTowho_Menu").html(strList);
            
            //有数据显示下拉框
            if(strList != "")
            {    
                GetSearchTowhUpList();
            }
            else    //没有数据隐藏掉提示框
            {
                $("#GetSearchTowho_Menu").hide();
            }

            
        },
        //出错调试         
        error: function(x, e) {

            //alert("加载异常");

            window.location.href = "http://c.instrument.com.cn/art/ilog/404.asp";
        },
        //执行成功后自动执行           
        complete: function(x) {

        }
    });
    
}

 

      //按字节计算获取字符的长度，汉字为2个字节
    function Getlength(str)
    {
        var len = 0;  
        
        var oVal = str; 
        var oValLength = 0; 
        oVal.replace(/n*s*/, '') == '' ? oValLength = 0 : oValLength = oVal.match(/[^ -~]/g) == null ? oVal.length : oVal.length + oVal.match(/[^ -~]/g).length; 
        
        return oValLength; 
    }
 
 
     //只能提示部分
     
     
     //搜索下拉
    function GetSearchTowhUpList()
    {

        //收件人文本框值
        var txtSearchValue=$("#towho").val();

        //框内数据不为空就开始定位
       if(txtSearchValue!=null && txtSearchValue!="")
       {
           setMenuPositionsSearch("GetSearch");
           MenuDivShow("GetSearch");
       } 
       else
       {
           $("#GetSearchTowho_Menu").hide();
       }
       
    }
     
    //下拉框定位
    function setMenuPositionsSearch(ShowID)
    {
	    var offset = $('#towho').offset();
	    var divheight = $('#towho').innerHeight();

	    var leftpadd = 0;
    	
	    $('#' + ShowID +'Towho_Menu').css
	    ({
		    //'left':offset.left + -208, //左右定位
		    //'top':offset.top+20,       //上下定位
		    'position':'absolute'
	    }).show();

    }

    //控制隐藏显示
    function MenuDivShow(showdiv)
    {
	    $('#'+showdiv+'Towho_Menu').mouseover(function () { $(this).show(); });
	    $('#'+showdiv+'Towho_Menu').mouseout(function () { $(this).hide(); });
	    $('#'+showdiv).mouseout(function () { $('#'+showdiv+'Towho_Menu').hide(); });
    }
     
    //把选中的收件人放入框中
    //towho：收件人
    function Getnickname_Box(towho)
    {
        $("#towho").val(towho);
        $("#GetSearchTowho_Menu").hide();
    }


       
    </script>
    
</head>
<body>
<div class="WindowWark">
    <%--<h1 class="WindowTil G4"><a href="#"><img class=" R Img" src="http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif" alt="关闭"  /></a>发短信</h1>--%>
    <div class="WindowBox">
     
    <ul class="WindowBD G4">
    <li><span class="Span L">收信人：</span>
      <input class="input" name="towho" id="towho"   maxlength="15" value="<%=strTow %>" type="text"  />
      
                  <!--智能提示框容器-->
      <ul class="WindowMenu WindowW Line_blue L30" id="GetSearchTowho_Menu" style="display:none; top:41px; left:81px; ">
      

      </ul>
      
    </li>
    <li><span class="Span L">内&nbsp;&nbsp;容：</span>
      <textarea name="content_r" id="content_r" onkeyup="checkSendMail_Box_ReplyMail('content_r')" class="textarea G4" cols="45" rows="5"></textarea>
            <!--操作类型-->
      <input type="hidden" id="ation_i" name="ation_i" />
      
    </li>
    </ul>
    <Div class="Hr_10"></Div>
    
    
    <div class=" WindowBD_btn">
    
        <!--提交按钮处-->
    <div id="btnImg_div_b" >
    <div class="WinBtn  R"><a href="javascript:void(0);" class="White"><span><input name="发送" type="button" id="发送" value="发送" onclick="ReplyMail();" /></span></a></div>
     </div>
     <!--提交按钮处-->

    <span class="R G4"><span id="Prompt_f">你还可以输入</span><font class="publish_num" id="prompt_b" >500</font><span id="Prompt_f2">字</span>&nbsp;</span>


    <div></div>
    </div>
    <DIV class="Hr_10"></DIV>
    </div>
    </div>
</body>
</html>
