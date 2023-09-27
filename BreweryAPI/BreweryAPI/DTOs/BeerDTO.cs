namespace BreweryAPI.DTOs
{
    public class BeerDTO
    {
        public int BeerId { get; set; }
        public string BeerName { get; set; }
        public decimal Price { get; set; }
        public int BreweryId { get; set; }
    }
}
