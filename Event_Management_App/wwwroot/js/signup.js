function SignUp() {
    if ($("#registerform").valid()) {

        var loginFormData = {
            Username: $('#Username').val(),
            Email: $('#Email').val(),
            SPassword: $('#SPassword').val(),
            SPassword: $('#ConfirmSPassword').val()
/*            Role: $('#Role').val()*/
        }

        console.log(loginFormData);

        var formData = new FormData();
        formData.append("model", JSON.stringify(loginFormData));

        $.ajax({
            type: "POST",
            url: "/User/SignUpPost",
            data: formData,
            contentType: false,
            processData: false,
            Cache: false,
            success: function (data) {

                if (data.status === "success") {
                    alert(data.message);
                    window.location.href = "/Login/Login";
                }
                else if (data.status === "warning") {
                    alert(data.message);
                }

            },
            error: function (error) {
                console.error("Error Register user:", error);
            }
        });
    }
}