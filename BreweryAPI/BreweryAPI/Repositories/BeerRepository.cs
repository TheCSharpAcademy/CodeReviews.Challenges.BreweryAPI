using BreweryAPI.Interface;
using BreweryAPI.Models;

namespace BreweryAPI.Repositories
{
    public class BeerRepository : IBeerRepository
    {
        private readonly Context _context;
        public BeerRepository(Context context)
        {
            _context = context;
        }

        public bool BeerExists(int id)
        {
            return _context.Beers.Any(b => b.BeerId == id);
        }

        public bool CreateBeer(BeerModel beerModel)
        {
            _context.Add(beerModel);
            return Save();
        }

        public bool DeleteBeer(BeerModel beerModel)
        {
            _context.Remove(beerModel);
            return Save();
        }

        public BeerModel GetBeer(int id)
        {
            return _context.Beers.Where(b => b.BeerId == id).FirstOrDefault();
        }

        public ICollection<BeerModel> GetBeers()
        {
            return _context.Beers.ToList();
        }

        public ICollection<BeerModel> GetBeersByBrewery(int breweryId)
        {
            return _context.Beers.Where(b => b.BreweryId == breweryId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateBeer(BeerModel beerModel)
        {
            _context.Update(beerModel);
            return Save();
        }
    }
}
