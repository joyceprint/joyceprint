/**************************************************************************************************
 * Validation
 *
 *************************************************************************************************/
$(document).ready(function () {

    handleCssHtmlRestriction();

    intializeValidation();
});

function intializeValidation() {

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
        
        // If the field is not required we stop processing
        if (!field.required) continue;

        // Ignore hidden fields
        if (field.type === "hidden") continue;

        // Ignore buttons, fieldsets, etc.
        if (field.nodeName !== "INPUT" && field.nodeName !== "TEXTAREA" && field.nodeName !== "SELECT") continue;
        
        // Bind focus for when the user clicks on the input
        jalidate.bindValidator(field, [field.previousElementSibling, field.nextElementSibling], "focus", ["valid", "invalid"]);

        // Bind blur for when the user leave the input
        jalidate.bindValidator(field, [field.previousElementSibling, field.nextElementSibling], "blur", ["valid", "invalid"]);

        // Bind keyup for when the user presses a key
        // This has special processing which will not trigger invalid styles on keyup, only valid events
        jalidate.bindValidator(field, [field.previousElementSibling, field.nextElementSibling], "keyup", ["valid"]);

        if (field.nodeName === "SELECT") {
            handleBindingForMaterializeDropDown(field);
        }
    }
}

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
                // TODO: Implement this
                field.setCustomValidity(LegacyValidation(field) ? "" : "error");
            }

            // Native browser check
            // TODO: This has to be added as a prototype function for the field?? other way??
            field.checkValidity();
        }
        else {

            // Native validation not available
            // TODO: Implelement this
            field.validity = field.validity || {};

            // Set to result of validation function
            field.validity.valid = LegacyValidation(field);

            // If "invalid" events are required, trigger it here            
            // Not sure what to do here yet            
        }

        if (field.validity.valid) {            
            jalidate.setValidDisplay(field);
        }
        else {            
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

/**************************************************************************************************
 * Creates an event that will add the touched class to required fields
 * This is required as the :visited pseudo class only applies to anchor tags
 *************************************************************************************************/
function handleCssHtmlRestriction() {

    // use $.fn.one here to fire the event only once.
    $(':required').one('blur keydown', function () {
        $(this).addClass('touched');
    });
}