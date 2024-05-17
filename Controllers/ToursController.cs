using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tour_API.Data;
using Tour_API.DTOs;
using Tour_API.DTOs.Tours;
using Tour_API.Mappers;
using Tour_API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tour_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly TourContext _context;
        public ToursController(TourContext context)
        {
            _context = context;
        }

        // GET: api/<ToursController>
        [HttpGet]
        public async Task<IActionResult> GetTours()
        {
            var tours = await _context.Tours.ToListAsync();
            /*foreach (var tour in tours)
            {
                tour.Destination = await _context.Destinations.FindAsync(tour.DestinationId);
            }*/
            var toursDto = tours.Select(t => t.ToTourDto());
            return Ok(tours);
        }


        // GET api/<ToursController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var tour = await _context.Tours.FindAsync(id);
            if (tour is null)
                return NotFound();
            return Ok(tour.ToTourDto());
        }

        // POST api/<ToursController>
        [HttpPost("add")]
        public async Task<IActionResult> AddTour([FromBody] CreateTourDto tourDto)
        {
            var newTour = tourDto.FromCreateDtoToTour();
            await _context.Tours.AddAsync(newTour);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new {id = newTour.Id}, newTour.ToTourDto());
        }

        // PUT api/<ToursController>/5
        [HttpPut]
        [Route("edit/{id}")]
        public async Task<IActionResult> EditTour([FromRoute] int id, [FromBody] UpdateTourDto tourDto)
        {
            var updateTour = await _context.Tours.FindAsync(id);
            // check tour exists or not
            if (updateTour is null)
                return NotFound();
            // update tour's params
            updateTour.Title = tourDto.Title;
            updateTour.DestinationId = tourDto.DestinationId;
            updateTour.Categories = tourDto.Categories;
            updateTour.Rating = tourDto.Rating;
            updateTour.Price = tourDto.Price;
            updateTour.Duration = tourDto.Duration;
            updateTour.Thumbnail = tourDto.Thumbnail;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<ToursController>/5
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            // check tour exists or not
            var tour = await _context.Tours.FindAsync(id);
            if (tour is null)
            {
                return NotFound();
            }

            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
