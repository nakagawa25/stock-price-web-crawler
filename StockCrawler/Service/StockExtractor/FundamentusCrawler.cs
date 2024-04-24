using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using StockCrawler.Domain;

namespace StockCrawler.Service.StockExtractor
{
    public class FundamentusCrawler
    {
        private readonly IWebDriver _driver;

        public FundamentusCrawler()
        {
            var options = new ChromeOptions();
            options.AddArgument("headless");

            _driver = new ChromeDriver();
        }

        public List<Asset> GetFIIs()
        {
            var assets = GetAssetBrazilBase
                (
                    "https://www.fundamentus.com.br/fii_resultado.php", 
                    "//*[@id=\"tabelaResultado\"]/tbody", 
                    Mappers.MapFII
                );

            return assets;
        }

        public List<Asset> GetAcoes()
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
