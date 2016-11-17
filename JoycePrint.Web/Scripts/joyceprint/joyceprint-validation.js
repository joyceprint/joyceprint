/**************************************************************************************************
 * Validation
 *
 *************************************************************************************************/
$(document).ready(function () {

    var form = document.getElementById("frm-quote");

    // Turn or native validation for compatibilty
    form.noValidate = true;

    // Set handler to validate the form
    // Onsubmit used for easier cross-browser compatibility
    form.onsubmit = validateForm;

    // Loop all fields
    for (f = 0; f < form.elements.length; f++) {

        // Get the field from the form collection
        var field = form.elements[f];

        // Ignore buttons, fieldsets, etc.
        if (field.nodeName !== "INPUT" && field.nodeName !== "TEXTAREA" && field.nodeName !== "SELECT") continue;

        // Ignore hidden fields
        if (field.type === "hidden") continue;

        if (!field.required) continue;

        jalidate.bindValidator(field, [field.previousElementSibling, field.nextElementSibling], "focus");
        jalidate.bindValidator(field, [field.previousElementSibling, field.nextElementSibling], "blur");
        jalidate.bindValidator(field, [field.previousElementSibling, field.nextElementSibling], "keyup");

        if (field.nodeName === "SELECT") {
            handleBindingForMaterializeDropDown(field);
        }
    }
});

function handleBindingForMaterializeDropDown(field) {

    var selectWrapper = $(field).closest("div");
    var selectInput = $(selectWrapper).find("input");

    var icon = $(selectWrapper).prev();
    var label = $(selectWrapper).next()

    // To get the variables to be passed as javascript variables rather than jQeury variable we need to access the first element in the jQuery object    
    jalidate.bindValidator(selectInput[0], [icon[0], label[0]], "input");
    jalidate.bindValidator(selectInput[0], [icon[0], label[0]], "change");
}

function validateForm(event) {

    // Fetch cross-browser event object and form node
    event = (event ? event : window.event);
    var
		form = (event.target ? event.target : event.srcElement),
		f, field, formvalid = true;

    // Loop all fields
    for (f = 0; f < form.elements.length; f++) {

        // Get the field from the form collection
        field = form.elements[f];

        // Ignore buttons, fieldsets, etc.
        if (field.nodeName !== "INPUT" && field.nodeName !== "TEXTAREA" && field.nodeName !== "SELECT") continue;

        // Ignore hidden fields
        if (field.type === "hidden") continue;

        // Is native browser validation available?
        if (typeof field.willValidate !== "undefined") {

            // Native validation available
            if (field.nodeName === "INPUT" && field.type !== field.getAttribute("type")) {

                // Input type not supported! Use legacy JavaScript validation
                field.setCustomValidity(LegacyValidation(field) ? "" : "error");
            }

            // Native browser check
            field.checkValidity();
        }
        else {

            // Native validation not available
            field.validity = field.validity || {};

            // Set to result of validation function
            field.validity.valid = LegacyValidation(field);

            // If "invalid" events are required, trigger it here            
        }

        if (field.validity.valid) {
            // Remove error styles and messages
            jalidate.setValidDisplay(field);
        }
        else {
            // Style field, show error, etc.
            jalidate.setInvalidDisplay(field);

            // Form is invalid
            formvalid = false;
        }
    }

    // Cancel form submit if validation fails
    if (!formvalid) {
        if (event.preventDefault) event.preventDefault();
    }

    //return formvalid;
    return false;
}