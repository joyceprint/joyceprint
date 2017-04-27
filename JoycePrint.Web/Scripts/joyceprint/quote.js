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
 * Wire up the clear button on the quote form
 *************************************************************************************************/
function setupClearButton() {
    $("#frm-quote button[type='reset']").click(function (e) {
        resetRecaptcha();
        
        var listOfInputs = ["Contact_Company", "Contact_Name", "Contact_Email", "Contact_Phone", "Enquiry_Message"];

        jalidate.resetValidation("frm-quote", listOfInputs);
    });
}

/**************************************************************************************************
 * Wire up the submit button on the quote form
 *************************************************************************************************/
function setupSubmitButton() {

    $("#frm-quote button[type='button']")
        .click(function (e) {

            var formId = "frm-quote";

            // Get the form
            var form = { target: $("#frm-quote")[0] };            
            
            showLoader();

            // Check if it's valid
            if (jalidate.validate(formId)) {

                $.ajax({
                        url: "/quote",
                        method: "POST",
                        cache: false,
                        data: $("#frm-quote").serialize()
                    })
                    .fail(function(jqXHR, textStatus) {
                        hideLoader();
                        
                        HandleAjaxError(jqXHR, textStatus);
                    })
                    .done(function(data) {
                        hideLoader();

                        if (data.modalView) {
                            showModal(data.modalView);
                        }
                        else {
                            loadView(data.view, data.target);

                            // Reinitialize the quote view if the entire view is returned from the server with validation errors
                            initializeQuote();
                        }
                    });
            } else {
                hideLoader();
            }            
        });
}