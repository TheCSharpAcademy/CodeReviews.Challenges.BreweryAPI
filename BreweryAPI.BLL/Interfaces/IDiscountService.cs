using BreweryAPI.BLL.DataTransferObjects.Quote;

namespace BreweryAPI.BLL.Interfaces
{
    public interface IDiscountService
    {
        decimal GetDiscountPercent(List<QuoteResponseItemDto> quoteItems);
        decimal GetDiscountPrice(decimal discountPercent, List<QuoteResponseItemDto> quoteItems);
    }
}
