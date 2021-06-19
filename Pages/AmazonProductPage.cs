using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestProject.Utilities;

namespace TestProject.Pages
{
    public class AmazonProductPage
    {
        private IWebDriver _driver;
        private UtilitiyMethods _utility;

        public AmazonProductPage(IWebDriver driver, UtilitiyMethods utility)
        {
            _driver = driver;
            _utility = utility;
        }


        private static By AddtoCart = By.XPath("//input[@id='add-to-cart-button']");

        private static By ProductTitle = By.XPath("//span[@id='productTitle']");

        private static By ProductPrice = By.XPath("//span[@id='priceblock_ourprice' or @id='priceblock_saleprice']");

        public string GetProductName()
        {
            if (_utility.IsElementDisplayed(ProductTitle))
            {
                return _driver.FindElement(ProductTitle).Text;

            }
            return "No Product Name";
        }

        public double GetProductPrice()
        {
            Console.WriteLine(_driver.FindElement(ProductPrice).Text);
            return Convert.ToDouble(_driver.FindElement(ProductPrice).Text.Substring(1));
        }


        public void clickOnAddtoCart()
        {
            _driver.FindElement(AddtoCart).Click();
        }


    }
}