using BreweryAPI.DAL.Entities;
using BreweryAPI.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BreweryAPI.DAL.Repositories;

public class BreweryRepository : IBreweryRepository
{
    private readonly BreweryAPIContext _context;

    public BreweryRepository(BreweryAPIContext context)
    {
        _context = context;
    }

    public async Task<Brewery?> GetBreweryByIdAsync(Guid breweryId)
    {
        return await _context.Breweries
            .Include(brewery => brewery.Beers)
            .FirstOrDefaultAsync(brewery => brewery.Id == breweryId);
    }

    public async Task AddSaleAsync(BrewerySale brewerySale)
    {
        await _context.BrewerySales.AddAsync(brewerySale);
        await _context.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
