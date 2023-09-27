using System.ComponentModel.DataAnnotations;

namespace BreweryAPI.Models
{
    public class WholesalerInventory
    {
        [Key]
        public int ItemId { get; set; }
        public int WholesalerId { get; set; }
        public int BeerId { get; set; }
        public int Quantity { get; set; }

        public WholesalerModel Wholesaler { get; set; }
    }
}
