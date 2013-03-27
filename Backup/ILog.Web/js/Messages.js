$(document).ready(function() {
    //加载Ilog站内导航
    funGetTopMenuService("" + vServiceUrl + "IlogTopMenu.asmx/ILogGetTopMenu", "{}", "");
    //加载Ilog用户导航
    funGetTopUserMenuService("" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenu", "{MenuLive:'0'}", "");
    //加载ilog左侧菜单
    funGetleftMenuService("" + vServiceUrl + "ILogWebLeftMenu.asmx/ILogGetHomeLeftMneu", "{MenuLive:'4'}", "");


    //获取当然用户id    
    var userid = $.cookie('useid');

    //获取用户头像信息
    VipILogHome("" + vServiceUrl + "VipIlogUser.asmx/ILogGetUserInfoById", userid);

    //其他页面过来的站短搜索
    //   var keyword_m = getParameter("keyword_s");
    var keyword_m = $("#keyword_a").val();


    //收件人下拉框
    $("#keyword_s").keyup(function() {
        //回车键搜提示无效
        if (!isEnterKey()) {
            searchtowho2();
        }

        //上下键处理
        funListBeginUp_m(event);

    });


    if (keyword_m != "") {
        //解码
        keyword_m = unescape(keyword_m);

        $("#keyword_s").val(keyword_m);

        //获取数据
        GetSearchList("" + vServiceUrl + "VipMail.asmx/GetMailList", 1, 45, keyword_m);

        return;
    }


    //加载站短
    GetList("" + vServiceUrl + "VipMail.asmx/GetMailList", 1, 45);



    //页面title
    ShowTitle("我的站短");

    //获取回车事件
    getenterevent_m();

});


//返回
function ReGetList() {
    //加载站短
    GetList("" + vServiceUrl + "VipMail.asmx/GetMailList", 1, 45);
}


//搜索列表
//servicesUrl：服务地址
//PageCurrent：当前页面
//pagesize：每页多少行数据
//keyword：关键字
function GetList(servicesUrl, PageCurrent, pagesize) {

    //不搜索时页面标题
    $("#myamil_a").html("我的站短");

    //不搜索时需要移除间隔标签
    $('#Hr_20_1').hide();
    $('#Hr_20_2').hide();

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
        data: "{PageCurrent:'" + PageCurrent + "',PageSize:'" + pagesize + "',keyword:'0',ation:'0'}",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var strList = "";

            //获取当然用户id
            var userid = $.cookie('useid');

            //举报链接
            var strReportUlr = "";

            //举报
            var strReport

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

                        $("#RowsCount").html("（已有0个联系人）");  //数据总数

                        //没有搜索到数据
                        $("#list_div").html("");

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
                    sowhpage_ul(PageCurrent, item.RecordCount, "", 0);   //分页
                    return true;
                }

                //大于一页显示页数
                //            if(item.RecordCount != undefined)
                //            {
                //                $("#RowsCount").html("约找到" + item.RecordCount + "页结果"); //联系人数（目前未定如何处理）
                //            
                //                return true;
                //            }

                //等于一页显示条数
                if (item.fromwhocount != undefined) {
                    $("#RowsCount").html("（已有" + item.fromwhocount + "个联系人）");  //联系人数（目前未定如何处理）

                    $("#div_all").html(""); //返回所有列表为空
                    //                return true;
                }

                //如果遍历到不存在节点就不构建
                if (item.id != undefined) {
                    //构建数据
                    strList += "<div class=\"messages Centent\">";
                    //伪静态
                    strList += " <div class=\"pic L\"><a href=\"" + (item.userid == userid ? "u" : "u_" + item.userid) + "\"  ><img id=\"user" + item.id + "\" onmouseover=\"UserInfoShowOver(this," + (item.userid == "" ? 0 : item.userid) + "," + item.id + ")\" src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"头像\" class=\"L Img\" /></a></div>";
                    //伪静态地址
                    strList += "<div class=\"info R G9\" style=\" position:relative\" >" + (item.issend == "True" ? "发给" : "") + "<a href=\"" + (item.userid == userid ? "u" : "u_" + item.userid) + "\" id=\"uu" + item.id + "\" onmouseover=\"UserInfoShowOver(this," + (item.userid == "" ? 0 : item.userid) + "," + item.id + ")\" class=\"Blue\">" + item.towho + "：</a>" + unescape(item.lastcontent) + "";
                    strList += " <div class=\"Hr_4\"></div>";

                    //自己不能举报自己
                    if (userid != item.userid) {
                        strReport = " | <a onclick=\"ShowPageReport(" + item.id + ",'" + item.towho + "','" + item.face + "','" + unescape(item.lastcontent) + "'," + item.vi_memberlevel + ")\" href=\"javascript:void(0);\" class=\"Blue\">举报</a>";
                    }
                    else {
                        strReport = "";
                    }

                    //伪静态地址
                    strList += " <div class=\" G9\"><span class=\"R\"><a href=\"Msgs_" + item.id + "\" class=\"Blue\">共" + item.m_number + "条站短</a> | <a href=\"javascript:void(0);\" onclick=\"showdialog_u('" + item.towho + "',1);\" class=\"Blue\">回复 </a></span>" + item.intime + strReport + " </div> ";

                    strList += " <img class=\" messages_san \" src=\"http://simg.instrument.com.cn/ilog/blue/images/san_1.gif\" /></div> ";
                    strList += " <div class=\" Hr_20\"></div> ";
                    strList += " </div>  ";

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
//id：站短最外面的页面传0就可以
function GetSearchList(servicesUrl, PageCurrent, pagesize, keyword) {
    //搜索时要修改页面标题
    $("#myamil_a").html("站短搜索");

    //搜索时不要显示联系人的数量
    $("#RowsCount").html("");

    //搜索时再显示标签
    $('#Hr_20_1').show();
    $('#Hr_20_2').show();

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
        data: "{PageCurrent:'" + PageCurrent + "',PageSize:'" + pagesize + "',keyword:'" + unescape(keyword) + "',ation:'1'}",
        //成功           
        success: function(json) {
            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var strList = "";

            //获取当然用户id
            var userid = $.cookie('useid');

            var strSearchInfo = "";

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

                        $("#RowsCount").html("");  //没有搜索结果不需要搜索条数提示


                        //没有搜索结果提示  
                        strSearchInfo = "<a href=\"javascript:void(0);\" onclick=\"ReGetList();\" class=\"Blue\" >返回所有站短</a>";
                        strSearchInfo += " > 共找到0条关于“" + keyword + "”的结果：</br>";
                        strSearchInfo += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp";
                        strSearchInfo += "<font color=\"#999999\" >没有符合条件的站短，返回</font><a href=\"javascript:void(0);\" onclick=\"ReGetList();\" class=\"Blue\" >查看全部站短</a>";


                        $("#div_all").html(strSearchInfo);


                        //无数据空白显示
                        $("#list_div").html("");

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
                if (item.RecordCount != undefined)   //拿到总页数
                {
                    sowhpage_ul(PageCurrent, item.RecordCount, keyword, 1);   //分页
                    strSearchInfo += "<a href=\"javascript:void(0);\" onclick=\"ReGetList();\" class=\"Blue\" >返回所有站短</a>";
                    strSearchInfo += " > ";
                    strSearchInfo += "共找到约<font color=\"red\" >" + item.RecordCount + "</font>页关于“" + unescape(keyword) + "”的结果：";
                    $("#div_all").html(strSearchInfo);
                    //　　            $("#RowsCount").html("约找到" + item.RecordCount + "页结果"); //数据页数显示
                    return true;
                }
                else if (item.RowsCount != undefined) //显示条数页数
                {
                    $("#sowhpage_div").html("");    //去掉分页    
                    strSearchInfo += "<a href=\"javascript:void(0);\" onclick=\"ReGetList();\" class=\"Blue\" >返回所有站短</a>";
                    strSearchInfo += " > ";
                    strSearchInfo += "共找到<font color=\"red\" >" + item.RowsCount + "</font>条关于“" + unescape(keyword) + "”的结果：";
                    $("#div_all").html(strSearchInfo);
                    //                $("#RowsCount").html("约找到" + item.RowsCount + "条数据");  //数据页数

                    return true;
                }

                //如果遍历到不存在节点就不构建
                if (item.id != undefined) {
                    //构建数据
                    strList += "<div class=\"messages Centent\">";

                    //伪静态地址
                    strList += " <div class=\"pic L\"><a href=\"" + (item.userid == userid ? "u" : "u_" + item.userid) + "\" id=\"user" + item.id + "\" onmouseover=\"UserInfoShowOver(this," + (item.userid == "" ? 0 : item.userid) + "," + item.id + ")\" ><img src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"头像\" class=\"L Img\" /></a></div>";
                    //伪静态地址
                    strList += "<div class=\"info R G9\" style=\" position:relative\" >" + (item.issend == "True" ? "发给" : "") + "<a href=\"" + (item.userid == userid ? "u" : "u_" + item.userid) + "\" id=\"uu" + item.id + "\" onmouseover=\"UserInfoShowOver(this," + (item.userid == "" ? 0 : item.userid) + "," + item.id + ")\" class=\"Blue\">" + (item.towho.replace(keyword, "<font color=\"red\">" + keyword + "</font>")) + "：</a>" + unescape(item.lastcontent) + "";
                    strList += " <div class=\"Hr_4\"></div>";

                    //伪静态地址
                    strList += " <div class=\" G9\"><span class=\"R\"><a href=\"Msgs_" + item.id + "\" class=\"Blue\">共" + item.m_number + "条站短</a> | <a href=\"javascript:void(0);\" onclick=\"showdialog_u('" + item.towho + "',1);\" class=\"Blue\">回复 </a></span>" + item.intime + " |  <a onclick=\"ShowPageReport(" + item.id + ",'" + item.towho + "','" + item.face + "','" + unescape(item.lastcontent) + "'," + item.vi_memberlevel + ")\" href=\"javascript:void(0);\" class=\"Blue\">举报</a> </div> ";

                    strList += " <img class=\" messages_san \" src=\"http://simg.instrument.com.cn/ilog/blue/images/san_1.gif\" /></div> ";
                    strList += " <div class=\" Hr_20\"></div> ";
                    strList += " </div>  ";
                }
            });
            $("#list_div").html(strList);
            scroll(0, 0);  //翻页后需要回到顶部
        },
        //出错调试         
        error: function(x, e) {

            //alert("加载异常");

            window.location.href = "http://c.instrument.com.cn/art/ilog/404.asp";
        },
        //执行成功后自动执行           
        complete: function(x) {

        }
    });
}

//分页控件（加载页面）
//PageCurrent：当然页码
//总页数
//搜索关键字
//操作类型0：读取，1，搜索
function sowhpage_ul(PageCurrent, RecordCount, keyword, ation) {
    var sowhpage_div = $("#sowhpage_div");

    var strShowPage = "";

    //当然页码等于总也数就隐藏下一页按钮
    //读取
    if (ation == 0) {
        strShowPage += "<span class=\"R\" " + (PageCurrent == RecordCount ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"nextbtn\" ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_page.gif\" onclick=\"nextpage(" + PageCurrent + "," + RecordCount + ",''," + ation + ")\" alt=\"下一页\" /></span>";
    }
    else //搜索
    {
        strShowPage += "<span class=\"R\" " + (PageCurrent == RecordCount ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"nextbtn\" ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_page.gif\" onclick=\"nextpage(" + PageCurrent + "," + RecordCount + ",'" + keyword + "'," + ation + ")\" alt=\"下一页\" /></span>";
    }

    strShowPage += "<span class=\"R span\" style=\"position:relative\" ><a href=\"javascript:void(0);\"  id='selOption' class=\"Blue\">第" + PageCurrent + "页</a><img src=\"http://simg.instrument.com.cn/ilog/blue/images/SanJ.gif\" />";

    strShowPage += "<ul class=\"pageBox R Line Fa BrWh\" id=\"selOption_menu\" style=\" position:absolute; bottom:-13px; left:15px;display:none;\">";

    for (var i = RecordCount; i >= 1; i--) {
        if (PageCurrent == i) {
            if (ation == 0)  //读取
            {
                strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"GetList('" + vServiceUrl + "VipMail.asmx/GetMailList'," + i + ",45)\"  >第<font color=\"red\">" + i + "</font>页</a></li>";
            }
            else            //搜索
            {
                strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"GetSearchList('" + vServiceUrl + "VipMail.asmx/GetSearchMailList'," + i + ",45,'" + keyword + "')\"  >第<font color=\"red\">" + i + "</font>页</a></li>";
            }
        }
        else {
            if (ation == 0) {
                strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"GetList('" + vServiceUrl + "VipMail.asmx/GetMailList'," + i + ",45,'" + keyword + "')\"  >第" + i + "页</a></li>";
            }
            else {
                strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"GetSearchList('" + vServiceUrl + "VipMail.asmx/GetSearchMailList'," + i + ",45,'" + keyword + "')\"  >第" + i + "页</a></li>";
            }
        }
    }

    strShowPage += "</ul>";
    strShowPage += "</span>";

    //当前页码小于1就隐藏上一页页码

    if (ation == 0)  //读取
    {
        strShowPage += "<span class=\"R\" " + (PageCurrent <= 1 ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"upbtn\"  ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_pageT.gif\" onclick=\"uppage(" + PageCurrent + "," + RecordCount + ",''," + ation + ")\" alt=\"上一页\" /></span>";
    }
    else            //搜索
    {
        strShowPage += "<span class=\"R\" " + (PageCurrent <= 1 ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"upbtn\"  ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_pageT.gif\" onclick=\"uppage(" + PageCurrent + "," + RecordCount + ",'" + keyword + "'," + ation + ")\" alt=\"上一页\" /></span>";
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
//keyword：搜索关键字
//ation：操作类型
function nextpage(PageCurrent, RecordCount, keyword, ation) {
    var RecordCount_index = RecordCount;    //记录总页数

    //没翻过上一页
    if (pageindex_n <= 2) {
        if (RecordCount >= 1) {
            pageindex++;

            if (ation == 0)       //读取
            {
                //重新绑定
                GetList("" + vServiceUrl + "VipMail.asmx/GetMailList", pageindex, 45);
            }
            else            //搜索
            {
                //重新绑定
                GetSearchList("" + vServiceUrl + "VipMail.asmx/GetSearchMailList", pageindex, 45, keyword);
            }
        }
    }
    else {
        if (RecordCount >= 1) {
            pageindex_n++;

            if (ation == 0)       //读取
            {
                //重新绑定
                GetList("" + vServiceUrl + "VipMail.asmx/GetMailList", pageindex_n, 45);
            }
            else                //搜索
            {
                //重新绑定
                GetSearchList("" + vServiceUrl + "VipMail.asmx/GetSearchMailList", pageindex_n, 45, keyword);
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
//keyword：搜索关键字
//ation：操作类型：0读取，1：搜索
function uppage(PageCurrent, RecordCount, keyword, ation) {
    var pageindex_n = PageCurrent;    //记录总页数

    if (pageindex == 1) {
        if (PageCurrent <= RecordCount) {
            pageindex_n--;

            if (ation == 0)  //读取
            {
                //重新绑定
                GetList("" + vServiceUrl + "VipMail.asmx/GetMailList", pageindex_n, 45);
            }
            else            //搜索
            {
                //重新绑定
                GetSearchList("" + vServiceUrl + "VipMail.asmx/GetSearchMailList", pageindex_n, 45, keyword);
            }
        }
    }
    else {
        if (PageCurrent <= RecordCount) {
            pageindex--;

            if (ation == 0)  //读取
            {
                //重新绑定
                GetList("" + vServiceUrl + "VipMail.asmx/GetMailList", pageindex, 45);
            }
            else            //搜索
            {
                //重新绑定
                GetSearchList("" + vServiceUrl + "VipMail.asmx/GetSearchMailList", pageindex, 45, keyword);
            }
        }
    }

    //如果到第一页了就隐藏上一也
    if (pageindex_n == 1) {
        pageindex_n = 2;    //索引初始化
    }
}

//校验搜索
//index：0上面的搜索，1下面的搜索
function checkform() {
    //上面搜索
    var element_p = document.getElementById("keyword_s");

    var element_o = "";     //搜索框对象
    var strValue = "";      //值

    strValue = element_p.value;
    element_o = element_p;

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
    GetSearchList("" + vServiceUrl + "VipMail.asmx/GetMailList", 1, 45, strValue);

    //隐藏下拉菜单
    $("#GetSearchTowho_Menu").hide();
}