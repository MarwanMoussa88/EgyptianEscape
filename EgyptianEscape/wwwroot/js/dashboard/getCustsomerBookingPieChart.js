$(document).ready(function () {
    loadTotalBookingCustomerChart();
});

function loadTotalBookingCustomerChart() {
    $(".chart-spinner").show();
    $.ajax({
        url: "/Dashboard/GetBookingPieChartData",
        type: "GET",
        success: function (data) {
            loadPieChart("customerBookingsPieChart",data)
            $(".chart-spinner").hide();
        }

    })

}

function loadPieChart(id,data) {
    var chartColor = getChartColorsArray(id)
    console.log(data)
    var options = {
        series: data.series,
        labels: data.labels,
        colors: chartColor,
        chart: {
            type: 'pie',
            width:380
        },
        stroke: {
            
        },
        legend: {
            position: 'bottom',
            horizontalAlign: 'center',
            labels: {
                colors: '#fff',
                userSeriesColors: true
            },
        }
    }

    var chart = new ApexCharts(document.querySelector("#" + id), options);
    chart.render();
}
