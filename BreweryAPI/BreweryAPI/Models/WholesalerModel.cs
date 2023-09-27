using System.ComponentModel.DataAnnotations;

namespace BreweryAPI.Models
{
    public class WholesalerModel
    {
        [Key]
        public int WholesalerID { get; set; }
        public string WholesalerName { get;set; }
        public string WholesalerLocation { get; set; }

        public ICollection<WholesalerInventory> WholesalerInventories { get; set; } // One-to-many relationship to Inventories
        public ICollection<WholesalerQuoteModel> WholesalerQuote { get; set; }
    }
}
