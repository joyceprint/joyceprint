/**************************************************************************************************
 * Home Page Javascript Functionality
 *
 * Call this function to initialize the javascript required for the home page
 *************************************************************************************************/
function initializeHomeFunctionality() {
        
    initializeMaterializeCarousel()
}

/**************************************************************************************************
 *
 *
 *************************************************************************************************/
function initializeMaterializeCarousel() {
    $('.carousel.carousel-slider').carousel({ full_width: true });

    //// Carousel init
    //$('.carousel').carousel();
    //// Slider init
    //$('.carousel-slider').slider({ full_width: true });
    ////$('.carousel.carousel-slider').carousel({ full_width: true });
}