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


setInterval("showLogin()", "500");
setInterval("showPort()", "500");