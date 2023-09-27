using BreweryAPI.DTOs;
using System.Net.Http.Json;

namespace BreweryMaui.Pages;

public partial class WholesallerUserPage : ContentPage
{
	private readonly HttpClient _httpClient;
	private WholesalerDTO _activeWholesalerUser;
    private readonly string _baseUrl = "https://localhost:7256";
    BrewerySalesDTO saleTobeMade = new BrewerySalesDTO();
    WholesalerQuoteDTO quoteToCreate = new WholesalerQuoteDTO();
    BeerDTO beerToOrder;

    public WholesallerUserPage(WholesalerDTO activeWholesalerUser, HttpClient httpClient)
	{
		_httpClient = httpClient;
		_activeWholesalerUser = activeWholesalerUser;
        InitializeComponent();

        tagWholesalerName.Text = "Wholesaler: " + _activeWholesalerUser.WholesalerName;
        tagWholesalerLocation.Text = "Location: " + _activeWholesalerUser.WholesalerLocation;
    }

    //UpdateWholesaler
    private async void OnUpdateWholesaler(object sender, EventArgs e)
    {
        if(updateWholesaleryBtn.Text == "Update Wholesaler")
        {
            UpdateWholesalerNameEntry.IsVisible = true;
            UpdateWholesalerLocationEntry.IsVisible = true;
            updateWholesaleryBtn.Text = "Confirm Update";
        }
        else if(updateWholesaleryBtn.Text == "Confirm Update")
        {
            _activeWholesalerUser.WholesalerName = UpdateWholesalerNameEntry.Text;
            _activeWholesalerUser.WholesalerLocation = UpdateWholesalerLocationEntry.Text;

            if(String.IsNullOrEmpty(_activeWholesalerUser.WholesalerName) || String.IsNullOrEmpty(_activeWholesalerUser.WholesalerName))
            {
                WholesalerWarningLabel.Text = "The name or location of the wholesaler can't be empty!";
            }
            else
            {
                int wholesalerId = _activeWholesalerUser.WholesalerID;
                await _httpClient.PutAsJsonAsync(_baseUrl + $"/api/Wholesaler/{wholesalerId}", _activeWholesalerUser);

                WholesalerWarningLabel.Text = null;
                ResetUpdateWholesalerButtonVisualElements();
                ResetTags();
            }
        }
    }

    //DeleteWholesaler
    private async void OnDeleteWholesaler(object sender, EventArgs e)
    {
        deleteWholesalerBtn.IsVisible = false;
        deleteWholesalerQuestionLabel.IsVisible = true;
        deleteWholesalerYesBtn.IsVisible = true;
        deleteWholesalerNoBtn.IsVisible = true;
    }

    private async void OnWholesalerConfirmDelete(object sender, EventArgs e)
    {
        ResetDeleteWholesalerButtonVisualElements();

        int wholesalerId = _activeWholesalerUser.WholesalerID;
        _httpClient.DeleteAsync(_baseUrl + $"/api/Wholesaler/{wholesalerId}");

        RetreatToMainWholesalerPage();
    }

    private async void OnWholesalerAbortDelete(object sender, EventArgs e)
    {
        ResetDeleteWholesalerButtonVisualElements();
    }

    //Order Shipment of Beer
    private async void OnShowBeersToOrder(object sender, EventArgs e)
    {
        ResetUpdateWholesalerButtonVisualElements();
        ResetDeleteWholesalerButtonVisualElements();
        ResetShowBeerShipmentButtonVisualElements();
        ResetShowInventoryButtonVisualElements();
        ResetShowQuotesButtonVisualElements();
        ResetCreateQuoteButtonVisualElements();


        if (OrderBeerShipment.Text == "Order Shipment of Beer")
        {
            OrderBeerShipment.Text = "Cancel Shipment of Beer";
            List<BeerDTO> allBeers = await _httpClient.GetFromJsonAsync<List<BeerDTO>>(_baseUrl + "/api/Beer");

            AllBeersListHeader.IsVisible = true;
            AllBeersListData.IsVisible = true;
            AllBeersListData.ItemsSource = allBeers;
        }
        else if (OrderBeerShipment.Text == "Cancel Shipment of Beer")
        {
            ResetOrderShipmentButtonVisualElements();
        }
    }

    private async void OnOrderBeer(object sender, EventArgs e)
    {
        beerToOrder = await GetBeerByContext(sender);

        saleTobeMade.BeerId = beerToOrder.BeerId;
        saleTobeMade.WholeSalerId = _activeWholesalerUser.WholesalerID;

        BeerIdOfBeerToOrder.Text = (beerToOrder.BeerId).ToString();
        WolesalerIdOfOrder.Text = (_activeWholesalerUser.WholesalerID).ToString();
        QuantityOfOrder.Text = "Quantity?";
        TotalPriceOfOrder.Text = "?";

        ShipmentToCreateGrid.IsVisible = true;
    }

    private async void AdjustBeerTotalPrice(object sender, EventArgs e)
    {
        int quantity;
        bool quantityIsInt = Int32.TryParse(QuantityOfOrder.Text, out quantity);

        if (quantityIsInt == true && quantity > 0)
        {
            decimal totalPrice = (decimal)quantity * beerToOrder.Price;
            TotalPriceOfOrder.Text = totalPrice.ToString();

            saleTobeMade.Quantity = quantity;
            saleTobeMade.TotalPrice = totalPrice;  //For some reason, the app does not start if i try to put a decimal to TotalPrice. Its very weird.

            WholesalerWarningLabel.Text = null;
            ConfirmOrderButton.IsEnabled = true;
        }
        else
        {
           WholesalerWarningLabel.Text = "Quantity must be a numeric value!";
           ConfirmOrderButton.IsEnabled = false;
        }
    }

    private async void OnConfirmOrder(object sender, EventArgs e)
    {
        await _httpClient.PostAsJsonAsync(_baseUrl + "/api/BrewerySales", saleTobeMade);

        ShipmentToCreateGrid.IsVisible = false;
    }

    private async void OnCancelOrder(object sender, EventArgs e)
    {
        ShipmentToCreateGrid.IsVisible = false;
    }

    //Show Beer Shipments
    private async void OnShowBeersShipments(object sender, EventArgs e)
    {
        ResetUpdateWholesalerButtonVisualElements();
        ResetDeleteWholesalerButtonVisualElements();
        ResetOrderShipmentButtonVisualElements();
        ResetShowInventoryButtonVisualElements();
        ResetShowQuotesButtonVisualElements();
        ResetCreateQuoteButtonVisualElements();

        List<BrewerySalesDTO> allOrders;
        List<BrewerySalesDTO> orders;

        if (ShowBeerShipments.Text == "Show Beer Shipments")
        {
            ShowBeerShipments.Text = "Hide Beer Shipments";
            BeerOrdersListHeader.IsVisible = true;
            BeerOrdersListData.IsVisible = true;

            allOrders = await _httpClient.GetFromJsonAsync<List<BrewerySalesDTO>>(_baseUrl + "/api/BrewerySales");
            orders = allOrders.Where(b => b.WholeSalerId == _activeWholesalerUser.WholesalerID).ToList();

            BeerOrdersListData.ItemsSource = orders;
        }
        else if (ShowBeerShipments.Text == "Hide Beer Shipments")
        {
            ResetShowBeerShipmentButtonVisualElements();
            allOrders = null;
            orders = null;
        }
    }

    //Show Inventory
    private async void OnShowInventory(object sender, EventArgs e)
    {
        ResetUpdateWholesalerButtonVisualElements();
        ResetDeleteWholesalerButtonVisualElements();
        ResetOrderShipmentButtonVisualElements();
        ResetShowBeerShipmentButtonVisualElements();
        ResetShowQuotesButtonVisualElements();
        ResetCreateQuoteButtonVisualElements();

        List<WholesalerInventoryDTO> allInventories;
        List<WholesalerInventoryDTO> inventory;

        if (ShowInventoryBtn.Text == "Show Inventory")
        {
            ShowInventoryBtn.Text = "Hide Inventory";
            WholesalerInventoryHeaderGrid.IsVisible = true;
            WholesalerInventoryDataGrid.IsVisible = true;

            allInventories = await _httpClient.GetFromJsonAsync<List<WholesalerInventoryDTO>>(_baseUrl + "/api/WholesalerInventory");
            inventory = allInventories.Where(i => i.WholesalerId == _activeWholesalerUser.WholesalerID).ToList();

            WholesalerInventoryDataGrid.ItemsSource = inventory;
        }
        else if (ShowInventoryBtn.Text == "Hide Inventory")
        {
            ResetShowInventoryButtonVisualElements();

            allInventories = null;
            inventory = null;
        }
    }

    private async void OnInventoryRecordDelete(object sender, EventArgs e)
    {
        List<WholesalerInventoryDTO> allInventories;
        List<WholesalerInventoryDTO> inventory;

        WholesalerInventoryDTO inventoryRecordToDelete = await GetInventoryRecordByContext(sender);

        int inventoryRecordIdToDelete = inventoryRecordToDelete.ItemId;

        await _httpClient.DeleteAsync(_baseUrl + $"/api/WholesalerInventory/{inventoryRecordIdToDelete}");

        allInventories = await _httpClient.GetFromJsonAsync<List<WholesalerInventoryDTO>>(_baseUrl + "/api/WholesalerInventory");
        inventory = allInventories.Where(i => i.WholesalerId == _activeWholesalerUser.WholesalerID).ToList();

        WholesalerInventoryDataGrid.ItemsSource = inventory;
    }

    private async void OnUpdateInventoryQuantity(object sender, EventArgs e)
    {
        int quantity;

        WholesalerInventoryDTO inventoryRecordToUpdate = await GetInventoryRecordByContext(sender);
        int recordId = inventoryRecordToUpdate.ItemId;

        if (inventoryRecordToUpdate.Quantity > 0)
        {
            await _httpClient.PutAsJsonAsync<WholesalerInventoryDTO>(_baseUrl + $"/api/WholesalerInventory/{recordId}", inventoryRecordToUpdate);
            WholesalerWarningLabel.Text = "";
        }
        else
        {
            WholesalerWarningLabel.Text = $"To update the item {recordId}, quantity must be a positive numeric value!";
        }
    }

    //Show Quotes
    private async void OnShowQuotes(object sender, EventArgs e)
    {
        ResetUpdateWholesalerButtonVisualElements();
        ResetDeleteWholesalerButtonVisualElements();
        ResetShowBeerShipmentButtonVisualElements();
        ResetOrderShipmentButtonVisualElements();
        ResetShowInventoryButtonVisualElements();
        ResetCreateQuoteButtonVisualElements();

        if (ShowQuotesBtn.Text == "Show Quotes")
        {
            ShowQuotesBtn.Text = "Hide Quotes";

            List<WholesalerQuoteDTO> allQuotes = await _httpClient.GetFromJsonAsync<List<WholesalerQuoteDTO>>(_baseUrl + "/api/WholesalerQuote");
            List<WholesalerQuoteDTO> quotes = allQuotes.Where(q => q.WholesalerId == _activeWholesalerUser.WholesalerID).ToList();

            QuoteDataGrid.ItemsSource = quotes;

            QuoteHeaderGrid.IsVisible = true;
            QuoteDataGrid.IsVisible = true;
        }
        else if(ShowQuotesBtn.Text == "Hide Quotes")
        {
            ResetShowQuotesButtonVisualElements();
        }
    }

    private async void OnQuoteDelete(object sender, EventArgs e)
    {
        WholesalerQuoteDTO quoteToDelete = await GetQuoteByContext(sender);

        int quoteId = quoteToDelete.QuoteId;
        await _httpClient.DeleteAsync(_baseUrl + $"/api/WholesalerQuote/{quoteId}");

        List<WholesalerQuoteDTO> allQuotes = await _httpClient.GetFromJsonAsync<List<WholesalerQuoteDTO>>(_baseUrl + "/api/WholesalerQuote");
        List<WholesalerQuoteDTO> quotes = allQuotes.Where(q => q.WholesalerId == _activeWholesalerUser.WholesalerID).ToList();

        QuoteDataGrid.ItemsSource = quotes;
    }

    //Create Quote
    private async void OnCreateQuote(object sender, EventArgs e)
    {
        ResetUpdateWholesalerButtonVisualElements();
        ResetDeleteWholesalerButtonVisualElements();
        ResetOrderShipmentButtonVisualElements();
        ResetShowBeerShipmentButtonVisualElements();
        ResetShowInventoryButtonVisualElements();
        ResetShowQuotesButtonVisualElements();

        List<WholesalerInventoryDTO> allInventories;
        List<WholesalerInventoryDTO> inventory;

        if (CreateQuoteBtn.Text == "Create Quote")
        {
            CreateQuoteBtn.Text = "Cancel creating Quote";

            WholesalerInventoryHeaderGrid.IsVisible = true;
            InventoryForQuoteDataGrid.IsVisible = true;

            allInventories = await _httpClient.GetFromJsonAsync<List<WholesalerInventoryDTO>>(_baseUrl + "/api/WholesalerInventory");
            inventory = allInventories.Where(i => i.WholesalerId == _activeWholesalerUser.WholesalerID).ToList();

            InventoryForQuoteDataGrid.ItemsSource = inventory;
        }
        else if(CreateQuoteBtn.Text == "Cancel creating Quote")
        {
            ResetCreateQuoteButtonVisualElements();
        }
    }

    private async void OnInventoryRecordSelect(object sender, EventArgs e)
    {
        QuoteToCreateGrid.IsVisible = true;

        WholesalerInventoryDTO inventoryRecordSelected = await GetInventoryRecordByContext(sender);
        int beedIdSelected = inventoryRecordSelected.BeerId;
        BeerDTO beerSelected = await _httpClient.GetFromJsonAsync<BeerDTO>(_baseUrl + $"/api/Beer/singular/{beedIdSelected}");

        quoteToCreate.WholesalerId = _activeWholesalerUser.WholesalerID;
        quoteToCreate.BeerId = beedIdSelected;
        quoteToCreate.Quantity = 1;
        quoteToCreate.TotalPrice = beerSelected.Price;

        ClientNameEntry.Text = "";
        WholesalerIdLabel.Text = _activeWholesalerUser.WholesalerID.ToString();
        BeerIdLabel.Text = beerSelected.BeerId.ToString();
        QuantityEntry.Text = "1";
        TotalPriceLabel.Text = beerSelected.Price.ToString();
    }

    private async void OnCancelQuote(object sender, EventArgs e)
    {
        QuoteToCreateGrid.IsVisible = false;

        WholesalerIdLabel.Text = "";
        BeerIdLabel.Text = "";
        TotalPriceLabel.Text = "";
    }

    private async void OnConfirmQuote(object sender, EventArgs e)
    {
        int quantity;
        List<WholesalerInventoryDTO> allInventories;
        List<WholesalerInventoryDTO> inventory;

        bool quantityIsInt = Int32.TryParse(QuantityEntry.Text, out quantity);
        decimal beerPrice = quoteToCreate.TotalPrice;


        if(!String.IsNullOrEmpty(ClientNameEntry.Text) && quantityIsInt == true && quantity > 0)
        {
            quoteToCreate.ClientName = ClientNameEntry.Text;
            quoteToCreate.Quantity = quantity;
            quoteToCreate.TotalPrice = (decimal)quantity * beerPrice;

            await _httpClient.PostAsJsonAsync<WholesalerQuoteDTO>(_baseUrl + "/api/WholesalerQuote", quoteToCreate);

            QuoteToCreateGrid.IsVisible = false;

            allInventories = await _httpClient.GetFromJsonAsync<List<WholesalerInventoryDTO>>(_baseUrl + "/api/WholesalerInventory");
            inventory = allInventories.Where(i => i.WholesalerId == _activeWholesalerUser.WholesalerID).ToList();

            InventoryForQuoteDataGrid.ItemsSource = inventory;

            WholesalerWarningLabel.Text = "";
        }
        else
        {
            WholesalerWarningLabel.Text = "Client name must not be empty and quantity must be a positive number!";
        }
    }

    //Visual elements reset methods
    private void ResetUpdateWholesalerButtonVisualElements()
    {
        UpdateWholesalerNameEntry.IsVisible = false;
        UpdateWholesalerLocationEntry.IsVisible = false;
        updateWholesaleryBtn.Text = "Update Wholesaler";
        WholesalerWarningLabel.Text = null;
    }

    private void ResetDeleteWholesalerButtonVisualElements()
    {
        deleteWholesalerBtn.IsVisible = true;
        deleteWholesalerQuestionLabel.IsVisible = false;
        deleteWholesalerYesBtn.IsVisible = false;
        deleteWholesalerNoBtn.IsVisible = false;
        WholesalerWarningLabel.Text = null;
    }

    private void ResetOrderShipmentButtonVisualElements()
    {
        OrderBeerShipment.Text = "Order Shipment of Beer";
        AllBeersListHeader.IsVisible = false;
        AllBeersListData.IsVisible = false;
        ShipmentToCreateGrid.IsVisible = false;
        WholesalerWarningLabel.Text = null;
    }

    private void ResetShowBeerShipmentButtonVisualElements()
    {
        ShowBeerShipments.Text = "Show Beer Shipments";
        BeerOrdersListHeader.IsVisible = false;
        BeerOrdersListData.IsVisible = false;
        WholesalerWarningLabel.Text = null;
    }

    private void ResetShowInventoryButtonVisualElements()
    {
        ShowInventoryBtn.Text = "Show Inventory";
        WholesalerInventoryHeaderGrid.IsVisible = false;
        WholesalerInventoryDataGrid.IsVisible = false;
        WholesalerWarningLabel.Text = null;
    }

    private void ResetShowQuotesButtonVisualElements()
    {
        ShowQuotesBtn.Text = "Show Quotes";
        QuoteHeaderGrid.IsVisible = false;
        QuoteDataGrid.IsVisible = false;
        WholesalerWarningLabel.Text = null;
    }

    private void ResetCreateQuoteButtonVisualElements()
    {
        CreateQuoteBtn.Text = "Create Quote";
        WholesalerInventoryHeaderGrid.IsVisible = false;
        InventoryForQuoteDataGrid.IsVisible = false;
        QuoteToCreateGrid.IsVisible= false;
        WholesalerWarningLabel.Text = null;
    }

    private void ResetTags()
    {
        tagWholesalerName.Text = "Wholesaler: " + _activeWholesalerUser.WholesalerName;
        tagWholesalerLocation.Text = "Location: " + _activeWholesalerUser.WholesalerLocation;
        UpdateWholesalerNameEntry.Text = "Wholesaler Name";
        UpdateWholesalerLocationEntry.Text = "Wholesaler Location";
    }

    //Get Context methods
    private async Task<WholesalerInventoryDTO> GetInventoryRecordByContext(object sender)
    {
        WholesalerInventoryDTO inventoryRecordToUpdate = null;

        if (sender is Entry entry)
        {
            if (entry.BindingContext is WholesalerInventoryDTO record)
            {
                inventoryRecordToUpdate = record;
            }
        }

        if (sender is Button button)
        {
            if (button.BindingContext is WholesalerInventoryDTO record)
            {
                inventoryRecordToUpdate = record;
            }
        }

        return inventoryRecordToUpdate;
    }

    private async Task<WholesalerQuoteDTO> GetQuoteByContext(object sender)
    {
        WholesalerQuoteDTO quoteToUpdate = null;

        if (sender is Entry entry)
        {
            if (entry.BindingContext is WholesalerQuoteDTO quote)
            {
                quoteToUpdate = quote;
            }
        }

        if (sender is Button button)
        {
            if (button.BindingContext is WholesalerQuoteDTO quote)
            {
                quoteToUpdate = quote;
            }
        }

        return quoteToUpdate;
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

    //navigation methods
    private async void OnLogOut(object sender, EventArgs e)
    {
        WholesalerWarningLabel.Text = null;
        Navigation.PushAsync(new MainPage());
    }

    private void RetreatToMainWholesalerPage()
    {
        Navigation.PushAsync(new WholesalerMainPage(_httpClient));
    }
}