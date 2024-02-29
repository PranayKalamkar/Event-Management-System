/* Created by Tivotal */

let menu = document.querySelector("#menu-bars");
let navbar = document.querySelector(".navbar");

menu.onclick = () => {
    menu.classList.toggle("fa-times");
    navbar.classList.toggle("active");
};

let themeToggler = document.querySelector(".theme-toggler");
let toggleBtn = document.querySelector(".toggle-btn");

toggleBtn.onclick = () => {
    themeToggler.classList.toggle("active");
};

window.onscroll = () => {
    menu.classList.remove("fa-times");
    navbar.classList.remove("active");
    themeToggler.classList.remove("active");
};

document.querySelectorAll(".theme-toggler .theme-btn").forEach((btn) => {
    btn.onclick = () => {
        let color = btn.style.background;
        document.querySelector(":root").style.setProperty("--theme-color", color);
    };
});

var swiper = new Swiper(".home-slider", {
    effect: "coverflow",
    grabCursor: true,
    centeredSlides: true,
    slidesPerView: "auto",
    coverflowEffect: {
        rotate: 0,
        stretch: 0,
        depth: 100,
        modifier: 2,
        slideShadows: true,
    },
    loop: true,
    autoplay: {
        delay: 3000,
        disableOnInteraction: false,
    },
});

var swiper = new Swiper(".review-slider", {
    slidesPerView: 1,
    grabCursor: true,
    loop: true,
    spaceBetween: 10,
    breakpoints: {
        0: {
            slidesPerView: 1,
        },
        700: {
            slidesPerView: 2,
        },
        1050: {
            slidesPerView: 3,
        },
    },
    autoplay: {
        delay: 5000,
        disableOnInteraction: false,
    },
});

// Custom JS

//Add Data Function
function AddMessage() {

    var messageObj = {
        Location: $('#Location').val(),
        Capacity: $('#Capacity').val(),
        Budget: $('#Budget').val(),
        Occassion: $('#Occassion').val(),
        Description: $('#Description').val(),
    }

    var formData = new FormData();
    formData.append("model", JSON.stringify(messageObj));

    $.ajax({
        url: "/Customer/Create",
        data: formData,
        processData: false,
        contentType: false,
        cache: false,
        type: "POST",
        success: function (data) {

            alert(data.message);

            window.location.reload();

        },
        error: function (errorThrown) {
            console.log("Error saving message:", errorThrown);
            Swal.fire("Oops", "An error occurred while saving your data, Please try again later.", "error");
        }
    });
}