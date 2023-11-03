var dataTables;


$(document).ready(() => {
    const urlParams = new URLSearchParams(window.location.search);
    const status = urlParams.get('status');
    loadDataTable(status);
});

function loadDataTable(status) {
    dataTable = $("#tblBookings").DataTable({
        "ajax": {
            url: `/booking/getall?status=${status}`,
        },
        columns: [
            { data: 'id', width: '5%' },
            { data: 'name', width: '15%' },
            { data: 'phoneNumber', width: '10%' },
            { data: 'email', width: '10%' },
            { data: 'status', width: '5%' },
            {
                data: 'checkInDate',
                width: '15%',
                render: function (data) {
                    var date = new Date(data);
                    var day = ("0" + date.getDate()).slice(-2);
                    var month = ("0" + (date.getMonth() + 1)).slice(-2);
                    var year = date.getFullYear();
                    return `${year}-${month}-${day}`; // change this to your preferred date format
                }
            },
            { data: 'numOfNights', width: '10%' },
            { data: 'totalCost', render: $.fn.dataTable.render.number(',','.',2), width: '10%' },
            {
                data: 'id',
                render: function (data) {
                    return `<div class="w-75 btn-group">
                        <a href="/booking/bookingDetails?bookingId=${data}" class="btn btn-outline-warning mx-2">
                            <i class="bi bi-pencil-square"></i> Details
                        </a>

                    </div>`
                }
            }
        ]
    })
}
