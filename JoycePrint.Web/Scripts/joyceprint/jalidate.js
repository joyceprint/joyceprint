/**************************************************************************************************
 * Validation Extension for jQuery that uses materialize
 *
 *************************************************************************************************/
(function (jalidate, $, undefined) {

/**************************************************************************************************
* PUBLIC METHODS
**************************************************************************************************/
    
    /***************************************************************************************     
     * Performs validation on the form by overriding some of the functionality of the 
     * following plugins
     * 
     * unobtrusive js [https://github.com/aspnet/jquery-validation-unobtrusive]
     * materializecss [http://materializecss.com/]
     ***************************************************************************************/
    jalidate.validate = function(formId) {
        var formValid = false;

        // Create the validator for the form
        var validator = ($("#frm-quote")).validate();
        
        // Override the default mvc error operation
        validator.settings.errorPlacement = overrideErrorPlacement();

        // Override the default mvc success operation
        validator.settings.success = overrideOnSuccess();        

        // Validate the entire form
        formValid = validator.form();

        return formValid;
    }    

    /***************************************************************************************
     * Resets the validation for the form and inputs supplied
     ***************************************************************************************/
    jalidate.resetValidation = function(formId, listOfInputs) {
        
        if (!listOfInputs) {
            listOfInputs = getInputsForForm(formId);
        }

        resetValidation(formId, listOfInputs);
    }

    /***************************************************************************************
     * jQueryValidation - Option - submitHandler:
     * TODO: Implement this
     ***************************************************************************************/
    jalidate.validationSuccess = function () {

    }
    /***************************************************************************************
     * jQueryValidation - Option - invalidHandler: $.proxy(onErrors, form)
     * TODO: Implement this
     ***************************************************************************************/
    jalidate.validationError = function () {

    }

    /***************************************************************************************
     * jQueryValidation - Option - errorPlacement: $.proxy(onError, form)
     * TODO: Implement this
     ***************************************************************************************/
    jalidate.showValidationError = function () {

    }

    /***************************************************************************************
     * jQueryValidation - Option - success: $.proxy(onSuccess, form)
     * TODO: Implement this
     ***************************************************************************************/
    jalidate.showValidationSuccess = function () {

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
     * The span is hidden, no other changes to it. [TODO: If the span is not used for anything else we can just remove if]
     * 
     * The class invalid will be added to the input to extend the materialize 
     * functionality. 
     * Custom CSS will also cause the icon, label, and background to change
     * 
     * unobtrusive js [https://github.com/aspnet/jquery-validation-unobtrusive]     
     ***************************************************************************************/
    function overrideErrorPlacement() {
        var errorPlacement = function (error, inputElement) {

            // Since even valid elements get passed into this, we check first
            
            // TODO: The only issue is we have to have the form hard coded
            // Find the span created by ValidationMessageFor method
            var container = $("#frm-quote").find("[data-valmsg-for='" + escapeAttributeValue(inputElement[0].name) + "']"),

            // Get the replace attribute from the span
            replaceAttrValue = container.attr("data-valmsg-replace"),

            // If the replaceAttrValue is null, set replace to null otherwise parse it
            replace = replaceAttrValue ? $.parseJSON(replaceAttrValue) !== false : null;

            // Remove the inputs valid class and add the error class
            container.removeClass("field-validation-valid").addClass("field-validation-error");

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
                var errorMessage = $("#" + inputElement[0].name + "-error").text();

                // TODO: use css to hit the rest of the elements with the valid and invalid class on the input being the driver
                // Add the invaid class to the input for materializecss classes to take effect
                if ($(inputElement).hasClass("input-validation-error")) {

                    // Bug: because i'm adding this class it's not getting removed. can i extend the function that does this or override it?
                    $(inputElement).addClass("invalid");
                }                

                // Use css with the after keyword to attach it to the label
                // Since after is not part of the dom, we add the validation message to a data attribute, and this is picked
                // up by the existing css and added
                $("label[for='" + escapeAttributeValue(inputElement[0].name) + "']").attr("validation-message", errorMessage);
            }
            else {
                error.hide();
                // Remove the css after content on the label
                $("label[for='" + escapeAttributeValue(inputElement[0].name) + "']").attr("validation-message", "");

                // The valid class should be removed, but the invalid class will have to be manually removed
                $(inputElement).removeClass("invalid");
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

    /***************************************************************************************
    * Get all the inputs for a form
    * 
    * TODO: This may be used in the future
    ***************************************************************************************/
    function getInputsForForm(formId) {
        return null;
    }

    /***************************************************************************************
     * 
     ***************************************************************************************/
    function getValidationOptions() {
        var options;

        options.submitHandler = function () {
            return false;
        }

        return options;
    }

    /***************************************************************************************
     * 
     ***************************************************************************************/
    function initializeValidation(form) {

        var options = getValidationOptions();
    }

}(window.jalidate = window.jalidate || {}, jQuery));