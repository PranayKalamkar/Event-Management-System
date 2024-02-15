﻿$(document).ready(function () {

    getAdminList();

});

var datatable;

function getAdminList() {
    $.ajax({

        type: "Get",
        url: "/Admin/GetAdmin",
        success: function (data) {

            debugger;
            datatable = $('#myTable').DataTable({
                data: data,
                columns: [
                    { data: 'Id' },
                    { data: 'Username' },
                    { data: 'Email' },
                    {
                        data: null,
                        render: function (data, type, row) {

                            return '<button type="button" onclick="populateadminData(' + row.Id + ')" class="btn btn-warning" data-bs-toggle="modal" data-bs-target="#updateAdmin" style="margin-right: 10px;">Edit</button><button type="button" onclick="deleteadminData(' + row.Id + ')" class="btn btn-danger" style="margin-right: 10px;" >Delete</button><button type="button" onclick="viewEventData(' + row.Id + ')" class="btn btn-info d-none" data-bs-toggle="modal" data-bs-target="#viewAddEventModal">Disable</button>';

                        }
                    },

                ]
            });

        },
        error: function (textStatus, errorThrown) {
            Success = false;
        }

    });
}

function addAdmin() {

    if ($("#create").valid()) {

        var eventObj = {
            Username: $('#Username').val(),
            Email: $('#Email').val(),
            SPassword: $('#SPassword').val(),
            ConfirmSPassword: $('#ConfirmSPassword').val(),
        }

        var formData = new FormData();
        formData.append("model", JSON.stringify(eventObj));

        $.ajax({
            url: "/Admin/AddAdminPost",
            data: formData,
            processData: false,
            contentType: false,
            cache: false,
            type: "POST",
            success: function (data) {
                debugger;

                $('#addAdmin').modal('hide');

                // clearForm();

                if (data.status === "success") {
                    Swal.fire({
                        title: "Good job!",
                        text: "User saved successfully!",
                        icon: "success",
                        button: "Ok",
                    });
                }
                else if (data.status === "warning") {
                    alert(data.message);
                }

                datatable.destroy();
                getAdminList();
                // windows.reload();

            },
            error: function (errorThrown) {
                console.log("Error saving User:", errorThrown);
                Swal.fire("Oops", "An error occurred while saving your data, Please try again later.", "error");
            }
        });
    }
}

function populateadminData(ID) {

    $.ajax({

        type: "GET",
        url: "/Admin/PopulateAdmin/" + ID,

        success: function (admin) {
            debugger;

            // Populate the form with the received employee details
            $('#u_Id').val(admin.Id);
            $('#u_Username').val(admin.Username);
            $('#u_Email').val(admin.Email);

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function updateAdmin() {

    if ($("#update").valid()) {

        var adminID = {
            id: $('#u_Id').val(),
        }

        var adminData = {
            Username: $('#u_Username').val(),
            Email: $('#u_Email').val(),
        };

        var formData = new FormData();
        formData.append("ID", adminID.id);
        formData.append("model", JSON.stringify(adminData));

        $.ajax({
            type: "POST",
            url: "/Admin/UpdateAdmin",
            data: formData,
            contentType: false,
            processData: false,
            cache: false,

            success: function (data) {

                $('#updateAdmin').modal('hide');


                if (data.status === "success") {
                    Swal.fire({
                        title: "Good job!",
                        text: "User updated successfully!",
                        icon: "success",
                        button: "Ok",
                    });
                }
                else if (data.status === "warning") {
                    alert(data.message);
                }

                datatable.destroy();
                getAdminList();
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
}

function deleteadminData(ID) {

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
                url: "/Admin/DeleteAdmin/" + ID,
                success: function (result) {

                    Swal.fire({
                        title: "Deleted!",
                        text: "Your file has been deleted.",
                        icon: "success"
                    });

                    datatable.destroy();
                    getAdminList();

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