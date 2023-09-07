using BreweryAPI.BLL.DataTransferObjects.Quote;
using BreweryAPI.BLL.Helpers;

namespace BreweryAPI.BLL.Interfaces
{
    public interface IWholesalerService
    {
        Task<ServiceResult<WholesalerQuoteResponseDto>> GetQuoteAsync(Guid wholesalerId, WholesalerQuoteRequestDto quoteRequestDto);
    }
}
