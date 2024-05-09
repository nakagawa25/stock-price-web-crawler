# Stocks Web Crawler 📈

![GitHub repo size](https://img.shields.io/github/repo-size/nakagawa25/stock-price-web-crawler?style=for-the-badge)
![GitHub language count](https://img.shields.io/github/languages/count/nakagawa25/stock-price-web-crawler?style=for-the-badge)
![GitHub forks](https://img.shields.io/github/forks/nakagawa25/stock-price-web-crawler?style=for-the-badge)
![GitHub contributors](https://img.shields.io/github/contributors/nakagawa25/stock-price-web-crawler?style=for-the-badge)
![GitHub issues](https://img.shields.io/github/issues/nakagawa25/stock-price-web-crawler?style=for-the-badge)

> This is a Web Crawler for extract USA stocks, REITs, ETFs, brazilian stocks and FIIs (similar a REIT) price, negotiation ticker, future dividends prevision and asset informations.

<p>
  <img src="https://user-images.githubusercontent.com/53760877/138791241-50a815be-dbd6-4a0e-b62c-68f3b28d155b.png" alt=".NET" />
  <img src="https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white" alt="C#" />
  <img src="https://camo.githubusercontent.com/f22051667b1f035a16e2c17c0ec3109f56a971a91d8acf84f61d5e163696b35e/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f2d73656c656e69756d2d253433423032413f7374796c653d666f722d7468652d6261646765266c6f676f3d73656c656e69756d266c6f676f436f6c6f723d7768697465" alt="Selenium"/>
  <img src="https://camo.githubusercontent.com/22c9aa77ff4eda4401f010263321f196535dd1c8894f8ec57ddb2abbffbd90e4/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f476f6f676c652532304368726f6d652d3432383546343f7374796c653d666f722d7468652d6261646765266c6f676f3d476f6f676c654368726f6d65266c6f676f436f6c6f723d7768697465" alt="Chrome"/>
  <img src="https://camo.githubusercontent.com/c8af3418f8fe508aed1c66f474b50f9e9d8f64db648d1bd947527b35b6243a99/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f636861744750542d3734616139633f7374796c653d666f722d7468652d6261646765266c6f676f3d6f70656e6169266c6f676f436f6c6f723d7768697465" alt="ChatGPT"/>
</p>

## :book: About
This is an application that uses web scrapping techniques to extract data about stocks from web pages 🌐, extracting price data, dividends, and other pertinent information.

To extract data from Brazilian assets, such as FIIs and Brazilian shares, I used the Fundamentus platform. For Stocks and REITs, I used Yahoo Finance and for ETFs, I used StockAnalysis.

## 🖥️ Development
Selenium 🤖 was used with the Google Chrome driver to access the web pages and extract data from the assets.
There is a project in WPF, called "Graphic.Interface" where there are 2 buttons, to run the program and obtain the extraction result. But in the future, this application will be an API. 🖥️

## ⚒️ Technologies
- [C#](https://docs.microsoft.com/pt-br/dotnet/csharp/)
- [.NET 8](https://learn.microsoft.com/pt-br/dotnet/core/whats-new/dotnet-8/overview)
- [WPF](https://learn.microsoft.com/pt-br/dotnet/desktop/wpf/overview/?view=netdesktop-8.0)
- [Selenium](https://www.selenium.dev/)
- [ChatGPT](https://chat.openai.com/)

## 🚀 Running the Application

- Clone this repository with `git clone https://github.com/nakagawa25/stock-price-web-crawler.git`
- Open Windows PowerShell in repository root directory
- Build the project with `dotnet build .\CLIApplication\`
- Start the application with `.\CLIApplication\bin\Debug\net8.0\CLIApplication.exe`

## 📝 How to use

- After running the application, click the button "Obter cotações atuais" to retrieve Brazilian stock information.
- Please wait a few seconds to view the results in the DataGrid.
- Click the button "Get Stock Price" to retrieve information on USA shares.
- Please wait a few seconds to view the results in the DataGrid.

## 🥾 Next Steps

Project progress.

- [x] Get Brazilian Stocks and FIIs informations.
- [x] Get USA Stocks and REITs informations.
- [x] Get USA ETFs informations.
- [x] Show results in a DataGrid
- [ ] Get more informations like: Sector, Debits, NET Value and others.
- [ ] Make this project an Async API.

