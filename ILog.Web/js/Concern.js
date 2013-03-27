
$(document).ready(function() {
    var keyword = $.trim($("#keyword").val());
    if (keyword == "请输入昵称") {
        keyword = "";
    }
    else {
        $("#txtFollowtSearch").val(keyword)
    }
    //消除提醒
    funGetFanCanCel();
    //加载Ilog站内导航
    funGetTopMenuService("" + vServiceUrl + "IlogTopMenu.asmx/ILogGetTopMenu", "{}", "");
    //加载Ilog用户导航
    funGetTopUserMenuService("" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenu", "{MenuLive:'0'}", "");
    //加载ilog左侧菜单
    funGetleftMenuService("" + vServiceUrl + "ILogWebLeftMenu.asmx/ILogGetFollowLeftMneu", "{MenuLive:'3'}", "");
    // 获取关注我的 人
    funGetFan("{}", "", "spanfan");
    $("#SearchFollow").click(function() {
        funSearchFollow();
    });

    ShowConcernListALL("" + vServiceUrl + "ILogFollow.asmx/GetList_Fan", 1, 45, keyword, 1);
    $("#txtFollowtSearch").keyup(function() { GetSearchFollowUpList() });

});

function funLoadPageInfo() {
    //加载ilog左侧菜单
    funGetleftMenuService("" + vServiceUrl + "ILogWebLeftMenu.asmx/ILogGetFollowLeftMneu", "{MenuLive:'3'}", "");
    //加载提醒
    funGetTopUserMenuOutList();
    // 获取关注我的 人
    funGetFan("{}", "", "spanfan");

}

//消除我的粉丝提醒
//GetUrl 请求的服务 Url
//GetParts 请求的参数
//GetWait 等待参数
function funGetFanCanCel() {

    $.ajax({
        url: "" + vServiceUrl + "ILogServiceUserCount.asmx/ILogServiceUserOutFanZero",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{}",
        success: function(json) {
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






function ShowConcernPageSort(listType) {
    var vcontent = " 排序方式： ";

    if (listType == 1) {
        vcontent += "<a href=\"javascript:void(0)\" onclick=\"ShowConcernListAllType(1)\"><strong>关注时间</strong></a> ";
        vcontent += "<span class=\"G9\"> | </span><a href=\"javascript:void(0)\" class=\"Blue\" onclick=\"ShowConcernListAllType(2)\">最近联系</a> ";
        vcontent += "<span class=\"G9\"> | </span> <a href=\"javascript:void(0)\" class=\"Blue\" onclick=\"ShowConcernListAllType(3)\">粉丝数</a> ";
    }
    else if (listType == 2) {
        vcontent += "<a href=\"javascript:void(0)\"  onclick=\"ShowConcernListAllType(1)\" class=\"Blue\">关注时间</a>";
        vcontent += "<span class=\"G9\"> | </span><a href=\"javascript:void(0)\" onclick=\"ShowConcernListAllType(2)\"><strong>最近联系</strong></a> ";
        vcontent += "<span class=\"G9\"> | </span> <a href=\"javascript:void(0)\" class=\"Blue\" onclick=\"ShowConcernListAllType(3)\">粉丝数</a> ";
    }
    else if (listType == 3) {
        vcontent += "<a href=\"javascript:void(0)\"  onclick=\"ShowConcernListAllType(1)\" class=\"Blue\">关注时间</a> ";
        vcontent += "<span class=\"G9\"> | </span><a href=\"javascript:void(0)\" class=\"Blue\" onclick=\"ShowConcernListAllType(2)\">最近联系</a> ";
        vcontent += "<span class=\"G9\"> | </span> <a href=\"javascript:void(0)\" onclick=\"ShowConcernListAllType(3)\"><strong>粉丝数</strong></a> ";
    }


    $("#PageSort").html(vcontent);
}




//搜索下拉列表
//zhangl 20120705
function GetSearchFollowUpList() {

    var txtFollowtSearch = $("#txtFollowtSearch").val();
    if (txtFollowtSearch != "") {
        //搜索下拉列表
        $.ajax({
            url: "" + vServiceUrl + "ILogFollow.asmx/GetList_ConcernUserid",
            type: "POST",
            dataType: "json",
            contentType: "application/json;",
            data: "{NickName:'" + txtFollowtSearch + "'}",
            success: function(json) {

                var dataObj = eval("(" + json.d + ")"); //转换为json对象

                var TopContent = "";
                $.each(dataObj.ConcernList, function(idx, item) {
                    if (idx == 0) {
                        if (item.UrlState == "0") {
                            $("#GetFollow_Menu").html("<li>加载错误</li>");
                            return false;
                        }
                        else if (item.UrlState == "2") {
                            TopContent = "";
                            return false;
                        }
                        else {
                            return true;
                        }
                    }
                    TopContent = TopContent + " <li onclick=\"setPageTxtFollowSearch('" + item.NickName + "')\"> &nbsp;" + item.NickName + "</li>";
                });
                $("#GetFollow_Menu").html(TopContent);
                if (TopContent != "") {
                    $("#GetFollow_Menu").show();
                }
                else {
                    $("#GetFollow_Menu").hide();
                }
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


//搜索
function funSearchFollow() {
    var txtSearch = $("#txtFollowtSearch").val();

    if (txtSearch == null || ignoreSpaces(txtSearch) == "") {
        $("#txtFollowtSearch").val("请输入昵称");
    }
    else if (txtSearch != "请输入昵称") {

        window.location.href = "fans_" + escape(txtSearch) + "";

    }
}


///选中值
function setPageTxtFollowSearch(vTxtValue) {
    $("#txtFollowtSearch").val(vTxtValue);
    $("#GetFollow_Menu").hide();
}


//获取排序名称
function ListSortName(listType) {
    if (listType == 1) {
        return "intime";
    }
    else if (listType == 2) {
        return "connecttime";
    }
    else {
        return "vic_fannum";
    }
}

//按不同排序查询
function ShowConcernListAllType(listtype) {
    ShowConcernListALL("" + vServiceUrl + "ILogFollow.asmx/GetList_Fan", 1, 45, "", listtype);

}
//搜索列表
//servicesUrl：服务地址
//PageCurrent：当前页面
//pagesize：每页多少行数据
//keyword：关键字
function ShowConcernListALL(servicesUrl, PageCurrent, pagesize, keyword, listType) {

    //排序方式 
    ShowConcernPageSort(listType)

    var listTypeContent = ListSortName(listType);


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
        data: "{PageCurrent:'" + PageCurrent + "',PageSize:'" + pagesize + "',keyWord:'" + keyword + "',PageSort:'" + listTypeContent + "'}",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var strList = "";

            //循环获取值
            $.each(dataObj.ConcernList, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        $("#Follow_PageList").html("加载错误");
                    }
                    else if (item.UrlState == "2")  //无数据不显示分页控件
                    {
                        var sowhpage_div = $("#Follow_Page");
                        sowhpage_div.html(""); //初始化分页控件

                        //构建数据
                        strList += "<DIV class=\"Hr_20\"></DIV>";
                        strList += " <div class=\"G4\">";
                        strList += " 抱歉，没有找到您的人粉丝<br>";
                        strList += " <div class=\"Hr_10\"></div></div> ";

                        $("#Follow_PageList").html(strList);

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
                if (idx == 1) {
                    if (item.RecordCount > 1)   //拿到总页数
                    {
                        sowhpage_ul(PageCurrent, item.RecordCount, keyword, listType);   //分页
                        // $("#RowsCount").html("约找到" + item.RecordCount + "页结果"); //数据页数显示
                        return true;
                    }
                    else if (item.RecordCount == 1) //显示条数页数
                    {
                        //  $("#RowsCount").html("约找到" + item.RowsCount + "条数据");  //数据页数
                        $("#sowhpage_div").html("");    //去掉分页

                        return true;
                    }
                }


                //如果遍历到不存在节点就不构建
                if (item.RecordID != undefined) {
                    //构建数据
                    strList += "<div class=\"Concern_Txt\" id=\"Concern_" + item.RecordID + "\">";
                    strList += "<div class=\"pic L\"><a href=\"/u_" + item.ConcernUserID + "\" >";
                    strList += "<img src=\"" + (item.face != "" ? "images/face/small/" + item.Face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"" + item.NickName + "\" class=\"img L\" id=\"user" + item.RecordID + "\" onMouseOver=\"UserInfoShowOver(this," + item.ConcernUserID + "," + item.RecordID + ")\"/></a><br />";
                    strList += "<a href=\"javascript:void(0)\" onclick=\"showdialog_u('" + item.NickName + "');\">";
                    strList += "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_mail.gif\" alt=\"站短\"  /></a></div>";
                    strList += "<div class=\"info L\" style=\"position:relative\">";
                    strList += "<a href=\"/u_" + item.ConcernUserID + "\"  id=\"nikename" + item.RecordID + "\" onMouseOver=\"UserInfoShowOver(this," + item.ConcernUserID + "," + item.RecordID + ")\"  class=\"Blue F14\" >" + item.NickName + "</a>";

                    strList += ShowVerifyImg(item.ILogClass);
                    strList += "<div class=\" L19  G4\">";
                    strList += " <img src=\"http://simg.instrument.com.cn/ilog/blue/images/";
                    if (item.Sex == "male") {
                        strList += "men.gif";
                    }
                    else {
                        strList += "women.jpg";
                    }
                    strList += "\"  /> " + item.City + " <br />";
                    strList += "关注 <b><a href=\"follow_" + item.ConcernUserID + "\" class=\"Blue\">" + item.Concernn + "</a></b> <span class=\"G9\"> | </span> ";
                    strList += "粉丝 <b><a href=\"fans_" + item.ConcernUserID + ":\" class=\"Blue\">" + item.Fan + "</a></b> <span class=\"G9\"> | </span>";
                    strList += "微博 <b><a href=\"/u_" + item.ConcernUserID + "\" class=\"Blue\">" + item.ILog + "</a></b>";

                    strList += " </div>";
                    if (item.IlogID != "0") {
                        strList += " <p class=\"txt ENG Fa\"> <a href=\"tcont_" + item.ConcernUserID + "_" + item.IlogID + "\" >" + unescape(item.IlogContent) + "" + item.intime + "</a></p>"
                    }
                    strList += "</div><div class=\"btn R L23 G4 Tr\">";
                    if (item.ConcernState == "1") {
                        strList += "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/concern.gif\" alt=\"互相关注\"  /><br />";
                    }
                    else {
                        strList += "<span id=\"cencernimg_" + item.RecordID + "\"><a href=\"javascript:void(0)\" ID=\"hr_fan_" + item.RecordID + "\" onclick=\"ShowAddFollowTrueConcern(" + item.RecordID + "," + item.ConcernUserID + ",'" + item.NickName + "')\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/concern_j.gif\" alt=\"加关注\"  /></a></span><br />";
                    }
                    strList += "<a href=\"javascript:void(0)\" class=\"Blue\" onclick=\"ShowPageDeleteFan_Fan(" + item.RecordID + "," + item.ConcernUserID + ",'" + item.NickName + "')\">移除粉丝</a><br />";
                    strList += "<a href=\"javascript:void(0)\" class=\"Blue\" onclick=\"ShowPageUserReport(" + item.ConcernUserID + ")\">举报</a></div>";
                    strList += "<div class=\" Hr_1\"></div>";
                    strList += "</div><div class=\"Line_ilog\" id=\"Concernline_" + item.RecordID + "\"></div>";

                }
            });
            $("#Follow_PageList").html(strList);
            scroll(0, 0);  //翻页后需要回到顶部
        },
        //出错调试         
        error: function(x, e) {

            alert(x.responseText);

            // window.location.href = "http://c.instrument.com.cn/art/ilog/404.asp";
        },
        //执行成功后自动执行           
        complete: function(x) {

        }
    });
}

//分页控件（加载页面）
function sowhpage_ul(PageCurrent, RecordCount, keyword, listType) {
    var sowhpage_div = $("#Follow_Page");

    var strShowPage = "";

    //当然页码等于总也数就隐藏下一页按钮
    strShowPage += "<span class=\"R\" " + (PageCurrent == RecordCount ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"nextbtn\" ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_page.gif\" onclick=\"nextpage(" + PageCurrent + "," + RecordCount + ",'" + keyword + "'," + listType + ")\" alt=\"下一页\" /></span>";

    strShowPage += "<span class=\"R span\" style=\"position:relative\" ><a href=\"javascript:void(0);\"  id='selOption' class=\"Blue\">第" + PageCurrent + "页</a><img src=\"http://simg.instrument.com.cn/ilog/blue/images/SanJ.gif\" />";

    strShowPage += "<ul class=\"pageBox R Line Fa BrWh\" id=\"selOption_menu\" style=\" position:absolute; bottom:16px; left:15px;display:none;\">";

    for (var i = RecordCount; i >= 1; i--) {
        if (PageCurrent == i) {
            strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"ShowConcernListALL('" + vServiceUrl + "ILogFollow.asmx/GetList_Fan'," + i + ",45,'" + keyword + "'," + listType + ")\"  >第<font color=\"red\">" + i + "</font>页</a></li>";
        }
        else {
            strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"ShowConcernListALL('" + vServiceUrl + "ILogFollow.asmx/GetList_Fan'," + i + ",45,'" + keyword + "'," + listType + ")\"  >第" + i + "页</a></li>";
        }
    }

    strShowPage += "</ul>";
    strShowPage += "</span>";

    //当前页码小于1就隐藏上一页页码
    strShowPage += "<span class=\"R\" " + (PageCurrent <= 1 ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"upbtn\"  ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_pageT.gif\" onclick=\"uppage(" + PageCurrent + "," + RecordCount + ",'" + keyword + "'," + listType + ")\" alt=\"上一页\" /></span>";

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
function nextpage(PageCurrent, RecordCount, keyword, pagesort) {
    var RecordCount_index = RecordCount;    //记录总页数

    //没翻过上一页
    if (pageindex_n <= 2) {
        if (RecordCount >= 1) {
            pageindex++;

            //重新绑定
            ShowConcernListALL("" + vServiceUrl + "ILogFollow.asmx/GetList_Fan", pageindex, 45, keyword, pagesort);
        }
    }
    else {
        if (RecordCount >= 1) {
            pageindex_n++;

            //重新绑定
            ShowConcernListALL("" + vServiceUrl + "ILogFollow.asmx/GetList_Fan", pageindex_n, 45, keyword, pagesort);
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
function uppage(PageCurrent, RecordCount, keyword, pagesort) {
    var pageindex_n = PageCurrent;    //记录总页数

    if (pageindex == 1) {
        if (PageCurrent <= RecordCount) {
            pageindex_n--;

            //重新绑定
            ShowConcernListALL("" + vServiceUrl + "ILogFollow.asmx/GetList_Fan", pageindex_n, 45, keyword, pagesort);
        }
    }
    else {
        if (PageCurrent <= RecordCount) {
            pageindex--;

            //重新绑定
            ShowConcernListALL("" + vServiceUrl + "ILogFollow.asmx/GetList_Fan", pageindex, 45, keyword, pagesort);
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


    //上面搜索
    var element_p = document.getElementById("keyword_s");

    //下面搜索
    var element_p2 = document.getElementById("keyword_s2");

    var element_o = "";     //搜索框对象
    var strValue = "";      //值
    var pagesort = "intime";
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

    if (strValue == "请输入昵称") {
        alert("请输入昵称！");
        element_o.focus();
        return false;
    }
    if (strValue == "" || strValue == null) {
        alert("请输入昵称！");
        element_o.focus();
        return false;
    }
    if (ignoreSpaces(strValue) == "") {
        alert("请输入昵称！");
        element_o.focus();
        return false;
    }
    if (HTMLEncode(strValue) == "") {
        alert("请输入昵称！");
        element_o.focus();
        return false;
    }
    if (removeHTMLTag(strValue) == "") {
        alert("请输入昵称！");
        element_o.focus();
        return false;
    }


}

var showConcerntDialog;
//移除粉丝
function ShowPageDeleteFan_Fan(iuc_id, concernuserid, nikename) {

    showConcerntDialog = $.dialog({
        id: "divShowRePort",
        title: false,
        content: ShowFanCancle(iuc_id, concernuserid, nikename),
        max: false,
        min: false,
        lock: true,
        cache: false,
        // width: 295,
        //height: 99,
        padding: 0


    });

    //取消
    $("#btnFanCancle").click(function() {
        CloseConcern();
    });
    //确定取消
    $("#btnFanTrue").click(function() {
        ShowFanSubmitInfo(iuc_id, concernuserid);
    });

}


//确认移除粉丝
function ShowFanSubmitInfo_Fan(iuc_id, concernuserid) {
    $.ajax({
        url: "" + vServiceUrl + "ILogFollow.asmx/ILogFollowFanCancle",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{iufID:'" + iuc_id + "',concernUserid:'" + concernuserid + "'}",
        cache: false,
        async: false,
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
                        $("#Concern_" + iuc_id + "").hide();
                        $("#Concernline_" + iuc_id + "").hide();

                        showTipe("移除粉丝成功。", 1);
                        //判断与用户的关系
                        CloseConcern();

                        //清除关注用户
                        $("#Concern_" + iuc_id).next().remove();
                        $("#Concern_" + iuc_id).remove();
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