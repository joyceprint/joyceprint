﻿/**************************************************************************************************
 * Validation Extension for jQuery that uses materialize
 *
 *************************************************************************************************/
(function (jalidate, $, undefined) {

/**************************************************************************************************
 * PUBLIC PROPERTIES
 **************************************************************************************************/

    // The response from the captcha
    jalidate.captchaResponse = "";

/**************************************************************************************************
 * PUBLIC METHODS
 **************************************************************************************************/

    // NEXT: FINSIH THE VALIDATION REGULAR EXPRESSIONS - TESTS MUST BE CREATED FOR THESE
    //          WHEN CHECKING FOR THE VALIDATION MESSAGE WE HAVE TO CHECK IN BOTH THE SPAN AND THE LABEL

    /***************************************************************************************     
     * Initializse the validation object for the form.
     * 
     * The errorPlacement and onSuccess methods are overridden
     * This should only have to happen once, however if the validation object is lost
     * this function will need to be called to re initialize validation     
     ***************************************************************************************/
    jalidate.initializeValidation = function(formId) {
        // Create the validator for the form
        var validator = ($("#" + formId)).validate();

        // Override the default mvc error operation
        validator.settings.errorPlacement = overrideErrorPlacement();

        // Override the default mvc success operation
        validator.settings.success = overrideOnSuccess();
    }

    /***************************************************************************************     
     * Performs validation on the form by overriding some of the functionality of the 
     * following plugins
     * 
     * unobtrusive js [https://github.com/aspnet/jquery-validation-unobtrusive]
     * materializecss [http://materializecss.com/]
     ***************************************************************************************/
    jalidate.validate = function (formId) {
        var formValid = false;

        // Create the validator for the form
        var validator = ($("#" + formId)).validate();
     
        // Validate the entire form
        formValid = validator.form();

        // Cancel form submit if validation fails
        if (!formValid) {
            if (event.preventDefault) event.preventDefault();

            displayValidationError();
        }
        else if (!checkRecaptcha()) {
            // Cancel form submit if recaptcha check fails
            if (event.preventDefault) event.preventDefault();

            // Form is invalid, recaptcha check not completed
            formValid = false;

            displayRecaptchaError();            
        }
        
        return formValid;
    }

    /***************************************************************************************
     * Resets the validation for the form and inputs supplied
     ***************************************************************************************/
    jalidate.resetValidation = function (formId, listOfInputs) {

        if (!listOfInputs) {
            //listOfInputs = getInputsForForm(formId);
        }

        resetValidation(formId, listOfInputs);
    }

    function tt() {
        
        // onfocusout
        // onkeyup
    }
/**************************************************************************************************
    * PRIVATE METHODS
**************************************************************************************************/

    /***************************************************************************************
     * This is a function that is required from the plugin
     * unobtrusive js [https://github.com/aspnet/jquery-validation-unobtrusive]
     * 
     * TODO: See if i can call this function from inside unobtrusive.js
     ***************************************************************************************/
    function escapeAttributeValue(value) {
        // As mentioned on http://api.jquery.com/category/selectors/
        return value.replace(/([!"#$%&'()*+,./:;<=>?@\[\\\]^`{|}~])/g, "\\$1");
    }

    /***************************************************************************************
    * This function overrides the default success display for mvc.
    * 
    * CHANGES
    * The invalid class is removed, no other changes to it.
    *     
    * unobtrusive js [https://github.com/aspnet/jquery-validation-unobtrusive]     
    ***************************************************************************************/
    function overrideOnSuccess() {

        var onSuccess = function (error, inputElement) {
            var container = error.data("unobtrusiveContainer"),
                replaceAttrValue = container.attr("data-valmsg-replace"),
                replace = replaceAttrValue ? $.parseJSON(replaceAttrValue) : null;

            if (container) {
                container.addClass("field-validation-valid").removeClass("field-validation-error");
                error.removeData("unobtrusiveContainer");

                if (replace) {
                    // This removes the text in the span making it take up no space so it would be hidden
                    container.empty();
                    $(inputElement).removeClass("invalid");
                    $(inputElement).prev().removeClass("orange-text danger-text").addClass("success-text");
                }
            }
        };

        return onSuccess;
    }

    /***************************************************************************************
     * This function overrides the default error display for mvc.
     * By default the error is written to a span, which is generated by ValidationMessageFor
     * 
     * CHANGES
     * The span is hidden, no other changes to it. 
     * [TODO: If the span is not used for anything else we can just remove if]
     * 
     * The class invalid will be added to the input to extend the materialize 
     * functionality. 
     * Custom CSS will also cause the icon, label, and background to change
     * 
     * unobtrusive js [https://github.com/aspnet/jquery-validation-unobtrusive]     
     ***************************************************************************************/
    function overrideErrorPlacement() {
        var errorPlacement = function (error, inputElement) {
            
            var formId = getFormId(inputElement);
            
            // Find the span created by ValidationMessageFor method
            var container = $("#" + formId).find("[data-valmsg-for='" + escapeAttributeValue(inputElement[0].name) + "']"),

            // Get the replace attribute from the span
            replaceAttrValue = container.attr("data-valmsg-replace"),

            // If the replaceAttrValue is null, set replace to null otherwise parse it
            replace = replaceAttrValue ? $.parseJSON(replaceAttrValue) !== false : null;

            // Remove the inputs valid class and add the error class
            container.removeClass("field-validation-valid").addClass("field-validation-error");

            // Hide the default mvc validation span
            $(".field-validation-error").hide();

            // Store the span in a data element
            error.data("unobtrusiveContainer", container);

            // If the ValidationMessageFor is used the error will be added to the span, otherwise the error is hidden
            if (replace) {
                // Empty the span
                container.empty();

                // This somehow fills the span with the correct error message
                // Probably using the validatejs functions to read the rules - anyway it's faster to let it do it's thing and use the message :D
                error.removeClass("input-validation-error").appendTo(container);

                // Get the error message from the span                
                var errorMessage = $("#" + escapeAttributeValue(inputElement[0].id) + "-error").text();

                // Add the invaid class to the input for materializecss classes to take effect
                if ($(inputElement).hasClass("input-validation-error")) {                    
                    $(inputElement).addClass("invalid");
                }

                // Use css with the after keyword to attach it to the label
                // Since after is not part of the dom, we add the validation message to a data attribute, and this is picked
                // up by the existing css and added
                $("label[for='" + escapeAttributeValue(inputElement[0].id) + "']").attr("validation-message", errorMessage);

                // Set the class on the prefix icon
                $(inputElement).prev().removeClass("success-text orange-text").addClass("danger-text");
            }
            else {
                error.hide();
                // Remove the css after content on the label
                $("label[for='" + escapeAttributeValue(inputElement[0].id) + "']").attr("validation-message", "");

                // The valid class should be removed, but the invalid class will have to be manually removed
                $(inputElement).removeClass("invalid");

                // Set the class on the prefix icon
                $(inputElement).prev().removeClass("success-text danger-text").addClass("orange-text");
            }
        };

        return errorPlacement;
    }

    /***************************************************************************************
    * Gets the form id that the input element is attached to        
    ***************************************************************************************/
    function getFormId(inputElement) {
        var formId;
        formId = $(inputElement).closest("form").attr("id");
        return formId;
    }

    /***************************************************************************************
     * Resets the validation for a form and a list of inputs
     * 
     * First the MVC errors must be removed
     * Then we can reset the value of the input and trigger the unobtrusive validation 
     * reset event
     ***************************************************************************************/
    function resetValidation(formId, listOfInputs) {

        $(listOfInputs).each(function () {

            // Clear the attribute value as this is what materialize will set
            $("#" + this).attr("value", "");

            // Reset the icon prefix
            $("#" + this).prev().removeClass("success-text danger-text").addClass("orange-text");

            // Trigger the reset event to reset the form, clear the unobtrusive validation objects
            $(this).trigger("reset.unobtrusiveValidation");
        });
    }

    /********************************************************************************************
     * Check the recaptcha
     *
     * Since the user may have already performed the recaptcha before clicking the submit
     * button, we check the stored recatpcha response by sending it to our security controller
     *******************************************************************************************/
    function checkRecaptcha() {

        var validRecaptcha = false;

        $.ajax({
            url: "/security/recaptcha",
            method: "POST",
            cache: false,
            data: {
                "captchaResponse": jalidate.captchaResponse
            },
            dataType: "json",
            async: false
        })
            .done(function (data, textStatus, jqXHR) {
                if (data) {
                    var jsonData = JSON.parse(data);

                    if (jsonData && jsonData.success) {
                        validRecaptcha = true;
                    }
                }

                // Reset the recaptcha response after using it
                jalidate.captchaResponse = "";
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                // We should only end up here if our end failed
            });        

        return validRecaptcha;
    }

    /********************************************************************************************
     * Display a toast holding the error message if the recaptcha fails
     * 
     *******************************************************************************************/
    function displayRecaptchaError() {
        Materialize.toast("Please complete the recaptcha", 4000);
    }

    /********************************************************************************************
     * Display a toast holding the error message if the form validation fails
     * 
     *******************************************************************************************/
    function displayValidationError() {
        Materialize.toast("Validation errors occurred. Please confirm the fields and submit it again.", 4000);
    }    

}(window.jalidate = window.jalidate || {}, jQuery));