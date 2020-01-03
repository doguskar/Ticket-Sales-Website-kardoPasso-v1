$(document).on('blur', '.input-effect input',function(){
	
	if( $(this).val().length > 0 ){
		if(!$(this).hasClass('has-content')){
			$(this).addClass('has-content');
		}
	}else{
		if($(this).hasClass('has-content')){
			$(this).removeClass('has-content');
		}
	}
	
})