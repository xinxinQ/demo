

//悬浮框对象
var objDivlog;

//发送站短
//ation：1站短第一个页面，2站短第一个页面，3站短第一个页面
function showdialog(ation)
{
//    alert(ation);
    
    //悬浮框函数
    objDivlog = $.dialog({
        id: "Divlog",
        title: false,
        content: "url:Ajax/AjaxSendMail.aspx?ation="+ation,
        max: false,
        min: false,
        lock: true,
        cache: false,
        width: 505,
        height: 218,
        
        padding: 0 
    });
}

//关闭弹出的窗口
function closeDivlog() 
{
    objDivlog.close(); 
}



//悬浮框对象发送成功
var objSuccess;

//ation：0无用户定位关闭，1,：有用户名定位关闭
//ation_p：1站短第一个页面，2站短第一个页面，3站短第一个页面
function showsuccess(ation,ation_p)
{
    if(ation == 0)
    {
//        alert(ation_p);
        
        //关闭表单
        closeDivlog();
        
        if(ation_p == 1)
        {     
            //重新绑定
            GetList(""+vServiceUrl + "VipMail.asmx/GetMailList",1,45);
        }
        else if(ation_p == 2)
        {
            //具体站短
            GetList2("" + vServiceUrl + "VipMail.asmx/GetAllMailList", 1, 45,$("#id_").val());
        }
        else if(ation_p == 3)
        {
            //删除站短
            GetList3("" + vServiceUrl + "VipMail.asmx/GetAllMailList", 1, 45,$("#id_").val());
        }

    }
    else if(ation == 1)
    {
        //关闭表单
        closeDivlog_u();
        
        if(ation_p == 1)
        {     
            //重新绑定
            GetList(""+vServiceUrl + "VipMail.asmx/GetMailList",1,45);
        }
        else if(ation_p == 2)
        {
            //具体站短
            GetList2("" + vServiceUrl + "VipMail.asmx/GetAllMailList", 1, 45,$("#id_").val());
        }
        else if(ation_p == 3)
        {
            //删除站短
            GetList3("" + vServiceUrl + "VipMail.asmx/GetAllMailList", 1, 45,$("#id_").val());
        }
    }
    else
    {
        //关闭表单
        closeDivlog();
    }


    //悬浮框函数
    objSuccess = $.dialog({
        id: "Divlog2",
        title: false,
        content: "url:Ajax/AjaxSendMailSuccess.aspx",
        max: false,
        min: false,
        lock: true,
        cache: false,
        time:2,
        width: 352,
        height: 102

    });
}

//关闭弹出的发送成功窗口
function closesuccess() 
{
    objSuccess.close(); 
}


//悬浮框对象（定位用户名）
var objDivlog_u;

//可以定位到用户名
//nickname：用户昵称
//ation：1站短第一个页面，2站短第一个页面，3站短第一个页面
function showdialog_u(nickname,ation)
{
    //悬浮框函数
    objDivlog_u = $.dialog({
        id: "Divlog",
        title: false,
        content: "url:Ajax/AjaxSendMail_u.aspx?nickname=" +escape(nickname) + "&ation=" + ation,
        max: false,
        min: false,
        lock: true,
        cache: false,
        width: 505,
        height: 218
    });
}

//关闭弹出的窗口
function closeDivlog_u() 
{
    objDivlog_u.close(); 
}


 //发送站短
//ation：1站短第一个页面，2站短第一个页面，3站短第一个页面
function SendMail(ation)
{
    //收信人
    var towho = $("#tohow_a2");
   
    
    //站短内容
    var content = $("#content_i");
    
    var strTowhoValue = towho.html();
    var strContentValu = content.val();

    //校验收信人
    if (strTowhoValue == "" || strTowhoValue == null) 
    {
        showTipe("请输入收信人！");
        towho.focus();
        return false;
    }
    if (ignoreSpaces(strTowhoValue) == "") 
    {
        showTipe("请输入收信人！");
        towho.focus();
        return false;
    }
    if (HTMLEncode(strTowhoValue) == "") 
    {
        showTipe("请输入收信人！");
        towho.focus();
        return false;
    }
    if (removeHTMLTag(strTowhoValue) == "") 
    {
        showTipe("请输入收信人！");
        towho.focus();
        return false;
    }

    //校验站短内容
    if (strContentValu == "" || strContentValu == null) 
    {
        showTipe("请输入内容！");
        content.focus();
        return false;
    }
    if (ignoreSpaces(strContentValu) == "") 
    {
        showTipe("请输入内容！");
        content.focus();
        return false;
    }
    if (HTMLEncode(strContentValu) == "") 
    {
        showTipe("请输入内容！");
        content.focus();
        return false;
    }
    if (removeHTMLTag(strContentValu) == "") 
    {
        showTipe("请输入内容！");
        content.focus();
        return false;
    }
    
    if(Getlength(strContentValu) > 1000)
    {
        showTipe("站短内容过长！");
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

                if (idx == 0) 
                {
                    if (item.State == "0") 
                    {
//                        alert("加载错误！");
                    }
                    else if (item.State == "2")  //无数据不显示分页控件
                    {
                        showTipe("未找到收件人！");

                        return false;   //非法收件人
                    }
                    else if (item.State == "3")  //无数据不显示分页控件
                    {
                        showTipe("10分钟之内不能重复发送！");

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
                        if(ation == 1)
                        {     
                            //重新绑定
                            GetList(""+vServiceUrl + "VipMail.asmx/GetMailList",1,45);
                        }
                        else if(ation == 2)
                        {
                            //具体站短
                            GetList2("" + vServiceUrl + "VipMail.asmx/GetAllMailList", 1, 45,$("#id_").val());
                            
                            //清空回复框
                            $("#content_i").html("");
                        }
                        else if(ation == 3)
                        {
                            //删除站短
                            GetList3("" + vServiceUrl + "VipMail.asmx/GetAllMailList", 1, 45,$("#id_").val());
                        }
                        

                        return true;
                    }
                }
            });
            
        },
        //出错调试         
        error: function(x, e) {

            //alert("加载异常");

//            window.location.href = "http://c.instrument.com.cn/art/ilog/404.asp";
        },
        //执行成功后自动执行           
        complete: function(x) {

        }
    });
}