using BreweryAPI.Interface;
using BreweryAPI.Models;

namespace BreweryAPI.Repositories
{
    public class WholesalerInventoryRepository : IWholesalerInventoryRepository
    {
        private readonly Context _context;
        public WholesalerInventoryRepository(Context context)
        {
            _context = context;
        }
        public bool CreateWholesalerInventory(WholesalerInventory wholesalerInventory)
        {
            _context.Add(wholesalerInventory);
            return Save();
        }

        public bool DeleteWholesalerInventory(WholesalerInventory wholesalerInventory)
        {
            _context.Remove(wholesalerInventory);
            return Save();
        }

        public ICollection<WholesalerInventory> GetWholesalerInventories()
        {
            return _context.WholesalerInventories.ToList();
        }

        public WholesalerInventory GetWholesalerInventory(int id)
        {
            return _context.WholesalerInventories.Where(wi => wi.ItemId == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateWholesalerInventory(WholesalerInventory wholesalerInventory)
        {
            _context.Update(wholesalerInventory);
            return Save();
        }

        public bool WholesalerInventoryExists(int id)
        { 
            return _context.WholesalerInventories.Any(wi => wi.ItemId == id);
        }

        public WholesalerInventory SelectRecord(int wholesalerId, int beerId)
        {
            return _context.WholesalerInventories.Where(wi => wi.WholesalerId == wholesalerId)
                .Where(wi => wi.BeerId == beerId).FirstOrDefault();
        }
        public bool IsUniqueRecord(WholesalerInventory wholesalerInventory)
        {
            return _context.WholesalerInventories.Where(wi => wi.WholesalerId == wholesalerInventory.WholesalerId)
                .Any(wi => wi.BeerId == wholesalerInventory.BeerId);
        } 
    }
}
