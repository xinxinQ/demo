
$(document).ready(function() {


    //加载Ilog站内导航
    funGetTopMenuService("" + vServiceUrl + "IlogTopMenu.asmx/ILogGetTopMenu", "{}", "");
    //加载Ilog用户导航
    funGetTopUserMenuService("" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenu", "{MenuLive:'0'}", "");

    //
    GetIlogList(1, 45, 0);

});

function changeType(type) {
    if (type == 0) {
        $("#hrefDay").html("<strong>今日热门转发</strong>");
        $("#hrefDay").removeClass("Blue");
        $("#hrefWeek").addClass("Blue");
        $("#hrefWeek").html("一周热门转发");
    }
    else {
        $("#hrefDay").html("今日热门转发");
        $("#hrefDay").addClass("Blue");
        $("#hrefWeek").removeClass("Blue");
        $("#hrefWeek").html("<strong>一周热门转发</strong>");
    }

}

//得到博文列表
function GetIlogList(PageCurrent, pagesize, type) {
    $.ajax({
        //请求WebService Url
        url: vServiceUrl + "ILog_Spread.asmx/GetHotSpreadListJsonStr",
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

                    //判断自己或是他人
                    if (userid == item.userid) {

                        url = "/u";

                        deleteHtml = "<a href=\"javascript:void(0);\" onclick=\"showBlogResult(" + item.is_id + ");\" class=\"Blue\">删除</a> | ";

                    } else {

                        url = "/u_" + item.userid;

                    }

                    var strList = "";
                    //构建数据
                    strList += "<div class=\"Hr_10\">";
                    strList += "</div>";
                    strList += "<div class=\"Centent\">";
                    strList += "<div class=\"L sum Tc G6\">";
                    strList += "<span class=\"publish_num Orange\">1259</span><div class=\"Hr_3\">";
                    strList += "</div>";
                    strList += "转发</div>";
                    strList += "<a href=\"" + url + "\">";
                    strList += "<img src=\"/images/face/small/" + item.face + " alt=\"头像\" class=\"L Img\" /></a>";
                    strList += "<div class=\"Txt R WidBox\">";
                    strList += "<p class=\"F14 G6 L26\">";
                    strList += "<a href=\"" + url + "\" class=\"Blue\">" + item.nickname + "</a>";
                    strList += ShowVerifyImg(item.memberlevel);
                    strList += "：" +unescape(item.io_content) + "</p>";
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
                    strList += "<span class=\"L\"><a href=\"/cont_" + item.iso_id + "\" class=\"Blue\">" + item.intime + "</a> &nbsp;来自<a href=\"" 
                    + item.is_url + "\" onclick=\"return LoginDiv(16);\">" + item.is_name + "</a>";
                    strList += "</span><span class=\"R\"><a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"checkForWard(" + item.userid + "," + item.iso_id + ",1,"
                     + (item.io_spreadnum == "" ? 0 : item.io_spreadnum) + ");\" >转发(" + (item.io_spreadnum == "" ? 0 : item.io_spreadnum) + ")</a> | ";
                    strList += "<a class=\"Blue\" id=\"replyCount" + item.iso_id + "\" href=\"javascript:void(0);\" onclick=\"checkComment(0,'c" + item.iso_id + "'," + item.iso_id
                    + ",1," + (item.io_commentnum == "" ? 0 : item.io_commentnum) + ");\" >评论(" + (item.io_commentnum == "" ? 0 : item.io_commentnum) + ")</a>";
                    if (userid != item.userid) {
                        strList += " | <a class=\"Blue\" href=\"javascript:void(0);\" onclick=\"ShowPageReport(" + item.iso_id + ",'" + item.nickname + "','" + item.face + "','" + item.userid + "')\" >举报</a>";
                    }
                    strList += "</span></div>";
                    strList += "</div>";
                    strList += "<div class=\"Hr_1\">";
                    strList += "</div>";
                    strList += "<div class=\"Line_ilog\">";
                    strList += "</div>";
                    strList += "</div>";

                    if (item.is_haspic == 1) //有图片
                    {
                        GetPic(vServiceUrl + "ILogPic.asmx/GetPic", item.io_id, item.io_id);
                    }
                    strList += " </div>  ";
                    $("#divContent").append(strList);
                }
            });

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

