using System.ComponentModel.DataAnnotations;

namespace BreweryApi.Models
{
    public class Sales
    {
        [Key]
        public int Id { get; set; }
        public int BreweryId { get; set; }
        public int WholeSalerId {  get; set; }
        public int BeerId { get; set; }
        public int Quantity { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
