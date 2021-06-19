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
    public class AmazonLandingPage
    {
        private IWebDriver _driver;
        private AmazonProductPage _amazonProductPage;
        private UtilitiyMethods _utility;

        public AmazonLandingPage(IWebDriver driver, AmazonProductPage amazon, UtilitiyMethods utility)
        {
            _driver = driver;
            _amazonProductPage = amazon;
            _utility = utility;
        }


        private static By FeatureDropdow = By.XPath("//select[@name='s']");

        private static By SearchBox = By.XPath("//input[@type='text' and @id='twotabsearchtextbox']");

        private static By SearchButton = By.XPath("//input[@type='submit' and @value='Go']");


        public void EnterTextIntoSearchBoxAndSearch(string product)
        {
            if (_utility.IsElementDisplayed(SearchBox))
            {
                _driver.FindElement(SearchBox).SendKeys(product);
                _driver.FindElement(SearchButton).Click();

            }

        }


        public void SelectFeaturedDropdwon(string option)
        {

            SelectElement dropdown = new SelectElement(_driver.FindElement(FeatureDropdow));

            if (_utility.IsElementDisplayed(FeatureDropdow))
                dropdown.SelectByText(option);
            //Thread.Sleep(30000);


        }


        public Dictionary<string, double> AddElementsToCartAndReturn()
        {
            Dictionary<string, double> products = new Dictionary<string, double>();

            IList<IWebElement> elements = _driver.FindElements(By.XPath("//a[@class='a-link-normal a-text-normal']"));
            Console.WriteLine("Number of elements:" + elements.Count());
            string parent = _driver.CurrentWindowHandle;
            //int NumberOfProducts = Convert.ToInt32(_utility.readjsondata().NoOfProducts);
            int NumberOfProducts = 1;
            foreach (IWebElement element in elements.Take(NumberOfProducts))
            {
                //  _utility.ScrollInToElement(elements[i]);
                element.Click();
                var productWindow = _driver.WindowHandles.FirstOrDefault(w => !w.Equals(parent));
                _driver.SwitchTo().Window(productWindow);
                products.Add(_amazonProductPage.GetProductName(), _amazonProductPage.GetProductPrice());
                _amazonProductPage.clickOnAddtoCart();
                _driver.Close();

                _driver.SwitchTo().Window(parent);
                //Thread.Sleep(50000);
            }



            return products;
        }



    }
}