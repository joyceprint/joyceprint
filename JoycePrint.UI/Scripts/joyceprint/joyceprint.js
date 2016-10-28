$(document).ready(function () {
    toggleNavigationMenu();

    contactUsValidation();

    handleCssHtmlRestriction();
});

function handleCssHtmlRestriction() {

    // use $.fn.one here to fire the event only once.
    $(':required').one('blur keydown', function () {
        $(this).addClass('touched');
    });
}

function contactUsValidation() {

    $(':required').on("focusout keyup change", function (e) {

        if (e.originalEvent.code === "Tab")
            return;

        var input = $(e.target);
        var symbolContainer = $(e.target).prev();
        var symbol = $(symbolContainer).children().first();

        if ($(input).is(':invalid')) {
            $(symbol).removeAttr("class");
            $(symbol).addClass("glyphicon glyphicon-remove-circle text-danger");

            if ($(input).is("textarea")) return;

            $(symbolContainer).removeAttr("class");
            $(symbolContainer).addClass("input-group-addon invalid");
        } else if ($(input).is(':valid')) {
            $(symbol).removeAttr("class");
            $(symbol).addClass("glyphicon glyphicon-ok-circle text-success");

            if ($(input).is("textarea")) return;

            $(symbolContainer).removeAttr("class");
            $(symbolContainer).addClass("input-group-addon valid");
        }

        //if ($(input).is(':focus')) {
        //    $(symbol).addClass('focus');
        //} else {
        //    $(symbol).removeClass('focus');
        //}
    });
}

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