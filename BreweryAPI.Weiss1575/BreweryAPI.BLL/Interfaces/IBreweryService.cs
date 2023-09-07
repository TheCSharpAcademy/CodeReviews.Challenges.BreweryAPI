using BreweryAPI.BLL.DataTransferObjects.Beer;
using BreweryAPI.BLL.DataTransferObjects.BrewerySale;
using BreweryAPI.BLL.Helpers;

namespace BreweryAPI.BLL.Interfaces;

public interface IBreweryService
{
    Task<ServiceResult<BeerDto>> AddBeerAsync(Guid breweryId, BeerCreateDto beerCreateDto);
    Task<ServiceResult> DeleteBeerAsync(Guid breweryId, Guid beerId);
    Task<ServiceResult<List<BeerDto>>> GetBeersAsync(Guid breweryId);
    Task<ServiceResult<BeerDto>> GetBeerByIdAsync(Guid breweryId, Guid beerId);
    Task<ServiceResult<BeerDto>> UpdateBeerAsync(Guid breweryId, Guid beerId, BeerUpdateDto beerUpdateDto);
    Task<ServiceResult> ProcessSaleAsync(Guid breweryId, Guid wholesalerId, BrewerySaleRequestDto brewerySaleRequestDto);
}
