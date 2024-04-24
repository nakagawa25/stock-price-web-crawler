namespace StockCrawler.Domain
{
    public class Asset
    {
        public string Ticker { get; set; }
        public decimal Price { get; set; }
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
