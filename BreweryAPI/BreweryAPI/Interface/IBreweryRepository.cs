using BreweryAPI.Models;

namespace BreweryAPI.Interface
{
    public interface IBreweryRepository
    {
        ICollection<BreweryModel> GetBreweries();
        BreweryModel GetBrewery(int id);
        bool CreateBrewery(BreweryModel breweryModel);
        bool UpdateBrewery(BreweryModel breweryModel);
        bool DeleteBrewery(BreweryModel breweryModel);
        bool BreweryExists(int id);
        bool Save(); 
    }
}
