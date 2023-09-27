using AutoMapper;
using BreweryAPI.DTOs;
using BreweryAPI.Models;

namespace BreweryAPI.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<BreweryModel, BreweryDTO>();
            CreateMap<BreweryDTO, BreweryModel>();
            CreateMap<BeerModel, BeerDTO>();
            CreateMap<BeerDTO, BeerModel>();
            CreateMap<BrewerySalesModel, BrewerySalesDTO>();
            CreateMap<BrewerySalesDTO, BrewerySalesModel>();
            CreateMap<WholesalerModel, WholesalerDTO>();
            CreateMap<WholesalerDTO, WholesalerModel>();
            CreateMap<WholesalerInventory, WholesalerInventoryDTO>();
            CreateMap<WholesalerInventoryDTO, WholesalerInventory>();
            CreateMap<WholesalerQuoteModel, WholesalerQuoteDTO>();
            CreateMap<WholesalerQuoteDTO, WholesalerQuoteModel>();
        }
    }
}
