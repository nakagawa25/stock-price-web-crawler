namespace StockCrawler.Domain
{
    public class WatchList
    {
        public WatchList()
        {
            Acoes = new List<string>()
            {
                "EGIE3",
                "TAEE3",
                "EQTL3",
                "ITUB3",
                "BBDC3",
                "BBSE3",
                "PSSA3",
                "VIVT3",
                "CAML3",
                "ABEV3",
                "FLRY3",
                "RDOR3",
                "WEGE3",
                "CSAN3",
                "SAPR3",
                "SLCE3",
                "RADL3",
                "LREN3",
                "VALE3",
                "PETR3"
            };

            FIIs = new List<string>()
            {
                "BCFF11",
                "HFOF11",
                "CPTS11",
                "IRDM11",
                "HGBS11",
                "XPML11",
                "VISC11",
                "HSML11",
                "HGLG11",
                "XPLG11",
                "VILG11",
                "BTLG11",
                "KNRI11",
                "HGRE11",
                "ALZR11",
                "HGRU11",
                "RBVA11",
                "BTAL11",
                "RZTR11"
            };
        }

        public List<string> Acoes { get; private set; }
        public List<string> FIIs { get; private set; }
    }
}
