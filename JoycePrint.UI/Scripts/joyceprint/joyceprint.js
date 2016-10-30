$(document).ready(function () {
    toggleNavigationMenu();

    contactUsValidation();

    handleCssHtmlRestriction();
});

/*
 * Creates an event that will add the touched class to required fields
 * This is required as the :visited pseudo class only applies to anchor tags
 */
function handleCssHtmlRestriction() {

    // use $.fn.one here to fire the event only once.
    $(':required').one('blur keydown', function () {
        $(this).addClass('touched');
    });
}

/*
 * Applies the classes used for validation depending on whether the target is [ :valid | :invalid ]
 */
function contactUsValidation() {

    $(':required').on("focusout keyup change", function (e) {

        // If we tabbed into the input we do not want to perform validation
        if (e.originalEvent.code === "Tab")
            return;

        var input = $(e.target);
        var symbolContainer = $(e.target).prev();
        var symbol = $(symbolContainer).children().first();

        if ($(input).is(':invalid')) {
            $(symbol).removeAttr("class");
            $(symbol).addClass("glyphicon glyphicon-remove-circle text-danger");

            if ($(input).is("textarea")) {
                $(input).addClass("bg-danger");
            } else {
                $(symbolContainer).removeAttr("class");
                $(symbolContainer).addClass("input-group-addon invalid");
            }
        } else if ($(input).is(':valid')) {
            $(symbol).removeAttr("class");
            $(symbol).addClass("glyphicon glyphicon-ok-circle text-success");

            if ($(input).is("textarea")) {
                $(input).removeClass("bg-danger");
            } else {
                $(symbolContainer).removeAttr("class");
                $(symbolContainer).addClass("input-group-addon valid");
            }
        }
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