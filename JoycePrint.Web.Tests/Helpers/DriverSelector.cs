using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace JoycePrint.Web.Tests.Helpers
{
    public static class DriverSelector
    {
        public static IWebDriver GetWebDriver(string browser, string standaloneServerAddress)
        {
            if (!string.IsNullOrEmpty(standaloneServerAddress))
            {
                var uri = new Uri(standaloneServerAddress);

                DesiredCapabilities capabilities;

                switch (browser.ToLower())
                {
                    case "chrome":
                        capabilities = DesiredCapabilities.Chrome();
                        break;
                    case "ie":
                    case "internetexplorer":
                        capabilities = DesiredCapabilities.InternetExplorer();
                        break;
                    case "firefox":
                        capabilities = DesiredCapabilities.Firefox();
                        break;
                    default:
                        return null;
                }

                return new RemoteWebDriver(uri, capabilities);
            }

            switch (browser.ToLower())
            {
                case "chrome":
                    return new ChromeDriver();
                case "firefox":
                    return new FirefoxDriver();
                case "ie":
                case "internetexplorer":
                case "internet explorer":
                    return new InternetExplorerDriver();
                default:
                    return null;
            }
        }
    }
}