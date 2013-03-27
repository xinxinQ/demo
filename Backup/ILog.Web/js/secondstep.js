
$(document).ready(function() {
    //加载Ilog站内导航
    funGetTopMenuService("" + vServiceUrl + "IlogTopMenu.asmx/ILogGetTopMenu", "{}", "");
    //加载Ilog用户导航
    funGetTopUserMenuService("" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenu", "{MenuLive:'0'}", "");
    //加载ilog左侧菜单
    funGetSettingsleftMenuService("" + vServiceUrl + "ILogWebLeftMenu.asmx/ILogGetSettingsLeftMneu", "{MenuLive:'5'}", "");





    $.validator.addMethod("isIdCardNo", function(value, element) {
        return this.optional(element) || checkidcard(value);
    }, "请输入正确的身份证号码！");



    $("#form_input").validate({
        rules: {
            IDNumber: {
                required: true,
                isIdCardNo: true
            },
            Comment: {
                required: true,
                maxlength: 60
            },
            fupCard: {
                required: true,
                accept: "jpg|gif|png"
            },
            fupPosition: {
                required: true,
                accept: "jpg|gif|png"
            },
            fupOther: {
                accept: "jpg|gif|png"
            }
        },
        messages: {
            IDNumber: {
                required: "请输入身份证号码！"
            },
            Comment: {
                required: "请输入认证说明！",
                maxlength: "认证说明最多输入60个字"
            },
            fupCard: {
                required: "请上传身份证明！",
                accept: "身份证明只支持jpg、png、gif格式！"
            },
            fupPosition: {
                required: "请上传职位证明！",
                accept: "职位证明只支持jpg、png、gif格式！"
            },
            fupOther: {
                accept: "其他证明只支持jpg、png、gif格式！"
            }
        },
        success: function(label) {
            label.addClass("valid").html("<img src=\"http://simg.instrument.com.cn/ilog/blue/images/OKK.gif\" alt=\"OK\" />");
        }

    });

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

function checkidcard(num) {
    var len = num.length, re;
    if (len == 15)
        re = new RegExp(/^(\d{6})()?(\d{2})(\d{2})(\d{2})(\d{3})$/);
    else if (len == 18)
        re = new RegExp(/^(\d{6})()?(\d{4})(\d{2})(\d{2})(\d{3})(\d)$/);
    else {
        //alert("请输入15或18位身份证号,您输入的是 "+len+ "位"); 
        return false;
    }
    var a = num.match(re);
    if (a != null) {
        if (len == 15) {
            var D = new Date("19" + a[3] + "/" + a[4] + "/" + a[5]);
            var B = D.getYear() == a[3] && (D.getMonth() + 1) == a[4] && D.getDate() == a[5];
        } else {
            var D = new Date(a[3] + "/" + a[4] + "/" + a[5]);
            var B = D.getFullYear() == a[3] && (D.getMonth() + 1) == a[4] && D.getDate() == a[5];
        }
        if (!B) {
            //alert("输入的身份证号 "+ a[0] +" 里出生日期不对！"); 
            return false;
        }
    }

    return true;

}

var changeTypeDialog;
//更改认证类型
function changeType() {
    var type = $("#type").val();
    var html = "<link href=\"http://simg.instrument.com.cn/ilog/blue/css/base.css\" rel=\"stylesheet\" type=\"text/css\" />";
    html += "<link href=\"http://simg.instrument.com.cn/ilog/blue/css/module.css\" rel=\"stylesheet\" type=\"text/css\" />";
    html += "<div class=\"WindowWark350\">";
    html += "<h1 class=\"WindowTil G4 F14\"><a href=\"javascript:void(0);\" onclick=\"changeTypeDialog.close();\">";
    html += "<img class=\"R Img\" src=\"http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif\" alt=\"关闭\"  /></a>更改类型</h1>";
    html += "<div class=\"Hr_20\">";
    html += "</div>";
    html += "<ul class=\"ListBD G4\">";
    html += "<li style=\"text-align:center;\">";
    if (type == 1) {
        html += "<label for=\"radType\"><input id=\"radSingle\" name=\"radType\" type=\"radio\" value=\"1\" checked=\"checked\"/>个人认证</label>&nbsp;";
    }
    else {
        html += "<label for=\"radType\"><input id=\"radSingle\" name=\"radType\" type=\"radio\" value=\"1\"/>个人认证</label>&nbsp;";
    }
    if (type == 2) {
        html += "<label for=\"radType\"> <input id=\"radFamous\" name=\"radType\" type=\"radio\" value=\"2\" checked=\"checked\"/>名人认证</label>";
    }
    else {
        html += "<label for=\"radType\"> <input id=\"radFamous\" name=\"radType\" type=\"radio\" value=\"2\" />名人认证</label>";
    }

    html += "</li>";
    html += " </ul>";
    html += " <br />";
    html += " <div class=\"Tc\">";
    html += "<div class=\"WinBtn\">";
    html += "<span>";
    html += "<input name=\"btnSubmit\" type=\"button\" id=\"btnSubmit\" value=\"保存修改\" onclick=\"closeType();\" /></span></div>";
    html += "</div>";
    html += " <br />";
    changeTypeDialog = $.dialog({
        title: false,
        content: html,
        max: false,
        min: false,
        lock: true
    });

}

function closeType() {
    showTipe("保存成功！", 1);
    var type = $('input:radio[name="radType"]:checked').val();
    if (type == 1) {
        $("#spanTypeName").html("个人认证");
    }
    else {
        $("#spanTypeName").html("名人认证");
    }
    $("#type").val(type);
    changeTypeDialog.close();

}

//跳回首页
function gotohome() {
    window.location.href = "/home.aspx";

}