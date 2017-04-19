"use strict";

/**************************************************************************************************
 * Loading Screens Functionality
 * 
 * Contains the code to display the loading images while the server is busy
 *************************************************************************************************/

/**************************************************************************************************
 * Page Fade In on load
 * 
 * @param  {string} load    Class targeting elements to load
 * @param  {string} loading Class to apply when elements are loading
 * @param  {string} loaded  Class to apply when elements have loaded
 * @return {void}
 * 
 * This function will only run if it detects the interaction-load on the main tag
 *************************************************************************************************/
pageFadeIn("interaction-load", "interaction-loading", "interaction-loaded");

function pageFadeIn(load, loading, loaded) {
        
    // Get the targeted elements
    var targets = document.querySelectorAll("main." + load);
    
    // Get the loader
    var loader = document.querySelector(".media-loader-holder");
    
    // Get the last index
    var last = targets.length - 1;
    
    // If we have targets
    if (targets.length) {
        
        // Loop through targets
        targets.forEach(target => {
            
            // Set the opacity of each element to 0
            target.style.opacity = 0;
        });

        // Watch for window load
        window.addEventListener("load", () => {
            
            // Item index
            var i = 0;

            // Loop through targets
            targets.forEach(target => {
                
                // Set timeout based upon index
                setTimeout(() => {
                    
                    // Add the loading class
                    target.classList.add(loading);
                    
                    // Remove style attribute from element
                    target.style.opacity = 1;
                    
                    // Add the loaded class
                    target.classList.add(loaded);
                    
                    // After half a second, remove all classes
                    setTimeout(() => {
                        target.classList.remove(load,loading,loaded);
                    }, 500);
                    
                    // If we're on the last item
                    if (i === last && loader) {
                        // Hide the loader
                        loader.style.opacity = 0;
                        setTimeout(() => loader.parentNode.removeChild(loader), 400);
                    }
                    
                    i++;
                }, i * 200);
                // Increate the index
            });
        });
    }
}