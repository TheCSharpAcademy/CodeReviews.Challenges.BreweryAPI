namespace BreweryAPI.Models
{
	public class BeerModel
	{
		public int BeerId { get; set; }
		public string Name { get; set; }
        public decimal Price { get; set; }
        public int BrewerId { get; set; }
    }
}
