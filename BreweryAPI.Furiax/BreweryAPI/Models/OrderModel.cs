using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreweryAPI.Models
{
	public class OrderModel
	{
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int OrderId { get; set; }
        public int WholesalerId { get; set; }
        public WholesalerModel Wholesaler { get; set; }
        public List<OrderListModel> OrderList { get; set; }
        public int? Discount { get; set; }
        public decimal Price { get; set; }

    }
}
