
$(document).ready(function() {
    //加载Ilog站内导航
    funGetTopMenuService("" + vServiceUrl + "IlogTopMenu.asmx/ILogGetTopMenu", "{}", "");
    //加载Ilog用户导航
    funGetTopUserMenuService("" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenu", "{MenuLive:'0'}", "");
    //加载ilog左侧菜单
    funGetSettingsleftMenuService("" + vServiceUrl + "ILogWebLeftMenu.asmx/ILogGetSettingsLeftMneu", "{MenuLive:'5'}", "");

    ShowTitle("iLog认证");


});

function showmsg(msg) {
    $.dialog({
        title: "信息提示",
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

// var vServiceUrl = "http://localhost:8121/";

//验证认证
function CheckAuthentication(type) {
    ajaxurl = vServiceUrl + "ILogAuthenticationHistory.asmx/CheckAuthentication";
    $.ajax({
        type: "POST",
        url: "" + ajaxurl + "",
        cache: false,
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        data: "{type:'" + type + "'}",
        success: function(data, status) {
            //获取服务器的值        
            var dataObj = eval("(" + data.d + ")"); //转换为json对象
            //循环获取值
            $.each(dataObj.CheckState, function(idx, item) {
                if (item.State == 2) {
                    showTipe("您已经提交过认证申请，请耐心等待审核！", 0);
                }
                else if (item.State == 3) {
                    showTipe("您已经通过该认证，不可以重复申请！", 0);
                }
                else if (item.State == 4) {
                    showTipe("您已经通过名人认证，不可以再申请个人认证！", 0);
                }
                else if (item.State == 5) {
                    showTipe("您未满足认证的基本条件！", 0);
                }
                else if (item.State == 1) {
                window.location.href = "/verify/first_" + type;
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

}