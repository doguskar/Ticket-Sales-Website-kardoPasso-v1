$.extend( true, $.fn.dataTable.defaults, {
	"ordering": true,
	"paging":true,
    "info": true,
	"lengthChange": true,
	"searching": true
} );

$(document).ready(function() {
	$('#dataTableEx').DataTable( {
        "language": {
            "lengthMenu": "Sayfa başına _MENU_ sonuç göster",
            "zeroRecords": "Sonuç Bulunamadı!",
            "info": "_TOTAL_ Sonuçdan Gösterilenler: _START_ - _END_",
            "infoEmpty": "",
            "infoFiltered": "",
            "search": "",
            "sSearchPlaceholder": "Ara",
            "first": "İlk",
            "last": "Son",
            "previous": "Geri",
            "next": "İleri"
        },
		"lengthMenu": [[10, 20, 50, -1], [10, 20, 50, "All"]]
    } );
	
	$('#dataTableEx').addClass("display");
} );

$(document).ready(function() {
	
	$('#tasksTable').DataTable( {
        "language": {
            "lengthMenu": "Sayfa başına _MENU_ sonuç göster",
            "zeroRecords": "Sonuç Bulunamadı!",
            "info": "_TOTAL_ Sonuçdan Gösterilenler: _START_ - _END_",
            "infoEmpty": "",
            "infoFiltered": "",
            "search": "",
            "sSearchPlaceholder": "",
            "first": "İlk",
            "last": "Son",
            "previous": "Geri",
            "next": "İleri"
        },
		"lengthMenu": [[5, 10, 20, -1], [5, 10, 20, "All"]]
    } );
	
	/* Modify*/
	//$('#tasksTable').addClass("");
	$('#tasksTable_length').addClass("taskTable");
	$('#tasksTable_filter').addClass("taskTable");
	$('#tasksTable_paginate').addClass("taskTable");
	$('#tasksTable_info').addClass("taskTable");
	
	//Select - Option
	$('#tasksTable_filter').append('<select name="tasksTable_filter_select" id="tasksTable_filter_select"></select>');
	$('#tasksTable_filter_select').append('<option value=""></option><option value="13">13</option><option value="2">2</option>');
} );

 $(document).on('change', '#tasksTable_filter_select',function(){
	$('#tasksTable_filter input').val($(this).val());
})
