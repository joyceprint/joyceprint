"use strict";

/**************************************************************************************************
 * Quote Page Javascript Functionality
 *
 * Call this function to initialize the javascript required for the quote page
 *************************************************************************************************/
function initializeQuote() {

    setupSubmitButton();

    setupClearButton();
}

/**************************************************************************************************
 *
 *************************************************************************************************/
function setupClearButton() {
    $("#frm-quote button[type='reset']").click(function (e) {
        resetRecaptcha();
    });
}

/**************************************************************************************************
 *
 *************************************************************************************************/
function setupSubmitButton() {

    $("#frm-quote button[type='button']")
        .click(function (e) {
            // 1 - check if form is valid perform operation
            // 2 - if valid send post
            // 3 - if invalid display validation errors

            // Get the form
            var form = { target: $("#frm-quote")[0] };

            // Check if it's valid
            if (jalidate.validate(form)) {

                $.ajax({
                    url: "/quote",
                    method: "POST",
                    cache: false,
                    data: $("#frm-quote").serialize()
                })
                    .fail(function (jqXHR, textStatus) {
                        HandleAjaxError(jqXHR, textStatus);
                    })
                    .done(function (data) {
                        showModal(data.modalView);
                    });
            }
        });
}