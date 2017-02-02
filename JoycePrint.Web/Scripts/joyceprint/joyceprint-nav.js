"use strict";

/**************************************************************************************************
 * Navigation Menu Javascript Functionality
 *
 * Call this function to initialize the javascript required for the navigation menu
 *************************************************************************************************/
function initializeNavFunctionality() {

    initializeMobileMenu();

    initializeScrollMenu();

    // Add the hashchange event listener, this event fires when the windows hash changes [location.hash]    
    window.addEventListener("hashchange", shiftWindow);    
}

/**************************************************************************************************
 * Sets up the hashchange function when the body loads
 *************************************************************************************************/
function loadHashChange() {
    if (window.location.hash)
        shiftWindow();
}

/**************************************************************************************************
 * Shitfs the scroll position down by the height of the fixed menu
 *
 * This allows href to point to an element location without having the top section covered
 * by the fixed menu
 *************************************************************************************************/
var shiftWindow = function () {
    scrollBy(0, -110);
};

/**************************************************************************************************
 * Initialize the side navigation menu for use on small screens and mobiles
 *************************************************************************************************/
function initializeMobileMenu() {
    $(".button-collapse").sideNav();
}

/**************************************************************************************************
 * Initialize the scroll and resize events to toggle the navigation menu to the correct link
 *************************************************************************************************/
function initializeScrollMenu() {
    $(window).on("resize scroll", function () {

        if ($("#home").isInViewport()) {
            toggleNavigationMenu("home");
        } else if ($("#services").isInViewport()) {
            toggleNavigationMenu("services");
        } else if ($("#aboutus").isInViewport()) {
            toggleNavigationMenu("aboutus");
        } else if ($("#quote").isInViewport()) {
            toggleNavigationMenu("quote");
        }
    });
}

/**************************************************************************************************
 * Toggle the navigation menu so the active page menu link reflects the view the user has currently 
 * scrolled on to. This is done by changing the text color.
 * 
 * This will also handle the side navigiation menu 
 *************************************************************************************************/
function toggleNavigationMenu(id) {

    // Find and remove the active class
    $("#nav").find(".active").removeClass("active");
    $("#nav").find(".active-text").removeClass("active-text");

    if (id === "home") {
        $("#nav #liHome").addClass("active");
        $("#nav #liHome a").addClass("active-text");

        $("#nav-mobile #liHome").addClass("active");
        $("#nav-mobile #liHome a").addClass("active-text");
    } else if (id === "quote") {
        $("#nav #liQuote").addClass("active");
        $("#nav #liQuote a").addClass("active-text");

        $("#nav-mobile #liQuote").addClass("active");
        $("#nav-mobile #liQuote a").addClass("active-text");
    } else if (id === "services") {
        $("#nav #liServices").addClass("active");
        $("#nav #liServices a").addClass("active-text");

        $("#nav-mobile #liServices").addClass("active");
        $("#nav-mobile #liServices a").addClass("active-text");
    } else if (id === "aboutus") {
        $("#nav #liAboutUs").addClass("active");
        $("#nav #liAboutUs a").addClass("active-text");

        $("#nav-mobile #liAboutUs").addClass("active");
        $("#nav-mobile #liAboutUs a").addClass("active-text");
    }
}

///**************************************************************************************************
// * Toggle the navigation menu so the active page menu link reflects the page the user is currently 
// * on. This is done by changing the text color.
// * 
// * This will also handle the side navigiation menu
// *************************************************************************************************/
//function toggleNavigationMenu() {

//    // Find and remove the active class
//    $("#nav").find(".active").removeClass("active");
//    $("#nav").find(".active-text").removeClass("active-text");

//    if ($("#home").length > 0) {
//        $("#nav #liHome").addClass("active");
//        $("#nav #liHome a").addClass("active-text");

//        $("#nav-mobile #liHome").addClass("active");
//        $("#nav-mobile #liHome a").addClass("active-text");
//    } else if ($("#quote").length > 0) {
//        $("#nav #liQuote").addClass("active");
//        $("#nav #liQuote a").addClass("active-text");

//        $("#nav-mobile #liQuote").addClass("active");
//        $("#nav-mobile #liQuote a").addClass("active-text");
//    } else if ($("#services").length > 0) {
//        $("#nav #liServices").addClass("active");
//        $("#nav #liServices a").addClass("active-text");

//        $("#nav-mobile #liServices").addClass("active");
//        $("#nav-mobile #liServices a").addClass("active-text");
//    } else if ($("#aboutus").length > 0) {
//        $("#nav #liAboutUs").addClass("active");
//        $("#nav #liAboutUs a").addClass("active-text");

//        $("#nav-mobile #liAboutUs").addClass("active");
//        $("#nav-mobile #liAboutUs a").addClass("active-text");
//    }
//}