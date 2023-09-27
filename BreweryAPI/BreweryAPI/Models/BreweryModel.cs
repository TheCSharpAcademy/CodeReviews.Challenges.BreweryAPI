using System.ComponentModel.DataAnnotations;

namespace BreweryAPI.Models
{
    public class BreweryModel
    {
        [Key]
        public int BreweryId { get; set; }
        public string BreweryName { get; set; }
        public string BreweryLocation { get; set; }

        public ICollection<BeerModel> Beers{get; set;}
    }
}

