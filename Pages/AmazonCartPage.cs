using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestProject.Pages
{
    public class AmazonCartPage
    {
        private IWebDriver _driver;
        public AmazonCartPage(IWebDriver driver)
        {
            _driver = driver;
        }

        private static By Cartlink = By.XPath("//a[@id='nav-cart']");

        private static By SubTotal = By.XPath("//span[@id='sc-subtotal-amount-activecart']");

        private static By ProductTitle = By.XPath("//span[contains(@class,'a-size-medium sc-product-title')]");

        private static By ProductPrice = By.XPath("//p[@class='a-spacing-small']");

        public void ClickOnCart()
        {
            _driver.FindElement(Cartlink).Click();
        }

        public string GetProductName(IWebElement productname)
        {

            return productname.Text;
        }

        public double GetProductPrice(IWebElement productprice)
        {
            var price = productprice.Text;
            Console.WriteLine(price);
            try
            {
                return Convert.ToDouble(price.Substring(1));
            }
            catch (Exception)
            {
                Console.WriteLine(price.Length);
                return Convert.ToDouble(price);
            }

        }

        public Dictionary<string, double> GetProcutsInCart()
        {
            Dictionary<string, double> CartProducts = new Dictionary<string, double>();
            IList<IWebElement> productNames = _driver.FindElements(ProductTitle);
            IList<IWebElement> productPrices = _driver.FindElements(ProductPrice);

            for (int i = 0; i < productNames.Count; i++)
            {
                CartProducts.Add(GetProductName(productNames[i]), GetProductPrice(productPrices[i]));
            }


            return CartProducts;
        }



        public double GetSubTotal()
        {
            return Convert.ToDouble(_driver.FindElement(SubTotal).Text.Substring(1));
        }


    }
}
