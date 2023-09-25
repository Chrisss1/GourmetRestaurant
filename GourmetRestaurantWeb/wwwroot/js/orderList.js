var dataTable;
var url = window.location.search;

if (url.includes("cancelled")) {
    loadList("cancelled");
}
else {
    if (url.includes("completed")) {
        loadList("completed");
    }
    else {
        if (url.includes("ready")) {
            loadList("ready");
        }
        else {
            loadList("inprocess");
        }
    }
}

function loadList(param) {
    dataTable = new DataTable('#DT_Load', {
        "ajax": {
            "url": "/api/order?status=" + param,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "15%" },
            { "data": "pickupName", "width": "15%" },
            { "data": "applicationUser.email", "width": "15%" },
            { "data": "orderTotal", "width": "15%" },
            { "data": "pickUpTime", "width": "25%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="w-100 btn-group">
                        <a href="/Admin/Order/OrderDetails?id=${data}" class="btn btn-success text-white mx-2 py-2">
                        <i class="bi bi-pencil-square"></i> </a>
                        </div>`
                },

                "width": "15%"
            }
        ],
        columnDefs: [{
            targets: 4, // Date column index
            render: function (data) {
                return moment(data).format('YYYY-MM-DD HH:mm:ss');
            }
        }],

        "width": "100%"
    });
}