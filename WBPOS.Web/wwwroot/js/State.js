var table = "";
$(document).ready(function () {
    table = $("#myTable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "orderMulti": false,
        "paging": true,
        "responsive": true,
        "ajax": {
            "url": "State/GetJsonData",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "stateName", "name": "stateName" },
            { "data": "stateCode", "name": "stateCode" }, 
            { "data": "status", "name": "status" },
            {
                data: null,
                orderable: false,
                width: 100,
                render: function (data, type, row) {
                    //console.log(data);
                    return "<a href='State/Edit/" + data.stateId + "' class='fa fa-pen-square'></a>"
                        + "&nbsp;&nbsp;<a id = " + data.stateId + " Onclick = 'fnConfirm(this);' class='fa fa-window-close pointer'></a>"
                }
            }
        ]
    });
});

function fnConfirm(model) {
    $('#myModalDelete').modal('show');
    $("#DeleteDiv").html("Are you sure you want to delete this record?");
    $("#ConfirmDeleting").click(function () {
        $.ajax({
            url: "/State/DeleteData/" + model.id,
            type: 'post',
            contentType: 'application/x-www-form-urlencoded',
            data: $(this).serialize(),
            success: function (data, textStatus, jQxhr) {
                $('#myModalDelete').modal('hide');
                table.ajax.reload(null, false);
            },
            error: function (jqXhr, textStatus, errorThrown) {
                console.log(errorThrown);
            }
        });
    });
}