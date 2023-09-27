using BreweryAPI.Models;

namespace BreweryAPI.ControllerTests
{
    public static class TestData
    {
        public static void SeedDataContext(Context _testContext)
        {
            if(!_testContext.Breweries.Any())
            {
                var brewery1 = new BreweryModel
                {
                    BreweryName = "TestBrewery1",
                    BreweryLocation = "TestLocation1"
                };
                var brewery2 = new BreweryModel
                {
                    BreweryName = "TestBrewery2",
                    BreweryLocation = "TestLocation2"
                };

                _testContext.Breweries.AddRange(brewery1, brewery2); 
                _testContext.SaveChanges();
            } 

            if(!_testContext.Beers.Any())
            {
                var beer1 = new BeerModel
                {
                    BeerName = "TestBeer1",
                    Price = 1,
                    BreweryId = 1,
                };
                var beer2 = new BeerModel
                {
                    BeerName = "TestBeer2",
                    Price = 2,
                    BreweryId = 1,
                };
                var beer3 = new BeerModel
                {
                    BeerName = "TestBeer3",
                    Price = 1,
                    BreweryId = 1,
                };
                var beer4 = new BeerModel
                {
                    BeerName = "TestBeer4",
                    Price = 1,
                    BreweryId = 2,
                };
                var beer5 = new BeerModel
                {
                    BeerName = "TestBeer5",
                    Price = 3,
                    BreweryId = 2,
                };

                _testContext.Beers.AddRange(beer2, beer1, beer3, beer4, beer5);
                _testContext.SaveChanges();
            }

            if (!_testContext.Wholesalers.Any())
            {
                var wholesaler1 = new WholesalerModel
                {
                    WholesalerName = "WholesalerTest1",
                    WholesalerLocation = "TestLocation1"
                };
                var wholesaler2 = new WholesalerModel
                {
                    WholesalerName = "WholesalerTest2",
                    WholesalerLocation = "TestLocation2"
                };
                _testContext.Wholesalers.AddRange(wholesaler1, wholesaler2);
                _testContext.SaveChanges();
            }

            if (!_testContext.BrewerySales.Any())
            {
                var brewerySale1 = new BrewerySalesModel
                {
                    WholeSalerId = 1,
                    BeerId = 1,
                    Quantity = 50,
                    TotalPrice = 50
                };
                var brewerySale2 = new BrewerySalesModel
                {
                    WholeSalerId = 2,
                    BeerId = 2,
                    Quantity = 10,
                    TotalPrice = 30
                };
                _testContext.BrewerySales.AddRange(brewerySale2, brewerySale1);
                _testContext.SaveChanges();
            }

            if (!_testContext.WholesalerInventories.Any())
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
                _testContext.WholesalerInventories.AddRange(wholesalerInventory1, wholesalerInventory2);
                _testContext.SaveChanges();
            }

            if (!_testContext.WholesalerQuotes.Any())
            {
                var wholesalerQuote1 = new WholesalerQuoteModel
                {
                    ClientName = "TestClient1",
                    WholesalerId = 1,
                    BeerId = 1,
                    Quantity = 10,
                    TotalPrice = 10
                };
                var wholesalerQuote2 = new WholesalerQuoteModel
                {
                    ClientName = "TestClient2",
                    WholesalerId = 2,
                    BeerId = 2,
                    Quantity = 10,
                    TotalPrice = 30
                };
                _testContext.WholesalerQuotes.AddRange(wholesalerQuote1, wholesalerQuote2);
                _testContext.SaveChanges();
            }
        }
    }
}
