using BreweryApi.Models;
using BreweryApi.Repositories;
using BreweryApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<BreweryContext>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("BreweryDatabase")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBreweryRepository, BreweryRepository>();
builder.Services.AddScoped<IBeerRepository, BeerRepository>();
builder.Services.AddScoped<ISalesRepository, SalesRepository>();
builder.Services.AddScoped<IWholesalerRepository, WholesalerRepository>();
builder.Services.AddScoped<IWholesalerStockRepository, WholesalerStockRepository>();
builder.Services.AddScoped<BreweryService>();
builder.Services.AddScoped<BeerService>();
builder.Services.AddScoped<SalesService>();
builder.Services.AddScoped<WholesalerService>();

//This is how you add extra more DI's through application
//builder.Services.AddScoped<BreweryService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = scope.ServiceProvider.GetRequiredService<BreweryContext>();
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
}

app.UseCors("AllowAngularDev");
app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
