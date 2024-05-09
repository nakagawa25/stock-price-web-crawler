using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using StockCrawler.Domain;
using StockCrawler.Service.Utils;

namespace StockCrawler.Service.StockExtractor
{
    internal class FundamentusCrawler
    {
        private readonly IWebDriver _driver;

        internal FundamentusCrawler()
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

        internal List<Asset> GetFIIs()
        {
            var assets = GetAssetBrazilBase
                (
                    "https://www.fundamentus.com.br/fii_resultado.php", 
                    "//*[@id=\"tabelaResultado\"]/tbody", 
                    Mappers.MapFII
                );

            return assets;
        }

        internal List<Asset> GetAcoes()
        {
            var assets = GetAssetBrazilBase
                (
                    "https://www.fundamentus.com.br/resultado.php",
                    "//*[@id=\"resultado\"]/tbody",
                    Mappers.MapAcao
                );

            return assets;
        }

        private List<Asset> GetAssetBrazilBase(string baseUrl, string baseAssetsTable, Func<IWebElement?, Asset> MapToAsset)
        {
            var assets = new List<Asset>();

            _driver.Navigate().GoToUrl(baseUrl);

            var assetsTable = _driver.FindElement(By.XPath(baseAssetsTable));

            if (assetsTable != null)
            {
                var assetsList = assetsTable.FindElements(By.TagName("tr"));

                foreach (var asset in assetsList)
                    assets.Add(MapToAsset(asset));         
            }

            return assets;
        }
    }
}
