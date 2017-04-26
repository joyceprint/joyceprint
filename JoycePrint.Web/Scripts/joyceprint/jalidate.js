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

    // TODO: CHECK FOR
    // Validation is working on submit
    // when failure on submit, then corrected, the input updates without hitting submit [success when typing]
    // when failure on a good field, the input updates without hitting submit [failure when typing]
    // writing the css using the data attribute [http://stackoverflow.com/questions/5041494/selecting-and-manipulating-css-pseudo-elements-such-as-before-and-after-usin/21709814#21709814]
    // remove unused functions
    // put all in namespace, adding jquery in as itself
    // archive off the previos version of js valiation
    // PERFORM THE RECAPTCHA CHECK

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
        var validator = ($("#frm-quote")).validate();

        // Override the default mvc error operation
        validator.settings.errorPlacement = overrideErrorPlacement();

        // Override the default mvc success operation
        validator.settings.success = overrideOnSuccess();
        
        // Validate the entire form
        formValid = validator.form();

        // Cancel form submit if validation fails
        if (!formValid) {
            if (event.preventDefault) event.preventDefault();

            Materialize.toast("Validation errors occurred. Please confirm the fields and submit it again.", 4000);
        } else if (!checkRecaptcha()) {

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
            
            // TODO: The only issue is we have to have the form hard coded
            // Find the span created by ValidationMessageFor method
            var container = $("#frm-quote").find("[data-valmsg-for='" + escapeAttributeValue(inputElement[0].name) + "']"),

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

                    // Bug: because i'm adding this class it's not getting removed. can i extend the function that does this or override it?
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
     * Resets the validation for a form and a list of inputs
     * 
     * First the MVC errors must be removed
     * Then we can reset the value of the input and trigger the unobtrusive validation 
     * reset event
     ***************************************************************************************/
    function resetValidation(formId, listOfInputs) {

        removeMvcErrors(formId);

        $(listOfInputs).each(function () {

            // Clear the attribute value as this is what materialize will set
            $("#" + this).attr("value", "");

            // Reset the icon prefix
            $("#" + this).prev().removeClass("success-text danger-text").addClass("orange-text");

            // Trigger the reset event to reset the form, clear the unobtrusive validation objects
            $(this).trigger("reset.unobtrusiveValidation");
        });
    }

    /***************************************************************************************
     * Removes the MVC validation errors from a form
     * 
     * MVC validation errors will be added to a span element that is regenerated each time
     * validation fails. To remove the error message we have to remove this element
     ***************************************************************************************/
    function removeMvcErrors(formId) {
        $("#" + formId).find(".field-validation-error").remove();
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
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
            });

        return validRecaptcha;
    }

    /********************************************************************************************
    * Display a toast holding the error message if the recaptcha fails
    *******************************************************************************************/
    function displayRecaptchaError() {
        Materialize.toast("Please complete the recaptcha", 4000);
    }

}(window.jalidate = window.jalidate || {}, jQuery));