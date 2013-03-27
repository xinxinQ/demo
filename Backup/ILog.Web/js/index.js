

$(document).ready(function() {


    //加载Ilog站内导航
    funGetTopMenuService("" + vServiceUrl + "IlogTopMenu.asmx/ILogGetTopMenu", "{}", "");
    //加载Ilog用户导航
    funGetTopUserMenuService("" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenu", "{MenuLive:'0'}", "");
    //得到每日名人榜列表
    GetFamousUserList();
    //得到每日草根榜列表
    GetCommonUserList();
    //得到开通ilog的前9个用户列表
    GetNewUserList();
    //得到最新经过名人认证的前9个用户列表
    GetNewFamousUserList();
    //得到大家正在说的内容
    GetNewIlogList();

    //幻灯片滚动
    focusflow();

    LoginDiv(16);

});

var Interval;
var theInt = null;
var $crosslink, $navthumb;
var curclicked = 0;
var _recommendCount;

//大家正在说
function flowDoing() {
    Interval = setInterval('action()', 3000);
    $("#divContent").hover(
        function() { clearInterval(Interval); },
        function() { Interval = setInterval('action()', 3000); }
    );
}

//幻灯片滚动
function focusflow() {


    $("#main-photo-slider").codaSlider();

    $navthumb = $(".nav-thumb");
    $crosslink = $(".cross-link");

    $navthumb
			.click(function() {
			    var $this = $(this);
			    theInterval($this.parent().attr('href').slice(1) - 1);
			    return false;
			});

    theInterval();

}

theInterval = function(cur) {
    clearInterval(theInt);

    if (typeof cur != 'undefined')
        curclicked = cur;

    $crosslink.removeClass("active-thumb");
    $navthumb.eq(curclicked).parent().addClass("active-thumb");
    $(".stripNav ul li a").eq(curclicked).trigger('click');

    theInt = setInterval(function() {
        $crosslink.removeClass("active-thumb");
        $navthumb.eq(curclicked).parent().addClass("active-thumb");
        $(".stripNav ul li a").eq(curclicked).trigger('click');
        curclicked++;
        if (5 == curclicked)
            curclicked = 0;

    }, 2000);
};



//得到每日名人榜列表
function GetFamousUserList() {
    ajaxurl = vServiceUrl + "VipILog.asmx/VipIlogGetFamousList";
    var userid = $.cookie("useid");
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
            $("#ulFamous").html("");
            var i = 1;
            //循环获取值
            $.each(dataObj.UserList, function(idx, item) {

                var url = "";
                //判断自己或是他人
                if (userid == item.userid) {
                    url = "/u";
                } else {
                    url = "/u_" + item.userid;
                }

                if (idx != 0) {
                    var lihtml = "";
                    if (i < 4) {
                        lihtml += "<li><span class=\"R\">" + item.fansnum + " </span><span class=\"sum Tc\">" + i + "</span>";
                        lihtml += "<a href=\"" + url + "\"><img  src=\"" + FaceImagesUrl
                            + item.face + "\" alt=\"" + item.nickname + "\" width=\"30\" height=\"30\" class=\"L img\"></a>";
                        lihtml += "<a href=\"" + url + "\" class=\"Blue\">" + item.nickname + "</a> </li>";
                    }
                    else {
                        lihtml += "<li><span class=\"R\">" + item.fansnum + " </span><span class=\"sum1 Tc\"> " + i + "</span><a href=\""
                        + url + "\" class=\"Blue\">" + item.nickname + "</a></li>";
                    }
                    i++;
                    $("#ulFamous").append(lihtml);
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

//得到每日草根榜列表
function GetCommonUserList() {
    ajaxurl = vServiceUrl + "VipILog.asmx/VipIlogGetCommonList";
    var userid = $.cookie("useid");

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
            $("#ulCommon").html("");
            var i = 1;
            //循环获取值
            $.each(dataObj.UserList, function(idx, item) {

                var url = "";
                //判断自己或是他人
                if (userid == item.userid) {
                    url = "/u";
                } else {
                    url = "/u_" + item.userid;
                }

                if (idx != 0) {
                    var lihtml = "";
                    if (i < 4) {
                        lihtml += "<li><span class=\"R\">" + item.fansnum + " </span><span class=\"sum Tc\">" + i + "</span>";
                        lihtml += "<a href=\"" + url + "\"><img  src=\"" + FaceImagesUrl
                            + item.face + "\" alt=\"" + item.nickname + "\" width=\"30\" height=\"30\" class=\"L img\"></a>";
                        lihtml += "<a href=\"" + url + "\" class=\"Blue\">" + item.nickname + "</a> </li>";
                    }
                    else {
                        lihtml += "<li><span class=\"R\">" + item.fansnum + " </span><span class=\"sum1 Tc\"> " + i + "</span><a href=\""
                        + url + "\" class=\"Blue\">" + item.nickname + "</a></li>";
                    }
                    i++;
                    $("#ulCommon").append(lihtml);
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

//得到开通ilog的前9个用户列表
function GetNewUserList() {
    ajaxurl = vServiceUrl + "VipILog.asmx/VipIlogGetNewList";
    var userid = $.cookie("useid");
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
            $("#ulUserList").html("");
            var i = 1;
            //循环获取值
            $.each(dataObj.UserList, function(idx, item) {

                var url = "";
                //判断自己或是他人
                if (userid == item.userid) {
                    url = "/u";
                } else {
                    url = "/u_" + item.userid;
                }

                if (idx != 0) {
                    var nickname = item.nickname;
                    if (nickname.length > 4) {
                        nickname = nickname.substring(0, 4);
                    }
                    var lihtml = "";
                    lihtml += "<li><a href=\"" + url + "\"><img src=\"" + FaceImagesUrl + item.face + "\" alt=\"" + item.nickname + "\" /></a><br />";
                    lihtml += "<a href=\"" + url + "\" class=\"Blue\">" + nickname + "</a></li>";
                    $("#ulUserList").append(lihtml);
                    i++;
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


//得到最新经过名人认证的前9个用户列表
function GetNewFamousUserList() {
    ajaxurl = vServiceUrl + "VipILog.asmx/VipIlogGetNewFamousList";
    var userid = $.cookie("useid");

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
            $("#ulNewFamousList").html("");
            var i = 1;
            //循环获取值
            $.each(dataObj.UserList, function(idx, item) {

                var url = "";
                //判断自己或是他人
                if (userid == item.userid) {
                    url = "u";
                } else {
                    url = "u_" + item.userid;
                }
                if (idx != 0) {
                    var nickname = item.nickname;
                    if (nickname.length > 4) {
                        nickname = nickname.substring(0, 4);
                    }
                    var lihtml = "";
                    lihtml += "<li><a href=\"" + url + "\"><img src=\"" + FaceImagesUrl + item.face + "\" alt=\"" + item.nickname + "\" /></a><br />";
                    lihtml += "<a href=\"" + url + "\" class=\"Blue\">" + nickname + "</a></li>";
                    $("#ulNewFamousList").append(lihtml);
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


//得到大家正在说的内容
function GetNewIlogList() {
    var userid = $.cookie("useid");
    ajaxurl = vServiceUrl + "ILog_Spread.asmx/GetNewSpreadListJsonStr"
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
            $("#divContent").html("");
            var i = 1;
            //循环获取值
            $.each(dataObj.ilogList, function(idx, item) {
                if (idx != 0) {
                    var url = "";
                    //判断自己或是他人
                    if (userid == item.userid) {
                        url = "u";
                    } else {
                        url = "u_" + item.userid;
                    }

                    var lihtml = "";
                    lihtml += "<div class=\"Centent ENG\" style=\"margin:0 auto;height:auto;z-index:100; overflow:hidden;\">";
                    lihtml += "<a href=\"" + url + "\"><img src=\"" + FaceImagesUrl + item.face + "\" alt=\"" + item.nickname + "\" class=\"L Img\" /></a>";
                    lihtml += "<div class=\"Txt R\">";
                    lihtml += "<p class=\"G6 L22\">";
                    lihtml += "<a href=\"" + url + "\" class=\"Blue\">" + item.nickname + "</a>";
                    lihtml += ShowVerifyImg(item.memberlevel);
                    lihtml += "：" + unescape(item.is_content) + "</p>";
                    lihtml += "<Div class=\"Hr_5\"></Div>";
                    lihtml += "<div class=\"G9 Info\">" + item.intime + "</div>";
                    lihtml += "</div>";
                    lihtml += "<Div class=\"Hr_1\"></Div>";
                    lihtml += "<div class=\"Line_ilog\"></div>";
                    lihtml += "</div>";
                    $("#divContent").append(lihtml);
                    i++;
                }
            });
            _recommendCount = parseInt(i);
            //滚动 大家正在说
            flowDoing();

        }, error: function(result, status) {
            if (status == 'error') {
                alert(result.responseText);
            }
        },
        complete: function() {
        }
    });

}


function action() {
    var strhtml;
    strhtml = $('#divContent .Centent').last().html();
    if (strhtml == null) {
        return false;
    }

    $('#divContent .Centent').last().remove();
    $('#divContent').prepend('<div class="Centent"  style="margin:0 auto;height:auto;z-index:100; overflow:hidden; display:none;">' + strhtml + '</div>');
    $('#divContent .Centent').first().slideDown(500);

}

