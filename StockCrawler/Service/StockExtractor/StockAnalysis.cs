using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using StockCrawler.Domain;
using StockCrawler.Service.Utils;
using System.Globalization;

namespace StockCrawler.Service.StockExtractor
{
    internal class StockAnalysis
    {
        private readonly IWebDriver _driver;
        private readonly WebScrappingTools _tools;

        internal StockAnalysis()
        {
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;

            var options = new ChromeOptions();
            options.AddArguments(
                "--headless",
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
        }

        internal AssetInformation GetETFInformation(string ticker)
        {
            var asset = new AssetInformation();
            _driver.Navigate().GoToUrl(string.Concat("https://stockanalysis.com/etf/", ticker));
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            Thread.Sleep(1000);

            var dividendText = _tools.GetTextFromWebElement("//*[@id=\"main\"]/div[2]/div[2]/table[1]/tbody/tr[5]/td[2]");
            dividendText = dividendText.Replace("$", "");

            asset.Dividend = decimal.TryParse(dividendText, out decimal dividend) ? dividend : 0;
            asset.Price = _tools.GetDecimalValueFromWebElement("//*[@id=\"main\"]/div[1]/div[2]/div/div[1]");
            asset.Ticker = ticker;

            return asset;
        }
    }
}
