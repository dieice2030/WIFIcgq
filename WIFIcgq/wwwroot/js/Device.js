$(document).ready(function modelOption() {
    var mark0 = $("#mark0");
    var mark1 = $("#mark1");
    var mark2 = $("#mark2");
    var partAD = $("#partAD");
    var partDA = $("#partDA");
    var partIO = $("#partIO");
    var btn_ad = $("#btn_ad");
    var btn_da = $("#btn_da");
    var btn_io = $("#btn_io");
    if (mark0.text() == 0) {
        btn_ad.addClass("hidden");
    }
    else {
        btn_ad.removeClass("hidden");
    };
    if (mark1.text() == 0) {
        btn_da.addClass("hidden");
    }
    else {
        btn_da.removeClass("hidden");
    };
    if (mark2.text() == 0) {

        btn_io.addClass("hidden");
    }
    else {
        btn_io.removeClass("hidden");
    };

    $("#showmodel").change(function () {
        var a = $("#btn_ad").children().prop('checked');
        var b = $("#btn_da").children().prop('checked');
        var c = $("#btn_io").children().prop('checked');
        if (a == true) {
            partAD.removeClass("hidden");
            partDA.addClass("hidden");
            partIO.addClass("hidden");
        }
        if (b == true) {
            partDA.removeClass("hidden");
            partAD.addClass("hidden");
            partIO.addClass("hidden");
        }
        if (c == true) {
            partIO.removeClass("hidden");
            partAD.addClass("hidden");
            partDA.addClass("hidden");
        }
    })
})