using StockCrawler.Domain;
using StockCrawler.Service.StockExtractor;

namespace StockCrawler.Service
{
    public class AssetsService
    {
        private readonly WatchList _watchList;
        private readonly FundamentusCrawler _webCrawler;

        public AssetsService()
        {
            _watchList = new WatchList();
            _webCrawler = new FundamentusCrawler();
        }

        public List<Asset> GetWalletAssets()
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
    }
}
