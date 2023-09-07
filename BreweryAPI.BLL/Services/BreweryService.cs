using AutoMapper;
using BreweryAPI.BLL.DataTransferObjects.Beer;
using BreweryAPI.BLL.DataTransferObjects.BrewerySale;
using BreweryAPI.BLL.DataTransferObjects.Quote;
using BreweryAPI.BLL.Helpers;
using BreweryAPI.BLL.Interfaces;
using BreweryAPI.DAL.Entities;
using BreweryAPI.DAL.Interfaces;

namespace BreweryAPI.BLL.Services
{
    public class BreweryService : IBreweryService
    {
        private readonly IBreweryRepository _breweryRepository;
        private readonly IWholesalerRepository _wholesalerRepository;
        private readonly IMapper _mapper;

        public BreweryService(IBreweryRepository breweryRepository, IWholesalerRepository wholesalerRepository, IMapper mapper)
        {
            _breweryRepository = breweryRepository;
            _wholesalerRepository = wholesalerRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResult<BeerDto>> AddBeerAsync(Guid breweryId, BeerCreateDto beerCreateDto)
        {
            Brewery? brewery = await _breweryRepository.GetBreweryByIdAsync(breweryId);
            if (brewery == null)
            {
                return ServiceResult<BeerDto>.ErrorResult(
                    ErrorType.NotFound,
                    string.Format(Constants.NotFoundMessage, nameof(Brewery), nameof(Brewery.Id), breweryId));
            }

            Beer beer = _mapper.Map<Beer>(beerCreateDto);
            brewery.Beers.Add(beer);
            await _breweryRepository.SaveChangesAsync();

            return ServiceResult<BeerDto>.SuccessResult(_mapper.Map<BeerDto>(beer));
        }

        public async Task<ServiceResult> DeleteBeerAsync(Guid breweryId, Guid beerId)
        {
            Brewery? brewery = await _breweryRepository.GetBreweryByIdAsync(breweryId);
            if (brewery == null)
            {
                return ServiceResult<BeerDto>.ErrorResult(
                    ErrorType.NotFound,
                    string.Format(Constants.NotFoundMessage, nameof(Brewery), nameof(Brewery.Id), breweryId));
            }

            Beer? beerToDelete = brewery.Beers.FirstOrDefault(beer => beer.Id == beerId);
            if (beerToDelete == null)
            {
                return ServiceResult<BeerDto>.ErrorResult(
                    ErrorType.NotFound,
                    string.Format(Constants.NotFoundMessage, nameof(Beer), nameof(Beer.Id), beerId));
            }

            brewery.Beers.Remove(beerToDelete);
            await _breweryRepository.SaveChangesAsync();

            return ServiceResult.SuccessResult();
        }

        public async Task<ServiceResult<List<BeerDto>>> GetBeersAsync(Guid breweryId)
        {
            Brewery? brewery = await _breweryRepository.GetBreweryByIdAsync(breweryId);
            if (brewery == null)
            {
                return ServiceResult<List<BeerDto>>.ErrorResult(
                    ErrorType.NotFound,
                    string.Format(Constants.NotFoundMessage, nameof(Brewery), nameof(Brewery.Id), breweryId));
            }

            return ServiceResult<List<BeerDto>>.SuccessResult(_mapper.Map<List<BeerDto>>(brewery.Beers));
        }

        public async Task<ServiceResult<BeerDto>> GetBeerByIdAsync(Guid breweryId, Guid beerId)
        {
            Brewery? brewery = await _breweryRepository.GetBreweryByIdAsync(breweryId);
            if (brewery == null)
            {
                return ServiceResult<BeerDto>.ErrorResult(
                    ErrorType.NotFound,
                    string.Format(Constants.NotFoundMessage, nameof(Brewery), nameof(Brewery.Id), breweryId));
            }

            Beer? beer = brewery.Beers.FirstOrDefault(beer => beer.Id == beerId);
            if (beer == null)
            {
                return ServiceResult<BeerDto>.ErrorResult(
                    ErrorType.NotFound,
                    string.Format(Constants.NotFoundMessage, nameof(Beer), nameof(Beer.Id), beerId));
            }

            return ServiceResult<BeerDto>.SuccessResult(_mapper.Map<BeerDto>(beer));
        }

        public async Task<ServiceResult<BeerDto>> UpdateBeerAsync(Guid breweryId, Guid beerId, BeerUpdateDto beerUpdateDto)
        {
            Brewery? brewery = await _breweryRepository.GetBreweryByIdAsync(breweryId);
            if (brewery == null)
            {
                return ServiceResult<BeerDto>.ErrorResult(
                    ErrorType.NotFound,
                    string.Format(Constants.NotFoundMessage, nameof(Brewery), nameof(Brewery.Id), breweryId));
            }

            Beer? beerToUpdate = brewery.Beers.FirstOrDefault(beer => beer.Id == beerId);
            if (beerToUpdate == null)
            {
                return ServiceResult<BeerDto>.ErrorResult(
                    ErrorType.NotFound,
                    string.Format(Constants.NotFoundMessage, nameof(Beer), nameof(Beer.Id), beerId));
            }

            _mapper.Map(beerUpdateDto, beerToUpdate);
            await _breweryRepository.SaveChangesAsync();

            return ServiceResult<BeerDto>.SuccessResult(_mapper.Map<BeerDto>(beerToUpdate));
        }

        public async Task<ServiceResult> ProcessSaleAsync(Guid breweryId, Guid wholesalerId, BrewerySaleRequestDto brewerySaleRequestDto)
        {
            // 1. Check for sale items
            if (brewerySaleRequestDto == null || brewerySaleRequestDto.SaleItems == null || !brewerySaleRequestDto.SaleItems.Any())
            {
                return ServiceResult.ErrorResult(ErrorType.InvalidParameter, Constants.OrderEmptyMessage);
            }

            // 2. Check for duplicates in sales items
            bool duplicatesInOrder =
                brewerySaleRequestDto.SaleItems.Count >
                brewerySaleRequestDto.SaleItems.Select(item => item.BeerId).Distinct().ToList().Count;
            if (duplicatesInOrder)
            {
                return ServiceResult.ErrorResult(ErrorType.InvalidParameter, Constants.DuplicatesInOrderMessage);
            }

            // 3. Validate brewery exists
            Brewery? brewery = await _breweryRepository.GetBreweryByIdAsync(breweryId);
            if (brewery == null)
            {
                return ServiceResult<BeerDto>.ErrorResult(
                    ErrorType.NotFound,
                    string.Format(Constants.NotFoundMessage, nameof(Brewery), nameof(Brewery.Id), breweryId));
            }

            // 4. Validate wholesaler exists
            Wholesaler? wholesaler = await _wholesalerRepository.GetWholesalerByIdAsync(wholesalerId, includeStock: true);
            if (wholesaler == null)
            {
                return ServiceResult<WholesalerQuoteResponseDto>.ErrorResult(
                    ErrorType.NotFound,
                    string.Format(Constants.NotFoundMessage, nameof(Wholesaler), nameof(Wholesaler.Id), wholesalerId));
            }

            // 5. Create sales record, update wholesaler inventory for each sale item in the request
            Guid saleId = Guid.NewGuid();
            DateTime saleDateTime = DateTime.Now;
            foreach (var saleRequestItem in brewerySaleRequestDto.SaleItems)
            {
                if (saleRequestItem.Quantity < 1) { continue; }

                // 1. Check if the beer exists / is sold by the brewery
                Beer? beer = brewery.Beers.FirstOrDefault(beer => beer.Id == saleRequestItem.BeerId);
                if (beer == null)
                {
                    return ServiceResult<BeerDto>.ErrorResult(
                        ErrorType.NotFound,
                        string.Format(Constants.NotFoundMessage, nameof(Beer), nameof(Beer.Id), saleRequestItem.BeerId));
                }

                // 2. Check if the wholesaler currently sells this beer
                WholesalerBeer? wholesalerBeer = wholesaler.WholesalerBeers.FirstOrDefault(wholesalerBeer => wholesalerBeer.BeerId == saleRequestItem.BeerId);
                if (wholesalerBeer == null)
                {
                    // wholesaler has never had this beer in stock before, create a new record
                    var newWholesalerBeer = new WholesalerBeer()
                    {
                        WholesalerId = wholesaler.Id,
                        BeerId = saleRequestItem.BeerId,
                        StockQuantity = saleRequestItem.Quantity
                    };
                    await _wholesalerRepository.AddWholesalerBeerAsync(newWholesalerBeer);
                }
                else
                {
                    // wholesaler has had this beer in stock at least once, update existing record
                    wholesalerBeer.StockQuantity += saleRequestItem.Quantity;
                    await _wholesalerRepository.UpdateWholesalerBeerAsync(wholesalerBeer);
                }

                // 3. Create a record of the sale
                var brewerySale = new BrewerySale()
                {
                    SaleId = saleId,
                    SaleDateTime = saleDateTime,
                    BreweryId = breweryId,
                    WholesalerId = wholesalerId,
                    BeerId = saleRequestItem.BeerId,
                    Quantity = saleRequestItem.Quantity,
                };
                await _breweryRepository.AddSaleAsync(brewerySale);
            }

            return ServiceResult.SuccessResult();
        }
    }
}
