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

//date and time 
// script.js
document.addEventListener('DOMContentLoaded', function () {
    updateCalendar();
    updateClock();
    setInterval(updateClock, 1000);
});

function updateCalendar() {
    const date = new Date();
    const monthNames = ["January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"
    ];

    const calendar = document.getElementById('calendar');
    calendar.innerHTML = `${monthNames[date.getMonth()]} ${date.getDate()}, ${date.getFullYear()}`;
}

function updateClock() {
    const date = new Date();
    const hours = date.getHours();
    const minutes = date.getMinutes();
    const seconds = date.getSeconds();
    const ampm = hours >= 12 ? 'PM' : 'AM';

    const formattedHours = hours % 12 || 12; // the hour '0' should be '12'
    const formattedMinutes = minutes < 10 ? '0' + minutes : minutes;
    const formattedSeconds = seconds < 10 ? '0' + seconds : seconds;

    const clock = document.getElementById('clock');
    clock.innerHTML = `${formattedHours}:${formattedMinutes}:${formattedSeconds} ${ampm}`;
}
