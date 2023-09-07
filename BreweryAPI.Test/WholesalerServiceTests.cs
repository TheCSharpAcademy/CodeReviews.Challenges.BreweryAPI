using BreweryAPI.BLL.DataTransferObjects.Quote;
using BreweryAPI.BLL.Helpers;
using BreweryAPI.BLL.Interfaces;
using BreweryAPI.BLL.Services;
using BreweryAPI.DAL.Entities;
using BreweryAPI.DAL.Entities.Enums;
using BreweryAPI.DAL.Interfaces;
using Moq;

namespace BreweryAPI.Test
{
    public class WholesalerServiceTests
    {
        #region Tests
        [Fact]
        public async void Test_GetQuote_NoErrors()
        {
            List<Guid> beerIds = GetBeerIds();
            Guid wholesalerId = GetWholesalerId();
            WholesalerQuoteRequestDto quoteRequest = GetQuoteRequestForBeers(beerIds);
            Wholesaler wholesaler = GetWholesaler(wholesalerId, beerIds);

            var mockWholesalerRepository = new Mock<IWholesalerRepository>();
            mockWholesalerRepository
                .Setup(mockWholesalerRepository => mockWholesalerRepository.GetWholesalerByIdAsync(wholesalerId, true))
                .ReturnsAsync(wholesaler);
            IWholesalerService wholeSalerService = new WholesalerService(mockWholesalerRepository.Object, new DiscountService());

            var serviceResult = await wholeSalerService.GetQuoteAsync(wholesalerId, quoteRequest);

            Assert.True(serviceResult.Success);
            Assert.NotNull(serviceResult.Data);
            Assert.True(serviceResult.Data.QuoteItems.Count > 0);
            Assert.Null(serviceResult.Message);
            Assert.Equal(ErrorType.None, serviceResult.ErrorType);
        }

        [Fact]
        public async void Test_GetQuote_NoItemsInRequest()
        {
            var mockWholesalerRepository = new Mock<IWholesalerRepository>();
            var mockDiscountService = new Mock<IDiscountService>();
            IWholesalerService wholesalerService = new WholesalerService(mockWholesalerRepository.Object, mockDiscountService.Object);

            WholesalerQuoteRequestDto quoteRequest = new WholesalerQuoteRequestDto();

            var serviceResult = await wholesalerService.GetQuoteAsync(GetWholesalerId(), quoteRequest);

            Assert.False(serviceResult.Success);
            Assert.Null(serviceResult.Data);
            Assert.Equal(ErrorType.InvalidParameter, serviceResult.ErrorType);
            Assert.Equal(serviceResult.Message, Constants.OrderEmptyMessage);
        }

        [Fact]
        public async void Test_GetQuote_DuplicatesInRequest()
        {
            WholesalerQuoteRequestDto quoteRequest = GetQuoteRequestWithDuplicates();
            Guid wholesalerId = GetWholesalerId();

            var mockWholesalerRepository = new Mock<IWholesalerRepository>();
            mockWholesalerRepository
                .Setup(mockWholesalerRepository => mockWholesalerRepository.GetWholesalerByIdAsync(wholesalerId, true))
                .ReturnsAsync(new Wholesaler());
            var mockDiscountService = new Mock<IDiscountService>();
            IWholesalerService wholeSalerService = new WholesalerService(mockWholesalerRepository.Object, mockDiscountService.Object);

            var serviceResult = await wholeSalerService.GetQuoteAsync(wholesalerId, quoteRequest);

            Assert.False(serviceResult.Success);
            Assert.Null(serviceResult.Data);
            Assert.Equal(ErrorType.InvalidParameter, serviceResult.ErrorType);
            Assert.Equal(serviceResult.Message, Constants.DuplicatesInOrderMessage);
        }

        [Fact]
        public async void Test_GetQuote_WholesalerDoesNotExist()
        {
            List<Guid> beerIds = GetBeerIds();
            Guid wholesalerId = GetWholesalerId();
            WholesalerQuoteRequestDto quoteRequest = GetQuoteRequestForBeers(beerIds);

            var mockWholesalerRepository = new Mock<IWholesalerRepository>();
            mockWholesalerRepository
                .Setup(mockWholesalerRepository => mockWholesalerRepository.GetWholesalerByIdAsync(wholesalerId, true))
                .ReturnsAsync((Wholesaler?)null);
            var mockDiscountService = new Mock<IDiscountService>();
            IWholesalerService wholeSalerService = new WholesalerService(mockWholesalerRepository.Object, mockDiscountService.Object);

            var serviceResult = await wholeSalerService.GetQuoteAsync(wholesalerId, quoteRequest);

            Assert.False(serviceResult.Success);
            Assert.Null(serviceResult.Data);
            Assert.Equal(ErrorType.NotFound, serviceResult.ErrorType);
            Assert.Equal(
                serviceResult.Message,
                string.Format(Constants.NotFoundMessage, nameof(Wholesaler), nameof(Wholesaler.Id), wholesalerId));
        }

        [Fact]
        public async void Test_GetQuote_BeerNotSoldByWholesaler()
        {
            List<Guid> beerIds = GetBeerIds();
            Guid wholesalerId = GetWholesalerId();
            WholesalerQuoteRequestDto quoteRequest = GetQuoteRequestForBeers(beerIds);
            Wholesaler wholesaler = GetWholesalerWithBeerNotSold(wholesalerId, beerIds);

            var mockWholesalerRepository = new Mock<IWholesalerRepository>();
            mockWholesalerRepository
                .Setup(mockWholesalerRepository => mockWholesalerRepository.GetWholesalerByIdAsync(wholesalerId, true))
                .ReturnsAsync(wholesaler);
            var mockDiscountService = new Mock<IDiscountService>();
            IWholesalerService wholeSalerService = new WholesalerService(mockWholesalerRepository.Object, mockDiscountService.Object);

            var serviceResult = await wholeSalerService.GetQuoteAsync(wholesalerId, quoteRequest);

            Assert.False(serviceResult.Success);
            Assert.Null(serviceResult.Data);
            Assert.Equal(ErrorType.InvalidParameter, serviceResult.ErrorType);
            Assert.Equal(serviceResult.Message, Constants.BeerNotSoldByWholesalerMessage);
        }

        [Fact]
        public async void Test_GetQuote_WholesalerBeerOutOfStock()
        {
            List<Guid> beerIds = GetBeerIds();
            Guid wholesalerId = GetWholesalerId();
            WholesalerQuoteRequestDto quoteRequest = GetQuoteRequestForBeers(beerIds);
            Wholesaler wholesaler = GetWholesalerWithBeerOutOfStock(wholesalerId, beerIds);

            var mockWholesalerRepository = new Mock<IWholesalerRepository>();
            mockWholesalerRepository
                .Setup(mockWholesalerRepository => mockWholesalerRepository.GetWholesalerByIdAsync(wholesalerId, true))
                .ReturnsAsync(wholesaler);
            var mockDiscountService = new Mock<IDiscountService>();
            IWholesalerService wholeSalerService = new WholesalerService(mockWholesalerRepository.Object, mockDiscountService.Object);

            var serviceResult = await wholeSalerService.GetQuoteAsync(wholesalerId, quoteRequest);

            Assert.False(serviceResult.Success);
            Assert.Null(serviceResult.Data);
            Assert.Equal(ErrorType.InvalidParameter, serviceResult.ErrorType);
            Assert.Equal(serviceResult.Message, Constants.NotEnoughStockMessage);
        }
        #endregion

        #region Helpers
        private static Guid GetWholesalerId()
        {
            return Guid.NewGuid();
        }

        private static List<Guid> GetBeerIds()
        {
            return new List<Guid> {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            };
        }

        private static WholesalerQuoteRequestDto GetQuoteRequestForBeers(List<Guid> beerIds)
        {
            WholesalerQuoteRequestDto quoteRequest = new WholesalerQuoteRequestDto();
            foreach (var beerId in beerIds)
            {
                quoteRequest.QuoteItems?.Add(new WholesalerQuoteRequestItemDto
                {
                    BeerId = beerId,
                    Quantity = 5
                });
            }

            return quoteRequest;
        }

        private static WholesalerQuoteRequestDto GetQuoteRequestWithDuplicates()
        {
            WholesalerQuoteRequestDto quoteRequest = new WholesalerQuoteRequestDto();
            Guid beerId = Guid.NewGuid();
            quoteRequest.QuoteItems?.Add(new WholesalerQuoteRequestItemDto()
            {
                BeerId = beerId,
                Quantity = 1,
            });
            quoteRequest.QuoteItems?.Add(new WholesalerQuoteRequestItemDto()
            {
                BeerId = beerId,
                Quantity = 2,
            });

            return quoteRequest;
        }

        private static Wholesaler GetWholesaler(Guid wholesalerId, List<Guid> beerIds)
        {
            Wholesaler wholesaler = new Wholesaler();
            wholesaler.Id = wholesalerId;

            foreach (var beerId in beerIds)
            {
                wholesaler.WholesalerBeers.Add(new WholesalerBeer
                {
                    Id = Guid.NewGuid(),
                    WholesalerId = wholesalerId,
                    BeerId = beerId,
                    StockQuantity = 10,
                    Beer = new Beer
                    {
                        Id = beerId,
                        Name = $"Beer {beerId}",
                        Price = 5,
                        BeerType = BeerType.Stout,
                        BreweryId = Guid.NewGuid(),                    
                    }
                });
            }

            return wholesaler;
        }

        private static Wholesaler GetWholesalerWithBeerNotSold(Guid wholesalerId, List<Guid> beerIds)
        {
            // Create wholesaler
            Wholesaler wholesaler = new Wholesaler();
            wholesaler.Id = wholesalerId;

            // Add beers to wholesaler stock
            foreach (var beerId in beerIds)
            {
                wholesaler.WholesalerBeers.Add(new WholesalerBeer
                {
                    Id = Guid.NewGuid(),
                    WholesalerId = wholesalerId,
                    BeerId = beerId,
                    StockQuantity = int.MaxValue
                });
            }

            // Remove one that is not going to be sold by the wholesaler
            wholesaler.WholesalerBeers.RemoveAt(0);

            return wholesaler;
        }

        private static Wholesaler GetWholesalerWithBeerOutOfStock(Guid wholesalerId, List<Guid> beerIds)
        {
            // Create wholesaler
            Wholesaler wholesaler = new Wholesaler();
            wholesaler.Id = wholesalerId;

            // Add beers to wholesaler stock
            foreach (var beerId in beerIds)
            {
                wholesaler.WholesalerBeers.Add(new WholesalerBeer
                {
                    Id = Guid.NewGuid(),
                    WholesalerId = wholesalerId,
                    BeerId = beerId,
                    StockQuantity = 0
                });
            }

            return wholesaler;
        }
        #endregion
    }
}
