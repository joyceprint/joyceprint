"use strict";

/**************************************************************************************************
 * Home Page Javascript Functionality
 *
 * Call this function to initialize the javascript required for the home page
 *************************************************************************************************/
function initializeHomeFunctionality() {

    //initializeMaterializeCarousel();

    initializeMaterializeParallax();

    //positionParallaxContent();
}

///**************************************************************************************************
// *
// *************************************************************************************************/
//function positionParallaxContent() {
//    var content = $("#parallax-content");
    
//    var homeTop = $("#home").offset().top;

//    var viewportTop = $(window).scrollTop();

//    var viewportBottom = viewportTop + $(window).height();

//    var heightOnScreen = viewportBottom - homeTop;

//    var paddingTop = heightOnScreen / 3;

//    $(content).css("padding-top", paddingTop);
//}

///**************************************************************************************************
// * Initialize the carousel for the home page
// *
// *************************************************************************************************/
//function initializeMaterializeCarousel() {
//    // Carousel init
//    $('.carousel.carousel-slider').carousel({ full_width: true });
//}

/**************************************************************************************************
 * Initialize the parallax functionality for the home page
 *
 *************************************************************************************************/
function initializeMaterializeParallax() {
    // Parallax init
    $(".parallax").parallax();
}