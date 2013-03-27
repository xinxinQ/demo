

//var vServiceUrl="http://localhost/Ilog.WebService/";

$(document).ready(function() {
    //加载Ilog站内导航
    funGetTopMenuService("" + vServiceUrl + "IlogTopMenu.asmx/ILogGetTopMenu", "{}", "");
    //加载Ilog用户导航
    funGetTopUserMenuService("" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenu", "{MenuLive:'0'}", "");
    //加载ilog左侧菜单
    funGetSettingsleftMenuService("" + vServiceUrl + "ILogWebLeftMenu.asmx/ILogGetSettingsLeftMneu", "{MenuLive:'3'}", "");

    ShowTitle("修改邮箱");

    $("#form_input").validate({
        rules: {
            email: {
                required: true,
                email: true,
                remote: {
                    type: "GET",
                    async: false,
                    url: "../Ajax/AjaxCheckEmail.aspx",
                    data: {
                        email: function() {
                            return $("#email").val();
                        }
                    }
                }
            }
        },
        messages: {
            email: {
                required: "请输入Email！",
                email: "请输入正确的Email！",
                remote: "输入的Email已被注册！"
            }

        },
        success: function(label) {
            label.addClass("valid").html("<img src=\"http://simg.instrument.com.cn/ilog/blue/images/OKK.gif\" alt=\"OK\" />");
        }

    });


});

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

