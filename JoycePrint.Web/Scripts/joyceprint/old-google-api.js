/**************************************************************************************************
 * All google api calls should be made from in here
 * TODO: move all google functionality here
 *
 *************************************************************************************************/
(function (jnalytics, $, undefined) {

/**************************************************************************************************
 * PUBLIC PROPERTIES
 *************************************************************************************************/

    jnalytics.url = "http://www.google-analytics.com/collect";

    jnalytics.measurementProtocolVersion = "1";

    jnalytics.trackingId = "UA-88639794-1";

    jnalytics.clientId = "1234";

/**************************************************************************************************
 * PUBLIC METHODS
 *************************************************************************************************/
    jnalytics.collectEvent = function (category, action, label) {

        var eventTracking = getEventTracking(category, action, label);

        $.ajax({
            url: jnalytics.url,
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

    function getEventTracking(category, action, label) {
        var eventTracking = "";

        // Version
        eventTracking += "v=" + jnalytics.measurementProtocolVersion;

        // Tracking ID / Property ID
        eventTracking += "&tid=" + jnalytics.trackingId;

        // Anonymous Client ID.
        eventTracking += "&cid=" + jnalytics.clientId;

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
}(window.jnalytics = window.jnalytics || {}, jQuery));