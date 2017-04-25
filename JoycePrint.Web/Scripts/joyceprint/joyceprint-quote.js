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
        
        var listOfInputs = ["Contact_Company", "Contact_Name", "Contact_Email", "Contact_Phone", "Enquiry_Message"];

        jalidate.resetValidation("frm-quote", listOfInputs);
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
            
            var formId = "frm-quote";

            // Get the form
            var form = { target: $("#frm-quote")[0] };            
            
            // Check if it's valid
            if (jalidate.validate(formId)) {

                showLoader();

                $.ajax({
                    url: "/quote",
                    method: "POST",
                    cache: false,
                    data: $("#frm-quote").serialize()
                })
                    .fail(function (jqXHR, textStatus) {
                        hideLoader();
                        alert("f");
                        HandleAjaxError(jqXHR, textStatus);
                    })
                    .done(function (data) {
                        hideLoader();
                        alert("s");
                        if (data.modalView)
                            showModal(data.modalView);
                        else
                            loadView(data.view);
                            initializeQuote();
                    });
            }
        });
}