$(document).ready(function () {
    loadTotalUserChart();
});

function loadTotalUserChart() {
    $(".chart-spinner").show();
    $.ajax({
        url: "/Dashboard/GetRegisteredUserChartData",
        type: "GET",
        success: function (data) {
            document.querySelector("#spanTotalUsersCount").innerHTML = data.totalCount;
            var sectionCurrentCount = document.createElement("span");
            if (data.hasRatioIncreased) {
                sectionCurrentCount.className = "text-success me-1";
                sectionCurrentCount.innerHTML = `<i class="bi bi-arrow-up-right-circle me-1"></i> <span> ${data.countInCurrentMonth} </span>`;
            }
            else
            {
                sectionCurrentCount.className = "text-danger me-1";
                sectionCurrentCount.innerHTML = `<i class="bi bi-arrow-down-right-circle me-1"></i> <span> ${data.countInCurrentMonth} </span>`;    
            }

            document.querySelector("#sectionUsersCount").append(sectionCurrentCount);
            document.querySelector("#sectionUsersCount").append("since last month");
            localRadialBarChart("totalUsersRadialChart",data)



            $(".chart-spinner").hide();
        }

    })

}

