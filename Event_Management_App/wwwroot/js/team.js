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
        url: "/Team/PopulateAdmin/",

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