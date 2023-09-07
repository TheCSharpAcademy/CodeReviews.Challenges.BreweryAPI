namespace BreweryAPI.BLL.DataTransferObjects.BrewerySale;

public class BrewerySaleRequestDto
{
    public List<BrewerySaleRequestItemDto> SaleItems { get; set; } = new List<BrewerySaleRequestItemDto>();
}

public class BrewerySaleRequestItemDto
{
    public Guid BeerId { get; set; }
    public int Quantity { get; set; }
}
