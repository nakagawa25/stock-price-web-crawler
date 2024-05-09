using StockCrawler.Domain;
using StockCrawler.Service.StockExtractor;

namespace StockCrawler.Service
{
    public class AssetsService
    {
        private readonly WatchList _watchList;
        private readonly FundamentusCrawler _webCrawler;
        private readonly YahooFinanceCrawler _yahooFinanceCrawler;
        private readonly StockAnalysis _stockAnalysis;

        public AssetsService()
        {
            _watchList = new WatchList();
            _webCrawler = new FundamentusCrawler();
            _yahooFinanceCrawler = new YahooFinanceCrawler();
            _stockAnalysis = new StockAnalysis();
        }

        public List<Asset> GetBrazilianWalletAssets()
        {
            var allAcoes = _webCrawler.GetAcoes();
            var allFIIs = _webCrawler.GetFIIs();
            allAcoes.AddRange(allFIIs);

            var watchList = _watchList.Acoes;
            watchList.AddRange(_watchList.FIIs);

            var walletAssets = allAcoes
                .Where(x => watchList.Contains(x.Ticker))
                .OrderBy(x => watchList.IndexOf(x.Ticker))
                .ToList();

            return walletAssets;
        }

        public List<Asset> GetUSAWalletAssets()
        {
            var watchList = _watchList.Stocks;
            var allStocks = _yahooFinanceCrawler.GetAllStocks();

            var walletAssets = allStocks
                .Where(x => watchList.Contains(x.Ticker))
                .OrderBy(x => watchList.IndexOf(x.Ticker))
                .ToList();

            //return walletAssets;
            return allStocks;
        }

        public List<AssetInformation> GetStocksInformation()
        {
            var assets = new List<AssetInformation>();

            var stocksList = _watchList.Stocks;
            foreach (var stock in stocksList)
            {
                var asset = _yahooFinanceCrawler.GetStockInformation(stock);
                assets.Add(asset);
            }

            var etfList = _watchList.ETFs;
            foreach (var etf in etfList)
            {
                var asset = _stockAnalysis.GetETFInformation(etf);
                assets.Add(asset);
            }

            return assets;
        }
    }
}
