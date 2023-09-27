using BreweryAPI.Models;
using BreweryAPI;
using FluentAssertions;
using IntegrationTests.Helpers;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text;

namespace IntegrationTests.Controllers;

public class WholesalerInventoryTests
{
    [Fact]
    public async Task Get_Always_GetWholesalerInventories()
    {
        string connectionString = "TestGetWholesalerInventories";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/WholesalerInventory");
        var results = await response.Content.ReadFromJsonAsync<List<WholesalerInventory>>();

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.Count.Should().Be(2);
        results[0].WholesalerId.Should().Be(2);
        results[0].BeerId.Should().Be(2);
        results[0].Quantity.Should().Be(10);

        dbContext.Dispose();
    }

    [Fact]
    public async Task Get_Always_GetWholesalerInventory()
    {
        string connectionString = "TestGetWholesalerInventory";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        var client = _factory.CreateClient();
        int wholesalerId = 2;

        var response = await client.GetAsync($"/api/WholesalerInventory/{wholesalerId}");
        var results = await response.Content.ReadFromJsonAsync<WholesalerInventory>();

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.WholesalerId.Should().Be(2);
        results.BeerId.Should().Be(2);
        results.Quantity.Should().Be(10);

        dbContext.Dispose();
    }

    [Fact]
    public async Task OnAddWholesalerInventory_WhenExecuteController_ShouldStoreInDb()
    {
        string connectionString = "TestAddWholesalerInventory";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        var client = _factory.CreateClient();
        var newWholesalerInventory = new BreweryAPI.Models.WholesalerInventory()
        {
            WholesalerId = 1,
            BeerId = 2,
            Quantity = 60,
        };

        var httpContent = new StringContent(JsonConvert.SerializeObject(newWholesalerInventory), Encoding.UTF8, "application/json");
        var request = await client.PostAsync("/api/WholesalerInventory", httpContent);

        var response = await client.GetAsync("/api/WholesalerInventory");
        var results = await response.Content.ReadFromJsonAsync<List<WholesalerInventory>>();

        request.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.Count.Should().Be(3);
        results[2].WholesalerId.Should().Be(1);
        results[2].BeerId.Should().Be(2);
        results[2].Quantity.Should().Be(60);

        dbContext.Dispose();
    }

    [Fact]
    public async Task OnUpdateWholesalerInventory_WhenExecuteController_ShouldUpdateInDb()
    {
        string connectionString = "TestUpdateWholesalerInventory";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        var client = _factory.CreateClient();
        int WholesalerInventoryId = 1;

        var updatedWholesalerInventory = new BreweryAPI.Models.WholesalerInventory()
        {
            ItemId = WholesalerInventoryId,
            WholesalerId = 1,
            BeerId = 2,
            Quantity = 60,
        };

        var httpContent = new StringContent(JsonConvert.SerializeObject(updatedWholesalerInventory), Encoding.UTF8, "application/json");
        var request = await client.PutAsync($"/api/WholesalerInventory/{WholesalerInventoryId}", httpContent);

        var response = await client.GetAsync("/api/WholesalerInventory");
        var results = await response.Content.ReadFromJsonAsync<List<WholesalerInventory>>();

        request.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.Count.Should().Be(2);
        results[1].WholesalerId.Should().Be(1);
        results[1].BeerId.Should().Be(2);
        results[1].Quantity.Should().Be(60);

        dbContext.Dispose();
    }

    [Fact]
    public async Task OnDeleteWholesalerInventory_WhenExecuteController_ShouldDeleteInDb()
    {
        string connectionString = "TestWHolesalerInventoryDelete";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        int wholesalerInventoryId = 2;

        var client = _factory.CreateClient();

        var request = await client.DeleteAsync($"/api/WholesalerInventory/{wholesalerInventoryId}");

        var response = await client.GetAsync("/api/WholesalerInventory");
        var results = await response.Content.ReadFromJsonAsync<List<WholesalerInventory>>();

        request.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.Count.Should().Be(1);
        results[0].WholesalerId.Should().Be(1);
        results[0].BeerId.Should().Be(1);
        results[0].Quantity.Should().Be(10);

        dbContext.Dispose();
    }
}
