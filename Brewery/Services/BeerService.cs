using BreweryApi.Models;
using BreweryApi.Models.DTOs;
using BreweryApi.Repositories;

namespace BreweryApi.Services
{
    public class BeerService
    {
        private readonly IBeerRepository _beerRepository;
        private readonly IBreweryRepository _breweryRepository;

        public BeerService( IBeerRepository beerRepository, IBreweryRepository breweryRepository )
        {
            _beerRepository = beerRepository;
            _breweryRepository = breweryRepository;
        }

        public List<BeerDTO> GetBeers()
        {
            var beers = _beerRepository.getBeers();
            var beersDTO = new List<BeerDTO>();

            foreach (var beer in beers)
            {
                var brewery = _breweryRepository.getBreweryByID(beer.BreweryId);

                beersDTO.Add(new BeerDTO
                {
                    Id = beer.Id,
                    Name = beer.Name,
                    Age = beer.Age,
                    Brewery = brewery,
                    BreweryId = beer.BreweryId,
                    BreweryPrice = beer.BreweryPrice,
                    Flavour = beer.Flavour
                });
            }

            return beersDTO;
        }

        public Beer GetBeer( int id )
        {
            var beer = _beerRepository.getBeerByID(id);

            if (beer == null)
            {
                return null;
            }

            return beer;
        }

        public BeerDTO GetBeerDTO( int id )
        {
            var beer = _beerRepository.getBeerByID(id);

            if (beer == null)
            {
                return null;
            }

            var brewery = _breweryRepository.getBreweryByID(beer.BreweryId);

            var dto = new BeerDTO
            {
                Id = beer.Id,
                Name = beer.Name,
                Age = beer.Age,
                Brewery = brewery,
                BreweryId = beer.BreweryId,
                BreweryPrice = beer.BreweryPrice,
                Flavour = beer.Flavour
            };

            return dto;
        }

        public void DeleteBeer(Beer beer)
        {
            _beerRepository.DeleteBeer(beer);
            SaveDb();
        }

        public void InsertBeer(Beer beer)
        {
            _beerRepository.InsertBeer(beer);
            SaveDb();
        }

        public void UpdateBeer(Beer beer)
        {
            _beerRepository.UpdateBeer(beer);
        }

        public void SaveDb()
        {
            _beerRepository.SaveAsync();
        }

        public bool BeerExsists(int id)
        {
           return _beerRepository.BeerExists(id);
        }
    }
}
