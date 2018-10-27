"use strict";

/**************************************************************************************************
 * Google Recaptcha Library
 *
 * This file contains the methods and properties required for the Google Recaptcha control
 * The functionality contained in this file will be added to the jLib.recaptcha global object
 *  
 *************************************************************************************************/
var jLib = (function (parent, $) {

    // Create the recaptcha sub module off the parent, if it does not exist
    var subModule = parent.recaptcha = parent.recaptcha || {};

/**************************************************************************************************
 * PUBLIC PROPERTIES
 *************************************************************************************************/
    subModule.captchaResponse = null;

    subModule.recaptchaId = "jp-recaptcha";

/**************************************************************************************************
 * PUBLIC METHODS
 *************************************************************************************************/

    /**************************************************************************************************
     * Handle the recaptcha response by saving it to a validate public property
     * 
     * @param {string} captchaResponse - The captcha response string from the google api
     *************************************************************************************************/
    subModule.handleCaptcha = function (captchaResponse) {
        subModule.captchaResponse = captchaResponse;
    }

    /**************************************************************************************************
     * Handle the expired recaptcha event
     *************************************************************************************************/
    subModule.handleExpiredCaptcha = function () {
        grecaptcha.reset();
    }

    /**************************************************************************************************
     * Handle the reset recaptcha event
     *************************************************************************************************/
    subModule.resetRecaptcha = function () {
        grecaptcha.reset();
    }

    /********************************************************************************************
     * Check the recaptcha
     *
     * Since the user may have already performed the recaptcha before clicking the submit
     * button, we check the stored recatpcha response by sending it to our security controller
     * 
     * @returns {boolean} validRecaptcha - A flag indicating if the recaptcha is valid
     *******************************************************************************************/
    subModule.checkRecaptcha = function () {

        var validRecaptcha = false;

        $.ajax({
            url: "/security/processrecaptcha",
            method: "POST",
            cache: false,
            data: {
                "captchaResponse": subModule.captchaResponse
            },
            dataType: "json",
            async: false
        })
            .done(function (data, textStatus, jqXHR) {
                if (data) {
                    var jsonData = JSON.parse(data);

                    if (jsonData && jsonData.success) {
                        validRecaptcha = true;
                    }
                }

                // Reset the recaptcha response after using it
                subModule.captchaResponse = "";
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                // We should only end up here if our end failed
            });

        return validRecaptcha;
    }

    /********************************************************************************************
     * Display a toast holding the error message if the recaptcha fails     
     *******************************************************************************************/
    subModule.displayRecaptchaError = function () {
        Materialize.toast("Please complete the recaptcha", 4000);
    }

    // Return the parent, this allows multiple files to contribute to the same module
    return parent;

}(jLib || {}, jQuery));