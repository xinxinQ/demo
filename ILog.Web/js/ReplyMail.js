
//发送站短
//大于0是回复的
//ation_p：1站短第一个页面，2站短第一个页面，3站短第一个页面
function ReplyMail(ation_p)
{
//    alert(ation_p);

    //站短内容
    var content = $("#content_i");
    
    var strTowhoValue = $("#tohow_a2").html();   //拿到收信人
    var strContentValu = content.val();;
    
//    alert(strTowhoValue);
//    alert(strContentValu);
   
    //校验站短内容
    if (strContentValu == "" || strContentValu == null) 
    {
        showTipe("请输入内容！",0);
        content.focus();
        return false;
    }
    if (ignoreSpaces(strContentValu) == "") 
    {
        showTipe("请输入内容！",0);
        content.focus();
        return false;
    }
    if (HTMLEncode(strContentValu) == "") 
    {
        showTipe("请输入内容！",0);
        content.focus();
        return false;
    }
    if (removeHTMLTag(strContentValu) == "") 
    {
        showTipe("请输入内容！",0);
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
//                        alert("加载错误！");
                    }
                    else if (item.State == "2")  //无数据不显示分页控件
                    {
                        showTipe("未找到收件人！",0);

                        return false;   //无数据不再往下执行
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
                        var id = $("#id_").val();  //记录到隐藏域

                        if(ation_p == 1)
                        {     
                            //重新绑定
                            GetList(""+vServiceUrl + "VipMail.asmx/GetMailList",1,45);
                        }
                        else if(ation_p == 2)
                        {
                            //具体站短
                            GetList2("" + vServiceUrl + "VipMail.asmx/GetAllMailList", 1, 45,id);
                        }
                        else if(ation_p == 3)   //删除页用户页面上固定框发送站短时要跳转到第二个页面
                        {
                            window.location.href = "msgs_" + id;

                            //删除站短
//                            GetList3("" + vServiceUrl + "VipMail.asmx/GetAllMailList", 1, 45,id);
                        }
                        
                        //清空文本框
                        $("#content_i").val("");
                        
                        //还原提示字数
                        $("#prompt").html("还可以输入500字");
                        
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


//悬浮框对象
var objDivlog;
//id：站短流水号
//ation：1站短第一个页面，2站短第一个页面，3站短第一个页面
function showdialog_r(id,ation)
{
    //悬浮框函数
    objDivlog = $.dialog({
        id: "Divlog",
        title: false,
        content: "url:Ajax/AjaxReplyMail.aspx?id=" + id + "&ation=" + ation,
        max: false,
        min: false,
        lock: true,
        cache: false,
        width: 502,
        height: 187

    });
}

//关闭弹出的窗口
function closeDivlog() 
{
    objDivlog.close(); 
}


//悬浮框对象发送成功
var objSuccess;


//回复成功提示框
//ation：1站短第一个页面，2站短第一个页面，3站短第一个页面
function showsuccess_r(ation)
{
    //关闭表单
    closeDivlog();

    //悬浮框函数
    objSuccess = $.dialog({
        id: "Divlog2",
        title: "回复成功",
        content: "url:Ajax/AjaxSendMailSuccess_r.aspx",
        max: false,
        min: false,
        lock: true,
        cache: false,
        time:2,
        width: 352,
        height: 102

    });
    
    if(ation == 1)
    {     
        //重新绑定
        GetList(""+vServiceUrl + "VipMail.asmx/GetMailList",1,45);
    }
    else if(ation == 2)
    {
        //具体站短
        GetList2("" + vServiceUrl + "VipMail.asmx/GetAllMailList", 1, 45,$("#id_").val());
    }
    else if(ation == 3)
    {
        //删除站短
        GetList3("" + vServiceUrl + "VipMail.asmx/GetAllMailList", 1, 45,$("#id_").val());
    }
                        
}

//关闭弹出的发送成功窗口
function closesuccess() 
{
    objSuccess.close(); 
}