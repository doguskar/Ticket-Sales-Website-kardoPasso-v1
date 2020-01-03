
$(".dk-diagram-box").draggable({cursor: "move", handle: ".ddb-header"});// ,containment: "#dk-diagram-canvas", grid: [ 10, 10 ]
$(".dk-diagram-box .ddb-content-sec ul").sortable({revert: true, items: "li:not(.ui-state-disabled)"});

//dd-menu
$(document).on('click', '.dd-add-class',function(){
	//alert("dd-add-class");
})
$(document).on('click', '.dd-save',function(){
	//alert("dd-save");
})
$(document).on('click', '.dd-back',function(){
	//alert("dd-back");
})
$(document).on('click', '.dd-next',function(){
	//alert("dd-next");
})
//dd-menu end

//ddb-header
$(document).on('click', '.ddb-header li.ddbc-item',function(){
	//alert(".ddb-header li.ddbc-item");
})
//ddb-header end
//ddb-content
$(document).on('click', '.ddb-content-sec li.ddbc-item',function(){
	//alert(".ddb-content-sec li.ddbc-item");
})
//ddb-content end

//ddb-header
$(document).on('click', '.ddb-remove-class',function(){
	//alert("ddb-remove-class");
})
$(document).on('click', '.ddb-add-fields',function(){
	//alert("ddb-add-fields");
})
$(document).on('click', '.ddb-add-methods',function(){
	//alert("ddb-add-methods");
})
$(document).on('click', '.ddb-add-parameters',function(){
	//alert("ddb-add-parameters");
})
$(document).on('click', '.ddb-add-comments',function(){
	//alert("ddb-add-comments");
})
//ddb-header end
//fields-sec
$(document).on('click', '.ddb-remove-fields',function(){
	//alert("ddb-remove-fields");
})
$(document).on('click', '.ddb-add-field',function(){
	var ddbid = $(this).data("ddbid"); if (ddbid == undefined) { ddbid = "Null" }
	creat_popup('attribute', 'field', 'Null', 'remove', ddbid, 'Null');
})
//fields-sec end
//methods-sec
$(document).on('click', '.ddb-remove-methods',function(){
	//alert("ddb-remove-methods");
})
$(document).on('click', '.ddb-add-method',function(){
	var ddbid = $(this).data("ddbid"); if (ddbid == undefined) { ddbid = "Null" }
	creat_popup('attribute', 'method', 'Null', 'remove', ddbid, 'Null');
})
//methods-sec end
//parameters-sec
$(document).on('click', '.ddb-remove-parameters',function(){
	//alert("ddb-remove-parameters");
})
$(document).on('click', '.ddb-add-parameter',function(){
	var ddbid = $(this).data("ddbid"); if (ddbid == undefined) { ddbid = "Null" }
	creat_popup('attribute', 'parameter', 'Null', 'remove', ddbid, 'Null');
})
//parameters-sec end
//comments-sec
$(document).on('click', '.ddb-remove-comments',function(){
	//alert("ddb-remove-comments");
})
$(document).on('click', '.ddb-add-comment',function(){
	var ddbid = $(this).data("ddbid"); if (ddbid == undefined) { ddbid = "Null" }
	creat_popup('comment', 'Null', 'Null', 'remove', ddbid, 'Null');
})
//comments-sec end