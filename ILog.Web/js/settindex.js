

//var vServiceUrl="http://localhost/Ilog.WebService/";

$(document).ready(function() {
    //加载Ilog站内导航
    funGetTopMenuService("" + vServiceUrl + "IlogTopMenu.asmx/ILogGetTopMenu", "{}", "");
    //加载Ilog用户导航
    funGetTopUserMenuService("" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenu", "{MenuLive:'0'}", "");
    //加载ilog左侧菜单
    funGetSettingsleftMenuService("" + vServiceUrl + "ILogWebLeftMenu.asmx/ILogGetSettingsLeftMneu", "{MenuLive:'1'}", "");

    ShowTitle("设置基本信息");

});

$(function() {

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


    $("#form_input").validate({
        rules: {
            vip_nickname: {
                required: true,
                remote: {
                    type: "GET",
                    async: false,
                    url: "../Ajax/AjaxCheckNickName.aspx",
                    data: {
                        nickname: function() {
                            return encodeURI($("#vip_nickname").val());
                        }
                    }
                }
            },
            mobile: {
                required: true,
                isMobile: true,
                remote: "../Ajax/AjaxCheckOldMobile.aspx?type=2"
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
            vip_nickname: {
                required: "请输入昵称！",
                remote: "输入的昵称已存在！"
            },
            mobile: {
                required: "请输入手机号码！",
                remote: "此手机号已被占用！"
            },
            CheckNumber: {
                required: "请输入验证码！",
                rangelength: "请输入6位验证码！"
            }
        },
        success: function(label) {
            label.addClass("valid").html("<img src=\"http://simg.instrument.com.cn/ilog/blue/images/OKK.gif\" alt=\"OK\" />");
        }

    });

});

var jobDialog;
var schoolDialog;
var cityDialog;
var chooseSchoolDialog;


//关闭选择工作性质窗口
function closeJobDialog(hasmsg, vccname, vcfname, vctname, vccid, vcfid, vctid) {
    if (hasmsg) {
        showTipe("保存成功！", 1);
        $("#vccname").html(vccname);
        $("#vcfname").html(vcfname);
        $("#vctname").html(vctname);
        $("#vccid").val(vccid);
        $("#vctid").val(vctid);
        $("#vcfid").val(vcfid);
    }
    jobDialog.close(); //关闭弹出的窗口

}

//关闭选择地区窗口
function closeCityDialog(hasmsg, countryName, provinceName, cityName, countryid, provinceid, cityid) {
    if (hasmsg == 1) {
        $("#region").html(provinceName + " " + cityName);
        $("#cityid").val(cityid);
        $("#countryid").val(countryid);
        $("#provinceid").val(provinceid);
        showTipe('保存成功！', 1);
    }
    cityDialog.close(); //关闭弹出的窗口

}

//关闭选择学校窗口
function closeSchoolDialog(type) {
    if (type == 1) {
        showTipe("保存成功！", 1);
        GetSchoolList();
    }
    else if (type == 2) {
        showTipe('最多添加5个学校！', 0);
    }
    schoolDialog.close(); //关闭弹出的窗口


}

//关闭选择大学窗口
function closeColledgeDialog() {
    chooseSchoolDialog.close(); //关闭弹出的窗口

}

var delschoolDialog;

function delSchool(id) {

    var html = "<div class=\"WindowWark350\">";
    html += "<h1 class=\"WindowTil G4 F14\"><a href=\"javascript:void(0);\" onclick=\"delschoolDialog.close();\">";
    html +="<img class=\"R Img\" src=\"http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif\" alt=\"关闭\"  /></a>提示</h1>";
    html += "<div class=\"WindowBox Tc\">";
    html += "<div class=\" Tc F14 L30  WindowSak\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/ask.gif\" class=\"L\" />确定要删除吗？</div>";
    html += "<div class=\"Hr_10\"></div>";
    html += "<div class=\"WinBtn_H R\"><span>";
    html += "<input name=\"btncancle\" type=\"button\" id=\"btncancle\" value=\"取消\" onclick=\"delschoolDialog.close();return false;\" />";
    html += "</span></div>";
    html += "<div class=\"WinBtn  R\"><span>";
    html += "<input name=\"btndel\" type=\"button\" id=\"btndel\" value=\"确定\" onclick=\"DeleteSchool(" + id + ");\" />";
    html += "</span></div>";
    html += "<div class=\"Hr_10\"></div>";
    html += "</div>";
    html += "</div>";

    delschoolDialog = $.dialog({
        title: false,
        content: html,
        max: false,
        min: false,
        lock: true
    });

}

//显示提示信息
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

//选择工作
function ChangeJob() {
    var vccid = $("#vccid").val();
    var vctid = $("#vctid").val();
    var vcfid = $("#vcfid").val();
    jobDialog = $.dialog({
        id: "divJob",
        title: false,
        content: "url:../Ajax/AjaxJobProperty.aspx?vccid=" + vccid + "&vctid=" + vctid + "&vcfid=" + vcfid,
        max: false,
        min: false,
        lock: true,
        cache: false,
        width: 800,
        height: 400
    });

}

//选择学校
function ChangeSchool(id) {

    schoolDialog = $.dialog({
        id: "divSchool",
        title: false,
        content: "url:../Ajax/AjaxSchool.aspx?id=" + id,
        max: false,
        min: false,
        lock: true,
        cache: false,
        width: 600,
        height: 230

    });

}

//选择大学
function ChangeColledge() {

    chooseSchoolDialog = $.dialog({
        id: "divColledge",
        title: false,
        content: "url:../Ajax/AjaxChooseSchool.aspx",
        max: false,
        min: false,
        lock: true,
        cache: false,
        width: 750,
        height: 400

    });

}

//选择地区
function ChangeRegion() {
    var cityid = $("#cityid").val();
    var countryid = $("#countryid").val();
    var provinceid = $("#provinceid").val();
    cityDialog = $.dialog({
        id: "divRegion",
        title: false,
        content: "url:../Ajax/AjaxCity.aspx?countryid=" + countryid + "&provinceid=" + provinceid + "&cityid=" + cityid,
        max: false,
        min: false,
        lock: true,
        cache: false,
        width: 800,
        height: 400

    });

}

//切换年月
$("#selYear,#selMonth").change(function() {
    var year = $("#selYear").val();
    var month = $("#selMonth").val();
    if (year == "") {
        year = 0;
    }
    if (month == "") {
        month = 0;
    }
    ajaxurl = vServiceUrl + "Vip.asmx/ILogGetDaysListStr"
    $.ajax({
        type: "POST",
        url: "" + ajaxurl + "",
        cache: false,
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        data: "{year:'" + year + "',month:'" + month + "',day: '0' }",
        success: function(data, status) {
            //获取服务器的值        
            var dataObj = eval("(" + data.d + ")"); //转换为json对象
            $("#selDay").html("");
            $("#selDay").append("<option value=''>&nbsp;</option>");
            //循环获取值
            $.each(dataObj.DayList, function(idx, item) {
                if (idx != 0) {
                    $("#selDay").append($("<option></option>").val(item.value).html(item.key));
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

});

//删除学校
function DeleteSchool(id) {
    ajaxurl = vServiceUrl + "IlogSchool.asmx/ILogSchoolDelete"
    $.ajax({
        type: "POST",
        url: "" + ajaxurl + "",
        cache: false,
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        data: "{id:'" + id + "'}",
        success: function(data, status) {
            //获取服务器的值        
            var dataObj = eval("(" + data.d + ")"); //转换为json对象
            //循环获取值
            $.each(dataObj.DeleteSchool, function(idx, item) {
                if (item.State == 1) {
                    delschoolDialog.close();
                    showTipe("删除成功！", 1);
                }
            });
            GetSchoolList();
        }, error: function(result, status) {
            if (status == 'error') {
                alert(result.responseText);
            }
        },
        complete: function() {
        }
    });


}

//得到学校列表
function GetSchoolList() {
    ajaxurl = vServiceUrl + "IlogSchool.asmx/ILogSchoolGetList"
    $.ajax({
        type: "POST",
        url: "" + ajaxurl + "",
        cache: false,
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        data: "{}",
        success: function(data, status) {
            //获取服务器的值
            var dataObj = eval("(" + data.d + ")"); //转换为json对象
            $("#tblSchool").html("");
            var trFirst = "";

            trFirst += "<tr>";
            trFirst += "<td height=\"36\" class=\"nav G6\"><strong>学校类型</strong></td>";
            trFirst += "<td class=\"nav  G6\"><strong>学校名称</strong></td>";
            trFirst += "<td class=\"nav  G6\"><strong>入学年份</strong></td>";
            trFirst += "<td class=\"nav  G6\">&nbsp;</td>";
            trFirst += "</tr>";

            $("#tblSchool").append(trFirst);
            //循环获取值
            $.each(dataObj.SchoolList, function(idx, item) {
                if (idx != 0) {
                    var trContent = "";
                    trContent += "<tr>";
                    trContent += "<td height=\"37\" class=\"F14\">" + item.degreename + "</td>";
                    trContent += "<td class=\"F14\">" + item.schoolname + "</td>";
                    trContent += "<td class=\"F14\">" + item.inyear + "年</td>";
                    trContent += "<td class=\"F14\"><a href=\"javascript:ChangeSchool(" + item.id + ");\">[修改]</a> <a href=\"javascript:delSchool(" + item.id + ");\">[删除]</a></td>";
                    trContent += "</tr>";
                    $("#tblSchool").append(trContent);
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

//email的窗口对象
var emailDialog = "";

//弹出修改邮箱窗口
function editemail() {

    emailDialog = $.dialog({
        id: "divEmail",
        title: false,
        content: "url:../Ajax/AjaxEditEmail.aspx",
        padding: 0,
        max: false,
        min: false,
        lock: true,
        cache: false,
        width: 700,
        height: 170

    });

}

//关闭修改email窗口
function closeEmailDialog(msg) {
    if (msg != "") {
        showTipeWithWidth(msg, 1,450);
    }
    emailDialog.close();

}

//修改手机的窗口对象
var mobileDialog = "";

//弹出修改mobile窗口
function editMobile() {
    mobileDialog = $.dialog({
        id: "divMobile",
        title: false,
        content: "url:../Ajax/AjaxEditMobile.aspx",
        max: false,
        min: false,
        lock: true,
        cache: false,
        width: 600,
        height: 310
    });

}

//关闭修改mobile窗口
function closeMobileDialog(mobile, msg) {
    if (msg != "") {
        showTipe(msg, 1);
        $("#spanMobile").html(mobile.substring(0, 3) + "****" + mobile.substring(7, 11));
    }
    mobileDialog.close();

}

//得到验证码
function GetCheckNumber() {
    //判断mobile格式
    var mobile = $.trim($("#mobile").val());
    if (mobile == "") {
        showTipe("请输入手机号码！",0);
        return false;
    }
    else {
        var mobileRule = /^1[358]\d{9}$/;
        if (!mobileRule.test(mobile)) {
            showTipe("请输入正确的手机号码！",0);
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
                showmsg(data);
            }
        }, error: function(result, status) {
            if (status == 'error') {
                alert(result.responseText);
            }
        },
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

//选择大学
function ShowColledge(schoolid, schoolname) {
    $(window.frames["divSchool"].document).find("#SchoolName").val(schoolname);
    $(window.frames["divSchool"].document).find("#schoolid").val(schoolid);
    window.parent.closeColledgeDialog();

}

