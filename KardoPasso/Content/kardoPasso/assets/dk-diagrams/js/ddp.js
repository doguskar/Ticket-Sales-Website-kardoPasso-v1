$(document).on('click', '.ddbc-item',function(){
	var ddbinfo = $(this).data("ddbinfo"); if(ddbinfo == undefined){ ddbinfo = "Null"}
	var title = $(this).attr('title'); if(title == undefined){ title = "Null"}
	var theme = $(this).data("theme"); if (theme == undefined) { theme = "Null" }
	var ddbid = $(this).data("ddbid"); if (ddbid == undefined) { ddbid = "Null" }
	var ddItemId = $(this).attr('id'); if (ddItemId == undefined) { ddItemId = "Null" }
	var typeOfRemove = $(this).data("typeofremove"); if (typeOfRemove == undefined) { typeOfRemove = "remove" }
	
	creat_popup(ddbinfo, title, theme, typeOfRemove, ddbid, ddItemId);
})

 $(document).on('click', '.ddp-cancel',function(){
     var id = "#" + $(this).data("ddpid");
     if ($(id).data("typeOfRemove") == "remove") {
         $(id).remove();
     }
})
/**/
function creat_popup(ddbinfo, title, theme, typeOfRemove, ddbid, ddItemId) {
	var uniqueID = guid();
	var result = "", content="", buttons="", returnType="", ddItemName="", className="", header="";
	
	if(ddbinfo == 'class'){
		header = "Edit"; 
		if(ddbid != "Null")
			className = $('#' + ddbid + ' .ddbc-item .txt').text();
		content += 					'<table>';
		content +=						'<tbody>';
		content +=							'<td>';
		content +=								'<select name="classType" id="classType">';
		content +=									'<option value="null">Class Type</option>';
		if(ddbid != "Null"){
			content +=									'<option value="class"';
			if(title == 'class'){content += ' selected'}
			content += 																'>Class</option>';
			content +=									'<option value="class-static"';
			if(title == 'class-static'){content += ' selected'}
			content += 																'>Static Class</option>';
			content +=									'<option value="class-abstract"';
			if(title == 'class-abstract'){content += ' selected'}
			content += 																'>Abstarct Class</option>';
			content +=									'<option value="interface"';
			if(title == 'interface'){content += ' selected'}
			content += 																'>InterFace</option>';
		}
		else{
			content +=							'<option value="class">Class</option>';
			content +=							'<option value="class-static">Static Class</option>';
			content +=							'<option value="class-abstract">Abstarct Class</option>';
			content +=							'<option value="interface">InterFace</option>';
		}
		content +=								'</select>';
		content +=							'</td>';
		content +=							'<td>';
		content +=								'<div class="input-effect">';
		if(ddbid != "Null")
			content +=									'<input type="text" name="" id="className" placeholder="" class="input-effect-2 has-content"  value="' + className + '"/>';
		else
			content +=									'<input type="text" name="" id="className" placeholder="" class="input-effect-2"/>';
		content +=									'<label>Class Name</label>';
		content +=									'<span class="focus-border"></span>';
		content +=								'</div>';
		content +=							'</td>';
		content +=						'</tbody>';
		content +=					'</table>';
	}
	else if(ddbinfo == 'attribute'){
		if(ddItemId != "Null")
			header = "Edit";
		else
			header = "Add";
		returnType = $('#'+ddItemId + ' .detail').text();
		ddItemName = $('#'+ddItemId + ' .name').text();
		content += 					'<table>';
		content +=						'<tbody>';
		content +=							'<td>';
		content +=								'<select name="attributeType" id="attributeType">';
		if(title == 'method' || title == 'method-static' || title == 'method-abstract'){
			content +=									'<option value="null">Method Type</option>';
			content +=									'<option value="method"';
			if(title == 'method' && ddItemId != "Null"){content += ' selected'}
			content += 																'>Method</option>';
			content +=									'<option value="method-static"';
			if(title == 'method-static' && ddItemId != "Null"){content += ' selected'}
			content += 																'>Static Method</option>';
			content +=									'<option value="method-abstract"';
			if(title == 'method-abstract' && ddItemId != "Null"){content += ' selected'}
			content += 																'>Abstarct Method</option>';
		}
		else if(title == 'field' || title == 'field-static' || title == 'field-abstract'){
			content +=									'<option value="null">Field Type</option>';
			content +=									'<option value="field"';
			if(title == 'field' && ddItemId != "Null"){content += ' selected'}
			content += 																'>Field</option>';
			content +=									'<option value="field-static"';
			if(title == 'field-static' && ddItemId != "Null"){content += ' selected'}
			content += 																'>Static Field</option>';
			content +=									'<option value="field-abstract"';
			if(title == 'field-abstract' && ddItemId != "Null"){content += ' selected'}
			content += 																'>Abstarct Field</option>';
		}
		else if(title == 'parameter'){
			content +=									'<option value="null">Parameter Type</option>';
			content +=									'<option value="parameter"';
			if(title == 'parameter' && ddItemId != "Null"){content += ' selected'}
			content += 																'>Parameter</option>';
		}
		content +=							'</td>';
		content +=							'<td>';
		content +=								'<div class="input-effect">';
		if(ddItemId != "Null")
			content +=									'<input type="text" name="" id="attributeName" placeholder="" class="input-effect-2 has-content"  value="' + ddItemName + '"/>';
		else	
			content +=									'<input type="text" name="" id="attributeName" placeholder="" class="input-effect-2"/>';
		content +=									'<label>Attribute Name</label>';
		content +=									'<span class="focus-border"></span>';
		content +=								'</div>';
		content +=							'</td>';
		content +=							'<td>';
		if(title == 'method'){
			content +=								'<select name="" id="attributeReturn">';
			content +=									'<option value="null">Return Type</option>';
			if(ddbid != "Null"){
				content +=									'<option value="void"';
					if(returnType == 'void'){content += ' selected'}
					content += 														'>void</option>';
				content +=									'<option value="string"';
					if(returnType == 'string'){content += ' selected'}
					content += 														'>String</option>';
				content +=									'<option value="stringarr"';
					if(returnType == 'stringarr'){content += ' selected'}
					content += 														'>String[]</option>';
				content +=									'<option value="char"';
					if(returnType == 'char'){content += ' selected'}
					content += 														'>char</option>';
				content +=									'<option value="chararr"';
					if(returnType == 'chararr'){content += ' selected'}
					content += 														'>char[]</option>';
				content +=									'<option value="byte"';
					if(returnType == 'byte'){content += ' selected'}
					content += 														'>byte</option>';
				content +=									'<option value="bytearr"';
					if(returnType == 'bytearr'){content += ' selected'}
					content += 														'>byte[]</option>';
				content +=									'<option value="short"';
					if(returnType == 'short'){content += ' selected'}
					content += 														'>short</option>';
				content +=									'<option value="shortarr"';
					if(returnType == 'shortarr'){content += ' selected'}
					content += 														'>short[]</option>';
				content +=									'<option value="int"';
					if(returnType == 'int'){content += ' selected'}
					content += 														'>int</option>';
				content +=									'<option value="intarr"';
					if(returnType == 'intarr'){content += ' selected'}
					content += 														'>int[]</option>';
				content +=									'<option value="integer"';
					if(returnType == 'integer'){content += ' selected'}
					content += 														'>Integer</option>';
				content +=									'<option value="integerarr"';
					if(returnType == 'integerarr'){content += ' selected'}
					content += 														'>Integer[]</option>';
				content +=									'<option value="float"';
					if(returnType == 'float'){content += ' selected'}
					content += 														'>float</option>';
				content +=									'<option value="floatarr"';
					if(returnType == 'floatarr'){content += ' selected'}
					content += 														'>float[]</option>';
				content +=									'<option value="double"';
					if(returnType == 'double'){content += ' selected'}
					content += 														'>double</option>';
				content +=									'<option value="doublearr"';
					if(returnType == 'doublearr'){content += ' selected'}
					content += 														'>double[]</option>';
				content +=									'<option value="long"';
					if(returnType == 'long'){content += ' selected'}
					content += 														'>long</option>';
				content +=									'<option value="longarr"';
					if(returnType == 'longarr'){content += ' selected'}
					content += 														'>long[]</option>';
				content +=									'<option value="boolen"';
					if(returnType == 'boolen'){content += ' selected'}
					content += 														'>boolen</option>';
				content +=									'<option value="boolenarr"';
					if(returnType == 'boolenarr'){content += ' selected'}
					content += 														'>boolen[]</option>';
				content +=									'<option value="boolenobj"';
					if(returnType == 'boolenobj'){content += ' selected'}
					content += 														'>Boolen</option>';
				content +=									'<option value="boolenobjarr"';
					if(returnType == 'boolenobjarr'){content += ' selected'}
					content += 														'>Boolen[]</option>';
			}
			else{
				content +=									'<option value="void">void</option>';
				content +=									'<option value="string">String</option>';
				content +=									'<option value="stringarr">String[]</option>';
				content +=									'<option value="char">char</option>';
				content +=									'<option value="chararr">char[]</option>';
				content +=									'<option value="byte">byte</option>';
				content +=									'<option value="bytearr">byte[]</option>';
				content +=									'<option value="short">short</option>';
				content +=									'<option value="shortarr">short[]</option>';
				content +=									'<option value="int">int</option>';
				content +=									'<option value="intarr">int[]</option>';
				content +=									'<option value="integer">Integer</option>';
				content +=									'<option value="integerarr">Integer[]</option>';
				content +=									'<option value="float">float</option>';
				content +=									'<option value="floatarr">float[]</option>';
				content +=									'<option value="double">double</option>';
				content +=									'<option value="doublearr">double[]</option>';
				content +=									'<option value="long">long</option>';
				content +=									'<option value="longarr">long[]</option>';
				content +=									'<option value="boolen">boolen</option>';
				content +=									'<option value="boolenarr">boolen[]</option>';
				content +=									'<option value="boolenobj">Boolen</option>';
				content +=									'<option value="boolenobjarr">Boolen[]</option>';
			}
			content +=								'</select>';
		}
		content +=							'</td>';
		content +=						'</tbody>';
		content +=					'</table>';
	}
	else if(ddbinfo == 'comment'){
		if(ddItemId != "Null")
			header = "Edit";
		else
			header = "Add Comment";
		content +=						'<table>';
		content +=						'<tbody>';
		content +=							'<td>';
		content +=								'<div class="input-effect">';
		if(ddItemId != "Null")
			content +=									'<input type="text" name="" id="ddb-comment" placeholder="" class="input-effect-2 has-content"  value="' + $('#'+ddItemId).text() + '"/>';
		else	
			content +=									'<input type="text" name="" id="ddb-comment" placeholder="" class="input-effect-2"/>';
		content +=									'<label>Comment</label>';
		content +=									'<span class="focus-border"></span>';
		content +=								'</div>';
		content +=							'</td>';
		content +=						'</tbody>';
		content +=					'</table>';
	}
	
	//buttons
	
	if(ddItemId != 'Null'){
		buttons +=							'<div class="col-xs-4 pad-r-0"><button class="dk-btn dk-btn-default dk-btn-wide ddp-cancel" data-ddpid="ddp-' + uniqueID + '">Cancel</button></div>';
		buttons +=							'<div class="col-xs-4 pad-l-0 pad-r-0"><button class="dk-btn dk-btn-default dk-btn-wide ddp-remove" data-ddbid="' + ddbid + '" data-dd-item-id="' + ddItemId + '">Remove</button></div>';
		buttons +=							'<div class="col-xs-4 pad-l-0"><button class="dk-btn dk-btn-default border-r-none dk-btn-wide ddp-save" data-ddbid="' + ddbid + '" data-dd-item-id="' + ddItemId + '">Confirm</button></div>';
	}else{	
		buttons +=							'<div class="col-xs-6 pad-r-0"><button class="dk-btn dk-btn-default dk-btn-wide ddp-cancel" data-ddpid="ddp-' + uniqueID + '">Cancel</button></div>';
		buttons +=							'<div class="col-xs-6 pad-l-0"><button class="dk-btn dk-btn-default border-r-none dk-btn-wide ddp-save" data-ddbid="' + ddbid + '">Add</button></div>';
	}
	
	//result
	result += 		'<div id="ddp-' + uniqueID + '"  class="ddp-overlay active" data-type-of-remove="' + typeOfRemove + '">';
	result += 			'<div class="middle">';
	if(theme != "Null")
		result +=				'<div class="dk-diagram-popup ' + theme + '" data-id="ddp-' + uniqueID + '">';
	else
		result +=				'<div class="dk-diagram-popup" data-id="ddp-' + uniqueID + '">';
	result +=					'<div class="ddp-hearder">' + header + '</div>';
	result +=					'<div class="ddp-content">';
	result += 						content;
	result +=					'</div>';
	result +=					'<div class="ddp-footer">';
	result +=						'<div class="row">';
	result += 							buttons
	result +=						'</div>';
	result +=					'</div>';
	result +=				'</div>';
	result +=			'</div>';
	result +=		'</div>';
	
	$('body').append(result);
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