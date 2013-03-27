
$(function() {
    GetRight();
});

//得到右侧的信息
function GetRight() {
    var userId = $.cookie('useid');
    //个人主页更多链接
    $("#hrefConcern,#hrefConcernMore").attr("href", "follow");
    $("#hrefFans,#hrefFansMore").attr("href", "fans");
    GetConcernList(userId);
    GetFansList(userId);
    GetUserCount(userId);
    GetVipInfo(userId);

}



//得到用户粉丝数与关注数
function GetUserCount(userid) {
    ajaxurl = vServiceUrl + "VipILog.asmx/GetIlogCountInfo";
    $.ajax({
        type: "POST",
        url: "" + ajaxurl + "",
        cache: false,
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型
        contentType: "application/json;",
        data: "{userid:'" + userid + "'}",
        success: function(data, status) {
            var ulHtml = "";
            var dataObj = eval("(" + data.d + ")"); //转换为json对象
            $("#spanConcernNum").html("(" + dataObj.Concern + ")");
            $("#spanFansNum").html("(" + dataObj.Fan + ")");
        }, error: function(result, status) {
            if (status == 'error') {
                alert(result.responseText);
            }
        },
        complete: function() {
        }
    });

}

//TA的粉丝
function GetFansList(userid) {
    ajaxurl = vServiceUrl + "ILogFollow.asmx/GetFansListWithTa";
    $.ajax({
        type: "POST",
        url: "" + ajaxurl + "",
        cache: false,
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型
        contentType: "application/json;",
        data: "{userid:'" + userid + "'}",
        success: function(data, status) {
            var ulHtml = "";
            var dataObj = eval("(" + data.d + ")"); //转换为json对象
            //循环获取值
            $.each(dataObj.FansList, function(idx, item) {
                if (idx == 0) {
                    if (dataObj.State == 0) {
                        $("#divFans").html("暂时没有找到相关数据");
                    }
                    else if (dataObj.State == 1) {
                        ulHtml = "<ul>";
                    }
                }
                else if (idx != 0) {
                    if (dataObj.State != 0) {
                        var nickname = item.nickname;
                        if (Getlength(nickname)> 8) {
                            nickname = substr(nickname,8);
                        }
                        ulHtml += "<li><a href=\"/u_" + item.userid + "\"><img src=\"" + item.face + "\" alt=\""
                        + item.nickname + "\" id=\"userrfan_" + item.userid + "\" onmouseover=\"UserInfoShowOver(this," + item.userid + ","
                        + item.userid + ");\"  /></a><br /><a href=\"/u_" + item.userid + "\" class=\"Blue\" id=\"rfan" + item.userid 
                        + "\" onmouseover=\"UserInfoShowOver(this," + item.userid+ ","+ item.userid + ");\">" + nickname + "</a><br />" + item.date + "</li>";
                    }
                }
            });
            if (ulHtml != "") {
                ulHtml += "</ul>";
                $("#divFans").html(ulHtml);
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

//TA的关注
function GetConcernList(userid) {
    ajaxurl = vServiceUrl + "ILogFollow.asmx/GetConcernListWithTa";
    $.ajax({
        type: "POST",
        url: "" + ajaxurl + "",
        cache: false,
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        data: "{userid:'" + userid + "'}",
        success: function(data, status) {
            var ulHtml = "";
            var dataObj = eval("(" + data.d + ")"); //转换为json对象
            //循环获取值
            $.each(dataObj.ConcernList, function(idx, item) {
                if (idx == 0) {
                    if (dataObj.State == 0) {
                        $("#divConcern").html("暂时没有找到相关数据");
                    }
                    else if (dataObj.State == 1) {
                        ulHtml = "<ul>";
                    }
                }
                else if (idx != 0) {
                    if (dataObj.State != 0) {
                        var nickname = item.nickname;
                        if (Getlength(nickname) > 8) {
                            nickname = substr(nickname, 8);
                        }
                        ulHtml += "<li><a href=\"/u_" + item.userid + "\"><img src=\"" + item.face + "\" alt=\""
                        + item.nickname + "\"  id=\"userrconcern_" + item.userid + "\" onmouseover=\"UserInfoShowOver(this," + item.userid + ","
                        + item.userid + ");\"  /></a><br /><a href=\"/u_" + item.userid + "\" class=\"Blue\" id=\"rconcern_" + item.userid + "\" onmouseover=\"UserInfoShowOver(this," + item.userid + ","
                        + item.userid + ");\">" + nickname + "</a><br />" + item.date + "</li>";
                    }
                }
            });
            if (ulHtml != "") {
                ulHtml += "</ul>";
                $("#divConcern").html(ulHtml);
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

//得到用户的基本信息
function GetVipInfo(userid) {
    ajaxurl = vServiceUrl + "VipILog.asmx/GetVIPIlogInfo";
    $.ajax({
        type: "POST",
        url: "" + ajaxurl + "",
        cache: false,
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        data: "{userid:'" + userid + "'}",
        success: function(data, status) {
            var ulHtml = "";
            var dataObj = eval("(" + data.d + ")"); //转换为json对象
            if (dataObj.memberlevel == 1) {
                $("#divVerify").html("<a href=\"javascript:void(0);\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-5.gif\" alt=\"个人认证\"/></a>");
            }
            else if (dataObj.vi_memberlevel == 2) {
                $("#divVerify").html("<a href=\"javascript:void(0);\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-5.gif\" alt=\"名人认证\"/></a>");
            }
            if (dataObj.memberlevel == 1 || dataObj.vi_memberlevel == 2) {
                var comment = dataObj.comment;
                if (comment.length > 31) {
                    comment = comment.substring(0, 31) + "...";
                }
                $("#divContent").html(comment);
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