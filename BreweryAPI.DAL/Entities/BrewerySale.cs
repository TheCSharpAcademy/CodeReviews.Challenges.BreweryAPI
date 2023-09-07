using System.ComponentModel.DataAnnotations.Schema;

namespace BreweryAPI.DAL.Entities
{
    [Table("BrewerySales")]
    public class BrewerySale
    {
        public Guid Id { get; set; }
        public Guid SaleId { get; set; } // Common SaleId for sales with multiple beers
        public Guid BreweryId { get; set; }
        public Guid WholesalerId { get; set; }
        public Guid BeerId { get; set; }
        public int Quantity { get; set; }
        public DateTime SaleDateTime { get; set; }
    }
}
