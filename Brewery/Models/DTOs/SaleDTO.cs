namespace BreweryApi.Models.DTOs
{
    public class SaleDTO
    {
        public int Id { get; set; }
        public int BreweryId { get; set; }
        public int WholeSalerId { get; set; }
        public int BeerId { get; set; }
        public int Quantity { get; set; }
        public DateTime SaleDate { get; set; }
        public Brewery? Brewery { get; set; }
        public Wholesaler? Wholesaler { get; set; }
        public Beer? Beer { get; set; }

        public static explicit operator SaleDTO( Sales v )
        {
            throw new NotImplementedException();
        }
    }
}
