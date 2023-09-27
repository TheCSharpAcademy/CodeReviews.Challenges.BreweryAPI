using AutoMapper;
using BreweryAPI.DTOs;
using BreweryAPI.Interface;
using BreweryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrewerySalesController: Controller
    {
        private readonly IBrewerySalesRepository _brewerySalesRepository;
        private readonly IBeerRepository _beerRepository;
        private readonly IWholesalerInventoryRepository _wholesalerInventoryRepository;
        private readonly IMapper _mapper;
        public BrewerySalesController(IWholesalerInventoryRepository wholesalerInventoryRepository, IBrewerySalesRepository brewerySalesRepository, IBeerRepository beerRepository, IMapper mapper)
        {
            _wholesalerInventoryRepository = wholesalerInventoryRepository;
            _beerRepository = beerRepository;
            _brewerySalesRepository = brewerySalesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BreweryModel>))]
        public IActionResult GetBrewerySales()
        {
            var brewerySales = _mapper.Map<List<BrewerySalesDTO>>(_brewerySalesRepository.GetBrewerySales());
            
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(brewerySales);
        }


        [HttpGet("{brewerySalesId}")]
        [ProducesResponseType(200, Type = typeof(BrewerySalesModel))]
        [ProducesResponseType(400)]
        public IActionResult GetBrewerySale(int brewerySalesId)
        {
            if (!_brewerySalesRepository.BrewerySaleExists(brewerySalesId))
                return NotFound();

            var brewerySalesMap = _mapper.Map<BrewerySalesDTO>(_brewerySalesRepository.GetBrewerySale(brewerySalesId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(brewerySalesMap);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBrewerySale([FromBody] BrewerySalesDTO brewerySaleCreate)
        {
            if (brewerySaleCreate == null)
                return BadRequest(ModelState);

            var beer = _mapper.Map<BeerDTO>(_beerRepository.GetBeer(brewerySaleCreate.BeerId));

            if(beer == null)
                return BadRequest(ModelState);

            if(brewerySaleCreate.TotalPrice != beer.Price * brewerySaleCreate.Quantity)
                return BadRequest(ModelState);

            //Update wholesaler inventory with the quantity of the sale
            var wholesalerInventory = _wholesalerInventoryRepository.SelectRecord(brewerySaleCreate.WholeSalerId, brewerySaleCreate.BeerId);
            
            if(wholesalerInventory != null)
            {
                wholesalerInventory.Quantity = wholesalerInventory.Quantity + brewerySaleCreate.Quantity;
               _wholesalerInventoryRepository.UpdateWholesalerInventory(wholesalerInventory);
            }
            else
            {
                //if the wholesaler buying beer does not have any items to update in their inventory, this will create a new item upon purchase.
                WholesalerInventoryDTO newInventoryRecord = new WholesalerInventoryDTO
                {
                    WholesalerId = brewerySaleCreate.WholeSalerId,
                    BeerId = brewerySaleCreate.BeerId,
                    Quantity = brewerySaleCreate.Quantity
                };

                var InventoryRecordToAdd = _mapper.Map<WholesalerInventory>(newInventoryRecord);

                _wholesalerInventoryRepository.CreateWholesalerInventory(InventoryRecordToAdd);
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var brewerySaleMap = _mapper.Map<BrewerySalesModel>(brewerySaleCreate);

            if (!_brewerySalesRepository.CreateBrewerySale(brewerySaleMap))
            {
                ModelState.AddModelError("", "something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }

        [HttpPut("{brewerySaleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBrewerySale(int brewerySaleId, [FromBody] BrewerySalesDTO updatedBrewerySale)
        {
            if (updatedBrewerySale == null)
                return BadRequest(ModelState);

            if (brewerySaleId != updatedBrewerySale.SalesId)
                return BadRequest(ModelState);

            var beer = _mapper.Map<BeerDTO>(_beerRepository.GetBeer(updatedBrewerySale.BeerId));

            if (beer == null)
                return BadRequest(ModelState);

            if (updatedBrewerySale.TotalPrice != beer.Price * updatedBrewerySale.Quantity)
                return BadRequest(ModelState);

            if (!_brewerySalesRepository.BrewerySaleExists(brewerySaleId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var brewerySaleMap = _mapper.Map<BrewerySalesModel>(updatedBrewerySale);

            if (!_brewerySalesRepository.UpdateBrewerySales(brewerySaleMap))
            {
                ModelState.AddModelError("", "Something went wrong updating brewerySale");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{brewerySaleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBrewerySale(int brewerySaleId)
        {
            if (!_brewerySalesRepository.BrewerySaleExists(brewerySaleId))
            {
                return NotFound();
            }

            var brewerySaleToDelete = _brewerySalesRepository.GetBrewerySale (brewerySaleId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_brewerySalesRepository.DeleteBrewerySales(brewerySaleToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting brewery");
            }

            return NoContent();
        }
    }
}
