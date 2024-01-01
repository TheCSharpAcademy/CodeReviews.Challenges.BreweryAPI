using BreweryApi.Models;

namespace BreweryApi.Repositories
{
    public interface IBeerRepository
    {
        IEnumerable<Beer> getBeers();
        Beer getBeerByID( int id );
        void InsertBeer( Beer beer );
        void DeleteBeer( Beer beer );
        void UpdateBeer( Beer beer );
        void Save();
        void SaveAsync();
        Boolean BeerExists( int id );
    }
}
