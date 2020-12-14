var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/GenreManagement/GetAll"
        },
        "columns": [
            { "data": "name", "width": "60%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/GenreManagement/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    Edit
                                </a>
                                <a onclick=Delete("/Admin/GenreManagement/Delete/${data}") class=btn btn-danger text-white" style="cursor:pointer">
                                    Delete
                                </a>
                            </div>`;
                }, "width": "40"
            }
        ]
    });
}