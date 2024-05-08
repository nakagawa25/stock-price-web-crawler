namespace StockCrawler.Domain
{
    public class AssetInformation
    {
        public string Ticker { get; set; }
        public decimal Price {  get; set; }
        public decimal Dividend { get; set; }
    }
}
