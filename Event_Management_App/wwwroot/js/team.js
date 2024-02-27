$(document).ready(function () {
    Profile();
});


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

function Profile() {

    $.ajax({

        type: "GET",
        url: "/Team/PopulateTeam/",

        success: function (admin) {
            debugger;

            // Populate the form with the received employee details
            $('#u_Id').val(admin.Id);
            $('#u_Username').val(admin.Username);
            $('#u_Email').val(admin.Email);
            $('#u_Contact').val(admin.Contact);
            $('#u_Address').val(admin.Address);

            var profilePreview = "/admin_userimage/" + admin.ProfilePath;
            $('#ProfileUpdate').attr('src', profilePreview).show();

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function updateProfile() {

    if ($("#update").valid()) {

        var userID = {
            id: $('#u_Id').val(),
        }

        var userData = {
            Username: $('#u_Username').val(),
            Email: $('#u_Email').val(),
            Contact: $('#u_Contact').val(),
            Address: $('#u_Address').val(),
        };

        var formData = new FormData();
        formData.append("ID", userID.id);
        formData.append("model", JSON.stringify(userData));
        formData.append("profile", $("#u_ProfileFile")[0].files[0]);

        $.ajax({
            type: "POST",
            url: "/Team/UpdateAdmin_User",
            data: formData,
            contentType: false,
            processData: false,
            cache: false,

            success: function (data) {

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