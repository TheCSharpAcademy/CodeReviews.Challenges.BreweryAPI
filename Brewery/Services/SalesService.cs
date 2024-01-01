using AutoMapper;
using BreweryApi.Models.DTOs;
using BreweryApi.Models;
using BreweryApi.Repositories;

namespace BreweryApi.Services
{
    public class SalesService
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IBreweryRepository _breweryRepository;
        private readonly IBeerRepository _beerRepository;
        private readonly IWholesalerRepository _wholesalerRepository;
        private readonly MapperConfiguration _mapperConfiguration;

        public SalesService( ISalesRepository repository, IBeerRepository beerRepository, IBreweryRepository breweryRepository, IWholesalerRepository wholesalerRepository )
        {
            _salesRepository = repository;
            _beerRepository = beerRepository;
            _breweryRepository = breweryRepository;
            _wholesalerRepository = wholesalerRepository;

            _mapperConfiguration = new MapperConfiguration(mapper => mapper.CreateMap<Sales, SaleDTO>());
        }

        public List<Sales> GetSales()
        {
            return _salesRepository.getSales().ToList();
        }

        public Sales GetSale( int id )
        {
            return _salesRepository.getSaleByID(id);
        }

        public void UpdateSale( Sales sale )
        {
            _salesRepository.UpdateSale(sale);
        }

        public void SaveDB()
        {
            _salesRepository.SaveAsync();
        }

        public bool SaleExists( int id )
        {
            return _salesRepository.SaleExists(id);
        }

        public void DeleteSale( Sales sale )
        {
            _salesRepository.DeleteSale(sale);
        }

        public (Wholesaler wholesaler, Brewery brewery, Beer beer) GetSalesInformation( Sales sales )
        {
            var wholesaler = _wholesalerRepository.getWholesalerByID(sales.WholeSalerId);
            var beer = _beerRepository.getBeerByID(sales.BeerId);
            var brewery = _breweryRepository.getBreweryByID(sales.BreweryId);

            return (wholesaler, brewery, beer);
        }

        public bool CanWholesalerSellBeer( Wholesaler wholesaler, Sales sale )
        {
            return wholesaler.AllowedBeersId.Contains(sale.BeerId);
        }

        public bool WholesalerHasSpaceInStock( Wholesaler wholesaler, Sales sale )
        {
            int stock = _wholesalerRepository.GetWholesalerStocks()
                    .Where(stock => stock.WholesalerId == wholesaler.Id).ToList()
                    .Select(stock => stock.StockQuantity)
                    .Aggregate(( StockUsed, next ) => StockUsed + next);

            return wholesaler.StockLimit > stock + sale.Quantity;
        }

        public async Task InsertSale( Wholesaler wholesaler, Sales sale )
        {
            var wholesaleStock = _wholesalerRepository.GetWholesalerStocks()
                    .FirstOrDefault(w =>
                    w.WholesalerId == wholesaler.Id
                    && w.BeerId == sale.BeerId);

            if (wholesaleStock != null)
            {
                wholesaleStock.StockQuantity += sale.Quantity;
                _wholesalerRepository.SaveAsync();
            }

            await _salesRepository.InsertSale(sale);
        }

        public List<SaleDTO> GetSalesDTO( List<Sales> sales )
        {
            List<SaleDTO> dtos = new List<SaleDTO>();

            var mapper = _mapperConfiguration.CreateMapper();

            foreach (Sales sale in sales)
            {
                SaleDTO dto = mapper.Map<SaleDTO>(sale);

                dto.Beer = _beerRepository.getBeerByID(sale.BeerId);
                dto.Brewery = _breweryRepository.getBreweryByID(sale.BreweryId);
                dto.Wholesaler = _wholesalerRepository.getWholesalerByID(sale.WholeSalerId);

                dtos.Add(dto);
            }

            return dtos;
        }

        public SaleDTO GetSaleDTO( Sales sale )
        {
            var mapper = _mapperConfiguration.CreateMapper();
            SaleDTO dto = mapper.Map<SaleDTO>(sale);

            dto.Beer = _beerRepository.getBeerByID(sale.BeerId);
            dto.Brewery = _breweryRepository.getBreweryByID(sale.BreweryId);
            dto.Wholesaler = _wholesalerRepository.getWholesalerByID(sale.WholeSalerId);

            return dto;
        }
    }
}
