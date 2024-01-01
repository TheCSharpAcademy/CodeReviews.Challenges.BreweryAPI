using BreweryApi.Models;

namespace BreweryApi.Repositories
{
    public interface IWholesalerStockRepository
    {
        Task InsertStockRegistry( WholesalerStock wholesalerStock );
    }
}
