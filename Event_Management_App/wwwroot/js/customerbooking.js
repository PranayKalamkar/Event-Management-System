﻿$(document).ready(function () {
    bookEventList();
});

function bookEventList() {
    $.ajax({
        type: "Get",
        url: "/CustomerBooking/CustomerListEvent",
        success: function (data) {
            var cardContainer = $('#cardContainer');
            data.forEach(function (item) {
                var card = `
                                                    <div class="col-md-4 mb-4">
                                                        <div class="card h-100 p-3" onclick="populateBookData(${item.AddEventModel.Id})" data-bs-toggle="modal" data-bs-target="#bookEvent">
                                                            <img src="/addeventimages/${item.AddEventModel.ImagePath}" class="card-img-top" alt="Image" style="height: 250px; width: 390px;">
                                                            <div class="card-body">
                                                                <h5 class="card-title">${item.AddEventModel.Category}</h5>
                                                                <p class="card-text">Location: ${item.AddEventModel.Location}</p>
                                                                <p class="card-text">Capacity: ${item.AddEventModel.Capacity}</p>
                                                                <p class="card-text">Amount: ${item.AddEventModel.Amount}</p>
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

function populateBookData(ID) {

    $.ajax({

        type: "GET",
        url: "/CustomerBooking/Populate/" + ID,

        success: function (event) {

            // Populate the form with the received employee details
            $('#Id').val(event.AddEventModel.Id);
            $('#Category').val(event.AddEventModel.Category);
            $('#Location').val(event.AddEventModel.Location);
            $('#Capacity').val(event.AddEventModel.Capacity);
            $('#Amount').val(event.AddEventModel.Amount);
            $('#Contact').val(event.AddEventModel.Contact);
            $('#Address').val(event.AddEventModel.Address);
            $('#Description').val(event.AddEventModel.Description);

            var imagePreview = "/addeventimages/" + event.AddEventModel.ImagePath;
            $('#imagePreviewView').attr('src', imagePreview).show();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function updateBook() {

    if ($("#update").valid()) {

        var bookDataId = {
            Id: $('#Id').val(),
            AddEvent_Id: $('#Id').val(),
        }

        var bookData = {

            AddEventModel: {
                Amount: $('#Amount').val(),
                Location: $('#Location').val(),
            },

            RequestedEventsModel: {
                Deposit: $('#Deposit').val(),
                Date: $('#Date').val(),
                Time: $('#Time').val(),
            }
        }

        console.log(bookData);

        var formData = new FormData();
        formData.append("ID", bookDataId.Id);
        formData.append("AddEvent_Id", bookDataId.AddEvent_Id);
        formData.append("model", JSON.stringify(bookData));

        $.ajax({
            type: "POST",
            url: "/CustomerBooking/Booked",
            data: formData,
            contentType: false,
            processData: false,
            cache: false,

            success: function (data) {

                $('#bookEvent').modal('hide');


                if (data.status === "success") {

                    Swal.fire({
                        title: "Do you want to save the changes?",
                        showDenyButton: true,
                        showCancelButton: true,
                        confirmButtonText: "Save",
                        denyButtonText: `Don't save`
                    }).then((result) => {
                        /* Read more about isConfirmed, isDenied below */
                        if (result.isConfirmed) {
                            Swal.fire("Saved!", "", "success");
                        }

                        //Remove all cards from the container
                        $('#cardContainer').empty();
                        bookEventList();

                        window.location.href = '/Customer/Customer';
                    });
                }
                else if (data.status === "warning") {
                    alert(data.message);
                }
                else if (data.status === "warning") {
                    alert(data.message);
                }
            },
            error: function (errormessage) {
                Swal.fire({
                    title: "Error saving event!",
                    text: "close",
                    icon: "Error"
                });
            }
        });
    }
}