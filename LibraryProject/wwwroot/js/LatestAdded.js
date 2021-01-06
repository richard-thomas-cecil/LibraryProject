var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/User/Home/GetLatestBooks"
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
                                <a href="/User/BookView/Details/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    Details
                                </a>
                            </div>`;
                }, "width": "15%"
            }
        ]
    });
}
