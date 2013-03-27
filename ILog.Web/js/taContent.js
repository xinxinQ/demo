
//获取原创及转发、评论
var ioid = 0;
var actionType = 0;

$(document).ready(function() {
    //获取当然用户id
    var userid = $("#userid").val();

    //获取传递的值
    ioid = $("#ioidHidden").val();

    actionType = $("#actionHidden").val();



    //加载Ilog站内导航
    funGetTopMenuService("" + vServiceUrl + "IlogTopMenu.asmx/ILogGetTopMenu", "{}", "");

    //加载Ilog用户导航

    funGetTopUserMenuService("" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenu", "{MenuLive:'0'}", "");

    //加载ilog左侧菜单
    funGetleftMenuService("" + vServiceUrl + "ILogWebLeftMenu.asmx/ILogGetHPersonalLeftMneu", "{MenuLive:'1',hUserID:'" + userid + "'}", "");

    //获取头像及用户统计基本信息
    VipILogPersonalInfo("" + vServiceUrl + "VipIlogUser.asmx/ILogGetPersonalUserInfoById", userid, 3);

    //显示关注、微博、粉丝、勋章
    showCount("" + vServiceUrl + "VipIlogUser.asmx/ILogGetUserInfoById", userid);

    //用户关系
    funGetConcern(userid);

    //获取原创信息     
    GetContentInfoById("" + vServiceUrl + "VipIlogUser.asmx/IlogGetContentInfoById", ioid, 0);

    $("#divNoWork").click(function() {
        FunGetWork();
    });

    RecordVisitHistory();

});


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

            var headInfo = "<a href=\"/settings/Face.aspx\"><img src=\"../images/face/big/" + dataObj.face + "\" title=\"" + dataObj.nickname + "\" alt=\"" + dataObj.nickname + "\" width=\"140\" height=\"140\" class=\"img\" id=\"face\" /></a>";

            $("#headInfo").html(headInfo);

            //设置title
            ShowTitle(dataObj.nickname + "的iLog");


            //显示昵称与开通状态

            var nickNameHtml = "<a href=\"/u\"><strong>" + dataObj.nickname + "</strong></a>";


            $("#conNickName").html(nickNameHtml);

            var certificationHtml = ShowVerifyImg(dataObj.memberlevel);

            $("#conCertification").html(certificationHtml);
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
                showCount += "<strong class=\" Fw\"><a href=\"/follow_" + userId + "\">" + dataObj.concern + "</a></strong><br>";
                showCount += "<a href=\"/follow_" + userId + "\">关注</a></div>";
                showCount += "<div class=\"box\">";
                showCount += "<strong class=\" Fw\"><a href=\"/fans_" + userId + "\">" + dataObj.fan + "</a></strong><br>";
                showCount += "<a href=\"/fans_" + userId + "\">粉丝</a></div>";
                showCount += "<div class=\"box  box_no\">";
                showCount += "<strong class=\" Fw\"><a href=\"/u_" + userId + "\">" + dataObj.blog + "</a></strong><br>";
                showCount += "<a href=\"/u_" + userId + "\">微博</a></div>";

                var insignia = dataObj.Insignia;
                var info = "";
                if (insignia != "") {


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



var detial = "";
var connSreadId = 0;
var newType = 0;

//获取原创. by lx on 20120716(type:0-评论 1-转发)
function GetContentInfoById(servicesUrl, id, type) {
    $.ajax({
        url: "" + servicesUrl + "",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{id:" + id + "}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象  

            var result = "";

            if (dataObj.list.length > 0) {

                //获取当然用户id
                var userid = $.cookie('useid');

                $.each(dataObj.list, function(idx, item) {

                    result += "<div class=\"Hr_10\"></div>";
                    result += "<p class=\"F14 G3\">";
                    result += unescape(item.content);
                    result += "<input id=\"contentHidden\" type=\"hidden\" value=\"" + item.facestr + "\" />";
                    result += "</p><div id=\"div" + item.is_id + "\"></div>";
                    result += "<div class=\"Hr_10\"></div>";

                    result += "<div class=\"Hr_10\"></div>";


                    if ((item.is_spreadtype == 1 && item.is_isoriginal == 0) || (item.is_spreadtype == 0 && item.is_isoriginal == 0 && item.is_type == 1)) {

                        result += "<div id=\"divContentInfo\"></div>";
                        GetContentOriginalInfoById(item.io_id, item.is_id);

                    }


                    result += "<div class=\"Hr_10\"></div>";
                    result += "<div class=\"Hr_10\"></div>";

                    result += "<div class=\"G9 Fa Info\">";
                    result += "<span class=\"L\">";
                    //result += "<a href=\"#\" class=\"Blue\">";
                    result += item.timestr;
                    // result += "</a>&nbsp;";
                    result += "&nbsp;";
                    result += "来自<a href=\"" + item.is_url + "\" onclick=\"return LoginDiv(16);\">";
                    result += item.is_name;
                    result += "</a>";

                    if (userid == item.userid) {

                        result += "&nbsp;&nbsp;";
                        result += "<a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"ShowPageReport(" + item.is_id + ",'" + item.nickname + "','" + item.face + "','" + item.io_content + "')\">";
                        result += "举报";
                        result += "</a>";

                    }

                    result += "</span>";
                    result += "<span class=\"R\">";

                    //赋值用于直接跳转转发赋值
                    detial = item.facestr;

                    //存储原创ID
                    connSreadId = item.is_id;

                    //存储原创的
                    connUserid = item.userid;

                    //转发
                    //item.is_spreadtype == 1 && item.is_isoriginal == 0 自己的转发
                    //item.is_spreadtype == 0 && item.is_isoriginal == 0 && item.is_type == 1 别人的转发
                    if ((item.is_spreadtype == 1 && item.is_isoriginal == 0) || (item.is_spreadtype == 0 && item.is_isoriginal == 0 && item.is_type == 1)) {

                        //转发
                        $("#contentTypeHidden").val("1");

                        var sourseType = 1;

                        //删除时候刷新列表
                        if (type == 1) {

                            actionType = 0;
                            sourseType = 0;

                        }

                        // actionType=type;

                        checkTag(actionType, sourseType, item.is_spreadnum, item.vic_commentnum);

                        result += "<a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"checkTag(0,1," + item.is_spreadnum + "," + item.vic_commentnum + ");\">";
                        result += "转发(" + item.is_spreadnum + ")";
                        result += "</a>";
                        result += " | ";
                        result += "<a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"checkTag(1,1," + item.is_spreadnum + "," + item.vic_commentnum + ");\">";
                        result += "评论(" + item.vic_commentnum + ")";
                        result += "</a>";


                    } else {

                        //原创
                        $("#contentTypeHidden").val("0");

                        checkTag(actionType, 0, item.is_spreadnum, item.vic_commentnum);

                        result += "<a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"checkTag(0,0," + item.is_spreadnum + "," + item.vic_commentnum + ");\">";
                        result += "转发(" + item.is_spreadnum + ")";
                        result += "</a>";
                        result += " | ";
                        result += "<a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"checkTag(1,0," + item.is_spreadnum + "," + item.vic_commentnum + ");\">";
                        result += "评论(" + item.vic_commentnum + ")";
                        result += "</a>";

                    }

                    result += "</span>";
                    result += "</div>";

                    if (item.is_haspic == 1) //有图片
                    {
                        GetPic(vServiceUrl + "ILogPic.asmx/GetPic", item.io_id, item.is_id);
                    }

                });

                //原创信息赋值
                $("#contentOriginal").html(result);

                //文本框赋值
            } else {

                window.location = "index.html";

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

//判断我与用户之间的关系
function funGetConcern(userid) {


    $.ajax({
        url: "" + vServiceUrl + "ILogFollow.asmx/ILogFollowConcernState",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{concernUserid:'" + userid + "'}",
        cache:false,
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            var ToLiContent = "";
            var nickname = $("#hidNickname").val();

            $.each(dataObj.ConcernState, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        TopContent = "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-4.gif\" class=\"L Rimg\" />"
                        TopContent += "<a href=\"javascript:void(0)\" class=\" Gray6\" >出错</a>";
                        return true;
                    }
                    else if (item.UrlState == "1") {
                        TopContent += "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-4.gif\" class=\"L Rimg\" /><span class=\"F16\"> |+</span>"
                        TopContent += "<a href=\"javascript:void(0)\" class=\" Gray6\" onclick=\"ShowAddFollowTaContent(0," + userid + ",'" + nickname + "')\">加关注</a>";
                        return true;
                    }
                    else if (item.UrlState == "2") {
                        TopContent += "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-4.gif\" class=\"L Rimg\" />"
                        TopContent += "已关注|<a href=\"javascript:void(0)\" class=\" Gray6\" onclick=\"CancleTaContentConcern(0," + userid + ",'" + nickname + "')\">取消</a>";
                        return true;
                    }
                    else if (item.UrlState == "3") {
                        TopContent += "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-3.gif\" class=\"L Rimg\" />"
                        TopContent += "互相关注|<a href=\"javascript:void(0)\" class=\" Gray6\" onclick=\"CancleTaContentConcern(0," + userid + ",'" + nickname + "')\">取消</a>";
                        return true;
                    }
                }
            });


            $("#HpersionConcern").html(TopContent);
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
function CancleTaContentConcern(iuc_id, concernuserid, nikename) {

    showConcerntDialog = $.dialog({
        id: "divShowRePort",
        title: false,
        content: ShowConcernCancle(iuc_id, concernuserid, nikename),
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
    CancleConcernSubmitInfo(iuc_id, concernuserid);
    });
}


//确认取消关注提
function CancleConcernSubmitInfo(iuc_id, concernuserid) {
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
                        //判断与用户的关系
                        funGetConcern(concernuserid);
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



//粉丝加关注
//iufid：
//concernuserid：被关注用户id
//nikename：昵称
//vi_id：vipILog表流水号，为以关注换图片用
function ShowAddFollowTaContent(iufid, concernuserid, nikename) {
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
                            content: ShowAddFollowTaContentGroup(iufid, concernuserid, nikename),
                            max: false,
                            min: false,
                            lock: true,
                            cache: false,
                            //width: 370,
                            //height: 210,
                            padding: 0

                        });

                        //判断我与用户之间的关系
                        funGetConcern(concernuserid);
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
//nikename：昵称
function ShowAddFollowTaContentGroup(iufid, concernuserid, nikename) {
    ShowDiv = "<div class=\"WindowWark350\"><div class=\"WindowTil G4 F14\">";

    ShowDiv += "<a href=\"javascript:void(0);\" onclick=\"CloseConcern()\"><img class=\" R Img\" src=\"http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif\" alt=\"关闭\"  /></a>关注成功</div>";

    ShowDiv += "<div class=\"WindowBox\">";

    ShowDiv += "<p class=\"G4 L25\">为 <strong>" + nikename + "</strong> 选择分组</p>";

    ShowDiv += "<div class=\"BrBlue P10\">";

    ShowDiv += "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"G4\" id=\"d" + iufid + "\">";

    ShowDiv += "</table><div class=\"Hr_10\">";

    ShowDiv += "</div>";

    ShowDiv += " <DIV>&nbsp;<img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-j.gif\"/> <a href=\"javascript:void(0);\" onclick=\"ShowGroupConcernAdd("
    + iufid + "," + concernuserid + ",'" + nikename + "')\" >创建分组</a></DIV>";

    ShowDiv += "</div><div class=\"Hr_10\"></div><div class=\"WinBtn_H R\"><span>";

    ShowDiv += "<input name=\"btnAddFollowCancle\" type=\"button\" id=\"btnAddFollowCancle\" onclick=\"CloseConcern()\" value=\"取消\" />";

    ShowDiv += "</span></div>";

    ShowDiv += "<div class=\"WinBtn  R\"><span>";

    ShowDiv += "<input name=\"btnAddFollowTrue\" type=\"button\" id=\"btnAddFollowTrue\" onclick=\"GrouptTaAdd(" + concernuserid + ")\" value=\"确定\" />";

    ShowDiv += " </span></div><div class=\"Hr_10\"></div></div></div>"

    //加载用户
    ShowGroupSel(iufid, iufid);
    return ShowDiv;
}




//原创信息.by lx on 20120627
function GetContentOriginalInfoById(ioId, isId) {

    var userid = $.cookie('useid');

    $.ajax({
        url: "" + vServiceUrl + "ILogOriginal.asmx/GetOriginalInfoByID",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{id:'" + ioId + "',i:" + rand + "}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var original = "";
            var haspic = 0;
            var piclist = "";
            var last = "";
            $.each(dataObj.Original, function(idx, item) {
                if (item.State != 0) {
                    if (idx == 0) {
                        if (item.State == 1) {

                            var url = "";
                            var contentUrl = "";



                            //判断自己或是他人
                            if (userid == item.userid) {

                                url = "/u";

                                contentUrl = "cont_" + item.spreadid;

                            } else {

                                url = "/u_" + item.userid;
                                contentUrl = "tcont_" + item.userid + "_" + item.spreadid;

                            }



                            //original += "<div class=\"Hr_10\"></div>";
                            original += "<div class=\"Round1\">";
                            original += "<div class=\"Round2\">";
                            original += "<div class=\"Round3\" style=\"width:520px\"> <p class=\"F12 G6 L22 Centent\"><a href=\"" + url + "\" id=\"uu" + isId + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.is_id + ")\" class=\"Blue Fa\">@" + item.nickname + "</a>";
                            original += ShowVerifyImg(item.memberlevel) + "：";
                            original += unescape(item.content) + "</p>";
                            haspic = item.haspic;
                            last = "<div class=\"L30\">";
                            last += "<a href=\"" + contentUrl + "\" class=\"Blue\">" + item.intime + "</a>&nbsp;&nbsp;";
                            last += "<span class=\"G9\">来自<a class=\"Fa\" href=\"" + item.sourceurl + "\" onclick=\"return LoginDiv(16);\">" + item.source + "</a></span> &nbsp; ";
                            last += "<a href=\"" + contentUrl + "_0\" class=\"Blue\">转发<span class=\"Fa\">(" + item.spreadnum + ")</span></a>";
                            last += " <a class=\"Blue\" href=\"" + contentUrl + "_1\">评论<span class=\"Fa\">(" + item.commentnum + ")</span></a></div>";
                            last += "</div>";
                            last += "</div>";
                            last += "<span class=\"Jiao\">◆</span>";
                            last += "</div><Div class=\"Hr_10\">";
                        }
                    }
                    else {
                        if (haspic) {
                            if (idx == 1) {
                                piclist += "<div class=\"Hr_10\"></div>";
                                piclist += "<ul class=\"imgList\" id=\"image_area_2_" + ioId + "\">";
                                piclist += "<li><a href=\"/images/Sourse/" + item.picname.substring(0, 8) + "/" + item.picname + "\" class=\"artZoomAll\" rel=\"/images/Middle/" + item.picname.substring(0, 8) + "/" + item.picname + "\" rev=\"2_" + ioId + "\">";
                                piclist += " <img src=\"/images/Little/" + item.picname.substring(0, 8) + "/" + item.picname + "\" /></a>";
                                piclist += "</li>";
                            }
                            else {
                                piclist += "<li><a href=\"/images/Sourse/" + item.picname.substring(0, 8) + "/" + item.picname + "\" class=\"artZoomAll\" rel=\"/images/Middle/" + item.picname.substring(0, 8) + "/" + item.picname + "\" rev=\"2_" + ioId + "\">";
                                piclist += " <img src=\"/images/Little/" + item.picname.substring(0, 8) + "/" + item.picname + "\" /></a>";
                                piclist += "</li>";
                            }
                        }
                    }
                }
                else {
                    original += "<div class=\"Hr_10\"></div>";
                    original += "<div class=\"Round1\">";
                    original += "<div class=\"Round2\">";
                    original += "<div class=\"Round3\" style=\"width:450px\"> <p class=\"F12 G6 L22 Centent\">";
                    original += "抱歉，此微博已被原作者删除。如需帮助，请联系客服。</p>";
                    last += "</div>";
                    last += "<span class=\"Jiao\">◆</span>";
                    last += "</div><Div class=\"Hr_10\">";
                }
            });
            if (piclist != "") {

                piclist += "</ul>";
                piclist += "<div class=\"Hr_10\"></div>";
                //piclist += "<Div class=\"Hr_10\"></Div>";

            }
            var originalHtml = original + piclist + last;
            if (originalHtml != "") {
                $("#divContentInfo").html(originalHtml);
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
//切换标签 转发-0  评论-1, type:是原创(0)还是转发(1)
function checkTag(index, type, spreadnum, commentnum) {

    var contentHidden = $("#contentHidden").val() == undefined ? detial : $("#contentHidden").val();

    contentHidden = unescape(contentHidden);

    var contentTypeHidden = $("#contentTypeHidden").val();

    var commentDetial = "";
    var forwardDetial = "";

    //转发
    if (index == 0) {

        forwardDetial += "<div class=\"top\"></div>";
        forwardDetial += "<div class=\"center\">";
        forwardDetial += "<a href=\"javascript:void(0);\" class=\"Blue\" id=\"conForwardId\" onclick=\"checkTag(0," + contentTypeHidden + "," + spreadnum + "," + commentnum + ")\">";
        forwardDetial += "<strong>";
        forwardDetial += "转发(" + spreadnum + ")";
        forwardDetial += "</strong>";
        forwardDetial += "</a></div>";

        commentDetial += "<a href=\"javascript:void(0);\" class=\"Blue\" id=\"conCommentId\" onclick=\"checkTag(1," + contentTypeHidden + "," + spreadnum + "," + commentnum + ")\">";
        commentDetial += "<strong>";
        commentDetial += " 评论(" + commentnum + ")";
        commentDetial += "</strong>";
        commentDetial += "</a>";

        //选中转发信息的转发链接
        if (type == "1") {
            myfocusSatrt("conTextarea");
        } else {
            $("#conTextarea").focus();
        }

        //转发按钮
        $("#commSubmit").html("<input name=\"forwardId\" type=\"button\" id=\"forwardId\" value=\"转发\" onclick=\"ConSendForwardInfo(" + connSreadId + "," + type + "," + connUserid + ")\"/>");

        //加载列表  
        GetAllForwardList("" + vServiceUrl + "ILog_Spread.asmx/GetContentForwordPageList", connSreadId, type, pageindex, 10, "f");

        newType = type;

    } else if (index == 1) {

        //评论按钮
        $("#commSubmit").html("<input name=\"commentId\" type=\"button\" id=\"commentId\" value=\"评论\" onclick=\"conSendComment(" + connSreadId + ",0,0);\"/>");

        //评论
        commentDetial += "<div class=\"top\"></div>";
        commentDetial += "<div class=\"center\">";
        commentDetial += "<a href=\"javascript:void(0);\" class=\"Blue\" id=\"conCommentId\" onclick=\"checkTag(1," + contentTypeHidden + "," + spreadnum + "," + commentnum + ");\">";
        commentDetial += "<strong>";
        commentDetial += "评论(" + commentnum + ")";
        commentDetial += "</strong>";
        commentDetial += "</a></div>";

        forwardDetial += "<a href=\"javascript:void(0);\" class=\"Blue\" id=\"conForwardId\" onclick=\"checkTag(0," + contentTypeHidden + "," + spreadnum + "," + commentnum + ");\">";
        forwardDetial += "<strong>";
        forwardDetial += " 转发(" + spreadnum + ")";
        forwardDetial += "</strong>";
        forwardDetial += "</a>";

        $("#conTextarea").val("");
        $("#conTextarea").focus();

        //刷新评论列表
        GetAllCommentList("" + vServiceUrl + "ILogComment.asmx/GetContentCommentPageList", connSreadId, type, pageindex, 10);

        newType = type;

    }

    $("#commentTagId").html(commentDetial);
    $("#forwardTagId").html(forwardDetial);

}


//转发
function ConSendForwardInfo(ilogID, type, originalID) {

    //获取当然用户id
    var userid = $.cookie('useid');

    //转发内容
    var content = $.trim($("#conTextarea").val()) == "" ? "转发微博" : $("#conTextarea").val();

    content = content.replace(/[\r\n]/mg, " "); //替换换行
    content = content.replace(/^ +| +$/g, '').replace(/ +/g, ' '); //去空格

    $.ajax({
        url: "" + vServiceUrl + "/VipIlogUser.asmx/ILogForwardingCommentInfo",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{ilogID:" + ilogID + ",type:" + type + ",originalID:'" + originalID + "',userid:'" + userid + "',comment:'" + content + "',i:'" + rand + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象
            var result = dataObj.state == 1 ? "转发成功." : "转发失败.";

            showTipe(result, dataObj.state); //提示

            $("#conTextarea").val(""); //清空内容  

            //刷新列表                
            GetContentInfoById("" + vServiceUrl + "/VipIlogUser.asmx/IlogGetContentInfoById", ilogID, 1);
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


//发送评论
function conSendComment(spreadid, isoriginal, comId) {

    var txtId = $("#cont" + comId).val() != undefined ? "cont" + comId : "conTextarea";

    //转发内容
    var content = $("#" + txtId).val();

    content = content.replace(/[\r\n]/mg, " "); //替换换行
    content = content.replace(/^ +| +$/g, '').replace(/ +/g, ' '); //去空格

    //是否是回复
    var indexat = content.indexOf("回复");

    //判断回复的是否为空
    var index = content.indexOf(":");
    var indexstr = content.substring(index + 1, index + 2)

    //判断点击了回复还是评论
    var tipeStr = "评论";

    //判断回复、纯文字
    if (content != "" && indexstr != "") {
        $.ajax({

            url: "" + vServiceUrl + "/VipIlogUser.asmx/ILogAddCommentInfo",
            type: "POST",
            dataType: "json",
            contentType: "application/json;",
            data: "{spreadid:'" + spreadid + "',isoriginal:" + isoriginal + ",content:'" + content + "',commontId:'" + comId + "',i:'" + rand + "'}",
            success: function(json) {

                var dataObj = eval("(" + json.d + ")"); //转换为json对象     　　     
                var result = "";

                if (dataObj.state == 1) {

                    result = tipeStr + "成功";

                    GetContentInfoById("" + vServiceUrl + "VipIlogUser.asmx/IlogGetContentInfoById", ioid, 0);

                    //回复的时候用到
                    $("#sendCommentHiddent").val("0");

                } else {

                    result = tipeStr + "失败";

                }

                showTipe(result, dataObj.state); //提示  

                $("#" + txtId).val("");
            },
            //出错调试         
            error: function(x, e) {
                //alert(x.responseText);     
            },
            //执行成功后自动执行           
            complete: function(x) {

            }
        });
    } else {
        showTipe(tipeStr + "的内容不能为空哦!", 0); //提示            

    }
}


//转发列表
function GetAllForwardList(servicesUrl, isoId, type, PageCurrent, Pagesize, refreshType) {

    var strList = "";

    //刷新列表
    if (refreshType == "conf") {

        GetContentInfoById("" + vServiceUrl + "/VipIlogUser.asmx/IlogGetContentInfoById", isoId, 1);


    } else {

        $.ajax({
            //请求WebService Url         
            url: "" + servicesUrl + "",
            //请求类型,请据WebService接受类型定制          
            type: "POST",
            //预期指定服务器返回类型
            dataType: "json",
            //内容返回类型            
            contentType: "application/json;",
            cache: false,

            //请求参数              
            data: "{isoId:'" + isoId + "',type:" + type + ",pageCurrent:'" + PageCurrent + "',pageSize:'" + Pagesize + "'}",
            //成功           
            success: function(json) {

                //获取服务器的值        
                var dataObj = eval("(" + json.d + ")"); //转换为json对象

                var strList = "";

                //获取当然用户id
                var userid = $.cookie('useid');

                var i = 0;

                //循环获取值
                $.each(dataObj.List, function(idx, item) {
                    if (idx == 0) {
                        if (item.State == "0") {
                            $("#list_div").html("加载错误");
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
                        sowhpage_ul(PageCurrent, item.RecordCount, 0, "conf");   //分页
                        return true;
                    }
                    else if (item.RecordCount == 1) //显示条数页数
                    {
                        $("#sowhpage_div").html("");    //去掉分页

                        return true;
                    }

                    //如果遍历到不存在节点就不构建
                    if (item.is_id != undefined) {

                        i++;

                        var url = "";
                        var deleteHtml = "";
                        var reportHtml = "";

                        //判断自己或是他人
                        if (userid == item.userid) {

                            url = "/u";
                            //con-评论  conf-转发
                            deleteHtml = "<a href=\"javascript:void(0);\" onclick=\"showBlogResult(" + item.is_id + ",'conf'," + type + ");\" class=\"Gray9\">删除</a> | ";

                        } else {

                            url = "/u_" + item.userid;

                            reportHtml = "<a href=\"javascript:void(0);\" onclick=\"ShowPageReport(" + item.is_id + ",'" + item.nickname + "','" + item.face + "','" + item.content + "')\" class=\"Gray9\">举报</a> | ";

                        }

                        var face = item.face == "" ? "default1.png" : item.face;

                        strList += "<div class=\"CenCom G6\">";
                        strList += "<a href=\"" + url + "\" id=\"miniuser" + item.is_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.is_id + ")\">";
                        strList += "<img src=\"images/face/small/" + face + "\" alt=\"" + item.nickname + "\" title=\"" + item.nickname + "\" width=\"30\" height=\"30\" class=\"L\" />";
                        strList += "<div class=\"L18 Pl10 L Rbox\">";
                        strList += "<a href=\"" + url + "\" alt=\"" + item.nickname + "\" title=\"" + item.nickname + "\" class=\"Blue\">";
                        strList += item.nickname;
                        strList += "</a>: ";
                        strList += unescape(item.content);
                        strList += " (" + item.timeStr + ")";
                        strList += "<br>";
                        strList += "<div class=\"Tr G9\">";

                        strList += reportHtml; //举报

                        strList += deleteHtml; //删除

                        var spreadnum = item.is_spreadnum == "" ? 0 : item.is_spreadnum

                        strList += "<a href=\"javascript:void(0);\" onclick=\"checkForWard(" + item.userid + "," + item.is_id + "," + item.is_isoriginal + "," + spreadnum + ",'conf');\" class=\"Gray9\">";

                        strList += "转发";

                        //转发数
                        if (item.is_spreadnum != 0) {

                            strList += "(" + item.is_spreadnum + ")";

                        }
                        strList += "</a>";
                        strList += "</div>";
                        strList += "</div>";
                        strList += "</div>";


                        if (i != 10 && i != dataObj.List.length - 2) {

                            strList += "<div class=\"Hr_10\"></div>";
                            strList += "<div class=\"Line_dashed\"></div>";
                            strList += "<div class=\"Hr_10\"></div>";

                        }

                    }

                });

                $("#list_div").html(strList);
                scroll(0, 0);  //翻页后需要回到顶部


            },
            //出错调试         
            error: function(x, e) {

                //alert("加载异常");

                //window.location.href = "http://c.instrument.com.cn/art/ilog/404.asp";

            },
            //执行成功后自动执行           
            complete: function(x) {

            }
        });
    }

}


//评论列表
function GetAllCommentList(servicesUrl, isoId, type, PageCurrent, Pagesize) {

    $.ajax({
        //请求WebService Url         
        url: "" + servicesUrl + "",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json",
        cache: false,
        //请求参数              
        data: "{currentid:'" + isoId + "',type:" + type + ",PageCurrent:'" + PageCurrent + "',PageSize:'" + Pagesize + "'}",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var strList = "";

            //获取当然用户id
            var userid = $.cookie('useid');

            var i = 0;
            var strList = "";

            //循环获取值
            $.each(dataObj.List, function(idx, item) {
                if (idx == 0) {
                    if (item.State == "0") {
                        $("#list_div").html("加载错误");
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
                    sowhpage_ul(PageCurrent, item.RecordCount, 0, "");   //分页
                    return true;
                }
                else if (item.RecordCount == 1) //显示条数页数
                {
                    $("#sowhpage_div").html("");    //去掉分页

                    return true;
                }

                //如果遍历到不存在节点就不构建
                if (item.ic_id != undefined) {

                    i++;

                    var url = "";
                    var deleteHtml = "";
                    var reportHtml = "";

                    //判断自己或是他人
                    if (userid == item.userid) {

                        url = "/u";

                        deleteHtml = "<a href=\"javascript:void(0);\" onclick=\"showContentCommentResult(" + item.ic_id + "," + type + ");\" class=\"Gray9\">删除</a> | ";

                    } else {

                        url = "/u_" + item.userid;

                        reportHtml = "<a href=\"javascript:void(0);\" onclick=\"ShowPageReport(" + item.ic_id + ",'"
                        + item.nickname + "','" + item.face + "','" + item.content + "')\" class=\"Gray9\">举报</a> | ";

                    }

                    var face = item.face == "" ? "default1.png" : item.face;

                    strList += "<div class=\"CenCom G6\">";
                    strList += "<a href=\"" + url + "\">";
                    strList += "<img src=\"images/face/small/" + face + "\" alt=\"" + item.nickname + "\" title=\"" + item.nickname + "\" id=\"miniuser" + item.ic_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.ic_id + ")\" width=\"30\" height=\"30\" class=\"L\" />";
                    strList += "<div class=\"L18 Pl10 L Rbox\">";
                    strList += "<a href=\"" + url + "\" id=\"uu" + item.ic_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.ic_id + ")\" alt=\"" + item.nickname + "\" title=\"" + item.nickname + "\" class=\"Blue\">";
                    strList += item.nickname;
                    strList += "</a>: ";
                    strList += unescape(item.content);
                    strList += " (" + (item.timeStr) + ")";
                    strList += "<br>";
                    strList += "<div class=\"Tr G9\">";

                    strList += reportHtml; //举报

                    strList += deleteHtml; //删除
                    //strList += " | ";
                    strList += "<a href=\"javascript:void(0);\" onclick=\"checkContent('" + isoId + "','" + item.nickname + "','" + item.ic_type + "','" + item.ic_id + "');\" class=\"Gray9\">回复</a>";
                    strList += "</div>";
                    strList += "<div id=\"ckCon" + item.ic_id + "\"></div>";
                    strList += "</div>";
                    strList += "</div>";


                    if (i != 10 && i != dataObj.List.length - 2) {

                        strList += "<div class=\"Hr_10\"></div>";
                        strList += "<div class=\"Line_dashed\"></div>";
                        strList += "<div class=\"Hr_10\"></div>";

                    }

                }

            });

            $("#list_div").html(strList);
            scroll(0, 0);  //翻页后需要回到顶部
        },
        //出错调试         
        error: function(x, e) {

        },
        //执行成功后自动执行           
        complete: function(x) {

        }
    });

}

//弹出删除评论提示框
function showContentCommentResult(icid, type) {

    //弹出转发框
    schoolDialog = $.dialog(
       {

           id: "divShowGroupTrue",
           title: false,
           content: showContentCommentHtml(icid, type),
           max: false,
           min: false,
           lock: true,
           cache: false,
           padding: 0

       });

}
//评论样式.by lx on 20120620
function showContentCommentHtml(icid, type) {

    ShowTable = "<div class=\"WindowWark350\">";
    ShowTable += "<h1 class=\"WindowTil G4 F14\">";
    ShowTable += "<a href=\"#\"><img class=\" R Img\" src=\"http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif\" alt=\"关闭\" onclick=\"schoolDialog.close();\"/></a>提示</h1>";
    ShowTable += "<div class=\"WindowBox Tc\">";
    ShowTable += '<div class=" Tc F14 L30  WindowSak">';
    ShowTable += '<img src="http://simg.instrument.com.cn/ilog/blue/images/ask.gif" class="L" />确定要删除该回复吗？';
    ShowTable += '</div>';
    ShowTable += '<div class="Hr_10"></div>';
    ShowTable += '<div class="WinBtn_H R"><span>';
    ShowTable += '<input name="取消" type="button" id="取消" value="取消" onclick=\"schoolDialog.close();\"  />';
    ShowTable += ' </span></div>';
    ShowTable += '<div class="WinBtn  R"><span>';
    ShowTable += "<input name=\"确定\" type=\"button\" id=\"确定\" value=\"确定\" onclick=\"deleteContentComment(" + icid + ", " + type + ");\" />";
    ShowTable += '</span></div>';
    ShowTable += '<div class="Hr_10"></div>';
    ShowTable += '</div>';
    ShowTable += '</div>';

    return ShowTable;

}

//删除评论
function deleteContentComment(icid, type) {

    //获取当然用户id
    var userid = $.cookie('useid');

    $.ajax({

        url: "" + vServiceUrl + "/VipIlogUser.asmx/ILogCommentDeleteById",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{icid:'" + icid + "',userid:'" + userid + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象 

            if (dataObj.state == 1) {

                schoolDialog.close();
                showTipe("删除评论成功!", 1);

                //刷新列表
                GetContentInfoById("" + vServiceUrl + "/VipIlogUser.asmx/IlogGetContentInfoById", ioid, 0);

            } else {

                schoolDialog.close();
                showTipe("删除评论失败!", 0);

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

//选择回复
function checkContent(isoId, nickname, type, id) {
    var replayHtml = $("#ckCon" + id).html();

    //判断是否已经
    if (replayHtml != "") {

        $("#ckCon" + id).html("");
        $("#ckCon" + id).hide();

    }
    else {

        var result = "<div class=\"LookH\">";
        result += "<div class=\"Round1\" >";
        result += "<div class=\"Round2\">";
        result += "<div class=\"Round3\" style=\"width:480px;\">";
        result += "<textarea class=\"Bd Fa P5\" onpropertychange=\"if(value.length>140) value=value.substr(0,140)\"  name=\"cont" + id + "\" style=\"overflow-y:hidden;width:460px;\" id=\"cont" + id + "\" cols=\"60\" rows=\"1\">";
        result += "回复@";
        result += nickname;
        result += ":";
        //result +="回复"+nickname ; 
        result += "</textarea>";
        result += "<div class=\"Hr_10\"></div>";
        result += "<div>";
        result += "<div class=\"Hr_10\"></div>";
        result += "<div class=\"WinBtn  R\"><span>";

        result += "<input name=\"Btncomment\" id=\"Btncomment\" type=\"button\" onclick=\"javascript:conSendComment(" + isoId + "," + type + "," + id + ");\"  value=\"评论\" />";

        result += "</span></div>";

        result += "<div class=\"ICOlist L\" style=\"position:relative;\">";
        result += "<ul>";
        result += "<li><span class=\"ico1\"></span><a href=\"javascript:void(0);\" id=\"forwardExpression\" onclick=\"changeExpressio(this,'cont" + id + "');\" class=\"Blue\">表情</a></li>";
        result += "</ul>";
        result += "</div>";

        result += "</div>";
        //result += "</div>";
        result += "<div class=\"Hr_10\"></div>";
        result += "</div></div>";
        result += "<span class=\"Jiao\" style=\"left: 480px; top: -9px\">◆</span>";
        result += "</div></div>";

        $("#ckCon" + id).html(result);
        $("#ckCon" + id).show();

        myfocus("cont" + id + "")//焦点聚焦

    }

}




//分页控件（加载页面）
//PageCurrent：当前页码
//总页数
//ation：0全部，1博文
//搜索关键字
function sowhpage_ul(PageCurrent, RecordCount, ation, keyword) {

    //keyword=$.trim(keyword);

    var sowhpage_div = $("#sowhpage_div");

    var strShowPage = "";

    //当然页码等于总也数就隐藏下一页按钮
    strShowPage += "<span class=\"R\" " + (PageCurrent == RecordCount ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"nextbtn\" ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_page.gif\" onclick=\"nextpage(" + PageCurrent + "," + RecordCount + "," + ation + ",'" + keyword + "')\" alt=\"下一页\" /></span>";

    strShowPage += "<span class=\"R span\" style=\"position:relative\" ><a href=\"javascript:void(0);\"  id='selOption' class=\"Blue\">第" + PageCurrent + "页</a><img src=\"http://simg.instrument.com.cn/ilog/blue/images/SanJ.gif\" />";

    strShowPage += "<ul class=\"pageBox R Line Fa BrWh\" id=\"selOption_menu\" style=\" position:absolute; bottom:16px; left:15px;display:none;\" onmouseout=\"javascript:$('#selOption_menu').hide();\">";

    for (var i = RecordCount; i >= 1; i--) {
        if (ation == 0)      //全部
        {
            if (PageCurrent == i) {
                if (keyword != "" && keyword != null) {

                    //转发 GetAllForwardList("" + vServiceUrl + "ILog_Spread.asmx/GetContentForwordPageList",connSreadId, type, 1, 10,"f");

                    strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"GetAllForwardList('" + vServiceUrl + "ILog_Spread.asmx/GetContentForwordPageList'," + connSreadId + "," + newType + "," + i + ",10,'f')\"  >第<font color=\"red\">" + i + "</font>页</a></li>";



                }
                else {

                    //评论
                    strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"GetAllCommentList('" + vServiceUrl + "ILogComment.asmx/GetContentCommentPageList'," + connSreadId + "," + newType + "," + i + ",10)\"  >第<font color=\"red\">" + i + "</font>页</a></li>";


                }
            }
            else {

                if (keyword != "" && keyword != null) {
                    strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"GetAllForwardList('" + vServiceUrl + "ILog_Spread.asmx/GetContentForwordPageList'," + connSreadId + "," + newType + "," + i + ",10,'f')\"  >第<font color=\"red\">" + i + "</font>页</a></li>";


                }
                else {

                    strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"GetAllCommentList('" + vServiceUrl + "ILogComment.asmx/GetContentCommentPageList'," + connSreadId + "," + newType + "," + i + ",10)\"  >第<font color=\"red\">" + i + "</font>页</a></li>";


                }
            }
        }
        else                //博文
        {
            if (PageCurrent == i) {
                if (keyword != "" || keyword != null) {
                    strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"GetSearchList('" + vServiceUrl + "ILogOriginal.asmx/GetSearchList2'," + i + ",45,'" + keyword + "')\"  >第<font color=\"red\">" + i + "</font>页</a></li>";
                }
                else {
                    strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"GetList('" + vServiceUrl + "ILogOriginal.asmx/GetList'," + i + ",45)\"  >第<font color=\"red\">" + i + "</font>页</a></li>";
                }
            }
            else {
                if (keyword != "" || keyword != null) {
                    strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"GetSearchList('" + vServiceUrl + "ILogOriginal.asmx/GetSearchList2'," + i + ",45,'" + keyword + "')\"  >第" + i + "页</a></li>";
                }
                else {
                    strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"GetList('" + vServiceUrl + "ILogOriginal.asmx/GetList'," + i + ",45)\"  >第" + i + "页</a></li>";
                }
            }
        }

    }

    strShowPage += "</ul>";
    strShowPage += "</span>";

    //当前页码小于1就隐藏上一页页码

    strShowPage += "<span class=\"R\" " + (PageCurrent <= 1 ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"upbtn\"  ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_pageT.gif\" onclick=\"uppage(" + PageCurrent + "," + RecordCount + "," + ation + ",'" + keyword + "')\" alt=\"上一页\" /></span>";

    strShowPage += "<div class=\"Hr_20\"></div>";

    sowhpage_div.html(strShowPage);

    //动态创建事件
    MousePage();

    return strShowPage;
}

//创建鼠标悬停事件
function MousePage() {
    var selOption = $('#selOption');
    var selOption_menu = $('#selOption_menu');

    selOption.mouseover(function() {
        selOption_menu.show(); //初始化设置
        MenuDivShow(this.id);
    }).mouseout(function() {
        $('#' + this.id + '_menu').hide();
    });

    selOption_menu.mouseover(function() {
        selOption_menu.show(); //初始化设置
        MenuDivShow(this.id);
    })

}

//记录当前页码
var pageindex = 1;

//下一页处理
//PageCurrent：当前页码
//RecordCount：总页数
//ation：0全部，1博文
//keyword：关键字
function nextpage(PageCurrent, RecordCount, ation, keyword) {
    var RecordCount_index = RecordCount;    //记录总页数

    //没翻过上一页
    if (pageindex_n <= 2) {
        if (RecordCount >= 1) {

            pageindex++;

            if (keyword != "" && keyword != null) {

                GetAllForwardList(vServiceUrl + "ILog_Spread.asmx/GetContentForwordPageList", connSreadId, newType, pageindex, 10, "f");


            }
            else {

                GetAllCommentList(vServiceUrl + "ILogComment.asmx/GetContentCommentPageList", connSreadId, newType, pageindex, 10);


            }

        }
    }
    else {
        if (RecordCount >= 1) {
            pageindex_n++;

            if (ation == 0) {

                if (keyword != "" && keyword != null) {

                    GetAllForwardList(vServiceUrl + "ILog_Spread.asmx/GetContentForwordPageList", connSreadId, newType, pageindex_n, 10, "f");


                }
                else {

                    GetAllCommentList(vServiceUrl + "ILogComment.asmx/GetContentCommentPageList", connSreadId, newType, pageindex_n, 10);


                }

            }
            else {
                //重新绑定
                GetAllCommentList(vServiceUrl + "ILogComment.asmx/GetContentCommentPageList", connSreadId, newType, pageindex, 10);

            }
        }
    }

    if (RecordCount_index == pageindex) {
        pageindex = 1;
    }

}




//记录当前页码
var pageindex_n = 2;

//上一页处理
//PageCurrent：当前页码
//RecordCount：总页数
//ation：0全部，1博文
//搜索关键字keyword
function uppage(PageCurrent, RecordCount, ation, keyword) {
    var pageindex_n = PageCurrent;    //记录总页数

    if (pageindex == 1) {
        if (PageCurrent <= RecordCount) {
            pageindex_n--;

            if (ation == 0)  //全部
            {
                if (keyword != "") {
                    GetAllForwardList(vServiceUrl + "ILog_Spread.asmx/GetContentForwordPageList", connSreadId, newType, pageindex_n, 10, "f");
                }
                else {
                    GetAllCommentList(vServiceUrl + "ILogComment.asmx/GetContentCommentPageList", connSreadId, newType, pageindex_n, 10);
                }
            }

        }
    }
    else {
        if (PageCurrent <= RecordCount) {
            pageindex--;

            if (ation == 0)  //全部
            {
                if (keyword != "") {
                    GetAllForwardList(vServiceUrl + "ILog_Spread.asmx/GetContentForwordPageList", connSreadId, newType, pageindex, 10, "f");
                }
                else {
                    GetAllCommentList(vServiceUrl + "ILogComment.asmx/GetContentCommentPageList", connSreadId, newType, pageindex, 10);
                }
            }

        }
    }

    //如果到第一页了就隐藏上一也
    if (pageindex_n == 1) {
        pageindex_n = 2;    //索引初始化
    }
}
 