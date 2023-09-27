using AutoMapper;
using BreweryAPI.DTOs;
using BreweryAPI.Interface;
using BreweryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : Controller
    {
        private readonly IBeerRepository _beerRepository;
        private readonly IBreweryRepository _breweryRepository;
        private readonly IMapper _mapper;

        public BeerController(IBeerRepository beerRepository, IBreweryRepository breweryRepository, IMapper mapper)
        {
            _breweryRepository = breweryRepository;
            _beerRepository = beerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BeerModel>))]
        public IActionResult GetBeers()
        {
            var beers = _mapper.Map<List<BeerDTO>>(_beerRepository.GetBeers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(beers);
        }

        [HttpGet("singular/{beerId}")]
        [ProducesResponseType(200, Type = typeof(BeerModel))]
        [ProducesResponseType(400)]
        public IActionResult GetBeer(int beerId)
        {
            if (!_beerRepository.BeerExists(beerId))
                return NotFound();

            var beer = _mapper.Map<BeerDTO>(_beerRepository.GetBeer(beerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(beer);
        }

        [HttpGet("{breweryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BeerModel>))]
        [ProducesResponseType(400)]
        public IActionResult GetBeersByBrewery(int breweryId)
        {
            if (!_breweryRepository.BreweryExists(breweryId))
                return NotFound();

            var beersByBrewery = _mapper.Map<List<BeerDTO>>(_beerRepository.GetBeersByBrewery(breweryId));

            if(beersByBrewery.Count == 0)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(beersByBrewery);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBeer([FromBody] BeerDTO beerCreate)
        {
            if (beerCreate == null)
                return BadRequest(ModelState);

            var beer = _beerRepository.GetBeers()
                .Where(b => b.BeerName.Trim().ToUpper() == beerCreate.BeerName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (beer != null)
            {
                ModelState.AddModelError("", "Beer already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var beerMap = _mapper.Map<BeerModel>(beerCreate);

            if (!_beerRepository.CreateBeer(beerMap))
            {
                ModelState.AddModelError("", "something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }

        [HttpPut("{beerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBeer(int beerId, [FromBody] BeerDTO updatedBeer)
        {
            if (updatedBeer == null)
                return BadRequest(ModelState);

            if (beerId != updatedBeer.BeerId)
                return BadRequest(ModelState);

            if (!_beerRepository.BeerExists(beerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var beerMap = _mapper.Map<BeerModel>(updatedBeer);

            if (!_beerRepository.UpdateBeer(beerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating beer");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{beerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBeer(int beerId)
        {
            if (!_beerRepository.BeerExists(beerId))
            {
                return NotFound();
            }

            var beerToDelete = _beerRepository.GetBeer(beerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_beerRepository.DeleteBeer(beerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting beer");
            }

            return NoContent();
        }
    }
}
