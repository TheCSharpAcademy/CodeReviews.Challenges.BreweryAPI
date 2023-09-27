using System.ComponentModel.DataAnnotations;

namespace BreweryAPI.Models
{
    public class BrewerySalesModel
    {
        [Key]
        public int SalesId { get; set; }
        public int WholeSalerId { get; set; }
        public int BeerId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public WholesalerModel Wholesaler { get; set; }
    }
}
