"use strict";
/**************************************************************************************************
 * Document Ready 
 *
 * Runs the initialization function for the different javascript files based on the page
 * that has been loaded
 *************************************************************************************************/
$(document).ready(function () {
    
    initializeNavFunctionality();

    if ($("#home").length > 0) {
        initializeHomeFunctionality();
    }

    if ($("#quote").length > 0) {
        initializeQuote();

        jalidate.initializeValidation("frm-quote");
    }

    if ($("#aboutus").length > 0) {
        initializeAboutUs();
    }
});