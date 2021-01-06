var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/EmployeeManagement/GetAll"
        },
        "columns": [
            { "data": "employeeNumber", "width": "20%" },
            { "data": "firstName", "width": "40%" },
            { "data": "lastName", "width": "40%" }                
        ]
    });
}
