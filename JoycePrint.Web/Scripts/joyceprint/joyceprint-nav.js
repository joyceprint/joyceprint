"use strict";
/**************************************************************************************************
 * Navigation Menu Javascript Functionality
 *
 * Call this function to initialize the javascript required for the navigation menu
 *************************************************************************************************/
function initializeNavFunctionality() {

    //toggleNavigationMenu();

    initializeMobileMenu();

    initializeScrollFireMenu();
}

/**************************************************************************************************
 * Initialize the side navigation menu for use on small screens and mobiles
 *************************************************************************************************/
function initializeMobileMenu() {
    $(".button-collapse").sideNav();
}

/**************************************************************************************************
 * Initialize the scroll fire for the navigation menu
 *************************************************************************************************/
function initializeScrollFireMenu() {
    Materialize.scrollFire(getScrollFireOptions());
}

/**************************************************************************************************
 * This sets up the elements to watch and scroll fire on
 *************************************************************************************************/
function getScrollFireOptions() {
    var options = [
      {
          selector: '#home', offset: 50, callback: function (el) {
              toggleNavigationMenuWithScrollFire(el.id);              
          }
      },
      {
          selector: '#services', offset: 600, callback: function (el) {
              toggleNavigationMenuWithScrollFire(el.id);              
          }
      },
      {
          selector: '#aboutus', offset: 600, callback: function (el) {
              toggleNavigationMenuWithScrollFire(el.id);              
          }
      }
    ];

    return options;
}

/**************************************************************************************************
 * Toggle the navigation menu so the active page menu link reflects the view the user has currently 
 * scrolled on to. This is done by changing the text color.
 * 
 * This will also handle the side navigiation menu
 * 
 * Because we re-initialize the scroll fire with each run of this function, the scroll fire will
 * run multiple times for the same offset, this is currently not an issue
 *************************************************************************************************/
function toggleNavigationMenuWithScrollFire(id) {

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

    initializeScrollFireMenu();
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