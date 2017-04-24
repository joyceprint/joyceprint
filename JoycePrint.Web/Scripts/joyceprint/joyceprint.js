"use strict";

/**************************************************************************************************
 * Document Ready 
 *
 * Runs the initialization function for the different javascript files based on the page
 * that has been loaded
 *************************************************************************************************/
$(document).ready(function () {    

    initializeMaterializeFunctionality();

    initializeNavFunctionality();

    // If the quote view is loaded
    if ($("#quote").length > 0) {
        initializeQuote();

        //jalidate.initializeValidation("frm-quote");
    }
});