namespace StockCrawler.Domain
{
    public class Asset
    {
        public string Ticker { get; set; } = string.Empty;
        public decimal Price { get; set; }

        private decimal _dividend;
        public decimal Dividend
        {
            get => _dividend;
            set => _dividend = Math.Round(value, 4);
        }

        public Type Type { get; set; }
    }

    public enum Type
    {
        Acao = 0,
        FII,
        Stock,
        REIT,
        ETF
    }
}
