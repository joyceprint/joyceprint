"use strict";

/**************************************************************************************************
 * Quote Page Javascript Functionality
 *
 * Call this function to initialize the javascript required for the quote page
 *************************************************************************************************/
function initializeQuote() {

    setupSubmitButton();

    setupClearButton();

    jalidate.initializeValidation("frm-quote");
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

                //jnalytics.collectEvent("button", "quote", "quote-request");

                var formData = getFormData();                
                //var formData = $("#frm-quote").serialize();

                $.ajax({
                        url: "/quote",
                        method: "POST",
                        cache: false,
                        data: formData,
                        headers: {                            
                            '__RequestVerificationToken': $("input[name='__RequestVerificationToken']").val()
                        }
                    })
                    .fail(function(jqXHR, textStatus) {                        
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

/**************************************************************************************************
 * 
 *************************************************************************************************/
function getFormData() {

    var formData;

    //formData = $("#frm-quote").serialize();

    formData = new FormData();
    
    var fileUpload = $("#Attachment_Attachments").get(0);
    var files = fileUpload.files;

    if (files) {
        // Looping over all files and add it to FormData object  
        for (var i = 0; i < files.length; i++) {
            console.log('(files[i].name:' + files[i].name);
            formData.append('productImg', files[i]);
        }
    }

    // You can update the jquery selector to use a css class if you want
    $("input[type='text'").each(function (x, y) {
        formData.append($(y).attr("name"), $(y).val());
    });

    return formData;
}