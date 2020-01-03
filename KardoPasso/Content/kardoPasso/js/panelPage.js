$("form").submit(function(e){
	e.preventDefault();
});

let simplepicker = new SimplePicker({
	zIndex: 10000
});
if($( "#addEvent_date" ).length){
	document.getElementById("addEvent_date").addEventListener("click", (e) => {
		simplepicker.open();
	document.getElementsByClassName('simpilepicker-date-picker')[0].classList.toggle('addEvent_date')
	});

	simplepicker.on('submit', (date, readableDate) => {
		document.getElementById("addEvent_date").value = readableDate;
	});
}

/**********************************/
$.extend( true, $.fn.dataTable.defaults, {
	"ordering": true,
	"paging":true,
	"info": true,
	"lengthChange": true,
	"searching": true
} );
$(document).ready(function() {
	$('#eventsTable').DataTable( {
		"lengthMenu": [[5, 10, 20, -1], [5, 10, 20, "All"]]
	} );
} );
$(document).ready(function() {
	$('#usersTable').DataTable( {
		"lengthMenu": [[5, 10, 20, -1], [5, 10, 20, "All"]]
	} );
} );
$(document).ready(function() {
	$('#stadiumsTable').DataTable( {
		"lengthMenu": [[5, 10, 20, -1], [5, 10, 20, "All"]]
	} );
} );
$(document).ready(function() {
	$('#teamsTable').DataTable( {
		"lengthMenu": [[5, 10, 20, -1], [5, 10, 20, "All"]]
	} );
} );
$(document).ready(function() {
	$('#sportTypesTable').DataTable( {
		"lengthMenu": [[5, 10, 20, -1], [5, 10, 20, "All"]]
	} );
} );
	
/**********************************************/	
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
		if(key == "event")
			fillEvent(dataid);
		/*else if(key == "stadium")
			fillStadium(dataid);*/
		else if(key == "team")
			fillTeam(dataid);
		else if(key == "sportType")
			fillSportType(dataid);
		else if(key == "user")
			fillUser(dataid);
	}
})

function fillEvent(dataid){
	$.ajax({
			type: 'POST',
			url: '/PagePanel/getEventFromId',
			data: {
				"eventId": dataid
			},
			success: function (json) {
				if(json == "{}"){
					alert("An error occurred!");
					window.location.reload();
				}
				var theEvent = JSON.parse(json);
				
				$('#addEvent_hostTeam').val(parseInt(theEvent.hostTeamId));
				$('#addEvent_otherTeam').val(parseInt(theEvent.otherTeamId));
				$('#addEvent_stadium').val(parseInt(theEvent.stadiumId));
				$('#addEvent_date').val(theEvent.eventDate);
				$('#addEvent_description').val(theEvent.eventDescription);
				$('#addEvent_price').val(parseFloat(theEvent.minTicketPrice));
			},
			error: function () { alert("An error occurred!");}
		})
}
/*function fillStadium(dataid){
	$.ajax({
			type: 'POST',
			url: '/PagePanel/getStadiumFromId',
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
}*/
function fillTeam(dataid){
	$.ajax({
			type: 'POST',
			url: '/PagePanel/getTeamFromId',
			data: {
				"teamId": dataid
			},
			success: function (json) {
				if(json == "{}"){
					alert("An error occurred!");
					window.location.reload();
				}
				var theItem = JSON.parse(json);
				
				$('#addTeam_name').val(theItem.teamName);
				$('#addTeam_sportType').val(parseInt(theItem.sportTypeId));
			},
			error: function () { alert("An error occurred!");}
	 })
}
function fillSportType(dataid){
	$.ajax({
			type: 'POST',
			url: '/PagePanel/getSportTypeFromId',
			data: {
				"sportTypeId": dataid
			},
			success: function (json) {
				if(json == "{}"){
					alert("An error occurred!");
					window.location.reload();
				}
				var theItem = JSON.parse(json);
				
				$('#addSportType_name').val(theItem.sportType);
			},
			error: function () { alert("An error occurred!");}
	 })
}
function fillUser(dataid){
	$.ajax({
			type: 'POST',
			url: '/PagePanel/getUserFromId',
			data: {
				"userId": dataid
			},
			success: function (json) {
				if(json == "{}"){
					alert("An error occurred!");
					window.location.reload();
				}
				var theItem = JSON.parse(json);
				
				$('#updateUser_role').val(theItem.roleId);
			},
			error: function () { alert("An error occurred!");}
	 })
}
/**************************************************/
$(document).on('click', '.confirmed-dk-popup', function () {
    var operation = $(this).data("operation")
	if (operation == "deleteEvent") {
        var dataid = $(this).data("dataid");
        $.ajax({
            type: "POST",
            url: "/PagePanel/deleteEvent",
            data: { 
				"dataid": dataid
			},
            success: function (e) {
                if (e == "True") {
					alert("The event have been deleted.");
                    window.location.reload();
                } else {
                    alert("An error occurred!");
                }
            },
            error: function () { alert("An error occurred!") }
        })
    }
	else if(operation == "deleteStadium"){
		var dataid = $(this).data("dataid");
        $.ajax({
            type: "POST",
            url: "/PagePanel/deleteStadium",
            data: { 
				"dataid": dataid
			},
            success: function (e) {
                if (e == "True") {
					alert("The stadium have been deleted.");
                    window.location.reload();
                } else {
                    alert("An error occurred!");
                }
            },
            error: function () { alert("An error occurred!") }
        })
	}
	else if(operation == "deleteUser"){
		var dataid = $(this).data("dataid");
        $.ajax({
            type: "POST",
            url: "/PagePanel/deleteUser",
            data: { 
				"dataid": dataid
			},
            success: function (e) {
                if (e == "True") {
					alert("The user have been deleted.");
                    window.location.reload();
                } else {
                    alert("An error occurred!");
                }
            },
            error: function () { alert("An error occurred!") }
        })
	}
	else if(operation == "deleteTeam"){
		var dataid = $(this).data("dataid");
        $.ajax({
            type: "POST",
            url: "/PagePanel/deleteTeam",
            data: { 
				"dataid": dataid
			},
            success: function (e) {
                if (e == "True") {
					alert("The team have been deleted.");
                    window.location.reload();
                } else {
                    alert("An error occurred!");
                }
            },
            error: function () { alert("An error occurred!") }
        })
	}
	else if(operation == "deleteSportType"){
		var dataid = $(this).data("dataid");
        $.ajax({
            type: "POST",
            url: "/PagePanel/deleteSportType",
            data: { 
				"dataid": dataid
			},
            success: function (e) {
                if (e == "True") {
					alert("The sport type have been deleted.");
                    window.location.reload();
                } else {
                    alert("An error occurred!");
                }
            },
            error: function () { alert("An error occurred!") }
        })
	}
	
	
})

/**********************************************/
$('#addEvent_form').on('submit', function(e) {
	var hTeamId = $('#addEvent_hostTeam').val();
	var hType = $('#addEvent_hostTeam').children("option:selected").data("sporttype");
	var oTeamId = $('#addEvent_otherTeam').val();
	var oType = $('#addEvent_otherTeam').children("option:selected").data("sporttype");
	
	if(oType != hType){
		$('#addEvent_warning').html("Teams' sport type must be same");
		return;
	}
	if(hTeamId == oTeamId){
		$('#addEvent_warning').html("Teams must be different");
		return;
	}
	
	var stadiumId = $('#addEvent_stadium').val();
	var eventDate = $('#addEvent_date').val();
	if(eventDate == 'Click to Select'){
		$('#addEvent_warning').html("Select date");
		return;
	}
	var today = new Date();
	var eDate = new Date(eventDate);
	var diff= dateDiffHours(today, eDate);
	if(diff < 24){
		$('#addEvent_warning').html("The date should be at least a day after.");
		return;
	}
	
	
	var description = $('#addEvent_description').val();
	if(description.length < 100){
		$('#addEvent_warning').html("Description must be least 100 chars.");
		return;
	}
	var price = $('#addEvent_price').val();
	if(price == ""){
		$('#addEvent_warning').html("Enter min ticket price.");
		return;
	}
	
	
	var update = $('#addEvent_form').data("update");
	var dataid = $('#addEvent_form').data("dataid");
	if(update != undefined && update){
		if(dataid == undefined){
			alert("DataId error!");
			return;
		}
		
		
		var formdata = new FormData();
		if($('#addEvent_pic').val() != null && $('#addEvent_pic').val() != ""){
			var fileInput = document.getElementById('addEvent_pic');
			for (i = 0; i < fileInput.files.length; i++) {
				formdata.append(fileInput.files[i].name, fileInput.files[i]);
			}
			formdata.append("updatePic", true);
		}else{
			formdata.append("updatePic", false);
		}
		formdata.append("hTeamId", hTeamId);
		formdata.append("oTeamId", oTeamId);
		formdata.append("stadiumId", stadiumId);
		formdata.append("eventDate", eventDate);
		formdata.append("description", description);
		formdata.append("price", price);
		formdata.append("eventId", dataid);
		
		$.ajax({
			type: 'POST',
			url: '/PagePanel/updateEvent',
			processData: false,
			contentType: false,
			cache: false,
			data: formdata,
			success: function (e) {
				if(e == "True"){
					alert("Event have been updated.");
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
		if($('#addEvent_pic').val() == null || $('#addEvent_pic').val() == ""){
			$('#addEvent_warning').html("Select an image.");
			return;
		}
			
		var formdata = new FormData();
		var fileInput = document.getElementById('addEvent_pic');
		for (i = 0; i < fileInput.files.length; i++) {
			formdata.append(fileInput.files[i].name, fileInput.files[i]);
		}
		formdata.append("hTeamId", hTeamId);
		formdata.append("oTeamId", oTeamId);
		formdata.append("stadiumId", stadiumId);
		formdata.append("eventDate", eventDate);
		formdata.append("description", description);
		formdata.append("price", price);
		
		$.ajax({
			type: 'POST',
			url: '/PagePanel/insertEvent',
			processData: false,
			contentType: false,
			cache: false,
			data: formdata,
			success: function (e) {
				if(e == "True"){
					alert("Event have been added.");
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
	if(update != undefined && update){
		
	}
	else{
		if($('#addStadium_pic').val() == null || $('#addStadium_pic').val() == ""){
			$('#addStadium_warning').html("Select an image.");
			return;
		}
		var formdata = new FormData();
		var fileInput = document.getElementById('addStadium_pic');
		for (i = 0; i < fileInput.files.length; i++) {
			formdata.append(fileInput.files[i].name, fileInput.files[i]);
		}
		formdata.append("name", name);
		formdata.append("sportTypeId", sportTypeId);
		formdata.append("country", country);
		formdata.append("state", state);
		formdata.append("city", city);
		formdata.append("location", location);
		formdata.append("streetAdd", streetAdd);
		
		$.ajax({
			type: 'POST',
			url: '/PagePanel/insertStadium',
			processData: false,
			contentType: false,
			cache: false,
			data: formdata,
			success: function (e) {
				if(e == "True"){
					alert("Stadium have been added.");
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
$('#addTeam_form').on('submit', function() {
	var name = $('#addTeam_name').val();
	if(name.length < 3 || name.length > 255){
		$('#addTeam_warning').html("Team's name contains 3-255 chars.");
		return;
	}
	var sportTypeId = $('#addTeam_sportType').val();
	var update = $('#addTeam_form').data("update");
	var dataid = $('#addTeam_form').data("dataid");
	if(update != undefined && update){
		if(dataid == undefined){
			alert("DataId error!");
			return;
		}
		$.ajax({
				type: 'POST',
				url: '/PagePanel/updateTeam',
				data: {
					"name": name,
					"sportTypeId": sportTypeId,
					"teamId": dataid
				},
				success: function (e) {
					if(e == "True"){
						alert("Team have been updated.");
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
		$.ajax({
				type: 'POST',
				url: '/PagePanel/insertTeam',
				data: {
					"name": name,
					"sportTypeId": sportTypeId
				},
				success: function (e) {
					if(e == "True"){
						alert("Team have been added.");
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
$('#addSportType_form').on('submit', function() {
	var name = $('#addSportType_name').val();
	if(name.length < 3 || name.length > 50){
		$('#addSportType_warning').html("Sport type contains 3-50 chars.");
		return;
	}
	var update = $('#addSportType_form').data("update");
	var dataid = $('#addSportType_form').data("dataid");
	if(update != undefined && update){
		if(dataid == undefined){
			alert("DataId error!");
			return;
		}
		$.ajax({
				type: 'POST',
				url: '/PagePanel/updateSportType',
				data: {
					"name": name,
					"sportTypeId": dataid
				},
				success: function (e) {
					if(e == "True"){
						alert("Sport type have been updated.");
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
		$.ajax({
				type: 'POST',
				url: '/PagePanel/insertSportType',
				data: {
					"name": name
				},
				success: function (e) {
					if(e == "True"){
						alert("Sport type have been added.");
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
$('#updateUser_form').on('submit', function() {
	var dataid = $('#updateUser_form').data("dataid");
	var roleId = $('#updateUser_role').val();
	$.ajax({
			type: 'POST',
			url: '/PagePanel/updateUser',
			data: {
				"roleId": roleId,
				"userId": dataid
			},
			success: function (e) {
				if(e == "True"){
					alert("User have been added.");
					window.location.reload();
				}
				else{
					alert("An error occurred!");
				}
			},
			error: function () { alert("An error occurred!");}
		})
})
/*************************************************/
function dateDiffHours(date1, date2){
	var diffTime = date2.getTime() - date1.getTime();
	var diffHours = Math.ceil(diffTime/(1000*60*60)); 
	return diffHours;
}
function getMonthName(m){
	const monthNames = ["January", "February", "March", "April", "May", "June",
  "July", "August", "September", "October", "November", "December"
];
	return monthNames[m];
}

/*****************************************************************/
function showStatistics(){
	
	/********************************************** TOP TEAMS ***************************************************/
	$.ajax({
            type: 'POST',
            url: '/PagePanel/getTopTeams',
            success: function (json) {
                if (json == "{}") {
                    alert("An error occurred!");
                    return;
                }
                var topTeams = JSON.parse(json);
                var amount = new Array();
                for (var i = 0; i < topTeams.amount.length; i++)
                    amount.push(parseInt(topTeams.amount[i]));
                Highcharts.chart('HC_topTeams', {
                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: 'Teams that its tickets were sold the most'
                    },
                    xAxis: {
                        categories: topTeams.teamName,
                        title: {
                            text: null
                        }
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: 'Total Number Of Tickets',
                            align: 'high'
                        },
                        labels: {
                            overflow: 'justify'
                        }
                    },
                    tooltip: {
                        valueSuffix: ' tickets'
                    },
                    plotOptions: {
                        bar: {
                            dataLabels: {
                                enabled: true
                            }
                        }
                    },
                    credits: {
                        enabled: false
                    },
                    series: [{
                        name: 'Total Number Of Tickets',
                        data: amount
                    }]
                });
            },
            error: function () { alert("An error occurred!"); }
        });
        
		/********************************************** TOP EVENTS ***************************************************/
		
		$.ajax({
            type: 'POST',
            url: '/PagePanel/getTopEvents',
            success: function (json) {
                if (json == "{}") {
                    alert("An error occurred!");
                    return;
                }
                var topEvents = JSON.parse(json);
                var events = new Array();
                var revenues = new Array();

                for (var i = 0; i < topEvents.eventId.length; i++){
                    events.push(
                        topEvents.hostTeamName[i]
                        + '<br />' +
                        topEvents.otherTeamName[i]
                        + '<br />' +
                        topEvents.eventDate[i]
                        );
                    revenues.push(parseInt(topEvents.totalRevenue[i]))
                }

                Highcharts.chart('HC_topEvents', {
                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: 'The most revenue-generating events'
                    },
                    xAxis: {
                        categories: events,
                        title: {
                            text: null
                        }
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: 'Revenue',
                            align: 'high'
                        },
                        labels: {
                            overflow: 'justify'
                        }
                    },
                    tooltip: {
                        valueSuffix: ' $'
                    },
                    plotOptions: {
                        bar: {
                            dataLabels: {
                                enabled: true
                            }
                        }
                    },
                    plotOptions: {
                        series: {
                            dataLabels: {
                                enabled: true,
                                format: '{point.y}$'
                            }
                        }
                    },
                    credits: {
                        enabled: false
                    },
                    series: [{
                        name: 'Revenue',
                        data: revenues
                    }]
                });

            },
            error: function () { alert("An error occurred!"); }
        });
		
		/********************************************** TOP STADIUMS ***************************************************/
		
		$.ajax({
            type: 'POST',
            url: '/PagePanel/getTopStadiums',
            success: function (json) {
                if (json == "{}") {
                    alert("An error occurred!");
                    return;
                }
                var topStadiums = JSON.parse(json);
                var eventNumbers = new Array();

                for (var i = 0; i < topStadiums.stadiumId.length; i++) {
                    eventNumbers.push(parseInt(topStadiums.totalEvents[i]))
                }



                Highcharts.chart('HC_topStadiums', {
                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: 'The stadium that hosts the most events in the last year.'
                    },
                    xAxis: {
                        categories: topStadiums.stadiumName,
                        title: {
                            text: null
                        }
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: 'Number of events',
                            align: 'high'
                        },
                        labels: {
                            overflow: 'justify'
                        }
                    },
                    tooltip: {
                        valueSuffix: ''
                    },
                    plotOptions: {
                        bar: {
                            dataLabels: {
                                enabled: true
                            }
                        }
                    },
                    credits: {
                        enabled: false
                    },
                    series: [{
                        name: 'Number of events',
                        data: eventNumbers
                    }]
                });
            },
            error: function () { alert("An error occurred!"); }
        });
		
		/********************************************** EVENTS GROUP BY YEARS AND MONTH ***************************************************/
		
		$.ajax({
            type: 'POST',
            url: '/PagePanel/getEventsDateGroups',
            success: function (json) {
                if (json == "{}") {
                    alert("An error occurred!");
                    return;
                }
                var EventsDateGroups = JSON.parse(json);
                var years = new Array();
                var months = new Array();
                var monthsData = new Array();
                var totalYear = 0;
                var curYear = "";
                for (var i = 0; i < EventsDateGroups.year.length; i++) {
                    if(EventsDateGroups.year[i] != curYear){
                        if (curYear != "") {
                            var subJson = {
                                name: curYear,
                                y: parseInt(totalYear),
                                drilldown: ('Y' + curYear)
                            }
                            years.push(subJson);
                            months.push(
                            {
                                name: "Months",
                                id: ('Y' + curYear),
                                data: monthsData
                            });
                            monthsData = new Array();
                        }
                        totalYear = parseInt(EventsDateGroups.count[i]);
                        curYear = EventsDateGroups.year[i];
                        monthsData.push([getMonthName(parseInt(EventsDateGroups.month[i]) - 1), parseInt(EventsDateGroups.count[i])]);
                        
                    }
                    else{
                        totalYear += parseInt(EventsDateGroups.count[i]);
                        monthsData.push([getMonthName(parseInt(EventsDateGroups.month[i]) - 1), parseInt(EventsDateGroups.count[i])]);
                    }
                }
                var subJson = {
                    name: curYear,
                    y: parseInt(totalYear),
                    drilldown: ('Y' + curYear)
                }
                years.push(subJson);
                months.push(
                {
                    name: "Months",
                    id: ('Y' + curYear),
                    data: monthsData
                });


                Highcharts.chart('HC_numOfEvents', {
                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: 'Number Of Events by Years and Month'
                    },
                    subtitle: {
                        text: 'Click the columns to view number of events by month.'
                    },
                    xAxis: {
                        type: 'category'
                    },
                    yAxis: {
                        title: {
                            text: 'Total number of events'
                        }

                    },
                    legend: {
                        enabled: false
                    },
                    plotOptions: {
                        series: {
                            borderWidth: 0,
                            dataLabels: {
                                enabled: true,
                                format: '{point.y}'
                            }
                        }
                    },

                    tooltip: {
                        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
                        pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y}</b><br/>'
                    },

                    series: [
                      {
                          name: "Years",
                          colorByPoint: true,
                          data: years
                      }
                    ],
                    drilldown: {
                        series: months
                    }
                });
            },
            error: function () { alert("An error occurred!");}
        });
		
		
}


