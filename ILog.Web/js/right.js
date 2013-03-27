
$(function() {

    //我看过谁
    FunGetUserLook(0);

    $("#hrlookme").click(function() {
        FunGetUserLook(0);
    });
    $("#hrmelook").click(function() {
        FunGetUserLook(1);
    });

    FunSetWorkInfo();

    $("#divNoWork").click(function() {
        FunGetWork();
    });
    
});


function FunSetWorkInfo() {
    $.ajax({
        //请求WebService Url         
        url: "" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserOpenCardInfo",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        //请求参数              
        data: "{}",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            var TopNumConent = "";
            //循环获取值
            $.each(dataObj.UserMenuList, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        $("#GetGuanchang_Menu").html("<li>加载错误</li>");
                        return true;
                    }
                    else {
                        return true;
                    }
                }
                else {
                    $("#spNoWork_Date").html(item.Month + "/" + item.Day);
                    $("#spNoWork_Day").text(item.Day);
                    $("#spNoWork_Week").html(item.Week);
                    $("#spWork_Date").html(item.Month + "/" + item.Day);
                    $("#spWork_Day").text(item.Day);
                    $("#spWork_Week").html(item.Week);
                }
            });
        },
        //出错调试         
        error: function(x, e) {
            // alert(x.responseText); 
        },
        //执行成功后自动执行           
        complete: function(x) {

        }
    });
}

function FunGetWork() {

    $("#divNoWork").hide();
    $("#divOkWork").show();
    ShowPageWorkInfo();

}


function ShowPageWorkInfo() {
    $.dialog({
        id: "divShowGroupTrue",
        title: false,
        content: ShowWorkInfo(),
        max: false,
        min: false,
        lock: true,
        cache: false,
        time: 2,
        width: 290,
        height: 65,
        padding: 0


    });

}

//设置成功
function ShowWorkInfo() {

    ShowTable = '<div class=\"WindowWark280\"><div class=\"WindowBox Tc\"> ';
    ShowTable += '<div class=\" Tc F14  WindowSak\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/ok.gif\" class=\"L\" />恭喜，打卡成功。</div> ';
    ShowTable += '<div class="Hr_10"></div> <div class="Hr_10"></div></div></div>'

    return ShowTable;
}

//显示我去看过谁，谁来看过我
function FunGetUserLook(type) {
    if (type == 0) {
        if (!$("#hrlookme").hasClass("Blue")) {
            $("#hrlookme").addClass("Blue");
        }

        if ($("#hrmelook").hasClass("Blue")) {
            $("#hrmelook").removeClass("Blue");
        }
        GetVisitedList();
    }
    else {
        if (!$("#hrmelook").hasClass("Blue")) {
            $("#hrmelook").addClass("Blue");
        }

        if ($("#hrlookme").hasClass("Blue")) {
            $("#hrlookme").removeClass("Blue");
        }
        GetVisitList();
    }

}


//我看过谁
function GetVisitList() {
    ajaxurl = vServiceUrl + "IlogVisitHistory.asmx/GetVisitList";
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
            var ulHtml = "";
            var dataObj = eval("(" + data.d + ")"); //转换为json对象
            //循环获取值
            $.each(dataObj.HistoryList, function(idx, item) {
                if (idx == 0) {
                    if (dataObj.State == 0) {
                        $(".RlogImg1&.G9").html("暂无我看过的");
                    }
                    else if (dataObj.State == 1) {
                        ulHtml = "<ul id=\"listpeople\">";
                    }
                }
                else if (idx != 0) {
                    if (item.State != -1) {
                        var nickname = item.nickname;
                        if (Getlength(nickname) > 8) {
                            nickname = substr(nickname, 8);
                        }
                        ulHtml += "<li><a href=\"/u_" + item.visiteduserid + "\" ><img src=\"" + item.face + "\" alt=\""
                        + item.nickname + "\" id=\"userv" + item.visiteduserid + "\" onmouseover=\"UserInfoShowOver(this," + item.visiteduserid + ","
                        + item.visiteduserid + ");\" /></a><br /><a href=\"/u_" + item.visiteduserid + "\" class=\"Blue\" id=\"uv" + item.visiteduserid + "\" onmouseover=\"UserInfoShowOver(this,"
                        + item.visiteduserid + "," + item.visiteduserid + ");\"  >" + nickname + "</a><br />" + item.date
                        + "</li>";
                    }
                }
            });
            if (ulHtml != "") {
                ulHtml += "</ul>";
                $(".RlogImg1&.G9").html(ulHtml);
            }
            else {
                $(".RlogImg1&.G9").html("");
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

//谁看过我
function GetVisitedList() {
    ajaxurl = vServiceUrl + "IlogVisitHistory.asmx/GetVisitedList";
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
            var ulHtml = "";
            var dataObj = eval("(" + data.d + ")"); //转换为json对象
            //循环获取值
            $.each(dataObj.HistoryList, function(idx, item) {

                if (idx == 0) {
                    if (dataObj.State == 0) {
                        $(".RlogImg1&.G9").html("暂无我看过的");
                    }
                    else if (dataObj.State == 1) {
                        ulHtml = "<ul id=\"listpeople\">";
                    }
                }
                else if (idx != 0) {
                    var nickname = item.nickname;
                    if (Getlength(nickname) > 8) {
                        nickname = substr(nickname, 8);
                    }
                    ulHtml += "<li><a href=\"/u_" + item.userid + "\"  ><img id=\"userf" + item.userid + "\" onmouseover=\"UserInfoShowOver(this," + item.userid + ","
                        + item.userid + ");\"  src=\"" + item.face + "\" alt=\""
                        + item.nickname + "\" /></a><br /><a href=\"/u_" + item.userid + "\" class=\"Blue\" id=\"uv" + item.userid + "\" onmouseover=\"UserInfoShowOver(this,"
                        + item.userid + "," + item.userid + ")\"  >" + nickname + "</a><br />" + item.date
                        + "</li>";
                }
            });
            if (ulHtml != "") {
                ulHtml += "</ul>";
                $(".RlogImg1&.G9").html(ulHtml);
            }
            else {
                $(".RlogImg1&.G9").html("");
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

//得到签到的显示内容
function GetSignInHtml() {
    DkShow(1, this.id);
    $('#dk').removeClass();
    $('#dk').addClass("daka1");

}



//打卡
function DkShow(dk, dkid) {
    var streer = "机器故障";
    if (dk == 1) {
        streer = "打卡中";
    }
    else {
        streer = "领取中";
    }

    $.ajax({
        url: "http://www.instrument.com.cn/vip/ajax/daka.asp",
        cache: false,
        data: 'daka:' + dk,
        beforeSend: function(XMLHttpRequest) {
            $('#' + dkid).html(streer);
        },
        success: function(html) {
            $('#' + dkid).html(html);
        },

        error: function() {
            $('#' + dkid).html('机器故障');
        }
    });
}

