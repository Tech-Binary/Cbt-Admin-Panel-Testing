
$(document).ready(function () {
    $('.Masters-cls').removeClass('active');
    $("#LocationSeries-master-tab").addClass('active');
    debugger;
    if ($("#LocSeriesPostResonse").val() != "") {
        var Response = JSON.parse($("#LocSeriesPostResonse").val())
        alert(Response.Message);
    }

    $(".search-input").on("keyup", function () {
        var value = this.value.toLowerCase().trim();
        $(".table-row td").show().filter(function () {
            return $(this).text().toLowerCase().trim().indexOf(value) == -1;
        }).hide();
    });
});
