using BreweryAPI.Models;
using BreweryAPI;
using FluentAssertions;
using IntegrationTests.Helpers;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text;

namespace IntegrationTests.Controllers;

public class BrewerySalesTest
{
    [Fact]
    public async Task Get_Always_GetBrewerySales()
    {
        string connectionString = "TestGetBrewerySales";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/BrewerySales");
        var results = await response.Content.ReadFromJsonAsync<List<BrewerySalesModel>>();

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.Count.Should().Be(2);
        results[0].WholeSalerId.Should().Be(1);
        results[0].BeerId.Should().Be(1);
        results[0].Quantity.Should().Be(50);
        results[0].TotalPrice.Should().Be(50);

        dbContext.Dispose();
    }

    [Fact]
    public async Task Get_Always_GetBrewerySale()
    {
        string connectionString = "TestGetBrewerySale";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        var client = _factory.CreateClient();
        int brewerySaleId = 2;

        var response = await client.GetAsync($"/api/BrewerySales/{brewerySaleId}");
        var results = await response.Content.ReadFromJsonAsync<BrewerySalesModel>();

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.WholeSalerId.Should().Be(1);
        results.BeerId.Should().Be(1);
        results.Quantity.Should().Be(50);
        results.TotalPrice.Should().Be(50);

        dbContext.Dispose();
    }

    [Fact]
    public async Task OnAddBrewerySale_WhenExecuteController_ShouldStoreInDb()
    {
        string connectionString = "TestAddBrewerySale";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        var client = _factory.CreateClient();
        var newBrewerySale = new BreweryAPI.Models.BrewerySalesModel()
        {
            WholeSalerId = 1,
            BeerId = 1,
            Quantity = 20,
            TotalPrice = 40
        };

        var httpContent = new StringContent(JsonConvert.SerializeObject(newBrewerySale), Encoding.UTF8, "application/json");
        var request = await client.PostAsync("/api/BrewerySales", httpContent);

        var response = await client.GetAsync("/api/BrewerySales");
        var results = await response.Content.ReadFromJsonAsync<List<BrewerySalesModel>>();

        request.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.Count.Should().Be(3);
        results[2].WholeSalerId.Should().Be(1);
        results[2].BeerId.Should().Be(1);
        results[2].Quantity.Should().Be(20);
        results[2].TotalPrice.Should().Be(40);

        dbContext.Dispose();
    }


    [Fact]
    public async Task OnUpdateBrewery_WhenExecuteController_ShouldUpdateInDb()
    {
        string connectionString = "TestUpdateBrewerySale";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        var client = _factory.CreateClient();
        int salesId = 2;

        var updatedBrewerySale = new BreweryAPI.Models.BrewerySalesModel()
        {
            SalesId = salesId,
            WholeSalerId = 1,
            BeerId = 1,
            Quantity = 20,
            TotalPrice = 40
        };

        var httpContent = new StringContent(JsonConvert.SerializeObject(updatedBrewerySale), Encoding.UTF8, "application/json");
        var request = await client.PutAsync($"/api/BrewerySales/{salesId}", httpContent);

        var response = await client.GetAsync("/api/BrewerySales");
        var results = await response.Content.ReadFromJsonAsync<List<BrewerySalesModel>>();

        request.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.Count.Should().Be(2);
        results[0].WholeSalerId.Should().Be(1);
        results[0].BeerId.Should().Be(1);
        results[0].Quantity.Should().Be(20);
        results[0].TotalPrice.Should().Be(40);

        dbContext.Dispose();
    }

    [Fact]
    public async Task OnDeleteBrewerySale_WhenExecuteController_ShouldDeleteInDb()
    {
        string connectionString = "TestBrewerySaleDelete";
        var _factory = TestEnvironment.CreateFactory(connectionString);
        Context dbContext = TestEnvironment.CreateDatabase(_factory);

        int brewerySaleId = 1;

        var client = _factory.CreateClient();

        var request = await client.DeleteAsync($"/api/BrewerySales/{brewerySaleId}");

        var response = await client.GetAsync("/api/BrewerySales");
        var results = await response.Content.ReadFromJsonAsync<List<BrewerySalesModel>>();

        request.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        results.Count.Should().Be(1);
        results[0].WholeSalerId.Should().Be(1);
        results[0].BeerId.Should().Be(1);
        results[0].Quantity.Should().Be(50);
        results[0].TotalPrice.Should().Be(50);

        dbContext.Dispose();
    }
}
