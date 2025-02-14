using StockCrawler.Service;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StockPriceUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnGetAssets_Click(object sender, RoutedEventArgs e)
        {
            var _assetsService = new AssetsService();
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
            var _assetsService = new AssetsService();
            try
            {
                btnGetUSAAssets.IsEnabled = false;
                var assets = await Task.Run(() => _assetsService.GetStocksInformation());
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