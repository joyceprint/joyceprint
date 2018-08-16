"use strict";

/**************************************************************************************************
 * Document Ready 
 *
 * Runs the initialization function for the different javascript files based on the page
 * that has been loaded
 *************************************************************************************************/
$(document).ready(function () {    

    jLib.materialize.initMaterialize();

    jLib.nav.initNav();

    // If the quote view is loaded
    if ($("#quote").length > 0) {
        jLib.quote.initQuote();
    }
    
    // If we're coming from the docketbooks.ie site, make sure the user can see the get a quote header
    if (document.referrer.contains("docketbooks.ie")
        && document.documentURI.contains("#quote"))
        $("a[href='#quote']").click();
});