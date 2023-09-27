using BreweryAPI.Models;
using BreweryAPI;
using FluentAssertions;
using IntegrationTests.Helpers;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text;

namespace IntegrationTests.Controllers;

public class WholesalerQuote
{
    [Fact]
    public async Task Get_Always_GetWholesalerQuotes()
    {
        string connectionString = "TestGetWholesalerQuotes";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/WholesalerQuote");
        var results = await response.Content.ReadFromJsonAsync<List<WholesalerQuoteModel>>();

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.Count.Should().Be(2);
        results[0].ClientName.Should().Be("TestClient2");
        results[0].WholesalerId.Should().Be(2);
        results[0].BeerId.Should().Be(2);
        results[0].Quantity.Should().Be(10);
        results[0].TotalPrice.Should().Be(30);

        Environment.SetEnvironmentVariable("TEST_ENVIRONMENT", null);
        dbContext.Dispose();
    }

    [Fact]
    public async Task Get_Always_GetWholesalerQuote()
    {
        string connectionString = "TestGetWholesalerQuote";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        var client = _factory.CreateClient();
        int wholesalerQuoteId = 2;

        var response = await client.GetAsync($"/api/WholesalerQuote/{wholesalerQuoteId}");
        var results = await response.Content.ReadFromJsonAsync<WholesalerQuoteModel>();

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.ClientName.Should().Be("TestClient2");
        results.WholesalerId.Should().Be(2);
        results.BeerId.Should().Be(2);
        results.Quantity.Should().Be(10);
        results.TotalPrice.Should().Be(30);

        Environment.SetEnvironmentVariable("TEST_ENVIRONMENT", null);
        dbContext.Dispose();
    }

    [Fact]
    public async Task OnAddWholesalerQuote_WhenExecuteController_ShouldStoreInDb()
    {
        string connectionString = "TestAddWholesalerQuote";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        var client = _factory.CreateClient();
        var newWholesalerQuote = new BreweryAPI.Models.WholesalerQuoteModel()
        {
            ClientName = "TestClient3",
            WholesalerId = 1,
            BeerId = 1,
            Quantity = 1,
            TotalPrice = 1,
        };

        var httpContent = new StringContent(JsonConvert.SerializeObject(newWholesalerQuote), Encoding.UTF8, "application/json");
        var request = await client.PostAsync("/api/WholesalerQuote", httpContent);

        var response = await client.GetAsync("/api/WholesalerQuote");
        var results = await response.Content.ReadFromJsonAsync<List<WholesalerQuoteModel>>();

        request.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.Count.Should().Be(3);
        results[2].ClientName.Should().Be("TestClient3");
        results[2].WholesalerId.Should().Be(1);
        results[2].BeerId.Should().Be(1);
        results[2].Quantity.Should().Be(1);
        results[2].TotalPrice.Should().Be(1);

        Environment.SetEnvironmentVariable("TEST_ENVIRONMENT", null);
        dbContext.Dispose();
    }

    [Fact]
    public async Task OnDeleteWholesalerQuote_WhenExecuteController_ShouldDeleteInDb()
    {
        string connectionString = "TestWholesalerQuoteDelete";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        int wholesalerQuoteId = 2;

        var client = _factory.CreateClient();

        var request = await client.DeleteAsync($"/api/WholesalerQuote/{wholesalerQuoteId}");

        var response = await client.GetAsync("/api/WholesalerQuote");
        var results = await response.Content.ReadFromJsonAsync<List<WholesalerQuoteModel>>();

        request.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results[0].ClientName.Should().Be("TestClient1");
        results[0].WholesalerId.Should().Be(1);
        results[0].BeerId.Should().Be(1);
        results[0].Quantity.Should().Be(10);
        results[0].TotalPrice.Should().Be(10);

        dbContext.Dispose();
    }
}
