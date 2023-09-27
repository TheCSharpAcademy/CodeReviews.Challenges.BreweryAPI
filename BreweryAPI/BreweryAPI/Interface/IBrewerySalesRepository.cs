using BreweryAPI.Models;

namespace BreweryAPI.Interface
{
    public interface IBrewerySalesRepository
    {
        ICollection<BrewerySalesModel> GetBrewerySales();
        BrewerySalesModel GetBrewerySale(int id);
        bool CreateBrewerySale(BrewerySalesModel brewerySalesModel);
        bool UpdateBrewerySales(BrewerySalesModel brewerySalesModel);
        bool DeleteBrewerySales(BrewerySalesModel brewerySalesModel);
        bool BrewerySaleExists(int id);
        bool Save();
    }
}
