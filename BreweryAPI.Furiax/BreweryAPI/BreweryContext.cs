using BreweryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryAPI
{
	public class BreweryContext : DbContext
	{
		public BreweryContext(DbContextOptions<BreweryContext> options) : base(options) { }

		public DbSet<BreweryModel> Breweries { get; set; }
		public DbSet<BeerModel> Beers { get; set; }
		public DbSet<WholesalerModel> Wholesalers { get; set; }
		public DbSet<SaleModel> Sales { get; set; }
		public DbSet<QuoteModel> Quotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			try
			{
				modelBuilder.Entity<BreweryModel>().HasData(
					new BreweryModel { Name = "ABInbev", BreweryId = 1 },
					new BreweryModel { Name = "Achouffe", BreweryId = 2 },
					new BreweryModel { Name = "Alken Maes", BreweryId = 3 },
					new BreweryModel { Name = "Belle Vue", BreweryId = 4 },
					new BreweryModel { Name = "Duvel-Moortgat", BreweryId = 5 },
					new BreweryModel { Name = "Hoegaarden", BreweryId = 6 },
					new BreweryModel { Name = "Lindemans", BreweryId = 7 },
					new BreweryModel { Name = "Palm", BreweryId = 8 },
					new BreweryModel { Name = "4KNT", BreweryId = 9 }
					);

				modelBuilder.Entity<BeerModel>().HasData(
					new BeerModel { BeerId = 1, Name = "Jupiler", BrewerId = 1, Price = 0.71m },
					new BeerModel { BeerId = 2, Name = "Stella Artois", BrewerId = 1, Price = 0.79m },
					new BeerModel { BeerId = 3, Name = "Leffe Blond", BrewerId = 1, Price = 1.52m },
					new BeerModel { BeerId = 4, Name = "Leffe Bruin", BrewerId = 1, Price = 1.52m },
					new BeerModel { BeerId = 5, Name = "Leffe Ruby", BrewerId = 1, Price = 1.66m },
					new BeerModel { BeerId = 6, Name = "Tripel Karmeliet", BrewerId = 1, Price = 1.75m },
					new BeerModel { BeerId = 7, Name = "La Chouffe", BrewerId = 2, Price = 1.95m },
					new BeerModel { BeerId = 8, Name = "Cherry Chouffe", BrewerId = 2, Price = 1.73m },
					new BeerModel { BeerId = 9, Name = "Houblon Chouffe", BrewerId = 2, Price = 1.95m },
					new BeerModel { BeerId = 10, Name = "Chouffe Soleil", BrewerId = 2, Price = 1.55m },
					new BeerModel { BeerId = 11, Name = "Maes Pils", BrewerId = 3, Price = 0.67m },
					new BeerModel { BeerId = 12, Name = "Cristal", BrewerId = 3, Price = 0.73m },
					new BeerModel { BeerId = 13, Name = "Grimbergen Blond", BrewerId = 3, Price = 1.51m },
					new BeerModel { BeerId = 14, Name = "Grimbergen Bruin", BrewerId = 3, Price = 1.51m },
					new BeerModel { BeerId = 15, Name = "Desperados", BrewerId = 3, Price = 2.04m },
					new BeerModel { BeerId = 16, Name = "Belle Vue Kriek", BrewerId = 4, Price = 1.39m },
					new BeerModel { BeerId = 17, Name = "Belle Vue Geuze", BrewerId = 4, Price = 1.31m },
					new BeerModel { BeerId = 18, Name = "Duvel", BrewerId = 5, Price = 1.65m },
					new BeerModel { BeerId = 19, Name = "Duvel 6,66", BrewerId = 5, Price = 1.61m },
					new BeerModel { BeerId = 20, Name = "Vedett", BrewerId = 5, Price = 1.21m },
					new BeerModel { BeerId = 21, Name = "Maredsous Tripel", BrewerId = 5, Price = 1.97m },
					new BeerModel { BeerId = 22, Name = "Hoegaarden", BrewerId = 6, Price = 1.00m },
					new BeerModel { BeerId = 23, Name = "Hoegaarden Grand Cru", BrewerId = 6, Price = 1.92m },
					new BeerModel { BeerId = 24, Name = "Lindemans Kriek", BrewerId = 7, Price = 1.24m },
					new BeerModel { BeerId = 25, Name = "Lindemans Oude Geuze", BrewerId = 7, Price = 0.97m },
					new BeerModel { BeerId = 26, Name = "Lindemans Framboise", BrewerId = 7, Price = 1.49m },
					new BeerModel { BeerId = 27, Name = "Lindemans Pecheresse", BrewerId = 7, Price = 1.41m },
					new BeerModel { BeerId = 28, Name = "Palm", BrewerId = 8, Price = 0.89m },
					new BeerModel { BeerId = 29, Name = "Dobbel Palm", BrewerId = 8, Price = 0.95m },
					new BeerModel { BeerId = 30, Name = "Rodenbach", BrewerId = 8, Price = 1.00m },
					new BeerModel { BeerId = 31, Name = "Cornet", BrewerId = 8, Price = 1.67m },
					new BeerModel { BeerId = 32, Name = "Estaminet Pils", BrewerId = 8, Price = 0.67m },
					new BeerModel { BeerId = 33, Name = "4KNT Tripel", BrewerId = 9, Price = 1.85m },
					new BeerModel { BeerId = 34, Name = "4KNT Square B", BrewerId = 9, Price = 1.85m },
					new BeerModel { BeerId = 35, Name = "4KNT Carre C", BrewerId = 9, Price = 1.85m }
					);

				modelBuilder.Entity<WholesalerModel>().HasData(
				new WholesalerModel { WholesalerId = 1, Name = "Colruyt" },
				new WholesalerModel { WholesalerId = 2, Name = "Prik & Tik" },
				new WholesalerModel { WholesalerId = 3, Name = "Drankenhal Van Callenberge" },
				new WholesalerModel { WholesalerId = 4, Name = "Dranken Van Remoortel" },
				new WholesalerModel { WholesalerId = 5, Name = "Drinkshop Dullaert" },
				new WholesalerModel { WholesalerId = 6, Name = "Bierland" }
				);

				modelBuilder.Entity<SaleModel>().HasData(
					new SaleModel { SaleId = 1, WholesalerId = 3, BeerId = 1, Quantity = 240 },
					new SaleModel { SaleId = 2, WholesalerId = 3, BeerId = 18, Quantity = 240 },
					new SaleModel { SaleId = 3, WholesalerId = 3, BeerId = 33, Quantity = 24 },
					new SaleModel { SaleId = 4, WholesalerId = 3, BeerId = 28, Quantity = 120 },
					new SaleModel { SaleId = 5, WholesalerId = 3, BeerId = 22, Quantity = 36 },
					new SaleModel { SaleId = 6, WholesalerId = 3, BeerId = 11, Quantity = 200 },
					new SaleModel { SaleId = 7, WholesalerId = 3, BeerId = 31, Quantity = 180 },
					new SaleModel { SaleId = 8, WholesalerId = 3, BeerId = 2, Quantity = 96 }
					);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Something went wrong when populating the database: {ex.Message}");
			}

		}
		public void CreateDatabase()
		{
			try
			{
				Database.EnsureCreated();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"The database could not be created: {ex.Message}");
			}
		}

		public void DeleteDatabase()
		{
			try
			{
				Database.EnsureDeleted();
			}
			catch ( Exception ex )
			{
				Console.WriteLine($"The database could not be deleted: {ex.Message}");
			}
		}
	}
}

