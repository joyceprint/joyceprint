"use strict";

/**************************************************************************************************
 * Error Handling Library
 *
 * This file contains the methods and properties required for error handling
 * The functionality contained in this file will be added to the jLib.error global object
 *  
 *************************************************************************************************/
var jLib = (function (parent, $) {
    
    // Create the error sub module off the parent, if it does not exist
    var subModule = parent.error = parent.error || {};

/**************************************************************************************************
 * PUBLIC PROPERTIES
 *************************************************************************************************/

    /**************************************************************************************************
     * This method handles an ajax error.
     * 
     * It calls Error/Ajax on the server
     * And displays either a modal or a browser alert
     * 
     * @param {string} jqXHR - The ajax request.
     * @param {string} textStatus - The text status from the ajax request.     
     *************************************************************************************************/
    subModule.HandleAjaxError = function (jqXHR, textStatus) {

        $.ajax({
            url: "/ajax",
            cache: false
        })
            .fail(function (jqXHR, textStatus) {
                jLib.loading.hideLoader();

                alert("Ajax seems to be having a serious problem at the moment");
            })
            .done(function (data) {
                jLib.loading.hideLoader();

                jLib.notify.showModal(data.modalView);
            });
    }

    // Return the parent, this allows multiple files to contribute to the same module
    return parent;

}(jLib || {}, jQuery));