$(document).ready(function () {

    getMessageList();

});

function getMessageList() {
    $.ajax({

        type: "Get",
        url: "/Message/GetMessage",
        success: function (data) {

            if (!$.fn.DataTable.isDataTable('#myTable')) {

                $('#myTable').DataTable({
                    data: data,
                    columns: [
                        { data: 'MessageModel.Id' },
                        { data: 'MessageModel.Location' },
                        { data: 'MessageModel.Budget' },
                        { data: 'MessageModel.Occassion' },
                    ],
                    rowCallback: function (row, data) {
                        $(row).on('click', function () {
                            viewMessageData(data.MessageModel.Id);
                        });
                    }
                });
            }

        },
        error: function (textStatus, errorThrown) {
            Success = false;
        }

    });
}

function viewMessageData(ID) {

    $.ajax({
        type: "GET",
        url: "/Message/PopulateMessage/" + ID,

        success: function (message) {

            // Populate the form with the received employee details
            $('#v_Id').val(message.MessageModel.Id);
            $('#v_Username').val(message.Admin_UserModel.Username);
            $('#v_Email').val(message.Admin_UserModel.Username);
            $('#v_Location').val(message.MessageModel.Location);
            $('#v_Capacity').val(message.MessageModel.Capacity);
            $('#v_Occassion').val(message.MessageModel.Occassion);
            $('#v_Description').val(message.MessageModel.Description);

            $('#viewMessage').modal('show');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}