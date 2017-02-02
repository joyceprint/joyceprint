"use strict";

/**************************************************************************************************
 * Quote Page Javascript Functionality
 *
 * Call this function to initialize the javascript required for the quote page
 *************************************************************************************************/
function initializeQuote() {

    initializeMaterializeSelect();
}

/**************************************************************************************************
 * Initialize the materialize select functionality
 *************************************************************************************************/
function initializeMaterializeSelect() {
    // Select - Single Initialize
    $("select:not([multiple])").material_select();

    // Select - Multiple Initialize
    //$('select').material_select();
}