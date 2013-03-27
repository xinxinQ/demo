function submitcheck() {
    if ($("#selDegree").val() == "") {
        alert("请选择学校类型！");
        return false;
    }
    if ($("#SchoolName").val() == "") {
        alert("请输入学校名称！");
        return false;
    }
    if ($("#selYear").val() == "0") {
        alert("请选择入学年份！");
        return false;
    }
    return true;
    
}


$(function() {
    ChangeDegree();
    $("#selDegree").change(function() {
        ChangeDegree();
    });

});



//选择大学后禁止输入，并弹窗
function ChangeDegree() {
    var degreeid = $("#selDegree").val();
    if (degreeid == 1) {
        $("#SchoolName").attr("readonly", true);
        $("#SchoolName").attr("color", "gray");
        $("#SchoolName").focus(function() {
            window.parent.ChangeColledge();
        });
    }
    else {
        $("#SchoolName").attr("readonly", false);
        $("#SchoolName").attr("color", "");
        $("#SchoolName").unbind("focus");
    }

}