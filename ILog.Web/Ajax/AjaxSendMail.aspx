<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxSendMail.aspx.cs" Inherits="ILog.Web.Ajax.AjaxSendMail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    
    <link href="http://simg.instrument.com.cn/ilog/blue/css/base.css" rel="stylesheet" type="text/css" />
    <link href="http://simg.instrument.com.cn/ilog/blue/css/module.css" rel="stylesheet" type="text/css" />
    
    <script src="../js/jquery-1.3.2.js" type="text/javascript" language="javascript"></script>
    <script language="javascript" type="text/javascript" src="../js/common.js"></script>
    <script language="javascript" type="text/javascript" src="../js/Messages.js"></script>
    <script language="javascript" type="text/javascript" src="../js/jquery.cookie.js"></script>
    <script src="../js/VipILogHome.js" type="text/javascript"></script>
    
    
    <script language="javascript" type="text/javascript">

 $(document).ready(function() 
{
     //收件人下拉框
     $("#towho").keyup(function()
     {
        //回车键搜提示无效
        if(!isEnterKey())
        {
            searchtowho();
        }
        
        //上下键处理
        funListBeginUp_ms(event);
     }); 


    //接收操作类型
    var ation = getParameter("ation");
    
    $("#ation_i").val(ation);
    
    
//    alert(ation);
    
     //获取回车事件
     getenterevent_ms();
});



        //发送站短
        //ation：1站短第一个页面，2站短第一个页面，3站短第一个页面
        function SendMail()
        {
            //收信人
            var towho = $("#towho");
            
            //站短内容
            var content = $("#content_b");
            
            var strTowhoValue = towho.val();
            var strContentValu = content.val();
            
            //校验收信人
            if (strTowhoValue == "" || strTowhoValue == null) 
            {
                window.parent.showTipe("请输入收信人！",0);
                towho.focus();
                return false;
            }
            if (ignoreSpaces(strTowhoValue) == "") 
            {
                window.parent.showTipe("请输入收信人！",0);
                towho.focus();
                return false;
            }
            if (HTMLEncode(strTowhoValue) == "") 
            {
                window.parent.showTipe("请输入收信人！",0);
                towho.focus();
                return false;
            }
            if (removeHTMLTag(strTowhoValue) == "") 
            {
                window.parent.showTipe("请输入收信人！",0);
                towho.focus();
                return false;
            }

            //校验站短内容
            if (strContentValu == "" || strContentValu == null) 
            {
                window.parent.showTipe("请输入内容！",0);
                content.focus();
                return false;
            }
            if (ignoreSpaces(strContentValu) == "") 
            {
                window.parent.showTipe("请输入内容！",0);
                content.focus();
                return false;
            }
            if (HTMLEncode(strContentValu) == "") 
            {
                window.parent.showTipe("请输入内容！",0);
                content.focus();
                return false;
            }
            if (removeHTMLTag(strContentValu) == "") 
            {
                window.parent.showTipe("请输入内容！",0);
                content.focus();
                return false;
            }
            
            if(Getlength(strContentValu) > 1000)
            {
                window.parent.showTipe("站短内容过长！",0);
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
                data: "{towho:'" + strTowhoValue + "',content:'" + strContentValu + "',subject:'站短标题'}",
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

//                        alert(item.State);

                        if (idx == 0) 
                        {
                            if (item.State == "0") 
                            {
                                alert("加载错误！");
                            }
                            else if (item.State == "2")  //无数据不显示分页控件
                            {
                                window.parent.showTipe("未找到收件人！",0);

                                return false;   //非法收件人
                            }
                            else if (item.State == "3")  //无数据不显示分页控件
                            {
                                window.parent.showTipe("10分钟之内不能重复发送！",0);

                                return false;   //非法收件人
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
//                                alert($("#ation_i").val());

                                //显示提示
                                window.parent.showsuccess(0,$("#ation_i").val());

                                return true;
                            }
                        }
                    });
                    
                },
                //出错调试         
                error: function(x, e) {

                    alert(x);
                    alert(e);

//                    window.location.href = "http://c.instrument.com.cn/art/ilog/404.asp";
                },
                //执行成功后自动执行           
                complete: function(x) {

                }
            });
        }
 
 
//是否在显示搜索提示时第一次按上键，如果是要选中最后一项，该变量默认是不选中的
 var isp = false;
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
            window.parent.showTipe("请输入收信人！",0);
            towho.focus();
            return false;
        }
        if (HTMLEncode(strTowhoValue) == "") 
        {
            window.parent.showTipe("请输入收信人！",0);
            towho.focus();
            return false;
        }
        if (removeHTMLTag(strTowhoValue) == "") 
        {
            window.parent.showTipe("请输入收信人！",0);
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
                        strList += "<li class=\"\" style=\"cursor:hand\" id=\"il_s"+idx+"\" onclick=\"Getnickname_Box('" + item.nickname + "')\" ><a href=\"javascript:void(0)\" id=\"hrSearchTowhPeple" + idx + "\" >" + item.nickname + "</a></li>";  

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
                
                //重新搜索数据时要初始化是否在搜索框中直接按上键的标记
                isp = false;
            }
            else    //没有数据隐藏掉提示框
            {
                $("#GetSearchTowho_Menu").hide();
            }
            
            
            //鼠标滑动保持唯以选中的样式（下拉项）
            $("#GetSearchTowho_Menu li").mouseover(function() 
            {
                //鼠标滑过
                $(this).addClass("WindowBG");
                var index = $("#GetSearchTowho_Menu li").index($(this));
                
                //开始遍历
                $("#GetSearchTowho_Menu li").each(function(i)
                {
                    if(i != index)
                    {
                        $(this).removeClass();
                    }
                })

//            }).mouseout(function() { //鼠标滑出
//                $(this).removeClass("WindowBG");

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
    
    //判断是否用鼠标选择搜索提示结果，如果是隐藏下拉，搜索框获取焦点，并执行搜索
    var event=arguments.callee.caller.arguments[0]||window.event; 

    if (event.keyCode != 38 && event.keyCode != 40 && event.keyCode != undefined)
    {
        $("#GetSearchTowho_Menu").hide();
        $("#towho").focus();
    }

}


//只能提示上下键
//上下键处理
//搜索下拉
function funListBeginUp_ms(e) 
{
    var keynum;

    if (window.event) // IE
    {   
        keynum = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        keynum = e.which;
    }

    if (keynum != 13 && keynum != 38 && keynum != 40) 
    {
        return false;
    }
    else if (keynum == 13) 
    {
        funListBeginUpUL_ms("GetSearchTowho_Menu", 2);
    }
    else if (keynum == 38) 
    {
        funListBeginUpUL_ms("GetSearchTowho_Menu", 0);
    }
    else if (keynum == 40) 
    {
        funListBeginUpUL_ms("GetSearchTowho_Menu", 1);
    }

    return false;

}


//上下键处理
function funListBeginUpUL_ms(NameID, vType) 
{
    var bl = true;

    //用户在搜索结果提示按上键直接选中最后一项
    if (vType == 0 && !isp) 
    {
        var ilsize = $("#GetSearchTowho_Menu li").size();

        $("#il_s"+ilsize).addClass("WindowBG");
        Getnickname_Box($("#il_s"+ilsize).text());
        
        isp = true;
    }
    else
    {
        $("#" + NameID + " li").each(function(i)
        {
            if (vType == 0) 
            {
                if ($(this).hasClass("WindowBG")) 
                {
                    if (bl) 
                    {
                        var index = $("#GetSearchTowho_Menu li").index($(this));
                        var ilsize = $("#GetSearchTowho_Menu li").size();
                   
                        //判断是不是选到第一个，如果是需要循环到最后一个
                        if(index == 0)
                        {

                            $(this).removeClass();
                            $("#il_s"+ilsize).addClass("WindowBG");
                            Getnickname_Box($("#il_s"+ilsize).text());
                        }
                        else
                        {
                            $("#GetSearchTowho_Menu li.WindowBG").prev().addClass("WindowBG");
                            $("#GetSearchTowho_Menu li.WindowBG").next().removeClass("WindowBG");
                            
                            
                            //开始遍历
                            $("#GetSearchTowho_Menu li").each(function(i)
                            {
                                if(i != (index -1) && index != 0)
                                {
                                    $(this).removeClass();
                                }
                            })
                        
                            //把选中的值放入搜索框中
                            if($(this).prev().text() != "")
                            {
                                Getnickname_Box($(this).prev().text());
                            }
                            else    //如果到了最后一个就保持当前选中的值
                            {
                                Getnickname_Box($(this).text());
                            }
                        }

                        bl = false;

                    }
                }
            }
            else if (vType == 1) 
            {
                if ($(this).hasClass("WindowBG")) 
                {
                    if (bl) 
                    {
                        var index = $("#GetSearchTowho_Menu li").index($(this));
                        var ilsize = $("#GetSearchTowho_Menu li").size();
                    
                        //判断是不是选到最后一个，如果是需要循环到第一个
                        if(index == (ilsize - 1))
                        {
                            $(this).removeClass();
                            $("#il_s1").addClass("WindowBG");
                            Getnickname_Box($("#il_s1").text());
                        }
                        else
                        {
                            //向下
                            $("#GetSearchTowho_Menu li.WindowBG").next().addClass("WindowBG");
                            $("#GetSearchTowho_Menu li.WindowBG").prev().removeClass("WindowBG");
                            
                            
                            //开始遍历，除了当前选中的选项其他都移除
                            $("#GetSearchTowho_Menu li").each(function(i)
                            {   
                                if(i != (index + 1) && (index + 1) != ilsize)
                                {
                                    $(this).removeClass();
                                }
                            })

                            //把选中的值放入搜索框中
                            if($(this).next().text() != "")
                            {
                                Getnickname_Box($(this).next().text());
                            }
                            else    //如果到了最后一个就保持当前选中的值
                            {
                                Getnickname_Box($(this).text());
                            }
                            
                            bl = false
                        }
                    }
                }
                if(bl)    //选择第一条搜索提示
                {
                    //只要不是第一次按上就改变“第一次上键的状态”
                    isp = true;
                    
                    $("#il_s1").addClass("WindowBG");

                    Getnickname_Box($("#il_s1").text());
                    
                }
            }
            else if (vType == 2) 
            {
                //回车
                if ($(this).hasClass("WindowBG")) 
                {
                    var strKeyWord = "";
                
                    //判断是否按了上下键如果没有说明用户想做模糊查询
                    if(!isUpDownKey())
                    {
                        strKeyWord = $("#towho").val();
                    }
                    else
                    {
                        strKeyWord = $(this).text();
                    }
                
                    Getnickname_Box(strKeyWord);
                    
                    GetSearchList(""+vServiceUrl + "VipMail.asmx/GetMailList",1,45,strKeyWord);
                }
            }

        })
    }
}

//获取回车事件（用户可以使用回车选择收件人）
function getenterevent_ms()
{
     $(function() 
     {
            $("input[type='text']").keypress(function(evt) 
            {
                evt = (evt) ? evt : ((window.event) ? window.event : "");
                var key = evt.keyCode ? evt.keyCode : evt.which;
                
                if (key == 13) 
                {
                    //如果没有选择在敲回车时就默认选择第一个
                    if (!isp) 
                    {
                        Getnickname_Box($("#il_s1").text());
                    }
                
                    $("#GetSearchTowho_Menu").hide();
                    $("#towho").focus();
                    
                    return false;
                }
            });
        });
}


    </script>
    
</head>
<body>
    <div style="width:100%;height:100%">
    
   <h1 class="WindowTil G4"><a href="javascript:void(0);" onclick="window.parent.closeDivlog();" ><img class=" R Img" src="http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif" alt="关闭"  /></a>站短</h1>
    <div class="WindowBox" style="position:relative">
     
    <ul class="WindowBD G4">
    <li><span class="Span L">收信人：</span>
      <input class="input" name="towho" maxlength="15" id="towho" type="text"  /></li>
      
      <!--智能提示框容器-->                            
      <ul class="WindowMenu WindowW Line_blue L30" id="GetSearchTowho_Menu" style="display:none; top:40px; left:80px; ">
      

      </ul>
      
      
    <li><span class="Span L">内&nbsp;&nbsp;容：</span>
      <textarea name="content_b" id="content_b" onkeyup="checkSendMail_Box('content_b')" class="textarea G4" style=" width:350px; height:60px;" cols="20" rows="3"></textarea>
      
      <!--操作类型-->
      <input type="hidden" id="ation_i" name="ation_i" />
    </li>
    </ul>
    <Div class="Hr_10"></Div>
    <div class=" WindowBD_btn">
    
    
    <!--提交按钮处-->
    <div id="btnImg_div_b" >
    <div class="WinBtn  R" ><a href="javascript:void(0);" class="White"><span><input name="发送" type="button" id="发送" value="发送" onclick="SendMail();" /></span></a></div>
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
