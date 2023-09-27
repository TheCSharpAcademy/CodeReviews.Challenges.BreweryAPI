using BreweryAPI;
using BreweryAPI.Models;
using FluentAssertions;
using IntegrationTests.Helpers;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace IntegrationTests.Controllers
{
    public class BreweryTests
    {
        [Fact]
        public async Task Get_Always_GetBreweries()
        {
            string connectionString = "TestGetBreweries";
            var _factory = TestEnvironment.CreateFactory(connectionString);
            Context dbContext = TestEnvironment.CreateDatabase(_factory);

            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/Brewery");
            var results = await response.Content.ReadFromJsonAsync<List<BreweryModel>>();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            results.Count.Should().Be(2);
            results[0].BreweryName.Should().Be("TestBrewery1");
            results[0].BreweryLocation.Should().Be("TestLocation1");
            results[0].BreweryLocation.Should().NotBe("Porto");

            dbContext.Dispose();
        }

        [Fact]
        public async Task Get_Always_GetBrewery()
        {
            string connectionString = "TestGetBrewery";
            var _factory = TestEnvironment.CreateFactory(connectionString);
            Context dbContext = TestEnvironment.CreateDatabase(_factory);

            var client = _factory.CreateClient();
            int breweryId = 1;

            var response = await client.GetAsync($"/api/Brewery/{breweryId}");
            var results = await response.Content.ReadFromJsonAsync<BreweryModel>();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            results.BreweryName.Should().Be("TestBrewery1");
            results.BreweryLocation.Should().Be("TestLocation1");
            results.BreweryLocation.Should().NotBe("Porto");

            dbContext.Dispose();
        }

        [Fact]
        public async Task OnAddBrewery_WhenExecuteController_ShouldStoreInDb()
        {
            string connectionString = "TestAddBrewery";
            var _factory = TestEnvironment.CreateFactory(connectionString);
            Context dbContext = TestEnvironment.CreateDatabase(_factory);

            var client = _factory.CreateClient();
            var newBrewery = new BreweryAPI.Models.BreweryModel()
            {
                BreweryName = "NewName",
                BreweryLocation = "NewLocation"
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(newBrewery), Encoding.UTF8, "application/json");
            var request = await client.PostAsync("/api/Brewery", httpContent);

            var response = await client.GetAsync("/api/Brewery");
            var results = await response.Content.ReadFromJsonAsync<List<BreweryModel>>();

            request.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            results.Count.Should().Be(3);
            results[2].BreweryName.Should().Be("NewName");
            results[2].BreweryLocation.Should().Be("NewLocation");
            results[2].BreweryLocation.Should().NotBe("Porto");

            dbContext.Dispose();
        }

        [Fact]
        public async Task OnUpdateBrewery_WhenExecuteController_ShouldUpdateInDb()
        {
            string connectionString = "TestUpdateBrewery";
            var _factory = TestEnvironment.CreateFactory(connectionString);
            Context dbContext = TestEnvironment.CreateDatabase(_factory);

            var client = _factory.CreateClient();
            int breweryId = 1;

            var newBrewery = new BreweryAPI.Models.BreweryModel()
            {
                BreweryId = breweryId,
                BreweryName = "UpdatedName",
                BreweryLocation = "UpdatedLocation"
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(newBrewery), Encoding.UTF8, "application/json");
            var request = await client.PutAsync($"/api/Brewery/{breweryId}", httpContent);

            var response = await client.GetAsync("/api/Brewery");
            var results = await response.Content.ReadFromJsonAsync<List<BreweryModel>>();

            request.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            results.Count.Should().Be(2);
            results[0].BreweryName.Should().Be("UpdatedName");
            results[0].BreweryLocation.Should().Be("UpdatedLocation");
            results[0].BreweryLocation.Should().NotBe("Porto");

            dbContext.Dispose();
        }

        [Fact]
        public async Task OnDeleteBrewery_WhenExecuteController_ShouldDeleteInDb()
        {
            string connectionString = "TestBreweryDelete";
            var _factory = TestEnvironment.CreateFactory(connectionString);
            Context dbContext = TestEnvironment.CreateDatabase(_factory);

            int breweryId = 1;

            var client = _factory.CreateClient();

            var request = await client.DeleteAsync($"/api/Brewery/{breweryId}");

            var response = await client.GetAsync("/api/Brewery");
            var results = await response.Content.ReadFromJsonAsync<List<BreweryModel>>();

            request.StatusCode.Should().Be(System.Net.HttpStatusCode.NoContent);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            results.Count.Should().Be(1);
            results[0].BreweryName.Should().Be("TestBrewery2");
            results[0].BreweryLocation.Should().Be("TestLocation2");
            results[0].BreweryLocation.Should().NotBe("Porto");

            dbContext.Dispose();
        }
    }
}
