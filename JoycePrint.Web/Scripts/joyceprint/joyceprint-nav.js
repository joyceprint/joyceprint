"use strict";

var scrollUp;
var lastScrollTop = 0;
var navOffset = -($("nav").height() - 4);

/**************************************************************************************************
 * This is for use with the side menu since the function [getElementIdInViewport] will not work 
 * with the side menu overlay in place. We store the section we are scrolling to here and will 
 * use it in the [toggleNavigationMenu] when the [id] passed in is [undefined]
 *  
*************************************************************************************************/
var toggleNavigationMenu_ScrollToId = null;

/**************************************************************************************************
 * Navigation Menu Javascript Functionality
 *
 * Call this function to initialize the javascript required for the navigation menu
 *************************************************************************************************/
function initializeNavFunctionality() {

    // When the page is loaded / reloaded, this gets the section the page is on and updates the menu link
    document.body.addEventListener("load", toggleNavigationMenu(getElementIdInViewport()));       

    // Sets up the scroll offset, required because of the use of a stick header
    initializeScrollMenu();

    // Sets up the scroll animation when a menu link is clicked
    initializeAnimatedMenu();
}

/**************************************************************************************************
 * Sets up the scrolling animations when navigating around the site
 * 
 *************************************************************************************************/
function initializeAnimatedMenu() {

    var $root = $("html, body");

    $("#nav li a, a, #nav-mobile li a").click(function () {

        var href = $.attr(this, "href");        

        // Break out if the link does not exist
        if (href === "#!") return false;

        toggleNavigationMenu_ScrollToId = href.replace("#", "");

        $root.animate({
            scrollTop: $(href).offset().top + navOffset
        }, getScrollDuration(this, $(href)), function () {
            window.location.hash = href;
        });

        return false;
    });
}

/**************************************************************************************************
 * Gets the duration of the scroll animation based on how far the screen needs to scroll
 * 
 *************************************************************************************************/
function getScrollDuration(currertElement, destinationElement) {
    var duration = 400;

    var source = $(currertElement).offset().top;
    var destination = $(destinationElement).offset().top;

    var distance = destination - source;

    if (distance < 0) distance = (distance * -1);

    if (distance >= 500 && distance <= 999) {
        duration = 600;
    } else if (distance >= 1000 && distance <= 1499) {
        duration = 1000;
    } else if (distance >= 1500) {
        duration = 1500;
    }

    return duration;
}

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
            // Downscroll code            
            scrollUp = false;
        } else {
            // Upscroll code            
            scrollUp = true;
        }
        lastScrollTop = st;

        toggleNavigationMenu(getElementIdInViewport());
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

    if (scrollUp === true)
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

    var elementId;

    var menuHeight = 110;

    // 1 - get the center of the viewport - account for the menu height
    var windowHeight = $(window).height();

    // Sub the menu height from the viewport height
    var windowHeightWithoutMenu = windowHeight - menuHeight;

    var screenCPt = (windowHeightWithoutMenu / 2) + menuHeight;

    // 2 - find the element at that location
    var offsetYPt = screenCPt;

    // Get the center of hte viewport
    var offsetXPt = $(window).width() / 2;

    var elementAtCenter = document.elementFromPoint(offsetXPt, offsetYPt + getScrollAdjust());

    // 3 - navagiate out until you get to a section
    var navSection = $(elementAtCenter).closest("section");

    elementId = $(navSection).attr("id");

    // If the element is undefined we are in the side menu and need to use the store href for where we are going
    if (elementId === undefined) {
        elementId = toggleNavigationMenu_ScrollToId;        
    }

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

    $("#nav-mobile").find(".active").removeClass("active");
    $("#nav-mobile").find(".active-text").removeClass("active-text");

    if (id === "home") {
        $("#nav [name=liHome]").addClass("active");
        $("#nav [name=liHome] a").addClass("active-text");

        $("#nav-mobile [name=liHome]").addClass("active");
        $("#nav-mobile [name=liHome] a").addClass("active-text");
    } else if (id === "quote") {
        $("#nav [name=liQuote]").addClass("active");
        $("#nav [name=liQuote] a").addClass("active-text");

        $("#nav-mobile [name=liQuote]").addClass("active");
        $("#nav-mobile [name=liQuote] a").addClass("active-text");
    } else if (id === "services") {
        $("#nav [name=liServices]").addClass("active");
        $("#nav [name=liServices] a").addClass("active-text");

        $("#nav-mobile [name=liServices]").addClass("active");
        $("#nav-mobile [name=liServices] a").addClass("active-text");
    } else if (id === "aboutus") {
        $("#nav [name=liAboutUs]").addClass("active");
        $("#nav [name=liAboutUs] a").addClass("active-text");

        $("#nav-mobile [name=liAboutUs]").addClass("active");
        $("#nav-mobile [name=liAboutUs] a").addClass("active-text");
    }
}

/**************************************************************************************************
 * Shitfs the scroll position down by the height of the fixed menu
 *
 * This allows href to point to an element location without having the top section covered
 * by the fixed menu. 
 * 
 * THIS FUNCTION IS NOT BEING CALLED - It screws up the animation
 *************************************************************************************************/
//var shiftWindow = function () {
//    scrollBy(0, navOffset);
//};

// Add the hashchange event listener, this event fires when the windows hash changes [location.hash]    
//window.addEventListener("hashchange", shiftWindow);

// the window url using javascript and window element
//window.location.pathname + window.location.hash == '/index.html#contact'