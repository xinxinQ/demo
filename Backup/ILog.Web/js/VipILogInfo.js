
//下拉菜单的定位
function setUserInfoMenuPosition(ShowID, extraTop,height,width) {

    //触发悬浮框对象的当前位置
    var offset = $('#' + ShowID).offset();
    
    //触发悬浮框对象的高度
    var divheight = $('#' + ShowID).innerHeight();
    
    //触发悬浮框对象的宽度
    var divwidth = $('#' + ShowID).innerWidth();

    //悬浮框的高度
    if (height == 0) {
        height = $("#" + ShowID + "_info").height();
    }
    
    //悬浮框的宽度
    if (width == 0) {
        width = $("#" + ShowID + "_info").width();
    }

    $("#" + ShowID + "_info").attr("height", height);
    $("#" + ShowID + "_info").attr("width", width);

    //左侧额外减去的长度
    var leftpadd = 0;

    //悬浮框的top位置
    var top = 0;
    //悬浮框的left位置
    var left = 0;

    //距离浏览器顶部的高度
    var topFaraway = offset.top - $(document).scrollTop(); 
    //距离浏览器右侧距离
    var rightFaraway = $(document.body).width() - offset.left + $(document).scrollLeft();

    if (topFaraway <= height && rightFaraway >= width)//距离浏览器的高度小于等于悬浮框自身的高度并且和浏览器右侧的距离大于等于悬浮框自身的宽度
    {
        //悬浮框是在触发对象的左下方显示
        top = offset.top + divheight-extraTop/4;
        left = offset.left;
        if (divwidth < 40) {
            left = left - divwidth + 2;
        }
        $("#" + ShowID + "_position").removeClass().addClass("WindowSanWT");
    }
    else if (topFaraway > height && rightFaraway >= width)//距离浏览器的高度大于悬浮框自身的高度并且和浏览器右侧的距离大于等于悬浮框自身的宽度
    {
        //悬浮框是在触发对象的左上方显示
        top = offset.top - height + extraTop;
        left = offset.left;
        if (divwidth < 40) {
            left = left - divwidth + 2;
        }
        $("#" + ShowID + "_position").removeClass().addClass("WindowSan");
    }
    else if (topFaraway <= height && rightFaraway < width)//距离浏览器的高度小于等于悬浮框自身的高度并且和浏览器右侧的距离小于等于悬浮框自身的宽度
    {
        //悬浮框是在触发对象的右下方显示
        top = offset.top + divheight + extraTop;
        left = offset.left - width + divwidth;

        if (divwidth < 40) {
            left = left + divwidth-2;
        }
        $("#" + ShowID + "_position").removeClass().addClass("WindowSanWTR");
    }
    else if (topFaraway > height && rightFaraway < width)//距离浏览器的高度大于悬浮框自身的高度并且和浏览器右侧的距离小于悬浮框自身的宽度
    {
        //悬浮框是在触发对象的右上方显示
        top = offset.top - height + extraTop;
        left = offset.left - width + divwidth;

        if (divwidth < 40) {
            left = left + divwidth-2;
        }
        $("#" + ShowID + "_position").removeClass().addClass("WindowSanR");
    }

    $('#' + ShowID + '_menu').css({
        'left': left,
        'top': top,
        'position': 'absolute'
    }).show();

}


//显示或隐藏层

function MenuDivShow_UserInfo(showdiv) {
    $('#' + showdiv + '_menu').mouseover(function() { $(this).show(); });
    $('#' + showdiv + '_menu').mouseout(function() { $(this).hide(); });

}




//鼠标移入（用户信息悬浮框）
function UserInfoShowOver(obj, userId, isid) {

    $(".WindowWark350").hide();
    $(".WindowWark280").hide();

    if ($("#" + obj.id + "_menu").length == 0) {

        $('<div id="' + obj.id + '_menu" style="z-index:9999" class="WindowWark350"></div>').appendTo('body');
    }

    //userId是自己或者他人的用户编号
    funcGetNoVipModul("" + vServiceUrl + "VipIlogUser.asmx/ILogGetVipIlogInfoById", obj.id, userId, isid);

}


//显示悬浮框数据
function funcGetNoVipModul(servicesUrl, objId, otherId, isId) {
    var boxid = $("#" + objId + "_info");

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
        //非异步
        async: false,
        //请求参数              
        data: "{userId:'" + otherId + "',i:'" + rand + "'}",
        //成功                     
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            //获取当前用户id
            var userId = $.cookie('useid');

            var blog = "";
            var fan = "";
            var concern = "";

            if (userId == otherId) {

                concern = "/follow";
                fan = "/fans";
                blog = "/u";

            }
            else {

                concern = "/follow_" + otherId;
                fan = "/fans_" + otherId;
                blog = "/u_" + otherId;

            }

            var openedHtml = "";
            var top = $(document).scrollTop();
            var offset = $('#' + objId + "").offset();
            var left;
            var tops;
            var topheight = offset.top - top;

            //存在(开通:1、未开通:0)
            if (dataObj.UrlState == 1) {

                var state = dataObj.state;

                var face = $.trim(dataObj.face) == "" ? "default.png" : dataObj.face; //头像

                var sexPic = dataObj.sex == "male" ? "men.gif" : "women.jpg";

                var existUser = 1;

                //开通
                if (state == 1 || state == 0) {

                    var success = userId == otherId ? 1 : 0;

                    openedHtml += "<div class=\"WindowWark350\" id=\"" + objId + "_info\">";
                    openedHtml += "<div class=\" WindowBox\">";
                    openedHtml += "<div class=\"WindowPic L\">";
                    openedHtml += "<a href=\"" + blog + "\">";
                    openedHtml += "<img class=\"L img\" width=\"50\"  height=\"50\" src=\"../images/face/small/" + face + "\" title=\"" + dataObj.nickname + "\" alt=\"" + dataObj.nickname + "\" />";
                    openedHtml += "</a>";
                    openedHtml += "</div>";
                    openedHtml += "<div class=\"WindowInfo L  L20  G4\">";
                    openedHtml += "<a href=\"" + blog + "\" class=\" Blue\">" + dataObj.nickname + "</a>";

                    openedHtml += ShowVerifyImg(dataObj.memberlevel);


                    openedHtml += "<br />";
                    openedHtml += "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/" + sexPic + "\" /> <br />";
                    openedHtml += "<a href=\"" + concern + "\" class=\"Blue\">关注</a>&nbsp;" + dataObj.concern;
                    openedHtml += "<span class=\"G9\">&nbsp;|&nbsp;</span>";
                    openedHtml += "<a href=\"" + fan + "\" class=\"Blue\">粉丝</a>&nbsp;" + dataObj.fan;
                    openedHtml += "<span class=\"G9\">&nbsp;|&nbsp;</span>";
                    openedHtml += "<a href=\"" + blog + "\" class=\"Blue\">微博</a>&nbsp;" + dataObj.blog;
                    openedHtml += "</div>";
                    openedHtml += "<div class=\" Cl\"></div>";
                    openedHtml += "<p class=\"L18 G6 Centent\">" + dataObj.remark + "</p>";
                    openedHtml += "<div class=\"Hr_10\">";
                    openedHtml += "</div></div>";

                    //查看他人
                    if (success == 0) {

                        //关注状态（加关注:0 ，已关注:1 互相关注: 2）
                        var concernState = dataObj.concernState;

                        if (concernState == 0) {

                            //已关注
                            openedHtml += "<div class=\"Windowbot\">";
                            openedHtml += "<div class=\"WinBtn_H L\">";
                            openedHtml += "<a href=\"javascript:showdialog_u('" + dataObj.nickname + "',0);\">";
                            openedHtml += "<span>站短</span>";
                            openedHtml += "</a>";
                            openedHtml += "</div>";
                            openedHtml += "<div class=\"L G6\">";
                            openedHtml += "<img class=\"L Limg\" src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-2.gif\" />";
                            openedHtml += "<a href=\"javascript:ShowAddFollowTrueTaGroup(0," + otherId + ",'" + dataObj.nickname + "');\">设置分组</a>";
                            openedHtml += "</div>";
                            openedHtml += "<div class=\"R RboxB G9\">";
                            openedHtml += "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-4.gif\" class=\"L Rimg\" alt=\"已关注\" />";
                            openedHtml += "<a href=\"javascript:void(0);\" class=\" Gray6\"> 已关注</a>";
                            openedHtml += "&nbsp;|&nbsp;";
                            openedHtml += "<a href=\"javascript:ShowPageConcern(0," + otherId + ",'" + dataObj.nickname + "');\" class=\"Blue\">取消</a>";
                            openedHtml += "</div>";

                            openedHtml += "<div id=\"" + objId + "_position\" >&nbsp;</div>";

                            openedHtml += "</div>";


                        }
                        else if (concernState == 1) {

                            //互相关注
                            openedHtml += "<div class=\"Windowbot\">";
                            openedHtml += "<div class=\"WinBtn_H L\">";
                            openedHtml += "<a href=\"javascript:showdialog_u('" + dataObj.nickname + "',0);\">";
                            openedHtml += "<span>站短</span>";
                            openedHtml += "</a>";
                            openedHtml += "</div>";
                            openedHtml += "<div class=\"L G6\">";
                            openedHtml += "<img class=\"L Limg\" src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-2.gif\" />";
                            openedHtml += "<a href=\"javascript:ShowAddFollowTrueTaGroup(0," + otherId + ",'" + dataObj.nickname + "');\">设置分组</a>";
                            openedHtml += "</div>";
                            openedHtml += "<div class=\"R RboxB G9\" style=\"width:130px\">";
                            openedHtml += "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-3.gif\" class=\"L Rimg\" alt=\"互相关注\" />";
                            openedHtml += "<a href=\"javascript:void(0);\" class=\" Gray6\"> 互相关注</a>";
                            openedHtml += "&nbsp;|&nbsp;";
                            openedHtml += "<a href=\"javascript:ShowPageConcern(0," + otherId + ",'" + dataObj.nickname + "');\" class=\"Blue\">取消</a>";
                            openedHtml += "</div>";

                            openedHtml += "<div id=\"" + objId + "_position\" >&nbsp;</div>";

                            openedHtml += "</div>";

                        }
                        else {

                            //加关注(concernState=2)

                            openedHtml += "<div class=\"Windowbot\">";
                            openedHtml += "<div class=\"WinBtn_H L\">";
                            openedHtml += "<a href=\"javascript:showdialog_u('" + dataObj.nickname + "',0);\">";
                            openedHtml += "<span>站短</span>";
                            openedHtml += "</a>";
                            openedHtml += "</div>";
                            openedHtml += "<div class=\"WinBtn  R\">";
                            openedHtml += "<a href=\"javascript:void(0);\" class=\"White\">";
                            openedHtml += "<span>";
                            openedHtml += "<input name=\"btnAddConcern\" type=\"button\" value=\"+ 加关注\" onclick=\"ShowAddFollowTrue(0," + otherId + ",'" + dataObj.nickname + "','" + dataObj.id + "')\" id=\"concern_a_" + dataObj.id + "\" />";
                            openedHtml += "</span>";
                            openedHtml += "</a>";
                            openedHtml += "</div>";

                            openedHtml += "<div id=\"" + objId + "_position\" >&nbsp;</div>";

                            openedHtml += "</div>";

                        }


                    } else {

                        openedHtml += "<div id=\"" + objId + "_position\" >&nbsp;</div>";

                    }

                    openedHtml += "</div>";

                } else {

                    //未开通(-1,2)

                    if (userId == otherId) {

                        //查看自己
                        openedHtml += "<div class=\"WindowWark350\" id=\"" + objId + "_info\">";
                        openedHtml += "<div class=\" WindowBox\">";
                        openedHtml += "<div class=\"WindowPic L\">";
                        openedHtml += "<a href=\"" + blog + "\">";
                        openedHtml += "<img class=\"L img\" width=\"50\"  height=\"50\" src=\"../images/face/small/" + face + "\" title=\"" + dataObj.nickname + "\" alt=\"" + dataObj.nickname + "\" />";
                        openedHtml += "</a>";
                        openedHtml += "</div>";
                        openedHtml += "<div class=\"WindowInfo L  L20  G4\">";
                        openedHtml += "<a href=\"" + blog + "\" class=\" Blue\">" + dataObj.nickname + "</a>";
                        openedHtml += "<br />";
                        openedHtml += "<img alt=\"\" src=\"http://simg.instrument.com.cn/ilog/blue/images/" + sexPic + "\" />";
                        openedHtml += "<br /></div>";
                        openedHtml += "<div class=\" Cl\"></div>";
                        openedHtml += "<div class=\"WinBtn  R\">";
                        openedHtml += "<a href=\"javascript:return LoginDiv(16);\" class=\"White\">";
                        openedHtml += "</span>";
                        openedHtml += "</a>";
                        openedHtml += "</div>";
                        openedHtml += "<div class=\"Hr_10\"></div>";
                        openedHtml += "</div>";

                        openedHtml += "<div id=\"" + objId + "_position\" >&nbsp;</div>";

                        openedHtml += "&nbsp;</div>";
                        openedHtml += "</div>";
                    }
                    else {
                        //查看他人
                        openedHtml += "<div class=\"WindowWark350\" id=\"" + objId + "_info\">";
                        openedHtml += "<div class=\" WindowBox\">";
                        openedHtml += "<div class=\"WindowPic L\">";
                        openedHtml += "<a href=\"" + blog + "\">";
                        openedHtml += "<img class=\"L img\" width=\"50\"  height=\"50\" src=\"../images/face/small/" + face + "\" title=\"" + dataObj.nickname + "\" alt=\"" + dataObj.nickname + "\" />";
                        openedHtml += "</a>";
                        openedHtml += "</div>";
                        openedHtml += "<div class=\"WindowInfo L  L20  G4\">";
                        openedHtml += "<a href=\"" + blog + "\" class=\" Blue\">" + dataObj.nickname + "</a>";
                        openedHtml += "<br />";
                        openedHtml += "<img alt=\"\" src=\"http://simg.instrument.com.cn/ilog/blue/images/" + sexPic + "\" />";
                        openedHtml += "<br /></div>";
                        openedHtml += "<div class=\" Cl\"></div>";
                        openedHtml += "<p class=\"L18 G6\">";
                        openedHtml += "该用户还未开通ILOG";
                        openedHtml += "</p>";
                        openedHtml += "<div class=\"Hr_10\"></div></div>";
                        openedHtml += "<div class=\"Windowbot\">";
                        openedHtml += "<div class=\"WinBtn_H L\">";
                        openedHtml += "<a href=\"javascript:void(0);\">";
                        openedHtml += "<span>";
                        openedHtml += "<input name=\"btnSendMail\" type=\"button\" id=\"btnSendMail\" onclick=\"showdialog_u('" + dataObj.nickname + "',0);\" value=\"站短\" />";
                        openedHtml += "</span>";
                        openedHtml += "</a>";
                        openedHtml += "</div>";

                        openedHtml += "<div id=\"" + objId + "_position\" >&nbsp;</div>";

                        openedHtml += "</div>";
                        openedHtml += "</div>";
                    }
                }
            }
            else {
                existUser = 0;

                //用户不存在
                openedHtml += "<div class=\"WindowWark350\"  id=\"" + objId + "_info\" >";
                openedHtml += "<div class=\"WindowBox Tc\">";
                openedHtml += "<div class=\"  Tc F14   WindowSak WindowG60\" >";
                openedHtml += "<span class=\"L G4\">";
                openedHtml += "抱歉，该用户目前不存在！";
                openedHtml += "</span>";
                openedHtml += "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/face.gif\" />";
                openedHtml += "</div>";
                openedHtml += "<div class=\"Hr_10\"></div>";
                openedHtml += "</div>";

                openedHtml += "<div id=\"" + objId + "_position\" >&nbsp;</div>";

                openedHtml += "&nbsp;</div>";
            }
            //悬浮框高度
            var height = "";
            //悬浮框宽度
            var width = "";

            if (boxid.length > 0) {
                //第二次读取悬浮框，取第一次读取的时候保存的高度和宽度属性，因为第二次加载时经常丢失高度和宽度
                height = boxid.attr("height");
                width = boxid.attr("width");
            }

            $('#' + objId + '_menu').html(openedHtml);
            
            var extraTop = 0;
  
            
            if (userId == otherId) {
                //如果悬浮框显示的是当前用户，因为没有下面的设置蓝框，所以要减去这段高度
                //如果触发悬浮框的是头像，加30高度，如果是超链接，加20高度
                if (objId.indexOf("user") == 0) {
                    extraTop = 30;
                }
                else if (objId.indexOf("mini") == 0) {
                    extraTop = 15;
                }
                else {
                    extraTop = 20;
                }
            }
            else {
                if (objId.indexOf("user") == 0) {
                    extraTop = 0;
                }
                else if (objId.indexOf("mini") == 0) {
                    extraTop = -2;
                }
                else {
                    extraTop = 0;
                }
            }
            if (existUser == 0) {
                extraTop = 20;
            }
            setUserInfoMenuPosition(objId, extraTop, height, width);
            MenuDivShow_UserInfo(objId);

            $('#' + objId).mouseout(function() { $('#' + objId + '_menu').hide(); });

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