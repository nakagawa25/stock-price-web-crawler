namespace StockCrawler.Domain
{
    public class WatchList
    {
        public WatchList()
        {
            Acoes = new List<string>()
            {
            };

            FIIs = new List<string>()
            {
            };

            Stocks = new List<string>()
            {
            };

            ETFs = new List<string>()
            {
            };
        }

        public List<string> Acoes { get; private set; }
        public List<string> FIIs { get; private set; }
        public List<string> Stocks { get; private set; }
        public List<string> ETFs { get; private set; }
    }
}
