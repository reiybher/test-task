using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using OpenQA.Selenium.Support.UI;


namespace webdriver_task.PageObjects
{
    class GoogleSearchPage : SlowLoadableComponent<GoogleSearchPage>
    {
        private IWebDriver driver;
        public TimeSpan timeout = TimeSpan.FromSeconds(20);

        [Obsolete]
   
        public GoogleSearchPage(IWebDriver driver, TimeSpan timeout):base(timeout)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }


        [FindsBy(How = How.Name, Using = "q")]
        public IWebElement GoogleSearchField { get; set; }

        [FindsBy(How = How.Name, Using = "btnK")]
        public IWebElement SubmitBtn { get; set; }

        public void TypeInSearchField(string text)
        {
            GoogleSearchField.SendKeys(text);
        }

        [Obsolete]
        public GoogleSearchResultsPage ClickSearchButton()
        {
            SubmitBtn.Click();
            return new GoogleSearchResultsPage(driver);
        }

        [Obsolete]
        public GoogleSearchResultsPage ClickEnterOnSearchField()
        {
            GoogleSearchField.SendKeys(Keys.Enter);
            return new GoogleSearchResultsPage(driver);
        }

        protected override void ExecuteLoad()
        {
            driver.Navigate().GoToUrl("https://www.google.com/");
        }

        protected override bool EvaluateLoadedStatus()
        {
            string url = driver.Url;
            if (url.Contains("google.com")==false)
            {
                return false;
            }
            return true;
        }
    }
}
