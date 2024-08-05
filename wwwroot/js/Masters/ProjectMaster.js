var msg = "";
$(document).ready(function () {

    if ($("#ProjectMasterPostResonse").val() != "") {
        var Response = JSON.parse($("#ProjectMasterPostResonse").val());
        alert(Response.Message);
    }
    $('#Projectfrom').submit(function () {
        debugger;
        if (validateString()) {
            alert("please fill " + msg);
            return false;
        }
    });
});

function validateString() {
    var flag = false;
    if ($("#ProjectName").val() == "" || $("#ProjectName").val() == undefined || $("#ProjectName").val() == null) {
        msg = 'prefix'
        flag = true;
    }
    else if ($("#LocationId").val() == "" || $("#LocationId").val() == undefined || $("#LocationId").val() == null) {
        msg = 'Location Id'
        flag = true;
    }
    else if ($("#ProjectDate").val() == "" || $("#ProjectDate").val() == undefined || $("#ProjectDate").val() == null) {
        msg = 'Location Name'
        flag = true;
    }
    return flag;
}


