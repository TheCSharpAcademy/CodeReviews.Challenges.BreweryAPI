namespace BreweryAPI.Models
{
	public class OrderModel
	{
		public int OrderId { get; set; }
        public int WholesalerId { get; set; }
        public List<BeerModel> BeersOrdered { get; set; }

    }
}
