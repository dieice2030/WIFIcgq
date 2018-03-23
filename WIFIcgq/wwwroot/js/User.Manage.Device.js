$(document).ready(function modelOption() {
    var mark0 = $("#mark0");
    var mark1 = $("#mark1");
    var mark2 = $("#mark2");
    var admodel = $("#admodel");
    var damodel = $("#damodel");
    var iomodel = $("#iomodel");
    var noadmodel = $("#noadmodel");
    var nodamodel = $("#nodamodel");
    var noiomodel = $("#noiomodel");
    if (mark0.text() == 0) {
        admodel.addClass("hidden");
        noadmodel.removeClass("hidden");
    }
    else {
        noadmodel.addClass("hidden");
        admodel.removeClass("hidden");
    };
    if (mark1.text() == 0) {
        damodel.addClass("hidden");
        nodamodel.removeClass("hidden");
    }
    else {
        nodamodel.addClass("hidden");
        damodel.removeClass("hidden");
    };
    if (mark2.text() == 0) {
        iomodel.addClass("hidden");
        noiomodel.removeClass("hidden");
    }
    else {
        noiomodel.addClass("hidden");
        iomodel.removeClass("hidden");
    };

    $("button#del").click(function () {
        var delad = $("#delad").prop('checked');
        var delda = $("#delda").prop('checked');
        var delio = $("#delio").prop('checked');
        $.getJSON(
            "/Device/DelModel",
            {
                delad: delad,
                delda: delda,
                delio: delio,
                mark0: mark0.text(),
                mark1: mark1.text(),
                mark2: mark2.text(),
            },
            function (response) {
                mark0.text(response[0]);
                mark1.text(response[1]);
                mark2.text(response[2]);
            }
        )
        if (mark0.text() == 0) {
            admodel.addClass("hidden");
            noadmodel.removeClass("hidden");
        }
        else {
            noadmodel.addClass("hidden");
            admodel.removeClass("hidden");
        };
        if (mark1.text() == 0) {
            damodel.addClass("hidden");
            nodamodel.removeClass("hidden");
        }
        else {
            nodamodel.addClass("hidden");
            damodel.removeClass("hidden");
        };
        if (mark2.text() == 0) {
            iomodel.addClass("hidden");
            noiomodel.removeClass("hidden");
        }
        else {
            noiomodel.addClass("hidden");
            iomodel.removeClass("hidden");
        };
    })

    $("button#add").click(function () {
        var addad = $("#addad").prop('checked');
        var addda = $("#addda").prop('checked');
        var addio = $("#addio").prop('checked');
        $.getJSON(
            "/Device/AddModel",
            {
                addad: addad,
                addda: addda,
                addio: addio,
                mark0: mark0.text(),
                mark1: mark1.text(),
                mark2: mark2.text(),
            },
            function(response) {
                mark0.text(response[0]);
                mark1.text(response[1]);
                mark2.text(response[2]);
            }
        )
        if (mark0.text() == 0) {
            admodel.addClass("hidden");
            noadmodel.removeClass("hidden");
        }
        else {
            noadmodel.addClass("hidden");
            admodel.removeClass("hidden");
        };
        if (mark1.text() == 0) {
            damodel.addClass("hidden");
            nodamodel.removeClass("hidden");
        }
        else {
            nodamodel.addClass("hidden");
            damodel.removeClass("hidden");
        };
        if (mark2.text() == 0) {
            iomodel.addClass("hidden");
            noiomodel.removeClass("hidden");
        }
        else {
            noiomodel.addClass("hidden");
            iomodel.removeClass("hidden");
        };
    })
})



