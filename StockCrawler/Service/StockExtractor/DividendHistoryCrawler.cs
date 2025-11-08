using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using StockCrawler.Domain;
using StockCrawler.Service.Utils;

namespace StockCrawler.Service.StockExtractor
{
    internal class DividendHistoryCrawler
    {
        private IWebDriver _driver = null!;
        private WebScrappingTools _tools = null!;

        internal void InitWebDriver()
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;

            var options = new ChromeOptions();
            options.AddArguments(
                "--headless=new",
                "--no-sandbox",
                "--disable-web-security",
                "--disable-gpu",
                "--incognito",
                "--proxy-bypass-list=*",
                "--proxy-server='direct://'",
                "--log-level=3",
                "--hide-scrollbars");

            _driver = new ChromeDriver(chromeDriverService, options);
            _tools = new WebScrappingTools(_driver);

            _driver.Navigate().GoToUrl($"https://dividendhistory.net/msft-dividend-yield");
            Thread.Sleep(1000);
        }

        internal void DisposeWebDriver()
        {
            _driver?.Dispose();
        }

        internal AssetInformation GetStockInformation(string ticker)
        {
            _driver.Navigate().GoToUrl($"https://dividendhistory.net/{ticker}-dividend-yield");
            Thread.Sleep(10);

            var priceExtracted = _driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div/table[2]/tbody/tr[3]/td"));
            var dividendExtracted = _driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div/table[2]/tbody/tr[10]/td"));

            var priceText = priceExtracted.Text.Replace(".", ",").Replace("$", "");
            var dividendText = dividendExtracted.Text.Replace(" Per Share", "").Replace(".", ",").Replace("$", "");

            Decimal.TryParse(priceText, out decimal price);
            Decimal.TryParse(dividendText, out decimal dividend);

            var asset = new AssetInformation()
            {
                Price = price,
                Dividend = dividend,
                Ticker = ticker
            };

            return asset;
        }
    }
}
