using TestProject.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Linq;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

    namespace TestProject.Steps
    {
        [Binding]
        public class TestAmazonScenarioSteps
        {
            private readonly IWebDriver _driver;
            private readonly AmazonLandingPage _amazonLandingPage;
            private readonly AmazonCartPage _amazonCartPage;
            private Dictionary<string, double> searchproducts;
            private Dictionary<string, double> cartproducts;

            public TestAmazonScenarioSteps(IWebDriver driver, AmazonLandingPage amazonLandingPage, AmazonCartPage amazonCartPage)
            {
                _driver = driver;
                _amazonLandingPage = amazonLandingPage;
                _amazonCartPage = amazonCartPage;

            }

            [Given(@"I have product '(.*)' to search")]
            public void GivenIHaveProductToSearch(string product)
            {
                //if (product.Equals("bat")) Assert.Fail("Unknown product");
                //else
                _amazonLandingPage.EnterTextIntoSearchBoxAndSearch(product);
            }

            [Given(@"I Select Sort By filter '(.*)' option")]
            public void GivenISelectSortByFilterLowToHighOption(string option)
            {
                _amazonLandingPage.SelectFeaturedDropdwon(option);
            }

            [Given(@"Take the No\.of Products from list and add to cart")]
            public void GivenTakeTheNo_OfProductsFromListAndAddToCart()
            {

                searchproducts = _amazonLandingPage.AddElementsToCartAndReturn();

            }

            [Then(@"the cart should match the products selected in product search")]
            public void ThenTheCartShouldMatchTheProductsSelectedInProductSearch()
            {
                _amazonCartPage.ClickOnCart();

                cartproducts = _amazonCartPage.GetProcutsInCart();
                var subtotal = _amazonCartPage.GetSubTotal();
                Assert.Multiple(() => {
                    Assert.AreEqual(searchproducts.Count, cartproducts.Count);
                    Assert.AreEqual(searchproducts.Sum(k => k.Value), subtotal);
                });

                foreach (KeyValuePair<string, double> kvp in searchproducts)
                {
                    Assert.IsNotNull(cartproducts[kvp.Key]);
                    Assert.AreEqual(kvp.Value, cartproducts[kvp.Key]);
                }

            }
        }
    }