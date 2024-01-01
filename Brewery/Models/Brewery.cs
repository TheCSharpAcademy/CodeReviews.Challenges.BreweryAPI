using System.ComponentModel.DataAnnotations;

namespace BreweryApi.Models
{
    public class Brewery
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public required ICollection<Beer> Beers { get; set; }
    }
}
