using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreweryAPI.Models
{
	public class SaleModel
	{
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int SaleId { get; set; }
        [Required]
        public int WholesalerId { get; set; }
        public WholesalerModel Wholesaler { get; set; }
        [Required]
        public int BeerId { get; set; }
        public BeerModel Beer { get; set; }
        [Required]
		public int Quantity { get; set; }
    }
}
