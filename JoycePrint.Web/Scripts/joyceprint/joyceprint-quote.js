"use strict";

/**************************************************************************************************
 * Quote Page Javascript Functionality
 *
 * Call this function to initialize the javascript required for the quote page
 *************************************************************************************************/
function initializeQuote() {

    setupSubmitQuote();

}

/**************************************************************************************************
 * TODO: Move this to an error.js file
 *
 *************************************************************************************************/
function setupSubmitQuote() {

    $("#submitQuoteEnquiry").click(function (e) {
        e.preventDefault();

        $.ajax({
            url: "/quote",
            method: "POST",
            cache: false,
            data: {
                // TODO: figure out how to send this data
            }
        })
            .fail(function (jqXHR, textStatus) {
                HandleAjaxError(jqXHR, textStatus);
            })
            .done(function (data) {
                showModal(data);
            });
    });
}