$(document).ready(function () {
    var showdevice = $("#choosedevice");
    $.getJSON(
        "/Device/ShowDevice",
        function (response) {
            showdevice.html("");
            var myHTML = "";
            $.each(response, function (i, item) {
                myHTML += "<option value='" + item + "'>" + item + "</option>";
            })
            showdevice.append(myHTML);
        }
    )
})