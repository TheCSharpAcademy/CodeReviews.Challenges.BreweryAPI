using BreweryAPI.DAL.Entities;
using BreweryAPI.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BreweryAPI.DAL.Repositories
{
    public class WholesalerRepository : IWholesalerRepository
    {
        private readonly BreweryAPIContext _context;

        public WholesalerRepository(BreweryAPIContext context)
        {
            _context = context;
        }

        public async Task<Wholesaler?> GetWholesalerByIdAsync(Guid wholesalerId, bool includeStock = false)
        {
            IQueryable<Wholesaler> query = _context.Wholesalers.Where(wholesaler => wholesaler.Id == wholesalerId);

            if (includeStock)
            {
                query = query.Include(wholesaler => wholesaler.WholesalerBeers)
                             .ThenInclude(wholesalerBeer => wholesalerBeer.Beer);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task AddWholesalerBeerAsync(WholesalerBeer wholesalerBeer)
        {
            await _context.WholesalerBeers.AddAsync(wholesalerBeer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWholesalerBeerAsync(WholesalerBeer wholesalerBeer)
        {
            _context.WholesalerBeers.Entry(wholesalerBeer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
