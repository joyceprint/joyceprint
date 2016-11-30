/**************************************************************************************************
 * About Us Page Javascript Functionality
 *
 * Call this function to initialize the javascript required for the about us page
 *************************************************************************************************/
function initializeAboutUs() {

    initializeMaterializeParallax();   
}

// This is how to ensure 1 function will run after another in javascript, due to it's bs
//    function doFirst(myCallback) {
//        console.log("first");
//        console.log("second")
//        mycallback();
//    };

//    function doLast() {

//        console.log("third")

//    }

//    doFirst(doLast);

// This is called an iffi function and it will run first
//(function () {
// Your code
//}());
///**************************************************************************************************
// * For now this function has to be on the page or the map will not work
// *
// *************************************************************************************************/
//function initMap() {

//    var uluru = { lat: 53.9807771, lng: -9.113247 };
//    var map = new google.maps.Map(document.getElementById('map'), {
//        zoom: 8,
//        center: uluru
//    });
//    var marker = new google.maps.Marker({
//        position: uluru,
//        map: map
//    });
//}

/**************************************************************************************************
 *
 *
 *************************************************************************************************/
function initializeMaterializeParallax() {
    $(document).ready(function () {
        $('.parallax').parallax();
    });
}