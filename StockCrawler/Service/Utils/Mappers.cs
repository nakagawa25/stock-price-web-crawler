using OpenQA.Selenium;
using StockCrawler.Domain;
using System.Globalization;

namespace StockCrawler.Service.Utils
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
                asset.Price = decimal.TryParse(assetColumns[2].Text, out decimal priceResult) ? priceResult : 0;
                asset.Type = Domain.Type.FII;

                var dividendYieldText = assetColumns[4].Text.Replace("%", "");
                var dividendNumber = decimal.TryParse(dividendYieldText, out decimal dividendResult) ? dividendResult : 0;

                asset.Dividend = WebScrappingTools.CalculateDividendValueByYield(asset.Price, dividendNumber);
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

                var dividendYieldText = assetColumns[5].Text.Replace("%", "");
                var dividendNumber = decimal.TryParse(dividendYieldText, out decimal dividendResult) ? dividendResult : 0;

                asset.Dividend = WebScrappingTools.CalculateDividendValueByYield(asset.Price, dividendNumber);
            }

            return asset;
        }

        public static Asset MapStock(IWebElement? assetElement)
        {
            var asset = new Asset();

            if (assetElement != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

                var assetColumns = assetElement.FindElements(By.TagName("td"));
                asset.Ticker = assetColumns[0].FindElement(By.TagName("a")).Text;
                var priceText = assetColumns[2].FindElement(By.TagName("fin-streamer")).Text;
                asset.Price = decimal.TryParse(priceText, out decimal result) ? result : 0;

                asset.Type = Domain.Type.Stock;
            }

            return asset;
        }
    }
}