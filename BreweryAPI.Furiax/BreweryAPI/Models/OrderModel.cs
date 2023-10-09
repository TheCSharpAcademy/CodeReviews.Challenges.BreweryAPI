namespace BreweryAPI.Models
{
    public class OrderModel
    {
        public decimal Price { get; set; }
        public string Summary { get; set; }
        public int? Discount { get; set; }
        public QuoteModel Quote { get; set; }

    }
}
