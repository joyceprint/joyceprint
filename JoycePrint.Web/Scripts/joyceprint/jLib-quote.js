"use strict";

/**************************************************************************************************
 * Quote Library
 *
 * Contains the code required for the quote page
 * The functionality contained in this file will be added to the jLib.quote global object
 *  
 *************************************************************************************************/
var jLib = (function (parent, $) {
    
    // Create the quote sub module off the parent, if it does not exist
    var subModule = parent.quote = parent.quote || {};

/**************************************************************************************************
 * PUBLIC METHODS
 *************************************************************************************************/

    /**************************************************************************************************
     * Quote Page Javascript Functionality
     *
     * Call this function to initialize the javascript required for the quote page
     *************************************************************************************************/
    subModule.initQuote = function () {

        subModule.setupSubmitButton();
    
        subModule.setupClearButton();

        jLib.validation.initValidation("frm-quote");       
    }

    /**************************************************************************************************
     * Wire up the clear button on the quote form
     *************************************************************************************************/
    subModule.setupClearButton = function () {
        $("#frm-quote button[type='reset']").click(function (e) {

            if ($("#" + jLib.recaptcha.recaptchaId).length > 0) {
                jLib.recaptcha.resetRecaptcha();
            }
            
            var listOfInputs = ["Contact_Company", "Contact_Name", "Contact_Email", "Contact_Phone", "Enquiry_Message"];

            jLib.validation.resetValidation("frm-quote", listOfInputs);
        });
    }

    /**************************************************************************************************
     * Wire up the submit button on the quote form
     *************************************************************************************************/
    subModule.setupSubmitButton = function () {

        $("#frm-quote button[type='button']")
            .click(function (e) {

                var formId = "frm-quote";

                jLib.loading.showLoader();

                // Check if it's valid
                if (jLib.validation.validate(formId)) {

                    var formData = new FormData($("#frm-quote").get(0));

                    $.ajax({
                            url: "/quote",
                            method: "POST",
                            cache: false,
                            contentType: false, // Not to set any content header  
                            processData: false, // Not to process data                         
                            data: formData,
                            headers: {                            
                                '__RequestVerificationToken': $("input[name='__RequestVerificationToken']").val()
                            }
                        })
                        .fail(function(jqXHR, textStatus) {                        
                            jLib.error.HandleAjaxError(jqXHR, textStatus);
                        })
                        .done(function(data) {
                            jLib.loading.hideLoader();

                            if (data.modalView) {
                                jLib.notify.showModal(data.modalView);
                            }
                            else {
                                jLib.notify.loadView(data.view, data.target);

                                // Reinitialize the quote view if the entire view is returned from the server with validation errors
                                jLib.quote.initQuote();

                                jLib.materialize.handleMaterializeForm("frm-quote");
                            }
                        });
                } else {
                    jLib.loading.hideLoader();
                }            
            });
    }

    // Return the parent, this allows multiple files to contribute to the same module
    return parent;

}(jLib || {}, jQuery));