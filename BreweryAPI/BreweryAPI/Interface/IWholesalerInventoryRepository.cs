using BreweryAPI.Models;

namespace BreweryAPI.Interface
{
    public interface IWholesalerInventoryRepository
    {
        ICollection<WholesalerInventory> GetWholesalerInventories();
        WholesalerInventory GetWholesalerInventory(int id);
        bool CreateWholesalerInventory(WholesalerInventory wholesalerInventory);
        bool UpdateWholesalerInventory(WholesalerInventory wholesalerInventory);
        bool DeleteWholesalerInventory(WholesalerInventory wholesalerInventory);
        bool WholesalerInventoryExists(int id);
        bool Save();
        public bool IsUniqueRecord(WholesalerInventory wholesalerInventory);
        public WholesalerInventory SelectRecord(int wholesalerId, int beerId);
    }
}
