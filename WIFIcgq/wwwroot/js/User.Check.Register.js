$(document).ready(function () {
    $("#username").click(function () {
        var button = $("button#submit");
        button.attr("disabled", true); 
    })
    $("#username").change(function () {
        var username = $("#username").val();
        var password = $("#password").val();
        var button = $("button#submit");
        button.attr("disabled", false); 
        $.getJSON(
            "/User/Check",
            {
                username: username,
                password: password,
            },
            function (response) {
                if (response != null) {
                alert(response);
                button.attr("disabled", true); 
                }
            }
        )
        if (username == "")
            button.attr("disabled", true); 
    });
})

