using OpenQA.Selenium;
using StockCrawler.Domain;

namespace StockCrawler.Service.StockExtractor
{
    public static class Mappers
    {
        public static Asset MapFII(IWebElement? assetElement)
        {
            var asset = new Asset();

            if (assetElement != null)
            {
                var assetColumns = assetElement.FindElements(By.TagName("td"));

                asset.Ticker = assetColumns[0].FindElement(By.TagName("a")).Text;
                asset.Price = decimal.TryParse(assetColumns[2].Text, out decimal result) ? result : 0;
                asset.Type = Domain.Type.FII;
            }

            return asset;
        }

        public static Asset MapAcao(IWebElement? assetElement)
        {
            var asset = new Asset();

            if (assetElement != null)
            {
                var assetColumns = assetElement.FindElements(By.TagName("td"));

                asset.Ticker = assetColumns[0].FindElement(By.TagName("a")).Text;
                asset.Price = decimal.TryParse(assetColumns[1].Text, out decimal result) ? result : 0;
                asset.Type = Domain.Type.Acao;
            }

            return asset;
        }
    }
}