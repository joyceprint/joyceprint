"use strict";
/**************************************************************************************************
 * Home Page Javascript Functionality
 *
 * Call this function to initialize the javascript required for the home page
 *************************************************************************************************/
function initializeHomeFunctionality() {
        
    initializeMaterializeCarousel()
}

/**************************************************************************************************
 * Initialize the carousel for the home page
 *
 *************************************************************************************************/
function initializeMaterializeCarousel() {
    // Carousel init
    $('.carousel.carousel-slider').carousel({ full_width: true });
}