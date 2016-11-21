$(document).ready(function () {

    toggleNavigationMenu();

    initMobileMenu();

    initMaterializeSelect();

    initExtendHtml5ResetEvent();

    // This has been moved to the validation function
    //initDocketHelp();

    //carousel()
});

/**************************************************************************************************
 *
 *
 *************************************************************************************************/
function initExtendHtml5ResetEvent() {
    // remove the touched class
    $("button[type='reset']").on("click", function () {
        //$(this).default();

        $(".touched").each(function () {
            $(this).removeClass("touched");
        });

        $(".valid").each(function () {
            $(this).removeClass("valid");
        });

        $(".invalid").each(function () {
            $(this).removeClass("invalid");
        });       
    });
    
    // reset the ul
}

/**************************************************************************************************
 *
 *
 *************************************************************************************************/
function initDocketHelp() {
    $("#docket-book input").each(function() {
        if ($(this).data("help").length > 0) {
            $(this).on("focus", function (e) {
                if (!$("#" + $(this).data("help")).hasClass("active")) {
                    $("#" + $(this).data("help")).trigger("click");
                }
            });
        }
    });
}

/**************************************************************************************************
 *
 *
 *************************************************************************************************/
function initMaterializeSelect() {    
    // Select - Single
    $('select:not([multiple])').material_select();

    //$('select').material_select();
}

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