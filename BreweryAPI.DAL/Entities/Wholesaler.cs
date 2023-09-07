using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreweryAPI.DAL.Entities
{
    [Table("Wholesalers")]
    [Index(nameof(Name), IsUnique = true)]
    public class Wholesaler
    {
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? Location { get; set; }

        // A wholesaler can have multiple beers in stock
        public List<WholesalerBeer> WholesalerBeers { get; } = new();
    }
}
