/**************************************************************************************************
 * Validation Extension for jQuery that uses materialize
 *
 *************************************************************************************************/
(function (jalidate, $, undefined) {

/**************************************************************************************************
* PRIVATE METHODS
**************************************************************************************************/

    /***************************************************************************************
     * Resets the validation for the form and inputs supplied
     ***************************************************************************************/
    jalidate.resetValidation = function(formId, listOfInputs) {
        
        if (!listOfInputs) {
            listOfInputs = getInputsForForm(formId);
        }

        resetValidation(formId, listOfInputs);
    }

/**************************************************************************************************
 * PRIVATE METHODS
**************************************************************************************************/
   
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

            // Clear the unobtrusive validation objects
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

}(window.jalidate = window.jalidate || {}, jQuery));