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

                DriverOptions capabilities;

                switch (browser.ToLower())
                {
                    case "chrome":
                        capabilities = new ChromeOptions();
                        ((ChromeOptions)capabilities).AddArguments("disable-infobars");
                        break;
                    case "ie":
                    case "internetexplorer":
                        capabilities = new InternetExplorerOptions();
                        break;
                    case "firefox":
                        capabilities = new FirefoxOptions();
                        break;
                    default:
                        return null;
                }

                return new RemoteWebDriver(uri, capabilities);
            }

            switch (browser.ToLower())
            {
                case "chrome":
                    var options = new ChromeOptions();
                    options.AddArguments("disable-infobars");
                    return new ChromeDriver(options);
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