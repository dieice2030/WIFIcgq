

//function showLogin() {
//    var text = $("#test");
//    var oCount = $("#oCount").text();
//    $(document).ready(function () {
//    $.getJSON(
//        "/Home/Test",
//        function (response) {
//            if (response != null) {
//                var myHTML = "";
//                myHTML += "<li>" + response + "</li>";
//                text.append(myHTML);
//            }
//        }
//    )
//    })
//}
function showLogin() {
    var text = $("#test");
    var oCount = $("#oCount").text();
    var templatead = $("#templatead");
    $(document).ready(function () {
        $.getJSON(
            "/Home/Test",
            function (response) {
                templatead.text("实时电压:");
                if (response != null) {
                    var myHTML = "";
                    myHTML += response;
                    text.text(myHTML);
                    templatead.append(myHTML);
                    templatead.append("V");
                }
            }
        )
    })
}

function showPort() {
    var text1 = $("#showport1");
    var text2 = $("#showport2");
    var oCountAD = $("#oCount").text();
    $(document).ready(function () {
        $.getJSON(
            "/Home/ShowPort",
            function (response) {
                if (response != null) {
                    text1.text(response[0]);
                    text2.text(response[1]);
                }
            }
        )
    })
}
$(document).ready(function () {

    // 基于准备好的dom，初始化echarts实例
    var myChart = echarts.init(document.getElementById('ADchart'));

    // 指定图表的配置项和数据
    var base = new Date();
    var date = [];
    var data = [];
    var now = new Date(base);
    var text = $("#test").text();
    var echarttimer = $("#echarttimer");

    function addData(shift) {
        var text = $("#test").text();
        now = [now.getHours(), now.getMinutes(), now.getSeconds()].join(':');
        date.push(now);
        data.push(text);

        if (shift) {
            date.shift();
            data.shift();
        }

        now = new Date();
    }

    for (var i = 1; i < 100; i++) {
        addData();
    }

    option = {
        xAxis: {
            type: 'category',
            boundaryGap: false,
        },
        yAxis: {
            boundaryGap: [0, '50%'],
            type: 'value'
        },
        series: [
            {
                name: '成交',
                type: 'line',
                smooth: true,
                symbol: 'none',
                type: 'line',
            }
        ]
    };

    myChart.setOption(option);

    $("#intervalbutton").click(function () {
        var annothertimer = $("#annothertimer").text();
        var sendinterval = $("#sendinterval").val();
        var recieveinterval = $("#recieveinterval").val();
        var sendunit = $("#sendunit").val();
        var recieveunit = $("#recieveunit").val();
        var send;
        var rec;
        switch (sendunit) {
            case "秒":
                send = sendinterval * 1000;
                break;
            case "分":
                send = sendinterval * 60 * 1000;
                break;
            case "时":
                send = sendinterval * 60 * 60 * 1000;
                break;
            case "天":
                send = sendinterval * 24 * 60 * 60 * 1000;
                break;
            default:
                send = sendinterval * 500;
        }
        switch (recieveunit) {
            case "秒":
                rec = recieveinterval * 1000;
                break;
            case "分":
                rec = recieveinterval * 60 * 1000;
                break;
            case "时":
                rec = recieveinterval * 60 * 60 * 1000;
                break;
            case "天":
                rec = recieveinterval * 24 * 60 * 60 * 1000;
                break;
            default:
                rec = recieveinterval * 500;
        }
        clearInterval(annothertimer);
        time = setInterval("showLogin()", send);
        $("#annothertimer").text(time);
        setInterval("showPort()", send);



        clearInterval(echarttimer.text());
        etime = setInterval(function () {
            addData(true);
            myChart.setOption({
                xAxis: {
                    data: date
                },
                series: [{
                    name: '成交',
                    data: data
                }]
            });
        }, send);
    echarttimer.text(etime);

        alert("更改成功");


    });

    $("#stopinterval").click(function () {
        var annothertimer = $("#annothertimer").text();
        clearInterval(annothertimer);
        alert("暂停成功");
    });


})



