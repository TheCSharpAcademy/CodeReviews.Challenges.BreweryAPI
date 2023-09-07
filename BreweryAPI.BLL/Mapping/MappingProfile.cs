using AutoMapper;
using BreweryAPI.BLL.DataTransferObjects.Beer;
using BreweryAPI.DAL.Entities;

namespace BreweryAPI.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Beer, BeerDto>();
            CreateMap<BeerCreateDto, Beer>();
            CreateMap<BeerUpdateDto, Beer>();
        }
    }
}