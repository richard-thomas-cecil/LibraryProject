var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/BookManagement/GetAll"
        },
        "columns": [
            { "data": "title", "width": "20%" },
            { "data": "authorFirstName", "width": "15%" },
            { "data": "authorLastName", "width": "15%"},
            { "data": "genre.name", "width": "15%" },
            { "data": "isbn", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/BookManagement/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    Edit
                                </a>
                                <a onclick=Delete("/Admin/BookManagement/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    Delete
                                </a>
                            </div>`;
                }, "width": "15%"
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Are You Sure You Want to Delete?",
        text: "Data cannot be recovered once deleted",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.erro(data.message);
                    }
                }
            });
        }
    });
}