using Microsoft.EntityFrameworkCore;

namespace BreweryApi.Models
{
    public class BreweryContext : DbContext
    {

        public BreweryContext() { }
        public BreweryContext( DbContextOptions<BreweryContext> options ) :base(options)
        {
        
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Brewery>().HasData(
                new Brewery { Id = 1, Name = "Malter", Beers = new List<Beer>() },
                new Brewery { Id = 2, Name = "Holing", Beers = new List<Beer>() },
                new Brewery { Id = 3, Name = "Mudkip", Beers = new List<Beer>() }
                );

            modelBuilder.Entity<Beer>().HasData(
                new Beer { Id = 1, Name = "Malt", Age = "Grand Reserve", BreweryId = 1, Flavour = "Honeyed", BreweryPrice = 15.0m },
                new Beer { Id = 2, Name = "Whisk", Age = "Common", BreweryId = 2, Flavour = "spice", BreweryPrice = 9.0m },
                new Beer { Id = 3, Name = "Hon", Age = "Rare", BreweryId = 3, Flavour = "Yest", BreweryPrice = 11.15m}
                );

            modelBuilder.Entity<Wholesaler>().HasData(
                new Wholesaler { Id = 1, Name = "Beer Dreams", StockLimit= 1000, Sales = new List<Sales>(), Stocks = new List<WholesalerStock>(), AllowedBeersId = [1] },
                new Wholesaler { Id = 2, Name = "Beerum", StockLimit= 800, Sales = new List<Sales>(), Stocks = new List<WholesalerStock>(), AllowedBeersId = [1,2,3] },
                new Wholesaler { Id = 3, Name = "Quantum Beers", StockLimit= 1500, Sales = new List<Sales>(), Stocks = new List<WholesalerStock>(), AllowedBeersId = [1,3] }
                );

            modelBuilder.Entity<Sales>().HasData(
                new Sales { Id = 1, BeerId = 2, BreweryId = 2, Quantity = 100, SaleDate = DateTime.Now, WholeSalerId = 1 },
                new Sales { Id = 2, BeerId = 1, BreweryId = 1, Quantity = 500, SaleDate = DateTime.Now, WholeSalerId = 1 },
                new Sales { Id = 3, BeerId = 2, BreweryId = 2, Quantity = 400, SaleDate = DateTime.Now, WholeSalerId = 2 },
                new Sales { Id = 4, BeerId = 3, BreweryId = 3, Quantity = 1000, SaleDate = DateTime.Now, WholeSalerId = 3 }
                );

            modelBuilder.Entity<WholesalerStock>().HasData(
                new WholesalerStock { Id = 1, WholesalerId = 1, BeerId = 2, StockQuantity = 100 },
                new WholesalerStock { Id = 2, WholesalerId = 1, BeerId = 1, StockQuantity = 500 },
                new WholesalerStock { Id = 3, WholesalerId = 2, BeerId = 2, StockQuantity = 400 },
                new WholesalerStock { Id = 4, WholesalerId = 3, BeerId = 3, StockQuantity = 1000 }
                );
        }

        public DbSet<Beer> Beer { get; set; } = null!;
        public DbSet<Brewery> Brewery { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Wholesaler> Wholesalers { get; set; }
        public DbSet<WholesalerStock> WholesalerStocks { get; set; }
    }
}
