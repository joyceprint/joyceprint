/**************************************************************************************************
 * Quote Page Javascript Functionality
 *
 * Call this function to initialize the javascript required for the quote page
 *************************************************************************************************/
function initializeQuote() {

    initializeMaterializeSelect();

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