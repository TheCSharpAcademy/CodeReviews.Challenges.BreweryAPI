namespace BreweryAPI.DTOs
{
    public class WholesalerInventoryDTO
    {
        public int ItemId { get; set; }
        public int WholesalerId { get; set; }
        public int BeerId { get; set; }
        public int Quantity { get; set; }
    }
}
