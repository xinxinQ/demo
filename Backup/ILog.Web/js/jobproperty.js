$(function() {
    //设置一级类样式与获取一级类id
    $("#CategoryLevel0 li").each(function() {
        $(this).click(function() {
            $("#CategoryLevel0 li").removeClass("Selected");
            $(this).addClass("Selected");
            var vccid = $(this).attr("key");

            ajaxurl = vServiceUrl + "Vip.asmx/ILogGetFieldListStr";
            $.ajax({
                type: "POST",
                url: "" + ajaxurl + "",
                cache: false,
                //预期指定服务器返回类型
                dataType: "json",
                //内容返回类型            
                contentType: "application/json;",
                data: "{vccid:'" + vccid + "'}",
                success: function(data, status) {
                    //获取服务器的值   
                    //转换为json对象
                    var dataObj = eval("(" + data.d + ")");
                    $("#CategoryLevel1").html("");
                    //循环获取值
                    $.each(dataObj.FieldList, function(idx, item) {
                        if (idx != 0) {

                            if (idx == 1) {
                                $("#CategoryLevel1").append("<li key='" + item.key + "' class='Selected'>" + item.value + "</li>");
                            }
                            else {
                                $("#CategoryLevel1").append("<li key='" + item.key + "'>" + item.value + "</li>");
                            }

                        }
                    });
                    $("#CategoryLevel1 li").click(function() {
                        $("#CategoryLevel1 li").removeClass("Selected");
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

            ajaxurl = vServiceUrl + "Vip.asmx/ILogGetTitleListStr";
            $.ajax({
                type: "POST",
                url: "" + ajaxurl + "",
                cache: false,
                //预期指定服务器返回类型
                dataType: "json",
                //内容返回类型            
                contentType: "application/json;",
                data: "{vccid:'" + vccid + "'}",
                success: function(data, status) {
                    //获取服务器的值        
                    //转换为json对象
                    var dataObj = eval("(" + data.d + ")");
                    $("#CategoryLevel2").html("");
                    //循环获取值
                    $.each(dataObj.TitleList, function(idx, item) {
                        if (idx != 0) {

                            if (idx == 1) {
                                $("#CategoryLevel2").append("<li key='" + item.key + "' class='Selected'>" + item.value + "</li>");
                            }
                            else {
                                $("#CategoryLevel2").append("<li key='" + item.key + "'>" + item.value + "</li>");
                            }

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
        });
    });

    $("#CategoryLevel1 li").click(function() {
        $("#CategoryLevel1 li").removeClass("Selected");
        $(this).addClass("Selected");
    });

    $("#CategoryLevel2 li").click(function() {
        $("#CategoryLevel2 li").removeClass("Selected");
        $(this).addClass("Selected");
    });

});

function submitcheck() {
    $("#CategoryLevel0 li").each(function() {
        if ($(this).attr("class") == "Selected") { $("#VCCName").val($(this).html()); $("#VCCID").val($(this).attr("key")); }
    });
    $("#CategoryLevel1 li").each(function() {
        if ($(this).attr("class") == "Selected") { $("#VCFName").val($(this).html()); $("#VCFID").val($(this).attr("key")); }
    });
    $("#CategoryLevel2 li").each(function() {
        if ($(this).attr("class") == "Selected") { $("#VCTName").val($(this).html()); $("#VCTID").val($(this).attr("key")); }
    });

    if ($("#VCCID").val() == "") {
        window.parent.showmsg("请选择单位性质！");
        return false;
    }
    if ($("#VCFID").val() == "") {
        window.parent.showmsg("请选择行业性质！");
        return false;
    }
    if ($("#VCTID").val() == "") {
        window.parent.showmsg("请选择职位！");
        return false;
    }
    return true;
}