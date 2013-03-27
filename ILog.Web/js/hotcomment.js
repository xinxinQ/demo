
$(document).ready(function() {


    //加载Ilog站内导航
    funGetTopMenuService("" + vServiceUrl + "IlogTopMenu.asmx/ILogGetTopMenu", "{}", "");
    //加载Ilog用户导航
    funGetTopUserMenuService("" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenu", "{MenuLive:'0'}", "");

    //
    GetIlogList(1, 10, 0);

    ShowTitle("随便看看-热门评论");

    //得到每日名人榜列表
    GetFamousUserList();
    //得到每日草根榜列表
    GetCommonUserList();

});

function changeType(type) {
    if (type == 0) {
        $("#hrefDay").html("<strong>今日热门评论</strong>");
        $("#hrefDay").removeClass("Blue");
        $("#hrefWeek").addClass("Blue");
        $("#hrefWeek").html("一周热门评论");
    }
    else {
        $("#hrefDay").html("今日热门评论");
        $("#hrefDay").addClass("Blue");
        $("#hrefWeek").removeClass("Blue");
        $("#hrefWeek").html("<strong>一周热门评论</strong>");
    }
    GetIlogList(1, 10, type);

}

//得到博文列表
function GetIlogList(PageCurrent, pagesize, type) {
    $.ajax({
        //请求WebService Url
        url: vServiceUrl + "ILogComment.asmx/GetHotCommentListJsonStr",
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

            var userid = $.cookie("useid");

            var strList = "";
            //循环获取值
            $.each(dataObj.List, function(idx, item) {
                if (idx == 0) {
                    if (item.State == "0") {
                        $("#divContent").html("加载错误");
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
                    sowhpage_ul(PageCurrent, item.RecordCount, 0, "",pagesize);   //分页
                    return true;
                }
                else if (item.RecordCount == 1) //显示条数页数
                {
                    $("#sowhpage_div").html("");    //去掉分页

                    return true;
                }

                //如果遍历到不存在节点就不构建
                if (item.io_id != undefined) {

                    //评论类型判断
                    var pinlun = item.is_isoriginal == 0 ? 2 : 1;


                    var url = "";
                    var deleteHtml = "";

                    //判断自己或是他人
                    if (userid == item.userid) {

                        url = "/u";

                        contentUrl = "/cont_" + item.iso_id;

                        deleteHtml = "<a href=\"javascript:void(0);\" onclick=\"showBlogResult(" + item.iso_id + ");\" class=\"Blue\">删除</a> | ";

                    } else {

                        url = "/u_" + item.userid;

                        contentUrl = "/tcont_"+item.userid+"_" + item.iso_id;

                    }


                    var listtype = "e";
                    if (listtype == "1") {
                        listtype = "ee";
                    }

                    //构建数据
                    strList += "<div class=\"Hr_10\">";
                    strList += "</div>";
                    strList += "<div class=\"Centent\">";
                    strList += "<div class=\"L sum Tc G6\">";
                    strList += "<span class=\"publish_num Orange\">" + item.ih_commentnum + "</span><div class=\"Hr_3\">";
                    strList += "</div>";
                    strList += "评论</div>";
                    strList += "<a href=\"" + url + "\" >";
                    strList += "<img src=\"images/face/small/" + item.face + "\" alt=\"" + item.nickname + "\" class=\"L Img\" id=\"user" 
                    + item.io_id + "\"  onmouseover=\"UserInfoShowOver(this," + item.userid + ","
                    + item.io_id + ")\" /></a>";
                    strList += "<div class=\"Txt R WidBox\">";
                    strList += "<p class=\"F14 G6 L26\">";
                    strList += "<a href=\"" + url + "\" id=\"nickname" + item.io_id + "\" class=\"Blue\" onmouseover=\"UserInfoShowOver(this," + item.userid + ","
                    + item.io_id + ")\" >" + item.nickname + "</a>";

                    var imgMemberLevel = ShowVerifyImg(item.level);

                    strList += imgMemberLevel;

                    strList += "：" + unescape(item.io_content) + "</p>";
                    strList += "<div id=\"div" + item.io_id + "\"></div>";
                    strList += "<div class=\"Hr_10\">";
                    strList += "</div>";

                    if (item.io_haspic == 1) //有图片
                    {
                        GetPic(vServiceUrl + "ILogPic.asmx/GetPic", item.io_id, item.io_id);
                    }
                    strList += "<div class=\"Hr_10\">";
                    strList += "</div>";
                    strList += "<div class=\"Hr_10\">";
                    strList += "</div>";
                    strList += "<div class=\"G9 Fa Info\">";
                    strList += "<span class=\"L\"><a href=\"" + contentUrl + "\" class=\"Blue\">" + item.intime + "</a> &nbsp;来自<a href=\"" + item.is_url + "\" onclick=\"return LoginDiv(16);\">" + item.is_name + "</a>";
                    strList += "</span><span class=\"R\"><a class=\"Blue\" href=\"javascript:checkForWard("
                    + item.userid + "," + item.iso_id + "," + "1," + item.io_spreadnum + ",'" + listtype + "');\"  >转发(" + (item.io_spreadnum == "" ? 0 : item.io_spreadnum) + ")</a> | ";
                    strList += "<a class=\"Blue\" id=\"replyCount" + item.iso_id + "\" href=\"javascript:checkComment(0,'hotc" + item.iso_id + "',"
                    + item.iso_id + "," + "1," + (item.io_commentnum == "" ? 0 : item.io_commentnum) + ");\" >评论(" + (item.io_commentnum == "" ? 0 : item.io_commentnum) + ")</a>";
                    if (userid != item.userid) {
                        strList += " | <a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"ShowPageReport(" + item.iso_id + ",'" + item.nickname + "','" + item.face + "','" + item.io_content + "')\" >举报</a>";
                    }
                    strList += "</span></div>";

                    strList += "<div id=\"hotc" + item.iso_id + "\" ></div>";

                    strList += "</div>";
                    strList += "<div class=\"Hr_1\">";
                    strList += "</div>";
                    strList += "<div class=\"Line_ilog\">";
                    strList += "</div>";
                    strList += " </div>";
                }
            });
            $("#divContent").html(strList);

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




//得到每日名人榜列表
function GetFamousUserList() {
    ajaxurl = vServiceUrl + "VipILog.asmx/VipIlogGetFamousList";
    var userid = $.cookie("useid");
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
            $("#ulFamous").html("");
            var i = 1;
            //循环获取值
            $.each(dataObj.UserList, function(idx, item) {

                var url = "";
                //判断自己或是他人
                if (userid == item.userid) {
                    url = "u";
                } else {
                    url = "u_" + item.userid;
                }

                if (idx != 0) {
                    var lihtml = "";
                    if (i < 4) {
                        lihtml += "<li style=\"padding: 0px 5px 0px 0px;\"><span class=\"R\">" + item.fansnum + " </span><span class=\"sum Tc\">" + i + "</span>";
                        lihtml += "<a href=\"" + url + "\"><img  src=\"" + FaceImagesUrl
                            + item.face + "\" alt=\"" + item.nickname + "\" width=\"30\" height=\"30\" class=\"L img\"></a>";
                        lihtml += "<a href=\"" + url + "\" class=\"Blue\">" + item.nickname + "</a> </li>";
                    }
                    else {
                        lihtml += "<li style=\"padding: 0px 5px 0px 0px;\"><span class=\"R\">" + item.fansnum + " </span><span class=\"sum1 Tc\"> " + i + "</span><a href=\""
                        + url + "\" class=\"Blue\">" + item.nickname + "</a></li>";
                    }
                    i++;
                    $("#ulFamous").append(lihtml);
                }
            });
        }, error: function(result, status) {
            if (status == 'error') {
                alert(result.responseText);
            }
        },
        complete: function() {
        }
    });

}

//得到每日草根榜列表
function GetCommonUserList() {
    ajaxurl = vServiceUrl + "VipILog.asmx/VipIlogGetCommonList";
    var userid = $.cookie("useid");

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
            $("#ulCommon").html("");
            var i = 1;
            //循环获取值
            $.each(dataObj.UserList, function(idx, item) {

                var url = "";
                //判断自己或是他人
                if (userid == item.userid) {
                    url = "u";
                } else {
                    url = "u_" + item.userid;
                }

                if (idx != 0) {
                    var lihtml = "";
                    if (i < 4) {
                        lihtml += "<li style=\"padding: 0px 5px 0px 0px;\"><span class=\"R\">" + item.fansnum + " </span><span class=\"sum Tc\">" + i + "</span>";
                        lihtml += "<a href=\"" + url + "\"><img  src=\"" + FaceImagesUrl
                            + item.face + "\" alt=\"" + item.nickname + "\" width=\"30\" height=\"30\" class=\"L img\"></a>";
                        lihtml += "<a href=\"" + url + "\" class=\"Blue\">" + item.nickname + "</a> </li>";
                    }
                    else {
                        lihtml += "<li style=\"padding: 0px 5px 0px 0px;\"><span class=\"R\">" + item.fansnum + " </span><span class=\"sum1 Tc\"> " + i + "</span><a href=\""
                        + url + "\" class=\"Blue\">" + item.nickname + "</a></li>";
                    }
                    i++;
                    $("#ulCommon").append(lihtml);
                }
            });
        }, error: function(result, status) {
            if (status == 'error') {
                alert(result.responseText);
            }
        },
        complete: function() {
        }
    });

}