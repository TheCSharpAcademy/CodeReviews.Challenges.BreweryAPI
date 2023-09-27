using BreweryAPI.Interface;
using BreweryAPI.Models;

namespace BreweryAPI.Repositories
{
    public class BrewerySalesRepository : IBrewerySalesRepository
    {
        private readonly Context _context;
        public BrewerySalesRepository(Context context)
        {
            _context = context;
        }

        public bool BrewerySaleExists(int id)
        {
            return _context.BrewerySales.Any(bs => bs.SalesId == id);
        }

        public bool CreateBrewerySale(BrewerySalesModel brewerySalesModel)
        {
            _context.Add(brewerySalesModel);
            return Save();
        }

        public bool DeleteBrewerySales(BrewerySalesModel brewerySalesModel)
        {
            _context.Remove(brewerySalesModel);
            return Save();
        }

        public BrewerySalesModel GetBrewerySale(int id)
        {
            return _context.BrewerySales.Where(bs => bs.SalesId == id).FirstOrDefault();
        }

        public ICollection<BrewerySalesModel> GetBrewerySales()
        {
            return _context.BrewerySales.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateBrewerySales(BrewerySalesModel brewerySalesModel)
        {
            _context.Update(brewerySalesModel);
            return Save();
        }
    }
}
