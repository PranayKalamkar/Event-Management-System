$(document).ready(function () {

    getEventList();

});

var datatable;

function getEventList() {
    $.ajax({

        type: "Get",
        url: "/EventHistory/EventHistoryList",
        success: function (data) {

            datatable = $('#myTable3').DataTable({
                data: data,
                columns: [
                    { data: 'RequestedEventsModel.Id' },
                    { data: 'SignUpModel.Username' },
                    { data: 'AddEventModel.Category' },
                    { data: 'AddEventModel.Amount' },
                    { data: 'AddEventModel.Location' },
                    { data: 'RequestedEventsModel.Date' },
                ],
                rowCallback: function (row, data) {
                    $(row).on('click', function () {
                        viewCompletedEventData(data.RequestedEventsModel.Id);
                    });
                }
            });

        },
        error: function (textStatus, errorThrown) {
            Success = false;
        }

    });
}

function viewCompletedEventData(ID) {

    $.ajax({

        type: "GET",
        url: "/EventHistory/PopulateCompletedEvent/" + ID,

        success: function (bookevent) {

            // Populate the form with the received employee details
            $('#v_Id').val(bookevent.RequestedEventsModel.Id);
            $('#v_Username').val(bookevent.SignUpModel.Username);
            $('#v_Email').val(bookevent.SignUpModel.Email);
            $('#v_Category').val(bookevent.AddEventModel.Category);
            $('#v_Location').val(bookevent.AddEventModel.Location);
            $('#v_Capacity').val(bookevent.AddEventModel.Capacity);
            $('#v_Amount').val(bookevent.AddEventModel.Amount);
            $('#v_Contact').val(bookevent.AddEventModel.Contact);
            $('#v_Deposit').val(bookevent.RequestedEventsModel.Deposit);
            $('#v_Balance').val(bookevent.RequestedEventsModel.Balance);
            $('#v_Date').val(bookevent.RequestedEventsModel.Date);
            $('#v_Time').val(bookevent.RequestedEventsModel.Time);
            $('#v_Status').val(bookevent.EventStatusModel.Status);

            var imagePreview = '/addeventimages/' + bookevent.AddEventModel.ImagePath;
            $('#imagePreviewView').attr('src', imagePreview).show();

            $('#viewcompletedEventModal').modal('show');

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}