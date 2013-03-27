
//去空格
function ignoreSpaces(string) {
    var temp = "";
    string = '' + string;
    splitstring = string.split(" ");
    for (i = 0; i < splitstring.length; i++)
        temp += splitstring[i];
    return temp;
}

//过滤html
function HTMLEncode(text) {
    text = text.replace(/&/g, "&amp;");
    text = text.replace(/"/g, "");
    text = text.replace(/</g, "");
    text = text.replace(/>/g, "");
    text = text.replace(/\\/g, "");
    text = text.replace(/\//g, "");
    text = text.replace(/　/g, "");
    text = text.replace(/(^\s*)|(\s*$)/g, "");

    return text;
}
//过滤html
function removeHTMLTag(str) {
    str = str.replace(/<\/?[^>]*>/g, ''); //去除HTML tag
    str = str.replace(/[ | ]*\n/g, '\n'); //去除行尾空白
    //str = str.replace(/\n[\s| | ]*\r/g,'\n'); //去除多余空行

    str = str.replace(/<\/?[^>]*/g, ''); //去除HTML tag
    str = str.replace(/&nbsp;/ig, ''); //去掉&nbsp;

    return str;
}

//是否是按了回车、up、down键、true是，false否
function isEnterKey() {
    var event = arguments.callee.caller.arguments[0] || window.event;

    if (event.keyCode == 13 || event.keyCode == 38 || event.keyCode == 40) {
        return true;
    }
    else {
        return false;
    }
}

//是否按了上下键
function isUpDownKey() {
    var event = arguments.callee.caller.arguments[0] || window.event;

    if (event.keyCode == 38 || event.keyCode == 40) {
        return true;
    }
    else {
        return false;
    }
}

var vServiceUrl = "http://ig.instrument.com.cn/ilogWebService/";
//var vServiceUrl = "http://localhost/ilogWebService/";
//var vServiceUrl = "http://localhost:5215/";

//头像图片地址
var FaceImagesUrl = "/images/face/small/";

//获取top 左侧导航
//GetUrl 请求的服务 Url
//GetParts 请求的参数
//GetWait 等待参数
function funGetTopMenuService(GetUrl, GetParts, GetWait) {
    $.ajax({
        url: "" + GetUrl + "",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象
            var LoginType = 0;
            var TopContent = "";
            $.each(dataObj.Menu, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        $("#TopMenu").html("<li>加载错误</li>");
                    }
                    else {
                        return true;
                    }
                }
                TopContent = TopContent + "<li><a href=\"" + item.MenuUrl + "\" class=\" White\" target=\"_blank\">" + item.MenuName + "</a></li> ";
            });


            funGetTopUserMenuOutList();


            $("#TopMenu").html(TopContent);

            $("#txtSearchAll").keydown(function() {


                funListBeginUp(event);

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



//获取ilog站内用户导航
function funGetTopUserMenuService(GetUrl, GetParts, GetWait) {
    $.ajax({
        //请求WebService Url         
        url: "" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenu",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        //请求参数              
        data: "" + GetParts + "",
        //成功           
        success: function(json) {


            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象
            var LoginType = 0;
            var TopContent = "";
            //循环获取值
            $.each(dataObj.UserMenu, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        $("#TopUserMenu").html("<li>加载错误</li>");
                    }
                    else {
                        LoginType = item.UrlState;
                        return true;
                    }
                }

                if (LoginType == 1) {
                    if (idx == 1) {
                        TopContent = TopContent + "<li id=\"GetGuanchang\"><a href=\"" + item.MenuUrl + "\" class=\" White F14\">" + item.MenuName + "</a></li> ";
                    }
                    else if (idx == 2) {
                        if (item.MenuLive == "1") {
                            TopContent = TopContent + "<li class=\"Bj\"><a href=\"" + item.MenuUrl + "\" class=\" White F14\">" + item.MenuName + "</a></li> ";
                        }
                        else {
                            TopContent = TopContent + "<li><a href=\"" + item.MenuUrl + "\" class=\" White\">" + item.MenuName + "</a></li> ";
                        }
                    }
                    else if (idx == 3) {
                        TopContent = TopContent + "<li class=\"Fw\"><a href=\"" + item.MenuUrl + "\" class=\" White\">" + item.MenuName + "</a></li> ";
                    }
                    else if (idx == 4) {
                        TopContent = TopContent + "<li id=\"GetMessage\"><span class=\"L\" ><a href=\"" + item.MenuUrl + "\" class=\" White\"   >" + item.MenuName + "</a></span><img class=\"img L\" src=\"http://simg.instrument.com.cn/ilog/blue/images/san.png\" /></li> ";
                    }
                    else {
                        TopContent = TopContent + "<li id=\"GetUser\"><span class=\"L\"><a href=\"" + item.MenuUrl + "\" class=\" White\"  >" + item.MenuName + "</a></span><img class=\"img L\" src=\"http://simg.instrument.com.cn/ilog/blue/images/san.png\" /></li> ";
                    }
                }
                else {
                    if (idx == 1) {
                        TopContent = TopContent + "<li id=\"GetGuanchang\"><a href=\"" + item.MenuUrl + "\" class=\" White F14\">" + item.MenuName + "</a></li> ";
                    }
                    else if (idx == 2) {
                        TopContent = TopContent + "<li><a href=\"" + item.MenuUrl + "\" class=\" White F14\">" + item.MenuName + "</a></li> ";
                    }
                    else {
                        TopContent = TopContent + "<li><a href=\"" + item.MenuUrl + "\" class=\" White F14\" onclick=\"return LoginDiv(16)\">" + item.MenuName + "</a></li> ";

                    }
                }
            });

            $("#TopUserMenu").html(TopContent);

            funGetTopGuanChangMenuUpList(LoginType);
            $("#GetGuanchang").mouseover(function() {
                setMenuPosition(this.id);
                MenuDivShowTop(this.id);
                $("#GetMessageOut_Menu").hide();
            });

            if (LoginType == 1) {
                funGetTopUserMenuUpList();
                $("#GetMessage").mouseover(function() {
                    setMenuPosition(this.id);
                    MenuDivShowTop(this.id);
                    $("#GetMessageOut_Menu").hide();
                });

                funGetTopUserSeetingsMenuUpList();
                $("#GetUser").mouseover(function() {
                    setMenuPosition(this.id);
                    MenuDivShowTop(this.id);
                    $("#GetMessageOut_Menu").hide();
                });
            }
            // alert(TopContent);      
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

//广场下拉列表
function funGetTopGuanChangMenuUpList(logintype) {
    $.ajax({
        //请求WebService Url         
        url: "" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenuGuangChangUpList",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        //请求参数              
        data: "{}",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            var TopNumConent = "";
            //循环获取值
            $.each(dataObj.UserMenuList, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        $("#GetGuanchang_Menu").html("<li>加载错误</li>");
                        return true;
                    }
                    else {
                        return true;
                    }
                }


                if (idx == 1) {
                    TopContent += "  <li class=\"L26  BrBlue\"><img src=\"" + item.ImgUrl + "\"  class=\"L\" style=\"margin-top:5px\" /><span class=\"Pl10\"><a href=\"" + item.MessageUrl + "\">" + item.MessageName + "</a></span></li>";
                }
                else {
                    TopContent += "  <li class=\"L26 \"><img src=\"" + item.ImgUrl + "\"  class=\"L\" style=\"margin-top:5px\" /><span class=\"Pl10\"><a href=\"" + item.MessageUrl + "\">" + item.MessageName + "</a></span></li>";
                }
            });
            var left = $("#GetGuanchang").offset().left;
            var extraLeft = $("#imgLogo").offset().left;

            $("#GetGuanchang_Menu").html(TopContent);
            $("#GetGuanchang_Menu").css({ "left": left - extraLeft });
        },
        //出错调试         
        error: function(x, e) {
            // alert(x.responseText); 
        },
        //执行成功后自动执行           
        complete: function(x) {

        }
    });
}


//获取ilog站内用户导航下拉列表
function funGetTopUserMenuUpList() {
    $.ajax({
        //请求WebService Url         
        url: "" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenuOutList",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        //请求参数              
        data: "{}",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            var TopNumConent = "";
            //循环获取值
            $.each(dataObj.UserMenuList, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        $("#GetMessage_Menu").html("");
                        return true;
                    }
                    else {
                        return true;
                    }
                }


                if (idx == 1) {
                    if (item.MessageNum > 0) {
                        TopNumConent = TopNumConent + "<li class=\"Fa\">" + item.MessageNum + "条@到我，<a href=\"" + item.MessageUrl + "\" class=\"Blue\">" + item.MessageName + "</a></li>";
                    }
                    else {
                        TopContent = TopContent + "<li class=\"Fa\"><a href=\"" + item.MessageUrl + "\">" + item.MessageName + "</a></li> ";
                    }
                }
                if (idx == 2) {
                    if (item.MessageNum > 0) {
                        TopNumConent = TopNumConent + "<li>";
                        TopNumConent = TopNumConent + " " + item.MessageNum + "条新评论，<a href=\"" + item.MessageUrl + "\" class=\"Blue\">" + item.MessageName + "</a></li>";
                    }
                    else {
                        TopContent = TopContent + "<li><a href=\"" + item.MessageUrl + "\">" + item.MessageName + "</a></li> ";
                    }
                }
                if (idx == 3) {
                    if (item.MessageNum > 0) {
                        TopNumConent = TopNumConent + "<li>";
                        TopNumConent = TopNumConent + "" + item.MessageNum + "个新粉丝，<a href=\"" + item.MessageUrl + "\" class=\"Blue\">" + item.MessageName + "</a></li>";
                    }
                    else {
                        TopContent = TopContent + "<li><a href=\"" + item.MessageUrl + "\">" + item.MessageName + "</a></li> ";
                    }
                }
                if (idx == 4) {
                    if (item.MessageNum > 0) {
                        TopNumConent = TopNumConent + "<li>";
                        TopNumConent = TopNumConent + " " + item.MessageNum + "条新站短，<a href=\"" + item.MessageUrl + "\" class=\"Blue\">" + item.MessageName + "</a></li>";
                    }
                    else {
                        TopContent = TopContent + "<li><a href=\"" + item.MessageUrl + "\">" + item.MessageName + "</a></li> ";
                    }
                }
            });

            TopContent = TopNumConent + TopContent;
            $("#GetMessage_Menu").html(TopContent);
            if (TopNumConent != "") {
                $("#GetMessage_Menu").width(160)
            }
        },
        //出错调试         
        error: function(x, e) {
            // alert(x.responseText); 
        },
        //执行成功后自动执行           
        complete: function(x) {

        }
    });
}


//获取ilog站内消息浮动提示
function funGetTopUserMenuOutList() {
    $.ajax({
        //请求WebService Url         
        url: "" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserMenuOutList",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        //请求参数              
        data: "{}",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            //循环获取值
            $.each(dataObj.UserMenuList, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        $("#GetMessage_Menu").html("");
                        return false;
                    }
                    else if (item.UrlState == 2) {
                        return TopContent = "";
                    }
                    else {
                        return true;
                    }
                }

                if (idx == 1) {
                    if (item.MessageNum > 0) {
                        TopContent = TopContent + "<li class=\"Fa\"><a href=\"javascript:void(0)\" onclick=\"funCloseOutMessage()\"><img class=\"R\" src=\"http://simg.instrument.com.cn/ilog/blue/images/co.gif\" style=\"margin:3px 3px 0 0\" /></a>";
                        TopContent = TopContent + " " + item.MessageNum + "条@到我，<a href=\"" + item.MessageUrl + "\" class=\"Blue\">" + item.MessageName + "</a></li>";
                    }
                }
                if (idx == 2) {
                    if (item.MessageNum > 0) {
                        if (TopContent == "") {
                            TopContent = TopContent + "<li><a href=\"javascript:void(0)\" onclick=\"funCloseOutMessage()\"><img class=\"R\" src=\"http://simg.instrument.com.cn/ilog/blue/images/co.gif\" style=\"margin:3px 3px 0 0\" /></a>";
                            TopContent = TopContent + " " + item.MessageNum + "条新评论，<a href=\"" + item.MessageUrl + "\" class=\"Blue\">" + item.MessageName + "</a></li>";
                        }
                        else {
                            TopContent = TopContent + "<li>";
                            TopContent = TopContent + " " + item.MessageNum + "条新评论，<a href=\"" + item.MessageUrl + "\" class=\"Blue\">" + item.MessageName + "</a></li>";
                        }
                    }
                }
                if (idx == 3) {
                    if (item.MessageNum > 0) {
                        if (TopContent == "") {
                            TopContent = TopContent + "<li><a href=\"javascript:void(0)\" onclick=\"funCloseOutMessage()\"><img class=\"R\" src=\"http://simg.instrument.com.cn/ilog/blue/images/co.gif\" style=\"margin:3px 3px 0 0\" /></a>";
                            TopContent = TopContent + " " + item.MessageNum + "个新粉丝，<a href=\"" + item.MessageUrl + "\" class=\"Blue\">" + item.MessageName + "</a></li>";
                        }
                        else {
                            TopContent = TopContent + "<li>";
                            TopContent = TopContent + " " + item.MessageNum + "个新粉丝，<a href=\"" + item.MessageUrl + "\" class=\"Blue\">" + item.MessageName + "</a></li>";
                        }
                    }
                }
                if (idx == 4) {
                    if (item.MessageNum > 0) {
                        if (TopContent == "") {
                            TopContent = TopContent + "<li><a href=\"javascript:void(0)\" onclick=\"funCloseOutMessage()\"><img class=\"R\" src=\"http://simg.instrument.com.cn/ilog/blue/images/co.gif\" style=\"margin:3px 3px 0 0\" /></a>";
                            TopContent = TopContent + " " + item.MessageNum + "条新站短，<a href=\"" + item.MessageUrl + "\" class=\"Blue\">" + item.MessageName + "</a></li>";
                        }
                        else {
                            TopContent = TopContent + "<li>";
                            TopContent = TopContent + " " + item.MessageNum + "个新站短，<a href=\"" + item.MessageUrl + "\" class=\"Blue\">" + item.MessageName + "</a></li>";
                        }
                    }
                }
            });

            if (TopContent == "") {
                $("#GetMessageOut_Menu").hide();
            }
            else {
                if (!$("#GetMessage_Menu").is(":visible") && !$("#GetUser_Menu").is(":visible")) {
                    $("#GetMessageOut_Menu").show();
                    $("#GetMessageOut_Menu").html(TopContent);
                }
                else {
                    $("#GetMessageOut_Menu").hide();
                }
            }
        },
        //出错调试         
        error: function(x, e) {
            // alert(x.responseText); 
        },
        //执行成功后自动执行           
        complete: function(x) {

        }
    });
}

//关闭下拉消息
function funCloseOutMessage() {
    $.ajax({
        //请求WebService Url         
        url: "" + vServiceUrl + "ILogFollow.asmx/ILogPageOutMessageNumState",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        //请求参数              
        data: "{}",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            //循环获取值
            $.each(dataObj.MessageState, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "1") {
                        $("#GetMessageOut_Menu").hide();
                        return true;
                    }
                }
            });
        },
        //出错调试         
        error: function(x, e) {
            // alert(x.responseText); 
        },
        //执行成功后自动执行           
        complete: function(x) {

        }
    });

}

//获取ilog站内用户导航下拉列表
function funGetTopUserSeetingsMenuUpList() {
    $.ajax({
        //请求WebService Url         
        url: "" + vServiceUrl + "ILogUserMenu.asmx/ILogGetUserSettingsUpList",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        //请求参数              
        data: "{}",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            //循环获取值
            $.each(dataObj.UserMenuList, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        $("#GetMessage_Menu").html("");
                        return true;
                    }
                    else {
                        return true;
                    }
                }

                if (idx == 1) {
                    TopContent += "<div class=\"Hr_5\"></div>";
                    TopContent += "<li class=\"L40\"><a href=\"" + item.MessageUrl + "\">"
                    TopContent += "<img src=\"" + (item.Face != "" ? "/images/face/small/" + item.Face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"" + item.MessageName + "\" width=\"30\" height=\"30\" class=\"L\" style=\"margin-top:5px\" /></a>";
                    TopContent += "<span class=\" Pl10\"><a href=\"" + item.MessageUrl + "\">" + item.MessageName + "</a></span></li>";
                }
                if (idx == 2) {
                    TopContent += "<div class=\" Line_solid \"></div><div class=\"Hr_10\"></div>";
                    TopContent += "<li class=\"L26  BrBlue\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-6.gif\"   class=\"L\" style=\"margin-top:5px\" /></a><span class=\"Pl10\"><a href=\"" + item.MessageUrl + "\">" + item.MessageName + "</a></span></li>";
                }
                if (idx == 3) {
                    TopContent += "  <li><img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico_9.png\" class=\"L\" style=\"margin-top:2px\" /><span class=\"Pl10\"><a href=\"" + item.MessageUrl + "\">" + item.MessageName + "</a></span></li> <div class=\"Hr_10\"></div>";
                }
            });
            $("#GetUser_Menu").html(TopContent);
        },
        //出错调试         
        error: function(x, e) {
            // alert(x.responseText); 
        },
        //执行成功后自动执行           
        complete: function(x) {

        }
    });
}



//获取ilog Left menu
function funGetleftMenuService(GetUrl, GetParts, GetWait) {
    $.ajax({
        //请求WebService Url         
        url: "" + GetUrl + "",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        //请求参数              
        data: "" + GetParts + "",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var Content = "";
            //循环获取值
            $.each(dataObj.leftMenu, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        $("#TopMenu").html("<li>加载错误</li>");
                    }
                    else {
                        return true;
                    }
                }
                Content = Content + "<li " + item.MenuLive + "><a " + item.MenuHrefLive + " href=\"" + item.MenuUrl + "\"><span class=\"" + item.MenuIco + "\"></span>" + item.MenuName + "</a></li>";
            });
            $("#leftmenu").html(Content);
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



//获取ilog Left menu
function funGetSettingsleftMenuService(GetUrl, GetParts, GetWait) {
    $.ajax({
        //请求WebService Url         
        url: "" + GetUrl + "",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        //请求参数              
        data: "" + GetParts + "",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var Content = "";
            //循环获取值
            $.each(dataObj.leftMenu, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        $("#leftMenu").html("<li>加载错误</li>");
                    }
                    else {
                        return true;
                    }
                }
                Content = Content + "<li " + item.MenuLive + "><a " + item.MenuHrefLive + " href=\"" + item.MenuUrl + "\"><span class=\"" + item.MenuIco + "\"></span>" + item.MenuName + "</a></li>";
            });
            $("#leftmenu").html(Content);
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

function funGetAd(GetUrl, GetParts) {

    $.ajax({
        //请求WebService Url         
        url: "" + GetUrl + "",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        //请求参数              
        data: "" + GetParts + "",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var Content = "";
            //循环获取值
            $.each(dataObj.leftMenu, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        $("#ILogAd").html("<li>加载错误</li>");
                    }
                    else {
                        return true;
                    }
                }
                Content = Content + "<li " + item.MenuLive + "><a " + item.MenuHrefLive + " href=\"" + item.MenuUrl + "\"><span class=\"" + item.MenuIco + "\"></span>" + item.MenuName + "</a></li>";
            });
            $("#leftmenu").html(Content);
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

var rand = Math.random(Math.random() * 10000)



//下拉菜单的定位
function setMenuPosition(ShowID) {
    var offset = $('#' + ShowID).offset();
    var divheight = $('#' + ShowID).innerHeight();
    var leftpadd = 0


    $('#' + ShowID + '_Menu').css({
        //'left':offset.left ,
        //'top':divheight-10,
        'position': 'absolute'
    }).show();
}

function setMenuPositions(ShowID) {
    var offset = $('#' + ShowID).offset();
    var divheight = $('#' + ShowID).innerHeight();
    var leftpadd = 235
    //this=$('#'+ShowID);
    //    var t = this.offsetTop;
    //    while( this == this.offsetParent)
    //    {
    //    t +=  this.offsetTop;
    //    }
    var top = document.documentElement.scrollTop;
    $('#' + ShowID + '_Menu').css({
        'left': offset.left - 30,
        'top': offset.top - top + 12,
        'position': 'fixed'
    }).show();
}

function setMenuPositionslist(ShowName, ShowID) {
    var offset = $('#' + ShowName + "_" + ShowID).offset();
    var divheight = $('#' + ShowName + "_" + ShowID).innerHeight();
    var leftpadd = 210
    $('#' + ShowName + "_" + ShowID + '_Menu').css({
        'position': 'absolute'
    }).show();
}

function setMenuPositionsSearch(ShowID) {
    var offset = $('#txtSearchAll').offset();
    var divheight = $('#txtSearchAll').innerHeight();
    var leftpadd = 0;
    $('#' + ShowID + '_Menu').css({
        //'left':offset.left + -208, //左右定位
        //'top':offset.top+20,       //上下定位
        'position': 'absolute'
    }).show();
}
//显示或隐藏层

function MenuDivShow(showdiv) {
    $('#' + showdiv + '_Menu').mouseover(function() { $(this).show(); });
    $('#' + showdiv + '_Menu').mouseout(function() { $(this).hide(); });
    $('#' + showdiv).mouseout(function() { $('#' + showdiv + '_Menu').hide(); });
}

function MenuDivShowTop(showdiv) {
    $('#' + showdiv + '_Menu').mouseover(function() { $(this).show(); $("#GetMessageOut_Menu").hide(); });
    $('#' + showdiv + '_Menu').mouseout(function() { $(this).hide(); funGetTopUserMenuOutList(); });
    $('#' + showdiv).mouseout(function() { $('#' + showdiv + '_Menu').hide(); funGetTopUserMenuOutList(); });


}


function MenuDivShowList(showdiv, HrID) {
    $('#' + showdiv + "_" + HrID + '_Menu').mouseover(function() { $(this).show(); });
    $('#' + showdiv + "_" + HrID + '_Menu').mouseout(function() { $(this).hide(); });
    $('#' + showdiv + '_' + HrID + '').mouseout(function() { $('#' + showdiv + '_' + HrID + '_Menu').hide(); });
}



//回到顶部
function GetTop() {
    var w = 90;
    var h = 100;
    var str = "";
    var obj = document.getElementById("divStayTopLeft");
    if (obj) str = obj.innerHTML;
    if (typeof document.compatMode != 'undefined' && document.compatMode != 'BackCompat') {
        document.writeln('<DIV  style="z-index:9;right:0;bottom:0;height:' + h + 'px;width:' + w + 'px;overflow:hidden;POSITION:fixed;_position:absolute; _margin-top:expression(document.documentElement.clientHeight-this.style.pixelHeight+document.documentElement.scrollTop);">');
    }
    else {
        document.writeln('<DIV  style="z-index:9;right:0;bottom:0;height:' + h + 'px;width:' + w + 'px;overflow:hidden;POSITION:fixed;*position:absolute; *top:expression(eval(document.body.scrollTop)+eval(document.body.clientHeight)-this.style.pixelHeight);">');
    }
    document.writeln('<div style="clear:both;height:60px;"><a href="javascript:scroll(0,0)" hidefocus="true"><img src="http://simg.instrument.com.cn/ilog/blue/images/top_img.png" alt="回到顶部" style="border: 0px;" /></a></div> ');
    document.write('<div style="clear:both;margin:auto;overflow:hidden;text-align:left;">' + str + '</div>');
    document.writeln('</DIV>');
}

//举报
//messageid 举报id
//type 举报类型 0是微博,1是站短
function funReport(messageid, type) {
    alert("请待心等！尚未处理");
}

//接收参数
function getParameter(param) {
    var query = window.location.search;
    var iLen = param.length;
    var iStart = query.indexOf(param);
    if (iStart == -1)
        return "";
    iStart += iLen + 1;
    var iEnd = query.indexOf("&", iStart);
    if (iEnd == -1)
        return query.substring(iStart);

    return query.substring(iStart, iEnd);
}



//获取关注导航 左侧导航
//GetUrl 请求的服务 Url
//GetParts 请求的参数
//GetWait 等待参数
function funGetFollowMenuService(GetParts, GetWait, UserGroupID) {

    $.ajax({
        url: "" + vServiceUrl + "ILogFollow.asmx/ILogFollowGetTopMenu",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "" + GetParts + "",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            var ToLiContent = "";
            var TopMore = "";
            $.each(dataObj.FollowMenu, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        $("#FollowMenu").html("<li>加载错误</li>");
                    }
                    else {
                        return true;
                    }
                }
                if (idx == 1) {
                    if (item.MenuLive == 1) {
                        TopContent = TopContent + "<li><div class=\"top\"></div><div class=\"center\"><a href=\"/Follow\" class=\"Blue\"><strong>" + item.MenuName + "</strong></a></div></li>";
                    }
                    else {
                        TopContent = TopContent + "<li><a href=\"/Follow\" >" + item.MenuName + "</a></li>";
                    }
                }
                if (idx == 2) {
                    if (item.MenuLive == 2) {
                        TopContent = TopContent + "<li><div class=\"top\"></div><div class=\"center\"><a href=\"/Friends\" class=\"Blue\"><strong>" + item.MenuName + "</strong></a></div></li>";
                    }
                    else {
                        TopContent = TopContent + "<li><a href=\"/Friends\" >" + item.MenuName + "</a></li>";
                    }
                }
                if (idx == 3) {
                    if (item.MenuLive == 3) {
                        TopContent = TopContent + "<li><div class=\"top\"></div><div class=\"center\"><a href=\"/FollowC\" class=\"Blue\"><strong>" + item.MenuName + "</strong></a></div></li>";
                    }
                    else {
                        TopContent = TopContent + "<li><a href=\"/FollowC\" >" + item.MenuName + "</a></li>";
                    }
                }
                if (idx >= 4 && idx <= 5) {
                    if (item.MenuLive == 4) {

                        if (item.MenuID == UserGroupID) {
                            $("#followName").val(item.MenuName);
                            TopContent = TopContent + "<li><div class=\"top\"></div><div class=\"center\"><a href=\"/FollowC_" + item.MenuID + "\" class=\"Blue\"><strong>" + item.MenuName + "</strong></a></div></li>";
                        }
                        else {
                            TopContent = TopContent + "<li><a href=\"/FollowC_" + item.MenuID + "\" >" + item.MenuName + "</a></li>";
                        }
                    }
                    else {
                        if (item.MenuID == UserGroupID) {
                            $("#followName").val(item.MenuName);
                            TopContent = TopContent + "<li><div class=\"top\"></div><div class=\"center\"><a href=\"/FollowC_" + item.MenuID + "\" class=\"Blue\"><strong>" + item.MenuName + "</strong></a></div></li>";
                        }
                        else {
                            TopContent = TopContent + "<li><a href=\"/FollowC_" + item.MenuID + "\" >" + item.MenuName + "</a></li>";
                        }
                    }
                }
                if (idx >= 6) {
                    if (idx >= 6) {
                        TopMore = "<li><a href=\"javascript:void(0);\" id=\"GroupMore\">更多</a></li>";
                    }
                    if (item.MenuID == UserGroupID) {
                        $("#followName").val(item.MenuName);
                        TopContent = TopContent + "<li><div class=\"top\"></div><div class=\"center\"><a href=\"/FollowC_" + item.MenuID + "\" class=\"Blue\"><strong>" + item.MenuName + "</strong></a></div></li>";
                    }
                    else {
                        if (idx == 6) {
                            ToLiContent = ToLiContent + "<li class=\"bj\"><a href=\"/FollowC_" + item.MenuID + "\" >" + item.MenuName + "</a></li>";
                        }
                        else {
                            ToLiContent = ToLiContent + "<li><a href=\"/FollowC_" + item.MenuID + "\" >" + item.MenuName + "</a></li>";
                        }
                    }
                }
            });
            $("#FollowMenu").html(TopContent + TopMore);
            $("#MenuUL").html(ToLiContent);
            $("#GroupMore").click(function() {
                setMenuPositions(this.id);
                MenuDivShow(this.id);
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

//获取我的粉丝
//GetUrl 请求的服务 Url
//GetParts 请求的参数
//GetWait 等待参数
function funGetFan(GetParts, GetWait, DivID) {

    $.ajax({
        url: "" + vServiceUrl + "ILogServiceUserCount.asmx/ILogServiceUserNumCount",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "" + GetParts + "",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            $.each(dataObj.IlogCount, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        $("#" + DivID).text(item.Fan);
                    }
                    else {
                        return true;
                    }
                }
                else {
                    $("#" + DivID).text(item.Fan);
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

//获取我关注了多少人
//GetUrl 请求的服务 Url
//GetParts 请求的参数
//GetWait 等待参数
function funGetConcern(GetParts, GetWait, DivID) {

    $.ajax({
        url: "" + vServiceUrl + "ILogServiceUserCount.asmx/ILogServiceUserNumCount",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "" + GetParts + "",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            $.each(dataObj.IlogCount, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        $("#" + DivID).text(item.Concern);
                    }
                    else {
                        return true;
                    }
                }
                else {
                    $("#" + DivID).text(item.Concern);
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



//显示I添加组窗口
function ShowGroupFollow() {

    showGroupDialog = $.dialog({
        id: "divShowGroup",
        title: false,
        content: showGroup(),
        max: false,
        min: false,
        lock: true,
        cache: false,
        //width: 365,
        // height: 130,
        padding: 0


    });

    //取消分组
    $("#btnCancle").click(function() {
        CloseShowGroup();
    });
    //添加分组
    $("#btntrue").click(function() {
        ShowGroupNewAdd();
    });
}


//添加分组
//GetUrl 请求的服务 Url
//GetParts 请求的参数
//GetWait 等待参数
function ShowGroupNewAdd() {

    var TxtGroupName = ignoreSpaces($("#txtGroupName").val());
    if (TxtGroupName == "" || TxtGroupName == null || TxtGroupName == "输入新分组名称") {
        ShowErrorMessage("分组名称不能为空！");

        //$("#txtGroupName").focus();

        return false;
    }
    else {
        if (TxtGroupName.length > 4) {
            ShowErrorMessage("分组名称长度不能超过4个汉字！");

            //  $("#txtGroupName").focus();
            return false;
        }
        else if (TxtGroupName.length < 2) {
            ShowErrorMessage("分组名称长度不能小于2个汉字！");

            // $("#txtGroupName").focus();

            return false;
        }
    }


    $.ajax({
        url: "" + vServiceUrl + "ILogFollow.asmx/ILogFollowInsertGroup",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{groupName:'" + TxtGroupName + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            var ToLiContent = "";
            $.each(dataObj.GroupInsert, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        ShowErrorMessage("设置失败，请重新尝试！");
                        return false;
                    }
                    else if (item.UrlState == "2") {
                        ShowErrorMessage("已经存在，不能重复设置！");
                        return false;
                    }
                    else if (item.UrlState == "3") {
                        ShowErrorMessage("最多只能设置10个分组！");
                        return false;
                    }
                }
                else {
                    CloseShowGroup();

                    ShowGroupFollowTrue(item.ListID);

                    return true;
                }
            });
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


//粉丝页添加组窗口
function ShowGroupConcernAdd(iufid, concernuserid, nikename) {

    showGroupDialog = $.dialog({
        id: "divShowGroup",
        title: false,
        content: showGroup(),
        max: false,
        min: false,
        lock: true,
        cache: false,
        //width: 365,
        // height: 130,
        padding: 0


    });

    //取消分组
    $("#btnCancle").click(function() {
        CloseShowGroup();
    });
    //添加分组
    $("#btntrue").click(function() {
        ShowGroupNewConcernAdd(iufid, concernuserid, nikename);
    });
}


//在下拉列表中添加用户组
function ShowGroupUpListFollow(concernuserid) {
    //alert(concernuserid);
    showGroupDialog = $.dialog({
        id: "divShowGroup",
        title: false,
        content: showGroup(),
        max: false,
        min: false,
        lock: true,
        cache: false,
        // width: 365,
        // height: 130,
        padding: 0


    });

    //取消分组
    $("#btnCancle").click(function() {
        CloseShowGroup();
    });
    //添加分组
    $("#btntrue").click(function() {
        ShowGroupNewAdd_ConcernUserID(concernuserid);
    });
}

function ShowWinEditGroup(GroupID, GroupName) {

    showGroupDialog = $.dialog({
        id: "divShowGroup",
        title: false,
        content: showEditGroup(GroupID, GroupName),
        max: false,
        min: false,
        lock: true,
        cache: false,
        padding: 0


    });

    //取消编辑分组
    $("#btnCancle").click(function() {
        CloseShowGroup();
    });
    //编辑分组
    $("#btnEdittrue").click(function() {
        ShowGroupEdit(GroupID);
    });
}


//添加分组
//GetUrl 请求的服务 Url
//GetParts 请求的参数
//GetWait 等待参数
function ShowGroupNewConcernAdd(iufid, concernuserid, nikename) {


    var TxtGroupName = ignoreSpaces($("#txtGroupName").val());
    if (TxtGroupName == "" || TxtGroupName == null || TxtGroupName == "输入新分组名称") {
        ShowErrorMessage("分组名称不能为空！");

        //$("#txtGroupName").focus();

        return false;
    }
    else {
        if (TxtGroupName.length > 4) {
            ShowErrorMessage("分组名称长度不能超过4个汉字！");

            //  $("#txtGroupName").focus();
            return false;
        }
        else if (TxtGroupName.length < 2) {
            ShowErrorMessage("分组名称长度不能小于2个汉字！");

            // $("#txtGroupName").focus();

            return false;
        }
    }


    $.ajax({
        url: "" + vServiceUrl + "ILogFollow.asmx/ILogFollowInsertGroup",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{groupName:'" + TxtGroupName + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            var ToLiContent = "";
            $.each(dataObj.GroupInsert, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        ShowErrorMessage("设置失败，请重新尝试！");
                        return false;
                    }
                    else if (item.UrlState == "2") {
                        ShowErrorMessage("已经存在，不能重复设置！");
                        return false;
                    }
                    else if (item.UrlState == "3") {
                        ShowErrorMessage("最多只能设置10个分组！");
                        return false;
                    }
                }
                else {
                    CloseShowGroup();

                    //关注操作框
                    showConcerntDialog = $.dialog
               ({
                   id: "divrDialog",
                   title: false,
                   content: ShowAddFollowTrueInfo(iufid, concernuserid, nikename),
                   max: false,
                   min: false,
                   lock: true,
                   cache: false,
                   //width: 370,
                   //height: 210,
                   padding: 0

               });

                    return true;
                }
            });
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


//添加分组
//GetUrl 请求的服务 Url
//GetParts 请求的参数
//GetWait 等待参数
function ShowGroupNewAdd_ConcernUserID(ConcernUserID) {

    var TxtGroupName = ignoreSpaces($("#txtGroupName").val());
    if (TxtGroupName == "" || TxtGroupName == null || TxtGroupName == "输入新分组名称") {
        ShowErrorMessage("分组名称不能为空！");

        //$("#txtGroupName").focus();

        return false;
    }
    else {
        if (TxtGroupName.length > 4) {
            ShowErrorMessage("分组名称长度不能超过4个汉字！");

            //  $("#txtGroupName").focus();
            return false;
        }
        else if (TxtGroupName.length < 2) {
            ShowErrorMessage("分组名称长度不能小于2个汉字！");

            // $("#txtGroupName").focus();

            return false;
        }
    }


    $.ajax({
        url: "" + vServiceUrl + "ILogFollow.asmx/ILogFollowInsertGroup_ConcernUserID",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{groupName:'" + TxtGroupName + "',concernUserID:'" + ConcernUserID + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            var ToLiContent = "";
            $.each(dataObj.GroupInsert, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        ShowErrorMessage("设置失败，请重新尝试！");
                        return false;
                    }
                    else if (item.UrlState == "2") {
                        ShowErrorMessage("已经存在，不能重复设置！");
                        return false;
                    }
                    else if (item.UrlState == "3") {
                        ShowErrorMessage("最多只能设置10个分组！");
                        return false;
                    }
                }
                else {
                    CloseShowGroup();

                    ShowGroupFollowTrue(item.ListID);

                    return true;
                }
            });
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


//编辑分组
//GetUrl 请求的服务 Url
//GetParts 请求的参数
//GetWait 等待参数
function ShowGroupEdit(GroupID) {

    var GroupID = GroupID;
    var TxtGroupName = ignoreSpaces($("#txtGroupName").val());
    if (TxtGroupName == "" || TxtGroupName == null || TxtGroupName == "输入新分组名称") {
        ShowErrorMessage("分组名称不能为空！");

        //$("#txtGroupName").focus();

        return false;
    }
    else {
        if (TxtGroupName.length > 4) {
            ShowErrorMessage("分组名称长度不能超过4个汉字！");

            //  $("#txtGroupName").focus();
            return false;
        }
        else if (TxtGroupName.length < 2) {
            ShowErrorMessage("分组名称长度不能小于2个汉字！");

            // $("#txtGroupName").focus();

            return false;
        }
    }

    $.ajax({
        url: "" + vServiceUrl + "ILogFollow.asmx/ILogFollowEditGroupName",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{groupName:'" + TxtGroupName + "',GroupID:'" + GroupID + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            var ToLiContent = "";
            $.each(dataObj.GroupEdit, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        ShowErrorMessage("设置失败，请重新尝试！");
                        return false;
                    }
                    else if (item.UrlState == "2") {
                        ShowErrorMessage("已经不存在！");
                        return false;
                    }
                    else {
                        CloseShowGroup();

                        ShowGroupFollowTrue(GroupID);

                        return true;
                    }
                }
            });
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



//自动弹层，提示设置成功，并且2秒钟后自动消息
function ShowGroupFollowTrue(groupid) {
    $.dialog({
        id: "divShowGroupTrue",
        title: false,
        content: ShowInfo(),
        max: false,
        min: false,
        lock: true,
        cache: false,
        time: 2,
        padding: 0


    });

    if (groupid > 0) {
        setTimeout("funRedirect('/FollowC_" + groupid + "')", 1000);
    }
    else {
        setTimeout("funRedirect('/Follow')", 1000);
    }

}


//自动弹层，提示设置成功，并且2秒钟后自动消息
function ShowGroupTaFollowTrue() {
    $.dialog({
        id: "divShowGroupTrue",
        title: false,
        content: ShowInfo(),
        max: false,
        min: false,
        lock: true,
        cache: false,
        time: 2,
        padding: 0


    });

}

//自动弹出层，错误提示
function ShowErrorMessage(message) {
    $.dialog({
        id: "divShowError",
        title: false,
        content: ShowErrorInfo(message),
        max: false,
        min: false,
        // lock: true,
        cache: false,
        time: 2,
        padding: 0


    });
}

//
function funRedirect(PageUrl) {
    location.href = "" + PageUrl + "";
}


//显示分组
function showGroup() {

    ShowTable = '<div class=\"WindowWark350\"><h1 class="WindowTil G4 F14"> ';
    ShowTable += '<a href="javascript:void(0);" onclick="CloseShowGroup()"><img class=" R Img" src=\"http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif\" alt="关闭"  /> ';
    ShowTable += '</a>创建分组</h1> ';

    ShowTable += '<div class=\"WindowBox\"> <ul class=\"WindowBD G4\"> ';
    ShowTable += '<li><span class=\"Span L\">分组名称：</span> ';
    ShowTable += '<input class=\"input G9" value="输入新分组名称\" onfocus=\"if(this.value==\'输入新分组名称\')this.value=\'\'\" ';
    ShowTable += 'onblur=\"if(!this.value)this.value=\'输入新分组名称\'\" type=\"text\" name=\"txtGroupName\" id=\"txtGroupName\" maxlength=\"4\" />';
    ShowTable += ' </li><li></li> </ul> ';


    ShowTable += '<div class=\"Hr_10\"></div> ';
    ShowTable += '<div class="WinBtn_H R"><span> ';
    ShowTable += '<input name=\"btnCancle\" type=\"button\" id=\"btnCancle\" value=\"取消\" /></span></div> ';
    ShowTable += '<div class=\"WinBtn  R\"><span>';
    ShowTable += '<input name=\"btntrue\" type=\"button\" id=\"btntrue\" value=\"确定\" /></span></div> ';
    ShowTable += '<div class=\"Hr_10\"></div></div></div>';

    return ShowTable;
}

//编辑分组
function showEditGroup(GroupID, GroupName) {



    ShowTable = '<div class=\"WindowWark350\"><h1 class="WindowTil G4 F14"> ';
    ShowTable += '<a href="javascript:void(0);" onclick="CloseShowGroup()"><img class=" R Img" src=\"http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif\" alt="关闭"  /> ';
    ShowTable += '</a>编辑分组</h1> ';

    ShowTable += '<div class=\"WindowBox\"> <ul class=\"WindowBD G4\"> ';
    ShowTable += '<li><span class=\"Span L\">分组名称：</span> ';
    ShowTable += '<input class=\"input G9" value=\"' + GroupName + '\" onfocus=\"if(this.value==\'输入新分组名称\')this.value=\'\'\" ';
    ShowTable += 'onblur=\"if(!this.value)this.value=\'输入新分组名称\'\" type=\"text\" name=\"txtGroupName\" id=\"txtGroupName\" maxlenght=\"4\" />';
    ShowTable += ' </li><li></li> </ul> ';


    ShowTable += '<div class=\"Hr_10\"></div> ';
    ShowTable += '<div class="WinBtn_H R"><span> ';
    ShowTable += '<input name=\"btnCancle\" type=\"button\" id=\"btnCancle\" value=\"取消\" /></span></div> ';
    ShowTable += '<div class=\"WinBtn  R\"><span>';
    ShowTable += '<input name=\"btntrue\" type=\"button\" id=\"btnEdittrue\" value=\"确定\" /></span></div> ';
    ShowTable += '<div class=\"Hr_10\"></div></div></div>';

    return ShowTable;
}

function ShowDeleteGroup(GroupID) {
    var followName = $("#followName").val();
    ShowDiv = "<div class=\"WindowWark350\"><h1 class=\"WindowTil G4 F14\"><a href=\"javascript:void(0)\" onclick=\"CloseShowDeleteGroup()\">";
    ShowDiv += "<img class=\" R Img\" src=\"http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif\" alt=\"关闭\"  /></a>确认删除</h1>";
    ShowDiv += "<div class=\"WindowBox \">";
    ShowDiv += " <div class=\" F14 L30 G4  \"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/ask.gif\" class=\"L\" />";
    ShowDiv += "<div class=\"L Pl10\">确定要删除&ldquo;<a href=\"#\" class=\"Blue\">" + followName + "</a>&rdquo;分组吗？<br />";
    ShowDiv += "删除后该组成员将不会被取消关注，<br />";
    ShowDiv += "这些人将自动归为&ldquo;未分组&rdquo;中</div></div>";
    ShowDiv += "<div class=\"Hr_10\"></div>";
    ShowDiv += "<div class=\"WinBtn_H R\"><span><input name=\"btndelcancle\" type=\"button\" id=\"btndelcancle\" value=\"取消\" />";
    ShowDiv += "</span></div>";
    ShowDiv += "<div class=\"WinBtn  R\"><span><input name=\"btndeltrue\" type=\"button\" id=\"btndeltrue\" value=\"确定\" />";
    ShowDiv += "</span></div><div class=\"Hr_10\"></div> </div></div>";

    return ShowDiv;
}

function ShowGroupDelete(GroupID) {

    showGroupDeleteDialog = $.dialog({
        id: "divShowGroup",
        title: false,
        content: ShowDeleteGroup(GroupID),
        max: false,
        min: false,
        lock: true,
        cache: false,
        padding: 0


    });

    //取消分组
    $("#btndelcancle").click(function() {
        CloseShowDeleteGroup();
    });
    //确认删除
    $("#btndeltrue").click(function() {
        ShowSubmitGroupDelete(GroupID);
    });
}

//关闭分组
function CloseShowDeleteGroup() {
    showGroupDeleteDialog.close();
}

//删除分组
//GetUrl 请求的服务 Url
//GetParts 请求的参数
//GetWait 等待参数
function ShowSubmitGroupDelete(GroupID) {


    $.ajax({
        url: "" + vServiceUrl + "ILogFollow.asmx/ILogFollowDeleteGoupID",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{GroupID:'" + GroupID + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            var ToLiContent = "";
            $.each(dataObj.GroupEdit, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        ShowErrorMessage("设置失败，请重新尝试！");
                        return false;
                    }
                    else if (item.UrlState == "2") {
                        ShowErrorMessage("已经不存在！");
                        return false;
                    }
                    else {
                        CloseShowDeleteGroup();

                        ShowGroupFollowTrue(0);

                        return true;
                    }
                }
            });
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


//获取一个分组名称
function ShowGetGroupName(GroupID) {
    var GroupName = "";
    $.ajax({
        url: "" + vServiceUrl + "ILogUserGroup.asmx/ILogGetGroupName",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{groupID:'" + GroupID + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            $.each(dataObj.GroupList, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        $.dialog.tips("对不起没有找到相关分组！");
                        return false;
                    }
                }
                else {
                    GroupName = item.GroupName;
                    ShowWinEditGroup(GroupID, GroupName)
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
    return GroupName;
}
//设置成功
function ShowInfo() {

    ShowTable = '<div class=\"WindowWark280\"><div class=\"WindowBox Tc\"> ';
    ShowTable += '<div class=\" Tc F14  WindowSak\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/ok.gif\" class=\"L\" />恭喜，设置成功啦。</div> ';
    ShowTable += '<div class="Hr_10"></div> <div class="Hr_10"></div></div></div>'

    return ShowTable;
}

//内容提示
function ShowErrorInfo(message) {

    ShowTable = '<div class=\"WindowWark280\"> <div class=\"WindowBox\">';
    ShowTable += '<div class=\" F14 WindowSak\" ><div class="\L G4 WindowW L30\">' + message + '</div> ';
    ShowTable += '<img src="http://simg.instrument.com.cn/ilog/blue/images/faceK.gif" /></div> <div class="Hr_10"></div></div></div>'

    return ShowTable;
}

//
function ShowInfoEmpty() {

    ShowTable = '<div class=\"WindowWark280\"><div class=\"WindowBox Tc\"> ';
    ShowTable += '<div class=\" Tc F14  WindowSak\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/ok.gif\" class=\"L\" />搜索内容不能为空。</div> ';
    ShowTable += '<div class="Hr_10"></div> <div class="Hr_10"></div></div></div>'

    return ShowTable;
}


//关闭登录框
function CloseShowGroup() {

    showGroupDialog.close();

}


//搜索下拉
function GetSearchUpList() {

    var txtSearchValue = $("#txtSearchAll").val();

    $("#searchPeople").text(txtSearchValue);

    $("#searchilog").text(txtSearchValue);

    if (txtSearchValue != null && txtSearchValue != "") {

        setMenuPositionsSearch("GetSearch");

        MenuDivShow("GetSearch");
    }
    else {
        $("#GetSearch_Menu").hide();
    }

}

//搜索下拉
function funListBeginUp(e) {

    var keynum;

    if (window.event) // IE
    {
        keynum = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        keynum = e.which;
    }

    if (keynum != 13 && keynum != 38 && keynum != 40) {
        GetSearchUpList();
    }
    else if (keynum == 13) {
        funListBeginUpUL("GetSearch_Menu", 2);
    }
    else if (keynum == 38) {
        GetSearchUpList();
        funListBeginUpUL("GetSearch_Menu", 0);
    }
    else if (keynum == 40) {
        GetSearchUpList();
        funListBeginUpUL("GetSearch_Menu", 1);
    }

    return false;

}

function funListBeginUpUL(NameID, vType) {

    var bl = true;
    $("#" + NameID + " li").each(function(i) {

        if (vType == 0) {
            if ($(this).hasClass("WindowBG")) {
                if (bl) {

                    //向上
                    $(this).removeClass("WindowBG");
                    $(this).prev().addClass("WindowBG");
                    bl = false
                }
            }

        }
        else if (vType == 1) {
            if ($(this).hasClass("WindowBG")) {
                if (bl) {

                    //向下
                    $(this).removeClass("WindowBG");
                    $(this).next().addClass("WindowBG");
                    bl = false
                }
            }
        }
        else if (vType == 2) {
            if ($(this).hasClass("WindowBG")) {
                GetSeachALL(i);
            }
        }

    })

}
//判断下拉列表是否显示当中，如果显示当中就不执行执行
//zhangl 20120713
function GetBtnSeachALL() {
    if (!LoginDiv(16)) {
        return;
    }

    if ($("#GetSearch_Menu").is(":hidden")) {
        //隐藏
        GetSeachALL(0);
    }



}

//top顶部搜索 
function GetSeachALL(Type) {
    var txtSearchValue = $("#txtSearchAll").val();

    if (txtSearchValue == "搜索微博、找人") {
        txtSearchValue = "";
    }

    if (Type == 0) {

        if (txtSearchValue != "") {
            location.href = "../search_" + txtSearchValue + "";
        }
        else {
            location.href = "../search";
        }
    }
    else {


        if (txtSearchValue != "") {
            location.href = "../rsearch_" + txtSearchValue + "";
        }
        else {
            location.href = "../rsearch";
        }
    }

}


///举取内容
//nickname：昵称
//face：头像
//content：内容
//userid：用户id
//stri：用户vi_memberlevel认证
function funGetPageReport(userid, nickname, face, content, vi_memberlevel) {
    ShowDiv = "<div class=\"WindowWark440\">";
    ShowDiv += "<h1 class=\"WindowTil F14 G4\"><a href=\"javascript:void(0)\" id=\"btnCancle\"><img class=\" R Img\" src=\"http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif\" alt=\"关闭\"  /></a>举报违规行为</h1>";

    //加i处理
    var strImg = ShowVerifyImg(vi_memberlevel);

    ShowDiv += "<div class=\"WindowBox \"><div class=\" G4  \">你要举报的是 “<a href=\"/u_" + userid + "\" class=\"Blue Fa\">@" + nickname + "</a>" + strImg + "”发的微博： </div>";
    ShowDiv += "<div class=\"Hr_5\"></div><div class=\" BrBlue  G6\">";
    ShowDiv += "<div class=\"Hr_5\"></div> <a href=\"/u_" + userid + "\">";
    ShowDiv += "<img src=\"" + (face != "" ? "images/face/small/" + face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"头像\" width=\"30\" height=\"30\" class=\"L WindowIMG\" /></a>";
    ShowDiv += "<p class=\"L18 L Centent\" style=\"width:350px; display:block; \"><a href=\"/u_" + userid + "\" class=\"Blue Fa\">@" + nickname + "</a>" + strImg + "：" + unescape(content) + "</p>";
    ShowDiv += " <div class=\"Hr_5\"></div></div><div class=\"Hr_10\"></div>    举报类型：<div >";
    ShowDiv += "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"G4\">  <tr>";
    ShowDiv += " <td width=\"9%\" height=\"30\"><input type=\"radio\" name=\"radio\" id=\"radio\" value=\"radio\" /></td>";
    ShowDiv += "<td width=\"33%\" height=\"30\">垃圾广告</td>";
    ShowDiv += "<td width=\"6%\" height=\"30\"><input type=\"radio\" name=\"radio\" id=\"radio2\" value=\"radio\" /></td>";
    ShowDiv += "<td width=\"22%\" height=\"30\"> 淫秽色情</td>";
    ShowDiv += "<td width=\"7%\" height=\"30\"><input type=\"radio\" name=\"radio\" id=\"radio3\" value=\"radio\" /></td>";
    ShowDiv += "<td width=\"23%\">虚假中奖</td> </tr> <tr>";
    ShowDiv += "<td height=\"30\"><input type=\"radio\" name=\"radio\" id=\"radio5\" value=\"radio\" /></td>";
    ShowDiv += "<td height=\"30\">敏感信息<br /></td>";
    ShowDiv += "<td height=\"30\"><input type=\"radio\" name=\"radio\" id=\"radio6\" value=\"radio\" /></td>";
    ShowDiv += "<td height=\"30\">不实信息</td>";
    ShowDiv += "<td><input type=\"radio\" name=\"radio\" id=\"radio9\" value=\"radio\" /></td>";
    ShowDiv += "<td height=\"30\">人身攻击</td></tr>";
    ShowDiv += " <tr> <td height=\"30\"><input type=\"radio\" name=\"radio\" id=\"radio7\" value=\"radio\" /></td>";
    ShowDiv += " <td height=\"30\">其他</td><td height=\"30\">&nbsp;</td><td height=\"30\">&nbsp;</td><td height=\"30\">&nbsp;</td>";
    ShowDiv += "<td height=\"30\">&nbsp;</td></tr>";
    ShowDiv += "</table><div class=\"Hr_10\"></div>";
    ShowDiv += "<div></div></div>";
    ShowDiv += "<div class=\"WinBtn  L\"><span><input name=\"btnReport\" type=\"button\" id=\"btnReport\" value=\"确定举报\" />";
    ShowDiv += "</span></div><div class=\"Hr_10\"></div> <div class=\" Line_dashed\"></div></div></div>";
    return ShowDiv;
}


//举报用户内容拼接
function FunGetPageReportUser(UserID, Memberlevel, Comment, NickName, Face) {



    ShowDiv = "<div class=\"WindowWark490\">";
    ShowDiv += "<h1 class=\"WindowTil F14 G4\"><a href=\"javascript:void(0)\" onclick=\"CloseReport()\"><img class=\" R Img\" src=\"http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif\" alt=\"关闭\"  />";
    ShowDiv += "</a>举报身份造假</h1>";
    ShowDiv += "<div class=\"WindowBox \"><div class=\" G4\">你要举报的是 &ldquo;<a href=\"/u_" + UserID + "\"  class=\"Blue Fa\">@" + NickName + "</a>";


    ShowDiv += ShowVerifyImg(Memberlevel);
    ShowDiv += "&rdquo;用户： </div>";



    ShowDiv += "<div class=\"Hr_5\"></div><div class=\" BrBlue  G6\"><div class=\"Hr_5\"></div>";
    ShowDiv += "<a href=\"/u_" + UserID + "\" ><img src=\"" + (Face != "" ? "images/face/small/" + Face : "" + FaceImagesUrl + "default1.png") + "\" alt=\"头像\" width=\"30\" height=\"30\" class=\"L WindowIM\" /></a>";

    ShowDiv += "<p class=\"L18\">";

    ShowDiv += "<a href=\"/u_" + UserID + "\"  class=\"Blue Fa\">@" + NickName + "</a>";

    ShowDiv += ShowVerifyImg(Memberlevel);

    ShowDiv += "<br />" + Comment + "</p>";


    ShowDiv += " <div class=\"Hr_5\"></div></div><div class=\"Hr_10\"></div>";
    ShowDiv += "<div class=\"G4\">举报理由：</div><textarea name=\"txtReportContent\" id=\"txtReportContent\" cols=\"45\" rows=\"5\" class=\"WindowTex\"></textarea>";
    ShowDiv += "<div class=\"Hr_5\"></div><table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"G4\">";
    ShowDiv += "<tr><td width=\"23%\" height=\"30\">举报人姓名<span class=\"G9\">(必填)</span>:</td>";
    ShowDiv += "<td width=\"29%\" height=\"30\"><input class=\"WindowTexW\" type=\"text\" name=\"txtReportUser\" id=\"txtReportUser\" maxlength=\"16\"/></td>";
    ShowDiv += "<td width=\"21%\" height=\"30\">联系手机<span class=\"G9\">(必填)</span>:</td>";
    ShowDiv += "<td width=\"27%\" height=\"30\"><input class=\"WindowTexW\" type=\"text\" name=\"txtReportMobile\" id=\"txtReportMobile\" maxlength=\"11\"  onkeyup=\"GetConentNumber(this)\"/></td>";
    ShowDiv += "</tr></table><div class=\"Hr_10\"></div>";
    ShowDiv += "<div class=\"Line_dashed\"></div><div class=\"Hr_10\"></div><p class=\"G9\">请放心，您的隐私将会得到保护</p>";
    ShowDiv += "<div class=\"WinBtn  L\"><span>";
    ShowDiv += " <input name=\"btnreporttrue\" type=\"button\" id=\"btnreporttrue\" value=\"确定举报\" />";
    ShowDiv += "</span></a></div><div class=\"Hr_10\"></div>";




    return ShowDiv;
}


//清空文本框中的非数字内容
function GetConentNumber(obj) {
    obj.value = obj.value.replace(/\D/gi, "");
}

//举报用户
function ShowPageUserReport(userid) {

    $.ajax({
        url: "" + vServiceUrl + "vipIlog.asmx/GetVIPIlogReportInfo",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{userid:'" + userid + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var ShowDiv = "";

            if (dataObj.UrlState == "0") {
                ShowErrorMessage("系统已经Over,请稍后再试...")
                return false;
            }
            else if (dataObj.UrlState == "2") {
                ShowErrorMessage("自己不能举报自己！")
                return false;
            }
            else {
                showReportDialog = $.dialog({
                    id: "divShowRePort",
                    title: false,
                    content: FunGetPageReportUser(userid, dataObj.memberlevel, dataObj.comment, dataObj.NickName, dataObj.Face),
                    max: false,
                    min: false,
                    lock: true,
                    cache: false,
                    padding: 0


                });
                //确认举报
                $("#btnreporttrue").click(function() {
                    ShowReportUserSubmitInfo();
                });
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
//举报内容
//nickname：昵称
//face：头像
//content：内容
//useri：用户id
//用户vi_memberlevel认证
function ShowPageReport(showID, nickname, face, content, userid, vi_memberlevel) {

    if (!LoginDiv(16)) {
        return;
    }

    showReportDialog = $.dialog({
        id: "divShowRePort",
        title: false,
        content: funGetPageReport(userid, nickname, face, content, vi_memberlevel),
        max: false,
        min: false,
        lock: true,
        cache: false,
        width: 365,
        height: 130,
        padding: 0


    });

    //取消举报
    $("#btnCancle").click(function() {
        CloseReport();
    });
    //添加举报
    $("#btnReport").click(function() {
        ShowReportSubmitInfo();
    });
}

//关闭举报
function CloseReport() {

    showReportDialog.close();
}

//关闭用户举报
function CloseUserReport() {

    showReportDialog.close();
}

//举报用户成功提示
function ShowReportUserSubmitInfo(groupid) {
    var txtReportContent = $("#txtReportContent").val();

    if (txtReportContent == "") {
        ShowErrorMessage("举报内容不能为空！");
        return false; ;
    }
    else if (Getlength(txtReportContent) < 20) {
        ShowErrorMessage("举报内容太短，不能小于10个汉字！");
        return false; ;
    }
    else if (Getlength(txtReportContent) > 1000) {
        ShowErrorMessage("举报内容过长，不能超过500个汉字！");
        return false; ;
    }


    var txtReportUser = $("#txtReportUser").val();
    if (txtReportUser == "") {
        ShowErrorMessage("举报人姓名不能为空！");
        return false; ;
    }
    else if (Getlength(txtReportUser) < 4) {
        ShowErrorMessage("举报人姓名太短，不能小于2个汉字！");
        return false; ;
    }
    else if (Getlength(txtReportUser) > 16) {
        ShowErrorMessage("举报内人内容过长，不能超过8个汉字！");
        return false;
    }



    var txtReportMobile = $("#txtReportMobile").val();
    if (txtReportMobile == "") {
        ShowErrorMessage("举报人手机不能为空！");
        return false; ;
    }
    else if (Getlength(txtReportMobile) != 11) {
        ShowErrorMessage("手机位数不正确，手机只能是11位！");
        return false; ;
    }





    $.dialog({
        id: "divrDialog",
        title: false,
        content: ShowReportInfo(),
        max: false,
        min: false,
        lock: true,
        cache: false,
        time: 1,
        //        width: 290,
        //        height: 65,
        padding: 0


    });
    CloseUserReport();

}
//举报内容成功提示
function ShowReportSubmitInfo(groupid) {
    $.dialog({
        id: "divrDialog",
        title: false,
        content: ShowReportInfo(),
        max: false,
        min: false,
        lock: true,
        cache: false,
        time: 1,
        width: 290,
        height: 65,
        padding: 0


    });
    CloseReport();

}



//设置成功
function ShowReportInfo() {

    ShowTable = '<div class=\"WindowWark280\"><div class=\"WindowBox Tc\"> ';
    ShowTable += '<div class=\" Tc F14  WindowSak\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/ok.gif\" class=\"L\" />举报成功。</div> ';
    ShowTable += '<div class="Hr_10"></div> <div class="Hr_10"></div></div></div>'

    return ShowTable;

}

//设置成功
function ShowConcernCancelInfo() {

    ShowTable = '<div class=\"WindowWark280\"><div class=\"WindowBox Tc\"> ';
    ShowTable += '<div class=\" Tc F14  WindowSak\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/ok.gif\" class=\"L\" />取消关注成功。</div> ';
    ShowTable += '<div class="Hr_10"></div> <div class="Hr_10"></div></div></div>'

    return ShowTable;

}



//取消粉丝确认
function ShowFanCancle(iuc_id, concernuserid, nikename) {
    ShowDiv = "<div class=\"WindowWark280\"><div class=\"WindowBox Tc\">";
    ShowDiv += "<div class=\" Tc F14 L30  WindowSak\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/ask.gif\" class=\"L\" />";
    ShowDiv += " 确认要移除 <a href=\"#\" class=\"Blue\">" + nikename + "</a>？";
    ShowDiv += "</div><div class=\"Hr_10\"></div>";
    ShowDiv += "<div class=\"WinBtn_H R\"><span>";
    ShowDiv += "<input name=\"btnFanCancle\" type=\"button\" id=\"btnFanCancle\" value=\"取消\" /> </span></a></div>";
    ShowDiv += "<div class=\"WinBtn  R\"><span>";
    ShowDiv += " <input name=\"btnFanTrue\" type=\"button\" id=\"btnFanTrue\" value=\"确定\" /></span>";
    ShowDiv += "</div><div class=\"Hr_10\"></div></div></div>";

    return ShowDiv;

}

//取消关注确认
function ShowConcernCancle(iuc_id, concernuserid, nikename) {
    ShowDiv = "<div class=\"WindowWark280\" id=\"Con\"><div class=\"WindowBox Tc\">";
    ShowDiv += "<div class=\" Tc F14 L30  WindowSak\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/ask.gif\" class=\"L\" />";
    ShowDiv += " 确认要取消 <a href=\"#\" class=\"Blue\">" + nikename + "</a>？";
    ShowDiv += "</div><div class=\"Hr_10\"></div>";
    ShowDiv += "<div class=\"WinBtn_H R\"><span>";
    ShowDiv += "<input name=\"btnFanCancle\" type=\"button\" id=\"btnConcernCancle\" value=\"取消\" /> </span></a></div>";
    ShowDiv += "<div class=\"WinBtn  R\"><span>";
    ShowDiv += " <input name=\"btnFanTrue\" type=\"button\" id=\"btnConcernTrue\" value=\"确定\" /></span>";
    ShowDiv += "</div><div class=\"Hr_10\"></div></div></div>";

    return ShowDiv;

}



//在加关注时加载出当前用户的分组
//iufid：关注id
//concernuserid：被关注id
//nikename：昵称
function ShowAddFollowTrueInfo(iufid, concernuserid, nikename) {
    ShowDiv = "<div class=\"WindowWark350\"><div class=\"WindowTil G4 F14\">";

    ShowDiv += "<a href=\"javascript:void(0);\" onclick=\"CloseConcern()\"><img class=\" R Img\" src=\"http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif\" alt=\"关闭\"  /></a>关注成功</div>";

    ShowDiv += "<div class=\"WindowBox\">";

    ShowDiv += "<p class=\"G4 L25\">为 <strong>" + nikename + "</strong> 选择分组</p>";

    ShowDiv += "<div class=\"BrBlue P10\">";


    ShowDiv += "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"G4\"  id=\"d" + iufid + "\">";





    ShowDiv += "</table><div class=\"Hr_10\">";

    ShowDiv += "</div>";

    ShowDiv += " <DIV>&nbsp;<img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-j.gif\"/> <a href=\"javascript:void(0);\" onclick=\"ShowGroupConcernAdd(" + iufid + "," + concernuserid + ",'" + nikename + "')\" >创建分组</a></DIV>";

    ShowDiv += "</div><div class=\"Hr_10\"></div><div class=\"WinBtn_H R\"><span>";

    ShowDiv += "<input name=\"btnAddFollowCancle\" type=\"button\" id=\"btnAddFollowCancle\" onclick=\"CloseConcern()\" value=\"取消\" />";

    ShowDiv += "</span></div>";

    ShowDiv += "<div class=\"WinBtn  R\"><span>";

    ShowDiv += "<input name=\"btnAddFollowTrue\" type=\"button\" id=\"btnAddFollowTrue\" onclick=\"GrouptAdd(" + concernuserid + ")\" value=\"确定\" />";

    ShowDiv += " </span></div><div class=\"Hr_10\"></div></div></div>"

    ShowGroupSel(iufid, iufid);

    return ShowDiv;
}


//在加关注时加载出当前用户的分组
//iufid：关注id
//concernuserid：被关注id
//nikename：昵称
function ShowAddFollowTaTrueInfo(iufid, concernuserid, nikename) {
    ShowDiv = "<div class=\"WindowWark350\"><div class=\"WindowTil G4 F14\">";

    ShowDiv += "<a href=\"javascript:void(0);\" onclick=\"CloseConcern()\"><img class=\" R Img\" src=\"http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif\" alt=\"关闭\"  /></a>关注成功</div>";

    ShowDiv += "<div class=\"WindowBox\">";

    ShowDiv += "<p class=\"G4 L25\">为 <strong>" + nikename + "</strong> 选择分组</p>";

    ShowDiv += "<div class=\"BrBlue P10\">";

    ShowDiv += "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" class=\"G4\" id=\"d" + iufid + "\">";

    ShowDiv += "</table><div class=\"Hr_10\">";

    ShowDiv += "</div>";

    ShowDiv += " <DIV>&nbsp;<img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-j.gif\"/> <a href=\"javascript:void(0);\" onclick=\"ShowGroupConcernAdd(" + iufid + "," + concernuserid + ",'" + nikename + "')\" >创建分组</a></DIV>";

    ShowDiv += "</div><div class=\"Hr_10\"></div><div class=\"WinBtn_H R\"><span>";

    ShowDiv += "<input name=\"btnAddFollowCancle\" type=\"button\" id=\"btnAddFollowCancle\" onclick=\"CloseConcern()\" value=\"取消\" />";

    ShowDiv += "</span></div>";

    ShowDiv += "<div class=\"WinBtn  R\"><span>";

    ShowDiv += "<input name=\"btnAddFollowTrue\" type=\"button\" id=\"btnAddFollowTrue\" onclick=\"GrouptTaAdd(" + concernuserid + ")\" value=\"确定\" />";

    ShowDiv += " </span></div><div class=\"Hr_10\"></div></div></div>"

    //加载用户
    ShowGroupSel(iufid, iufid);
    return ShowDiv;
}


//选择组
//ConcernUserID：被关注用户id
function ShowGroupSel(ConcernUserID, iufid) {
    //行索引
    var index_tr = 0;

    var strGroup = "";

    $.ajax({
        url: "" + vServiceUrl + "ILogFollow.asmx/ILogFollowGetGroupListConcern",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{ConcernUserID:'" + ConcernUserID + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象
            $.each(dataObj.GroupList, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        $("#MenuListUL").html("<li>加载错误</li>");
                    }
                    else {
                        return true;
                    }
                }
                if (item.GroupID > 0) {

                    //放入组每行放两个dt

                    //每行开始
                    if (index_tr == 0) {
                        strGroup += "<tr>";
                    }

                    strGroup += "<td width=\"30\" height=\"30\"><input type=\"checkbox\" value=\"" + item.GroupID + "\" name=\"Groupcbox\" id=\"Groupcbox\" /></td>";

                    strGroup += "<td width=\"110\" height=\"30\">" + item.GroupName + "</td>";



                    index_tr++;
                    if (index_tr == 2) {
                        strGroup += "</tr>";
                        index_tr = 0;
                    }    //索引自增
                }
            });
            //初始化行索引
            if (index_tr == 1) {
                strGroup += "<td width=\"50%\" height=\"30\" colspan=\"2\">&nbsp;</td></tr>";
            }
            $("#d" + iufid).html(strGroup);
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

//提交用户组
//ConcernUserID：被关注id
function GrouptAdd(ConcernUserID) {
    //有选中项
    if (!Ischecked()) {
        $.dialog.tips("<font color=\"red\">请选择组！</font>");
    }
    else {
        var strGroup = "";

        $("input:checkbox[name='Groupcbox']:checked").each(function() {
            strGroup += $(this).val() + ",";
        })


        //掉用服务器端
        $.ajax({
            url: "" + vServiceUrl + "ILogFollow.asmx/ILogFollowEditGroupConnect_s",
            type: "POST",
            dataType: "json",
            contentType: "application/json;",
            data: "{groupID:'" + strGroup + "',concernUserid:'" + ConcernUserID + "'}",
            success: function(json) {

                var dataObj = eval("(" + json.d + ")"); //转换为json对象

                var TopContent = "";
                $.each(dataObj.GroupEdit, function(idx, item) {
                    if (idx == 0) {
                        if (item.UrlState == "0") {
                            $("#" + HrName + "_" + HrID + "").html("<li>加载错误</li>");
                            return false;
                        }
                        else {
                            ShowGroupFollowTrue(0);
                            CloseConcern();
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
}

//提交用户组
//ConcernUserID：被关注id
function GrouptTaAdd(ConcernUserID) {
    //有选中项
    if (!Ischecked()) {
        $.dialog.tips("<font color=\"red\">请选择组！</font>");
    }
    else {
        var strGroup = "";

        $("input:checkbox[name='Groupcbox']:checked").each(function() {
            strGroup += $(this).val() + ",";
        })


        //掉用服务器端
        $.ajax({
            url: "" + vServiceUrl + "ILogFollow.asmx/ILogFollowEditGroupConnect_s",
            type: "POST",
            dataType: "json",
            contentType: "application/json;",
            data: "{groupID:'" + strGroup + "',concernUserid:'" + ConcernUserID + "'}",
            success: function(json) {

                var dataObj = eval("(" + json.d + ")"); //转换为json对象

                var TopContent = "";
                $.each(dataObj.GroupEdit, function(idx, item) {
                    if (idx == 0) {
                        if (item.UrlState == "0") {
                            $("#" + HrName + "_" + HrID + "").html("<li>加载错误</li>");
                            return false;
                        }
                        else {
                            ShowGroupTaFollowTrue();
                            CloseConcern();
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
}


//是否有选中的站短ture：是，false：否
function Ischecked() {
    var issel = false;

    //查看是否有选中
    $("input:checkbox[name='Groupcbox']:checked").each(function() {
        issel = true;
    })

    return issel;
}


//粉丝加关注
//iufid：
//concernuserid：被关注用户id
//nikename：昵称
//vi_id：vipILog表流水号，为以关注换图片用
function ShowAddFollowTrueConcern(iufid, concernuserid, nikename) {
    $.ajax({
        url: "" + vServiceUrl + "ILogFollow.asmx/ILogFollowAddFan",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{iufID:'" + iufid + "',concernUserid:'" + concernuserid + "'}",
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
                        //关注操作框
                        showConcerntDialog = $.dialog({
                            id: "divrDialog",
                            title: false,
                            content: ShowAddFollowTrueInfo(iufid, concernuserid, nikename),
                            max: false,
                            min: false,
                            lock: true,
                            cache: false,
                            //width: 370,
                            //height: 210,
                            padding: 0

                        });

                        if (item.UrlState == "1") {
                            //成功关注后要切换成已关注图片
                            $("#cencernimg_" + iufid).html("<img src=\"http://simg.instrument.com.cn/ilog/blue/images/concern.gif\" alt=\"互相关注\"  />");
                        }
                        else if (item.UrlState == "2") {
                            $("#cencernimg_" + iufid).html("<img src=\"http://simg.instrument.com.cn/ilog/blue/images/concern_y.gif\" alt=\"已关注\"  />");
                        }


                        // CloseConcern();

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

//粉丝加关注
//iufid：
//concernuserid：被关注用户id
//nikename：昵称
//vi_id：vipILog表流水号，为以关注换图片用
function ShowAddFollowTrueTaConcern(iufid, concernuserid, nikename) {
    $.ajax({
        url: "" + vServiceUrl + "ILogFollow.asmx/ILogFollowAddFan",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{iufID:'" + iufid + "',concernUserid:'" + concernuserid + "'}",
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
                        //关注操作框
                        showConcerntDialog = $.dialog({
                            id: "divrDialog",
                            title: false,
                            content: ShowAddFollowTaTrueInfo(iufid, concernuserid, nikename),
                            max: false,
                            min: false,
                            lock: true,
                            cache: false,
                            //width: 370,
                            //height: 210,
                            padding: 0

                        });

                        if (iufid != 0) {
                            if (item.UrlState == "1") {
                                //成功关注后要切换成已关注图片
                                $("#cencernimg_" + iufid).html("<img src=\"http://simg.instrument.com.cn/ilog/blue/images/concern.gif\" alt=\"互相关注\"  />");
                            }
                            else if (item.UrlState == "2") {
                                $("#cencernimg_" + iufid).html("<img src=\"http://simg.instrument.com.cn/ilog/blue/images/concern_y.gif\" alt=\"已关注\"  />");
                            }
                        }
                        else {
                            //判断我与用户之间的关系
                            funGetConcernExists(concernuserid);
                        }


                        // CloseConcern();

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



//粉丝加关注（搜人专用）
//iufid：
//concernuserid：被关注用户id
//nikename：昵称
//vi_id：vipILog表流水号，为以关注换图片用
function ShowAddFollowTrueTaConcern_s(iufid, concernuserid, nikename) {
    $.ajax({
        url: "" + vServiceUrl + "ILogFollow.asmx/ILogFollowAddFan",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{iufID:'" + iufid + "',concernUserid:'" + concernuserid + "'}",
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
                        //关注操作框
                        showConcerntDialog = $.dialog({
                            id: "divrDialog",
                            title: false,
                            content: ShowAddFollowTaTrueInfo(iufid, concernuserid, nikename),
                            max: false,
                            min: false,
                            lock: true,
                            cache: false,
                            //width: 370,
                            //height: 210,
                            padding: 0

                        });

                        if (iufid != 0) {


                            if (item.UrlState == "1") {

                                //成功关注后要切换成已关注图片
                                $("#cencernimg_" + iufid).html("<img src=\"http://simg.instrument.com.cn/ilog/blue/images/concern.gif\" alt=\"互相关注\"  />");
                            }
                            else if (item.UrlState == "2") {
                                $("#cencernimg_" + iufid).html("<img src=\"http://simg.instrument.com.cn/ilog/blue/images/concern_y.gif\" alt=\"已关注\"  />");
                            }
                        }
                        else {
                            //判断我与用户之间的关系
                            funGetConcernExists(concernuserid);
                        }


                        // CloseConcern();

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




//已经关注的情况下设置分组
//iufid：
//concernuserid：被关注用户id
//nikename：昵称
//vi_id：vipILog表流水号，为以关注换图片用
function ShowAddFollowTrueTaGroup(iufid, concernuserid, nikename) {
    //关注操作框
    showConcerntDialog = $.dialog
           ({
               id: "divrDialog",
               title: false,
               content: ShowAddFollowTaTrueInfo(iufid, concernuserid, nikename),
               max: false,
               min: false,
               lock: true,
               cache: false,
               //width: 370,
               //height: 210,
               padding: 0

           });


}


//确认取消关注提
function ShowConcernSubmitInfo(iuc_id, concernuserid) {
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
                        //判断与用户的关系
                        funGetConcernExists(concernuserid);
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




//确认移除粉丝
function ShowFanSubmitInfo(iuc_id, concernuserid) {
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
                        funGetConcernExists(concernuserid);
                        CloseConcern();
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

//关闭举报
function CloseConcern() {
    showConcerntDialog.close();

}



//关注
function ShowPageConcern(iuc_id, concernuserid, nikename) {

    showConcerntDialog = $.dialog({
        id: "divShowRePort",
        title: false,
        content: ShowConcernCancle(iuc_id, concernuserid, nikename),
        max: false,
        min: false,
        lock: true,
        cache: false,
        // width: 295,
        //  height: 99,
        padding: 0


    });

    //取消
    $("#btnConcernCancle").click(function() {
        CloseConcern();
    });
    //确定取消
    $("#btnConcernTrue").click(function() {
        ShowConcernSubmitInfo(iuc_id, concernuserid);
    });
}


//移除粉丝
function ShowPageDeleteFan(iuc_id, concernuserid, nikename) {

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

//搜有搜索页面左边的菜单
//ation：操作类型0招人，1找文章
function LeftMenu(ation) {
    var ation_s = $("#ation_s").val();

    if (ation == 0)  //找人
    {
        //真实地址

        //伪静态地址

        if (ation_s == "") {
            window.location.href = "search";
        }
        else {
            window.location.href = "search_" + ation_s;
        }

    }
    else if (ation == 1)   //找文章
    {
        //真实地址


        //伪静态地址
        if (ation_s == "") {
            window.location.href = "rsearch";
        }
        else {
            window.location.href = "rsearch_" + ation_s;
        }
    }
    else {
        //真实地址


        //伪静态地址

        if (ation_s == "") {
            window.location.href = "search";
        }
        else {
            window.location.href = "search_" + ation_s;
        }
    }
}


//遮罩弹窗
var schoolDialog = "";

//选择转发获取原创信息(传递用户userid,原创Id)
function checkForWard(userId, spredid, type, forwardCount, fromType) {

    if (!LoginDiv(16)) {
        return;
    }


    $.ajax({
        url: "" + vServiceUrl + "/VipIlogUser.asmx/ILogGetSpreadWindowContent",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{spreadId:" + spredid + "}",
        success: function(json) {

            // alert(json);
            var dataObj = eval("(" + json.d + ")"); //转换为json对象          

            if (dataObj.State == 0) {

                showTipe("抱歉，此博文已被删除.", 0);

            }
            else if (dataObj.State == 2) {

                showTipe("网络繁忙，请稍后重试.", 0);

            }
            else {

                //showForWard(userId, type, dataObj.oguserid, dataObj.ogusername, unescape(dataObj.ogcontent), dataObj.spuserid, dataObj.spusername, unescape(dataObj.spcontent));

                //弹出转发框
                schoolDialog = $.dialog(
           {

               id: "divShowGroupTrue",
               title: false,
               content: showForWard(forwardCount, spredid, type, dataObj.oguserid, dataObj.ogusername, unescape(dataObj.ogcontent), dataObj.spuserid, dataObj.spusername, unescape(dataObj.spcontent), dataObj.isoriginal, fromType),
               max: false,
               min: false,
               lock: true,
               cache: false,
               padding: 0

           });

                $("#forwardInfo").focus();

                //myfocus("forwardInfo");

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



//弹出转发层
function showForWard(forwardCount, id, type, oguserid, originalUserName, ogcontent, spuserid, spusername, spcontent, isoriginal, fromType) {

    //获取当前用户id
    var userId = $.cookie('useid');

    var blog = "";

    if (userId == oguserid) {

        blog = "/u";

    }
    else {

        blog = "/u_" + oguserid;

    }

    var ShowTable = "<div class=\"WindowWark490\" width=\"400\" id=\"forwardId\">";
    ShowTable += "<h1 class=\"WindowTil\">转发微博<img class=\" R Img\" src=\"http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif\" alt=\"关闭\"  onclick=\"javascript:schoolDialog.close();\"/></h1>";

    ShowTable += "<div class=\"WindowBox \">";
    ShowTable += "<div class=\" G4  L30 \">转发到：我的微博</div>";
    ShowTable += "<div class=\"L35 BrBlue Pl10 G6 Centent\">";
    ShowTable += "<a href=\"" + blog + "\" class=\"Blue\">";

    ShowTable += "@" + originalUserName;
    ShowTable += "</a>:" + ogcontent;
    ShowTable += "</div>";

    ShowTable += "<div class=\"Hr_10\"></div>";
    ShowTable += "<div class=\"publish\" style=\"height:130px\">";
    ShowTable += "<div class=\"G3\"><div class=\"L list\" style=\"position:relative;width:100px\">";
    ShowTable += "<ul>";
    ShowTable += "<li><span class=\"ico1\"></span><a href=\"javascript:void(0);\" id=\"forwardExpression\" onclick=\"changeExpressio(this,'forwardInfo');\" class=\"Blue\">表情</a></li>";
    ShowTable += "</ul>";

    ShowTable += "</div>";

    var classType = "WinBtn R";
    var forDisabled = "";

    //转发
    if (isoriginal == 0) {

        //判断
        var content = "//@" + spusername + ":" + spcontent;

        var contentlen = Getlength(content);

        var font_count = Math.floor((280 - contentlen) / 2);

        var num = Math.abs(font_count);

        if (font_count < 0) {

            ShowTable += "<span class=\"R G6\" id=\"countNum\">已经超过<font class=\"publish_num\" >" + num + "</font>字</span>";

            classType = "WinBtn_H R";
            forDisabled = "disabled";

        } else {

            ShowTable += "<span class=\"R G6\" id=\"countNum\">还可以输入<font class=\"publish_num\" >" + num + "</font>字</span>";

            classType = "WinBtn R";
            forDisabled = "";

        }

        //ShowTable += "";

    } else {

        ShowTable += "<span class=\"R G6\" id=\"countNum\">你还可以输入<font class=\"publish_num\" >140</font>字</span>";

    }

    ShowTable += "</div>";
    ShowTable += "<div class=\"Hr_5\"></div>";

    ShowTable += "<textarea class=\" Input F14 textarea Fa\" style=\"width:440px\" name=\"forwardInfo\" id=\"forwardInfo\" cols=\"45\" onkeyup=\"checkForWardLength('forwardInfo');\" rows=\"5\">";

    //isoriginal:0:转发, 1:原创
    if (isoriginal == 0) {

        ShowTable += "//@" + spusername;
        ShowTable += ":" + spcontent;


    }
    ShowTable += "</textarea>";

    ShowTable += "<div class=\"Hr_4\"></div>";


    ShowTable += "<div id=\"forwardBtn\" class=\"" + classType + "\">";


    ShowTable += "<span>";

    ShowTable += " <input name=\"转发\" type=\"button\" id=\"forwardClickId\" onclick=\"javascript:sendForwardInfo(" + id + "," + type + ",'" + oguserid + "','" + fromType + "'," + oguserid + ");\" value=\"转发\" " + forDisabled + "/>";

    ShowTable += "</span>";
    ShowTable += "</div>";
    ShowTable += "</div>";
    ShowTable += "<div class=\"Hr_10\"></div>";

    //判断转发数目
    if (forwardCount >= 1) {
        //异步查看信息

        //转发:0-is_isoriginal

        ShowTable += "<div class=\"WindowBoo\"><p class=\"L \" id=\"pForwark" + id + "\"><span class=\"G6\">当前已转发" + forwardCount + "次</span>, 展开</p>&nbsp;&nbsp;";

        //收起转发
        ShowTable += "<a href=\"javascript:void(0);\" id='more" + id + "' style=\"display:none;\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/Jt01.gif\"   onclick=\"showMoreForwardInfo(" + forwardCount + "," + id + ",1);\" /></a>";

        //展示转发
        ShowTable += "<a href=\"javascript:void(0);\" id='moreo" + id + "'><img src=\"http://simg.instrument.com.cn/ilog/blue/images/Jt02.gif\"   onclick=\"showMoreForwardInfo(" + forwardCount + "," + id + ",0);\" /></a>";


        ShowTable += "</div>";
        ShowTable += "<div class=\"Hr_10\"></div>";
        ShowTable += "<div id=\"moreFor" + id + "\"></div>";

    }

    ShowTable += " </div>";
    ShowTable += "</div>";

    //checkForWardLength("forwardInfo");

    //$("#forwardInfo").insertAtCaret("    ");

    //$("#forwardInfo").focus();

    return ShowTable;

}

//点击查看转发列表
function showMoreForwardInfo(forwardCount, isoId, type) {

    if (type == 0) {

        $("#moreo" + isoId).hide();
        $("#more" + isoId).show();
        $("#pForwark" + isoId).text("收起转发记录");

        var ShowTable = "";

        $.ajax({
            url: "" + vServiceUrl + "/ILog_Spread.asmx/GetForwordList",
            type: "POST",
            dataType: "json",
            contentType: "application/json;",
            data: "{ isoId:'" + isoId + "',type:'1',PageCurrent:'" + 1 + "',PageSize:'" + 10 + "'}", //type没用到，先保留
            success: function(json) {

                // alert(json);
                var dataObj = eval("(" + json.d + ")"); //转换为json对象  

                if (dataObj.List.length > 0) {

                    var myUserId = $.cookie('useid');
                    var url = "";
                    //循环获取值
                    $.each(dataObj.List, function(idx, item) {

                        if (item.is_id != undefined) {

                            url = myUserId == item.userid ? "/u" : "/u_" + item.userid;


                            //迭代集合
                            ShowTable += "<div class=\"G4 WindowBoi\">";
                            ShowTable += "<a href=\"" + url + "\">";

                            ShowTable += "<img  width=\"30\" height=\"30\" src=\"images/face/small/" + item.face + "\" alt=\"" + item.nickname + "\" class=\"L img\" />";
                            ShowTable += "</a>";
                            ShowTable += "<div class=\"L18   R Rbox\">";
                            ShowTable += "<a href=\"" + url + "\" class=\"Blue\">" + item.nickname + "</a>:";
                            ShowTable += item.content;
                            ShowTable += "<br />";
                            ShowTable += "<div class=\"Hr_1\"></div>";
                            ShowTable += "<span class=\"G9\">(" + item.timestr + ")</span>";
                            ShowTable += "|";
                            ShowTable += "<a href=\"javascript:void(0);\" onclick=\"ShowPageReport(" + item.is_id + ",'" + item.nickname + "','"
                            + item.face + "','" + escape(item.content) + "'," + item.userid + "," + item.memberlevel + ")\" class=\"Blue\">举报</a>";
                            ShowTable += "</div>";
                            ShowTable += "</div>";
                            ShowTable += "<div class=\"Hr_10\"></div>";
                            ShowTable += "<div class=\" Line_dashed\"></div>";
                            ShowTable += "<div class=\"Hr_10\"></div>";


                        }
                    }

                );

                }
                //转发条数大于10显示查看更多
                if (forwardCount > 10) {

                    ShowTable += "<div><a href=\"cont_" + isoId + "_0\" class=\"Gray9\">查看所有" + forwardCount + "条转发 >></a> </div>";

                }

                $("#moreFor" + isoId).html(ShowTable);
            },
            //出错调试         
            error: function(x, e) {
                //alert(x.responseText);     
            },
            //执行成功后自动执行           
            complete: function(x) {

            }
        });

    } else {

        $("#moreFor" + isoId).html("");
        $("#moreo" + isoId).show();
        $("#more" + isoId).hide();
        $("#pForwark" + isoId).html("<span class=\"G6\">当前已转发" + forwardCount + "次</span>, 展开");

    }

    myfocus("forwardInfo");

}


//转发微博
//刷新列表标示fromType: 
//          all    blog
//首页转发：a      aa
//个人主页: b      bb
//他人主页  c      cc
//搜索   s
//At我的博文 d
function sendForwardInfo(ilogID, type, originalID, fromType, userid) {
    if (!LoginDiv(16)) {
        return;
    }

    //获取当然用户id
    var userid = $.cookie('useid');

    //转发内容
    var content = $.trim($("#forwardInfo").val()) == "" ? "转发微博" : $("#forwardInfo").val();

    content = content.replace(/[\r\n]/mg, " "); //替换换行
    content = content.replace(/^ +| +$/g, '').replace(/ +/g, ' '); //去空格

    $.ajax({
        url: "" + vServiceUrl + "/VipIlogUser.asmx/ILogForwardingCommentInfo",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{ilogID:" + ilogID + ",type:" + type + ",originalID:'" + originalID + "',userid:'" + userid + "',comment:'" + content + "',i:'" + rand + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象     　　     
            var result = dataObj.state == 1 ? "转发成功." : "转发失败.";
            if (fromType == "a") {
                //刷新首页全部列表信息
                GetAllList("" + vServiceUrl + "ILog_Spread.asmx/GetAllList", 1, 45, 0);
            } else if (fromType == "aa") {
                //刷新首页/个人主页博文列表信息
                GetList(vServiceUrl + "ILogOriginal.asmx/GetList", 1, 45);
            } else if (fromType == "b") {
                //刷新个人主页全部列表信息
                GetAllList("" + vServiceUrl + "ILog_Spread.asmx/GetAllList", 1, 45, 1);
            }
            else if (fromType == "c") {

                var userHe = $("#userid").val();

                //刷新他人主页全部列表信息
                GetAllList("" + vServiceUrl + "ILog_Spread.asmx/GetAllList_He", 1, 45, userHe);
            }
            else if (fromType == "cc") {

                var userHe = $("#userid").val();
                //刷新他人主页博文列表信息    　　          
                GetList(vServiceUrl + "ILogOriginal.asmx/GetList_He", pageindex, 45, userHe);

            }
            else if (fromType == "conf") {

                var newId = $("#ioidHidden").val();

                //刷新博文内页转发删除后列表信息
                GetContentInfoById("" + vServiceUrl + "/VipIlogUser.asmx/IlogGetContentInfoById", newId, 1);


            }
            else if (fromType == "s") {
                var strKeyword = $("#keyword_h").val();
                //刷新搜索列表
                GetSearchList("" + vServiceUrl + "ILog_Spread.asmx/GetSerchSpreadAllList", 1, 45, strKeyword);
            }
            else if (fromType == "e") {
                GetIlogList(1, 45, 0); //一天转发
            }
            else if (fromType == "ee") {
                GetIlogList(1, 45, 1); //一周转发
            }
            else if (fromType == "d") {
                var ation = $("#ation_s").val();
                var keyword = $.trim($("#keyword_s").val());
                var currentIndex = $("#hidPageIndex").val();
                if (keyword == "" || keyword == "请输入昵称") {
                    GetUserILList(currentIndex, 45, ation);
                }
                else {
                    GetSearchList(currentIndex, 45, keyword);
                }
            }
            schoolDialog.close(); //关闭遮罩 
            showTipe(result, dataObj.state); //提示     　　       　　      
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

//公共提示框
function showTipe(str, ret) {

    $.dialog({

        id: "divShowGroupTrue",
        title: false,
        content: sendResult(str, ret),
        max: false,
        min: false,
        lock: true,
        cache: false,
        time: 2,
        padding: 0


    });

}


//公共提示框，传递秒数
function showTipeWithSeconds(str, ret, seconds) {

    $.dialog({

        id: "divShowGroupTrue",
        title: false,
        content: sendResult(str, ret),
        max: false,
        min: false,
        lock: true,
        cache: false,
        time: seconds,
        padding: 0


    });

}

//公共提示框，传递宽度
function showTipeWithWidth(str, ret, widthlen) {

    $.dialog({

        id: "divShowGroupTrue",
        title: false,
        content: sendResultWithWidth(str, ret, widthlen),
        max: false,
        min: false,
        lock: true,
        cache: false,
        time: 2,
        padding: 0

    });

}

//弹出提示框层
function sendResult(str, ret) {

    var info = ret == 1 ? "ok.gif" : "NO.gif";
    ShowTable = "<div class=\"WindowWark200\" style=\"width:280px;\" >";
    ShowTable += "<div class=\"WindowBox Tc\">";
    ShowTable += "<div class=\" Tc F14  WindowSak\"  >";
    ShowTable += "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/" + info + "\" class=\"L\" />";
    ShowTable += str;
    ShowTable += '</div>';
    ShowTable += '<div class="Hr_20"></div>';
    ShowTable += '</div></div>';
    return ShowTable;

}

//弹出提示框层
function sendResultWithWidth(str, ret, width) {

    var info = ret == 1 ? "ok.gif" : "NO.gif";
    ShowTable = "<div class=\"WindowWark200\" style=\"width:" + width + "px;\" >";
    ShowTable += "<div class=\"WindowBox Tc\">";
    ShowTable += "<div class=\" Tc F14  WindowSak\"  >";
    ShowTable += "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/" + info + "\" class=\"L\" />";
    ShowTable += str;
    ShowTable += '</div>';
    ShowTable += '<div class="Hr_20"></div>';
    ShowTable += '</div></div>';
    return ShowTable;

}

//弹出删除评论提示框
function showCommentResult(icid) {

    //弹出转发框
    schoolDialog = $.dialog(
       {

           id: "divShowGroupTrue",
           title: false,
           content: showCommentHtml(icid),
           max: false,
           min: false,
           lock: true,
           cache: false,
           padding: 0

       });

}



//评论样式.by lx on 20120620
function showCommentHtml(icid) {

    ShowTable = "<div class=\"WindowWark350\">";
    ShowTable += "<h1 class=\"WindowTil G4 F14\">";
    ShowTable += "<a href=\"#\"><img class=\" R Img\" src=\"http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif\" alt=\"关闭\" onclick=\"schoolDialog.close();\"/></a>提示</h1>";
    ShowTable += "<div class=\"WindowBox Tc\">";
    ShowTable += '<div class=" Tc F14 L30  WindowSak">';
    ShowTable += '<img src="http://simg.instrument.com.cn/ilog/blue/images/ask.gif" class="L" />确定要删除该回复吗？';
    ShowTable += '</div>';
    ShowTable += '<div class="Hr_10"></div>';
    ShowTable += '<div class="WinBtn_H R"><span>';
    ShowTable += '<input name="取消" type="button" id="取消" value="取消" onclick=\"schoolDialog.close();\"  />';
    ShowTable += ' </span></div>';
    ShowTable += '<div class="WinBtn  R"><span>';
    ShowTable += '<input name="确定" type="button" id="确定" value="确定" onclick=\"deleteComment(' + icid + ');\" />';
    ShowTable += '</span></div>';
    ShowTable += '<div class="Hr_10"></div>';
    ShowTable += '</div>';
    ShowTable += '</div>';
    ShowTable += '';
    ShowTable += '';
    return ShowTable;

}

//删除评论
function deleteComment(icid) {

    //获取当然用户id
    var userid = $.cookie('useid');

    $.ajax({

        url: "" + vServiceUrl + "/VipIlogUser.asmx/ILogCommentDeleteById",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{icid:'" + icid + "',userid:'" + userid + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象 

            if (dataObj.state == 1) {

                schoolDialog.close();
                showTipe("删除评论成功!", 1);


                var deleteGuid = $("#deleteIdHidden").val();

                var guidId = deleteGuid.split('|');

                var newSum = guidId[4] - 1; //评论数              

                $("#replyCount" + guidId[2]).html("评论(" + newSum + ")");
                checkComment(1, guidId[1], guidId[2], guidId[3], guidId[4] - 1);

            } else {

                schoolDialog.close();
                showTipe("删除评论失败!", 0);

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


//--------------------------------评论部分专用-------------------------------------

//弹出删除评论提示框
//icid：评论id
//keyword：搜索关键字，如果是空说明就不是在搜索评论时删掉的
//ation：1收到的评论，0发出的评论
function showCommentResult_c(icid, keyword, ation) {

    //弹出转发框
    schoolDialog_c = $.dialog(
       {

           id: "divShowGroupTrue",
           title: false,
           content: showCommentHtml_c(icid, keyword, ation),
           max: false,
           min: false,
           lock: true,
           cache: false,
           padding: 0

       });

}



//评论样式.by lx on 20120620
//icid：评论id
//keyword：搜索关键字，如果是空说明就不是在搜索评论时删掉的
//ation：1收到的评论，0发出的评论
function showCommentHtml_c(icid, keyword, ation) {

    ShowTable = "<div class=\"WindowWark350\">";
    ShowTable += "<h1 class=\"WindowTil G4 F14\">";
    ShowTable += "<a href=\"#\"><img class=\" R Img\" src=\"http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif\" alt=\"关闭\" onclick=\"schoolDialog_c.close();\"/></a>提示</h1>";
    ShowTable += "<div class=\"WindowBox Tc\">";
    ShowTable += '<div class=" Tc F14 L30  WindowSak">';
    ShowTable += '<img src="http://simg.instrument.com.cn/ilog/blue/images/ask.gif" class="L" />确定要删除该回复吗？';
    ShowTable += '</div>';
    ShowTable += '<div class="Hr_10"></div>';
    ShowTable += '<div class="WinBtn_H R"><span>';
    ShowTable += '<input name="取消" type="button" id="取消" value="取消" onclick=\"schoolDialog_c.close();\"  />';
    ShowTable += ' </span></div>';
    ShowTable += '<div class="WinBtn  R"><span>';
    ShowTable += "<input name=\"确定\" type=\"button\" id=\"确定\" value=\"确定\" onclick=\"deleteComment_c(" + icid + ",'" + keyword + "'," + ation + ");\" />";
    ShowTable += '</span></div>';
    ShowTable += '<div class="Hr_10"></div>';
    ShowTable += '</div>';
    ShowTable += '</div>';
    ShowTable += '';
    ShowTable += '';
    return ShowTable;

}

//删除评论
//icid：评论id
//keyword：搜索关键字，如果是空说明就不是在搜索评论时删掉的
//ation：1收到的评论，0发出的评论
function deleteComment_c(icid, keyword, ation) {

    //获取当然用户id
    var userid = $.cookie('useid');

    $.ajax({

        url: "" + vServiceUrl + "/VipIlogUser.asmx/ILogCommentDeleteById",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{icid:'" + icid + "',userid:'" + userid + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象 

            if (dataObj.state == 1) {
                schoolDialog_c.close();
                showTipe("删除评论成功!", 1);

                //重新绑定列表数据
                if (keyword == "")   //默认列表
                {
                    GetUserCommentList("" + vServiceUrl + "ILogComment.asmx/GetUserCommentList", 1, 45, ation);
                }
                else    //搜索列表
                {
                    GetSearchList("" + vServiceUrl + "ILogComment.asmx/GetSearchCommentList", 1, 45, keyword);
                }

                //联动切换标签
                commentTyle_Search(ation);

            }
            else {

                schoolDialog_c.close();
                showTipe("删除评论失败!", 0);

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


//-------------------------------评论部分专用--------------------------------------


//删除原创或转发的博文.by lx on 20120704
function deleteSpreadInfoWhereId(spreadId, fromType, userId) {


    $.ajax({

        url: "" + vServiceUrl + "/ILog_Spread.asmx/ILogSpreadDeleteById",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{isid:'" + spreadId + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象 

            if (dataObj.state == 1) {

                //关闭遮罩              
                schoolDialog.close();

                //提示
                showTipe("博文删除成功", 1);


                if (fromType == "a") {
                    //刷新首页全部列表信息
                    GetAllList("" + vServiceUrl + "ILog_Spread.asmx/GetAllList", 1, 45, 0);
                } else if (fromType == "aa") {
                    //刷新首页/个人主页博文列表信息
                    GetList("" + vServiceUrl + "ILogOriginal.asmx/GetList", 1, 45);
                } else if (fromType == "b") {
                    //刷新个人主页全部列表信息
                    GetAllList("" + vServiceUrl + "ILog_Spread.asmx/GetAllList", 1, 45, 1);
                }
                else if (fromType == "conf") {

                    var newId = $("#ioidHidden").val();

                    //刷新博文内页转发删除后列表信息
                    GetAllForwardList("" + vServiceUrl + "ILog_Spread.asmx/GetContentForwordPageList", newId, userId, 1, 10, "conf"); //userid为原创或是转发类型


                }
                else if (fromType == "c") {

                    var userHe = $("#userid").val();
                    //刷新他人主页全部列表信息
                    GetAllList("" + vServiceUrl + "ILog_Spread.asmx/GetAllList_He", 1, 45, userHe);

                }
                else if (fromType == "cc") {

                    var userHe = $("#userid").val();
                    //刷新他人主页博文列表信息    　　          
                    GetList("" + vServiceUrl + "ILogOriginal.asmx/GetList_He", 1, 45, userHe);

                } else if (fromType == "s") {
                    var strKeyword = $("#keyword_h").val();
                    //刷新搜索列表
                    GetSearchList("" + vServiceUrl + "ILog_Spread.asmx/GetSerchSpreadAllList", 1, 45, strKeyword);
                }
                else if (fromType == "d") {

                    //@评论
                    GetUserILList("" + vServiceUrl + "ITILogList.asmx/GetATPageList", 1, 40, 0);

                }



            } else {

                //提示
                showTipe("博文删除失败", 0);

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

//删除博文.by lx on 20120704
function showBlogHtml(spreadId, fromType, userId) {

    ShowTable = "<div class=\"WindowWark280\">";
    ShowTable += "<div class=\"WindowBox Tc\">";
    ShowTable += "<div class=\" Tc F14 L30  WindowSak\">";
    ShowTable += "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/ask.gif\" class=\"L\" />确认要删除这条博文吗？";
    ShowTable += "</div>";
    ShowTable += " <div class=\"Hr_10\"></div>";
    ShowTable += "<div class=\"WinBtn_H R\"><span>";
    ShowTable += "<input name=\"取消\" type=\"button\" id=\"取消\" value=\"取消\" onclick=\"schoolDialog.close();\" />";
    ShowTable += "</span></div>";
    ShowTable += "<div class=\"WinBtn  R\"><span>";
    ShowTable += "<input name=\"确定\" type=\"button\" id=\"确定\" value=\"确定\" onclick=\"deleteSpreadInfoWhereId(" + spreadId + ",'" + fromType + "'," + userId + ");\"/>";
    ShowTable += " </span></div>";
    ShowTable += "<div class=\"Hr_10\"></div>";
    ShowTable += "</div>";
    ShowTable += "</div>";
    return ShowTable;

}

//弹出删除博文提示框
function showBlogResult(spreadId, fromType, userId) {

    //弹出博文框
    schoolDialog = $.dialog(
       {

           id: "divShowGroupTrue",
           title: false,
           content: showBlogHtml(spreadId, fromType, userId),
           max: false,
           min: false,
           lock: true,
           cache: false,
           padding: 0

       });

}

//评论内容前10条
var resultComment = "";

//选择评论弹出层
//ret:0 表示点击,1:表示异步刷新
function checkComment(ret, divId, spreadid, isoriginal, sum) {

    if (!LoginDiv(16)) {
        return;
    }

    var numComment = $("#replyCount" + spreadid).text();

    if (numComment != "") {

        var start = parseInt(numComment.indexOf("(")) + 1;
        var end = numComment.indexOf(")");

        sum = numComment.substring(start, end);

    }

    $("#deleteIdHidden").val(ret + "|" + divId + "|" + spreadid + "|" + isoriginal + "|" + sum);

    //判断是否重复点击
    var check = $("#" + divId).html();

    if (check != "" && ret == 0) {

        $("#" + divId).html("");
        $("#" + divId).hide();

    } else {

        //GetCommentList("" + vServiceUrl + "ILog_Spread.asmx/GetAllList", 1, 10);

        //调用异步方法获取值

        var comment = "";

        $.ajax({
            //请求WebService Url         
            url: "" + vServiceUrl + "ILogComment.asmx/GetCommentPageList",
            //请求类型,请据WebService接受类型定制          
            type: "POST",
            //预期指定服务器返回类型
            dataType: "json",
            //内容返回类型            
            contentType: "application/json;",
            cache: false,
            //请求参数              
            data: "{ currentid:'" + spreadid + "',type:'1',PageCurrent:'" + 1 + "',PageSize:'" + 10 + "'}", //1：收到的评论，0：发出的评论
            //成功           
            success: function(json) {

                //获取服务器的值        
                var dataObj = eval("(" + json.d + ")"); //转换为json对象            

                //获取当然用户id
                var userid = $.cookie('useid');

                var commentclass = "";
                if (divId.indexOf("hotc") == 0 || divId.indexOf("atc") == 0) {
                    commentclass = "LookL";
                }
                else {
                    commentclass = "LookH";
                }


                var result = "<div class=\"" + commentclass + "\"><div class=\"Round1\">";
                result += "<div class=\"Round2\">";
                result += "<div class=\"Round3\">";
                result += "<textarea class=\"Bd Fa\" onpropertychange=\"if(value.length>140) value=value.substr(0,140)\"  name=\"commentInfoId" + spreadid + "\" style=\"overflow-y:hidden;\" id=\"commentInfoId" + spreadid + "\" cols=\"60\" rows=\"2\"></textarea>";
                result += "<div class=\"Hr_10\"></div> <div>";
                result += "<div class=\"Hr_10\"></div>";
                result += "<div class=\"WinBtn  R\"><span><input name=\"Btncomment\" id=\"Btncomment\" type=\"button\" onclick=\"javascript:sendComment('" + divId + "','" + spreadid + "','" + isoriginal + "'," + sum + ");\"  value=\"评论\" /></span></div>";
                result += "<div class=\"ICOlist L\" style=\"position:relative;\">";
                result += "<ul >";
                result += "<li><span class=\"ico1\"></span><a href=\"javascript:void(0);\" class=\"Blue\" id=\"commentfaceId" + spreadid + "\" onmouseover=\"this.style.cursor='pointer'\" onclick=\"changeExpressio(this,'commentInfoId" + spreadid + "');\">表情</a></li></ul></div></div>";


                if (dataObj.List.length > 0) {

                    result += "<div class=\"Hr_10\"></div>";
                    result += "<div class=\"L30 G9\"><span class=\"R\" id=\"sumCount\"> 约" + sum + "条</span><strong>全部</strong></div>";
                    result += "<div class=\"Line_dashed\"></div>";
                    result += "<div class=\"Hr_10\"></div>";

                }

                var myUserId = $.cookie('useid');

                var url = "";


                //循环获取值
                $.each(dataObj.List, function(idx, item) {

                    if (item.ic_id != undefined) {

                        url = myUserId == item.userid ? "/u" : "/u_" + item.userid;

                        comment += "<div class=\" G6\">";
                        comment += "<a href=\"" + url + "\" ><img src=\"../images/face/small/" + item.face
                        + "\" alt=\"头像\" width=\"30\" height=\"30\" class=\"L\" id=\"usercomment" + item.ic_id + "\" onmouseover=\"UserInfoShowOver(this," + item.userid + "," + item.ic_id + ")\" /></a>";
                        comment += "<Div class=\"L18 Pl10 L Rbox\">";

                        comment += "<a href=\"" + url + "\" id=\"uu" + item.ic_id + "\" onmouseover=\"UserInfoShowOver(this," + item.userid + "," + item.ic_id + ")\" class=\"Blue\">" + item.nickname + "</a>：" + unescape(item.content) + "(" + item.timestr + ") <br>";

                        comment += "<div class=\"Tr G9\">";


                        //评论是
                        if (myUserId == item.ic_currentuserid || myUserId == item.userid) {

                            comment += "<a href=\"javascript:void(0);\" onclick=\"showCommentResult(" + item.ic_id + ");\" class=\"Gray9\">删除</a>";

                        } else {


                            comment += "<a href=\"javascript:void(0);\" onclick=\"ShowPageReport(" + item.ic_id + ",'" + item.nickname + "','" + item.face + "','" + escape(item.content) + "')\" class=\"Gray9\">举报</a>";



                        }

                        comment += " | ";

                        comment += "<a href=\"javascript:void(0);\" onclick=\"replyComment('" + item.nickname + "','commentInfoId" + spreadid + "','" + item.ic_id + "');\" class=\"Gray9\">回复</a>";


                        comment += "</div></div></div>";

                        if (idx != dataObj.List.length - 1) {

                            comment += "<div class=\"Hr_10\"></div><div class=\"Line_dashed\"></div><div class=\"Hr_10\"></div>";

                        }

                    }
                });


                result += comment;
                result += "<div class=\"Hr_10\"></div>";
                //转发条数大于10显示查看更多
                if (parseInt(sum) > 10) {

                    result += "<div><a href=\"cont_" + spreadid + "_1\" class=\"Gray9 R\">查看所有" + sum + "条评论>></a> </div>";

                }

                result += "</div></div>";
                result += "<span class=\"Jiao\" style=\"left:435px; top:-9px\">◆</span>";
                result += "</div></div>";

                $("#" + divId).html(result);
                $("#" + divId).show();


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

}

//回复评论
function replyComment(nickName, inputId, commentId) {

    $("#sendCommentHiddent").val("0");
    $("#sendCommentHiddent").val(commentId)

    //先清空
    $("#" + inputId).val("");
    $("#" + inputId).val("回复@" + nickName + ":");
    //$("#commentInfoId").focus();   
    myfocus(inputId); //焦点聚焦

}

//发送评论
function sendComment(divid, spreadid, isoriginal, sum) {

    var commentId = $("#sendCommentHiddent").val();

    if (commentId == undefined) {
        commentId = 0;
    }

    //转发内容
    var content = $("#commentInfoId" + spreadid).val();
    content = content.replace(/[\r\n]/mg, " "); //替换换行
    content = content.replace(/^ +| +$/g, '').replace(/ +/g, ' '); //去空格

    //是否是回复
    var indexat = content.indexOf("回复");

    //判断回复的是否为空
    var index = content.indexOf(":");
    var indexstr = content.substring(index + 1, index + 2)

    //判断点击了回复还是评论
    var tipeStr = "评论";

    //判断回复、纯文字
    if (content != "" && indexstr != "") {
        $.ajax({

            url: "" + vServiceUrl + "/VipIlogUser.asmx/ILogAddCommentInfo",
            type: "POST",
            dataType: "json",
            contentType: "application/json;",
            data: "{spreadid:'" + spreadid + "',isoriginal:" + isoriginal + ",content:'" + content + "',commontId:'" + commentId + "',i:'" + rand + "'}",
            success: function(json) {

                var dataObj = eval("(" + json.d + ")"); //转换为json对象     　　     
                var result = "";

                if (dataObj.state == 1) {

                    result = tipeStr + "成功";

                    var newSum = parseInt(sum) + 1; //评论数加1

                    $("#sumCount").html("约" + newSum + "条");
                    $("#replyCount" + spreadid).html("评论(" + newSum + ")");

                    //加载全部内容（个人首页专用）
                    //GetAllList("" + vServiceUrl + "ILog_Spread.asmx/GetAllList", 1, 45); 
                    $("#sendCommentHiddent").val("0");
                    checkComment(1, divid, spreadid, isoriginal, newSum);


                } else {

                    result = tipeStr + "失败";

                }

                showTipe(result, dataObj.state); //提示  
                $("#commentInfoId" + spreadid).val("");
            },
            //出错调试         
            error: function(x, e) {
                //alert(x.responseText);     
            },
            //执行成功后自动执行           
            complete: function(x) {

            }
        });
    } else {
        showTipe(tipeStr + "的内容不能为空哦!", 0); //提示            

    }

}

//移出a标签隐藏层
function mouseroutInfo(isId) {

    $("#h" + isId).hide();

}

//移出隐藏层
function mousermoveInfo(isId) {

    $("#h" + isId).show();

}


//悬浮层
function moveDiv(id) {

    //var top1=document.body.scrollTop+event.clientY;

    $(".F14").mouseover(function() {

        hide();

    });

    $("#" + id).mouseout(function() {



        $("#h" + id).mouseover(function() {

            $("#h" + id).hover(function() {

                $("#h" + id).show();

            },
                function() {

                    $("#h" + id).hide();

                }

            );
        });

    });
}


//判断我与用户之间的关系
function funGetConcernExists(userid) {

    $.ajax({
        url: "" + vServiceUrl + "ILogFollow.asmx/ILogFollowConcernState",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{concernUserid:'" + userid + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            var ToLiContent = "";
            var nickname = $("#personalInfo a:first").text();

            $.each(dataObj.ConcernState, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        TopContent = "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-4.gif\" class=\"L Rimg\" />"
                        TopContent += "<a href=\"javascript:void(0);\" class=\" Gray6\" >出错</a>";
                        return true;
                    }
                    else if (item.UrlState == "1") {
                        TopContent = "<span class=\"R G9\"><a href=\"javascript:showdialog_u('" + nickname + "',0);\" class=\"Blue\">发站短</a>";
                        TopContent += " </span>";
                        TopContent += "<div class=\"WinBtn\">";

                        TopContent += "<a href=\"javascript:void(0);\" class=\"White\">";
                        TopContent += "<span>";
                        TopContent += "<input name=\"btnAddConcern\" type=\"button\" value=\"+ 加关注\" onclick=\"ShowAddFollowTrueTaConcern(0," + userid + ",'" + nickname + "')\" />";
                        TopContent += "</span>";
                        TopContent += "</a>";

                        TopContent += "</div>";




                        return true;
                    }
                    else if (item.UrlState == "2") {
                        TopContent = "<span class=\"R G9\"><a href=\"javascript:showdialog_u('" + nickname + "',0);\" class=\"Blue\">发站短</a>";
                        TopContent += " | <a href=\"javascript:ShowAddFollowTrueTaGroup(0," + userid + ",'" + nickname + "')\" class=\"Blue\">设置分组</a>";
                        TopContent += " </span>";
                        TopContent += "<div class=\"GuanZ G9\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-4.gif\" class=\"L Rimg\" />"
                        TopContent += "<a href=\"javascript:void(0);\" class=\" Gray6\"> 已关注</a>";
                        TopContent += "&nbsp;|&nbsp;";
                        TopContent += "<a class=\"Blue\" href=\"javascript:ShowPageConcern(0," + userid + ",'" + nickname + "')\">取消</a></div>";
                        return true;
                    }
                    else if (item.UrlState == "3") {
                        TopContent = "<span class=\"R G9\"><a href=\"javascript:showdialog_u('" + nickname + "',0);\" class=\"Blue\">发站短</a>";
                        TopContent += " | <a href=\"javascript:ShowAddFollowTrueTaGroup(0," + userid + ",'" + nickname + "')\" class=\"Blue\">设置分组</a>";
                        TopContent += " | <a href=\"javascript:ShowPageDeleteFan(0," + userid + ",'" + nickname + "')\" class=\"Blue\">移除粉丝</a>";
                        TopContent += " </span>";
                        TopContent += "<div class=\"GuanZ G9\" style=\"width:120px\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/ico-3.gif\" class=\"L Rimg\" />"

                        TopContent += "<a href=\"javascript:void(0);\" class=\" Gray6\"> 互相关注</a>";
                        TopContent += "&nbsp;|&nbsp;";

                        TopContent += "<a class=\" Blue\"  href=\"javascript:ShowPageConcern(0," + userid + ",'" + nickname + "')\">取消</a></div>";
                        return true;
                    }

                }
            });


            $("#HpersionConcern").html(TopContent);
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


//他人主页加关注
//iufid：
//concernuserid：被关注用户id
//nikename：昵称
//vi_id：vipILog表流水号，为以关注换图片用
function ShowAddFollowTrue(iufid, concernuserid) {
    $.ajax({
        url: "" + vServiceUrl + "ILogFollow.asmx/ILogFollowAddFan",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{iufID:'" + iufid + "',concernUserid:'" + concernuserid + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var TopContent = "";
            $.each(dataObj.FanCancel, function(idx, item) {
                if (idx == 0) {
                    if (item.UrlState == "0") {
                        //　　                    alert("加载错误");
                        return false;
                    }
                    else if (item.UrlState == "3") {
                        showTipe("自己不能关注自己", 0);
                        return false;
                    }
                    else {
                        showTipe("关注成功", 1);
                        //　　                alert("关注成功");
                        //　　                   nikename="自定义";
                        //　　                       //关注操作框
                        //　　                       showConcerntDialog=$.dialog
                        //　　                       ({
                        //                                id: "divrDialog",
                        //                                title: false,
                        //                                content: ShowAddFollowTrueInfo(iufid,concernuserid,nikename),
                        //                                max: false,
                        //                                min: false,
                        //                                lock: true,
                        //                                cache: false,
                        //                                width: 500,
                        //                                height: 400,
                        //                                padding: 0 
                        //                            
                        //                        });
                        //                        

                        //                        CloseConcern();

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



//发送内容长度限定.by lx on 20120528
//inputid：文本域id
function checkForWardLength(inputid) {

    var content = $("#" + inputid).val();

    var contentlen = Getlength(content);

    var font_count = Math.floor((280 - contentlen) / 2);

    var num = Math.abs(font_count);

    //内容超长不能发送
    if (font_count < 0) {
        $("#countNum").html("已经超出<font class=\"publish_num\" >" + num + "</font>字");
        $("#forwardBtn").attr("class", "");
        $("#forwardBtn").addClass("WinBtn_H R");
        $("#forwardClickId").attr('disabled', 'disabled');
        $("#" + inputid).focus();
    }
    else  //可以发送
    {
        $("#countNum").html("还能输入<font class=\"publish_num\" >" + num + "</font>字");
        $("#forwardBtn").attr("class", "");
        $("#forwardBtn").addClass("WinBtn R");
        $("#forwardClickId").removeAttr('disabled');
        $("#" + inputid).focus();
    }

}



//发送内容长度限定.by lx on 20120528
//inputid：文本域id
//ation：1站短第一个页面，2站短第一个页面，3站短第一个页面
function checkSendMail(inputid, ation) {
    var content = $("#" + inputid).val();

    //站短内容长度
    var contentlen = Getlength(content);

    var font_count = Math.floor((1000 - contentlen) / 2);

    //内容超长不能发送
    if (font_count >= 0) {
        $("#prompt").text("还能输入" + font_count + "字");
        $("#btnImg_div").html("<div class=\"WinBtn  R\" ><a class=\"White\" id=\"btnImg\" href=\"javascript:void(0);\" onclick=\"ReplyMail(" + ation + ");\" ><span>发送</span></a></div>");
        $("#" + inputid).focus();
    }
    else  //可以发送
    {
        $("#prompt").text("已经超过" + Math.abs(font_count) + "字");
        $("#btnImg_div").html("<div class=\"WinBtn_H  R\" style=\"cursor:default;\" ><span style=\"cursor:default;\" >发送</span></div>");
        $("#" + inputid).focus();
    }

}


//发送内容长度限定.by lx on 20120528（悬浮框专用为避免标签冲突）
//inputid：文本域id（悬浮框专用为避免标签冲突）
function checkSendMail_Box(inputid) {
    var content = $("#" + inputid).val();

    //站短内容长度
    var contentlen = Getlength(content);

    var font_count = Math.floor((1000 - contentlen) / 2);

    if (font_count >= 0) {
        $("#Prompt_f").html("还能输入");
        $("#prompt_b").text(font_count);   //数据部分需要样式
        $("#Prompt_f2").html("字")

        $("#btnImg_div_b").html("<div class=\"WinBtn  R\" ><a class=\"White\" href=\"javascript:void(0);\" class=\"White\"><span><input name=\"发送\" type=\"button\" id=\"发送\" value=\"发送\" onclick=\"SendMail();\" /></span></a></div>");
        $("#" + inputid).focus();

        return true;
    }
    else {
        $("#Prompt_f").html("已经超过");
        $("#prompt_b").text(Math.abs(font_count));           //数据部分需要样式
        $("#Prompt_f2").html("字")

        $("#btnImg_div_b").html("<div class=\"WinBtn_H  R\" style=\"cursor:default;\" ><span style=\"cursor:default;\" >发送</span></div>");
        $("#" + inputid).focus();
    }
}

//发送内容长度限定.by lx on 20120528（悬浮框专用为避免标签冲突回复站短功能）
//inputid：文本域id（悬浮框专用为避免标签冲突回复站短功能）
function checkSendMail_Box_ReplyMail(inputid) {
    var content = $("#" + inputid).val();

    //站短内容长度
    var contentlen = Getlength(content);

    var font_count = Math.floor((1000 - contentlen) / 2);

    if (font_count >= 0) {
        $("#Prompt_f").html("还能输入");
        $("#prompt_b").text(font_count);   //数据部分需要样式
        $("#Prompt_f2").html("字")

        $("#btnImg_div_b").html("<div class=\"WinBtn  R\" ><a class=\"White\" href=\"javascript:void(0);\" class=\"White\"><span><input name=\"发送\" type=\"button\" id=\"发送\" value=\"发送\" onclick=\"ReplyMail();\" /></span></a></div>");
        $("#" + inputid).focus();

        return true;
    }
    else {
        $("#Prompt_f").html("已经超过");
        $("#prompt_b").text(Math.abs(font_count));           //数据部分需要样式
        $("#Prompt_f2").html("字")

        $("#btnImg_div_b").html("<div class=\"WinBtn_H  R\" style=\"cursor:default;\" ><span style=\"cursor:default;\" >发送</span></div>");
        $("#" + inputid).focus();
    }
}

//按字节计算获取字符的长度，汉字为2个字节
function Getlength(str) {

    var len = 0;
    if (str == undefined) {
        return len;
    }

    var oVal = str;
    var oValLength = 0;
    oVal.replace(/n*s*/, '') == '' ? oValLength = 0 : oValLength = oVal.match(/[^ -~]/g) == null ? oVal.length : oVal.length + oVal.match(/[^ -~]/g).length;

    return oValLength;
}


//是否在显示搜索提示时第一次按上键，如果是要选中最后一项，该变量默认是不选中的
var isp = false;

//只能提示收件人（页面搜索用户）
function searchtowho2() {
    //收件人昵称
    var towho = $("#keyword_s");


    var strTowhoValue = towho.val();

    //校验收信人
    if (strTowhoValue != "") {
        if (ignoreSpaces(strTowhoValue) == "") {
            showTipe("请输入昵称！", 0);
            towho.focus();
            return false;
        }
        if (HTMLEncode(strTowhoValue) == "") {
            showTipe("请输入昵称！", 0);
            towho.focus();
            return false;
        }
        if (removeHTMLTag(strTowhoValue) == "") {
            showTipe("请输入昵称！", 0);
            towho.focus();
            return false;
        }
    }


    //开始发送
    $.ajax({
        //请求WebService Url         
        url: "" + vServiceUrl + "VipMail.asmx/GetNickNameByUserID_MailList",
        //请求类型,请据WebService接受类型定制          
        type: "POST",
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        cache: false,
        //请求参数              
        data: "{nickname:'" + strTowhoValue + "'}",
        //成功           
        success: function(json) {

            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var strList = "";

            //获取当然用户id
            //var userid = $.cookie('useid'); 

            //循环获取值
            $.each(dataObj.List, function(idx, item) {

                if (idx == 0) {
                    if (item.State == "0") {
                        //                        alert("加载错误！");
                    }
                    else if (item.State == "2")  //无数据不显示分页控件
                    {
                        //                        showTipe("未找到收件人！",0);

                        return false;   //无数据不再往下执行
                    }
                    else {
                        return true;
                    }
                }
                else {
                    if (item.nickname != undefined) {
                        strList += "<li class=\"\" style=\"cursor:hand\" id=\"il_s" + idx + "\" onclick=\"Getnickname_Box('" + item.nickname + "')\" ><a href=\"javascript:void(0)\" id=\"hrSearchTowhPeple" + idx + "\" >" + item.nickname + "</a></li>";

                        return true;
                    }
                }
            });

            //加载下拉菜单
            $("#GetSearchTowho_Menu").html(strList);

            //有数据显示下拉框
            if (strList != "") {
                GetSearchTowhUpList();

                //重新搜索数据时要初始化是否在搜索框中直接按上键的标记
                isp = false;
            }
            else    //没有数据隐藏掉提示框
            {
                $("#GetSearchTowho_Menu").hide();
            }


            //鼠标滑动保持唯以选中的样式（下拉项）
            $("#GetSearchTowho_Menu li").mouseover(function() {
                //鼠标滑过
                $(this).addClass("WindowBG");
                var index = $("#GetSearchTowho_Menu li").index($(this));

                //开始遍历
                $("#GetSearchTowho_Menu li").each(function(i) {
                    if (i != index) {
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

            window.location.href = "http://c.instrument.com.cn/art/ilog/404.asp";
        },
        //执行成功后自动执行           
        complete: function(x) {

        }
    });

}


//只能提示部分（站短搜索用）


//搜索下拉
function GetSearchTowhUpList() {

    //收件人文本框值
    var txtSearchValue = $("#keyword_s").val();

    //框内数据不为空就开始定位
    if (txtSearchValue != null && txtSearchValue != "") {
        setMenuPositionsSearch2("GetSearch");
        MenuDivShow2("GetSearch");
    }
    else {
        $("#GetSearchTowho_Menu").hide();
    }

}

//下拉框定位
function setMenuPositionsSearch2(ShowID) {
    var offset = $('#keyword_s').offset();
    var divheight = $('#keyword_s').innerHeight();

    var leftpadd = 0;

    $('#' + ShowID + 'Towho_Menu').css
	({
	    //'left':offset.left + -208, //左右定位
	    //'top':offset.top+20,       //上下定位
	    'position': 'absolute'
	}).show();

}

//控制隐藏显示
function MenuDivShow2(showdiv) {
    $('#' + showdiv + 'Towho_Menu').mouseover(function() { $(this).show(); });
    $('#' + showdiv + 'Towho_Menu').mouseout(function() { $(this).hide(); });
    $('#' + showdiv).mouseout(function() { $('#' + showdiv + 'Towho_Menu').hide(); });
}

//把选中的收件人放入框中
//towho：收件人
function Getnickname_Box(towho) {
    $("#keyword_s").val(towho);

    //判断是否用鼠标选择搜索提示结果，如果是隐藏下拉，搜索框获取焦点，并执行搜索
    var event = arguments.callee.caller.arguments[0] || window.event;

    if (event.keyCode != 38 && event.keyCode != 40 && event.keyCode != undefined) {
        $("#GetSearchTowho_Menu").hide();
        $("#keyword_s").focus();

        //获取数据
        GetSearchList("" + vServiceUrl + "VipMail.asmx/GetMailList", 1, 45, towho);
    }
}


//上下键处理
//搜索下拉
function funListBeginUp_m(e) {
    var keynum;

    if (window.event) // IE
    {
        keynum = e.keyCode;
    }
    else if (e.which) // Netscape/Firefox/Opera
    {
        keynum = e.which;
    }

    if (keynum != 13 && keynum != 38 && keynum != 40) {
        return false;
    }
    else if (keynum == 13) {
        funListBeginUpUL_m("GetSearchTowho_Menu", 2);
    }
    else if (keynum == 38) {
        funListBeginUpUL_m("GetSearchTowho_Menu", 0);
    }
    else if (keynum == 40) {
        funListBeginUpUL_m("GetSearchTowho_Menu", 1);
    }

    return false;

}


//上下键处理
function funListBeginUpUL_m(NameID, vType) {
    var bl = true;

    //用户在搜索结果提示按上键直接选中最后一项
    if (vType == 0 && !isp) {
        var ilsize = $("#GetSearchTowho_Menu li").size();

        $("#il_s" + ilsize).addClass("WindowBG");
        Getnickname_Box($("#il_s" + ilsize).text());

        isp = true;
    }
    else {
        $("#" + NameID + " li").each(function(i) {
            if (vType == 0) {
                if ($(this).hasClass("WindowBG")) {
                    if (bl) {
                        var index = $("#GetSearchTowho_Menu li").index($(this));
                        var ilsize = $("#GetSearchTowho_Menu li").size();

                        //判断是不是选到第一个，如果是需要循环到最后一个
                        if (index == 0) {

                            $(this).removeClass();
                            $("#il_s" + ilsize).addClass("WindowBG");
                            Getnickname_Box($("#il_s" + ilsize).text());
                        }
                        else {
                            $("#GetSearchTowho_Menu li.WindowBG").prev().addClass("WindowBG");
                            $("#GetSearchTowho_Menu li.WindowBG").next().removeClass("WindowBG");


                            //开始遍历
                            $("#GetSearchTowho_Menu li").each(function(i) {
                                if (i != (index - 1) && index != 0) {
                                    $(this).removeClass();
                                }
                            })

                            //把选中的值放入搜索框中
                            if ($(this).prev().text() != "") {
                                Getnickname_Box($(this).prev().text());
                            }
                            else    //如果到了最后一个就保持当前选中的值
                            {
                                Getnickname_Box($(this).text());
                            }
                        }

                        bl = false;

                    }
                }
            }
            else if (vType == 1) {
                if ($(this).hasClass("WindowBG")) {
                    if (bl) {
                        var index = $("#GetSearchTowho_Menu li").index($(this));
                        var ilsize = $("#GetSearchTowho_Menu li").size();

                        //判断是不是选到最后一个，如果是需要循环到第一个
                        if (index == (ilsize - 1)) {
                            $(this).removeClass();
                            $("#il_s1").addClass("WindowBG");
                            Getnickname_Box($("#il_s1").text());
                        }
                        else {
                            //向下
                            $("#GetSearchTowho_Menu li.WindowBG").next().addClass("WindowBG");
                            $("#GetSearchTowho_Menu li.WindowBG").prev().removeClass("WindowBG");


                            //开始遍历，除了当前选中的选项其他都移除
                            $("#GetSearchTowho_Menu li").each(function(i) {
                                if (i != (index + 1) && (index + 1) != ilsize) {
                                    $(this).removeClass();
                                }
                            })

                            //把选中的值放入搜索框中
                            if ($(this).next().text() != "") {
                                Getnickname_Box($(this).next().text());
                            }
                            else    //如果到了最后一个就保持当前选中的值
                            {
                                Getnickname_Box($(this).text());
                            }

                            bl = false
                        }
                    }
                }
                if (bl)    //选择第一条搜索提示
                {
                    //只要不是第一次按上就改变“第一次上键的状态”
                    isp = true;

                    $("#il_s1").addClass("WindowBG");

                    Getnickname_Box($("#il_s1").text());

                }
            }
            else if (vType == 2) {
                //回车
                if ($(this).hasClass("WindowBG")) {
                    var strKeyWord = "";

                    //判断是否按了上下键如果没有说明用户想做模糊查询
                    if (!isUpDownKey()) {
                        strKeyWord = $("#keyword_s").val();
                    }
                    else {
                        strKeyWord = $(this).text();
                    }

                    Getnickname_Box(strKeyWord);

                    GetSearchList("" + vServiceUrl + "VipMail.asmx/GetMailList", 1, 45, strKeyWord);
                }
            }

        })
    }
}

//获取回车事件
function getenterevent_m() {
    $(function() {
        $("input[type='text']").keypress(function(evt) {
            evt = (evt) ? evt : ((window.event) ? window.event : "");
            var key = evt.keyCode ? evt.keyCode : evt.which;

            if (key == 13) {
                var id = new String($(this).attr("id"));
                var num = id.substr(id.length - 1, 1);

                $("#bnt_" + num).click();
                return false;
            }
        });
    });
}


//焦点定位在内容之后
function myfocus(myid) {
    if (isNav) {
        document.getElementById(myid).focus(); // 获取焦点   

    } else {
        setFocus.call(document.getElementById(myid));
    }
}

//焦点定位在内容之前
function myfocusSatrt(myid) {
    if (isNav) {
        document.getElementById(myid).focus(); // 获取焦点   

    } else {
        startFocus.call(document.getElementById(myid));
    }
}


var isNav = (window.navigator.appName.toLowerCase().indexOf("netscape") >= 0);
var isIE = (window.navigator.appName.toLowerCase().indexOf("microsoft") >= 0);
function setFocus() {
    var range = this.createTextRange(); //建立文本选区 
    range.moveStart('character', this.value.length); //选区的起点移到最后去
    range.collapse(true);
    range.select();
}

function startFocus() {
    var range = this.createTextRange(); //建立文本选区 
    range.moveStart('character', 0); //选区的起点移到最后去
    range.collapse(true);
    range.select();
}


//表情插入
function insertExpressio(obj, inputId) {

    var ret = $("#" + inputId).val();

    var index = ret.indexOf("//@");

    if (inputId == "forwardInfo") {

        //判断转发的内容是否为空定位焦点位置
        if (ret == "" || index < 0) {

            //$("#" + inputId).val(ret + "[" + obj.alt + "] ");

            $("#" + inputId).insertAtCaret("[" + obj.alt + "]");

            checkForWardLength(inputId);
            //myfocus("forwardInfo");

        }
        else {

            // $("#" + inputId).val("[" + obj.alt + "] " + ret);
            $("#" + inputId).insertAtCaret("[" + obj.alt + "]");
            checkForWardLength("forwardInfo");
            //myfocusSatrt("forwardInfo");

        }

    }
    else {


        //$("#" + inputId).val(ret + "[" + obj.alt + "] ");
        $("#" + inputId).insertAtCaret("[" + obj.alt + "]");
        checkSendBlog(inputId); //检查长度
        // myfocus(inputId);

    }


    $("#expressioId").hide();

}

//选择表情
function changeExpressio(obj, inputId) {

    //图片层、表情层隐藏
    $("#screenId").hide();
    $("#picId").hide();

    var ss = ShowExpression(inputId);
    $("#" + obj.id).after(ss);

}

//../images/head/face_bb.gif

//公共表情调用
function ShowExpression(inputId) {

    var ShowTable = "<div id=\"expressioId\" class=\"WindowWark490 BrWh\" style=\"position:absolute;display:block;z-index:2;top:25px;left:0px;\">";
    ShowTable += "<div class=\"WindowTil G4 \">";
    ShowTable += "<a href=\"javascript:void(0);\">";
    ShowTable += "<img class=\" R Img\" src=\"http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif\" alt=\"关闭\" onclick=\"javascript:$('#expressioId').hide();\" />";
    ShowTable += "</a></div>";
    //    
    //    ShowTable += "<img class=\" R Img\" src=\"http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif\" alt=\"关闭\" onclick=\"javascript:$('#expressioId').hide();\"/>";
    ShowTable += "<div class=\" WindowBox\">";
    ShowTable += "<div class=\" Cl\">";
    ShowTable += "<ul class=\"WindowBq\">";
    ShowTable += "<li ><img src=\"../images/head/weixiao.gif\" alt=\"微笑\" title=\"微笑\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += "<li ><img src=\"../images/head/pizui.gif\" alt=\"撇嘴\" title=\"撇嘴\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += "<li ><img src=\"../images/head/se.gif\"alt=\"色\" title=\"色\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += "<li ><img src=\"../images/head/fadai.gif\" alt=\"发呆\" title=\"发呆\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += "<li ><img src=\"../images/head/deyi.gif\" alt=\"得意\" title=\"得意\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += " <li ><img src=\"../images/head/liulei.gif\" alt=\"流泪\" title=\"流泪\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += "<li ><img src=\"../images/head/haixiu.gif\" alt=\"害羞\" title=\"害羞\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += "<li ><img src=\"../images/head/bizui.gif\" alt=\"闭嘴\" title=\"闭嘴\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += "<li ><img src=\"../images/head/shuijiao.gif\" alt=\"睡\" title=\"睡\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += " <li ><img src=\"../images/head/daku.gif\" alt=\"大哭\" title=\"大哭\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += "<li ><img src=\"../images/head/gangga.gif\" alt=\"尴尬\" title=\"尴尬\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += "<li ><img src=\"../images/head/fanu.gif\" alt=\"发怒\" title=\"发怒\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += "<li ><img src=\"../images/head/tiaopi.gif\" alt=\"调皮\" title=\"调皮\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += " <li ><img src=\"../images/head/ciya.gif\" alt=\"呲牙\" title=\"呲牙\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += "<li ><img src=\"../images/head/jingya.gif\" alt=\"惊讶\" title=\"惊讶\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += "<li ><img src=\"../images/head/nanguo.gif\" alt=\"难过\" title=\"难过\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += "<li ><img src=\"../images/head/ku.gif\" alt=\"酷\" title=\"酷\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += "<li ><img src=\"../images/head/lenghan.gif\" alt=\"冷汗\" title=\"冷汗\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += " <li ><img src=\"../images/head/zhuakuang.gif\" alt=\"抓狂\" title=\"抓狂\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += " <li ><img src=\"../images/head/tu.gif\" alt=\"吐\" title=\"吐\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += "<li ><img src=\"../images/head/touxiao.gif\" alt=\"偷笑\" title=\"偷笑\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += "<li ><img src=\"../images/head/keai.gif\" alt=\"可爱\" title=\"可爱\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += "<li ><img src=\"../images/head/baiyan.gif\" alt=\"白眼\" title=\"白眼\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li>";
    ShowTable += "<li ><img src=\"../images/head/aoman.gif\" alt=\"傲慢\" title=\"傲慢\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/er.gif\" alt=\"饥饿\" title=\"饥饿\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/kun.gif\" alt=\"困\" title=\"困\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/jingkong.gif\" alt=\"惊恐\" title=\"惊恐\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/liuhan.gif\" alt=\"流汗\" title=\"流汗\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/haha.gif\" alt=\"憨笑\" title=\"憨笑\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/dabing.gif\" alt=\"大兵\" title=\"大兵\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/fendou.gif\" alt=\"奋斗\" title=\"奋斗\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/ma.gif\" alt=\"咒骂\" title=\"咒骂\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/wen.gif\" alt=\"疑问\" title=\"疑问\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/xu.gif\" alt=\"嘘\" title=\"嘘\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";

    ShowTable += "<li ><img src=\"../images/head/yun.gif\" alt=\"晕\" title=\"晕\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/zhemo.gif\" alt=\"折磨\" title=\"折磨\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/shuai.gif\" alt=\"衰\" title=\"衰\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/kulou.gif\" alt=\"骷髅\" title=\"骷髅\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/da.gif\" alt=\"敲打\" title=\"敲打\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/zaijian.gif\" alt=\"再见\" title=\"再见\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/cahan.gif\" alt=\"擦汗\" title=\"擦汗\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/wabi.gif\" alt=\"抠鼻\" title=\"抠鼻\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/guzhang.gif\" alt=\"鼓掌\" title=\"鼓掌\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/qioudale.gif\" alt=\"糗大了\" title=\"糗大了\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/huaixiao.gif\" alt=\"坏笑\" title=\"坏笑\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/zuohengheng.gif\" alt=\"左哼哼\" title=\"左哼哼\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/youhengheng.gif\" alt=\"右哼哼\" title=\"右哼哼\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/haqian.gif\" alt=\"哈欠\" title=\"哈欠\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/bishi.gif\" alt=\"鄙视\" title=\"鄙视\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/weiqu.gif\" alt=\"委屈\" title=\"委屈\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/kuaikule.gif\" alt=\"快哭了\" title=\"快哭了\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/yinxian.gif\" alt=\"阴险\" title=\"阴险\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/qinqin.gif\" alt=\"亲亲\" title=\"亲亲\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/xia.gif\" alt=\"吓\" title=\"吓\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/kelian.gif\" alt=\"可怜\" title=\"可怜\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/caidao.gif\" alt=\"菜刀\" title=\"菜刀\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/xigua.gif\" alt=\"西瓜\" title=\"西瓜\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/pijiu.gif\" alt=\"啤酒\" title=\"啤酒\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/lanqiu.gif\" alt=\"篮球\" title=\"篮球\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/pingpang.gif\" alt=\"乒乓\" title=\"乒乓\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/kafei.gif\" alt=\"咖啡\" title=\"咖啡\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/fan.gif\" alt=\"饭\" title=\"饭\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/zhutou.gif\" alt=\"猪头\" title=\"猪头\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/hua.gif\" alt=\"玫瑰\" title=\"玫瑰\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/diaoxie.gif\" alt=\"凋谢\" title=\"凋谢\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/kiss.gif\" alt=\"示爱\" title=\"示爱\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/love.gif\" alt=\"爱心\" title=\"爱心\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/xinsui.gif\" alt=\"心碎\" title=\"心碎\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/dangao.gif\" alt=\"蛋糕\" title=\"蛋糕\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/shandian.gif\" alt=\"闪电\" title=\"闪电\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/zhadan.gif\" alt=\"炸弹\" title=\"炸弹\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/dao.gif\" alt=\"刀\" title=\"刀\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/zuqiu.gif\" alt=\"足球\" title=\"足球\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/chong.gif\" alt=\"瓢虫\" title=\"瓢虫\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/dabian.gif\" alt=\"便便\" title=\"便便\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/yueliang.gif\" alt=\"月亮\" title=\"月亮\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/taiyang.gif\" alt=\"太阳\" title=\"太阳\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/liwu.gif\" alt=\"礼物\" title=\"礼物\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/yongbao.gif\" alt=\"拥抱\" title=\"拥抱\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/qiang.gif\" alt=\"强\" title=\"强\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/ruo.gif\" alt=\"弱\" title=\"弱\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/woshou.gif\" alt=\"握手\" title=\"握手\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/shengli.gif\" alt=\"胜利\" title=\"胜利\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/peifu.gif\" alt=\"抱拳\" title=\"抱拳\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/gouyin.gif\" alt=\"勾引\" title=\"勾引\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/quantou.gif\" alt=\"拳头\" title=\"拳头\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/chajin.gif\" alt=\"差劲\" title=\"差劲\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/geili.gif\" alt=\"给力\" title=\"给力\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/no.gif\" alt=\"NO\" title=\"NO\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/ok.gif\" alt=\"OK\" title=\"OK\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/cheer.gif\" alt=\"干杯\" title=\"干杯\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/feiwen.gif\" alt=\"飞吻\" title=\"飞吻\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/tiao.gif\" alt=\"跳跳\" title=\"跳跳\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/fadou.gif\" alt=\"发抖\" title=\"发抖\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/dajiao.gif\" alt=\"怄火\" title=\"怄火\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/zhuanquan.gif\" alt=\"转圈\" title=\"转圈\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/ketou.gif\" alt=\"磕头\" title=\"磕头\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/huitou.gif\" alt=\"回头\" title=\"回头\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/tiaosheng.gif\" alt=\"跳绳\" title=\"跳绳\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/huishou.gif\" alt=\"挥手\" title=\"挥手\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/jidong.gif\" alt=\"激动\" title=\"激动\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/tiaowu.gif\" alt=\"街舞\" title=\"街舞\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/xianwen.gif\" alt=\"献吻\" title=\"献吻\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/zuotaiji.gif\" alt=\"左太极\" title=\"左太极\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";
    ShowTable += "<li ><img src=\"../images/head/youtaiji.gif\" alt=\"右太极\" title=\"右太极\" onclick=\"insertExpressio(this,'" + inputId + "');\"/></li> ";

    ShowTable += "</ul></div>";
    ShowTable += "<div class=\"Hr_10\"></div>";
    ShowTable += "<div class=\"WindowSanWT\">&nbsp;</div>";
    ShowTable += "</div></div>";
    return ShowTable;

}

//发送内容长度限定.by lx on 20120528
function checkSendBlog(input) {

    var content = $("#" + input).val();

    var contentlen = Getlength(content);

    var font_count = Math.floor((280 - contentlen) / 2);

    var num = Math.abs(font_count);

    if (font_count < 0) {

        $("#prompt").html("已经超过<font class=\"publish_num\">" + num + "</font>字");
        $("#btnImg").attr('disabled', 'disabled');
        $("#btnImg").attr("src", "http://simg.instrument.com.cn/ilog/blue/images/btn_fbH.gif");
        $("#" + input).focus();

    } else {

        $("#prompt").html("还能输入<font class=\"publish_num\">" + num + "</font>字");
        $("#btnImg").removeAttr('disabled');
        $("#btnImg").attr("src", "http://simg.instrument.com.cn/ilog/blue/images/btn_fb.gif");
        $("#" + input).focus();

    }

}

//发布博文焦点事件时隐藏层信息
function hideDiv() {

    //隐藏表情层
    $("#expressioId").hide();

    //隐藏图片层
    $("#picId").hide();

    //隐藏图片层
    $("#screenId").hide();

}

//页面标题
function ShowTitle(title) {
    document.title = title + " iLog-仪器信息网-记录身边的点滴";

}


//插入表情是用到

(function($) {
    $.fn.extend({
        insertAtCaret: function(myValue) {
            var $t = $(this)[0];
            if (document.selection) {
                this.focus();
                sel = document.selection.createRange();
                sel.text = myValue;
                this.focus();
            }
            else
                if ($t.selectionStart || $t.selectionStart == '0') {
                var startPos = $t.selectionStart;
                var endPos = $t.selectionEnd;
                var scrollTop = $t.scrollTop;
                $t.value = $t.value.substring(0, startPos) + myValue + $t.value.substring(endPos, $t.value.length);
                this.focus();
                $t.selectionStart = startPos + myValue.length;
                $t.selectionEnd = startPos + myValue.length;
                $t.scrollTop = scrollTop;
            }
            else {
                this.value += myValue;
                this.focus();
            }
        }
    })
})(jQuery);


//判断浏览器
function getInternet() {
    if (navigator.userAgent.indexOf("MSIE") > 0) {
        return "MSIE";       //IE浏览器  
    }
    if (isFirefox = navigator.userAgent.indexOf("Firefox") > 0) {
        return "Firefox";     //Firefox浏览器  
    }
    if (isSafari = navigator.userAgent.indexOf("Safari") > 0) {
        return "Safari";      //Safan浏览器  
    }
    if (isCamino = navigator.userAgent.indexOf("Camino") > 0) {
        return "Camino";   //Camino浏览器  
    }
    if (isMozilla = navigator.userAgent.indexOf("Gecko/") > 0) {
        return "Gecko";    //Gecko浏览器  
    }
}

//显示个人认证与名人认证图标
function ShowVerifyImg(memberlevel) {
    var imgMemberLevel = "";
    if (memberlevel == 1) {
        imgMemberLevel = "<a href=\"/verify/index.aspx\" title=\"个人认证\" alt=\"个人认证\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/i_blue.png\" class=\"ConImg\" /></a>";
    }
    else if (memberlevel == 2) {
        imgMemberLevel = "<a href=\"/verify/index.aspx\" title=\"名人认证\" alt=\"名人认证\"><img src=\"http://simg.instrument.com.cn/ilog/blue/images/i_blue.png\" class=\"ConImg\" /></a>";
    }
    return imgMemberLevel;

}

//记录访问历史
function RecordVisitHistory() {
    var ajaxurl = vServiceUrl + "IlogVisitHistory.asmx/AddVisitHistory";
    if ($("#userid") == undefined) {
        return;
    }
    var visiteduserid = $("#userid").val();
    if (visiteduserid == "undefined" || visiteduserid == "" || visiteduserid == $.cookie("useid") || visiteduserid == "0") {
        return;
    }
    $.ajax({
        type: "POST",
        url: "" + ajaxurl + "",
        cache: false,
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        data: "{visiteduserid:'" + visiteduserid + "'}",
        success: function(data, status) {

        }, error: function(result, status) {
            if (status == 'error') {
                alert(result.responseText);
            }
        },
        complete: function() {
        }
    });

}

//按照字节长度截取字符串
function substr(str, len) {
    if (!str || !len) {
        return '';
    }
    // 预期计数：中文2字节，英文1字节
    var a = 0;
    // 循环计数
    var i = 0;
    // 临时字串
    var temp = '';
    for (i = 0; i < str.length; i++) {
        if (str.charCodeAt(i) > 255) {
            // 按照预期计数增加2
            a += 2;
        }
        else {
            a++;
        }
        // 如果增加计数后长度大于限定长度，就直接返回临时字符串
        if (a > len) {
            return temp;

        }
        // 将当前内容加到临时字符串
        temp += str.charAt(i);
    }
    // 如果全部是单字节字符，就直接返回源字符串
    return str;
}


//得到转发中嵌套的原创内容
//io_id:原创id
function GetOriginalInfo(io_id, is_id, keyword) {

    var userid = $.cookie('useid');

    $.ajax({
        url: "" + vServiceUrl + "ILogOriginal.asmx/GetOriginalInfoByID",
        type: "POST",
        dataType: "json",
        contentType: "application/json;",
        data: "{id:'" + io_id + "'}",
        success: function(json) {

            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            var original = "";
            var haspic = 0;
            var piclist = "";
            var last = "";
            $.each(dataObj.Original, function(idx, item) {

                if (item.State != 0) {
                    if (idx == 0) {
                        if (item.State == 1) {

                            var url = "";
                            var contentUrl = "";

                            //判断自己或是他人
                            if (userid == item.userid) {
                                url = "/u";

                                contentUrl = "cont_" + item.spreadid;

                            } else {

                                url = "/u_" + item.userid;
                                contentUrl = "tcont_" + item.userid + "_" + item.spreadid;

                            }

                            original += "<div class=\"Hr_10\"></div>";
                            original += "<div class=\"Round1\">";
                            original += "<div class=\"Round2\">";
                            original += "<div class=\"Round3\" style=\"width:450px\"> <p class=\"F12 G6 L22 Centent\"><a href=\"" + url + "\" id=\"inner"
                            + is_id + "\" onMouseOver=\"UserInfoShowOver(this," + item.userid + "," + is_id + ")\" class=\"Blue Fa\">@" + item.nickname + "</a>";

                            original += ShowVerifyImg(item.memberlevel);

                            var content = unescape(item.content);

                            if (keyword != "") {
                                content = content.replace(keyword, "<font color=\"red\">" + keyword + "</font>");
                            }

                            original += "：" + content + "</p>";
                            haspic = item.haspic;
                            last = "<div class=\"L30\">";
                            last += "<a href=\"" + contentUrl + "\" class=\"Blue\">" + item.intime + "</a>&nbsp;&nbsp;";
                            last += "<span class=\"G9\">来自<a class=\"Fa\" href=\"" + item.sourceurl + "\" onclick=\"return LoginDiv(16);\">" + item.source + "</a></span> &nbsp; ";
                            last += "<a href=\"" + contentUrl + "_0\" class=\"Blue\">转发<span class=\"Fa\">(" + item.spreadnum + ")</span></a>";
                            last += " <a class=\"Blue\" href=\"" + contentUrl + "_1\">评论<span class=\"Fa\">(" + item.commentnum + ")</span></a></div>";
                            last += "</div>";
                            last += "</div>";
                            last += "<span class=\"Jiao\">◆</span>";
                            last += "</div><Div class=\"Hr_10\"><Div class=\"Hr_20\">";
                        }
                    }
                    else {
                        if (haspic) {
                            if (idx == 1) {
                                piclist += "<div class=\"Hr_10\"></div>";
                                piclist += "<ul class=\"imgList\" id=\"image_area_2_" + io_id + "\">";
                                piclist += "<li><a href=\"/images/Sourse/" + item.picname.substring(0, 8) + "/" + item.picname + "\" class=\"artZoomAll\" rel=\"/images/Middle/" + item.picname.substring(0, 8) + "/" + item.picname + "\" rev=\"2_" + io_id + "\">";
                                piclist += " <img src=\"/images/Little/" + item.picname.substring(0, 8) + "/" + item.picname + "\" /></a>";
                                piclist += "</li>";
                            }
                            else {
                                piclist += "<li><a href=\"/images/Sourse/" + item.picname.substring(0, 8) + "/" + item.picname + "\" class=\"artZoomAll\" rel=\"/images/Middle/" + item.picname.substring(0, 8) + "/" + item.picname + "\" rev=\"2_" + io_id + "\">";
                                piclist += " <img src=\"/images/Little/" + item.picname.substring(0, 8) + "/" + item.picname + "\" /></a>";
                                piclist += "</li>";
                            }
                        }
                    }
                }
                else {
                    original += "<div class=\"Hr_10\"></div>";
                    original += "<div class=\"Round1\">";
                    original += "<div class=\"Round2\">";
                    original += "<div class=\"Round3\" style=\"width:450px\"> <p class=\"F12 G6 L22 Centent\">";
                    original += "抱歉，此微博已被原作者删除。如需帮助，请联系客服。</p>";
                    last += "</div>";
                    last += "</div>";
                    last += "<span class=\"Jiao\">◆</span>";
                    last += "</div><Div class=\"Hr_10\"><Div class=\"Hr_20\">";
                }
            });
            if (piclist != "") {

                piclist += "</ul>";
                piclist += "<div class=\"Hr_10\"></div>";
                piclist += "<Div class=\"Hr_10\"></Div>";

            }
            var originalHtml = original + piclist + last;
            if (originalHtml != "") {
                $("#divContent" + is_id).html(originalHtml);
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

//根据用户的性别得到对应的图标
function GetSex(sex) {
    var sexhtml = "";
    if (sex == "male") {
        sexhtml = "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/men.gif\" />";
    }
    else {
        sexhtml = "<img src=\"http://simg.instrument.com.cn/ilog/blue/images/women.jpg\" />";
    }
    return sexhtml;

}



//得到博文图片
//IoId：博文id
function GetPic(servicesUrl, IoId, IsId) {
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
        data: "{IoId:'" + IoId + "'}",
        //成功
        success: function(json, status) {
            var strList = "";
            //获取服务器的值        
            var dataObj = eval("(" + json.d + ")"); //转换为json对象

            //循环获取值
            $.each(dataObj.List, function(idx, item) {
                if (idx == 0) {
                    if (item.State == "0") {
                        //alert("加载异常！");
                    }
                    else if (item.State == "2") {
                        return false;   //无数据跳出
                    }
                    else    //如果成功就返回循环里面做拼接，该“return”不会跳出循环
                    {
                        return true;
                    }
                }

                //博文图片
                if (item.ip_id != undefined) {
                    //构建数据href：原图，rel：缩略图，src：原图
                    strList += "<ul class=\"imgList\" id=\"image_area_2_" + IsId + "\">";    //操作容器
                    strList += "<li><a href=\"/images/Sourse/" + item.ip_name.substring(0, 8) + "/" + item.ip_name + "\" class=\"artZoomAll\" rel=\"/images/Middle/" + item.ip_name.substring(0, 8) + "/" + item.ip_name + "\" rev=\"2_" + IoId + "\">";
                    strList += " <img src=\"/images/Little/" + item.ip_name.substring(0, 8) + "/" + item.ip_name + "\" /></a>";
                    strList += "</li>";
                    strList += " </ul>";
                }
            });
            $("#div" + IsId).html(strList); //追加回复站短

        },
        //出错调试
        error: function(result, status) {
            if (status == 'error') {
                // alert(result.responseText);
            }
        },
        complete: function() {
        }

    });


}


//复制内容到剪贴板


var clipBoard_Error="抱歉！您现在的浏览器不支持自动拷贝功能，请手动拷贝链接发给好友";
function copy(txtMsg) {
    if (window.clipboardData) {
        return (window.clipboardData.setData("Text", txtMsg));
    }
    else if (window.netscape) {
        netscape.security.PrivilegeManager.enablePrivilege('UniversalXPConnect');
        var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);
        if (!clip) return;
        var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);
        if (!trans) return;
        trans.addDataFlavor('text/unicode');
        var str = new Object();
        var len = new Object();
        var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);
        var copytext = txtMsg;
        str.data = copytext;
        trans.setTransferData("text/unicode", str, copytext.length * 2);
        var clipid = Components.interfaces.nsIClipboard;
        if (!clip) return false;
        clip.setData(trans, null, clipid.kGlobalClipboard);
        return true;
    }
    return false; 

}








