$(document).ready(function () {
    $("button#adddevice").click(function () {
        var deviceid = $("#deviceid").val();
        var warning = $("#warning");
        $.getJSON(
            "/Device/CheckDevice",
            { deviceid: deviceid },
            function (response) {
                myHTML = "";
                myHTML += response;
                warning.text(myHTML);
            }
        )
    })
})