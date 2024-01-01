namespace BreweryApi.Models
{
    public class WholesalerStock
    {
        public int Id { get; set; }
        public int WholesalerId { get; set; }
        public int BeerId {  get; set; }
        public int StockQuantity { get; set; }
    }
}
