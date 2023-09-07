using BreweryAPI.DAL.Entities;
using BreweryAPI.DAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace BreweryAPI.DAL
{
    public class BreweryAPIContext : DbContext
    {
        public BreweryAPIContext(DbContextOptions<BreweryAPIContext> options) : base(options) { }

        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Wholesaler> Wholesalers { get; set; }
        public DbSet<WholesalerBeer> WholesalerBeers { get; set; }
        public DbSet<BrewerySale> BrewerySales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var brewerAId = Guid.NewGuid();
            var brewerBId = Guid.NewGuid();
            modelBuilder.Entity<Brewery>().HasData(
                new Brewery
                {
                    Id = brewerAId,
                    Name = "Brewery A",
                    Location = "Location A"
                },
                new Brewery
                {
                    Id = brewerBId,
                    Name = "Brewery B",
                    Location = "Location B"
                }
            );

            var beerA1Id = Guid.NewGuid();
            var beerA2Id = Guid.NewGuid();
            var beerB1Id = Guid.NewGuid();
            var beerB2Id = Guid.NewGuid();
            modelBuilder.Entity<Beer>().HasData(
                new Beer
                {
                    Id = beerA1Id,
                    BreweryId = brewerAId,
                    Name = "Beer A1",
                    BeerType = BeerType.Sour,
                    Description = "Description A1",
                    Price = 5.99m
                },
                new Beer
                {
                    Id = beerA2Id,
                    BreweryId = brewerAId,
                    Name = "Beer A2",
                    BeerType = BeerType.IPA,
                    Description = "Description A2",
                    Price = 6.99m
                },
                new Beer
                {
                    Id = beerB1Id,
                    BreweryId = brewerBId,
                    Name = "Beer B1",
                    BeerType = BeerType.Pilsner,
                    Description = "Description B1",
                    Price = 4.99m
                },
                new Beer
                {
                    Id = beerB2Id,
                    BreweryId = brewerBId,
                    Name = "Beer B2",
                    BeerType = BeerType.Stout,
                    Description = "Description B2",
                    Price = 7.99m
                }
            );

            var wholesalerAId = Guid.NewGuid();
            var wholesalerBId = Guid.NewGuid();
            modelBuilder.Entity<Wholesaler>().HasData(
                new Wholesaler
                {
                    Id = wholesalerAId,
                    Name = "Wholesaler A",
                    Location = "Location A"
                },
                new Wholesaler
                {
                    Id = wholesalerBId,
                    Name = "Wholesaler B",
                    Location = "Location B"
                });
            modelBuilder.Entity<WholesalerBeer>().HasData(
                new WholesalerBeer
                {
                    Id = Guid.NewGuid(),
                    WholesalerId = wholesalerAId,
                    BeerId = beerA1Id,
                    StockQuantity = 10
                },
                new WholesalerBeer
                {
                    Id = Guid.NewGuid(),
                    WholesalerId = wholesalerAId,
                    BeerId = beerB1Id,
                    StockQuantity = 10
                },
                new WholesalerBeer
                {
                    Id = Guid.NewGuid(),
                    WholesalerId = wholesalerBId,
                    BeerId = beerA2Id,
                    StockQuantity = 10
                },
                new WholesalerBeer
                {
                    Id = Guid.NewGuid(),
                    WholesalerId = wholesalerBId,
                    BeerId = beerB2Id,
                    StockQuantity = 10
                }
            );
        }
    }
}
