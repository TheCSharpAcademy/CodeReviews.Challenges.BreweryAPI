namespace BreweryApi.Models.DTOs
{
    public class WholesalerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int StockLimit { get; set; }

        public List<Sales> Sales { get; set; }

        public List<WholesalerStock> Stocks { get; set; }

        public List<int> AllowedBeersId { get; set; }

        public List<string> AllowedBeersNames { get; set; }
    }
}
