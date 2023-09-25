var dataTable;
dataTable = new DataTable('#DT_Load', {
    "ajax": {
        "url": "/api/MenuItem",
        "type": "GET",
        "datatype": "json"
    },
    "columns": [
        { "data": "name", "width": "25%" },
        { "data": "price", "width": "25%" },
        { "data": "category.name", "width": "15%" },
        { "data": "foodType.name", "width": "15%" },
        {
            "data": "id",
            "render": function (data) {
                return `<div class="w-100 btn-group">
                        <a href="/Admin/MenuItems/upsert?id=${data}" class="btn btn-success text-white mx-2 py-2">
                        <i class="bi bi-pencil-square"></i> </a>
                        <a onClick=Delete('/api/MenuItem/'+${data})  class="btn btn-danger text-white mx-2 py-2">
                        <i class="bi bi-trash-fill"></i> </a>
                        </div>`
            },

            "width": "15%"
        }
    ],
    "width": "100%"
});

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        //success notification
                        toastr.success(data.message);
                    }
                    else {
                        //failure notification
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}