$(function() {

    //选择省份
    $("#CategoryLevel1 li").click(function() {
        $("#CategoryLevel1 li").removeClass("Selected");
        $(this).addClass("Selected");
        GetCity($(this).attr("key"));
    });

    //选择城市
    $("#CategoryLevel2 li").click(function() {
        $("#CategoryLevel2 li").removeClass("Selected");
        $(this).addClass("Selected");
    });

    //选择省份后绑定城市
    function GetCity(provinceID) {
        ajaxurl = vServiceUrl + "Vip.asmx/ILogGetCityListStr";
        $.ajax({
            type: "POST",
            url: "" + ajaxurl + "",
            cache: false,
            //预期指定服务器返回类型
            dataType: "json",
            //内容返回类型            
            contentType: "application/json;",
            data: "{provinceID:'" + provinceID + "'}",
            success: function(data, status) {
                //获取服务器的值        
                //转换为json对象
                var dataObj = eval("(" + data.d + ")");
                $("#CategoryLevel2").html("");
                //循环获取值
                $.each(dataObj.CityList, function(idx, item) {
                    if (idx != 0) {
                        $("#CategoryLevel2").append("<li key='" + item.key + "'>" + item.value + "</li>");
                    }
                });
                $("#CategoryLevel2 li").click(function() {
                    $("#CategoryLevel2 li").removeClass("Selected");
                    $(this).addClass("Selected");
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

    //设置一级类样式与获取一级类id
    $("#CategoryLevel0 li").each(function() {
        $(this).click(function() {
            $("#CategoryLevel0 li").removeClass("Selected");
            $(this).addClass("Selected");
            var countryID = $(this).attr("key");
            $("#CountryID").val(countryID);

            ajaxurl = vServiceUrl + "Vip.asmx/ILogGetProvinceListStr";
            $.ajax({
                type: "POST",
                url: "" + ajaxurl + "",
                cache: false,
                //预期指定服务器返回类型
                dataType: "json",
                //内容返回类型            
                contentType: "application/json;",
                data: "{countryID:'" + countryID + "'}",
                success: function(data, status) {
                    //获取服务器的值   
                    //转换为json对象
                    var dataObj = eval("(" + data.d + ")");
                    $("#CategoryLevel1").html("");
                    //循环获取值
                    $.each(dataObj.ProvinceList, function(idx, item) {
                        if (idx != 0) {
                            $("#CategoryLevel1").append("<li key='" + item.key + "'>" + item.value + "</li>");
                        }
                    });
                    $("#CategoryLevel1 li").click(function() {
                        $("#CategoryLevel1 li").removeClass("Selected");
                        $(this).addClass("Selected");
                        GetCity($(this).attr("key"));
                    });
                    var provid = $("#CategoryLevel1 li:first").attr("key");
                    GetCity(provid);
                }, error: function(result, status) {
                    if (status == 'error') {
                        alert(result.responseText);
                    }
                },
                complete: function() {
                }
            });

        });
    });
});

function submitcheck() {
    $("#CategoryLevel0 li").each(function() {
        if ($(this).attr("class") == "Selected") { $("#CountryID").val($(this).attr("key")); $("#CountryName").val($(this).html()); }
    });
    $("#CategoryLevel1 li").each(function() {
        if ($(this).attr("class") == "Selected") { $("#ProvinceID").val($(this).attr("key")); $("#ProvinceName").val($(this).html()); }
    });
    $("#CategoryLevel2 li").each(function() {
        if ($(this).attr("class") == "Selected") { $("#CityID").val($(this).attr("key")); $("#CityName").val($(this).html()); }
    });

    if ($("#CountryID").val() == "") { showmsg("请选择国家！"); return false; }
    if ($("#ProvinceID").val() == "") { showmsg("请选择省份！"); return false; }
    if ($("#CityID").val() == "") { showmsg("请选择城市！"); return false; }

    return true;
}

function showmsg(msg) {
    $.dialog({
        title: "提示信息",
        content: msg,
        max: false,
        min: false,
        lock: true,
        ok: function() {
            this.hide();
            return false;
        }
    });

}