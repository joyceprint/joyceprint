using System.Collections.Generic;

namespace JoycePrint.Web.Tests
{
    public static class SupportedBrowsers
    {
        /// <summary>
        /// A list of browsers to run the tests in
        /// </summary>
        public static readonly IEnumerable<string> Browsers = new List<string> { "chrome" };
        //public static readonly IEnumerable<string> Browsers = new List<string> { "chrome", "firefox" };
        //public static readonly IEnumerable<string> Browsers = new List<string> { "chrome", "ie", "firefox" };
    }
}
