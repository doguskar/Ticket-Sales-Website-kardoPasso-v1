/*$(document).ready(function () {
    $.get("http://ipinfo.io", function (response) {
        $("#userInfo").data("info", response); //city,country,hostname,ip,loc,org,region
    }, "jsonp");
    setTimeout(function () { sessionControl() }, 500);
});*/

function sessionControl() {
    $.ajax({
        type: "POST",
        url: "/Default/sessionsControl",
        success: function (i) {
            if (i == "False") {
                if (getCookieInfo()) {
                    $.ajax({
                        type: "POST",
                        data: {
                            ipAddress: getIpAddress(),
                            country: getCountry(),
                            city: getCity(),
                            loc: getLoc(),
                            hostname: getHostname(),
                            language: getLanguage(),
                            nowDate: getNowDate()
                        },
                        url: "Kardo/creatNewVisitor",
                        success: function (i) {
                            if (i == "True") {
                                window.location.reload();
                            }
                        },
                        error: function () { alert("creatVisitor - Hata!") }
                    })
                } else { 
                    //notification for cookie
                }
            }
        },
        error: function () { alert("sessionsControl - Hata!") }
    })
}


/**/

function getIpAddress() { return "";return $("#userInfo").data("info").ip; }
function getCity() { return "";return $("#userInfo").data("info").city; }
function getCountry() { return "";return $("#userInfo").data("info").country; }
function getHostname() { return "";return $("#userInfo").data("info").hostname; }
function getLoc() { return "";return $("#userInfo").data("info").loc; }
function getNowDate() {
    var now = new Date();
    var GMT = (now.toString().split(" "))[5];
    var nowf = now.getFullYear() + "-" + now.getMonth() + "-" + now.getDate() + " " + now.getHours() + ":" + now.getMinutes() + ":" + now.getSeconds() + ":" + now.getMilliseconds() + " " + GMT;

    return nowf;
}
function getCookieInfo() { return navigator.cookieEnabled; }
function getLanguage() { return navigator.language; }
function getUserAgent() { return navigator.userAgent; }

