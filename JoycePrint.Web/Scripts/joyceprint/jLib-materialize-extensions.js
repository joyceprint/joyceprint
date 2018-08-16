"use strict";

/**************************************************************************************************
 * Extensions for MaterializeCss Library
 * 
 * The functionality contained in this file will be added to the jLib.materialize global object
 *  
 *************************************************************************************************/
var jLib = (function (parent, $) {

    // Create the materialize sub module off the parent, if it does not exist
    var subModule = parent.materialize = parent.materialize || {};

/**************************************************************************************************
 * PUBLIC METHODS
 *************************************************************************************************/

    /**************************************************************************************************
     * Initialize the materialize functionality
     *************************************************************************************************/
    subModule.initMaterialize = function () {

        initMaterializeParallax();

        initMaterializeSelect();

        initMaterializeModal();
    
        initMobileMenu();
    }

    /**************************************************************************************************
     * Initialize the materialize form functionality
     * 
     * This will handle the forms and move the labels out of the way
     * It will also run validation on the input elements that have values
     *************************************************************************************************/
    subModule.handleMaterializeForm = function (formId) {
        
        // Create the validator for the form
        let validator = ($("#" + formId)).validate();

        $("#" + formId)
            .find(".input-field:not(.file-field)")
            .each(function() {

                let input = $(this).find("input, textarea").filter(":visible:first");                                

                if ($(input).val()) {
                    // Only validate the field if it has a value - this will update the color scheme to green
                    validator.element(input);

                    $(input).prev().removeClass("orange-text danger-text").addClass("success-text");

                    let label = $(input).next("label");

                    if (!$(label).hasClass("active")) {
                        $(label).addClass("active");
                    }                    
                } else {
                    let label = $(input).next("label");

                    if ($(label).hasClass("active")) {
                        $(label).removeClass("active");
                    }
                }
            });
    }

/**************************************************************************************************
 * PRIVATE METHODS
 *************************************************************************************************/

    /**************************************************************************************************
     * Initialize the parallax functionality 
     *************************************************************************************************/
    function initMaterializeParallax() {
        // Parallax init
        $(".parallax").parallax();
    }

    /**************************************************************************************************
     * Initialize the materialize select functionality     
     *************************************************************************************************/
    function initMaterializeSelect() {
        // Select - Single Initialize
        $("select:not([multiple])").material_select();

        // Select - Multiple Initialize
        //$('select').material_select();
    }

    /**************************************************************************************************
     * Initialize the materialize modal functionality
     *************************************************************************************************/
    function initMaterializeModal() {
        $(".modal").modal({
            dismissible: false, // Modal can be dismissed by clicking outside of the modal [true - yes | false - no]
            complete: function() {
                if ($("#" + jLib.recaptcha.recaptchaId).length > 0) {
                    jLib.recaptcha.resetRecaptcha();
                }
            }
        });
    }

    /**************************************************************************************************
     * Initialize the side navigation menu for use on small screens and mobiles    
     *************************************************************************************************/
    function initMobileMenu() {
        // Initializes the side nav menu for mobile screens
        $(".button-collapse").sideNav();
    }

    // Return the parent, this allows multiple files to contribute to the same module
    return parent;

}(jLib || {}, jQuery));