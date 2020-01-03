$(document).on('blur', '.dk-regex', function(){
	var id = '#' + $(this).attr('id') + '_RegEx';
	var _id = $(this).data('regex_container');	if(_id != undefined) { id = _id; }
	var expression = $(this).data('expression');	if(expression == undefined) { expression = '[^ -■]';}
	var expression_type = $(this).data('expression_type');	if(expression_type == undefined) { expression_type = 'non-coupling';}
	var minchar = $(this).data('minchar');	if(minchar == undefined) { minchar = 0;}
	var maxchar = $(this).data('maxchar');	if(minchar == undefined) { maxchar = Number.MAX_SAFE_INTEGER;}
	var _null = $(this).data('null');	if(_null == undefined) { _null = true;}
	var group = '#' + $(this).data('regex_group');	if(group == undefined) { group = null;}
	
	var type = $(this).data('regex_type');
	var errorS_ex = 'Bu alana uygun olmayan bir karakter girdiniz!';		
	var errorS_min = 'Bu alan en az ' + minchar + ' karakter içermelidir!';	
	var errorS_max = 'Bu alan en fazla ' + maxchar + ' karakter içermelidir!';	
	var errorS_null = 'Bu alan boş bırakılamaz!';
	
	switch(type){
		case 'username': expression = '[^a-zA-Z0-9.]'; expression_type = 'non-coupling'; _null = false; minchar = 6; maxchar = 16; errorS_ex = 'Kullanıcı adı sadce harf, rakam ve nokta içerebilir!'; errorS_min = 'Kullanıcı adı en az ' + minchar + ' karakter içermelidir!'; errorS_max = 'Kullanıcı adı en fazla ' + maxchar + ' karakter içermelidir!'; errorS_null = 'Kullanıcı adı boş bırakılamaz!'; break;
		case 'password': expression = '[^ -~ZçÇöÖşŞıİğĞüÜ]'; expression_type = 'non-coupling'; _null = false; minchar = 6; maxchar = 16;  errorS_ex = 'Şifreye uygun olmayan bir karakter girdiniz!'; errorS_min = 'Şifre en az ' + minchar + ' karakter içermelidir!'; errorS_max = 'Şifre en fazla ' + maxchar + ' karakter içermelidir!'; errorS_null = 'Şifre boş bırakılamaz!'; break;
		case 'email': expression = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/ ; expression_type = 'coupling'; _null = false; errorS_ex = 'E-posta adresini yanlış girdiniz!'; errorS_null = 'E-posta boş bırakılamaz!'; break;
		case 'letters': expression = '[^a-zA-ZçÇöÖşŞıİğĞüÜ]' ; expression_type = 'non-coupling'; _null = false; errorS_ex = 'Bu alana uygun olmayan bir karakter girdiniz!';  errorS_min = 'Bu alan en az ' + minchar + ' karakter içermelidir!'; break;
		case 'lettersAndSpace': expression = '[^a-zA-ZçÇöÖşŞıİğĞüÜ ]' ; expression_type = 'non-coupling'; _null = false; errorS_ex = 'Bu alana uygun olmayan bir karakter girdiniz!';  errorS_min = 'Bu alan en az ' + minchar + ' karakter içermelidir!'; break;
		case 'numbers': expression = '[^0-9]' ; expression_type = 'non-coupling'; _null = false; errorS_ex = 'Bu alana sadece sayı girilebilir!'; break;
		default : ; break;
	}
	
	if(!_null && $(this).val().length < 1){
		if(!$(id).hasClass('regex-error')){
			$(id).prepend('<div class="regex-error-text" id="' + $(this).attr('id') + '_error_text">' + errorS_null + '</div>');
			$(id).addClass('regex-error');
			$(id + ' input').addClass('danger');
		}
	}else{
		if($(id + ' input').hasClass('danger')){
			$(id + ' input').removeClass('danger');
			$(id).removeClass('regex-error');
			$(id + ' .regex-error-text').remove();
		}
		if(expression_type != 'coupling'){
			if($(this).val().length < minchar){
				if(!$(id).hasClass('regex-error')){
					$(id).prepend('<div class="regex-error-text" id="' + $(this).attr('id') + '_error_text">' + errorS_min + '</div>');
					$(id).addClass('regex-error');
					$(id + ' input').addClass('danger');
				}
			}
			else if($(this).val().length > maxchar){
				if(!$(id).hasClass('regex-error')){
					$(id).prepend('<div class="regex-error-text" id="' + $(this).attr('id') + '_error_text">' + errorS_max + '</div>');
					$(id).addClass('regex-error');
					$(id + ' input').addClass('danger');
				}
			}else{
				if($(id + ' input').hasClass('danger')){
					$(id + ' input').removeClass('danger');
					$(id).removeClass('regex-error');
					$(id + ' .regex-error-text').remove();
				}
				if($(this).val().search(expression) != -1){
					if(!$(id).hasClass('regex-error')){
						$(id).prepend('<div class="regex-error-text" id="' + $(this).attr('id') + '_error_text">' + errorS_ex + '</div>');
						$(id).addClass('regex-error');
						$(id + ' input').addClass('danger');
					}
				}else{
					if($(id + ' input').hasClass('danger')){
						$(id + ' input').removeClass('danger');
						$(id).removeClass('regex-error');
						$(id + ' .regex-error-text').remove();
					}
				}
			}
		}else{
			if(!expression.test($(this).val().toLowerCase())){
				if(!$(id).hasClass('regex-error')){
					$(id).prepend('<div class="regex-error-text" id="' + $(this).attr('id') + '_error_text">' + errorS_ex + '</div>');
					$(id).addClass('regex-error');
					$(id + ' input').addClass('danger');
				}
			}else{
				if($(id + ' input').hasClass('danger')){
					$(id + ' input').removeClass('danger');
					$(id).removeClass('regex-error');
					$(id + ' .regex-error-text').remove();
				}
			}
		}
	}
})

$(document).on('keyup keydown', '.dk-regex',function(){
	
	var text_id = '#' + $(this).attr('id') + '_error_text';
	var container_id = '#' + $(this).attr('id') + '_RegEx';

	$(text_id).remove();
	$(this).removeClass('danger');
	$(container_id).removeClass('regex-error');
})

$(document).on('blur keyup keydown', '.dk-regex',function(){
	
	var group = '#' + $(this).data('regex_group');	if(group == undefined) { group = null;}
	
	if(group != null){
		if($(group).has('.regex-error-text').length > 0){
			$(group + ' input.regex-group-btn').prop("disabled", true);
			$(group + ' input.regex-group-btn').addClass("disabled");
		}else{
			var nullControl = 0;
			$(group + ' div .dk-regex').each(function(){
				if($(this).val() == ''){nullControl++;}
			});
			if(nullControl == 0){
				$(group + ' input.regex-group-btn').prop("disabled", false);
				$(group + ' input.regex-group-btn').removeClass("disabled");
			}
		}
	}
})
