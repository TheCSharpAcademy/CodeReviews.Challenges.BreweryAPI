using BreweryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryApi.Repositories;

public class WholesalerRepository : IWholesalerRepository
{
    private BreweryContext _context;

    public WholesalerRepository(BreweryContext context)
    {
        _context = context;
    }

    public void DeleteWholesaler( Wholesaler wholesaler )
    {
        _context.Wholesalers.Remove(wholesaler);
        SaveAsync();
    }

    public Wholesaler getWholesalerByID( int id )
    {
        return _context.Wholesalers.Find(id);
    }

    public IEnumerable<Wholesaler> getWholesalers()
    {
        return _context.Wholesalers.ToList();
    }

    public async Task InsertWholesaler( Wholesaler wholesaler )
    {
        await _context.Wholesalers.AddAsync(wholesaler);
        await _context.SaveChangesAsync();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public void SaveAsync()
    {
        _context.SaveChangesAsync();
    }

    public void UpdateWholesaler( Wholesaler wholesaler )
    {
        _context.Entry(wholesaler).State = EntityState.Modified;
    }

    public List<string?> GetBeersSold(Wholesaler wholesaler)
    {
       return _context.Beer
            .Where(b => wholesaler.AllowedBeersId
            .Contains(b.Id))
            .Select(b => b.Name).ToList();    
    }

    public DbSet<WholesalerStock> GetWholesalerStocks()
    {
        return _context.WholesalerStocks;
    }

    public bool WholesalerExists( int id )
    {
        return _context.Wholesalers.Any(e => e.Id == id);
    }
}
