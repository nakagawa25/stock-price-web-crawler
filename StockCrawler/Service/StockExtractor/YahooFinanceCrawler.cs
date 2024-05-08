using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using StockCrawler.Domain;
using System.Globalization;

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
        }

        internal List<Asset> GetAllStocks()
        {
            _driver.Navigate().GoToUrl("https://finance.yahoo.com/screener/new/");
            Thread.Sleep(1000);

            ClickButtonAndSleep("//*[@id=\"screener-criteria\"]/div[2]/div[1]/div[1]/div[2]/div/div[2]/div/button[4]", 300);
            ClickButtonAndSleep("//*[@id=\"screener-criteria\"]/div[2]/div[1]/div[1]/div[2]/div/div[2]/div/button[3]", 300);
            ClickButtonAndSleep("//*[@id=\"screener-criteria\"]/div[2]/div[1]/div[1]/div[2]/div/div[2]/div/button[2]", 1000);
            ClickButtonAndSleep("//*[@id=\"screener-criteria\"]/div[2]/div[1]/div[3]/button[1]", 3000);
            
            var assets = new List<Asset>();
            var nextPage = true;
            do
            {
                var table = _driver.FindElement(By.XPath("//*[@id=\"scr-res-table\"]/div[1]/table/tbody"));
                var assetsElements = table.FindElements(By.TagName("tr"));
                Thread.Sleep(1000);

                foreach (var asset in assetsElements)    
                    assets.Add(Mappers.MapStock(asset));

                var nextButton = _driver.FindElement(By.XPath("//*[@id=\"scr-res-table\"]/div[2]/button[3]"));

                if (nextButton.Enabled)
                {
                    nextButton.Click();
                    Thread.Sleep(1000);
                }

                nextPage = nextButton.Enabled;

            } while (nextPage);

            return assets;
        }

        internal AssetInformation GetStockInformation(string ticker)
        {
            var asset = new AssetInformation();
            _driver.Navigate().GoToUrl(string.Concat("https://finance.yahoo.com/quote/", ticker));
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            Thread.Sleep(1000);

            var dividendsText = GetTextFromWebElement("//*[@id=\"nimbus-app\"]/section/section/section/article/div[2]/ul/li[14]/span[2]");
            dividendsText = dividendsText.Substring(0, dividendsText.IndexOf("("));
            asset.Dividend = decimal.TryParse(dividendsText, out var dividend) ? dividend : 0;
            asset.Price = GetDecimalValueFromWebElement("//*[@id=\"nimbus-app\"]/section/section/section/article/section[1]/div[2]/div[1]/section/div/section/div[1]/fin-streamer[1]/span");
            asset.Ticker = ticker;

            return asset;
        }

        private string GetTextFromWebElement(string xPath)
        {
            var text = _driver.FindElement(By.XPath(xPath)).Text;
            return string.IsNullOrWhiteSpace(text) ? string.Empty : text;
        }

        private decimal GetDecimalValueFromWebElement(string xPath)
        {
            var text = _driver.FindElement(By.XPath(xPath)).Text;
            return decimal.TryParse(text, out var decimalValue) ? decimalValue : 0;
        }

        private void ClickButtonAndSleep(string buttonXPath, int sleepTime)
        {
            _driver.FindElement(By.XPath(buttonXPath)).Click();
            Thread.Sleep(sleepTime);
        }
    }
}
