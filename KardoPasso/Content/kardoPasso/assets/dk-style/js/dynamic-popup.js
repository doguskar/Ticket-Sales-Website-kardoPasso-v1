
 $(document).on('click', '.dk-dynamic-popup',function(){
	var title = $(this).data("title"); if(title == undefined){ title = "Null"}
	var iframe = $(this).data("iframe"); if(iframe == undefined){ iframe = false }
	var content = $(this).data("content"); if(content == undefined){ content = "İçerik yok." }
	var type = $(this).data("type"); if(type == undefined){ type = "alert" }
	var theme = $(this).data("theme"); if (theme == undefined) { theme = "bg-primary" }
	var typeOfRemove = $(this).data("typeofremove"); if (typeOfRemove == undefined) { typeOfRemove = "remove" } // for login popup
	var operation = $(this).data("operation");
	var datas = $(this).data("datas");
	var datasNames = [];
	var datasValues = [];
	if(datas != undefined){
		var datasArr = datas.split('&');
		for(var i = 0; i < datasArr.length; i++){
			tempData = datasArr[i].split('_');
			datasNames.push(tempData[0]);
			datasValues.push(tempData[1]);
		}
	}
	
	
	switch(theme){
		case 'confirm': type = "confirm"; break;
	}
	
	creat_popup(title, iframe, content, type, theme, typeOfRemove, operation, datasNames, datasValues);
})

 $(document).on('click', '.remove-popup',function(){
     var id = "#" + $(this).data("id");
     if ($(id).data("typeOfRemove") != "d_remove") {
         $(id).remove();
     } else {// for login popup
         $.ajax({
             type: "POST",
             url: "/Login/sessionsControlBool",
             success: function (i) {
                 if(i == "True")
                     window.location.reload();
                 else
                     $(id).remove();
             },
             error: function () { alert("Beklenmedik bir hata oluştu!")}
         })
     }
})


 function creat_popup(title, iframe, content, type, theme, typeOfRemove, operation, datasNames, datasValues) {
	var uniqueID = guid();
	var result = "";
	result += '<div id="dk-popup-' + uniqueID + '" class="dk-popup-overlay active" data-type-of-remove="' + typeOfRemove + '">';
	result += 	'<div class="middle">';
	result += 		'<div class="dk-popup ' + theme + '" data-id="dk-popup-' + uniqueID + '">';
	if(theme != "confirm"){
		result += 			'<div class="title">';
		result += 				'<span class="text">' + title + '</span>';
		result += 				'<span class="popup-close remove-popup" data-id="dk-popup-' + uniqueID + '"><i class="fa fa-times"></i></span>';
		result += 			'</div>';
	}
	result += 			'<div class="content">';
	if(theme == "confirm"){
		result += 			'<span class="close-btn remove-popup" data-id="dk-popup-' + uniqueID + '"><i class="fa fa-times"></i></span>'
	}
							if(iframe == "false"){
								result +=content;
							}
							else if(iframe){
								result +='<iframe name="loginPageIFrame" style="height:350px; width:100%;" src="' + content + '" scrolling="no" frameborder="0"></iframe>';
							}
							else{
								result +=content;
							}
	result += 			'</div>';
	result += 			'<div class="footer">';
							if(type == "alert"){
								result += 			'<button class="dk-btn dk-btn-danger remove-popup" data-id="dk-popup-' + uniqueID + '">Kapat</button>';
							}
							else if(type == "dialog"){
								result += 			'<button class="dk-btn dk-btn-danger mar-r-10 remove-popup" data-id="dk-popup-' + uniqueID + '">İptal</button>';
								result += 			'<button class="dk-btn dk-btn-success confirm-dk-popup" data-id="dk-popup-1">Onayla</button>';
							}
							else if(type == "confirm"){
								if(theme == "confirm"){
									result += 			'<div class="row">'
									result += 				'<div class="col-xs-6 padding-0"><button class="dk-btn dk-btn-danger dk-btn-wide remove-popup" style="border:none" data-id="dk-popup-' + uniqueID + '"><i class="fa fa-times"></i></button></div>';
									result += 				'<div class="col-xs-6 padding-0"><button class="dk-btn dk-btn-primary dk-btn-wide confirmed-dk-popup" style="border:none" data-id="dk-popup-' + uniqueID + '" data-operation="' + operation + '" ';
									if(datasNames.length != 0){
										for(var i = 0; i < datasNames.length; i++){
											result += 									'data-'+ datasNames[i] + '="' + datasValues[i] + '" ';
										}
									}
									result += 										'><i class="fa fa-check"></i></button></div>';
									result += 			'</div>'
								}else{
									result += 			'<button class="dk-btn dk-btn-danger mar-r-10 remove-popup" data-id="dk-popup-' + uniqueID + '">İptal</button>';
									result += 			'<button class="dk-btn dk-btn-primary confirmed-dk-popup" data-id="dk-popup-' + uniqueID + '" data-operation="' + operation + '" ';
									if(datas != undefined){
										for(var i = 0; i < datasNames.length; i++){
											result += 									'data-'+ datasNames[i] + '="' + datasValues[i] + '" ';
										}
									}
									result += 									'>Onayla</button>';
								}
							}else{
								result += 			'<button class="dk-btn dk-btn-danger remove-popup" data-id="dk-popup-' + uniqueID + '">Kapat</button>';
							}
	result += 			'</div>';
	result += 		'</div>';
	result += 	'</div>';
	result += '</div>';		
	$('.dk-container').append(result);
}


function guid() {
  function s4() {
    return Math.floor((1 + Math.random()) * 0x10000)
      .toString(16)
      .substring(1);
  }
  return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
    s4() + '-' + s4() + s4() + s4();
}


