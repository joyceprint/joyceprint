/**************************************************************************************************
 * Validation
 *
 *************************************************************************************************/
function initializeValidation() {

    initializeFormValidation();

    handleCssHtmlRestriction();
}

/**************************************************************************************************
*
 *************************************************************************************************/
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
        // May have to change this to use the property - undefined
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

        // TODO: There may be a bug here
        var additionalFields = getAdditionalFields(field);
        
        if (field.validity.valid) {
            jalidate.setValidDisplay($(field)[0], additionalFields, ["valid", "invalid"]);
        }
        else {
            jalidate.setInvalidDisplay($(field)[0], additionalFields, ["valid", "invalid"]);

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
*
 *************************************************************************************************/
function initializeFormValidation() {

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

        // If this is a drop down we need to perform special logic for the materialize select
        var isDropDown = false;
        if (field.className.contains("select-dropdown")) isDropDown = true;

        bindValidators(field, isDropDown);
    }
}

/**************************************************************************************************
*
 *************************************************************************************************/
function bindValidators(field, isDropDown) {

    var additionalFields = getAdditionalFields(field);
    
    var preEventFunction = "";
    if (isDropDown) {
        // Here the pre event function will readd the selected class to the active li element, materialize removes it for some reason
        preEventFunction = handleMaterializeSelectFeatureForBlurEvent;
    }

    // Bind blur for when the user leave the input    
    jalidate.bindValidator(field, additionalFields, "blur", ["valid", "invalid"], preEventFunction);

    // Bind keyup for when the user presses a key
    // This has special processing which will not trigger invalid styles on keyup, only valid events    
    jalidate.bindValidator(field, additionalFields, "keyup", ["valid"], preEventFunction);

    // Bind
    jalidate.bindValidator(field, additionalFields, "mousedown", ["valid", "invalid"], preEventFunction);

    // Bind the change event.
    // This enables the number input type to function correctly
    jalidate.bindValidator(field, additionalFields, "change", ["valid"]);
}

/**************************************************************************************************
 * Gets the icon and label for the input group
 *************************************************************************************************/
function getAdditionalFields(field) {
    var icon = null;
    var label = null;

    // If we get the select tag that has been initialized by materialize we have all ready accounted for it's input tag
    if (field.className.contains("initialized")) return;

    if (field.nodeName === "INPUT" || field.nodeName === "TEXTAREA") {

        // Handle the materialize drop downs
        if ($(field).hasClass("select-dropdown")) {
            icon = $(field).closest("div").prev();
            label = $(field).closest("div").next();

            // Why are we doing this?
            clearMaterializeSelections(field);
        } else {
            icon = $(field).prev();
            label = $(field).next();
        } //else if (field.nodeName === "SELECT") { }        
    }
    
    return [icon[0], label[0]];
}

/**************************************************************************************************
 * Global variable to hold the function that will fix the materialize select valiadation
 * issue.
 * This will be called in the jalidate.js script
 *************************************************************************************************/
var handleMaterializeSelectFeatureForBlurEvent = function (field, additionalFields) {
    
    var ul = $(field).next();
    var initialSelection = $(ul).find("li:first").text();

    $(ul).find("li.active").addClass("selected");
    
    if ($(field).val() === initialSelection) {
        jalidate.setInvalidDisplay(field, additionalFields, ["valid", "invalid"]);
    } else {
        jalidate.setValidDisplay(field, additionalFields, ["valid", "invalid"]);
    }

    // This instructs the jalidate function to not run it's validation as we've done it here
    return false;
}

/**************************************************************************************************
 *
 *************************************************************************************************/
function switchToRequiredDisplay(field) {
    
    var additionalFields = getAdditionalFields(field);

    jalidate.setRequiredDisplay($(field)[0], additionalFields, ["valid", "invalid"]);
}

/**************************************************************************************************
 *
 *************************************************************************************************/
function clearMaterializeSelections(field) {
    var ul = $(field).next("ul");
    $(ul).find("li.active.selected").removeClass("active selected");
    $(ul).find("li:first").addClass("active selected");

    var select = $(field).nextAll("select");
    $(select).find(":selected").removeAttr("selected");
    $(select).find("option:first").attr("selected", "selected");

    var selectedOption = $(ul).find("li.active.selected").text();
    $(field).attr("value", selectedOption);
}

/**************************************************************************************************
 * Creates an event that will add the touched class to required fields
 * This is required as the :visited pseudo class only applies to anchor tags
 *
 * NOTE: This must be run after the validation has been wired up
 *************************************************************************************************/
function handleCssHtmlRestriction() {

    // use $.fn.one here to fire the event only once.    
    $(':required').on('focus keydown', function () {
        if ($(this).hasClass("touched")) return;
        $(this).addClass('touched');
    });
}

/**************************************************************************************************
* jQuery Regular Expression Filter - 3rd Party
*
* http://james.padolsey.com/javascript/regex-selector-for-jquery/
 *************************************************************************************************/
jQuery.expr[':'].regex = function (elem, index, match) {
    var matchParams = match[3].split(','),
        validLabels = /^(data|css):/,
        attr = {
            method: matchParams[0].match(validLabels) ?
                        matchParams[0].split(':')[0] : 'attr',
            property: matchParams.shift().replace(validLabels, '')
        },
        regexFlags = 'ig',
        regex = new RegExp(matchParams.join('').replace(/^\s+|\s+$/g, ''), regexFlags);
    return regex.test(jQuery(elem)[attr.method](attr.property));
}