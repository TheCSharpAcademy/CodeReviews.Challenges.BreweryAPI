namespace BreweryAPI.BLL.DataTransferObjects.Quote
{
    public class WholesalerQuoteRequestDto
    {
        public List<WholesalerQuoteRequestItemDto>? QuoteItems { get; set; } = new List<WholesalerQuoteRequestItemDto>();
    }

    public class WholesalerQuoteRequestItemDto
    {
        public Guid BeerId { get; set; }
        public int Quantity { get; set; }
    }
}
