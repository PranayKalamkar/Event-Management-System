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
                                                                                                             <img src="/addeventimages/${item.AddEventModel.ImagePath}" class="card-img-top" alt="Image">
                                                                                                                   <div class="card-body">
                                                                                                                      <h5 class="card-title">${item.AddEventModel.Category}</h5>
                                                                                                                      <p class="card-text">Location: ${item.AddEventModel.Location}</p>
                                                                                                                      <p class="card-text">Amount: ${item.AddEventModel.Amount}</p>
                                                                                                                      <p class="card-text">Status: ${item.EventStatusModel.Status}</p>
                                                                                                                      <a class="btn btn-warning" onclick="populateEventData(${item.AddEventModel.Id})" data-bs-toggle="modal" data-bs-target="#updateEvent">Edit</a>
                                                                                                                      <a class="btn btn-danger" onclick="deleteEventData(${item.AddEventModel.Id})">Delete</a>
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

            clearForm();


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
    $('#Category').val("");
    $('#Location').val("");
    $('#Capacity').val("");
    $('#Amount').val("");
    $('#Description').val("");
    $('#Status').val("");
    $('#Address').val("");
    $('#Contact').val("");
    $('#ImageFile').val("");
}


$(function () {

    $("form[name='add']").validate({
        // Specify validation rules
        rules: {

            Category: {
                required: true,
                maxlength: 30
            },
            Location: {
                required: true,
                maxlength: 30
            },
            Capacity: {
                required: true,
                maxlength: 50,
            },
            Amount: {
                required: true,
                maxlength: 50
            },
            Description: {
                required: true,
                maxlength: 200
            },
            Status: {
                required: true,
                maxlength: 30
            },
            Address: {
                required: true,
                maxlength: 100
            },
            Contact: {
                required: true,
                maxlength: 10
            }
        },
        // Specify validation error messages
        messages: {
            Category: {
                required: "Please provide a Category!",
                maxlength: "Your Category must be at max 30 characters long"
            },
            Location: {
                required: "Please provide a Location!",
                maxlength: "Your Location must be at max 30 characters long"
            },
            Capacity: {
                required: "Please provide a Capacity!",
                maxlength: "Your Capacity must be at max 50 characters long"
            },
            Amount: {
                required: "Please provide a Amount!",
                maxlength: "Your Amount must be at max 30 characters long"
            },
            Description: {
                required: "Please provide a Description!",
                maxlength: "Your Description must be at max 200 characters long"
            },
            Status: {
                required: "Please provide a Status!",
                maxlength: "Your Status must be at max 30 characters long"
            },
            Address: {
                required: "Please provide a Address!",
                maxlength: "Your Address must be at max 100 characters long"
            },
            Contact: {
                required: "Please provide a Contact!",
                maxlength: "Your Contact must be at max 10 characters long"
            }
        },

        errorClass: "error",
        errorElement: "div",

        submitHandler: function (form) {
            form.submit();
        }
    });


    $("form[name='add'] input[name='Category']").on("blur", function () {
        $("form[name='add']").validate().element($(this));
    });

    $("form[name='add'] input[name='Location']").on("blur", function () {
        $("form[name='add']").validate().element($(this));
    });

    $("form[name='add'] input[name='Capacity']").on("blur", function () {
        $("form[name='add']").validate().element($(this));
    });

    $("form[name='add'] input[name='Amount']").on("blur", function () {
        $("form[name='add']").validate().element($(this));
    });

    $("form[name='add'] input[name='Description']").on("blur", function () {
        $("form[name='add']").validate().element($(this));
    });

    $("form[name='add'] input[name='Status']").on("blur", function () {
        $("form[name='add']").validate().element($(this));
    });

    $("form[name='add'] input[name='Address']").on("blur", function () {
        $("form[name='add']").validate().element($(this));
    });

    $("form[name='add'] input[name='Contact']").on("blur", function () {
        $("form[name='add']").validate().element($(this));
    });
});

function validateField(fieldName) {
    $("form[name='add']").validate().element($("form[name='add'] input[name='" + fieldName + "']"));
}