"use strict";

/**************************************************************************************************
 * Extensions for jQuery
 *
 * All extensions for javascript and jQuery are defined here
 *************************************************************************************************/

(function () {

    /**********************************************************************************************
     * Adding a contains function for string objects
     * Currently this is only supported in firefox
     * 
     * @param {string} str - The string to search
     * @param {string} startIndex - The index to start the search
     * @returns {boolean} match - A flag indicating if a match was found
     **********************************************************************************************/
    if (!('contains' in String.prototype)) {
        String.prototype.contains = function (str, startIndex) {
            return -1 !== String.prototype.indexOf.call(this, str, startIndex);
        };
    }
})();

/**************************************************************************************************
 * Regular Expression Filter - 3rd Party
 *
 * http://james.padolsey.com/javascript/regex-selector-for-jquery/
 * 
 * @param {string} elem - The element to search
 * @param {string} index - 
 * @param {string} match - The text to match
 * @returns {boolean} isMatched - A flag indicating if a match was obtained
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
 * Element In Viewport Check
 *
 * This is used to check where the user has scrolled to 
 *
 * NOTE: This needs to be updated to not change on the scroll up until the item is in view
 * currently it works when the bottom of the element is visible / or the top depending on the 
 * scroll direction
 * 
 * @returns {boolean} elementBottom - Determines if we are at the bottom of an element
 *************************************************************************************************/
$.fn.isInViewport = function () {
    var menuHeight = 110;

    // $(this).offset() - Get the current coordinates of the first element in the set of matched 
    // elements, relative to the document. Offset returns top and left
    var elementTop = $(this).offset().top;

    // $(this).outerHeight(); - Gets the outer height of an element including [padding, border, margin:optional(defaults:false)]
    var elementBottom = elementTop + $(this).outerHeight();

    // $(window).scrollTop() - Get the current vertical position of the scroll bar for the first 
    // element in the set of matched elements
    var viewportTop = $(window).scrollTop() + menuHeight;

    // $(window).height() - The height of the window element
    var viewportBottom = viewportTop + $(window).height();

    return elementBottom > viewportTop && elementTop < viewportBottom;
};

/**************************************************************************************************
 * jQuery Extension - Clear Validation
 *
 * The resetValidation function doesn't work so this is it's replacement 
 * This function works with MVC ValidationFor Html Helpers
 * 
 * 
 * Call using $("#<FORM ID>").clearValidation();
 * 
 * NOTE: This is not in use as materialize is used on the site, this is here as a reference
 *************************************************************************************************/
$.fn.clearValidation = function () {

    //Internal $.validator is exposed through $(form).validate()
    var validator = $(this).validate();

    //Iterate through named elements inside of the form, and mark them as error free
    $("[name]", this).each(function () {
        validator.successList.push(this);//mark as error free
        validator.showErrors();//remove error messages if present
    });

    validator.resetForm();//remove error class on name elements and clear history
    validator.reset();//remove all error and success data
}