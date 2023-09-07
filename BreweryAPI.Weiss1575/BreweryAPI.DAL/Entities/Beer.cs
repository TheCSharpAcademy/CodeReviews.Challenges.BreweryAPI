using BreweryAPI.DAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreweryAPI.DAL.Entities;

[Table("Beers")]
[Index(nameof(BreweryId), nameof(Name), IsUnique = true)]
public class Beer
{
    public Guid Id { get; set; }
    public Guid BreweryId { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    public BeerType BeerType { get; set; }
    [MaxLength(500)]
    public string? Description { get; set; }
    public decimal Price { get; set; }
}
