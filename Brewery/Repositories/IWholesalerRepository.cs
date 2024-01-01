using BreweryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryApi.Repositories
{
    public interface IWholesalerRepository
    {
        IEnumerable<Wholesaler> getWholesalers();
        Wholesaler getWholesalerByID( int id );
        Task InsertWholesaler( Wholesaler wholesaler );
        void DeleteWholesaler( Wholesaler wholesaler );
        void UpdateWholesaler( Wholesaler wholesaler );
        void Save();
        void SaveAsync();
        Boolean WholesalerExists( int id );
        DbSet<WholesalerStock> GetWholesalerStocks();
        List<string> GetBeersSold( Wholesaler wholesaler );
    }
}
