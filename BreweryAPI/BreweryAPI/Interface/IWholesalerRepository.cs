using BreweryAPI.Models;

namespace BreweryAPI.Interface
{
    public interface IWholesalerRepository
    {
        ICollection<WholesalerModel> GetWholesalers();
        WholesalerModel GetWholesaler(int id);
        bool CreateWholesaler(WholesalerModel wholesalerModel);
        bool UpdateWholesaler(WholesalerModel wholesalerModel);
        bool DeleteWholesaler(WholesalerModel wholesalerModel);
        bool WholesalerExists(int id);
        bool Save();
    }
}
