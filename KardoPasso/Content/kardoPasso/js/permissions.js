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
	$('#permissionsTable').DataTable( {
		"lengthMenu": [[5, 10, 20, -1], [5, 10, 20, "All"]]
	} );
} );

/**************************************************/
$(document).on('change', '.roleCheck', function () {
    var column = $(this).data("column");
    var roleId = $(this).data("roleid");
	if (column != undefined && roleId != undefined) {
		var isChecked = $(this).is(':checked');
        $.ajax({
            type: "POST",
            url: "/Permissions/updatePermission",
            data: { 
				"column": column,
				"roleId": roleId,
				"isChecked": isChecked
			},
            success: function (e) {
                if (e == "True") {
					run_gritter("","Permission has been updated","","false","1000");
                } else {
                    alert("An error occurred!");
                }
            },
            error: function () { alert("An error occurred!") }
        })
    }
})

$('#addRole_form').on('submit', function() {
	var name = $('#addRole_name').val();
	if(name.length < 3 || name.length > 50){
		$('#addRole_warning').html("Team's name contains 3-50 chars.");
		return;
	}
	$.ajax({
			type: 'POST',
			url: '/Permissions/insertRole',
			data: {
				"name": name
			},
			success: function (e) {
				if(e == "True"){
					alert("Role have been added.");
					window.location.reload();
				}
				else{
					alert("An error occurred!");
				}
			},
			error: function () { alert("An error occurred!");}
		})
})