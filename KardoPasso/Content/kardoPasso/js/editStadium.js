$("form").submit(function(e){
	e.preventDefault();
});
$.extend( true, $.fn.dataTable.defaults, {
	"ordering": true,
	"paging":true,
	"info": true,
	"lengthChange": true,
	"searching": true
} );
$(document).ready(function() {
	$('#sectionsTable').DataTable( {
		"lengthMenu": [[10, 25, -1], [10, 25,  "All"]]
	} );
} );


$(".open-dk-popup").click(function(){
	var update = $(this).data("update");
	var dataid = $(this).data("dataid");
	var key = $(this).data("key");
	if(dataid == undefined || key == undefined)
		return;
	if(update != undefined && update){
		var popupId = $(this).data("id");
		var formId = '#' + popupId.substring(0,popupId.length-5) + '_form';
		$(formId).data("update", true);
		$(formId).data("dataid", dataid);
		if(key == "stadium")
			fillStadium(dataid);
		else if(key == "section")
			fillSection(dataid);
	}
})

function fillStadium(dataid){
	$.ajax({
			type: 'POST',
			url: '/EditStadium/getStadiumFromId',
			data: {
				"stadiumId": dataid
			},
			success: function (json) {
				if(json == "{}"){
					alert("An error occurred!");
					window.location.reload();
				}
				var theItem = JSON.parse(json);
				
				$('#addStadium_name').val(theItem.stadiumName);
				$('#addStadium_sportType').val(parseInt(theItem.sportTypeId));
				$('#addStadium_country').val(theItem.country);
				$('#addStadium_state').val(theItem.state);
				$('#addStadium_city').val(theItem.city);
				$('#addStadium_location').val(theItem.location);
				$('#addStadium_streetAdd').val(theItem.streetAddress);
			},
			error: function () { alert("An error occurred!");}
	 })
}

function fillSection(dataid){
	$.ajax({
			type: 'POST',
			url: '/EditStadium/getSectionFromId',
			data: {
				"sectionId": dataid
			},
			success: function (json) {
				if(json == "{}"){
					alert("An error occurred!");
				}
				var theItem = JSON.parse(json);
				$('#addSection_name').val(theItem.sectionName);
				$('#addSection_capacity').val(theItem.capacity);
				$('#addSection_category').val(theItem.categoryId);
			},
			error: function () { alert("An error occurred!");}
	 })
}


/**************************************************/
$(document).on('click', '.confirmed-dk-popup', function () {
    var operation = $(this).data("operation")
	if (operation == "deleteSection") {
        var dataid = $(this).data("dataid");
        $.ajax({
            type: "POST",
            url: "/EditStadium/deleteSection",
            data: { 
				"dataid": dataid
			},
            success: function (e) {
                if (e == "True") {
					alert("The section have been deleted.");
                    window.location.reload();
                } else {
                    alert("An error occurred!");
                }
            },
            error: function () { alert("An error occurred!") }
        })
    }
})

/**************************************************/
$('#addStadium_form').on('submit', function(e) {
	var name = $('#addStadium_name').val();
	if(name.length < 10 || name.length > 255){
		$('#addStadium_warning').html("Stadium name must be 10 - 255 chars.");
		return;
	}
	var sportTypeId = $('#addStadium_sportType').val();
	var country = $('#addStadium_country').val();
	if(country.length <= 0 || country.length > 100){
		$('#addStadium_warning').html("Country cannot be null.");
		return;
	}
	var state = $('#addStadium_state').val();
	var city = $('#addStadium_city').val();
	if(city.length <= 0 || city.length > 100){
		$('#addStadium_warning').html("City cannot be null.");
		return;
	}
	var location = $('#addStadium_location').val();
	var streetAdd = $('#addStadium_streetAdd').val();
	if(streetAdd.length <= 0 || city.length > 255){
		$('#addStadium_warning').html("Street address cannot be null.");
		return;
	}
	
	
	var update = $('#addStadium_form').data("update");
	var dataid = $('#addStadium_form').data("dataid");
	if(update != undefined && update){
		var formdata = new FormData();
		var asd = $('#addStadium_pic').val();debugger
		if($('#addStadium_pic').val() != null && $('#addStadium_pic').val() != ""){
			var fileInput = document.getElementById('addStadium_pic');
			for (i = 0; i < fileInput.files.length; i++) {
				formdata.append(fileInput.files[i].name, fileInput.files[i]);
			}
			formdata.append("updatePic", true);
		}else{
			formdata.append("updatePic", false);
		}
		
		formdata.append("name", name);
		formdata.append("sportTypeId", sportTypeId);
		formdata.append("country", country);
		formdata.append("state", state);
		formdata.append("city", city);
		formdata.append("location", location);
		formdata.append("streetAdd", streetAdd);
		formdata.append("stadiumId", dataid);
		$.ajax({
			type: 'POST',
			url: '/EditStadium/updateStadium',
			processData: false,
			contentType: false,
			cache: false,
			data: formdata,
			success: function (e) {
				if(e == "True"){
					alert("Stadium have been updated.");
					window.location.reload();
				}
				else{
					alert("An error occurred!");
				}
			},
			error: function () { alert("An error occurred!");}
		})
	}
	else{
		 alert("An error occurred!");
	}
})


