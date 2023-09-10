using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreweryAPI.Models
{
	public class BreweryModel
	{
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int BreweryId { get; set; }
		[Required]
        public string Name { get; set; }
    }
}
