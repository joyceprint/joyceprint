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
    var message

    //Public Property - The validation message label
    jalidate.validationMessage;

    // Public Property
    jalidate.validEvent = "valid";

    // Public Property
    jalidate.invalidEvent = "invalid";

    // Public Property
    jalidate.version = "v1.0";

    // Public Property - Css class name of the icon in a required state
    jalidate.required = "orange-text";

    // Public Property - Css class name of the icon in a valid state
    jalidate.iconValid = "success-text";

    // Public Property - Css class name of the icon in a invalid state
    jalidate.iconInvalid = "danger-text";

    // Public Property - Css class name of the valid state
    jalidate.valid = "valid";

    // Public Property - Css class name of the invalid state
    jalidate.invalid = "invalid";

    // Public Property - Css class name of the touched state
    jalidate.touched = "touched";

    // Public Method - Set the display using the valid styles
    jalidate.setValidDisplay = function (field, additionalFields, validationEvents) {
        try {

            this.icon = additionalFields[0];
            this.input = field;

            if (!runEvent(validationEvents, this.validEvent)) return;
            if (!hasClass(this.touched, this.input)) return;

            switchValidationMessage(this.input);

            removeClass(this.required, this.icon);
            removeClass(this.iconInvalid, this.icon);
            addClass(this.iconValid, this.icon);

            removeClass(this.invalid, this.input);
            removeClass(this.required, this.input);
            addClass(this.valid, this.input);
        } catch (e) {
            console.log(e);
        }
    }

    // Public Method - Set the display using the invalid styles
    jalidate.setInvalidDisplay = function (field, additionalFields, validationEvents) {
        try {
            this.icon = additionalFields[0];
            this.input = field;

            if (!runEvent(validationEvents, this.invalidEvent)) return;                        
            if (!hasClass(this.touched, this.input)) return;

            switchValidationMessage(this.input);

            removeClass(this.required, this.icon);
            removeClass(this.iconValid, this.icon);
            addClass(this.iconInvalid, this.icon);

            removeClass(this.valid, this.input);
            removeClass(this.required, this.input);
            addClass(this.invalid, this.input);

            // TODO: need to style and position the materialize toast - is this a good method to handle validation?
            // TODO: will the server side validation work with this scenario? - we could use a tooltip instead?
            // TODO: how do i know if this is aleady on the screen - it's getting added multiple times
            //Materialize.toast(this.message, 4000);            
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

    // Private Method - Check if the field is valid
    function validate(field, additionalFields) {

        if (field.checkValidity()) {
            jalidate.setValidDisplay(field);
        } else {
            jalidate.setInvalidDisplay(field);
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
                
        
        if (this.input.validity.valueMissing === true) {
            validationMessage = $(this.input).attr("data-val-req-msg");
        } else if (this.inputfield.validity.patternMismatch === true) {
            validationMessage = $(this.input).attr("data-val-pat-msg");
        } else {
            validationMessage = $(this.input).attr("data-val-msg");
        }

        // This is a catch all that will return the default message
        // TODO: Verify that this needs to be here
        return validationMessage
    }

    // Private Method - Get the validation message to display based on the type of validation that failed
    function runEvent(validationEvents, event) {
        return validationEvents.includes(event);
    }

    // Private Method - Switch the validation message to the current message on the field
    function switchValidationMessage(field) {
        this.input = field;
        this.message = getValidationMessage();

        // TODO: figure out how to use javascript global variables - can we or do i have to pass everything
        this.validationMessage = $(this.input).nextUntil("val-msg");
        this.validationMessage.val(this.message);
    }

}(window.jalidate = window.jalidate || {}, jQuery));