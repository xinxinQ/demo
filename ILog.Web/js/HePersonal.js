
var strUserId_A = 0;        //全部
var strUserId_O = 0;        //博文

$(document).ready(function() {

    //获取当然用户id
    var userid = $("#userid").val();
    strUserId_A = userid;
    strUserId_O = userid;
    //加载Ilog站内导航
    funGetTopMenuService("" + vServiceUrl + "IlogTopMenu.asmx/ILogGetTopMenu", "{}", "");
    //加载Ilog用户导航
    funGetTopUserMenuService("" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenu", "{MenuLive:'0'}", "");

    //加载ilog左侧菜单
    funGetleftMenuService("" + vServiceUrl + "ILogWebLeftMenu.asmx/ILogGetHPersonalLeftMneu", "{MenuLive:'1',hUserID:'" + userid + "'}", "");

    //加载全部内容（个人首页专用）
    GetAllList("" + vServiceUrl + "ILog_Spread.asmx/GetAllList_He", 1, 45, userid);

    //他人主页信息
    VipILogPersonalInfo("" + vServiceUrl + "VipIlogUser.asmx/ILogGetPersonalUserInfoById", userid);

    //显示关注、微博、粉丝、勋章
    showCount("" + vServiceUrl + "VipIlogUser.asmx/ILogGetUserInfoById", userid);

    //判断与用户的关系
    funGetConcernExists(userid);

    RecordVisitHistory();

});

//全部
//servicesUrl：服务地址
//PageCurrent：当前页面
//pagesize：每页多少行数据
function GetAllList(servicesUrl, PageCurrent, pagesize, userid_he) {
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
        data: "{PageCurrent:'" + PageCurrent + "',PageSize:'" + pagesize + "',userid:'" + userid_he + "'}",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var strList = "";

            //获取当然用户id
            var userid = $.cookie('useid');

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
                    sowhpage_ul(PageCurrent, item.RecordCount, 0, "", userid_he);   //分页
                    return true;
                }
                else if (item.RecordCount == 1) //显示条数页数
                {
                    $("#sowhpage_div").html("");    //去掉分页

                    return true;
                }

                //如果遍历到不存在节点就不构建
                if (item.is_id != undefined) {

                    var url = "";
                    var deleteHtml = "";
                    var contentUrl = "";

                    //判断自己或是他人
                    if (userid == item.userid) {

                        url = "/u";
                        contentUrl = "cont_" + item.is_id + "";
                        deleteHtml = "<a href=\"javascript:void(0);\" onclick=\"showBlogResult(" + item.is_id + ",'c'," + userid + ");\" class=\"Blue\">删除</a> | ";


                    } else {

                        contentUrl = "tcont_" + item.userid + "_" + item.is_id;
                        url = "/u_" + item.userid;

                    }


                    //构建数据
                    strList += "<div class=\"Centent\"><a href=\"" + url + "\" ><img src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"头像\" id=\"user" + item.is_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.is_id + ")\"   class=\"L Img\" /></a>";
                    strList += "<div class=\"Txt R \" style=\" position:relative\"><p class=\"F14 G6 L26 ENG\"><a href=\"" + url + "\" id=\"uu" + item.is_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.is_id + ")\"  class=\"Blue\">" + item.nickname + "</a>" + ShowVerifyImg(item.memberlevel) + "：" + unescape(item.io_content) + "</p><div id=\"div" + item.is_id + "\"></div>";

                    if ((item.is_spreadtype == 1 && item.is_isoriginal == 0) || (item.is_spreadtype == 0 && item.is_isoriginal == 0 && item.is_type == 1)) {
                        strList += "<div id=\"divContent" + item.is_id + "\"></div>";
                        GetOriginalInfo(item.io_id, item.is_id);
                    }

                    strList += " <div class=\"G9 Fa Info\"><span class=\"L\"><a href=\"" + contentUrl + "\" class=\"Blue\">" + item.intime + "</a> &nbsp;来自<a onclick=\"return LoginDiv(16);\" href=\"" + item.is_url + "\">" + item.is_name + "</a>" + (userid == item.userid ? "" : "  <a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"ShowPageReport(" + item.is_id + ",'" + item.nickname + "','" + item.face + "','" + item.io_content + "')\" >举报</a>") + " </span><span class=\"R\"><a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"checkForWard(" + item.userid + "," + item.is_id + "," + item.is_isoriginal + "," + (item.is_spreadnum == "" ? 0 : item.is_spreadnum) + ",'c');\" >转发(" + (item.is_spreadnum == "" ? 0 : item.is_spreadnum) + ")</a> |  <a class=\"Blue\" id=\"replyCount" + item.is_id + "\" href=\"javascript:void(0);\" onclick=\"checkComment(0,'c" + item.is_id + "'," + item.is_id + "," + item.is_isoriginal + "," + (item.vic_commentnum == "" ? 0 : item.vic_commentnum) + ");\" >评论(" + (item.vic_commentnum == "" ? 0 : item.vic_commentnum) + ")</a></span></div> ";

                    strList += "<div id=\"c" + item.is_id + "\"></div>";
                    strList += " </div> ";
                    strList += " <Div class=\"Hr_1\"></Div> ";
                    strList += " <div class=\"Line_ilog\"></div> ";

                    if (item.is_haspic == 1) //有图片
                    {
                        GetPic(vServiceUrl + "ILogPic.asmx/GetPic", item.io_id, item.is_id);
                    }
                    strList += " </div>  ";
                }
            });
            $("#list_div").html(strList);
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





//博文
//servicesUrl：服务地址
//PageCurrent：当前页面
//pagesize：每页多少行数据
function GetList(servicesUrl, PageCurrent, pagesize, userid_he) {
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
        data: "{PageCurrent:'" + PageCurrent + "',PageSize:'" + pagesize + "',userid:'" + userid_he + "'}",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var strList = "";

            //获取当然用户id
            var userid = $.cookie('useid');

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
                    sowhpage_ul(PageCurrent, item.RecordCount, 1, "", userid_he);   //分页
                    return true;
                }
                else if (item.RecordCount == 1) //显示条数页数
                {
                    $("#sowhpage_div").html("");    //去掉分页

                    return true;
                }

                //如果遍历到不存在节点就不构建
                if (item.io_id != undefined) {

                    var url = "";
                    var deleteHtml = "";
                    var contentUrl = "";

                    //判断自己或是他人
                    if (userid == item.userid) {

                        url = "/u";

                        contentUrl = "cont_" + item.is_id + "";

                        deleteHtml = "<a href=\"javascript:void(0);\" onclick=\"showBlogResult(" + item.is_id + ",'c'," + userid + ");\" class=\"Blue\">删除</a> | ";


                    } else {

                        url = "/u_" + item.userid;

                        contentUrl = "tcont_"+item.userid+"_" + item.is_id;

                    }

                    //构建数据
                    strList += "<div class=\"Centent\"><a href=\"" + url + "\"  ><img id=\"user" + item.is_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.is_id + ")\"  src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"头像\" class=\"L Img\" /></a>";
                    strList += "<div class=\"Txt R \" style=\" position:relative\"><p class=\"F14 G6 L26 ENG\"><a href=\"" + url + "\" id=\"uu" + item.is_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.is_id + ")\"  class=\"Blue\">" + item.nickname + "</a>" + ShowVerifyImg(item.memberlevel) + "：" + unescape(item.io_content) + "</p><div id=\"div" + item.is_id + "\"></div>";

                    if ((item.is_spreadtype == 1 && item.is_isoriginal == 0) || (item.is_spreadtype == 0 && item.is_isoriginal == 0 && item.is_type == 1)) {
                        strList += "<div id=\"divContent" + item.is_id + "\"></div>";
                        GetOriginalInfo(item.io_id, item.is_id);
                    }
                    strList += " <div class=\"G9 Fa Info\"><span class=\"L\"><a href=\"" + contentUrl + "\" class=\"Blue\">" + item.intime + "</a> &nbsp;来自<a href=\"" + item.is_url + "\" onclick=\"return LoginDiv(16);\">" + item.is_name + "</a>" + (userid == item.userid ? "" : "  <a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"ShowPageReport(" + item.is_id + ",'" + item.nickname + "','" + item.face + "','" + item.userid + "')\" >举报</a>") + " </span><span class=\"R\"> " + deleteHtml + "<a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"checkForWard(" + item.userid + "," + item.is_id + "," + item.is_isoriginal + "," + (item.is_spreadnum == "" ? 0 : item.is_spreadnum) + ",'cc');\" >转发(" + (item.is_spreadnum == "" ? 0 : item.is_spreadnum) + ")</a> |  <a class=\"Blue\" id=\"replyCount" + item.is_id + "\" href=\"javascript:void(0);\" onclick=\"checkComment(0,'c" + item.is_id + "'," + item.is_id + "," + item.is_isoriginal + "," + (item.vic_commentnum == "" ? 0 : item.vic_commentnum) + ");\" >评论(" + (item.vic_commentnum == "" ? 0 : item.vic_commentnum) + ")</a></span></div> ";

                    strList += "<div id=\"c" + item.is_id + "\"></div>";
                    strList += " </div> ";
                    strList += " <Div class=\"Hr_1\"></Div> ";
                    strList += " <div class=\"Line_ilog\"></div> ";

                    if (item.is_haspic == 1) //有图片
                    {
                        GetPic(vServiceUrl + "ILogPic.asmx/GetPic", item.io_id, item.is_id);
                    }
                    strList += " </div>  ";
                }


            });
            $("#list_div").html(strList);
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




//博文
//servicesUrl：服务地址
//PageCurrent：当前页面
//pagesize：每页多少行数据
//keyword：搜索关键字
function GetSearchList(servicesUrl, PageCurrent, pagesize, keyword, userid_he) {
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
        data: "{PageCurrent:'" + PageCurrent + "',PageSize:'" + pagesize + "',keyword:'" + keyword + "',userid:'" + userid_he + "'}",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var strList = "";

            //获取当然用户id
            var userid = $.cookie('useid');

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
                    sowhpage_ul(PageCurrent, item.RecordCount, 1, keyword, userid_he);   //分页 0：全部，1：博文
                    return true;
                }
                else if (item.RecordCount == 1) //显示条数页数
                {
                    $("#sowhpage_div").html("");    //去掉分页

                    return true;
                }

                //如果遍历到不存在节点就不构建
                if (item.io_id != undefined) {


                    var url = "";
                    var deleteHtml = "";
                    var contentUrl = "";

                    //判断自己或是他人
                    if (userid == item.userid) {

                        url = "/u";

                        contentUrl = "cont_" + item.is_id + "";

                        deleteHtml = "<a href=\"javascript:void(0);\" onclick=\"showBlogResult(" + item.is_id + ",'c'," + userid + ");\" class=\"Blue\">删除</a> | ";


                    } else {

                        url = "/u_" + item.userid;

                        contentUrl = "tcont_" + item.userid + "_" + item.is_id;

                    }


                    //构建数据
                    strList += "<div class=\"Centent\"><a href=\"" + url + "\"  ><img id=\"user" + item.is_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," 
                    + item.is_id + ")\"  src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"头像\" class=\"L Img\" /></a>";
                    strList += "<div class=\"Txt R \" style=\" position:relative\"><p class=\"F14 G6 L26 ENG\"><a href=\"" + url + "\" id=\"uu" + item.is_id
                    + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.is_id + ")\"  class=\"Blue\">" + item.nickname + "</a>" + ShowVerifyImg(item.memberlevel) + "："
                    +(unescape(item.io_content).replace(keyword, "<font color=\"red\">" + keyword + "</font>")) + "</p><div id=\"div" + item.is_id + "\"></div>";

                    if ((item.is_spreadtype == 1 && item.is_isoriginal == 0) || (item.is_spreadtype == 0 && item.is_isoriginal == 0 && item.is_type == 1)) {
                        strList += "<div id=\"divContent" + item.is_id + "\"></div>";
                        GetOriginalInfo(item.io_id, item.is_id, keyword);
                    }
                    strList += " <div class=\"G9 Fa Info\"><span class=\"L\"><a href=\"" + contentUrl + "\" class=\"Blue\">" + item.intime + "</a> &nbsp;来自<a href=\"" + item.is_url + "\" onclick=\"return LoginDiv(16);\">" + item.is_name + "</a>" + (userid == item.userid ? "" : "  <a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"ShowPageReport(" + item.is_id + ",'" + item.nickname + "','" + item.face + "','" + item.userid + "')\" >举报</a>") + " </span><span class=\"R\"> " + deleteHtml + "<a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"checkForWard(" + item.userid + "," + item.is_id + "," + item.is_isoriginal + "," + (item.is_spreadnum == "" ? 0 : item.is_spreadnum) + ",'cc');\" >转发(" + (item.is_spreadnum == "" ? 0 : item.is_spreadnum) + ")</a> |  <a class=\"Blue\" id=\"replyCount" + item.is_id + "\" href=\"javascript:void(0);\" onclick=\"checkComment(0,'c" + item.is_id + "'," + item.is_id + "," + item.is_isoriginal + "," + (item.vic_commentnum == "" ? 0 : item.vic_commentnum) + ");\" >评论(" + (item.vic_commentnum == "" ? 0 : item.vic_commentnum) + ")</a></span></div> ";

                    strList += "<div id=\"c" + item.is_id + "\"></div>";
                    strList += " </div> ";
                    strList += " <Div class=\"Hr_1\"></Div> ";
                    strList += " <div class=\"Line_ilog\"></div> ";

                    if (item.is_haspic == 1) //有图片
                    {
                        GetPic(vServiceUrl + "ILogPic.asmx/GetPic", item.io_id, item.is_id);
                    }
                    strList += " </div>  ";
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


//全部
//servicesUrl：服务地址
//PageCurrent：当前页面
//pagesize：每页多少行数据
//keyword：搜索关键字
function GetSearchList2(servicesUrl, PageCurrent, pagesize, keyword, userid_he) {

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
        data: "{PageCurrent:'" + PageCurrent + "',PageSize:'" + pagesize + "',keyword:'" + keyword + "',userid:'" + userid_he + "'}",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var strList = "";

            //获取当然用户id
            var userid = $.cookie('useid');

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
                    sowhpage_ul(PageCurrent, item.RecordCount, 0, keyword, userid_he);   //分页 0：全部，1：博文
                    return true;
                }
                else if (item.RecordCount == 1) //显示条数页数
                {
                    //绑定数据
                    $("#sowhpage_div").html("");    //去掉分页

                    return true;
                }

                //如果遍历到不存在节点就不构建
                if (item.is_id != undefined) {


                    var url = "";
                    var deleteHtml = "";
                    var contentUrl = "";

                    //判断自己或是他人
                    if (userid == item.userid) {

                        url = "/u";

                        contentUrl = "cont_" + item.is_id + "";

                        deleteHtml = "<a href=\"javascript:void(0);\" onclick=\"showBlogResult(" + item.is_id + ",'c'," + userid + ");\" class=\"Blue\">删除</a> | ";


                    } else {

                        url = "/u_" + item.userid;

                        contentUrl = "tcont_" + item.userid + "_" + item.is_id;

                    }
                
                
                    //构建数据
                    strList += "<div class=\"Centent\"><a href=\"" + url + "\"  ><img id=\"user" + item.is_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.is_id + ")\"  src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"头像\" class=\"L Img\" /></a>";
                    strList += "<div class=\"Txt R \" style=\" position:relative\"><p class=\"F14 G6 L26 ENG\"><a href=\"" + url + "\" id=\"uu" + item.is_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.is_id + ")\"  class=\"Blue\">" + item.nickname + "</a>" + ShowVerifyImg(item.memberlevel) + "："
                    + (unescape(item.io_content).replace(keyword, "<font color=\"red\">" + keyword + "</font>"))
                    + "</p><div id=\"div" + item.is_id + "\"></div>";

                    if ((item.is_spreadtype == 1 && item.is_isoriginal == 0) || (item.is_spreadtype == 0 && item.is_isoriginal == 0 && item.is_type == 1)) {
                        strList += "<div id=\"divContent" + item.is_id + "\"></div>";
                        GetOriginalInfo(item.io_id, item.is_id,keyword);
                    }
                    strList += " <div class=\"G9 Fa Info\"><span class=\"L\"><a href=\"" + contentUrl + "\" class=\"Blue\">" + item.intime + "</a> &nbsp;来自<a href=\"" + item.is_url + "\" onclick=\"return LoginDiv(16);\">" + item.is_name + "</a>" + (userid == item.userid ? "" : "  <a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"ShowPageReport(" + item.is_id + ",'" + item.nickname + "','" + item.face + "','" + item.userid + "')\" >举报</a>") + " </span><span class=\"R\"> " + deleteHtml + "<a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"checkForWard(" + item.userid + "," + item.is_id + "," + item.is_isoriginal + "," + (item.is_spreadnum == "" ? 0 : item.is_spreadnum) + ",'cc');\" >转发(" + (item.is_spreadnum == "" ? 0 : item.is_spreadnum) + ")</a> |  <a class=\"Blue\" id=\"replyCount" + item.is_id + "\" href=\"javascript:void(0);\" onclick=\"checkComment(0,'c" + item.is_id + "'," + item.is_id + "," + item.is_isoriginal + "," + (item.vic_commentnum == "" ? 0 : item.vic_commentnum) + ");\" >评论(" + (item.vic_commentnum == "" ? 0 : item.vic_commentnum) + ")</a></span></div> ";

                    strList += "<div id=\"c" + item.is_id + "\"></div>";
                    strList += " </div> ";
                    strList += " <Div class=\"Hr_1\"></Div> ";
                    strList += " <div class=\"Line_ilog\"></div> ";

                    if (item.is_haspic == 1) //有图片
                    {
                        GetPic(vServiceUrl + "ILogPic.asmx/GetPic", item.io_id, item.is_id);
                    }
                    strList += " </div>  ";
                }
            });

            $("#list_div").html(strList);
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


//校验搜索
function checkform() {
    if (!LoginDiv(16)) {
        return;
    }
    //上面搜索
    var element_p = document.getElementById("keyword_s");

    //保存userid

    var element_o = "";     //搜索框对象
    var strValue = "";      //值

    strValue = element_p.value;
    element_o = element_p;

    if (strValue == "请输入内容") {
        alert("请输入内容！");
        element_o.focus();
        return false;
    }
    if (strValue == "" || strValue == null) {
        alert("请输入内容！");
        element_o.focus();
        return false;
    }
    if (ignoreSpaces(strValue) == "") {
        alert("请输入内容！");
        element_o.focus();
        return false;
    }
    if (HTMLEncode(strValue) == "") {
        alert("请输入内容！");
        element_o.focus();
        return false;
    }
    if (removeHTMLTag(strValue) == "") {
        alert("请输入内容！");
        element_o.focus();
        return false;
    }

    var type_s = $("#ation_s").val();

    if (type_s == 1)       //博文
    {

        //获取数据
        //GetSearchList("" + vServiceUrl + "ILogOriginal.asmx/GetSearchList2_He", 1, 45, strValue, strUserId_O);

        GetSearchList2("" + vServiceUrl + "ILog_Spread.asmx/GetSerchaAllList_He", 1, 45, strValue, strUserId_A);

    }
    else                  //全部
    {

        GetSearchList2("" + vServiceUrl + "ILog_Spread.asmx/GetSerchaAllList_He", 1, 45, strValue, strUserId_A);
    }

    ListTyle_s($("#ation_s").val());
}



//分页控件（加载页面）
//PageCurrent：当然页码 x
//总页数
//ation：0全部，1博文
//搜索关键字
function sowhpage_ul(PageCurrent, RecordCount, ation, keyword, userid) {
    var sowhpage_div = $("#sowhpage_div");

    var strShowPage = "";

    //当然页码等于总也数就隐藏下一页按钮
    strShowPage += "<span class=\"R\" " + (PageCurrent == RecordCount ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"nextbtn\" onclick=\"return LoginDiv(16);\"><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_page.gif\" onclick=\"nextpage(" + PageCurrent + "," + RecordCount + "," + ation + ",'" + keyword + "','" + userid + "')\" alt=\"下一页\" /></span>";

    strShowPage += "<span class=\"R span\" style=\"position:relative\" ><a href=\"javascript:void(0);\"  id='selOption' class=\"Blue\" onclick=\"return LoginDiv(16);\">第" + PageCurrent + "页</a><img src=\"http://simg.instrument.com.cn/ilog/blue/images/SanJ.gif\" />";

    strShowPage += "<ul class=\"pageBox R Line Fa BrWh\" id=\"selOption_menu\" style=\" position:absolute; bottom:16px; left:15px;display:none;\" onmouseout=\"javascript:$('#selOption_menu').hide();\">";

    for (var i = RecordCount; i >= 1; i--) {
        if (ation == 0)      //全部
        {
            if (PageCurrent == i) {
                if (keyword != "") {
                    strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"GetSearchList2('" + vServiceUrl + "ILog_Spread.asmx/GetSerchaAllList_He'," + i + ",45,'" + keyword + "','" + userid + "')\"  >第<font color=\"red\">" + i + "</font>页</a></li>";
                }
                else {
                    strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"GetAllList('" + vServiceUrl + "ILog_Spread.asmx/GetAllList_He'," + i + ",45,'" + userid + "')\"  >第<font color=\"red\">" + i + "</font>页</a></li>";
                }
            }
            else {
                if (keyword != "") {
                    strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"GetSearchList2('" + vServiceUrl + "ILog_Spread.asmx/GetSerchaAllList_He'," + i + ",45,'" + keyword + "','" + userid + "')\"  >第" + i + "页</a></li>";
                }
                else {
                    strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"GetAllList('" + vServiceUrl + "ILog_Spread.asmx/GetAllList_He'," + i + ",45,'" + userid + "')\"  >第" + i + "页</a></li>";
                }
            }
        }
        else                //博文
        {
            if (PageCurrent == i) {
                if (keyword != "") {
                    strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"GetSearchList('" + vServiceUrl + "ILogOriginal.asmx/GetSearchList2_He'," + i + ",45,'" + keyword + "','" + userid + "')\"  >第<font color=\"red\">" + i + "</font>页</a></li>";
                }
                else {
                    strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"GetList('" + vServiceUrl + "ILogOriginal.asmx/GetList_He'," + i + ",45,'" + userid + "')\"  >第<font color=\"red\">" + i + "</font>页</a></li>";
                }
            }
            else {
                if (keyword != "") {
                    strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"GetSearchList('" + vServiceUrl + "ILogOriginal.asmx/GetSearchList2_He'," + i + ",45,'" + keyword + "','" + userid + "')\"  >第" + i + "页</a></li>";
                }
                else {
                    strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"GetList('" + vServiceUrl + "ILogOriginal.asmx/GetList_He'," + i + ",45,'" + userid + "')\"  >第" + i + "页</a></li>";
                }
            }
        }

    }

    strShowPage += "</ul>";
    strShowPage += "</span>";

    //当前页码小于1就隐藏上一页页码

    strShowPage += "<span class=\"R\" " + (PageCurrent <= 1 ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"upbtn\"  ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_pageT.gif\" onclick=\"uppage(" + PageCurrent + "," + RecordCount + "," + ation + ",'" + keyword + "','" + userid + "')\" alt=\"上一页\" /></span>";

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
function nextpage(PageCurrent, RecordCount, ation, keyword, userid) {
    var RecordCount_index = RecordCount;    //记录总页数

    //没翻过上一页
    if (pageindex_n <= 2) {
        if (RecordCount >= 1) {
            pageindex++;

            if (ation == 0)      //全部
            {
                if (keyword != "") {
                    GetSearchList2("" + vServiceUrl + "ILog_Spread.asmx/GetSerchaAllList_He", pageindex, 45, keyword, userid);
                }
                else {
                    GetAllList(vServiceUrl + "ILog_Spread.asmx/GetAllList_He", pageindex, 45, userid);
                }
            }
            else                //博文
            {
                if (keyword != "") {
                    GetSearchList("" + vServiceUrl + "ILogOriginal.asmx/GetSearchList2_He", pageindex, 45, keyword, userid);
                }
                else {
                    //重新绑定
                    GetList(vServiceUrl + "ILogOriginal.asmx/GetList_He", pageindex, 45, userid);
                }
            }
        }
    }
    else {
        if (RecordCount >= 1) {
            pageindex_n++;

            if (ation == 0) {
                if (keyword != "") {
                    GetSearchList2("" + vServiceUrl + "ILog_Spread.asmx/GetSerchaAllList_He", pageindex_n, 45, keyword, userid);
                }
                else {
                    GetAllList(vServiceUrl + "ILog_Spread.asmx/GetAllList_He", pageindex_n, 45, userid);
                }
            }
            else {
                if (keyword != "") {
                    GetSearchList("" + vServiceUrl + "ILogOriginal.asmx/GetSearchList2_He", pageindex_n, 45, keyword, userid);
                }
                else {
                    //重新绑定
                    GetList(vServiceUrl + "ILogOriginal.asmx/GetList_He", pageindex_n, 45, userid);
                }
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
//ation：0全部，1博文
//搜索关键字keyword
function uppage(PageCurrent, RecordCount, ation, keyword, userid) {
    var pageindex_n = PageCurrent;    //记录总页数

    if (pageindex == 1) {
        if (PageCurrent <= RecordCount) {
            pageindex_n--;

            if (ation == 0)  //全部
            {
                if (keyword != "") {
                    GetSearchList2("" + vServiceUrl + "ILog_Spread.asmx/GetSerchaAllList_He", pageindex_n, 45, keyword, userid);
                }
                else {
                    GetAllList(vServiceUrl + "ILog_Spread.asmx/GetAllList_He", pageindex_n, 45, userid);
                }
            }
            else {
                if (keyword != "") {
                    GetSearchList("" + vServiceUrl + "ILogOriginal.asmx/GetSearchList2_He", pageindex_n, 45, keyword, userid);
                }
                else {
                    //重新绑定
                    GetList(vServiceUrl + "ILogOriginal.asmx/GetList_He", pageindex_n, 45, userid);
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
                    GetSearchList2("" + vServiceUrl + "ILog_Spread.asmx/GetSerchaAllList_He", pageindex, 45, keyword, userid);
                }
                else {
                    GetAllList(vServiceUrl + "ILog_Spread.asmx/GetAllList_He", pageindex, 45, userid);
                }
            }
            else {
                if (keyword != "") {
                    GetSearchList("" + vServiceUrl + "ILogOriginal.asmx/GetSearchList2_He", pageindex, 45, keyword, userid);
                }
                else {
                    //重新绑定
                    GetList(vServiceUrl + "ILogOriginal.asmx/GetList_He", pageindex, 45, userid);
                }
            }
        }
    }

    //如果到第一页了就隐藏上一也
    if (pageindex_n == 1) {
        pageindex_n = 2;    //索引初始化
    }
}

//列表类型
function ListTyle(index) {
    //切换标签容器
    var Original_li = $("#Original_li");    //博文
    var all_il = $("#all_il");              //全部

    $("#ation_s").val(index);               //当前被选中的标记0：全部，1：博文

    //博文
    if (index == 1) {
        all_il.html("<a  href=\"javascript:void(0);\" onclick=\"ListTyle(0);\" >全部</a>");
        Original_li.html("<div class=\"top\"></div><div class=\"center\"><a href=\"javascript:void(0);\" onclick=\"ListTyle(1);\" class=\"Blue\"><strong>博文</strong></a></div>");

        //加载用户博文
        //GetList("" + vServiceUrl + "ILogOriginal.asmx/GetList_He", 1, 45, strUserId_O);
        GetAllList("" + vServiceUrl + "ILog_Spread.asmx/GetAllList_He", 1, 45, strUserId_A);
    }
    else //全部
    {
        Original_li.html("<a  href=\"javascript:void(0);\" onclick=\"ListTyle(1);\" >博文</a>");
        all_il.html("<div class=\"top\"></div><div class=\"center\"><a href=\"javascript:void(0);\" onclick=\"ListTyle(0);\" class=\"Blue\"><strong>全部</strong></a></div>");

        //加载用户博文
        GetAllList("" + vServiceUrl + "ILog_Spread.asmx/GetAllList_He", 1, 45, strUserId_A);
    }
}

//列表类型
function ListTyle_s(index) {
    //切换标签容器
    var Original_li = $("#Original_li");    //博文
    var all_il = $("#all_il");              //全部

    //博文
    if (index == 1) {
        all_il.html("<a  href=\"javascript:void(0);\" onclick=\"ListTyle(0);\" >全部</a>");
        Original_li.html("<div class=\"top\"></div><div class=\"center\"><a href=\"javascript:void(0);\" onclick=\"ListTyle(1);\" class=\"Blue\"><strong>博文</strong></a></div>");




    }
    else //全部
    {
        Original_li.html("<a  href=\"javascript:void(0);\" onclick=\"ListTyle(1);\" >博文</a>");
        all_il.html("<div class=\"top\"></div><div class=\"center\"><a href=\"javascript:void(0);\" onclick=\"ListTyle(0);\" class=\"Blue\"><strong>全部</strong></a></div>")



    }
}


//显示用户基本信息
function VipILogPersonalInfo(servicesUrl, otherId) {

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
        data: "{userid:'" + otherId + "',i:'" + rand + "'}",
        //成功           
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象 

            if (dataObj.UrlState == 1) {

                //获取当前用户id
                var userId = $.cookie('useid');

                //设置title
                ShowTitle(dataObj.nickname + "的iLog");

                var headInfo = "<img src=\"/images/face/big/" + dataObj.face + "\" title=\"" + dataObj.nickname + "\" alt=\"" + dataObj.nickname + "\" width=\"140\" height=\"140\" class=\"img\" id=\"face\" />";

                //用于关注
                $("#nikename").val("" + dataObj.nickname + "");

                $("#headInfo").html(headInfo);

                var personalInfo = "<a href=\"/u_" + otherId + "\" class=\"F14\"><strong>" + dataObj.nickname + "</strong></a>";

                personalInfo += ShowVerifyImg(dataObj.memberlevel);

                personalInfo += "<br><span class=\"Fa\"><a href=\"http://ig.instrument.com.cn/u_" + otherId + "\" class=\"Blue\">http://ig.instrument.com.cn/u_" + otherId + "</a></span><br>";

                if (dataObj.address != "未填写") {
                    personalInfo += "地区：<span class=\"blue\">" + dataObj.address + " </span>";
                }
                if (dataObj.school != "未填写") {
                    personalInfo += "学校：<span class=\"blue\">" + dataObj.school + "</span>";
                }
                personalInfo += "<br/>";

                $("#personalInfo").html(personalInfo);

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


//显示关注、微博、粉丝、勋章
function showCount(url, otherId) {

    $.ajax({
        url: "" + url + "",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{userid:" + otherId + ",i:" + rand + "}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            //获取当前用户id
            var userId = $.cookie('useid');

            var blog = "";
            var fan = "";
            var concern = "";

            if (userId == otherId) {
                concern = "/follow";
                fan = "/fans";
                blog = "/u";
            }
            else {
                concern = "/follow_" + otherId;
                fan = "/fans_" + otherId;
                blog = "/u_" + otherId;
            }

            if (dataObj.UrlState == 1) {

                var showCount = "<div class=\"box\">";
                showCount += "<strong class=\" Fw\"><a href=\"" + concern + "\">" + dataObj.concern + "</a></strong><br>";
                showCount += "<a href=\"" + concern + "\">关注</a></div>";
                showCount += "<div class=\"box\">";
                showCount += "<strong class=\" Fw\"><a href=\"" + fan + "\">" + dataObj.fan + "</a></strong><br>";
                showCount += "<a href=\"" + fan + "\">粉丝</a></div>";
                showCount += "<div class=\"box  box_no\">";
                showCount += "<strong class=\" Fw\"><a href=\"" + blog + "\">" + dataObj.blog + "</a></strong><br>";
                showCount += "<a href=\"" + blog + "\">微博</a></div>";

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


//得到右侧的信息
function GetRight(strUserId_A) {
    var userId = strUserId_A;
    $("#hrefConcern,#hrefConcernMore").attr("href", "/follow_" + userId);
    $("#hrefFans,#hrefFansMore").attr("href", "/fans_" + userId);
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
                    if (dataObj.State != 0) {
                        var nickname = item.nickname;
                        if (nickname.length > 6) {
                            nickname = nickname.substring(0, 6);
                        }
                        ulHtml += "<li><a href=\"/u_" + item.userid + "\"><img src=\"" + item.face + "\" alt=\""
                        + item.nickname + "\" /></a><br /><a href=\"/u_" + item.userid + "\" class=\"Blue\">" + nickname + "</a><br />" + item.date + "</li>";
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
                        if (nickname.length > 6) {
                            nickname = nickname.substring(0, 6);
                        }
                        ulHtml += "<li><a href=\"/u_" + item.userid + "\"><img src=\"" + item.face + "\" alt=\""
                        + item.nickname + "\" /></a><br /><a href=\"/u_" + item.userid + "\" class=\"Blue\">" + nickname + "</a><br />" + item.date + "</li>";
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
            var comment = dataObj.comment;
            if (comment.length > 31) {
                comment = comment.substring(0, 31) + "...";
            }
            $("#divContent").html(comment);
        }, error: function(result, status) {
            if (status == 'error') {
                alert(result.responseText);
            }
        },
        complete: function() {
        }
    });

}











