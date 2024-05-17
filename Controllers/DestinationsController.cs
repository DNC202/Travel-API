using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Tour_API.Data;
using Tour_API.DTOs.Destinations;
using Tour_API.DTOs.Tours;
using Tour_API.Mappers;
using Tour_API.Services.DestinationServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tour_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {
       
        private readonly IDestinationService _destinationService;
        public DestinationsController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
            
        }
        // GET: api/<DestinationsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var destinations = await _destinationService.GetAllAsync();
            var destinationDto =  destinations.Select(c => c.ToDestinationDto());
            return Ok(destinationDto);
        }

        // GET api/<DestinationsController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var destination = await _destinationService.GetByIdAsync(id);
            if(destination is null)
                return NotFound();
            return Ok(destination);
        }

        // POST api/<DestinationsController>
        [HttpPost("add")]
        public async Task<IActionResult> Post([FromBody] CreateDestinationDto destinationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var newDestination = destinationDto.FromCreateDtoToDestination();
            await _destinationService.CreateAsync(newDestination);
            return CreatedAtAction(nameof(Get), new { id = newDestination.Id }, newDestination.ToDestinationDto());
        }

        // PUT api/<DestinationsController>/5
        [HttpPut("edit/{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateDestinationDto destinationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var destination = await _destinationService.UpdateAsync(id, destinationDto);
            if (destination is null)
                return NotFound();
            return NoContent();
        }

        // DELETE api/<DestinationsController>/5
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // check tour exists or not
            var destination = await _destinationService.DeleteAsync(id);
            if (destination is null)
                return NotFound();
            return NoContent();
        }
    }
}
