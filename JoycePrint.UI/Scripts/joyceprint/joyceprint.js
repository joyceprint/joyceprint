$(document).ready(function () {
    ToggleNavigationMenu();
});

/*
 * Toggle the navigation menu so the active page reflects the page the user is currently on
 */
function ToggleNavigationMenu() {

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