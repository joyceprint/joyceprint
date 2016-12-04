/**************************************************************************************************
 * Quote Page Javascript Functionality
 *
 * Call this function to initialize the javascript required for the quote page
 *************************************************************************************************/
function initializeQuote() {

    initializeMaterializeSelect();

    initializeExtendHtml5ResetEvent();

    // We have to run this function first so the help data attribute is transferred
    handleMaterializeSelectJankyness();

    initializeDocketHelp();
}

/**************************************************************************************************
 * Initialize the materialize select functionality
 *************************************************************************************************/
function initializeMaterializeSelect() {
    // Select - Single Initialize
    $('select:not([multiple])').material_select();

    // Select - Multiple Initialize
    //$('select').material_select();
}

/**************************************************************************************************
 * TODO: This function needs to handle the materialize select and refocus on the 
 * element with the autofocus field
 *************************************************************************************************/
function initializeExtendHtml5ResetEvent() {

    //
    // Remove the touched, invalid and valid classes
    $("button[type='reset']").on("click", function () {
        var autoFocusField = null;

        $(".touched").each(function () {
            var field = this;

            var additionalFields = getAdditionalFields(field);
            var icon = additionalFields[0];
            var label = additionalFields[1];

            if ($(field).hasClass("valid")) $(field).removeClass("valid");
            if ($(icon).hasClass("valid")) $(icon).removeClass("valid");
            if ($(label).hasClass("valid")) $(label).removeClass("valid");

            if ($(field).hasClass("invalid")) $(field).removeClass("invalid");
            if ($(icon).hasClass("invalid")) $(icon).removeClass("invalid");
            if ($(label).hasClass("invalid")) $(label).removeClass("invalid");

            if ($(field).hasClass("validate")) {
                if (field.nodeName === "INPUT" || field.nodeName === "TEXTAREA" || field.nodeName === "SELECT") {
                    if ($(field).hasClass("select-dropdown")) resetMaterializeSelectToInitialValue(field); 
                    switchToRequiredDisplay(field);
                }
            }

            $(field).removeClass("touched");

            //// TODO: Fix this - exception being thrown
            //if ($(field).attr("autofocus").length > 0) {
            //    autoFocusField = field;
            //}
        });

        //$(autoFocusField).focus();
        $("#Contact_Company").focus();
    });
}

/**************************************************************************************************
 * This function transfers the select tag attributes to the ul and input that the materialize
 * select will generate.
 * 
 * The onlclick event for li items has been setup to update the select tag with the chosen li data
 * as well as updating the value of the input to the enum value
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
            $(input).attr("required", "required");
        }

        //
        // Switch the validation class from the select to the materizlise input        
        if (!$(input).hasClass("validate")) $(input).addClass("validate")

        //
        // Switch the validation messages
        $(select[0].attributes).each(function () {
            if (!this.nodeName.startsWith("data-val-")) return;
            $(input).attr(this.nodeName, this.nodeValue);
        });

        // Switch the data-help toggle switch
        // May have to change this to use the property - undefined
        if ($(select).data("help") !== "undefined") {
            var help = $(select).data("help");

            $(input).data("help", help);
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