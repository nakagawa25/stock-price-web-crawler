using StockCrawler.Service;
using System.Windows;

namespace Graphic.Interface
{
    /// <summary>
    /// Interaction logic for StockInfo.xaml
    /// </summary>
    public partial class StockInfo : Window
    {
        private readonly AssetsService _assetsService;

        public StockInfo()
        {
            InitializeComponent();
            _assetsService = new AssetsService();
        }

        private async void btnGetAssets_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnGetAssets.IsEnabled = false;
                var assets = await Task.Run(() => _assetsService.GetBrazilianWalletAssets());
                dgAssets.ItemsSource = assets;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnGetAssets.IsEnabled = true;
            }
        }

        private async void btnGetUSAAssets_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnGetUSAAssets.IsEnabled = false;
                var assets = await Task.Run(() => _assetsService.GetUSAWalletAssets());
                dgAssets.ItemsSource = assets;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnGetUSAAssets.IsEnabled = true;
            }
        }
    }
}
