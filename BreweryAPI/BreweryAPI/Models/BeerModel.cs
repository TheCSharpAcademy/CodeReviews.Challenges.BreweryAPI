using System.ComponentModel.DataAnnotations;

namespace BreweryAPI.Models
{
    public class BeerModel
    {
        [Key]
        public int BeerId { get; set; }
        public string BeerName { get; set; }
        public decimal Price { get; set; }
        public int BreweryId { get; set; }

       public BreweryModel Brewery { get; set; }
       public ICollection<WholesalerInventory> WholesalerInventories { get; set; }
    }
}
