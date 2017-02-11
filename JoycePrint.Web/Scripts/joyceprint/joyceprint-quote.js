﻿"use strict";

/**************************************************************************************************
 * Quote Page Javascript Functionality
 *
 * Call this function to initialize the javascript required for the quote page
 *************************************************************************************************/
function initializeQuote() {

    setupSubmitButton();

    setupClearButton();
}

/**************************************************************************************************
 *
 *************************************************************************************************/
function setupClearButton() {
    $("#frm-quote button[type='reset']").click(function(e) {
        resetRecaptcha();
    });
}

/**************************************************************************************************
 *
 *************************************************************************************************/
function setupSubmitButton() {

    $("#frm-quote button[type='button']").click(function (e) {
        // we have to check here if the form is valid and if it is not display the message
        // it should be possible to hook into the normal validate from mvc
        //e.preventDefault();
        
        //var t = $("#frm-quote").submit();

        // 1 - check if form is valid perform operation
        // 2 - if valid send post
        // 3 - if invalid display validation errors
        $.ajax({
            url: "/quote",
            method: "POST",
            cache: false,
            data: $("#frm-quote").serialize()            
        })
            .fail(function (jqXHR, textStatus) {
                HandleAjaxError(jqXHR, textStatus);
            })
            .done(function (data) {
                showModal(data.modalView);                                
            });
    });
}