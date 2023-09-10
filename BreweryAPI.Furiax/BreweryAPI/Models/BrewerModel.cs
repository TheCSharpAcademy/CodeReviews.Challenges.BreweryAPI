using System.ComponentModel.DataAnnotations;

namespace BreweryAPI.Models
{
	public class BreweryModel
	{
        [Key]
        public int BreweryId { get; set; }
        public string Name { get; set; }
    }
}
