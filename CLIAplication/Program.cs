// See https://aka.ms/new-console-template for more information
using StockCrawler.Service.StockExtractor;

Console.WriteLine("Hello, World!");

FundamentusCrawler fundamentusCrawler = new FundamentusCrawler();

var assets = fundamentusCrawler.GetAcoes();

Console.WriteLine("Resultado: ");

foreach (var asset in assets)
{
    Console.WriteLine($"{asset.Ticker} -> R$ {asset.Price}.");
}