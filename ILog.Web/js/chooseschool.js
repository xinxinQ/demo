$(function() {
    $("#selProvince").change(function() {
        var provid = $(this).val();
        ChangeSchool(provid);
    });
    var provid = $("#selProvince").val();
    ChangeSchool(provid);

    $("#CategorySelector0 li").each(function() {
        $(this).mouseover(function() {
            $(this).removeClass().addClass("Selected");
        }).mouseout(function() {
            $(this).removeClass();
        });
    });

});

//关闭选择学校弹窗
function CloseChooseSchool(id,obj) {
    var schoolName = $(obj).html();
    window.parent.ShowColledge(id,schoolName);
    
}

//选择学校
function ChangeSchool(provid) {
    ajaxurl = vServiceUrl + "IlogSchool.asmx/ILogSchoolGetListByProvID";
    $.ajax({
        type: "POST",
        url: "" + ajaxurl + "",
        cache: false,
        //预期指定服务器返回类型
        dataType: "json",
        //内容返回类型            
        contentType: "application/json;",
        data: "{provid:'" + provid + "'}",
        success: function(data, status) {
            //获取服务器的值   
            //转换为json对象
            var dataObj = eval("(" + data.d + ")");
            $("#CategorySelector0").html("");
            //循环获取值
            $.each(dataObj.SchoolList, function(idx, item) {
                if (idx != 0) {
                    $("#CategorySelector0").append("<li onclick=\"javascript:CloseChooseSchool(" + item.id + ",this)\" >" + item.schoolname + "</li>");
                }
            });
            $("#CategorySelector0 li").each(function() {
                $(this).mouseover(function() {
                    $(this).removeClass().addClass("Selected");
                }).mouseout(function() {
                    $(this).removeClass();
                });
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