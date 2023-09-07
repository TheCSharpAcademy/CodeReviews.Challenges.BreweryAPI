namespace BreweryAPI.BLL.Helpers
{
    public static class Constants
    {
        public static readonly string NotFoundMessage = "{0} with {1} {2} not found.";
        public static readonly string ConflictMessage = "A duplicate {0} already exists.";
        public static readonly string OrderEmptyMessage = "The order cannot be empty.";
        public static readonly string DuplicatesInOrderMessage = "The order cannot contain duplicates";
        public static readonly string BeerNotSoldByWholesalerMessage = "The beer must be sold by the wholesaler.";
        public static readonly string NotEnoughStockMessage = "The number of beers ordered cannot be greater than the wholesaler's stock.";

        public static readonly decimal OrderAbove10UnitsDiscount = 0.10m;
        public static readonly decimal OrderAbove20UnitsDiscount = 0.20m;
    }
}
