$(document).ready(function () {
    loadTotalBookingCustomerLineChart();
});

function loadTotalBookingCustomerLineChart() {
    $(".chart-spinner").show();
    $.ajax({
        url: "/Dashboard/GetMemberAndBookingChartData",
        type: "GET",
        success: function (data) {
            loadLineChart("newMembersAndBookingsLineChart",data)
            $(".chart-spinner").hide();
        }

    })

}

function loadLineChart(id,data) {
    var chartColor = getChartColorsArray(id)
    var options = {
        series: data.series,
        colors: chartColor,

        chart: {
            type: 'line',
            height:350
        },
        stroke: {
            show: true,
            curve: 'smooth',
            width:2
        },
        markers: {
            size: 3,
            strokeWidth:0,
            hover: {
                size:9
            }
        },
        xaxis: {
            categories: data.categories,
            labels: {
                style: {
                    colors: '#ddd',
                },
            }

        },
        yaxis: {
            labels: {
                style: {
                    colors: '#fff',
                },
            }
        },
        legend: {
            labels: {
                colors: '#fff',
            },
        },
        tooltip: {
            theme:"dark"
        }
    }

    var chart = new ApexCharts(document.querySelector("#" + id), options);
    chart.render();
}
