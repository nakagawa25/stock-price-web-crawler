using OpenQA.Selenium;

namespace StockCrawler.Service.Utils
{
    internal class WebScrappingTools
    {
        private readonly IWebDriver _driver;
        public WebScrappingTools(IWebDriver driver)
        {
            _driver = driver;
        }

        internal string GetTextFromWebElement(string xPath)
        {
            var text = _driver.FindElement(By.XPath(xPath)).Text;
            return string.IsNullOrWhiteSpace(text) ? string.Empty : text;
        }

        internal decimal GetDecimalValueFromWebElement(string xPath)
        {
            var text = _driver.FindElement(By.XPath(xPath)).Text;
            return decimal.TryParse(text, out var decimalValue) ? decimalValue : 0;
        }

        internal void ClickButtonAndSleep(string buttonXPath, int sleepTime)
        {
            _driver.FindElement(By.XPath(buttonXPath)).Click();
            Thread.Sleep(sleepTime);
        }
    }
}
