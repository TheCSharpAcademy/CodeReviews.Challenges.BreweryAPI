using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreweryAPI.Models
{
	public class QuoteModel
	{
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int QuoteId { get; set; }
        [Required]
		public int WholeSalerId { get; set; }
		public WholesalerModel WholeSaler { get; set; }
        [Required]
		[NotMapped]
        public List<OrderListModel> Orders { get; set; }
	}
}
