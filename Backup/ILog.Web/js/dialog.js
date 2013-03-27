
function ShowLoading() {
    return "<div id=loading>Loading...</div>";
}

///创建对话框的容器

function floatDiv(floattitle, wh, hg) {
    var pmwidth = wh;
    var clientHeight = document.documentElement.clientHeight ? document.documentElement.clientHeight : document.body.clientHeight;

    var windowstitle;

    if (!hg) {
        var pmheight = clientHeight * 0.9;
    } else {
        var pmheight = hg;
    }

    //pmwidthiframe=pmheight -35

    if (!floattitle) {
        windowstitle = "信息提示";
    } else {
        windowstitle = floattitle;
    }

    var html;
    html = ' <div id="messagebox_win" style="position:absolute;z-index:99999;border:1px solid #b8dcea;background-color:#fff;width:' + pmwidth + 'px;height:' + pmheight + 'px;"><div id="headDiv" style="background:#cee9f2; height:30px; line-height:30px; padding-left:10px;color:#444;"><h1 style=\"background:#cee9f2; height:30px; line-height:30px; padding-left:10px;color:#444;\"><span onclick="hideDiv(\'messagebox_win\');"><img style=\"float:right;border:0;margin:5px 10px 0 0\" src="http://simg.instrument.com.cn/ilog/blue/images/Win_x.gif" alt="关闭"  /></span>' + windowstitle + '</h1>' + +'</div><div id="messagebox_body" style="padding:20px 20px 0 20px;"></div></div>';

    $(document.body).append(html);
    SetEnabledStyle();
    floatDivsetMenuPosition('messagebox_win', clientHeight, pmheight);

    window.onscroll = function() {
        floatDivsetMenuPosition('messagebox_win', clientHeight, pmheight);
    };
    DivMovePosition('headDiv', 'messagebox_win');
}

//对话框的定位
function floatDivsetMenuPosition(floatdivID, cheight, pheight) {
    var div_obj = $('#' + floatdivID);
    var windowWidth = document.documentElement.clientWidth;
    var scrollTop = document.body.scrollTop ? document.body.scrollTop : document.documentElement.scrollTop;
    // var windowHeight = document.documentElement.clientHeight;       
    var popupHeight = div_obj.height();
    var popupWidth = div_obj.width();

    div_obj.css({ "position": "absolute",
        'left': windowWidth / 2 - popupWidth / 2,
        'top': (cheight - pheight) / 2 + scrollTop
    }).show();
}

//关闭对话框
function hideDiv(div_id) {
    //$('#mask').remove();   
    $("#" + div_id).remove();
    //showAllSelect();
}


//设置显示时遮罩样式
function SetEnabledStyle() {
    var css;
    css = { width: $('body').width() + "px", height: $('body').height() + "px", left: '0px', top: '0px' }
    GetOpacity(css);
    $("#mask").css(css);
}

//设置透明式样
function GetOpacity(css) {
    if (window.navigator.userAgent.indexOf('MSIE') >= 1) {
        css.filter = 'progid:DXImageTransform.Microsoft.Alpha(opacity=30)';
    } else {
        css.opacity = '0.3';
    }
}

//移动div
function DivMovePosition(headDivID, moveid) {
    $("#" + headDivID).mousedown(
     function(event) {
         var offset = $("#" + moveid).offset();
         x1 = event.clientX - offset.left;
         y1 = event.clientY - offset.top;
         var witchButton = false;
         if (document.all && event.button == 1) { witchButton = true; }
         else { if (event.button == 0) witchButton = true; }
         if (witchButton)//按左键,FF是0，IE是1
         {
             $(document).mousemove(function(event) {
                 $("#" + moveid).css("left", (event.clientX - x1) + "px");
                 $("#" + moveid).css("top", (event.clientY - y1) + "px");
             })
         }
     })
    $("#" + headDivID).mouseup(
     function(event) {
         $(document).unbind("mousemove");
     })
}

///登陆对话框
function LoginDiv(LoginSource) {
    //hideAllSelect();	
    var Logintable;
    Logintable = '';

    Logintable = '<div class="ULOpenArea" id="login_win"><div class="ULP"><div class="ULL" style="font-family:黑体; font-size:16px; height:35px; line-height:35px; padding-left:15px;">欢迎登录仪器信息网</div><div class="ULR"><a class="ULmenu1" title="关闭" href="javascript:void(0);" onclick="hideDivlogin(\'login_win\')">关闭</a></div><div class="ULHr_2"></div><div  style="padding-left:15px;"><form id="first_login" name="first_login" method="post" action="http://www.instrument.com.cn/VipLog_check.asp" onSubmit="return chkLogin();" style="padding:0; margin:0"><div class="ULHr_10"></div><div class="ULL" style="width:40px; font-size:12px;line-height:26px;">账号：</div><div class="ULL" style="width:225px;"><input class="ULLoginArea" type="text" name="username" id="username" style="color:#999999; margin-top:0px;" value="邮箱/用户名/手机/展位号" onfocus="this.value=\'\';"/></div><div class="ULHr_10"></div><div class=" ULL" style="width:40px;font-size:12px; line-height:26px;">密码：</div><div class="ULL" style="width:225px;"><input class="ULLoginArea" type="password" name="password" id="password" style="color:#999999; margin-top:0px;"/></div><div class="ULHr_10"></div><div class="ULL" style="width:40px;line-height:26px;">&nbsp;</div><div class="ULL" style="width:225px;font-size:12px;"><span><INPUT id="ISmemorize" type="checkbox" CHECKED value="1" name="ISmemorize"> </span><span>记住我的登录状态</span></div><div class="ULHr_10"></div><div class="ULL" style="width:40px; line-height:26px;">&nbsp;</div><div class="ULL" style="width:225px;"><div class="ULL" style="padding-right:10px;"><input name="strURL" type="hidden" id="strURL" value="' + window.location.href + '" /><input name="LoginSource" type="hidden" id="LoginSource" value="' + LoginSource + '" /><input name="LoginInit" type="hidden" id="LoginInit" value="1" /><input  type="image" name="button" src="http://simg.instrument.com.cn/vip/20111020/images/iconb.gif" id="button" value="提交" alt="登录 " /></div><div class="ULL" style="line-height:28px;"><a class="ULBlue" href="http://www.instrument.com.cn/vip/getpass.htm" target="_blank">忘记密码</a></div></div></form><div class="ULHr_10"></div></div><div class="ULHr_2"></div><div class="ULHr_10"></div><div><div class="ULL" style="padding-left:50px; line-height:25px;">还没有账号？</div><div class="ULR"><a class="ULmenu3" title="注册" href="http://www.instrument.com.cn/vip/Register01.asp?registerSource=' + LoginSource + '" target="_blank">注册</a></div></div><div class="ULHr_10"></div><div class="ULCl"></div></div></div>';



    var Lenabled = '<div id="mask" style="background-color: lightgrey;width:100%;height:100%;position:absolute;z-index:99998;"></div>';

    $(document.body).append(Lenabled);
    $(document.body).append(Logintable);
    SetEnabledStyle();
    var lpmwidth = 360;
    var lpmheight = 235
    var clientHeight = document.documentElement.clientHeight ? document.documentElement.clientHeight : document.body.clientHeight;
    floatDivsetMenuPosition('login_win', clientHeight, lpmheight);
    window.onscroll = function() {
        floatDivsetMenuPosition('login_win', clientHeight, lpmheight);
    };
    DivMovePosition('headDiv', 'login_win');
} 

function getcoded() {
    document.getElementById("MzImgExpPwd").src = 'http://www.instrument.com.cn/ValidatImg.aspx?temp=' + Math.random();

}

//ajax function
// ajurl 请求响应的路径 showID显示结果页面必须的参数，waitID 等待加载的显示ID,postquerystr  查询的字符串

function getajax(ajurl, showID, waitID) {

    if (!ajurl) $('#' + showID).html('加载失败... 请刷新页面重试');
    if (!showID) { alert("error"); return false; }
    if (!waitID) waitID = showID;

    $.ajax({
        url: ajurl,
        cache: false,
        beforeSend: function(XMLHttpRequest) {
            Loadingstr = ShowLoading();
            $('#' + waitID).html(Loadingstr);
        },
        success: function(html) {
            $('#' + showID).html(html);
        },
        complete: function() {
            $('#loading').remove();
        },
        error: function() {
            $('#' + waitID).html('加载失败...');
        }
    });
}

function hideAllSelect() {
    //隐藏select
    var selects = document.getElementsByTagName("Select");
    for (var i = 0; i < selects.length; i++) {
        selects[i].style.display = "none";
    }

    //隐藏falsh
    var objs = document.getElementsByTagName("OBJECT");
    for (i = 0; i < objs.length; i++) {
        if (objs[i].style.visibility != 'hidden') {
            objs[i].setAttribute("oldvisibility", objs[i].style.visibility);
            objs[i].style.visibility = 'hidden';
        }
    }

    //隐藏falsh
    var objs = document.getElementsByTagName("embed");
    for (i = 0; i < objs.length; i++) {
        if (objs[i].style.visibility != 'hidden') {
            objs[i].setAttribute("oldvisibility", objs[i].style.visibility);
            objs[i].style.visibility = 'hidden';
        }
    }
}

function showAllSelect() {
    //显示select
    var selects = document.getElementsByTagName("Select");
    for (var i = 0; i < selects.length; i++) {
        selects[i].style.display = "block";
    }

    //显示falsh	
    var objs = document.getElementsByTagName("OBJECT");
    for (i = 0; i < objs.length; i++) {
        if (objs[i].attributes['oldvisibility']) {
            objs[i].style.visibility = objs[i].attributes['oldvisibility'].nodeValue;
            objs[i].removeAttribute('oldvisibility');
        }
    }

    var objs = document.getElementsByTagName("embed");
    for (i = 0; i < objs.length; i++) {
        if (objs[i].attributes['oldvisibility']) {
            objs[i].style.visibility = objs[i].attributes['oldvisibility'].nodeValue;
            objs[i].removeAttribute('oldvisibility');
        }
    }
}

//验证表单
function chkLogin() {
    if (document.first_login.username.value == "") {
        alert("请输入帐号!");
        document.first_login.username.focus();
        return false;
    }
    if (document.first.username.value == "邮箱/用户名/手机/展位号") {
        alert("请输入帐号!");
        document.first.username.focus();
        return false;
    }

    if (document.first_login.password.value == "") {
        alert("请输入密码!");
        document.first_login.password.focus();
        return false;
    }

}


function othreboard(otherurl, othretitle, dw, wh) {
    floatDiv(othretitle, dw, wh);
    getajax(otherurl, 'messagebox_body');
    //hideAllSelect();
}

///创建loading对话框的容器

function LoadingDiv() {
    hideAllSelect();
    var Lpmwidth = 250;
    var Lpmheight = 80;
    var LclientHeight = document.documentElement.clientHeight ? document.documentElement.clientHeight : document.body.clientHeight;

    var html;

    html = '<div id="loadingbox_win" style="width:250px; height:80px; border:#09c solid 5px;background-color:#FFFFFF; line-height:80px; text-align:center; color:#1388DC">请耐心等待，正在处理....</div>';

    var Lenabled = '<div id="mask" style="background-color: lightgrey;width:100%;height:100%;position:absolute;z-index:99998;"></div>';

    $(document.body).append(Lenabled);
    $(document.body).append(html);

    SetEnabledStyle();
    floatDivsetMenuPosition('loadingbox_win', LclientHeight, Lpmheight);
}



(function($) {
    $.fn.extend({
        //主函数
        toggleLoading: function(options) {
            var crust = this.children(".x-loading-wanghe");
            if (crust.length > 0) {
                if (crust.is(":visible")) {
                    crust.fadeOut(500);
                } else {
                    crust.fadeIn(500);
                }
                return this;
            }
            // 扩展参数
            var op = $.extend({
                z: 9999,
                width: 0,
                height: 0,
                borderColor: '#6bc4f5',
                opacity: 0.5
            }, options);
            var thisjQuery = this;
            if (thisjQuery.css("position") == "static")
                thisjQuery.css("position", "relative");
            var w = thisjQuery.outerWidth(),
            	h = thisjQuery.outerHeight();

            crust = $("<div></div>").css({//外壳
                'position': 'absolute',
                'z-index': op.z,
                'display': 'none',
                'width': w + 'px',
                'height': h + 'px',
                'text-align': 'center',
                'top': '0px',
                'left': '0px',
                'font-family': 'arial',
                'font-size': '12px',
                'font-weight': '500'
            }).attr("class", "x-loading-wanghe");

            var mask = $("<div></div>").css({//蒙版
                'position': 'absolute',
                'z-index': op.z + 1,
                'width': '100%',
                'height': '100%',
                'background-color': '#333333',
                'top': '0px',
                'left': '0px',
                'opacity': op.opacity
            });
            //71abc6,89d3f8,6bc4f5
            var msgCrust = $("<span></span>").css({//消息外壳
                'position': 'relative',
                'top': (h - 30) / 2 + 'px',
                'z-index': op.z + 2,
                'height': '0px',
                'display': 'inline-block',
                'background-color': '#cadbe6',
                'padding': '2px',
                'color': '#000000',
                'border': '1px solid ' + op.borderColor,
                'text-align': 'left',
                'opacity': 0.9
            });
            var msg = $("<span>" + op.msg + "</span>").css({//消息主体
                'position': 'relative',
                'margin': '0px',
                'z-index': op.z + 3,
                'line-height': '22px',
                'height': '22px',
                'display': 'inline-block',
                'background-color': '#efefef',
                'padding-left': '25px',
                'padding-right': '5px',
                'border': '1px solid ' + op.borderColor,
                'text-align': 'left',
                'text-indent': '0'
            });


            msgCrust.prepend(msg);
            crust.prepend(mask);
            crust.prepend(msgCrust);
            thisjQuery.prepend(crust);
            // alert(thisjQuery.html());
            crust.fadeIn(500);
            //模态设置
            return this;
        }
    });
})(jQuery);