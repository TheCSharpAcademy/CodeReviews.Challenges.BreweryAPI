using BreweryAPI.BLL.DataTransferObjects.Beer;
using BreweryAPI.BLL.DataTransferObjects.BrewerySale;
using BreweryAPI.BLL.Helpers;
using BreweryAPI.BLL.Interfaces;
using BreweryAPI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Controllers;

[Route("api/breweries")]
[ApiController]
public class BreweriesController : ControllerBase
{
    private readonly IBreweryService _breweryService;

    public BreweriesController(IBreweryService breweryService)
    {
        _breweryService = breweryService;
    }

    [HttpPost("{id}/beers")]
    public async Task<IActionResult> AddBeer([FromRoute] Guid id, [FromBody] BeerCreateDto beerCreateDto)
    {
        ServiceResult<BeerDto> result = await _breweryService.AddBeerAsync(id, beerCreateDto);
        return result.Success ?
            CreatedAtAction("GetBeer", new { id, beerId = result.Data?.Id }, result.Data) :
            this.FromErrorResult(result);
    }

    [HttpDelete("{id}/beers/{beerId}")]
    public async Task<IActionResult> DeleteBeer([FromRoute] Guid id, [FromRoute] Guid beerId)
    {
        ServiceResult result = await _breweryService.DeleteBeerAsync(id, beerId);
        return result.Success ? Ok() : this.FromErrorResult(result);
    }

    [HttpGet("{id}/beers/{beerId}", Name = "GetBeer")]
    public async Task<IActionResult> GetBeer([FromRoute] Guid id, [FromRoute] Guid beerId)
    {
        ServiceResult<BeerDto> result = await _breweryService.GetBeerByIdAsync(id, beerId);
        return result.Success ? Ok(result.Data) : this.FromErrorResult(result);
    }

    [HttpGet("{id}/beers")]
    public async Task<IActionResult> GetBeers([FromRoute] Guid id)
    {
        ServiceResult<List<BeerDto>> result = await _breweryService.GetBeersAsync(id);
        return result.Success ? Ok(result.Data) : this.FromErrorResult(result);
    }

    [HttpPut("{id}/beers/{beerId}")]
    public async Task<IActionResult> UpdateBeer([FromRoute] Guid id, [FromRoute] Guid beerId, [FromBody] BeerUpdateDto beerUpdateDto)
    {
        ServiceResult<BeerDto> result = await _breweryService.UpdateBeerAsync(id, beerId, beerUpdateDto);
        return result.Success ? Ok(result.Data) : this.FromErrorResult(result);
    }

    [HttpPost("{id}/sales/{wholesalerId}")]
    public async Task<IActionResult> ProcessSale([FromRoute] Guid id, [FromRoute] Guid wholesalerId, [FromBody] BrewerySaleRequestDto brewerySaleRequestDto)
    {
        ServiceResult result = await _breweryService.ProcessSaleAsync(id, wholesalerId, brewerySaleRequestDto);
        return result.Success ? Ok() : this.FromErrorResult(result);
    }
}
