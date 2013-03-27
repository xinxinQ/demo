

$(document).ready(function() {
    //加载Ilog站内导航
    funGetTopMenuService("" + vServiceUrl + "IlogTopMenu.asmx/ILogGetTopMenu", "{}", "");
    //加载Ilog用户导航
    funGetTopUserMenuService("" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenu", "{MenuLive:'0'}", "");
    //加载ilog左侧菜单
    funGetleftMenuService("" + vServiceUrl + "ILogWebLeftMenu.asmx/ILogGetHomeLeftMneu", "{MenuLive:'2'}", "");

    //获取当然用户id
    var userid = $.cookie('useid');


    //获取用户头像信息
    VipILogHome(vServiceUrl + "VipIlogUser.asmx/ILogGetUserInfoById", userid);
    
    //全部it信息
    GetUserILList(1, 45, 0);

    //清除@记录
    ClearAtNum();

    ShowTitle("@我的博文");

    //搜索智能提示
    $("#keyword_s").keyup(function(evt) {
        //回车键搜提示无效
        if (!isEnterKey()) {
            searchtowho_s();
        }
        //上下键处理
        funListBeginUp(evt);
    });
    //获取回车事件
    getenterevent();

});


//是否在显示搜索提示时第一次按上键，如果是要选中最后一项，该变量默认是不选中的
var isp = false;

//智能提示收件人（页面搜索用户）
function searchtowho_s() {
    //收件人昵称
    var towho = $("#keyword_s");

    var strTowhoValue = $.trim(towho.val());


    //校验收信人
    if (strTowhoValue == "") {
        return false;
    }

    //查看类型（0：博文，1：评论）
    var ation_s = $("#ation_s").val();

    //开始发送
    $.ajax({
        //请求WebService Url
        url: "" + vServiceUrl + "ITILogList.asmx/GetATUserList",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        cache: false,
        //请求参数
        data: "{keyword:'" + strTowhoValue + "',ation:'" + ation_s + "'}",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var strList = "";

            //获取当然用户id
            //var userid = $.cookie('useid'); 

            //循环获取值
            $.each(dataObj.List, function(idx, item) {

                if (idx == 0) {
                    if (item.State == "0") {
                    }
                    else if (item.State == "2")  //无数据不显示分页控件
                    {
                        return false;   //无数据不再往下执行
                    }
                    else {
                        return true;
                    }
                }
                else {
                    if (item.nickname != undefined) {

                        strList += "<li style=\"cursor:hand;\" id=\"il_s" + idx + "\"  onclick=\"Getnickname_Box_s2('"
                         + item.nickname + "')\" ><span id=\"hrSearchTowhPeple" + idx + "\" >" + item.nickname + "</span></li>";

                        return true;
                    }
                }
            });

            //加载下拉菜单
            $("#GetSearchTowho_Menu").html(strList);

            //有数据显示下拉框
            if (strList != "") {
                GetSearchTowhUpList_s();

                //重新搜索数据时要初始化是否在搜索框中直接按上键的标记
                isp = false;
            }
            else    //没有数据隐藏掉提示框
            {
                $("#GetSearchTowho_Menu").hide();
            }


            //鼠标滑动保持唯一选中的样式（下拉项）
            $("#GetSearchTowho_Menu li").mouseover(function() {
                //鼠标滑过
                $(this).addClass("WindowBG");
                var index = $("#GetSearchTowho_Menu li").index($(this));

                //开始遍历
                $("#GetSearchTowho_Menu li").each(function(i) {
                    if (i != index) {
                        $(this).removeClass();
                    }
                })
            });

        },
        //出错调试         
        error: function(x, e) {


        },
        //执行成功后自动执行           
        complete: function(x) {

        }
    });

}

//把选中的收件人放入框中
//towho：收件人
function Getnickname_Box_s2(towho) {
    $("#keyword_s").val(towho);

    //判断是否用鼠标选择搜索提示结果，如果是隐藏下拉，搜索框获取焦点，并执行搜索
    var event = arguments.callee.caller.arguments[0] || window.event;

    if (event.keyCode != 38 && event.keyCode != 40 && event.keyCode != undefined) {
        $("#GetSearchTowho_Menu").hide();
        $("#keyword_s").focus();
        GetSearchList(1, 45, towho);
    }
}

//搜索下拉
function GetSearchTowhUpList_s() {

    //收件人文本框值
    var txtSearchValue = $("#keyword_s").val();

    //框内数据不为空就开始定位
    if (txtSearchValue != null && txtSearchValue != "") {
        setMenuPositionsSearch_s2("GetSearch");
        MenuDivShow_s2("GetSearch");
    }
    else {
        $("#GetSearchTowho_Menu").hide();
    }

}

//下拉框定位
function setMenuPositionsSearch_s2(ShowID) {
    var offset = $('#keyword_s').offset();
    var divheight = $('#keyword_s').innerHeight();

    var leftpadd = 0;

    $('#' + ShowID + 'Towho_Menu').css
	({
	    'position': 'absolute'
	}).show();

}

//控制隐藏显示
function MenuDivShow_s2(showdiv) {
    $('#' + showdiv + 'Towho_Menu').mouseover(function() { $(this).show(); });
    $('#' + showdiv + 'Towho_Menu').mouseout(function() { $(this).hide(); });
    $('#' + showdiv).mouseout(function() { $('#' + showdiv + 'Towho_Menu').hide(); });

}

//清空at提醒信息
function ClearAtNum() {
    var ajaxurl = vServiceUrl + "VipILog.asmx/ClearAtNum";
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
            if (dataObj.State == 1) {
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


//评论列表处理
//servicesUrl：服务地址
//PageCurrent：当前页面
//pagesize：每页多少行数据
//ation：查看类型（0：博文，1：评论）
function GetUserILList(PageCurrent, pagesize, ation) {
    //查看类型（0：博文，1：评论）
    var ation_s = $("#ation_s");
    ation_s.val(ation);
    $.ajax({
        //请求WebService Url
        url: vServiceUrl + "ITILogList.asmx/GetATPageList",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        //请求参数              
        data: "{PageCurrent:'" + PageCurrent + "',PageSize:'" + pagesize + "',ation:'" + ation + "'}",
        //成功           
        success: function(json) {
            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var strList = "";

            //获取当然用户id
            var userid = $.cookie('useid');

            //循环获取值
            $.each(dataObj.ILList, function(idx, item) {
                if (idx == 0) {
                    if (item.State == "0") {
                        $("#list_div").html("加载错误");
                    }
                    else if (item.State == "2")  //无数据不显示分页控件
                    {
                        var sowhpage_div = $("#sowhpage_div");
                        sowhpage_div.html(""); //初始化分页控件
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

    
                //获取当然用户id
                var userid = $.cookie('useid');


                var url = "";
                var deleteHtml = "";
                var contentUrl = "";
                var contentPreUrl = "";

                //判断自己或是他人
                if (userid == item.ia_atuserid) {

                    url = "/u";

                    contentUrl = "cont_" + item.spreadid + "";

                    deleteHtml = "<a href=\"javascript:void(0);\" onclick=\"showBlogResult(" + item.is_id + ",'a'," + userid + ");\" class=\"Blue\">删除</a> | ";


                } else {

                    url = "/u_" + item.ia_atuserid;

                    contentUrl = "tcont_" + item.ia_atuserid + "_" + item.spreadid;
                    contentPreUrl = "tcont_";

                }

                //0：博文，1：评论
                if (ation == 0) {
                    if (item.ia_id != undefined) {
                        //构建数据
                        strList += "<div class=\"Centent ENG\"><a href=\"" + url + "\" ><img src=\"" + (item.face != "" ? "images/face/small/" + item.face : ""
                        + FaceImagesUrl + "default1.png") + "\" alt=\"" + item.nickname + "\" class=\"L Img\" id=\"user"
                        + item.ia_id + "\" onmouseover=\"UserInfoShowOver(this," + item.ia_atuserid + "," + item.ia_id + ")\" /></a>";
                        strList += "<div class=\"Txt R \" style=\" position:relative\"><p class=\"F14 G6 L26\"><a href=\"" + url + "\" id=\"nickname"
                        + item.ia_id + "\" onmouseover=\"UserInfoShowOver(this," + item.ia_atuserid + "," + item.ia_id + ")\" class=\"Blue\">"
                        + item.nickname + "</a>" + ShowVerifyImg(item.memberlevel) + "："
                        + unescape(item.ia_content) + "</p><div id=\"div" + item.ia_id + "\"></div>";
                        if (item.ia_type == 2) {
                            //转发
                            strList += "<div id=\"divContent" + item.ia_logid + "\"></div>";
                            GetOriginalInfo(item.iso_id,item.ia_logid);
                        }

                        strList += " <div class=\"G9 Fa Info\"><span class=\"L\"><a href=\"" + contentUrl + "\" class=\"Blue\">" + item.intime + "</a> &nbsp;来自<a href=\"" + item.is_url + "\" onclick=\"return LoginDiv(16);\">" + item.is_name + "</a>" + (userid == item.ia_atuserid ? "" : "  <a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"ShowPageReport(" + item.spreadid + ",'" + item.nickname + "','" + item.face + "','" + item.ia_content + "')\" >举报</a>") + " </span><span class=\"R\"> <a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"checkForWard(" + item.ia_atuserid + "," + item.spreadid + "," + (item.ia_type == 0 ? 1 : 0) + "," + (item.is_spreadnum == "" ? 0 : item.is_spreadnum) + ",'d');\" >转发(" + (item.is_spreadnum == "" ? 0 : item.is_spreadnum) + ")</a> |  <a class=\"Blue\" id=\"replyCount" + item.spreadid + "\" href=\"javascript:void(0);\" onclick=\"checkComment(0,'c" + item.ia_id + "'," + item.spreadid + "," + (item.ia_type == 0 ? 1 : 0) + "," + (item.is_commentnum == "" ? 0 : item.is_commentnum) + ");\" >评论(" + (item.is_commentnum == "" ? 0 : item.is_commentnum) + ")</a></span></div> ";
                        strList += "<div id=\"c" + item.ia_id + "\"></div>";
                        strList += " </div> ";
                        strList += " <Div class=\"Hr_1\"></Div> ";
                        strList += " <div class=\"Line_ilog\"></div> ";

                        if (item.is_haspic == 1) //有图片
                        {
                            GetPic(vServiceUrl + "ILogPic.asmx/GetPic", item.iso_id, item.ia_id);
                        }

                        strList += " </div> ";
                    }
                }
                else {
                    if (item.ia_id != undefined) {
                        var commentedurl = "";

                        if (userid == item.commenteduserid) {
                            contentPreUrl = "cont_";
                            commentedurl = "/u";
                        }
                        else {
                            contentPreUrl = "tcont_" + item.commenteduserid + "_";
                            commentedurl = "/u_" + item.commenteduserid;
                        }

                        strList += "<div class=\"comment ENG\">";
                        strList += " <div class=\"pic L\"><a href=\"" + url + "\"><img src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png")
                        + "\" alt=\"" + item.nickname + "\" class=\"L Img\" id=\"user" + item.ia_id + "\" onmouseover=\"UserInfoShowOver(this," + item.ia_atuserid + "," + item.ia_id
                        + ")\"/></a></div> ";
                        strList += " <div class=\"info R G9\"><a href=\"" + url + "\" id=\"nickname" + item.ia_id + "\" onmouseover=\"UserInfoShowOver(this," + item.ia_atuserid + ","
                        + item.ia_id + ")\"  class=\"Blue\">" + item.nickname
                        + "</a>" + ShowVerifyImg(item.memberlevel) + "：<a href=\""
                        + contentUrl + "\">" + unescape(item.ia_content) + "</a><span>（" + item.intime + "）</span> <br /> ";
                        if (item.ic_commentid == 0) {
                            strList += " <p class=\" L19\">评论 <a href=\"" + commentedurl + "\" id=\"to" + item.ia_id + "\" onmouseover=\"UserInfoShowOver(this,"
                            + item.commenteduserid + "," + item.ia_id + ")\"  class=\"Blue\">" + item.commentednickname + "</a> 的微博： <a href=\""
                            + contentPreUrl + item.spreadid + "_" + item.ia_type + "\" class=\"Blue\">&ldquo;" + unescape(item.atContent) + "&rdquo;</a></p> ";
                        }
                        else {

                            strList += " <p class=\" L19\">回复 <a href=\"" + commentedurl + "\" id=\"to" + item.ia_id + "\" onmouseover=\"UserInfoShowOver(this,"
                            + item.commenteduserid + "," + item.ia_id + ")\" class=\"Blue\">"
                            + item.commentednickname + "</a> 的评论： <a href=\"" + contentPreUrl +  item.spreadid + "_" + item.ia_type
                            + "\" class=\"Blue\">&ldquo;" + unescape(item.atContent) + "&rdquo;</a></p> ";
                        }
                        strList += " <div class=\"Hr_4\"></div> ";
                        strList += " <div class=\"txt G9\"><span class=\"R\"><a class=\"Blue\" onclick=\"AtReplyComment(0,'atc" + item.ia_id + "'," + item.spreadid + ",0,'" + item.nickname + "'," + item.ia_logid + ");\" href=\"javascript:void(0);\">回复</a></span>来自：<a target=\"_blank\" title=\"" + item.is_name + "\" href=\"" + item.is_url + "\" class=\"Blue\" onclick=\"return LoginDiv(16);\">" + item.is_name + "</a></div> ";
                        strList += " </div> ";
                        strList += "<div id=\"atc" + item.ia_id + "\"></div>";
                        strList += " <div class=\" Hr_1\"></div> ";
                        strList += " <div class=\"Line_ilog\"></div> ";
                        strList += " </div>  ";
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




//搜索列表
//servicesUrl：服务地址
//PageCurrent：当前页面
//pagesize：每页多少行数据
//keyword：关键字
function GetSearchList(PageCurrent, pagesize, keyword) {
    //查看类型（0：博文，1：评论）
    var ation = $("#ation_s").val();
    $.ajax({
        //请求WebService Url
        url: vServiceUrl + "ITILogList.asmx/GetIlSearchList",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        //请求参数              
        data: "{PageCurrent:'" + PageCurrent + "',PageSize:'" + pagesize + "',keyword:'" + keyword + "',ation:'" + ation + "'}",
        //成功           
        success: function(json) {
            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var strList = "";

            //获取当然用户id
            var userid = $.cookie('useid');

            //循环获取值
            $.each(dataObj.ILList, function(idx, item) {
                if (idx == 0) {
                    if (item.State == "0") {
                        $("#list_div").html("加载错误");
                    }
                    else if (item.State == "2")  //无数据不显示分页控件
                    {
                        var sowhpage_div = $("#sowhpage_div");
                        sowhpage_div.html(""); //初始化分页控件
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
                    sowhpage_ul(PageCurrent, item.RecordCount, 0, keyword);   //分页
                    return true;
                }
                else if (item.RecordCount == 1) //显示条数页数
                {
                    $("#sowhpage_div").html("");    //去掉分页

                    return true;
                }

                //获取当然用户id
                var userid = $.cookie('useid');


                var url = "";
                var deleteHtml = "";
                var contentUrl = "";
                var contentPreUrl = "";

                //判断自己或是他人
                if (userid == item.userid) {

                    url = "/u";

                    contentUrl = "cont_" + item.iso_id + "";

                    deleteHtml = "<a href=\"javascript:void(0);\" onclick=\"showBlogResult(" + item.is_id + ",'a'," + userid + ");\" class=\"Blue\">删除</a> | ";


                } else {

                    url = "/u_" + item.userid;

                    contentUrl = "/tcont_"+item.userid+"_" + item.iso_id;
                    contentPreUrl = "/tcont_";

                }

                //0：博文，1：评论
                if (ation == 0) {
                    if (item.ia_id != undefined) {
                        //构建数据
                        strList += "<div class=\"Centent ENG\"><a href=\"" + url + "\" id=\"user" + item.ia_id + "\" onmouseover=\"UserInfoShowOver(this," + item.ia_atuserid + ","
                        + item.ia_id + ");\" ><img src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"" + item.nickname + "\" class=\"L Img\" /></a>";
                        strList += "<div class=\"Txt R \" style=\" position:relative\"><p class=\"F14 G6 L26\"><a href=\"" + url + "\" id=\"nickname"
                        + item.ia_id + "\" onmouseover=\"UserInfoShowOver(this," + item.ia_atuserid + "," + item.ia_id + ")\" class=\"Blue\">"
                        + item.nickname.replace(keyword, "<font color=\"red\">" + keyword + "</font>")
                        + "</a>" + ShowVerifyImg(item.memberlevel) + "："
                        + unescape(item.ia_content) + "</p><div id=\"div" + item.ia_id + "\"></div>";
                        if (item.ia_type == 2) {
                            //转发
                            strList += "<div id=\"divContent" + item.ia_logid + "\"></div>";
                            GetOriginalInfo(item.iso_id,item.ia_logid);
                        }

                        strList += " <div class=\"G9 Fa Info\"><span class=\"L\"><a href=\"" + contentUrl + "\" class=\"Blue\">" + item.intime + "</a> &nbsp;来自<a href=\"" + item.is_url + "\" onclick=\"return LoginDiv(16);\">"
                        + item.is_name + "</a>" + (userid == item.ia_atuserid ? "" : "  <a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"ShowPageReport(" + item.spreadid + ",'"
                        + item.nickname + "','" + item.face + "','" + item.ia_content + "')\" >举报</a>") + " </span><span class=\"R\"> <a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"checkForWard("
                        + item.ia_atuserid + "," + item.spreadid + "," + (item.ia_type == 0 ? 1 : 0) + "," + (item.is_spreadnum == "" ? 0 : item.is_spreadnum) + ",'d');\" >转发("
                        + (item.is_spreadnum == "" ? 0 : item.is_spreadnum) + ")</a> |  <a class=\"Blue\" id=\"replyCount" + item.spreadid + "\" href=\"javascript:void(0);\" onclick=\"checkComment(0,'c"
                        + item.ia_id + "'," + item.spreadid + "," + (item.ia_type == 0 ? 1 : 0) + "," + (item.is_commentnum == "" ? 0 : item.is_commentnum) + ");\" >评论("
                        + (item.is_commentnum == "" ? 0 : item.is_commentnum) + ")</a></span></div> ";
                        strList += "<div style=\"display:none;\" id=\"h" + item.ia_id + "\" onmouseover=\"mousermoveInfo(" + item.ia_id + ");\" ></div>";
                        strList += "<div id=\"c" + item.ia_id + "\"></div>";
                        strList += " </div> ";
                        strList += " <Div class=\"Hr_1\"></Div> ";
                        strList += " <div class=\"Line_ilog\"></div> ";

                        if (item.is_haspic == 1) //有图片
                        {
                            GetPic(vServiceUrl + "ILogPic.asmx/GetPic", item.iso_id, item.ia_id);
                        }

                        strList += " </div> ";
                    }
                }
                else {
                    if (item.ia_id != undefined) {
                        var commentedurl = "";

                        if (userid == item.commenteduserid) {
                            contentPreUrl = "cont_";
                            commentedurl = "/u";
                        }
                        else {
                            contentPreUrl = "tcont_" + item.commenteduserid + "_";
                            commentedurl = "/u_" + item.commenteduserid;
                        }

                        strList += "<div class=\"comment ENG\">";
                        strList += " <div class=\"pic L\"><a href=\"" + url + "\"><img src=\"" + (item.face != "" ? "images/face/small/" + item.face : ""
                        + FaceImagesUrl + "default1.png") + "\" alt=\"" + item.nickname + "\" class=\"L Img\" id=\"user" + item.ia_id + "\" onmouseover=\"UserInfoShowOver(this,"
                         + item.ia_atuserid + "," + item.ia_id + ")\" /></a></div> ";
                        strList += " <div class=\"info R G9\"><a href=\"" + url + "\" id=\"nickname" + item.ia_id + "\" onmouseover=\"UserInfoShowOver(this," + item.ia_atuserid
                        + "," + item.ia_id + ")\" class=\"Blue\">" + item.nickname.replace(keyword, "<font color=\"red\">" + keyword + "</font>")
                        + "</a>" + ShowVerifyImg(item.memberlevel) + "：<a href=\"" + contentUrl + "\">" + unescape(item.ia_content) + "</a><span>（" + item.intime + "）</span> <br /> ";
                        if (item.ic_commentid == 0) {
                            strList += " <p class=\" L19\">评论 <a href=\"" + commentedurl + "\" id=\"to" + item.ia_id + "\" onmouseover=\"UserInfoShowOver(this," + item.commenteduserid + ","
                             + item.ia_id + ")\"   class=\"Blue\">" + item.commentednickname.replace(keyword, "<font color=\"red\">" + keyword + "</font>")
                             + "</a> 的微博： <a href=\"" + contentPreUrl  + item.spreadid + "_" + item.ia_type + "\" class=\"Blue\">&ldquo;"
                             + unescape(item.atContent) + "&rdquo;</a></p> ";
                        }
                        else {

                            strList += " <p class=\" L19\">回复 <a href=\"" + commentedurl + "\" id=\"to" + item.ia_id + "\" onmouseover=\"UserInfoShowOver(this,"
                            + item.commenteduserid + "," + item.ia_id + ")\"  class=\"Blue\">"
                            + item.commentednickname.replace(keyword, "<font color=\"red\">" + keyword + "</font>") + "</a> 的评论： <a href=\"" + contentPreUrl  + item.spreadid + "_" + item.ia_type
                            + "\" class=\"Blue\">&ldquo;" + unescape(item.atContent) + "&rdquo;</a></p> ";
                        }
                        strList += " <div class=\"Hr_4\"></div> ";
                        strList += " <div class=\"txt G9\"><span class=\"R\"><a class=\"Blue\" onclick=\"AtReplyComment(0,'atc" + item.ia_id + "'," + item.spreadid + ",0,'"
                        + item.nickname + "'," + item.ia_logid + ");\" href=\"javascript:void(0);\">回复</a></span>来自：<a target=\"_blank\" title=\"" + item.is_name
                        + "\" href=\"" + item.is_url + "\" class=\"Blue\" onclick=\"return LoginDiv(16);\">" + item.is_name + "</a></div> ";
                        strList += " </div> ";

                        strList += "<div id=\"atc" + item.ia_id + "\"></div>";
                        strList += " <div class=\" Hr_1\"></div> ";
                        strList += " <div class=\"Line_ilog\"></div> ";
                        strList += " </div>  ";
                    }
                }
            });
            $("#list_div").html(strList);
            scroll(0, 0);  //翻页后需要回到顶部
        },
        //出错调试         
        error: function(x, e) {
            alert("网络繁忙请稍后...");
        },
        //执行成功后自动执行           
        complete: function(x) {

        }
    });

}

//分页控件（加载页面）
function sowhpage_ul(PageCurrent, RecordCount, ation, keyword) {
    var sowhpage_div = $("#sowhpage_div");

    var strShowPage = "";

    //搜索操作
    if (ation == 3) {
        //当然页码等于总也数就隐藏下一页按钮
        strShowPage += "<span class=\"R\" " + (PageCurrent == RecordCount ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"nextbtn\" ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_page.gif\" onclick=\"nextpage(" + PageCurrent + "," + RecordCount + "," + ation + ",'" + keyword + "')\" alt=\"下一页\" /></span>";
    }
    else {
        //当然页码等于总也数就隐藏下一页按钮
        strShowPage += "<span class=\"R\" " + (PageCurrent == RecordCount ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"nextbtn\" ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_page.gif\" onclick=\"nextpage(" + PageCurrent + "," + RecordCount + "," + ation + ")\" alt=\"下一页\" /></span>";
    }

    strShowPage += "<span class=\"R span\" style=\"position:relative\"  id='selOption'><a href=\"javascript:void(0);\"  class=\"Blue\">第" + PageCurrent + "页</a><img src=\"http://simg.instrument.com.cn/ilog/blue/images/SanJ.gif\" />";

    $("#hidPageIndex").val(PageCurrent);


    strShowPage += "<ul class=\"pageBox R Line Fa BrWh\" id=\"selOption_menu\" style=\" position:absolute; bottom:17px; left:15px;display:none;\">";

    for (var i = RecordCount; i >= 1; i--) {
        if (ation == 3)  //搜索操作
        {
            if (PageCurrent == i) {
                strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"GetSearchList(" + i + ",45,'" + keyword + "')\"  >第<font color=\"red\">" + i + "</font>页</a></li>";
            }
            else {
                strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"GetSearchList(" + i + ",45,'" + keyword + "')\"  >第" + i + "页</a></li>";
            }
        }
        else {
            if (PageCurrent == i) {
                strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"GetUserILList(" + i + ",45," + ation + ")\"  >第<font color=\"red\">" + i + "</font>页</a></li>";
            }
            else {
                strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"GetUserILList(" + i + ",45," + ation + ")\"  >第" + i + "页</a></li>";
            }
        }
    }

    strShowPage += "</ul>";
    strShowPage += "</span>";

    if (ation == 3) {
        //当前页码小于1就隐藏上一页页码
        strShowPage += "<span class=\"R\" " + (PageCurrent <= 1 ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"upbtn\"  ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_pageT.gif\" onclick=\"uppage(" + PageCurrent + "," + RecordCount + "," + ation + ",'" + keyword + "')\" alt=\"上一页\" /></span>";
    }
    else {
        //当前页码小于1就隐藏上一页页码
        strShowPage += "<span class=\"R\" " + (PageCurrent <= 1 ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"upbtn\"  ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_pageT.gif\" onclick=\"uppage(" + PageCurrent + "," + RecordCount + "," + ation + ")\" alt=\"上一页\" /></span>";
    }

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
}



//记录当前页码
var pageindex = 1;

//下一页处理
//PageCurrent：当前页码
//RecordCount：总页数
//操作类型：3是搜索0和1是默认加载的数据类型
function nextpage(PageCurrent, RecordCount, ation, keyword) {
    var RecordCount_index = RecordCount;    //记录总页数

    //没翻过上一页
    if (pageindex_n <= 2) {
        if (RecordCount >= 1) {
            pageindex++;

            if (ation == 3) {
                GetSearchList(pageindex, 45, keyword);
            }
            else {
                GetUserILList(pageindex, 45, ation);
            }
        }
    }
    else {
        if (RecordCount >= 1) {
            pageindex_n++;

            if (ation == 3) {
                GetSearchList(pageindex_n, 45, keyword);
            }
            else {
                GetUserILList(pageindex_n, 45, ation);
            }
        }
    }

    //如果到最大页码就隐藏该按钮（去掉第一页和最后一页）
    if (RecordCount_index == pageindex) {
        pageindex = 1;  //索引初始化
    }
}

//记录当前页码
var pageindex_n = 2;

//上一页处理
//PageCurrent：当前页码
//RecordCount：总页数
//操作类型：3是搜索0和1是默认加载的数据类型
function uppage(PageCurrent, RecordCount, ation, keyword) {
    var pageindex_n = PageCurrent;    //记录总页数

    if (pageindex == 1) {
        if (PageCurrent <= RecordCount) {
            pageindex_n--;

            //搜索操作
            if (ation == 3) {
                GetSearchList(pageindex_n, 45, keyword);
            }
            else {
                GetUserILList(pageindex_n, 45, ation);
            }
        }
    }
    else {
        if (PageCurrent <= RecordCount) {
            pageindex--;

            //搜索操作
            if (ation == 3) {
                GetSearchList(pageindex, 45, keyword);
            }
            else {
                GetUserILList(pageindex, 45, ation);
            }
        }
    }

    //如果到第一页了就隐藏上一也
    if (pageindex_n == 1) {
        pageindex_n = 2;    //索引初始化
    }
}

//切换评论的类型
function commentTyle(index) {
    //切换标签容器
    var my_a = $("#mycomment");
    var post_a = $("#postcomment");

    //圆角容器
    var top_my = $("#top_comment_my");
    var top_post = $("#top_comment_post");

    //我的博文
    if (index == 1) {
        //切换
        post_a.removeClass("center F14");
        my_a.addClass("center F14");
        top_post.removeClass("top");
        top_my.addClass("top");

        ShowTitle("@我的博文");

        //内容
        my_a.html("<a href=\"javascript:void(0);\" onclick=\"commentTyle(1)\" class=\"Blue\"><strong><span class=\"Fa\">@</span>我的博文</strong></a>");
        post_a.html("<a href=\"javascript:void(0);\" onclick=\"commentTyle(0)\" class=\"F14\"><span class=\"Fa\">@</span>我的评论</a>")

        //加载数据
        GetUserILList(1, 45, 0);
    }
    else //我的评论
    {

        ShowTitle("@我的评论");
        //切换
        my_a.removeClass("center F14");
        post_a.addClass("center F14");
        top_my.removeClass("top");
        top_post.addClass("top");

        //内容
        post_a.html("<a href=\"javascript:void(0);\" onclick=\"commentTyle(0)\" class=\"Blue\"><strong><span class=\"Fa\">@</span>我的评论</strong></a>");
        my_a.html("<a href=\"javascript:void(0);\" onclick=\"commentTyle(1)\" class=\"F14\"><span class=\"Fa\">@</span>我的博文</a>")

        //加载数据
        GetUserILList(1, 45, 1);
    }
}

//切换评论的类型（搜索专用，之切换不加载数据）
function commentTyle_Search(index) {
    //切换标签容器
    var my_a = $("#mycomment");
    var post_a = $("#postcomment");

    //圆角容器
    var top_my = $("#top_comment_my");
    var top_post = $("#top_comment_post");

    //收到的评论
    if (index == 1) {
        //切换
        post_a.removeClass("center F14");
        my_a.addClass("center F14");
        top_post.removeClass("top");
        top_my.addClass("top");

        //内容
        my_a.html("<a href=\"javascript:void(0);\" onclick=\"commentTyle(1)\" class=\"Blue\"><strong><span class=\"Fa\">@</span>我的博文</strong></a>");
        post_a.html("<a href=\"javascript:void(0);\" onclick=\"commentTyle(0)\" class=\"F14\"><span class=\"Fa\">@</span>我的评论</a>")
    }
    else //发出的评论
    {
        //切换
        my_a.removeClass("center F14");
        post_a.addClass("center F14");
        top_my.removeClass("top");
        top_post.addClass("top");

        //内容
        post_a.html("<a href=\"javascript:void(0);\" onclick=\"commentTyle(0)\" class=\"Blue\"><strong><span class=\"Fa\">@</span>我的评论</strong></a>");
        my_a.html("<a href=\"javascript:void(0);\" onclick=\"commentTyle(1)\" class=\"F14\"><span class=\"Fa\">@</span>我的博文</a>")
    }

}

//校验搜索
function checkform() {

    if (!LoginDiv(16)) {
        return;
    }

    var element_p = document.getElementById("keyword_s");

    if (element_p.value == "请输入昵称") {
        alert("请输入昵称！");
        element_p.focus();
        return false;
    }
    if (element_p.value == "" || element_p.value == null) {
        alert("请输入昵称！");
        element_p.focus();
        return false;
    }
    if (ignoreSpaces(element_p.value) == "") {
        alert("请输入昵称！");
        element_p.focus();
        return false;
    }
    if (HTMLEncode(element_p.value) == "") {
        alert("请输入昵称！");
        element_p.focus();
        return false;
    }
    if (removeHTMLTag(element_p.value) == "") {
        alert("请输入昵称！");
        element_p.focus();
        return false;
    }

    //获取数据
    GetSearchList(1, 45, element_p.value);

}



//选择评论弹出层
//ret:0 表示点击,1:表示异步刷新
function AtReplyComment(ret, divId, spreadid, isoriginal, nickName, commentid) {

    //判断是否重复点击
    var check = $("#" + divId).html();

    if (check != "" && ret == 0) {
        $("#" + divId).html("");
        $("#" + divId).hide();
    } else {

        var result = "<div class=\"LookH\" style=\"margin-left:80px\" ><div class=\"Round1\">";
        result += "<div class=\"Round2\">";
        result += "<div class=\"Round3\">";
        result += "<textarea class=\"Bd Fa\" onpropertychange=\"if(value.length>140) value=value.substr(0,140)\"  name=\"commentInfoId"
                + spreadid + "\" style=\"overflow-y:hidden;\" id=\"commentInfoId" + spreadid + "\" cols=\"60\" rows=\"2\">回复@" + nickName + ":</textarea>";
        result += "<div class=\"Hr_10\"></div> <div>";
        result += "<div class=\"Hr_10\"></div>";
        result += "<div class=\"WinBtn  R\"><span><input name=\"评论\" id=\"Btncomment\" type=\"button\" onclick=\"javascript:sendReplyComment('" + divId
                 + "','" + spreadid + "','" + isoriginal + "',0," + commentid + ");\"  value=\"评论\" /></span></div>";
        result += "<div class=\"ICOlist L\" style=\"position:relative;\">";
        result += "<ul >";
        result += "<li><span class=\"ico1\"></span><a href=\"javascript:void(0);\" class=\"Blue\" id=\"commentfaceId"
                + spreadid + "\" onmouseover=\"this.style.cursor='pointer'\" onclick=\"changeExpressio(this,'commentInfoId" + spreadid + "');\">表情</a></li></ul></div></div>";

        result += "<div class=\"Hr_10\"></div>";
        result += "</div></div>";
        result += "<span class=\"Jiao\" style=\"left:435px; top:-9px\">◆</span>";
        result += "</div></div>";

        $("#" + divId).html(result);
        $("#" + divId).show();

    }

}


//发送评论
function sendReplyComment(divid, spreadid, isoriginal, commentid) {

    //转发内容
    var content = $("#commentInfoId" + spreadid).val();
    content = content.replace(/[\r\n]/mg, " "); //替换换行
    content = content.replace(/^ +| +$/g, '').replace(/ +/g, ' '); //去空格

    //是否是回复
    var indexat = content.indexOf("回复");

    //判断回复的是否为空
    var index = content.indexOf(":");
    var indexstr = content.substring(index + 1, index + 2)


    //判断回复、纯文字
    if (content != "" && indexstr != "") {
        $.ajax({

            url: "" + vServiceUrl + "/VipIlogUser.asmx/ILogAddCommentInfo",
            type: "POST",
            dataType: "json",
            contentType: "application/json;",
            data: "{spreadid:'" + spreadid + "',isoriginal:" + isoriginal + ",content:'" + content + "',commontId:'" + commentid + "',i:'" + rand + "'}",
            success: function(json) {

                var dataObj = eval("(" + json.d + ")"); //转换为json对象     　　     
                var result = "";

                if (dataObj.state == 1) {

                    result = "评论成功";

                } else {

                    result = "评论失败";

                }

                showTipe(result, dataObj.state); //提示
                $("#commentInfoId" + spreadid).val("");
                $("#" + divid).hide();
                $("#" + divid).html("");


                var ation = $("#ation_s").val();
                var keyword = $.trim($("#keyword_s").val());
                var currentIndex = $("#hidPageIndex").val();
                if (keyword == "" || keyword == "请输入昵称") {
                    GetUserILList(currentIndex, 45, ation);
                }
                else {
                    GetSearchList(currentIndex, 45, keyword);
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
    } else {

        showTipe("评论的内容不能为空哦!", 0); //提示

    }

}


//上下键处理
//搜索下拉
function funListBeginUp(evt) {
    var keynum="";

    if (window.event) // IE
    {
        keynum = evt.keyCode;
    }
    else// Netscape/Firefox/Opera
    {
        keynum = evt.which;
    }

    if (keynum != 13 && keynum != 38 && keynum != 40) {
        return false;
    }
    else if (keynum == 13) {
        funListBeginUpUL("GetSearchTowho_Menu", 2);
    }
    else if (keynum == 38) {
        funListBeginUpUL("GetSearchTowho_Menu", 0);
    }
    else if (keynum == 40) {
        funListBeginUpUL("GetSearchTowho_Menu", 1);
    }

    return false;

}

//上下键处理
function funListBeginUpUL(NameID, vType) {
    var bl = true;

    //用户在搜索结果提示按上键直接选中最后一项
    if (vType == 0 && !isp) {
        var ilsize = $("#GetSearchTowho_Menu li").size();

        $("#il_s" + ilsize).addClass("WindowBG");
        Getnickname_Box_s2($("#il_s" + ilsize).text());

        isp = true;
    }
    else {

        $("#" + NameID + " li").each(function(i) {
            if (vType == 0) {
                if ($(this).hasClass("WindowBG")) {
                    if (bl) {
                        var index = $("#GetSearchTowho_Menu li").index($(this));
                        var ilsize = $("#GetSearchTowho_Menu li").size();

                        //判断是不是选到第一个，如果是需要循环到最后一个
                        if (index == 0) {

                            $(this).removeClass();
                            $("#il_s" + ilsize).addClass("WindowBG");
                            Getnickname_Box_s2($("#il_s" + ilsize).text());
                        }
                        else {
                            $("#GetSearchTowho_Menu li.WindowBG").prev().addClass("WindowBG");
                            $("#GetSearchTowho_Menu li.WindowBG").next().removeClass("WindowBG");


                            //开始遍历
                            $("#GetSearchTowho_Menu li").each(function(i) {
                                if (i != (index - 1) && index != 0) {
                                    $(this).removeClass();
                                }
                            })

                            //把选中的值放入搜索框中
                            if ($(this).prev().text() != "") {
                                Getnickname_Box_s2($(this).prev().text());
                            }
                            else    //如果到了最后一个就保持当前选中的值
                            {
                                Getnickname_Box_s2($(this).text());
                            }
                        }

                        bl = false;

                    }
                }
            }
            else if (vType == 1) {
                if ($(this).hasClass("WindowBG")) {
                    if (bl) {
                        var index = $("#GetSearchTowho_Menu li").index($(this));
                        var ilsize = $("#GetSearchTowho_Menu li").size();

                        //判断是不是选到最后一个，如果是需要循环到第一个
                        if (index == (ilsize - 1)) {
                            $(this).removeClass();
                            $("#il_s1").addClass("WindowBG");
                            Getnickname_Box_s2($("#il_s1").text());
                        }
                        else {
                            //向下
                            $("#GetSearchTowho_Menu li.WindowBG").next().addClass("WindowBG");
                            $("#GetSearchTowho_Menu li.WindowBG").prev().removeClass("WindowBG");


                            //开始遍历，除了当前选中的选项其他都移除
                            $("#GetSearchTowho_Menu li").each(function(i) {
                                if (i != (index + 1) && (index + 1) != ilsize) {
                                    $(this).removeClass();
                                }
                            })

                            //把选中的值放入搜索框中
                            if ($(this).next().text() != "") {
                                Getnickname_Box_s2($(this).next().text());
                            }
                            else    //如果到了最后一个就保持当前选中的值
                            {
                                Getnickname_Box_s2($(this).text());
                            }

                            bl = false
                        }
                    }
                }
                if (bl)    //选择第一条搜索提示
                {
                    //只要不是第一次按上就改变“第一次上键的状态”
                    isp = true;

                    $("#il_s1").addClass("WindowBG");
                    Getnickname_Box_s2($("#il_s1").text());
                }
            }
            else if (vType == 2) {
                //回车
                if ($(this).hasClass("WindowBG")) {
                    var strKeyWord = "";

                    //判断是否按了上下键如果没有说明用户想做模糊查询
                    if (!isUpDownKey()) {
                        strKeyWord = $("#keyword_s").val();
                    }
                    else {
                        strKeyWord = $(this).text();
                    }

                    Getnickname_Box_s2(strKeyWord);

                    GetSearchList(1, 45, strKeyWord);
                }
                $("#GetSearchTowho_Menu").hide();
            }

        })
    }
}




//回车事件
function getenterevent() {
    $(function() {
        $("input[type='text']").keypress(function(evt) {
            evt = (evt) ? evt : ((window.event) ? window.event : "");
            var key = evt.keyCode ? evt.keyCode : evt.which;

            if (key == 13) {
                var id = new String($(this).attr("id"));
                var num = id.substr(id.length - 1, 1);

                $("#bnt_" + num).click();
                return false;
            }
        });
    });
}
