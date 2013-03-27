
$(document).ready(function() {
    //加载Ilog站内导航
    funGetTopMenuService("" + vServiceUrl + "IlogTopMenu.asmx/ILogGetTopMenu", "{}", "");
    //加载Ilog用户导航
    funGetTopUserMenuService("" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenu", "{MenuLive:'0'}", "");
    //加载ilog左侧菜单
    funGetleftMenuService("" + vServiceUrl + "ILogWebLeftMenu.asmx/ILogGetFollowLeftMneu", "{MenuLive:'4'}", "");

    ShowTitle("邀请好友");

    GetInviteUserList();

});


//得到邀请好友列表
function GetInviteUserList() {
    ajaxurl = vServiceUrl + "Vip.asmx/GetInviteList";

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
            $("#ulInvite").html("");
            var i = 0;
            //循环获取值
            $.each(dataObj.InviteList, function(idx, item) {
                if (idx != 0) {
                    var lihtml = "";
                    lihtml = "<li style=\"height:30px\">";
                    lihtml += "<input name=\"btnCopy" + idx + "\" id=\"btnCopy" + idx 
                    + "\" onclick=\"copyurl('"+item.code+"');return false;\" type=\"button\" style=\"cursor: pointer;border:none;padding:5px 10px;float:left\" value=\"复制\"/>";
                    lihtml += "<input style=\"margin-left:5px;margin-right:5px;\" type=\"text\" class=\"input L\" size=\"75\" name=\"txtCopy" + idx + "\" id=\"txtCopy" + idx
                  + "\" value=\"http://ig.instrument.com.cn/index_" + item.code + "\" /></li>";
                    $("#ulInvite").append(lihtml);

                    i++;
                }
            });
            if (i == 0) {
                $("#divInviteInfo").html("你的邀请链接已经用完了！");
            }
            else {
                $("#divInviteInfo").html("你还有" + i + "个邀请链接，马上邀请更多好友参与ilog新版公测！");
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

function copyurl(code) {
    copy("http://ig.instrument.com.cn/index_" + code);
    showTipe("邀请链接复制成功！ 你可以利用快捷方式Ctrl+V键粘贴到UC、QQ或MSN等聊天工具中。", 1);

}



