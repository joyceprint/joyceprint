$(document).ready(function () {

    toggleNavigationMenu();

    initMobileMenu();

    //carousel()
});

/**************************************************************************************************
 *
 *
 *************************************************************************************************/
function carousel() {
    // Carousel init
    $('.carousel').carousel();
    // Slider init
    $('.carousel-slider').slider({ full_width: true });
    //$('.carousel.carousel-slider').carousel({ full_width: true });
}

/**************************************************************************************************
 * Initialize the side navigation menu
 *
 *************************************************************************************************/
function initMobileMenu() {
    $(".button-collapse").sideNav();
}

/**************************************************************************************************
 * Toggle the navigation menu so the active page reflects the page the user is currently on
 * This will also handle the side navigiation menu
 *
 *************************************************************************************************/
function toggleNavigationMenu() {

    // Find and remove the active class
    $("#nav").find(".active").removeClass("active");
    $("#nav").find(".active-text").removeClass("active-text");

    if ($("#home").length > 0) {
        $("#nav #liHome").addClass("active");
        $("#nav #liHome a").addClass("active-text");

        $("#nav-mobile #liHome").addClass("active");
        $("#nav-mobile #liHome a").addClass("active-text");
    } else if ($("#quote").length > 0) {
        $("#nav #liQuote").addClass("active");
        $("#nav #liQuote a").addClass("active-text");

        $("#nav-mobile #liQuote").addClass("active");
        $("#nav-mobile #liQuote a").addClass("active-text");
    } else if ($("#services").length > 0) {
        $("#nav #liServices").addClass("active");
        $("#nav #liServices a").addClass("active-text");

        $("#nav-mobile #liServices").addClass("active");
        $("#nav-mobile #liServices a").addClass("active-text");
    } else if ($("#aboutus").length > 0) {
        $("#nav #liAboutUs").addClass("active");
        $("#nav #liAboutUs a").addClass("active-text");

        $("#nav-mobile #liAboutUs").addClass("active");
        $("#nav-mobile #liAboutUs a").addClass("active-text");
    }
}