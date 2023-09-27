using AutoMapper;
using BreweryAPI.DTOs;
using BreweryAPI.Interface;
using BreweryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WholesalerInventoryController: Controller
    {
        private readonly IWholesalerInventoryRepository _wholesalerInventoryRepository;
        private readonly IBeerRepository _beerRepository;
        private readonly IMapper _mapper;
        public WholesalerInventoryController(IWholesalerInventoryRepository wholesalerInventoryRepository, IBeerRepository beerRepository, IMapper mapper)
        {
            _wholesalerInventoryRepository = wholesalerInventoryRepository;
            _beerRepository = beerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<WholesalerInventory>))]
        public IActionResult GetWholesalerInventories()
        {
            var wholesalerInventories = _mapper.Map<List<WholesalerInventoryDTO>>(_wholesalerInventoryRepository.GetWholesalerInventories());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(wholesalerInventories);
        }

        [HttpGet("{wholesalerInventoryId}")]
        [ProducesResponseType(200, Type = typeof(WholesalerInventory))]
        [ProducesResponseType(400)]
        public IActionResult GetWholesalerInventory(int wholesalerInventoryId)
        {
            if (!_wholesalerInventoryRepository.WholesalerInventoryExists(wholesalerInventoryId))
                return NotFound();

            var wholesaler = _mapper.Map<WholesalerInventoryDTO>(_wholesalerInventoryRepository.GetWholesalerInventory(wholesalerInventoryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(wholesaler);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult createWholesalerInventory([FromBody] WholesalerInventoryDTO wholesalerInventoryCreate)
        {
            if (wholesalerInventoryCreate == null)
                return BadRequest(ModelState);

            var beer = _mapper.Map<BeerDTO>(_beerRepository.GetBeer(wholesalerInventoryCreate.BeerId));
             
            if (beer == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var wholesalerInventoryMap = _mapper.Map<WholesalerInventory>(wholesalerInventoryCreate);

            if (_wholesalerInventoryRepository.IsUniqueRecord(wholesalerInventoryMap))
            {
                ModelState.AddModelError("", "Record already exists");
                return StatusCode(422, ModelState);
            }

            if (!_wholesalerInventoryRepository.CreateWholesalerInventory(wholesalerInventoryMap))
            {
                ModelState.AddModelError("", "something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }

        [HttpPut("{wholesaleInventoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult updateWholesalerInventory(int wholesaleInventoryId, [FromBody] WholesalerInventoryDTO updateWholesaleInventory)
        {
            if (updateWholesaleInventory == null)
                return BadRequest(ModelState);

            if (wholesaleInventoryId != updateWholesaleInventory.ItemId)
                return BadRequest(ModelState);

            var beer = _mapper.Map<BeerDTO>(_beerRepository.GetBeer(updateWholesaleInventory.BeerId));

            if (beer == null)
                return BadRequest(ModelState);

            if (!_wholesalerInventoryRepository.WholesalerInventoryExists(wholesaleInventoryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var wholesaleInventoryMap = _mapper.Map<WholesalerInventory>(updateWholesaleInventory);

            if (!_wholesalerInventoryRepository.UpdateWholesalerInventory(wholesaleInventoryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating wholesaleInventory");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{wholesaleInventoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteWholesaleInventory(int wholesaleInventoryId)
        {
            if (!_wholesalerInventoryRepository.WholesalerInventoryExists(wholesaleInventoryId))
            {
                return NotFound();
            }

            var wholesaleInventoryModel = _wholesalerInventoryRepository.GetWholesalerInventory(wholesaleInventoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_wholesalerInventoryRepository.DeleteWholesalerInventory(wholesaleInventoryModel))
            {
                ModelState.AddModelError("", "Something went wrong deleting wholesaleInventory");
            }

            return NoContent();
        }
    }
}
