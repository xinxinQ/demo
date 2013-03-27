

$(document).ready(function()
{
　//加载Ilog站内导航
　 funGetTopMenuService(""+vServiceUrl+"IlogTopMenu.asmx/ILogGetTopMenu","{}","");
　 
　 
　 　//加载Ilog用户导航
　funGetTopUserMenuService(""+vServiceUrl+"ILogUserMenu.asmx/ILogGetUserMenu","{MenuLive:'0'}","");
　
  //加载ilog左侧菜单
　 funGetleftMenuService(""+vServiceUrl+"ILogWebLeftMenu.asmx/ILogGetHomeLeftMneu","{MenuLive:'3'}","");

//  　//获取用户名
//   funGetVipIlogModulWeb(0,1);

  //获取当然用户id
    var userid = $.cookie('useid');
    
    //获取用户头像信息
    VipILogHome("" + vServiceUrl + "VipIlogUser.asmx/ILogGetUserInfoById", userid);

    //评论列表处理
    GetUserCommentList(""+vServiceUrl + "ILogComment.asmx/GetUserCommentList",1,45,1);     
    
     //搜索只能提示
    $("#keyword_s").keyup(function(evt)
     {
        //回车键搜提示无效
        if(!isEnterKey())
        {
            searchtowho_s();
        }
        
        //上下键处理
        funListBeginUp(event);

     }); 
     
     //页面标题为收到的评论
     ShowTitle("收到的评论");
     
    //获取回车事件
    getenterevent();
    


});


//评论列表处理
//servicesUrl：服务地址
//PageCurrent：当前页面
//pagesize：每页多少行数据
//ation：查看类型（1：收到的评论，0：发出的评论 ）
function GetUserCommentList(servicesUrl,PageCurrent,pagesize,ation)
{
    //查看类型（1：收到的评论，0：发出的评论 ）
    var ation_s = $("#ation_s");


   //不搜索时需要移除间隔标签
    $('#Hr_20_1').hide();
    $('#Hr_20_2').hide();
    
    $("#div_all").html("");  //去掉搜索的相关提示
    
    $("#hr_d").show();      //显示页面的默认间距标签

    
    //存储当前页码回复评论加载列表用
    $("#currentIndex").val(PageCurrent);


    $.ajax({
        //请求WebService Url         
        url: "" + servicesUrl + "",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        //请求参数              
        data: "{PageCurrent:'" + PageCurrent + "',PageSize:'" + pagesize + "',ation:'" + ation + "'}",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var strCommentList = "";

            //获取当然用户id
            var userid = $.cookie('useid');

            var strUrl = "";

            //用户头像昵称链接地址
            var url = "";

            //博文内页地址
            var contentUrl = "";

            //回复评论
            var strReply = "";

            //加i处理
            var strI = "";
            var strIPrompt = "";
            var strImg = "";

            //删除评论
            var strDel = "";


            //循环获取值
            $.each(dataObj.CommentList, function(idx, item) {
                if (idx == 0) {
                    if (item.State == "0") {
                        $("#commentlist_div").html("加载错误");
                    }
                    else if (item.State == "2")  //无数据不显示分页控件
                    {
                        var sowhpage_div = $("#sowhpage_div");
                        sowhpage_div.html(""); //初始化分页控件

                        return false;
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
                    sowhpage_ul(PageCurrent, item.RecordCount, ation, "");   //分页
                    return true;
                }
                //来源id（1：ILog）
                var is_id = item.is_id;
                //评论来源：ILot
                if (is_id == 1) {
                    strUrl = "h";
                }

                //判断自己或是他人
                if (userid == item.userid) {

                    //伪静态地址
                    url = "u";

                    //伪静态地址
                    contentUrl = "cont_" + item.is_id_;
                }
                else {

                    //伪静态地址
                    url = "u_" + item.userid;

                    //伪静态地址
                    contentUrl = "tcont_" + item.userid_ + "_" + item.is_id_;
                }

                var strReport = "";

                if (item.RecordCount > 1)   //2页以上数据显示页数
                {
                    sowhpage_ul(PageCurrent, item.RecordCount, "");   //分页
                    $("#RowsCount").html("共" + item.RecordCount + "页"); //数据页数显示
                    return true;
                }
                else if (item.RowsCount > 0)           //一页数据显示条数
                {
                    $("#RowsCount").html("共" + item.RowsCount + "条");  //数据页数
                    $("#sowhpage_div").html("");    //去掉分页

                    return true;
                }

                if (item.ic_id != undefined) {
                    strCommentList += "<div class=\"comment Centent\">";

                    strImg = ShowVerifyImg(item.vi_memberlevel);

                    if (ation == 0)       //发出的评论
                    {
                        strCommentList += " <div class=\"pic L\"><a href=\"" + url + "\"  ><img id=\"user" + item.ic_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.ic_currentuserid + "," + item.ic_id + ")\" src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"头像\" class=\"L Img\" /></a></div> ";
                        strCommentList += " <div class=\"info R G9\">" + unescape(item.ic_content) + "<span >（" + item.intime + "）</span> <br /> ";
                    }
                    else     //收到的评论
                    {
                        strCommentList += " <div class=\"pic L\"><a href=\"" + url + "\" ><img id=\"user" + item.ic_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.ic_id + ")\" src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"头像\" class=\"L Img\" /></a></div> ";
                        strCommentList += " <div class=\"info R G9\"><a href=\"" + url + "\" id=\"uu" + item.ic_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.ic_id + ")\"  class=\"Blue\">" + item.nickname + strImg + "：</a>" + unescape(item.ic_content) + "<span >（" + item.intime + "）</span> <br /> ";
                    }

                    //判断是不是评论的回复
                    if (item.ic_commentid > 0) {
                        //被评论的博文内容（判断是否是评论的我的文章）
                        strCommentList += " <p class=\" L19\">回复 " + (userid == item.ic_currentuserid ? "我" : "<a href=\"" + url + "\" id=\"uu" + item.ic_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.ic_id + ")\"  class=\"Blue\">" + item.is_nickname + "</a>") + " 的评论： <a href=\"" + contentUrl + "\" class=\"Blue\">&ldquo;" + unescape(item.is_content) + "&rdquo;</a></p> ";
                    }
                    else {
                        //被评论的博文内容（判断是否是评论的我的文章）
                        strCommentList += " <p class=\" L19\">评论 " + (userid == item.ic_currentuserid ? "我" : "<a href=\"" + url + "\" id=\"uu" + item.ic_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.ic_id + ")\"  class=\"Blue\">" + item.is_nickname + "</a>") + " 的微博： <a href=\"" + contentUrl + "\" class=\"Blue\">&ldquo;" + unescape(item.is_content) + "&rdquo;</a></p> ";
                    }

                    strCommentList += " <div class=\"Hr_4\"></div> ";

                    if (ation == 1)   //收到的评论（收到的评论才有去报，发出的评论没有举报因为是自己的数据）
                    {
                        //自己不能举报自己
                        if (userid != item.userid) {
                            strReport = " | <a class=\"Blue\" onclick=\"ShowPageReport(" + item.ic_id + ",'" + item.nickname + "','" + item.face + "','" + item.ic_content + "'," + item.vi_memberlevel + ")\" href=\"javascript:void(0);\" >举报</a>";
                        }
                        else {
                            strReport = "";
                        }
                    }
                    else if (ation == 0) {
                        strReport = " ";
                    }
                    else {
                        strReport = " | <a class=\"Blue\" onclick=\"ShowPageReport(" + item.ic_id + ",'" + item.nickname + "','" + item.face + "','" + item.ic_content + "'," + item.vi_memberlevel + ")\" href=\"javascript:void(0);\" >举报</a>";
                    }

                    //发出的评论没有回复
                    if (ation == 0) {
                        strReply = " ";
                    }
                    else //回复的评论
                    {
                        strReply = " <a href=\"javascript:void(0);\" onclick=\"AtReplyComment(0,'c" + item.ic_id + "'," + item.is_id_ + ",0,'" + item.nickname + "'," + item.ic_id + ");\" class=\"Blue\">回复</a>";
                    }

                    //收到的评论删除处理
                    if (ation == 1) {
                        //评论和回复属于我发的、文章作者是我、自己给自己发的并且不是别人回复我的评论才能删
                        if ((userid == item.userid || userid == item.ic_currentuserid || item.userid == userid.ic_currentuserid) && item.ic_commentid == 0) {
                            strDel = "<a href=\"javascript:void(0);\" onclick=\"showCommentResult_c(" + item.ic_id + ",''," + ation + ")\" class=\"Blue\">删除</a> | ";
                        }
                        else {
                            strDel = "";
                        }
                    }
                    else {
                        strDel = "<a href=\"javascript:void(0);\" onclick=\"showCommentResult_c(" + item.ic_id + ",''," + ation + ")\" class=\"Blue\">删除</a>";
                    }


                    strCommentList += " <div class=\"txt G9\"><span class=\"R\">" + strDel + strReply + "</span>来自：<a target=\"_blank\" title=\"" + item.is_name + "\" href=\"" + strUrl + "\" class=\"Blue\">" + item.is_name + "</a>" + strReport + "</span></div> ";
 
                    strCommentList += "<div id=\"c" + item.ic_id + "\"></div>";
                    strCommentList += " </div> ";
                    strCommentList += " <div class=\" Hr_1\"></div> ";
                    strCommentList += " <div class=\"Line_ilog\"></div> ";
                    strCommentList += " </div>  ";
                }
            });
            $("#commentlist_div").html(strCommentList);
            scroll(0, 0);  //翻页后需要回到顶部
        },
        //出错调试         
        error: function(x, e) {

            alert("加载异常");
        },
        //执行成功后自动执行           
        complete: function(x) {

        }
    });
  
}

//搜索评论列表
//servicesUrl：服务地址
//PageCurrent：当前页面
//pagesize：每页多少行数据
//ation：查看类型（1：收到的评论，0：发出的评论 ）
function GetSearchList(servicesUrl,PageCurrent,pagesize,keyword)
{

    //查看类型（1：收到的评论，0：发出的评论 ）
    var ation = $("#ation_s").val();

    if(ation == "")
    {
        ation = 1;
    }


    //存储当前页码回复评论加载列表用
    $("#currentIndex").val(PageCurrent);

    $.ajax({
        //请求WebService Url         
        url: "" + servicesUrl + "",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        //请求参数              
        data: "{PageCurrent:'" + PageCurrent + "',PageSize:'" + pagesize + "',keyword:'" + keyword + "',ation:'" + ation + "'}",
        //成功           
        success: function(json) {


            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var strCommentList = "";

            //获取当然用户id
            var userid = $.cookie('useid');

            var strUrl = "";

            //回复评论
            var strReply = "";

            //博文内页地址
            var contentUrl = "";

            //搜索结果
            var strSearchInfo = "";

            //加i处理
            var strI = "";
            var strIPrompt = "";
            var strImg = "";

            //删除评论
            var strDel = "";

            //循环获取值
            $.each(dataObj.CommentList, function(idx, item) {
                if (idx == 0) {
                    if (item.State == "0") {
                        $("#commentlist_div").html("加载错误");
                    }
                    else if (item.State == "2")  //无数据不显示分页控件
                    {
                        var sowhpage_div = $("#sowhpage_div");
                        sowhpage_div.html(""); //初始化分页控件

                        $("#RowsCount").html("");  //没有搜索结果不需要搜索条数提示

                        $("#hr_d").hide();  //隐藏页面默认间距标签

                        //没有搜索结果提示  
                        strSearchInfo = "<div class=\"Hr_10\"></div>";
                        strSearchInfo += "<a href=\"javascript:void(0);\" onclick=\"ReGetList();\" class=\"Blue\" >返回所有评论</a></br>";
                        strSearchInfo += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp";
                        strSearchInfo += "<font color=\"#999999\" >没有找到符合条件的评论</font>";


                        $("#div_all").html(strSearchInfo);

                        //无数据空白显示
                        $("#commentlist_div").html("");

                        commentTyle_Search(ation);     //保持效果

                        return false;
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
                    sowhpage_ul(PageCurrent, item.RecordCount, "");   //分页
                    $("#RowsCount").html("共" + item.RecordCount + "页"); //数据页数显示
                    return true;
                }
                else if (item.RowsCount > 0)           //一页数据显示条数
                {
                    $("#RowsCount").html("共" + item.RowsCount + "条");  //数据页数
                    $("#sowhpage_div").html("");    //去掉分页

                    return true;
                }
                if (item.RecordCount != undefined)   //拿到总页数
                {
                    sowhpage_ul(PageCurrent, item.RecordCount, 3, keyword);   //分页
                    $("#hr_d").hide();  //隐藏页面默认间距标签
                    //不搜索时需要移除间隔标签
                    $('#Hr_20_1').hide();
                    $('#Hr_20_2').hide();
                    strSearchInfo = "<div class=\"Hr_10\"></div>";
                    strSearchInfo += "<a href=\"javascript:void(0);\" onclick=\"ReGetList();\" class=\"Blue\" >返回所有评论</a>";
                    strSearchInfo += " > ";
                    strSearchInfo += "共找到约<font color=\"red\" >" + item.RecordCount + "</font>页关于“" + unescape(keyword) + "”的结果：";
                    $("#div_all").html(strSearchInfo);
                    //　　            $("#RowsCount").html("约找到" + item.RecordCount + "页结果"); //数据页数显示
                    return true;
                }
                else if (item.RowsCount != undefined) //显示条数页数
                {
                    $("#sowhpage_div").html("");    //去掉分页    
                    $("#hr_d").hide();  //隐藏页面默认间距标签
                    //不搜索时需要移除间隔标签
                    $('#Hr_20_1').hide();
                    $('#Hr_20_2').hide();
                    strSearchInfo = "<div class=\"Hr_10\"></div>";
                    strSearchInfo += "<a href=\"javascript:void(0);\" onclick=\"ReGetList();\" class=\"Blue\" >返回所有评论</a>";
                    strSearchInfo += " > ";
                    strSearchInfo += "共找到<font color=\"red\" >" + item.RowsCount + "</font>条关于“" + unescape(keyword) + "”的结果：";
                    $("#div_all").html(strSearchInfo);
                    //                $("#RowsCount").html("约找到" + item.RowsCount + "条数据");  //数据页数

                    return true;
                }
                //来源id（1：ILog）
                var is_id = item.is_id;
                //评论来源：ILot
                if (is_id == 1) {
                    strUrl = "h";
                }

                //判断自己或是他人
                if (userid == item.userid) {

                    //伪静态地址
                    url = "u";

                    //伪静态地址
                    contentUrl = "cont_" + item.is_id_;
                }
                else {

                    //伪静态地址
                    url = "u_" + item.userid;

                    //伪静态地址
                    contentUrl = "tcont_" + item.userid_ + "_" + item.is_id_;
                }

                var strReport = "";

                if (item.ic_id != undefined) {
                    strCommentList += "<div class=\"comment Centent\">";

                    //加i认证
                    strImg = ShowVerifyImg(item.vi_memberlevel);

                    if (ation == 0)       //发出的评论
                    {
                        strCommentList += " <div class=\"pic L\"><a href=\"" + url + "\"  ><img id=\"user" + item.ic_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.ic_currentuserid + "," + item.ic_id + ")\" src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"头像\" class=\"L Img\" /></a></div> ";
                        strCommentList += " <div class=\"info R G9\">" + unescape(item.ic_content).replace("@", "..").replace(keyword, "<font color=\"red\">" + keyword + "</font>").replace("..", "@"); +"<span >（" + item.intime + "）</span> <br /> ";
                    }
                    else     //收到的评论
                    {
                        strCommentList += " <div class=\"pic L\"><a href=\"" + url + "\" ><img id=\"user" + item.ic_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.ic_id + ")\" src=\"" + (item.face != "" ? "images/face/small/" + item.face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"头像\" class=\"L Img\" /></a></div> ";
                        strCommentList += " <div class=\"info R G9\"><a href=\"" + url + "\" id=\"uu" + item.ic_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.ic_id + ")\"  class=\"Blue\">" + (item.nickname.replace(keyword, "<font color=\"red\">" + keyword + "</font>")) + strImg + "：</a>" + unescape(item.ic_content) + "<span >（" + item.intime + "）</span> <br /> ";
                    }

                    //判断是不是评论的回复
                    if (item.ic_commentid > 0) {
                        //被评论的博文内容（判断是否是评论的我的文章）
                        strCommentList += " <p class=\" L19\">回复 " + (userid == item.ic_currentuserid ? "我" : "<a href=\"" + url + "\" id=\"uu" + item.ic_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.ic_id + ")\"  class=\"Blue\">" + (item.nickname.replace(keyword, "<font color=\"red\">" + keyword + "</font>")) + "</a>") + " 的评论： <a href=\"" + contentUrl + "\" class=\"Blue\">&ldquo;" + unescape(item.is_content) + "&rdquo;</a></p> ";
                    }
                    else {
                        //被评论的博文内容（判断是否是评论的我的文章）
                        strCommentList += " <p class=\" L19\">评论 " + (userid == item.ic_currentuserid ? "我" : "<a href=\"" + url + "\" id=\"uu" + item.ic_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + item.ic_id + ")\" class=\"Blue\">" + (item.nickname.replace(keyword, "<font color=\"red\">" + keyword + "</font>")) + "</a>") + " 的微博： <a href=\"" + contentUrl + "\" class=\"Blue\">&ldquo;" + unescape(item.is_content) + "&rdquo;</a></p> ";
                    }

                    strCommentList += " <div class=\"Hr_4\"></div> ";

                    if (ation == 1)   //收到的评论（收到的评论才有去报，发出的评论没有举报因为是自己的数据）
                    {
                        //自己不能举报自己
                        if (userid != item.userid) {
                            strReport = " | <a class=\"Blue\" onclick=\"ShowPageReport(" + item.ic_id + ",'" + item.nickname + "','" + item.face + "','" + item.ic_content + "'," + item.vi_memberlevel + ")\" href=\"javascript:void(0);\" >举报</a>";
                        }
                        else {
                            strReport = "";
                        }
                    }
                    else if (ation == 0) {
                        strReport = " ";
                    }
                    else {
                        strReport = " | <a class=\"Blue\" onclick=\"ShowPageReport(" + item.ic_id + ",'" + item.nickname + "','" + item.face + "','" + item.ic_content + "'," + item.vi_memberlevel + ")\" href=\"javascript:void(0);\" >举报</a>";
                    }

                    //发出的评论没有回复
                    if (ation == 0) {
                        strReply = " ";
                    }
                    else //回复的评论
                    {
                        strReply = " <a href=\"javascript:void(0);\" onclick=\"AtReplyComment(0,'c" + item.ic_id + "'," + item.is_id_ + ",0,'" + item.nickname + "'," + item.ic_id + ");\" class=\"Blue\">回复</a>";
                    }

                    //发出的评论没有回复
                    if (ation == 0) {
                        strReply = " ";
                    }
                    else //回复的评论
                    {
                        strReply = " <a href=\"javascript:void(0);\" onclick=\"AtReplyComment(0,'c" + item.ic_id + "'," + item.is_id_ + ",0,'" + item.nickname + "'," + item.ic_id + ");\" class=\"Blue\">回复</a>";
                    }

                    //收到的评论删除处理
                    if (ation == 1) {
                        //评论和回复属于我发的、文章作者是我、自己给自己发的并且不是别人回复我的评论才能删
                        if ((userid == item.userid || userid == item.ic_currentuserid || item.userid == userid.ic_currentuserid) && item.ic_commentid == 0) {
                            strDel = "<a href=\"javascript:void(0);\" onclick=\"showCommentResult_c(" + item.ic_id + ",''," + ation + ")\" class=\"Blue\">删除</a> | ";
                        }
                        else {
                            strDel = "";
                        }
                    }
                    else {
                        strDel = "<a href=\"javascript:void(0);\" onclick=\"showCommentResult_c(" + item.ic_id + ",''," + ation + ")\" class=\"Blue\">删除</a>";
                    }

                    strCommentList += " <div class=\"txt G9\"><span class=\"R\">" + strDel + strReply + "</span>来自：<a target=\"_blank\" title=\"" + item.is_name + "\" href=\"" + strUrl + "\" class=\"Blue\">" + item.is_name + "</a>" + strReport + "</span></div> ";

                    strCommentList += "<div id=\"c" + item.ic_id + "\"></div>";
                    strCommentList += " </div> ";
                    strCommentList += " <div class=\" Hr_1\"></div> ";
                    strCommentList += " <div class=\"Line_ilog\"></div> ";
                    strCommentList += " </div>  ";
                }
            });
            $("#commentlist_div").html(strCommentList);
            commentTyle_Search(ation);     //保持效果   
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
function sowhpage_ul(PageCurrent,RecordCount,ation,keyword)
{
    var sowhpage_div = $("#sowhpage_div");

    var strShowPage = "";
      
    //搜索操作
    if(ation == 3)
    {
        //当然页码等于总也数就隐藏下一页按钮
        strShowPage += "<span class=\"R\" " + (PageCurrent == RecordCount ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"nextbtn\" ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_page.gif\" onclick=\"nextpage("+PageCurrent+","+RecordCount+","+ation+",'"+keyword+"')\" alt=\"下一页\" /></span>";   
    }
    else
    {
        //当然页码等于总也数就隐藏下一页按钮
        strShowPage += "<span class=\"R\" " + (PageCurrent == RecordCount ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"nextbtn\" ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_page.gif\" onclick=\"nextpage("+PageCurrent+","+RecordCount+","+ation+")\" alt=\"下一页\" /></span>";   
    }

    strShowPage += "<span class=\"R span\" style=\"position:relative\" ><a href=\"javascript:void(0);\"  id='selOption' class=\"Blue\">第" + PageCurrent + "页</a><img src=\"http://simg.instrument.com.cn/ilog/blue/images/SanJ.gif\" />";
    
    strShowPage += "<ul class=\"pageBox R Line Fa BrWh\" id=\"selOption_menu\" style=\" position:absolute; bottom:17px; left:15px;display:none;\">";

    for(var i = RecordCount ; i >= 1 ; i--)
    {
        if(ation == 3)  //搜索操作
        {
            if(PageCurrent == i)
            {
                 strShowPage += "<li id=\"selpage_"+i+"\" ><a href=\"javascript:void(0);\" onclick=\"GetSearchList('" + vServiceUrl + "ILogComment.asmx/GetSearchCommentList'," + i + ",45,'"+keyword+"')\"  >第<font color=\"red\">" + i + "</font>页</a></li>";
            }
            else
            {
                strShowPage += "<li id=\"selpage_"+i+"\"  ><a href=\"javascript:void(0);\" onclick=\"GetSearchList('" + vServiceUrl + "ILogComment.asmx/GetSearchCommentList'," + i + ",45,'"+keyword+"')\"  >第" + i + "页</a></li>";
            }
        }
        else
        {
            if(PageCurrent == i)
            {
                 strShowPage += "<li id=\"selpage_"+i+"\" ><a href=\"javascript:void(0);\" onclick=\"GetUserCommentList('" + vServiceUrl + "ILogComment.asmx/GetUserCommentList'," + i + ",45," + ation + ")\"  >第<font color=\"red\">" + i + "</font>页</a></li>";
            }
            else
            {
                strShowPage += "<li id=\"selpage_"+i+"\"  ><a href=\"javascript:void(0);\" onclick=\"GetUserCommentList('" + vServiceUrl + "ILogComment.asmx/GetUserCommentList'," + i + ",45," + ation + ")\"  >第" + i + "页</a></li>";
            }
        }        
    }

    strShowPage += "</ul>";
    strShowPage += "</span>";
    
    if(ation == 3)
    {
        //当前页码小于1就隐藏上一页页码
        strShowPage += "<span class=\"R\" " + (PageCurrent <= 1 ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"upbtn\"  ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_pageT.gif\" onclick=\"uppage("+PageCurrent+","+RecordCount+","+ation+",'"+keyword+"')\" alt=\"上一页\" /></span>";
    }
    else
    {
        //当前页码小于1就隐藏上一页页码
        strShowPage += "<span class=\"R\" " + (PageCurrent <= 1 ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"upbtn\"  ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_pageT.gif\" onclick=\"uppage("+PageCurrent+","+RecordCount+","+ation+")\" alt=\"上一页\" /></span>";
    }
    
    strShowPage += "<div class=\"Hr_20\"></div>";


    sowhpage_div.html(strShowPage);
    
    //动态创建事件
    MousePage();
    
    return strShowPage;
}

//创建鼠标悬停事件
function MousePage()
{
    var selOption = $('#selOption');
    var selOption_menu = $('#selOption_menu');

     selOption.mouseover(function(){
        selOption_menu.show();//初始化设置
        MenuDivShow(this.id);		
    }).mouseout(function () { $('#' + this.id + '_menu').hide();
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
//操作类型：3是搜索0和1是默认加载的数据类型
function nextpage(PageCurrent,RecordCount,ation,keyword)
{
    var RecordCount_index = RecordCount;    //记录总页数

    //没翻过上一页
    if(pageindex_n <= 2)
    {
        if(RecordCount >= 1)
        {
            pageindex++;

            if(ation == 3)
            {
                GetSearchList(""+vServiceUrl + "ILogComment.asmx/GetSearchCommentList",pageindex,45,keyword);
            }
            else
            {
                GetUserCommentList(""+vServiceUrl + "ILogComment.asmx/GetUserCommentList",pageindex,45,ation);
            }
        }
    }
    else
    {
        if(RecordCount >= 1)
        {
            pageindex_n++;
            
            if(ation == 3)
            {
                GetSearchList(""+vServiceUrl + "ILogComment.asmx/GetSearchCommentList",pageindex_n,45,keyword);
            }
            else
            {
                GetUserCommentList(""+vServiceUrl + "ILogComment.asmx/GetUserCommentList",pageindex_n,45,ation);
            }
        }
    }
    
    //如果到最大页码就隐藏该按钮（去掉第一页和最后一页）
    if(RecordCount_index == pageindex)  
    {
        pageindex = 1;  //索引初始化
    }
}

//记录当前页码
var pageindex_n = 2;

//上一页处理
//PageCurrent：当前页码
//RecordCount：总页数
//操作类型：3是搜索0和1是默认加载的数据类型
function uppage(PageCurrent,RecordCount,ation,keyword)
{
    var pageindex_n = PageCurrent;    //记录总页数

    if(pageindex == 1)
    {
        if(PageCurrent <= RecordCount)
        {
            pageindex_n--;
            
            //搜索操作
            if(ation == 3)
            {
                GetSearchList(""+vServiceUrl + "ILogComment.asmx/GetSearchCommentList",pageindex_n,45,keyword);
            }
            else
            {
                GetUserCommentList(""+vServiceUrl + "ILogComment.asmx/GetUserCommentList",pageindex_n,45,ation);
            }
        }
    }
    else
    {
        if(PageCurrent <= RecordCount)
        {
            pageindex--;
            
            //搜索操作
            if(ation == 3)
            {
                GetSearchList(""+vServiceUrl + "ILogComment.asmx/GetSearchCommentList",pageindex,45,keyword);
            }
            else
            {
                GetUserCommentList(""+vServiceUrl + "ILogComment.asmx/GetUserCommentList",pageindex,45,ation);
            }
        }
    }
    
    //如果到第一页了就隐藏上一也
    if(pageindex_n == 1)  
    {
        pageindex_n = 2;    //索引初始化
    }
}

//切换评论的类型
function commentTyle(index)
{
    $("#ation_s").val(index);   //保存当前用户的切换状态

    //切换标签容器
    var my_a = $("#mycomment");
    var post_a = $("#postcomment");
    
    //圆角容器
    var top_my = $("#top_comment_my");
    var top_post = $("#top_comment_post");
    
    //当前切换标签时要清空搜索框，该操作说明当前用户已经放弃搜索的操作重新查看数据
    $("#keyword_s").val("请输入昵称");
    
    //收到的评论
    if(index == 1)
    {
        //切换
        post_a.removeClass("center F14");
        my_a.addClass("center F14");
        top_post.removeClass("top");
        top_my.addClass("top");
        
        //内容
        my_a.html("<a href=\"javascript:void(0);\" onclick=\"commentTyle(1)\" class=\"Blue\"><strong>收到的评论</strong></a>");
        post_a.html("<a href=\"javascript:void(0);\" onclick=\"commentTyle(0)\" class=\"F14\">发出的评论</a>")
        
        //加载数据
        GetUserCommentList(""+vServiceUrl + "ILogComment.asmx/GetUserCommentList",1,45,1);
        
        //切换页面title
        ShowTitle("收到的评论");
    }
    else //发出的评论
    {
        //切换
        my_a.removeClass("center F14");
        post_a.addClass("center F14");
        top_my.removeClass("top");
        top_post.addClass("top");
        
        //内容
        post_a.html("<a href=\"javascript:void(0);\" onclick=\"commentTyle(0)\" class=\"Blue\"><strong>发出的评论</strong></a>");
        my_a.html("<a href=\"javascript:void(0);\" onclick=\"commentTyle(1)\" class=\"F14\">收到的评论</a>")
        
        //加载数据
        GetUserCommentList(""+vServiceUrl + "ILogComment.asmx/GetUserCommentList",1,45,0);
        
        //切换页面title
        ShowTitle("发出的评论");
    }
}

//切换评论的类型（搜索专用，之切换不加载数据）
function commentTyle_Search(index)
{
    $("#ation_s").val(index);   //保存当前用户的切换状态

    //切换标签容器
    var my_a = $("#mycomment");
    var post_a = $("#postcomment");
    
    //圆角容器
    var top_my = $("#top_comment_my");
    var top_post = $("#top_comment_post");
    
    //收到的评论
    if(index == 1)
    {
        //切换
        post_a.removeClass("center F14");
        my_a.addClass("center F14");
        top_post.removeClass("top");
        top_my.addClass("top");
        
        //内容
        my_a.html("<a href=\"javascript:void(0);\" onclick=\"commentTyle(1)\" class=\"Blue\"><strong>收到的评论</strong></a>");
        post_a.html("<a href=\"javascript:void(0);\" onclick=\"commentTyle(0)\" class=\"F14\">发出的评论</a>");
        
         //切换页面title
        ShowTitle("收到的评论");
    }
    else //发出的评论
    {
        //切换
        my_a.removeClass("center F14");
        post_a.addClass("center F14");
        top_my.removeClass("top");
        top_post.addClass("top");
        
        //内容
        post_a.html("<a href=\"javascript:void(0);\" onclick=\"commentTyle(0)\" class=\"Blue\"><strong>发出的评论</strong></a>");
        my_a.html("<a href=\"javascript:void(0);\" onclick=\"commentTyle(1)\" class=\"F14\">收到的评论</a>");
        
        //切换页面title
        ShowTitle("发出的评论");
    }
}

//校验搜索
function checkform() {

    var element_p = document.getElementById("keyword_s");

    if(element_p.value == "请输入昵称")
    {
        showTipe("请输入昵称！",0);
        element_p.focus();
        return false;
    }
    if(element_p.value == "" || element_p.value == null)
    {
        showTipe("请输入昵称！",0);
        element_p.focus();
        return false;
    }
    if(ignoreSpaces(element_p.value) == "")
    {
        showTipe("请输入昵称！",0);
        element_p.focus();
        return false;
    }
    if(HTMLEncode(element_p.value) == "")
    {
        showTipe("请输入昵称！",0);
        element_p.focus();
        return false;
    }
    if(removeHTMLTag(element_p.value) == "")
    {
        showTipe("请输入昵称！",0);
        element_p.focus();
        return false;
    }
    
    //获取数据
    GetSearchList(""+vServiceUrl + "ILogComment.asmx/GetSearchCommentList",1,45,element_p.value);
    
    //隐藏下拉菜单
    $("#GetSearchTowho_Menu").hide();
}


//是否在显示搜索提示时第一次按上键，如果是要选中最后一项，该变量默认是不选中的
 var isp = false;

//只能提示收件人（页面搜索用户）
function searchtowho_s()
{
     //收件人昵称
    var towho = $("#keyword_s");
   
    var strTowhoValue = towho.val();   
 
    //查看类型（1：收到的评论，0：发出的评论 ）
    var ation = $("#ation_s").val();

    //如果没有选择查看类型默认“收到的评论”
    if(ation == "")
    {
        ation = 1;
    }

    //校验收信人
    if (strTowhoValue != "") 
    {
        if (ignoreSpaces(strTowhoValue) == "") 
        {
            showTipe("请输入昵称！",0);
            towho.focus();
            return false;
        }
        if (HTMLEncode(strTowhoValue) == "") 
        {
            showTipe("请输入昵称！",0);
            towho.focus();
            return false;
        }
        if (removeHTMLTag(strTowhoValue) == "") 
        {
            showTipe("请输入昵称！",0);
            towho.focus();
            return false;
        }
    }

    //开始发送
    $.ajax({
        //请求WebService Url         
        url: "" + vServiceUrl + "ILogComment.asmx/GetSearchCommentInfo",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        cache: false,
        //请求参数              
        data: "{commentinfo:'" + strTowhoValue + "',ation:'" + ation + "'}",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var strList = "";

            //获取当然用户id
            //var userid = $.cookie('useid'); 

            //循环获取值
            $.each(dataObj.List, function(idx, item) 
            {

                if (idx == 0) 
                {
                    if (item.State == "0") 
                    {
//                        alert("加载错误！");
                    }
                    else if (item.State == "2")  //无数据不显示分页控件
                    {
//                        showTipe("未找到收件人！",0);

                        return false;   //无数据不再往下执行
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    if(item.nickname != undefined)
                    {
                        var strClass = "";
                    
                        //默认选中第一个
                        if(idx == 1)
                        {
//                            strClass = "WindowBG";
                        }
                        else
                        {
                            //strClass = "Cl";
                        }
                    
                        strList += "<li class=\""+strClass+"\" style=\"cursor:hand;\" id=\"il_s"+idx+"\"  onclick=\"Getnickname_Box_s2('" + item.nickname + "')\" ><span id=\"hrSearchTowhPeple" + idx + "\" >" + item.nickname + "</span></li>";  

                        return true;
                    }
                }
            });

            //加载下拉菜单
            $("#GetSearchTowho_Menu").html(strList);
            
            //有数据显示下拉框
            if(strList != "")
            {    
                GetSearchTowhUpList_s();
                
                //重新搜索数据时要初始化是否在搜索框中直接按上键的标记
                isp = false;
            }
            else    //没有数据隐藏掉提示框
            {
                $("#GetSearchTowho_Menu").hide();
            }
            
    
            //鼠标滑动保持唯以选中的样式（下拉项）
            $("#GetSearchTowho_Menu li").mouseover(function() 
            {
                //鼠标滑过
                $(this).addClass("WindowBG");
                var index = $("#GetSearchTowho_Menu li").index($(this));
                
                //开始遍历
                $("#GetSearchTowho_Menu li").each(function(i)
                {
                    if(i != index)
                    {
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
function GetSearchTowhUpList_s()
{

    //收件人文本框值
    var txtSearchValue=$("#keyword_s").val();

    //框内数据不为空就开始定位
   if(txtSearchValue!=null && txtSearchValue!="")
   {
       setMenuPositionsSearch_s2("GetSearch");
       MenuDivShow_s2("GetSearch");
   } 
   else
   {
       $("#GetSearchTowho_Menu").hide();
   }
   
}
 
//下拉框定位
function setMenuPositionsSearch_s2(ShowID)
{
	var offset = $('#keyword_s').offset();
	var divheight = $('#keyword_s').innerHeight();

	var leftpadd = 0;
	
	$('#' + ShowID +'Towho_Menu').css
	({
		//'left':offset.left + -208, //左右定位
		//'top':offset.top+20,       //上下定位
		'position':'absolute'
	}).show();

}

//控制隐藏显示
function MenuDivShow_s2(showdiv)
{
	$('#'+showdiv+'Towho_Menu').mouseover(function () { $(this).show(); });
	$('#'+showdiv+'Towho_Menu').mouseout(function () { $(this).hide(); });
	$('#'+showdiv).mouseout(function () { $('#'+showdiv+'Towho_Menu').hide(); });
}
 
//把选中的收件人放入框中
//towho：收件人
function Getnickname_Box_s2(towho)
{
    $("#keyword_s").val(towho);
    
    //判断是否用鼠标选择搜索提示结果，如果是隐藏下拉，搜索框获取焦点，并执行搜索
    var event=arguments.callee.caller.arguments[0]||window.event; 

    if (event.keyCode != 38 && event.keyCode != 40 && event.keyCode != undefined)
    {
        $("#GetSearchTowho_Menu").hide();
        $("#keyword_s").focus();
        GetSearchList(""+vServiceUrl + "ILogComment.asmx/GetSearchCommentList",1,45,towho);
    }
}


//上下键处理
//搜索下拉
function funListBeginUp(evt) 
{
    var keynum;

    if (window.event) // IE
    {
        keynum = evt.keyCode;
    }
    else // Netscape/Firefox/Opera
    {
        keynum = evt.which;
    }

    if (keynum != 13 && keynum != 38 && keynum != 40) 
    {
        return false;
    }
    else if (keynum == 13) 
    {
        funListBeginUpUL("GetSearchTowho_Menu", 2);
    }
    else if (keynum == 38) 
    {
        funListBeginUpUL("GetSearchTowho_Menu", 0);
    }
    else if (keynum == 40) 
    {
        funListBeginUpUL("GetSearchTowho_Menu", 1);
    }

    return false;

}


//上下键处理
function funListBeginUpUL(NameID, vType) 
{
    var bl = true;

    //用户在搜索结果提示按上键直接选中最后一项
    if (vType == 0 && !isp) 
    {
        var ilsize = $("#GetSearchTowho_Menu li").size();

        $("#il_s"+ilsize).addClass("WindowBG");
        Getnickname_Box_s2($("#il_s"+ilsize).text());
        
        isp = true;
    }
    else
    {
        
        $("#" + NameID + " li").each(function(i)
        {
            if (vType == 0) 
            {
                if ($(this).hasClass("WindowBG")) 
                {
                    if (bl) 
                    {
                        var index = $("#GetSearchTowho_Menu li").index($(this));
                        var ilsize = $("#GetSearchTowho_Menu li").size();

                        //判断是不是选到第一个，如果是需要循环到最后一个
                        if(index == 0)
                        {

                            $(this).removeClass();
                            $("#il_s"+ilsize).addClass("WindowBG");
                            Getnickname_Box_s2($("#il_s"+ilsize).text());
                        }
                        else
                        {
                            $("#GetSearchTowho_Menu li.WindowBG").prev().addClass("WindowBG");
                            $("#GetSearchTowho_Menu li.WindowBG").next().removeClass("WindowBG");
                            
                            
                            //开始遍历
                            $("#GetSearchTowho_Menu li").each(function(i)
                            {
                                if(i != (index -1) && index != 0)
                                {
                                    $(this).removeClass();
                                }
                            })
                        
                            //把选中的值放入搜索框中
                            if($(this).prev().text() != "")
                            {
                                Getnickname_Box_s2($(this).prev().text());
                            }
                            else    //如果到了最后一个就保持当前选中的值
                            {
                                Getnickname_Box_s2($(this).text());
                            }
                        }

                        bl = false;

                    }
                }
            }
            else if (vType == 1) 
            {
                if ($(this).hasClass("WindowBG")) 
                {
                    if (bl) 
                    {
                        var index = $("#GetSearchTowho_Menu li").index($(this));
                        var ilsize = $("#GetSearchTowho_Menu li").size();
                    
                        //判断是不是选到最后一个，如果是需要循环到第一个
                        if(index == (ilsize - 1))
                        {
                            $(this).removeClass();
                            $("#il_s1").addClass("WindowBG");
                            Getnickname_Box_s2($("#il_s1").text());
                        }
                        else
                        {
                            //向下
                            $("#GetSearchTowho_Menu li.WindowBG").next().addClass("WindowBG");
                            $("#GetSearchTowho_Menu li.WindowBG").prev().removeClass("WindowBG");
                            
                            
                            //开始遍历，除了当前选中的选项其他都移除
                            $("#GetSearchTowho_Menu li").each(function(i)
                            {   
                                if(i != (index + 1) && (index + 1) != ilsize)
                                {
                                    $(this).removeClass();
                                }
                            })

                            //把选中的值放入搜索框中
                            if($(this).next().text() != "")
                            {
                                Getnickname_Box_s2($(this).next().text());
                            }
                            else    //如果到了最后一个就保持当前选中的值
                            {
                                Getnickname_Box_s2($(this).text());
                            }
                            
                            bl = false
                        }
                    }
                }
                if(bl)    //选择第一条搜索提示
                {
                    //只要不是第一次按上就改变“第一次上键的状态”
                    isp = true;
                    
                    $("#il_s1").addClass("WindowBG");
                    Getnickname_Box_s2($("#il_s1").text());
                }
            }
            else if (vType == 2) 
            {
                //回车
                if ($(this).hasClass("WindowBG")) 
                {
                    var strKeyWord = "";
                
                    //判断是否按了上下键如果没有说明用户想做模糊查询
                    if(!isUpDownKey())
                    {
                        strKeyWord = $("#keyword_s").val();
                    }
                    else
                    {
                        strKeyWord = $(this).text();
                    }
                
                    Getnickname_Box_s2(strKeyWord);
                    
                    GetSearchList(""+vServiceUrl + "ILogComment.asmx/GetSearchCommentList",1,45,strKeyWord);
                }
            }

        })
    }
}

//返回
function ReGetList()
{
   //获取当前操作类型
   var ation = $("#ation_s").val();
    
   GetUserCommentList(""+vServiceUrl + "ILogComment.asmx/GetUserCommentList",1,45,ation);
}


//选择评论弹出层
//ret:0 表示点击,1:表示异步刷新
function AtReplyComment(ret, divId, spreadid, isoriginal, nickName, commentid) 
{

    //判断是否重复点击
    var check = $("#" + divId).html();

    if (check != "" && ret == 0) 
    {
        $("#" + divId).html("");
        $("#" + divId).hide();
    } 
    else 
    {

        var result = "<div class=\"LookH\"  ><div class=\"Round1\">";
        result += "<div class=\"Round2\">";
        result += "<div class=\"Round3\">";
        result += "<textarea class=\"Bd Fa\" onpropertychange=\"if(value.length>140) value=value.substr(0,140)\"  name=\"commentInfoId"
                + spreadid + "\" style=\"overflow-y:hidden;\" id=\"commentInfoId" + spreadid + "\" cols=\"60\" rows=\"2\">回复@" + nickName + ":</textarea>";
        result += "<div class=\"Hr_10\"></div> <div>";
        result += "<div class=\"Hr_10\"></div>";
        result += "<div class=\"WinBtn  R\"><span><input name=\"评论\" id=\"Btncomment\" type=\"button\" onclick=\"javascript:sendReplyComment('" + divId
                 + "','" + spreadid + "','" + isoriginal + "'," + commentid + ");\"  value=\"评论\" /></span></div>";
        result += "<div class=\"ICOlist L\" style=\"position:relative;\">";
        result += "<ul >";
        result += "<li><span class=\"ico1\"></span><a href=\"javascript:void(0);\" class=\"Blue\" id=\"commentfaceId"
                + spreadid + "\" onmouseover=\"this.style.cursor='pointer'\" onclick=\"changeExpressio(this,'commentInfoId" + spreadid + "');\">表情</a></li></ul></div></div>";

        result += "<div class=\"Hr_10\"></div>";
        result += "</div></div>";
        result += "<span class=\"Jiao\" style=\"left:452px; top:-9px\">◆</span>";
        result += "</div></div>";

        $("#" + divId).html(result);
        $("#" + divId).show();

    }

}


//发送评论
function sendReplyComment(divid, spreadid, isoriginal, commentid) 
{
    //转发内容
    var content = $("#commentInfoId" + spreadid).val();
    content = content.replace(/[\r\n]/mg, " "); //替换换行
    content = content.replace(/^ +| +$/g, '').replace(/ +/g, ' '); //去空格

    //是否是回复
    var indexat = content.indexOf("回复");

    //判断回复的是否为空
    var index = content.indexOf(":");
    var indexstr = content.substring(index + 1, index + 2)

    alert(content);
    //判断回复、纯文字
    if (content != "" && indexstr != "") 
    {
        $.ajax({

            url: "" + vServiceUrl + "VipIlogUser.asmx/ILogAddCommentInfo",
            type: "POST",
            dataType: "json",
            contentType: "application/json;",
            data: "{spreadid:'" + spreadid + "',isoriginal:" + isoriginal + ",content:'" + content + "',commontId:'" + commentid + "',i:'" + rand + "'}",
            success: function(json) 
            {

                var dataObj = eval("(" + json.d + ")"); //转换为json对象     　　     
                var result = "";

                if (dataObj.state == 1) 
                {

                    result = "评论成功";

                } 
                else 
                {

                    result = "评论失败";

                }

                showTipe(result, dataObj.state); //提示
                $("#commentInfoId" + spreadid).val("");
                $("#" + divid).hide();
                $("#" + divid).html("");

                //操作类型1：收到的评论，0：发出的评论
                var ation = $("#ation_s").val();
                var keyword = $.trim($("#keyword_s").val());
                var currentIndex = $("#hidPageIndex").val();
                
                
                if (keyword == "" || keyword == "请输入昵称")   //没有走搜索操作
                {
                    GetUserCommentList(""+vServiceUrl + "ILogComment.asmx/GetUserCommentList",currentIndex,45,1); 
                }
                else        //在搜索列表中操作
                {
                    GetSearchList(""+vServiceUrl + "ILogComment.asmx/GetSearchCommentList",1,45,keyword);
                }
            },
            //出错调试         
            error: function(x, e) 
            {
                //alert(x.responseText);     
            },
            //执行成功后自动执行           
            complete: function(x) 
            {

            }
        });
    } 
    else 
    {

        showTipe("评论的内容不能为空哦!", 0); //提示

    }

}



//回车事件
function getenterevent()
{
     $(function() 
     {
            $("input[type='text']").keypress(function(evt) 
            {
                evt = (evt) ? evt : ((window.event) ? window.event : "");
                var key = evt.keyCode ? evt.keyCode : evt.which;
                
                if (key == 13) 
                {
                    var id = new String($(this).attr("id"));
                    var num = id.substr(id.length - 1, 1);
                   
                    $("#bnt_" + num).click();
                    return false;
                }
            });
        });
}
