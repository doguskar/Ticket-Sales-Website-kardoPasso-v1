function getAppendUserTickets(json){
	if(json == '{}'){
		$('#myTickets_warning').html("There is no ticket!");
		return;
	}
	var tickets = JSON.parse(json);
	if(tickets.eventId.length == 0)
		$('#myTickets_warning').html("There is no ticket!");
	else{
		var myTicketsHTML = "";
		for(var i = 0; i< tickets.eventId.length; i++){
			var curEventHeader = tickets.hostTeamName[i] + ' - ' + tickets.otherTeamName[i]
			var curStadium = tickets.stadiumName[i];
			var curAmount = tickets.amount[i];
			var curDate = new Date(tickets.eventDate[i]);
			var curSectionName = tickets.sectionName[i];
			var dd = curDate.getDate();
			var mm = curDate.getMonth(); 
			var yyyy = curDate.getFullYear();
			var curDateFormat = dd + ' ' + getMonthName(mm) + ' ' + yyyy + ' ';
			curDateFormat +=	((''+curDate.getHours()).length == 1)? '0' + curDate.getHours(): curDate.getHours() + ':';
			curDateFormat +=	((''+curDate.getMinutes()).length == 1)? '0' + curDate.getMinutes(): curDate.getMinutes();
			var dateDiff = dateDiffHour(curDate, new Date());
			
			myTicketsHTML += '							<div class="ticket-group'; 
			myTicketsHTML += 														(dateDiff < 1)? 'active':''
			myTicketsHTML += '																					">';
			myTicketsHTML += '								<label for="">Event:</label>';
			myTicketsHTML += '								<span>' + curEventHeader + '</span>';
			myTicketsHTML += '								<br />';
			myTicketsHTML += '								<label for="">Where:</label>';
			myTicketsHTML += '								<span>' + curStadium + '</span>';
			myTicketsHTML += '								<br />';
			myTicketsHTML += '								<label for="">Date:</label>';
			myTicketsHTML += '								<span>' + curDateFormat + '</span>';
			myTicketsHTML += '								<br />';
			myTicketsHTML += '								<label for="">Section:</label>';
			myTicketsHTML += '								<span>' + curSectionName + '</span>';
			myTicketsHTML += '								<br />';
			myTicketsHTML += '								<label for="">Amount:</label>';
			myTicketsHTML += '								<span>' + curAmount + '</span>';
			myTicketsHTML += '								<br />';
			myTicketsHTML += '							</div>';
		}
		
		$('#myTicketsContainer').html(myTicketsHTML);
	}
}

function dateDiffHour(date1, date2){
	var diffTime = Math.abs(date2.getTime() - date1.getTime());
	var diffHours = Math.ceil(diffTime / (1000 * 60 * 60)); 
	return diffHours;
}

function getMonthName(m){
	const monthNames = ["January", "February", "March", "April", "May", "June",
  "July", "August", "September", "October", "November", "December"
];
	return monthNames[m];
}




$('#transferMoney_form').on('submit', function() {
	$.ajax({
		type: 'POST',
		url: '/UserProfile/insertMoney',
		data: {
			money: $('#transferMoney_money').val()
		},
		success: function (e) {
			if(e == "True"){
				window.location.reload();
			}
			else{
				alert("An error occurred!");
			}
		},
        error: function () { alert("An error occurred!");}
	})
})
$(document).on('click', '.confirmed-dk-popup', function() {
	if($(this).attr('data-operation') == 'updateDuesDate'){
		$.ajax({
			type: 'POST',
			url: '/UserProfile/updateDues',
			success: function (e) {
				if(e == "True"){
					window.location.reload();
				}
				else{
					alert("An error occurred!");
				}
			},
			error: function () { alert("An error occurred!");}
		})
	}
})

$('#changePassword_form').on('submit', function() {
	$.ajax({
		type: 'POST',
		url: '/UserProfile/updatePassword',
		data: {
			newPassword: $('#changePassword_pass').val()
		},
		success: function (e) {
			if(e == "True"){
				window.location.reload();//***********************************************************************************
			}
			else{
				alert("An error occurred!");
			}
		},
        error: function () { alert("An error occurred!");}
	})
})
$('#updateProfile_form').on('submit', function() {
	$.ajax({
		type: 'POST',
		url: '/UserProfile/updateProfile',
		data: {
			name: $('#updateProfile_name').val(),
			surname: $('#updateProfile_surname').val(),
			mail: $('#updateProfile_mail').val(),
			phone: $('#updateProfile_tel').val(),
			countryCode: $('#updateProfile_countryCode').val()
		},
		success: function (e) {
			if(e == "True"){
				window.location.reload();//***********************************************************************************
			}
			else{
				alert("An error occurred!");
			}
		},
        error: function () { alert("An error occurred!");}
	})
})






