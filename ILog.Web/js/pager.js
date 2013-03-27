
//分页控件（加载页面）
function sowhpage_ul(PageCurrent, RecordCount, ation, keyword, PageSize, fnname) {
    var sowhpage_div = $("#sowhpage_div");

    var strShowPage = "";

    //当然页码等于总也数就隐藏下一页按钮
    strShowPage += "<span class=\"R\" " + (PageCurrent == RecordCount ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"nextbtn\" ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_page.gif\" onclick=\"nextpage(" + PageCurrent + "," + RecordCount + "," + ation + "," + PageSize + ",'" + fnname + "')\" alt=\"下一页\" /></span>";


    strShowPage += "<span class=\"R span\" style=\"position:relative\" id=\"selOption\" ><a href=\"javascript:void(0);\"  class=\"Blue\">第" + PageCurrent + "页</a><img src=\"http://simg.instrument.com.cn/ilog/blue/images/SanJ.gif\" />";

    strShowPage += "<ul class=\"pageBox R Line Fa BrWh\" id=\"selOption_menu\" style=\" position:absolute; bottom:17px; left:15px;display:none;\">";

    for (var i = RecordCount; i >= 1; i--) {

        if (PageCurrent == i) {
            strShowPage += "<li id=\"selpage_" + i + "\" ><a href=\"javascript:void(0);\" onclick=\"if (!LoginDiv(16)) { return false;} eval("+fnname + "(" + i + "," + PageSize + "," + ation + "));\" >第<font color=\"red\">" + i + "</font>页</a></li>";
        }
        else {
            strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"if (!LoginDiv(16)){ return false;} eval(" + fnname + "(" + i + "," + PageSize + "," + ation + "));\" >第" + i + "页</a></li>";
        }

    }

    strShowPage += "</ul>";
    strShowPage += "</span>";


    //当前页码小于1就隐藏上一页页码
    strShowPage += "<span class=\"R\" " + (PageCurrent <= 1 ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"upbtn\"  ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_pageT.gif\" onclick=\"uppage(" + PageCurrent + "," + RecordCount + "," + ation + "," + PageSize + ",'" + fnname + "')\" alt=\"上一页\" /></span>";


    strShowPage += "<div class=\"Hr_20\"></div>";

    sowhpage_div.html(strShowPage);

    //动态创建事件
    MousePage();

    return strShowPage;
}

//记录当前页码
var pageindex = 1;

//记录当前页码
var pageindex_n = 2;


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

//下一页处理
//PageCurrent：当前页码
//RecordCount：总页数
//PageSize:每页多少条记录
//操作类型：3是搜索0和1是默认加载的数据类型
function nextpage(PageCurrent, RecordCount, ation, PageSize, fnname) {

    if (!LoginDiv(16)) {
        return;
    }

    var RecordCount_index = RecordCount;    //记录总页数

    //没翻过上一页
    if (pageindex_n <= 2) {
        if (RecordCount >= 1) {
            pageindex++;
            eval(fnname + "(" + pageindex + "," + PageSize + "," + ation + ")");
        }
    }
    else {
        if (RecordCount >= 1) {
            pageindex_n++;
            eval(fnname + "(" + pageindex_n + "," + PageSize + "," + ation + ")");
        }
    }

    //如果到最大页码就隐藏该按钮（去掉第一页和最后一页）
    if (RecordCount_index == pageindex) {
        pageindex = 1;  //索引初始化
    }
}

//上一页处理
//PageCurrent：当前页码
//RecordCount：总页数
//操作类型：3是搜索0和1是默认加载的数据类型
function uppage(PageCurrent, RecordCount, ation, PageSize, fnname) {

    if (!LoginDiv(16)) {
        return;
    }

    var pageindex_n = PageCurrent;    //记录总页数

    if (pageindex == 1) {
        if (PageCurrent <= RecordCount) {
            pageindex_n--;

            eval(fnname + "(" + pageindex_n + "," + PageSize + "," + ation + ")");
        }
    }
    else {
        if (PageCurrent <= RecordCount) {
            pageindex--;

            //搜索操作
            eval(fnname + "(" + pageindex + "," + PageSize + "," + ation + ")");
        }
    }

    //如果到第一页了就隐藏上一页
    if (pageindex_n == 1) {
        pageindex_n = 2;    //索引初始化
    }
}

//分页控件，无页签切换（加载页面）
function sowhpage_ul_one(PageCurrent, RecordCount, keyword, PageSize, fnname) {
    var sowhpage_div = $("#sowhpage_div");

    var strShowPage = "";

    //当然页码等于总也数就隐藏下一页按钮
    strShowPage += "<span class=\"R\" " + (PageCurrent == RecordCount ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"nextbtn\" ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_page.gif\" onclick=\"nextpage_one(" + PageCurrent + "," + RecordCount + "," + PageSize + ",'" + fnname + "')\" alt=\"下一页\" /></span>";


    strShowPage += "<span class=\"R span\" style=\"position:relative\" id=\"selOption\" ><a href=\"javascript:void(0);\"  class=\"Blue\">第" + PageCurrent + "页</a><img src=\"http://simg.instrument.com.cn/ilog/blue/images/SanJ.gif\" />";

    strShowPage += "<ul class=\"pageBox R Line Fa BrWh\" id=\"selOption_menu\" style=\" position:absolute; bottom:17px; left:15px;display:none;\">";

    for (var i = RecordCount; i >= 1; i--) {

        if (PageCurrent == i) {
            strShowPage += "<li id=\"selpage_" + i + "\" >";
            strShowPage += "<input id=\"hidCurrentPage\" type=\"hidden\" value=\""+i+"\" />";
            strShowPage += "<a href=\"javascript:void(0);\" onclick=\"if (!LoginDiv(16)) { return false;} eval(" + fnname + "(" + i + "," + PageSize + "));\" >第<font color=\"red\">" + i + "</font>页</a></li>";
        }
        else {
            strShowPage += "<li id=\"selpage_" + i + "\"  ><a href=\"javascript:void(0);\" onclick=\"if (!LoginDiv(16)){ return false;} eval(" + fnname + "(" + i + "," + PageSize + "));\" >第" + i + "页</a></li>";
        }

    }

    strShowPage += "</ul>";
    strShowPage += "</span>";


    //当前页码小于1就隐藏上一页页码
    strShowPage += "<span class=\"R\" " + (PageCurrent <= 1 ? "style=\"display:none;\"" : "style=\"display: ;\"") + " id=\"upbtn\"  ><input type=\"image\" src=\"http://simg.instrument.com.cn/ilog/blue/images/btn_pageT.gif\" onclick=\"uppage_one(" + PageCurrent + "," + RecordCount + "," + PageSize + ",'" + fnname + "')\" alt=\"上一页\" /></span>";


    strShowPage += "<div class=\"Hr_20\"></div>";

    sowhpage_div.html(strShowPage);

    //动态创建事件
    MousePage();

    return strShowPage;

}

//下一页处理
//PageCurrent：当前页码
//RecordCount：总页数
//PageSize:每页多少条记录
//操作类型：3是搜索0和1是默认加载的数据类型
function nextpage_one(PageCurrent, RecordCount, PageSize, fnname) {

    if (!LoginDiv(16)) {
        return;
    }

    var RecordCount_index = RecordCount;    //记录总页数

    //没翻过上一页
    if (pageindex_n <= 2) {
        if (RecordCount >= 1) {
            pageindex++;
            eval(fnname + "(" + pageindex + "," + PageSize + ")");
        }
    }
    else {
        if (RecordCount >= 1) {
            pageindex_n++;
            eval(fnname + "(" + pageindex_n + "," + PageSize + ")");
        }
    }

    //如果到最大页码就隐藏该按钮（去掉第一页和最后一页）
    if (RecordCount_index == pageindex) {
        pageindex = 1;  //索引初始化
    }
}



//上一页处理
//PageCurrent：当前页码
//RecordCount：总页数
//操作类型：3是搜索0和1是默认加载的数据类型
function uppage_one(PageCurrent, RecordCount, PageSize, fnname) {

    if (!LoginDiv(16)) {
        return;
    }

    var pageindex_n = PageCurrent;    //记录总页数

    if (pageindex == 1) {
        if (PageCurrent <= RecordCount) {
            pageindex_n--;

            eval(fnname + "(" + pageindex_n + "," + PageSize + ")");
        }
    }
    else {
        if (PageCurrent <= RecordCount) {
            pageindex--;

            //搜索操作
            eval(fnname + "(" + pageindex + "," + PageSize + ")");
        }
    }

    //如果到第一页了就隐藏上一也
    if (pageindex_n == 1) {
        pageindex_n = 2;    //索引初始化
    }
}
