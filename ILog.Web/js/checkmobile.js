$(function() {
    jQuery.validator.addMethod("isMobile", function(value, element) {
        var length = value.length;
        return this.optional(element) || (length == 11 && /^1[358]\d{9}$/.test(value));
    }, "请填写正确的手机号码！");

    $("#btnSubmitMobile").click(function() {
        window.parent.confirmOpenIlog($.trim($("#CheckNumber").val()), $.trim($("#mobile").val()));
        return false;
    });

    $("#form_mobile").validate({
        rules: {
            mobile: {
                required: true,
                isMobile: true
            },
            CheckNumber: {
                required: true,
                rangelength: [6, 6]
            }
        },
        messages: {
            mobile: {
                required: "请输入手机号！"
            },
            CheckNumber: {
                required: "请输入验证码！",
                rangelength: "请输入6位验证码！"
            }
        }
    });

    $("#spanGetNumber").click(function() {
        var mobile = $.trim($("#mobile").val());
        if (mobile == "") {
            window.parent.showTipe("请输入手机号码！", 0);
            return false;
        }
        else {
            var mobileRule = /^1[358]\d{9}$/;
            if (!mobileRule.test(mobile)) {
                window.parent.showTipe("请输入正确的手机号码！", 0);
                return false;
            }
        }
        var ajaxurl = "AjaxSendMobile.aspx?mobile=" + mobile;
        $.ajax({
            url: ajaxurl,
            type: "get",
            cache: false,
            success: function(data, status) {
                if (data == 0) {
                    secs = 120;
                    wait = secs * 1000;
                    for (i = wait / 1000; i >= 0; i--) {
                        window.setTimeout("doUpdate(" + i + ")", i * 1000);
                    }
                }
                else {
                    alert(data);
                }
            }, error: function(result, status) {
                if (status == 'error') {
                    alert(result.responseText);
                }
            },
            complete: function() {
            }
        });
    });
});

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