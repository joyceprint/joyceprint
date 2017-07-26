"use strict";

/**************************************************************************************************
 * Navigation Menu Library
 *
 * Contains the code to deal with the navigation menu
 * The functionality contained in this file will be added to the jLib.nav global object
 *  
 *************************************************************************************************/
var jLib = (function (parent, $) {

    // Create the loading sub module off the parent, if it does not exist
    var subModule = parent.nav = parent.nav || {};

/**************************************************************************************************
 * PUBLIC PROPERTIES
 *************************************************************************************************/

    subModule.scrollUp = null;
    subModule.lastScrollTop = 0;
    subModule.navOffset = -($("nav").height() - 4);

    /**************************************************************************************************
     * This is for use with the side menu since the function [getElementIdInViewport] will not work 
     * with the side menu overlay in place. We store the section we are scrolling to here and will 
     * use it in the [toggleNavigationMenu] when the [id] passed in is [undefined] 
     *************************************************************************************************/
    subModule.toggleNavigationMenu_ScrollToId = null;

/**************************************************************************************************
 * PUBLIC METHODS
 *************************************************************************************************/

    /************************************************************************************************** 
     * Call this function to initialize the javascript required for the navigation menu     
     *************************************************************************************************/
    subModule.initNav = function () {

        // When the page is loaded / reloaded, this gets the section the page is on and updates the menu link
        document.body.addEventListener("load", subModule.toggleNavMenu(getElementIdInViewport()));       

        // Sets up the scroll offset, required because of the use of a stick header
        subModule.initScrollMenu();

        // Sets up the scroll animation when a menu link is clicked
        subModule.initAnimatedMenu();
    }

    /**************************************************************************************************
    * Sets up the scrolling animations when navigating around the site 
     *************************************************************************************************/
    subModule.initAnimatedMenu = function () {

        var $root = $("html, body");

        $("#nav li a, a, #nav-mobile li a").click(function () {

            var href = $.attr(this, "href");        

            // Break out if the link does not exist
            if (href === "#!") return false;

            subModule.toggleNavigationMenu_ScrollToId = href.replace("#", "");

            $root.animate({
                scrollTop: $(href).offset().top + subModule.navOffset
            }, getScrollDuration(this, $(href)), function () {
                window.location.hash = href;
            });

            return false;
        });
    }

    /**************************************************************************************************
     * Initializes the menu update via scrolling and resizing
     * 
     * Toggles the navigation menu based on where the user is on the page and which direction
     * they are scrolling
     *************************************************************************************************/
    subModule.initScrollMenu = function () {

        // This accounts for updating the menu link when the user is scrolling and resizing the screen
        $(window).on("resize scroll", function () {

            var st = $(this).scrollTop();

            if (st > subModule.lastScrollTop) {
                // Downscroll code            
                subModule.scrollUp = false;
            } else {
                // Upscroll code            
                subModule.scrollUp = true;
            }
            
            subModule.lastScrollTop = st;

            subModule.toggleNavMenu(getElementIdInViewport());
        });
    }

    /**************************************************************************************************
     * Toggle the navigation menu so the active page menu link reflects the view the user has currently 
     * scrolled on to. This is done by changing the text color.
     * 
     * This will also handle the side navigiation menu 
     * 
     * @param {string} id - The id of the menu link to activate     
     *************************************************************************************************/
    subModule.toggleNavMenu = function (id) {

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
        } else if (id === "services") {
            $("#nav [name=liContactUs]").addClass("active");
            $("#nav [name=liContactUs] a").addClass("active-text");

            $("#nav-mobile [name=liContactUs]").addClass("active");
            $("#nav-mobile [name=liContactUs] a").addClass("active-text");
        } 
    }

/**************************************************************************************************
 * PRIVATE METHODS
 *************************************************************************************************/

    /**************************************************************************************************
     * Gets the duration of the scroll animation based on how far the screen needs to scroll
     * 
     * @param {string} currertElement - The current element in view.
     * @param {string} destinationElement - The destination element to scroll to.
     * @returns {number} duration - The duration of the scroll animation.
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
     * Get the scroll adjust
     *
     * This is the value to add to the vertical center point of the viewport
     * The scroll direction is checked and the offset applied
     * 
     * @returns {number} scrollAdjust - The scroll adjustment due to the use of the fixed menu
     *************************************************************************************************/
    function getScrollAdjust() {

        var scrollAdjust = 0;

        // TODO: This needs to adjust up or down using my screen size as a ratio
        var scrollAdjustUp = 200;
        var scrollAdjustDown = -200;

        if (subModule.scrollUp === true)
            scrollAdjust = scrollAdjustUp;
        else if (subModule.scrollUp === false)
            scrollAdjust = scrollAdjustDown;

        return scrollAdjust;
    }

    /**************************************************************************************************
     * Gets the element in the viewport
     * 
     * This gets the Id of the section using the center of the viewport and the scroll offset
     * 
     * @returns {string} elementId - The id of the element in the center of the viewport
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
        var navSection = $(elementAtCenter).closest("section[data-menu-item]");

        elementId = $(navSection).attr("id");

        // If the element is undefined we are in the side menu and need to use the store href for where we are going
        if (elementId === undefined) {
            elementId = subModule.toggleNavigationMenu_ScrollToId;
        }

        return elementId;
    }

    // Return the parent, this allows multiple files to contribute to the same module
    return parent;

}(jLib || {}, jQuery));