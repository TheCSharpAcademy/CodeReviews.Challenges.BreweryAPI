using BreweryAPI.DAL.Entities.Enums;

namespace BreweryAPI.BLL.DataTransferObjects.Beer;

public class BeerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public BeerType BeerType { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
}
