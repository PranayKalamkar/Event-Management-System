$(document).ready(function () {
    Details();
    getBookedEventList();
});

var datatable;

function getBookedEventList() {
    $.ajax({

        type: "Get",
        url: "/AdminDashboard/BookedEventsList",
        success: function (data) {

            datatable = $('#myTableDashboard').DataTable({
                data: data,
                columns: [
                    { data: 'RequestedEventsModel.Id' },
                    { data: 'SignUpModel.Username' },
                    { data: 'AddEventModel.Amount' },
                    { data: 'RequestedEventsModel.Deposit' },
                    { data: 'RequestedEventsModel.Balance' },
                    { data: 'RequestedEventsModel.Date' },
                ]
            });

        },
        error: function (textStatus, errorThrown) {
            Success = false;
        }

    });
}


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
            $('#b_complete').text(dashboard.Total_Events_Completed);

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}