using System.ComponentModel.DataAnnotations;

namespace BreweryAPI.Models
{
	public class QuoteModel
	{
        [Key]
        public int QuoteId { get; set; }
        public int WholeSalerId { get; set; }
        public WholesalerModel WholeSaler { get; set; }
        public decimal Price { get; set; }
        public string Summary { get; set; }
    }
}
