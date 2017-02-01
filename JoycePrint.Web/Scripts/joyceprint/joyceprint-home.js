"use strict";
/**************************************************************************************************
 * Home Page Javascript Functionality
 *
 * Call this function to initialize the javascript required for the home page
 *************************************************************************************************/
function initializeHomeFunctionality() {

    //initializeMaterializeCarousel();

    initializeMaterializeParallax();
}

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