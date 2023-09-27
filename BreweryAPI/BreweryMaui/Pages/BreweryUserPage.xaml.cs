using BreweryAPI.DTOs;
using System.Net.Http.Json;

namespace BreweryMaui.Pages;

public partial class BreweryUserPage : ContentPage
{
	private readonly HttpClient _httpClient;
	private BreweryDTO _activeUserBrewery;
    private readonly string _baseUrl = "https://localhost:7256";
 

    public BreweryUserPage(BreweryDTO activeUserBrewery, HttpClient httpClient)
	{
		InitializeComponent();
		_httpClient = httpClient;
		_activeUserBrewery = activeUserBrewery;

        DefineHeadlineBreweryLabels();
	}

	public async void DefineHeadlineBreweryLabels()
	{
		tagBreweryName.Text = "Brewery Name: " + _activeUserBrewery.BreweryName;
		tagBreweryLocation.Text = "Brewery Location: " + _activeUserBrewery.BreweryLocation;
    }

	public async void OnBreweryUpdate(object sender, EventArgs e)
	{
        ResetCreateBeerVisualElements();
        ResetDeleteBreweryVisualElements();
        ResetShowBeersVisualElements();

        if (updateBreweryBtn.Text == "Update Brewery")
		{
            UpdateBreweryNameInput.IsVisible = true;
            UpdateBreweryLocationInput.IsVisible = true;
			updateBreweryBtn.Text = "Submit to update";
        }
		else if(updateBreweryBtn.Text == "Submit to update")
		{
			if(!String.IsNullOrEmpty(UpdateBreweryNameInput.Text) && !String.IsNullOrEmpty(UpdateBreweryLocationInput.Text))
			{
                int breweryId = _activeUserBrewery.BreweryId;
                BreweryDTO breweryToUpdate = await ConstructBreweryToUpdate();

                await _httpClient.PutAsJsonAsync(_baseUrl + $"/api/Brewery/{breweryId}", breweryToUpdate);

                ResetUpdateBreweryVisualElements();

                _activeUserBrewery = breweryToUpdate;
                DefineHeadlineBreweryLabels();
                BreweryWarningLabel.Text = null;
            }
            else
            {
                BreweryWarningLabel.Text = "Failed to update brewery. Check connection to breweryAPI";
            }
        }
    }

    public async void OnBreweryDelete(object sender, EventArgs e)
    {
        ResetUpdateBreweryVisualElements();
        ResetCreateBeerVisualElements();
        ResetShowBeersVisualElements();


        if (deleteBreweryBtn.IsVisible == true)
        {
            deleteBreweryBtn.IsVisible = false;
            deleteBreweryQuestion.IsVisible = true;
            deleteBreweryYesBtn.IsVisible = true;
            deleteBreweryNoBtn.IsVisible = true;
        }
    }

    public async void OnBreweryAbortDelete(object sender, EventArgs e)
    {
        ResetDeleteBreweryVisualElements();
    }

    public async void OnBreweryConfirmDelete(object sender, EventArgs e)
    {
        int breweryId = _activeUserBrewery.BreweryId;

        try
        {
            await _httpClient.DeleteAsync(_baseUrl + $"/api/Brewery/{breweryId}");
            RetreatToMainBreweryPage();

            ResetDeleteBreweryVisualElements();
        }
        catch
        {
            BreweryWarningLabel.Text = "Unable to delete brewery.Check connection to breweryAPI!";
        }
    }

    public async void OnShowBeers(object sender, EventArgs e)
    {
        if(showBeers.Text == "Show Beers")
        {
            ResetUpdateBreweryVisualElements();
            ResetDeleteBreweryVisualElements();

            showBeers.Text = "Hide Beers";
            beersListHeader.IsVisible = true;

            beersListData.ItemsSource = await GetBeersByBreweryId();
            beersListData.IsVisible = true;
        }
        else if(showBeers.Text == "Hide Beers")
        {
            ResetCreateBeerVisualElements();
            ResetShowBeersVisualElements();
        }
    }

    private async void OnEntryChanged(object sender, EventArgs e)
    {
        BeerDTO beerToUpdate = await GetBeerByContext(sender);
        decimal price;
        string priceString = beerToUpdate.Price.ToString();
        bool isDecimal = decimal.TryParse(priceString, out price);


        if(!string.IsNullOrEmpty(beerToUpdate.BeerName) && isDecimal == true && price > 0)
        {
            int beerId = beerToUpdate.BeerId;

            await _httpClient.PutAsJsonAsync(_baseUrl + $"/api/Beer/{beerId}", beerToUpdate);
            BreweryWarningLabel.Text = null;
        }
        else 
        {
            BreweryWarningLabel.Text = "To update record beer name can´t be empty and price must be a positive number!";
        }
    }

    private async void OnCreateBeer(object sender, EventArgs e)
    {
        if(CreateBeerBtn.Text == "Create a new beer")
        {
            ResetUpdateBreweryVisualElements();
            ResetDeleteBreweryVisualElements();

            CreateBeerBtn.Text = "Confirm";

            BeerNameInput.IsVisible = true;
            BeerPriceInput.IsVisible = true;
        }
        else if(CreateBeerBtn.Text == "Confirm")
        {
            string beerName = BeerNameInput.Text;
            decimal price;
            bool isDecimal = decimal.TryParse(BeerPriceInput.Text, out price);

            if (!string.IsNullOrEmpty(beerName) && !string.IsNullOrEmpty(BeerPriceInput.Text) && isDecimal == true)
            {
                var beerToCreate = new BeerDTO {BeerName = beerName, Price = price, BreweryId = _activeUserBrewery.BreweryId };
                await _httpClient.PostAsJsonAsync(_baseUrl + "/api/Beer", beerToCreate);

                ResetCreateBeerVisualElements();

                if (showBeers.Text == "Hide Beers")
                {
                    beersListData.ItemsSource = await GetBeersByBreweryId();
                }
            }
            else
            {
                BreweryWarningLabel.Text = "Beer name and beer price must not be empty. And beer price must be numeric!";
            }
        }
    }

    private async void OnDeleteBeer(object sender, EventArgs e)
    {
        BeerDTO beerToDelete = await GetBeerByContext(sender);
        int beerId = beerToDelete.BeerId;

        await _httpClient.DeleteAsync(_baseUrl + $"/api/Beer/{beerId}");
        beersListData.ItemsSource = await GetBeersByBreweryId();
    }

    private async Task<List<BeerDTO>> GetBeersByBreweryId()
    {
       List<BeerDTO> beersByBrewery = null;

        try
        {
            int breweryId = _activeUserBrewery.BreweryId;
            beersByBrewery = await _httpClient.GetFromJsonAsync<List<BeerDTO>>(_baseUrl + $"/api/Beer/{breweryId}");
        }
        catch 
        {
            BreweryWarningLabel.Text = "Error getting beers list. Check connection to breweryAPI!";
        }

        return beersByBrewery;
    }

    private void RetreatToMainBreweryPage()
    {
        BreweryWarningLabel.Text = null;
        Navigation.PushAsync(new BreweryMainPage(_httpClient));
    }

    private void ResetUpdateBreweryVisualElements()
    {
        BreweryWarningLabel.Text = null;
        updateBreweryBtn.Text = "Update Brewery";
        UpdateBreweryNameInput.IsVisible = false;
        UpdateBreweryNameInput.Text = "Brewery Name";
        UpdateBreweryLocationInput.IsVisible = false;
        UpdateBreweryLocationInput.Text = "Brewery Location";
    }

    private void ResetDeleteBreweryVisualElements()
    {
        BreweryWarningLabel.Text = null;
        deleteBreweryBtn.IsVisible = true;
        deleteBreweryQuestion.IsVisible = false;
        deleteBreweryYesBtn.IsVisible = false;
        deleteBreweryNoBtn.IsVisible = false;
    }

    private void ResetCreateBeerVisualElements()
    {
        BreweryWarningLabel.Text = null;
        CreateBeerBtn.Text = "Create a new beer";
        BeerNameInput.IsVisible = false;
        BeerNameInput.Text = "New Beer Name";
        BeerPriceInput.IsVisible = false;
        BeerPriceInput.Text = "New beer Price";
    }

    private void ResetShowBeersVisualElements()
    {
        BreweryWarningLabel.Text = null;
        showBeers.Text = "Show Beers";
        beersListHeader.IsVisible = false;
        beersListData.IsVisible = false;
    }

    private async void OnLogOut(object sender, EventArgs e)
    {
        BreweryWarningLabel.Text = null;
        Navigation.PushAsync(new MainPage());
    }

    public async Task<BreweryDTO> ConstructBreweryToUpdate()
	{
        BreweryDTO breweryToUpdate = new BreweryDTO
        {
            BreweryId = _activeUserBrewery.BreweryId,
            BreweryName = UpdateBreweryNameInput.Text,
            BreweryLocation = UpdateBreweryLocationInput.Text
        };

		return breweryToUpdate;
    }

    private async Task<BeerDTO> GetBeerByContext(object sender)
    {
        BeerDTO beerToUpdate = null;

        if (sender is Entry entry)
        {
            if (entry.BindingContext is BeerDTO beer)
            {
                beerToUpdate = beer;
            }
        }

        if (sender is Button button)
        {
            if (button.BindingContext is BeerDTO beer)
            {
                beerToUpdate = beer;
            }
        }

        return beerToUpdate;
    }
}