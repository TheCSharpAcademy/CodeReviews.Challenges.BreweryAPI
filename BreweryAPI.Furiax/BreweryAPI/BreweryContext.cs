using BreweryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryAPI
{
	public class BreweryContext : DbContext
	{
        public DbSet<BreweryModel> Breweries { get; set; }
		public DbSet<BeerModel> Beers { get; set; }
		public DbSet<WholesalerModel> Wholesalers { get; set; }
		public DbSet<WholesaleBeersModel> WholesaleBeers { get; set; }
		public DbSet<OrderModel> Orders { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
		optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=BreweryAPI;Trusted_Connection=True; ");

	}


}
