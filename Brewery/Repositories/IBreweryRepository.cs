using BreweryApi.Models;

namespace BreweryApi.Repositories
{
    public interface IBreweryRepository
    {
        IEnumerable<Brewery> getBreweries();
        Brewery getBreweryByID(int id);
        void InsertBrewery(Brewery brewery);
        void DeleteBrewery(Brewery brewery);
        void UpdateBrewery( Brewery brewery );
        void Save();
        void SaveAsync();
        IEnumerable<Beer> GetBreweryBeers(Brewery brewery);
        Boolean BreweryExists( int id );
    }
}
