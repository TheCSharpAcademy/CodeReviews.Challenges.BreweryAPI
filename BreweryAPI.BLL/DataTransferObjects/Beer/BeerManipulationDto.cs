using BreweryAPI.DAL.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace BreweryAPI.BLL.DataTransferObjects.Beer
{
    public class BeerManipulationDto
    {
        [MaxLength(50)]
        public string Name { get; set; }
        public BeerType BeerType { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
