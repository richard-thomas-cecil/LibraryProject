var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Librarian/CheckInCheckOut/GetAll"
        },
        "columns": [
            { "data": "title", "width": "15%" },
            { "data": "authorFirstName", "width": "15%" },
            { "data": "authorLastName", "width": "15%"},
            { "data": "genre.name", "width": "15%" },
            { "data": "isbn", "width": "10%" },
            { "data": "checkedOut", "width": "10%" },
            { "data": "available", "width": "10%" },
            {"data": "dateAdded", "width": "10%"},
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Librarian/CheckInCheckOut/Details/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    Select &nbsp;
                                </a>`
                }, "width": "15%"
            }
        ]
    });
}
