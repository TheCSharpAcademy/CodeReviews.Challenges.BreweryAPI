using System.ComponentModel.DataAnnotations;

namespace BreweryAPI.Models
{
	public class OrderListModel
	{
        public int BeerId { get; set; }
		public int Quantity { get; set; }
	}
}
