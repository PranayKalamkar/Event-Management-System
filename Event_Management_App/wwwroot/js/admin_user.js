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

                            return '<button type="button" onclick="populateadmin_userData(' + row.Id + '); event.stopPropagation();" class="btn btn-warning" data-bs-toggle="modal" data-bs-target="#updateUser" style="margin-right: 10px;">Edit</button><button type="button" onclick="deleteadmin_userData(' + row.Id + '); event.stopPropagation();" class="btn btn-danger" style="margin-right: 10px;" >Delete</button>';

                        }
                    },

                ],
                rowCallback: function (row, data) {
                    $(row).on('click', function () {
                        viewadmin_userData(data.Id);
                    });
                }
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
            Contact: $('#Contact').val(),
            Address: $('#Address').val(),
        }

        var formData = new FormData();
        formData.append("model", JSON.stringify(eventObj));
        formData.append("idproof", $('#IdProofFile')[0].files[0]);
        formData.append("profile", $('#ProfileFile')[0].files[0]);

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



function previewIdProof(input) {
    var file = input.files[0];

    if (file) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#IdProofUpdate').attr('src', e.target.result).show();
        };

        reader.readAsDataURL(file);
    } else {
        // Clear the image preview if no file is selected
        $('#IdProofUpdate').attr('src', '').hide();
    }
}


function previewProfile(input) {
    var file = input.files[0];

    if (file) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#ProfileUpdate').attr('src', e.target.result).show();
        };

        reader.readAsDataURL(file);
    } else {
        // Clear the image preview if no file is selected
        $('#ProfileUpdate').attr('src', '').hide();
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
            $('#u_Contact').val(admin_user.Contact);
            $('#u_Address').val(admin_user.Address);

            var IdProofPreview = "/admin_useridproof/" + admin_user.IdProofPath;
            $('#IdProofUpdate').attr('src', IdProofPreview).show();

            var profilePreview = "/admin_userimage/" + admin_user.ProfilePath;
            $('#ProfileUpdate').attr('src', profilePreview).show();

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
            Contact: $('#u_Contact').val(),
            Address: $('#u_Address').val(),
        };

        var formData = new FormData();
        formData.append("ID", admin_userID.id);
        formData.append("model", JSON.stringify(admin_userData));
        formData.append("idproof", $("#u_IdProofFile")[0].files[0]);
        formData.append("profile", $("#u_ProfileFile")[0].files[0]);

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

            var IdProofPreview = "/admin_useridproof/" + admin_user.IdProofPath;
            $('#IdProofView').attr('src', IdProofPreview).show();

            var profilePreview = "/admin_userimage/" + admin_user.ProfilePath;
            $('#ProfileView').attr('src', profilePreview).show();

            $('#viewUser').modal('show');
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

function clearForm() {
    $('#Username').val('');
    $('#Email').val('');
    $('#SPassword').val('');
    $('#ConfirmSPassword').val('');
    $('#Contact').val('');
    $('#Address').val('');
    $('#IdProofFile').val('');
    $('#ProfileFile').val('');
}