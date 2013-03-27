
$(document).ready(function() {
    //加载Ilog站内导航
    funGetTopMenuService("" + vServiceUrl + "IlogTopMenu.asmx/ILogGetTopMenu", "{}", "");
    //加载Ilog用户导航
    funGetTopUserMenuService("" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenu", "{MenuLive:'0'}", "");
    //得到每日草根榜列表
    GetFameList(1, 20);

});


//得到博文列表
function GetFameList(PageCurrent, pagesize) {
    var keyword = $.trim($("#txtKeyword").val());

    $.ajax({
        //请求WebService Url
        url: vServiceUrl + "VipILog.asmx/VipIlogGetFameList",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        cache: false,
        //请求参数
        data: "{PageCurrent:'" + PageCurrent + "',PageSize:'" + pagesize + "',keyword:'" + keyword + "'}",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var userid = $.cookie("useid");

            var strList = "";
            var count = 0;
            var recordcount = 0;
            //循环获取值
            $.each(dataObj.List, function(idx, item) {
                if (idx == 0) {
                    if (item.State == "0") {
                        $("#divContent").html("加载错误");
                    }
                    else if (item.State == "2")  //无数据不显示分页控件
                    {
                        var sowhpage_div = $("#sowhpage_div");
                        sowhpage_div.html(""); //初始化分页控件

                        return false;   //无数据不再往下执行
                    }
                    else    //如果成功就返回循环里面做拼接，该“return”不会跳出循环
                    {
                        //上一页与下一页面的页码计算，重新设置起点
                        if (PageCurrent > 1) {
                            //上一页
                            pageindex = PageCurrent;
                            //下一页
                            pageindex_n = PageCurrent;
                        }
                        else //初始化
                        {
                            //上一页
                            pageindex = 1;
                            //下一页
                            pageindex_n = 2;
                        }
                        return true;
                    }
                }
                if (item.RecordCount > 1)   //拿到总页数
                {
                    sowhpage_ul_one(PageCurrent, item.RecordCount, "", pagesize, "GetFameList");   //分页
                    return true;
                }
                else if (item.RecordCount == 1) //显示条数页数
                {
                    $("#sowhpage_div").html("");    //去掉分页

                    return true;
                }

                //如果遍历到不存在节点就不构建
                if (item.vi_id != undefined) {
                    recordcount++;

                    //评论类型判断
                    var pinlun = item.is_isoriginal == 0 ? 2 : 1;
                    //个人主页地址
                    var url = "";

                    //博文地址
                    var contentUrl = "";

                    //关注列表地址
                    var followurl = "";

                    //粉丝列表地址
                    var fansurl = "";

                    //判断自己或是他人
                    if (userid == item.userid) {

                        url = "/u";

                        contentUrl = "/cont_" + item.iso_id;
                        followurl = "/follow";
                        fansurl = "/fans";

                    } else {

                        url = "/u_" + item.userid;

                        contentUrl = "/tcont_" + item.userid + "_" + item.iso_id;
                        followurl = "/follow_" + item.userid;
                        fansurl = "/fans_" + item.userid;
                    }

                    strList += "<div class=\"Concern_Txt HofCen L\">";

                    strList += "<div class=\"pic L\">";
                    strList += "<a href=\"" + url + "\">";
                    strList += "<img class=\"img\" alt=\"" + item.nickname + "\"  src=\"/images/face/small/" + item.face + "\">";
                    strList += "</a>";
                    strList += "<br>";
                    strList += "<div class=\"Tc\">";
                    strList += "<input type=\"checkbox\" name=\"chkFame\" id=\"chk" + item.userid + "\"  value=\"" + item.userid + "\"/>";
                    strList += "</div>";
                    strList += "</div>";


                    strList += "<div class=\"RigBox R\">";

                    strList += funGetConcern_Fame(item.userid, item.nickname, item.concernstate, "cencernimg_" + item.userid);

                    strList += "<div class=\" L19  G4\">";
                    var nickname = item.nickname;
                    if (Getlength(nickname) > 10) {
                        nickname = substr(nickname, 10);
                    }
                    strList += "<a href=\"" + url + "\" class=\"Blue F14\" id=\"nickname" + item.vi_id + "\">" + nickname + "</a> ";
                    strList += ShowVerifyImg(item.memberlevel);
                    strList += "<br />";

                    strList += GetSex(item.sex);
                    strList += item.address;

                    strList += "<br />";
                    strList += "关注 <b><a href=\"" + followurl + "\" class=\"Blue\">" + item.vic_concernnum + "</a></b> <span class=\"G9\">| </span>粉丝 <b><a href=\"" + fansurl + "\"";
                    strList += " class=\"Blue\">" + item.vic_fannum + "</a></b> <span class=\"G9\">| </span>微博 <b><a href=\"" + url + "\" class=\"Blue\">" + item.vic_ilognum + "</a></b></div>";


                    strList += "<p class=\"txt\">";
                    if (item.iso_id != "0") {
                        var content = item.is_content;
                        if (Getlength(content) > 100) {
                            content = substr(content, 100);
                        }

                        strList += "<a href=\"" + contentUrl + "\" target=\"_blank\">" + unescape(content) + "(" + item.intime + ")</a>";
                    }
                    else {
                        strList += "暂无博文";
                    }
                    strList += "</p></div>";
                    strList += "<div class=\"Hr_1\">";
                    strList += "</div>";
                    strList += "</div>";

                    if (count == 1) {
                        strList += "<div class=\"Line_ilog\"></div>";
                        count = 0;
                    }
                    else {
                        if (recordcount == dataObj.List.length - 2) {

                            strList += "<div class=\"Line_ilog\"></div>";
                        }
                        count = 1;
                    }
                }
            });

            $("#divContent").html(strList);

            scroll(0, 0);  //翻页后需要回到顶部
        },
        //出错调试         
        error: function(x, e) {

            alert("加载异常");

            //window.location.href = "http://c.instrument.com.cn/art/ilog/404.asp";
        },
        //执行成功后自动执行           
        complete: function(x) {

        }
    });
}

//得到关注状态
function GetConcertState(otherId) {
    var state = 0;
    $.ajax({
        url: vServiceUrl + "ILogFollow.asmx/ILogFollowConcernState",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{concernUserid:'" + otherId + "'}",
        async: false,
        cache: false,
        success: function(json) {
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            $.each(dataObj.ConcernState, function(idx, item) {
                if (idx == 0) {
                    state = item.UrlState;
                }
            });
        }
    });
    return state;

}




//判断我与用户之间的关系
function funGetConcern_Fame(otherId, nickname) {
    var openedHtml = "";
    var state = GetConcertState(otherId);
    openedHtml += "<span  id=\"cencernimg_" + otherId + "\">";
    if (state == "0") {
        openedHtml += "<div class=\"R Rbox G9\">";
        openedHtml = "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-4.gif\" class=\"L Rimg\" />";
        openedHtml += "<a href=\"javascript:void(0)\" class=\" Gray6\" >出错</a>";
        openedHtml += "</div>";
    }
    else if (state == "1") {

        //加关注
        openedHtml += "<div class=\"btn R L23 G4 Tr\">";
        openedHtml += "<a href=\"javascript:void(0);\" class=\"White\">";
        openedHtml += "<span>";
        openedHtml += "<a href=\"javascript:ShowAddFollowTrueFameList(" + otherId + "," + otherId + ",'" + nickname + "')\" id=\"concern_a_" + otherId + "\" >";
        openedHtml += "<img alt=\"加关注\" src=\"http://simg.instrument.com.cn/ilog/blue/images/concern_j.gif\"  />";
        openedHtml += "</a>";
        openedHtml += "</span>";
        openedHtml += "</a>";
        openedHtml += "</div>";
    }
    else if (state == "2") {
        //已关注
        openedHtml += "<div class=\"R Rbox G9\">";
        openedHtml += "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-4.gif\" class=\"L Rimg\" alt=\"已关注\" />";
        openedHtml += "<a href=\"javascript:void(0);\" class=\"Gray6\">已关注</a>";
        openedHtml += "&nbsp;|&nbsp;";
        openedHtml += "<a href=\"javascript:ShowPageConcern_Fame(" + otherId + "," + otherId + ",'" + nickname + "');\" class=\"Blue\">取消</a>";
        openedHtml += "</div>";
    }
    else if (state == "3") {
        //互相关注
        openedHtml += "<div class=\"R Rbox G9\" style=\"width:115px;\">";
        openedHtml += "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-3.gif\" class=\"L Rimg\" alt=\"互相关注\" />";
        openedHtml += "<a href=\"javascript:void(0);\" class=\" Gray6\">互相关注</a>";
        openedHtml += "&nbsp;|&nbsp;";
        openedHtml += "<a href=\"javascript:ShowPageConcern_Fame(" + otherId + "," + otherId + ",'" + nickname + "');\" class=\"Blue\">取消</a>";
        openedHtml += "</div>";
    }
    openedHtml += "</span>";

    return openedHtml;

}

//粉丝加关注
//iufid：
//concernuserid：被关注用户id
//nickname：昵称
//vi_id：vipILog表流水号，为以关注换图片用
function ShowAddFollowTrueFameList(iufid, concernuserid, nickname) {
    $.ajax({
        url: "" + vServiceUrl + "ILogFollow.asmx/ILogFollowAddFan",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{iufID:'" + iufid + "',concernUserid:'" + concernuserid + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            $.each(dataObj.FanCancel, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        alert("加载错误");
                        return false;
                    }
                    else {
                        //关注操作框
                        showConcerntDialog = $.dialog({
                            id: "divrDialog",
                            title: false,
                            content: ShowAddFollow_Fame(iufid, concernuserid, nickname, item.UrlState),
                            max: false,
                            min: false,
                            lock: true,
                            cache: false,
                            padding: 0

                        });
                        return true;
                    }
                }
            });
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




//关注
function ShowPageConcern_Fame(iuc_id, concernuserid, nickname) {

    showConcerntDialog = $.dialog({
        id: "divShowRePort",
        title: false,
        content: ShowConcernCancle(iuc_id, concernuserid, nickname),
        max: false,
        min: false,
        lock: true,
        cache: false,
        // width: 295,
        //  height: 99,
        padding: 0
    });

    //取消
    $("#btnConcernCancle").click(function() {
        CloseConcern();
    });
    //确定取消
    $("#btnConcernTrue").click(function() {
        ShowConcernSubmitInfo_Fame(iuc_id, concernuserid, nickname);
    });

}



//确认取消关注
function ShowConcernSubmitInfo_Fame(iuc_id, concernuserid, nickname) {
    $.ajax({
        url: "" + vServiceUrl + "ILogFollow.asmx/ILogFollowConcernCancle",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{iucID:'" + iuc_id + "',concernUserid:'" + concernuserid + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            $.each(dataObj.ConcernCancel, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        alert("加载错误");
                        return false;
                    }
                    else {

                        showTipe("取消关注成功。", 1);

                        CloseConcern();

                        var concernhtml = funGetConcern_Fame(concernuserid, nickname);
                        //成功关注后要切换成已关注图片
                        $("#cencernimg_" + concernuserid).html(concernhtml);
                        return true;
                    }
                }
            });
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



//在加关注时加载出当前用户的分组
//iufid：关注id
//concernuserid：被关注id
//nickname：昵称
function ShowAddFollow_Fame(iufid, concernuserid, nickname) {
    ShowDiv = "<div class=\"WindowWark350\"><div class=\"WindowTil G4 F14\">";

    ShowDiv += "<a href=\"javascript:void(0);\" onclick=\"CloseConcern()\"><img class=\" R Img\" src=\"http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif\" alt=\"关闭\"  /></a>关注成功</div>";

    ShowDiv += "<div class=\"WindowBox\">";

    ShowDiv += "<p class=\"G4 L25\">为 <strong>" + nickname + "</strong> 选择分组</p>";

    ShowDiv += "<div class=\"BrBlue P10\">";

    ShowDiv += "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"G4\" id=\"d" + iufid + "\">";

    ShowDiv += "</table><div class=\"Hr_10\">";

    ShowDiv += "</div>";

    ShowDiv += " <DIV>&nbsp;<img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-j.gif\"/> <a href=\"javascript:void(0);\" onclick=\"ShowGroupConcernAdd(" + iufid + "," + concernuserid + ",'" + nickname + "')\" >创建分组</a></DIV>";

    ShowDiv += "</div><div class=\"Hr_10\"></div><div class=\"WinBtn_H R\"><span>";

    ShowDiv += "<input name=\"btnAddFollowCancle\" type=\"button\" id=\"btnAddFollowCancle\" onclick=\"CloseConcern()\" value=\"取消\" />";

    ShowDiv += "</span></div>";

    ShowDiv += "<div class=\"WinBtn  R\"><span>";

    ShowDiv += "<input name=\"btnAddFollowTrue\" type=\"button\" id=\"btnAddFollowTrue\" onclick=\"GrouptAdd_Fame(" + concernuserid + ",'" + nickname + "');\" value=\"确定\" />";

    ShowDiv += " </span></div><div class=\"Hr_10\"></div></div></div>"

    //加载用户
    ShowGroupSel(iufid, iufid);
    return ShowDiv;

}


//提交用户组
//ConcernUserID：被关注id
function GrouptAdd_Fame(ConcernUserID, nickname) {
    //有选中项
    if (!Ischecked()) {
        $.dialog.tips("<font color=\"red\">请选择组！</font>");
    }
    else {
        var strGroup = "";

        $("input:checkbox[name='Groupcbox']:checked").each(function() {
            strGroup += $(this).val() + ",";
        })

        //掉用服务器端
        $.ajax({
            url: "" + vServiceUrl + "ILogFollow.asmx/ILogFollowEditGroupConnect_s",
            type: "POST",
            dataType: "json",
            contentType: "application/json;",
            data: "{groupID:'" + strGroup + "',concernUserid:'" + ConcernUserID + "'}",
            success: function(json) {

                var dataObj = eval("(" + json.d + ")"); //转换为json对象

                var TopContent = "";
                $.each(dataObj.GroupEdit, function(idx, item) {
                    if (idx == 0) {
                        if (item.UrlState == "0") {
                            $("#" + HrName + "_" + HrID + "").html("<li>加载错误</li>");
                            return false;
                        }
                        else {
                            //显示添加成功
                            ShowGroupTaFollowTrue();
                            CloseConcern();
                            var concernhtml = funGetConcern_Fame(ConcernUserID, nickname);
                            //成功关注后要切换成已关注图片
                            $("#cencernimg_" + ConcernUserID).html(concernhtml);

                            return true;
                        }
                    }
                });
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
}

//全选
function CheckAllFame() {

    $("input:checkbox[name='chkFame']").each(function() {
        $(this).attr("checked", true);
    });

}

//反选
function RevAllFame() {

    $("input:checkbox[name='chkFame']").each(function() {
        var chkState = $(this).attr("checked");
        $(this).attr("checked", !chkState);
    });

}

//关注选中的用户，如果已经加过关注，则跳过
function ConcernThem() {
    var checkednum = 0;
    var concernednum = 0;
    $("input:checkbox[name='chkFame']:checked").each(function() {
        var chkuserid = $(this).val();
        var state = GetConcertState(chkuserid);
        if (state == 1) {
            ShowAddFollowTrue_Fame(chkuserid, chkuserid);
            concernednum++;
        }
        checkednum++;
    });
    if (concernednum > 0) {
        var hidCurrentPage = $("#hidCurrentPage").val();
        var currentPage = 1;
        if (hidCurrentPage != "" || hidCurrentPage != undefined) {
            currentPage = hidCurrentPage;
        }
        GetFameList(currentPage, 20);
    }
    if (checkednum == 0) {
        showTipe("请选择要关注的对象！", 0);
    } else {
        showTipe("关注成功", 1);
    }

}


//他人主页加关注
//iufid：
//concernuserid：被关注用户id
//nikename：昵称
//vi_id：vipILog表流水号，为以关注换图片用
function ShowAddFollowTrue_Fame(iufid, concernuserid) {
    $.ajax({
        url: "" + vServiceUrl + "ILogFollow.asmx/ILogFollowAddFan",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{iufID:'" + iufid + "',concernUserid:'" + concernuserid + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            $.each(dataObj.FanCancel, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {

                        return false;
                    }
                    else if (item.UrlState == "3") {

                        return false;
                    }
                    else {

                        return true;
                    }
                }
            });
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