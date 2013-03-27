
$(document).ready(function()
{
　//加载Ilog站内导航
　 funGetTopMenuService(""+vServiceUrl+"IlogTopMenu.asmx/ILogGetTopMenu","{}","");
　 
　 　//加载Ilog用户导航
　funGetTopUserMenuService(""+vServiceUrl+"ILogUserMenu.asmx/ILogGetUserMenu","{MenuLive:'0'}","");
　
  //加载ilog左侧菜单
　 funGetSettingsleftMenuService(""+vServiceUrl+"ILogWebLeftMenu.asmx/ILogGetSettingsLeftMneu","{MenuLive:'5'}","");

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
           }
       },
       messages: {
       vip_nickname: {
               required: "请输入昵称！"
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

function closeJobDialog(vccname, vcfname, vctname, vccid, vcfid, vctid) {
    $("#vccname").html(vccname);
    $("#vcfname").html(vcfname);
    $("#vctname").html(vctname);
    $("#vccid").val(vccid);
    $("#vctid").val(vctid);
    $("#vcfid").val(vcfid);
    jobDialog.close(); //关闭弹出的窗口

}

function closeCityDialog(countryName, provinceName, cityName, countryid, provinceid, cityid) {
    $("#region").html(provinceName + " " + cityName);
    $("#cityid").val(cityid);
    $("#countryid").val(countryid);
    $("#provinceid").val(provinceid);
    cityDialog.close(); //关闭弹出的窗口

}

function closeSchoolDialog() {
    schoolDialog.close(); //关闭弹出的窗口
    GetSchoolList();

}

function showmsg(msg) {
    $.dialog({
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

function ChangeJob() {
    var vccid = $("#vccid").val();
    var vctid = $("#vctid").val();
    var vcfid = $("#vcfid").val();
    jobDialog = $.dialog({
        id: "divJob",
        title: "修改工作信息",
        content: "url:../Ajax/AjaxJobProperty.aspx?vccid=" + vccid + "&vctid=" + vctid + "&vcfid=" + vcfid,
        max: false,
        min: false,
        lock: true,
        cache: false,
        width: 800,
        height: 500
    });

}

function ChangeSchool(id) {

    schoolDialog = $.dialog({
        id: "divSchool",
        title: "教育信息",
        content: "url:../Ajax/AjaxSchool.aspx?id=" + id,
        max: false,
        min: false,
        lock: true,
        cache: false,
        width: 600,
        height: 300

    });

}

function ChangeRegion() {
    var cityid = $("#cityid").val();
    var countryid = $("#countryid").val();
    var provinceid = $("#provinceid").val();
    cityDialog = $.dialog({
        id: "divRegion",
        title: "修改地区",
        content: "url:../Ajax/AjaxCity.aspx?countryid=" + countryid + "&provinceid=" + provinceid + "&cityid=" + cityid,
        max: false,
        min: false,
        lock: true,
        cache: false,
        width: 800,
        height: 500

    });

}


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

function DeleteSchool(id) {
    if (confirm("确定要删除吗？")) {
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
                        alert("删除成功！");
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

}

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
                    trContent += "<td height=\"37\">" + item.degreename + "</td>";
                    trContent += "<td>" + item.schoolname + "</td>";
                    trContent += "<td>" + item.inyear + "年</td>";
                    trContent += "<td><a href=\"javascript:ChangeSchool(" + item.id + ");\">[修改]</a> <a href=\"javascript:DeleteSchool(" + item.id + ");\">[删除]</a></td>";
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

//是否符合条件
function CheckBaseInfo() {
    var isSuccess = $("#hidPercent").val();
    if (isSuccess == 0) {
        showTipe("您的部分信息未完善，请到“个人资料”中完善。", 0);
        window.setTimeout("funRedirect('index.aspx')", 3000);
        return false;
    }
    return true;

}