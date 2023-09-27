using BreweryAPI.Models;
using BreweryAPI;
using FluentAssertions;
using IntegrationTests.Helpers;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text;

namespace IntegrationTests.Controllers;

public class BeerTests
{
    [Fact]
    public async Task Get_Always_GetBeers()
    {
        string connectionString = "TestGetBeers";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/Beer");
        var results = await response.Content.ReadFromJsonAsync<List<BeerModel>>();

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.Count.Should().Be(5);
        results[0].BeerName.Should().Be("TestBeer1");
        results[0].Price.Should().Be(1);
        results[0].BreweryId.Should().Be(1);

        dbContext.Dispose();
    }

    [Fact]
    public async Task Get_Always_GetBeer()
    {
        string connectionString = "TestGetSingBeer";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        var client = _factory.CreateClient();
        int beerId = 1;

        var response = await client.GetAsync($"/api/Beer/Singular/{beerId}");
        var results = await response.Content.ReadFromJsonAsync<BeerModel>();

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.BeerName.Should().Be("TestBeer2");
        results.Price.Should().Be(2);
        results.BreweryId.Should().Be(1);

        dbContext.Dispose();
    }

    [Fact]
    public async Task Get_Always_GetBeersByBrewery()
    {
        string connectionString = "TestGetBeersByBrewery";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        var client = _factory.CreateClient();
        int breweryId = 1;

        var response = await client.GetAsync($"/api/Beer/{breweryId}");
        var results = await response.Content.ReadFromJsonAsync<List<BeerModel>>();

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.Count.Should().Be(3);
        results[1].BeerName.Should().Be("TestBeer2");
        results[1].Price.Should().Be(2);
        results[1].BreweryId.Should().Be(1);

        dbContext.Dispose();
    }

    [Fact]
    public async Task OnAddBeer_WhenExecuteController_ShouldStoreInDb()
    {
        string connectionString = "TestAddBeer";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        var client = _factory.CreateClient();
        var newBeer = new BreweryAPI.Models.BeerModel()
        {
            BeerName = "NewBeer",
            Price = 10,
            BreweryId = 1
        };

        var httpContent = new StringContent(JsonConvert.SerializeObject(newBeer), Encoding.UTF8, "application/json");
        var request = await client.PostAsync("/api/Beer", httpContent);

        var response = await client.GetAsync("/api/Beer");
        var results = await response.Content.ReadFromJsonAsync<List<BeerModel>>();

        request.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.Count.Should().Be(6);
        results[5].BeerName.Should().Be("NewBeer");
        results[5].Price.Should().Be(10);
        results[5].BreweryId.Should().Be(1);

        dbContext.Dispose();
    }

    [Fact]
    public async Task OnUdateBeer_WhenExecuteController_ShouldStoreInDb()
    {
        string connectionString = "TestUpdateBeer";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        var client = _factory.CreateClient();
        int beerId = 4;

        var newBeer = new BreweryAPI.Models.BeerModel()
        {
            BeerId = beerId,
            BeerName = "UpdatedBeer",
            Price = 5,
            BreweryId = 2
        };

        var httpContent = new StringContent(JsonConvert.SerializeObject(newBeer), Encoding.UTF8, "application/json");
        var request = await client.PutAsync($"/api/Beer/{beerId}", httpContent);

        var response = await client.GetAsync($"/api/Beer");
        var results = await response.Content.ReadFromJsonAsync<List<BeerModel>>();

        request.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.Count.Should().Be(5);
        results[3].BeerName.Should().Be("UpdatedBeer");
        results[3].Price.Should().Be(5);
        results[3].BreweryId.Should().Be(2);

        dbContext.Dispose();
    }

    public async Task OnDeleteBeer_WhenExecuteController_ShouldDeleteInDb() 
    {
        string connectionString = "TestBeerDelete";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        int beerId = 1;

        var client = _factory.CreateClient();

        var request = await client.DeleteAsync($"/api/Beer/{beerId}");

        var response = await client.GetAsync("/api/Beer");
        var results = await response.Content.ReadFromJsonAsync<List<BeerModel>>();

        request.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.Count.Should().Be(4);
        results[0].BeerName.Should().Be("TestBeer2");
        results[0].Price.Should().Be(2);
        results[0].BreweryId.Should().Be(1);

        dbContext.Dispose();
    }
}
