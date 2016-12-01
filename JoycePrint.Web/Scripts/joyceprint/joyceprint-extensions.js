/**************************************************************************************************
 *  Javascript Extensions Functionality
 *
 *
 *************************************************************************************************/

(function () {

    /**************************************************************************************************
    *  Adding a contains function for string objects
    * Currently this is only supported in firefox
    *************************************************************************************************/
    if (!('contains' in String.prototype)) {
        String.prototype.contains = function (str, startIndex) {
            return -1 !== String.prototype.indexOf.call(this, str, startIndex);
        };
    }
})();