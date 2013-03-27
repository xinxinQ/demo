

$(document).ready(function() {
    //加载Ilog站内导航
    funGetTopMenuService("" + vServiceUrl + "IlogTopMenu.asmx/ILogGetTopMenu", "{}", "");
    //加载Ilog用户导航
    funGetTopUserMenuService("" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenu", "{MenuLive:'0'}", "");
    //加载ilog左侧菜单
    funGetleftMenuService("" + vServiceUrl + "ILogWebLeftMenu.asmx/ILogGetHomeLeftMneu", "{MenuLive:'1'}", "");

    //获取当然用户id
    var userid = $.cookie('useid');

    //获取用户头像信息
    VipILogHome("" + vServiceUrl + "VipIlogUser.asmx/ILogGetUserInfoById", userid);


    //键盘上、下、enter键事件
    changeKeyBoard();

    //设置title
    ShowTitle("我的首页");

    //加载全部内容（个人首页专用）
    GetAllList("" + vServiceUrl + "ILog_Spread.asmx/GetAllList", 1, 45, 0);

});

//全部
//servicesUrl：服务地址
//PageCurrent：当前页面
//pagesize：每页多少行数据
function GetAllList(servicesUrl, PageCurrent, pagesize, type) {
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
        data: "{PageCurrent:'" + PageCurrent + "',PageSize:'" + pagesize + "',type:'" + type + "'}",
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
                    sowhpage_ul(PageCurrent, item.RecordCount, 0, "");   //分页
                    return true;
                }
                else if (item.RecordCount == 1) //显示条数页数
                {
                    $("#sowhpage_div").html("");    //去掉分页

                    return true;
                }

                //如果遍历到不存在节点就不构建
                if (item.is_id != undefined) {

                    //评论类型判断
                    var pinlun = item.is_isoriginal == 0 ? 2 : 1;

                    var url = "";
                    var deleteHtml = "";
                    var contentUrl = "";

                    //判断自己或是他人
                    if (userid == item.userid) {

                        url = "/u";

                        contentUrl = "cont_" + item.is_id + "";

                        deleteHtml = "<a href=\"javascript:void(0);\" onclick=\"showBlogResult(" + item.is_id + ",'a'," + userid + ");\" class=\"Blue\">删除</a> | ";

                    } else {

                        url = "/u_" + item.userid;

                        contentUrl = "tcont_" + item.userid + "_" + item.is_id;

                    }

                    var imgMemberLevel = ShowVerifyImg(item.memberlevel);


                    //构建数据
                    strList += "<div class=\"Centent\"><a href=\"" + url + "\" ><img src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"头像\" class=\"L Img\" id=\"user" + item.is_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.is_id + ")\"/></a>";
                    strList += "<div class=\"Txt R \" style=\" position:relative\"><p class=\"F14 G6 L26 ENG\"><a href=\"" + url + "\" id=\"uu" + item.is_id + "\" onmouseover=\"UserInfoShowOver(this," + item.userid + "," + item.is_id + ")\"  class=\"Blue\">" + item.nickname + "</a>" + imgMemberLevel + "：" + unescape(item.io_content) + "</p><div id=\"div" + item.is_id + "\"></div>";

                    if ((item.is_spreadtype == 1 && item.is_isoriginal == 0) || (item.is_spreadtype == 0 && item.is_isoriginal == 0 && item.is_type == 1)) {
                        strList += "<div id=\"divContent" + item.is_id + "\"></div>";
                        GetOriginalInfo(item.io_id, item.is_id);
                    }
                    strList += " <Div class=\"Hr_10\"></Div> ";
                    strList += " <div class=\"G9 Fa Info\"><span class=\"L\"><a href=\"" + contentUrl + "\" class=\"Blue\">" + item.intime + "</a> &nbsp;来自<a href=\"" + item.is_url + "\">" + item.is_name + "</a>" + (userid == item.userid ? "" : "  <a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"ShowPageReport(" + item.is_id + ",'" + item.nickname + "','" + item.face + "','" + item.io_content + "'," + item.userid + "," +
                    item.memberlevel+")\" >举报</a>") + " </span><span class=\"R\"> " + deleteHtml + "<a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"checkForWard(" + item.userid + "," + item.is_id + "," + item.is_isoriginal + "," + (item.is_spreadnum == "" ? 0 : item.is_spreadnum) + ",'a');\" >转发(" + (item.is_spreadnum == "" ? 0 : item.is_spreadnum) + ")</a> |  <a class=\"Blue\" id=\"replyCount" + item.is_id + "\" href=\"javascript:void(0);\" onclick=\"checkComment(0,'c" + item.is_id + "'," + item.is_id + "," + item.is_isoriginal + "," + (item.vic_commentnum == "" ? 0 : item.vic_commentnum) + ");\" >评论(" + (item.vic_commentnum == "" ? 0 : item.vic_commentnum) + ")</a></span></div> ";

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




//博文
//servicesUrl：服务地址
//PageCurrent：当前页面
//pagesize：每页多少行数据
function GetList(servicesUrl, PageCurrent, pagesize) {
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
        data: "{PageCurrent:'" + PageCurrent + "',PageSize:'" + pagesize + "'}",
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
                    sowhpage_ul(PageCurrent, item.RecordCount, 1, "");   //分页
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

                        deleteHtml = "<a href=\"javascript:void(0);\" onclick=\"showBlogResult(" + item.is_id + ",'aa'," + userid + ");\" class=\"Blue\">删除</a> | ";

                        contentUrl = "cont_" + item.is_id;

                    } else {

                    contentUrl = "tcont_" + item.userid + "_" + item.is_id;

                    }

                    //构建数据
                    strList += "<div class=\"Centent\"><a href=\"" + url + "\" id=\"user" + item.is_id + "\" onMouseOver=\"\"  ><img src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"" + item.nickname + "\" class=\"L Img\" /></a>";
                    strList += "<div class=\"Txt R \" style=\" position:relative\"><p class=\"F14 G6 L26 ENG\"><a href=\"" + url + "\" id=\"uu" + item.is_id + "\" onMouseOver=\"\"  class=\"Blue\">" + item.nickname + "</a>" + ShowVerifyImg(item.memberlevel) + "：" + unescape(item.io_content) + "</p><div id=\"div" + item.is_id + "\"></div>";

                    if ((item.is_spreadtype == 1 && item.is_isoriginal == 0) || (item.is_spreadtype == 0 && item.is_isoriginal == 0 && item.is_type == 1)) {
                        strList += "<div id=\"divContent" + item.is_id + "\"></div>";
                        GetOriginalInfo(item.io_id, item.is_id);
                    }
                    strList += " <div class=\"G9 Fa Info\"><span class=\"L\"><a href=\"" + contentUrl + "\" class=\"Blue\">" + item.intime + "</a> &nbsp;来自<a href=\"" + item.is_url + "\">" + item.is_name + "</a>" + (userid == item.userid ? "" : "  <a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"ShowPageReport(" + item.is_id + ",'" + item.nickname + "','" + item.face + "','" + item.io_content + "'," + item.userid + ","
                    +item.memberlevel+")\" >举报</a>") + " </span><span class=\"R\"> " + deleteHtml + "<a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"checkForWard(" + item.userid + "," + item.is_id + "," + item.is_isoriginal + "," + (item.is_spreadnum == "" ? 0 : item.is_spreadnum) + ",'aa');\" >转发(" + (item.is_spreadnum == "" ? 0 : item.is_spreadnum) + ")</a> |  <a class=\"Blue\" id=\"replyCount" + item.is_id + "\" href=\"javascript:void(0);\" onclick=\"checkComment(0,'c" + item.is_id + "'," + item.is_id + "," + item.is_isoriginal + "," + (item.vic_commentnum == "" ? 0 : item.vic_commentnum) + ");\" >评论(" + (item.vic_commentnum == "" ? 0 : item.vic_commentnum) + ")</a></span></div> ";
                    strList += "<div style=\"display:none;z-index:6666;\" id=\"h" + item.is_id + "\" onMouseOver=\"mousermoveInfo(" + item.is_id + ");\" onmouseout=\"mouseroutInfo(" + item.is_id + ");\" ></div>";
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



//博文
//servicesUrl：服务地址
//PageCurrent：当前页面
//pagesize：每页多少行数据
//keyword：搜索关键字
function GetSearchList(servicesUrl, PageCurrent, pagesize, keyword) {
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
        data: "{PageCurrent:'" + PageCurrent + "',PageSize:'" + pagesize + "',keyword:'" + keyword + "'}",
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
                    sowhpage_ul(PageCurrent, item.RecordCount, 1, keyword);   //分页 0：全部，1：博文
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

                        deleteHtml = "<a href=\"javascript:void(0);\" onclick=\"showBlogResult(" + item.is_id + ",'aa'," + userid + ");\" class=\"Blue\">删除</a> | ";

                        contentUrl = "cont_" + item.is_id;

                    } else {

                        contentUrl = "tcont_"+item.userid+"_" + item.is_id;

                    }

                    //构建数据
                    strList += "<div class=\"Centent\"><a href=\"javascript:void(0);\" id=\"user" + item.io_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.io_id + ")\"  ><img src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"" + item.nickname + "\" class=\"L Img\"/></a>";
                    strList += "<div class=\"Txt R \" style=\" position:relative\"><p class=\"F14 G6 L26\"><a href=\"javascript:void(0);\" id=\"user" + item.io_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.io_id + ")\" class=\"Blue\">" + item.nickname + "</a>" + ShowVerifyImg(item.memberlevel) + "：" + item.is_content + "</p>";
                    strList += " <div class=\"Hr_10\"></div>";
                    strList += "<a href=\"#\"><img src=\"http://ww1.sinaimg.cn/thumbnail/7080ab5bjw1dsy430ubf4j.jpg\" /></a> ";
                    strList += " <Div class=\"Hr_10\"></Div>";
                    strList += " <div class=\"G9 Fa Info\"><span class=\"L\"><a href=\"#\" class=\"Blue\">" + item.intime + "</a> &nbsp;来自<a href=\"" + item.is_url + "\">" + item.is_name + "</a> </span><span class=\"R\"> <a class=\"Blue\" href=\"#\">转发(" + (item.io_spreadnum == "" ? 0 : item.io_spreadnum) + ")</a> |  <a class=\"Blue\" href=\"#\">评论(" + (item.vic_commentnum == "" ? 0 : item.vic_commentnum) + ")</a> | <a class=\"Blue\" onclick=\"ShowPageReport(" + item.io_id + ",'" + item.nickname + "','" + item.face + "','" + escape(item.is_content) + "',"+item.userid+","+item.memberlevel+")\" href=\"javascript:void(0);\" >" + (userid == item.userid ? "" : "举报") + "</a></span></div> ";
                    strList += "<div style=\"display:none;\" id=\"h" + item.io_id + "\" onMouseOver=\"mousermoveInfo(" + item.io_id + ");\" onmouseout=\"mouseroutInfo(" + item.io_id + ");\" ></div>";
                    strList += " </div> ";
                    strList += " <Div class=\"Hr_1\"></Div> ";
                    strList += " <div class=\"Line_ilog\"></div> ";
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
function GetSearchList2(servicesUrl, PageCurrent, pagesize, keyword) {
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
        data: "{PageCurrent:'" + PageCurrent + "',PageSize:'" + pagesize + "',keyword:'" + keyword + "'}",
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
                    sowhpage_ul(PageCurrent, item.RecordCount, 0, keyword);   //分页 0：全部，1：博文
                    return true;
                }
                else if (item.RecordCount == 1) //显示条数页数
                {
                    $("#sowhpage_div").html("");    //去掉分页

                    return true;
                }

                //如果遍历到不存在节点就不构建
                if (item.io_id != undefined) {
                    //构建数据
                    strList += "<div class=\"Centent\"><a href=\"javascript:void(0);\" id=\"user" + item.io_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.io_id + ")\"  ><img src=\"http://ww4.sinaimg.cn/thumb50/6f013cf3jw1dqmj6n79d2j.jpg\" alt=\"头像\" class=\"L Img\" /></a>";
                    strList += "<div class=\"Txt R \" style=\" position:relative\"><p class=\"F14 G6 L26\"><a href=\"javascript:void(0);\" id=\"user" + item.io_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.io_id + ")\" class=\"Blue\">" + item.nickname + "</a>" + ShowVerifyImg(item.memberlevel) + "：" + item.is_content + "</p>";
                    strList += " <div class=\"Hr_10\"></div>";
                    strList += "<a href=\"#\"><img src=\"http://ww1.sinaimg.cn/thumbnail/7080ab5bjw1dsy430ubf4j.jpg\" /></a> ";
                    strList += " <Div class=\"Hr_10\"></Div>";
                    strList += " <div class=\"G9 Fa Info\"><span class=\"L\"><a href=\"#\" class=\"Blue\">" + item.intime + "</a> &nbsp;来自<a href=\"" + item.is_url + "\">" + item.is_name + "</a> </span><span class=\"R\"> <a class=\"Blue\" href=\"#\">转发(" + (item.is_spreadnum == "" ? 0 : item.is_spreadnum) + ")</a> |  <a class=\"Blue\" href=\"#\">评论(" + (item.vic_commentnum == "" ? 0 : item.vic_commentnum) + ")</a> | <a class=\"Blue\" onclick=\"ShowPageReport(" + item.is_id + ",'" + item.nickname + "','" + item.face + "','" + escape(item.is_content) + "'," + item.userid + "," + item.memberlevel + ")\" href=\"javascript:void(0);\" >" + (userid == item.userid ? "" : "举报") + "</a></span></div> ";
                    strList += "<div style=\"display:none;\" id=\"h" + item.io_id + "\" onMouseOver=\"mousermoveInfo(" + item.io_id + ");\" onmouseout=\"mouseroutInfo(" + item.io_id + ");\" ></div>";
                    strList += " </div> ";
                    strList += " <Div class=\"Hr_1\"></Div> ";
                    strList += " <div class=\"Line_ilog\"></div> ";
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



//校验搜索
function checkform() {
    //上面搜索
    var element_p = document.getElementById("keyword_s");

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
        GetSearchList("" + vServiceUrl + "ILogOriginal.asmx/GetSearchList2", 1, 45, strValue, $("#id_").val());
    }
    else                  //全部
    {
        GetSearchList2("" + vServiceUrl + "ILog_Spread.asmx/GetSerchaAllList", 1, 45, strValue, $("#id_").val());
    }

    ListTyle_s($("#ation_s").val());
}



//分页控件（加载页面）
//PageCurrent：当前页码
//总页数
//ation：0全部，1博文
//搜索关键字
function sowhpage_ul(PageCurrent, RecordCount, ation, keyword) {
    var sowhpage_div = $("#sowhpage_div");

    var strShowPage = "";

    //当然页码等于总也数就隐藏下一页按钮
    strShowPage += "<span class=\"R\" " + (PageCurrent == RecordCount ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"nextbtn\" ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_page.gif\" onclick=\"nextpage(" + PageCurrent + "," + RecordCount + "," + ation + ",'" + keyword + "')\" alt=\"下一页\" /></span>";

    strShowPage += "<span class=\"R span\" style=\"position:relative\" ><a href=\"javascript:void(0);\"  id='selOption' onclick=\"GetAllList('" + vServiceUrl + "ILog_Spread.asmx/GetAllList'," + PageCurrent + ",45,0);\" class=\"Blue\">第" + PageCurrent + "页</a><img src=\"http://simg.instrument.com.cn/ilog/blue/images/SanJ.gif\" />";

    strShowPage += "<ul class=\"pageBox R Line Fa BrWh\" id=\"selOption_menu\" style=\" position:absolute; bottom:16px; left:15px;display:none;z-index:5;\" onmouseout=\"javascript:$('#selOption_menu').hide();\" >";

    for (var i = RecordCount; i >= 1; i--) {
        if (ation == 0)      //全部
        {
            if (PageCurrent == i) {
                if (keyword != "" || keyword != null) {
                    //                    strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"GetSearchList2('" + vServiceUrl + "ILog_Spread.asmx/GetSerchaAllList'," + i + ",45,'" + keyword + "')\"  >第<font color=\"red\">" + i + "</font>页</a></li>";

                    strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"GetAllList('" + vServiceUrl + "ILog_Spread.asmx/GetAllList'," + i + ",45,0)\"  >第" + i + "页</a></li>";

                }
                else {
                    strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"GetAllList('" + vServiceUrl + "ILog_Spread.asmx/GetAllList'," + i + ",45,0)\"  >第<font color=\"red\">" + i + "</font>页</a></li>";
                }
            }
            else {
                if (keyword != "" || keyword != null) {
                    //                    strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"GetSearchList2('" + vServiceUrl + "ILog_Spread.asmx/GetSerchaAllList'," + i + ",45,'" + keyword + "')\"  >第" + i + "页</a></li>";


                    strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"GetAllList('" + vServiceUrl + "ILog_Spread.asmx/GetAllList'," + i + ",45,0)\"  >第" + i + "页</a></li>";

                }
                else {
                    strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"GetAllList('" + vServiceUrl + "ILog_Spread.asmx/GetAllList'," + i + ",45,0)\"  >第" + i + "页</a></li>";
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
    //    .mouseout(function() {
    //        $('#' + this.id + '_menu').hide();
    //    });

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

            if (ation == 0)      //全部
            {
                if (keyword != "") {
                    GetSearchList2("" + vServiceUrl + "ILog_Spread.asmx/GetSerchaAllList", pageindex, 45, keyword);
                }
                else {
                    GetAllList(vServiceUrl + "ILog_Spread.asmx/GetAllList", pageindex, 45, 0);
                }
            }
            else                //博文
            {
                if (keyword != "") {
                    GetSearchList("" + vServiceUrl + "ILogOriginal.asmx/GetSearchList2", pageindex, 45, keyword);
                }
                else {
                    //重新绑定
                    GetList(vServiceUrl + "ILogOriginal.asmx/GetList", pageindex, 45);
                }
            }
        }
    }
    else {
        if (RecordCount >= 1) {
            pageindex_n++;

            if (ation == 0) {
                if (keyword != "") {
                    GetSearchList2("" + vServiceUrl + "ILog_Spread.asmx/GetSerchaAllList", pageindex_n, 45, keyword);
                }
                else {
                    GetAllList(vServiceUrl + "ILog_Spread.asmx/GetAllList", pageindex_n, 45, 0);
                }
            }
            else {
                if (keyword != "") {
                    GetSearchList("" + vServiceUrl + "ILogOriginal.asmx/GetSearchList2", pageindex_n, 45, keyword);
                }
                else {
                    //重新绑定
                    GetList(vServiceUrl + "ILogOriginal.asmx/GetList", pageindex_n, 45);
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
function uppage(PageCurrent, RecordCount, ation, keyword) {
    var pageindex_n = PageCurrent;    //记录总页数

    if (pageindex == 1) {
        if (PageCurrent <= RecordCount) {
            pageindex_n--;

            if (ation == 0)  //全部
            {
                if (keyword != "") {
                    GetSearchList2("" + vServiceUrl + "ILog_Spread.asmx/GetSerchaAllList", pageindex_n, 45, keyword);
                }
                else {
                    GetAllList(vServiceUrl + "ILog_Spread.asmx/GetAllList", pageindex_n, 45, 0);
                }
            }
            else {
                if (keyword != "") {
                    GetSearchList("" + vServiceUrl + "ILogOriginal.asmx/GetSearchList2", pageindex_n, 45, keyword);
                }
                else {
                    //重新绑定
                    GetList(vServiceUrl + "ILogOriginal.asmx/GetList", pageindex_n, 45);
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
                    GetSearchList2("" + vServiceUrl + "ILog_Spread.asmx/GetSerchaAllList", pageindex, 45, keyword);
                }
                else {
                    GetAllList(vServiceUrl + "ILog_Spread.asmx/GetAllList", pageindex, 45, 0);
                }
            }
            else {
                if (keyword != "") {
                    GetSearchList("" + vServiceUrl + "ILogOriginal.asmx/GetSearchList2", pageindex, 45, keyword);
                }
                else {
                    //重新绑定
                    GetList(vServiceUrl + "ILogOriginal.asmx/GetList", pageindex, 45);
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
        //GetList("" + vServiceUrl + "ILogOriginal.asmx/GetList", 1, 45);
        GetAllList("" + vServiceUrl + "ILog_Spread.asmx/GetAllList", 1, 45, 0);

    }
    else //全部
    {
        Original_li.html("<a  href=\"javascript:void(0);\" onclick=\"ListTyle(1);\" >博文</a>");
        all_il.html("<div class=\"top\"></div><div class=\"center\"><a href=\"javascript:void(0);\" onclick=\"ListTyle(0);\" class=\"Blue\"><strong>全部</strong></a></div>");

        //加载用户博文
        GetAllList("" + vServiceUrl + "ILog_Spread.asmx/GetAllList", 1, 45, 0);
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




var atContent = "";

var rand = Math.random(Math.random() * 10000)

//得到坐标
function getCursortPosition(ctrl) {

    var CaretPos = 0;   // IE Support  

    if (document.selection) {

        ctrl.focus();
        var Sel = document.selection.createRange();
        Sel.moveStart('character', -ctrl.value.length);
        CaretPos = Sel.text.length;

    }

    // Firefox support 

    else if (ctrl.selectionStart || ctrl.selectionStart == '0')

        CaretPos = ctrl.selectionStart;

    return (CaretPos);

}


//调用服务插入图片信息
function fileUploadInsertData(guid, picName, picType) {

    $.ajax({

        //请求WebService Url         
        url: "" + vServiceUrl + "VipIlogUser.asmx/ILogSharePicture",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        //缓存
        cache: false,
        //参数
        data: "{ guid: '" + guid + "', picName:'" + picName + "', picType:'" + picType + "'}",
        //成功
        success: function(data, status) {

            var dataObj = eval("(" + data.d + ")"); //转换为json对象                    

            if (dataObj.state == '1') {

                var ret = $("#textarea").val();

                var d = ret.indexOf("分享图片");

                if (ret.indexOf("分享图片") < 0) {

                    $("#textarea").val(ret + "分享图片");

                }

                //提示
                showTipe("上传成功.", 1);
                //字数大小判断
                checkSendBlog("textarea");
                //焦点
                myfocus("textarea");
                //清空图片框
                $("#uploadpic").val("");

                //更新图片是否存在标示(0:没有/上传失败 1:成功)
                $("#hasPicHidden").val("1");


            } else {

                $("#guidHidden").val("");
                // alert("图片上传成功！");                       
                showTipe("上传失败.", 0);
                //$("#textarea").focus();

                myfocus("textarea");

                $("#uploadpic").val("");

            }


        },
        error: function(data, status, e) {
            // alert(e);
        }
    }
        )

}


//提交上传图片请求
function imgUpload() {

    var imgUrl = $("#uploadpic").val();

    var guid = $("#guidHidden").val();

    var hasPic = $("#hasPicHidden").val();

    //未上传成功过
    if (hasPic == 1) {

        $("#guidHidden").val("");
        showTipe("图片已上传成功,不支持多次上传.", 0);
        myfocus("textarea");
        $("#uploadpic").val("");


    } else {

        var index1 = imgUrl.lastIndexOf('.');

        if (index1 > -1) {

            var img = imgUrl.substr(index1).toLowerCase();

            if ((img == ".jpg" || img == ".jpeg" || img == ".gif" || img == ".bmp" || img == ".png")) {

                $.ajaxFileUpload
        (
            {
                url: '../Ajax/AjaxUploadPicture.aspx',
                secureuri: false,
                fileElementId: 'uploadpic',
                dataType: 'json',
                data: { guid: guid }, //传递guid
                success: function(data, status) {

                    //var dataObj = eval("(" + data.data + ")"); //转换为json对象

                    if (data.state == '1') {

                        fileUploadInsertData(data.guid, data.picName, data.picType);

                    } else {

                        $("#guidHidden").val("");

                        showTipe("上传失败.", 0);

                        myfocus("textarea");

                        $("#uploadpic").val("");

                        $("#hasPicHidden").val("1");

                    }
                },
                error: function(data, status, e) {
                    // alert(e);
                }
            }
        )


            } else {


                showTipe("亲~只能上传jpg或gif格式的照片哦~", 0);

            }

        }




    }

}

//发布博文
function sendBlog() {
    if (!LoginDiv(16)) {
        return;
    }

    var content = $("#textarea").val();


    content = content.replace(/[\r\n]/mg, " "); //替换换行

    content = content.replace(/^ +| +$/g, '').replace(/ +/g, ' '); //处理多了空格(替换为一个空格)

    content = escape(content); //加编码

    if (content != "") {

        //发布内容就删除cookie存的内容
        //$.cookie('blogsContent', '', { expires: -1 }); // delete cookie  

        sendContent("" + vServiceUrl + "VipIlogUser.asmx/ILogAddOriginalInfo", content);
        return true;

    } else {

        showTipe("发布的内容不能为空！", 0);
        //alert("请填写要发布的内容!");
        return false;

    }

}



//博文发布
function sendContent(servicesUrl, content) {

    var guid = "";
    if ($("#hasPicHidden").val() == "1") {
        var guid = $("#guidHidden").val();
    }

    var ip = $("#ipHidden").val();
    //获取当前用户id
    var userId = $.cookie('useid');

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
        data: "{userId:'" + userId + "',mark:'" + guid + "',isid:'" + 0 + "',ip:'" + ip + "',content:'" + content + "',type:0,i:'" + rand + "'}",

        //成功
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象
            // alert(html);

            if (dataObj.state == 1) {

                $("#textarea").val("");
                $("#textarea").focus();
                showTipe("发布成功.", 1);

                //更新图片是否存在标示(0:没有/上传失败 1:成功)
                $("#hasPicHidden").val("0");
                //刷新当前用户信息
                GetAllList("" + vServiceUrl + "ILog_Spread.asmx/GetAllList", 1, 45, 0);


            } else if (dataObj.state == 2) {

                $("#textarea").focus();
                showTipe("内容相同请重试！", 0);

            }
            else {

                $("#textarea").focus();
                showTipe("发布失败.", 0);

            }

            $("#prompt").html("还能输入<font class=\"publish_num\">140</font>字");

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

//视屏图片生成短地址
function uploadScreen() {

    //视屏源地址
    var url = $("#screen").val();

    if ((url == null || url != "") && url.toLowerCase().indexOf("http://") == 0) {

        $("#screenHidden").val(url); //存储视屏源地址

        //短地址生成    
        createScreenUrl(url);

        //清空视屏文本框内容
        $("#screen").val("");

    } else {

        //alert("你输入的链接地址无法识别！");

        showTipe("你输入的链接地址无法识别!", 0);


    }
}


//短地址生成
function createScreenUrl(url) {

    $.ajax({
        url: "" + vServiceUrl + "/VipIlogUser.asmx/ILogGetShortUrl",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{url:'" + url + "',type:1,i:'" + rand + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象 

            var ret = $("#textarea").val();

            if (ret.indexOf("" + dataObj.shortUrl + "") < 0) {

                $("#textarea").val(ret + "" + dataObj.shortUrl + " ");

            }

            checkSendBlog("textarea");

            myfocus("textarea");

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


//选择视频
function changeScreen() {

    //图片层、表情层隐藏
    $("#picId").hide();
    $("#picId").html("");
    $("#expressioId").hide();
    $("#expressioId").html("");
    var screen = $("#screenId").html();

    if (screen == null || screen == "") {

        var screenshow = ShowScreen();
        $("#showee").after(screenshow);

    }
    else {

        $("#screenId").hide();
        $("#screenId").html("");

    }

}

//选择图片
function changePicture() {

    //视频层、表情层隐藏
    $("#screenId").hide();
    $("#screenId").html("");
    $("#expressioId").hide();
    $("#expressioId").html("");

    var pic = $("#picId").html();

    if (pic == null || pic == "") {

        var picShow = ShowPicture();
        $("#showee").after(picShow);

    } else {

        $("#picId").hide();
        $("#picId").html("");

    }

}

//计算输入内容的像素值
function strLeng(str) {

    if (str.indexOf('\n') > 0) {

        str = str.substring(0, str.indexOf('\n'));

    }

    var numCount = 0, caCount = 0, spaceCount = 0, cnCount = 0, otherCount = 0;

    //首先来区分数字
    var res = str.match(/\d/g);
    if (res != null) {
        numCount = ((res + '').split(',')).length;
        //将所有的数字替换掉,便于下面的计算
        str = str.replace(/\d+/g, '');
    }

    //区分字母
    res = str.match(/[a-zA-Z]/g);
    if (res != null) {
        caCount = ((res + '').split(',')).length;
        //将所有的字母替换掉,便于下面的计算
        str = str.replace(/[a-zA-Z]/g, '');
    }

    //区分中文
    res = str.match(/[^\x00-\xff]/g);
    if (res != null) {
        cnCount = ((res + '').split(',')).length;
        str = str.replace(/[^\x00-\xff]/g, '');
    }

    //区分空格
    res = str.match(/\s/g);
    if (res != null) {
        spaceCount = ((res + '').split(',')).length;
        str = str.replace(/\s/g, '');
    }

    //其他
    otherCount = str.length;

    //计算像素值
    var ret = cnCount * 13 + caCount * 8 + numCount * 8 + spaceCount * 8 + otherCount * 10;

    return ret;

    //alert(numCount+"|"+caCount+"|"+spaceCount+"|"+cnCount+"|"+otherCount+"|");

}

//悬浮框的定位
function setPosition(objId, isId) {
    var offset = $('#' + objId).offset();
    var divheight = $('#' + objId).innerHeight();
    var leftpadd = 0


    //	$('#h'+isId).css({
    //		'left':-20+"px",
    //		'top':-145+"px",
    //		'position':'absolute'
    //	}).show();

    //$("#h"+isId).css({position:"absolute",left:""+-20+"px",top:""+-145+"px"}).show();//修改pop层的css属性   

}

function hide() {

    $("#showContent").hide();

}

function show() {

    $("#showContent").show();

}

//@粉丝信息
function checkFan(userId, nickName) {

    $.ajax({
        url: "" + vServiceUrl + "/VipIlogUser.asmx/ILogGetfanInfoByUserId",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{userId:'" + userId + "',nickname:'" + nickName + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            // var TopContent="<ul id=\"flttishi\" class=\"WindowMenu  WindowW Line_blue L30\">";

            var TopContent = "";
            $.each(dataObj.fans, function(idx, item) {

                if (idx == 0) {
                    if (item.UrlState == "0") {
                        TopContent += TopContent + "<li>请轻敲空格完成输入</li>";
                    }
                    else {

                        TopContent = TopContent + "<li>选择最近@的人</li>";

                    }

                } else {

                    TopContent = TopContent + "<li>" + item.nickname + "</li>";

                }
            });
            $("#flttishi").html(TopContent);
            atContent = TopContent;
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

//转发或是评论的结果
function showResult(result) {

    var ShowTable = "<div class=\"WindowWark280\">";
    ShowTable += "<div class=\"WindowBox Tc\">";
    ShowTable += "<div class=\" Tc F14  WindowSak\">";
    ShowTable += "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/ok.gif\" class=\"L\" />";
    ShowTable += result;
    ShowTable += "</div>";
    ShowTable += "<div class=\"Hr_10\"></div>";
    ShowTable += "<div class=\"Hr_10\"></div>";
    ShowTable += "</div><BR />";
    ShowTable += "</div>";
    return ShowTable;

}

//显示图片
function ShowPicture() {
    var result = "<div class=\"WindowWark350\" id=\"picId\" style=\"z-index:0;position:absolute;display:block;z-index:0;top:25px;left:45px;\">";
    result += "<div class=\"WindowTil G4 \">";
    result += "<a href=\"javascript:void(0);\">";
    result += "<img class=\" R Img\" src=\"http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif\" alt=\"关闭\" onclick=\"javascript:$('#picId').hide();\" />";
    result += "</a>";
    result += "<span class=\"Span L\">";
    result += "<a href=\"#\" class=\"Blue\">本地上传</a>";
    result += "</span>";
    result += "<SPAN class=\"Span1 L\">";
    result += "<a href=\"#\"class=\"Blue\"></a></SPAN></div>";
    result += "<div class=\"Hr_10\"></div>";
    result += "<p class=\"G6 Tc\">仅支持JPG、GIF、JPEG、BMP、PNG格式，且文件不大于4M</p>";
    result += "<div class=\" WindowBox\" style=\"background:url('http://simg.instrument.com.cn/ilog/blue/images/add.gif') center no-repeat; padding-top:0px; \"><div class=\" Cl\"></div>";
    result += "<div class=\"Tc\" style=\"height:40px;\">";

    result += "<input style=\"background: none repeat scroll 0 0 transparent;border: medium none;cursor: pointer;height: 30px;float:left;opacity: 0;margin:15px 0 0 90px;width: 60px;filter:alpha(opacity=0);\"type=\"file\" name=\"uploadpic\" id=\"uploadpic\" size=\"5\" onchange=\"imgUpload();\" />";
    result += "</div>";
    result += "<div class=\"Hr_20\"></div>";
    result += "<div class=\"WindowSanWT\">&nbsp;</div>";
    result += "</div></div>";
    return result;

}

//显示视屏
function ShowScreen() {

    var result = "<div class=\"WindowWark350\" id=\"screenId\" style=\"position:absolute;display:block;z-index:2;left:110px;top:25px;\">";
    result += "<div class=\"WindowTil G4 \">";
    result += "<a href=\"javascript:void(0);\">";
    result += "<img class=\" R Img\" src=\"http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif\" alt=\"关闭\" onclick=\"javascript:$('#screenId').hide();\"/>";
    result += "</a>";
    result += "<span class=\"Span L blue\">视频链接</span>";
    result += "</div>";
    result += "<div class=\" WindowBox\">";
    result += "<div class=\" Cl G6\">";
    result += "<p class=\"L18\">目前已支持优酷、土豆、酷6等网站的视频播放页链接。</p>";
    result += " <div class=\"Hr_10\"></div>";
    result += "<div><input name=\"screen\" id=\"screen\" type=\"text\" class=\"L WindowInput\" /> ";
    result += " <div class=\"WinBtn  L \"><span>";
    result += "<input name=\"确定\" type=\"button\" id=\"确定\" value=\"确定\" onclick=\"uploadScreen();\" />";
    result += " <input id=\"screenHidden\" name=\"screenHidden\" type=\"hidden\" value=\"\"  />";
    result += "</span></div></div>";
    result += " <div class=\"Hr_20\"></div>";
    result += " <div class=\"WindowSanWT\">&nbsp;</div>";
    result += "</div></div></div>";
    return result;

}

var txtindex = 0;

//获取光标前的内容
var prevText = "";

//获取光标后的内容
var nextText = "";
//光标位置前的最后一个字符
var prevLastChar = "";
//光标前最后一个@位置
var lastatIndex = -1;
//截取最后一个@到光标的内容
var lastattofocus = "";
//是否可进行智能搜索
var isMatch = false;

//光标前最后一个@之前的内容
var prevTextBeforeat = "";

function clickTr(currTrIndex) {
    //样式背景色控制
    var prevTrIndex = $("#prevTrIndex").val();

    if (currTrIndex > -1) {
        $("#li_" + currTrIndex).addClass("WindowBG");
    }

    $("#li_" + 0).removeClass("WindowBG");
    $("#li_" + prevTrIndex).removeClass("WindowBG");
    $("#prevTrIndex").val(currTrIndex); // alert(prevTrIndex);     
    $("#nuname").val(currTrIndex);


}

//记录li总数
var trSize;

function changeKeyBoard() {

    $("#textarea").click(function() {
        txtindex = $("#textarea").getCurPos();
        prevText = $("#textarea").val().substring(0, txtindex);
        nextText = $("#textarea").val().substring(txtindex, $("#textarea").val().length);
        prevLastChar = $("#textarea").val().substring(txtindex - 1, txtindex);
        lastatIndex = prevText.lastIndexOf("@");
        if (lastatIndex >= 0) {
            if (prevLastChar == "@") {
                lastattofocus = "";
            }
            else {
                lastattofocus = prevText.substring(lastatIndex + 1, prevText.length);
            }
            //遍历截取出的内容，如果其中有不是数字字母下划线中文的，不进行智能搜索
            var reg = "^[a-zA-Z0-9_\u4E00-\u9FA5\uf900-\ufa2d]+$";
            if (lastattofocus.match(reg) || lastattofocus == "") {
                //进行智能搜索
                isMatch = true;
                prevTextBeforeat = $("#textarea").val().substring(0, lastatIndex);
            }
            else {
                isMatch = false;
            }
        }

        if (isMatch && lastatIndex >= 0) {

            show(document.getElementById("textarea"));

            $("#flttishi").show();

            GetAtUsreList(lastattofocus);

            trSize = $("#flttishi li").size(); //datagrid中li的数量


            $("#flttishi li").mouseover(function() {//鼠标滑过
                $(this).addClass("WindowBG");

            }).mouseout(function() { //鼠标滑出
                $(this).removeClass("WindowBG");

            })
                   .each(function(i) { //初始化 id 和 index 属性
                       $(this).attr("id", "li_" + i).attr("index", i);
                   })
                    .click(function() { //鼠标单击
                        var index = $(this).attr("index");
                        if (index != 0) {
                            clickTr($(this).attr("index"));
                            $("#textarea").val(prevTextBeforeat + "@" + $(this).text() + " " + nextText);
                            $("#flttishi").hide();
                        }
                    })

        }
        else {
            $("#flttishi").hide();
            $("#flttishi").html("");
        }

    });

    $("#textarea").keyup(function(evt) {
        var content = $("#textarea").val();

        var contentlen = Getlength(content);

        var font_count = Math.floor((280 - contentlen) / 2);

        var num = Math.abs(font_count);



        if (font_count < 0) {

            $("#prompt").html("已经超过<font class=\"publish_num\">" + num + "</font>字");
            $("#btnImg").attr('disabled', 'disabled');
            $("#btnImg").attr("src", "http://simg.instrument.com.cn/ilog/blue/images/btn_fbH.gif");
            $("#textarea").focus();
            return;

        } else {

            $("#prompt").html("还能输入<font class=\"publish_num\">" + num + "</font>字");
            $("#btnImg").removeAttr('disabled');
            $("#btnImg").attr("src", "http://simg.instrument.com.cn/ilog/blue/images/btn_fb.gif");
            $("#textarea").focus();

        }

        txtindex = $("#textarea").getCurPos();
        prevText = $("#textarea").val().substring(0, txtindex);
        nextText = $("#textarea").val().substring(txtindex, $("#textarea").val().length);
        prevLastChar = $("#textarea").val().substring(txtindex - 1, txtindex);
        lastatIndex = prevText.lastIndexOf("@");

        //键盘键值
        var keyCode = window.event ? evt.keyCode : evt.which;
        //文本框内容
        var textValue = $.trim($("#textarea").val());

        if (lastatIndex >= 0) {
            if (prevLastChar == "@") {
                lastattofocus = "";
            }
            else {
                lastattofocus = prevText.substring(lastatIndex + 1, prevText.length);
            }
            //遍历截取出的内容，如果其中有不是数字字母下划线中文的，不进行智能搜索
            var reg = "^[a-zA-Z0-9_\u4E00-\u9FA5\uf900-\ufa2d]+$";
            if (lastattofocus.match(reg) || lastattofocus == "") {
                //进行智能搜索
                isMatch = true;
                prevTextBeforeat = $("#textarea").val().substring(0, lastatIndex);
            }
            else {
                isMatch = false;
            }
        } else {

            $("#flttishi").hide();
            $("#flttishi").html("");

        }

        if (!isMatch) {
            $("#flttishi").hide();
            $("#flttishi").html("");
        }

        trSize = $("#flttishi li").size(); //datagrid中li的数量

        if (textValue != "" && keyCode != 38 && keyCode != 40 && keyCode != 13) {

            if (isMatch && lastatIndex >= 0) {

                show(document.getElementById("textarea"));

                $("#flttishi").show();

                GetAtUsreList(lastattofocus);

                $("#prevTrIndex").val("-1"); //默认-1

                $("#flttishi li").mouseover(function() {//鼠标滑过
                    $(this).addClass("WindowBG");

                }).mouseout(function() { //鼠标滑出
                    $(this).removeClass("WindowBG");

                })
                   .each(function(i) { //初始化 id 和 index 属性
                       $(this).attr("id", "li_" + i).attr("index", i);
                   })
                    .click(function() { //鼠标单击
                        var index = $(this).attr("index");
                        if (index != 0) {
                            $("#textarea").val(prevTextBeforeat + "@" + $(this).text() + " " + nextText);
                            $("#flttishi").hide();
                        }
                    })

            }
            else {
                $("#flttishi").hide();
                $("#flttishi").html("");
            }
        }
        else if (textValue != "" && keyCode == 38 && $("#flttishi").html() != "")//向上箭头
        {
            $("#flttishi li.WindowBG").prev().addClass("WindowBG");
            $("#flttishi li.WindowBG").next().removeClass("WindowBG");
        }
        else if (textValue != "" && keyCode == 40 && $("#flttishi").html() != "")//向下箭头
        {
            $("#flttishi li.WindowBG").next().addClass("WindowBG");
            $("#flttishi li.WindowBG").prev().removeClass("WindowBG");
        }

        else if (textValue != "" && keyCode == 13 && $("#flttishi").html() != "")//回车
        {
            var selectedUser = $("#flttishi  li.WindowBG").html()
            if (selectedUser != "选择最近@的人或直接输入") {
                var textval = prevTextBeforeat + "@" + $("#flttishi  li.WindowBG").html() + " " + nextText;
                textval = textval.replace(/[\r\n]/g, "");
                $("#textarea").val(textval);
                $("#flttishi").hide();
            }
            else {
                textValue = textValue.replace(/[\r\n]/g, "");
                $("#textarea").val(textValue);
            }
        }

        /*新添加 结束*/
    });

}

//得到@用户智能搜索列表
function GetAtUsreList(nickname) {
    ajaxurl = vServiceUrl + "VipILog.asmx/GetAtUserList"
    $.ajax({
        type: "POST",
        url: "" + ajaxurl + "",
        cache: false,
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        data: "{nickname:'" + nickname + "'}",
        success: function(data, status) {
            //获取服务器的值        
            var dataObj = eval("(" + data.d + ")"); //转换为json对象
            $("#flttishi").html("");
            $("#flttishi").append("<li class=\"WindowBG\" style=\"cursor:pointer\">选择最近@的人或直接输入</li>");
            //循环获取值
            $.each(dataObj.AtUserList, function(idx, item) {
                if (idx != 0) {
                    $("#flttishi").append("<li style=\"cursor:pointer\">" + item.nickname + "</li>");
                }
            });

            $("#flttishi li").mouseover(function() {//鼠标滑过
                $(this).addClass("WindowBG");

            }).mouseout(function() { //鼠标滑出
                $(this).removeClass("WindowBG");

            })
                   .each(function(i) { //初始化 id 和 index 属性
                       $(this).attr("id", "li_" + i).attr("index", i);
                   })
                    .click(function() { //鼠标单击
                        var index = $(this).attr("index");
                        if (index != 0) {
                            clickTr($(this).attr("index"));
                            $("#textarea").val(prevTextBeforeat + "@" + $(this).text() + " " + nextText);
                            $("#flttishi").hide();
                        }
                    })
        }, error: function(result, status) {
            if (status == 'error') {
                //alert(result.responseText);
            }
        },
        complete: function() {
        }
    });


}


function show(elem) {
    var p = textareaCursorPosition.getInputPositon(elem);
    var s = document.getElementById('flttishi');
    var leftmore = getPos(document.getElementById("divleft")).x;
    var topmore = getPos(document.getElementById("divleft")).y;
    s.style.top = p.bottom - topmore + 'px';
    s.style.left = p.left - leftmore + 'px';
    s.style.display = 'inherit';

}










