//SIDEBAR
$(".sidebar-close").click(function(){
	if($("#dk-sidebar-menu").css("left") == "-320px"){
		$("#dk-sidebar-menu").css({left:'0px'});
		$("#dk-sidebar-menu-close").css({left:'0px'});
		if($("#dk-sidebar-menu").hasClass("sidebar-fixed")){
			$("#content-section").addClass('active');
			$("#footer-section").addClass('active');
			$("#search-box-wide").addClass('fixed');
		}
		Cookies.set('dk-sidebar-statu', 'active');
		
	}else{
		$("#dk-sidebar-menu").css({left:'-320px'});
		$("#dk-sidebar-menu-close").css({left:'-100%'});
		if($("#dk-sidebar-menu").hasClass("sidebar-fixed")){
			$("#content-section").removeClass('active');
			$("#footer-section").removeClass('active');
			$("#search-box-wide").removeClass('fixed');
		}
		Cookies.set('dk-sidebar-statu', 'non-active');
	}
})

$("#dk-sidebar-menu-off-fixed").click(function(){
	if($(this).hasClass("fixed")){
		$("#dk-sidebar-menu").removeClass("sidebar-fixed");
		$("#dk-sidebar-menu-close").removeClass("fixed");
		$("#content-section").removeClass("fixed-sidebar");
		$("#footer-section").removeClass("fixed-sidebar");
		$("#search-box-wide").removeClass("fixed");
		$(this).removeClass("fixed");
		Cookies.set('dk-sidebar-type', 'non-fixed');
	}else{
		$("#dk-sidebar-menu").addClass("sidebar-fixed");
		$("#dk-sidebar-menu-close").addClass("fixed");
		$("#content-section").addClass("fixed-sidebar");
		$("#footer-section").addClass("fixed-sidebar");
		$("#content-section").addClass("active");
		$("#footer-section").addClass("active");
		$("#search-box-wide").addClass("fixed");
		$(this).addClass("fixed");
		Cookies.set('dk-sidebar-type', 'fixed');
	}
})

$(document).ready(function(){
	var cookieControl = Cookies.get('dk-sidebar-type');
	if(cookieControl == undefined){
		/*Cookies.set('dk-sidebar-type', 'fixed', { expires: 365 });
		Cookies.set('dk-sidebar-statu', 'active', { expires: 365 });
		
		$("#dk-sidebar-menu").addClass("sidebar-fixed");
		$("#dk-sidebar-menu-close").addClass("fixed");
		$("#content-section").addClass("fixed-sidebar");
		$("#footer-section").addClass("fixed-sidebar");
		$("#search-box-wide").addClass("fixed");
		$("#dk-sidebar-menu-off-fixed").addClass("fixed");
		$("#content-section").addClass('active');
		$("#footer-section").addClass('active');
		$("#search-box-wide").addClass('fixed');
		$("#dk-sidebar-menu").css({left:'0px'});
		$("#dk-sidebar-menu-close").css({left:'0px'});*/
		
	}else{
		var type = Cookies.get('dk-sidebar-type');
		var statu = Cookies.get('dk-sidebar-statu');
		
		if(type == "fixed"){
			$("#dk-sidebar-menu").addClass("sidebar-fixed");
			$("#dk-sidebar-menu-close").addClass("fixed");
			$("#content-section").addClass("fixed-sidebar");
			$("#footer-section").addClass("fixed-sidebar");
			$("#dk-sidebar-menu-off-fixed").addClass("fixed");
		}
		if(type == "fixed" && statu == "active"){
			$("#dk-sidebar-menu").css({left:'0px'});
			$("#dk-sidebar-menu-close").css({left:'0px'});
			$("#content-section").addClass('active');
			$("#footer-section").addClass('active');
			$("#search-box-wide").addClass('fixed');	
		}
	}
})
//SIDEBAR

/*dropdown*/
$(".dk-dropdown").click(function(){
	var dropdownId;
	dropdownId = "#"+$(".dk-dropdown.active").data("dropdown");
	if(dropdownId != '#undefined' ){
		if($(".dk-dropdown.active").data("dropdown") != $(this).data("dropdown")){
			$(dropdownId).removeClass("active");
			$(".dk-dropdown.active").removeClass("active");
		}
	}
	dropdownId = "#"+$(this).data("dropdown");
	if($(dropdownId).hasClass("active")){
		$(dropdownId).removeClass("active");
		$(this).removeClass("active");
		click = 0;
	}else{
		$(dropdownId).addClass("active");
		$(this).addClass("active");
		click = 1;
	}
})
$(document).click(function(){
	dropdownId = "#"+$(".dk-dropdown.active").data("dropdown");
	if(dropdownId != '#undefined'){
		if(click == 1){
			click++;
		}else{
			$(dropdownId).removeClass("active");
			$(".dk-dropdown").removeClass("active");
			click = 0;
		}
	}
})
/*dropdown end*/

/*close-btn*/
$(".close-btn").click(function(){
	var id = "#"+ $(this).data("id");
	$(id).remove();
})
/*close-btn end*/
/*dk-popup */
$(".close-dk-popup").click(function(){
	var id = "#"+ $(this).data("id");
	$(id).removeClass("active");
})
/*$(".closeable-popup").click(function(){
	$(this).removeClass("active");
}) 
SIKINTILI*/
$(".open-dk-popup").click(function(){
	var id = "#"+ $(this).data("id");
	$(id).addClass("active");
})
/*dk-popup end*/

/*dk-accordion-sec*/
/**/$(".dk-acc-header").click(function(){
	var content = this.nextElementSibling;
	if($(this).hasClass("active")){
		$(this).removeClass("active");
      content.style.maxHeight = null;
	}else{
		$(this).addClass("active");
      content.style.maxHeight = content.scrollHeight + "px";
	}
})
/*dk-accordion-sec END*/


//popup is for ad //
function dk_popup_ad_onoff(){
	var deger = document.getElementsByClassName("dk-popup-ad")[0].style.display;
	if(deger=="block"){
		document.getElementsByClassName("dk-popup-ad")[0].style.display = "none";	
		document.getElementById("dk-popup-ad-close").innerHTML='<i class="fa fa-times" onClick="work_sec()"></i>';
	}
	else{
		document.getElementsByClassName("dk-popup-ad")[0].style.display = "block";
	}
}
var wait=5;
var islem;
var clicked=0;
function work_sec(){
	if(clicked == 0){
		islem = setInterval( function(){popup_time()},1000 )
		clicked++;
	}
	
}
function popup_time() {
	if(wait>0){
		document.getElementById("dk-popup-ad-close").innerHTML='<i class="fa">'+wait+'</i>';
		wait--;
	}
	else{
		clearInterval(islem);
		document.getElementById("dk-popup-ad-close").innerHTML='<i class="fa fa-times" onClick="dk_popup_ad_onoff()"></i>';
		wait=5;
		clicked = 0;
	}
}
//popup is for ad  end //

var click = 0;
function display_changing(id,overlay){
	var y = document.getElementById(id).style.opacity; if(y == ""){	y = "0";	}
	if(overlay == 'true'){	var x = document.getElementById("dk-sc-cover").style.opacity;	if(x == ""){	x = "0";	}	}
	var z = document.getElementById(id).style.top;
	
	if(overlay == 'true'){
		if(y == 1){
			if(click == 1){
				click++;
				if(x == "0.6"){
					document.getElementById("dk-sc-cover").style.opacity="0";
					document.getElementById("dk-sc-cover").style.zIndex="-998";
				}
				document.getElementById(id).style.opacity = 0;
				setTimeout(function(){document.getElementById(id).style.zIndex = "-999"},300);
				setTimeout(function(){click = 0},300);
			}
			else if(click == 0){
				click++;
				click++;
				if(x == "0.6"){
					document.getElementById("dk-sc-cover").style.opacity="0";
					document.getElementById("dk-sc-cover").style.zIndex="-998";
				}
				document.getElementById(id).style.opacity = 0;
				setTimeout(function(){document.getElementById(id).style.zIndex = "-999"},300);
				setTimeout(function(){click = 0},300);
			}
		}
		else{
			if(click == 0){
				click++;
				if(x == "0"){
					document.getElementById("dk-sc-cover").style.opacity="0.6";
					document.getElementById("dk-sc-cover").style.zIndex="998";
				}
				document.getElementById(id).style.opacity = 1;
				document.getElementById(id).style.zIndex  = "999";
			}
		}
	}
	else{
		var control = document.getElementById(id).style.display;
		if(control == 'none'){
			document.getElementById("resultForDisplayChanging").innerHTML = '<span id="dk-sc-cover-transparent" style="opacity:0; z-index:-998;" onClick="display_changing(\''+id+'\', \'false}\')"></span>';
			document.getElementById("dk-sc-cover-transparent").style.zIndex="998";
			document.getElementById("dk-sc-cover-transparent").style.opacity="1";
			document.getElementById("dk-sc-cover-transparent").style.display = 'block';
			document.getElementById(id).style.zIndex  = "999";
			document.getElementById(id).style.display = 'block';
		}
		else{
			document.getElementById("dk-sc-cover-transparent").style.display = 'none';
			document.getElementById("dk-sc-cover-transparent").style.opacity="0";
			document.getElementById(id).style.display = 'none';
		}
	}
	
}

/*
*/

//dk-text-editor

function dk_editor(x){
	switch(x){
		case 'header' : document.getElementById("dk-text-editor-t").innerHTML += '<h>BURAYA_YAZINIZ<h />'; break;
		case 'bold' : document.getElementById("dk-text-editor-t").innerHTML += '<b>BURAYA_YAZINIZ<b />'; break;
		case 'font-1' : document.getElementById("dk-text-editor-t").innerHTML += '<f1>BURAYA_YAZINIZ<f1 />'; break;
		case 'font-2' : document.getElementById("dk-text-editor-t").innerHTML += '<f2>BURAYA_YAZINIZ<f2 />'; break;
		case 'strikethrough' : document.getElementById("dk-text-editor-t").innerHTML += '<s>BURAYA_YAZINIZ<s />'; break;
		case 'underline' : document.getElementById("dk-text-editor-t").innerHTML += '<u>BURAYA_YAZINIZ<u />'; break;
		default : alert("Hata Oluştu"); break;
		
	}
}
//dk-text-editor end

function changeDisplay(id,first,second){
	var x  = document.getElementById(id).style.display;
	
	if(x == first){
		document.getElementById(id).style.display = second;
	}
	else{
		document.getElementById(id).style.display = first;
	}
}

//idarelik
$(".search-box-btn").click(function(){
	if($("#search-box-wide").hasClass("active")){
		$("#search-box-wide").removeClass("active");
	}else{
		$("#search-box-wide").addClass("active");
	}
	
})

//input file
/*
'use strict';

;( function ( document, window, index )
{
	var inputs = document.querySelectorAll( '.inputfile' );
	Array.prototype.forEach.call( inputs, function( input )
	{
		var label	 = input.nextElementSibling,
			labelVal = label.innerHTML;

		input.addEventListener( 'change', function( e )
		{
			var fileName = '';
			if( this.files && this.files.length > 1 )
				fileName = ( this.getAttribute( 'data-multiple-caption' ) || '' ).replace( '{count}', this.files.length );
			else
				fileName = e.target.value.split( '\\' ).pop();

			if( fileName )
				label.querySelector( 'span' ).innerHTML = fileName;
			else
				label.innerHTML = labelVal;
		});

		// Firefox bug fix
		input.addEventListener( 'focus', function(){ input.classList.add( 'has-focus' ); });
		input.addEventListener( 'blur', function(){ input.classList.remove( 'has-focus' ); });
	});
}( document, window, 0 ));*/
//input file end