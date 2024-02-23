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
            //$('#s_Id').val(dashboard.Total_Users);
            //$('#b_Id').val(dashboard.Total_Events);
            //$('#b_deposit').val(dashboard.Total_Deposit);
            $('#s_Id').text(dashboard.Total_Users);
            $('#b_Id').text(dashboard.Total_Events);
            $('#b_deposit').text(dashboard.Total_Deposit);

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}