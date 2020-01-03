/*$(document).ready(function () {
    $.get("http://ipinfo.io", function (response) {
        $("#userInfo").data("info", response); //city,country,hostname,ip,loc,org,region
    }, "jsonp");
});*/

$('#logout-btn').click(function () {
    $.ajax({
        type: "POST",
        url: "/Login/clearSessions",
        success: function () { window.location.reload(); },
        error: function () { alert("Oturum kapatılamadı!") }
    })
})

function getOtherAccounts(userIds) {
    $.ajax({
        type: "POST",
        url: "/Default/getOtherAccounts",
        data: { userIds: userIds },
        success: function (json) {
            appendOtherAccounts(json);
        },
        error: function () { alert("getOtherAccounts - Hata!") }
    })
}

function appendOtherAccounts(json) {
    var usersInfos = JSON.parse(json);
    var htmlString = "";
    for (var i = 0; i < usersInfos.username.length; i++) {
        htmlString +=           '<div class="accounts-item">';
        htmlString +=           '<a href="javascript:;" class="dk-dynamic-popup"';
        htmlString +=                                   'data-content="Do you want to switch to @' + usersInfos.username[i] + ' user account?" ';
        htmlString +=                                   'data-theme="confirm" ';
        htmlString +=                                   'data-operation="changeAccount" ';
        htmlString +=                                   'data-datas="uuid_' + usersInfos.UUID[i] + '">';
        if (usersInfos.profilPic[i] == null || usersInfos.profilPic[i] == "")
            htmlString +=                 '<img src="/Content/kardoPasso/img/userProfilPics/profil_pic.png" alt="" />';
        else
            htmlString +=                 '<img src="/Content/kardoPasso/img/userProfilPics/' + usersInfos.profilPic[i] + '" alt="" />';
        htmlString +=                     '<div class="user-rname">' + usersInfos.name[i] + ' ' + usersInfos.surname[i] + '</div>';
        htmlString +=                     '<div class="username">&#64;' + usersInfos.username[i] + '</div>';
        htmlString +=                ' </a>';
        htmlString +=           '</div>';
    }
    $("#other-accounts").html(htmlString);
}

$(document).on('click', '.confirmed-dk-popup', function () {
    if ($(this).data("operation") == "changeAccount") {
        var UUID = $(this).data("uuid");
        $.ajax({
            type: "POST",
            url: "/Login/changeAccount",
            data: { uuid: UUID,
                    ipAddress: getIpAddress(),
                    country: getCountry(),
                    city: getCity(),
                    loc: getLoc(),
                    hostname: getHostname() },
            success: function (e) {
                if (e == "True") {
                    window.location.reload();
                } else {
                    alert("Giçiş yapılamadı!");
                }
            },
            error: function () { alert("getOtherAccounts - Hata!") }
        })
    }
})
/*********************************************************************/
$('#getKardoPassoPopup').on('submit', function() {
	$.ajax({
		type: 'POST',
		url: '/UserProfile/getKardoPasso',
		success: function (e) {
			if(e == "True"){
				window.location.reload();
			}
			else{
				alert("An error occurred!");
			}
		},
		error: function () { alert("An error occurred!");}
	})
})
/*********************************************************************/



function getIpAddress() { 	return null;
	var ip =$("#userInfo").data("info").ip;
	if(ip == undefined)
		return ""; 
	return ip
}
function getCity() { return "";return $("#userInfo").data("info").city; }
function getCountry() { return "";return $("#userInfo").data("info").country; }
function getHostname() { return "";return $("#userInfo").data("info").hostname; }
function getLoc() { return "";return $("#userInfo").data("info").loc; }
function getNowDate() {
    var now = new Date();
    var GMT = (now.toString().split(" "))[5]; debugger;
    var monthFixed = now.getMonth() + 1;
    var month = (monthFixed.toString().length != 2) ? ("0" + monthFixed) : monthFixed;
    var day = ((now.getDate()).toString().length != 2) ? ("0" + now.getDate()) : now.getDate();
    var hour = ((now.getHours()).toString().length != 2) ? ("0" + now.getHours()) : now.getHours();
    var minute = ((now.getMinutes()).toString().length != 2) ? ("0" + now.getMinutes()) : now.getMinutes();
    var second = ((now.getSeconds()).toString().length != 2) ? ("0" + now.getSeconds()) : now.getSeconds();
    var millisecond = ((now.getMilliseconds()).toString().length == 1) ? ("00" + now.getMilliseconds()) : (((now.getMilliseconds()).toString().length == 2) ? ("0" + now.getMilliseconds()) : now.getMilliseconds());
    var nowf = now.getFullYear() + "-" + month + "-" + day + " " + hour + ":" + minute + ":" + second + ":" + millisecond + " " + GMT;

    return nowf;
}
function getCookieInfo() { return navigator.cookieEnabled; }
function getLanguage() { return navigator.language; }
function getUserAgent() { return navigator.userAgent; }
