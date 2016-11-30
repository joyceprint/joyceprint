/**************************************************************************************************
 * Quote Page Javascript Functionality
 *
 * Call this function to initialize the javascript required for the quote page
 *************************************************************************************************/
function initializeQuote() {
    
    initializeMaterializeSelect();

    initializeExtendHtml5ResetEvent();

    // This has been moved to the validation function
    initializeDocketHelp();
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
    //$("#docket-book input").each(function () {
    //    if ($(this).data("help").length > 0) {
    //        $(this).on("focus", function (e) {
    //            if (!$("#" + $(this).data("help")).hasClass("active")) {
    //                $("#" + $(this).data("help")).trigger("click");
    //            }
    //        });
    //    }
    //});
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