let simplepicker = new SimplePicker({
  zIndex: 10
});

document.getElementById("startDate").addEventListener("click", (e) => {
  simplepicker.open();
  document.getElementsByClassName('simpilepicker-date-picker')[0].classList.toggle('startDate')
});
document.getElementById("endDate").addEventListener("click", (e) => {
  simplepicker.open();
});

/*********************************************************************/
simplepicker.on('submit', (date, readableDate) => {
	var eCL = document.getElementsByClassName('simpilepicker-date-picker')[0].classList;
	if(eCL.contains('startDate')){
		eCL.toggle('startDate');
		document.getElementById("startDate").value = readableDate;
		getFilteredEvents();
	}else{
		document.getElementById("endDate").value = readableDate;
		getFilteredEvents();
	}
});
/*********************************************************************/

$('#eventsCitiesSelect').on('change', function() {
  getFilteredEvents();
});
$('#eventsTeamsSelect').on('change', function() {
  getFilteredEvents();
});


$('#eventCategoryToday').on('click', function() {
	var today = new Date();
	var dd = today.getDate();
	var mm = today.getMonth(); 
	var yyyy = today.getFullYear();
	
	document.getElementById("startDate").value = dd + ' ' + getMonthName(mm) + ' ' + yyyy + ' 00:00 AM';
	document.getElementById("endDate").value = dd + ' ' + getMonthName(mm) + ' ' + yyyy + ' 11:59 PM';
	getFilteredEvents();
});
$('#eventCategoryTomorrow').on('click', function() {
	var today = new Date();debugger;
	today = today.addDays(1);
	
	var dd = today.getDate();
	var mm = today.getMonth(); 
	var yyyy = today.getFullYear();
	
	document.getElementById("startDate").value = dd + ' ' + getMonthName(mm) + ' ' + yyyy + ' 00:00 AM';
	document.getElementById("endDate").value = dd + ' ' + getMonthName(mm) + ' ' + yyyy + ' 11:59 PM';
	getFilteredEvents();
});

$('#eventCategoryWeek').on('click', function() {
	var today = new Date();
	var date = new Date();
	var today = new Date();
	var dd = today.getDate();
	var mm = today.getMonth(); 
	var yyyy = today.getFullYear();
	document.getElementById("startDate").value = dd + ' ' + getMonthName(mm) + ' ' + yyyy + ' 00:00 AM';
	
	var dayOfWeek = today.getDay();debugger;
	today = today.addDays(7-dayOfWeek);
	dd = today.getDate();
	mm = today.getMonth(); 
	yyyy = today.getFullYear();
	
	document.getElementById("endDate").value = dd + ' ' + getMonthName(mm) + ' ' + yyyy + ' 11:59 PM';
	getFilteredEvents();
});

$('#eventCategoryMonth').on('click', function() {
	var today = new Date();
	var date = new Date();
	var today = new Date();
	var dd = today.getDate();
	var mm = today.getMonth(); 
	var yyyy = today.getFullYear();
	document.getElementById("startDate").value = dd + ' ' + getMonthName(mm) + ' ' + yyyy + ' 00:00 AM';
	
	var lastDayOfMonth = new Date(date.getFullYear(), date.getMonth() + 1, 0);
	today = today.addDays(lastDayOfMonth.getDate()-dd);
	dd = today.getDate();
	mm = today.getMonth(); 
	yyyy = today.getFullYear();
	
	document.getElementById("endDate").value = dd + ' ' + getMonthName(mm) + ' ' + yyyy + ' 11:59 PM';
	getFilteredEvents();
});

Date.prototype.addDays = function(days) {
	var date = new Date(this.valueOf());
	date.setDate(date.getDate() + days);
	return date;
}


function getFilteredEvents(){
	$('#dk-loading-sec').addClass('active');
	$.ajax({
		type: 'POST',
		url: '/Default/getFilteredEvents',
		data: {
			city: $('#eventsCitiesSelect').val(),
			team: $('#eventsTeamsSelect').val(),
			startDate: $('#startDate').val(),
			endDate: $('#endDate').val()
		},
		success: function (e) { 
			reloadEvents(e);
			$('#dk-loading-sec').removeClass('active');
		},
        error: function () { alert("An error occurred!");$('#dk-loading-sec').removeClass('active'); }
	})
}

function reloadEvents(json){
	if(json == '{}'){
		$('#eventsContainer').html('<h1>There is no result!</h1>');
		return;
	}debugger
	var events = JSON.parse(json)
	$('#eventsContainer').html("");
	var eventsHTML = "";
	for (var i = 0; i < events.eventId.length; i++) {
		
		var str = events.eventDate[i].split(' ')[0]; 
		var eventDate = new Date(events.eventDate[i].split(' ')[0]);
		var eventDay = eventDate.getDate();
		eventDay = ((''+eventDay).length == 1)? '0'+eventDay : eventDay;
		
		var curEventPic = events.eventPic[i];
		var curEventHostTeam = events.hostTeamName[i];
		var curEventOtherTeam = events.otherTeamName[i];
		var curEventHeader = curEventHostTeam + ' - ' + curEventOtherTeam;
		
		eventsHTML += '   <a href="/EventDetail?eventId=' + events.eventId[i] + '">';
		eventsHTML += '		<div id="event-' + events.eventId[i] + '" class="eventMainBox">';
		eventsHTML += '            <div class="imgContainer"><img src="/Content/kardoPasso/img/uploads/' + curEventPic + '" alt="" /></div>';
		eventsHTML += '            <div class="eMBTxtBox-overlay">';
		eventsHTML += '                <div class="eMBTxtBox">';
		eventsHTML += '                    <div class="eMBTxtBox-left">' + curEventHeader + '</div>';
		eventsHTML += '                    <div class="eMBTxtBox-right">';
		eventsHTML += '                        <div class="txtBoxL-Day">' + eventDay + '</div>';
		eventsHTML += '                        <div class="txtBoxL-Month">' + getMonthMMM(eventDate.getMonth()) + '</div>';
		eventsHTML += '                   </div>';
		eventsHTML += '               </div>';
		eventsHTML += '           </div>';
		eventsHTML += '   	</div>';
		eventsHTML += '   </a>';
	}
	$('#eventsContainer').html(eventsHTML);
}

function getMonthMMM(m){
	var monthNames = [
		"Jan", "Feb", "Mar",
		"Apr", "May", "Jun", "Jul",
		"Aug", "Sep", "Oct",
		"Nov", "Dec"
	  ];
	return monthNames[m];
}
function getMonthName(m){
	const monthNames = ["January", "February", "March", "April", "May", "June",
  "July", "August", "September", "October", "November", "December"
];
	return monthNames[m];
}

























