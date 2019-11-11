
function AddPageMessage(msg, type) {
    if (!type) type = "primary";
    var e = $("#pagemessages");
    e.append(GenerateAlertMessage(msg, type));
    $("html, body").animate({
        scrollTop: e.offset().top - 56
    }, 1000);
}
function GenerateAlertMessage(msg, type) {
    if (!type) type = "primary";
    return "<div class=\"form-group alert-container\"><div class=\"alert alert-" + type + " alert-dismissible fade show\" role=\"alert\">" +
        msg +
        "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\">" +
        "<span aria-hidden=\"true\">&times;</span></button ></div ></div>";
}

var benhof_isloading = false;
$(document).ready(function () {
    $.ajaxSetup({
        cache: false
    });

    $('.noclipboard').on("cut copy paste", function (e) {
        e.preventDefault();
    });

    $("#loadingspinner").hide();
}).bind("ajaxStart", function () {
    $("#loadingspinner").show();
}).bind("ajaxStop", function () {
    if (!benhof_isloading) {
        $("#loadingspinner").hide();
    }
});
