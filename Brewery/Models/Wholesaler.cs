namespace BreweryApi.Models
{
    public class Wholesaler
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int StockLimit { get; set; }

        public List<Sales> Sales { get; set; }

        public List<WholesalerStock> Stocks { get; set; }

        public List<int> AllowedBeersId { get; set; }
    }
}
