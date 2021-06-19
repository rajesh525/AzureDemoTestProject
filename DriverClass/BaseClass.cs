using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Utilities;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Reflection;

namespace TestProject.DriverClass
{
    public class BaseClass
    {
        public IWebDriver Driver;
        public BaseClass()
        {

        }

        public IWebDriver GetWebdriver()
        {
            Driver = InitilizeWebDriver();
            return Driver;
        }

        public IWebDriver InitilizeWebDriver()
        {

            try
            {
                IWebDriver _driver = SelectBrowser(Driver);
                _driver.Manage().Cookies.DeleteAllCookies();
                _driver.Manage().Window.Maximize();
                return _driver;
            }
            catch (WebDriverException e)
            {
                Assert.Fail("failed to load driver:" + e.Message);
                return null;
            }

        }

        public IWebDriver SelectBrowser(IWebDriver _driver)
        {
            var BrowserType = "firefox";
            //ConfigurationManager.AppSettings["browser"];
            //Environment.SetEnvironmentVariable("Browser", "firefox");
            var Browser = Environment.GetEnvironmentVariable("Browser", EnvironmentVariableTarget.Process);
            switch (Browser)
            {
                case "chrome":
                    _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    break;

                case "firefox":
                    _driver = new FirefoxDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    break;
                default:
                    _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    break;
            }

            return _driver;

        }

        public void NavigateToUrl(string url)
        {

            Driver.Navigate().GoToUrl(url);
        }

        public void CloseInstance(IWebDriver Driver)
        {
            Driver.Quit();
        }
    }
}