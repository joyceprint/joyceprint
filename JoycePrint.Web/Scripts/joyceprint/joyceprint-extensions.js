"use strict";

/**************************************************************************************************
 *  Javascript Extensions Functionality
 *
 * All extensions for javascript and jQuery are defined here
 *************************************************************************************************/

(function () {

    /**************************************************************************************************
    * Adding a contains function for string objects
    * Currently this is only supported in firefox
    *************************************************************************************************/
    if (!('contains' in String.prototype)) {
        String.prototype.contains = function (str, startIndex) {
            return -1 !== String.prototype.indexOf.call(this, str, startIndex);
        };
    }
})();

/**************************************************************************************************
 * jQuery Regular Expression Filter - 3rd Party
 *
 * http://james.padolsey.com/javascript/regex-selector-for-jquery/
 *************************************************************************************************/
jQuery.expr[':'].regex = function (elem, index, match) {
    var matchParams = match[3].split(','),
        validLabels = /^(data|css):/,
        attr = {
            method: matchParams[0].match(validLabels) ?
                        matchParams[0].split(':')[0] : 'attr',
            property: matchParams.shift().replace(validLabels, '')
        },
        regexFlags = 'ig',
        regex = new RegExp(matchParams.join('').replace(/^\s+|\s+$/g, ''), regexFlags);
    return regex.test(jQuery(elem)[attr.method](attr.property));
}

/**************************************************************************************************
 * jQuery Element In Viewport Check
 *
 * This is used to check where the user has scrolled to 
 *
 * NOTE: This needs to be updated to not change on the scroll up until the item is in view
 * currently it works when the bottom of the element is visible / or the top depending on the 
 * scroll direction
 *************************************************************************************************/
$.fn.isInViewport = function () {
    var menuHeight = 110;

    var elementTop = $(this).offset().top;
    var elementBottom = elementTop + $(this).outerHeight();

    var viewportTop = $(window).scrollTop() + menuHeight;
    var viewportBottom = viewportTop + $(window).height();

    return elementBottom > viewportTop && elementTop < viewportBottom;
};