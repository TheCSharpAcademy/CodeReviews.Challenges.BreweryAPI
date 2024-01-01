using BreweryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BreweryApi.Repositories
{
    public class BreweryRepository : IBreweryRepository
    {

        private BreweryContext _context;

        public BreweryRepository(BreweryContext context)
        {
            _context = context;
        }

        public bool BreweryExists( int id )
        {
            return _context.Brewery.Any(e => e.Id == id);
        }

        public void DeleteBrewery( Brewery brewery )
        {
            _context.Brewery.Remove(brewery);
            SaveAsync();
        }


        public IEnumerable<Brewery> getBreweries()
        {
            return _context.Brewery.ToList();
        }

        public IEnumerable<Beer> GetBreweryBeers( Brewery brewery )
        {
            return _context.Beer.Where(b => b.BreweryId == brewery.Id).ToList();
        }

        public Brewery getBreweryByID( int id )
        {
            return _context.Brewery.Find(id);
        }

        public void InsertBrewery( Brewery brewery )
        {
            _context.Brewery.Add(brewery);
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

        public void UpdateBrewery( Brewery brewery )
        {
            _context.Entry(brewery).State = EntityState.Modified;
        }
    }
}
