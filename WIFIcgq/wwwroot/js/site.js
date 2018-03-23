// Write your JavaScript code.


$("button#send").click(function () {
    var mark = $("#mark").text();
    var timer = $("#timer");
    var adchart = $("#ADchart");
    if (mark == 0) {
        timer.text(setInterval("send()", 500));
        $("#mark").text(1);
        $("#send").html("停止");
        adchart.removeClass("hidden");
    }
    else {
        clearInterval(timer.text());
        $("#mark").text(0);
        $("#send").html("发送");
        $("#test").text("");
        adchart.addClass("hidden");
    }
})

$("button#send_da").click(function () {
    var mark = $("#mark_da").text();
    var timer = $("#timer");
    if (mark == 0) {
        timer.text(setInterval("send_da()", 500));
        $("#mark_da").text(1);
        $("#send_da").html("停止");
    }
    else {
        clearInterval(timer.text());
        $("#mark_da").text(0);
        $("#send_da").html("发送");
    }
})

$("button#send_io").click(function () {
    var mark = $("#mark_io").text();
    var timer = $("#timer");
    if (mark == 0) {
        timer.text(setInterval("send_io()", 500));
        $("#mark_io").text(1);
        $("#send_io").html("停止");
    }
    else {
        clearInterval(timer.text());
        $("#mark_io").text(0);
        $("#send_io").html("发送");
    }
})

function send() {
    var data = $("#senddata").val();
    var deviceid = $("#choosedevice").find("option:selected").text();
    $.getJSON(
        "/User/Send",
        {
            data: data,
            deviceid: deviceid,
        },
        function (response) { }
    )
}

function send_da() {
    var da1 = $("#da1").val();
    var da2 = $("#da2").val();
    var da3 = $("#da3").val();
    var deviceid = $("#choosedevice").find("option:selected").text();
    $.getJSON(
        "/User/SendDA",
        {
            da1: da1,
            da2: da2,
            da3: da3,
            deviceid: deviceid,
        },
        function (response) { }
    )
}

function send_io() {
    var port1 = $("#port1").val();
    var port2 = $("#port2").val();
    var deviceid = $("#choosedevice").find("option:selected").text();
    $.getJSON(
        "/User/SendIO",
        {
            port1: port1,
            port2: port2,
            deviceid: deviceid,
        },
        function (response) { }
    )
}

var loginArr = [];
$.getJSON(
    "/User/LogNav",
    function (response) {
        if (response) {
            loginArr.push('<div class="dropdown"><a class="dropdown-toggle" id= "dropdownMenu1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" >'
                + response +
                '<span class="caret"></span></a ><ul class="dropdown-menu"><li><a href="/User/EditPassword">修改密码</a></li></ul></div>');
            loginArr.push('<li><a href="/User/Logout">退出登录</a></li>');
        }
        else {
            loginArr.push('<li><a href="/User/Index">登录</a></li>');
        }
        $("#ul_login").html(loginArr.join(''));
    }
)