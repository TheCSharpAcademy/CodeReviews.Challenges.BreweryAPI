namespace BreweryAPI.DTOs
{
    public class WholesalerQuoteDTO
    {
        public int QuoteId { get; set; }
        public string ClientName { get; set; }
        public int WholesalerId { get; set; }
        public int BeerId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
