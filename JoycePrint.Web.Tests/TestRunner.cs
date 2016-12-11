using System.Collections.Generic;
using OpenQA.Selenium;

namespace JoycePrint.Web.Tests
{
    public static class TestRunner
    {
        public static void RunTest<T>(IEnumerable<IWebDriver> drivers) where T : WebDriverTestBase, new()
        {
            new T().Run(drivers);
        }
    }
}
