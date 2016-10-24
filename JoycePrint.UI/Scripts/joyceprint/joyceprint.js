$(document).ready(function () {
    toggleNavigationMenu();

    contactUsValidation();
});

/*
 * Toggle the navigation menu so the active page reflects the page the user is currently on
 */
function toggleNavigationMenu() {

    // Find and remove the active class
    $("#jpNav").find(".active").removeClass("active");

    if ($("#home").length > 0) {
        $("#jpNav #liHome").addClass("active");
    } else if ($("#quote").length > 0) {
        $("#jpNav #liQuote").addClass("active");
    } else if ($("#services").length > 0) {
        $("#jpNav #liServices").addClass("active");
    } else if ($("#aboutus").length > 0) {
        $("#jpNav #liAboutUs").addClass("active");
    }
}

function contactUsValidation() {

    $(document).bind('keyup', function (e) {
        if ($(e.target).is(':invalid')) {
            $(e.target).prev().addClass('invalid');

        } else {
            $(e.target).prev().removeClass('invalid');
        }

        if ($(e.target).is(':valid')) {
            $(e.target).prev().addClass('valid');
        } else {
            $(e.target).prev().removeClass('valid');
        }

        if ($(e.target).is(':focus')) {
            $(e.target).prev().addClass('focus');
        } else {
            $(e.target).prev().removeClass('focus');
        }
    });
}