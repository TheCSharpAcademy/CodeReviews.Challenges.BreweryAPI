using System.ComponentModel.DataAnnotations;

namespace BreweryAPI.Models
{
	public class WholesalerModel
	{
		[Key]
		public int WholesalerId { get; set; }
		public string Name { get; set; }
	}
}
