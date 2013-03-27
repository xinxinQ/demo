$(document).ready(function() {
    //加载Ilog站内导航
    funGetTopMenuService("" + vServiceUrl + "IlogTopMenu.asmx/ILogGetTopMenu", "{}", "");
    //加载Ilog用户导航
    funGetTopUserMenuService("" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenu", "{MenuLive:'0'}", "");

    //    var strKeyword = getParameter("keyword_s");    

    //接收参数
    var strKeyword = $("#keyword_h").val();

    strKeyword = unescape(strKeyword);

    $("#keyword_s").val(strKeyword);    //保持搜索框
    $("#keyword_s2").val(strKeyword);    //保持搜索框


    //获取数据
    GetSearchList("" + vServiceUrl + "ILog_Spread.asmx/GetSerchSpreadAllList", 1, 45, strKeyword);

    //随机抽取用户
    GetSearchPersonalInfo(vServiceUrl + "ILog_Spread.asmx/GetSearchPersonalInfo", strKeyword);

    //-------------------------只能提示部分-------------------------------

    //搜索只能提示（上搜索）
    $("#keyword_s").keyup(function() {
        //回车键搜提示无效
        if (!isEnterKey()) {
            searchtowho_s();
        }
    });

    //搜索只能提示（下搜索）
    $("#keyword_s2").keyup(function() {
        //回车键搜提示无效
        if (!isEnterKey()) {
            searchtowho_u();
        }
    });

    //-------------------------只能提示部分-------------------------------

    //获取回车事件
    getenterevent();

    //页面title
    ShowTitle(strKeyword + "的相关博文");
});




//根据搜索关键字随机显示用户
//keyword：搜索关键字
function GetSearchPersonalInfo(servicesUrl, keyword) {
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
        data: "{keyword:'" + keyword + "'}",
        //成功
        success: function(json, status) {
            var strList = "";

            //加i处理
            var strI = "";
            var strIPrompt = "";
            var strImg = "";

            //关注按钮
            var strAttention = "";

            var url = "";

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            //获取当然用户id
            var userid = $.cookie('useid');

            //循环获取值
            $.each(dataObj.List, function(idx, item) {
                if (idx == 0) {
                    if (item.State == "0") {
                        //alert("加载异常！");
                    }
                    else if (item.State == "2") {
                        return false;   //无数据跳出
                    }
                    else    //如果成功就返回循环里面做拼接，该“return”不会跳出循环
                    {
                        //匹配出的用户容器开始
                        strList += "<div class=\"Search_Tj\">";

                        return true;
                    }
                }

                //匹配用户
                if (item.userid != undefined) {

                    strI = ShowVerifyImg(item.vi_memberlevel);

                    //r如果是自己就不显示关注按钮
                    if (item.userid != userid) {
                        if (item.isuserconcernstate == "True")   //判断是否双项关注
                        {
                            strAttention = "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/concern.gif\" alt=\"互相关注\"  />";
                        }
                        else if (item.isfollow == "True")    //已经单项关注
                        {
                            strAttention = "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/concern_y.gif\" alt=\"已关注\"  />";
                        }
                        else    //未关注
                        {
                            strAttention = "<a href=\"javascript:void(0);\" onclick=\"ShowAddFollowTrueTaConcern_s(" + item.vi_id + "," + item.userid + ",'" + item.nickname + "')\" id=\"concern_a_" + item.vi_id + "\" ><img src=\"http://simg.instrument.com.cn/ilog/blue/images/concern_j.gif\" alt=\"加关注\"  /></a>";
                        }
                    }
                    else if (item.userid == userid) {
                        strAttention = "";
                    }


                    var contentUrl = "";

                    //判断自己或是他人
                    if (userid == item.userid) {
                        //伪静态
                        url = "u";

                        //伪静态
                        contentUrl = "cont_" + item.is_id + "";
                    }
                    else {
                        //伪静态地址
                        url = "u_" + item.userid;

                        //伪静态
                        contentUrl = "tcont_" + item.is_id + "_" + item.userid;
                    }

                    var nickname = item.nickname;
                    if (Getlength(nickname) > 16) {
                        nickname = substr(nickname, 16);
                    }

                    strList += "<div class=\"L L18 BoxWin\" style=\" position:relative\" >";    //操作容器
                    strList += "<a href=\"" + url + "\" >";
                    strList += " <img id=\"user" + item.vi_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.vi_id + ")\" src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"头像\" title=\"头像\" class=\"L img\"></a>";
                    strList += "<a href=\"" + url + "\"  id=\"uu" + item.vi_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.vi_id + ")\" class=\"Blue \">" + nickname + "</a>";
                    strList += strI;
                    strList += "<br><span class=\"G4\">粉丝" + item.vic_fannum + "人</span><br>";
                    strList += "<div id=\"cencernimg_" + item.vi_id + "\">" + strAttention + "</div></div>";

                }
            });

            //匹配出的用户容器结束
            //清楚浮动
            strList += "<div class=\"Cl\"></div>";
            strList += "</div>";

            //点击刷新页面
            strList += "<DIV class=\"Hr_20\"></DIV>";
            strList += "<div class=\"Search_Db Tc\"><a href=\"javascript:void(0);\" onclick=\"GetSearchList('" + vServiceUrl + "ILog_Spread.asmx/GetSerchSpreadAllList',1,45,'" + keyword + "');\" >点击查看最新博文</a></div>";
            strList += "<DIV class=\"Hr_20\"></DIV>";

            $("#Personal_s_div").html(strList); //追加回复站短

        },
        //出错调试
        error: function(result, status) {
            if (status == 'error') {
                // alert(result.responseText);
            }
        },
        complete: function() {
        }

    });


}




//全部
//servicesUrl：服务地址
//PageCurrent：当前页面
//pagesize：每页多少行数据
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

            //加i处理
            var strI = "";
            var strIPrompt = "";
            var strImg = "";


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
                        $("#RowsCount").html("找到0条结果");  //数据总数

                        //构建数据
                        strList += "<DIV class=\"Hr_20\"></DIV>";
                        strList += " <div class=\"G4\">";
                        if (keyword == "" || keyword == null) {
                            strList += " 抱歉，没有找到相关的结果<br>";
                        }
                        else {
                            strList += " 抱歉，没有找到“<span class=\"Red\">" + keyword + "</span>”相关的结果<br>";
                        }
                        strList += " <div class=\"Hr_10\"></div> ";
                        strList += " <p class=\"G9\"> ";
                        strList += " 建议：<br> ";
                        strList += " 请尽量输入常用词<br> ";
                        strList += " 请尽量使用简洁的关键词<br> ";
                        strList += " 可用空格将多个关键词分开</p></div> ";
                        strList += " <div class=\"Line_ilog\"></div>  ";
                        strList += " <DIV class=\"Hr_20\"></DIV> ";

                        $("#list_div").html(strList);

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

                if (item.RecordCount > 1)   //2页以上数据显示页数
                {
                    sowhpage_ul(PageCurrent, item.RecordCount, keyword);   //分页
                    $("#RowsCount").html("约找到" + item.RecordCount + "页结果"); //数据页数显示
                    return true;
                }
                else if (item.RowsCount > 0)           //一页数据显示条数
                {
                    $("#RowsCount").html("约找到" + item.RowsCount + "条数据");  //数据页数
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

                        //伪静态
                        url = "u";

                        //伪静态
                        contentUrl = "cont_" + item.is_id;

                        deleteHtml = "<a href=\"javascript:void(0);\" onclick=\"showBlogResult(" + item.is_id + ",'s'," + userid + ");\" class=\"Blue\">删除</a> | ";

                    }
                    else {

                        //伪静态
                        url = "u_" + item.userid;

                        //伪静态
                        contentUrl = "tcont_" + item.is_id + "_" + item.userid;

                    }


                    //构建数据
                    strList += "<div class=\"Centent\"><a href=\"" + url + "\"  ><img id=\"user" + item.is_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.is_id + ")\" src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"头像\" class=\"L Img\" /></a>";

                    //加i认证
                    strImg = ShowVerifyImg(item.vi_memberlevel);

                    //伪静态地址
                    strList += "<div class=\"Txt R \" style=\" position:relative\"><p class=\"F14 G6 L26 ENG\"><a href=\"" + url + "\" id=\"uu" + item.is_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.is_id + ")\"  class=\"Blue\">" + item.nickname + "</a>" + strImg + "：" + (unescape(item.io_content).replace(keyword, "<font color=\"red\">" + keyword + "</font>")) + "</p><div id=\"div" + item.is_id + "\"></div>";

                    //判断当前博文是不是转发，如果是转发就取原创
                    if ((item.is_spreadtype == 1 && item.is_isoriginal == 0) || (item.is_spreadtype == 0 && item.is_isoriginal == 0 && item.is_type == 1)) {
                        strList += "<div id=\"divContent" + item.is_id + "\"></div>";

                        GetOriginalInfo(item.io_id, item.is_id);
                    }

                    strList += " <div class=\"G9 Fa Info\"><span class=\"L\"><a href=\"" + contentUrl + "\" class=\"Blue\">" + item.intime + "</a> &nbsp;来自<a target=\"_blank\"  title=\"" + item.is_name + "\" href=\"" + item.is_url + "\" onclick=\"return LoginDiv(16);\">" + item.is_name + "</a>" + (userid == item.userid ? "" : "  <a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"ShowPageReport(" + item.is_id + ",'" + item.nickname + "','" + item.face + "','" + item.io_content + "'," + item.vi_memberlevel + ")\" >举报</a>") + " </span><span class=\"R\"> " + deleteHtml + "<a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"checkForWard(" + item.userid + "," + item.is_id + "," + item.is_isoriginal + "," + (item.is_spreadnum == "" ? 0 : item.is_spreadnum) + ",'s');\" >转发(" + (item.is_spreadnum == "" ? 0 : item.is_spreadnum) + ")</a> |  <a class=\"Blue\" id=\"replyCount" + item.is_id + "\" href=\"javascript:void(0);\" onclick=\"checkComment(0,'c" + item.is_id + "'," + item.is_id + "," + item.is_isoriginal + "," + (item.vic_commentnum == "" ? 0 : item.vic_commentnum) + ");\" >评论(" + (item.vic_commentnum == "" ? 0 : item.vic_commentnum) + ")</a></span></div> ";
                    strList += "<div id=\"c" + item.is_id + "\"></div>";
                    strList += " </div> ";
                    strList += " <Div class=\"Hr_1\"></Div> ";
                    strList += " <div class=\"Line_ilog\"></div> ";

                    if (item.is_haspic == 1) //有图片
                    {
                        GetPic(vServiceUrl + "ILogPic.asmx/GetPic", item.io_id, item.is_id);
                    }

                    //strList += "<input id=\"count"+item.is_id+"\" name=\"screenHidden\" type=\"hidden\" value=\"\"  />";
                    strList += " </div>  ";
                }
            });

            //保存搜索关键字
            $("#keyword_h").val(keyword);

            $("#list_div").html(strList);
            scroll(0, 0);  //翻页后需要回到顶部

            //保存最终搜索结果

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











//分页控件（加载页面）
function sowhpage_ul(PageCurrent, RecordCount, keyword) {
    var sowhpage_div = $("#sowhpage_div");

    var strShowPage = "";

    //当然页码等于总也数就隐藏下一页按钮
    strShowPage += "<span class=\"R\" " + (PageCurrent == RecordCount ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"nextbtn\" ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_page.gif\" onclick=\"nextpage(" + PageCurrent + "," + RecordCount + ",'" + keyword + "')\" alt=\"下一页\" /></span>";

    strShowPage += "<span class=\"R span\" style=\"position:relative\" ><a href=\"javascript:void(0);\"  id='selOption' class=\"Blue\">第" + PageCurrent + "页</a><img src=\"http://simg.instrument.com.cn/ilog/blue/images/SanJ.gif\" />";

    strShowPage += "<ul class=\"pageBox R Line Fa BrWh\" id=\"selOption_menu\" style=\" position:absolute; bottom:16px; left:15px;display:none;\">";

    for (var i = RecordCount; i >= 1; i--) {
        if (PageCurrent == i) {
            strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"GetSearchList('" + vServiceUrl + "ILogOriginal.asmx/GetSrechList'," + i + ",45,'" + keyword + "')\"  >第<font color=\"red\">" + i + "</font>页</a></li>";
        }
        else {
            strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"GetSearchList('" + vServiceUrl + "ILogOriginal.asmx/GetSrechList'," + i + ",45,'" + keyword + "')\"  >第" + i + "页</a></li>";
        }
    }

    strShowPage += "</ul>";
    strShowPage += "</span>";

    //当前页码小于1就隐藏上一页页码
    strShowPage += "<span class=\"R\" " + (PageCurrent <= 1 ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"upbtn\"  ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_pageT.gif\" onclick=\"uppage(" + PageCurrent + "," + RecordCount + ",'" + keyword + "')\" alt=\"上一页\" /></span>";

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

////显示或隐藏层
//function MenuDivShow(showdiv)
//{
//    $('#' + showdiv + '_menu').mouseover(function () { $(this).show(); });
//    $('#' + showdiv + '_menu').mouseout(function () { $(this).hide(); });
//}

//记录当前页码
var pageindex = 1;

//下一页处理
//PageCurrent：当前页码
//RecordCount：总页数
function nextpage(PageCurrent, RecordCount, keyword) {
    var RecordCount_index = RecordCount;    //记录总页数

    //没翻过上一页
    if (pageindex_n <= 2) {
        if (RecordCount >= 1) {
            pageindex++;

            //重新绑定
            GetSearchList("" + vServiceUrl + "ILog_Spread.asmx/GetSerchSpreadAllList", pageindex, 45, keyword);
        }
    }
    else {
        if (RecordCount >= 1) {
            pageindex_n++;

            //重新绑定
            GetSearchList("" + vServiceUrl + "ILog_Spread.asmx/GetSerchSpreadAllList", pageindex_n, 45, keyword);
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
function uppage(PageCurrent, RecordCount, keyword) {
    var pageindex_n = PageCurrent;    //记录总页数

    if (pageindex == 1) {
        if (PageCurrent <= RecordCount) {
            pageindex_n--;

            //重新绑定
            GetSearchList("" + vServiceUrl + "ILog_Spread.asmx/GetSerchSpreadAllList", pageindex_n, 45, keyword);
        }
    }
    else {
        if (PageCurrent <= RecordCount) {
            pageindex--;

            //重新绑定
            GetSearchList("" + vServiceUrl + "ILog_Spread.asmx/GetSerchSpreadAllList", pageindex, 45, keyword);
        }
    }

    //如果到第一页了就隐藏上一也
    if (pageindex_n == 1) {
        pageindex_n = 2;    //索引初始化
    }
}

//校验搜索
//index：0上面的搜索，1下面的搜索
function checkform(index) {

    if (!LoginDiv(16)) {
        return;
    }

    //上面搜索
    var element_p = document.getElementById("keyword_s");

    //下面搜索
    var element_p2 = document.getElementById("keyword_s2");

    var element_o = "";     //搜索框对象
    var strValue = "";      //值

    if (index == 0) {
        strValue = element_p.value;
        element_o = element_p;
    }
    else if (index == 1) {
        strValue = element_p2.value;
        element_o = element_p2;
    }
    else {
        strValue = element_p.value;
        element_o = element_p;
    }

    $("#keyword_s").val(strValue);    //保持搜索框（上）
    $("#keyword_s2").val(strValue);    //保持搜索框（下）

    if (strValue == "请输入搜索关键字") {
        showTipe("请输入搜索关键字！");
        element_o.focus();
        return false;
    }
    if (strValue == "" || strValue == null) {
        showTipe("请输入搜索关键字！");
        element_o.focus();
        return false;
    }
    if (ignoreSpaces(strValue) == "") {
        showTipe("请输入搜索关键字！");
        element_o.focus();
        return false;
    }
    if (HTMLEncode(strValue) == "") {
        showTipe("请输入搜索关键字！");
        element_o.focus();
        return false;
    }
    if (removeHTMLTag(strValue) == "") {
        showTipe("请输入搜索关键字！");
        element_o.focus();
        return false;
    }

    //随机抽取用户
    GetSearchPersonalInfo(vServiceUrl + "ILog_Spread.asmx/GetSearchPersonalInfo", strValue);

    
    //获取数据
    GetSearchList("" + vServiceUrl + "ILog_Spread.asmx/GetSerchSpreadAllList", 1, 45, strValue);

    //隐藏搜索只能提示框
    if (index == 0)          //上
    {
        $("#GetSearchTowho_Menu").hide();
    }
    else if (index == 1)     //下
    {
        $("#GetSearchTowho_Menu2").hide();
    }
    else                    //上
    {
        $("#GetSearchTowho_Menu").hide();
    }
}



//只能提示收件人（页面搜索用户）
function searchtowho_s() {
    //收件人昵称
    var towho = $("#keyword_s");

    var strTowhoValue = towho.val();


    //校验收信人
    if (strTowhoValue != "") {
        if (ignoreSpaces(strTowhoValue) == "") {
            showTipe("请输入搜索关键字！", 0);
            towho.focus();
            return false;
        }
        if (HTMLEncode(strTowhoValue) == "") {
            showTipe("请输入搜索关键字！", 0);
            towho.focus();
            return false;
        }
        if (removeHTMLTag(strTowhoValue) == "") {
            showTipe("请输入搜索关键字！", 0);
            towho.focus();
            return false;
        }
    }

    //开始发送
    $.ajax({
        //请求WebService Url         
        url: "" + vServiceUrl + "ILogOriginal.asmx/GetSearchOriginalInfo",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        cache: false,
        //请求参数              
        data: "{Originaltitle:'" + strTowhoValue + "'}",
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
                        //                        alert("加载错误！");
                    }
                    else if (item.State == "2")  //无数据不显示分页控件
                    {
                        //                        showTipe("未找到收件人！",0);

                        return false;   //无数据不再往下执行
                    }
                    else {
                        return true;
                    }
                }
                else {
                    if (item.originalinfo != undefined && unescape(item.originalinfo) != "") {
                        strList += "<li style=\"cursor:hand\" onclick=\"Getnickname_Box_s2('" + item.originalinfo + "')\" ><a href=\"javascript:void(0)\" id=\"hrSearchTowhPeple" + idx + "\" >" + item.originalinfo + "</a></li>";

                        return true;
                    }
                }
            });

            //加载下拉菜单
            $("#GetSearchTowho_Menu").html(strList);

            //有数据显示下拉框
            if (strList != "") {
                GetSearchTowhUpList_s();
            }
            else    //没有数据隐藏掉提示框
            {
                $("#GetSearchTowho_Menu").hide();
            }

        },
        //出错调试         
        error: function(x, e) {

            //alert("加载异常");

            //            window.location.href = "http://c.instrument.com.cn/art/ilog/404.asp";
        },
        //执行成功后自动执行           
        complete: function(x) {

        }
    });

}


//只能提示部分（站短搜索用）


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
	    //'left':offset.left + -208, //左右定位
	    //'top':offset.top+20,       //上下定位
	    'position': 'absolute'
	}).show();

}

//控制隐藏显示
function MenuDivShow_s2(showdiv) {
    $('#' + showdiv + 'Towho_Menu').mouseover(function() { $(this).show(); });
    $('#' + showdiv + 'Towho_Menu').mouseout(function() { $(this).hide(); });
    $('#' + showdiv).mouseout(function() { $('#' + showdiv + 'Towho_Menu').hide(); });
}

//把选中的收件人放入框中
//towho：收件人
function Getnickname_Box_s2(towho) {
    $("#keyword_s").val(towho);
    $("#GetSearchTowho_Menu").hide();
}


//下搜索智能提示定位
//只能提示收件人（页面搜索用户）
function searchtowho_u() {
    //收件人昵称
    var towho = $("#keyword_s2");

    var strTowhoValue = towho.val();


    //校验收信人
    if (strTowhoValue != "") {
        if (ignoreSpaces(strTowhoValue) == "") {
            showTipe("请输入搜索关键字！", 0);
            towho.focus();
            return false;
        }
        if (HTMLEncode(strTowhoValue) == "") {
            showTipe("请输入搜索关键字！", 0);
            towho.focus();
            return false;
        }
        if (removeHTMLTag(strTowhoValue) == "") {
            showTipe("请输入搜索关键字！", 0);
            towho.focus();
            return false;
        }
    }


    //开始发送
    $.ajax({
        //请求WebService Url         
        url: "" + vServiceUrl + "ILogOriginal.asmx/GetSearchOriginalInfo",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        cache: false,
        //请求参数              
        data: "{Originaltitle:'" + strTowhoValue + "'}",
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
                        //                        alert("加载错误！");
                    }
                    else if (item.State == "2")  //无数据不显示分页控件
                    {
                        //                        showTipe("未找到收件人！",0);

                        return false;   //无数据不再往下执行
                    }
                    else {
                        return true;
                    }
                }
                else {
                    if (item.originalinfo != undefined) {
                        strList += "<li style=\"cursor:hand\" onclick=\"Getnickname_Box_u2('" + item.originalinfo + "')\" ><a href=\"javascript:void(0)\" id=\"hrSearchTowhPeple" + idx + "\" >" + item.originalinfo + "</a></li>";

                        return true;
                    }
                }
            });

            //加载下拉菜单
            $("#GetSearchTowho_Menu2").html(strList);

            //有数据显示下拉框
            if (strList != "") {
                GetSearchTowhUpList_u();
            }
            else    //没有数据隐藏掉提示框
            {
                $("#GetSearchTowho_Menu2").hide();
            }

        },
        //出错调试         
        error: function(x, e) {

            //alert("加载异常");

            //            window.location.href = "http://c.instrument.com.cn/art/ilog/404.asp";
        },
        //执行成功后自动执行           
        complete: function(x) {

        }
    });

}


//只能提示部分（站短搜索用）


//搜索下拉
function GetSearchTowhUpList_u() {
    //收件人文本框值
    var txtSearchValue = $("#keyword_s2").val();

    //框内数据不为空就开始定位
    if (txtSearchValue != null && txtSearchValue != "") {
        setMenuPositionsSearch_u2("GetSearch");
        MenuDivShow_u2("GetSearch");
    }
    else {
        $("#GetSearchTowho_Menu2").hide();
    }

}

//下拉框定位
function setMenuPositionsSearch_u2(ShowID) {
    var offset = $('#keyword_s2').offset();
    var divheight = $('#keyword_s2').innerHeight();

    var leftpadd = 0;

    $('#' + ShowID + 'Towho_Menu2').css
	({
	    //'left':offset.left + -208, //左右定位
	    //'top':offset.top+20,       //上下定位
	    'position': 'absolute'
	}).show();
}

//控制隐藏显示
function MenuDivShow_u2(showdiv) {
    $('#' + showdiv + 'Towho_Menu2').mouseover(function() { $(this).show(); });
    $('#' + showdiv + 'Towho_Menu2').mouseout(function() { $(this).hide(); });
    $('#' + showdiv).mouseout(function() { $('#' + showdiv + 'Towho_Menu2').hide(); });
}

//把选中的收件人放入框中
//towho：收件人
function Getnickname_Box_u2(towho) {
    $("#keyword_s2").val(towho);
    $("#GetSearchTowho_Menu2").hide();
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

                $("#bnt_s" + num).click();
                return false;
            }
        });
    });
}

//是否是按了回车建true是，false否
function isEnterKey() {
    var event = arguments.callee.caller.arguments[0] || window.event;

    if (event.keyCode == 13) {
        return true;
    }
    else {
        return false;
    }
}

