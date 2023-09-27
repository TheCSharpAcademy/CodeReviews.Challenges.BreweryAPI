namespace BreweryAPI.DTOs
{
    public class BrewerySalesDTO
    {
        public int SalesId { get; set; }
        public int WholeSalerId { get; set; }
        public int BeerId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
