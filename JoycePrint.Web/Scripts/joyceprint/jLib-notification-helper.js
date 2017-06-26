"use strict";

/**************************************************************************************************
 * Notification Library
 *
 * Contains the code to display notifications
 * The functionality contained in this file will be added to the jLib.notify global object
 *  
 *************************************************************************************************/
var jLib = (function (parent, $) {
    
    // Create the loading sub module off the parent, if it does not exist
    var subModule = parent.notify = parent.notify || {};

/**************************************************************************************************
 * PUBLIC METHODS
 *************************************************************************************************/

    /**************************************************************************************************
     * Call this function to show a modal
     * 
     * Requires: 
     *      @param {string} data - The inner html of the modal to show
     *      A container be placed on the page with the id of "modal-placeholder"     
     *************************************************************************************************/
    subModule.showModal = function (data) {

        var containerId = "modal-placeholder";

        var container = $("#" + containerId);

        $(container).html(data);

        $("#modal-placeholder").modal("open");
    }

    /**************************************************************************************************
     * Call this function to load a view
     * 
     * Requires: 
     *      @param {string} data - The inner html of the view to load
     *      @param {string} target - The container to load the view into
     *************************************************************************************************/
    subModule.loadView = function (data, target) {

        var container = $("#" + target);

        $(container).html(data);    
    }

    // Return the parent, this allows multiple files to contribute to the same module
    return parent;

}(jLib || {}, jQuery));