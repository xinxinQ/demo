$(document).ready(function() {
    //获取当然用户id
    var userid = $("#userid").val();


    //加载Ilog站内导航
    funGetTopMenuService("" + vServiceUrl + "IlogTopMenu.asmx/ILogGetTopMenu", "{}", "");

    //加载Ilog用户导航

    funGetTopUserMenuService("" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenu", "{MenuLive:'0'}", "");

    //加载ilog左侧菜单
    funGetleftMenuService("" + vServiceUrl + "ILogWebLeftMenu.asmx/ILogGetHPersonalLeftMneu", "{MenuLive:'3',hUserID:'" + userid + "'}", "");

    //获取头像及用户统计基本信息
    VipILogPersonalInfo("" + vServiceUrl + "VipIlogUser.asmx/ILogGetPersonalUserInfoById", userid, 3);

    //获取
    ShowConcernListALL("" + vServiceUrl + "ILogFollow.asmx/GetList_ConcernOtherSearch", 1, 45, "", userid);
    RecordVisitHistory();
    ShowTitle("他关注的人");

});




//显示用户基本信息
function VipILogPersonalInfo(servicesUrl, otherId, vType) {

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

                var weibo = userId == otherId ? "/u" : "/u_" + otherId; //他人主页链接            

                var headInfo = "<a href=\"" + weibo + "\"><img src=\"../images/face/big/" + dataObj.face + "\" title=\"" + dataObj.nickname + "\" alt=\"" + dataObj.nickname + "\" width=\"140\" height=\"140\" class=\"img\" id=\"face\" /></a>";

                $("#headInfo").html(headInfo);

                var personalInfo = "<a href=\"" + weibo + "\" class=\"F14\"><strong>" + dataObj.nickname + "</strong></a>" + ShowVerifyImg(dataObj.memberlevel) + "<br>iLog地址：<span class=\"Fa\"><a href=\"http://ig.instrument.com.cn/\" class=\"Blue\">http://ig.instrument.com.cn/u_" + otherId + "</a></span><br>地区：" + dataObj.address + " 学校：" + dataObj.school + "<br>";

                $("#personalInfo").html(personalInfo);

                if (vType == 3) {

                    //显示，他的关注，他的关注数量
                    funShowPageUserCountInfo("" + vServiceUrl + "/VipIlogUser.asmx/ILogGetUserInfoById", otherId, dataObj.nickname, dataObj.sex);
                }

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


//显示他的关注数
function funShowPageUserCountInfo(url, otherId, nickName, sex) {

    $.ajax({
        url: "" + url + "",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{userid:" + otherId + ",i:" + rand + "}",
        success: function(json) {



            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            //获取当前用户id

            if (dataObj.UrlState == 1) {

                ShowTitle(nickName + "关注的人");
                if (sex == "female") {

                    $("#hfollow_title").html("<b>她的关注</b>");

                }
                else {

                    $("#hfollow_title").html("<b>他的关注</b>");

                }
                $("#FollowNikeName").text(nickName + "关注了" + dataObj.concern + "人")

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


//搜索列表
//servicesUrl：服务地址
//PageCurrent：当前页面
//pagesize：每页多少行数据
//keyword：关键字
function ShowConcernListALL(servicesUrl, PageCurrent, pagesize, keyword, userID) {

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
        data: "{PageCurrent:'" + PageCurrent + "',PageSize:'" + pagesize + "',keyWord:'" + keyword + "',otherUserID:'" + userID + "'}",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var strList = "";

            //获取当然用户id
            var logouserid = $.cookie('useid');

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
                        strList += " 还没有关注的朋友<br></div>";
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
                        sowhpage_ul(PageCurrent, item.RecordCount, keyword, userID);   //分页
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
                    strList += " <div class=\"pic L\"><a href=\"/u_" + item.ConcernUserID + "\" >";
                    strList += " <img src=\"" + (item.face != "" ? "images/face/small/" + item.Face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"" + item.NickName
                    + "\" class=\"img\" id=\"user" + item.RecordID + "\" onmouseover=\"UserInfoShowOver(this," + item.ConcernUserID + "," + item.RecordID + ")\"/></a><br />";
                    strList += " <a href=\"javascript:void(0)\" onclick=\"showdialog_u('" + item.NickName + "',0);\">";
                    strList += " <img src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_mail.gif\" alt=\"站短\"  /></a></div>";
                    strList += " <div class=\"info L\" style=\"position:relative\" id=\"infol_" + item.RecordID + "\">";
                    strList += " <a href=\"/u_" + item.ConcernUserID + "\" class=\"Blue F14\" id=\"nikename" + item.RecordID + "\" onmouseover=\"UserInfoShowOver(this," + item.ConcernUserID + "," + item.RecordID + ")\">" + item.NickName + "</a>";
                    strList += ShowVerifyImg(item.ILogClass) + "<br/>";
                    strList += " <div class=\" L19  G4\">";
                    if (item.Sex == "male") {
                        strList += " <img src=\"http://simg.instrument.com.cn/ilog/blue/images/men.gif\"  /> " + item.City + " <br />";
                    }
                    else {
                        strList += " <img src=\"http://simg.instrument.com.cn/ilog/blue/images/women.jpg\"  /> " + item.City + " <br />";
                    }
                    strList += " 关注 <b><a href=\"/follow_" + item.ConcernUserID + "\" class=\"Blue\">" + item.Concernn + "</a></b> <span class=\"G9\"> | </span> ";
                    strList += " 粉丝 <b><a href=\"/fans_" + item.ConcernUserID + "\" class=\"Blue\">" + item.Fan + "</a></b> <span class=\"G9\"> | </span>";
                    strList += " 微博 <b><a href=\"/u_" + item.ConcernUserID + "\" class=\"Blue\">" + item.ILog + "</a></b>";
                    strList += " </div>";
                    if (item.IlogID != "0") {
                        strList += " <p class=\"txt ENG Fa\"> <a href=\"tcont_" + item.ConcernUserID + "_" + item.IlogID + "\" >" + unescape(item.IlogContent) + "" + item.intime + "</a></p>"
                    }
                    strList += " </div><div class=\"btn R L23 G4 Tr\">";
                    if (item.ConcernState == "1") {
                        strList += " <img src=\"http://simg.instrument.com.cn/ilog/blue/images/concern.gif\" alt=\"已互相关注\"  /><br />";
                    }
                    else if (item.ConcernState == "0") {
                        strList += " <img src=\"http://simg.instrument.com.cn/ilog/blue/images/concern_y.gif\" alt=\"已关注\"  /><br />";
                    }
                    else if (item.ConcernState == "2") {
                        strList += "<span id=\"cencernimg_" + item.RecordID + "\"><a href=\"javascript:void(0)\" ID=\"hr_fan_" + item.RecordID + "\" onclick=\"ShowAddFollowTrueTaConcern(" + item.RecordID + "," + item.ConcernUserID + ",'" + item.NickName + "')\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/concern_j.gif\" alt=\"加关注\"  /></a></span><br/>"
                    }
                    if (logouserid != item.ConcernUserID) {
                        strList += " <a href=\"javascript:void(0)\" class=\"Blue\" onclick=\"ShowPageUserReport(" + item.ConcernUserID + ")\">举报</a></div>";
                    }
                    else {
                        strList += " </div>";
                    }
                    strList += " <div class=\"Hr_1\"></div>";
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
function sowhpage_ul(PageCurrent, RecordCount, keyword, userID) {
    var sowhpage_div = $("#Follow_Page");

    var strShowPage = "";

    //当然页码等于总也数就隐藏下一页按钮
    strShowPage += "<span class=\"R\" " + (PageCurrent == RecordCount ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"nextbtn\" ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_page.gif\" onclick=\"nextpage(" + PageCurrent + "," + RecordCount + ",'" + keyword + "','" + userID + "')\" alt=\"下一页\" /></span>";

    strShowPage += "<span class=\"R span\" style=\"position:relative\" id='selOption' ><a href=\"javascript:void(0);\"  class=\"Blue\">第" + PageCurrent + "页</a><img src=\"http://simg.instrument.com.cn/ilog/blue/images/SanJ.gif\" />";

    strShowPage += "<ul class=\"pageBox R Line Fa BrWh\" id=\"selOption_menu\" style=\" position:absolute; bottom:16px; left:15px;display:none;\">";

    for (var i = RecordCount; i >= 1; i--) {
        if (PageCurrent == i) {
            strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"if (!LoginDiv(16)){ return false;}ShowConcernListALL('" + vServiceUrl + "ILogFollow.asmx/GetList_ConcernOtherSearch'," + i + ",   45,'" + keyword + "','" + userID + "')\"  >第<font color=\"red\">" + i + "</font>页</a></li>";
        }
        else {
            strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"if (!LoginDiv(16)){ return false;}ShowConcernListALL('" + vServiceUrl + "ILogFollow.asmx/GetList_ConcernOtherSearch'," + i + ",   45,'" + keyword + "','" + userID + "')\"  >第" + i + "页</a></li>";
        }
    }

    strShowPage += "</ul>";
    strShowPage += "</span>";

    //当前页码小于1就隐藏上一页页码
    strShowPage += "<span class=\"R\" " + (PageCurrent <= 1 ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"upbtn\"  ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_pageT.gif\" onclick=\"uppage(" + PageCurrent + "," + RecordCount + ",'" + keyword + "','" + userID + "')\" alt=\"上一页\" /></span>";

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
function nextpage(PageCurrent, RecordCount, keyword, userID) {

    if (!LoginDiv(16)) {
        return;
    }

    var RecordCount_index = RecordCount;    //记录总页数

    //没翻过上一页
    if (pageindex_n <= 2) {
        if (RecordCount >= 1) {
            pageindex++;

            //重新绑定
            ShowConcernListALL("" + vServiceUrl + "ILogFollow.asmx/GetList_ConcernOtherSearch", pageindex, 45, keyword, userID);
        }
    }
    else {
        if (RecordCount >= 1) {
            pageindex_n++;

            //重新绑定
            ShowConcernListALL("" + vServiceUrl + "ILogFollow.asmx/GetList_ConcernOtherSearch", pageindex_n, 45, keyword, userID);
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
function uppage(PageCurrent, RecordCount, keyword, userID) {

    if (!LoginDiv(16)) {
        return;
    }

    var pageindex_n = PageCurrent;    //记录总页数

    if (pageindex == 1) {
        if (PageCurrent <= RecordCount) {
            pageindex_n--;

            //重新绑定
            ShowConcernListALL("" + vServiceUrl + "ILogFollow.asmx/GetList_ConcernOtherSearch", pageindex_n, 45, keyword, userID);
        }
    }
    else {
        if (PageCurrent <= RecordCount) {
            pageindex--;

            //重新绑定
            ShowConcernListALL("" + vServiceUrl + "ILogFollow.asmx/GetList_ConcernOtherSearch", pageindex, 45, keyword, userID);
        }
    }

    //如果到第一页了就隐藏上一也
    if (pageindex_n == 1) {
        pageindex_n = 2;    //索引初始化
    }
}

