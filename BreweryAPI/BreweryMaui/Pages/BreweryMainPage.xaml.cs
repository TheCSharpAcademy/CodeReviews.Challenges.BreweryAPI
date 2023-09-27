using BreweryAPI.DTOs;
using BreweryMaui.Pages;
using System.Net.Http.Json;

namespace BreweryMaui;

public partial class BreweryMainPage : ContentPage
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://localhost:7256";
    public BreweryMainPage(HttpClient httpClient)
	{
        _httpClient = httpClient;

        InitializeComponent();
    }

    private async void ResetLabels()
    {
        CreateBreweryNameInput.Text = "Brewery Name";
        CreateBreweryLocationInput.Text = "BreweryLocation";
        SelectBreweryByNameInput.Text = "Brewery Name";
        BreweryWarningLabel.Text = string.Empty;

        BreweriesListView.IsVisible = false;
        ShowBreweriesButton.Text = "Show breweries List";
    }

    private async void OnShowBreweries(object sender, EventArgs e)
    {
        List<BreweryDTO> breweries = await GetAllBreweries();

        if (BreweriesListView.IsVisible == false)
        {
            BreweriesListView.IsVisible = true;
            ShowBreweriesButton.Text = "Hide breweries List";
        }
        else
        {
            BreweriesListView.IsVisible = false;
            ShowBreweriesButton.Text = "Show breweries List";
        }
        BreweriesListView.ItemsSource = breweries;
    }

    private async Task<List<BreweryDTO>> GetAllBreweries()
	{
       List<BreweryDTO> breweries = null;

       try
        {
            breweries = await _httpClient.GetFromJsonAsync<List<BreweryDTO>>(_baseUrl + "/api/Brewery");
        }
        catch
        {
            BreweryWarningLabel.Text = "Error ocorred! Check connection to BreweryAPI!";
        }

		return breweries;
    }

	private async Task<bool> BreweryExists(string breweryName)
	{
		bool breweryExists = false;
		var breweries = await GetAllBreweries();

        if(breweries != null)
        {
            foreach (var brewery in breweries)
            {
                if (breweryName.Trim().ToLower() == brewery.BreweryName.Trim().ToLower())
                {
                    breweryExists = true;
                }
            }
        }

		return breweryExists;
	}

	private async Task<BreweryDTO> SelectActiveUserBrewery(string breweryName)
	{
		BreweryDTO activeBrewery = null;

        var breweries = await GetAllBreweries();

        foreach (var brewery in breweries)
        {
            if (breweryName.Trim().ToLower() == brewery.BreweryName.Trim().ToLower())
            {
                activeBrewery = brewery;
            }
        }

        return activeBrewery;
    }

	private async void OnBreweryCreate(object sender, EventArgs e)
	{
		string breweryName = CreateBreweryNameInput.Text;
		string breweryLocation = CreateBreweryLocationInput.Text;

		bool breweryExists = await BreweryExists(breweryName);

        if (breweryExists == false) 
		{
            if (!String.IsNullOrEmpty(breweryLocation) && !String.IsNullOrEmpty(breweryName))
            {
                try
                {
                    var breweryToCreate = new BreweryDTO { BreweryName = breweryName, BreweryLocation = breweryLocation };
                    await _httpClient.PostAsJsonAsync(_baseUrl + "/api/Brewery", breweryToCreate);

                    Thread.Sleep(1000);
                    BreweryWarningLabel.Text = "Brewery succesfuly registered";


                    BreweryDTO activeUserBrewery = await SelectActiveUserBrewery(breweryName);
                    GoToBreweryUserPage(activeUserBrewery);
                }
                catch
                {
                    BreweryWarningLabel.Text = "Error ocorred while registering new brewery! Check connection to BreweryAPI!";
                }

            }
            else
            {
                BreweryWarningLabel.Text = "Brewery name and brewery location cannot be empty!";
            }
        }
		else
		{
            BreweryWarningLabel.Text = "Failed to register new brewery. Brewery name already exists!";
        }
    }

	private async void OnBrewerySelect(object sender, EventArgs e)
	{
		string breweryName = SelectBreweryByNameInput.Text;

        bool breweryExists = await BreweryExists(breweryName);

        if (breweryExists == false)
        {
            BreweryWarningLabel.Text = "Failed to select brewery. Brewery name does not exist or bad connection to BreweryAPI!";
        }
        else
        {
            try 
            {
                BreweryWarningLabel.Text = "Brewery succesfuly selected";

                BreweryDTO activeUserBrewery = await SelectActiveUserBrewery(breweryName);
                GoToBreweryUserPage(activeUserBrewery);
            }
            catch
            {
                BreweryWarningLabel.Text = "Failed to select a brewery. Check connection to BreweryAPI!";
            }
        }
    }

    private void GoToBreweryUserPage(BreweryDTO activeUserBrewery)
    {
        Navigation.PushAsync(new BreweryUserPage(activeUserBrewery, _httpClient));
        ResetLabels();
    }
}