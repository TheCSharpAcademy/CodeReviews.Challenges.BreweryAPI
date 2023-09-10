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

			modelBuilder.Entity<BreweryModel>().HasData(
				new BreweryModel { Name = "ABInbev", BreweryId = 1},
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
				new BeerModel { BeerId = 1, Name = "Jupiler", BrewerId = 1 },
				new BeerModel { BeerId = 2, Name = "Stella Artois", BrewerId = 1 },
				new BeerModel { BeerId = 3, Name = "Leffe Blond", BrewerId = 1 },
				new BeerModel { BeerId = 4, Name = "Leffe Bruin", BrewerId = 1 },
				new BeerModel { BeerId = 5, Name = "Leffe Ruby", BrewerId = 1 },
				new BeerModel { BeerId = 6, Name = "Tripel Karmeliet", BrewerId = 1 },
				new BeerModel { BeerId = 7, Name = "La Chouffe", BrewerId = 2 },
				new BeerModel { BeerId = 8, Name = "Cherry Chouffe", BrewerId = 2 },
				new BeerModel { BeerId = 9, Name = "Houblon Chouffe", BrewerId = 2 },
				new BeerModel { BeerId = 10, Name = "Chouffe Soleil", BrewerId = 2 },
				new BeerModel { BeerId = 11, Name = "Maes Pils", BrewerId = 3 },
				new BeerModel { BeerId = 12, Name = "Cristal", BrewerId = 3 },
				new BeerModel { BeerId = 13, Name = "Grimbergen Blond", BrewerId = 3 },
				new BeerModel { BeerId = 14, Name = "Grimbergen Bruin", BrewerId = 3 },
				new BeerModel { BeerId = 15, Name = "Desperados", BrewerId = 3 },
				new BeerModel { BeerId = 16, Name = "Belle Vue Kriek", BrewerId = 4 },
				new BeerModel { BeerId = 17, Name = "Belle Vue Geuze", BrewerId = 4 },
				new BeerModel { BeerId = 18, Name = "Duvel", BrewerId = 5 },
				new BeerModel { BeerId = 19, Name = "Duvel 6,66", BrewerId = 5 },
				new BeerModel { BeerId = 20, Name = "Vedett", BrewerId = 5 },
				new BeerModel { BeerId = 21, Name = "Maredsous", BrewerId = 5 },
				new BeerModel { BeerId = 22, Name = "Hoegaarden", BrewerId = 6 },
				new BeerModel { BeerId = 23, Name = "Hoegaarden Grand Cru", BrewerId = 6 },
				new BeerModel { BeerId = 24, Name = "Lindemans Kriek", BrewerId = 7 },
				new BeerModel { BeerId = 25, Name = "Lindemans Oude Geuze", BrewerId = 7 },
				new BeerModel { BeerId = 26, Name = "Lindemans Framboise", BrewerId = 7 },
				new BeerModel { BeerId = 27, Name = "Lindemans Pecheresse", BrewerId = 7 },
				new BeerModel { BeerId = 28, Name = "Palm", BrewerId = 8 },
				new BeerModel { BeerId = 29, Name = "Dobbel Palm", BrewerId = 8 },
				new BeerModel { BeerId = 30, Name = "Rodenbach", BrewerId = 8 },
				new BeerModel { BeerId = 31, Name = "Cornet", BrewerId = 8 },
				new BeerModel { BeerId = 32, Name = "Estaminet Pils", BrewerId = 8 },
				new BeerModel { BeerId = 33, Name = "Tripel", BrewerId = 9 },
				new BeerModel { BeerId = 34, Name = "Square B", BrewerId = 9 },
				new BeerModel { BeerId = 35, Name = "Carre C", BrewerId = 9 }
				);

			modelBuilder.Entity<WholesalerModel>().HasData(
			new WholesalerModel { WholesalerId = 1, Name = "Colruyt" },
			new WholesalerModel { WholesalerId = 2, Name = "Prik & Tik" },
			new WholesalerModel { WholesalerId = 3, Name = "Van Callenberge" },
			new WholesalerModel { WholesalerId = 4, Name = "Dranken Van Remoortel" },
			new WholesalerModel { WholesalerId = 5, Name = "Drinkshop Dullaert" },
			new WholesalerModel { WholesalerId = 6, Name = "Bierland" }
			);

		}
		public void CreateDatabase()
		{
			Database.EnsureCreated();
		}

		public void DeleteDatabase()
		{
			Database.EnsureDeleted();
		}
	}
}

