using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Utilities
{
    public class UtilitiyMethods
    {
        private readonly IWebDriver _driver;
        public UtilitiyMethods(IWebDriver driver)
        {
            this._driver = driver;
        }

        public Products readjsondata()
        {
            string Path = @"" + AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\netcoreapp3.1\\", "TestData\\Products.json");
            Products product = JsonConvert.DeserializeObject<Products>(File.ReadAllText(Path));
            return product;
        }

        public bool IsElementDisplayed(By element)
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            bool elt;
            try
            {
                elt = wait.Until<bool>(driver =>
                {

                    var elementTobeDisplayed = driver.FindElement(element);
                    return (elementTobeDisplayed.Displayed) ? true : false;

                });
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
            //catch (Exception)
            //{
            //    return false;
            //}
            return elt;
        }

        public void ScrollInToElement(IWebElement element)
        {
            IJavaScriptExecutor script = (IJavaScriptExecutor)_driver;
            script.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }
    }
}