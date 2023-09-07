using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreweryAPI.DAL.Entities
{
    [Table("Breweries")]
    [Index(nameof(Name), IsUnique = true)]
    public class Brewery
    {
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Location { get; set; }

        // A brewery can make multiple beers
        public ICollection<Beer> Beers { get; set; } = new List<Beer>();
    }
}
