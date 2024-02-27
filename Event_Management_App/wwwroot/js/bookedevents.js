$(document).ready(function () {
    getAddEventList();
    GetStatus();
});

function getAddEventList() {
    $.ajax({
        type: "Get",
        url: "/BookedEvents/BookedEventsList",
        success: function (data) {
            var cardContainer = $('#cardContainer');
            data.forEach(function (item) {
                var card = `
                                                                                                    <div class="col-md-4 mb-4">
                                                                                                        <div class="card h-100 p-3" onclick="populateEventData(${item.RequestedEventsModel.Id})" data-bs-toggle="modal" data-bs-target="#viewbookEventModal">
                                                                                                            <img src="/addeventimages/${item.AddEventModel.ImagePath}" class="card-img-top" alt="Image">
                                                                                                            <div class="card-body">
                                                                                                                <h5 class="card-title">${item.AddEventModel.Category}</h5>
                                                                                                                <p class="card-text">Id: ${item.RequestedEventsModel.Id}</p>
                                                                                                                <p class="card-text">Location: ${item.AddEventModel.Location}</p>
                                                                                                                <p class="card-text">Deposit: ${item.RequestedEventsModel.Deposit}</p>
                                                                                                                <p class="card-text">Balance: ${item.RequestedEventsModel.Balance}</p>
                                                                                                                <p class="card-text">Status: ${item.EventStatusModel.Status}</p>
                                                                                                            </div>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                `;
                cardContainer.append(card);
            });
        },
        error: function (textStatus, errorThrown) {
            console.log('Error: ' + textStatus);
        }
    });
}

function previewImage(input) {
    var file = input.files[0];

    if (file) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imagePreviewUpdate').attr('src', e.target.result).show();
        };

        reader.readAsDataURL(file);
    } else {
        // Clear the image preview if no file is selected
        $('#imagePreviewUpdate').attr('src', '').hide();
    }
}

//function for populating update data
function populateEventData(Id) {

    $.ajax({

        type: "GET",
        url: "/BookedEvents/PopulateBookEvent/" + Id,

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
            $('#v_Status').val(bookevent.RequestedEventsModel.Status_Id);

            var imagePreview = '/addeventimages/' + bookevent.AddEventModel.ImagePath;
            $('#imagePreviewView').attr('src', imagePreview).show();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}


function GetStatus() {

    $.ajax({
        url: '/BookedEvents/GetStatus',
        type: 'GET',
        dataType: 'json', // assuming your server returns JSON
        success: function (data) {

            var dropdown = $('#v_Status');

            dropdown.empty();

            if (data) {

                $.each(data, function (i, status) {
                    if (status && status.EventStatusModel.Status && status.EventStatusModel.Id) {
                        dropdown.append($('<option></option>').text(status.EventStatusModel.Status).val(status.EventStatusModel.Id));
                    }
                });

            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(textStatus, errorThrown);
        }
    });

}


function changeStatus() {

    var statusID = {
        Id: $('#v_Id').val(),
        Status_Id: $('#v_Status').val(),
    }

    var bookData = {

        AddEventModel: {
            Amount: $('#v_Amount').val(),
        },

        RequestedEventsModel: {
            Deposit: $('#v_Deposit').val(),
            Balance: $('#v_Balance').val(),
        }
    }


    var formData = new FormData();
    formData.append("Id", statusID.Id);
    formData.append("Status_Id", statusID.Status_Id);
    formData.append("model", JSON.stringify(bookData));

    console.log(statusID);

    $.ajax({
        type: "POST",
        url: "/BookedEvents/UpdateBookEvent",
        data: formData,
        contentType: false,
        processData: false,
        cache: false,

        success: function (data) {

            $('#viewbookEventModal').modal('hide');

            Swal.fire({
                title: "Do you want to save the changes?",
                showDenyButton: true,
                showCancelButton: true,
                confirmButtonText: "Save",
                denyButtonText: `Don't save`
            }).then((result) => {
                /* Read more about isConfirmed, isDenied below */
                if (result.isConfirmed) {
                    Swal.fire("Status Updated successfully!", "", "success");
                }

                // Remove all cards from the container
                $('#cardContainer').empty();
                getAddEventList();

                window.location.href = "/EventHistory/EventHistory";
            });

        },
        error: function (errormessage) {
            Swal.fire({
                title: "Error updating Status!",
                text: "close",
                icon: "Error"
            });
        }
    });

}