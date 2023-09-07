using BreweryAPI.BLL.DataTransferObjects.Quote;
using BreweryAPI.BLL.Helpers;
using BreweryAPI.BLL.Interfaces;

namespace BreweryAPI.BLL.Services
{
    public class DiscountService : IDiscountService
    {
        public decimal GetDiscountPercent(List<QuoteResponseItemDto> quoteItems)
        {
            if (quoteItems.Count > 20)
            {
                return Constants.OrderAbove20UnitsDiscount;
            }
            else if (quoteItems.Count > 10)
            {
                return Constants.OrderAbove10UnitsDiscount;
            }
            else
            {
                return 0m;
            }
        }

        public decimal GetDiscountPrice(decimal discountPercent, List<QuoteResponseItemDto> quoteItems)
        {
            decimal originalPrice = quoteItems.Sum(item => item.Subtotal);
            decimal discountPrice = originalPrice - (discountPercent * originalPrice);
            return discountPrice;
        }
    }
}
