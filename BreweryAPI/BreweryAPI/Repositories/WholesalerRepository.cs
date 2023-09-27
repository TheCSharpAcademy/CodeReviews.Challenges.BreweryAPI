using BreweryAPI.Interface;
using BreweryAPI.Models;

namespace BreweryAPI.Repositories
{
    public class WholesalerRepository : IWholesalerRepository
    {
        private readonly Context _context
            ;
        public WholesalerRepository(Context context
            )
        {
            _context = context;
        }
        public bool CreateWholesaler(WholesalerModel wholesalerModel)
        {
            _context.Add(wholesalerModel );
            return Save();
        }

        public bool DeleteWholesaler(WholesalerModel wholesalerModel)
        {
            _context.Remove(wholesalerModel);
            return Save();
        }

        public WholesalerModel GetWholesaler(int id)
        {
            return _context.Wholesalers.Where(w => w.WholesalerID == id).FirstOrDefault();
        }

        public ICollection<WholesalerModel> GetWholesalers()
        {
            return _context.Wholesalers.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateWholesaler(WholesalerModel wholesalerModel)
        {
            _context.Update(wholesalerModel);
            return Save();
        }

        public bool WholesalerExists(int id)
        {
            return _context.Wholesalers.Any(w => w.WholesalerID ==id);
        }
    }
}
