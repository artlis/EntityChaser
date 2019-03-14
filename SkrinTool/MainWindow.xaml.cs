using Newtonsoft.Json;
using SkrinTool.ApiSkrin;
using SkrinTool.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAnimatedGif;

namespace SkrinTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly int _imageAmount = 50; 
        private static List<BitmapImage> _images { get; set; } = new List<BitmapImage>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {
            Loader.Visibility = Visibility.Hidden;
            DiclaimerTbx.Visibility = Visibility.Hidden;
            await LoadImages();
        }

        private async Task LoadImages()
        {
            var result = await new GiphyService().Search("waiting", _imageAmount);

            var urls = result.data
                .Select(x => x.Images.fixed_height_small.Url)
                .ToList();

            urls.ForEach(x => _images.Add(new BitmapImage(new Uri(x))));
        }

        private async void ReportBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowRandomLoader();
            ResultViewTbx.Text = 
                await GetGompanyReportAsync(userID: "Your_Company_Code", ogrn: OgrnTbx.Text, inn: InnTbx.Text, okpo: OkpoTbx.Text, isMain: 1);
            HideLoader();
        }

        private void ShowRandomLoader()
        {
            var r = new Random();
            var rInt = r.Next(0, _imageAmount); //for ints
            var _imageSource = _images[rInt];

            var image = _imageSource;
            ImageBehavior.SetAnimatedSource(Loader, image);

            DiclaimerTbx.Visibility = Visibility.Visible;
            Loader.Visibility = Visibility.Visible;
        }

        private void HideLoader()
        {
            DiclaimerTbx.Visibility = Visibility.Hidden;
            Loader.Visibility = Visibility.Hidden;
        }

        private async void StructureBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowRandomLoader();
            ResultViewTbx.Text =
                await GetCompanyStructureAsync(userID: "Your_Company_Code", ogrn: OgrnTbx.Text, inn: InnTbx.Text, okpo: OkpoTbx.Text);
            HideLoader();
        }

        private async void HistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowRandomLoader();
            ResultViewTbx.Text =
                await GetCompanyCoownersHistoryAsync(userID: "Your_Company_Code", ogrn: OgrnTbx.Text, inn: InnTbx.Text, okpo: OkpoTbx.Text);
            HideLoader();
        }

        private void CopyBtn_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(ResultViewTbx.Text);
        }

        public async Task<string> GetGompanyReportAsync(string userID, string ogrn, string inn, string okpo, int isMain)
        {
            using (var client = new ApiSkrinClient())
            {
                var response = await client.GetCompanyReportAsync(userID, ogrn, inn, okpo, isMain);
                return JsonConvert.SerializeObject(response);
            }
        }

        public async Task<string> GetCompanyStructureAsync(string userID, string ogrn, string inn, string okpo)
        {
            using (var client = new ApiSkrinClient())
            {
                var response = await client.GetCompanyStructureAsync(userID, ogrn, inn, okpo);
                return JsonConvert.SerializeObject(response);
            }
        }

        public async Task<string> GetCompanyCoownersHistoryAsync(string userID, string ogrn, string inn, string okpo)
        {
            using (var client = new ApiSkrinClient())
            {
                var response = await client.GetCompanyCoownersHistoryAsync(userID, ogrn, inn, okpo);
                return JsonConvert.SerializeObject(response);
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }


    }
}
