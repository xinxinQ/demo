//islogin
//var vServiceUrl = "http://localhost:8121/";

var j_logindialog;

var j_mobiledialog;

function LoginDiv(LoginSource) {

    var hasright = false;

    ajaxurl = vServiceUrl + "Vip.asmx/CheckUserLoginorIlogState";

    $.ajax({
        type: "POST",
        url: "" + ajaxurl + "",
        cache: false,
        //预期指定服务器返回类型
        dataType: "json",
        async: false,
        //内容返回类型            
        contentType: "application/json;",
        data: "{}",
        success: function(data, status) {
            //获取服务器的值        
            var dataObj = eval("(" + data.d + ")"); //转换为json对象
            //循环获取值
            $.each(dataObj.CheckLogin, function(idx, item) {
                if (item.State == 2)//未登录
                {
                    j_logindialog = $.dialog({
                        id: "divLoginCheck",
                        title: false,
                        content: showlogin(LoginSource),
                        max: false,
                        min: false,
                        lock: true,
                        cache: false,
                        left: "40%"

                    });
                }
                else if (item.State == 3)//未认证手机
                {
                    showMobile();
                }
                else {
                    hasright = true;
                }
            });
        }, error: function(result, status) {
            if (status == 'error') {
                alert(result.responseText);
            }
        },
        complete: function() {
        }
    });
    return hasright;

}

//关闭登录框
function CloseLoginDialog() {
    j_logindialog.close();

}

//关闭手机框
function CloseMobileDialog(msg) {
    if (msg != "") {
        showTipe(msg, 1);
    }
    j_mobiledialog.close();

}

//显示登录框
function showlogin(LoginSource) {

    var Logintable = "";
    Logintable += '<div class="ULOpenArea" id="login_win" style="z-index:99999999;">';
    Logintable += '<div class="ULP">';
    Logintable += '<div class="ULL" style="font-family:黑体; font-size:16px; height:35px; line-height:35px; padding-left:15px;">欢迎登录仪器信息网</div>';
    Logintable += '<div class="ULR"><a class="ULmenu1" title="关闭" href="javascript:void(0);" onclick="CloseLoginDialog();">关闭</a></div>';
    Logintable += '<div class="ULHr_2"></div>';
    Logintable += '<div  style="padding-left:15px;">';
    Logintable += '<form id="first_login" name="first_login" method="post" action="/test2.aspx" onSubmit="return chkLogin();" style="padding:0; margin:0">';
    Logintable += '<div class="ULHr_10"></div>';
    Logintable += '<div class="ULL" style="width:40px; font-size:12px;line-height:26px;">账号：</div>';
    Logintable += '<div class="ULL" style="width:225px;">';
    Logintable += '<input class="ULLoginArea" type="text" name="username" id="username" style="color:#999999; margin-top:0px;" value="邮箱/用户名/手机/展位号" onfocus="this.value=\'\';"/>';
    Logintable += '</div>';
    Logintable += '<div class="ULHr_10"></div>';
    Logintable += '<div class=" ULL" style="width:40px;font-size:12px; line-height:26px;">密码：</div>';
    Logintable += '<div class="ULL" style="width:225px;">';
    Logintable += '<input class="ULLoginArea" type="password" name="password" id="password" style="color:#999999; margin-top:0px;"/></div>';
    Logintable += '<div class="ULHr_10"></div>';
    Logintable += '<div class="ULL" style="width:40px;line-height:26px;">&nbsp;</div>';
    Logintable += '<div class="ULL" style="width:225px;font-size:12px;"><span><INPUT id="ISmemorize" type="checkbox" checked="checked" value="1" name="ISmemorize"> </span><span>记住我的登录状态</span></div>';
    Logintable += '<div class="ULHr_10"></div><div class="ULL" style="width:40px; line-height:26px;">&nbsp;</div>';
    Logintable += '<div class="ULL" style="width:225px;"><div class="ULL" style="padding-right:10px;">';
    //    var returnUrl = window.location.href;
    //    if (returnUrl.indexof("?") > 0) {
    //        returnUrl += "&loginshort=1";
    //    }
    //    else {
    //        returnUrl += "?loginshort=1";
    //    }
    if ($("#code") != undefined) {
        var activecode = $("#code").val();
        Logintable += '<input name="acode" type="hidden" id="acode" value="' + activecode + '" />';
    }
    Logintable += '<input name="strURL" type="hidden" id="strURL" value="' + window.location.href + '" />';
    Logintable += '<input name="LoginSource" type="hidden" id="LoginSource" value="' + LoginSource + '" />';
    Logintable += '<input name="LoginInit" type="hidden" id="LoginInit" value="1" />';
    Logintable += '<input  type="image" name="button" src="http://simg.instrument.com.cn/vip/20111020/images/iconb.gif" id="button" value="提交" alt="登录 " />';
    Logintable += '</div>';
    Logintable += '<div class="ULL" style="line-height:28px;"><a class="ULBlue" href="http://www.instrument.com.cn/vip/getpass.htm" target="_blank">忘记密码</a></div>';
    Logintable += '</div></form>';
    Logintable += '<div class="ULHr_10"></div></div><div class="ULHr_2"></div><div class="ULHr_10"></div><div>';
    Logintable += '<div class="ULL" style="padding-left:50px; line-height:25px;">还没有账号？</div>';
    Logintable += '<div class="ULR"><a class="ULmenu3" title="注册" href="http://www.instrument.com.cn/vip/Register01.asp?registerSource=' + LoginSource + '" target="_blank">注册</a></div>';

    return Logintable;

}

//显示手机认证登录框
function showMobile() {
    j_mobiledialog = $.dialog({
        id: "divMobileCheck",
        title: false,
        content: "url:../ajax/AjaxCheckMobile.aspx",
        max: false,
        min: false,
        lock: true,
        cache: false,
        width: 500,
        height: 320
    });

}

//验证表单
function chkLogin() {
    if (document.first_login.username.value == "") {
        showTipe("请输入用户名!", 0);
        document.first_login.username.focus();
        return false;
    }
    if (document.first_login.password.value == "") {
        showTipe("请输入密码!", 0);
        document.first_login.password.focus();
        return false;
    }

}

var confirmOpenDialogDialog = "";

//弹出开通ilog的悬浮框
function confirmOpenIlog(CheckNumber, Mobile) {
    var html = "<div class=\"WindowWark350\">";
    html += "<h1 class=\"WindowTil G4 F14\"><a href=\"javascript:void(0);\" onclick=\"confirmOpenDialogDialog.close();\">";
    html += "<img class=\"R Img\" src=\"http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif\" alt=\"关闭\"  /></a>提示</h1>";
    html += "<div class=\"WindowBox Tc\">";
    html += "<div class=\" Tc F14 L30  WindowSak\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/ask.gif\" class=\"L\" />确定要开通ilog吗？</div>";
    html += "<div class=\"Hr_10\"></div>";
    html += "<div class=\"WinBtn_H R\"><span>";
    html += "<input name=\"btncancle\" type=\"button\" id=\"btncancle\" value=\"取消\" onclick=\"confirmOpenDialogDialog.close();return false;\" />";
    html += "</span></div>";
    html += "<div class=\"WinBtn  R\"><span>";
    html += "<input name=\"btnconfirm\" type=\"button\" id=\"btnconfirm\" value=\"确定\" onclick=\"openIlog('" + CheckNumber + "','" + Mobile + "');\" />";
    html += "</span></div>";
    html += "<div class=\"Hr_10\"></div>";
    html += "</div>";
    html += "</div>";

    confirmOpenDialogDialog = $.dialog({
        title: false,
        content: html,
        max: false,
        min: false,
        lock: true
    });

}

//开通ilog
function openIlog(CheckNumber, Mobile) {
    ajaxurl = vServiceUrl + "VipILog.asmx/ConfirmOpenIlog";
    $.ajax({
        type: "POST",
        url: "" + ajaxurl + "",
        cache: false,
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型
        contentType: "application/json;",
        data: "{CheckNumber:'" + CheckNumber + "',Mobile:'" + Mobile + "'}",
        success: function(data, status) {
            //获取服务器的值
            var dataObj = eval("(" + data.d + ")"); //转换为json对象
            showTipe(dataObj.msg, dataObj.state);
            if (dataObj.state == 1) {
                CloseMobileDialog("");
            }
            confirmOpenDialogDialog.close();
        }, error: function(result, status) {
            if (status == 'error') {
                alert(result.responseText);
            }
        },
        complete: function() {
        }
    });

}
