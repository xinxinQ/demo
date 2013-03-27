
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

    //读取隐藏域的中的id参数
    var id = $("#id_a").val();

    if (id > 0) {
        $("#id_").val(id);  //记录到隐藏域

        //加载站短
        GetList2("" + vServiceUrl + "VipMail.asmx/GetAllMailList", 1, 45, id);

        //获取站短用户的关系
        GetMailRelationship("" + vServiceUrl + "VipMail.asmx/GetTowhoById", id);
    }
    else {
        //真实地址

        //伪静态地址
        window.location.href = "Msg";
    }


    //收件人下拉框
    $("#keyword_s").keyup(function() {
        //回车键搜提示无效
        if (!isEnterKey()) {
            searchtowho2();
        }

        //上下键处理
        funListBeginUp_m(event);
    });


    //页面title
    ShowTitle("站短对话列表");

    //获取回车事件
    getenterevent_m();

});

//获取站短用户的关系
//id：收件人流水号
function GetMailRelationship(servicesUrl, id) {
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
        data: "{id:'" + id + "'}",
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
                        $("#tohow_a").html("加载错误");
                    }
                    else if (item.State == "2")  //无数据不显示分页控件
                    {
                        $("#tohow_a").html("非法收件人");

                        return false;   //无数据不再往下执行
                    }
                    else {
                        return true;
                    }
                }
                else {
                    if (item.Towho != undefined) {
                        $("#tohow_a").html(item.Towho);         //发信关系
                        $("#tohow_a").attr("href", "/u_" + item.towhoid);
                        $("#tohow_a2").html(item.Towho);        //回复框上的收信人
                        $("#RowsCount").html("&nbsp;&nbsp;" + item.m_number + "条站短");    //站短数量
                        $("#tohow_h").val(item.towhoid);   //收信人id

                        //开始标签
                        var strFaceBox = "<div style=\" position:relative\"></div>";
                        strFaceBox += "<a href=\"javascript:void(0);\"  >";

                        //结束标签
                        var strFaceBox_a = "</a>";

                        $("#face_a").html(strFaceBox + "<img id=\"user" + item.towhoid + "\" onmouseover=\"UserInfoShowOver(this," + (userid == "" ? 0 : userid) + "," + item.towhoid + ")\" src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"头像\" class=\"L Img\" />" + strFaceBox_a);        //当前用户头像

                        return true;
                    }
                }
            });

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

//搜索列表
//servicesUrl：服务地址
//PageCurrent：当前页面
//pagesize：每页多少行数据
//keyword：关键字
//id：收件人id
function GetList2(servicesUrl, PageCurrent, pagesize, id) {
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
        data: "{PageCurrent:'" + PageCurrent + "',PageSize:'" + pagesize + "',id:'" + id + "'}",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var strList = "";

            //用户昵称效果当前用户昵称上不用弹出
            var strnickname = "";

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

                        $("#RowsCount").html("&nbsp;&nbsp;0条站短");  //数据总数

                        //没有搜索到数据
                        $("#list_div").html("");

                        //真实地址
                        //window.location.href = "Messages.aspx";

                        //伪静态地址
                        window.location.href = "Msg";

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
                    sowhpage_ul(PageCurrent, item.RecordCount, "", 0, id);   //分页
                    return true;
                }
                if (item.m_number != undefined) {
                    $("#RowsCount").html("&nbsp;&nbsp;" + item.m_number + "条站短"); //联系人数（目前未定如何处理）

                    return true;
                }

                //如果遍历到不存在节点就不构建
                if (item.id != undefined) {

                    if (item.userid == userid)    //是当前用户
                    {
                        //伪静态
                        strPersonUlr = "u";
                    }
                    else //他人主页
                    {
                        //伪静态
                        strPersonUlr = "u_" + item.userid;
                    }


                    if (item.fromwho_ == userid)    //如果收件人是对方就是发出的站短
                    {
                        //构建数据
                        strList += "<div id=\"div" + item.id + "\">";
                        strList += "<div class=\"messages_bot Centent\" >";

                        strList += " <div class=\"LEFTBOX R\"><a href=\"/u\" ><img  id=\"user" + item.id + "\" onmouseover=\"UserInfoShowOver(this," + (item.fromwho_ == "" ? 0 : item.fromwho_) + "," + item.id + ")\"  src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"头像\" class=\"L Img\" /></a></div>";
                        strList += "<div class=\"RIGHTBOX L\"> ";
                        strList += " <div class=\"Sound1\">";
                        strList += "  <div class=\"Sound2\"> ";

                        //当前用户
                        if (item.fromwho_ == userid) {
                            strnickname = "<span class=\"Blue\">我</span>";
                        }
                        else    //非当前用户
                        {
                            strnickname = "<a href=\"/u\" id=\"uu" + item.id + "\" onmouseover=\"UserInfoShowOver(this," + (item.fromwho_ == "" ? 0 : item.fromwho_) + "," + item.id + ")\" class=\"Blue\">" + item.fromwho + "</a>";

                        }

                        strList += " <div class=\"Sound3\" style=\" position:relative\" ><p class=\"F12 G6 L22\">" + strnickname + "：" + unescape(item.content) + "</p> ";
                        strList += " <div class=\"Hr_4\"></div> ";
                        strList += " <div class=\" G9\"><span class=\"R\"><a href=\"javascript:void(0);\" onclick = \"replyfocus();\" class=\"Blue\">回复</a> | <a href=\"javascript:void(0);\" onclick=\"showdialog_d(" + item.id + "," + item.fromwho_ + "," + item.userid + ")\" class=\"Blue\">删除</a></span>" + item.intime + "</div> ";

                        strList += " </div> ";
                        strList += " </div> ";
                        strList += " <span class=\"Sound_span\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/san_2.gif\" /></span> ";
                        strList += " </div> ";
                        strList += " </div> ";
                        strList += " <DIV class=\"Hr_10\"></DIV> ";
                        strList += " </div> ";
                        strList += " </div> ";
                    }
                    else                    //回信
                    {
                        strList += "<div id=\"div" + item.id + "\">";    //操作容器
                        strList += "<DIV class=\"Hr_10\"></DIV>";
                        strList += " <div class=\"messages_top Centent\">";
                        strList += "<div class=\"LEFTBOX L\"><a href=\"/u_" + item.fromwho_ + "\" ><img src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"头像\" class=\"L Img\" id=\"user" + item.id + "\" onmouseover=\"UserInfoShowOver(this," + (item.fromwho_ == "" ? 0 : item.fromwho_) + "," + item.id + ")\"/></a></div>";


                        strList += " <div class=\"RIGHTBOX R\">";
                        strList += " <div class=\"Mound1\"> ";
                        strList += " <div class=\"Mound2\"> ";
                        strList += " <div class=\"Mound3\" style=\" position:relative\"  ><p class=\"F12 G6 L22 P10\"><a href=\"/u_" + item.fromwho_ + "\"  id=\"uu" + item.id + "\" onmouseover=\"UserInfoShowOver(this," + (item.fromwho_ == "" ? 0 : item.fromwho_) + "," + item.id + ")\" class=\"Blue\">" + item.fromwho + "</a>：" + unescape(item.content) + "</p> ";

                        strList += " <div class=\" G9 \" style=\"padding:0 10px\"><span class=\"R\"><a href=\"javascript:void(0);\" onclick = \"replyfocus();\" class=\"Blue\">回复</a> | <a href=\"javascript:void(0);\" onclick=\"showdialog_d(" + item.id + "," + item.userid + "," + item.fromwho_ + ")\" class=\"Blue\">删除</a></span>" + item.intime + "</div> ";

                        strList += " </div> ";
                        strList += " </div> ";
                        strList += " <span class=\"Mound_span\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/san_3.gif\" /></span> ";
                        strList += " </div> ";
                        strList += " </div> ";
                        strList += " <DIV class=\"Hr_10\"></DIV> ";
                        strList += " </div> ";
                        strList += " </div> ";
                    }


                }
            });

            $("#list_div").html("");

            $("#list_div").append(strList);
            scroll(0, 0);  //翻页后需要回到顶部

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


//获取回信列表
//id：发信id
function GetRemailList(servicesUrl, id) {

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
        data: "{id:'" + id + "'}",
        //成功
        success: function(json, status) {
            var strList = "";
            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            //获取当然用户id
            var userid = $.cookie('useid');

            //循环获取值
            $.each(dataObj.List, function(idx, item) {
                if (idx == 0) {
                    if (item.State == "0") {
                        //                        alert("加载异常！");
                    }
                    else if (item.State == "2") {
                        return false;   //无数据跳出
                    }
                    else    //如果成功就返回循环里面做拼接，该“return”不会跳出循环
                    {
                        return true;
                    }
                }

                //回复站短
                if (item.id_ != undefined) {
                    //构建数据
                    strList += "<div id=\"div" + item.id_ + "\">";    //操作容器
                    strList += "<DIV class=\"Hr_10\"></DIV>";
                    strList += " <div class=\"messages_top\">";
                    strList += "<div class=\"LEFTBOX L\"><a href=\"#\"><img src=\"http://ww4.sinaimg.cn/thumb50/6f013cf3jw1dqmj6n79d2j.jpg\" alt=\"头像\" class=\"L Img\" /></a></div>";
                    strList += " <div class=\"RIGHTBOX R\">";
                    strList += " <div class=\"Mound1\"> ";
                    strList += " <div class=\"Mound2\"> ";
                    strList += " <div class=\"Mound3\"><p class=\"F12 G6 L22 P10\"><a href=\"javascript:void(0);\" id=\"uu" + item.id + "\" onmouseover=\"UserInfoShowOver(this," + (item.userid == "" ? 0 : item.userid) + "," + item.id + ")\" class=\"Blue\">" + item.towho_ + "</a>：" + item.content_ + "</p> ";
                    strList += " <div class=\" G9 \" style=\"padding:0 10px\"><span class=\"R\"><a href=\"javascript:void(0);\" onclick=\"showdialog_d(" + item.id_ + ")\" class=\"Blue\">删除</a></span>" + item.intime_ + "</div> ";
                    strList += " </div> ";
                    strList += " </div> ";
                    strList += " <span class=\"Mound_span\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/san_3.gif\" /></span> ";
                    strList += " </div> ";
                    strList += " </div> ";
                    strList += " <DIV class=\"Hr_10\"></DIV> ";
                    strList += " </div> ";
                    strList += " </div> ";

                }
            });

            $("#div" + id).append(strList); //追加回复站短
        },
        //出错调试
        error: function(result, status) {
            if (status == 'error') {
                //                alert(result.responseText);
            }
        },
        complete: function() {
        }

    });


}



//搜索列表
//servicesUrl：服务地址
//PageCurrent：当前页面
//pagesize：每页多少行数据
//keyword：关键字
//id：收件人流水号
function GetSearchList(servicesUrl, PageCurrent, pagesize, keyword, id) {

    $("#sowhpage_div").html("");    //初始化分页

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
        data: "{PageCurrent:'" + PageCurrent + "',PageSize:'" + pagesize + "',keyword:'" + keyword + "',id:'" + id + "'}",
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
                        $("#RowsCount").html("找到0条结果");  //数据总数

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

                if (item.RecordCount > 1)   //拿到总页数
                {
                    sowhpage_ul(PageCurrent, item.RecordCount, "", 0, id);   //分页
                    return true;
                }
                if (item.mailcount != undefined) {
                    $("#RowsCount").html(item.mailcount + "条站短"); //联系人数（目前未定如何处理）
                    return true;
                }

                //如果遍历到不存在节点就不构建
                if (item.id != undefined) {
                    if (item.mailid == 0)    //发信
                    {
                        //构建数据
                        strList += "<div id=\"div" + item.id + "\">";
                        strList += "<div class=\"messages_bot Centent\" >";
                        strList += " <div class=\"LEFTBOX R\"><a href=\"javascript:void(0);\" ><img src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"头像\" class=\"L Img\" id=\"user" + item.id + "\" onmouseover=\"UserInfoShowOver(this," + (item.userid == "" ? 0 : item.userid) + "," + item.id + ")\"/></a></div>";
                        strList += "<div class=\"RIGHTBOX L\"> ";
                        strList += " <div class=\"Sound1\">";
                        strList += "  <div class=\"Sound2\"> ";
                        strList += " <div class=\"Sound3\" style=\" position:relative\" ><p class=\"F12 G6 L22\"><a href=\"javascript:void(0);\" id=\"uu" + item.id + "\" onmouseover=\"UserInfoShowOver(this," + (item.userid == "" ? 0 : item.userid) + "," + item.id + ")\" class=\"Blue\">" + item.fromwho + "</a>：" + unescape(item.content) + "</p> ";
                        strList += " <div class=\"Hr_4\"></div> ";
                        strList += " <div class=\" G9\"><span class=\"R\"><a href=\"javascript:void(0);\" onclick = \"replyfocus();\" class=\"Blue\">回复</a> | <a href=\"javascript:void(0);\" onclick=\"showdialog_d(" + item.id + ")\" class=\"Blue\">删除</a></span>" + item.intime + "</div> ";

                        strList += " </div> ";
                        strList += " </div> ";
                        strList += " <span class=\"Sound_span\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/san_2.gif\" /></span> ";
                        strList += " </div> ";
                        strList += " </div> ";
                        strList += " <DIV class=\"Hr_10\"></DIV> ";
                        strList += " </div> ";
                        strList += " </div> ";
                    }
                    else                    //回信
                    {
                        strList += "<div id=\"div" + item.id + "\">";    //操作容器
                        strList += "<DIV class=\"Hr_10\"></DIV>";
                        strList += " <div class=\"messages_top Centent\">";
                        strList += "<div class=\"LEFTBOX L\"><a href=\"javascript:void(0);\" ><img id=\"user" + item.id + "\" onmouseover=\"UserInfoShowOver(this," + (item.userid == "" ? 0 : item.userid) + "," + item.id + ")\" src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"头像\" class=\"L Img\" /></a></div>";
                        strList += " <div class=\"RIGHTBOX R\">";
                        strList += " <div class=\"Mound1\"> ";
                        strList += " <div class=\"Mound2\"> ";
                        strList += " <div class=\"Mound3\" style=\" position:relative\"  ><p class=\"F12 G6 L22 P10\"><a href=\"javascript:void(0);\" id=\"uu" + item.id + "\" onmouseover=\"UserInfoShowOver(this," + (item.userid == "" ? 0 : item.userid) + "," + item.id + ")\" class=\"Blue\">" + item.towho + "</a>：" + unescape(item.content) + "</p> ";
                        strList += " <div class=\" G9 \" style=\"padding:0 10px\"><span class=\"R\"><a href=\"javascript:void(0);\" onclick = \"replyfocus();\" class=\"Blue\">回复</a> | <a href=\"javascript:void(0);\" onclick=\"showdialog_d(" + item.id + ")\" class=\"Blue\">删除</a></span>" + item.intime + "</div> ";

                        strList += " </div> ";
                        strList += " </div> ";
                        strList += " <span class=\"Mound_span\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/san_3.gif\" /></span> ";
                        strList += " </div> ";
                        strList += " </div> ";
                        strList += " <DIV class=\"Hr_10\"></DIV> ";
                        strList += " </div> ";
                        strList += " </div> ";
                    }


                    //获取获取回复的站短
                    //                    if (item.mailid == 0) 
                    //                    {
                    //                        GetRemailList("" + vServiceUrl + "VipMail.asmx/GetReMailList", item.id);
                    //                    }
                }
            });

            $("#list_div").html("");
            $("#list_div").append(strList);
            scroll(0, 0);  //翻页后需要回到顶部

        },
        //出错调试         
        error: function(x, e) {

            //alert("加载异常");
            //var obj = x;
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
//id：回帖流水号
function sowhpage_ul(PageCurrent, RecordCount, keyword, ation, id) {
    var sowhpage_div = $("#sowhpage_div");

    var strShowPage = "";

    //当然页码等于总也数就隐藏下一页按钮
    //读取
    if (ation == 0) {
        strShowPage += "<span class=\"R\" " + (PageCurrent == RecordCount ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"nextbtn\" ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_page.gif\" onclick=\"nextpage(" + PageCurrent + "," + RecordCount + ",''," + ation + "," + id + ")\" alt=\"下一页\" /></span>";
    }
    else //搜索
    {
        strShowPage += "<span class=\"R\" " + (PageCurrent == RecordCount ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"nextbtn\" ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_page.gif\" onclick=\"nextpage(" + PageCurrent + "," + RecordCount + ",'" + keyword + "'," + ation + "," + id + ")\" alt=\"下一页\" /></span>";
    }

    strShowPage += "<span class=\"R span\" style=\"position:relative\" ><a href=\"javascript:void(0);\"  id='selOption' class=\"Blue\">第" + PageCurrent + "页</a><img src=\"http://simg.instrument.com.cn/ilog/blue/images/SanJ.gif\" />";

    strShowPage += "<ul class=\"pageBox R Line Fa BrWh\" id=\"selOption_menu\" style=\" position:absolute; bottom:16px; left:15px;display:none;\">";

    for (var i = RecordCount; i >= 1; i--) {
        if (PageCurrent == i) {
            if (ation == 0)  //读取
            {
                strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"GetList2('" + vServiceUrl + "VipMail.asmx/GetAllMailList'," + i + ",45," + id + ")\"  >第<font color=\"red\">" + i + "</font>页</a></li>";
            }
            else            //搜索                                                                      
            {
                strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"GetSearchList('" + vServiceUrl + "VipMail.asmx/GetSearchMailList'," + i + ",45,'" + keyword + "'," + id + ")\"  >第<font color=\"red\">" + i + "</font>页</a></li>";
            }
        }
        else {
            if (ation == 0) {
                strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"GetList2('" + vServiceUrl + "VipMail.asmx/GetAllMailList'," + i + ",45," + id + ")\"  >第" + i + "页</a></li>";
            }
            else {
                strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"GetSearchList('" + vServiceUrl + "VipMail.asmx/GetSearchMailList'," + i + ",45,'" + keyword + "'," + id + ")\"  >第" + i + "页</a></li>";
            }
        }
    }

    strShowPage += "</ul>";
    strShowPage += "</span>";

    //当前页码小于1就隐藏上一页页码

    if (ation == 0)  //读取
    {
        strShowPage += "<span class=\"R\" " + (PageCurrent <= 1 ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"upbtn\"  ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_pageT.gif\" onclick=\"uppage(" + PageCurrent + "," + RecordCount + ",''," + ation + "," + id + ")\" alt=\"上一页\" /></span>";
    }
    else            //搜索
    {
        strShowPage += "<span class=\"R\" " + (PageCurrent <= 1 ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"upbtn\"  ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_pageT.gif\" onclick=\"uppage(" + PageCurrent + "," + RecordCount + ",'" + keyword + "'," + ation + "," + id + ")\" alt=\"上一页\" /></span>";
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
//function MenuDivShow(showdiv) {
//    $('#' + showdiv + '_menu').mouseover(function() { $(this).show(); });
//    $('#' + showdiv + '_menu').mouseout(function() { $(this).hide(); });
//}

//记录当前页码
var pageindex = 1;

//下一页处理
//PageCurrent：当前页码
//RecordCount：总页数
//keyword：搜索关键字
//ation：操作类型
//id：回信人流水号
function nextpage(PageCurrent, RecordCount, keyword, ation, id) {
    var RecordCount_index = RecordCount;    //记录总页数

    //没翻过上一页
    if (pageindex_n <= 2) {
        if (RecordCount >= 1) {
            pageindex++;

            if (ation == 0)       //读取
            {
                //重新绑定
                GetList2("" + vServiceUrl + "VipMail.asmx/GetAllMailList", pageindex, 45, id);
            }
            else            //搜索
            {
                //重新绑定
                GetSearchList("" + vServiceUrl + "VipMail.asmx/GetSearchMailList", pageindex, 45, keyword, id);
            }
        }
    }
    else {
        if (RecordCount >= 1) {
            pageindex_n++;

            if (ation == 0)       //读取
            {
                //重新绑定
                GetList2("" + vServiceUrl + "VipMail.asmx/GetAllMailList", pageindex_n, 45, id);
            }
            else                //搜索
            {
                //重新绑定
                GetSearchList("" + vServiceUrl + "VipMail.asmx/GetSearchMailList", pageindex_n, 45, keyword, id);
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
function uppage(PageCurrent, RecordCount, keyword, ation, id) {
    var pageindex_n = PageCurrent;    //记录总页数

    if (pageindex == 1) {
        if (PageCurrent <= RecordCount) {
            pageindex_n--;

            if (ation == 0)  //读取
            {
                //重新绑定
                GetList2("" + vServiceUrl + "VipMail.asmx/GetAllMailList", pageindex_n, 45, id);
            }
            else            //搜索
            {
                //重新绑定
                GetSearchList("" + vServiceUrl + "VipMail.asmx/GetSearchMailList", pageindex_n, 45, keyword, id);
            }
        }
    }
    else {
        if (PageCurrent <= RecordCount) {
            pageindex--;

            if (ation == 0)  //读取
            {
                //重新绑定
                GetList2("" + vServiceUrl + "VipMail.asmx/GetAllMailList", pageindex, 45, id);
            }
            else            //搜索
            {
                //重新绑定
                GetSearchList("" + vServiceUrl + "VipMail.asmx/GetSearchMailList", pageindex, 45, keyword, id);
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

    //    //获取数据
    //    GetSearchList("" + vServiceUrl + "VipMail.asmx/GetSearchMailList", 1, 45, strValue,$("#id_").val());



    //伪静态地址
    window.location.href = "msg_" + strValue;

}

//跳转删除也
function GoMailDel() {


    //伪静态地址
    location.href = 'Msgdel_' + $("#id_").val()
}

//获取站短用户的关系
//id：收件人流水号
//fromwhoid：发件人
//towhoid：收件人
function VipMailDel_a(id, fromwhoid, towhoid) {

    //        alert(id);
    //        alert(fromwhoid);
    //        alert(towhoid);

    $.ajax({
        //请求WebService Url         
        url: "" + vServiceUrl + "VipMail.asmx/VipMailDel",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        cache: false,
        //请求参数              
        data: "{id:'" + id + "',fromwhoid:'" + fromwhoid + "',towhoid:'" + towhoid + "'}",
        //成功           
        success: function(json) {


            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var strList = "";

            //获取当然用户id
            //var userid = $.cookie('useid'); 

            //循环获取值
            $.each(dataObj.List, function(idx, item) {

                //                alert(item.State);

                if (idx == 0) {
                    if (item.State == "0") {
                        alert("加载错误！");
                    }
                    else if (item.State == "2")  //无数据不显示分页控件
                    {
                        showTipe("未找到收件人！", 0);

                        return false;   //无数据不再往下执行
                    }
                    else {
                        return true;
                    }
                }
                else {
                    //                    alert(item.exec);

                    if (item.exec != undefined) {
                        $("#div" + id).remove();    //移除标签

                        closeDivlog();      //关闭对话框

                        //重新绑定站短
                        $("#RowsCount").html("&nbsp;&nbsp;" + item.m_number + "条站短");

                        //发信人与收信收的站短数
                        //alert(item.m_number);

                        if (item.m_number <= 0)      //如果该页的站短都被删除了，就跳转到第一个页面
                        {
                            //真实地址
                            //window.location.href = "Messages.aspx";

                            //伪静态地址
                            window.location.href = "Msg";
                        }

                        return true;
                    }
                }
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



//悬浮框对象
var objDivlog;
//id：站短流水号
function showdialog_d(id, fromwhoid, towhoid) {
    //悬浮框函数
    objDivlog = $.dialog({
        id: "Divlog",
        title: false,
        content: "url:Ajax/AjaxMessagesDel.aspx?id=" + id + "&fromwhoid=" + fromwhoid + "&towhoid=" + towhoid,
        max: false,
        min: false,
        lock: true,
        cache: false,
        width: 352,
        height: 96

    });
}

//关闭弹出的窗口
function closeDivlog() {
    objDivlog.close();
}

//让回复框获取焦点
function replyfocus() {
    scroll(0, 0);  //翻页后需要回到顶部
    $("#content_i").focus();
}