using BreweryAPI.Models;

namespace BreweryAPI
{
    public class SeedData
    {
        private readonly Context _context;

        public SeedData(Context context)
        {
            _context = context;
        }

        public void SeedDataContext()
        {
            _context.Database.EnsureCreated();

            if(!_context.Breweries.Any())
            {
                var brewery1 = new BreweryModel
                {
                    BreweryName = "SuperBock Casa da Cerveja",
                    BreweryLocation = "Porto"
                };
                var brewery2 = new BreweryModel
                {
                    BreweryName = "Sagres",
                    BreweryLocation = "Algarve"
                };

                _context.Breweries.AddRange(brewery1, brewery2); 
                _context.SaveChanges();
            } 

            if(!_context.Beers.Any())
            {
                var beer1 = new BeerModel
                {
                    BeerName = "SuperBock",
                    Price = 1,
                    BreweryId = 1,
                };
                var beer2 = new BeerModel
                {
                    BeerName = "Abadia",
                    Price = 2,
                    BreweryId = 1,
                };
                var beer3 = new BeerModel
                {
                    BeerName = "Stout",
                    Price = 1,
                    BreweryId = 1,
                };
                var beer4 = new BeerModel
                {
                    BeerName = "Original",
                    Price = 1,
                    BreweryId = 2,
                };
                var beer5 = new BeerModel
                {
                    BeerName = "Bohemia",
                    Price = 3,
                    BreweryId = 2,
                };

                _context.Beers.AddRange(beer1, beer2, beer3, beer4, beer5);
                _context.SaveChanges();
            }

            if (!_context.Wholesalers.Any())
            {
                var wholesaler1 = new WholesalerModel
                {
                    WholesalerName = "Solbel",
                    WholesalerLocation = "Lisboa"
                };
                var wholesaler2 = new WholesalerModel
                {
                    WholesalerName = "KBE",
                    WholesalerLocation = "Algarve"
                };
                _context.Wholesalers.AddRange(wholesaler1, wholesaler2);
                _context.SaveChanges();
            }

            if (!_context.BrewerySales.Any())
            {
                var brewerySale1 = new BrewerySalesModel
                {
                    WholeSalerId = 1,
                    BeerId = 1,
                    Quantity = 10,
                    TotalPrice = 10
                };
                var brewerySale2 = new BrewerySalesModel
                {
                    WholeSalerId = 2,
                    BeerId = 2,
                    Quantity = 10,
                    TotalPrice = 30
                };
                _context.BrewerySales.AddRange(brewerySale1, brewerySale2);
                _context.SaveChanges();
            }

            if (!_context.WholesalerInventories.Any())
            {
                var wholesalerInventory1 = new WholesalerInventory
                {
                    WholesalerId = 1,
                    BeerId = 1,
                    Quantity = 10,
                };
                var wholesalerInventory2 = new WholesalerInventory
                {
                    WholesalerId = 2,
                    BeerId = 2,
                    Quantity = 10,
                };
                _context.WholesalerInventories.AddRange(wholesalerInventory1, wholesalerInventory2);
                _context.SaveChanges();
            }

            if (!_context.WholesalerQuotes.Any())
            {
                var wholesalerQuote1 = new WholesalerQuoteModel
                {
                    ClientName = "Cervejeria Lima",
                    WholesalerId = 1,
                    BeerId = 1,
                    Quantity = 10,
                    TotalPrice = 10
                };
                var wholesalerQuote2 = new WholesalerQuoteModel
                {
                    ClientName = "Bar Fritos",
                    WholesalerId = 2,
                    BeerId = 2,
                    Quantity = 10,
                    TotalPrice = 30
                };
                _context.WholesalerQuotes.AddRange(wholesalerQuote1, wholesalerQuote2);
                _context.SaveChanges();
            }
        }
    }
}
