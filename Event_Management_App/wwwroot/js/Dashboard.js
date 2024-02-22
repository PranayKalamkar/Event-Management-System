$(document).ready(function () {
    Details();
});



function Details() {

    $.ajax({

        type: "GET",
        url: "/AdminDashboard/Populate/",

        success: function (dashboard) {
            debugger;

            // Populate the form with the received employee details
            $('#s_Id').val(dashboard.SignUpModel.Id);
            $('#b_Id').val(dashboard.RequestedEventsModel.Id);
            $('#b_deposit').val(dashboard.RequestedEventsModel.Deposit);

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}