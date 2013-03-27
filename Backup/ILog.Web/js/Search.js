
$(document).ready(function() {
    //加载Ilog站内导航
    funGetTopMenuService("" + vServiceUrl + "IlogTopMenu.asmx/ILogGetTopMenu", "{}", "");
    //加载Ilog用户导航
    funGetTopUserMenuService("" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenu", "{MenuLive:'0'}", "");

    //接收参数
    var strKeyword = $("#keyword_h").val();

    //存储搜索的关键字
    strKeyword = unescape(strKeyword);

    $("#keyword_s").val(strKeyword);    //保持搜索框（上）

    $("#keyword_s2").val(strKeyword);    //保持搜索框（下）

    //获取数据
    GetSearchList("" + vServiceUrl + "VipILog.asmx/GetSrechList", 1, 45, strKeyword);

    //搜索只能提示（上搜索）
    $("#keyword_s").keyup(function() {
        //回车键搜提示无效
        if (!isEnterKey()) {
            searchtowho_s();

            //记录当前正在操作的文本框
            $("#as").val(0);
        }

        //上下键处理
        funListBegin_Up(event);

    });

    //搜索只能提示（下搜索）
    $("#keyword_s2").keyup(function() {
        //回车键搜提示无效
        if (!isEnterKey()) {
            searchtowho_u();

            //记录当前正在操作的文本框
            $("#as").val(1);
        }

        //上下键处理
        funListBegin_Up(event);
    });

    //获取回车事件
    getenterevent();

    //页面title
    ShowTitle(strKeyword + "的相关用户");

});


//搜索列表
//servicesUrl：服务地址
//PageCurrent：当前页面
//pagesize：每页多少行数据
//keyword：关键字
function GetSearchList(servicesUrl, PageCurrent, pagesize, keyword) {
    $("#keyword_s2").val(keyword);    //保持搜索框（下）

    $.ajax
     ({
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
             var strUrl = "";
             var strMamlImage = "";

             //关注按钮
             var strAttention = "";
             //头像性别
             var strSex = "";
             //头像alt
             var strSexAlt = "";

             //加i处理
             var strI = "";
             var strIPrompt = "";
             var strImg = "";

             //头像与昵称跳转
             var strPersonUlr = "";

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
                 //来源id（1：ILog）
                 var is_id = item.is_id;
                 //评论来源：ILot
                 if (is_id == 1) {
                     strUrl = "http://www.instrument.com.cn/ilog/";
                 }

                 //如果遍历到不存在节点就不构建
                 if (item.vi_id != undefined) {
                     //是否在线
                     //不在线
                     if (item.IsOnline == "False") {
                         strMamlImage = "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_mailN.gif\" alt=\"站短\"  />";
                     }
                     else //在线
                     {
                         strMamlImage = "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_mail.gif\" alt=\"站短\"  />"; ;
                     }

                     //构建数据
                     strList += "<div class=\"Concern_Txt\" style=\" position:relative\">";

                     var contentUrl = "";

                     if (item.userid == userid)    //是当前用户
                     {
                         //伪静态
                         strPersonUlr = "u";
                         contentUrl = "cont_" + item.is_id;
                     }
                     else //他人主页
                     {
                         //伪静态
                         strPersonUlr = "u_" + item.userid;
                         contentUrl = "tcont_" + item.userid + "_" + item.is_id;
                     }
                     strList += " <div class=\"pic L\"><a href=\"" + strPersonUlr + "\" ><img id=\"user" + item.vi_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.vi_id + ")\" src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"头像\" class=\"img\" /></a><a href=\"javascript:void(0);\" onclick=\"showdialog_u('" + item.nickname + "')\" >" + strMamlImage + "</a></div>";
                     //判断性别头像
                     if (item.sex == "male") {
                         strSex = "men.gif";
                         strSexAlt = "男";
                     }
                     else {
                         strSex = "women.jpg";
                         strSexAlt = "女";
                     }

                     //伪静态
                     strI = ShowVerifyImg(item.vi_memberlevel);
                     strList += " <div class=\"info L\"  ><a href=\"" + strPersonUlr + "\" id=\"uu" + item.vi_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.vi_id + ")\"  class=\"Blue F14\">" + (item.nickname.replace(keyword, "<font color=\"red\">" + keyword + "</font>")) + "</a>" + strI + "<span class=\" L19  G4\"> <img src=\"http://simg.instrument.com.cn/ilog/blue/images/" + strSex + "\" title=\"" + strSexAlt + "\" alt=\"" + strSexAlt + "\" /> " + (item.PR_Name == item.CI_Name ? item.CI_Name : item.PR_Name + " " + item.CI_Name) + "</span><br />";
                     strList += " <div class=\"Fa \"><a class=\" Blue\" href=\"" + strPersonUlr + "\">" + escape(strPersonUlr) + "</a></div> ";

                     //伪静态地址
                     strList += " <div class=\" L19  G4\">关注 <b><a href=\"follow_" + item.userid + "\" class=\"Blue\">" + item.vic_concernnum + "</a></b> <span class=\"G9\"> | </span> 粉丝 <b><a href=\"fans_" + item.userid + "\" class=\"Blue\">" + item.vic_fannum + "</a></b> <span class=\"G9\"> | </span> 微博 <b><a href=\"u_" + item.userid + "\" class=\"Blue\">" + item.vic_ilognum + "</a></b></div> ";

                     if (item.is_id != 0) {
                         //伪静态地址
                         strList += " <p class=\"txt\"><a href=\"" + contentUrl + "\">" + unescape(item.is_content) + (item.intime != "" ? "（" + item.intime + "）" : "") + "</a></p> ";
                     }

                     strList += " </div> ";

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

                     strList += " <div class=\"btn R L23 G4 Tr\" id=\"cencernimg_" + item.vi_id + "\">" + strAttention + "<br /> ";
                     strList += " <br /> ";
                     strList += " </div>  ";
                     strList += " <div class=\" Hr_1\"></div> ";
                     strList += " </div> ";
                     strList += " <div class=\"Line_ilog\"></div> ";
                 }
             });
             $("#list_div").html(strList);
             scroll(0, 0);  //翻页后需要回到顶部
         },
         //出错调试         
         error: function(x, e) {

             //alert("加载异常");

             //        window.location.href = "http://c.instrument.com.cn/art/ilog/404.asp";
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
            strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"GetSearchList('" + vServiceUrl + "VipILog.asmx/GetSrechList'," + i + ",45,'" + keyword + "')\"  >第<font color=\"red\">" + i + "</font>页</a></li>";
        }
        else {
            strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"GetSearchList('" + vServiceUrl + "VipILog.asmx/GetSrechList'," + i + ",45,'" + keyword + "')\"  >第" + i + "页</a></li>";
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

//显示或隐藏层
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
            GetSearchList("" + vServiceUrl + "VipILog.asmx/GetSrechList", pageindex, 45, keyword);
        }
    }
    else {
        if (RecordCount >= 1) {
            pageindex_n++;

            //重新绑定
            GetSearchList("" + vServiceUrl + "VipILog.asmx/GetSrechList", pageindex_n, 45, keyword);
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
            GetSearchList("" + vServiceUrl + "VipILog.asmx/GetSrechList", pageindex_n, 45, keyword);
        }
    }
    else {
        if (PageCurrent <= RecordCount) {
            pageindex--;

            //重新绑定
            GetSearchList("" + vServiceUrl + "VipILog.asmx/GetSrechList", pageindex, 45, keyword);
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

    if (strValue == "请输入昵称") {
        showTipe("请输入昵称！", 0);
        element_o.focus();
        return false;
    }
    if (strValue == "" || strValue == null) {
        showTipe("请输入昵称！", 0);
        element_o.focus();
        return false;
    }
    if (ignoreSpaces(strValue) == "") {
        showTipe("请输入昵称！", 0);
        element_o.focus();
        return false;
    }
    if (HTMLEncode(strValue) == "") {
        showTipe("请输入昵称！", 0);
        element_o.focus();
        return false;
    }
    if (removeHTMLTag(strValue) == "") {
        showTipe("请输入昵称！", 0);
        element_o.focus();
        return false;
    }

    //获取数据
    GetSearchList("" + vServiceUrl + "VipILog.asmx/GetSrechList", 1, 45, strValue);

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



//是否在显示搜索提示时第一次按上键，如果是要选中最后一项，该变量默认是不选中的
var isp = false;

//只能提示收件人（页面搜索用户）
function searchtowho_s() {
    //收件人昵称
    var towho = $("#keyword_s");

    var strTowhoValue = towho.val();


    //校验收信人
    if (strTowhoValue != "") {
        if (ignoreSpaces(strTowhoValue) == "") {
            showTipe("请输入昵称！", 0);
            towho.focus();
            return false;
        }
        if (HTMLEncode(strTowhoValue) == "") {
            showTipe("请输入昵称！", 0);
            towho.focus();
            return false;
        }
        if (removeHTMLTag(strTowhoValue) == "") {
            showTipe("请输入昵称！", 0);
            towho.focus();
            return false;
        }
    }


    //开始发送
    $.ajax({
        //请求WebService Url         
        url: "" + vServiceUrl + "VipILog.asmx/GetvipilogByNickName",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        cache: false,
        //请求参数              
        data: "{nickname:'" + strTowhoValue + "'}",
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
                    if (item.nickname != undefined) {
                        strList += "<li class=\"\" style=\"cursor:hand\" onclick=\"Getnickname_Box_s2('" + item.nickname + "')\" id=\"il_s" + idx + "\" ><span id=\"hrSearchTowhPeple" + idx + "\" >" + item.nickname + "</span></li>";

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

            //鼠标滑动保持唯以选中的样式（下拉项）
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

                //            }).mouseout(function() { //鼠标滑出
                //                $(this).removeClass("WindowBG");

            });

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

    //判断是否用鼠标选择搜索提示结果，如果是隐藏下拉，搜索框获取焦点，并执行搜索
    var event = arguments.callee.caller.arguments[0] || window.event;

    if (event.keyCode != 38 && event.keyCode != 40 && event.keyCode != undefined) {
        $("#GetSearchTowho_Menu").hide();
        $("#keyword_s").focus();

        GetSearchList("" + vServiceUrl + "VipILog.asmx/GetSrechList", 1, 45, towho);
    }
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
            showTipe("请输入昵称！", 0);
            towho.focus();
            return false;
        }
        if (HTMLEncode(strTowhoValue) == "") {
            showTipe("请输入昵称！", 0);
            towho.focus();
            return false;
        }
        if (removeHTMLTag(strTowhoValue) == "") {
            showTipe("请输入昵称！", 0);
            towho.focus();
            return false;
        }
    }


    //开始发送
    $.ajax({
        //请求WebService Url         
        url: "" + vServiceUrl + "VipILog.asmx/GetvipilogByNickName",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        cache: false,
        //请求参数              
        data: "{nickname:'" + strTowhoValue + "'}",
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
                    if (item.nickname != undefined) {
                        strList += "<li class=\"\" style=\"cursor:hand\" onclick=\"Getnickname_Box_u2('" + item.nickname + "')\" id=\"il_s" + idx + "\" ><a href=\"javascript:void(0)\" id=\"hrSearchTowhPeple" + idx + "\" >" + item.nickname + "</a></li>";

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

    //判断是否用鼠标选择搜索提示结果，如果是隐藏下拉，搜索框获取焦点，并执行搜索
    var event = arguments.callee.caller.arguments[0] || window.event;

    if (event.keyCode != 38 && event.keyCode != 40 && event.keyCode != undefined) {
        $("#GetSearchTowho_Menu2").hide();
        $("#keyword_s").focus();

        GetSearchList("" + vServiceUrl + "VipILog.asmx/GetSrechList", 1, 45, towho);
    }
}


//上下键处理
//搜索下拉
function funListBegin_Up(evt) {
    var keynum;

    if (window.event) // IE
    {
        keynum = evt.keyCode;
    }
    else // Netscape/Firefox/Opera
    {
        keynum = evt.which;
    }

    if (keynum != 13 && keynum != 38 && keynum != 40) {
        return false;
    }
    else if (keynum == 13) {
        funListBeginUp_UL("GetSearchTowho_Menu", 2);
    }
    else if (keynum == 38) {
        funListBeginUp_UL("GetSearchTowho_Menu", 0);
    }
    else if (keynum == 40) {
        funListBeginUp_UL("GetSearchTowho_Menu", 1);
    }

    return false;

}


//上下键处理
function funListBeginUp_UL(NameID, vType) {
    $("#il_s1").addClass("WindowBG");

    //    var bl = true;

    //    var num = $("#as").val();

    //    //用户在搜索结果提示按上键直接选中最后一项
    //    if (vType == 0 && !isp) 
    //    {
    //        var ilsize = $("#GetSearchTowho_Menu li").size();

    //        $("#il_s"+ilsize).addClass("WindowBG");
    //      
    //        //上搜索框
    //        if(num == 0)
    //        {
    //            Getnickname_Box_s2($("#il_s"+ilsize).text());
    //        }
    //        else if(num == 1) //下搜搜索框
    //        {
    //            Getnickname_Box_u2($("#il_s"+ilsize).text());
    //        }
    //        else //容错处理
    //        {
    //            Getnickname_Box_s2($("#il_s"+ilsize).text());
    //        }

    //        isp = true;
    //        
    //    }
    //    else
    //    {
    //        
    //        $("#" + NameID + " li").each(function(i)
    //        {
    //            if (vType == 0) 
    //            {
    //                if ($(this).hasClass("WindowBG")) 
    //                {
    //                    if (bl) 
    //                    {
    //                        var index = $("#GetSearchTowho_Menu li").index($(this));
    //                        var ilsize = $("#GetSearchTowho_Menu li").size();

    //                        //判断是不是选到第一个，如果是需要循环到最后一个
    //                        if(index == 0)
    //                        {

    //                            $(this).removeClass();
    //                            $("#il_s"+ilsize).addClass("WindowBG");

    //                            
    //                            //上搜索框
    //                            if(num == 0)
    //                            {
    //                                Getnickname_Box_s2($("#il_s"+ilsize).text());
    //                            }
    //                            else if(num == 1) //下搜搜索框
    //                            {
    //                                Getnickname_Box_u2($("#il_s"+ilsize).text());
    //                            }
    //                            else //容错处理
    //                            {
    //                                Getnickname_Box_s2($("#il_s"+ilsize).text());
    //                            }
    //                            
    //                        }
    //                        else
    //                        {
    //                            $("#GetSearchTowho_Menu li.WindowBG").prev().addClass("WindowBG");
    //                            $("#GetSearchTowho_Menu li.WindowBG").next().removeClass("WindowBG");
    //                            
    //                            
    //                            //开始遍历
    //                            $("#GetSearchTowho_Menu li").each(function(i)
    //                            {
    //                                if(i != (index -1) && index != 0)
    //                                {
    //                                    $(this).removeClass();
    //                                }
    //                            })
    //                        
    //                            //把选中的值放入搜索框中
    //                            if($(this).prev().text() != "")
    //                            {
    //                                //上搜索框
    //                                if(num == 0)
    //                                {
    //                                    Getnickname_Box_s2($(this).prev().text());
    //                                }
    //                                else if(num == 1) //下搜搜索框
    //                                {
    //                                    Getnickname_Box_u2($(this).prev().text());
    //                                }
    //                                else //容错处理
    //                                {
    //                                    Getnickname_Box_s2($(this).prev().text());
    //                                }
    //                            }
    //                            else    //如果到了最后一个就保持当前选中的值
    //                            {
    //                                //上搜索框
    //                                if(num == 0)
    //                                {
    //                                    Getnickname_Box_s2($(this).text());
    //                                }
    //                                else if(num == 1) //下搜搜索框
    //                                {
    //                                    Getnickname_Box_u2($(this).text());
    //                                }
    //                                else //容错处理
    //                                {
    //                                    Getnickname_Box_s2($(this).text());
    //                                }
    //                            }
    //                        }

    //                        bl = false;

    //                    }
    //                }
    //            }
    //            else if (vType == 1) 
    //            {
    //                if ($(this).hasClass("WindowBG")) 
    //                {
    //                    if (bl) 
    //                    {
    //                        var index = $("#GetSearchTowho_Menu li").index($(this));
    //                        var ilsize = $("#GetSearchTowho_Menu li").size();
    //                    
    //                        //判断是不是选到最后一个，如果是需要循环到第一个
    //                        if(index == (ilsize - 1))
    //                        {
    //                            $(this).removeClass();
    //                            $("#il_s1").addClass("WindowBG");
    //                            
    //                            //上搜索框
    //                            if(num == 0)
    //                            {
    //                                Getnickname_Box_s2($("#il_s1").text());
    //                            }
    //                            else if(num == 1) //下搜搜索框
    //                            {
    //                                Getnickname_Box_u2($("#il_s1").text());
    //                            }
    //                            else //容错处理
    //                            {
    //                                Getnickname_Box_s2($("#il_s1").text());
    //                            }
    //                        }
    //                        else
    //                        {
    //                            //向下
    //                            $("#GetSearchTowho_Menu li.WindowBG").next().addClass("WindowBG");
    //                            $("#GetSearchTowho_Menu li.WindowBG").prev().removeClass("WindowBG");
    //                            
    //                            
    //                            //开始遍历，除了当前选中的选项其他都移除
    //                            $("#GetSearchTowho_Menu li").each(function(i)
    //                            {   
    //                                if(i != (index + 1) && (index + 1) != ilsize)
    //                                {
    //                                    $(this).removeClass();
    //                                }
    //                            })

    //                            //把选中的值放入搜索框中
    //                            if($(this).next().text() != "")
    //                            {
    //                                //上搜索框
    //                                if(num == 0)
    //                                {
    //                                    Getnickname_Box_s2($(this).next().text());
    //                                }
    //                                else if(num == 1) //下搜搜索框
    //                                {
    //                                    Getnickname_Box_u2($(this).next().text());
    //                                }
    //                                else //容错处理
    //                                {
    //                                    Getnickname_Box_s2($(this).next().text());
    //                                }
    //                            }
    //                            else    //如果到了最后一个就保持当前选中的值
    //                            {
    //                                Getnickname_Box_s2($(this).text());
    //                                
    //                                //上搜索框
    //                                if(num == 0)
    //                                {
    //                                    Getnickname_Box_s2($(this).text());
    //                                }
    //                                else if(num == 1) //下搜搜索框
    //                                {
    //                                    Getnickname_Box_u2($(this).text());
    //                                }
    //                                else //容错处理
    //                                {
    //                                    Getnickname_Box_s2($(this).text());
    //                                }
    //                            }
    //                            
    //                            bl = false
    //                        }
    //                    }
    //                }
    //                if(bl)    //选择第一条搜索提示
    //                {
    //                    //只要不是第一次按上就改变“第一次上键的状态”
    //                    isp = true;
    //                    
    //                    $("#il_s1").addClass("WindowBG");

    //                    //上搜索框
    //                    if(num == 0)
    //                    {
    //                        Getnickname_Box_s2($("#il_s1").text());
    //                    }
    //                    else if(num == 1) //下搜搜索框
    //                    {
    //                        Getnickname_Box_u2($("#il_s1").text());
    //                    }
    //                    else //容错处理
    //                    {
    //                        Getnickname_Box_s2($("#il_s1").text());
    //                    }  
    //                }
    //            }
    //            else if (vType == 2) 
    //            {
    //                //回车
    //                if ($(this).hasClass("WindowBG")) 
    //                {
    //                    var strKeyWord = "";
    //                
    //                    //判断是否按了上下键如果没有说明用户想做模糊查询
    //                    if(!isUpDownKey())
    //                    {
    //                        strKeyWord = $("#keyword_s").val();
    //                    }
    //                    else
    //                    {
    //                        strKeyWord = $(this).text();
    //                    }
    //                    
    //                    //上搜索框
    //                    if(num == 0)
    //                    {
    //                        Getnickname_Box_s2(strKeyWord);
    //                    }
    //                    else if(num == 1) //下搜搜索框
    //                    {
    //                        Getnickname_Box_u2(strKeyWord);
    //                    }
    //                    else //容错处理
    //                    {
    //                        Getnickname_Box_s2(strKeyWord);
    //                    }
    //                    
    //                    GetSearchList(""+vServiceUrl + "VipILog.asmx/GetSrechList",1,45,strKeyWord);

    //                }
    //            }
    //        })
    //    }

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