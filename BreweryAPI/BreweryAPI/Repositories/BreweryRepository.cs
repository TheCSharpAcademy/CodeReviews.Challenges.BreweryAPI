using BreweryAPI.Interface;
using BreweryAPI.Models;

namespace BreweryAPI.Repositories
{
    public class BreweryRepository : IBreweryRepository
    {
        private readonly Context _context;
        public BreweryRepository(Context context)
        {
            _context = context;
        }

        public bool BreweryExists(int id)
        {
            return _context.Breweries.Any(c => c.BreweryId == id);
        }

        public bool CreateBrewery(BreweryModel breweryModel)
        {
            _context.Add(breweryModel);
            return Save();
        }

        public bool DeleteBrewery(BreweryModel breweryModel)
        {
            _context.Remove(breweryModel);
            return Save();
        }

        public BreweryModel GetBrewery(int id)
        {
            return _context.Breweries.Where(b => b.BreweryId == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateBrewery(BreweryModel breweryModel)
        {
            _context.Update(breweryModel);
            return Save(); 
        }

        ICollection<BreweryModel> IBreweryRepository.GetBreweries()
        {
           return _context.Breweries.ToList();
        }
    }
}
