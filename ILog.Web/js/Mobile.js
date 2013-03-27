
$(document).ready(function() {
    //加载Ilog站内导航
    funGetTopMenuService("" + vServiceUrl + "IlogTopMenu.asmx/ILogGetTopMenu", "{}", "");
    //加载Ilog用户导航
    funGetTopUserMenuService("" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenu", "{MenuLive:'0'}", "");
    //加载ilog左侧菜单
    funGetSettingsleftMenuService("" + vServiceUrl + "ILogWebLeftMenu.asmx/ILogGetSettingsLeftMneu", "{MenuLive:'4'}", "");

    ShowTitle("修改手机");


    jQuery.validator.addMethod("isMobile", function(value, element) {
        if (value != "") {
            var pattern = /^1[358]\d{9}$/;
            if (!pattern.test(value)) {
                return false;
            }
            else {
                return true;
            }
        }
    }, "请输入正确的手机号码！");


    jQuery.validator.addMethod("notEqualMobile", function(value, element) {
        if (value == $("#oldmobile").val()) {
            return false;
        }
        else {
            return true;
        }
    }, "手机号码不可与原手机号码一致！");


    $("#form_input").validate({
        rules: {
            oldmobile: {
                required: true,
                remote: {
                    type: "GET",
                    async: false,
                    url: "../Ajax/AjaxCheckOldMobile.aspx?type=0",
                    cache:false,
                    data: {
                        oldmobile: function() {
                            return $("#oldmobile").val();
                        }
                    }
                }
            },
            mobile: {
                required: true,
                isMobile: true,
                notEqualMobile: true,
                  remote: {
                    type: "GET",
                    async: false,
                    url: "../Ajax/AjaxCheckOldMobile.aspx?type=2",
                    cache:false,
                    data: {
                        mobile: function() {
                            return $("#mobile").val();
                        }
                    }
                }
            },
            CheckNumber: {
                required: true,
                rangelength: [6, 6],
                remote: {
                    type: "GET",
                    async: false,
                    url: "../Ajax/AjaxCheckOldMobile.aspx?type=1",
                    data: {
                        mobile: function() {
                            return $("#mobile").val();
                        },
                        CheckNumber: function() {
                            return $("#CheckNumber").val();
                        }
                    }
                }
            }
        },
        messages: {
            oldmobile: {
                required: "请输入原手机号码！",
                remote: "请输入正确的原手机号码！"
            },
            mobile: {
                required: "请输入手机号码！",
                remote: "此手机号已被其它用户占用，请重新输入一个新的手机号！"
            },
            CheckNumber: {
                required: "请输入验证码！",
                rangelength: "请输入6位验证码！",
                remote: "手机验证码输入错误！"
            }
        },
        success: function(label) {
            label.addClass("valid").html("<img src=\"http://simg.instrument.com.cn/ilog/blue/images/OKK.gif\" alt=\"OK\" />");
        }
    });


});



//得到验证码
function GetCheckNumber() {
    //判断mobile格式
    var mobile = $.trim($("#mobile").val());
    if (mobile == "") {
        window.parent.showTipe("请输入手机号码！",0);
        return false;
    }
    else {
        var mobileRule = /^1[358]\d{9}$/;
        if (!mobileRule.test(mobile)) {
            window.parent.showTipe("请输入正确的手机号码！",0);
            return false;
        }
    }

    var ajaxurl = "../Ajax/AjaxSendMobile.aspx?mobile=" + mobile;
    $.ajax({
        url: ajaxurl,
        type: "get",
        cache: false,
        success: function(data, status) {
            if (data == "") {
                secs = 120;
                wait = secs * 1000;
                for (i = wait / 1000; i >= 0; i--) {
                    window.setTimeout("doUpdate(" + i + ")", i * 1000);
                }
            }
            else {
                window.parent.showTipe(data,0);
            }
        }, error: function(result, status) {
            if (status == 'error') {
                alert(result.responseText);
            }
        },
        //            errorPlacement: function(error, element) {  //验证消息放置的地方
        //            if (element.attr("id") == "mobile") {
        //                    element.next("span").next("span").html("");
        //                    error.appendTo(element.next("span"));
        //                }
        //            },   
        complete: function() {
        }
    });
}

//倒计时
function doUpdate(num) {
    if (num == 120) {
        $("#spanGetNumber").attr("disabled", false);
        $("#spanGetNumber").html("重新获取验证码");
        $("#spanSendMsg").html("请输入您手机收到的6位短信验证码");
    } else {
        var wut = (wait / 1000) - num;
        $("#spanSendMsg").html("如果" + wut + "秒后仍未收到短信，请重新获取");
        $("#spanGetNumber").parent().removeClass().addClass("WinBtn L");
        $("#spanGetNumber").html("短信已发送");
        $("#spanGetNumber").attr("disabled", true);
    }

}

//提示信息
function showmsg(msg) {
    $.dialog({
        title: "提示信息",
        content: msg,
        max: false,
        min: false,
        lock: true,
        ok: function() {
            this.hide();
            return false;
        }
    });

}




