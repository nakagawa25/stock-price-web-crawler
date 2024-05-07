using StockCrawler.Domain;
using StockCrawler.Service.StockExtractor;

namespace StockCrawler.Service
{
    public class AssetsService
    {
        private readonly WatchList _watchList;
        private readonly FundamentusCrawler _webCrawler;
        private readonly YahooFinanceCrawler _marketWatchCrawler;

        public AssetsService()
        {
            _watchList = new WatchList();
            _webCrawler = new FundamentusCrawler();
            _marketWatchCrawler = new YahooFinanceCrawler();
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
            var allStocks = _marketWatchCrawler.GetStocks();

            var walletAssets = allStocks
                .Where(x => watchList.Contains(x.Ticker))
                .OrderBy(x => watchList.IndexOf(x.Ticker))
                .ToList();

            //return walletAssets;
            return allStocks;
        }
    }
}
