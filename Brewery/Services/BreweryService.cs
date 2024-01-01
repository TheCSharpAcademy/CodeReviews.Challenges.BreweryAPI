using BreweryApi.Models;
using BreweryApi.Repositories;

namespace BreweryApi.Services
{
    public class BreweryService
    {
        private IBreweryRepository _breweryRepository;

        public BreweryService( IBreweryRepository breweryRepository )
        {
            _breweryRepository = breweryRepository;
        }

        public List<Brewery> GetBreweries()
        {
            List<Brewery> Breweries = (List<Brewery>)_breweryRepository.getBreweries();

            foreach (var brewery in Breweries)
            {
                brewery.Beers = (ICollection<Beer>)_breweryRepository.GetBreweryBeers(brewery);
            }

            return Breweries;
        }

        public Brewery GetBrewery( int id )
        {
            var brewery = _breweryRepository.getBreweryByID(id);

            if (brewery == null)
            {
                return null;
            }

            brewery.Beers = (ICollection<Beer>)_breweryRepository.GetBreweryBeers(brewery);

            return brewery;
        }

        public void DeleteBrewery( Brewery brewery )
        {
            _breweryRepository.DeleteBrewery(brewery);
        }

        public void InsertBrewery( Brewery brewery )
        {
            _breweryRepository.InsertBrewery(brewery);
        }

        public void UpdateBrewery( Brewery brewery )
        {
            _breweryRepository.UpdateBrewery(brewery);
        }

        public void SaveDb()
        {
            _breweryRepository.SaveAsync();
        }

        public bool BreweryExsists( int id )
        {
            return _breweryRepository.BreweryExists(id);
        }
    }
}
