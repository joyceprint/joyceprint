/**************************************************************************************************
 * Validation Extension for jQuery that uses materialize
 *
 *************************************************************************************************/
(function (jalidate, $, undefined) {

    // Private Property
    var jbort = false;

    // Private Property - The icon for the input field that's being validated
    var icon;

    // Private Property - The input field that's being validated
    var input;

    // Private Property - The message to display when validation fails
    var message;

    // Private Property - The validation message label
    var label;

    // Public Property
    jalidate.validEvent = "valid";

    // Public Property
    jalidate.invalidEvent = "invalid";

    // Public Property
    jalidate.version = "v1.0";

    // Public Property - Css class name of the icon in a required state
    jalidate.requiredCSS = "orange-text";

    // Public Property - Css class name of the icon in a valid state
    jalidate.iconValidCSS = "success-text";

    // Public Property - Css class name of the icon in a invalid state
    jalidate.iconInvalidCSS = "danger-text";

    // Public Property - Css class name of the valid state
    jalidate.validCSS = "valid";

    // Public Property - Css class name of the invalid state
    jalidate.invalidCSS = "invalid";

    // Public Property - Css class name of the touched state
    jalidate.touched = "touched";

    // Public Property - Css class name of the touched state
    jalidate.validationMessageLabelName = "val-msg";

    // Public Method - Set the display using the valid styles
    jalidate.setValidDisplay = function (field, additionalFields, validationEvents) {
        try {

            jalidate.icon = additionalFields[0];
            jalidate.input = field;

            if (!runEvent(validationEvents, jalidate.validEvent)) return;
            if (!hasClass(jalidate.touched, jalidate.input)) return;

            switchValidationMessage();

            removeClass(jalidate.requiredCSS, jalidate.icon);
            removeClass(jalidate.iconInvalidCSS, jalidate.icon);
            addClass(jalidate.iconValidCSS, jalidate.icon);

            removeClass(jalidate.invalidCSS, jalidate.input);
            removeClass(jalidate.requiredCSS, jalidate.input);
            addClass(jalidate.validCSS, jalidate.input);
        } catch (e) {
            console.log(e);
        }
    }

    // Public Method - Set the display using the invalid styles
    jalidate.setInvalidDisplay = function (field, additionalFields, validationEvents) {
        try {
            jalidate.icon = additionalFields[0];
            jalidate.input = field;

            if (!runEvent(validationEvents, jalidate.invalidEvent)) return;
            if (!hasClass(jalidate.touched, jalidate.input)) return;

            switchValidationMessage();

            removeClass(jalidate.requiredCSS, jalidate.icon);
            removeClass(jalidate.iconValidCSS, jalidate.icon);
            addClass(jalidate.iconInvalidCSS, jalidate.icon);

            removeClass(jalidate.validCSS, jalidate.input);
            removeClass(jalidate.requiredCSS, jalidate.input);
            addClass(jalidate.invalidCSS, jalidate.input);          
        } catch (e) {
            console.log(e);
        }
    }

    // Public Method - Set the display using the required styles
    jalidate.setRequiredDisplay = function (field, additionalFields, validationEvents) {
        try {

            jalidate.icon = additionalFields[0];
            jalidate.input = field;

            if (!runEvent(validationEvents, jalidate.validEvent)) return;
            if (!hasClass(jalidate.touched, jalidate.input)) return;

            switchValidationMessage(true);

            removeClass(jalidate.iconValidCSS, jalidate.icon);
            removeClass(jalidate.iconInvalidCSS, jalidate.icon);
            addClass(jalidate.requiredCSS, jalidate.icon);

            removeClass(jalidate.invalidCSS, jalidate.input);
            removeClass(jalidate.validCSS, jalidate.input);
            //addClass(jalidate.requiredCSS, jalidate.input); // This changes to color of the label to orange
        } catch (e) {
            console.log(e);
        }
    }

    // Public Method - Bind an event listener to perform validation
    //
    jalidate.bindValidator = function (field, additionalFields, listener, validationEvents) {

        field.addEventListener(listener, function (event) {            
            if (event.target.checkValidity()) {
                jalidate.setValidDisplay(event.target, additionalFields, validationEvents);
            }
            else {
                jalidate.setInvalidDisplay(event.target, additionalFields, validationEvents);
            }
        });
    }

    // Basic legacy validation checking
    jalidate.legacyValidation = function (field) {

        var
            valid = true,
            val = field.value,
            type = field.getAttribute("type"),
            chkbox = (type === "checkbox" || type === "radio"),
            required = field.getAttribute("required"),
            minlength = field.getAttribute("minlength"),
            maxlength = field.getAttribute("maxlength"),
            pattern = field.getAttribute("pattern");

        // disabled fields should not be validated
        if (field.disabled) return valid;

        // value required?
        valid = valid && (!required ||
            (chkbox && field.checked) ||
            (!chkbox && val !== "")
        );

        // minlength or maxlength set?
        valid = valid && (chkbox || (
            (!minlength || val.length >= minlength) &&
            (!maxlength || val.length <= maxlength)
        ));

        // test pattern
        if (valid && pattern) {
            pattern = new RegExp(pattern);
            valid = pattern.test(val);
        }

        return valid;
    }

    // TODO: Check what this is even for?
    // Private Method - Check if the field is valid
    function validate(field, additionalFields) {

        if (field.nodeName === "INPUT" || field.nodeName === "TEXTAREA") {
            icon = $(field).prev();
            label = $(field).next();
        } else if (field.nodeName === "SELECT") {
            icon = $(field).closest("div").prev();
            label = $(field).closest("div").next();
        }

        if (field.checkValidity()) {
            //jalidate.setValidDisplay(field);

            //get the fields differently for select
            jalidate.setInvalidDisplay($(field)[0], [icon[0], label[0]], ["valid", "invalid"]);
        } else {
            //jalidate.setInvalidDisplay(field);
            jalidate.setInvalidDisplay($(field)[0], [icon[0], label[0]], ["valid", "invalid"]);
        }
    }

    // Private Method - Check if the class is present and if not, add it
    function addClass(ccsClass, element) {
        if (!$(element).hasClass(ccsClass))
            $(element).addClass(ccsClass);
    }

    // Private Method - Check if the class is present and if it is, remove it
    function removeClass(ccsClass, element) {
        if ($(element).hasClass(ccsClass))
            $(element).removeClass(ccsClass);
    }

    // Private Method - Check if the class is present
    function hasClass(ccsClass, element) {
        if ($(element).hasClass(ccsClass) === true)
            return true;
        else
            return false;
    }

    // Private Method - Get the validation message to display based on the type of validation that failed
    function getValidationMessage() {

        if (jalidate.input.checkValidity()) {
            jalidate.message = null;
        } else if (jalidate.input.validity.valueMissing === true) {
            jalidate.message = $(jalidate.input).attr("data-val-req-msg");
        } else if (jalidate.input.validity.patternMismatch === true) {
            jalidate.message = $(jalidate.input).attr("data-val-pat-msg");
        } else {
            jalidate.message = $(jalidate.input).attr("data-val-msg");
        }

        // This is a catch all that will return the default message
    }

    // Private Method - Get the validation message to display based on the type of validation that failed
    function runEvent(validationEvents, event) {
        return validationEvents.includes(event);
    }

    // Private Method - Switch the validation message to the current message on the field
    function switchValidationMessage(toEmpty) {

        if (toEmpty) {
            jalidate.message = "";
        } else {
            getValidationMessage();
        }

        // TODO: figure out how to use javascript global variables - can we or do i have to pass everything
        jalidate.label = $(jalidate.input).nextUntil(jalidate.validationMessageLabelName);

        for (var index = 0; index < jalidate.label.length; index++) {
            if (hasClass(jalidate.validationMessageLabelName, jalidate.label[index])) {
                jalidate.label[index].textContent = jalidate.message;
                //Materialize.updateTextFields();                
                break;
            }
        }
    }

}(window.jalidate = window.jalidate || {}, jQuery));