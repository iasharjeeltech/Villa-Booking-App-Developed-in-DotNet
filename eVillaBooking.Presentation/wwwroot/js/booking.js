$(document).ready(function () {
    $('#bookingTable').DataTable({
        "ajax": {
            "url": "/Booking/GetAllBookings",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { data: "id" },
            { data: "user.name" },
            { data: "phone" },
            { data: "email" },
            { data: "status" },
            {
                data: "checkInDate",
                render: function (data) {
                    return new Date(data).toLocaleDateString(); // Optional: formats the date nicely
                }
            },
            { data: "nights" },
            { data: "villa.name" },
            {
                data: "totalCost",
                render: function (data) {
                    return "$" + data.toFixed(2); // Optional: formats as currency
                }
            },
            {
                data: "id",
                render: function (data) {
                    return `<a href="/Booking/Details?id=${data}" class="btn btn-info btn-sm">View</a>`;
                }
            }
        ]
    });
});
