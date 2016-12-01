/**************************************************************************************************
 * Quote Page Javascript Functionality
 *
 * Call this function to initialize the javascript required for the quote page
 *************************************************************************************************/
function initializeQuote() {

    initializeMaterializeSelect();

    initializeExtendHtml5ResetEvent();

    initializeDocketHelp();

    // Calls the entry point for the validation script
    initializeValidation();
}

/**************************************************************************************************
 *
 *************************************************************************************************/
function initializeMaterializeSelect() {
    // Select - Single
    $('select:not([multiple])').material_select();

    //$('select').material_select();
}

/**************************************************************************************************
 *
 *************************************************************************************************/
function initializeDocketHelp() {

    // We have to run this function first so the help data attribute is transferred
    handleMaterializeSelectJankyness();

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

/**************************************************************************************************
 *
 *************************************************************************************************/
function initializeExtendHtml5ResetEvent() {

    //
    // Remove the touched, invalid and valid classes
    $("button[type='reset']").on("click", function () {
        var autoFocusField = null;

        $(".touched").each(function () {
            var field = this;

            if ($(field).hasClass("valid")) {
                $(field).removeClass("valid");
            }

            if ($(field).hasClass("invalid")) {
                $(field).removeClass("invalid");
            }

            if ($(field).hasClass("validate")) {
                if (field.nodeName === "INPUT" || field.nodeName === "TEXTAREA" || field.nodeName === "SELECT") {
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
 *
 *************************************************************************************************/
function switchToRequiredDisplay(field) {
    var icon = null;
    var label = null;

    if (field.nodeName === "INPUT" || field.nodeName === "TEXTAREA") {
        // Handle the materialize drop downs
        if ($(field).hasClass("select-dropdown")) {
            icon = $(field).closest("div").prev();
            label = $(field).closest("div").next();

            clearMaterializeSelections(field);
        } else {
            icon = $(field).prev();
            label = $(field).next();
        } //else if (field.nodeName === "SELECT") { }        
    }

    jalidate.setRequiredDisplay($(field)[0], [icon[0], label[0]], ["valid", "invalid"]);
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
* This needs to be put into a materialize work arounds file with all other functions like it
* or they at least need to be in the same location in all of the files - depends on bundle findings
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
            //$(select).removeAttr("required");
            $(input).attr("required", "required");
        }

        //
        // Switch the validation class from the select to the materizlise input
        //if ($(select).hasClass("validate")) $(select).removeClass("validate")
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

            //$(select).removeAttr("data-help");
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

            $(input).attr("value", ulSelectedItem);

            if ($(select).find("option:selected").val() === "") {
                jalidate.setInvalidDisplay($(input)[0], [icon[0], label[0]], ["valid", "invalid"]);
            } else {
                jalidate.setValidDisplay($(input)[0], [icon[0], label[0]], ["valid", "invalid"]);
            }
        });
    });
}