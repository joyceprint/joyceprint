$(document).ready(function () {
    ToggleNavigationMenu();
});

/*
 * Toggle the navigation menu so the active page reflects the page the user is currently on
 */
function ToggleNavigationMenu() {

    // Find and remove the active class
    $("#jp-nav").find(".active").removeClass("active");

    if ($("#home").length > 0) {
        $("#jp-nav #liHome").addClass("active");
    } else if ($("#aboutus").length > 0) {
        $("#jp-nav #liAboutUs").addClass("active");
    } else if ($("#contactus").length > 0) {
        $("#jp-nav #liContactUs").addClass("active");
    } else if ($("#services").length > 0) {
        $("#jp-nav #liServices").addClass("active");
    }       
}