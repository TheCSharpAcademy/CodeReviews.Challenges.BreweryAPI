using BreweryAPI.DAL.Entities;

namespace BreweryAPI.DAL.Interfaces;

public interface IWholesalerRepository
{
    Task<Wholesaler?> GetWholesalerByIdAsync(Guid wholesalerId, bool includeStock);
    Task AddWholesalerBeerAsync(WholesalerBeer wholesalerBeer);
    Task UpdateWholesalerBeerAsync(WholesalerBeer wholesaler);
}
