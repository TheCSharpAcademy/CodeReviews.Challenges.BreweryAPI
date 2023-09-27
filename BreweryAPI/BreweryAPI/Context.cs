using BreweryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryAPI
{
    public class Context : DbContext
    {
        public DbSet<BreweryModel> Breweries { get; set; }
        public DbSet<BeerModel> Beers { get; set; }
        public DbSet<BrewerySalesModel> BrewerySales { get; set; }
        public DbSet<WholesalerModel> Wholesalers { get; set; }
        public DbSet<WholesalerInventory> WholesalerInventories { get; set; }
        public DbSet<WholesalerQuoteModel> WholesalerQuotes { get; set; }

        public Context (DbContextOptions<Context> options) : base (options)
        {
        }
    }
}
