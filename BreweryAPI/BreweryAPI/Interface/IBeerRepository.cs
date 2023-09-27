using BreweryAPI.Models;

namespace BreweryAPI.Interface
{
    public interface IBeerRepository
    {
        ICollection<BeerModel> GetBeers();
        BeerModel GetBeer(int id);
        bool CreateBeer(BeerModel beerModel);
        bool UpdateBeer(BeerModel beerModel);
        bool DeleteBeer(BeerModel beerModel);
        public ICollection<BeerModel> GetBeersByBrewery(int breweryId);
        bool BeerExists(int id);
        bool Save();
    }
}
