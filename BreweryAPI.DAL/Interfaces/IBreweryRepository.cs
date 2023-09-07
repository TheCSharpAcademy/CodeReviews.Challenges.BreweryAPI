using BreweryAPI.DAL.Entities;

namespace BreweryAPI.DAL.Interfaces
{
    public interface IBreweryRepository
    {
        Task<Brewery?> GetBreweryByIdAsync(Guid breweryId);
        Task AddSaleAsync(BrewerySale sale);
        Task SaveChangesAsync();
    }
}
