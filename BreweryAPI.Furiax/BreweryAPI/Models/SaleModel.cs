using System.ComponentModel.DataAnnotations;

namespace BreweryAPI.Models
{
	public class SaleModel
	{
        [Key]
        public int SaleId { get; set; }
        public int WholesalerId { get; set; }
        public WholesalerModel Wholesaler { get; set; }
        public int BeerId { get; set; }
        public BeerModel Beer { get; set; }
		public int Quantity { get; set; }
    }
}
