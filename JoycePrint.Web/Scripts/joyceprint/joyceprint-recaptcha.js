﻿"use strict";
/**************************************************************************************************
 * 
 * 
 *************************************************************************************************/

/**************************************************************************************************
 * TODO: this init function needs to hook into the onsubmit event and hyjack it
 *************************************************************************************************/
//function initializeRecaptcha() {
//    grecaptcha.render('example3', {
//        'sitekey': 'your_site_key',
//        'callback': verifyCallback,
//        'theme': 'dark'
//    });
//}

/**************************************************************************************************
 * 
 *************************************************************************************************/
function handleCaptcha(captchaResponse) {

    $.ajax({
        url: "/security/recaptcha",
        method: "POST",
        cache: false,
        data: {
            "captchaResponse": captchaResponse
        },
        dataType: "json"
    })
        .done(function (data, textStatus, jqXHR) {
            if (data && data.success) {
                // Allow the onsubmit function
            }
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            // Cancel the onsubmit function
        });
}

/**************************************************************************************************
 * 
 *************************************************************************************************/
function handleExpiredCaptcha(captchaResponse) {
    // This function should call the reset method
    // I am unable to get the control to time out
}
