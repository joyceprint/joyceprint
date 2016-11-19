/**************************************************************************************************
 * Validation
 *
 *************************************************************************************************/
$(document).ready(function () {

    intializeValidation();

    // Move this to line ~52
    handleMaterializeSelectJankyness();

    handleCssHtmlRestriction();
});

/**************************************************************************************************
*
 *************************************************************************************************/
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
            //handleMaterializeSelectJankyness(field);            
        }
    }
}

/**************************************************************************************************
*
 *************************************************************************************************/
function handleMaterializeSelectJankyness() {

    $(".select-wrapper").each(function () {

        var select = $(this).find("select");
        var ul = $(this).find("ul");
        var input = $(this).find("input");
        var icon = $(this).prev();
        var label = $(this).next()

        //
        // Switch the required attribute
        var attr = $(select).attr("required");

        // For some browsers, `attr` is undefined; for others, `attr` is false.  Check for both.
        if (typeof attr !== typeof undefined && attr !== false) {
            $(select).removeAttr("required");
            $(input).attr("required", "required");
        }

        //
        // Switch the validation class from the select to the materizlise input
        if ($(select).hasClass("validate")) $(select).removeClass("validate")
        if (!$(input).hasClass("validate")) $(input).addClass("validate")
       
        //
        // Switch the validation messages
        $(select[0].attributes).each(function () {
            if (!this.nodeName.startsWith("data-val-")) return;

            $(input).attr(this.nodeName, this.nodeValue);
        });

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

            $(input).attr("value", ulSelectedItem);

            if ($(select).find("option:selected").val() === "") {
                jalidate.setInvalidDisplay($(input)[0], [icon[0], label[0]], ["valid", "invalid"]);
            } else {                
                jalidate.setValidDisplay($(input)[0], [icon[0], label[0]], ["valid", "invalid"]);
            }            
        });

        // TODO: This is currently not working so it will be left out for now
        //// Switch the data-help toggle switch
        //if ($(select).data("help") !== "undefined") {
        //    var help = $(select).data("help");
        //    $(select).removeAttr("data-help");

        //    $(ul).data("help", help);            
        //}
    });
}

/**************************************************************************************************
 * Creates an event that will add the touched class to required fields
 * This is required as the :visited pseudo class only applies to anchor tags
 *
 * NOTE: This must be run after the validation has been wired up
 *************************************************************************************************/
function handleCssHtmlRestriction() {

    // use $.fn.one here to fire the event only once.
    $(':required').one('blur keydown', function () {
        $(this).addClass('touched');
    });
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