/**************************************************************************************************
 * Validation Extension for jQuery that uses materialize
 *
 *************************************************************************************************/
(function (jalidate, $, undefined) {

    /********************************************************************************************
     * PRIVATE PROPERTIES ***********************************************************************
     *******************************************************************************************/

    // This will be used to abort operations
    var jbort = false;

    // The icon for the input field that's being validated
    var icon;

    // The input field that's being validated
    var input;

    // The message to display when validation fails
    var message;

    // The validation message label
    var label;

    /********************************************************************************************
     * PUBLIC PROPERTIES ************************************************************************
     *******************************************************************************************/

    // The response from the captcha
    jalidate.captchaResponse = "";

    // The valid event name
    jalidate.validEvent = "valid";

    // The invalid event name
    jalidate.invalidEvent = "invalid";

    // The version
    jalidate.version = "1.0";

    // Css class name of the icon in a required state
    jalidate.requiredCSS = "orange-text";

    // Css class name of the icon in a valid state
    jalidate.iconValidCSS = "success-text";

    // Css class name of the icon in a invalid state
    jalidate.iconInvalidCSS = "danger-text";

    // Css class name of the valid state
    jalidate.validCSS = "valid";

    // Css class name of the invalid state
    jalidate.invalidCSS = "invalid";

    // Css class name of the touched state
    jalidate.touchedCss = "touched";

    // Css class name of the label for displaying the validation message
    jalidate.validationMessageCss = "val-msg";

    /********************************************************************************************
     * PUBLIC METHODS ***************************************************************************
     *******************************************************************************************/

    /********************************************************************************************
     * Initialize the validation for the form
     * This function expects the form id to be passed in
     * This uses the onsubmit for better compatiblity with browsers
     *******************************************************************************************/
    jalidate.initializeValidation = function (formId) {

        // We have to setup the drop down materialize created to be used with our validation
        handleMaterializeSelectJankyness();

        // Wire up the help information to the controls
        initializeDocketHelp();

        var form = document.getElementById(formId);

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
            var preEvent = "";
            if (field.className.contains("select-dropdown")) {
                isDropDown = true;
                preEvent = handleMaterializeSelectFeature;
            }

            bindValidators(field, isDropDown, preEvent);
        }

        // We need to know when the user visited the input before we can set the correct state
        handleCssHtmlRestriction();

        // Wire up the clear event to the form
        initializeExtendHtml5ResetEvent();
    }

    /********************************************************************************************
     * Set the display using the valid styles
     *******************************************************************************************/
    jalidate.setValidDisplay = function (field, additionalFields, validationEvents, ignoreTouched) {
        try {

            jalidate.icon = additionalFields[0];
            jalidate.input = field;

            if (!runEvent(validationEvents, jalidate.validEvent)) return;

            if (!ignoreTouched && !hasClass(jalidate.touchedCss, jalidate.input)) return;

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

    /********************************************************************************************
     * Set the display using the invalid styles
     *******************************************************************************************/
    jalidate.setInvalidDisplay = function (field, additionalFields, validationEvents, ignoreTouched) {
        try {
            jalidate.icon = additionalFields[0];
            jalidate.input = field;

            if (!runEvent(validationEvents, jalidate.invalidEvent)) return;
            if (!ignoreTouched && !hasClass(jalidate.touchedCss, jalidate.input)) return;

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

    /********************************************************************************************
     * Set the display using the required styles
     *
     * We don't change the color of the input label / text to orange as it would reduce 
     * readability
     *******************************************************************************************/
    jalidate.setRequiredDisplay = function (field, additionalFields, validationEvents, ignoreTouched) {
        try {

            jalidate.icon = additionalFields[0];
            jalidate.input = field;

            if (!runEvent(validationEvents, jalidate.validEvent)) return;
            if (!ignoreTouched && !hasClass(jalidate.touchedCss, jalidate.input)) return;

            switchValidationMessage(true);

            removeClass(jalidate.iconValidCSS, jalidate.icon);
            removeClass(jalidate.iconInvalidCSS, jalidate.icon);
            addClass(jalidate.requiredCSS, jalidate.icon);

            removeClass(jalidate.invalidCSS, jalidate.input);
            removeClass(jalidate.validCSS, jalidate.input);

        } catch (e) {
            console.log(e);
        }
    }

    /********************************************************************************************
     * Bind an event listener to perform validation
     *******************************************************************************************/
    jalidate.bindValidator = function (field, additionalFields, listener, validationEvents, preEventFunction) {

        field.addEventListener(listener, function (event) {

            var runDefault = true;

            if (typeof preEventFunction !== typeof undefined && preEventFunction !== "") {
                // Running this pre event can break us out of the validation and allow us to call the functions directly
                // This is only in use for the materialize select
                runDefault = preEventFunction(field, additionalFields); // or we can use -> preEventFunction.call(); 
            }

            if (runDefault) {
                if (event.target.checkValidity()) {
                    jalidate.setValidDisplay(event.target, additionalFields, validationEvents);
                }
                else {
                    jalidate.setInvalidDisplay(event.target, additionalFields, validationEvents);
                }
            }
        });
    }

    /********************************************************************************************
     * TODO: This is not complete!!!
     * Basic legacy validation checking
     *******************************************************************************************/
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

    /********************************************************************************************
     * PRIVATE METHODS **************************************************************************
     *******************************************************************************************/

    /********************************************************************************************
     * Check if the class is present and if not, add it
     *******************************************************************************************/
    function addClass(ccsClass, element) {
        if (!$(element).hasClass(ccsClass))
            $(element).addClass(ccsClass);
    }

    /********************************************************************************************
     * Check if the class is present and if it is, remove it
     *******************************************************************************************/
    function removeClass(ccsClass, element) {
        if ($(element).hasClass(ccsClass))
            $(element).removeClass(ccsClass);
    }

    /********************************************************************************************
     * Check if the class is present
     *******************************************************************************************/
    function hasClass(ccsClass, element) {
        if ($(element).hasClass(ccsClass) === true)
            return true;
        else
            return false;
    }

    /********************************************************************************************
     * Get the validation message to display based on the type of validation that failed
     * Here we access the HTML 5 validation object to see what part of the validation failed so 
     * we can display a more specific message
     *******************************************************************************************/
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
    }

    /********************************************************************************************
     * Get the additional fields the input uses to display notifications to the user
     * 
     * Gets the icon and label for the input group based on the field passed in
     *******************************************************************************************/
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
            } else {
                icon = $(field).prev();
                label = $(field).next();
            } //else if (field.nodeName === "SELECT") { }        
        }

        return [icon[0], label[0]];
    }

    /********************************************************************************************
     * Determines if the validation event shoul be run
     *******************************************************************************************/
    function runEvent(validationEvents, event) {
        return validationEvents.includes(event);
    }

    /********************************************************************************************
     * Switch the validation message to the current message on the field
     *******************************************************************************************/
    function switchValidationMessage(toEmpty) {

        if (toEmpty) {
            jalidate.message = "";
        } else {
            getValidationMessage();
        }

        jalidate.label = $(jalidate.input).nextUntil(jalidate.validationMessageCss);

        // We get more than one label returned in the array so we have to look for the label with the validation css class
        for (var index = 0; index < jalidate.label.length; index++) {
            if (hasClass(jalidate.validationMessageCss, jalidate.label[index])) {
                jalidate.label[index].textContent = jalidate.message;
                break;
            }
        }
    }

    /********************************************************************************************
     * Set the field and it's additional elements used to display notifications to the user
     * to their initial required display
     *
     * This is used when clearing the form
     *******************************************************************************************/
    function switchToRequiredDisplay(field, additionalFields, ignoreTouched) {

        jalidate.setRequiredDisplay($(field)[0], additionalFields, ["valid", "invalid"], ignoreTouched);
    }

    /********************************************************************************************
     * Creates an event that will add the touched class to required fields
     * This is required as the :visited pseudo class only applies to anchor tags
     *
     * NOTE: This must be run after the validation has been wired up
     *******************************************************************************************/
    function handleCssHtmlRestriction() {

        // use $.fn.one here to fire the event only once.    
        $(':required').on('focus keydown', function () {
            if ($(this).hasClass("touched")) return;
            $(this).addClass('touched');
        });
    }

    /********************************************************************************************
     * Bind the validation events to the field
     *
     * A pre event function can be passed into the bindValidator function. 
     * This pre event function will run before the validation event is run. 
     * This helps with the materialize select control
     *******************************************************************************************/
    function bindValidators(field, isDropDown, preEvent) {

        var additionalFields = getAdditionalFields(field);

        // Bind blur for when the user leave the input    
        jalidate.bindValidator(field, additionalFields, "blur", ["valid", "invalid"], preEvent);

        // Bind
        jalidate.bindValidator(field, additionalFields, "mousedown", ["valid", "invalid"], preEvent);

        // Bind the change event.
        // This enables the number input type to function correctly
        jalidate.bindValidator(field, additionalFields, "change", ["valid"]);
    }

    /********************************************************************************************
     * If a button element exists on the page with a type of reset, this function will handle the 
     * materialize select and refocus on the element with the autofocus field
     *
     *******************************************************************************************/
    function initializeExtendHtml5ResetEvent() {

        // Check if we have a button to link the reset event to
        if ($("button[type='reset']").length > 0) {

            //
            // Remove the touched, invalid and valid classes
            $("button[type='reset']").on("click", function () {
                var autoFocusField = null;

                $(".validate:not(.initialized)").each(function () {
                    var field = this;
                    var ignoreTouched = true;

                    var requiredAttr = $(field).attr("required");

                    // For some browsers, `requiredAttr` is undefined; for others, `requiredAttr` is false.  Check for both.
                    if (typeof requiredAttr !== typeof undefined && requiredAttr !== false && requiredAttr == "required") {

                        var additionalFields = getAdditionalFields(field);
                        var icon = additionalFields[0];
                        var label = additionalFields[1];

                        if ($(field).hasClass("valid")) $(field).removeClass("valid");
                        if ($(field).hasClass("invalid")) $(field).removeClass("invalid");

                        if ($(field).hasClass("validate")) {
                            if (field.nodeName === "INPUT" || field.nodeName === "TEXTAREA" || field.nodeName === "SELECT") {
                                if ($(field).hasClass("select-dropdown")) resetMaterializeSelectToInitialValue(field);
                                switchToRequiredDisplay(field, additionalFields, ignoreTouched);
                            }
                        }

                        $(field).removeClass("touched");

                        var autoFocusAttr = $(field).attr("autofocus");

                        // For some browsers, `autoFocusAttr` is undefined; for others, `autoFocusAttr` is false.  Check for both.
                        if (typeof autoFocusAttr !== typeof undefined && autoFocusAttr !== false) {
                            autoFocusAttr.length > 0;
                            autoFocusField = field;
                        }
                    }
                });

                if (autoFocusField != null && typeof autoFocusField !== typeof undefined)
                    $(autoFocusField).focus();
            });
        }
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

    /********************************************************************************************
     * Validate the form and prevent the submission to the server if it's invalid         
     *******************************************************************************************/
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

            // If the field is not required we stop processing
            if (!field.required) continue;

            // Ignore hidden fields
            if (field.type === "hidden") continue;

            // Ignore buttons, fieldsets, etc.
            if (field.nodeName !== "INPUT" && field.nodeName !== "TEXTAREA" && field.nodeName !== "SELECT") continue;

            // Is native browser validation available?        
            if (typeof field.willValidate !== typeof undefined) {

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

            var runDefault = true;
            var ignoreTouchedClass = true;

            var additionalFields = getAdditionalFields(field);

            // If this is a drop down we need to perform special logic for the materialize select        
            if (field.className.contains("select-dropdown")) {
                runDefault = handleMaterializeSelectFeature(field, additionalFields, ignoreTouchedClass);
            }

            if (runDefault) {
                if (field.validity.valid) {
                    jalidate.setValidDisplay(field, additionalFields, ["valid", "invalid"], ignoreTouchedClass);
                }
                else {
                    jalidate.setInvalidDisplay(field, additionalFields, ["valid", "invalid"], ignoreTouchedClass);

                    // Form is invalid
                    formvalid = false;
                }
            }
        }

        // Cancel form submit if validation fails
        if (!formvalid) {
            if (event.preventDefault) event.preventDefault();

            Materialize.toast("Validation errors occurred. Please confirm the fields and submit it again.", 4000);
        } else if (!checkRecaptcha()) {

            // Form is invalid, recaptcha check not completed
            formvalid = false;

            displayRecaptchaError();
        }

        return formvalid;
    }

    /********************************************************************************************
    * MATERIALIZE METHODS ***********************************************************************
    *******************************************************************************************/

    /********************************************************************************************
     * This function transfers the select tag attributes to the ul and input that the materialize
     * select will generate.
     * 
     * The onlclick event for li items has been setup to update the select tag with the chosen li data
     * as well as updating the value of the input to the enum value
     *******************************************************************************************/
    function handleMaterializeSelectJankyness() {

        $(".select-wrapper").each(function () {

            var select = $(this).find("select");
            var ul = $(this).find("ul");
            var input = $(this).find("input");
            var icon = $(this).prev();
            var label = $(this).next();

            //
            // Switch the required attribute
            var attr = $(select).attr("required");

            // For some browsers, `attr` is undefined; for others, `attr` is false.  Check for both.
            if (typeof attr !== typeof undefined && attr !== false) {
                $(input).attr("required", "required");
            }

            //
            // Switch the validation class from the select to the materizlise input        
            if (!$(input).hasClass("validate")) $(input).addClass("validate");

            //
            // Switch the validation messages
            $(select[0].attributes).each(function () {
                if (!this.nodeName.startsWith("data-val-")) return;
                $(input).attr(this.nodeName, this.nodeValue);
            });

            // Switch the data-help toggle switch
            // May have to change this to use the property - undefined
            if ($(select).attr("data-help") !== "undefined") {
                var help = $(select).attr("data-help");

                $(input).attr("data-help", help);
            }

            //
            // Switch selected option based on selected li item
            $(ul).find("li").on("click", function (event) {

                var ulSelectedItem = event.target.textContent;
                var tempValue = $(this).val();

                $(select).find('option').filter(function () {
                    return ($(this).text() !== ulSelectedItem);
                }).attr('selected', false);

                $(select).find('option').filter(function () {
                    return ($(this).text() === ulSelectedItem);
                }).attr('selected', true);

                // We need to get the option here and if the value is "" [blank] we dont set the input            
                if ($(select).find("option:not([value])").text() !== ulSelectedItem) {
                    var selectedEnumValue = $(select).find("option:selected").val();
                    $(input).attr("value", selectedEnumValue); // Sets the input to the enum numeric value
                    //$(input).attr("value", ulSelectedItem); // Sets the input to the enum text value
                }

                if ($(select).find("option:selected").val() === "") {
                    jalidate.setInvalidDisplay($(input)[0], [icon[0], label[0]], ["valid", "invalid"]);
                } else {
                    jalidate.setValidDisplay($(input)[0], [icon[0], label[0]], ["valid", "invalid"]);
                }
            });
        });
    }

    /**************************************************************************************************
    * Initialize the help for the docket book.
    *
    * This ensure that the help information is set to display when the user has focus on the correct
    * input
    *************************************************************************************************/
    function initializeDocketHelp() {

        $("#docket-book input").each(function () {
            if ($(this).data("help") !== undefined && $(this).data("help").length > 0) {
                $(this).on("focus", function (e) {
                    if (!$("#" + $(this).data("help")).hasClass("active")) {
                        $("#" + $(this).data("help")).trigger("click");
                    }
                });
            }
        });
    }

    /********************************************************************************************
     * Global variable to hold the function that will fix the materialize select valiadation issue.
     *
     * This will be called in the jalidate.js script by the bindValidator function
     *
     * The select needs to be validated in this way, due to the way materialize works
     * When an item is selected, it will be set on the input control, as the validation is being
     * performed on the input, if the input has a value it will be set to a valid state, this function
     * will ensure that if the selected item is the option label that the input will be correctly 
     * set to invalid.
     *******************************************************************************************/
    var handleMaterializeSelectFeature = function (field, additionalFields, ignoreTouchedClass) {

        var ul = $(field).next();
        var initialSelection = $(ul).find("li:first").text();

        $(ul).find("li.active").addClass("selected");

        if ($(field).val() === initialSelection) {
            jalidate.setInvalidDisplay(field, additionalFields, ["valid", "invalid"], ignoreTouchedClass);
        } else {
            jalidate.setValidDisplay(field, additionalFields, ["valid", "invalid"], ignoreTouchedClass);
        }

        // This instructs the jalidate function to not run it's validation, as we've done it here
        return false;
    }

    /********************************************************************************************
     * Resets the materialize select control to it's initial value
     *
     * To accomplish this the input, ul and select elements need to be reset
     *******************************************************************************************/
    function resetMaterializeSelectToInitialValue(field) {
        var ul = $(field).next("ul");
        $(ul).find("li.active.selected").removeClass("active selected");
        $(ul).find("li:first").addClass("active selected");

        var select = $(field).nextAll("select");
        $(select).find(":selected").removeAttr("selected");
        $(select).find("option:first").attr("selected", "selected");

        var selectedOption = $(ul).find("li.active.selected").text();
        $(field).attr("value", selectedOption);
    }

    //// Check if the field is valid
    //function validate(field, additionalFields) {

    //    // Have to get the group fields for select inputs differently due to using the materialize select control
    //    if (field.nodeName === "INPUT" || field.nodeName === "TEXTAREA") {
    //        icon = $(field).prev();
    //        label = $(field).next();
    //    } else if (field.nodeName === "SELECT") {
    //        icon = $(field).closest("div").prev();
    //        label = $(field).closest("div").next();
    //    }

    //    if (field.checkValidity()) {                        
    //        jalidate.setValidDisplay($(field)[0], [icon[0], label[0]], ["valid", "invalid"]);
    //    } else {            
    //        jalidate.setInvalidDisplay($(field)[0], [icon[0], label[0]], ["valid", "invalid"]);
    //    }
    //}

}(window.jalidate = window.jalidate || {}, jQuery));