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
    jalidate.setValidDisplay = function (field, additionalFields) {
        try {
            this.icon = additionalFields[0];
            this.input = field;
            
            if (hasClass(this.touched, this.input)) {
                removeClass(this.required, this.icon);
                removeClass(this.iconInvalid, this.icon);
                addClass(this.iconValid, this.icon);

                removeClass(this.invalid, this.input);
                removeClass(this.required, this.input);
                addClass(this.valid, this.input);
            } else {
                removeClass(this.iconInvalid, this.icon);
                removeClass(this.iconValid, this.icon);
                addClass(this.required, this.icon);

                removeClass(this.invalid, this.input);
                removeClass(this.valid, this.input);
                addClass(this.required, this.input);
            }
        } catch (e) {
            console.log(e);
        }
    }

    // Public Method - Set the display using the invalid styles
    jalidate.setInvalidDisplay = function (field, additionalFields) {
        try {
            this.icon = additionalFields[0];
            this.input = field;           

            if (hasClass(this.touched, this.input)) {
                removeClass(this.required, this.icon);
                removeClass(this.iconValid, this.icon);
                addClass(this.iconInvalid, this.icon);

                removeClass(this.valid, this.input);
                removeClass(this.required, this.input);
                addClass(this.invalid, this.input);
            } else {
                removeClass(this.iconValid, this.icon);
                removeClass(this.iconInvalid, this.icon);
                addClass(this.required, this.icon);

                removeClass(this.valid, this.input);
                removeClass(this.invalid, this.input);
                addClass(this.required, this.input);
            }
        } catch (e) {
            console.log(e);
        }
    }

    // Public Method - Bind an event listener to perform validation
    //
    jalidate.bindValidator = function (field, additionalFields, listener) {

        field.addEventListener(listener, function (event) {
            if (event.target.checkValidity()) {
                jalidate.setValidDisplay(event.target, additionalFields);
            } else {
                jalidate.setInvalidDisplay(event.target, additionalFields);
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

    function hasClass(ccsClass, element) {
        if ($(element).hasClass(ccsClass) === true)
            return true;
        else
            return false;
    }

}(window.jalidate = window.jalidate || {}, jQuery));