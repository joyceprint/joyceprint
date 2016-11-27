/**************************************************************************************************
 * About Us Page Javascript Functionality
 *
 * Call this function to initialize the javascript required for the about us page
 *************************************************************************************************/
function initializeAboutUs() {

    initializeMaterializeParallax();

    // TODO: Add this to api file and only use it when required
    initializeGoogleMapApi();        
}

/**************************************************************************************************
 *
 *
 *************************************************************************************************/
function initializeGoogleMapApi() {

    var uluru = { lat: 53.9807771, lng: -9.113247 };
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 9,
        center: uluru
    });
    var marker = new google.maps.Marker({
        position: uluru,
        map: map
    });
}

/**************************************************************************************************
 *
 *
 *************************************************************************************************/
function initializeMaterializeParallax() {
    $(document).ready(function () {
        $('.parallax').parallax();
    });
}