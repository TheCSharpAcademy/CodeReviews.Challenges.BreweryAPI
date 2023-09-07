using BreweryAPI.BLL.DataTransferObjects.Quote;
using BreweryAPI.BLL.Helpers;
using BreweryAPI.BLL.Interfaces;
using BreweryAPI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Controllers
{
    [Route("api/wholesalers")]
    [ApiController]
    public class WholesalersController : ControllerBase
    {
        private readonly IWholesalerService _wholesalerService;

        public WholesalersController(IWholesalerService wholesalerService)
        {
            _wholesalerService = wholesalerService;
        }

        [HttpPost("{id}/quotes")]
        public async Task<IActionResult> GetQuote([FromRoute] Guid id, [FromBody] WholesalerQuoteRequestDto quoteRequestDto)
        {
            ServiceResult<WholesalerQuoteResponseDto> result = await _wholesalerService.GetQuoteAsync(id, quoteRequestDto);
            return result.Success ? Ok(result.Data) : this.FromErrorResult(result);
        }
    }
}
