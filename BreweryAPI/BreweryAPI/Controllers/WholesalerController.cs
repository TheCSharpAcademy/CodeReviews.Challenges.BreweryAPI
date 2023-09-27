using AutoMapper;
using BreweryAPI.DTOs;
using BreweryAPI.Interface;
using BreweryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WholesalerController: Controller
    {
        private readonly IWholesalerRepository _wholesalerRepository;
        private readonly IMapper _mapper;
        public WholesalerController(IWholesalerRepository wholesalerRepository, IMapper mapper)
        {
            _wholesalerRepository = wholesalerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<WholesalerModel>))]
        public IActionResult GetWholesalers()
        {
            var wholesalers = _mapper.Map<List<WholesalerDTO>>(_wholesalerRepository.GetWholesalers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(wholesalers);
        }

        [HttpGet("{wholesalerId}")]
        [ProducesResponseType(200, Type = typeof(WholesalerModel))]
        [ProducesResponseType(400)]
        public IActionResult GetWholesaler(int wholesalerId)
        {
            if (!_wholesalerRepository.WholesalerExists(wholesalerId))
                return NotFound();

            var wholesaler = _mapper.Map<WholesalerDTO>(_wholesalerRepository.GetWholesaler(wholesalerId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(wholesaler);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateWholesaler([FromBody] WholesalerDTO wholesalerCreate)
        {
            if (wholesalerCreate == null)
                return BadRequest(ModelState);

            var wholesaler = _wholesalerRepository.GetWholesalers()
                .Where(b => b.WholesalerName.Trim().ToUpper() == wholesalerCreate.WholesalerName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (wholesaler != null)
            {
                ModelState.AddModelError("", "Wholesaler already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var wholesalerMap = _mapper.Map<WholesalerModel>(wholesalerCreate);

            if (!_wholesalerRepository.CreateWholesaler(wholesalerMap))
            {
                ModelState.AddModelError("", "something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }

        [HttpPut("{wholesalerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateWholesaler(int wholesalerId, [FromBody] WholesalerDTO updateWholesaler)
        {
            if (updateWholesaler == null)
                return BadRequest(ModelState);

            if (wholesalerId != updateWholesaler.WholesalerID)
                return BadRequest(ModelState);

            if (!_wholesalerRepository.WholesalerExists(wholesalerId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var wholesalerMap = _mapper.Map<WholesalerModel>(updateWholesaler);

            if (!_wholesalerRepository.UpdateWholesaler(wholesalerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating wholesaler");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{wholesalerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteWholesaler(int wholesalerId)
        {
            if (!_wholesalerRepository.WholesalerExists(wholesalerId))
            {
                return NotFound();
            }

            var wholesalerToDelete = _wholesalerRepository.GetWholesaler(wholesalerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_wholesalerRepository.DeleteWholesaler(wholesalerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting wholesaler");
            }

            return NoContent();
        }
    }
}
