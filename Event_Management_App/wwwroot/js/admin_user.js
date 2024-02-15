$(document).ready(function () {

    getAdmin_UserList();

});

var datatable;

function getAdmin_UserList() {
    $.ajax({

        type: "Get",
        url: "/Admin_User/GetAdmin_User",
        success: function (data) {

            datatable = $('#myTable').DataTable({
                data: data,
                columns: [
                    { data: 'Id' },
                    { data: 'Username' },
                    { data: 'Email' },
                    {
                        data: null,
                        render: function (data, type, row) {

                            return '<button type="button" onclick="populateadmin_userData(' + row.Id + ')" class="btn btn-warning" data-bs-toggle="modal" data-bs-target="#updateUser" style="margin-right: 10px;">Edit</button><button type="button" onclick="deleteadmin_userData(' + row.Id + ')" class="btn btn-danger" style="margin-right: 10px;" >Delete</button><button type="button" onclick="viewadmin_userData(' + row.Id + ')" class="btn btn-info d-none" data-bs-toggle="modal" data-bs-target="#viewUser">View</button>';

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

function addUser() {

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
            url: "/Admin_User/AddAdmin_UserPost",
            data: formData,
            processData: false,
            contentType: false,
            cache: false,
            type: "POST",
            success: function (data) {

                $('#addUser').modal('hide');

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
                getAdmin_UserList();

            },
            error: function (errorThrown) {
                console.log("Error saving User:", errorThrown);
                Swal.fire("Oops", "An error occurred while saving your data, Please try again later.", "error");
            }
        });
    }
}


function populateadmin_userData(ID) {

    $.ajax({

        type: "GET",
        url: "/Admin_User/PopulateAdmin_User/" + ID,

        success: function (admin_user) {

            // Populate the form with the received employee details
            $('#u_Id').val(admin_user.Id);
            $('#u_Username').val(admin_user.Username);
            $('#u_Email').val(admin_user.Email);

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function updateUser() {

    if ($("#update").valid()) {

        var admin_userID = {
            id: $('#u_Id').val(),
        }

        var admin_userData = {
            Username: $('#u_Username').val(),
            Email: $('#u_Email').val(),
        };

        var formData = new FormData();
        formData.append("ID", admin_userID.id);
        formData.append("model", JSON.stringify(admin_userData));

        $.ajax({
            type: "POST",
            url: "/Admin_User/UpdateAdmin_User",
            data: formData,
            contentType: false,
            processData: false,
            cache: false,

            success: function (data) {

                $('#updateUser').modal('hide');


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
                getAdmin_UserList();
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


function viewadmin_userData(ID) {

    $.ajax({
        type: "GET",
        url: "/Admin_User/PopulateAdmin_User/?ID=" + ID,

        success: function (admin_user) {

            // Populate the form with the received employee details
            $('#v_Id').val(admin_user.Id);
            $('#v_Username').val(admin_user.Username);
            $('#v_Email').val(admin_user.Email);
            $('#v_Contact').val(admin_user.Contact);
            $('#v_Address').val(admin_user.Address);

            var imagePreview = "/adminuserimage/" + admin_user.IdProofPath;
            $('#imagePreviewUpdate').attr('src', imagePreview).show();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function deleteadmin_userData(ID) {

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
                url: "/Admin_User/DeleteAdmin_User/" + ID,
                success: function (result) {

                    datatable.destroy();
                    getAdmin_UserList();

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