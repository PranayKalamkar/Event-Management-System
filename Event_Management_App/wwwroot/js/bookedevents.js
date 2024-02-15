$(document).ready(function () {
    getAddEventList();
    // GetStatus();
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
                                                                                                        <div class="card h-100 p-3">
                                                                                                            <img src="/addeventimages/${item.AddEventModel.ImagePath}" class="card-img-top" alt="Image">
                                                                                                            <div class="card-body">
                                                                                                                <h5 class="card-title">${item.AddEventModel.Category}</h5>
                                                                                                                <p class="card-text">Id: ${item.RequestedEventsModel.Id}</p>
                                                                                                                <p class="card-text">Location: ${item.AddEventModel.Location}</p>
                                                                                                                <p class="card-text">Deposit: ${item.RequestedEventsModel.Deposit}</p>
                                                                                                                <p class="card-text">Balance: ${item.RequestedEventsModel.Balance}</p>
                                                                                                                <p class="card-text">Status: ${item.EventStatusModel.Status}</p>
                                                                                                                <button class="btn btn-info" onclick="populateEventData(${item.RequestedEventsModel.Id})" data-bs-toggle="modal" data-bs-target="#viewbookEventModal">View</button>
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
            $('#v_Status').val(bookevent.EventStatusModel.Status);

            var imagePreview = '/addeventimages/' + bookevent.AddEventModel.ImagePath;
            $('#imagePreviewView').attr('src', imagePreview).show();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}