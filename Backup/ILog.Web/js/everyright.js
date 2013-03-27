$(function() {

    //得到每日名人榜列表
    GetFamousUserList();
    //得到每日草根榜列表
    GetCommonUserList();

});

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

                    var nickname = item.nickname
                    if (Getlength(nickname) > 14) {
                        nickname = substr(nickname, 14);
                    }

                    if (i < 4) {
                        lihtml += "<li style=\"padding: 0px 5px 0px 0px;\"><span class=\"R\">" + item.fansnum + " </span><span class=\"sum Tc\">" + i + "</span>";
                        lihtml += "<a href=\"" + url + "\"><img  src=\"" + FaceImagesUrl
                            + item.face + "\" alt=\"" + item.nickname + "\" width=\"30\" height=\"30\" class=\"L img\"></a>";
                        lihtml += "<a href=\"" + url + "\" class=\"Blue\">" + nickname + "</a> </li>";
                    }
                    else {
                        lihtml += "<li style=\"padding: 0px 5px 0px 0px;\"><span class=\"R\">" + item.fansnum + " </span><span class=\"sum1 Tc\"> " + i + "</span><a href=\""
                        + url + "\" class=\"Blue\">" + nickname + "</a></li>";
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
                    var nickname = item.nickname
                    if (Getlength(nickname) > 14) {
                        nickname = substr(nickname, 14);
                    }
                    var lihtml = "";
                    if (i < 4) {
                        lihtml += "<li style=\"padding: 0px 5px 0px 0px;\"><span class=\"R\">" + item.fansnum + " </span><span class=\"sum Tc\">" + i + "</span>";
                        lihtml += "<a href=\"" + url + "\"><img  src=\"" + FaceImagesUrl
                            + item.face + "\" alt=\"" + item.nickname + "\" width=\"30\" height=\"30\" class=\"L img\"></a>";
                        lihtml += "<a href=\"" + url + "\" class=\"Blue\">" + nickname + "</a> </li>";
                    }
                    else {
                        lihtml += "<li style=\"padding: 0px 5px 0px 0px;\"><span class=\"R\">" + item.fansnum + " </span><span class=\"sum1 Tc\"> " + i + "</span><a href=\""
                        + url + "\" class=\"Blue\">" + nickname + "</a></li>";
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