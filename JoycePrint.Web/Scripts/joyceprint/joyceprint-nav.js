"use strict";

var scrollUp;
var lastScrollTop = 0;
var navOffset = -($("nav").height() - 4);

/**************************************************************************************************
 * Navigation Menu Javascript Functionality
 *
 * Call this function to initialize the javascript required for the navigation menu
 *************************************************************************************************/
function initializeNavFunctionality() {

    // When the page is loaded / reloaded, this gets the section the page is on and updates the menu link
    document.body.addEventListener("load", toggleNavigationMenu(getElementIdInViewport()));

    // Add the hashchange event listener, this event fires when the windows hash changes [location.hash]    
    window.addEventListener("hashchange", shiftWindow);

    // Initializes the side nav menu for mobile screens
    initializeMobileMenu();

    // Sets up the scroll offset, required because of the use of a stick header
    initializeScrollMenu();

    // TODO: This is not working yet
    initializeAnimatedMenu();
}

/**************************************************************************************************
 * Initialize the side navigation menu for use on small screens and mobiles
 *************************************************************************************************/
function initializeMobileMenu() {
    $(".button-collapse").sideNav();
}

/**************************************************************************************************
 * This function needs to account for the fact when you this the menu link and that menu page
 * is at the top of the viewport, a second click will cause it to shoot up above the viewable
 * area. This needs to be detected and prevented on the onclick function
 *************************************************************************************************/
function initializeAnimatedMenu() {

    // This is not need, but it could be used to make the navigation have an animation
    //$(".nav li a").click(function (event) {
    //    event.preventDefault();

    //    $($(this).attr("href"))[0].scrollIntoView();

    //    scrollBy(0, -offset);
    //});


    //$('a[href*=#]').on('click', function (event) {
    //event.preventDefault();
    //$('html,body').animate({
    //    scrollTop: $(this.hash).offset().top
    //}, 500);
    //});


    //$("ul li a[href*=\\#]").on("click", function(e) {
    //    e.preventDefault();

    //    var id = $(this).attr("href");

    //    $("html,body").animate({ scrollTop: $(this).offset().top }, "slow");
    //});            
}

/**************************************************************************************************
 * Shitfs the scroll position down by the height of the fixed menu
 *
 * This allows href to point to an element location without having the top section covered
 * by the fixed menu. 
 *************************************************************************************************/
var shiftWindow = function () {    
    scrollBy(0, navOffset);
};

/**************************************************************************************************
 * Initializes the menu update via scrolling and resizing
 * 
 * Toggles the navigation menu based on where the user is on the page and which direction
 * they are scrolling
 *************************************************************************************************/
function initializeScrollMenu() {

    // This accounts for updating the menu link when the user is scrolling and resizing the screen
    $(window).on("resize scroll", function () {        

        var st = $(this).scrollTop();

        if (st > lastScrollTop) {
            // downscroll code            
            scrollUp = false;
        } else {
            // upscroll code            
            scrollUp = true;
        }
        lastScrollTop = st;

        toggleNavigationMenu(getElementIdInViewport());

        console.log(getElementIdInViewport());
        console.log("scroll direction : " + scrollUp);
    });
}

/**************************************************************************************************
 * Get the scroll adjust
 *
 * This is the value to add to the vertical center point of the viewport
 * The scroll direction is checked and the offset applied
 *************************************************************************************************/
function getScrollAdjust() {

    var scrollAdjust = 0;

    // TODO: This needs to adjust up or down using my screen size as a ratio
    var scrollAdjustUp = 200;
    var scrollAdjustDown = -200;
    
    if(scrollUp === true)
        scrollAdjust = scrollAdjustUp;
    else if (scrollUp === false)
        scrollAdjust = scrollAdjustDown;

    return scrollAdjust;
}

/**************************************************************************************************
 * Gets the element in the viewport
 * 
 * This gets the Id of the section using the center of the viewport and the scroll offset
 *************************************************************************************************/
function getElementIdInViewport() {

    var elementId = null;

    var menuHeight = 110;

    // the window url using javascript and window element
    //window.location.pathname + window.location.hash == '/index.html#contact'

    // 1 - get the center of the viewport - account for the menu height
    var windowHeight = $(window).height();

    // Sub the menu height from the viewport height
    var windowHeightWithoutMenu = windowHeight - menuHeight;

    var screenCPt = (windowHeightWithoutMenu / 2) + menuHeight;

    console.log("screen vcenter - " + screenCPt);

    // 2 - find the element at that location
    var offsetYPt = screenCPt;

    // Get the center of hte viewport
    var offsetXPt = $(window).width() / 2;

    var elementAtCenter = document.elementFromPoint(offsetXPt, offsetYPt + getScrollAdjust());

    // 3 - navagiate out until you get to a section
    var navSection = $(elementAtCenter).closest("section");

    elementId = $(navSection).attr("id");

    return elementId;
}

/**************************************************************************************************
 * Toggle the navigation menu so the active page menu link reflects the view the user has currently 
 * scrolled on to. This is done by changing the text color.
 * 
 * This will also handle the side navigiation menu 
 *************************************************************************************************/
function toggleNavigationMenu(id) {

    // Find and remove the active class
    $("#nav").find(".active").removeClass("active");
    $("#nav").find(".active-text").removeClass("active-text");

    if (id === "home") {
        $("#nav #liHome").addClass("active");
        $("#nav #liHome a").addClass("active-text");

        $("#nav-mobile #liHome").addClass("active");
        $("#nav-mobile #liHome a").addClass("active-text");
    } else if (id === "quote") {
        $("#nav #liQuote").addClass("active");
        $("#nav #liQuote a").addClass("active-text");

        $("#nav-mobile #liQuote").addClass("active");
        $("#nav-mobile #liQuote a").addClass("active-text");
    } else if (id === "services") {
        $("#nav #liServices").addClass("active");
        $("#nav #liServices a").addClass("active-text");

        $("#nav-mobile #liServices").addClass("active");
        $("#nav-mobile #liServices a").addClass("active-text");
    } else if (id === "aboutus") {
        $("#nav #liAboutUs").addClass("active");
        $("#nav #liAboutUs a").addClass("active-text");

        $("#nav-mobile #liAboutUs").addClass("active");
        $("#nav-mobile #liAboutUs a").addClass("active-text");
    }
}

///**************************************************************************************************
// * Toggle the navigation menu so the active page menu link reflects the page the user is currently 
// * on. This is done by changing the text color.
// * 
// * This will also handle the side navigiation menu
// *************************************************************************************************/
//function toggleNavigationMenu() {
//
//    // Find and remove the active class
//    $("#nav").find(".active").removeClass("active");
//    $("#nav").find(".active-text").removeClass("active-text");
//
//    if ($("#home").length > 0) {
//        $("#nav #liHome").addClass("active");
//        $("#nav #liHome a").addClass("active-text");
//
//        $("#nav-mobile #liHome").addClass("active");
//        $("#nav-mobile #liHome a").addClass("active-text");
//    } else if ($("#quote").length > 0) {
//        $("#nav #liQuote").addClass("active");
//        $("#nav #liQuote a").addClass("active-text");
//
//        $("#nav-mobile #liQuote").addClass("active");
//        $("#nav-mobile #liQuote a").addClass("active-text");
//    } else if ($("#services").length > 0) {
//        $("#nav #liServices").addClass("active");
//        $("#nav #liServices a").addClass("active-text");
//
//        $("#nav-mobile #liServices").addClass("active");
//        $("#nav-mobile #liServices a").addClass("active-text");
//    } else if ($("#aboutus").length > 0) {
//        $("#nav #liAboutUs").addClass("active");
//        $("#nav #liAboutUs a").addClass("active-text");
//
//        $("#nav-mobile #liAboutUs").addClass("active");
//        $("#nav-mobile #liAboutUs a").addClass("active-text");
//    }
//}