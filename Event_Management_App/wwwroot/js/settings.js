document.addEventListener('DOMContentLoaded', function () {
    var billingForm = document.getElementById('billingForm');

    billingForm.addEventListener('submit', function (event) {
        if (!billingForm.checkValidity()) {
            event.preventDefault();
            event.stopPropagation();
        }
        billingForm.classList.add('was-validated');
    }, false);
});
