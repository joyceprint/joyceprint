"use strict";

/**************************************************************************************************
 * Error Handling Functionality
 * 
 *************************************************************************************************/

/**************************************************************************************************
 * This method handles an ajax error.
 * 
 * It calls Error/Ajax on the server
 * And displays either a modal or a browser alert
 *************************************************************************************************/
function HandleAjaxError(jqXHR, textStatus) {

    $.ajax({
        url: "/ajax",
        cache: false
    })
        .fail(function (jqXHR, textStatus) {
            hideLoader();

            alert("Ajax seems to be having a serious problem at the moment");
        })
        .done(function (data) {
            hideLoader();

            showModal(data.modalView);
        });
}