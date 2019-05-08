using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;

namespace webdriver_task.PageObjects
{
    class GoogleSearchResultsPage 
    {
        private IWebDriver driver;

        [System.Obsolete]
        public GoogleSearchResultsPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "span.st")]
        private IList<IWebElement> AllElementsDescription { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div.TbwUpd")]
        private IList<IWebElement> AllElementsLinks { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div.e2BEnf")]
        private IList<IWebElement> AdditionalFieldsOnSearchResPage { get; set; }

        [FindsBy(How = How.CssSelector, Using = "h3.med")]
        private IWebElement NotNeededElementForTest { get; set; }

        [Obsolete]
        public GoogleSearchResultsPage GoToPageWithParams(string searchQuery, int numberOfResults)
        {
            driver.Navigate().GoToUrl($"https://www.google.com/search?q={searchQuery}&num={numberOfResults}");
            return new GoogleSearchResultsPage(driver);
        }

        public List<String> CollectSearchResultsDescriptions()
        {
            List<String> textOfElements = new List<String>();
            foreach (IWebElement element in AllElementsDescription)
            {
                textOfElements.Add(element.Text.ToLower());
            }
            return textOfElements;
        }

        public List<String> CollectSearchResultsLinks()
        {
            List<String> textOfElements = new List<String>();
            foreach (IWebElement element in AllElementsLinks)
            {
                textOfElements.Add(element.Text.ToLower());
            }
            return textOfElements;
        }

        public List<String> CollectAllResults()
        {
            List<String> textOfElements = new List<String>();
            foreach (IWebElement element in AllElementsDescription)
            {
                textOfElements.Add(element.Text.ToLower());
            }
            foreach (IWebElement element in AdditionalFieldsOnSearchResPage)
            {
                if (element.Text.ToLower()!= NotNeededElementForTest.Text.ToLower())
                textOfElements.Add(element.Text.ToLower());
            }
            return textOfElements;
        }

    }
}