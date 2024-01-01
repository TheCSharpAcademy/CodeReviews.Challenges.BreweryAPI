using BreweryApi.Controllers;
using BreweryApi.Models;
using BreweryApi.Repositories;
using BreweryApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BreweryTests
{
    [Collection("ControllerCollection")]
    public class DatabaseControllerTests : IDisposable
    {
        private readonly BreweryContext _dbContext;

        private readonly WholesalerRepository _wholesalerRepository;
        private readonly BeerRepository _beerRepository;
        private readonly SalesRepository _salesRepository;
        private readonly BreweryRepository _breweryRepository;
        private readonly WholesalerStockRepository _wholesalerStockRepository;

        private readonly SalesService _salesService;
        private readonly WholesalerService _wholesalerService;

        private readonly SalesController _saleController;
        private readonly WholesalersController _wholesalerController;

        public DatabaseControllerTests()
        {
            var options = new DbContextOptionsBuilder<BreweryContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BrewTesting;Trusted_Connection=True;MultipleActiveResultSets=true")
            .Options;

            _dbContext = new BreweryContext(options);

            _wholesalerStockRepository = new WholesalerStockRepository(_dbContext);
            _wholesalerRepository = new WholesalerRepository(_dbContext);
            _beerRepository = new BeerRepository(_dbContext);
            _salesRepository = new SalesRepository(_dbContext);
            _breweryRepository = new BreweryRepository(_dbContext);

            _salesService = new SalesService(_salesRepository, _beerRepository, _breweryRepository, _wholesalerRepository);
            _wholesalerService = new WholesalerService(_wholesalerRepository, _salesRepository, _beerRepository, _wholesalerStockRepository);

            _saleController = new SalesController(_salesService);
            _wholesalerController = new WholesalersController(_wholesalerService);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        [Fact]
        public async Task TestCase_Fail_If_WholesalerNotBuyingAssignedBeer()
        {

            var sale = new Sales { BeerId = 2, BreweryId = 2, Quantity = 1, SaleDate = new DateTime(), WholeSalerId = 3 };

            await _saleController.PostSales(sale);

            var updatedEntity = _dbContext.Sales.FirstOrDefault(e => e.Id == 5);

            Assert.Null(updatedEntity);
        }

        [Fact]
        public async Task TestCase_Fail_If_WholesalerBuyingOverStock()
        {

            var sale = new Sales { BeerId = 1, BreweryId = 1, Quantity = 100000, SaleDate = new DateTime(), WholeSalerId = 1 };

            await _saleController.PostSales(sale);

            var updatedEntity = _dbContext.Sales.FirstOrDefault(e => e.Id == 5);

            Assert.Null(updatedEntity);
        }

        [Fact]
        public async Task TestCase_Fail_If_WholesalerDontExist()
        {

            var sale = new Sales { BeerId = 1, BreweryId = 1, Quantity = 1, SaleDate = new DateTime(), WholeSalerId = 5 };

            await _saleController.PostSales(sale);

            var updatedEntity = _dbContext.Sales.FirstOrDefault(e => e.Id == 5);

            Assert.Null(updatedEntity);
        }

        [Fact]
        public async Task TestCase_Fail_If_BeerDontExist()
        {

            var sale = new Sales { BeerId = 99, BreweryId = 1, Quantity = 1, SaleDate = new DateTime(), WholeSalerId = 1 };

            await _saleController.PostSales(sale);

            var updatedEntity = _dbContext.Sales.FirstOrDefault(e => e.Id == 5);

            Assert.Null(updatedEntity);
        }

        [Fact]
        public async Task TestCase_Fail_If_BreweryDontExist()
        {

            var sale = new Sales { BeerId = 1, BreweryId = 99, Quantity = 1, SaleDate = new DateTime(), WholeSalerId = 1 };

            await _saleController.PostSales(sale);

            var updatedEntity = _dbContext.Sales.FirstOrDefault(e => e.Id == 5);

            Assert.Null(updatedEntity);
        }

        [Fact]
        public async Task TestCase_Fail_If_SaleWith0OrNoQuantity()
        {

            var sale = new Sales { BeerId = 1, BreweryId = 1, Quantity = 0, SaleDate = new DateTime(), WholeSalerId = 1 };

            await _saleController.PostSales(sale);

            var updatedEntity = _dbContext.Sales.FirstOrDefault(e => e.Id == 5);

            Assert.Null(updatedEntity);
        }

        [Fact]
        public async Task TestCase_Fail_If_WholesalerDosntExist()
        {

            ActionResult<string> quoteResult = await _wholesalerController.GetQuote(99, 1, 100);
            BadRequestObjectResult q = (BadRequestObjectResult)quoteResult.Result;

            Assert.True(q.Value == "Beer or wholesaler don't exist");
        }

        [Fact]
        public async Task TestCase_Fail_If_BeerDosntExist()
        {

            ActionResult<string> quoteResult = await _wholesalerController.GetQuote(1, 99, 100);
            BadRequestObjectResult q = (BadRequestObjectResult)quoteResult.Result;

            Assert.True(q.Value == "Beer or wholesaler don't exist");
        }

        [Fact]
        public async Task TestCase_Fail_If_WholesalerDosntSellBeer()
        {

            ActionResult<string> quoteResult = await _wholesalerController.GetQuote(1, 3, 100);
            BadRequestObjectResult q = (BadRequestObjectResult)quoteResult.Result;

            Assert.True(q.Value == "Wholesaler can't sell this beer");
        }

        [Fact]
        public async Task TestCase_Pass_If_QuoteValid()
        {

            var quoteResult = await _wholesalerController.GetQuote(1, 1, 100);
            ContentResult quote = (ContentResult)quoteResult.Result;

            string valid = "\"The price for the quoted order from Beer Dreams for 100 units of Malt will total at around 1500,00\"";

            Assert.True(quote.Content == valid);
        }
    }
}