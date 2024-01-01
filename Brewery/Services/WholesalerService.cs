using AutoMapper;
using BreweryApi.Models.DTOs;
using BreweryApi.Models;
using BreweryApi.Repositories;

namespace BreweryApi.Services
{
    public class WholesalerService
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IWholesalerRepository _wholesalerRepository;
        private readonly IBeerRepository _beerRepository;
        private readonly IWholesalerStockRepository _wholesalerStockRepository;
        private readonly MapperConfiguration _mapperConfiguration;

        public WholesalerService( IWholesalerRepository repository, ISalesRepository salesRepository, IBeerRepository beerRepository, IWholesalerStockRepository wholesalerStockRepository )
        {
            _wholesalerRepository = repository;
            _salesRepository = salesRepository;
            _beerRepository = beerRepository;
            _wholesalerStockRepository = wholesalerStockRepository;

            _mapperConfiguration = new MapperConfiguration(mapper => mapper.CreateMap<Wholesaler, WholesalerDTO>());

        }

        public List<Wholesaler> GetWholesalers()
        {
            return _wholesalerRepository.getWholesalers().ToList();
        }

        public Wholesaler GetWholesaler( int id )
        {
            var wholesaler = _wholesalerRepository.getWholesalerByID(id);

            if (wholesaler == null)
            {
                return null;
            }

            wholesaler.Sales = _salesRepository.GetAll()
                    .Where(s => s.WholeSalerId == wholesaler.Id)
                    .ToList();

            wholesaler.Stocks = _wholesalerRepository.GetWholesalerStocks()
                .Where(s => s.WholesalerId == wholesaler.Id)
                .ToList();

            return wholesaler;
        }

        public async Task UpdateWholesaler( Wholesaler wholesaler )
        {         
            _wholesalerRepository.UpdateWholesaler(wholesaler);
            await UpdateStockRegistry(wholesaler);
        }

        public void SaveDb()
        {
            _wholesalerRepository.SaveAsync();
        }

        public bool WholesalerExists( int id )
        {
            return _wholesalerRepository.WholesalerExists(id);
        }

        public void DeleteWholesaler( Wholesaler wholesaler )
        {
            _wholesalerRepository.DeleteWholesaler(wholesaler);
        }

        private async Task UpdateStockRegistry(Wholesaler wholesaler)
        {
            foreach (int beerId in wholesaler.AllowedBeersId)
            {
                var wholesaleStock = _wholesalerRepository.GetWholesalerStocks()
                    .FirstOrDefault(w =>
                    w.WholesalerId == wholesaler.Id
                    && w.BeerId == beerId);

                if (wholesaleStock == null)
                {
                    await _wholesalerStockRepository.InsertStockRegistry(
                        new WholesalerStock
                        {
                            Id = 0,
                            BeerId = beerId,
                            StockQuantity = 0,
                            WholesalerId = wholesaler.Id,
                        });
                }
            }
        }

        public async Task InsertWholesaler( Wholesaler wholesaler )
        {
            await _wholesalerRepository.InsertWholesaler(wholesaler);
            await UpdateStockRegistry(wholesaler);
        }

        public (bool, string) CalculateQuote( int wholesalerId, int beerId, int quantity )
        {
            var wholesaler = _wholesalerRepository.getWholesalerByID(wholesalerId);
            var beer = _beerRepository.getBeerByID(beerId);

            if (wholesaler == null || beer == null)
            {
                return (false, "Beer or wholesaler don't exist");
            }

            if (!wholesaler.AllowedBeersId.Contains(beer.Id))
            {
                return (false, "Wholesaler can't sell this beer");
            }

            decimal quotePrice = quantity * beer.BreweryPrice;

            if (quantity > 20)
            {
                quotePrice = quotePrice - (quotePrice * (20 / 100));

            }
            if (quantity > 10)
            {
                quotePrice = quotePrice - (quotePrice * (10 / 100));
            }

            var result = $"The price for the quoted order from {wholesaler.Name} for {quantity} units of {beer.Name} will total at around {quotePrice}";

            return (true, result);
        }


        public List<WholesalerDTO> GetWholesalerDTOs( List<Wholesaler> wholesalers )
        {
            var mapper = _mapperConfiguration.CreateMapper();

            List<WholesalerDTO> dtos = new List<WholesalerDTO>();

            foreach (Wholesaler wholesaler in wholesalers)
            {
                WholesalerDTO dto = mapper.Map<WholesalerDTO>(wholesaler);

                dto.Sales = _salesRepository.GetAll()
                    .Where(s => s.WholeSalerId == wholesaler.Id)
                    .ToList();

                dto.Stocks = _wholesalerRepository.GetWholesalerStocks()
                    .Where(s => s.WholesalerId == wholesaler.Id)
                    .ToList();

                dto.AllowedBeersNames = _wholesalerRepository.GetBeersSold(wholesaler);

                dtos.Add(dto);
            }

            return dtos;
        }
    }
}
