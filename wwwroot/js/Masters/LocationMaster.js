var msg = "";
$(document).ready(function () {
    debugger;
    //$("#locationform").submit(function (e) {
    //    e.preventDefault();

    //    console.log('Doing ajax submit');

    //    var formAction = $(this).attr("action");
    //    var fdata = new FormData();
    //    $.ajax({
    //        type: 'post',
    //        url: formAction,
    //        processData: false,
    //        contentType: false
    //    }).done(function (result) {
    //        // do something with the result now
    //        console.log(result);
    //        if (result.status === "success") {
    //            alert(result.url);
    //        } else {
    //            alert(result.message);
    //        }
    //    });
    //});
    //$("#btnsubmit").click(function () {
    //    $("#locationform").submit();
    //});
    if ($("#LocMasterPostResonse").val() != "") {
        var Response = JSON.parse($("#LocMasterPostResonse").val());
        alert(Response.Message);
    }
    $('#locationform').submit(function () {
        debugger;
        if (validateString()) {
            alert("please fill " + msg);
            return false;
        }
    });
});

function validateString() {
    var flag = false;
    if ($("#Locationprefix").val() == "" || $("#Locationprefix").val() == undefined || $("#Locationprefix").val() == null) {
        msg = 'prefix'
        flag = true;
    }
    else if ($("#LocationId").val() == "" || $("#LocationId").val() == undefined || $("#LocationId").val() == null) {
        msg = 'Location Id'
        flag = true;
    }
    else if ($("#LocationName").val() == "" || $("#LocationName").val() == undefined || $("#LocationName").val() == null) {
        msg = 'Location Name'
        flag = true;
    }
    else if ($("#Address").val() == "" || $("#Address").val() == undefined || $("#Address").val() == null) {
        msg = 'Address'
        flag = true;
    }
    else if ($("#Country").val() == "" || $("#Country").val() == undefined || $("#Country").val() == null) {
        msg = 'Country'
        flag = true;
    }
    else if ($("#City").val() == "" || $("#City").val() == undefined || $("#City").val() == null) {
        msg = 'City'
        flag = true;
    }
    else if ($("#PostalCode").val() == "" || $("#PostalCode").val() == undefined || $("#PostalCode").val() == null) {
        msg = 'Postal Code'
        flag = true;
    }
    return flag;
}


function GetCityList() {
    debugger;
    $.ajax({
        type: 'POST',
        url: '/LocationMaster/GetcityList',
        async: false,
        dataType: 'json',
        data: { Id: $("#Country").val() },
        success: function (result) {
            debugger;
            var html = ""
            $("#City").html("");
            $.each(result, function (index, item) {
                html += '<option value=' + item.value + '>' + item.text + '</option>'
            });
            $("#City").append(html);
        }
    });
}