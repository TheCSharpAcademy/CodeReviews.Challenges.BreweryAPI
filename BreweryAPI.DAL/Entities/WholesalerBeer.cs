using System.ComponentModel.DataAnnotations.Schema;

namespace BreweryAPI.DAL.Entities
{
    [Table("WholesalerBeers")]
    public class WholesalerBeer
    {
        public Guid Id { get; set; }
        public Guid WholesalerId { get; set; }
        public Guid BeerId { get; set; }
        public int StockQuantity { get; set; }

        public Beer Beer { get; set; } = null!;
    }
}
