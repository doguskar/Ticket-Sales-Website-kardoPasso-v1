/*$(document).ready(function () {
    $.get("http://ipinfo.io", function (response) {
        $("#userInfo").data("info", response); //city,country,hostname,ip,loc,org,region
    }, "jsonp");
});*/

$("#login-step-1-form-btn").click(function () {
    findUser();
});
$('#user_name').keypress(function (e) {
    if (e.which == 13) {
        if($('#login-step-1').hasClass('active'))
            findUser();
    }
});

// Selection of account
$(document).on('click', '.accounts-item', function () {
    var username = ($(this).find('.username')).text();
    var clearUsername = (username.split('@'))[1];
    $('#user_name').val(clearUsername);
    findUser();
})
// Selection of account END

$("#login-step-2-form-btn").click(function () {
    controlLogin();
});
$('#password').keypress(function (e) {
    if (e.which == 13) {
        if ($('#login-step-2').hasClass('active'))
            controlLogin();
    }
});


$('#login-step-back-1').click(function () {
    $('#login-step-1').removeClass('passive');
    $('#login-step-1').addClass('active');
    $('#login-step-2').removeClass('active');
    $('#login-step-2').addClass('passive');
    $('#dk-signin-box .user-info').removeClass('active')
    $('#dk-signin-box .user-info').addClass('passive');
    $('#dk-signin-box .user-info .text').html("");
    $('#login-step-1-form-btn').removeClass('disabled');
    $('#login-step-1-form-btn').prop("disabled", false);
    $('#login_form .warnings').html("");
    $('#login-step-2-footer').removeClass('active');
    $('#login-step-2-footer').addClass('passive');
    $('#login-step-1-footer').removeClass('passive');
    $('#login-step-1-footer').addClass('active');
})

function findUser() {
    $('#login-step-1-form-btn').addClass('disabled');
    $('#login-step-1-form-btn').prop("disabled", true);
    $('#dk-loading-sec').addClass('active');
    $('#login-step-1-footer').removeClass('active');
    $('#login-step-1-footer').addClass('passive');

    var user_name_type = "username";
    var eMailExpression = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (eMailExpression.test($('#user_name').val().toLowerCase()))
        user_name_type = "email";

    $.ajax({
        type: "POST",
        url: "/Login/findUser",
        data: {
            user_login_key: $('#user_name').val(),
            user_name_type: user_name_type
        },
        success: function (i) {
            if (i == "True") {
                $('#login_form .warnings').html("");
                $('#login-step-1').removeClass('active');
                $('#login-step-1').addClass('passive');
                $('#login-step-2').removeClass('passive');
                $('#login-step-2').addClass('active');
                $('#dk-signin-box .user-info').removeClass('passive')
                $('#dk-signin-box .user-info').addClass('active');
                $('#dk-signin-box .user-info .text').html($('#user_name').val());
                $('#dk-signin-box .user-info .text').attr('id', ($('#user_name').val() + "-" + i));
                $('#dk-loading-sec').removeClass('active');
                $('#login-step-1-footer').removeClass('active');
                $('#login-step-1-footer').addClass('passive');
                $('#login-step-2-footer').removeClass('passive');
                $('#login-step-2-footer').addClass('active');

            }
            else if (i == "showCaptcha") {
                //will be filled this block
				//THIS PART IS NOT ACTIVE
                $('#login_form .warnings').html("");
                $('#login-step-1').removeClass('active');
                $('#login-step-1').addClass('passive');
                $('#login-step-2').removeClass('passive');
                $('#login-step-2').addClass('active');
                $('#dk-signin-box .user-info').removeClass('passive')
                $('#dk-signin-box .user-info').addClass('active');
                $('#dk-signin-box .user-info .text').html($('#user_name').val());
                $('#dk-signin-box .user-info .text').attr('id', ($('#user_name').val() + "-" + i));
                $('#dk-loading-sec').removeClass('active');
                $('#login-step-1-footer').removeClass('active');
                $('#login-step-1-footer').addClass('passive');
                $('#login-step-2-footer').removeClass('passive');
                $('#login-step-2-footer').addClass('active');

            }
            else if (i == "banned30Min") {
                $('#login_form .warnings').html('<div class="dk-alert-style-2 bg-danger">     <p> User is blocked for a short time due to too many incorrect password entries!   </p>     </div>');
                $('#dk-loading-sec').removeClass('active');
                $('#login-step-1-form-btn').removeClass('disabled');
                $('#login-step-1-form-btn').prop("disabled", false);
                $('#login-step-1-footer').removeClass('passive');
                $('#login-step-1-footer').addClass('active');

            } else {
                $('#login_form .warnings').html('<div class="dk-alert-style-2 bg-danger">     <p> User not found!   </p>     </div>');
                $('#dk-loading-sec').removeClass('active');
                $('#login-step-1-form-btn').removeClass('disabled');
                $('#login-step-1-form-btn').prop("disabled", false);
                $('#login-step-1-footer').removeClass('passive');
                $('#login-step-1-footer').addClass('active');
            }
        },
        error: function () {
            alert("Hata!");
        }
    })
}

function controlLogin() {
    $('#login-step-2-form-btn').addClass('disabled');
    $('#login-step-2-form-btn').prop("disabled", true);
    $('#dk-loading-sec').addClass('active');
    $('#login-step-2-footer').removeClass('active');
    $('#login-step-2-footer').addClass('passive');

    var remember_me = "";
    var session_checkbox = "";
    if ($('#remember_me').is(":checked"))
        remember_me = "checked";
    if ($('#session_checkbox').is(":checked"))
        session_checkbox = "checked";
    $.ajax({
        type: "POST",
        url: "/Login/controlLogin",
        data: {
            user_login_key: $('#user_name').val(),
            remember_me: remember_me,
            session_checkbox: session_checkbox,
            password: $('#password').val(),
            ipAddress: getIpAddress(),
            country: getCountry(),
            city: getCity(),
            loc: getLoc(),
            hostname: getHostname()
        },
        success: function (i) {
            if (i == "True") {
                if ($('#redirect').val() == "True")
                    window.location = "/";
                else
                    $('#showCheckIcon').addClass("active");
            }
            else if (i == "showCaptcha") {
                //will be filled this block
				//THIS PART IS NOT ACTIVE
                $('#login_form .warnings').html('<div class="dk-alert-style-2 bg-danger">     <p> Invalid password!   </p>     </div>');
                $('#login-step-2-form-btn').removeClass('disabled');
                $('#login-step-2-form-btn').prop("disabled", false);
                $('#dk-loading-sec').removeClass('active');
                $('#login-step-2-footer').removeClass('passive');
                $('#login-step-2-footer').addClass('active');
            
            }
            else if (i == "banned30Min") {
                $('#login_form .warnings').html('<div class="dk-alert-style-2 bg-danger">     <p> User is blocked for a short time due to too many incorrect password entries!   </p>     </div>');
                $('#login-step-2-form-btn').removeClass('disabled');
                $('#login-step-2-form-btn').prop("disabled", false);
                $('#dk-loading-sec').removeClass('active');
                $('#login-step-2-footer').removeClass('passive');
                $('#login-step-2-footer').addClass('active');
            
            } else {
                $('#login_form .warnings').html('<div class="dk-alert-style-2 bg-danger">     <p> Invalid password!   </p>     </div>');
                $('#login-step-2-form-btn').removeClass('disabled');
                $('#login-step-2-form-btn').prop("disabled", false);
                $('#dk-loading-sec').removeClass('active');
                $('#login-step-2-footer').removeClass('passive');
                $('#login-step-2-footer').addClass('active');
            }
        },
        error: function () {
            alert("Hata!");
        }
    })

}

function getIpAddress() { return "";return $("#userInfo").data("info").ip; }
function getCity() { return "";return $("#userInfo").data("info").city; }
function getCountry() { return "";return $("#userInfo").data("info").country; }
function getHostname() { return "";return $("#userInfo").data("info").hostname; }
function getLoc() { return "";return $("#userInfo").data("info").loc; }
function getNowDate() {
    var now = new Date();
    var GMT = (now.toString().split(" "))[5]; 
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


//Register Section
$(document).on('click', '#register-step-1-btn', function () {
    controlUsername();
})
$(document).on('keypress', '#r_user_name, #r_eposta, #r_password', function (e) {
    if (e.which == 13) {
        controlUsername();
    }
})
function controlUsername() {
    $('#register-step-1-btn').addClass('disabled');
    $('#register-step-1-btn').prop("disabled", true);
    $('#dk-loading-sec').addClass('active');

    var userName = $('#r_user_name').val();
    var eMail = $('#r_eposta').val();
    var password = $('#r_password').val();
    var progress = 0;
    var eMailExpression = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    var userNameExpression = '[^a-zA-Z0-9.]';
    var passwordExpression = '[^ -~ZçÇöÖşŞıİğĞüÜ]';
    if (!eMailExpression.test(eMail.toLowerCase()))
        progress++;
    if (userName.length < 6 || userName.length > 16 || userName.search(userNameExpression) != -1)
        progress++;
    if (password.length < 6 || password.length > 16 || password.search(passwordExpression) != -1)
        progress++;
    if (progress == 0) {
        $.ajax({
            type: "POST",
            url: "/Login/controlUseEma",
            data: {
                userName: userName,
                eMail: eMail
            },
            success: function (i) {
                if (i == "True") {
                    $('#register_form .warnings').html('');
                    $('#register-step-2').addClass('active'); 
                    $('#register-step-1').removeClass('active');
                    $('#register-step-1').addClass('passive');
                    $('#dk-loading-sec').removeClass('active');
                    $('#register-step-2').removeClass('passive');
                    if ($('#register-step-2-btn').hasClass('disabled')) {
                        $('#register-step-2-btn').remove('disabled');
                        $('#register-step-2-btn').prop("disabled", false);
                    }
                }
                else if (i == "userName"){
                    $('#register_form .warnings').html('<div class="dk-alert-style-2 bg-danger">     <p> Girdiğiniz <strong>kullanıcı</strong> daha önce kullanılmıştır.    </p>     </div>');
                    $('#dk-loading-sec').removeClass('active');
                }
                else if (i == "eMail"){
                    $('#register_form .warnings').html('<div class="dk-alert-style-2 bg-danger">     <p> Girdiğiniz <strong>e-Posta</strong> adresi daha önce kullanılmıştır.    </p>     </div>');
                    $('#dk-loading-sec').removeClass('active');
                }else {
                    $('#register_form .warnings').html('<div class="dk-alert-style-2 bg-danger">     <p> Yolunda gitmeyen bir şeylerle karşılaştık.    </p>     </div>');
                    $('#dk-loading-sec').removeClass('active');
                }
            },
            error: function () {
                alert("controlUseEma Hata!");
            }
        })
    } else{
        $('#dk-loading-sec').removeClass('active');
        $('#register_form .warnings').html('<div class="dk-alert-style-2 bg-danger">     <p> Girdiğiniz değerleri kontrol edin.    </p>     </div>');
    }
    
    
}


$(document).on('click', '#register-step-2-btn', function () {
    registerStep2();
})
$(document).on('keypress', '#r_rname, #r_surname', function (e) {
    if (e.which == 13) {
        registerStep2();
    }
})
$(document).on('click', '#register-step-2-btn-back', function () {
    $('#register_form .warnings').html('');
    $('#register-step-2-btn').addClass('disabled');
    $('#register-step-2-btn').prop("disabled", true);
    $('#dk-loading-sec').addClass('active');
    $('#register-step-2').removeClass('active');
    $('#register-step-2').addClass('passive');
    $('#register-step-1').removeClass('passive');
    $('#register-step-1').addClass('active');
    $('#register-step-1-btn').remove('disabled');
    $('#register-step-1-btn').prop("disabled", false);
    $('#dk-loading-sec').removeClass('active');
})
function registerStep2() {
    $('#register-step-1-btn').addClass('disabled');
    $('#register-step-1-btn').prop("disabled", true);
    $('#dk-loading-sec').addClass('active');

    var rName = $('#r_rname').val();
    var sName = $('#r_surname').val();
    var expression = '[^a-zA-ZçÇöÖşŞıİğĞüÜ]';
    var progress = 0;

    if (rName.length < 2 || rName.length > 32 || rName.search(expression) != -1)
        progress++;
    if (sName.length < 2 || sName.length > 32 || sName.search(expression) != -1)
        progress++;
    if (progress == 0) {
        $('#register_form .warnings').html('');
        $('#register-step-2').removeClass('active');
        $('#register-step-2').addClass('passive');
        $('#dk-loading-sec').removeClass('active');
        $('#register-step-3').addClass('active');
        $('#register-step-3').removeClass('passive');
        if ($('#register-step-3-btn').hasClass('disabled')) {
            $('#register-step-3-btn').remove('disabled');
            $('#register-step-3-btn').prop("disabled", false);
        }
    } else {
        $('#dk-loading-sec').removeClass('active');
        $('#register_form .warnings').html('<div class="dk-alert-style-2 bg-danger">     <p>Ad ve soyad 2 ile 32 karakter arağılında olmalıdır ve sadece harf içerebilir.</p>     </div>');
    }
}


$(document).on('click', '#register-step-3-btn', function () {
    registerStep3();
})
$(document).on('click', '#register-step-3-btn-back', function () {
    $('#register_form .warnings').html('');
    $('#register-step-3-btn').addClass('disabled');
    $('#register-step-3-btn').prop("disabled", true);
    $('#dk-loading-sec').addClass('active');
    $('#register-step-3').removeClass('active');
    $('#register-step-3').addClass('passive');
    $('#register-step-2').removeClass('passive');
    $('#register-step-2').addClass('active');
    $('#register-step-2-btn').remove('disabled');
    $('#register-step-2-btn').prop("disabled", false);
    $('#dk-loading-sec').removeClass('active');
})

function registerStep3() {
    $('#register-step-3-btn').addClass('disabled');
    $('#register-step-3-btn').prop("disabled", true);
    $('#dk-loading-sec').addClass('active');

    var day = $('#r_day').val();
    var month = $('#r_month').val();
    var year = $('#r_year').val();
    if (day != "null" || month != "null" || year != "null") {
        var date = new Date(parseInt(year), (parseInt(month) - 1), parseInt(day));
        if (date.getMonth() == (parseInt(month) - 1)) {

            var userName = $('#r_user_name').val();
            var eMail = $('#r_eposta').val();
            var password = $('#r_password').val();
            var rName = $('#r_rname').val();
            var sName = $('#r_surname').val();

            $.ajax({
                type: "POST",
                url: "/Login/completeRegistry",
                data: {
                    userName: userName,
                    eMail: eMail,
                    password: password,
                    rName: rName,
                    sName: sName,
                    day: day,
                    month: month,
                    year: year,
                    languagePreference: getLanguage()
                },
                success: function (i) {
                    if (i == "True") {
                        $('#register_form .warnings').html('');
                        $('#register-step-3').removeClass('active');
                        $('#register-step-3').addClass('passive');
                        $('#dk-loading-sec').removeClass('active');
                        $('#register-step-4').addClass('active');
                        $('#register-step-4').removeClass('passive');
                    } else {
                        $('#register_form .warnings').html('<div class="dk-alert-style-2 bg-danger">     <p>Beklenmedik bir hatayla karşılaştık, lütfen sayfayı yenileyerek tekrar deneyiniz.</p> <p> Anlayışınız için teşşekür ederiz.</p> <p><strong>Hata Kodu:</strong> ' + i + '</p>     </div>');
                        $('#dk-loading-sec').removeClass('active');
                    }
                },
                error: function () {
                    alert("completeRegistry Hata!");
                }
            })
        } else {
            $('#dk-loading-sec').removeClass('active');
            $('#register-step-3-btn').removeClass('disabled');
            $('#register-step-3-btn').prop("disabled", false);
            $('#register_form .warnings').html('<div class="dk-alert-style-2 bg-danger">     <p>Geçerli bir tarih seçiniz.</p>     </div>');
        }

    } else {
        $('#dk-loading-sec').removeClass('active');
        $('#register-step-3-btn').removeClass('disabled');
        $('#register-step-3-btn').prop("disabled", false);
        $('#register_form .warnings').html('<div class="dk-alert-style-2 bg-danger">     <p>Geçerli bir tarih seçiniz.</p>     </div>');
    }
}
//Register Section END

