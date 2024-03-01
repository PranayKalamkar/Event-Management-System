$(document).ready(function () {
    getAddEventList();
});

function getAddEventList() {
    $.ajax({
        type: "Get",
        url: "/AddEvent/ListEvent",
        success: function (data) {
            var cardContainer = $('#cardContainer');
            data.forEach(function (item) {
                var card = `
                                                                                                 <div class="col-md-4 mb-4">
                                                                                                       <div class="card h-100 mb-4" onclick="viewEventData(${item.AddEventModel.Id})" data-bs-toggle="modal" data-bs-target="#viewAddEventModal">
                                                                                                             <img src="/addeventimages/${item.AddEventModel.ImagePath}" class="card-img-top" alt="Image" style="height: 200px; width: 335px;">
                                                                                                                   <div class="card-body">
                                                                                                                      <h5 class="card-title">${item.AddEventModel.Category}</h5>
                                                                                                                      <p class="card-text">Location: ${item.AddEventModel.Location}</p>
                                                                                                                      <p class="card-text">Amount: ${item.AddEventModel.Amount}</p>
                                                                                                                      <p class="card-text">Status: ${item.EventStatusModel.Status}</p>
                                                                                                                      <a class="btn btn-warning" onclick="populateEventData(${item.AddEventModel.Id})" data-bs-toggle="modal" data-bs-target="#updateEvent">Edit</a>
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



//Add Data Function
function addEvent() {

    var eventObj = {
        Category: $('#Category').val(),
        Location: $('#Location').val(),
        Capacity: $('#Capacity').val(),
        Amount: $('#Amount').val(),
        Description: $('#Description').val(),
        Address: $('#Address').val(),
        Contact: $('#Contact').val(),
    }

    var formData = new FormData();
    formData.append("model", JSON.stringify(eventObj));
    formData.append("file", $('#ImageFile')[0].files[0]);

    $.ajax({
        url: "/AddEvent/Create",
        data: formData,
        processData: false,
        contentType: false,
        cache: false,
        type: "POST",
        success: function (data) {

            $('#addEvent').modal('hide');

            Swal.fire({
                title: "Good job!",
                text: "Event saved successfully!",
                icon: "success",
                button: "Ok",
            });

            // datatable.destroy();
            // Remove all cards from the container
            $('#cardContainer').empty();
            getAddEventList();

        },
        error: function (errorThrown) {
            console.log("Error saving event:", errorThrown);
            Swal.fire("Oops", "An error occurred while saving your data, Please try again later.", "error");
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
        url: "/AddEvent/Populate/" + Id,

        success: function (addevent) {

            // Populate the form with the received employee details
            $('#u_Id').val(addevent.AddEventModel.Id);
            $('#u_Category').val(addevent.AddEventModel.Category);
            $('#u_Location').val(addevent.AddEventModel.Location);
            $('#u_Capacity').val(addevent.AddEventModel.Capacity);
            $('#u_Amount').val(addevent.AddEventModel.Amount);
            $('#u_Description').val(addevent.AddEventModel.Description);
            $('#u_Address').val(addevent.AddEventModel.Address);
            $('#u_Contact').val(addevent.AddEventModel.Contact);
            $('#u_Status').val(addevent.EventStatusModel.Status);

            var imagePreview = "/addeventimages/" + addevent.AddEventModel.ImagePath;
            $('#imagePreviewUpdate').attr('src', imagePreview).show();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}

function updateAddEvent() {

    var eventAddID = {
        id: $('#u_Id').val(),
    }

    var addEventData = {
        Category: $('#u_Category').val(),
        Location: $('#u_Location').val(),
        Capacity: $('#u_Capacity').val(),
        Amount: $('#u_Amount').val(),
        Description: $('#u_Description').val(),
        Address: $('#u_Address').val(),
        Contact: $('#u_Contact').val(),
        Status: $('#u_Status').val(),
    };

    var formData = new FormData();
    formData.append("ID", eventAddID.id);
    formData.append("model", JSON.stringify(addEventData));
    formData.append("file", $("#updateImageFile")[0].files[0]);

    $.ajax({
        type: "POST",
        url: "/AddEvent/Update",
        data: formData,
        contentType: false,
        processData: false,
        cache: false,

        success: function (data) {

            $('#updateEvent').modal('hide');

            Swal.fire({
                title: "Event updated successfully!",
                text: "close",
                icon: "success"
            });

            // datatable.destroy();
            // Remove all cards from the container
            $('#cardContainer').empty();
            getAddEventList();
        },
        error: function (errormessage) {
            Swal.fire({
                title: "Error updating event!",
                text: "close",
                icon: "Error"
            });
        }
    });

}

function viewEventData(eventId) {

    $.ajax({
        type: "GET",
        url: "/AddEvent/Populate?ID=" + eventId,

        success: function (addevent) {

            // Populate the form with the received employee details
            $('#v_Id').val(addevent.AddEventModel.Id);
            $('#v_Category').val(addevent.AddEventModel.Category);
            $('#v_Location').val(addevent.AddEventModel.Location);
            $('#v_Capacity').val(addevent.AddEventModel.Capacity);
            $('#v_Amount').val(addevent.AddEventModel.Amount);
            $('#v_Description').val(addevent.AddEventModel.Description);
            $('#v_Address').val(addevent.AddEventModel.Address);
            $('#v_Contact').val(addevent.AddEventModel.Contact);
            $('#v_Status').val(addevent.EventStatusModel.Status);

            var imagePreview = "/addeventimages/" + addevent.AddEventModel.ImagePath;
            $('#imagePreviewView').attr('src', imagePreview).show();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function deleteEventData(eventId) {

    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {

        if (result.isConfirmed) {

            $.ajax({
                type: "GET",
                url: "/AddEvent/Delete/" + eventId,
                success: function (result) {

                    $('#updateEvent').modal('hide');
                    // datatable.destroy();
                    // Remove all cards from the container
                    $('#cardContainer').empty();
                    getAddEventList();

                    Swal.fire({
                        title: "Deleted!",
                        text: "Your file has been deleted.",
                        icon: "success"
                    });

                },
                error: function (errormessage) {

                    Swal.fire({
                        title: "Error Delete employee!",
                        text: "close",
                        icon: "Error"
                    });

                }
            });

        }
    });
}

function clearForm() {
    $('#Category').val('');
    $('#Location').val('');
    $('#Capacity').val('');
    $('#Amount').val('');
    $('#Description').val('');
    $('#Status').val('');
    $('#Address').val('');
    $('#Contact').val('');
    $('#ImageFile').val('');
}
