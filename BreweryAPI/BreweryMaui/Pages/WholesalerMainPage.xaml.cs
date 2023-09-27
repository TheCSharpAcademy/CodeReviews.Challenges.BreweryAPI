using BreweryAPI.DTOs;
using System.Net.Http.Json;

namespace BreweryMaui.Pages;

public partial class WholesalerMainPage : ContentPage
{
	private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://localhost:7256";

    public WholesalerMainPage(HttpClient httpClient)
	{
		_httpClient = httpClient;
		InitializeComponent();
	}

    private async void OnWholesalerCreate(object sender, EventArgs e)
    {
        string wholesalerName = CreateWholesalerNameInput.Text;
        string wholesalerLocation = CreateWholesalerLocationInput.Text;

        bool breweryExists = await WholesalerExists(wholesalerName);

        if (breweryExists == false)
        {
            if (!String.IsNullOrEmpty(wholesalerLocation) && !String.IsNullOrEmpty(wholesalerName))
            {
                try
                {
                    var breweryToCreate = new WholesalerDTO { WholesalerName = wholesalerName, WholesalerLocation = wholesalerLocation };
                    await _httpClient.PostAsJsonAsync(_baseUrl + "/api/Wholesaler", breweryToCreate);

                    Thread.Sleep(1000);
                    WholesalerWarningLabel.Text = "Wholesaler succesfuly registered";

                    WholesalerDTO activeUserWholesaler = await SelectActiveUserWholesaler(wholesalerName);
                    GoToWholesalerUserPage(activeUserWholesaler);
                }
                catch
                {
                    WholesalerWarningLabel.Text = "Error ocorred while registering new brewery! Check connection to WholesalerAPI!";
                }
            }
            else
            {
                WholesalerWarningLabel.Text = "Wholesaler location cannot be empty!";
            }
        }
        else
        {
            WholesalerWarningLabel.Text = "Failed to register new brewery. Wholesaler name already exists!";
        }
    }

    private async void OnWholesalerSelect(object sender, EventArgs e)
    {
        string breweryName = SelectWholesalerByNameInput.Text;

        bool breweryExists = await WholesalerExists(breweryName);

        if (breweryExists == false)
        {
            WholesalerWarningLabel.Text = "Failed to select brewery. Wholesaler name does not exist or bad connection to WholesalerAPI!";
        }
        else
        {
            try
            {
                WholesalerWarningLabel.Text = "Wholesaler succesfuly selected";

                WholesalerDTO activeUserWholesaler = await SelectActiveUserWholesaler(breweryName);
                GoToWholesalerUserPage(activeUserWholesaler);
            }
            catch
            {
                WholesalerWarningLabel.Text = "Failed to select a brewery. Check connection to WholesalerAPI!";
            }
        }
    }

    private async void OnShowWholesalers(object sender, EventArgs e)
    {
        List<WholesalerDTO> breweries = await GetAllWholesalers();

        if (WholesalersListView.IsVisible == false)
        {
            WholesalersListView.IsVisible = true;
            ShowWholesalersButton.Text = "Hide wholesalers List";
        }
        else
        {
            WholesalersListView.IsVisible = false;
            ShowWholesalersButton.Text = "Show wholesalers List";
        }

        WholesalersListView.ItemsSource = breweries;
    }

    private async Task<List<WholesalerDTO>> GetAllWholesalers()
    {
        List<WholesalerDTO> wholesalers = null;

        try
        {
            wholesalers = await _httpClient.GetFromJsonAsync<List<WholesalerDTO>>(_baseUrl + "/api/Wholesaler");
        }
        catch 
        {
            WholesalerWarningLabel.Text = "Error ocorred! Check connection to WholesalerAPI!";
        }

        return wholesalers;
    }

    private async Task<bool> WholesalerExists(string wholesalerName)
    {
        bool wholesalerExists = false;
        var wholesalers = await GetAllWholesalers();

        if (wholesalers != null)
        {
            foreach (var wholesaler in wholesalers)
            {
                if (wholesalerName.Trim().ToLower() == wholesaler.WholesalerName.Trim().ToLower())
                {
                    wholesalerExists = true;
                }
            }
        }

        return wholesalerExists;
    }

    private async Task<WholesalerDTO> SelectActiveUserWholesaler(string breweryName)
    {
        WholesalerDTO activeWholesaler = null;

        var breweries = await GetAllWholesalers();

        foreach (var brewery in breweries)
        {
            if (breweryName.Trim().ToLower() == brewery.WholesalerName.Trim().ToLower())
            {
                activeWholesaler = brewery;
            }
        }

        return activeWholesaler;
    }

    private async void ResetLabels()
    {
        CreateWholesalerNameInput.Text = "Wholesaler Name";
        CreateWholesalerLocationInput.Text = "WholesalerLocation";
        SelectWholesalerByNameInput.Text = "Wholesaler Name";
        WholesalerWarningLabel.Text = string.Empty;

        WholesalersListView.IsVisible = false;
        ShowWholesalersButton.Text = "Show Wholesalers List";
    }

    private void GoToWholesalerUserPage(WholesalerDTO activeUserWholesaler)
    {
        Navigation.PushAsync(new WholesallerUserPage(activeUserWholesaler, _httpClient));
        ResetLabels();
    }
}