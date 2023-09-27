using BreweryAPI.Models;
using BreweryAPI;
using FluentAssertions;
using IntegrationTests.Helpers;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text;

namespace IntegrationTests.Controllers;

public class WholsesalerTest
{
    [Fact]
    public async Task Get_Always_GetWholesalers()
    {
        string connectionString = "TestGetWholesalers";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/Wholesaler");
        var results = await response.Content.ReadFromJsonAsync<List<WholesalerModel>>();

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.Count.Should().Be(2);
        results[0].WholesalerName.Should().Be("WholesalerTest2");
        results[0].WholesalerLocation.Should().Be("TestLocation2");

        dbContext.Dispose();
    }

    [Fact]
    public async Task Get_Always_GetWholesaler()
    {
        string connectionString = "TestGetWholesaler";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        var client = _factory.CreateClient();
        int wholesalerId = 1;

        var response = await client.GetAsync($"/api/Wholesaler/{wholesalerId}");
        var results = await response.Content.ReadFromJsonAsync<WholesalerModel>();

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.WholesalerName.Should().Be("WholesalerTest1");
        results.WholesalerLocation.Should().Be("TestLocation1");

        dbContext.Dispose();
    }

    [Fact]
    public async Task OnAddWholesaler_WhenExecuteController_ShouldStoreInDb()
    {
        string connectionString = "TestAddWholesaler";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        var client = _factory.CreateClient();
        var newWholesaler = new BreweryAPI.Models.WholesalerModel()
        {
            WholesalerName = "Wholesaler3",
            WholesalerLocation = "WholesalerLocation3"
        };

        var httpContent = new StringContent(JsonConvert.SerializeObject(newWholesaler), Encoding.UTF8, "application/json");
        var request = await client.PostAsync("/api/Wholesaler", httpContent);

        var response = await client.GetAsync("/api/Wholesaler");
        var results = await response.Content.ReadFromJsonAsync<List<WholesalerModel>>();

        request.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.Count.Should().Be(3);
        results[2].WholesalerName.Should().Be("Wholesaler3");
        results[2].WholesalerLocation.Should().Be("WholesalerLocation3");

        dbContext.Dispose();
    }

    [Fact]
    public async Task OnUpdateWholesaler_WhenExecuteController_ShouldUpdateInDb()
    {
        string connectionString = "TestUpdateWholesaler";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        var client = _factory.CreateClient();
        int wholesalerId = 1;

        var newBrewery = new BreweryAPI.Models.WholesalerModel()
        {
            WholesalerID = wholesalerId,
            WholesalerName = "Wholesaler3",
            WholesalerLocation = "WholesalerLocation3"
        };

        var httpContent = new StringContent(JsonConvert.SerializeObject(newBrewery), Encoding.UTF8, "application/json");
        var request = await client.PutAsync($"/api/Wholesaler/{wholesalerId}", httpContent);

        var response = await client.GetAsync("/api/Wholesaler");
        var results = await response.Content.ReadFromJsonAsync<List<WholesalerModel>>();

        request.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.Count.Should().Be(2);
        results[1].WholesalerName.Should().Be("Wholesaler3");
        results[1].WholesalerLocation.Should().Be("WholesalerLocation3");

        dbContext.Dispose();
    }

    [Fact]
    public async Task OnDeleteWholesaler_WhenExecuteController_ShouldDeleteInDb()
    {
        string connectionString = "TestWholesalerDelete";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        int wholesalerId = 2;

        var client = _factory.CreateClient();

        var request = await client.DeleteAsync($"/api/Wholesaler/{wholesalerId}");

        var response = await client.GetAsync("/api/Wholesaler");
        var results = await response.Content.ReadFromJsonAsync<List<WholesalerModel>>();

        request.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.Count.Should().Be(1);
        results[0].WholesalerName.Should().Be("WholesalerTest1");
        results[0].WholesalerLocation.Should().Be("TestLocation1");

        dbContext.Dispose();
    }
}
