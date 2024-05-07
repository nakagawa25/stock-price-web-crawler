using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using StockCrawler.Domain;

namespace StockCrawler.Service.StockExtractor
{
    internal class YahooFinanceCrawler
    {
        private readonly IWebDriver _driver;

        internal YahooFinanceCrawler()
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;

            var options = new ChromeOptions();
            //options.AddArguments(
            //    "--headless",
            //    "--no-sandbox",
            //    "--disable-web-security",
            //    "--disable-gpu",
            //    "--incognito",
            //    "--proxy-bypass-list=*",
            //    "--proxy-server='direct://'",
            //    "--log-level=3",
            //    "--hide-scrollbars");

            _driver = new ChromeDriver(chromeDriverService, options);
        }

        internal List<Asset> GetStocks()
        {
            var assets = new List<Asset>();

            _driver.Navigate().GoToUrl("https://finance.yahoo.com/screener/new/");
            Thread.Sleep(1000);

            _driver.FindElement(By.XPath("//*[@id=\"screener-criteria\"]/div[2]/div[1]/div[1]/div[2]/div/div[2]/div/button[4]")).Click();
            Thread.Sleep(300);
            _driver.FindElement(By.XPath("//*[@id=\"screener-criteria\"]/div[2]/div[1]/div[1]/div[2]/div/div[2]/div/button[3]")).Click();
            Thread.Sleep(300);
            _driver.FindElement(By.XPath("//*[@id=\"screener-criteria\"]/div[2]/div[1]/div[1]/div[2]/div/div[2]/div/button[2]")).Click();
            Thread.Sleep(1000);

            _driver.FindElement(By.XPath("//*[@id=\"screener-criteria\"]/div[2]/div[1]/div[3]/button[1]")).Click();
            Thread.Sleep(3000);

            var nextPage = true;
            do
            {
                var table = _driver.FindElement(By.XPath("//*[@id=\"scr-res-table\"]/div[1]/table/tbody"));
                var assetsElements = table.FindElements(By.TagName("tr"));
                Thread.Sleep(1000);

                foreach (var asset in assetsElements)
                {
                    assets.Add(Mappers.MapStock(asset));
                }

                var nextButton = _driver.FindElement(By.XPath("//*[@id=\"scr-res-table\"]/div[2]/button[3]"));

                if (nextButton.Enabled)
                {
                    nextButton.Click();
                    Thread.Sleep(1000);
                }
                else
                {
                    nextPage = false;
                }
            } while (nextPage);

            return assets;
        }
    }
}
