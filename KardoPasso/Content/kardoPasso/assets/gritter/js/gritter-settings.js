$(".gritter-notification").click(function(){
	$.gritter.add({
		// (string | mandatory) the heading of the notification
		title: $(this).data("title"),
		// (string | mandatory) the text inside the notification
		text: $(this).data("content"),
		// (string | optional) the image to display on the left
		image: $(this).data("image"),
		// (bool | optional) if you want it to fade out on its own or just sit there
		sticky: $(this).data("sticky"),
		// (int | optional) the time you want it to be alive for before fading out
		time: $(this).data("time")
	});

	return false;
})

function run_gritter(titleG,contentG,imageG,stickyG,timeG){
	var stickyB = false;
	if(stickyG == "true")
		stickyB = true;
	
	$.gritter.add({
	title: titleG,
	text: contentG,
	image: imageG,
	sticky: stickyB,
	time: timeG
	});

	return false;
}
/*$(document).ready(function(){
	run_gritter("","Deneme notification","","false","1000");
})*/