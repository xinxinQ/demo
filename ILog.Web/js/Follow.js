


$(document).ready(function() {

    ShowTitle("我关注的人");
    //加载Ilog站内导航
    funGetTopMenuService("" + vServiceUrl + "IlogTopMenu.asmx/ILogGetTopMenu", "{}", "");
    //加载Ilog用户导航
    funGetTopUserMenuService("" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenu", "{MenuLive:'0'}", "");
    //加载ilog左侧菜单
    funGetleftMenuService("" + vServiceUrl + "ILogWebLeftMenu.asmx/ILogGetFollowLeftMneu", "{MenuLive:'1'}", "");

    //关注的数量
    funGetConcern("{}", "", "spanConcern");
    //获取关注页面内导航
    funGetFollowMenuService("{MenuLive:'1'}", "", 0);
    //搜索关注
    $("#SearchFollow").click(function() {
        funSearchFollow();
    });
    //添加分组
    $("#GroupAdd").click(function() {
        ShowGroupFollow();
    });
    //页面更多添加分组
    $("#GroupAddP").click(function() {
        ShowGroupFollow();
    });
    //页面搜索下拉
    $("#txtFollowtSearch").keyup(function() { GetSearchFollowUpList() });
    //显示全部关注
    ShowConcernListALL("" + vServiceUrl + "ILogFollow.asmx/GetList_Concern", 1, 45, "", 1);


});

//页面更新提醒
//zhangl 20120705
function funLoadPageInfo() {
    //关注的数量
    funGetConcern("{}", "", "spanConcern");

    //加载ilog左侧菜单
    funGetleftMenuService("" + vServiceUrl + "ILogWebLeftMenu.asmx/ILogGetFollowLeftMneu", "{MenuLive:'1'}", "");

    //加载提醒
    funGetTopUserMenuOutList();

    //显示全部关注
    ShowConcernListALL("" + vServiceUrl + "ILogFollow.asmx/GetList_Concern", 1, 45, "", 1);
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

//执行搜索
//zhangl 20120705
function funSearchFollow() {
    var txtSearch = $("#txtFollowtSearch").val();

    if (txtSearch == null || ignoreSpaces(txtSearch) == "") {
        $("#txtFollowtSearch").val("请输入昵称");
    }
    else if (txtSearch != "请输入昵称") {

        window.location.href = "/FollowS_" + txtSearch + "";

    }
}


///选中值
function setPageTxtFollowSearch(vTxtValue) {
    $("#txtFollowtSearch").val(vTxtValue);
    $("#GetFollow_Menu").hide();
}
//下拉用户组
function ShowGroupListAll(HrName, HrID, ConcernUserID) {



    $.ajax({
        url: "" + vServiceUrl + "ILogFollow.asmx/ILogFollowGetGroupListConcern",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{ConcernUserID:'" + ConcernUserID + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            $.each(dataObj.GroupList, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        $("#GroupMoreList_" + HrID + "_Menu").html("<li>加载错误</li>");
                        return false;
                    }
                    else if (item.UrlState == "2") {
                        TopContent = "<li>尚未分组！</li>";
                        return false;
                    }
                    else {
                        return true;
                    }
                }
                if (item.GroupID > 0) {
                    if (idx == 1) {
                        if (item.MenuLive == "1") {
                            TopContent = TopContent + " <li class=\"bj\"><input type=\"checkbox\" id=\"check_" + item.GroupID + "\"  checked=\"checked\"  onclick=\"CheckListGroup('" + HrName + "'," + HrID + "," + ConcernUserID + "," + item.GroupID + ")\"/>&nbsp;" + item.GroupName + "</li>";
                        }
                        else {
                            TopContent = TopContent + " <li class=\"bj\"><input type=\"checkbox\" id=\"check_" + item.GroupID + "\"    onclick=\"CheckListGroup('" + HrName + "'," + HrID + "," + ConcernUserID + "," + item.GroupID + ")\"/>&nbsp;" + item.GroupName + "</li>";
                        }
                    }
                    else {
                        if (item.MenuLive == "1") {
                            TopContent = TopContent + " <li ><input type=\"checkbox\" id=\"check_" + item.GroupID + "\"  checked=\"checked\"  onclick=\"CheckListGroup('" + HrName + "'," + HrID + "," + ConcernUserID + "," + item.GroupID + ")\"/>&nbsp;" + item.GroupName + "</li>";
                        }
                        else {
                            TopContent = TopContent + " <li><input type=\"checkbox\" id=\"check_" + item.GroupID + "\"  onclick=\"CheckListGroup('" + HrName + "'," + HrID + "," + ConcernUserID + "," + item.GroupID + ")\"/>&nbsp;" + item.GroupName + "</li>";
                        }
                    }
                }
            });
            TopContent = "<ul  id=\"MenuListUL\"> " + TopContent + " </ul>";
            TopContent += " <div class=\" Line_dashed\"></div>";
            TopContent += " <div>&nbsp;<img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-j1.gif\"  /> <a href=\"javascript:void(0);\" class=\"Gray9\" id=\"GroupAddList_" + HrID + "\" >创建分组</a></div>"
            $("#GroupMoreList_" + HrID + "_Menu").html(TopContent);
            //添加分组
            $("#GroupAddList_" + HrID + "").click(function() {
                ShowGroupUpListFollow(ConcernUserID);
            });
            setMenuPositionslist(HrName, HrID);

            MenuDivShowList(HrName, HrID);
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


//选中或取消分组
function CheckListGroup(HrName, HrID, ConcernUserID, GroupID) {
    var type = 0;
    if ($("#check_" + GroupID + "").attr("checked")) {
        type = 1;
    }



    $.ajax({
        url: "" + vServiceUrl + "ILogFollow.asmx/ILogFollowEditGroupConnect",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{groupID:'" + GroupID + "',concernUserid:'" + ConcernUserID + "',type:'" + type + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            $.each(dataObj.GroupEdit, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        $("#" + HrName + "_" + HrID + "").html("<li>加载错误</li>");
                        return false;
                    }
                    else if (item.UrlState == "2") {
                        return false;
                    }
                }


                if (item.MenuLive == "1") {
                    if (TopContent == "") {
                        TopContent += item.GroupName;
                    }
                    else {
                        TopContent += "," + item.GroupName;
                    }
                }
            });
            if (TopContent != "") {
                var topTitle = TopContent;
                if (TopContent.length > 5) {
                    $("#" + HrName + "_" + HrID + "").html(TopContent.substring(0, 4) + "... <img src=\"http://simg.instrument.com.cn/ilog/blue/images/SanJ.gif\" />");
                }
                else {
                    $("#" + HrName + "_" + HrID + "").html(TopContent + "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/SanJ.gif\" />");
                }
                $("#" + HrName + "_" + HrID + "").attr("title", topTitle);
            }
            else {
                $("#" + HrName + "_" + HrID + "").html("未分组<img src=\"http://simg.instrument.com.cn/ilog/blue/images/SanJ.gif\" />")
                $("#" + HrName + "_" + HrID + "").attr("title", "未分组");
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



//页面数据排序方式
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
    ShowConcernListALL("" + vServiceUrl + "ILogFollow.asmx/GetList_Concern", 1, 45, "", listtype);

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
                        strList += " 没还没有关注的朋友<br></div>";
                        strList += " <div class=\"Hr_10\"></div> ";
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
                    strList += "<div class=\"Concern_Txt\" id=\"Concern_" + item.RecordID + "\" >";
                    strList += " <div class=\"pic L\"><a href=\"/u_" + item.ConcernUserID + "\">";
                    strList += " <img src=\"" + (item.face != "" ? "images/face/small/" + item.Face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"" + item.NickName + "\" class=\"L img\" id=\"user"
                   + item.RecordID + "\" onMouseOver=\"UserInfoShowOver(this," + item.ConcernUserID + "," + item.RecordID + ");\" /></a><br />";
                    strList += " <a href=\"javascript:void(0)\" onclick=\"showdialog_u('" + item.NickName + "',0);\">";
                    strList += " <img src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_mail.gif\" alt=\"站短\"  /></a></div>";
                    if (item.GroupID == 0) {
                        strList += " <div class=\"info L\" style=\"position:relative; \" id=\"infol_" + item.RecordID + "\"><span class=\"R\"><a href=\"javascript:void(0)\" id=\"GroupMoreList_" + item.RecordID + "\"   onclick=\"ShowGroupListAll('GroupMoreList','" + item.RecordID + "','" + item.ConcernUserID + "')\">未分组<img src=\"http://simg.instrument.com.cn/ilog/blue/images/SanJ.gif\" /></a>";
                        strList += " </span>";
                    }
                    else {

                        var groupname = item.GroupName;
                        if (Getlength(groupname) > 10) {
                            groupname = substr(groupname, 10);
                        }

                        strList += " <div class=\"info L\"  style=\"position:relative\"><span class=\"R\"><a href=\"javascript:void(0)\" id=\"GroupMoreList_" + item.RecordID + "\"    onclick=\"ShowGroupListAll('GroupMoreList','" + item.RecordID + "','" + item.ConcernUserID + "')\" title=\"" + item.GroupName + "\">" + groupname + "";

                        strList += "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/SanJ.gif\" /></a></span>";
                    }
                    strList += " <a href=\"/u_" + item.ConcernUserID + "\" class=\"Blue F14\" id=\"nikename" + item.RecordID + "\" onMouseOver=\"UserInfoShowOver(this," + item.ConcernUserID + "," + item.RecordID + ")\" >" + item.NickName + "</a>";
                    strList += ShowVerifyImg(item.ILogClass) + "<br/>";
                    strList += " <div class=\" L19  G4\">";

                    strList += " <img src=\"http://simg.instrument.com.cn/ilog/blue/images/";
                    if (item.Sex == "male") {
                        strList += "men.gif";
                    }
                    else {
                        strList += "women.jpg";
                    }
                    strList += "\"  /> " + item.City + " <br />";
                    strList += " 关注 <b><a href=\"/follow_" + item.ConcernUserID + "\" class=\"Blue\">" + item.Concernn + "</a></b> <span class=\"G9\"> | </span> ";
                    strList += " 粉丝 <b><a href=\"/fans_" + item.ConcernUserID + "\" class=\"Blue\">" + item.Fan + "</a></b> <span class=\"G9\"> | </span>";
                    strList += " 微博 <b><a href=\"/u_" + item.ConcernUserID + "\" class=\"Blue\">" + item.ILog + "</a></b>";
                    strList += " </div>";
                    if (item.IlogID != "0") {
                        strList += " <p class=\"txt ENG Fa\"> <a href=\"/tcont_" + item.ConcernUserID + "_" + item.IlogID + "\" >" + unescape(item.IlogContent) + "(" + item.intime + ")</a></p>"
                    }
                    strList += " <div class=\"WindowMenu2  L30\" id=\"GroupMoreList_" + item.RecordID + "_Menu\" style=\"position:absolute;right:-20px; top:17px; z-index:3 ;display:none;\"></div>"
                    strList += " </div><div class=\"btn R L23 G4 Tr\">";
                    if (item.ConcernState == "1") {
                        strList += " <img src=\"http://simg.instrument.com.cn/ilog/blue/images/concern.gif\" alt=\"互相关注\"  /><br />";
                    }
                    strList += " <a href=\"javascript:void(0)\" class=\"Blue\" onclick=\"ShowPageConcern_follow(" + item.RecordID + "," + item.ConcernUserID + ",'" + item.NickName + "')\">取消关注</a><br />";
                    strList += " <a href=\"javascript:void(0)\" class=\"Blue\" onclick=\"ShowPageUserReport(" + item.ConcernUserID + ")\">举报</a></div>";
                    strList += " <div class=\" Hr_1\"></div>";
                    strList += " </div><div class=\"Line_ilog\" ></div>";

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
            strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"ShowConcernListALL('" + vServiceUrl + "ILogFollow.asmx/GetList_Concern'," + i + ",45,'" + keyword + "'," + listType + ")\"  >第<font color=\"red\">" + i + "</font>页</a></li>";
        }
        else {
            strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"ShowConcernListALL('" + vServiceUrl + "ILogFollow.asmx/GetList_Concern'," + i + ",45,'" + keyword + "'," + listType + ")\"  >第" + i + "页</a></li>";
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
function nextpage(PageCurrent, RecordCount, keyword, pagesort) {
    var RecordCount_index = RecordCount;    //记录总页数

    //没翻过上一页
    if (pageindex_n <= 2) {
        if (RecordCount >= 1) {
            pageindex++;

            //重新绑定
            ShowConcernListALL("" + vServiceUrl + "ILogFollow.asmx/GetList_Concern", pageindex, 45, keyword, pagesort);
        }
    }
    else {
        if (RecordCount >= 1) {
            pageindex_n++;

            //重新绑定
            ShowConcernListALL("" + vServiceUrl + "ILogFollow.asmx/GetList_Concern", pageindex_n, 45, keyword, pagesort);
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
            ShowConcernListALL("" + vServiceUrl + "ILogFollow.asmx/GetList_Concern", pageindex_n, 45, keyword, pagesort);
        }
    }
    else {
        if (PageCurrent <= RecordCount) {
            pageindex--;

            //重新绑定
            ShowConcernListALL("" + vServiceUrl + "ILogFollow.asmx/GetList_Concern", pageindex, 45, keyword, pagesort);
        }
    }

    //如果到第一页了就隐藏上一也
    if (pageindex_n == 1) {
        pageindex_n = 2;    //索引初始化
    }
}

var showConcerntDialog;
//关注
function ShowPageConcern_follow(iuc_id, concernuserid, nikename) {
    showConcerntDialog = $.dialog({
        id: "divShowRePort",
        title: false,
        content: ShowConcernCancle(iuc_id, concernuserid, nikename),
        max: false,
        min: false,
        lock: true,
        cache: false,
        padding: 0


    });

    //取消
    $("#btnConcernCancle").click(function() {
        CloseConcern();
    });
    //确定取消
    $("#btnConcernTrue").click(function() {
        ShowConcernSubmitInfo_Follow(iuc_id, concernuserid);
    });

}

//确认取消关注提
function ShowConcernSubmitInfo_Follow(iuc_id, concernuserid) {
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
                        
                        //清除关注用户
                        $("#Concern_" + iuc_id).next().remove();
                        $("#Concern_" + iuc_id).remove();
   
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