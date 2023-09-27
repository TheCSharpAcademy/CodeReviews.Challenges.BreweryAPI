using AutoMapper;
using BreweryAPI.DTOs;
using BreweryAPI.Interface;
using BreweryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreweryController : Controller
    {
        private readonly IBreweryRepository _breweryRepository;
        private readonly IMapper _mapper;

        public BreweryController(IBreweryRepository breweryRepository, IMapper mapper)
        {
            _breweryRepository = breweryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BreweryModel>))]
        public IActionResult GetBreweries()
        {
            var breweries = _mapper.Map<List<BreweryDTO>>(_breweryRepository.GetBreweries());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(breweries);
        }

        [HttpGet("{breweryId}")]
        [ProducesResponseType(200, Type = typeof(BreweryModel))]
        [ProducesResponseType(400)]
        public IActionResult GetBrewery(int breweryId)
        {
            if (!_breweryRepository.BreweryExists(breweryId))
                return NotFound();

            var brewery = _mapper.Map<BreweryDTO>(_breweryRepository.GetBrewery(breweryId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(brewery);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBrewery([FromBody] BreweryDTO breweryCreate)
        {
            if (breweryCreate == null)
                return BadRequest(ModelState);

            var brewery = _breweryRepository.GetBreweries()
                .Where(b => b.BreweryName.Trim().ToUpper() == breweryCreate.BreweryName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (brewery != null)
            {
                ModelState.AddModelError("", "Brewery already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var breweryMap = _mapper.Map<BreweryModel>(breweryCreate);

            if (!_breweryRepository.CreateBrewery(breweryMap))
            {
                ModelState.AddModelError("", "something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }

        [HttpPut("{breweryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBrewery(int breweryId, [FromBody] BreweryDTO updatedBrewery)
        {
            if (updatedBrewery == null)
                return BadRequest(ModelState);

            if (breweryId != updatedBrewery.BreweryId)
                return BadRequest(ModelState);

            if (!_breweryRepository.BreweryExists(breweryId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var breweryMap = _mapper.Map<BreweryModel>(updatedBrewery);

            if (!_breweryRepository.UpdateBrewery(breweryMap))
            {
                ModelState.AddModelError("", "Something went wrong updating brewery");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{breweryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBrewery(int breweryId)
        {
            if (!_breweryRepository.BreweryExists(breweryId))
            {
                return NotFound();
            }

            var breweryToDelete = _breweryRepository.GetBrewery(breweryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_breweryRepository.DeleteBrewery(breweryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting brewery");
            }

            return NoContent();
        }
    }
}