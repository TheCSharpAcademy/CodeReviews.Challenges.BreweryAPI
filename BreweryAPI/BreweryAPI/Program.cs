using BreweryAPI;

using BreweryAPI.Interface;
using BreweryAPI.Repositories;

using Microsoft.EntityFrameworkCore;

public class Program
{
    private static void Main(string[] args)
    {
        Context context;
        bool isTestEnvironment = Environment.GetEnvironmentVariable("TEST_ENVIRONMENT")?.Equals("true", StringComparison.OrdinalIgnoreCase) ?? false;

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddScoped<IBreweryRepository, BreweryRepository>();
        builder.Services.AddScoped<IBeerRepository, BeerRepository>();
        builder.Services.AddScoped<IBrewerySalesRepository, BrewerySalesRepository>();
        builder.Services.AddScoped<IWholesalerRepository, WholesalerRepository>();
        builder.Services.AddScoped<IWholesalerInventoryRepository, WholesalerInventoryRepository>();
        builder.Services.AddScoped<IWholesalerQuoteRepository, WholesalerQuoteRepository>();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<Context>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            context = scope.ServiceProvider.GetService<Context>();

            try
            {
                if(!isTestEnvironment)
                {
                    SeedData seedData = new SeedData(context);
                    seedData.SeedDataContext();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database creation error: {ex.Message}");
            }
        }
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static bool IsTestEnvironment(string[] args)
    {
        return args.Contains("--test-environment");
    }
}