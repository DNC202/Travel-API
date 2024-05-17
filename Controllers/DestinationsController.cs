using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Tour_API.Data;
using Tour_API.DTOs.Destinations;
using Tour_API.DTOs.Tours;
using Tour_API.Mappers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tour_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {
        private readonly TourContext _context;
        public DestinationsController(TourContext context)
        {
            _context = context;
        }
        // GET: api/<DestinationsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {   
            var Destinations = await _context.Destinations.Include(c => c.Tours).ToListAsync();
            var destinationDto =  Destinations.Select(c => c.ToDestinationDto());
            return Ok(destinationDto);
        }

        // GET api/<DestinationsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var destination = await _context.Destinations.Include(c => c.Tours).FirstOrDefaultAsync(i => i.Id == id);
            return Ok(destination);
        }

        // POST api/<DestinationsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDestinationDto destinationDto)
        {
            var newDestination = destinationDto.FromCreateDtoToDestination();
            await _context.Destinations.AddAsync(newDestination);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = newDestination.Id }, newDestination.ToDestinationDto());
        }

        // PUT api/<DestinationsController>/5
        [HttpPut]
        [Route("edit/{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateDestinationDto destinationDto)
        {
            var updateDestination = await _context.Destinations.FindAsync(id);
            // check destination exists or not
            if (updateDestination is null)
                return NotFound();
            // update destination's params
            updateDestination.Name = destinationDto.Name;
            updateDestination.Description = destinationDto.Description;
            updateDestination.Image = destinationDto.Image;
            
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<DestinationsController>/5
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // check tour exists or not
            var destination = await _context.Destinations.FindAsync(id);
            if (destination is null)
            {
                return NotFound();
            }

            _context.Destinations.Remove(destination);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
