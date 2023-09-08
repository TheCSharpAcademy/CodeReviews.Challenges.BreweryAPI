namespace BreweryAPI.Models
{
	public class WholesalerModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<WholesaleBeersModel> BeersInStock { get; set; }
	}
}
