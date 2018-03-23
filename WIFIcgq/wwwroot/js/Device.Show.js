$(document).ready(function () {
    var showdevice = $("#showdevice");
    $.getJSON(
        "/Device/ShowDevice",
        function (response) {
            showdevice.html("");
            var myHTML = "";
            $.each(response, function (i, item) {
                myHTML += "<tr><td>" + item + "<td><a href='../Device/DelDevice\?id="+item+"'>删除</a></td></tr>";
            })
            showdevice.append(myHTML);
        }
    )

    $("#refresh").click(function () {
        var showdevice = $("#showdevice");
        var warning = $("#warning");
        $.getJSON(
            "/Device/ShowDevice",
            function (response) {
                showdevice.html("");
                warning.text("");
                var myHTML = "";
                $.each(response, function (i, item) {
                    myHTML += "<tr><td>" + item + "<td><a href='../Device/DelDevice\?id=" + item + "'>删除</a></td></tr>";
                })
                showdevice.append(myHTML);
            }
        )
    })
})