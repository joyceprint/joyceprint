"use strict";

/**************************************************************************************************
 * Google Analytics Library
 *
 * This file contains the methods and properties required for the Google Analytics API
 * The functionality contained in this file will be added to the jLib.analytics global object
 *  
 *************************************************************************************************/
var jLib = (function (parent, $) {

    // Create the google sub module off the parent, if it does not exist
    var subModule = parent.analytics = parent.analytics || {};

    /**************************************************************************************************
     * PUBLIC PROPERTIES
     *************************************************************************************************/

    subModule.url = "http://www.google-analytics.com/collect";

    subModule.measurementProtocolVersion = "1";

    subModule.trackingId = "UA-88639794-1";

    subModule.clientId = "1234";

/**************************************************************************************************
 * PUBLIC METHODS
 *************************************************************************************************/

    /**************************************************************************************************
     * Collect the analytics information and post it to the collection url
     *
     * @param {string} category - The event tracking category.
     * @param {string} action - The event tracking action.
     * @param {string} label - The event tracking label.
     *************************************************************************************************/
    subModule.collectEvent = function (category, action, label) {

        var eventTracking = getEventTracking(category, action, label);

        $.ajax({
            url: subModule.url,
            method: "POST",
            cache: false,
            data: eventTracking
        })
               .fail(function (jqXHR, textStatus) {
               })
               .done(function (data) {
               });
    }

/**************************************************************************************************
 * PRIVATE METHODS
 *************************************************************************************************/

    /**************************************************************************************************
     * Setup the tracking query string parameters
     * 
     * @param {string} category - The event tracking category.
     * @param {string} action - The event tracking action.
     * @param {string} label - The event tracking label.
     * @returns {string} eventTracking - The event tracking query string.
     *************************************************************************************************/
    function getEventTracking(category, action, label) {
        var eventTracking = "";

        // Version
        eventTracking += "v=" + subModule.measurementProtocolVersion;

        // Tracking ID / Property ID
        eventTracking += "&tid=" + subModule.trackingId;

        // Anonymous Client ID.
        eventTracking += "&cid=" + subModule.clientId;

        // Event hit type
        eventTracking += "&t=event";

        // Event Category [Required]
        eventTracking += "&ec=" + category;

        // Event Action [Required]
        eventTracking += "&ea=" + action;

        // Event label
        eventTracking += "&el=" + label;

        // Event value
        //eventTracking += "&ev=300";

        return eventTracking;
    }

    // Return the parent, this allows multiple files to contribute to the same module
    return parent;

}(jLib || {}, jQuery));