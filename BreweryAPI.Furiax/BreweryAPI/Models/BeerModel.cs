using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreweryAPI.Models
{
	public class BeerModel
	{
		[Key]
		public int BeerId { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int BrewerId { get; set; }
        public BreweryModel Brewer { get; set; }
    }
}
