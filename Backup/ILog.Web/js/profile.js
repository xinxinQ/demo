



$(document).ready(function() {
    //获取当然用户id
    var userid = $.cookie('useid');

    //加载Ilog站内导航
    funGetTopMenuService("" + vServiceUrl + "IlogTopMenu.asmx/ILogGetTopMenu", "{}", "");

    //加载Ilog用户导航
    funGetTopUserMenuService("" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenu", "{MenuLive:'0'}", "");

    //加载ilog左侧菜单
    funGetleftMenuService("" + vServiceUrl + "ILogWebLeftMenu.asmx/ILogGetPersonalLeftMneu", "{MenuLive:'2'}", "");

    VipILogPersonalInfo("" + vServiceUrl + "VipIlogUser.asmx/ILogGetPersonalUserInfoById", userid);

    //显示关注、微博、粉丝、勋章
    showCount("" + vServiceUrl + "/VipIlogUser.asmx/ILogGetUserInfoById", userid);

    ShowTitle("个人资料");
});

//得到右侧的信息
function GetRight() {
    var userId = $.cookie('useid');
    $("#hrefConcern,#hrefConcernMore").attr("href", "/fans_" + userId);
    $("#hrefFans,#hrefFansMore").attr("href", "/follow_" + userId);
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
            $("#spanConcernNum").html("（" + dataObj.Concern + "）");
            $("#spanFansNum").html("（" + dataObj.Fan + "）");
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
                    var nickname = item.nickname;
                    if (nickname.length > 4) {
                        nickname = nickname.substring(0, 3) + "...";
                    }
                    ulHtml += "<li><a href=\"/u_" + item.userid + "\"><img src=\"" + item.face + "\" alt=\""
                        + item.nickname + "\" /></a><br /><a href=\"u_" + item.userid + "\" class=\"Blue\">" + nickname + "</a><br />" + item.date + "</li>";
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
                    var nickname = item.nickname;
                    if (nickname.length > 3) {
                        nickname = nickname.substring(0, 3) + "...";
                    }
                    ulHtml += "<li><a href=\"/u_" + item.userid + "\"><img src=\"" + item.face + "\" alt=\""
                        + item.nickname + "\" /></a><br /><a href=\"/u_" + item.userid + "\" class=\"Blue\">" + nickname + "</a><br />" + item.date + "</li>";
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
            else if (dataObj.memberlevel == 2) {
                $("#divVerify").html("<a href=\"javascript:void(0);\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-5.gif\" alt=\"名人认证\"/></a>");
            }
            var comment = dataObj.comment;
            if (comment.length > 31) {
                comment = comment.substring(0, 31) + "...";
            }
            if (comment != "") {
                comment += "<div class=\"Tr\"><a href=\"/verify/\" class=\"Blue\">申请认证>></a></div><br>"
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


//显示用户基本信息
function VipILogPersonalInfo(servicesUrl, userid) {

    $.ajax({

        //请求WebService Url         
        url: "" + servicesUrl + "",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        //缓存
        cache: false,
        //请求参数              
        data: "{userid:'" + userid + "',i:'" + rand + "'}",
        //成功           
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象 

            var sexPic = dataObj.sex == 1 ? "men.gif" : "women.jpg";

            var headInfo = "<a href=\"/settings/Face.aspx\"><img src=\"../images/face/big/" + dataObj.face + "\" title=\"" + dataObj.nickname + "\" alt=\"" + dataObj.nickname + "\" width=\"140\" height=\"140\" class=\"img\" id=\"face\" /></a>";

            //<img src=\"http://simg.instrument.com.cn/ilog/blue/images/" + sexPic + "\" />

            $("#headInfo").html(headInfo);


            var personalInfo = "";

            personalInfo += "<a href=\"/u\" class=\"F14\"><strong>" + dataObj.nickname + "</strong></a>";
            personalInfo += ShowVerifyImg(dataObj.memberlevel);

            personalInfo += "<br><span class=\"Fa\"><a href=\"http://ig.instrument.com.cn/u\" class=\"Blue\">http://ig.instrument.com.cn/u</a></span><br>";

            if (dataObj.address != "未填写") {
                personalInfo += "地区：<span class=\"blue\">" + dataObj.address + " </span>";
            }
            if (dataObj.school != "未填写") {
                personalInfo += "学校：<span class=\"blue\">" + dataObj.school + "</span>";
            }

            personalInfo += "<br>";

            $("#personalInfo").html(personalInfo);

        },
        //出错调试         
        error: function(x, e) {

            alert(x.responseText);

        },
        //执行成功后自动执行           
        complete: function(x) {

        }
    });

}


//显示关注、微博、粉丝、勋章
function showCount(url, userId) {

    //userId=2383703;

    $.ajax({
        url: "" + url + "",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{userid:" + userId + ",i:" + rand + "}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象  

            if (dataObj.UrlState == 1) {

                var showCount = "<div class=\"box\">";
                showCount += "<strong class=\" Fw\"><a href=\"/Follow\">" + dataObj.concern + "</a></strong><br>";
                showCount += "<a href=\"/Follow\">关注</a></div>";
                showCount += "<div class=\"box\">";
                showCount += "<strong class=\" Fw\"><a href=\"/fans\">" + dataObj.fan + "</a></strong><br>";
                showCount += "<a href=\"/fans\">粉丝</a></div>";
                showCount += "<div class=\"box  box_no\">";
                showCount += "<strong class=\" Fw\"><a href=\"/u\">" + dataObj.blog + "</a></strong><br>";
                showCount += "<a href=\"/u\">微博</a></div>";

                var insignia = dataObj.Insignia;
                var info = "";
                if (insignia != " ") {


                    info += "<div class=\"Hr_10\"></div><div class=\"Rlog_Line\"></div><div class=\"Hr_10\"></div>";
                    info += insignia;

                }

                $("#showCount").html(showCount);

                $("#showInsignia").html(info);

            }
        },
        //出错调试         
        error: function(x, e) {
            //alert(x.responseText);     
        },
        //执行成功后自动执行           
        complete: function(x) {

        }

    });
}     





