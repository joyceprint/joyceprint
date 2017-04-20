"use strict";

/**************************************************************************************************
 * Notification View Javascript Functionality
 *
 * Call this function to show a modal
 * 
 * Requires: 
 *      data - The inner html of the modal to show
 *      A container be placed on the page with the id of "modal-placeholder"
 *************************************************************************************************/
function showModal(data) {

    var containerId = "modal-placeholder";

    var container = $("#" + containerId);

    $(container).html(data);

    $("#modal-placeholder").modal("open");

    //$('.modal').modal({
    //    dismissible: true, // Modal can be dismissed by clicking outside of the modal
    //    opacity: .5, // Opacity of modal background
    //    inDuration: 300, // Transition in duration
    //    outDuration: 200, // Transition out duration
    //    startingTop: '4%', // Starting top style attribute
    //    endingTop: '10%', // Ending top style attribute
    //    ready: function (modal, trigger) { // Callback for Modal open. Modal and trigger parameters available.
    //        alert("Ready");
    //        console.log(modal, trigger);
    //    },
    //    complete: function () { alert('Closed'); } // Callback for Modal close
    //});
}