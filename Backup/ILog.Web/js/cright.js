
$(document).ready(function() 
{
    //得到热门评论
    GetHotComment();
    //得到热门转发
    GetHotTransmit();

});


//得到热门评论
function GetHotComment() 
{
    ajaxurl = vServiceUrl + "ILogOriginal.asmx/GetHotCommentList";
    
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
            var ulHtml = "";
            var dataObj = eval("(" + data.d + ")"); //转换为json对象
            //循环获取值
            $.each(dataObj.HotCommentList, function(idx, item) 
            {
                if (idx != 0 && item.State != -1) 
                {
                    var nickname = item.nickname;

                    if (nickname.length > 4) 
                    {
                        nickname = nickname.substring(0, 4);
                    }

                    //获取当然用户id
                    var userid = $.cookie('useid');

                    //判断自己或是他人
                    if (userid == item.userid) 
                    {
                        //伪静态地址
                        url = "u";

                        //伪静态地址
                        contentUrl = "cont_" + item.iso_id + "";

                    }
                    else 
                    {
                        //伪静态地址
                        url = "u_" + item.userid;

                        //伪静态地址
                        contentUrl = "tcont_" + item.userid + "_" + item.iso_id;

                    }

                    var imgMemberLevel = ShowVerifyImg(item.level)

                    ulHtml += "<div class=\"P10 ENG\">";
                    ulHtml += "<div>";
                    ulHtml += "<span class=\"R\"><a href=\"" + contentUrl + "\" class=\"Gray9\">(评论 "
                           + item.commentnum + ")</a></span><a href=\"" + url + "\" class=\"Blue\"  id=\"hcuser" + item.userid 
                           + "\" onmouseover=\"UserInfoShowOver(this," + item.userid + "," + item.iso_id + ")\">" + item.nickname + "</a>" + imgMemberLevel + "</div>";

                    ulHtml += "<a href=\"" + contentUrl + "\">" + unescape(item.content) + "</a><br />";
                    ulHtml += "<div class=\"Hr_10\">";
                    ulHtml += "</div>";
                    ulHtml += "<div class=\"Line_dashed\">";
                    ulHtml += "</div>";
                    ulHtml += "</div>";
                }
            });
            if (ulHtml != "") 
            {
                $("#divComment").html(ulHtml);
            }
        }, error: function(result, status) 
        {
            if (status == 'error') 
            {
                alert(result.responseText);
            }
        },
        complete: function() 
        {
        }
    });

}

//得到热门转发
function GetHotTransmit() 
{
    ajaxurl = vServiceUrl + "ILogOriginal.asmx/GetHotTransmitTopTwoList";
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
            var ulHtml = "";
            var dataObj = eval("(" + data.d + ")"); //转换为json对象
            //循环获取值
            $.each(dataObj.HotSpreadList, function(idx, item) {
                if (idx != 0 && item.State != -1) {
                    var nickname = item.nickname;
                    if (nickname.length > 4) {
                        nickname = nickname.substring(0, 4);
                    }

                    //获取当然用户id
                    var userid = $.cookie('useid');

                    //判断自己或是他人
                    if (userid == item.userid) {

                        //伪静态地址
                        url = "u";

                        //伪静态地址
                        contentUrl = "cont_" + item.iso_id + "";


                    }
                    else {
                        //伪静态地址
                        url = "u_" + item.userid;

                        //伪静态地址
                        contentUrl = "tcont_" + item.userid + "_" + item.iso_id;

                    }

                    var imgMemberLevel = ShowVerifyImg(item.level);

                    ulHtml += "<div class=\"P10 ENG\">";
                    ulHtml += "<div>";
                    ulHtml += "<span class=\"R\"><a href=\"" + contentUrl + "\" class=\"Gray9\">(转发 "
                           + item.spreadnum + ")</a></span><a href=\""
                           + url + "\" class=\"Blue\" id=\"hsuser" + item.userid + "\" onmouseover=\"UserInfoShowOver(this," + item.userid + "," + item.iso_id + ")\">" 
                           + item.nickname + "</a>" + imgMemberLevel + "</div>";
                    ulHtml += "<a href=\"" + contentUrl + "\" >" + unescape(item.content) + "</a><br />";
                    ulHtml += "<div class=\"Hr_10\">";
                    ulHtml += "</div>";
                    ulHtml += "<div class=\"Line_dashed\">";
                    ulHtml += "</div>";
                    ulHtml += "</div>";
                }
            });
            if (ulHtml != "") {
                $("#divTransmit").html(ulHtml);
            }
        }, error: function(result, status) {
            if (status == 'error') {
                alert(result.responseText);
            }
        },
        complete: function() {
        }
    });
}
