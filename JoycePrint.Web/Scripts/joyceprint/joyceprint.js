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

    //
    // Remove the touched, invalid and valid classes
    $("button[type='reset']").on("click", function () {
        var autoFocusField = null;

        $(".touched").each(function () {
            var field = this;
            
            if ($(field).hasClass("valid")) {
                $(field).removeClass("valid");                
            }

            if ($(field).hasClass("invalid")) {
                $(field).removeClass("invalid");                                             
            }

            if ($(field).hasClass("validate")) {
                if (field.nodeName === "INPUT" || field.nodeName === "TEXTAREA" || field.nodeName === "SELECT") {
                    switchToRequiredDisplay(field);

                    // need to reset the materialize dropdown, the select and the input value
                    // by now all the css classes should be reset [touched, valid, invalid]
                    // TOOD: inplement this - and fix the bug in the binding event - event is missing
                    //resetMaterializeDropDown();
                }
            }

            $(field).removeClass("touched");

            //// TODO: Fix this - exception being thrown
            //if ($(field).attr("autofocus").length > 0) {
            //    autoFocusField = field;
            //}
        });

        //$(autoFocusField).focus();
        $("#Contact_Company").focus();
    });
}

function switchToRequiredDisplay(field) {
    var icon = null;
    var label = null;

    if (field.nodeName === "INPUT" || field.nodeName === "TEXTAREA") {
        if ($(field).hasClass("select-dropdown")) {
            icon = $(field).closest("div").prev();
            label = $(field).closest("div").next();
        } else {
            icon = $(field).prev();
            label = $(field).next();        
        } //else if (field.nodeName === "SELECT") { }        
    }    

    jalidate.setRequiredDisplay($(field)[0], [icon[0], label[0]], ["valid", "invalid"]);    
}

/**************************************************************************************************
 *
 *
 *************************************************************************************************/
function initDocketHelp() {
    $("#docket-book input").each(function () {
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