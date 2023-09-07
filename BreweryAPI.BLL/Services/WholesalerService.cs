using BreweryAPI.BLL.DataTransferObjects.Quote;
using BreweryAPI.BLL.Helpers;
using BreweryAPI.BLL.Interfaces;
using BreweryAPI.DAL.Entities;
using BreweryAPI.DAL.Interfaces;

namespace BreweryAPI.BLL.Services
{
    public class WholesalerService : IWholesalerService
    {
        private readonly IWholesalerRepository _wholesalerRepository;
        private readonly IDiscountService _discountService;

        public WholesalerService(IWholesalerRepository wholesalerRepository, IDiscountService discountService)
        {
            _wholesalerRepository = wholesalerRepository;
            _discountService = discountService;
        }

        public async Task<ServiceResult<WholesalerQuoteResponseDto>> GetQuoteAsync(Guid wholesalerId, WholesalerQuoteRequestDto quoteRequestDto)
        {
            // 1. Check for quote items
            if (quoteRequestDto == null || quoteRequestDto.QuoteItems == null || !quoteRequestDto.QuoteItems.Any())
            {
                return ServiceResult<WholesalerQuoteResponseDto>.ErrorResult(
                    ErrorType.InvalidParameter,
                    Constants.OrderEmptyMessage);
            }

            // 2. Check for duplicates in quote items
            bool duplicatesInOrder =
                quoteRequestDto.QuoteItems.Count >
                quoteRequestDto.QuoteItems.Select(item => item.BeerId).Distinct().ToList().Count;
            if (duplicatesInOrder)
            {
                return ServiceResult<WholesalerQuoteResponseDto>.ErrorResult(
                    ErrorType.InvalidParameter,
                    Constants.DuplicatesInOrderMessage);
            }

            // 3. Validate wholesaler exists
            Wholesaler? wholesaler = await _wholesalerRepository.GetWholesalerByIdAsync(wholesalerId, includeStock: true);
            if (wholesaler == null)
            {
                return ServiceResult<WholesalerQuoteResponseDto>.ErrorResult(
                    ErrorType.NotFound,
                    string.Format(Constants.NotFoundMessage, nameof(Wholesaler), nameof(Wholesaler.Id), wholesalerId));
            }

            // 4. Generate the quote for each item
            var quoteResponseItems = new List<QuoteResponseItemDto>();
            foreach (var quoteRequestItem in quoteRequestDto.QuoteItems)
            {
                // 1. Check if beer is sold by the wholesaler
                WholesalerBeer? wholesalerBeer = wholesaler.WholesalerBeers.FirstOrDefault(wholesalerBeer => wholesalerBeer.BeerId == quoteRequestItem.BeerId);
                if (wholesalerBeer == null)
                {
                    return ServiceResult<WholesalerQuoteResponseDto>.ErrorResult(
                        ErrorType.InvalidParameter,
                        Constants.BeerNotSoldByWholesalerMessage);
                }
                // 2. Check if there is enough stock of the beer
                else if (quoteRequestItem.Quantity > wholesalerBeer.StockQuantity)
                {
                    return ServiceResult<WholesalerQuoteResponseDto>.ErrorResult(
                        ErrorType.InvalidParameter,
                        Constants.NotEnoughStockMessage);
                }

                // 3. Create a quote of the item
                var quoteResponseItem = new QuoteResponseItemDto
                {
                    BeerName = wholesalerBeer.Beer.Name,
                    Quantity = quoteRequestItem.Quantity,
                    UnitPrice = wholesalerBeer.Beer.Price,
                    Subtotal = quoteRequestItem.Quantity * wholesalerBeer.Beer.Price
                };

                quoteResponseItems.Add(quoteResponseItem);
            }

            // 5. Apply discounts and formulate response
            decimal discount = _discountService.GetDiscountPercent(quoteResponseItems);
            var quoteResponseDto = new WholesalerQuoteResponseDto
            {
                QuoteItems = quoteResponseItems,
                Discount = discount,
                TotalPrice = _discountService.GetDiscountPrice(discount, quoteResponseItems),
                TotalQuantity = quoteResponseItems.Sum(item => item.Quantity),
            };

            return ServiceResult<WholesalerQuoteResponseDto>.SuccessResult(quoteResponseDto);
        }
    }
}
