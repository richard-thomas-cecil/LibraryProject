var dataTable;

$(document).ready(function () {
    var url = window.location.pathname;
    var urlSplit = url.split("/");
    var id = urlSplit[urlSplit.length - 1];
    loadDataTable(id);
});

function loadDataTable(id) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Librarian/LibraryMember/GetOverdue?id=" + id
        },
        "columns": [
            { "data": "book.isbn", "width": "20%" },
            { "data": "book.title", "width": "15%" },
            { "data": "book.authorFirstName", "width": "15%" },
            { "data": "book.authorLastName", "width": "15%" },
            { "data": "dueDate", "width": "15%" }
            
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