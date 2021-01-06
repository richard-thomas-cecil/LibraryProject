var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Librarian/LibraryMember/GetAll"
        },
        "columns": [
            { "data": "id", "width": "20%" },
            { "data": "name", "width": "15%" },
            { "data": "phoneNumber", "width": "15%"},
            { "data": "streetAddress", "width": "15%" },
            { "data": "city", "width": "15%" },
            {"data": "state", "width": "10%"},
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Librarian/LibraryMember/Details/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    Details
                                </a>
                            </div>`;
                }, "width": "10%"
            }
        ]
    });
}

//function Delete(url) {
//    swal({
//        title: "Are You Sure You Want to Delete?",
//        text: "Data cannot be recovered once deleted",
//        icon: "warning",
//        buttons: true,
//        dangerMode: true
//    }).then((willDelete) => {
//        if (willDelete) {
//            $.ajax({
//                type: "DELETE",
//                url: url,
//                success: function (data) {
//                    if (data.success) {
//                        toastr.success(data.message);
//                        dataTable.ajax.reload();
//                    }
//                    else {
//                        toastr.erro(data.message);
//                    }
//                }
//            });
//        }
//    });
//}