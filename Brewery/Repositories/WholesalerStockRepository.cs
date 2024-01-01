using BreweryApi.Models;

namespace BreweryApi.Repositories
{
    public class WholesalerStockRepository : IWholesalerStockRepository
    {

        private BreweryContext _context;

        public WholesalerStockRepository( BreweryContext context )
        {
            _context = context;
        }

        public async Task InsertStockRegistry( WholesalerStock wholesalerStock )
        {
            await _context.WholesalerStocks.AddAsync(wholesalerStock);
            await _context.SaveChangesAsync();
        }
    }
}
