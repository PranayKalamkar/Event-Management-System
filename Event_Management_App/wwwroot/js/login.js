function Login() {
    if ($("#loginform").valid()) {

        var emailData = {
            // Email: $('#email').val()
            Email: $('#Email').val()
        }

        var passwordData = {
            // SPassword: $('#password').val()
            SPassword: $('#SPassword').val()
        }

        console.log(passwordData);

        var formData = new FormData();
        // formData.append("email", emailData.Email);
        // formData.append("password", passwordData.SPassword);
        formData.append("Email", emailData.Email);
        formData.append("SPassword", passwordData.SPassword);

        $.ajax({
            type: "POST",
            url: "/Login/LoginPost",
            data: formData,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.status === "warning") {
                    alert(data.message);
                }
                else if (data.status === "success" && data.role === 1) {
                    alert(data.message);
                    // window.location.href = '/User/Dashboard';
                    window.location.href = '/AdminDashboard/Dashboard';
                }
                else if (data.status === "success" && data.role === 2) {
                    alert(data.message);
                    // window.location.href = '/User/Dashboard';
                    // window.location.href = '/UserPage/UserPage';
                    window.location.href = '/Customer/Customer';
                }


            },
            error: function (error) {
                console.error("Error Login user : ", error);
            }
        });
    }
}