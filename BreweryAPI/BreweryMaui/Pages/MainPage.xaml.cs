using BreweryMaui.Pages;

namespace BreweryMaui
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public MainPage() 
        { 
            InitializeComponent();
        }

        private void OnBreweryPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BreweryMainPage(_httpClient));
        }

        private void OnWholesalerPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WholesalerMainPage(_httpClient));
        }
    }
}