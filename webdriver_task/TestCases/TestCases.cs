using NUnit.Framework;
using webdriver_task.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace webdriver_task.TestCases
{
    [TestFixture]
    class TestCases
    {
        IWebDriver driver;
        TimeSpan timeout;
        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            timeout = TimeSpan.FromSeconds(20);
        }
        [Test, Order(1),Description("Perform search by clicking Search button and verify results")]
        [Obsolete]
        public void Test1([Values("cheese")] string searchQuery)
        {
            GoogleSearchPage searchPage = new GoogleSearchPage(driver,timeout).Load();
            searchPage.TypeInSearchField(searchQuery);
            GoogleSearchResultsPage resultsPage = searchPage.ClickSearchButton();
            var results = resultsPage.CollectSearchResultsDescriptions();
            for (int i = 0; i < results.Count; i++)
            {
                Assert.That(results[i].Contains(searchQuery));
            }
        }
        [Test, Order(2), Description("Perform search with clarifying query by clicking Enter button and  verify results")]
        [Obsolete]
        public void Test2([Values("cheese")] string searchQuery,[Values("wikipedia.com")] string site)
        {
            GoogleSearchPage searchPage = new GoogleSearchPage(driver, timeout).Load();
            searchPage.TypeInSearchField($"{searchQuery} site:{site}");
            GoogleSearchResultsPage resultsPage = searchPage.ClickEnterOnSearchField();
            var links = resultsPage.CollectSearchResultsLinks();
            var results = resultsPage.CollectSearchResultsDescriptions();
            for (int i = 0; i < links.Count; i++)
            {
                Assert.That(links[i].Contains(site));
                Assert.That(results[i].Contains(searchQuery));
            }
           
        }
        [Test, Order(3), Description("Perform parametrized search and verify quantity of results")]
        [Obsolete]
        public void Test3([Values(4)] int numberOfResults, [Values("cheese", "meat")] string searchQuery) 
        {
            GoogleSearchResultsPage resultsPage = new GoogleSearchResultsPage(driver);
            resultsPage.GoToPageWithParams(searchQuery, numberOfResults);
            var results = resultsPage.CollectAllResults();
            Assert.That(results.Count == numberOfResults, "results quantity is wrong");
        }
        [Test, Order(4), Description("Run Test3 for all possible values for number of results")]
        [Obsolete]
        public void Test3a([Range(0, 10, 1)]int numberOfResults, [Values("cheese")] string searchQuery) 
        {
            GoogleSearchResultsPage resultsPage = new GoogleSearchResultsPage(driver);
            resultsPage.GoToPageWithParams(searchQuery,numberOfResults);
            var results = resultsPage.CollectAllResults();
            Assert.That(results.Count == numberOfResults, "results quantity is wrong");
        }
        [TearDown]
        public void TearDown()
        {
            driver.Navigate().Back();
        }
        [OneTimeTearDown]
        public void FinishTest()
        {
            driver.Quit();
        }

    }
}
