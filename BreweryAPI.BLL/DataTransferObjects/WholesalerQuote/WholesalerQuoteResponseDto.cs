namespace BreweryAPI.BLL.DataTransferObjects.Quote
{
    public class WholesalerQuoteResponseDto
    {
        public decimal TotalPrice { get; set; }
        public int TotalQuantity { get; set; }
        public decimal Discount { get; set; }
        public List<QuoteResponseItemDto> QuoteItems { get; set; }
    }

    public class QuoteResponseItemDto
    {
        public string BeerName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal { get; set; }
    }
}