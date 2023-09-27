using AutoMapper;
using BreweryAPI.DTOs;
using BreweryAPI.Interface;
using BreweryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WholesalerQuoteController: Controller
    {
        private readonly IWholesalerInventoryRepository _wholesalerInventoryRepository;
        private readonly IWholesalerQuoteRepository _wholesalerQuoteRepository;
        private readonly IMapper _mapper;

        public WholesalerQuoteController(IWholesalerInventoryRepository wholesalerInventoryRepository, IWholesalerQuoteRepository wholesalerQuoteRepository, IMapper mapper)
        {
            _wholesalerInventoryRepository = wholesalerInventoryRepository;
            _wholesalerQuoteRepository = wholesalerQuoteRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<WholesalerQuoteModel>))]
        public IActionResult GetWholesalerQuotes()
        {
            var wholesalerQuotes = _mapper.Map<List<WholesalerQuoteDTO>>(_wholesalerQuoteRepository.GetWholesalerQuotes());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(wholesalerQuotes);
        }


        [HttpGet("{wholesalerQuoteId}")]
        [ProducesResponseType(200, Type = typeof(WholesalerQuoteModel))]
        [ProducesResponseType(400)]
        public IActionResult GetWholesalerQuote(int wholesalerQuoteId)
        {
            if (!_wholesalerQuoteRepository.WholesalerQuoteExists(wholesalerQuoteId))
                return NotFound();

            var wholesalerQuoteMap = _mapper.Map<WholesalerQuoteDTO>(_wholesalerQuoteRepository.GetWholesalerQuote(wholesalerQuoteId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(wholesalerQuoteMap);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateWholesalerQuote([FromBody] WholesalerQuoteDTO wholesalerQuoteCreate)
        {
            if (wholesalerQuoteCreate == null)
                return BadRequest(ModelState);

            var wholesalerInventoryRecord = _mapper.Map<WholesalerInventory>(_wholesalerInventoryRepository.SelectRecord(wholesalerQuoteCreate.WholesalerId, wholesalerQuoteCreate.BeerId));

            if (wholesalerInventoryRecord == null)
                return BadRequest(ModelState);

            if(wholesalerQuoteCreate.Quantity > wholesalerInventoryRecord.Quantity)
            {
                return BadRequest(ModelState);
            }
            else
            {
                wholesalerInventoryRecord.Quantity = wholesalerInventoryRecord.Quantity - wholesalerQuoteCreate.Quantity;
            }

            if(wholesalerQuoteCreate.Quantity > 10)
            {
                wholesalerQuoteCreate.TotalPrice = wholesalerQuoteCreate.TotalPrice * 90;
            }
            else if (wholesalerQuoteCreate.Quantity > 20)
            {
                wholesalerQuoteCreate.TotalPrice = wholesalerQuoteCreate.TotalPrice * 80;
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var wholesalerQuoteMap = _mapper.Map<WholesalerQuoteModel>(wholesalerQuoteCreate);

            if (!_wholesalerQuoteRepository.CreateWholesalerQuote(wholesalerQuoteMap))
            {
                ModelState.AddModelError("", "something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            if (!_wholesalerInventoryRepository.UpdateWholesalerInventory(wholesalerInventoryRecord))
            {
                ModelState.AddModelError("", "something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }

        [HttpDelete("{wholesalerQuoteId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteWholesalerQuote(int wholesalerQuoteId)
        {
            if (!_wholesalerQuoteRepository.WholesalerQuoteExists(wholesalerQuoteId))
            {
                return NotFound();
            }

            var wholesalerQuoteToDelete = _wholesalerQuoteRepository.GetWholesalerQuote(wholesalerQuoteId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_wholesalerQuoteRepository.DeleteWholesalerQuote(wholesalerQuoteToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting wholesaler");
            }

            return NoContent();
        }
    }
}
