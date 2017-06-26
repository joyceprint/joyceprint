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
     * 
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
            complete: function () { jLib.recaptcha.resetRecaptcha(); }
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