
/*dk-register */
/*$('#register-step-1 .regex-group-btn').click(function(){
	$('#register-step-1').addClass('passive');
	$('#register-step-1').removeClass('active');
	$('#register-step-2').removeClass('passive');
	$('#register-step-2').addClass('active');
})
$('#register-step-2 .regex-group-btn').click(function(){
	$('#register-step-2').addClass('passive');
	$('#register-step-2').removeClass('active');
	$('#register-step-3').removeClass('passive');
	$('#register-step-3').addClass('active');
})
$('#register-step-2 .back').click(function(){
	$('#register-step-1').addClass('active');
	$('#register-step-1').removeClass('passive');
	$('#register-step-2').removeClass('active');
	$('#register-step-2').addClass('passive');
})
$('#register-step-3 .back').click(function(){
	$('#register-step-2').addClass('active');
	$('#register-step-2').removeClass('passive');
	$('#register-step-3').removeClass('active');
	$('#register-step-3').addClass('passive');
})*/
var months = ["Ocak","Şubat","Mart","Nisan","Mayıs","Haziran","Temmuz","Ağustos","Eylül","Ekim","Kasım","Aralık"];

$('#register_link').click(function(){
	
	$('#dk-signin-box .box-content-sec #login_form').css('display','none');
	$('#dk-signin-box .box-footer-sec').css('display','none');
	$('#dk-signin-box .box-head-sec .back-btn a').css('display','none');
	$('#dk-signin-box .box-head-sec .back-btn').append('<a href="javascript:;" id="back_to_login"><i class="fa fa-chevron-left"></i></a>');
	
	var register_form = "";
	register_form += 						'<div id="register_form">';
	register_form += 							'<div class="warnings"></div>';
	register_form +=							'<div class="box-content-register mar-t-10">';
	register_form +=								'<div class="register-steps active" id="register-step-1">';
	register_form +=										'<div class="header">Hesap Oluştur</div>';
	register_form +=										'<div class="text">Kardo hesabınız size avantajlı bir dünyanın kapılarını açar.</div>';
	register_form +=										'<div class="regex_group" id="register-step-1_RegEx">';
	register_form +=											'<div id="r_user_name_RegEx">';
	register_form +=												'<div class="input-effect">';
	register_form +=													'<input type="text" name="r_user_name" id="r_user_name" placeholder="" class="input-effect-2 dk-regex" data-regex_type="username" data-regex_group="register-step-1_RegEx"/>';
	register_form +=													'<label>Kullanıcı Adı</label>';
	register_form +=													'<span class="focus-border"></span>';
	register_form +=												'</div>';
	register_form +=											'</div>';
	register_form +=											'<div id="r_eposta_RegEx">';
	register_form +=												'<div class="input-effect">';
	register_form +=													'<input type="text" name="r_eposta" id="r_eposta" placeholder="" class="input-effect-2 dk-regex" data-regex_type="email" data-regex_group="register-step-1_RegEx"/>';
	register_form +=													'<label>E-Posta</label>';
	register_form +=													'<span class="focus-border"></span>';
	register_form +=												'</div>';
	register_form +=											'</div>';
	register_form +=											'<div id="r_password_RegEx">';
	register_form +=												'<div class="input-effect">';
	register_form +=													'<input type="password" name="r_password" id="r_password" placeholder="" class="input-effect-2 dk-regex" data-regex_type="password" data-regex_group="register-step-1_RegEx"/>';
	register_form +=													'<label>Şifre</label>';
	register_form +=													'<span class="focus-border"></span>';
	register_form +=												'</div>';
	register_form +=											'</div>';
	register_form +=											'<input type="button" value="İlerle" id="register-step-1-btn" class="dk-btn dk-btn-wide dk-btn-primary regex-group-btn " />';
	register_form +=										'</div>';
	register_form +=										'<div class="text warning"><b>İleri</b>\'yi seçtiğinizde, <a href="#">Kardo Hizmet Sözleşmesi</a>\'ni ve <a href="#">gizlilik ve tanımlama bilgileri bildirimini</a> kabul etmiş olursunuz.</div>';
	register_form +=								'</div>';
	register_form +=								'<div class="register-steps passive" id="register-step-2">';
	register_form +=									'<div class="header">Ayrıntıları Ekle</div>';
	register_form +=									'<div class="text">Hesap kurulumunuzu tamamlamak için biraz daha bilgiye ihtiyacımız var.</div>';
	register_form +=									'<div class="regex_group" id="register-step-2_RegEx">';
	register_form +=										'<div id="r_rname_RegEx">';
	register_form +=											'<div class="input-effect">';
	register_form +=												'<input type="text" name="r_rname" id="r_rname" placeholder="" class="input-effect-2 dk-regex" data-regex_type="letters" data-minchar="2" data-maxchar="32" data-regex_group="register-step-2_RegEx"/>';
	register_form +=												'<label>Adınız</label>';
	register_form +=												'<span class="focus-border"></span>';
	register_form +=											'</div>';
	register_form +=										'</div>';
	register_form +=										'<div id="r_surname_RegEx">';
	register_form +=											'<div class="input-effect">';
	register_form +=												'<input type="text" name="r_surname" id="r_surname" placeholder="" class="input-effect-2 dk-regex" data-regex_type="letters" data-minchar="2" data-maxchar="32" data-regex_group="register-step-2_RegEx"/>';
	register_form +=												'<label>Soyadınız</label>';
	register_form +=												'<span class="focus-border"></span>';
	register_form +=											'</div>';
	register_form +=										'</div>';
	register_form +=										'<div class="row">';
	register_form +=											'<div class="col-xs-6 pad-r-5"><input type="button" id="register-step-2-btn-back"  value="Geri" class="dk-btn dk-btn-wide dk-btn-default back" /></div>';
	register_form +=											'<div class="col-xs-6 pad-l-5"><input type="button" id="register-step-2-btn"  value="İlerle" class="dk-btn dk-btn-wide dk-btn-primary regex-group-btn " /></div>';
	register_form +=										'</div>';
	register_form +=									'</div>';
	register_form +=								'</div>';
	register_form +=								'<div class="register-steps passive" id="register-step-3">';
	register_form +=									'<div class="header">Ayrıntıları Ekle</div>';
	register_form +=									'<div class="text">Hesap kurulumunuzu tamamlamak için biraz daha bilgiye ihtiyacımız var.</div>';
	register_form +=									'<label>Doğum Tarihiniz</label>';
	register_form +=									'<div class="row">';
	register_form +=										'<div class="col-xs-4 pad-r-5">';
	register_form +=											'<select name="r_day" id="r_day">';
	register_form +=												'<option value="null">GÜN</option>';
	for(var i = 1; i <= 31; i++ ){	register_form +=				'<option value="'; register_form += (i < 10)? '0' + i + '">': i + '">'; register_form += i + '</option>';	}
	register_form +=											'</select>';
	register_form +=										'</div>';
	register_form +=										'<div class="col-xs-4 pad-l-5 pad-r-5">';
	register_form +=											'<select name="r_month" id="r_month">';
	register_form +=												'<option value="null">AY</option>';
	for(var i = 1; i <= 12; i++ ){	register_form +=				'<option value="'; register_form += (i < 10)? '0' + i + '">': i + '">'; register_form += months[i-1] + '</option>';	}
	register_form +=											'</select>';
	register_form +=										'</div>';
	register_form +=										'<div class="col-xs-4 pad-l-5">';
	register_form +=											'<select name="r_year" id="r_year">';
	register_form +=												'<option value="null">YIL</option>';
	for(var i = 2018; i >= 1900; i-- ){	register_form +=			'<option value="' + i + '">' + i + '</option>';	}
	register_form +=											'</select>';
	register_form +=										'</div>';
	register_form +=									'</div>';
	register_form +=									'<div class="row">';
	register_form +=										'<div class="col-xs-6 pad-r-5"><input type="button" id="register-step-3-btn-back" value="Geri" class="dk-btn dk-btn-wide dk-btn-default back" /></div>';
	register_form +=										'<div class="col-xs-6 pad-l-5"><input type="button" id="register-step-3-btn" value="Kayıdı Tamamla" class="dk-btn dk-btn-wide dk-btn-primary regex-group-btn"/></div>';
	register_form +=									'</div>';
	register_form +=								'</div>';
	register_form +=								'<div class="register-steps passive" id="register-step-4"><div class="out-of-middle">';
	register_form +=										'<div class="middle">';
	register_form +=											'<div class="row">';
	register_form +=												'<div class="col-xs-12">';
	register_form +=													'<div class="out-of-middle">';
	register_form +=														'<div class="middle">';
	register_form +=															'<div class="txt-align-c">';
	register_form +=																'<div class="header">Kayıdınızı Başarıyla Tamamladınız!</div>';
	register_form +=																'<img src="/Content/kardoPasso/img/systemPics/checkIcon2.gif" alt="checkIcon" />';
	register_form +=															'</div>';
	register_form +=														'</div>';
	register_form +=													'</div>';
	register_form +=												'</div>';
	register_form +=												'<div class="col-xs-12">';
	if($('#redirect').val() == "True"){
		register_form +=												'<a href="/Login/Index" class="dk-btn dk-btn-wide dk-btn-primary">Giriş Yap</a>';
	}else{
		register_form +=												'<a href="/Login/popupIndex" class="dk-btn dk-btn-wide dk-btn-primary">Giriş Yap</a>';
	}
	register_form +=												'</div>';
	register_form +=											'</div>';
	register_form +=										'</div>';
	register_form +=									'</div>';
	register_form +=								'</div>';
	register_form +=							'</div>';
	register_form +=						'</div>';
	
	$('#dk-signin-box .box-content-sec').append(register_form);
})


$(document).on('click', '#back_to_login',function(){
	$('#dk-signin-box .box-content-sec #login_form').css('display','block');
	$('#dk-signin-box .box-footer-sec').css('display','block');
	$('#dk-signin-box .box-head-sec .back-btn a').css('display','block');
	$('#dk-signin-box .box-head-sec .back-btn #back_to_login').remove();
	$('#register_form').remove();
})
/*dk-register END*/



