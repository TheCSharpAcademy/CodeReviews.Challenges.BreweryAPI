namespace BreweryAPI.Models
{
	public class QuoteModel
	{
		public int WholeSalerId { get; set; }
		public WholesalerModel WholeSaler { get; set; }
		public List<OrderListModel> Orders { get; set; }
	}
}
