$(document).ready(function () {
    
    initializeNavFunctionality();

    if ($("#home").length > 0) {
        initializeHomeFunctionality();
    }

    if ($("#quote").length > 0) {
        initializeQuote();
    }

    if ($("#aboutus").length > 0) {
        initializeAboutUs();
    }
});