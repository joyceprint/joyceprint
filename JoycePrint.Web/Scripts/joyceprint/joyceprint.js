"use strict";

/**************************************************************************************************
 * Document Ready 
 *
 * Runs the initialization function for the different javascript files based on the page
 * that has been loaded
 *************************************************************************************************/
$(document).ready(function () {
    
    initializeNavFunctionality();

    // If the home view is loaded
    if ($("#home").length > 0) {
        initializeHomeFunctionality();
    }

    // If the quote view is loaded
    if ($("#quote").length > 0) {
        initializeQuote();

        jalidate.initializeValidation("frm-quote");
    }

    // If the about us view is loaded
    if ($("#aboutus").length > 0) {
        initializeAboutUs();
    }
});