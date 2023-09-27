using BreweryAPI.ControllerTests;
using BreweryAPI;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IntegrationTests.Helpers;

public static class TestEnvironment
{
    public static WebApplicationFactory<Program> CreateFactory(string connectionString)
    {
        Environment.SetEnvironmentVariable("TEST_ENVIRONMENT", "true");

        var _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<Context>));
                services.AddDbContext<Context>(options =>
                {
                    //The database needs to have different connections string trougout the running of the tests asynchonously, otherwise they access and make changes to the same database which affects the results!!
                    options.UseInMemoryDatabase(connectionString);
                });
            });
        });

            return _factory;
        }
    
    public static Context CreateDatabase(WebApplicationFactory<Program> _factory)
    {
        Context dbContext;
        using (var scope = _factory.Services.CreateScope())
        {
            var scopeService = scope.ServiceProvider;
            dbContext = scopeService.GetRequiredService<Context>();

            dbContext.Database.EnsureCreated();

            TestData.SeedDataContext(dbContext);
        }

        return dbContext;
    }
}
