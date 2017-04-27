"use strict";

/**************************************************************************************************
 * Extensions for MaterializeCss
 *
 * All extensions for MaterializeCss are defined here
 *************************************************************************************************/

/**************************************************************************************************
 * Current entry point for this script
 * 
 *************************************************************************************************/
function initializeMaterializeFunctionality() {

    initializeMaterializeParallax();

    initializeMaterializeSelect();

    initializeMaterializeModal();
    
    initializeMobileMenu();
}

/**************************************************************************************************
 * Initialize the parallax functionality
 *
 *************************************************************************************************/
function initializeMaterializeParallax() {
    // Parallax init
    $(".parallax").parallax();
}

/**************************************************************************************************
 * Initialize the materialize select functionality
 * 
 *************************************************************************************************/
function initializeMaterializeSelect() {
    // Select - Single Initialize
    $("select:not([multiple])").material_select();

    // Select - Multiple Initialize
    //$('select').material_select();
}

/**************************************************************************************************
 * Initialize the materialize modal functionality
 * 
 *************************************************************************************************/
function initializeMaterializeModal() {
    $(".modal").modal({
        complete: function() { resetRecaptcha(); }
    });
}

/**************************************************************************************************
 * Initialize the side navigation menu for use on small screens and mobiles
 * 
 *************************************************************************************************/
function initializeMobileMenu() {
    // Initializes the side nav menu for mobile screens
    $(".button-collapse").sideNav();
}