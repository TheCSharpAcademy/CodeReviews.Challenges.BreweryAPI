using BreweryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryApi.Repositories
{
    public class BeerRepository : IBeerRepository
    {

        private BreweryContext _context;

        public BeerRepository(BreweryContext context)
        {
            _context = context;
        }

        public void DeleteBeer( Beer beer )
        {
            _context.Beer.Remove(beer);
            SaveAsync();
        }

        public Beer getBeerByID( int id )
        {
            return _context.Beer.Find(id);
        }

        public IEnumerable<Beer> getBeers()
        {
            return _context.Beer.ToList();
        }

        public void InsertBeer( Beer beer )
        {
            _context.Beer.Add(beer);
            SaveAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void SaveAsync()
        {
            _context.SaveChangesAsync();
        }

        public void UpdateBeer( Beer beer )
        {
            _context.Entry(beer).State = EntityState.Modified;
        }

        public Boolean BeerExists(int id)
        {
            return _context.Beer.Any(e => e.Id == id);
        }
    }
}
