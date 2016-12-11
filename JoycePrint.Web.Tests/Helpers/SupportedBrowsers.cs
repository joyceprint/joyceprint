using JoycePrint.Web.Tests.Enums;
using JoycePrint.Web.Tests.Helpers;
using System.Collections.Generic;

namespace JoycePrint.Web.Tests
{
    public static class Supported
    {
        /// <summary>
        /// A list of browsers to run the tests in
        /// </summary>
        public static readonly IEnumerable<string> Browsers = new List<string> { "chrome" };
        //public static readonly IEnumerable<string> Browsers = new List<string> { "chrome", "firefox" };
        //public static readonly IEnumerable<string> Browsers = new List<string> { "chrome", "ie", "firefox" };

        /// <summary>
        /// Gets the screen size for the screen type passed in
        /// </summary>
        public static ScreenSize GetScreenSize (ScreenType screenType)
        {
            ScreenSize screenSize = null;

            switch(screenType)
            {
                case ScreenType.Tiny:
                    screenSize = new ScreenSize(568, 320); // IPhone 5
                    break;
                case ScreenType.Small:
                    screenSize = new ScreenSize(667, 375); // IPhone 6
                    break;
                case ScreenType.Medium:
                    screenSize = new ScreenSize(1024, 768); // IPad
                    break;
                case ScreenType.Large:
                    screenSize = new ScreenSize(1366, 768); // Laptop [My Dell]
                    break;
                default:
                    screenSize = new ScreenSize(1366, 768); // Monitor
                    break;
            }

            return screenSize;
        }
    }
}
