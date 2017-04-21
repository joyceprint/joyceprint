"use strict";

/**************************************************************************************************
 * Error Handling Functionality
 * 
 *
 *************************************************************************************************/
function HandleAjaxError(jqXHR, textStatus) {

    $.ajax({
        url: "/Error/Ajax",
        cache: false
    })
        .fail(function (jqXHR, textStatus) {
            alert("Ajax seems to be having a serious problem at the moment");
        })
        .done(function (data) {
            showModal(data.modalView);
        });
}